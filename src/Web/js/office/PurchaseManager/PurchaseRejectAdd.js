//采购到货通知

var DtlS_Item = new Array();
var Dtl_Item2 = new Array();
var DtlCount = 0;

var page = "";
var rejectNo ;
var Title ;
var TypeID ;
var Purchaser  ;
var PurchaserName  ;
var FromType  ;
var ProviderID  ;
var ProviderName  ;
var BillStatus ;
var UsedStatus ;
var DeptID;
var DeptName;
var Isliebiao ;

$(document).ready(function() {
    SetDefaultValues();
    requestobj = GetRequest(location.search);
    $("#hidSearchCondition").val(location.search.substr(1));
    var intMasterArriveID = document.getElementById("txtIndentityID").value;
    var intFromType = requestobj["intFromType"];
    if (intFromType != null) {
        $("#btn_back").show(); // 桌面返回修改
    }
    if (intMasterArriveID != null) {
        DealPage(intMasterArriveID);
    }
    else {
       
    }
    GetFlowButton_DisplayControl();
});

// 填充默认值
function SetDefaultValues()
{
    var temp=FormatAfterDotNumber(0,selPoint);
    $("#txtCountTotal").val(temp);
    $("#txtTotalMoney").val(temp);
    $("#txtTotalTaxHo").val(temp);
    $("#txtTotalFeeHo").val(temp);
    $("#txtDiscount").val(FormatAfterDotNumber(100,selPoint));
    $("#txtDiscountTotal").val(temp);
    $("#txtRealTotal").val(temp);
    $("#txtTotalDyfzk").val(temp);
    $("#txtTotalYthkhj").val(temp);
}

// 获取url中"?"符后的字串
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

// 处理加载页面
function DealPage(recordnoparam)
{
    if(recordnoparam !=0)
    {
        document.getElementById("txtAction").value="2";
        document.getElementById("divTitle").innerText="采购退货单";      
        GetRejectInfo(recordnoparam);
    }
    else {
        GetExtAttr('officedba.PurchaseReject', null);
        document.getElementById("txtAction").value="1";
        document.getElementById("divTitle").innerText="新建采购退货单";     
    }
}

// 返回
function Back()
{ 
    var URLParams = $("#hidSearchCondition").val();
    var requestobj2 = GetRequest(location.search);
    var intFromType = requestobj2["intFromType"];
    switch(intFromType)
    {
        case "1":
            URLParams = URLParams.replace("ModuleID=2042001","ModuleID=2042002");
            window.location.href='PurchaseRejectInfo.aspx?'+URLParams;
            break;
        case "2":
            window.location.href='../../../Desktop.aspx';
            break;
        case "3":          
            window.location.href='../../Personal/WorkFlow/FlowMyApply.aspx?ModuleID='+ListModuleID;
            break;
        case "4":
            window.location.href='../../Personal/WorkFlow/FlowMyProcess.aspx?ModuleID='+ListModuleID;
            break;
        case "5":
            window.location.href='../../Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID='+ListModuleID;
            break;
    }
}
// 修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
    var strKey = $.trim($("#hiddKey").val()); // 扩展属性字段值

    var arrKey = strKey.split('|');
    if (typeof (data) != 'undefined') {
        $.each(data, function(i, item) {
            for (var t = 0; t < arrKey.length; t++) {
                // 不为空的字段名才取值
                if ($.trim(arrKey[t]) != '') {
                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);

                }
            }

        });
    }
}
// --------------------扩展属性操作----------------------------------------------------------------------------//
function GetRejectInfo(ID)
{
     $.ajax({
       type: "POST",// 用POST方式传输
       dataType:"json",// 数据格式:JSON
       url:"../../../Handler/Office/PurchaseManager/PurchaseRejectEdit.ashx",// 目标地址
       data:"ID="+ID+"",
       cache:false,
       beforeSend:function(){AddPop();},// 发送数据之前
       success: function(msg){
                // 数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                        // 基本信息
                        $("#CodingRuleControl1_txtCode").attr("value",item.RejectNo);// 合同编号
                        $("#divPurchaseRejectNo").attr("value",item.RejectNo);// 合同编号
                        $("#txtTitle").attr("value",item.Title);// 主题
                        $("#drpFromType").attr("value",item.FromType);// 源单类型id
                        $("#drpTypeID").attr("value",item.TypeID);// 采购类别
                        $("#HidDeptID").attr("value",item.DeptID);// 部门id
                        $("#DeptDeptID").attr("value",item.DeptName);// 部门名称
                        $("#txtProviderID").attr("value",item.ProviderName);// 供应商名称
                        $("#txtHidProviderID").attr("value",item.ProviderID);// 供应商ID
                        $("#drpTakeType").attr("value",item.TakeType);// 交货方式
                        $("#drpCarryType").attr("value",item.CarryType);// 运送方式
                        $("#drpPayType").attr("value",item.PayType);// 结算方式
                        $("#txtRate").attr("value",FormatAfterDotNumber(item.Rate,4));// 汇率
                        $("#hidRate").attr("value",item.Rate);// 汇率
                        $("#txtHidOurDept").attr("value",item.DeptID);// 部门id
                        $("#hidProjectID").val(item.ProjectID);// 项目ID
                        $("#txtProject").val(item.ProjectName);// 项目名称
                        
                        if (item.isAddTax ==1)
                        {
                            document.getElementById("chkIsAddTax").checked = true;
                            document.getElementById("AddTax").innerHTML = "是增值税";
                        }
                        else
                        {
                            document.getElementById("chkIsAddTax").checked = false;
                            document.getElementById("AddTax").innerHTML = "非增值税";
                        }
                        
                        for(var i=0;i<document.getElementById("drpCurrencyType").options.length;++i)
                            {
                                if(document.getElementById("drpCurrencyType").options[i].value.split('_')[0]== item.CurrencyType)
                                {
                                    $("#drpCurrencyType").attr("selectedIndex",i);
                                    break;
                                }
                            }
                        $("#CurrencyTypeID").attr("value",item.CurrencyType);// 币种ID
                        $("#HidCheckUserID").attr("value",item.CheckUserID);// 点收人ID
                        
                        $("#txtReceiveMan").attr("value",item.ReceiveMan);// 收货人名称
                        $("#txtReceiveTel").attr("title",item.ReceiveTel);// 收货人联系电话
                        $("#drpMoneyType").attr("value",item.MoneyType);// 支付方式
                        $("#HidPurchaser").attr("value",item.Purchaser);// 采购员ID
                        $("#UserPurchaser").attr("value",item.PurchaserName);// 采购员名称
                        $("#txtRejectDate").attr("value",item.RejectDate);// 退货日期
                        $("#txtisOpenbill").attr("value",item.isOpenbillName);// 是否已开票
                        $("#txtSendAddress").attr("value",item.SendAddress);// 发货地址
                        $("#txtReceiveOverAddress").attr("value",item.ReceiveOverAddress);// 收货地址
                        
                        // 合计信息
                        $("#txtCountTotal").attr("value",FormatAfterDotNumber(item.CountTotal,selPoint));// 采购到货数量合计
                        $("#txtTotalMoney").attr("value",FormatAfterDotNumber(item.TotalPrice,selPoint));// 金额合计
                        $("#txtTotalTaxHo").attr("value",FormatAfterDotNumber(item.TotalTax,selPoint));// 税额合计
                        $("#txtTotalFeeHo").attr("value",FormatAfterDotNumber(item.TotalFee,selPoint));// 含税金额合计
                        $("#txtDiscount").attr("value",FormatAfterDotNumber(item.Discount,selPoint));// 整单折扣
                        $("#txtDiscountTotal").attr("value",FormatAfterDotNumber(item.DiscountTotal,selPoint));// 折扣金额
                        $("#txtRealTotal").attr("value",FormatAfterDotNumber(item.RealTotal,selPoint));// 折后含税金额
                        $("#txtTotalDyfzk").attr("value",FormatAfterDotNumber(item.TotalDyfzk,selPoint));// 抵应付账款
                        $("#txtTotalYthkhj").attr("value",FormatAfterDotNumber(item.TotalYthkhj,selPoint));// 应退货款合计
                        // 备注信息
                        $("#txtCreateDate").attr("value",item.CreateDate);// 制单时间
                        $("#ddlBillStatus").attr("value",item.BillStatus);// 单据状态
                        $("#txtConfirmor").attr("value",item.Confirmor);// 确认人id
                        $("#txtConfirmorReal").attr("value",item.ConfirmorName);// 确认人名称
                        $("#txtConfirmDate").attr("value",item.ConfirmDate);// 确认时间
                        $("#txtCloser").attr("value",item.Closer);// 结单人id
                        $("#txtCloserReal").attr("value",item.CloserName);// 结单人名称
                        $("#txtCloseDate").attr("value",item.CloseDate);// 结单日期
                        $("#txtModifiedUserID").attr("value",item.ModifiedUserID);// 最后更新人id
                        $("#txtModifiedUserIDReal").attr("value",item.ModifiedUserID);// 最后更新人id显示
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);// 最后更新时间
                        $("#hfPageAttachment").attr("value",item.Attachment);// 附件
                        $("#txtRemark").attr("value",item.Remark);// 备注
                        
                        document.getElementById("divPurchaseRejectNo").innerHTML=item.RejectNo;
                        document.getElementById("divPurchaseRejectNo").style.display="block";
                        document.getElementById("divInputNo").style.display="none";
                        
                        $("#FlowStatus").val(item.UsedStatus);
                        $("#Isyinyong").val(item.IsCite);
                        fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                    }
                    var TableName = "officedba.PurchaseReject";
                    GetExtAttr(TableName, msg.data);
               });
               $.each(msg.data2,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                            var FromTypeName = document.getElementById("hidFromTypeName").value.Trim();
                            var Rate = document.getElementById("hidRate").value.Trim();
                            if(FromTypeName =="采购到货单")
                            {
                                document.getElementById('txtProviderID').disabled=true;
                                document.getElementById('drpCurrencyType').disabled=true;
                            }
                            var index = AddSignRow();
                            $("#txtProductID"+index).attr("value",item.ProductID);
                            $("#txtProductNo"+index).attr("value",item.ProductNo);
                            $("#txtProductName" + index).attr("value", item.ProductName);
                            $("#txtstandard" + index).attr("value", item.Specification);
                            $("#txtColorName"+index).attr("value",item.ColorName);
                            if(isMoreUnit)
                            {// 启用多计量单位
                                $("#UsedUnitID"+index).attr("value",item.UnitID);
                                $("#UsedUnitName"+index).attr("value",item.UnitName);
                                $("#UsedUnitCount"+index).attr("value",FormatAfterDotNumber(item.BackCount,selPoint));
                                $("#txtBackCount"+index).attr("value",FormatAfterDotNumber(item.UsedUnitCount,selPoint));
                                GetUnitGroupSelect(item.ProductID, 'InUnit', 'txtUnitID'+index, 'ChangeUnit(this,'+index+',1)', 'tdUnitName'+index,item.UsedUnitID);
                                $("#txtBackCount"+index).attr("onblur",'ChangeUnit(this,'+index+')'); 
                                
                                $("#txtUnitPrice"+index).attr("value",FormatAfterDotNumber(item.UsedPrice,selPoint));
                                $("#hiddUnitPrice"+index).attr("value",FormatAfterDotNumber(item.UnitPrice*Rate,selPoint));
                            }
                            else
                            {
                                $("#txtUnitID"+index).attr("value",item.UnitID);
                                $("#txtUnitName"+index).attr("value",item.UnitName);
                                $("#txtBackCount"+index).attr("value",FormatAfterDotNumber(item.BackCount,selPoint));
                                
                                $("#txtUnitPrice"+index).attr("value",FormatAfterDotNumber(item.UnitPrice,selPoint));
                                $("#hiddUnitPrice"+index).attr("value",FormatAfterDotNumber(item.UnitPrice*Rate,selPoint));
                            }
                            var btshuliang = item.ProductCount;
                            if(btshuliang == FormatAfterDotNumber(0,selPoint))
                            {
                                $("#txtProductCount"+index).attr("value","");
                            }
                            else
                            {
                                $("#txtProductCount"+index).attr("value",FormatAfterDotNumber(item.ProductCount,selPoint));
                            }
                            $("#txtYBackCount"+index).attr("value",FormatAfterDotNumber(item.YBackCount,selPoint));
                            $("#txtApplyReason"+index).attr("value",item.ApplyReason);
                            $("#txtTaxPrice"+index).attr("value",FormatAfterDotNumber(item.TaxPrice,selPoint));
                            $("#hiddTaxPrice"+index).attr("value",FormatAfterDotNumber(item.TaxPrice*Rate,selPoint));
                            $("#txtTaxRate"+index).attr("value",FormatAfterDotNumber(item.TaxRate,selPoint));
                            $("#hiddTaxRate"+index).attr("value",FormatAfterDotNumber(item.HidTaxRate,selPoint));
                            $("#txtTotalPrice"+index).attr("value",FormatAfterDotNumber(item.TotalPrice,selPoint));
                            $("#txtTotalFee"+index).attr("value",FormatAfterDotNumber(item.TotalFee,selPoint));
                            $("#txtTotalTax"+index).attr("value",FormatAfterDotNumber(item.TotalTax,selPoint));
                            $("#txtRemark"+index).attr("value",item.Remark);
                            $("#txtFromBillID"+index).attr("value",item.FromBillID);
                            $("#txtFromBillNo"+index).attr("value",item.FromBillNo);
                            $("#txtFromLineNo"+index).attr("value",item.FromLineNo);
                            $("#txtOutedTotal"+index).attr("value",item.OutedTotal);
                        }
                        if(msg.IsCite[0].IsCite == "True")
                       {// 如果被引用，保存按钮隐藏
                            $("#Isyinyong").val("True");
                            $("#imgSave").css("display", "none");
                            $("#imgUnSave").css("display", "inline");
                            $("#imgAdd").css("display", "none");
                            $("#imgUnAdd").css("display", "inline");
                            $("#imgDel").css("display", "none");
                            $("#imgUnDel").css("display", "inline");
                            $("#Get_Potential").css("display", "none");
                            $("#Get_UPotential").css("display", "inline"); 
                            $("#btnGetGoods").css("display", "none");// 条码扫描按钮
                       }
                       GetFlowButton_DisplayControl();
               });
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}// 接收数据完毕
       });
}


