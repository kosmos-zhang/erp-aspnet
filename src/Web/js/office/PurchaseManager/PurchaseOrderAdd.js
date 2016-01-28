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
    document .getElementById ("spUsedUnitID").style.display="block";
    }
    
    var requestobj = GetRequest(location.search);
    document.getElementById("HiddenURLParams").value = location.search;
    var SourcePage = requestobj['SourcePage'];
    if (SourcePage == "Info") {//列表页面进入
        //返回按钮可用
        $("#btn_back").css("display", "inline");
        $("#HiddenURLParams").val(location.search);
    }
    //    else if(SourcePage == "Desk")
    //    {//个人桌面进入
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

        FillPurchaseOrder(ID);
        document.getElementById("UpdateTitle").style.display = "Block";
        document.getElementById("AddTitle").style.display = "none";
    }
    else {
        GetExtAttr('officedba.PurchaseOrder', null);
    }
    GetFlowButton_DisplayControl();
    //    fnGetExtAttr();
    //    fnPurchaseArriveGetExtAttrOther();
});

var zerosign=false;


function fnPrint()
{
    var ID = $("#ThisID").val();
    if(parseInt(ID) == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("../../../Pages/PrinttingModel/PurchaseManager/PurchaseOrderPrint.aspx?ID="+ID);
}


function BackPurchaseOrder()
{
    var URLParams = document.getElementById("HiddenURLParams").value;
    var requestobj = GetRequest(URLParams);
    var SourcePage = requestobj['SourcePage'];
    if(SourcePage == "Info")
    {//从列表页面过来的        
        //替换ModuleID
        URLParams = URLParams.replace("ModuleID=2041801","ModuleID=2041802");
        URLParams = URLParams.replace("SourcePage=Info","SourcePage=Add");
        window.location.href='PurchaseOrderInfo.aspx'+URLParams;
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
                var OutCount = $("#UsedUnitCount"+i).val();/*采购数量*/
                 
                if (OutCount != '')
                {
                    var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val()); 
                    $("#OrderProductCount"+i).val(tempcount);/*基本数量根据出库数量和比率算出*/ 
                }
            }
        }
    }
    
    
    
}
function SavePurchaseOrder()
{//保存
CountBaseNum();
    //保存前校验
    if(fnCheckAllInfo() == true)
    {
        return;
    }
    if (zerosign)
    {
      if(!confirm("明细中有单价为0的数据，是否保存？"))
        return;
    }
    //获取主表
    var URLParams = fnGetBaseInfo();
    //获取明细
    URLParams += fnGetDetailInfo() + GetExtAttrValue();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseOrderAdd.ashx",
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
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
                if(document.getElementById("HiddenAction").value == "Add")
                {
                    document.getElementById("PurOrderNo_txtCode").value = data.data.split('#$@')[0];
                    document.getElementById('PurOrderNo_txtCode').className='tdinput';
                    document.getElementById('PurOrderNo_txtCode').style.width='90%';
                    document.getElementById('PurOrderNo_ddlCodeRule').style.display='none';
                    //设置编辑模式
                    document.getElementById("HiddenAction").value = "Update";
                    /* 设置编号的显示 */ 
                    //显示采购计划的编号 采购计划编号DIV可见              
//                    document.getElementById("divPurOrderNo").style.display = "block";
                    //设置采购计划编号
                    
//                    document.getElementById("divPurOrderNo").innerHTML = data.data.split('#$@')[0];
                    document.getElementById("ThisID").value = data.data.split('#$@')[1];
//                    //编码规则DIV不可见
//                    document.getElementById("divCodeRule").style.display = "none";
                    
                    //设置源单类型不可改
                    $("#ddlFromType").attr("disabled","disabled");
                    
                }
                else
                {
                    var aaa = data.data.split('#');
                    $("#txtModifiedUserID").attr("value",aaa[0]);
                    $("#txtModifiedUserName").attr("value",aaa[0]);
                    $("#txtModifiedDate").attr("value",aaa[2]);
                }
                if($("#txtBillStatusID").val() == "2")
                {
                    $("#txtBillStatusID").attr("value","3");        
                    $("#txtBillStatusName").attr("value","变更");
                    $("#txtConfirmorID").attr("value","");
                    $("#txtConfirmorName").attr("value","");
                    $("#txtConfirmorDate").attr("value","");
                }
                    
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                GetFlowButton_DisplayControl();
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
            else if(data.sta == 3)
            {//被引用不能编辑
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该单据已被其他单据引用，不能编辑！");
            }
            else if(data.sta == 5)
            {//保存失败
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.data);
            }
                else if (data.sta==0)
            {
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.info);
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        }
        //complete:function(){hidePopup();}//接收数据完毕
    }); 
}

function fnGetBaseInfo()
{
    var ActionOrder = document.getElementById("HiddenAction").value;//状态隐藏域
    var strParams = "ActionOrder=" + ActionOrder;//编辑标识
    if(ActionOrder == "Add")
    {//新增时
        codeRule = document.getElementById("PurOrderNo_ddlCodeRule").value.Trim();
        strParams += "&CodeRuleID=" + document.getElementById("PurOrderNo_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            strParams += "&PurOrderNo=" + document.getElementById("PurOrderNo_txtCode").value.Trim();
        }
    }
    else if(ActionOrder == "Update")
    {//此处有些问题，回头处理
        strParams += "&PurOrderNo=" + document.getElementById("PurOrderNo_txtCode").value.Trim();
        strParams += "&ID="+document.getElementById("ThisID").value.Trim();
    }
       strParams += "&ProjectID=" + escape(document.getElementById("hidProjectID").value.Trim());
    strParams += "&Title=" + escape(document.getElementById("txtTitle").value.Trim());
    strParams += "&PurchaseType=" + document.getElementById("ddlTypeID_ddlCodeType").value.Trim();
    strParams += "&PurchaserID=" + document.getElementById("txtPurchaserID").value.Trim();
    strParams += "&DeptID=" + document.getElementById("txtDeptID").value.Trim();
    strParams += "&TakeType=" + document.getElementById("ddlTakeType_ddlCodeType").value.Trim();
    
    strParams += "&FromType=" + document.getElementById("ddlFromType").value.Trim();    
//    strParams += "&PurchaseDate=" + document.getElementById("txtPurchaseDate").value;
    strParams += "&ProviderID=" + document.getElementById("txtProviderID").value.Trim();
    strParams += "&ProviderBillID=" + document.getElementById("txtProviderBillID").value.Trim();
    strParams += "&CurrencyTypeID=" + document.getElementById("CurrencyTypeID").value.Trim();
    
    strParams += "&ExchangeRate=" + document.getElementById("txtExchangeRate").value.Trim();    
    strParams += "&CarryType=" + document.getElementById("ddlCarryType_ddlCodeType").value.Trim();
    strParams += "&PayType=" + document.getElementById("ddlPayType_ddlCodeType").value.Trim();
    strParams += "&MoneyType=" + document.getElementById("ddlMoneyType_ddlCodeType").value.Trim();
    
    strParams += "&OurDelegate=" + document.getElementById("txtOurDelegateID").value.Trim();
    strParams += "&TheyDelegate=" + escape(document.getElementById("txtTheyDelegate").value.Trim());
    strParams += "&OrderDate=" + document.getElementById("txtOrderDate").value.Trim();
    
    
    strParams += "&IsAddTax=" + ((document.getElementById("chkIsAddTax").checked == true)?1:0);
    
    //合计信息
    strParams += "&CountTotal=" + document.getElementById("txtCountTotal").value.Trim();    
    strParams += "&TotalPrice=" + document.getElementById("txtTotalPrice").value.Trim();
    strParams += "&TotalTax=" + document.getElementById("txtTotalTax").value.Trim();
    strParams += "&TotalFee=" + document.getElementById("txtTotalFee").value.Trim();
    strParams += "&Discount=" + document.getElementById("txtDiscount").value.Trim();
    
    strParams += "&DiscountTotal=" + document.getElementById("txtDiscountTotal").value.Trim();
    strParams += "&RealTotal=" + document.getElementById("txtRealTotal").value.Trim();
    strParams += "&OtherTotal=" + document.getElementById("txtOtherTotal").value.Trim();
    
    //备注信息
    strParams += "&CreatorID=" + document.getElementById("txtCreatorID").value.Trim();    
    strParams += "&CreateDate=" + document.getElementById("txtCreateDate").value.Trim();

    strParams += "&BillStatusID=" + document.getElementById("txtBillStatusID").value.Trim();
    strParams += "&ConfirmorID=" + document.getElementById("txtConfirmorID").value.Trim();
    strParams += "&ConfirmorDate=" + document.getElementById("txtConfirmorDate").value.Trim();
    
    strParams += "&ModifiedUserID=" + escape(document.getElementById("txtModifiedUserID").value.Trim());    
    strParams += "&ModifiedDate=" + document.getElementById("txtModifiedDate").value.Trim();
    strParams += "&CloserID=" + document.getElementById("txtCloserID").value.Trim();
    strParams += "&CloseDate=" + escape(document.getElementById("txtCloseDate").value.Trim());
    strParams += "&Remark=" + escape(document.getElementById("txtRemark").value.Trim());
    strParams += "&CanUserID=" + escape(document.getElementById("txtCanUserID").value.Trim());
    strParams += "&CanUserName=" + escape(document.getElementById("txtCanUserName").value.Trim());
    //附件
    strParams += "&Attachment=" + escape($("#hfPageAttachment").val().replace(/\\/g,"\\\\"));//附件
    
    return strParams;
}