/*******************************************************************************
 * 切换单位
 ******************************************************************************/
function ChangeUnit(own, rowID, calPrice) 
{
    if(isMoreUnit) 
    {
        var price=0;
        var rate=0;
        CalCulateNum('txtUnitID'+rowID,'txtBackCount'+rowID,'UsedUnitCount'+rowID,'','',selPoint);
        var unitPrice=$("#hiddUnitPrice"+rowID).val();
        if(unitPrice!='' && (calPrice=="1" ) && parseFloat(unitPrice)>0)
        {
            $("#txtUnitPrice" + rowID).val(FormatAfterDotNumber(parseFloat(unitPrice)*parseFloat($("#txtUnitID" + rowID).val().split('|')[1]), selPoint));
        }
        price=parseFloat($("#txtUnitPrice" + rowID).val());
        rate=1+parseFloat($("#txtTaxRate" + rowID).val())/100;
        $("#txtTaxPrice" + rowID).val(FormatAfterDotNumber(price*rate, selPoint));
    }
    fnTotalInfo();
}


// 如果启用多计量单位重新计算基本数量等
function ReCacl()
{
    if(!isMoreUnit) 
    {
        return;
    }
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
    var signFrame = findObj("dg_Log",document); 
    var rowID=0;
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        
        if(signFrame.rows[i+1].style.display!="none")
        {
            rowID=i+1;
            CalCulateNum('txtUnitID'+rowID,'txtBackCount'+rowID,'UsedUnitCount'+rowID,'','',selPoint);
        }
    }
    fnTotalInfo();
}



// 新增退货单
function InsertPurchaseReject() 
{ 
    // 重新计算
    ReCacl();
    if(!CheckBaseInfo())return;

        var action = null;

         var fieldText = "";
         var msgText = "";
         var isFlag = true;
         var rejectNo = "";
         var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
         
         // 基本信息
   
         if (CodeType == "")
         {
            rejectNo=$("#CodingRuleControl1_txtCode").val();
         }
         var title=document.getElementById('txtTitle').value.Trim(); // 单据主题
         var fromType=$("#drpFromType").val(); // 源单类型id
         var typeID=$("#drpTypeID").val();// 采购类别id
         var deptid=$("#HidDeptID").val();// 部门ID
         var providerID=$("#txtHidProviderID").val();// 供应商id
         var purchase=$("#HidPurchaser").val();// 采购员id
         var takeType=$("#drpTakeType").val();// 交货方式id
         var carryType=$("#drpCarryType").val();// 运送方式id
         var payeType=$("#drpPayType").val();// 结算方式
         var rate = $("#txtRate").val();// 汇率
         var currencyType = $("#CurrencyTypeID").val();// 币种取隐藏域的值
         var txtProjectID = $("#hidProjectID").val();// 项目ID
         var isAddtax=0;// 是否增值税
        if($("#chkisAddTax") .checked)
        {
            isAddtax=1;
        }
        else
        {
            isAddtax=0;
        }
        var receiveMan = $("#txtReceiveMan").val();// 收货人名称
        var receiveTel = $("#txtReceiveTel").val();// 收货人联系电话
        var rejectDate = $("#txtRejectDate").val();// 退货日期
        var moneyType=$("#drpMoneyType").val();// 支付方式
        var sendAddress = $("#txtSendAddress").val();// 发货地址
        var receiveOverAddress = $("#txtReceiveOverAddress").val();// 收货地址
        
        // 合计信息
        var countTotal = $("#txtCountTotal").val();// 到货数量合计
        var totalMoney = $("#txtTotalMoney").val();// 金额合计
        var totalTax = $("#txtTotalTaxHo").val();// 税额合计
        var totalfee = document.getElementById('txtTotalFeeHo').value.Trim();// 含税金额合计
        var discount = document.getElementById('txtDiscount').value.Trim();// 整单折扣(%）合计
        var discounttotal = document.getElementById('txtDiscountTotal').value.Trim();// 折扣金额合计
        var realtotal = document.getElementById('txtRealTotal').value.Trim();// 折后含税金额合计
        var totalDyfzk = document.getElementById('txtTotalDyfzk').value.Trim();// 抵应付账款
        var totalYthkhj = document.getElementById('txtTotalYthkhj').value.Trim();// 应退货款合计
        
         // 备注信息
         var creator=document .getElementById ("txtCreator").value.Trim(); // 制单人
 
         var createDate=$("#txtCreateDate").val(); // 制单时间
         var fflag2 = "";
         if(document.getElementById("ddlBillStatus").value.Trim() =="2")
        {
            document.getElementById("ddlBillStatus").value.Trim()="3";
            fflag2 = 1;
        }
        else
        {
            fflag2 = 2;
        }
         var billStatus=$("#ddlBillStatus").val();// 单据状态
         var confirmor=$("#txtConfirmor").val();// 确认人
         var confirmDate=$("#txtConfirmDate").val();// 确认时间
         var closer=$("#txtCloser").val();// 结单人
         var closeDate=$("#txtCloseDate").val();// 结单日期
         var modifiedUserID = $("#txtModifiedUserID").val();// 最后更新人
         var modifiedDate=$("#txtModifiedDate").val();// 最后更新日期
         var remark=$("#txtremark").val(); // 备注
         if(document.getElementById("txtAction").value.Trim()=="1")
        {
            action="Add";
        }
        else
        {
             action="Update";
        }
        
        
     
        
        var DetailID = new Array();
        var Detailchk = new Array();
        var DetailProductID = new Array();
        var DetailProductNo = new Array();
        var DetailProductName = new Array();
        var DetailUnitID = new Array();
        var DetailProductCount = new Array();
        var DetailBackCount = new Array();
        var DetailApplyReason = new Array();
        var DetailUnitPrice = new Array();
        var DetailTaxPrice = new Array();
        var DetailDiscount = new Array();
        var DetailTaxRate = new Array();
        var DetailTotalPrice = new Array();
        var DetailTotalFee = new Array();
        var DetailTotalTax = new Array();
        var DetailRemark = new Array();
        var DetailFromBillID = new Array();
        var DetailFromBillNo = new Array();
        var DetailFromLineNo = new Array();
        var DetailUsedUnitID = new Array();
        var DetailUsedUnitCount = new Array();
        var DetailUsedPrice = new Array();
          
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
        var length = 0;
        var signFrame = findObj("dg_Log",document); 
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            
            if(signFrame.rows[i+1].style.display!="none")
            {
                
                var objID = 'ID'+(i+1);
                var objchk = 'chk'+(i+1);
                var objProductID = 'txtProductID'+(i+1);
                var objProductNo = 'txtProductNo'+(i+1);
                var objProductName = 'txtProductName'+(i+1);
                var objUnitID = 'txtUnitID'+(i+1);
                var objProductCount = 'txtProductCount'+(i+1);
                var objBackCount = 'txtBackCount'+(i+1);
                var objApplyReason = 'txtApplyReason'+(i+1);
                var objUnitPrice = 'txtUnitPrice'+(i+1);
                var objTaxPrice = 'txtTaxPrice'+(i+1);
                var objDiscount = 'txtDiscount'+(i+1);
                var objTaxRate = 'txtTaxRate'+(i+1);
                var objTotalPrice = 'txtTotalPrice'+(i+1);
                var objTotalFee = 'txtTotalFee'+(i+1);
                var objTotalTax = 'txtTotalTax'+(i+1);
                var objRemark = 'txtRemark'+(i+1);
                var objFromBillID = 'txtFromBillID'+(i+1);
                var objFromBillNo = 'txtFromBillNo'+(i+1);
                var objFromLineNo = 'txtFromLineNo'+(i+1);
                
                Detailchk.push(document.getElementById(objchk).value.Trim());
                DetailProductID.push(document.getElementById(objProductID.toString()).value.Trim());
                DetailProductNo.push(document.getElementById(objProductNo.toString()).value.Trim());
                DetailProductName.push(document.getElementById(objProductName.toString()).value.Trim());
                DetailProductCount.push(  document.getElementById(objProductCount.toString()).value.Trim());
                if(isMoreUnit)
                {// 启用多计量单位
                    DetailUnitID.push($("#UsedUnitID"+(i+1)).val());
                    DetailBackCount.push($("#UsedUnitCount"+(i+1)).val());
                    DetailUsedUnitID.push($("#"+objUnitID).val());
                    DetailUsedUnitCount.push($("#"+objBackCount).val());
                    DetailUnitPrice.push($("#hiddUnitPrice"+(i+1)).val());
                    DetailUsedPrice.push(document.getElementById(objUnitPrice.toString()).value.Trim());
                }
                else
                {
                    DetailUnitID.push(document.getElementById(objUnitID.toString()).value.Trim());
                    DetailBackCount.push(document.getElementById(objBackCount.toString()).value.Trim());
                    DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value.Trim());
                }
                
                DetailApplyReason.push(document.getElementById(objApplyReason.toString()).value.Trim());
                DetailTaxPrice.push(document.getElementById(objTaxPrice.toString()).value.Trim());
                DetailTaxRate.push(document.getElementById(objTaxRate.toString()).value.Trim());
                DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value.Trim());
                DetailTotalFee.push(document.getElementById(objTotalFee.toString()).value.Trim());
                DetailTotalTax.push(document.getElementById(objTotalTax.toString()).value.Trim());
                DetailRemark.push(document.getElementById(objRemark.toString()).value.Trim());
                DetailFromBillID.push(document.getElementById(objFromBillID.toString()).value.Trim());
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
                length++;
            }
        }
         if(rejectNo.length<=0)
         {
            isFlag = false;
            fieldText = fieldText + "退货单编号|";
   		    msgText = msgText +  "退货单编号不允许为空|";
         }
         

         var no=  document.getElementById("divPurchaseRejectNo").innerHTML;
         var txtIndentityID = $("#txtIndentityID").val();
 
         
         
         
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/PurchaseManager/PurchaseRejectAdd.ashx",
                  dataType:'json',// 返回json格式数据
                  data: "rejectNo="+escape(rejectNo)
                        +"&title="+escape(title)
                        +"&fromType="+escape(fromType)
                        +"&providerID="+escape(providerID)
                        +"&typeID="+escape(typeID)
                        +"&deptid="+escape(deptid)
                        +"&purchase="+escape(purchase)
                        +"&takeType="+escape(takeType)
                        +"&carryType="+escape(carryType)
                        +"&payeType="+escape(payeType)
                        +"&rate="+escape(rate)
                        +"&currencyType="+escape(currencyType)
                        +"&isAddtax="+escape(isAddtax)
                        +"&receiveMan="+escape(receiveMan)
                        +"&projectID="+escape(txtProjectID)
                        +"&receiveTel="+escape(receiveTel)
                        +"&moneyType="+escape(moneyType)
                        +"&sendAddress="+escape(sendAddress)
                        +"&receiveOverAddress="+escape(receiveOverAddress)
                        +"&countTotal="+escape(countTotal)
                        +"&totalMoney="+escape(totalMoney)
                        +"&totalTax="+escape(totalTax)
                        +"&totalfee="+escape(totalfee)
                        +"&discount="+escape(discount)
                        +"&discounttotal="+escape(discounttotal)
                        +"&realtotal="+escape(realtotal)
                        +"&totalDyfzk="+escape(totalDyfzk)
                        +"&totalYthkhj="+escape(totalYthkhj)
                        +"&creator="+escape(creator)
                        +"&createDate="+escape(createDate)
                        +"&billStatus="+escape(billStatus)
                        +"&rejectDate="+(rejectDate)
                        +"&confirmDate="+escape(confirmDate)
                        +"&closer="+escape(closer)
                        +"&closeDate="+escape(closeDate)
                        +"&modifiedUserID="+escape(modifiedUserID)
                        +"&remark="+escape(remark)
                        +"&DetailProductID="+escape(DetailProductID)
                        +"&DetailProductNo="+escape(DetailProductNo)
                        +"&DetailProductName="+escape(DetailProductName)
                        +"&DetailUnitID="+escape(DetailUnitID)
                        +"&DetailProductCount="+escape(DetailProductCount)
                        +"&DetailBackCount="+escape(DetailBackCount)
                        +"&DetailApplyReason="+escape(DetailApplyReason)
                        +"&DetailUnitPrice="+escape(DetailUnitPrice)
                        +"&DetailUsedPrice="+escape(DetailUsedPrice)
                        +"&DetailTaxPrice="+escape(DetailTaxPrice)
                        +"&DetailDiscount="+escape(DetailDiscount)
                        +"&DetailTaxRate="+escape(DetailTaxRate)
                        +"&DetailTotalPrice="+escape(DetailTotalPrice)
                        +"&DetailTotalFee="+escape(DetailTotalFee)
                        +"&DetailTotalTax="+escape(DetailTotalTax)
                        +"&DetailRemark="+escape(DetailRemark)
                        +"&DetailFromBillID=" + escape(DetailFromBillID) 
                        +"&DetailFromBillNo=" + escape(DetailFromBillNo) 
                        +"&DetailFromLineNo=" + escape(DetailFromLineNo) 
                        +"&DetailUsedUnitID="+escape(DetailUsedUnitID)
                        +"&DetailUsedUnitCount="+escape(DetailUsedUnitCount)
                        +"&action=" + escape(action) 
                        +"&CodeType=" + escape(CodeType) 
                        +"&length=" + escape(length) 
                        +"&cno=" + escape(no) 
                        +"&fflag2=" + escape(fflag2) 
                        +"&ID=" + escape(txtIndentityID) 
                        + GetExtAttrValue() + "", // 数据
                
                  cache:false,
                  beforeSend:function()
                  { 
                      // AddPop();
                  }, 
                // complete :function(){hidePopup();},
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
                           if(CodeType!="")
                           {
                                isnew="edit";
                                document.getElementById("divPurchaseRejectNo").innerHTML=data.data; 
                                document.getElementById("divPurchaseRejectNo").style.display="block";
                                document.getElementById("divInputNo").style.display="none";
                                
                                // 设置源单类型不可改
                                $("#ddlFromType").attr("disabled","disabled");
                                fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                                fnFlowStatus($("#FlowStatus").val()); 
                                GetFlowButton_DisplayControl();// 审批处理
                           }
                           else
                           {
                               document.getElementById("divPurchaseRejectNo").innerHTML=data.data;
                               document.getElementById("divPurchaseRejectNo").style.display="block";
                               document.getElementById("divInputNo").style.display="none";
                               
                               // 设置源单类型不可改
                                $("#ddlFromType").attr("disabled","disabled");    
                                fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val()); 
                                fnFlowStatus($("#FlowStatus").val()); 
                                GetFlowButton_DisplayControl();// 审批处理
                                
                           }
                         
                        }
                        else
                        {
                              popMsgObj.ShowMsg(data.info);
                              document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                              document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                              document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                              fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());  
                              fnFlowStatus($("#FlowStatus").val()); 
                              GetFlowButton_DisplayControl();// 审批处理
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

function Fun_Clear_Input()
{
action="Add";
    window.location='PurchaseContract_Add.aspx'; 
}

// 全选
function SelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

function Isquanxuan()
{
    var signFrame = findObj("dg_Log",document);
    var quanxuan = true;
    for(var i=1;i<signFrame.rows.length;++i)
        {
            if(document.getElementById("chk"+i).checked == false)
            {
                quanxuan = false;
            }
        }
    if(quanxuan)
    {
        document.getElementById("checkall").checked = true;
    }
    else
    {
        document.getElementById("checkall").checked = false;
    }
}

function ShowProdInfo()
{
 popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"','Check');
}


// 多选明细方法

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
           type: "POST",// 用POST方式传输
           dataType:"json",// 数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str='+str,// 目标地址
           cache:false,
               data:'',// 数据
               beforeSend:function(){},// 发送数据之前
         
           success: function(msg){
                    // 数据获取完毕，填充页面据显示
                    // 数据列表
                    $.each(msg.data,function(i,item)
                    {
                    // 填充物品ID，物品编号，物品名称,单位ID，单位名称，规格，
                        if(!IsExistCheck(item.ProdNo))
                        {
                        var Index = AddSignRow();  
                        $("#txtProductID"+Index).attr("value",item.ID);
                        $("#txtProductNo"+Index).attr("value",item.ProdNo);
                        $("#txtProductName"+Index).attr("value",item.ProductName);
                        $("#txtstandard"+Index).attr("value",item.Specification);
                        $("#txtColorName"+Index).attr("value",item.ColorName);
                        $("#txtUnitPrice"+Index).attr("value",FormatAfterDotNumber(parseFloat(item.TaxBuy) ,selPoint));// 去税进价
                        $("#txtTaxPrice"+Index).attr("value",FormatAfterDotNumber(parseFloat(item.StandardBuy) ,selPoint));// 含税进价
                        $("#txtTaxRate"+Index).attr("value",FormatAfterDotNumber(item.InTaxRate,selPoint));// 进项税率
                        if(isMoreUnit)
                        {// 启用多计量单位
                            $("#UsedUnitID"+Index).attr("value",item.UnitID);
                            $("#UsedUnitName"+Index).attr("value",item.CodeName); 
                            GetUnitGroupSelectEx(item.ID, 'InUnit', 'txtUnitID'+Index, 'ChangeUnit(this,'+Index+',1)', 'tdUnitName'+Index,'', 'ChangeUnit(this,'+Index+',1)');
                            $("#txtBackCount"+Index).attr("onblur",'ChangeUnit(this,'+Index+')'); 
                        }
                        else
                        {
                            $("#txtUnitID"+Index).attr("value",item.UnitID);
                            $("#txtUnitName"+Index).attr("value",item.CodeName); 
                        }
                        $("#hiddUnitPrice"+Index).attr("value",FormatAfterDotNumber(parseFloat(item.TaxBuy) ,selPoint));
                        $("#hiddTaxPrice"+Index).attr("value",FormatAfterDotNumber(parseFloat(item.StandardBuy) ,selPoint));
                        $("#hiddTaxRate"+Index).attr("value",FormatAfterDotNumber(item.InTaxRate,selPoint));
                        var isAddTax = document.getElementById("chkIsAddTax").checked;
    // 新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true)
            {// 是增值税则
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于隐藏域含税价
																																									// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();// 税率等于隐藏域税率
                document.getElementById("AddTax").innerHTML = "是增值税";
            }
            else
            {
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于单价
																																									// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(0,selPoint);// 税率等于0
                document.getElementById("AddTax").innerHTML = "非增值税";
            }
        }
    }
                        }
                   });
                     
                      },
             
               complete:function(){}// 接收数据完毕
           });
      closeProductdiv();
}  