function fnGetDetailInfo()
{
    var strParams = "";
    var signFrame = findObj("DetailTable",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {   
        
        for (i = 1; i < signFrame.rows.length; i++) 
        {
            if (signFrame.rows[i].style.display != "none") 
            {
                strParams +="&SortNo"+DetailLength+"="+document.getElementById("DtlSortNo" + i).innerHTML;
                strParams +="&FromBillID"+DetailLength+"="+document.getElementById("OrderFromBillID" + i).value.Trim();
                strParams +="&FromBillNo"+DetailLength+"="+document.getElementById("OrderFromBillNo" + i).value.Trim();
                strParams +="&FromLineNo"+DetailLength+"="+document.getElementById("OrderFromLineNo" + i).value.Trim();
                strParams +="&ProductID"+DetailLength+"="+document.getElementById("OrderProductID" + i).value.Trim();
                strParams +="&ProductNo"+DetailLength+"="+document.getElementById("OrderProductNo" + i).value.Trim();
                   if($("#HiddenMoreUnit").val()=="True")
                 {
                   strParams += "&UsedPrice"+DetailLength+"="+escape($("#UsedPrice"+i+"").val());
                   strParams += "&UsedUnitCount"+DetailLength+"="+escape($("#UsedUnitCount"+i+"").val());
                  var ExRate= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString();
                   var UsedUnitID= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[0].toString();
                strParams += "&ExRate"+DetailLength+"="+escape( ExRate);
                strParams += "&UsedUnitID"+DetailLength+"="+escape(UsedUnitID);
                 }
                strParams +="&ProductName"+DetailLength+"="+escape(document.getElementById("OrderProductName" + i).value.Trim());
                strParams +="&ProductCount"+DetailLength+"="+document.getElementById("OrderProductCount" + i).value.Trim();
                strParams +="&UnitID"+DetailLength+"="+document.getElementById("OrderUnitID" + i).value.Trim();
                strParams +="&UnitPrice"+DetailLength+"="+document.getElementById("OrderUnitPrice" + i).value.Trim();
                strParams +="&TaxPrice"+DetailLength+"="+document.getElementById("OrderTaxPrice" + i).value.Trim();
                
//                strParams +="&Discount"+DetailLength+"="+document.getElementById("OrderDiscount" + i).value.Trim();
                strParams +="&TaxRate"+DetailLength+"="+document.getElementById("OrderTaxRate" + i).value.Trim();
                strParams +="&TotalFee"+DetailLength+"="+document.getElementById("OrderTotalFee" + i).value.Trim();
                strParams +="&TotalPrice"+DetailLength+"="+document.getElementById("OrderTotalPrice" + i).value.Trim();
                strParams +="&TotalTax"+DetailLength+"="+document.getElementById("OrderTotalTax" + i).value.Trim();
                
                strParams +="&RequireDate"+DetailLength+"="+document.getElementById("OrderRequireDate" + i).value.Trim();
                strParams +="&Remark"+DetailLength+"="+escape(document.getElementById("OrderRemark" + i).value.Trim());
                
                
                
                
                
                
                DetailLength++;
            }
        }
    }
    strParams +="&DetailLength="+DetailLength+"";
    return strParams;
}


//确认时需要传递的明细
function fnGetDtlCon()
{
    var strParams = "";
    var signFrame = findObj("DetailTable",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {   
        
        for (i = 1; i < signFrame.rows.length; i++) 
        {
            if (signFrame.rows[i].style.display != "none") 
            {
                strParams +="&SortNo"+DetailLength+"="+document.getElementById("DtlSortNo" + i).innerHTML;
                strParams +="&FromBillID"+DetailLength+"="+document.getElementById("OrderFromBillID" + i).value.Trim();
                strParams +="&FromBillNo"+DetailLength+"="+document.getElementById("OrderFromBillNo" + i).value.Trim();
                strParams +="&FromLineNo"+DetailLength+"="+document.getElementById("OrderFromLineNo" + i).value.Trim();
                strParams +="&ProductID"+DetailLength+"="+document.getElementById("OrderProductID" + i).value.Trim();
                strParams += "&ProductCount" + DetailLength + "=" + document.getElementById("OrderProductCount" + i).value.Trim();
               
            }
        }
    }
}

function ChangeCurreny()
{//选择币种
 
    var IDExchangeRate = document.getElementById("ddlCurrencyType").value.Trim();
    document.getElementById("CurrencyTypeID").value = IDExchangeRate.split('_')[0];
    document.getElementById("txtExchangeRate").value = IDExchangeRate.split('_')[1];
    var Rate = document.getElementById("txtExchangeRate").value.Trim();
    
    var signFrame = findObj("DetailTable", document);
    for (i = 1; i < signFrame.rows.length; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        { 
        if (Rate==0)
        { 
          if($("#HiddenMoreUnit").val()=="True")
                 {
            $("#UsedPrice"+i).val((parseFloat(0)).toFixed($("#HiddenPoint").val()));
            }
            else
            {
                $("#OrderUnitPrice"+i).val((parseFloat(0)).toFixed($("#HiddenPoint").val()));
            }
            $("#OrderTaxRate"+i).val($("#OrderTaxRateHide"+i).val());
            $("#OrderTaxPrice"+i).val((parseFloat(0)).toFixed($("#HiddenPoint").val()));
        }
        else
        {
           var unitPrice=0;
         if($("#HiddenMoreUnit").val()=="True")
                 {
         unitPrice=$("#UsedPricHid"+i).val();
           $("#UsedPrice"+i).val(    (parseFloat(unitPrice)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
            }
            else
            {
               unitPrice=$("#OrderUnitPriceHid"+i).val();
                 $("#OrderUnitPrice"+i).val(    (parseFloat(unitPrice)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
            }
            
       
          var TaxPriceHide=$("#OrderTaxPriceHide"+i).val();
        
          $("#OrderTaxRate"+i).val($("#OrderTaxRateHide"+i).val());
          $("#OrderTaxPrice"+i).val(  (parseFloat(TaxPriceHide)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
       }
            
            
            
            
        }
    }
    fnMergeDetail();
}
function SelectSource()
{
    
    
    var SourceType = document.getElementById("ddlFromType").value.Trim();
    var ProviderID = document.getElementById("txtProviderID").value.Trim();
    
    
    switch(SourceType)
    {
        case '0':
        {//无来源
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择源单类型！");            
            break;
        }
        case '1':
        {//来源采购申请
            popPurchaseApplyUC.ShowList('Order');
            break;
        }
        case '2':
        {//来源采购计划            
            popPurchasePlanUC.ShowList("Order",ProviderID);
            break;
        }
        case '3':
        {//来源询价单   
            if(ProviderID == "")
                ProviderID = 0;  
            var CurrencyTypeID = document.getElementById("CurrencyTypeID").value.Trim();
            var Rate = document.getElementById("txtExchangeRate").value.Trim();
            var signFrame = findObj("DetailTable",document);       
            var totalSort = 0;//明细行数
            for( var i = 1; i<signFrame.rows.length;i++ )
            {
                var rowID = i;
                if (signFrame.rows[rowID].style.display!="none")
                {
                   totalSort++;
                   break;
                }
            }
            if(totalSort == 0)
            {
                Rate = 0;
            }       
            popAskPrice.ShowList("Order",ProviderID,CurrencyTypeID,Rate);
            break;
        }
        case '4':
        {//来源采购合同     
            var CurrencyTypeID = document.getElementById("CurrencyTypeID").value.Trim();    
            popPurchaseContractUC2.ShowList(CurrencyTypeID,ProviderID);
            break;
        }
    }
}

function fnChangeSource(Source)
{
    //清空明细
    DeleteAll();
    
    if(Source == "0")
    {//无来源时从源单选择明细不可用
        $("#imgGetDtl").css("display", "none");
        $("#imgUnGetDtl").css("display", "inline");
    }
    else
    {
        $("#imgGetDtl").css("display", "inline");
        $("#imgUnGetDtl").css("display", "none");
    }
//    //重置采购类别
//    document.getElementById("ddlTypeID_ddlCodeType").value = ""; 
    //采购类别可选
    $("#ddlTypeID_ddlCodeType").removeAttr("disabled");   
//    //重置交货方式
//    document.getElementById("ddlTakeType_ddlCodeType").value = ""; 
    //交货方式可选
    $("#ddlTakeType_ddlCodeType").removeAttr("disabled");   
//    //重置运送方式
//    document.getElementById("ddlCarryType_ddlCodeType").value = "";
    //运送方式可选
    $("#ddlCarryType_ddlCodeType").removeAttr("disabled");   
//    //重置结算方式
//    document.getElementById("ddlPayType_ddlCodeType").value = "";
    //结算方式可选
    $("#ddlPayType_ddlCodeType").removeAttr("disabled");  
//    //重置交付方式
//    document.getElementById("ddlMoneyType_ddlCodeType").value = "";
    //交付方式可选
    $("#ddlMoneyType_ddlCodeType").removeAttr("disabled"); 
    $("#txtProviderName").removeAttr("disabled");
    $("#ddlCurrencyType").removeAttr("disabled");
}
function DeleteAll()
{//切换源单类型时清空明细
    var signFrame = document.getElementById("DetailTable");  
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=signFrame.rows.length-1;i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    findObj("DetailCount",document).value = 1;
    $("#txtProviderName").removeAttr("disabled");//供应商可选
    fnMergeDetail();
}

//计算各种合计信息
function fnMergeDetail() 
{
try{
 
    var CountTotal = 0; //数量合计
    var TotalPrice = 0; //金额合计
    var Tax = 0; //税额合计
    var TotalFee = 0; //含税金额合计
    var Discount = $("#txtDiscount").val(); //整单折扣
    var DiscountTotal = 0; //折扣金额
    var RealTotal = 0; //折后含税金额
    
    var signFrame = findObj("DetailTable", document);
    for (i = 1; i < signFrame.rows.length; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        {
            var rowid = i;
            var pCountDetail =0;
                if($("#HiddenMoreUnit").val()=="True")
             {
                   pCountDetail=($("#UsedUnitCount" + rowid).val()=="")?0:$("#UsedUnitCount" + rowid).val(); //实际数量
               var EXRate = $("#SignItem_TD_UnitID_Select"+rowid).val().split('|')[1].toString(); /*比率*/
                   $("#OrderProductCount" + rowid).attr("value",(parseFloat (pCountDetail *EXRate).toFixed($("#HiddenPoint").val())));     
                   
            }
            else
            {
            
                   pCountDetail=   ($("#OrderProductCount" + rowid).val()=="")?0:$("#OrderProductCount" + rowid).val(); //数量
             }
            var UnitPriceDetail =0;
              if($("#HiddenMoreUnit").val()=="True")
               {
                   UnitPriceDetail=    ($("#UsedPrice" + rowid).val()=="")?0:$("#UsedPrice" + rowid).val(); //单价
               }
               else
               {
                    UnitPriceDetail=    ($("#OrderUnitPrice" + rowid).val()=="")?0:$("#OrderUnitPrice" + rowid).val(); //单价
            
               }
            
            
            

        //判断是否是增值税，不是增值税含税价始终等于单价
        if(!document.getElementById('chkIsAddTax').checked)
        {
            $("#OrderTaxPrice" + rowid).val(($("#OrderUnitPrice" + rowid).val()=="")?0:$("#OrderUnitPrice" + rowid).val());
        }
            var TaxPriceDetail = ($("#OrderTaxPrice" + rowid).val()=="")?0:$("#OrderTaxPrice" + rowid).val();; //含税价
//            var DiscountDetail = ($("#OrderDiscount" + rowid).val()=="")?0:$("#OrderDiscount" + rowid).val();; //折扣
            var TaxRateDetail = ($("#OrderTaxRate" + rowid).val()=="")?0:$("#OrderTaxRate" + rowid).val();; //税率
            
             
                    
            var TotalFeeDetail =  (parseFloat(TaxPriceDetail * pCountDetail )).toFixed($("#HiddenPoint").val()); //含税金额
            var TotalPriceDetail =  (parseFloat(UnitPriceDetail * pCountDetail )).toFixed($("#HiddenPoint").val()); //金额
            var TotalTaxDetail =  (parseFloat(TotalFeeDetail * TaxRateDetail/100 )).toFixed($("#HiddenPoint").val()); //税额
            
            $("#OrderTotalFee" + rowid).val(   (parseFloat(TotalFeeDetail )).toFixed($("#HiddenPoint").val())); //含税金额
            $("#OrderTotalPrice" + rowid).val((parseFloat(TotalPriceDetail )).toFixed($("#HiddenPoint").val())); //金额
            $("#OrderTotalTax" + rowid).val((parseFloat(TotalPriceDetail*TaxRateDetail/100 )).toFixed($("#HiddenPoint").val())); //税额
            TotalPrice += parseFloat(TotalPriceDetail);
            Tax += parseFloat(TotalTaxDetail);
            TotalFee += parseFloat(TotalFeeDetail);
            CountTotal += parseFloat(pCountDetail);
            DiscountTotal += 0;
        }
    }
          
    $("#txtTotalPrice").val( (parseFloat(TotalPrice )).toFixed($("#HiddenPoint").val()));//金额合计
    $("#txtTotalTax").val( (parseFloat(Tax )).toFixed($("#HiddenPoint").val()));//税额合计
    $("#txtTotalFee").val(  (parseFloat(TotalFee )).toFixed($("#HiddenPoint").val()));//含税金额合计
    $("#txtCountTotal").val((parseFloat(CountTotal )).toFixed($("#HiddenPoint").val())); //数量合计
    DiscountTotal += TotalFee*(100-Discount)/100
    $("#txtDiscountTotal").val(  (parseFloat(DiscountTotal )).toFixed($("#HiddenPoint").val())); //折扣金额
    $("#txtRealTotal").val(  (parseFloat(TotalFee * Discount / 100 )).toFixed($("#HiddenPoint").val())); //折后含税金额
    
    
    }
    catch (Error )
    {}
}

function fnChangeAddTax()
{//改变是否为增值税
    var isAddTax = document.getElementById("chkIsAddTax").checked;
    
    if(isAddTax == true)
    {//是增值税则
        document.getElementById("AddTax").innerHTML = "是增值税";
        var signFrame = findObj("DetailTable",document);
        if((typeof(signFrame) != "undefined")&&(signFrame !=null))
        {
            var ck = document.getElementsByName("Dtlchk");
            for( var i = 0; i<ck.length;i++ )
            {
                var rowID = i+1;
                if(signFrame.rows[rowID].style.display!="none")
                {
                
                 
                    document.getElementById("OrderTaxPrice"+rowID).value= ( parseFloat(document.getElementById("OrderTaxPriceHide"+rowID).value.Trim())/parseFloat(document.getElementById("txtExchangeRate").value.Trim())).toFixed($("#HiddenPoint").val()); //含税价等于隐藏域的含税价
                    document.getElementById("OrderTaxRate"+rowID).value=document.getElementById("OrderTaxRateHide"+rowID).value.Trim();//税率等于隐藏域的税率
                }
            }
        }
    }
    else
    {
        document.getElementById("AddTax").innerHTML = "非增值税";
        var signFrame = findObj("DetailTable",document);
        if((typeof(signFrame) != "undefined")&&(signFrame !=null))
        {
            var ck = document.getElementsByName("Dtlchk");
            for( var i = 0; i<ck.length;i++ )
            {
                var rowID = i+1;
                if(signFrame.rows[rowID].style.display!="none")
                {
                    document.getElementById("OrderTaxPrice"+rowID).value=document.getElementById("OrderUnitPrice"+rowID).value.Trim() //含税价等于单价
                    document.getElementById("OrderTaxRate"+rowID).value=0;//税率等于0
                }
            }
        }
    }
    fnMergeDetail();
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
        var codeRule = document.getElementById("PurOrderNo_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            var PurOrderNo = document.getElementById("PurOrderNo_txtCode").value.Trim();
            //编号必须输入
            if (PurOrderNo == "")
            {
                isErrorFlag = true;
                fieldText += "订单编号|";
   		        msgText += "请输入订单编号|";
            }
            else if(!CodeCheck(PurOrderNo))
            {
                isErrorFlag = true;
                fieldText = fieldText + "编号|";
	            msgText = msgText +  "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
            }
            else if(strlen(PurOrderNo)>50)
            {
                isErrorFlag = true;
                fieldText += "订单编号|";
                msgText += "订单编号不能长于50|";
            }
        }    
    } 
    //主题不空
    if (document.getElementById("txtTitle").value.Trim() == "")
    {
//        isErrorFlag = true;
//        fieldText += "主题|";
//        msgText += "请输入主题|";
    }
    else if(strlen(document.getElementById("txtTitle").value.Trim())>100)
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "主题不能长于100|";
    }
//    //采购类别不空
//    if(document.getElementById("ddlTypeID_ddlCodeType").value.Trim() == "")
//    {
//        isErrorFlag = true;
//        fieldText += "采购类别|";
//        msgText += "请选择采购类别|";
//    }
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
    if (document.getElementById("UserPurchaserName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "采购员|";
        msgText += "请选择采购员|";
    }
    //部门不为空
    if (document.getElementById("txtDeptID").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "部门|";
        msgText += "请选择部门|";
    }
//    //下单日期符合格式
//    if (document.getElementById("txtPurchaseDate").value == ""||!isDate(document.getElementById("txtPurchaseDate").value))
//    {
//        isErrorFlag = true;
//        fieldText += "下单日期|";
//        msgText += "请输入正确的下单日期|";
//    }
//    //供方订单号不为空
//    if (document.getElementById("txtProviderBillID").value == "")
//    {
//        isErrorFlag = true;
//        fieldText += "供方订单号|";
//        msgText += "请输入供方订单号|";
//    }
    //对方代表长度不大于50
    if (strlen(document.getElementById("txtProviderBillID").value.Trim())>100)
    {
        isErrorFlag = true;
        fieldText += "供方订单号|";
        msgText += "供方订单号长度不大于100|";
    }
    //我方代表不为空
    if (document.getElementById("UserOurDelegate").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "我方代表|";
        msgText += "请输入我方代表|";
    }
//    //对方代表不为空
//    if (document.getElementById("txtTheyDelegate").value == "")
//    {
//        isErrorFlag = true;
//        fieldText += "对方代表|";
//        msgText += "请输入对方代表|";
//    }
    //对方代表长度不大于50
    if (strlen(document.getElementById("txtTheyDelegate").value.Trim())>50)
    {
        isErrorFlag = true;
        fieldText += "对方代表|";
        msgText += "对方代表长度不大于50|";
    }
    //签单日期符合格式
    if (document.getElementById("txtOrderDate").value.Trim() == ""||!isDate(document.getElementById("txtOrderDate").value.Trim()))
    {
        isErrorFlag = true;
        fieldText += "签单日期|";
        msgText += "请输入正确的签单日期|";
    }
    var RemarkLen =  strlen($("#txtRemark").val());
    if(RemarkLen>200)
    {
        isErrorFlag = true;
        fieldText += "备注|";
        msgText += "备注长度不能长于200|";
    }

    var signFrame = findObj("DetailTable",document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        var DtlSCnt = findObj("DetailCount",document);
        var RealCount = 0;
        for(var i=1;i<parseInt(DtlSCnt.value);++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                RealCount++;
                //明细中物品不能为空
                if (document.getElementById("OrderProductNo"+i).value.Trim() == "")
                {
                    isErrorFlag = true;
                    fieldText += "物品|";
                    msgText += "请输入物品|";
                }
                //明细中数量符合格式
                var ProductCount = document.getElementById("OrderProductCount"+i).value.Trim();
                if (document.getElementById("OrderProductCount"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("OrderProductCount"+i).value.Trim(),14,$("#HiddenPoint").val()))
                {
                    isErrorFlag = true;
                    fieldText += "采购数量|";
                    msgText += "请输入正确的采购数量|";
                }
                else
                {
                
                if (document.getElementById("OrderProductCount"+i).value.Trim() >0)
                {
                }
                else
                {
                            isErrorFlag = true;
                    fieldText += "采购数量|";
                    msgText += " 采购数量需大于零|";
                
                
                }
                
                }
                //单价符合格式
 
                   var UnitPrice='';
                   if($("#HiddenMoreUnit").val()=="True")
               {
                   UnitPrice=    ($("#UsedPrice" + i).val()=="")?0:$("#UsedPrice" + i).val(); //单价
               }
               else
               {
                    UnitPrice=    ($("#OrderUnitPrice" + i).val()=="")?0:$("#OrderUnitPrice" + i).val(); //单价
            
               }
               
       
                if (document.getElementById("OrderUnitPrice"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("OrderUnitPrice"+i).value.Trim(),14,$("#HiddenPoint").val()))
                {
                    isErrorFlag = true;
                    fieldText += "单价|";
                    msgText += "请输入正确的单价|";
                }
                else      if (UnitPrice=="0.00"| parseInt(UnitPrice,10) ==0)
                { 
                zerosign=true ;
                }
                else
                {
                zerosign=false  ;
                }
                //含税价符合格式
                var TaxPrice = document.getElementById("OrderTaxPrice"+i).value.Trim();
                if (document.getElementById("OrderTaxPrice"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("OrderTaxPrice"+i).value.Trim(),22,$("#HiddenPoint").val()))
                {
                    isErrorFlag = true;
                    fieldText += "含税价|";
                    msgText += "请输入正确的含税价|";
                }
                //折扣  
//                var DetailDisCount = document.getElementById("OrderDiscount"+i).value.Trim();
//                if (document.getElementById("OrderDiscount"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("OrderDiscount"+i).value.Trim(),12,4))
//                {
//                    isErrorFlag = true;
//                    fieldText += "折扣|";
//                    msgText += "请输入正确的折扣|";
//                }   
                //交货日期
                if (document.getElementById("OrderRequireDate"+i).value.Trim() == ""||!isDate(document.getElementById("OrderRequireDate"+i).value.Trim()))
                {
                    isErrorFlag = true;
                    fieldText += "交货日期|";
                    msgText += "请输入正确的交货日期|";
                }
                if(strlen($("#OrderRemark"+i).val()) > 200)
                {
                    isErrorFlag = true;
                    fieldText += "明细备注|";
                    msgText += "明细备注长度不大于200|";
                }
            }
        }
        if(RealCount == 0)
        {
            isErrorFlag = true;
            fieldText += "采购订单明细|";
            msgText += "请输入采购订单明细|";
        }
    }
    else
    {
        isErrorFlag = true;
        fieldText += "采购订单明细|";
        msgText += "请输入采购订单明细|";
    }

    var isRepeat = false;
    /*验证明细中是否存在重复记录*/
    var rowlength = signFrame.rows.length;
    for (var i = 1; i < rowlength - 1; i++) {
        if (signFrame.rows[i].style.display != "none") {
            for (var j = i + 1; j < rowlength; j++) {
                var ProductNoi = "OrderProductID" + i;
                var ProductNoj = "OrderProductID" + j;
                if (signFrame.rows[j].style.display != "none") {
                    if (document.getElementById(ProductNoi).value == document.getElementById(ProductNoj).value) {
                        if (!isRepeat) {
                            isErrorFlag = true;
                            fieldText = fieldText + "采购订单明细|";
                            msgText = msgText + "明细中不允许存在重复记录|";
                            isRepeat = true;
                            break;
                        }
                    }
                }
            }
        }
    }
    
    
    if(isErrorFlag == true)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isErrorFlag;
}

function fnSelectAll()
{
    $.each($("#DetailTable :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//判断是否有相同记录有返回true，没有返回false
function OrdertExistFrom(frombillno,fromLineno)
{
    var signFrame = document.getElementById("DetailTable");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var frombillid = document.getElementById("OrderFromBillNo"+i).value.Trim();
        var fromlineno = document.getElementById("OrderFromLineNo"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(frombillid==frombillno)&&(fromlineno == fromLineno))
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
  
                          var Index = AddSignRow();  
                        $("#OrderProductID"+Index).attr("value",item.ID);
                        $("#OrderProductNo"+Index).attr("value",item.ProdNo);
                        $("#OrderProductName"+Index).attr("value",item.ProductName);
                        $("#OrderUnitID"+Index).attr("value",item.UnitID);
                        $("#OrderUnitName"+Index).attr("value",item.CodeName); 
                        $("#OrderSpecification"+Index).attr("value",item.Specification);
                        $("#DtlSColor" + Index).attr("value", item.ColorName);
                        $("#OrderUnitPrice"+Index).attr("value", (parseFloat(item.TaxBuy)).toFixed($("#HiddenPoint").val()));//去税进价
                         $("#OrderTaxPrice"+Index).attr("value",   (parseFloat(item.StandardBuy)).toFixed($("#HiddenPoint").val()));//含税进价 txtTotalFee
                         $("#OrderTaxRate"+Index).attr("value",(parseFloat(item.InTaxRate)).toFixed($("#HiddenPoint").val()));//进项税率
//                              var Rate = $("#txtExchangeRate").val()
                              
                         $("#OrderUnitPriceHide"+Index).attr("value",(parseFloat(item.TaxBuy)).toFixed($("#HiddenPoint").val()));
                         $("#OrderTaxPriceHide"+Index).attr("value", (parseFloat(item.StandardBuy)).toFixed($("#HiddenPoint").val()));
                         $("#OrderTaxRateHide"+Index).attr("value",(parseFloat(item.InTaxRate)).toFixed($("#HiddenPoint").val()));
                         
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
function FillApplyContent(rowID,UnitPrice,hanshuiPrice,ProductCount,UsedUnitCount,UsedUnitID,UsedPrice,sssign)
{ 



document .getElementById ("OrderUnitPrice"+rowID ).value=(parseFloat(UnitPrice) ).toFixed($("#HiddenPoint").val());
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
  
             $("#OrderTaxPriceHide"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#OrderTaxPrice"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
             if (UsedUnitCount!='')
             {
                $("#UsedUnitCount"+rowID).attr("value", (parseFloat(UsedUnitCount) ).toFixed($("#HiddenPoint").val()));
             }
             else
             {
                  $("#UsedUnitCount"+rowID).attr("value", (parseFloat(ProductCount)/parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
                  }
              var planCount = $("#UsedUnitCount"+rowID).val();/*采购数量*/

           if (planCount !='')
           {
        
           var totalPrice=parseFloat(planCount*UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
           var taxRate=$("#OrderTaxRate"+rowID).val();
   
           if (taxRate=="")
           {
 
               $("#OrderTaxRate"+rowID).attr("value", (parseFloat("0")).toFixed($("#HiddenPoint").val()));
           taxRate=0;
           }
           var TotalFee=parseFloat(planCount*hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
             var TotalTax=parseFloat(totalPrice*taxRate /100).toFixed($("#HiddenPoint").val());
            $("#OrderTotalTax"+rowID).attr("value", (parseFloat(TotalTax)).toFixed($("#HiddenPoint").val()));
            $("#OrderTotalFee"+rowID).attr("value", (parseFloat(TotalFee)).toFixed($("#HiddenPoint").val()));
            $("#OrderTotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
           
    
          
           }
                 // fnMergeDetail();
}
function FillContent(rowID,UnitPrice,hanshuiPrice)
{ 
 

document .getElementById ("OrderUnitPrice"+rowID ).value=(parseFloat(UnitPrice) ).toFixed($("#HiddenPoint").val());

 var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
           $("#UsedPrice"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
           
           $("#UsedPricHid"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
             $("#OrderTaxPriceHide"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#OrderTaxPrice"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              var planCount = $("#UsedUnitCount"+rowID).val();/*采购数量*/

           if (planCount !='')
           {
           var tempcount=parseFloat(planCount*EXRate).toFixed($("#HiddenPoint").val());
           var totalPrice=parseFloat(planCount*UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
           var taxRate=$("#OrderTaxRate"+rowID).val();
           var TotalFee=parseFloat(planCount*hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
             var TotalTax=parseFloat(totalPrice*taxRate /100).toFixed($("#HiddenPoint").val());
            $("#OrderTotalTax"+rowID).attr("value", (parseFloat(TotalTax)).toFixed($("#HiddenPoint").val()));
            $("#OrderTotalFee"+rowID).attr("value", (parseFloat(TotalFee)).toFixed($("#HiddenPoint").val()));
            $("#OrderTotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
           
          $("#OrderProductCount"+rowID).attr("value", (parseFloat(tempcount)).toFixed($("#HiddenPoint").val()));
          
           }
    
          fnMergeDetail();
}
//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,RowID/*行号*/,UsedPrice/*基本单价*/,hanshuiPrice)
 {
 
    var EXRate = $("#SignItem_TD_UnitID_Select"+RowID).val().split('|')[1].toString(); /*比率*/
  
           $("#UsedPrice"+RowID).attr("value", (parseFloat(UsedPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
     
           $("#UsedPricHid"+RowID).attr("value", (parseFloat(UsedPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
             $("#OrderTaxPrice"+RowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#OrderTaxPriceHide"+RowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));

    var OutCount = $("#UsedUnitCount"+RowID).val();/*采购数量*/
    
    if (OutCount != '')
    {
        var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val());
        var tempprice=parseFloat(UsedPrice*EXRate).toFixed($("#HiddenPoint").val());
           var totalPrice=parseFloat(OutCount*tempprice).toFixed($("#HiddenPoint").val());
           var TaxPrice =parseFloat(hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
           $("#OrderTaxPrice"+RowID).val(TaxPrice);/*含税价根据原始含税价和比率算出*/
           $("#OrderTaxPriceHide"+RowID).val(TaxPrice);
            
        $("#OrderProductCount"+RowID).val(tempcount);/*基本数量根据计划数量和比率算出*/
        $("#UsedPrice"+RowID).val(tempprice);/*实际单价根据基本单价和比率算出*/
             $("# OrderTotalPrice"+RowID).val(tempprice);/*实际总价根据实际单价和实际数量算出*/
            
    
    }
     fnMergeDetail();
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
          var prodNo1 = document.getElementById("OrderProductNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}

function AddSignRow(Flag)
{//新增明细行
    var DetailCount = findObj("DetailCount",document);//明细来源新增行号
    var rowID = parseInt(DetailCount.value);
    var signFrame = findObj("DetailTable",document);
    var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
    newTR.id = "DetailSignItem" + rowID; 
    var i = 0;
     
    
    var newNameXH=newTR.insertCell(i++);//添加列:选择    
    newNameXH.className="tdColInputCenter";
    newNameXH.innerHTML="<input name='Dtlchk' id='Dtlchk" + rowID + "'  onclick=\"IfSelectAll('Dtlchk','checkall')\" value="+rowID+" type='checkbox' size='20'  />";   

    
    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="tdColInputCenter";
    newNameTD.id="DtlSortNo" + rowID; 
    newNameTD.innerHTML =GenerateNo(rowID);
    
    var newProductID=newTR.insertCell(i++);//添加列:物品ID
    newProductID.style.display = "none";    
    newProductID.innerHTML = "<input id='OrderProductID" + rowID + "' type='text' class=\"tdinput\" style='width:95%;'  />";


    var SetFunNo = "SetEventFocus('OrderProductNo','OrderProductCount'," + rowID + ",false);";
    if ($("#HiddenMoreUnit").val() == 'True') {
        SetFunNo = "SetEventFocus('OrderProductNo','UsedUnitCount'," + rowID + ",false);";
    }
    
    
    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="tdColInputCenter";  
//    if(Flag == "From")
//    {
//        newProductNo.innerHTML = "<input id='OrderProductNo" + rowID + "' type='text' class=\"tdinput\" style='width:95%;'  readonly  onkeydown=\"" + SetFunNo + "\"/>";  
//    }
//    else if(Flag != "From")
//    {
//        newProductNo.innerHTML = "<input id='OrderProductNo" + rowID + "' type='text' class=\"tdinput\" style='width:95%;'  readonly onclick=\"GetProductInfo('" + rowID + "')\"  onkeydown=\"" + SetFunNo + "\"/>";
//    }

    if (Flag == "From") {
        newProductNo.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                                "<tr><td><input id=\"OrderProductNo" + rowID + "\"    onkeydown=\"" + SetFunNo + "\"  class=\"tdinput\" size=\"7\" type=\"text\" readonly></td>" +
                                "<td align=\"right\"><img style=\"cursor: hand\"   align=\"absMiddle\" src=\"../../../Images/search.gif\" height=\"21\"></td>" +
                             "</tr></table>";
    }
    else if (Flag != "From") {
    newProductNo.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                                "<tr><td><input id=\"OrderProductNo" + rowID + "\" onblur=\"SetMatchProduct(" + rowID + ",this.value);\"  onkeydown=\"" + SetFunNo + "\"  class=\"tdinput\" size=\"7\" type=\"text\"></td>" +
                                "<td align=\"right\"><img style=\"cursor: hand\" onclick=\"popTechObj.ShowList('" + rowID + "');\" align=\"absMiddle\" src=\"../../../Images/search.gif\" height=\"21\"></td>" +
                             "</tr></table>";
    }
    
                             
                             
    
    
    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="tdColInputCenter";
    newProductName.innerHTML = "<input id='OrderProductName" + rowID + "' type='text' class=\"tdinput\"  style='width:95%;'  disabled />";
    
    var newSpecification=newTR.insertCell(i++);//添加列:规格
    newSpecification.className="tdColInputCenter";
    newSpecification.innerHTML = "<input id='OrderSpecification" + rowID + "' type='text' class=\"tdinput\"  style='width:90%; '  disabled />";


    var newDtlSColor = newTR.insertCell(i++); //添加列:颜色
    newDtlSColor.className = "tdColInputCenter";
    newDtlSColor.innerHTML = "<input id='DtlSColor" + rowID + "' type='text' class=\"tdinput\"  style='width:90%; '  disabled />";
    
    
    
      if($("#HiddenMoreUnit").val()=="False")
        { 
    var newProductCount=newTR.insertCell(i++);//添加列:采购数量
    newProductCount.className="tdColInputCenter";
    newProductCount.innerHTML = "<input id='OrderProductCount" + rowID + "' type='text' class=\"tdinput\"  onblur=\"Number_round(this," + $("#HiddenPoint").val() + "); fnMergeDetail();\" style='width:95%;padding-left :2px'   onkeydown=\"SetEventFocus('OrderProductCount','OrderProductNo'," + rowID + ",true);\"   />";
    }
    else
    {
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block";
    var newProductCount=newTR.insertCell(i++);//添加列:基本数量
    newProductCount.className="tdColInputCenter";
    newProductCount.innerHTML = "<input id='OrderProductCount" + rowID + "' type='text' class=\"tdinput\"    style='width:95%;padding-left :2px'  readonly='readonly'/>";
   
      var newUsedUnitCount=newTR.insertCell(i++);//添加列:采购数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='UsedUnitCount" + rowID + "' type='text' class=\"tdinput\"  value=''  style='width:90%;' onblur=\"Number_round(this," + $("#HiddenPoint").val() + "); fnMergeDetail();\"  onkeydown=\"SetEventFocus('UsedUnitCount','OrderProductNo'," + rowID + ",true);\"  />"; 
     
    }
    
         if($("#HiddenMoreUnit").val()=="False")
        { 
    var newUnitID=newTR.insertCell(i++);//添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input id='OrderUnitID" + rowID + "' type='hidden' style='width:95%;' />";
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="tdColInputCenter";
    newUnitName.innerHTML = "<input  id='OrderUnitName" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;'   disabled />";
    
    }
    else
    {
         document .getElementById ("spUnitID").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID").style.display="block";
        var newUnitID=newTR.insertCell(i++);//添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input id='OrderUnitID" + rowID + "' type='hidden' style='width:95%;' />";
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="tdColInputCenter";
    newUnitName.innerHTML = "<input  id='OrderUnitName" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;'   disabled />";
    
      var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
    
    
    }
       if($("#HiddenMoreUnit").val()=="False")
        {
    var newUnitPrice=newTR.insertCell(i++);//添加列:单价
    newUnitPrice.className="tdColInputCenter";
    newUnitPrice.innerHTML = "<input id='OrderUnitPrice" + rowID + "'type='text' class=\"tdinput\"   onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnMergeDetail();\" style='width:80%;'   />";
    
    var newUnitPriceHid=newTR.insertCell(i++);//添加列:单价隐藏
    newUnitPriceHid.style.display = "none";
    newUnitPriceHid.innerHTML = "<input id='OrderUnitPriceHid" + rowID + "'type='text' class=\"tdinput\"   onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnMergeDetail();\" style='width:80%;'   />";
    }
    else
    {
     document .getElementById ("spUnitPrice").style.display="none";
document .getElementById ("spUsedPrice").style.display="block";
       var newUnitPrice=newTR.insertCell(i++);//添加列:基本单价
  newUnitPrice.style.display = "none";
    newUnitPrice.innerHTML = "<input id='OrderUnitPrice" + rowID + "'type='text' class=\"tdinput\"   onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnMergeDetail();\" style='width:80%;'   />";
    
    var newUnitPriceHid=newTR.insertCell(i++);//添加列:单价隐藏
    newUnitPriceHid.style.display = "none";
    newUnitPriceHid.innerHTML = "<input id='OrderUnitPriceHid" + rowID + "'type='text' class=\"tdinput\"   onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnMergeDetail();\" style='width:80%;'   />";
    
    
      var newUsedPrice=newTR.insertCell(i++);//添加列:实际单价
    newUsedPrice.className="cell"; 
    newUsedPrice.innerHTML = "<input id='UsedPrice" + rowID + "' type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnMergeDetail();\"   style='width:80%;'   />";
     var newUsedPricHid=newTR.insertCell(i++);//添加列:实际单价
    newUsedPricHid.style.display = "none";
    newUsedPricHid.innerHTML = "<input id='UsedPricHid" + rowID + "' type='text' class=\"tdinput\"   style='width:80%;'   />";
    }
    
    var newTaxPric=newTR.insertCell(i++);//添加列:含税价
    newTaxPric.className="tdColInputCenter";
    newTaxPric.innerHTML = "<input id='OrderTaxPrice" + rowID + "'type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnMergeDetail();\"  style='width:95%;'    />";
    
    var newTaxPricHide=newTR.insertCell(i++);//添加列:含税价隐藏
    newTaxPricHide.style.display = "none";
    newTaxPricHide.innerHTML = "<input id='OrderTaxPriceHide" + rowID + "'type='text' class=\"tdinput\"  style='width:95%;'    />";
    
//    var newDiscount=newTR.insertCell(i++);//添加列:折扣
//    newDiscount.className="tdColInputCenter";
//    newDiscount.innerHTML = "<input id='OrderDiscount" + rowID + "'type='text' class=\"tdinput\" onblur=\"Number_round(this,2);fnMergeDetail();\"  style='width:95%;'    />";
//    
    var newTaxRate=newTR.insertCell(i++);//添加列:税率
    newTaxRate.className="tdColInputCenter";
    newTaxRate.innerHTML = "<input id='OrderTaxRate" + rowID + "'type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnMergeDetail();\"  style='width:80%;'   />";
    
    var newTaxRateHide=newTR.insertCell(i++);//添加列:税率隐藏
    newTaxRateHide.style.display = "none";
    newTaxRateHide.innerHTML = "<input id='OrderTaxRateHide" + rowID + "'type='text' class=\"tdinput\"  style='width:80%;'   />";
    
    var newTotalPrice=newTR.insertCell(i++);//添加列:金额
    newTotalPrice.className="tdColInputCenter";
    newTotalPrice.innerHTML = "<input id='OrderTotalPrice" + rowID + "'type='text' class=\"tdinput\"  style='width:80%;'  disabled />";
    
    var newTotalFee=newTR.insertCell(i++);//添加列:含税金额
    newTotalFee.className="tdColInputCenter";
    newTotalFee.innerHTML = "<input id='OrderTotalFee" + rowID + "'type='text' class=\"tdinput\"  style='width:80%;' disabled  />";
    
    var newTotalTax=newTR.insertCell(i++);//添加列:税额
    newTotalTax.className="tdColInputCenter";
    newTotalTax.innerHTML = "<input id='OrderTotalTax" + rowID + "'type='text' class=\"tdinput\"  style='width:80%;'   disabled />";
    
    var newRequireDate=newTR.insertCell(i++);//添加列:交货日期
    newRequireDate.className="tdColInputCenter";
    newRequireDate.innerHTML = "<input id='OrderRequireDate" + rowID + "'type='text' class=\"tdinput\"  style='width:95%;'  readonly=\"readonly\" onclick=\"WdatePicker();\"  />";
//    $("#OrderRequireDate" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#OrderRequireDate" + rowID)})}); 
    
    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.style.display = "none";
    newRemark.innerHTML = "<input id='OrderRemark" + rowID + "'type='text' class=\"tdinput\"  style='width:95%;'    />";
    
    var newFromBillID=newTR.insertCell(i++);//添加列:源单ID
    newFromBillID.style.display = "none";
    newFromBillID.innerHTML = "<input id='OrderFromBillID" + rowID + "'type='text' class=\"tdinput\"  style='width:95%;'   readonly />";
    
    var newFromBillNo=newTR.insertCell(i++);//添加列:源单编号
    newFromBillNo.className="tdColInputCenter";
    newFromBillNo.innerHTML = "<input id='OrderFromBillNo" + rowID + "'type='text' class=\"tdinput\"  style='width:95%;'  disabled />";
    
    var newFromLineNo=newTR.insertCell(i++);//添加列:源单序号
    newFromLineNo.className="tdColInputCenter";
    newFromLineNo.innerHTML = "<input id='OrderFromLineNo" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;border:0px'   disabled />";
    
    var newOrderCount=newTR.insertCell(i++);//添加列:已订购数量
       newFromLineNo.className="tdColInputCenter";
    newOrderCount.id= "OrderCounttd"+rowID;
    newOrderCount.style.display = "none";
    newOrderCount.innerHTML = "<input id='OrderCount" + rowID + "'  type='text'  class=\"tdinput\"  style='width:98%;border:0px'   disabled />";
    
    DetailCount.value = (rowID+1).toString();
    
    return rowID;
    
}

function GenerateNo(Edge)
{//生成序号
    DtlSCnt = findObj("DetailCount",document);//明细来源新增行号
    var signFrame = findObj("DetailTable",document);
    var SortNo = 1;//起始行号
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<Edge;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                document.getElementById("DtlSortNo"+i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0;//明细表不存在时返回0
}
function DeleteOrderSignRow()
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
        document.getElementById("DtlSortNo"+rowID).innerHTML = GenerateNo(rowID);
    }
    var Flag = document.getElementById("ddlFromType").value.Trim();
    
    //判断是否有明细了，若没有了，则将供应商设为可选
    var totalSort = 0;//明细行数
    for( var i = 0; i<ck.length;i++ )
    {
        var rowID = i+1;
        if (signFrame.rows[rowID].style.display!="none")
        {
           totalSort++;
           break;
        }
    }
    if(totalSort !=0)
    {//明细行数大于0
    }
    else
    {//无明细，供应商可选
        $("#txtProviderName").removeAttr("disabled");
        $("#ddlCurrencyType").removeAttr("disabled");
    }
    
    fnMergeDetail();
}

function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard,df,dd,StandardBuy,TaxBuy,InTaxRate)
{
 
//StandardBuy 含税进价
//TaxBuy    去税进价   InTaxRate 进项税率
    var Rate = $("#txtExchangeRate").val();
    var RowID = popTechObj.InputObj;

    if (price == "") {
        price = 0;
    }
    if (taxprice == "") {
        taxprice = 0;
    }

    if (taxrate == "") {
        taxrate = 0;
    }
    if (TaxBuy == "") {
        TaxBuy = 0;
    }
    if (InTaxRate == "") {
        InTaxRate = 0;
    }

    if (StandardBuy == "") {
        StandardBuy = 0;
    }
    
    $("#OrderProductID"+RowID).val(id);
    $("#OrderProductNo"+RowID).val(no);
    $("#OrderProductName"+RowID).val(productname);

    $("#OrderUnitPrice"+RowID).val( (parseFloat(TaxBuy)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
    var unitPrice=(parseFloat(TaxBuy)/parseFloat(Rate)).toFixed($("#HiddenPoint").val());
    $("#OrderUnitPriceHid"+RowID).val(  (parseFloat(TaxBuy)).toFixed($("#HiddenPoint").val()));
    $("#OrderUnitID"+RowID).val(unitid);
    $("#OrderUnitName"+RowID).val(unit);
    $("#OrderTaxRate"+RowID).val(InTaxRate);
    $("#OrderTaxRateHide"+RowID).val(InTaxRate);
    $("#OrderTaxPrice"+RowID).val( (parseFloat(StandardBuy)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
    var taxPrice=(parseFloat(StandardBuy)/parseFloat(Rate)).toFixed($("#HiddenPoint").val());
    $("#OrderTaxPriceHide"+RowID).val( (parseFloat(StandardBuy)).toFixed($("#HiddenPoint").val()));
//    $("#OrderDiscount"+RowID).val(discount);
    $("#OrderSpecification"+RowID).val(standard);
         if($("#HiddenMoreUnit").val()=="False")
            {}
            else
            {
             GetUnitGroupSelectEx(id,"InUnit","SignItem_TD_UnitID_Select" + RowID,"ChangeUnit(this.id,"+RowID+","+unitPrice+","+taxPrice+")","unitdiv" + RowID,'',"FillContent("+RowID+","+unitPrice+","+taxPrice+")");//绑定单位组
            }
}
 
function GetProductInfo(rowID)
{
    //如果是从其他页面添加的则不让弹出
    if(IsFromBill(rowID))
    {
    }
    else
    {
        popTechObj.ShowList(rowID);
//        popProductInfoUC.ShowList("OrderProductID"+rowID+"","OrderProductNo"+rowID+"","OrderProductName"+rowID+""
//                     ,"OrderSpecification"+rowID+"","OrderUnitID"+rowID+"","OrderUnitName"+rowID+"","OrderUnitPrice"+rowID+""
//                     ,"OrderTaxPrice"+rowID+"","OrderTaxPriceHide"+rowID+"");
    }
}

function IsFromBill(rowID)
{//判断是否是从其他页面添加的,是则返回true，否则返回false
    if(document.getElementById("OrderFromBillNo"+rowID).value.Trim() != "")
    {
        return true;
    }
    return false;
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

function FillPurchaseOrder(ID)
{
var ActionOrder = "Fill";
var URLParams = "ActionOrder="+ActionOrder;
URLParams += "&ID="+ID;
$("#HiddenAction").val("Update");
$.ajax({
    type: "POST",
    url: "../../../Handler/Office/PurchaseManager/PurchaseOrderAdd.ashx",
    dataType: 'json', //返回json格式数据
    cache: false,
    data: URLParams,
    beforeSend: function() {
        AddPop();
    },
    error: function() {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
    },
    success: function(msg) {
        $.each(msg.PurchaseOrderPrimary, function(i, item) {
            if (item.ID != null && item.ID != "") {
                fnFillPurchaseOrder(item);
            }
            var TableName = "officedba.PurchaseOrder";
         
            GetExtAttr(TableName, msg.PurchaseOrderPrimary);
        });



        $.each(msg.PurchaseOrderDetail, function(i, item) {
            if (item.ID != null && item.ID != "") {
                fnFillPurchaseOrderDetail(item);
            }
        });
        fnStatus($("#txtBillStatusID").val(), $("#IsCite").val());
        fnFlowStatus($("#FlowStatus").val());
        GetFlowButton_DisplayControl();

    },
    complete: function() { hidePopup(); } //接收数据完毕
}); 
}

function fnFillPurchaseOrder(item)
{
    //document.getElementById("divCodeRule").style.display = "none";
    //document.getElementById("divPurOrderNo").style.display = "block";
    document.getElementById("PurOrderNo_txtCode").value = item.OrderNo;
    document.getElementById('PurOrderNo_txtCode').className='tdinput';
    document.getElementById('PurOrderNo_txtCode').style.width='90%';
    document.getElementById('PurOrderNo_ddlCodeRule').style.display='none';
    
    $("#txtTitle").attr("value",item.Title);
    $("#txtPurchaserID").attr("value",item.Purchaser);
    $("#UserPurchaserName").attr("value",item.PurchaserName);
    $("#txtDeptID").attr("value",item.DeptID);
    $("#DeptName").attr("value",item.DeptName);
    
    $("#ddlFromType").attr("value",item.FromType);
    if(item.FromType != "0")
    {
        $("#imgGetDtl").css("display", "inline");
        $("#imgUnGetDtl").css("display", "none");
    }
//    $("#txtPurchaseDate").attr("value",item.PurchaseDate);
    $("#txtProviderID").attr("value",item.ProviderID);
    $("#txtProviderName").attr("value",item.ProviderName);
    $("#txtProviderBillID").attr("value",item.ProviderBillID);
 
    for(var i=0;i<document.getElementById("ddlCurrencyType").options.length;i++)
    {
    var sd=document.getElementById("ddlCurrencyType").options[i].value.split('_')[0];
        if(sd== item.CurrencyType)
        {
           document .getElementById ("ddlCurrencyType").options[i].selected =true ;     
 
            break;
        }
    }
//    var ssss = item.CurrencyType +"_"+item.Rate;
//    $("#ddlCurrencyType").attr("value",ssss);
//    $("#CurrencyTypeID").attr("value",item.CurrencyType);
 
    
    $("#txtExchangeRate").attr("value", item.Rate);
    
    $("#txtOurDelegateID").attr("value",item.OurDelegate);
    $("#UserOurDelegate").attr("value",item.OurDelegateName);    
    $("#txtTheyDelegate").attr("value",item.TheyDelegate);
    $("#txtOrderDate").attr("value",item.OrderDate);
    $("#chkIsAddTax").attr("checked",(item.isAddTax == "1"));
    if(item.TypeID != 0)
    $("#ddlTypeID_ddlCodeType").attr("value",item.TypeID);
$("#txtCanUserName").attr("value", item.UserName);
$("#txtCanUserID").attr("value", item.CanUserID);
        $("#hidProjectID").attr("value",item.ProjectID);
            $("#txtProject").attr("value",item.ProjectName); 
                    
    $("#ddlTakeType_ddlCodeType").attr("value",(item.TakeType == "0")?"":item.TakeType);
    $("#ddlCarryType_ddlCodeType").attr("value",(item.CarryType == "0")?"":item.CarryType);
    $("#ddlPayType_ddlCodeType").attr("value",(item.PayType == "0")?"":item.PayType);
    
    $("#ddlMoneyType_ddlCodeType").attr("value",(item.MoneyType == "0")?"":item.MoneyType);
      
         
    $("#txtCountTotal").attr("value",(parseFloat(item.CountTotal)).toFixed($("#HiddenPoint").val()) );
    $("#txtTotalPrice").attr("value",  (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()) );
    $("#txtTotalTax").attr("value", (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val()) );
    $("#txtTotalFee").attr("value",(parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val()) );
    
    $("#txtDiscount").attr("value", (parseFloat(item.Discount)).toFixed($("#HiddenPoint").val()) );
    $("#txtDiscountTotal").attr("value", (parseFloat(item.DiscountTotal)).toFixed($("#HiddenPoint").val()) );
    $("#txtRealTotal").attr("value", (parseFloat(item.RealTotal)).toFixed($("#HiddenPoint").val()) );
    $("#txtOtherTotal").attr("value",(parseFloat(item.OtherTotal)).toFixed($("#HiddenPoint").val()) );
    $("#txtCreatorID").attr("value",item.Creator);
    
    $("#txtCreatorName").attr("value",item.CreatorName);
    $("#txtCreateDate").attr("value",item.CreateDate);
    $("#txtBillStatusID").attr("value",item.BillStatus);
    $("#txtBillStatusName").attr("value",item.BillStatusName);
    $("#txtConfirmorID").attr("value",item.Confirmor);
    
    $("#txtConfirmorName").attr("value",item.ConfirmorName);
    $("#txtConfirmorDate").attr("value",item.ConfirmDate);
    $("#txtModifiedUserID").attr("value",item.ModifiedUserID);
    $("#txtModifiedUserName").attr("value",item.ModifiedUserID);
    $("#txtCloserID").attr("value",item.Closer);
    
    $("#txtCloserName").attr("value",item.CloserName);
    $("#txtCloseDate").attr("value",item.CloseDate);
    $("#txtModifiedDate").attr("value",item.ModifiedDate);
    $("#txtRemark").attr("value",item.remark);
    $("#ThisID").attr("value",item.ID);
    $("#FlowStatus").val(item.FlowStatus);
    $("#IsCite").val(item.IsCite);
//$("#txtCloserID").attr("value",item.Specification);//附件
    //将已到货数量设为可见
    $("#OrderCount").css("display","inline");
    $("#btn_back").css("display","inline");
    
    $("#hfPageAttachment").attr("value",item.Attachment);//附件
    
    if(item.Attachment!="")
    {
        //下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        //上传不显示
        document.getElementById("divUploadResume").style.display = "none"
    }
}

function fnFillPurchaseOrderDetail(item)
{
    var rowID = AddSignRow();
//    $("#DtlSortNo"+rowID).attr("value",item.DtlSortNo);
    $("#OrderProductID"+rowID).attr("value",item.ProductID);
    $("#OrderProductNo"+rowID).attr("value",item.ProductNo);
    $("#OrderProductName"+rowID).attr("value",item.ProductName);
    $("#OrderSpecification"+rowID).attr("value",item.Specification);
    $("#DtlSColor" + rowID).attr("value", item.ColorName);
    
    $("#OrderProductCount"+rowID).attr("value",  (parseFloat(item.ProductCount)).toFixed($("#HiddenPoint").val()) );
    $("#OrderUnitID"+rowID).attr("value",item.UnitID);
    $("#OrderUnitName"+rowID).attr("value",item.UnitName);
    $("#OrderUnitPrice"+rowID).attr("value", (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val()) );
    var unitPrice=(parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val()) ;
 
   var sdsd= item .Rate;
    $("#OrderUnitPriceHid"+rowID).attr("value",   (parseFloat(item.UnitPrice)*parseFloat(item.Rate)).toFixed($("#HiddenPoint").val()) );
    
    
    $("#OrderTaxPrice"+rowID).attr("value",   (parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val()) );
    var taxPrice=(parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val()) ;
    $("#OrderTaxPriceHide"+rowID).attr("value",   (parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val()) );
//    $("#OrderDiscount"+rowID).attr("value",FormatAfterDotNumber(item.Discount, 2));
    $("#OrderTaxRate"+rowID).attr("value", (parseFloat(item.TaxRate)).toFixed($("#HiddenPoint").val()) );
    $("#OrderTaxRateHide"+rowID).attr("value", (parseFloat(item.TaxRate)).toFixed($("#HiddenPoint").val()) );
    $("#OrderTotalPrice"+rowID).attr("value",(parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()) );
    
    $("#OrderTotalFee"+rowID).attr("value",(parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val()) );
    $("#OrderTotalTax"+rowID).attr("value", (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val()) );
    $("#OrderRequireDate"+rowID).attr("value",item.RequireDate);
    $("#OrderRemark"+rowID).attr("value",item.Remark);
    $("#OrderFromBillID"+rowID).attr("value",item.FromBillID);
    $("#OrderFromBillNo"+rowID).attr("value",item.FromBillNo);
    if (item.FromLineNo=="0")
    {
        $("#OrderFromLineNo"+rowID).attr("value",'');
    }
    else
    {
    $("#OrderFromLineNo"+rowID).attr("value",item.FromLineNo);
    }
    
    if($("#ddlFromType").attr("value") == "4")
    {//是采购合同则
        $("#ddlTypeID_ddlCodeType").attr("disabled","disabled");
        $("#ddlTakeType_ddlCodeType").attr("disabled","disabled");
        $("#ddlCarryType_ddlCodeType").attr("disabled","disabled");
        $("#ddlPayType_ddlCodeType").attr("disabled","disabled");
        $("#ddlMoneyType_ddlCodeType").attr("disabled","disabled");
        
        //将供应商设为不可选
        $("#txtProviderName").attr("disabled","disabled");
    }
    //将已到货数量设为可见
    $("#OrderCounttd"+rowID).css("display","inline");
   
       
    $("#OrderCount"+rowID).val( (parseFloat(item.ArrivedCount)).toFixed($("#HiddenPoint").val()) );
    
     if($("#HiddenMoreUnit").val()=="True")
                          {
                       if (item.UsedUnitCount=="")
                       {
                       
                       }
                       else
                       {
                       $("#UsedUnitCount"+rowID).attr("value",(parseFloat(item.UsedUnitCount)).toFixed($("#HiddenPoint").val()));
                       }
                           if (item.UsedPrice=="")
                           {
                           
                           }
                           else{
                             $("#UsedPrice"+rowID).attr("value",  (parseFloat(item.UsedPrice)).toFixed($("#HiddenPoint").val())); 
 
    $("#UsedPricHid"+rowID).attr("value",   (parseFloat(item.UsedPrice)*parseFloat(item.Rate)).toFixed($("#HiddenPoint").val()) );
                           }
  
//    alert(document .getElementById ("UsedPricHid"+rowID ).value);
     var issign="";
                        if (item.FromBillNo!="")
                        {
                        issign="Bill";
                        }
        GetUnitGroupSelectEx(item.ProductID,"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+","+unitPrice+","+taxPrice+")","unitdiv" + rowID,"","FillSelect('"+rowID +"','"+item .UsedUnitID+"','"+issign+"')");//绑定单位组 
                         }    
    
}
 function FillSelect(rowID,UsedUnitID,Fillsign)
 { if (Fillsign=="Bill")
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
/*
* 附件处理
*/
function DealResume(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传简历
    else if ("upload" == flag)
    {
        ShowUploadFile();
    }
    //清除简历
    else if ("clear" == flag)
    {
            //设置简历路径
            document.getElementById("hfPageAttachment").value = "";
            //下载删除不显示
            document.getElementById("divDealResume").style.display = "none";
            //上传显示 
            document.getElementById("divUploadResume").style.display = "block";
    }
    //下载简历
    else if ("download" == flag)
    {
     //获取简历路径
            resumeUrl = document.getElementById("hfPageAttachment").value.Trim();
            window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + resumeUrl, "_blank");
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
        
//        document.getElementById("tbAttachment").value = url;
                var testurl=url.lastIndexOf('\\');
        testurl=url.substring(testurl+1,url.lenght);
        document.getElementById('attachname').innerHTML=testurl;
    }
}

function  Fun_ConfirmOperate()
{//确认
 
if(!confirm("是否确认！"))
return;

 
    
    var ActionOrder = "Confirm";
    var ID = $("#ThisID").attr("value");
        
    var URLParams = "ActionOrder="+ActionOrder;
    URLParams += "&ID="+ID;
        URLParams += "&TotalFee="+$("#txtTotalFee").val();
          URLParams += "&CurrencyTypeID="+$("#CurrencyTypeID").val();
     URLParams += "&ExchangeRate="+$("#txtExchangeRate").val();
     URLParams += "&ProviderID="+$("#txtProviderID").val();
    
    URLParams += "&OrderNo="+$("#PurOrderNo_txtCode").val();
    URLParams += "&FromType="+document.getElementById("ddlFromType").value.Trim();
    URLParams += fnGetDetailInfo();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseOrderAdd.ashx",
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
            if(msg.sta==1|msg.sta==3)
            {
               //更改状态
    $("#txtBillStatusID").attr("value","2");
    $("#txtBillStatusName").attr("value","执行");
                var aaa = msg.data.split('#');
                $("#txtConfirmorID").attr("value",aaa[0]);
                $("#txtConfirmorName").attr("value",aaa[1]);
                $("#txtConfirmorDate").attr("value",aaa[2]);
                $("#txtModifiedUserID").val(msg.data.split('#')[3]);
                $("#txtModifiedUserName").val(msg.data.split('#')[3]);
                $("#txtModifiedDate").val(msg.data.split('#')[2]);
                if (msg .sta==3)
                {
     
                   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功!警告：自动生成凭证失败，请在“财务管理-初始设置”中设置或启用对应的凭证模板！");
                }
                else
                {
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
                }
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                fnFlowStatus($("#FlowStatus").val());
                GetFlowButton_DisplayControl();
            }
            else if(msg.sta==2)
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif", msg.data);
            }
        } 
    }); 
}

function Fun_UnConfirmOperate()
{
    if(!confirm("是否取消确认！"))
        return;
    var URLParams = "ActionOrder=ConcelConfirm";
    URLParams += "&ID="+$("#ThisID").val();
    URLParams += "&OrderNo="+$("#PurOrderNo_txtCode").val();
    URLParams += "&FromType="+document.getElementById("ddlFromType").value.Trim();
    URLParams += fnGetDetailInfo();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseOrderAdd.ashx",
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认成功！");
            GetFlowButton_DisplayControl();
            }
            else if(msg.sta == 2)
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","此单已被引用，不允许取消确认");
            }
        } 
    });
}

//取消确认后
function fnAfterConcelConfirm(flag,BackValue)
{
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
            try{
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
            }catch(e){
            }
            break;
        case 2:
            break;
    }
    //所有text可用
    $(":text").each(function(){ 
        this.disabled=false; 
    });
    //所有checkbox可用
    $(":checkbox").each(function(){ 
        this.disabled=false; 
    });
    
    $("#txtProviderName").attr("disabled",false);
    $("#ddlTypeID_ddlCodeType").attr("disabled",false);
    $("#ddlTakeType_ddlCodeType").attr("disabled",false);
    $("#ddlPayType_ddlCodeType").attr("disabled",false);
    $("#ddlOrderMethod_ddlCodeType").attr("disabled",false);
    $("#ddlMoneyType_ddlCodeType").attr("disabled",false);
}

function Fun_CompleteOperate(isComplete)
{
    if(isComplete)
    {
        if(!confirm("是否结单！"))
        return;
        $("#txtBillStatusID").attr("value","4");
        $("#txtBillStatusName").attr("value","手动结单");
        
//        $("#HiddenAction").attr("value","Complete");
        var ActionOrder = "Complete";
        var ID = $("#ThisID").attr("value");
            
        var URLParams = "ActionOrder="+ActionOrder;
        URLParams += "&ID="+ID;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseOrderAdd.ashx",
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
                $("#txtModifiedUserID").val(msg.data.split('#')[3]);
                $("#txtModifiedUserName").val(msg.data.split('#')[3]);
                $("#txtModifiedDate").val(msg.data.split('#')[2]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单成功！");
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                fnFlowStatus($("#FlowStatus").val());
                GetFlowButton_DisplayControl();
            } 
        }); 
    }
    else
    {
        if(!confirm("是否取消结单！"))
        return;
        $("#txtBillStatusID").attr("value","2");
        $("#txtBillStatusName").attr("value","执行");
        
//        $("#HiddenAction").attr("value","ConcelComplete");
        var ActionOrder = "ConcelComplete";
        var ID = $("#ThisID").attr("value");
            
        var URLParams = "ActionOrder="+ActionOrder;
        URLParams += "&ID="+ID;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseOrderAdd.ashx",
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
                $("#txtModifiedUserID").val(msg.data.split('#')[0]);
                $("#txtModifiedUserName").val(msg.data.split('#')[0]);
                $("#txtModifiedDate").val(msg.data.split('#')[2]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消结单成功！");
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                fnFlowStatus($("#FlowStatus").val());
                GetFlowButton_DisplayControl();
            } 
        }); 
    }
    
}

function Fun_FlowApply_Operate_Succeed(operateType)
{
    try{
        if(operateType == "0")
        {//提交审批成功后,不可改
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
        }
        else if(operateType == "1")
        {//审批成功后，不可改
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
        }
        else if(operateType == "2")
        {//审批不通过
            if(document.getElementById("txtBillStatusID").value.Trim() == "1")
            {//如果单据状态为制单，则可改
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
            }
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
                $("#HiddenAction").val('Update');
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
                break;

            case '5':
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                break;
        }
    }catch(e){
    }
}

function fnFlowStatus(FlowStatus)
{
    try{
        switch (FlowStatus){
                    case '0': //待提交审批         
                        break;
                    case '1': //当前单据正在待审批
                        $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#imgGetDtl").css("display", "none");
                        $("#imgUnGetDtl").css("display", "inline");
                        $("#btnGetGoods").css("display", "none");//条码扫描按钮                   
                        break;
                    case '2': //当前单据正在审批中
                        $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#imgGetDtl").css("display", "none");
                        $("#imgUnGetDtl").css("display", "inline");
                        $("#btnGetGoods").css("display", "none");//条码扫描按钮
                        break;
                    case '3': //当前单据已经通过审核
                        //制单状态的审批通过单据,不可修改
                        if ($("#txtBillStatusID").val() == "1") {
                            $("#imgSave").css("display", "none");
                            $("#imgUnSave").css("display", "inline");
                            $("#imgAdd").css("display", "none");
                            $("#imgUnAdd").css("display", "inline");
                            $("#imgDel").css("display", "none");
                            $("#imgUnDel").css("display", "inline");
                            $("#imgGetDtl").css("display", "none");
                            $("#imgUnGetDtl").css("display", "inline");
                            $("#btnGetGoods").css("display", "none");//条码扫描按钮
                        }

                        break;
                    case '4': //当前单据审批未通过
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
                        $("#btnGetGoods").css("display", "inline");//条码扫描按钮
                        break;   
        }
    }catch(e){
    }
}

//获取审批状态，判断操作按钮
function fnGetBillStatus(BillTypeFlag, BillTypeCode, BillID) {
    var Action = "Get";
    var UrlParam = "Action=Get&BillTypeFlag=" + BillTypeFlag + "\
                               &BillTypeCode=" + BillTypeCode + "\
                               &BillID=" + BillID + "";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Common/Flow.ashx?" + UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取单据审批状态出错！");
        },
        success: function(data) {
            try{
                switch (data.sta) {
                    case 0: //未提交审批         
                        break;
                    case 1: //当前单据正在待审批
                        $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#btnZCBJ").css("display", "none");
                        $("#btnUnZCBJ").css("display", "inline");
                        $("#btnGetGoods").css("display", "none");//条码扫描按钮
                        break;
                    case 2: //当前单据正在审批中

                       $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#btnZCBJ").css("display", "none");
                        $("#btnUnZCBJ").css("display", "inline");
                        $("#btnGetGoods").css("display", "none");//条码扫描按钮
                        break;
                    case 3: //当前单据已经通过审核
                        //制单状态的审批通过单据,不可修改
                        if ($("#BillStatus").html() == "制单") {
                            $("#imgSave").css("display", "none");
                            $("#imgUnSave").css("display", "inline");
                            $("#imgAdd").css("display", "none");
                            $("#imgUnAdd").css("display", "inline");
                            $("#imgDel").css("display", "none");
                            $("#imgUnDel").css("display", "inline");
                            $("#btnZCBJ").css("display", "none");
                            $("#btnUnZCBJ").css("display", "inline");
                            $("#btnGetGoods").css("display", "none");//条码扫描按钮
                        }

                        break;
                    case 4: //当前单据审批未通过
                        break;
                }
            }catch(e)
            {
            }
        }
    });
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
       var rowID=AddSignRow();//插入行
       //填充数据
       //物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
       BarCode_FillParent_Content(ColorName,ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, rowID);
    }
    else
    {
     if($("#HiddenMoreUnit").val()=="False")
      {
      document.getElementById("OrderProductCount"+rerowID).value=    parseFloat (parseFloat(document.getElementById("OrderProductCount"+rerowID).value)+1).toFixed($("#HiddenPoint").val());
   
        }
        else
        {
            var count1=document.getElementById("UsedUnitCount"+rerowID).value;
 
        document.getElementById("UsedUnitCount"+rerowID).value=     parseFloat (parseFloat(count1)+1).toFixed($("#HiddenPoint").val());
        }
    
    
         
        fnMergeDetail();//更改数量后重新计算
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
              var ProductID = $("#OrderProductID" + i).val(); //商品ID（对应商品表ID）
              if(ProductID==ID)
               {
                   rerowID=i;
                   return true;
               }
            }
        }
     return false;
}

//填充扫描条码获取的物品信息
//物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
function BarCode_FillParent_Content(ColorName,id, no, productname, price, unitid, unit, taxrate, taxprice, discount, standard, df, dd, StandardBuy, TaxBuy, InTaxRate, RowID)
{
//StandardBuy 含税进价
//TaxBuy    去税进价   InTaxRate 进项税率
    var Rate = $("#txtExchangeRate").val();
    //var RowID = popTechObj.InputObj;
    $("#OrderProductID"+RowID).val(id);
    $("#OrderProductNo"+RowID).val(no);
    $("#OrderProductName"+RowID).val(productname);
    $("#DtlSColor" + RowID).val(ColorName);
         var unitPrice=( parseFloat(TaxBuy)/parseFloat(Rate)).toFixed($("#HiddenPoint").val());
    $("#OrderUnitPrice"+RowID).val(  ( parseFloat(TaxBuy)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()) );
    
    $("#OrderUnitPriceHid"+RowID).val(( parseFloat(TaxBuy)).toFixed($("#HiddenPoint").val()) );
    $("#OrderUnitID"+RowID).val(unitid);
    $("#OrderUnitName"+RowID).val(unit);
    $("#OrderTaxRate"+RowID).val(  ( parseFloat(InTaxRate)).toFixed($("#HiddenPoint").val()) );
    $("#OrderTaxRateHide"+RowID).val(   ( parseFloat(InTaxRate)).toFixed($("#HiddenPoint").val()) );
    $("#OrderTaxPrice"+RowID).val( ( parseFloat(StandardBuy)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()) );
    var taxPrice= ( parseFloat(StandardBuy)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()) ;
    $("#OrderTaxPriceHide"+RowID).val(( parseFloat(StandardBuy)).toFixed($("#HiddenPoint").val()) );
    $("#OrderSpecification"+RowID).val(standard);
    $("#OrderProductCount"+RowID).val(1);//设置初始数量为1

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
                intProductID = document.getElementById('OrderProductID' + rowID).value;
                snapProductName = document.getElementById('OrderProductName' + rowID).value;
                snapProductNo = document.getElementById('OrderProductNo' + rowID).value;
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



/*焦点2：鼠标失去焦点时，匹配数据库物品信息*/
function SetMatchProduct(rows, values) {
    popTechObj.InputObj = '';
    var ProdNo = values;
    if (values != "") {
        popTechObj.InputObj = rows;
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../../Handler/Office/SupplyChain/BatchProductInfo.ashx?ProdNo=" + ProdNo, //目标地址
            cache: false,
            success: function(msg) {
                var rowsCount = 0;
                var rowsExistCount = 0;

                if (typeof (msg.dataBatch) != 'undefined') {
                    $.each(msg.dataBatch, function(i, item) {

                    if (!IsExistll(item.ProdNo, rows)) {
                            rowsCount++;
                            Fun_FillParent_Content(item.ID, item.ProdNo, item.ProductName, item.StandardSell, item.UnitID,
                                            item.CodeName, item.TaxRate, item.SellTax, item.Discount, item.Specification,
                                            item.CodeTypeName, item.TypeID, item.StandardBuy, item.TaxBuy, item.InTaxRate,
                                            item.StandardCost, item.GroupUnitNo, item.SaleUnitID, 'saleUnitName', item.InUnitID,
                                            'inUnitName', item.StockUnitID, 'stockUnitName', item.MakeUnitID, 'maxkUnitName',
                                            item.IsBatchNo, item.ColorName);
                        }
                        else {
                            rowsExistCount++;
                        }
                    });
                }
                if (rowsCount == 0) {
                    ClearOneProductInfo(rows);
                    if (rowsExistCount > 0) {
                        popMsgObj.Show("采购订单明细|", "明细中不允许存在重复记录");
                    }
                    else {
                        popMsgObj.Show("物品编号" + rows + "|", "不存在此" + values + "物品，请重新输入或选择物品");
                    }
                }

            },
            error: function() { popMsgObj.ShowMsg('匹配物品数据时发生请求异常!'); },
            complete: function() { }
        });
    }
    else {
        if (document.getElementById('OrderProductID' + rows).value != "") {
            popMsgObj.Show("物品编号" + rows + "|", "请重新输入或选择物品");
        }
    }
}


/*判断是否有相同记录有返回true，没有返回false*/
var rerowID = "";
function IsExistll(prodNo, rows) {
    var signFrame = document.getElementById("DetailTable");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var prodNo1 = document.getElementById("OrderProductNo" + i).value;
        if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
            if (i != rows) {
                rerowID = i;
                return true;
            }
        }
    }
    return false;
}



function ClearOneProductInfo(rows) {


    $("#OrderProductID" + rows).val("");
    $("#OrderProductNo" + rows).val("");
    $("#OrderProductName" + rows).val("");
    $("#OrderUnitPrice" + rows).val(""); 
    $("#OrderUnitPriceHid" + rows).val("");
    $("#OrderUnitID" + rows).val("");
    $("#OrderUnitName" + rows).val("");
    $("#OrderTaxRate" + rows).val("");
    $("#OrderTaxRateHide" + rows).val("");
    $("#OrderTaxPrice" + rows).val(""); 
    $("#OrderTaxPriceHide" + rows).val(""); 
    $("#OrderSpecification" + rows).val("");
    $("#DtlSColor" + rows).val("");
    $("#OrderProductCount" + rows).val("");


    $("#OrderTotalPrice" + rows).val("");

    $("#OrderTotalFee" + rows).val("");
    $("#OrderTotalTax" + rows).val("");

    $("#OrderRequireDate" + rows).val("");
    $("#OrderRemark" + rows).val("");
    
    

    if ($("#HiddenMoreUnit").val() == 'True') {
        document.getElementById('UsedPrice' + rows).value = '';
        //        document.getElementById('UsedPriceHid' + rows).value = '';
        document.getElementById('unitdiv' + rows).innerHTML = '';
        document.getElementById('UsedUnitCount' + rows).value = '';
    }
    else {

        //        document.getElementById('Hidden_TD_Text_UnitID_' + rows).value = '';
        //        document.getElementById('TD_Text_UnitID_' + rows).value = '';
    }
}