function IsExistCheck(prodNo)
{
    var sign=false ;
    var signFrame = findObj("dg_Log",document);
    var DetailLength = 0;// 明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
          var prodNo1 = document.getElementById("txtProductNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}
// /添加行
 function AddSignRow()
 { 
        // 读取最后一行的行号，存放在txtTRLastIndex文本框中
 
        var txtTRLastIndex = findObj("txtTRLastIndex",document);
    
        var rowID = parseInt(txtTRLastIndex.value);
        
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);// 添加行
        newTR.id = "ID" + rowID;
        var i=0;
              
        var newNameXH=newTR.insertCell(i++);// 添加列:选择
        newNameXH.className="cell";
        newNameXH.innerHTML="<input name='chk' id='chk" + rowID + "' size='10' value="+rowID+" type='checkbox'   onclick=\"Isquanxuan();\" class='tdinput'  />";
        
        var newNameTD=newTR.insertCell(i++);// 添加列:序号
        newNameTD.className="cell";
        newNameTD.id = "newNameTD"+rowID;
        newNameTD.innerHTML =GenerateNo(rowID);
        
        var newProductNo=newTR.insertCell(i++);// 添加列:物品ID
        newProductNo.style.display = "none";      
        newProductNo.innerHTML = "<input name='txtProductID" + rowID + "'  id='txtProductID" + rowID + "' type='text' style='width:95%;border:0px'  class='tdinput'/>";// 添加列内容
        
        var newProductNo=newTR.insertCell(i++);// 添加列:物品编号
        newProductNo.className="cell";        
        newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly onclick=\"popTechObj.ShowList('txtProductNo"+rowID+"');\" type='text' style='width:95%;border:0px'  readonly class='tdinput'/>";// 添加列内容
        
        var newProductName=newTR.insertCell(i++);// 添加列:物品名称
        newProductName.className="cell";
        newProductName.innerHTML = "<input name='txtProductName" + rowID + "' id='txtProductName" + rowID + "' type='text'  style='width:95%;border:0px'  readonly  class='tdinput'/>";// 添加列内容
        
        var newProductName=newTR.insertCell(i++);// 添加列:规格(从物品表中带来显示，不往明细表中存)
        newProductName.className="cell";
        newProductName.innerHTML = "<input name='txtstandard" + rowID + "' id='txtstandard" + rowID + "' type='text'  style='width:95%;border:0px'  readonly  class='tdinput'/>";// 添加列内容
        
        var newColorName=newTR.insertCell(i++);// 添加列:颜色(从物品表中带来显示，不往明细表中存)
        newColorName.className="cell";
        newColorName.innerHTML = "<input name='txtColorName" + rowID + "' id='txtColorName" + rowID + "' type='text'  style='width:95%;border:0px'  readonly  class='tdinput'/>";// 添加列内容
        
        if(isMoreUnit)
        {// 启用多计量单位
            // 基本单位
            var UsedUnitName=newTR.insertCell(i++);
            UsedUnitName.className="cell";
            UsedUnitName.innerHTML = "<input name='UsedUnitName" + rowID + "' id='UsedUnitName" + rowID + "' type='text' style='width:95%;border:0px' readonly class='tdinput'/><input id='UsedUnitID"+rowID+"' type='hidden'  >";
            
            // 基本数量
            var UsedUnitCount=newTR.insertCell(i++);
            UsedUnitCount.className="cell";
            UsedUnitCount.innerHTML = "<input name='UsedUnitCount" + rowID + "' id='UsedUnitCount" + rowID + "' type='text' style='width:95%;border:0px' onchange='Number_round(this,"+selPoint+");' readOnly class='tdinput'/>";
            
            // 单位
            var tdUnitName=newTR.insertCell(i++);
            tdUnitName.className="cell";
            tdUnitName.id="tdUnitName"+rowID;
            
        }
        else
        {
            var newUnitID=newTR.insertCell(i++);// 添加列:单位ID
            newUnitID.style.display = "none";
            newUnitID.innerHTML = "<input name='txtUnitID"+rowID+"' id='txtUnitID" + rowID + "'style='width:10%' type='text'  class='tdinput' readonly />";// 添加列内容
            
            var newUnitID=newTR.insertCell(i++);// 添加列:单位
            newUnitID.className="cell";
            newUnitID.innerHTML = "<input name='txtUnitName" + rowID + "' id='txtUnitName" + rowID + "' type='text' style='width:95%;border:0px' readonly class='tdinput'/>";// 添加列内容
        }
        
        var newProductCount=newTR.insertCell(i++);// 添加列:到货数量
        newProductCount.className="cell";
        newProductCount.innerHTML = "<input name='txtProductCount" + rowID + "'  id='txtProductCount" + rowID + "' style='width:95%;border:0px'  readonly  class='tdinput' type='text'/>";// 添加列内容
         
        var newYBackCount=newTR.insertCell(i++);// 添加列:已退货数量
        newYBackCount.style.display = "none";
        newYBackCount.innerHTML = "<input name='txtYBackCount" + rowID + "'  id='txtYBackCount" + rowID + "'  style='width:95%;border:0px'  readonly  class='tdinput' type='text'/>";// 添加列内容
        
        var newBackCount=newTR.insertCell(i++);// 添加列:退货数量
        newBackCount.className="cell";
        newBackCount.innerHTML = "<input name='txtBackCount" + rowID + "'  id='txtBackCount" + rowID + "'  onchange='Number_round(this,"+selPoint+");' onblur=\"ChangeUnit(this,"+rowID+");\" style='width:95%;border:0px'  type='text' class='tdinput'/>";// 添加列内容
        
        var newApplyReason=newTR.insertCell(i++);// 申请原因下拉选择
        newApplyReason.className="cell";
        newApplyReason.innerHTML="<select  style='width:95%;border:0px' class='tdinput' id='txtApplyReason" + rowID + "'>" + document.getElementById("drpApplyReason").innerHTML + "</select>";
            
        var newUnitPrice=newTR.insertCell(i++);// 添加列:单价
        newUnitPrice.className="cell";
        newUnitPrice.innerHTML = "<input name='txtUnitPrice" + rowID + "' id='txtUnitPrice" + rowID + "' type='text' onchange='Number_round(this,"+selPoint+");' onblur='ChangeUnit(this,"+rowID+")'   style='width:95%;border:0px' class='tdinput'/><input type=\"hidden\"  id='hiddUnitPrice" + rowID + "'/>";// 添加列内容
        
        var newUnitPrice=newTR.insertCell(i++);// 添加列:含税价
        newUnitPrice.className="cell";
        newUnitPrice.innerHTML = "<input name='txtTaxPrice" + rowID + "' id='txtTaxPrice" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  class='tdinput'/> <input type=\"hidden\"  id='hiddTaxPrice" + rowID + "'/>";// 添加列内容
        
        var newTaxPricHide=newTR.insertCell(i++);// 添加列:含税价隐藏
        newTaxPricHide.style.display = "none";
        newTaxPricHide.innerHTML = "<input id='OrderTaxPriceHide" + rowID + "'type='text'  style='width:98%'  class='tdinput' />";
        
        var newUnitPrice=newTR.insertCell(i++);// 添加列:税率
        newUnitPrice.className="cell";
        newUnitPrice.innerHTML = "<input name='txtTaxRate" + rowID + "' id='txtTaxRate" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  class='tdinput'/> <input type=\"hidden\"  id='hiddTaxRate" + rowID + "'/>";// 添加列内容
        
        var newTotalPrice=newTR.insertCell(i++);// 添加列:金额
        newTotalPrice.className="cell";
        newTotalPrice.innerHTML = "<input name='txtTotalPrice" + rowID + "' id='txtTotalPrice" + rowID + "' type='text'  style='width:95%;border:0px'   readonly  class='tdinput'/>";// 添加列内容
        $("#txtTotalPrice" + rowID).onfocus = "TotalPrice1();"
        
        var newUnitPrice=newTR.insertCell(i++);// 添加列:含税金额
        newUnitPrice.className="cell";
        newUnitPrice.innerHTML = "<input name='txtTotalFee" + rowID + "' id='txtTotalFee" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  class='tdinput'/>";// 添加列内容
        
        var newUnitPrice=newTR.insertCell(i++);// 添加列:税额
        newUnitPrice.className="cell";
        newUnitPrice.innerHTML = "<input name='txtTotalTax" + rowID + "' id='txtTotalTax" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  class='tdinput'/>";// 添加列内容

        var newRemark=newTR.insertCell(i++);// 添加列:备注
        newRemark.className = "cell";
        newRemark.innerHTML = "<input name='txtRemark" + rowID + "' id='txtRemark" + rowID + "' type='text' style='width:95%;border:0px' class='tdinput'/>";// 添加列内容

        var newFromBillID=newTR.insertCell(i++);// 添加列:源单ID
        newFromBillID.style.display = "none";
        newFromBillID.innerHTML = "<input name='txtFromBillID" + rowID + "' id='txtFromBillID" + rowID + "' type='text' style='width:95%;border:0px'   readonly class='tdinput'/>";// 添加列内容
        
        var newFromBillID=newTR.insertCell(i++);// 添加列:源单编号
        newFromBillID.className = "cell";
        newFromBillID.innerHTML = "<input name='txtFromBillNo" + rowID + "' id='txtFromBillNo" + rowID + "' type='text'  style='width:95%;border:0px'  readonly   class='tdinput'/>";// 添加列内容
        
        var newFromLineNo=newTR.insertCell(i++);// 添加列:源单序号
        newFromLineNo.className="cell";
        newFromLineNo.innerHTML = "<input name='txtFromLineNo" + rowID + "' id='txtFromLineNo" + rowID + "' type='text'  style='width:95%;border:0px'  readonly   class='tdinput'/>";// 添加列内容
        
        var newFromLineNo=newTR.insertCell(i++);// 添加列:已出库数量
        newFromLineNo.className="cell";
        newFromLineNo.innerHTML = "<input name='txtOutedTotal" + rowID + "' id='txtOutedTotal" + rowID + "' type='text'  style='width:95%;border:0px'  readonly   class='tdinput'/>";// 添加列内容

                var newFromLineNo=newTR.insertCell(i++);// 添加列:已入库数量
        newFromLineNo.className="cell";
              newFromLineNo.style.display = "none";    
        newFromLineNo.innerHTML = "<input name='txtInCount" + rowID + "' id='txtInCount" + rowID + "' type='text'  style='width:95%;border:0px'  readonly  class='tdinput'  />";// 添加列内容
        
        txtTRLastIndex.value = (rowID + 1).toString() ;// 将行号推进下一行
        return rowID;
}

function GenerateNo(Edge)
{// 生成序号
    DtlSCnt = findObj("txtTRLastIndex",document);// 明细来源新增行号
    var signFrame = findObj("dg_Log",document);
    var SortNo = 1;// 起始行号
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<Edge;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                document.getElementById("newNameTD"+i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0;// 明细表不存在时返回0
}

function DeleteSignRowReject()
{// 删除明细行，需要将序号重新生成
    var signFrame = findObj("dg_Log",document);        
    var ck = document.getElementsByName("chk");
    for( var i = 0; i<ck.length;i++ )
    {
        var rowID = i+1;
        if ( ck[i].checked )
        {
           signFrame.rows[rowID].style.display="none";
        }
        document.getElementById("newNameTD"+rowID).innerHTML = GenerateNo(rowID);
    }
    var Flag = document.getElementById("drpFromType").value.Trim();
    
    // 判断是否有明细了，若没有了，则将供应商设为可选
    var totalSort = 0;// 明细行数
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
    {// 明细行数大于0
    }
    else
    {// 无明细，供应商可选
        document.getElementById("txtProviderID").style.display="block";
        document.getElementById("txtHiddenProviderID1").style.display="none";
        
        document.getElementById("checkall").checked = false;
        document.getElementById('txtProviderID').disabled=false;
        document.getElementById('drpCurrencyType').disabled=false;
    }
    
    fnTotalInfo();
}
function CheckCount() 
{
    var CountTotal = 0; // 数量合计
    var TotalPrice = 0; // 金额合计
    var Tax = 0; // 税额合计
    var TotalFee = 0; // 含税金额合计
    var Discount = $("#txtDiscount").val(); // 整单折扣
    var DiscountTotal = 0; // 折扣金额
    var RealTotal = 0; // 折后含税金额
    var TotalDyfzk = $("#txtTotalDyfzk").val();; // 抵应付账款
    var Zhekoumingxi = 0;// 明细中折扣合计
            var msg="";
    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        {
        
            var rowid = i;
            var pProductCount = $("#txtProductCount" + rowid).val(); // 到货数量
            var pYBackCount = $("#txtYBackCount" + rowid).val();// 已退货数量
               if (document .getElementById ("drpFromType").value==2)
            {
            pYBackCount=0;
            }
            var pCountDetail = $("#txtBackCount" + rowid).val(); // 退货数量
            if(pCountDetail == "")
            {
                pCountDetail=FormatAfterDotNumber(0,selPoint);
            }
            if(IsNumberOrNumeric(pCountDetail,9,selPoint) == false)
            {
                alert("【退货数量】格式不正确！");
                return;
            }
    
            document.getElementById("txtBackCount" + rowid).value = FormatAfterDotNumber(pCountDetail,selPoint);
            if(pProductCount != "")
            {
            var daoHuoProduct=FormatAfterDotNumber(document .getElementById ("txtInCount"+rowid ).value,selPoint);
                if((parseFloat(pProductCount)-parseFloat (daoHuoProduct )-parseFloat(pYBackCount)) < parseFloat(pCountDetail))
                {
                    var xxxxx = parseFloat(pProductCount)-parseFloat (daoHuoProduct )-parseFloat(pYBackCount);
                 msg =   "第"+i+"行的【退货数量】不能大于当前可用退货数量("+xxxxx+")！";
                    break ;
                }
            }
        }
    }
        return   msg;
}

// 计算各种合计信息
function fnTotalInfo() 
{
    var CountTotal = 0; // 数量合计
    var TotalPrice = 0; // 金额合计
    var Tax = 0; // 税额合计
    var TotalFee = 0; // 含税金额合计
    var Discount = $("#txtDiscount").val(); // 整单折扣
    var DiscountTotal = 0; // 折扣金额
    var RealTotal = 0; // 折后含税金额
    var TotalDyfzk = $("#txtTotalDyfzk").val();; // 抵应付账款
    var Zhekoumingxi = 0;// 明细中折扣合计
    
    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        {
    
            var rowid = i;
            var pProductCount = $("#txtProductCount" + rowid).val(); // 到货数量
            
            var pYBackCount = $("#txtYBackCount" + rowid).val();// 已退货数量
            if (document .getElementById ("drpFromType").value==2)
            {
                pYBackCount=0;
            }
            var pCountDetail = $("#txtBackCount" + rowid).val(); // 退货数量
            if(pCountDetail == "")
            {
                pCountDetail=FormatAfterDotNumber(0,selPoint);
            }
            if(IsNumberOrNumeric(pCountDetail,9,selPoint) == false)
            {
                alert("【退货数量】格式不正确！");
                return;
            }
            document.getElementById("txtBackCount" + rowid).value = FormatAfterDotNumber(pCountDetail,selPoint);
            if(pProductCount != "")
            {
            var daoHuoProduct=FormatAfterDotNumber(document .getElementById ("txtInCount"+rowid ).value,selPoint);
                if((parseFloat(pProductCount)-parseFloat (daoHuoProduct )-parseFloat(pYBackCount)) < parseFloat(pCountDetail))
                {
                    var xxxxx = parseFloat(pProductCount)-parseFloat (daoHuoProduct )-parseFloat(pYBackCount);
                    alert("第"+i+"行的【退货数量】不能大于当前可用退货数量("+xxxxx+")！");
                    return   ;
                }
            }
            var UnitPriceDetail = $("#txtUnitPrice" + rowid).val(); // 单价
        // 判断是否是增值税，不是增值税含税价始终等于单价
       
        
        if(document .getElementById ("chkIsAddTax").checked)
        {}
        else
         {
            $("#txtTaxPrice" + rowid).val($("#txtUnitPrice" + rowid).val());
        }
            var TaxPriceDetail = $("#txtTaxPrice" + rowid).val(); // 含税价
            var TaxRateDetail = $("#txtTaxRate" + rowid).val(); // 税率
            
            var TotalPriceDetail = FormatAfterDotNumber((UnitPriceDetail * pCountDetail  ), selPoint); // 金额=数量*单价*折扣
            var TotalTaxDetail = FormatAfterDotNumber((TotalPriceDetail * TaxRateDetail / 100), selPoint); // 税额=金额
																											// *税率
            var TotalFeeDetail = FormatAfterDotNumber((TaxPriceDetail * pCountDetail   ), selPoint); // 含税金额=数量*含税单价*折扣
            
           
            $("#txtTotalPrice" + rowid).val(FormatAfterDotNumber(TotalPriceDetail, selPoint)); // 金额
            $("#txtTotalTax" + rowid).val(FormatAfterDotNumber(TotalTaxDetail, selPoint)); // 税额
             $("#txtTotalFee" + rowid).val(FormatAfterDotNumber(TotalFeeDetail, selPoint)); // 含税金额
            TotalPrice += parseFloat(TotalPriceDetail);// 金额
            Tax += parseFloat(TotalTaxDetail);// 税额
            TotalFee += parseFloat(TotalFeeDetail);// 含税金额
            CountTotal += parseFloat(pCountDetail);// 数量合计
            Zhekoumingxi += 0;// 明细折扣金额=含税价*数量*（1-折扣）
        }
    }
    $("#txtTotalMoney").val(FormatAfterDotNumber(TotalPrice, selPoint));// 金额合计
    $("#txtTotalTaxHo").val(FormatAfterDotNumber(Tax, selPoint));// 税额合计
    $("#txtTotalFeeHo").val(FormatAfterDotNumber(TotalFee, selPoint));// 含税金额合计
    $("#txtCountTotal").val(FormatAfterDotNumber(CountTotal, selPoint)); // 数量合计
    $("#txtDiscountTotal").val(FormatAfterDotNumber(((TotalFee * (100 - Discount) / 100) + Zhekoumingxi), selPoint)); // 折扣金额
    $("#txtRealTotal").val(FormatAfterDotNumber((TotalFee * Discount / 100), selPoint)); // 折后含税金额
    $("#txtTotalYthkhj").val(FormatAfterDotNumber(((TotalFee -((TotalFee * (100 - Discount) / 100) + Zhekoumingxi)) - TotalDyfzk) , selPoint)); // 应退货款合计
}

function fnChangeAddTax()
{// 改变是否为增值税
    var isAddTax = document.getElementById("chkIsAddTax").checked;
    // 新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
    if(isAddTax == true)
    {
        document.getElementById("AddTax").innerHTML = "是增值税";
    }
    else
    {
        document.getElementById("AddTax").innerHTML = "非增值税";
    }
    
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true)
            {// 是增值税则
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于隐藏域含税价
																																									// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();// 税率等于隐藏域税率
                
            }
            else
            {
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("txtUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于单价
																																									// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(0,selPoint);// 税率等于0
                
            }
        }
    }
    fnTotalInfo();
}



// 获取明细信息
function getDtlSValue()
{
arrlength=0;
 var signFrame = document.getElementById("dg_Log");
 if((typeof(signFrame)!="undefined")&&(signFrame!=null))
 {
    for(i=1;i<signFrame.rows.length;i++)
    {         
      if(signFrame.rows[i].style.display!="none")
       {
 
       var temp = new Array();
        signFrame.rows[i].cells[0].innerHTML = i.toString();
        rowid =parseInt(signFrame.rows[i].cells[0].innerText);
        temp.push(document.getElementById("chk" + rowid).value.Trim());// 0
        temp.push(document.getElementById("txtProductNo" + rowid).value.Trim());//
        temp.push(document.getElementById("txtProductName" + rowid).value.Trim());// 2
        temp.push(document.getElementById("txtProductCount" + rowid).value.Trim());// 3
        temp.push(document.getElementById("txtUnitID" + rowid).value.Trim());// 4
        temp.push(document.getElementById("txtRequireDate" + rowid).value.Trim());// 5
        temp.push(document.getElementById("txtUnitPrice"+rowid).value.Trim());// 6
        temp.push(document.getElementById("txtTotalPrice" + rowid).value.Trim());// 7
        temp.push(document.getElementById("txtTotalPrice" + rowid).value.Trim());// 8
        temp.push(document.getElementById("txtApplyReason" + rowid).value.Trim());// 9需求日期
        temp.push(document.getElementById("txtRemark" + rowid).value.Trim());// 10
        temp.push(document.getElementById("txtFromBillID" + rowid).value.Trim());// 11
        temp.push(document.getElementById("txtFromLineNo" + rowid).value.Trim());// 12
        DtlS_Item[i-1] = temp;  
        arrlength++;
      }
      showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","已缓存配件信息，如需保存请提交！");
   }
   }
}



// 附件处理
function DealResume(flag)
{
    // flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    // 上传附件
    else if ("upload" == flag)
    {
        ShowUploadFile();
    }
    // 清除附件
    else if ("clear" == flag)
    {
            // 设置附件路径
            document.getElementById("hfPageAttachment").value = "";
            // 下载删除不显示
            document.getElementById("divDealResume").style.display = "none";
            // 上传显示
            document.getElementById("divUploadResume").style.display = "block";
    }
    // 下载附件
    else if ("download" == flag)
    {
     // 获取简历路径
            resumeUrl = document.getElementById("hfPageAttachment").value.Trim();
            window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + resumeUrl, "_blank");
    }
}

function AfterUploadFile(url)
{
    if (url != "")
    {
       // alert(url);
// //设置简历路径
// document.getElementById("hfPageResume").value = url;
        // 下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        // 上传不显示
        document.getElementById("divUploadResume").style.display = "none";
        
        
        // 设置简历路径
        document.getElementById("hfPageAttachment").value = url;
    }
}

function ChangeCurreny()
{// 选择币种带出汇率
    var IDExchangeRate = document.getElementById("drpCurrencyType").value.Trim();
    document.getElementById("CurrencyTypeID").value = IDExchangeRate.split('_')[0];
    document.getElementById("txtRate").value = IDExchangeRate.split('_')[1];

    var isAddTax =$("#chkisAddTax").checked;
   
    // 新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
    
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true)
            {// 是增值税则
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于隐藏域含税价
																																									// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();// 税率等于隐藏域税率
                document.getElementById("AddTax").innerHTML = "是增值税";
            }
            else
            {
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("txtUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于单价
																																									// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(0,selPoint);// 税率等于0
                document.getElementById("AddTax").innerHTML = "非增值税";
            }
        }
    }
    
    fnTotalInfo();
}

function DeleteAll()
{

    var Flag= document.getElementById("drpFromType").value.Trim();
    
     if(Flag == 0)
     {
         DeleteSignRow100();
         fnTotalInfo();
         document.getElementById("txtProviderID").style.display="block";
         document.getElementById("txtHiddenProviderID1").style.display="none";
         document.getElementById('txtProviderID').disabled=false;
        document.getElementById('drpCurrencyType').disabled=false;
     }
     else
     {
        document.getElementById('txtProviderID').disabled=false;
        document.getElementById('drpCurrencyType').disabled=false;
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







// 验证
/*
 * 基本信息校验
 */
function CheckBaseInfo()
{

     var fieldText = "";
      var msgText = "";
      var isFlag = true;   
      
      // 先检验页面上的特殊字符
// var RetVal=CheckSpecialWords();
// if(RetVal!="")
// {
// isFlag = false;
// fieldText = fieldText + RetVal+"|";
// msgText = msgText +RetVal+ "不能含有特殊字符|";
// }
//      
      var ssw = CheckSpecialWords();
      if (ssw != "") {
 isFlag = false;
          var tmpKeys = ssw.split(',');
          for (var i = 0; i < tmpKeys.length; i++) {
        isFlag = false;
              fieldText = fieldText + tmpKeys[i].toString() + "|";
              msgText = msgText + "不能含有特殊字符|";
          }
      }


    // 新建时，编号选择手工输入时
    if (document.getElementById("txtAction").value.Trim()=="1")
    {
// 获取编码规则下拉列表选中项
        codeRule = document.getElementById("CodingRuleControl1_ddlCodeRule").value.Trim();
        // 如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            // 获取输入的编号
            employeeNo = document.getElementById("CodingRuleControl1_txtCode").value.Trim();
            // 编号必须输入
            if (employeeNo == "")
            {
                isFlag = false;
                fieldText += "单据编号|";
   		        msgText += "请输入单据编号|";
            }
            else
            {
                if(!CodeCheck($.trim($("#CodingRuleControl1_txtCode").val())))
                {
                    isFlag = false;
                    fieldText = fieldText + "单据编号|";
   		            msgText = msgText +  "单据编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
                }
                else if(strlen($.trim($("#CodingRuleControl1_txtCode").val())) > 50)
                {
                    isErrorFlag = true;
                    fieldText += "单据编号|";
   		            msgText += "单据编号长度不大于50|";
   		        }
   		        
            }
        }    
     }         

    // 整单折扣验证是否为数字
    if(document.getElementById("txtDiscount").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtDiscount").value.Trim(),12,selPoint) == false)
        {
            isFlag = false;
            fieldText += "整单折扣|";
            msgText += "请输入正确的整单折扣|";
        }
    }
    if(document.getElementById("txtTotalMoney").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtTotalMoney").value.Trim(),20,selPoint) == false)
        {
            isFlag = false;
            fieldText += "金额合计|";
            msgText += "请输入正确的金额合计|";
        }
    }
    if(document.getElementById("txtTotalTaxHo").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtTotalTaxHo").value.Trim(),20,selPoint) == false)
        {
            isFlag = false;
            fieldText += "税额合计|";
            msgText += "请输入正确的税额合计|";
        }
    }
    if(document.getElementById("txtTotalFeeHo").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtTotalFeeHo").value.Trim(),20,selPoint) == false)
        {
            isFlag = false;
            fieldText += "含税金额合计|";
            msgText += "请输入正确的含税金额合计|";
        }
    }
    if(document.getElementById("txtDiscountTotal").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtDiscountTotal").value.Trim(),20,selPoint) == false)
        {
            isFlag = false;
            fieldText += "折扣金额合计|";
            msgText += "请输入正确的折扣金额合计|";
        }
    }
    if(document.getElementById("txtRealTotal").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtRealTotal").value.Trim(),20,selPoint) == false)
        {
            isFlag = false;
            fieldText += "折后含税金额|";
            msgText += "请输入正确的折后含税金额|";
        }
    }
    if(document.getElementById("txtTotalDyfzk").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtTotalDyfzk").value.Trim(),12,selPoint) == false)
        {
            isFlag = false;
            fieldText += "抵应付账款|";
            msgText += "请输入正确的抵应付账款|";
        }
    }
    if(document.getElementById("txtRate").value.Trim() !="")
    {
        if(IsNumberOrNumeric(document.getElementById("txtRate").value.Trim(),12,4) == false)
        {
            isFlag = false;
            fieldText += "汇率|";
            msgText += "请输入正确的汇率|";
        }
    }
    if(document.getElementById("txtTitle").value.Trim() == "")
    {
    }
    else
    {
        if(strlen(document.getElementById("txtTitle").value.Trim())>100)
        {
            isFlag = false;
            fieldText +=  "单据主题|";
   		    msgText +=  "单据主题仅限于100个字符以内|";      
        }
    }
    if(document.getElementById("txtProviderID").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "供应商|";
        msgText += "请输入供应商|";
    }
    if(document.getElementById("txtReceiveMan").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "收货人|";
        msgText += "请输入收货人|";
    }
    if(document.getElementById("UserPurchaser").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "采购员|";
        msgText += "请输入采购员|";
    }
    if(document.getElementById("txtRejectDate").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "退货时间|";
        msgText += "请输入退货时间|";
    }
    
    // 限制字数
    var ReceiveMan=document.getElementById("txtReceiveMan").value.Trim();// 收货人
    var SendAddress=document.getElementById("txtSendAddress").value.Trim();// 发货地址
    var ReceiveOverAddress=document.getElementById("txtReceiveOverAddress").value.Trim();// 收货地址
    var Remark=document.getElementById("txtremark").value.Trim();// 备注
    if(strlen(ReceiveMan)>50)
    {
        isFlag = false;
        fieldText += "收货人|";
   		msgText +=  "收货人仅限于50个字符以内|";      
    }
    if(strlen(SendAddress)>200)
    {
        isFlag = false;
        fieldText += "发货地址|";
   		msgText +=  "发货地址仅限于200个字符以内|";      
    }
    if(strlen(ReceiveOverAddress)>200)
    {
        isFlag = false;
        fieldText += "收货地址|";
   		msgText +=  "收货地址仅限于200个字符以内|";      
    }
    if(strlen(Remark)>200)
    {
        isFlag = false;
        fieldText += "备注|";
   		msgText +=  "备注仅限于200个字符以内|";      
    }
    
    // 明细来源的校验
    var signFrame = document.getElementById("dg_Log");
    if((typeof(signFrame) != "undefined")||signFrame != null)
    {
       RealCount = 0;
         for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                RealCount++;
                var BackCount = "txtBackCount"+i;
                var no = document.getElementById(BackCount).value.Trim();
                var remarks = "txtRemark"+i;
                var Remarks = document.getElementById(remarks).value.Trim();
                var ProductNo1 = "txtProductNo"+i;
                var ProductNo2 = document.getElementById(ProductNo1).value.Trim();
                if (no=="")
                {
                           isFlag = false;
                        fieldText +="退货数量|";
                        msgText += ProductNo2+"的退货数量不允许为空|";
                }
                else
                {
                    if( !IsNumeric(no,12,selPoint)  )
                    {
                   
                       isFlag = false;
                        fieldText +="退货数量|";
                        msgText +="请输入正确的物品编号为"+ProductNo2+"的退货数量|";
                    
                    
                    }
                    else
                    {
                      if ( no==0||no<0 )
                       {
                       isFlag = false;
                        fieldText +="退货数量|";
                        msgText +="物品编号为"+ProductNo2+"的退货数量需大于零|";
                       }
                    }
                }
                
                var UnitPrice = "txtUnitPrice" + i;
                var no1 = document.getElementById(UnitPrice).value.Trim();
                if(IsNumeric(no1,12,selPoint) == false)
                {
                    isFlag = false;
                    fieldText +="单价|";
                    msgText +="请输入正确的物品编号为"+ProductNo2+"的单价数量";
                }
                if(strlen(Remarks)>200)
                {
                    isFlag = false;
                    fieldText += "备注|";
   		            msgText +=  "物品编号为"+ProductNo2+"的备注仅限于200个字符以内|";      
                }
            }
        }
        if(RealCount == 0)
        {
            isFlag = false;
            fieldText += "采购退货明细|";
            msgText += "采购退货明细不能为空|";
        }
    }
    else
    {
        isFlag = false;
        fieldText +="采购退货明细|";
        msgText +="采购退货明细不能为空|";
    }
    
 var mss=CheckCount();
 if (mss!="")
 {
         isFlag = false;
        fieldText +="采购退货明细|";
        msgText += mss + "|";
 }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}



function PurchaseRejectSelect()
{
    var Flag= document.getElementById("drpFromType").value.Trim();
    
    if(Flag == 0)
    {// 无来源
        document.getElementById("txtProviderID").style.display="none";
        document.getElementById("txtHiddenProviderID1").style.display="block";
    }
    else if(Flag==1)
    {// 采购订单
 
        var ProviderID = document.getElementById("txtHidProviderID").value.Trim();
        var CurrencyTypeID = document.getElementById("CurrencyTypeID").value.Trim();
        var Rate = document.getElementById("txtRate").value.Trim();
        var signFrame = findObj("dg_Log",document);        
        var ck = document.getElementsByName("chk");
        var totalSort = 0;// 明细行数
        for( var i = 0; i<ck.length;i++ )
        {
            var rowID = i+1;
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
        
        if(ProviderID =="")
        {
            ProviderID = 0;
        }
        popOrder.ShowList('',ProviderID,CurrencyTypeID,Rate,isMoreUnit);
    }
        else if( Flag==2)// 采购入库
    {
 
        var StorageProviderID = document.getElementById("txtHidProviderID").value.Trim();
        var StorageCurrencyTypeID = document.getElementById("CurrencyTypeID").value.Trim();
        var StorageRate = document.getElementById("txtRate").value.Trim();
        var StoragesignFrame = findObj("dg_Log",document);        
        var Storageck = document.getElementsByName("chk");
        var StoragetotalSort = 0;// 明细行数
        for( var Storagei = 0; Storagei<Storageck.length;Storagei++ )
        {
            var StoragerowID = Storagei+1;
            if (StoragesignFrame.rows[StoragerowID].style.display!="none")
            {
               StoragetotalSort++;
               break;
            }
        }
        if(StoragetotalSort == 0)
        {
            StorageRate = 0;
        }
        
        if(StorageProviderID =="")
        {
            StorageProviderID = 0;
        }
        popStorageObject.ShowList('',StorageProviderID,StorageCurrencyTypeID,StorageRate,isMoreUnit);
    }
}


function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)// 选择供应商带出部分信息
{  
 
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    document.getElementById("txtHiddenProviderID1").value = providername;// 当选择源单类型时，用于显示供应商并且不允许再修改
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

// 判断是否有相同记录有返回true，没有返回false
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
// /遍历选择的是否都是同一个供应商
function CheckIsOneProvider()
{
    var ck = document.getElementsByName("ChkBoxPPurReject");
    var  getCheck=new Array ();
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
                  getCheck.push (  $("#PPurRejectProviderID"+i).html());
        }
    }  
    if (getCheck.length >1)
    {
         
          for (var a=0;a<getCheck.length;a++)
          {
          if (getCheck [a]==getCheck [0])
          {
          continue ;
          }
          else
          {
          return false;
          break ;
          }
          }
    }
   return true ;
}

 


function GetValuePPurReject()
{ 
   if ( !CheckIsOneProvider())
   {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的明细只能来自一个供应商！");
        return ;
   }
    var ck = document.getElementsByName("ChkBoxPPurReject");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            if(isMoreUnit)
            {
                FillFromArrive("1",  $("#PPurRejectProductID"+i).html(), $("#PPurRejectProductNo"+i).html(), $("#PPurRejectProductName"+i).html(), $("#PPurRejectstandard"+i).html(), $("#PPurRejectUsedUnitID"+i).html(), 
                $("#PPurRejectUsedUnitName"+i).html(), $("#PPurRejectUsedUnitCount"+i).html(), $("#PPurRejectUsedPrice"+i).html(), $("#PPurRejectTaxPrice"+i).html(), $("#PPurRejectDiscount"+i).html(), 
                $("#PPurRejectTaxRate"+i).html(), $("#PPurRejectTotalPrice"+i).html(), $("#PPurRejectTotalFee"+i).html(), $("#PPurRejectTotalTax"+i).html(), $("#PPurRejectRemark"+i).html(), 
                $("#PPurRejectFromBillID"+i).html(), $("#PPurRejectFromBillNo"+i).html(), $("#PPurRejectFromLineNo"+i).html(), $("#PPurRejectBackCount"+i).html(),
                $("#PPurRejectProviderID"+i).html(), $("#PPurRejectProviderName"+i).html(), $("#PPurRejectCurrencyType"+i).html(), $("#PPurRejectCurrencyTypeName"+i).html()
                , $("#PPurRejectRate"+i).html() , $("#PPurRejectInCount"+i).html(), $("#PPurRejectUnitID"+i).html(), $("#PPurRejectColorName"+i).html());
            }
            else
            {
                FillFromArrive("1",  $("#PPurRejectProductID"+i).html(), $("#PPurRejectProductNo"+i).html(), $("#PPurRejectProductName"+i).html(), $("#PPurRejectstandard"+i).html(), $("#PPurRejectUnitID"+i).html(), 
                $("#PPurRejectUnitName"+i).html(), $("#PPurRejectProductCount"+i).html(), $("#PPurRejectUnitPrice"+i).html(), $("#PPurRejectTaxPrice"+i).html(), $("#PPurRejectDiscount"+i).html(), 
                $("#PPurRejectTaxRate"+i).html(), $("#PPurRejectTotalPrice"+i).html(), $("#PPurRejectTotalFee"+i).html(), $("#PPurRejectTotalTax"+i).html(), $("#PPurRejectRemark"+i).html(), 
                $("#PPurRejectFromBillID"+i).html(), $("#PPurRejectFromBillNo"+i).html(), $("#PPurRejectFromLineNo"+i).html(), $("#PPurRejectBackCount"+i).html(),
                $("#PPurRejectProviderID"+i).html(), $("#PPurRejectProviderName"+i).html(), $("#PPurRejectCurrencyType"+i).html()
                , $("#PPurRejectCurrencyTypeName"+i).html(), $("#PPurRejectRate"+i).html() , $("#PPurRejectInCount"+i).html(),'', $("#PPurRejectColorName"+i).html());
            }
        }
    }  
}
function CheckIsOneStorageProvider()
{
    var ck = document.getElementsByName("ChkStorageReject");
    var  getCheck=new Array ();
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
                  getCheck.push (  $("#PStorageProviderID"+i).html());
        }
    }  
    if (getCheck.length >1)
    {
         
          for (var a=0;a<getCheck.length;a++)
          {
          if (getCheck [a]==getCheck [0])
          {
          continue ;
          }
          else
          {
          return false;
          break ;
          }
          }
    }
   return true ;
}
function GetValueStorageReject()
{ 
   if ( !CheckIsOneStorageProvider())
   {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的明细只能来自一个供应商！");
        return ;
   }
   
    var ck = document.getElementsByName("ChkStorageReject");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            if(isMoreUnit)
            {
                FillFromArrive("2",$("#PStorageProductID"+i).html(), $("#PStorageProductNo"+i).html(), $("#PStorageProductName"+i).html(), $("#PStorageStandard"+i).html(), $("#PStorageUsedUnitID"+i).html(), 
                $("#PStorageUsedUnitName"+i).html(), $("#PStorageUsedUnitCount"+i).html(), $("#PStorageUsedPrice"+i).html(), $("#PStorageTaxPrice"+i).html(), $("#PStorageDiscount"+i).html(), 
                $("#PStorageTaxRate"+i).html(), $("#PStorageTotalPrice"+i).html(), "0", "0", $("#PStorageRemark"+i).html(), 
                $("#PStorageFromBillID"+i).html(), $("#PStorageFromBillNo"+i).html(), $("#PStorageFromLineNo"+i).html(), $("#PStorageBackCount"+i).html(),
                $("#PStorageProviderID"+i).html(), $("#PStorageProviderName"+i).html(), $("#PStorageCurrencyType"+i).html(), $("#PStorageCurrencyTypeName"+i).html(), $("#PStorageRejectRate"+i).html(),"0", $("#PStorageUnitID"+i).html(), $("#PStorageColorName"+i).html()  );
            }
            else
            {
                FillFromArrive("2",$("#PStorageProductID"+i).html(), $("#PStorageProductNo"+i).html(), $("#PStorageProductName"+i).html(), $("#PStorageStandard"+i).html(), $("#PStorageUnitID"+i).html(), 
                $("#PStorageUnitName"+i).html(), $("#PStorageProductCount"+i).html(), $("#PStorageUnitPrice"+i).html(), $("#PStorageTaxPrice"+i).html(), $("#PStorageDiscount"+i).html(), 
                $("#PStorageTaxRate"+i).html(), $("#PStorageTotalPrice"+i).html(), "0", "0", $("#PStorageRemark"+i).html(), 
                $("#PStorageFromBillID"+i).html(), $("#PStorageFromBillNo"+i).html(), $("#PStorageFromLineNo"+i).html(), $("#PStorageBackCount"+i).html(),
                $("#PStorageProviderID"+i).html(), $("#PStorageProviderName"+i).html(), $("#PStorageCurrencyType"+i).html(), $("#PStorageCurrencyTypeName"+i).html(), $("#PStorageRejectRate"+i).html(),"0",'', $("#PStorageColorName"+i).html()  );
            }
        }
    }  
}




function FillFromArrive(sign,productid,productno,productname,standard,unitid,unitname,productcount,unitprice
                        ,taxprice,discount,taxrate,totalprice,totalfee,totaltax,remark,frombillid,frombillno
                        ,fromLineno,backcount,providerid,providername,currencytype,currencytypename,rate,InCount
                        ,usedUnitID,ColorName)
{
 
    if(!ExistFromBill(productid,frombillid,fromLineno))
        {
            var Index = findObj("txtTRLastIndex",document).value.Trim();
            AddSignRow();
            var ProductID = 'txtProductID'+Index;
            var ProductNo = 'txtProductNo'+Index;
            var ProductName = 'txtProductName'+Index;
            var Standard = 'txtstandard'+Index;
            var txtColorName = 'txtColorName'+Index;
            var UnitID = 'txtUnitID'+Index;
            var UnitName = 'txtUnitName'+Index;
            var ProductCount = 'txtProductCount'+Index;
            var UnitPrice = 'txtUnitPrice'+Index;
            var TaxPrice = 'txtTaxPrice'+Index;
            var Discount = 'txtDiscount'+Index;
            var TaxRate = 'txtTaxRate'+Index;
            var Remark = 'txtRemark'+Index;
            var FromBillID = 'txtFromBillID'+Index;
            var FromBillNo = 'txtFromBillNo'+Index;
            var FromLineNo = 'txtFromLineNo'+Index;
            var HiddTaxPrice = 'hiddTaxPrice'+Index;
            var HiddTaxRate = 'hiddTaxRate'+Index;
            var YBackCount = 'txtYBackCount'+Index;
            var BackCount = 'txtBackCount'+Index;
            var HiddUnitPrice = 'hiddUnitPrice'+Index;
           var hiddtxtInCount='txtInCount'+Index ;
           
            
            document.getElementById(ProductID).value = productid;
            document.getElementById(ProductNo).value = productno;
            document.getElementById(ProductName).value = productname;
            document.getElementById(Standard).value = standard;
            document.getElementById(txtColorName).value = ColorName;
            document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice,selPoint);
            document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate,selPoint);
            document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(unitprice,selPoint);
            if(isMoreUnit)
            {// 启用多计量单位
                document.getElementById('UsedUnitName'+Index).value=unitname;
                document.getElementById('UsedUnitID'+Index).value=unitid;
                GetUnitGroupSelectEx(productid, 'InUnit', 'txtUnitID'+Index, 'ChangeUnit(this,'+Index+',1)', 'tdUnitName'+Index,usedUnitID,'SetDisabled('+Index+');ChangeUnit(this,'+Index+',1);');
                $("#txtBackCount"+Index).attr("onblur",'ChangeUnit(this,'+Index+')');               
            }   
            else
            {
                document.getElementById("txtUnitID"+Index).value = unitid;
                document.getElementById("txtUnitName"+Index).value=unitname;
            }
            document.getElementById(ProductCount).value = FormatAfterDotNumber(productcount,selPoint);
            document.getElementById(hiddtxtInCount).value=FormatAfterDotNumber(InCount,selPoint);
            document.getElementById(YBackCount).value = FormatAfterDotNumber(backcount,selPoint);
            document.getElementById(BackCount).value = FormatAfterDotNumber(0,selPoint);// 默认为0，好计算金额
            document.getElementById(UnitPrice).value = FormatAfterDotNumber(unitprice,selPoint);
            document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice,selPoint);
            document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate,selPoint);
            document.getElementById(Remark).value = remark;
            document.getElementById(FromBillID).value = frombillid;
            document.getElementById(FromBillNo).value = frombillno;
            document.getElementById(FromLineNo).value = fromLineno;
            
           
        }
    if (sign=="1")
    {
        closeMaterialdiv();
        }
        else
        {
        closeMadiv();
        }
        
        // 带出供应商及币种等信息
        document.getElementById("txtHidProviderID").value = providerid;
        document.getElementById("txtProviderID").value = providername;
        document.getElementById("txtHiddenProviderID1").value = providername;
        for(var i=0;i<document.getElementById("drpCurrencyType").options.length;++i)
        {
            if(document.getElementById("drpCurrencyType").options[i].value.split('_')[0]== currencytype)
            {
                $("#drpCurrencyType").attr("selectedIndex",i);
                break;
            }
        }
        document.getElementById("CurrencyTypeID").value = currencytype;
        document.getElementById("txtRate").value = rate;
        document.getElementById('txtProviderID').disabled=true;
        document.getElementById('drpCurrencyType').disabled=true;
  
        var isAddTax = document.getElementById("chkIsAddTax").checked;
        // 新加币种的汇率问题
        var Rate = document.getElementById("txtRate").value.Trim();
        
        var signFrame = findObj("dg_Log",document);
        for (i = 1; i < signFrame.rows.length; i++)
        {
            if (signFrame.rows[i].style.display != "none")
            {
                var rowIndex = i;
                if(isAddTax == true)
                {// 是增值税则
                    document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于隐藏域含税价
																																								// 再除以汇率(币种要求修改)
                    document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                    document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();// 税率等于隐藏域税率
                    document.getElementById("AddTax").innerHTML = "是增值税";
                }
                else
                {
                    document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于单价
																																								// 再除以汇率(币种要求修改)
                    document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                    document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(0,selPoint);// 税率等于0
                    document.getElementById("AddTax").innerHTML = "非增值税";
                }
            }
        }
}

// 设置单位不可以修改
function SetDisabled(iRow)
{
    $("#txtUnitID"+iRow).attr("disabled","true");
}

 function Fun_FillParent_Content(id,no,productname,dddf,unitid,unit,df,sdfge,discount,standard
                                    ,fg,fgf,taxprice,price,taxrate,ColorName)
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
    var txtColorName='txtColorName'+index;
    var TaxPrice = 'txtTaxPrice'+index;
    var Discount = 'txtDiscount'+index;
    var TaxRate = 'txtTaxRate'+index;
    var HiddTaxPrice = 'hiddTaxPrice'+index;
    var HiddTaxRate = 'hiddTaxRate'+index;
    var HiddUnitPrice = 'hiddUnitPrice'+index;
    
    document.getElementById(TaxRate).value = taxrate ;
    document.getElementById(TaxPrice).value = taxprice;
    document.getElementById(Standard).value=standard;
    document.getElementById(txtColorName).value=ColorName;
    if(isMoreUnit)
    {// 启用多计量单位
        document.getElementById('UsedUnitName'+index).value=unit;
        document.getElementById('UsedUnitID'+index).value=unitid;
        GetUnitGroupSelectEx(id, 'InUnit', 'txtUnitID'+index, 'ChangeUnit(this,'+index+',1)', 'tdUnitName'+index,'', 'ChangeUnit(this,'+index+',1)');
        $("#txtBackCount"+index).attr("onblur",'ChangeUnit(this,'+index+')');               
    }
    else
    {
        document.getElementById("txtUnitID"+index).value = unitid;
        document.getElementById("txtUnitName"+index).value=unit;
    }
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    document.getElementById(Price).value = price;
    document.getElementById(HiddTaxPrice).value = taxprice;
    document.getElementById(HiddTaxRate).value = taxrate ;
    document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(price,selPoint);
    
    var isAddTax = document.getElementById("chkIsAddTax").checked;
    // 新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true)
            {// 是增值税则
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于隐藏域含税价
																																							// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxRate"+rowIndex).value.Trim(),selPoint);// 税率等于隐藏域税率
                document.getElementById("AddTax").innerHTML = "是增值税";
            }
            else
            {
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于单价
																																							// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(0,selPoint);// 税率等于0
                document.getElementById("AddTax").innerHTML = "非增值税";
            }
        }
    }
    
}


function FillUnit(unitid,unitname) // 回填单位
{
    var i = popUnitObj.InputObj;
    var UnitID = "txtUnitID"+i;
    var UnitName = "txtUnitName"+i;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(UnitName).value = unitname;
}



// 确认
function Fun_ConfirmOperate()
{
var c=window.confirm("确认执行确认操作吗？")
    if(c==true)
    {
    var ActionArrive = document.getElementById("txtAction").value
        
        if(ActionArrive == "1")
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存再确认！");
            return;
        }
        
        glb_BillID = document.getElementById('txtIndentityID').value;
        document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value.Trim();
        document.getElementById("txtConfirmor").value = document.getElementById("UserID").value.Trim();
        document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value.Trim();
        action ="Confirm";
        
        var DetailBackCount = new Array();
	    var DetailFromBillNo = new Array();
        var DetailFromLineNo = new Array();
         var DetailProductNo= new Array();
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
        var length = 0;
        var signFrame = findObj("dg_Log",document); 
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objBackCount = 'txtBackCount'+(i+1);
                var objFromBillNo = 'txtFromBillNo'+(i+1);
                var objFromLineNo = 'txtFromLineNo'+(i+1);
                    var objProductNo= 'txtProductID'+(i+1);
                
                DetailBackCount.push(document.getElementById(objBackCount.toString()).value.Trim());
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
                  DetailProductNo.push(document.getElementById(objProductNo.toString()).value.Trim());
                length++;
            }
        }
        var FromType=document .getElementById ("drpFromType").value.Trim();
        var confirmor = document.getElementById("txtConfirmor").value.Trim();
        var rejectNo = document.getElementById("divPurchaseRejectNo").innerHTML;
        var strParams = "action="+action+"&confirmor="+confirmor+"&rejectNo="+rejectNo+"&ID="+glb_BillID+"&DetailBackCount="+DetailBackCount+"&DetailFromBillNo="+DetailFromBillNo+"&DetailFromLineNo="+DetailFromLineNo+"&DetailProductNo="+DetailProductNo+"&FromType="+FromType+"&length="+length+"";
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseRejectAdd.ashx?"+strParams,
               
            dataType:'json',// 返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                      }, 
                      error: function() 
                      {
                       // popMsgObj.ShowMsg('sdf');
                      }, 
                      success:function(data) 
                      { 
                        if(data.sta>0)
                        {document.getElementById("ddlBillStatus").value = "2";
                            popMsgObj.ShowMsg('确认成功');
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val()); 
                            GetFlowButton_DisplayControl();// 审批处理
                        }
                        else
                        {
                              popMsgObj.ShowMsg(data.data);
                        }
                      } 
                   });  
        } 
}



// 取消确认
function Fun_UnConfirmOperate()
{
var c=window.confirm("确认执行取消确认操作吗？")
    if(c==true)
    {
        
    // document.getElementById("txtBillStatus").value = "执行";
    
        
        glb_BillID = document.getElementById('txtIndentityID').value;
        document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value.Trim();
        document.getElementById("txtConfirmor").value = document.getElementById("UserID").value.Trim();
        document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value.Trim();
        action ="CancelConfirm";
        
        var DetailBackCount = new Array();
	    var DetailFromBillNo = new Array();
        var DetailFromLineNo = new Array();
        var DetailProductNo= new Array();
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
        var length = 0;
        var signFrame = findObj("dg_Log",document); 
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objBackCount = 'txtBackCount'+(i+1);
                var objFromBillNo = 'txtFromBillNo'+(i+1);
                var objFromLineNo = 'txtFromLineNo'+(i+1);
                 var objProductNo= 'txtProductID'+(i+1);
                
                DetailBackCount.push(document.getElementById(objBackCount.toString()).value.Trim());
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
                DetailProductNo.push(document.getElementById(objProductNo.toString()).value.Trim());
                length++;
            }
        }
            var FromType=document .getElementById ("drpFromType").value.Trim();
        var confirmor = document.getElementById("txtConfirmor").value.Trim();
        var confirmdate = document.getElementById("txtConfirmDate").value.Trim();
        var rejectNo = document.getElementById("divPurchaseRejectNo").innerHTML;
        var strParams = "action="+action+"&confirmor="+confirmor+"&confirmdate="+confirmdate+"&rejectNo="+rejectNo+"&ID="+glb_BillID+"&DetailBackCount="+DetailBackCount+"&DetailFromBillNo="+DetailFromBillNo+"&DetailFromLineNo="+DetailFromLineNo+"&DetailProductNo="+DetailProductNo+"&FromType="+FromType+"&length="+length+"";
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseRejectAdd.ashx?"+strParams,
               
            dataType:'json',// 返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                      }, 
                      error: function() 
                      {
                       // popMsgObj.ShowMsg('sdf');
                      }, 
                      success:function(data) 
                      { 
                        if(data.sta>0)
                        {    document.getElementById("ddlBillStatus").value = "1";
                            popMsgObj.ShowMsg('取消确认成功');
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val()); 
                            GetFlowButton_DisplayControl();// 审批处理
                        }
                        else
                        {
                            popMsgObj.ShowMsg('该单据已被其它单据调用了，不允许取消确认！');
                        }
                      } 
                   });  
        } 
}


// 结单或取消结单按钮操作
function Fun_CompleteOperate(isComplete)
{
if(isComplete)
{
    var c=window.confirm("确认执行结单操作吗？")
    if(c==true)
    {
    glb_BillID = document.getElementById('txtIndentityID').value;
    document.getElementById("txtCloserReal").value = document.getElementById("UserName").value.Trim();
    document.getElementById("txtCloser").value = document.getElementById("UserID").value.Trim();
    document.getElementById("txtCloseDate").value = document.getElementById("SystemTime").value.Trim();
    var closer = document.getElementById("txtCloser").value.Trim();
    var rejectNo = document.getElementById("divPurchaseRejectNo").innerHTML;
    
    if(isComplete)
    {
        action ="Close";
    }
    else
    {
        action ="CancelClose";
    }
    // 结单操作
        
        var strParams = "action="+action+"&closer="+closer+"&rejectNo="+rejectNo+"&ID="+glb_BillID+"";
        $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseRejectAdd.ashx?"+strParams,
           
        dataType:'json',// 返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                   // popMsgObj.ShowMsg('sdf');
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta>0)
                    {
                        if(data.sta == 1)
                        {
                            document.getElementById("ddlBillStatus").value = "4";
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val()); 
                        }
                        else
                        {
                            document.getElementById("ddlBillStatus").value = "2";
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val()); 
                        }
// popMsgObj.ShowMsg('确认成功');
                        popMsgObj.ShowMsg(data.info);
                        GetFlowButton_DisplayControl();// 审批处理
                    }
                  } 
               });   
        }
}
else
{
    var c=window.confirm("确认执行取消结单操作吗？")
    if(c==true)
    {
    glb_BillID = document.getElementById('txtIndentityID').value;
    document.getElementById("txtCloserReal").value = document.getElementById("UserName").value.Trim();
    document.getElementById("txtCloser").value = document.getElementById("UserID").value.Trim();
    document.getElementById("txtCloseDate").value = document.getElementById("SystemTime").value.Trim();
    var closer = document.getElementById("txtCloser").value.Trim();
    var rejectNo = document.getElementById("divPurchaseRejectNo").innerHTML;
    
    if(isComplete)
    {
        action ="Close";
    }
    else
    {
        action ="CancelClose";
    }
    // 结单操作
        
        var strParams = "action="+action+"&closer="+closer+"&rejectNo="+rejectNo+"&ID="+glb_BillID+"";
        $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseRejectAdd.ashx?"+strParams,
           
        dataType:'json',// 返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                   // popMsgObj.ShowMsg('sdf');
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta>0)
                    {
                        if(data.sta == 1)
                        {
                            document.getElementById("ddlBillStatus").value = "4";
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val()); 
                        }
                        else
                        {
                            document.getElementById("ddlBillStatus").value = "2";
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val()); 
                        }
// popMsgObj.ShowMsg('确认成功');
                        popMsgObj.ShowMsg(data.info);
                        GetFlowButton_DisplayControl();// 审批处理
                    }
                  } 
               });   
        }
}
}



function Fun_FlowApply_Operate_Succeed(operateType)
{
try{
    if(operateType == "0")
    {// 提交审批成功后,不可改
        $("#imgUnSave").css("display", "inline");// 保存灰
        $("#imgSave").css("display", "none");// 保存
        
        $("#imgAdd").css("display", "none");// 明细添加
        $("#imgUnAdd").css("display", "inline");// 明细添加灰
        $("#imgDel").css("display", "none");// 明细删除
        $("#imgUnDel").css("display", "inline");// 明细删除灰
        $("#Get_Potential").css("display", "none");// 源单总览
        $("#Get_UPotential").css("display", "inline"); // 源单总览灰
        $("#btnGetGoods").css("display", "none");// 条码扫描按钮
    }
    else if(operateType == "1")
    {// 审批成功后，不可改
        $("#imgUnSave").css("display", "inline");
        $("#imgSave").css("display", "none");
        
        
        $("#imgAdd").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        $("#imgDel").css("display", "none");
        $("#imgUnDel").css("display", "inline");
        $("#Get_Potential").css("display", "none");
        $("#Get_UPotential").css("display", "inline"); 
        $("#btnGetGoods").css("display", "none");// 条码扫描按钮
    }
    else if(operateType == "2")
    {// 审批不通过
        $("#imgUnSave").css("display", "none");
        $("#imgSave").css("display", "inline");
        
        
        $("#imgAdd").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        $("#imgDel").css("display", "inline");
        $("#imgUnDel").css("display", "none");
        $("#Get_Potential").css("display", "inline");
        $("#Get_UPotential").css("display", "none"); 
        $("#btnGetGoods").css("display", "inline");// 条码扫描按钮
    }
   }
   catch(e)
   {}
}

// 根据单据状态决定页面按钮操作
function fnStatus(BillStatus,Isyinyong) {
try{
    switch (BillStatus) { // 单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        case '1': // 制单
// $("#HiddenAction").val('Add');
// fnFlowStatus($("#FlowStatus").val());
            break;
        case '2': // 执行
            if(Isyinyong == 'True')
            {// 被引用不可编辑
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none");// 条码扫描按钮
            }
            else
            {
// $("#HiddenAction").val('Update');
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                $("#imgAdd").css("display", "inline");
                $("#imgUnAdd").css("display", "none");
                $("#imgDel").css("display", "inline");
                $("#imgUnDel").css("display", "none");
                $("#Get_Potential").css("display", "inline");
                $("#Get_UPotential").css("display", "none");
                $("#btnGetGoods").css("display", "inline");// 条码扫描按钮
            }
            
            break;
        case '3': // 变更
// $("#HiddenAction").val('Update');
            $("#FromType").attr("disabled", "disabled");
            $("#imgSave").css("display", "inline");
            $("#imgUnSave").css("display", "none");
            break;
        case '4': // 手工结单
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#Get_Potential").css("display", "none");
            $("#Get_UPotential").css("display", "inline");
            $("#btnGetGoods").css("display", "none");// 条码扫描按钮
            break;

        case '5':
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            break;
         }
     }
   catch(e)
   {}
}

function fnFlowStatus(FlowStatus)
{
try{
    switch (FlowStatus) {
                case "": // 未提交审批
                    break;
                case "待审批": // 当前单据正在待审批
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline");
                    $("#btnGetGoods").css("display", "none");// 条码扫描按钮
                    break;
                case "审批中": // 当前单据正在审批中
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline");
                    $("#btnGetGoods").css("display", "none");// 条码扫描按钮
                    break;
                case "审批通过": // 当前单据已经通过审核
                    // 制单状态的审批通过单据,不可修改
                    if ($("#ddlBillStatus").val() == "1") {
                        $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#Get_Potential").css("display", "none");
                        $("#Get_UPotential").css("display", "inline");
                        $("#btnGetGoods").css("display", "none");// 条码扫描按钮
                    }

                    break;
                case "撤消审批": // 当前单据进行取消确认了.
                    // 取消确认后，为制单,可以修改
                    if ($("#ddlBillStatus").val() == "1") {
                        $("#imgSave").css("display", "inline");
                        $("#imgUnSave").css("display", "none");
                        $("#imgAdd").css("display", "inline");
                        $("#imgUnAdd").css("display", "none");
                        $("#imgDel").css("display", "inline");
                        $("#imgUnDel").css("display", "none");
                        $("#Get_Potential").css("display", "inline");
                        $("#Get_UPotential").css("display", "none");
                        $("#btnGetGoods").css("display", "inline");// 条码扫描按钮
                    }

                    break;
                case "审批不通过": // 当前单据审批未通过
                    break;
            }
    }
    catch(e)
   {}
}



function SetSaveButton_DisplayControl(flowStatus)
{
    // 流程状态：0：待提交 1：待审批 2：审批中 3：审批通过 4：审批不通过 5：撤销审批
    // 制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    // 制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    // 变更和手工结单的不可以修改
// var PageBillID = document.getElementById('txtIndentityID').value;
// var PageBillStatus = document.getElementById('ddlBillStatus').value;
// if(PageBillID>0)
// {

// if(PageBillStatus=='2' || PageBillStatus=='3' || PageBillStatus=='4')
// {
// //单据状态：变更和手工结单状态
// document.getElementById('imgUnSave').style.display='block';
// document.getElementById('imgSave').style.display='none';
// }
// else
// {
// if(PageBillStatus==1 && (flowStatus ==1 || flowStatus==2 || flowStatus ==3))
// {
// //单据状态+审批状态：制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
// document.getElementById('imgUnSave').style.display='block';
// document.getElementById('imgSave').style.display='none';
// }
// else
// {
// document.getElementById('imgUnSave').style.display='none';
// document.getElementById('imgSave').style.display='block';
// }
// }
// }
try{
    switch (parseInt(flowStatus)) {
                case 5: // 当前单据进行取消确认了.
                    // 取消确认后，为制单,可以修改
                    document.getElementById("imgSave").style.display="inline";
                    document.getElementById("imgUnSave").style.display="none";
                    document.getElementById("imgAdd").style.display="inline";
                    document.getElementById("imgUnAdd").style.display="none";
                    document.getElementById("imgDel").style.display="inline";
                    document.getElementById("imgUnDel").style.display="none";
                    document.getElementById("Get_Potential").style.display="inline";
                    document.getElementById("Get_UPotential").style.display="none";
                    document.getElementById("btnGetGoods").style.display="inline";// 条码扫描按钮
                    break;
                case 0: // 未提交审批
                    break;
                case 1: // 当前单据正在待审批
                    document.getElementById("imgSave").style.display="none";
                    document.getElementById("imgUnSave").style.display="inline";
                    document.getElementById("imgAdd").style.display="none";
                    document.getElementById("imgUnAdd").style.display="inline";
                    document.getElementById("imgDel").style.display="none";
                    document.getElementById("imgUnDel").style.display="inline";
                    document.getElementById("Get_Potential").style.display="none";
                    document.getElementById("Get_UPotential").style.display="inline";
                    document.getElementById("btnGetGoods").style.display="none";// 条码扫描按钮
                    
// $("#imgSave").css("display", "none");
// $("#imgUnSave").css("display", "inline");
// $("#imgAdd").css("display", "none");
// $("#imgUnAdd").css("display", "inline");
// $("#imgDel").css("display", "none");
// $("#imgUnDel").css("display", "inline");
// $("#Get_Potential").css("display", "none");
// $("#Get_UPotential").css("display", "inline");
                    break;
                case 2: // 当前单据正在审批中
                    document.getElementById("imgSave").style.display="none";
                    document.getElementById("imgUnSave").style.display="inline";
                    document.getElementById("imgAdd").style.display="none";
                    document.getElementById("imgUnAdd").style.display="inline";
                    document.getElementById("imgDel").style.display="none";
                    document.getElementById("imgUnDel").style.display="inline";
                    document.getElementById("Get_Potential").style.display="none";
                    document.getElementById("Get_UPotential").style.display="inline";
                    document.getElementById("btnGetGoods").style.display="none";// 条码扫描按钮
// $("#imgSave").css("display", "none");
// $("#imgUnSave").css("display", "inline");
// $("#imgAdd").css("display", "none");
// $("#imgUnAdd").css("display", "inline");
// $("#imgDel").css("display", "none");
// $("#imgUnDel").css("display", "inline");
// $("#Get_Potential").css("display", "none");
// $("#Get_UPotential").css("display", "inline");
                    break;
                case 3: // 当前单据已经通过审核
                    // 制单状态的审批通过单据,不可修改
                    
// if ($("#ddlBillStatus").val() == "1") {
                        document.getElementById("imgSave").style.display="none";
                        document.getElementById("imgUnSave").style.display="inline";
                        document.getElementById("imgAdd").style.display="none";
                        document.getElementById("imgUnAdd").style.display="inline";
                        document.getElementById("imgDel").style.display="none";
                        document.getElementById("imgUnDel").style.display="inline";
                        document.getElementById("Get_Potential").style.display="none";
                        document.getElementById("Get_UPotential").style.display="inline";
                        document.getElementById("btnGetGoods").style.display="none";// 条码扫描按钮
                    
// $("#imgSave").css("display", "none");
// $("#imgUnSave").css("display", "inline");
// $("#imgAdd").css("display", "none");
// $("#imgUnAdd").css("display", "inline");
// $("#imgDel").css("display", "none");
// $("#imgUnDel").css("display", "inline");
// $("#Get_Potential").css("display", "none");
// $("#Get_UPotential").css("display", "inline");
// }

                    break;
                
                case 4: // 当前单据审批未通过
                    break;
          }
    }
    catch(e)
   {}
}



function clearProviderdiv()
{
    document.getElementById("txtProviderID").value = "";
    document.getElementById("txtHidProviderID").value = "";
}



/* 单据打印 */
function PurchaseRejectPrint() {
    var ID = $("#txtIndentityID").val();
    if (parseInt(ID) == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存！");
        return;
    } 
    
       window.open("../../../Pages/PrinttingModel/PurchaseManager/PurchaseRejectPrint.aspx?ID="+document.getElementById("txtIndentityID").value);
}


// 供应商控件委琐遮照
/* 弹出 */
function PopPurProviderInfo()
{
// openRotoscopingDiv(false,'divPBackShadow','PBackShadowIframe');
    popProviderObj.ShowList()
}
/* 关闭 */
function closeProviderdiv()
{   
    closeRotoscopingDiv(false,'divPBackShadow');
    document.getElementById("divProviderInfohhh").style.display="none";
}

// ---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
/*
 * 参数：商品ID，商品编号，商品名称，去税售价，单位ID，单位名称，销项税率（%），含税售价，销售折扣，规格，类型名称，类型ID，含税进价，去税进价，进项税率(%)，标准成本
 */
// 根据条码获取的商品信息填充数据
function GetGoodsDataByBarCode(ID,ProdNo,ProductName,StandardSell,UnitID,UnitName,TaxRate,SellTax,Discount
                                ,Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,IsBatchNo,BatchNo
                                ,ProductCount,CurrentStore,Source,ColorName)
{
    if(!IsExist(ID))// 如果重复记录，就不增加
    {
       var rowID=AddSignRow();// 插入行
       // 填充数据
       // 物品ID
		// 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
       BarCode_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,rowID,ColorName);
    }
    else
    {
        document.getElementById("txtBackCount"+rerowID).value=parseFloat(document.getElementById("txtBackCount"+rerowID).value)+1;
        ChangeUnit(this,rerowID);// 更改数量后重新计算
    }
    
}

var rerowID="";
// 判断是否有相同记录有返回true，没有返回false
function IsExist(ID)
{
     var signFrame = findObj("dg_Log", document);
     if((typeof(signFrame)=="undefined")|| signFrame==null)
     {
        return false;
     }
     for (i = 1; i < signFrame.rows.length; i++) {// 判断商品是否在明细列表中
         if (signFrame.rows[i].style.display != "none") {
              var ProductID = $("#txtProductID" + i).val(); // 商品ID（对应商品表ID）
              if(ProductID==ID)
               {
                   rerowID=i;
                   return true;
               }
            }
        }
     return false;
}
// 物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
function BarCode_FillParent_Content(id,no,productname,dddf,unitid,unit,df,sdfge,discount,standard
                                    ,fg,fgf,taxprice,price,taxrate,index,ColorName)
{
    var ProductID = 'txtProductID'+index;
    var ProductNo = 'txtProductNo'+index;
    var ProductName='txtProductName'+index;
    var UnitID = 'txtUnitID'+index;
    var Unit='txtUnitName'+index;
    var Price='txtUnitPrice'+index;
    var Standard='txtstandard'+index;
    var txtColorName='txtColorName'+index;
    var TaxPrice = 'txtTaxPrice'+index;
    var Discount = 'txtDiscount'+index;
    var TaxRate = 'txtTaxRate'+index;
    var HiddTaxPrice = 'hiddTaxPrice'+index;
    var HiddTaxRate = 'hiddTaxRate'+index;
    var HiddUnitPrice = 'hiddUnitPrice'+index;
    
    document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate,selPoint) ;
    document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice,selPoint);
    document.getElementById(Standard).value=FormatAfterDotNumber(standard,selPoint);
    if(isMoreUnit)
    {// 启用多计量单位
        document.getElementById('UsedUnitName'+index).value=unit;
        document.getElementById('UsedUnitID'+index).value=unitid;
        GetUnitGroupSelectEx(id, 'InUnit', 'txtUnitID'+index, 'ChangeUnit(this,'+index+',1)', 'tdUnitName'+index,'','ChangeUnit(this,'+index+',1)');
        $("#txtBackCount"+index).attr("onblur",'ChangeUnit(this,'+index+')');               
    }
    else
    {
        document.getElementById("txtUnitID"+index).value = unitid;
        document.getElementById("txtUnitName"+index).value=unit;
    }
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    document.getElementById(txtColorName).value = ColorName;
    document.getElementById(Price).value = FormatAfterDotNumber(price,selPoint);
    document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice,selPoint);
    document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate,selPoint) ;
    document.getElementById(HiddUnitPrice).value = FormatAfterDotNumber(price,selPoint);
    document.getElementById("txtBackCount"+index).value=FormatAfterDotNumber(1,selPoint);
    
    var isAddTax = document.getElementById("chkIsAddTax").checked;
    // 新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true)
            {// 是增值税则
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于隐藏域含税价
																																							// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();// 税率等于隐藏域税率
                document.getElementById("AddTax").innerHTML = "是增值税";
            }
            else
            {
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 含税价等于单价
																																							// 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = FormatAfterDotNumber(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,selPoint);// 单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(0,selPoint);// 税率等于0
                document.getElementById("AddTax").innerHTML = "非增值税";
            }
        }
    }
    fnTotalInfo();
}
// ---------------------------------------------------条码扫描END------------------------------------------------------------------------------------

function ShowSnapshot() {
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var ck = document.getElementsByName("chk");
    var signFrame = findObj("dg_Log", document);
    for (var i = 0; i < ck.length; i++) {

        var rowID = i + 1;
        if (signFrame.rows[rowID].style.display != "none") {
            if (ck[i].checked) {
                detailRows++;
                intProductID = document.getElementById('txtProductID' + rowID).value;
                snapProductName = document.getElementById('txtProductName' + rowID).value;
                snapProductNo = document.getElementById('txtProductNo' + rowID).value;
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
