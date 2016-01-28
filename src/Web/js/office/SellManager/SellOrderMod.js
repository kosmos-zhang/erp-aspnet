 var hidCodeBarObj = '0';
$(document).ready(function() {
fnGetExtAttr();
    funSetCodeBar();//是否显示条码控制
    var pakeageUC = document.getElementById("PackageUC_ddlCodeType");
    pakeageUC.style.display = "none";
    $("#FeeType_ddlCodeType").css("display", "none");

    $("#sellTypeUC_ddlCodeType").css("width", "120px");
    $("#PayTypeUC_ddlCodeType").css("width", "120px");
    $("#MoneyTypeUC_ddlCodeType").css("width", "120px");
    $("#OrderMethod_ddlCodeType").css("width", "120px");
    $("#CarryType_ddlCodeType").css("width", "120px");
    $("#TakeType_ddlCodeType").css("width", "120px");

  
    //获取URl参数
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var orderID = requestObj['id']; //销售机会Id
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031501', 'ModuleID=2031502');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (orderID != null) {
            $("#hiddOrderID").val(orderID);
            fnGetOrderInfo(orderID); //获取报价单详细信息         
        }
        else
        {
            GetExtAttr("officedba.SellOrder",null);
        }
    }
    else
    {
        GetExtAttr("officedba.SellOrder",null);
    }
 
});

/*参数设置：是否显示条码控制*/
function funSetCodeBar()
{
    try
    {
        hidCodeBarObj = document.getElementById('hidBarCode').value;
        var objCodeBar = document.getElementById('GetGoods');
        hidCodeBarObj=='1'?objCodeBar.style.display='':objCodeBar.style.display='none';
    }catch(e){}
}

//获取报价单详细信息
function fnGetOrderInfo(orderID) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOrderList.ashx",
        data: "action=orderInfo&orderID=" + escape(orderID),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            if (data.data.length == 1) {
                fnInitPage(data.data[0]); //给页面赋值
                fnGetDetail(data.data[0].OrderNo); //获取报价单明细信息
                fnGetFee(data.data[0].OrderNo);
                fnIsRef(); //单据是否被其他单据引用
                fnStatus(data.data[0].BillStatus); //根据单据状态决定页面按钮操作
                fnGetBillStatus('5', '3', orderID); //获取审批状态，判断操作按钮
                fnState(data.data[0].State);
            }
        }
    });
}

//根据合同状态控制操作按钮
function fnState(state) {
    //意外终止不允许进行任何操作
    if (state == '3') {
        try {
            $("#btn_save").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#AddMore").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            if(hidCodeBarObj=='1')
            {
                $("#GetGoods").css("display", "none");
                $("#UnGetGoods").css("display", "inline");
            }
            
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#FromType").attr("disabled", "disabled");
            $("#btn_zzht").css("display", "none");
            $("#btn_Unzzht").css("display", "inline");
            $("#AddFee").css("display", "none");
            $("#UnAddFee").css("display", "inline");
            $("#DelFee").css("display", "none");
            $("#UnDelFee").css("display", "inline");
        } catch (e) { }
    }
}

//给页面赋值
function fnInitPage(data) {
    $("#OfferNo").val(data.OrderNo);        //报价单编号
    
    $("#FromType").val(data.FromType);       //源单类型（0无来源，1销售机会）
    fnFromTypeChange(document.getElementById("FromType"));
    $("#CustID").val(data.CustName);         //客户ID（关联客户信息表）
    $("#CustID").attr("title", data.CustID);         //客户ID（关联客户信息表）
    $("#CustTel").val(data.CustTel);        //客户联系电话
    $("#FromBillID").val(data.FromBillNo);     //销售机会ID
    $("#FromBillID").attr("title", data.FromBillID);     //销售机会ID
    $("#Title").val(data.Title);          //报价主题
    $("#UserSeller").val(data.SellerName);         //业务员(对应员工表ID)
    $("#Seller").val(data.Seller);         //业务员(对应员工表ID)
    $("#SellDeptId").val(data.SellDeptId);     //部门(对部门表ID)
    $("#DeptId").val(data.DeptName);     //部门(对部门表ID)
    $("#sellTypeUC_ddlCodeType").val(data.SellType);       //销售类别ID（对应分类代码表ID）
    $("#BusiType").val(data.BusiType);       //业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨）
    $("#PayTypeUC_ddlCodeType").val(data.PayType);        //结算方式ID（对应分类代码表ID）
    $("#OrderMethod_ddlCodeType").val(data.OrderMethod);
    $("#MoneyTypeUC_ddlCodeType").val(data.MoneyType);      //支付方式ID（对应分类代码表ID）
    $("#CarryType_ddlCodeType").val(data.CarryType);      //运送方式ID（对应分类代码表ID）
    $("#TakeType_ddlCodeType").val(data.TakeType);       //交货方式ID（对应分类代码表ID）
    $("#TotalPrice").val(FormatAfterDotNumber(data.TotalPrice, precisionLength));     //金额合计
    $("#Tax").val(FormatAfterDotNumber(data.Tax, precisionLength));       //税额合计
    $("#TotalFee").val(FormatAfterDotNumber(data.TotalFee, precisionLength));       //含税金额合计
    $("#Discount").val(FormatAfterDotNumber(data.Discount, precisionLength));       //整单折扣（%）
    $("#DiscountTotal").val(FormatAfterDotNumber(data.DiscountTotal, precisionLength));  //折扣金额
    $("#RealTotal").val(FormatAfterDotNumber(data.RealTotal, precisionLength));      //折后含税金额
    $("#TheyDelegate").val(data.TheyDelegate); //客户方代表
    $("#TalkProcess").val(data.TalkProcess); //奇谈进展  
    $("#StartDate").val(data.StartDate); //开始时间
    $("#EndDate").val(data.EndDate); //截止时间
    $("#UserOurDelegate").val(data.OurDelegateName); //我方代表
    $("#OurDelegate").val(data.OurDelegate);   //我方代表（员工表ID）
    $("#hfPageAttachment").attr("value", data.Attachment); //文档URL
    $("#spanAttachmentName").html(data.Attachment.substr(data.Attachment.lastIndexOf('\\') + 1)); //文档URL
    if (data.Attachment.length != 0) {
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "block";
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
    }

    if (data.isAddTax == '0') {
        $("#NotAddTax").attr("checked", "checked"); //是否增值税（0否,1是
        $("#isAddTax").removeAttr("checked", "checked"); //是否增值税（0否,1是
    }
    else {
        $("#isAddTax").attr("checked", "checked"); //是否增值税（0否,1是
        $("#NotAddTax").removeAttr("checked", "checked"); //是否增值税（0否,1是
    }

    $("#hiddStatus").val(data.Status);
    if (data.Status == '1') {
        $("#State").val('处理中');
    }
    else if (data.Status == '3') {
        $("#State").val('终止');
    }
    else if (data.Status == '2') {
        $("#State").val('处理完');
    }
    $("#CountTotal").val(FormatAfterDotNumber(data.CountTotal, precisionLength));     //产品数量合计
    $("#CurrencyType").attr("title", data.CurrencyType); //币种
    $("#CurrencyType").val(data.CurrencyName);
    $("#Rate").val(data.Rate);           //汇率
    $("#hiddBillStatus").val(data.BillStatus); //单据状态
    $("#isSendText").val(data.isSendText);      //发货情况
    $("#isOpenbillText").val(data.isOpenbillText);      //建单情况
    $("#Remark").val(data.Remark);         //备注
    $("#BillStatus").html(data.BillStatusText);      //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
    $("#Creator").val(data.CreatorName);        //制单人ID
    $("#CreateDate").val(data.CreateDate);     //制单日期
    $("#Confirmor").val(data.ConfirmorName);      //确认人ID
    $("#ConfirmDate").val(data.ConfirmDate);    //确认日期
    $("#Closer").val(data.CloserName);         //结单人ID
    $("#CloseDate").val(data.CloseDate);      //结单日期
    $("#ModifiedDate").val(data.ModifiedDate);   //最后更新日期
    $("#ModifiedUserID").val(data.ModifiedUserID); //最后更新用户ID(对应操作用户UserID)

    $("#SaleFeeTotal").val(FormatAfterDotNumber(data.SaleFeeTotal,precisionLength));   //销售费用合计        
    $("#PayRemark").val(data.PayRemark);      //付款说明
    $("#DeliverRemark").val(data.DeliverRemark);  //交付说明
    $("#PackTransit").val(data.PackTransit);    //包装运输说明
    $("#StatusNote").val(data.StatusNote);     //异常终止原因
    $("#CustOrderNo").val(data.CustOrderNo);    //客户订单号
    $("#SendDate").val(data.SendDate);       //最迟发货时间（精确到分）
    $("#OrderDate").val(data.OrderDate);      //下单日期
    $("#huikuan").val(FormatAfterDotNumber(data.YAccounts,precisionLength)); //回款金额
    $("#IsAccText").val(data.IsAccText); //是否回款
    $("#UserCanViewUserName").val(data.CanViewUserName); //可查看此订单人员
    $("#CanViewUser").val(data.CanViewUser); 
    $("#hiddProjectID").val(data.ProjectID);//所属项目ID
    $("#ProjectID").val(data.ProjectName);
    
    GetExtAttr("officedba.SellOrder",data);
    GetFlowButton_DisplayControl();
}
//清除客户信息
function ClearSellModuCustdiv() {
    fnFromTypeChange(document.getElementById("FromType"));
    closeSellModuCustdiv(); //关闭客户选择控件
}
//获取报价单明细信息
function fnGetDetail(orderNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOrderList.ashx",
        data: "action=detail&orderNo=" + escape(orderNo),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            //fnDelRow();
            if (data.data != null) {
                $.each(data.data, function(i, item) {
                    
                    AddSignRow();
                    var txtTRLastIndex = findObj("txtTRLastIndex", document);
                    var rowID = parseInt(txtTRLastIndex.value);

                    $("#ProductID" + rowID).val(item.ProdNo); //商品编号
                    $("#hiddProductID" + rowID).val(item.ProductID); //商品ID
                    $("#ProductID" + rowID).attr("title", item.ProductID); //商品ID
                    $("#ProductName" + rowID).val(item.ProductName); //商品名称
                    //$("#UnitID" + rowID).val(item.CodeName); //单位名称
                    //$("#UnitID" + rowID).attr("title", item.UnitID); //单位ID
                    //$("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
                    //$("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
                    $("#Specification" + rowID).val(item.Specification); //规格
                    $("#ColorID"+rowID).val(item.ColorName);//颜色
                    $("#TaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                    $("#hiddBaseTaxPrice" + rowID).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                    $("#Discount" + rowID).val(FormatAfterDotNumber(item.Discount, precisionLength)); //折扣
                    $("#TaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                    $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate1, precisionLength)); //税率
                    $("#TotalFee" + rowID).val(FormatAfterDotNumber(item.TotalFee, precisionLength));    //含税金额
                    $("#TotalPrice" + rowID).val(FormatAfterDotNumber(item.TotalPrice, precisionLength));  //金额
                    $("#TotalTax" + rowID).val(FormatAfterDotNumber(item.TotalTax, precisionLength));    //税额
                    $("#ProductCount" + rowID).val(FormatAfterDotNumber(item.ProductCount,precisionLength));    //数量
                    $("#SendTime" + rowID).val(item.SendTime);    //交货期限
                    $("#Package" + rowID).val(item.Package);     //包装要求ID
                    $("#Remark" + rowID).val(item.Remark);      //备注
                    //item.SendCount,item.PlanProductCount
                    $("#DSendCount" + rowID).val(FormatAfterDotNumber(item.SendCount,precisionLength));      //已发货数量
                    $("#DPlanProductCount" + rowID).val(FormatAfterDotNumber(item.PlanProductCount,precisionLength));      //计划数量
                    
                    //如果是增值税
                    if ($("#isAddTax").attr("checked")) {
                        $("#TaxRate" + rowID).removeAttr("readonly");
                    }
                    //如果不是增值税
                    //如果是增值税
                    if ($("#NotAddTax").attr("checked")) {
                        $("#TaxRate" + rowID).attr("readonly", "readonly");
                        $("#TaxRate" + rowID).val(FormatAfterDotNumber(0, precisionLength));
                    }
                    
                    //多计量单位启用
                    if($("#txtIsMoreUnit").val() == "1")
                    {
                        $("#BaseUnitID" + rowID).val(item.CodeName); //单位名称
                        $("#BaseUnitID" + rowID).attr("title", item.UnitID); //单位ID
                        $("#BaseCountD" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength)); //基本数量
                        $("#ProductCount" + rowID).val(FormatAfterDotNumber(item.UsedUnitCount, precisionLength)); //数量
                        $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UsedPrice, precisionLength)); //单价
                        $("#hiddBaseUnitPrice"+rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength));//基本单价（对应基本价格）
                    }
                    else
                    {
                       $("#UnitID" + rowID).val(item.CodeName); //单位名称
                       $("#UnitID" + rowID).attr("title", item.UnitID); //单位ID  
                       $("#ProductCount" + rowID).val(FormatAfterDotNumber(item.ProductCount, precisionLength)); //数量
                       $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
                    }
                    //$("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardSell, precisionLength)); //单价
                    if($("#txtIsMoreUnit").val() == "1")
                    {   
                        //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
                        GetUnitGroupSelect(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UsedUnitID);
                    }
                    txtTRLastIndex.value = rowID; //将行号推进下一行
                });
               // fnTotalInfo();
            }
        }
    });
}

//获取费用明细信息
function fnGetFee(orderNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOrderList.ashx",
        data: "action=fee&orderNo=" + escape(orderNo),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            
            if (data.data != null) {
                $.each(data.data, function(i, item) {
                //读取最后一行的行号，存放在txtTRLastIndex文本框中 
                var hiddFeeIndex = findObj("hiddFeeIndex", document);
                var rowID = parseInt(hiddFeeIndex.value) + 1;
                var signFrame = findObj("dg_Log2", document);
                var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
                newTR.id = rowID;

                var newNameXH = newTR.insertCell(0); //添加列:序号
                newNameXH.className = "cell";
                newNameXH.innerHTML = "<input id='chk2" + rowID + "'  onclick = 'fnUnSelect1(this)'  value=" + rowID + " type='checkbox'  />";

                var newFitNotd = newTR.insertCell(1); //添加列:费用类别
                newFitNotd.className = "cell";
                newFitNotd.innerHTML = "<input id='FeeType" + rowID + "' value='" + item.CodeName + "' title='" + item.FeeID + "' onclick=\"fnSelectFeeType('FeeType" + rowID + "')\" readonly='readonly' style=' width:85%;' type='text'  class='tdinput' />"; //添加列内容

                var newFitNametd = newTR.insertCell(2); //添加列:金额
                newFitNametd.className = "cell";
                newFitNametd.innerHTML = "<input id='FeeTotal" + rowID + "'  maxlength='12' value='" + FormatAfterDotNumber(item.FeeTotal, precisionLength) + "' onblur=\"fnFee()\"  style=' width:85%;' type='text'  class='tdinput' />"; //添加列内容

                var newFitDesctd = newTR.insertCell(3); //添加列:备注
                newFitDesctd.className = "cell";
                newFitDesctd.innerHTML = "<input id='FeeRemark" + rowID + "'  maxlength='100' value='" + item.Remark + "' type='text' class='tdinput' style=' width:85%;'/>"; //添加列内容

                hiddFeeIndex.value = rowID; //将行号推进下一行
                });
                fnFee();
            }
        }
    });
}

//根据单据状态决定页面按钮操作
function fnStatus(BillStatus) {
    try {
        switch (BillStatus) { //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
            case '1': //制单

                break;
            case '2': //执行
                $("#btn_save").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#AddMore").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                if(hidCodeBarObj=='1')
                {
                    $("#GetGoods").css("display", "none");
                    $("#UnGetGoods").css("display", "inline");
                }
                
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#FromType").attr("disabled", "disabled");
                $("#AddFee").css("display", "none");
                $("#UnAddFee").css("display", "inline");
                $("#DelFee").css("display", "none");
                $("#UnDelFee").css("display", "inline");

                break;

            case '4': //手工结单
                $("#btn_save").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#AddMore").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                if(hidCodeBarObj=='1')
                {
                    $("#GetGoods").css("display", "none");
                    $("#UnGetGoods").css("display", "inline");
                }
                
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#FromType").attr("disabled", "disabled");
                $("#AddFee").css("display", "none");
                $("#UnAddFee").css("display", "inline");
                $("#DelFee").css("display", "none");
                $("#UnDelFee").css("display", "inline");
                $("#btn_zzht").css("display", "none");
                $("#btn_Unzzht").css("display", "inline");
                break;

            case '5':
                $("#btn_save").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#AddMore").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                if(hidCodeBarObj=='1')
                {
                    $("#GetGoods").css("display", "none");
                    $("#UnGetGoods").css("display", "inline");
                }
                
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#AddFee").css("display", "none");
                $("#UnAddFee").css("display", "inline");
                $("#DelFee").css("display", "none");
                $("#UnDelFee").css("display", "inline");
                $("#FromType").attr("disabled", "disabled");
                $("#btn_zzht").css("display", "none");
                $("#btn_Unzzht").css("display", "inline");
                break;
        }
    } catch (e) { }
}

//单据是否被其他单据引用,被其他单据引用明细不可删除
function fnIsRef() {
    var requestObj = GetRequest(); //参数列表
    var isRef = requestObj['isRef']; //单据是否贝引用
    if (isRef == '被引用') {
        try {
            $("#btn_save").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#AddMore").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            if(hidCodeBarObj=='1')
            {
               $("#GetGoods").css("display", "none");
                $("#UnGetGoods").css("display", "inline"); 
            }
            
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#AddFee").css("display", "none");
            $("#UnAddFee").css("display", "inline");
            $("#DelFee").css("display", "none");
            $("#UnDelFee").css("display", "inline");
            $("#btn_zzht").css("display", "none");
            $("#btn_Unzzht").css("display", "inline");
        } catch (e) { }
        $("#isRef").val("被引用");
    }
}

//获取审批状态，判断操作按钮
function fnGetBillStatus(BillTypeFlag, BillTypeCode, BillID) {
    var Action = "Get";
    var UrlParam = "Action=Get&BillTypeFlag=" + BillTypeFlag + "&BillTypeCode=" + BillTypeCode + "&BillID=" + BillID + "";
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
            try {
                switch (data.sta) {
                    case 0: //未提交审批         
                        break;
                    case 1: //当前单据正在待审批
                        $("#btn_save").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#AddMore").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        if(hidCodeBarObj=='1')
                        {
                            $("#GetGoods").css("display", "none");
                            $("#UnGetGoods").css("display", "inline");
                        }
                        
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#AddFee").css("display", "none");
                        $("#UnAddFee").css("display", "inline");
                        $("#DelFee").css("display", "none");
                        $("#UnDelFee").css("display", "inline");

                        break;
                    case 2: //当前单据正在审批中
                        $("#btn_save").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#AddMore").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        if(hidCodeBarObj=='1')
                        {
                            $("#GetGoods").css("display", "none");
                            $("#UnGetGoods").css("display", "inline");
                        }
                        
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#AddFee").css("display", "none");
                        $("#UnAddFee").css("display", "inline");
                        $("#DelFee").css("display", "none");
                        $("#UnDelFee").css("display", "inline");

                        break;
                    case 3: //当前单据已经通过审核
                        //制单状态的审批通过单据,不可修改
                        if ($("#BillStatus").html() == "制单") {
                            $("#btn_save").css("display", "none");
                            $("#imgUnSave").css("display", "inline");
                            $("#imgAdd").css("display", "none");
                            $("#AddMore").css("display", "none");
                            $("#imgUnAdd").css("display", "inline");
                            if(hidCodeBarObj=='1')
                            {
                                $("#GetGoods").css("display", "none");
                                $("#UnGetGoods").css("display", "inline");
                            }
                            
                            $("#imgDel").css("display", "none");
                            $("#imgUnDel").css("display", "inline");
                            $("#AddFee").css("display", "none");
                            $("#UnAddFee").css("display", "inline");
                            $("#DelFee").css("display", "none");
                            $("#UnDelFee").css("display", "inline");

                        }

                        break;
                    case 4: //当前单据审批未通过
                        break;
                }
            } catch (e) { }
        }
    });
}


//获取查询参数
function GetRequestToHidden() {
    var url = location.search;
    return url;
}

//返回按钮
function fnBack() {
    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        if (flag != null && typeof (flag) != "undefined") {
            var URLParams = document.getElementById("HiddenURLParams").value;
            window.location.href = 'SellOrderList.aspx' + URLParams;
        }
        else {
            var intFromType = requestObj['intFromType']; //是否从列表页面进入           
            var ListModuleID = requestObj['ListModuleID']; //来源页MODULEID       
            if (intFromType == 2) {
                window.location.href = '../../../DeskTop.aspx';
            }
            if (intFromType == 3) {
                window.location.href = '../../Personal/WorkFlow/FlowMyApply.aspx?ModuleID=' + ListModuleID;
            }
            if (intFromType == 4) {
                window.location.href = '../../Personal/WorkFlow/FlowMyProcess.aspx?ModuleID=' + ListModuleID;
            }
            if (intFromType == 5) {
                window.location.href = '../../Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=' + ListModuleID;
            }
            
            if (intFromType == 6) //zhangyy--2010-06-21
            {
                var ListType = requestObj['ListType'];
                var custID = requestObj['custID'];
                var custNo = requestObj['custNo'];
                window.location.href = '../../Office/CustManager/CustColligate.aspx?ModuleID=2022101'
                        + '&ListType=10&custID='+custID+'&custNo='+custNo;
            }

        }
    }
   
}

//获取url中"?"符后的字串
function GetRequest() {
    var url = location.search;
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }

    return theRequest;
}

//选择部门
function fnSelectDept() {
   
            alertdiv('DeptId,SellDeptId');
        
}
//选择币种
function fnSelectCurrency() {
    if ($("#FromType").val() == 0 || $("#FromType").val() == 3) {
        popSellCurrObj.ShowList('CurrencyType', 'Rate');
    }
}


//删除明细一行
function fnDelOneRow() {
    var dg_Log = findObj("dg_Log", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除

        if ($("#chk" + i).attr("checked")) {
            dg_Log.rows[i].style.display = "none";
        }
    }
    $("#checkall").removeAttr("checked");
    fnTotalInfo();
}

//删除明细一行
function fnDelOneRow2() {
    var dg_Log = findObj("dg_Log2", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除

        if ($("#chk2" + i).attr("checked")) {
            dg_Log.rows[i].style.display = "none";
        }
    }
    $("#checkall1").removeAttr("checked");
    fnTotalInfo();
}

function fnSelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

function fnSelectAll2() {
    $.each($("#dg_Log2 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall1").attr("checked");
    });
}
function fnSelectFeeType(obj) {
    popSellFeeTypeObj.ShowList(obj);
}

//添加费用明细
function fnAddFeeRow() {
    //读取最后一行的行号，存放在txtTRLastIndex文本框中 
    var hiddFeeIndex = findObj("hiddFeeIndex", document);
    var rowID = parseInt(hiddFeeIndex.value) + 1;
    var signFrame = findObj("dg_Log2", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;

    var newNameXH = newTR.insertCell(0); //添加列:序号
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk2" + rowID + "'  onclick = 'fnUnSelect1(this)'  value=" + rowID + " type='checkbox'  />";

    var newFitNotd = newTR.insertCell(1); //添加列:费用类别
    newFitNotd.className = "cell";
    newFitNotd.innerHTML = "<input id='FeeType" + rowID + "' onclick=\"fnSelectFeeType('FeeType" + rowID + "')\" readonly='readonly' style=' width:85%;' type='text'  class='tdinput' />"; //添加列内容

    var newFitNametd = newTR.insertCell(2); //添加列:金额
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<input id='FeeTotal" + rowID + "' onblur=\"fnFee()\"  maxlength='12' style=' width:85%;' type='text'  class='tdinput' />"; //添加列内容

    var newFitDesctd = newTR.insertCell(3); //添加列:备注
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='FeeRemark" + rowID + "'  maxlength='100' type='text' class='tdinput' style=' width:85%;'/>"; //添加列内容

    hiddFeeIndex.value = rowID; //将行号推进下一行
}

function fnFee() {

    var SellFeeFit_Item = new Array();
    var totalFee = 0;
    var signFrame = findObj("dg_Log2", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            var FeeTotal = $("#FeeTotal" + rowid).val(); //费用金额
            if ($.trim(FeeTotal) != '') {
                Number_round(document.getElementById("FeeTotal" + rowid), precisionLength);
                if (!IsNumeric(FeeTotal, 14, precisionLength)) {
                    $("#FeeTotal" + rowid).val('');
                }
                else {
                    totalFee += parseFloat(FeeTotal);
                }
            }


        }
    }
    $("#SaleFeeTotal").val(FormatAfterDotNumber(totalFee, precisionLength));
}

//选择订单来源时
function fnFromTypeChange(obj) {
    if (obj.selectedIndex == 0 || obj.selectedIndex == 1) {
        //清除已填写数据
        fnDelRow();

        $("#FromBillID").val(''); //发货单编号
        $("#FromBillID").removeAttr("title"); //发货单ID
        $("#CustID").val(''); //客户名称

        $("#PayTypeUC_ddlCodeType").attr("selectedIndex", 0); //结算方式
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").removeAttr("title"); //币种
        $("#CustTel").val(''); //客户类型     
        $("#sellTypeUC_ddlCodeType").attr("selectedIndex", 0); //销售类别
        $("#Rate").val(FormatAfterDotNumber(0, 4)); //汇率           
        $("#UserSeller").val(''); //业务员名称
        $("#hiddSeller").val(''); //业务员编号
        $("#DeptId").val(''); //部门名称
        $("#hiddDeptID").val(''); //部门编号
        $("#MoneyTypeUC_ddlCodeType").attr("selectedIndex", 0); //支付方式
        $("#CarryType_ddlCodeType").attr("selectedIndex", 0);
        $("#TakeType_ddlCodeType").attr("selectedIndex", 0);

        $("#SellDeptId").val('');

        $("#Discount").val(FormatAfterDotNumber(100, precisionLength));
        $("#isAddTax").removeAttr("disabled");
        $("#NotAddTax").removeAttr("disabled");
        $("#isAddTax").attr("checked", "checked");
        $("#NotAddTax").removeAttr("checked", "checked");
    }
    if (obj.selectedIndex == 2 || obj.selectedIndex == 3) {
        fnDelRow();
        //清除已填写数据
        $("#FromBillID").val(''); //发货单编号
        $("#FromBillID").removeAttr("title"); //发货单ID
        $("#CustID").val(''); //客户名称
        $("#CustTel").val(''); //客户类型
        $("#CustID").removeAttr("title"); //客户编号
        $("#BusiType").attr("selectedIndex", 0); //业务类型
        $("#PayTypeUC_ddlCodeType").attr("selectedIndex", 0); //结算方式
        $("#sellTypeUC_ddlCodeType").attr("selectedIndex", 0); //销售类别
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").removeAttr("title"); //币种
        $("#Rate").val(FormatAfterDotNumber(0, 4)); //汇率           
        $("#UserSeller").val(''); //业务员名称
        $("#hiddSeller").val(''); //业务员编号
        $("#DeptId").val(''); //部门名称
        $("#hiddDeptID").val(''); //部门编号
        $("#MoneyTypeUC_ddlCodeType").attr("selectedIndex", 0); //支付方式
        $("#CarryType_ddlCodeType").attr("selectedIndex", 0); //运送方式
        $("#TakeType_ddlCodeType").attr("selectedIndex", 0); //交货方式

        $("#SellDeptId").val('');


        $("#Discount").val(FormatAfterDotNumber(100, precisionLength));
        $("#isAddTax").attr("disabled", "false");
        $("#NotAddTax").attr("disabled", "false");
        $("#isAddTax").attr("checked", "checked");
        $("#NotAddTax").removeAttr("checked", "checked");
    }
}
//清除销售机会
function clearSellChance() {
    fnFromTypeChange(document.getElementById("FromType"));
    closeSellSenddiv();
}
//选择业务员
function fnSelectSeller() {
    
        alertdiv('UserSeller,Seller');
    
}
//验证整单折扣是否正确
function fnCheckPer(obj) {
    if (obj.value.length > 0) {
        Number_round(obj, precisionLength);
        if (!IsNumeric(obj.value, 12, precisionLength)) {
            obj.value = FormatAfterDotNumber(100, precisionLength);
            //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "代销提成率输入错误,请确认！");
        }
        else {
            if (100 < parseFloat(obj.value)) {
                obj.value = FormatAfterDotNumber(100, precisionLength);
            }
            if (parseFloat(obj.value) < 0) {
                obj.value = FormatAfterDotNumber(100, precisionLength);
            }
        }
    }
    else {
        obj.value = FormatAfterDotNumber(100, precisionLength);
        // showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "代销提成率不能为空！");
    }
    fnTotalInfo();
}


//计算各种合计信息
function fnTotalInfo() {
    var TotalPrice = 0; //金额合计
    var Tax = 0; //税额合计
    var TotalFee = 0; //含税金额合计
    var Discount = $("#Discount").val(); //整单折扣
    var DiscountTotal = 0; //折扣金额
    var DiscountTotalDetail = 0; //折扣金额
    var RealTotal = 0; //折后含税金额
    var CountTotal = 0; //发货数量合计
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            try{
                if($("#txtIsMoreUnit").val() == "1")
                {
                    //根据单位和数量计算基本数量。参数（单位下列表ID，数量文本框ID，基本数量文本框ID，单价ID，基本单价ID）
                    //此处“单价ID”和“基本单价ID”均传空，此处仅作基本数量的计算，单价不作计算，单价只在“单位”改变时重新计算。
                    CalCulateNum("selectUnitID"+rowid ,"ProductCount"+rowid ,"BaseCountD"+rowid,'','', precisionLength );
                }
            }catch(e){}
            var pCountDetail = $("#ProductCount" + rowid).val(); //数量
            if ($.trim(pCountDetail) == '') {
                pCountDetail = 0;
            }
            else {
                Number_round(document.getElementById("ProductCount" + rowid), precisionLength);
                if (!IsNumeric($("#ProductCount" + rowid).val(), 14, precisionLength)) {
                    $("#ProductCount" + rowid).val('');
                    pCountDetail = 0;
                }

            }
            var UnitPriceDetail = $("#UnitPrice" + rowid).val(); //单价
            if ($.trim(UnitPriceDetail) == '') {
                UnitPriceDetail = 0;
            }
            else {
                Number_round(document.getElementById("UnitPrice" + rowid), precisionLength);
                if (!IsNumeric($("#UnitPrice" + rowid).val(), 14, precisionLength)) {
                    $("#UnitPrice" + rowid).val('');
                    UnitPriceDetail = 0;
                }
            }

            var TaxPriceDetail = $("#TaxPrice" + rowid).val(); //含税价
            if ($.trim(TaxPriceDetail) == '') {
                TaxPriceDetail = 0;
            }
            else {
                Number_round(document.getElementById("TaxPrice" + rowid), precisionLength);
                if (!IsNumeric($("#TaxPrice" + rowid).val(), 14, precisionLength)) {
                    $("#TaxPrice" + rowid).val('');
                    TaxPriceDetail = 0;
                }
            }
            var DiscountDetail = $("#Discount" + rowid).val(); //折扣
            if ($.trim(DiscountDetail) == '') {
                DiscountDetail = FormatAfterDotNumber(100, precisionLength);
            }
            else {
                Number_round(document.getElementById("Discount" + rowid), precisionLength);
                if (!IsNumeric($("#Discount" + rowid).val(), 12, precisionLength)) {
                    $("#Discount" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    DiscountDetail = FormatAfterDotNumber(100, precisionLength);
                }
                else {
                    if (100 < parseFloat(DiscountDetail)) {
                        $("#Discount" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    }
                    if (parseFloat(DiscountDetail) < 0) {
                        $("#Discount" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    }
                }
            }
            var TaxRateDetail = $("#TaxRate" + rowid).val(); //税率
            if ($.trim(TaxRateDetail) == '') {
                TaxRateDetail = FormatAfterDotNumber(100, precisionLength);
            }
            else {
                Number_round(document.getElementById("TaxRate" + rowid), precisionLength);
                if (!IsNumeric($("#TaxRate" + rowid).val(), 12, precisionLength)) {
                    $("#TaxRate" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    TaxRateDetail = FormatAfterDotNumber(100, precisionLength);
                }
                else {
                    if (100 < parseFloat(TaxRateDetail)) {
                        $("#TaxRate" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    }
                    if (parseFloat(TaxRateDetail) < 0) {
                        $("#TaxRate" + rowid).val(FormatAfterDotNumber(100, precisionLength));
                    }
                }
            }

            var TotalFeeDetail = FormatAfterDotNumber((TaxPriceDetail * pCountDetail * DiscountDetail / 100), precisionLength); //含税金额
            var TotalPriceDetail = FormatAfterDotNumber((UnitPriceDetail * pCountDetail * DiscountDetail / 100), precisionLength); //金额
            var TotalTaxDetail = FormatAfterDotNumber((TotalPriceDetail * TaxRateDetail / 100), precisionLength); //税额
            $("#TotalFee" + rowid).val(FormatAfterDotNumber(TotalFeeDetail, precisionLength)); //含税金额
            $("#TotalPrice" + rowid).val(FormatAfterDotNumber(TotalPriceDetail, precisionLength)); //金额
            $("#TotalTax" + rowid).val(FormatAfterDotNumber(TotalTaxDetail, precisionLength)); //税额
            TotalPrice += parseFloat(TotalPriceDetail);
            Tax += parseFloat(TotalTaxDetail);
            TotalFee += parseFloat(TotalFeeDetail);
            CountTotal += parseFloat(pCountDetail);
            DiscountTotalDetail += ((100 - parseFloat(DiscountDetail)) / 100) * parseFloat(TaxPriceDetail) * parseFloat(pCountDetail);
        }
    }
    $("#TotalPrice").val(FormatAfterDotNumber(TotalPrice, precisionLength));
    $("#Tax").val(FormatAfterDotNumber(Tax, precisionLength));
    $("#TotalFee").val(FormatAfterDotNumber(TotalFee, precisionLength));
    $("#CountTotal").val(FormatAfterDotNumber(CountTotal, precisionLength));
    $("#DiscountTotal").val(FormatAfterDotNumber(((TotalFee * (100 - Discount) / 100) + parseFloat(DiscountTotalDetail)), precisionLength));
    $("#RealTotal").val(FormatAfterDotNumber((TotalFee * parseFloat(Discount) / 100), precisionLength));
}


//删除明细中所有数据
function fnDelRow() {
    var dg_Log = findObj("dg_Log", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
        dg_Log.deleteRow(i);
    }
    var txtTRLastIndex = findObj("txtTRLastIndex", document); //重置最后行号为
    txtTRLastIndex.value = "0";

    fnTotalInfo();
}

//选择是否是增值税时就行的操作
function fnAddTax() {
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowIndex = signFrame.rows[i].id;
            //如果是增值税
            if ($("#isAddTax").attr("checked")) {
                $("#TaxRate" + rowIndex).removeAttr("readonly");
                $("#TaxRate" + rowIndex).val($("#hiddTaxRate" + rowIndex).val());
              
                $("#TaxPrice" + rowIndex).val($("#hiddTaxPrice" + rowIndex).val());
            }
            //如果不是增值税
            if ($("#NotAddTax").attr("checked")) {
                $("#TaxRate" + rowIndex).attr("readonly", "readonly");
                $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(0, precisionLength));
                
                $("#TaxPrice" + rowIndex).val($("#UnitPrice" + rowIndex).val());
            }
        }
    }
    fnTotalInfo();
}

//选择源单
function fnSelectOfferInfo() {
    if ($("#FromType").val() == 1) {
        if ($("#FromType").attr("disabled") != true) {
            popSellOfferObj.ShowList('protion');
        }
    }
    if ($("#FromType").val() == 2) {
        if ($("#FromType").attr("disabled") != true) {
            popSellConObj.ShowList('protion');
        }
    }
    if ($("#FromType").val() == 3) {
        if ($("#FromType").attr("disabled") != true) {
            popSellSendObj.ShowList('all');
        }
    }
}
//获取销售机会详细信息
function fnSelectSellSend(orderID) {

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChanceList.ashx",
        data: "action=orderInfo&orderID=" + escape(orderID),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            if (data.data.length == 1) {
                $("#FromBillID").val(data.data[0].ChanceNo); //机会编号
                $("#FromBillID").attr("title", orderID); //机会编号
                $("#CustID").attr("title", data.data[0].CustID);     //客户ID（对应客户表ID）
                $("#CustID").val(data.data[0].CustName);     //客户ID（对应客户表ID）                    
                $("#CustTel").val(data.data[0].CustTel);    //客户联系电话           
                $("#hiddSeller").val(data.data[0].Seller);     //业务员(对应员工表ID)
                $("#hiddDeptID").val(data.data[0].SellDeptId); //部门(对部门表ID)
                $("#UserSeller").val(data.data[0].EmployeeName); //业务员
                $("#DeptId").val(data.data[0].DeptName); //部门
                closeSellSenddiv();
            }
        }
    });
}

//清除销售报价单
function clearSellOff() {
    fnFromTypeChange(document.getElementById("FromType"));

    closeOffdiv();
}

//清除销售合同
function clearSellCon() {
    fnFromTypeChange(document.getElementById("FromType"));

    closeCondiv();
}


//选择客户
function fnSelectCustInfo() {
    if ($("#FromType").val() == 0) {
        popSellCustObj.ShowList('protion');
    }
}

//选择客户后，为页面填充数据
function fnSelectCust(custID) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellModuleSelectCustUC.ashx",
        data: 'actionSellCust=info&id=' + custID,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取客户数据失败,请确认"); },
        success: function(data) {
            $("#CustID").val(data.CustName); //客户名称
            $("#CustID").attr("title", custID); //客户编号
            $("#CustTel").val(data.Tel); //客户类型
            $("#BusiType").val(data.BusiType); //业务类型
            $("#TakeType_ddlCodeType").val(data.TakeType); //交货方式
            $("#CarryType_ddlCodeType").val(data.CarryType); //运送方式
            $("#PayTypeUC_ddlCodeType").val(data.PayType); //结算方式
            $("#MoneyTypeUC_ddlCodeType").val(data.MoneyType); //支付方式
            if(data.CurrencyType!="0" && data.CurrencyType!="" && data.CurrencyType!=null && data.CurrencyType!="undefined")
            {
                $("#CurrencyType").val(data.CurrencyName); //币种
                $("#CurrencyType").attr("title", data.CurrencyType); //币种
                $("#Rate").val(data.ExchangeRate); //汇率
            }
            try {
                fnGetPriceByRate();
            } catch (e) { }
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}


////选择合同后带出订单明细信息
function fnSelectSellCon(ConId, ConNo) {

    fnDelRow();
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectSellContractUC.ashx",
        data: 'actionCon=info&ContractNo=' + ConNo,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取合同数据失败,请确认"); },
        success: function(data) {
            //合同基本信息    
            if (data.con != null) {
                $("#FromBillID").val(ConNo); //发货单编号
                $("#FromBillID").attr("title", ConId); //发货单ID
                $("#CustID").val(data.con[0].CustName); //客户名称
                $("#CustTel").val(data.con[0].CustTel); //客户类型
                $("#CustID").attr("title", data.con[0].CustID); //客户编号
                $("#PayTypeUC_ddlCodeType").val(data.con[0].PayType); //结算方式
                $("#CurrencyType").attr("title", data.con[0].CurrencyType); //币种
                $("#CurrencyType").val(data.con[0].CurrencyName);
                $("#Rate").val(data.con[0].Rate); //汇率
                $("#UserSeller").val(data.con[0].EmployeeName)//业务员名称
                $("#Seller").val(data.con[0].Seller)//业务员名称
                $("#Discount").val(FormatAfterDotNumber(parseFloat(data.con[0].Discount), precisionLength));      //整单折扣（%） 

                $("#sellTypeUC_ddlCodeType").val(data.con[0].SellType); //销售类别
                $("#BusiType").val(data.con[0].BusiType); //业务类型

                $("#MoneyTypeUC_ddlCodeType").val(data.con[0].MoneyType); //支付方式
                $("#TakeType_ddlCodeType").val(data.con[0].TakeType); //交货方式
                $("#CarryType_ddlCodeType").val(data.con[0].CarryType); //运送方式

                $("#DeptId").val(data.con[0].DeptName); //部门名称
                $("#SellDeptId").val(data.con[0].SellDeptId); //部门编号
                if (data.con[0].isAddTax == '0') {
                    $("#NotAddTax").attr("checked", "checked");
                    $("#isAddTax").removeAttr("checked", "checked");
                }
                else {
                    $("#isAddTax").attr("checked", "checked");
                    $("#NotAddTax").removeAttr("checked", "checked");
                }
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
            //订单明细信息
            if (data.conList != null) {
                $.each(data.conList, function(i, item) {
                        AddSignRow();
                        var txtTRLastIndex = findObj("txtTRLastIndex", document);
                        var rowID = parseInt(txtTRLastIndex.value);
                        
                    //$("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
                    $("#ProductID" + rowID).val(item.ProdNo); //商品编号
                    $("#hiddProductID" + rowID).val(item.ProductID); //商品ID
                    $("#ProductID" + rowID).attr("title", item.ProductID); //商品ID
                    $("#ProductName" + rowID).val(item.ProductName); //商品名称
                    //$("#UnitID" + rowID).val(item.CodeName); //单位名称
                    //$("#UnitID" + rowID).attr("title", item.UnitID); //单位ID
                    $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
                    $("#Specification" + rowID).val(item.Specification); //规格
                    $("#ColorID"+rowID).val(item.ColorName);//颜色
                    $("#TaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#hiddBaseTaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#Discount" + rowID).val(FormatAfterDotNumber(item.Discount, precisionLength)); //折扣
                    $("#TaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                    $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                    $("#Package" + rowID).val(item.Package); //包装要求
                    $("#SendTime" + rowID).val(item.SendTime); //交货期限
                    //$("#ProductCount" + rowID).val(item.ProductCount); //数量
                    $("#TotalFee" + rowID).val(item.ProductCount); //数量
                    $("#TotalPrice" + rowID).val(item.ProductCount); //数量
                    $("#TotalTax" + rowID).val(item.ProductCount); //数量
                    
                    //多计量单位启用
                    if($("#txtIsMoreUnit").val() == "1")
                    {
                        $("#BaseUnitID" + rowID).val(item.CodeName); //单位名称
                        $("#BaseUnitID" + rowID).attr("title", item.UnitID); //单位ID
                        $("#BaseCountD" + rowID).val(item.ProductCount); //基本数量
                        $("#ProductCount" + rowID).val(item.UsedUnitCount); //数量
                        $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UsedPrice, precisionLength)); //单价
                        $("#hiddBaseUnitPrice"+rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength));//基本单价（对应基本价格）
                    }
                    else
                    {
                       $("#UnitID" + rowID).val(item.CodeName); //单位名称
                       $("#UnitID" + rowID).attr("title", item.UnitID); //单位ID  
                       $("#ProductCount" + rowID).val(item.ProductCount); //数量
                       $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
                    }
                    $("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardSell, 2)); //单价
                    if($("#txtIsMoreUnit").val() == "1")
                    {   
                        //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
                        //GetUnitGroupSelect(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UsedUnitID);
                        GetUnitGroupSelectEx(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UsedUnitID,'fnTotalInfo();');
                    }
                    txtTRLastIndex.value = item.SortNo; //将行号推进下一行
                });
                //fnTotalInfo();
            }
        }
    });
    closeCondiv(); //关闭客户选择控件

}

////选择报价单后带出报价单明细信息
function fnSelectSellOffer(offerId, offerNo) {

    fnDelRow();
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SelectSellOfferUC.ashx",
        data: 'actionOff=info&offerNo=' + offerNo,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取合同数据失败,请确认"); },
        success: function(data) {
            //合同基本信息    
            if (data.off != null) {
                $("#FromBillID").val(data.off[0].OfferNo); //发货单编号
                $("#FromBillID").attr("title", offerId); //发货单ID
                $("#CustID").val(data.off[0].CustName); //客户名称
                $("#CustTel").val(data.off[0].CustTel); //客户类型
                $("#CustID").attr("title", data.off[0].CustID); //客户编号
                $("#PayTypeUC_ddlCodeType").val(data.off[0].PayType); //结算方式
                $("#CurrencyType").attr("title", data.off[0].CurrencyType); //币种
                $("#CurrencyType").val(data.off[0].CurrencyName);
                $("#Rate").val(data.off[0].Rate); //汇率
                $("#UserSeller").val(data.off[0].EmployeeName)//业务员名称
                $("#Seller").val(data.off[0].Seller)//业务员名称
                $("#sellTypeUC_ddlCodeType").val(data.off[0].SellType); //销售类别
                $("#BusiType").val(data.off[0].BusiType); //业务类型
                $("#Discount").val(FormatAfterDotNumber(parseFloat(data.off[0].Discount), precisionLength));      //整单折扣（%） 
                $("#MoneyTypeUC_ddlCodeType").val(data.off[0].MoneyType); //支付方式
                $("#TakeType_ddlCodeType").val(data.off[0].TakeType); //交货方式
                $("#CarryType_ddlCodeType").val(data.off[0].CarryType); //运送方式

                $("#DeptId").val(data.off[0].DeptName); //部门名称
                $("#SellDeptId").val(data.off[0].SellDeptId); //部门编号
                if (data.off[0].isAddTax == '0') {
                    $("#NotAddTax").attr("checked", "checked");
                    $("#isAddTax").removeAttr("checked", "checked");
                }
                else {
                    $("#isAddTax").attr("checked", "checked");
                    $("#NotAddTax").removeAttr("checked", "checked");
                }
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "该单据信息已不存在,请确认");
            }
            //订单明细信息
            if (data.offList != null) {
                $.each(data.offList, function(i, item) {
                        AddSignRow();
                         var txtTRLastIndex = findObj("txtTRLastIndex", document);
                         var rowID = parseInt(txtTRLastIndex.value);
                    
                    //$("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
                    $("#ProductID" + rowID).val(item.ProdNo); //商品编号
                    $("#hiddProductID" + rowID).val(item.ProductID); //商品ID
                    $("#ProductID" + rowID).attr("title", item.ProductID); //商品ID
                    $("#ProductName" + rowID).val(item.ProductName); //商品名称
                    //$("#UnitID" + rowID).val(item.CodeName); //单位名称
                    //$("#UnitID" + rowID).attr("title", item.UnitID); //单位ID
                    $("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
                    $("#Specification" + rowID).val(item.Specification); //规格
                    $("#ColorID"+rowID).val(item.ColorName);//颜色
                    $("#TaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#hiddBaseTaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#Discount" + rowID).val(FormatAfterDotNumber(item.Discount, precisionLength)); //折扣
                    $("#TaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                    $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                    $("#Package" + rowID).val(item.Package); //包装要求
                    $("#SendTime" + rowID).val(item.SendTime); //交货期限
                    //$("#ProductCount" + rowID).val(item.ProductCount); //数量
                    $("#TotalFee" + rowID).val(item.TotalFee); //数量
                    $("#TotalPrice" + rowID).val(item.TotalPrice); //数量
                    $("#TotalTax" + rowID).val(item.TotalTax); //数量
                    
                    //多计量单位启用
                    if($("#txtIsMoreUnit").val() == "1")
                    {
                        $("#BaseUnitID" + rowID).val(item.CodeName); //单位名称
                        $("#BaseUnitID" + rowID).attr("title", item.UnitID); //单位ID
                        $("#BaseCountD" + rowID).val(item.ProductCount); //基本数量
                    }
                    else
                    {
                       $("#UnitID" + rowID).val(item.CodeName); //单位名称
                       $("#UnitID" + rowID).attr("title", item.UnitID); //单位ID 
                    }
                    $("#ProductCount" + rowID).val(item.UsedUnitCount); //数量 
                    
                    $("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardSell, precisionLength)); //单价
                    $("#hiddBaseUnitPrice"+rowID).val(FormatAfterDotNumber(item.StandardSell, precisionLength));//基本单价（对应基本价格）
                    if($("#txtIsMoreUnit").val() == "1")
                    {   
                        //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
                        //GetUnitGroupSelect(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit(this)', "UnitID" + rowIndex, SaleUnitID);
                        GetUnitGroupSelectEx(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UnitID,'fnTotalInfo()');
                    }
                    txtTRLastIndex.value = item.SortNo; //将行号推进下一行
                });
                //fnTotalInfo();
            }
        }
    });
    closeOffdiv(); //关闭客户选择控件

}

//添加包装要求选择项的值
function fnGetPackage(id) {
    var pakeageUC = document.getElementById("PackageUC_ddlCodeType");
    var obj = document.getElementById(id);
    for (var i = 0; i < pakeageUC.length; i++) {
        var varItem = new Option(pakeageUC.options[i].text, pakeageUC.options[i].value);
        obj.options.add(varItem);

    }
}

//多选添加行
function AddSignRows()
{
    popTechObj.ShowListCheckSpecial('','check');
}
//多选填充值
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
                        if(!IsExistCheck(item.ProdNo))//如果是重复的物品编号不允许添加了
                        {
                             AddSignRow();
                             var txtTRLastIndex = findObj("txtTRLastIndex", document);
                             var rowIndex = parseInt(txtTRLastIndex.value);
                             //var rowIndex = $("#rowIndex").val(); //当前选择物品的行号
                            $("#ProductID" + rowIndex).val(item.ProdNo); //商品编号
                            $("#hiddProductID" + rowIndex).val(item.ID); //商品ID
                            $("#ProductID" + rowIndex).attr("title", item.ID); //商品ID
                            $("#ProductName" + rowIndex).val(item.ProductName); //商品名称
                            $("#Specification" + rowIndex).val(item.Specification); //规格
                            $("#ColorID"+rowIndex).val(item.ColorName);//颜色
                            $("#TaxPrice" + rowIndex).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                            $("#hiddTaxPrice" + rowIndex).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                            $("#hiddBaseTaxPrice" + rowIndex).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                            $("#Discount" + rowIndex).val(FormatAfterDotNumber(item.Discount, precisionLength)); //折扣
                            $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                            $("#hiddTaxRate" + rowIndex).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率

                            //如果是增值税
                            if ($("#isAddTax").attr("checked")) {
                                $("#TaxRate" + rowIndex).removeAttr("readonly");
                                $("#TaxRate" + rowIndex).val($("#hiddTaxRate" + rowIndex).val());

                                $("#TaxPrice" + rowIndex).val($("#hiddTaxPrice" + rowIndex).val());
                            }
                            //如果不是增值税
                            //如果是增值税
                            if ($("#NotAddTax").attr("checked")) {
                                $("#TaxRate" + rowIndex).attr("readonly", "readonly");
                                $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(0, precisionLength));

                                $("#TaxPrice" + rowIndex).val($("#UnitPrice" + rowIndex).val());
                            }
                            
                            //多计量单位启用
                            if($("#txtIsMoreUnit").val() == "1")
                            {
                                $("#BaseUnitID" + rowIndex).val(item.CodeName); //单位名称
                                $("#BaseUnitID" + rowIndex).attr("title", item.UnitID); //单位ID
                            }
                            else
                            {
                               $("#UnitID" + rowIndex).val(item.CodeName); //单位名称
                               $("#UnitID" + rowIndex).attr("title", item.UnitID); //单位ID  
                            }
                            
                            $("#hiddUnitPrice" + rowIndex).val(FormatAfterDotNumber(item.StandardSell, precisionLength)); //单价
                            $("#hiddBaseUnitPrice"+rowIndex).val(FormatAfterDotNumber(item.StandardSell, precisionLength));//基本单价（对应基本价格）
                            if($("#txtIsMoreUnit").val() == "1")
                            {   
                                //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
                                //GetUnitGroupSelect(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit(this)', "UnitID" + rowIndex, SaleUnitID);
                                GetUnitGroupSelectEx(item.ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit('+rowIndex+')', "UnitID" + rowIndex, item.SaleUnitID,'fnGetPriceByRate1('+rowIndex+')');
                            }
                            else
                            {
                                fnGetPriceByRate1(rowIndex);
                            }
                        }
                   });
                     
                      },
             
               complete:function(){}//接收数据完毕
           });
      closeProductdiv();
} 
//判断物品在明细中添加是否重复
function IsExistCheck(prodNo)
{
    var sign=false ;
    var signFrame = findObj("dg_Log",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
          var prodNo1 = document.getElementById("ProductID" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}

//添加新行
function AddSignRow() { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    var txtTRLastIndex = findObj("txtTRLastIndex", document);
    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m=0;
    
    var SetFunNo = "SetEventFocus('ProductID','ProductCount'," + rowID + ",false);";

    var newNameXH = newTR.insertCell(m); //添加列:序号
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
    m++;
    
//    var newFitNotd = newTR.insertCell(m); //添加列:物品编号
//    newFitNotd.className = "cell";
//    newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' readonly='readonly' style=' width:90%;' type='text'  class='tdinput' /><input type=\"hidden\" value='' id='hiddProductID" + rowID + "'/>"; //添加列内容
//    $("#ProductID" + rowID).bind("click", function() {
//        $("#rowIndex").val(rowID);
//        var v = 'ProductID' + rowID;
//        popTechObj.ShowList(v);
//    }); //绑定选择物品事件
    /*焦点1：大部分模块需要修改的地方(一行两列，第一列放物品编号输入框，第二列放放大的图片)*/
    var colProductNo = newTR.insertCell(m); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                                "<tr><td><input id=\"ProductID" + rowID + "\" onblur=\"SetMatchProduct(" + rowID + ",this.value);\" onkeydown=\"" + SetFunNo + "\"  class=\"tdinput\" size=\"7\" title=\"\"></td>" +
                                "<td align=\"right\"><input type=\"hidden\" id=\"ExistProduct_" + rowID + "\" value=\"0\"><img style=\"cursor: hand\" onclick=\"if(document.getElementById('ExistProduct_" + rowID + "').value=='1' ||document.getElementById('ProductID" + rowID + "').value==''){popTechObj.ShowList('" + rowID + "');};\" align=\"absMiddle\" src=\"../../../Images/search.gif\" height=\"21\"><input type=\"hidden\" value='' id='hiddProductID" + rowID + "'/></td>" +
                             "</tr></table>";
    m++;
    
    var newFitNametd = newTR.insertCell(m); //添加列:物品名称
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'  disabled='disabled'  style=' width:90%; ' type='text'  class='tdinput' />"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:规格
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'  style=' width:80%;' disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:颜色
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input  id='ColorID" + rowID + "'  style=' width:80%;' disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
    m++;
    
     //计量单位开启
    if ($("#txtIsMoreUnit").val() == "1") 
    {
        var newFitDesctd = newTR.insertCell(m); //添加列:基本单位(从物品档案带出不给修改)
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='BaseUnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:基本数量(从物品档案带出不给修改)
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='BaseCountD" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:单位
        newFitDesctd.className = "cell";
        newFitDesctd.id="UnitID"+rowID;
       // newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
        newFitDesctd.innerHTML = ""; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='ProductCount" + rowID + "' onblur=\"fnTotalInfo()\"   maxlength='10' type='text'  class='tdinput'  style=' width:90%;' onkeydown=\"SetEventFocus('ProductCount','UnitPrice'," + rowID + ",false);\"/>"; //添加列内容
        m++;
    }
    else
    {
        var newFitDesctd = newTR.insertCell(m); //添加列:单位
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容
        m++;
        
        var newFitDesctd = newTR.insertCell(m); //添加列:数量
        newFitDesctd.className = "cell";
        newFitDesctd.innerHTML = "<input id='ProductCount" + rowID + "' onblur=\"fnTotalInfo()\"   maxlength='10' type='text'  class='tdinput'  style=' width:90%;' onkeydown=\"SetEventFocus('ProductCount','UnitPrice'," + rowID + ",false);\"/>"; //添加列内容
        m++;
    }
    
    var newFitDesctd = newTR.insertCell(m); //添加列:交货期限
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input  id='SendTime" + rowID + "'   type='text'  class='tdinput'  maxlength='10' style=' width:80%;'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:包装要求
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<select id='Package" + rowID + "'   style=' width:90%;'></select>"; //添加列内容
    fnGetPackage("Package" + rowID); //添加包装要求选择项的值
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "' onblur=\"fnTotalInfo()\"   maxlength='10' type='text' class='tdinput' style=' width:90%;' onkeydown=\"SetEventFocus('UnitPrice','TaxPrice'," + rowID + ",false);\"/><input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:含税价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='TaxPrice" + rowID + "' onblur=\"fnTotalInfo()\"  maxlength='10'  type='text' class='tdinput' style=' width:90%;' onkeydown=\"SetEventFocus('TaxPrice','Discount'," + rowID + ",false);\"/> <input type=\"hidden\" value='0.00' id='hiddTaxPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseTaxPrice" + rowID + "'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:折扣(%)
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='Discount" + rowID + "' onblur=\"fnTotalInfo()\"  value='100'  maxlength='7' type='text' class='tdinput' style=' width:90%;' onkeydown=\"SetEventFocus('Discount','TaxRate'," + rowID + ",false);\"/> "; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列: 税率(%)
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='TaxRate" + rowID + "' onblur=\"fnTotalInfo()\"   type='text' class='tdinput' style=' width:90%;' onkeydown=\"SetEventFocus('TaxRate','ProductID'," + rowID + ",true);\"/> <input type=\"hidden\" value='0.00' id='hiddTaxRate" + rowID + "'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列: 含税金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='TotalFee" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列: 金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='TotalPrice" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列: 税额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='TotalTax" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列: 备注
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='Remark" + rowID + "' type='text' class='tdinput'  maxlength='100' style=' width:94%;'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列: 发货数量item.SendCount
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='DSendCount" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //计划生产数量item.PlanProductCount
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='DPlanProductCount" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;
                    
    //var newFitDesctd = newTR.insertCell(18); //使用库存数量
    //newFitDesctd.className = "cell";
  //  newFitDesctd.innerHTML = ''; //添加列内容
    txtTRLastIndex.value = rowID; //将行号推进下一行
    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        $("#TaxRate" + rowID).removeAttr("readonly");
       
    }
    //如果不是增值税
    //如果是增值税
    if ($("#NotAddTax").attr("checked")) {
        $("#TaxRate" + rowID).attr("readonly", "readonly");
        $("#TaxRate" + rowID).val(FormatAfterDotNumber(0, precisionLength));
       
    }

}

//确认按钮的调用的函数名称，业务逻辑不同模块各自处理
function Fun_ConfirmOperate() {
    if (confirm("是否确认单据？")) {
        var CodeType = ''; //报价单编号产生的规则
        var OrderNo = $("#OfferNo").val();        //退货单编号
        var ID=$("#hiddOrderID").val();//单据ID
        
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/sellOrderAdd.ashx",
            data: 'CodeType=' + escape(CodeType) + '&OrderNo=' + escape(OrderNo) + '&ID='+ID+ getMainParamValue()+'&action=confirm',
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            //complete :function(){hidePopup();},
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
            success: function(data) {
                if (data.sta == 1) {
                    $("#hiddOrderID").val(data.id);
                    $("#Confirmor").val($("#Creator").val());
                    $("#ConfirmDate").val($("#CreateDate").val());
                    $("#FromType").attr("disabled", "disabled");

                    $("#BillStatus").html("执行");
                    $("#hiddBillStatus").val('2');
                    $("#imgUnSave").css("display", "inline");
                    $("#btn_save").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#AddMore").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    if(hidCodeBarObj=='1')
                    {
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                    }
                    
                    $("#imgDel").css("display", "none");
                    $("#AddFee").css("display", "none");
                    $("#UnAddFee").css("display", "inline");
                    $("#DelFee").css("display", "none");
                    $("#UnDelFee").css("display", "inline");
                   
                    GetFlowButton_DisplayControl();

                }
                else {
                    hidePopup();

                }
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
            }
        });
    }
}


//终止合同
function fnEndOrder() {
    if (confirm("是否终止订单？")) {
        var CodeType = ''; //报价单编号产生的规则
        var OrderNo = $("#OfferNo").val();        //退货单编号
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/sellOrderAdd.ashx",
            data: 'CodeType=' + escape(CodeType) + '&OrderNo=' + escape(OrderNo) + '&action=end',
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            //complete :function(){hidePopup();},
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
            success: function(data) {
                if (data.sta == 1) {
                    $("#hiddOrderID").val(data.id);
                    $("#Confirmor").val($("#Creator").val());
                    $("#ConfirmDate").val($("#CreateDate").val());
                    $("#FromType").attr("disabled", "disabled");
                    
                    $("#BillStatus").html("手工结单");
                    $("#hiddBillStatus").val('4');
                    $("#imgUnSave").css("display", "inline");
                    $("#btn_save").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#AddMore").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    if(hidCodeBarObj=='1')
                    {
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                    }
                    
                    $("#imgDel").css("display", "none");
                    $("#hiddStatus").val('3');
                    GetFlowButton_DisplayControl();
                    fnGetOrderInfo(data.id); //获取报价单详细信息         
                }
                else {
                    hidePopup();
                }
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
            }
        });
    }
}


//结单按钮调用的函数名称，业务逻辑不同模块各自处理
//isFlag=true结单操作，isFlag=false取消结单
function Fun_CompleteOperate(isFlag) {
    var CodeType = ''; //报价单编号产生的规则
    var OrderNo = $("#OfferNo").val();        //退货单编号
    var acction = '';
    if (isFlag) {
        acction = 'close';
        if (!confirm("是否结单？")) {
            return;
        }
    }
    else {
        if (!confirm("是否取消结单？")) {
            return;
        }
        acction = 'unClose';
    }
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/sellOrderAdd.ashx",
        data: 'CodeType=' + escape(CodeType) + '&OrderNo=' + escape(OrderNo) + '&action=' + acction,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                if (isFlag) {
                    $("#BillStatus").html("手工结单");
                    $("#Closer").val($("#Creator").val());
                    $("#CloseDate").val($("#CreateDate").val());
                    $("#FromType").attr("disabled", "disabled");
                    $("#btn_save").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#AddMore").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    if(hidCodeBarObj=='1')
                    {
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                    }
                    
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#State").val('处理完');
                    $("#hiddBillStatus").val('4');
                    $("#AddFee").css("display", "none");
                    $("#UnAddFee").css("display", "inline");
                    $("#DelFee").css("display", "none");
                    $("#UnDelFee").css("display", "inline");
                    $("#btn_zzht").css("display", "none");
                    $("#btn_Unzzht").css("display", "inline");
                    $("#hiddStatus").val('2');
                }
                else {
                    $("#BillStatus").html("执行");
                    $("#Closer").val('');
                    $("#CloseDate").val('');
                    $("#FromType").attr("disabled", "disabled");
                    $("#btn_save").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#AddMore").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    if(hidCodeBarObj=='1')
                    {
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                    }
                    
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#hiddBillStatus").val('2');
                    $("#State").val('处理中');
                    $("#AddFee").css("display", "none");
                    $("#UnAddFee").css("display", "inline");
                    $("#DelFee").css("display", "none");
                    $("#UnDelFee").css("display", "inline");
                    $("#btn_zzht").css("display", "inline");
                    $("#btn_Unzzht").css("display", "none");
                    $("#hiddStatus").val('1');
                }
                $("#hiddOrderID").val(data.id);
                $("#FromType").attr("disabled", "disabled");
                GetFlowButton_DisplayControl();
            }
            else {
                hidePopup();

            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//选择物品
function Fun_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax, Discount, Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,GroupUnitNo,SaleUnitID,SaleUnitName,InUnitID,InUnitName,StockUnitID,StockUnitName,MakeUnitID,MakeUnitName,IsBatchNo,ColorName) {
    
    //var rowIndex = $("#rowIndex").val(); //当前选择物品的行号
    var rowIndex=popTechObj.InputObj; //当前选择物品的行号
    if(!IsExist2(ProdNo,rowIndex))//如果是重复的物品编号不允许添加了
    {
        document.getElementById('ExistProduct_' + rowIndex).value = '1';
        
        $("#ProductID" + rowIndex).val(ProdNo); //商品编号
        $("#hiddProductID" + rowIndex).val(ID); //商品ID
        $("#ProductID" + rowIndex).attr("title", ID); //商品ID
        $("#ProductName" + rowIndex).val(ProductName); //商品名称
        //$("#UnitID" + rowIndex).val(CodeName); //单位名称
        //$("#UnitID" + rowIndex).attr("title", UnitID); //单位ID
        $("#UnitPrice" + rowIndex).val(FormatAfterDotNumber(StandardCost, precisionLength)); //单价
        $("#Specification" + rowIndex).val(Specification); //规格
        $("#TaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
        $("#hiddTaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
        $("#hiddBaseTaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
        $("#Discount" + rowIndex).val(FormatAfterDotNumber(Discount, precisionLength)); //折扣
        $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(TaxRate, precisionLength)); //税率
        $("#hiddTaxRate" + rowIndex).val(FormatAfterDotNumber(TaxRate, precisionLength)); //税率
        $("#ColorID"+rowIndex).val(ColorName);//颜色

        //如果是增值税
        if ($("#isAddTax").attr("checked")) {
            $("#TaxRate" + rowIndex).removeAttr("readonly");
            $("#TaxRate" + rowIndex).val($("#hiddTaxRate" + rowIndex).val());
           
            $("#TaxPrice" + rowIndex).val($("#hiddTaxPrice" + rowIndex).val());
        }
        //如果不是增值税
        //如果是增值税
        if ($("#NotAddTax").attr("checked")) {
            $("#TaxRate" + rowIndex).attr("readonly", "readonly");
            $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(0, precisionLength));
          
            $("#TaxPrice" + rowIndex).val($("#UnitPrice" + rowIndex).val());
        }
        //$("#hiddUnitPrice" + rowIndex).val(FormatAfterDotNumber(StandardCost, precisionLength)); //单价
        //fnGetPriceByRate1(rowIndex);        
        //多计量单位启用
        if($("#txtIsMoreUnit").val() == "1")
        {
            $("#BaseUnitID" + rowIndex).val(CodeName); //单位名称
            $("#BaseUnitID" + rowIndex).attr("title", UnitID); //单位ID
        }
        else
        {
           $("#UnitID" + rowIndex).val(CodeName); //单位名称
           $("#UnitID" + rowIndex).attr("title", UnitID); //单位ID  
        }
        $("#hiddUnitPrice" + rowIndex).val(FormatAfterDotNumber(StandardSell, precisionLength)); //单价
        $("#hiddBaseUnitPrice"+rowIndex).val(FormatAfterDotNumber(StandardSell, precisionLength));//基本单价（对应基本价格）
        if($("#txtIsMoreUnit").val() == "1")
        {   
            //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
            //GetUnitGroupSelect(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit(this)', "UnitID" + rowIndex, SaleUnitID);
            GetUnitGroupSelectEx(ID, 'SaleUnit', 'selectUnitID' + rowIndex, 'ChangeUnit('+rowIndex+')', "UnitID" + rowIndex, SaleUnitID,'fnGetPriceByRate1('+rowIndex+')');
        }
    }
    else
    {
        popMsgObj.ShowMsg('明细已存在！');
    }

}
//改变计量单位时改变基本数量和价格
function ChangeUnit(rowid)
{
    //根据单位和数量计算基本数量。参数（单位下列表ID，数量文本框ID，基本数量文本框ID，单价，基本单价）
    CalCulateNum("selectUnitID"+rowid ,"ProductCount"+rowid ,"BaseCountD"+rowid,"UnitPrice"+rowid,"hiddBaseUnitPrice"+rowid , precisionLength);
    CalCulateNum("selectUnitID"+rowid ,"ProductCount"+rowid ,"BaseCountD"+rowid,"TaxPrice"+rowid,"hiddBaseTaxPrice"+rowid , precisionLength);
    $("#hiddTaxPrice" + rowid).val($("#TaxPrice" + rowid).val());   //把带回的单价放到隐藏域中
    $("#hiddUnitPrice" + rowid).val($("#UnitPrice" + rowid).val());   //把带回的单价放到隐藏域中
    //fnTotalInfo();
    fnGetPriceByRate1(rowid);
}

//新选择物品根据汇率计算单价含税价
function fnGetPriceByRate1(rowid) {
    var Rate = $("#Rate").val();           //汇率
    if(Rate==''|| Rate=="0.0000")
    {
        Rate = FormatAfterDotNumber(1,4);
    }

    var UnitPrice = $("#hiddUnitPrice" + rowid).val();   //单价
    var TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
    }
    //如果不是增值税
    //如果是增值税
    if ($("#NotAddTax").attr("checked")) {
        TaxPrice = $("#hiddUnitPrice" + rowid).val();    //含税价
    }
    UnitPrice = parseFloat(UnitPrice) / parseFloat(Rate);
    TaxPrice = parseFloat(TaxPrice) / parseFloat(Rate);
    $("#UnitPrice" + rowid).val(FormatAfterDotNumber(UnitPrice, precisionLength));   //单价
    $("#TaxPrice" + rowid).val(FormatAfterDotNumber(TaxPrice, precisionLength));    //含税价                                      

    fnTotalInfo();
}

//改变币种时重新获取单价含税价
function fnGetPriceByRate() {
    var Rate = $("#Rate").val();           //汇率
    if(Rate==''|| Rate=="0.0000")
    {
        Rate = FormatAfterDotNumber(1,4);
    }

    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            var UnitPrice = $("#hiddUnitPrice" + rowid).val();   //单价
            var TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
            //如果是增值税
            if ($("#isAddTax").attr("checked")) {
                TaxPrice = $("#hiddTaxPrice" + rowid).val();    //含税价
            }
            //如果不是增值税
            //如果是增值税
            if ($("#NotAddTax").attr("checked")) {
                TaxPrice = $("#hiddUnitPrice" + rowid).val();    //含税价
            }
            UnitPrice = parseFloat(UnitPrice) / parseFloat(Rate);
            TaxPrice = parseFloat(TaxPrice) / parseFloat(Rate);

            $("#UnitPrice" + rowid).val(FormatAfterDotNumber(UnitPrice, precisionLength));   //单价
            $("#TaxPrice" + rowid).val(FormatAfterDotNumber(TaxPrice, precisionLength));    //含税价                                      
        }
    }
    fnTotalInfo();
}
//取消确认
function Fun_UnConfirmOperate() {
    if (confirm("是否取消确认？")) {
        var CodeType = ''; //报价单编号产生的规则
        var OrderNo = $("#OfferNo").val();        //退货单编号
        var acction = 'UnConfirm';
        var ID=$("#hiddOrderID").val();//单据ID

        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/sellOrderAdd.ashx",
            data: 'CodeType=' + escape(CodeType) + '&OrderNo=' + escape(OrderNo) +'&ID='+ID+ getMainParamValue()+'&action=' + acction,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            //complete :function(){hidePopup();},
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
            success: function(data) {
                if (data.sta == 1) {

                    $("#BillStatus").html("制单");
                    $("#Closer").val('');
                    $("#CloseDate").val('');
                    $("#imgUnSave").css("display", "none");
                    $("#btn_save").css("display", "inline");
                    $("#imgUnAdd").css("display", "none");
                    $("#imgAdd").css("display", "inline");
                    $("#AddMore").css("display", "inline");
                    $("#imgUnDel").css("display", "none");
                    if(hidCodeBarObj=='1')
                    {
                        $("#GetGoods").css("display", "inline");
                        $("#UnGetGoods").css("display", "none");
                    }
                    
                    $("#imgDel").css("display", "inline");
                    $("#Confirmor").val('');
                    $("#ConfirmDate").val('');

                    $("#hiddBillStatus").val('1');
                    $("#hiddStatus").val('1');
                    $("#AddFee").css("display", "inline");
                    $("#UnAddFee").css("display", "none");
                    $("#DelFee").css("display", "inline");
                    $("#UnDelFee").css("display", "none");

                    $("#hiddOrderID").val(data.id);
                    $("#FromType").removeAttr("disabled");
                    GetFlowButton_DisplayControl();
                }
                else {
                    hidePopup();

                }
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
            }
        });
    }
}

/*
* 附件处理
*/
function DealAttachment(flag) {
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "") {
        return;
    }
    //上传附件
    else if ("upload" == flag) {
        ShowUploadFile();
    }
    else if ("clear" == flag) {
        var FilePath = document.getElementById("hfPageAttachment").value;
        var FileName = document.getElementById("spanAttachmentName").innerHTML;
        DeleteUploadFile(FilePath, FileName);
    }

    //下载附件
    else if ("download" == flag) {
        //获取附件路径
        attachUrl = document.getElementById("hfPageAttachment").value;
        //下载文件
        window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + attachUrl, "_blank");

    }
}


/*
* 上传文件后回调处理
*/
function AfterUploadFile(url, docName) {
    if (url != "") {
        //设置附件路径
        document.getElementById("hfPageAttachment").value = url;
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "block";
        document.getElementById("spanAttachmentName").innerHTML = docName;
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
    }
}


//保存数据
function InsertSellOfferData() {
    if (!CheckInput()) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var strfitinfo = getDropValue().join("|");
    var strFeeInfo = getFeeValue().join("|");
    var CodeType = ''; //订单编号产生的规则
    var OrderNo = $("#OfferNo").val(); //订单编号                                                                                                                    
    
    var BillStatus = '1';

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOrderAdd.ashx",
        data: 'CodeType=' + escape(CodeType) + '&OrderNo=' + escape(OrderNo) + '&BillStatus=' + escape(BillStatus) +
            getMainParamValue()+
             '&strfitinfo=' + escape(strfitinfo) + '&strFeeInfo=' + escape(strFeeInfo) + '&action=update'+GetExtAttrValue(),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                $("#hiddOrderID").val(data.id);

                $("#hiddBillStatus").val('1');
                $("#btn_save").css("display", "inline");
                $("#btn_save").css("display", "inline");
                
                GetFlowButton_DisplayControl();
            }
            else {
                hidePopup();

            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}
//获取主表参数
function getMainParamValue()
{
    var paramStr="";
    var CustID = $("#CustID").attr("title"); 	  //客户ID（关联客户信息表）                             
    var CustTel = $("#CustTel").val();        //客户联系电话                                                                                   
    var Title = $.trim($("#Title").val());          //主题                                                 
    var FromType = $("#FromType").val();       //源单类型（0无来源，1销售报价单，2销售合同）          
    var FromBillID = $("#FromBillID").attr("title");     //源单ID
    var Seller = $("#Seller").val();          //业务员ID（对应员工表ID）
    var SellDeptId = $("#SellDeptId").val();    //部门(对部门表ID)                                
    var SellType = $("#sellTypeUC_ddlCodeType").val();       //销售类别ID（对应分类代码表ID）                       
    var BusiType = $("#BusiType").val();       //业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨
    var OrderMethod = $("#OrderMethod_ddlCodeType").val();    //订货方式ID（对应分类代码表ID）                       
    var PayType = $("#PayTypeUC_ddlCodeType").val();        //结算方式ID（对应分类代码表ID）                       
    var MoneyType = $("#MoneyTypeUC_ddlCodeType").val();      //支付方式ID                                           
    var CarryType = $("#CarryType_ddlCodeType").val();      //运送方式ID（对应分类代码表ID）                       
    var TakeType = $("#TakeType_ddlCodeType").val();       //交货方式ID（对应分类代码表ID）
    var CurrencyType = $("#CurrencyType").attr("title");   //币种ID
    var Rate = $("#Rate").val();           //汇率                                                  
    var TotalPrice = $("#TotalPrice").val();    //金额合计
    var Tax = $("#Tax").val();           //税额合计                                                  
    var TotalFee = $("#TotalFee").val();      //含税金额合计                                              
    var Discount = $("#Discount").val();      //整单折扣（%）                                             
    var DiscountTotal = $("#DiscountTotal").val(); //折扣金额                                                  
    var RealTotal = $("#RealTotal").val();     //折否含税金额                                                                                 
    var CountTotal = $("#CountTotal").val();    //合同数量合计                         
    var SaleFeeTotal = $("#SaleFeeTotal").val();   //销售费用合计                                         
    var Status = '1'; //订单处理状太               
    var SendDate = $("#SendDate").val();       //最迟发货时间（精确到分）                             
    var OrderDate = $("#OrderDate").val();      //下单日期                                             
    var StartDate = $("#StartDate").val();      //开始日期                                             
    var EndDate = $("#EndDate").val();        //截止日期                                             
    var TheyDelegate = $("#TheyDelegate").val();   //对方代表                                             
    var OurDelegate = $("#OurDelegate").val();    //我方代表                                                                
    var PayRemark = $("#PayRemark").val();      //付款说明                                             
    var DeliverRemark = $("#DeliverRemark").val();  //交付说明                                             
    var PackTransit = $("#PackTransit").val();    //包装运输说明                                         
    var StatusNote = $("#StatusNote").val();     //异常终止原因                                         
    var CustOrderNo = $("#CustOrderNo").val();    //客户订单号                                           
    var Remark = $("#Remark").val();         //备注
    var Attachment = $("#hfPageAttachment").val().replace(/\\/g, "\\\\"); //附件的路径
    var CanViewUser = $("#CanViewUser").val();//可查看此订单人员
    var ProjectID = $("#hiddProjectID").val();//所属项目ID
    
    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        var isAddTax = 1; //是否增值税（0否,1是 ）    
    }
    //如果不是增值税
    if ($("#NotAddTax").attr("checked")) {
        var isAddTax = 0; //是否增值税（0否,1是 ）
    }

    if ($("#FromType").val() == 0) {
        FromBillID = '';
    }
    
    paramStr='&CustID=' + escape(CustID) + '&CustTel=' + escape(CustTel) + '&Title=' + escape(Title) +
            '&FromType=' + escape(FromType) + '&FromBillID=' + escape(FromBillID) + '&Seller=' + escape(Seller) +
            '&SellDeptId=' + escape(SellDeptId) + '&SellType=' + escape(SellType) + '&BusiType=' + escape(BusiType) +
            '&OrderMethod=' + escape(OrderMethod) + '&PayType=' + escape(PayType) + '&MoneyType=' + escape(MoneyType) +
            '&CarryType=' + escape(CarryType) + '&TakeType=' + escape(TakeType) + '&CurrencyType=' + escape(CurrencyType) +
            '&Rate=' + escape(Rate) + '&TotalPrice=' + escape(TotalPrice) + '&Tax=' + escape(Tax) + '&TotalFee=' + escape(TotalFee) +
            '&Discount=' + escape(Discount) + '&SaleFeeTotal=' + escape(SaleFeeTotal) + '&DiscountTotal=' + escape(DiscountTotal) +
            '&RealTotal=' + escape(RealTotal) + '&isAddTax=' + escape(isAddTax) + '&CountTotal=' + escape(CountTotal) +
            '&SendDate=' + escape(SendDate) + '&OrderDate=' + escape(OrderDate) + '&StartDate=' + escape(StartDate) +
            '&EndDate=' + escape(EndDate) + '&TheyDelegate=' + escape(TheyDelegate) + '&OurDelegate=' + escape(OurDelegate) +
            '&Status=' + escape(Status) + '&PayRemark=' + escape(PayRemark) + '&DeliverRemark=' + escape(DeliverRemark) +
            '&PackTransit=' + escape(PackTransit) + '&StatusNote=' + escape(StatusNote) + '&CustOrderNo=' + escape(CustOrderNo) +
            '&Remark=' + escape(Remark) + '&Attachment=' + escape(Attachment) + 
             '&CanViewUser=' + escape(CanViewUser) + '&ProjectID=' + escape(ProjectID);
  return paramStr;
    
}

////获取明细数据
function getDropValue() {
    fnTotalInfo();
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (var i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            //ChangeUnit(rowid);
            var SortNo = j;      //序号（行号）                             
            var ProductID = $("#ProductID" + rowid).attr("title");   //物品ID（对应物品表ID）
            //var ProductCount = $("#ProductCount" + rowid).val(); //数量                                     
            //var UnitID = $("#UnitID" + rowid).attr("title");      //单位ID（对应计量单位ID）                 
            //var UnitPrice = $("#UnitPrice" + rowid).val();   //单价                                     
            var TaxPrice = $("#TaxPrice" + rowid).val();    //含税价                                   
            var Discount = $("#Discount" + rowid).val();    //折扣（%）                                
            var TaxRate = $("#TaxRate" + rowid).val();     //税率（%）                                
            var TotalFee = $("#TotalFee" + rowid).val();    //含税金额                                 
            var TotalPrice = $("#TotalPrice" + rowid).val();  //金额                                     
            var TotalTax = $("#TotalTax" + rowid).val();    //税额
            var SendTime = $("#SendTime" + rowid).val();    //交货期限                                 
            var Package = $("#Package" + rowid).val();     //包装要求ID
            var Remark = $("#Remark" + rowid).val();      //备注
            
            var UnitID = $("#UnitID" + rowid).attr("title");      //基本单位ID（对应计量单位ID）
            var ProductCount = $("#ProductCount" + rowid).val(); //基本数量 
            var UnitPrice = $("#UnitPrice" + rowid).val();   //单价（基本单价）
            var UsedUnitID=$("#UnitID" + rowid).attr("title");//单位
            var UsedUnitCount=$("#ProductCount" + rowid).val();//数量
            var UsedPrice=$("#UnitPrice" + rowid).val();//单价（对应于单位的单价，也就是明细中显示的实际单价）
            var ExRate=1;//换算比率
            
            if($("#txtIsMoreUnit").val() == "1")
            {
                UnitID=$("#BaseUnitID" + rowid).attr("title");
                ProductCount=$("#BaseCountD" + rowid).val();
                UnitPrice = $("#hiddBaseUnitPrice" + rowid).val(); 
                
                var UnitRate=document.getElementById("selectUnitID"+rowid).value;
                var arrUnitRate=UnitRate.split('|');
                UsedUnitID=arrUnitRate[0];
                ExRate=arrUnitRate[1];
                
            }

            SendOrderFit_Item[j] = [[SortNo], [ProductID], [ProductCount], [UnitID], [UnitPrice], [TaxPrice], [Discount], [TaxRate], [TotalFee], [TotalPrice], [TotalTax], [SendTime], [Package], [Remark],[UsedUnitID],[UsedUnitCount],[UsedPrice],[ExRate]];
            arrlength++;
        }
    }
    return SendOrderFit_Item;
}

////获取费用明细数据
function getFeeValue() {
    var SellFeeFit_Item = new Array();
    var signFrame = findObj("dg_Log2", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;

            var FeeType = $("#FeeType" + rowid).attr("title")//费用类型
            var FeeTotal = $("#FeeTotal" + rowid).val()//费用金额             
            var FeeRemark = $("#FeeRemark" + rowid).val()//备注 

            SellFeeFit_Item[j] = [[j], [FeeType], [FeeTotal], [FeeRemark]];
        }
    }
    return SellFeeFit_Item;
}

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var CustID = $("#CustID").val(); //客户ID（关联客户信息表）                                                                             
    var FromType = $("#FromType").val(); //源单类型（0无来源，1销售机会）                                                                        
    //var Title = $.trim($("#Title").val()); //报价主题
    var Seller = $("#UserSeller").val(); //业务员ID（对应员工表ID）
    var SellDeptId = $("#DeptId").val();    //部门(对部门表ID)
    var Discount = $.trim($("#Discount").val()); //整单折扣（%）              
    
    var StartDate = $("#StartDate").val(); //开始日期
    var EndDate = $("#EndDate").val(); //截止日期
    var FromBillID = $("#FromBillID").val(); //来源单据编号
    var SendDate = $("#SendDate").val();       //最迟发货时间（精确到分）                             
    var OrderDate = $("#OrderDate").val();      //下单日期   
    var CurrencyType = $("#CurrencyType").val();   //币种ID


    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    var TotalPrice = $("#TotalPrice").val();    //金额合计                                                                             
    var Tax = $("#Tax").val();           //税额合计                                                                             
    var TotalFee = $("#TotalFee").val();      //含税金额合计

    if (!IsNumeric(TotalPrice, 22, precisionLength)) {
        isFlag = false;
        fieldText = fieldText + "金额合计|";
        msgText = msgText + "金额合计超出范围|";
    }

    if (!IsNumeric(Tax, 22, precisionLength)) {
        isFlag = false;
        fieldText = fieldText + "税额合计|";
        msgText = msgText + "税额合计超出范围|";
    }

    if (!IsNumeric(TotalFee, 22, precisionLength)) {
        isFlag = false;
        fieldText = fieldText + "含税金额合计|";
        msgText = msgText + "含税金额合计超出范围|";
    }
    
    if (FromType == "1") {
        if (FromBillID == "") {
            isFlag = false;
            fieldText = fieldText + "来源单据|";
            msgText = msgText + "请选择销售报价单|";
        }
    }
    if (FromType == "2") {
        if (FromBillID == "") {
            isFlag = false;
            fieldText = fieldText + "来源单据|";
            msgText = msgText + "请选择销售合同|";
        }
    }
//    if (Title == "") {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//        msgText = msgText + "请输入订单主题|";
//    }

    if (SellDeptId == "") {
        isFlag = false;
        fieldText = fieldText + "部门|";
        msgText = msgText + "请选择部门|";
    }
    if (CustID == "") {
        isFlag = false;
        fieldText = fieldText + "客户名称|";
        msgText = msgText + "请选择客户|";
    }
    if (Seller == "") {
        isFlag = false;
        fieldText = fieldText + "业务员|";
        msgText = msgText + "请选择业务员|";
    }
    if (OrderDate == "") {
        isFlag = false;
        fieldText = fieldText + "下单日期|";
        msgText = msgText + "请选择下单日期|";
    }

    if (CurrencyType == "") {
        isFlag = false;
        fieldText = fieldText + "币种|";
        msgText = msgText + "请选择币种|";
    }
    if (Discount.length > 0) {
        if (!IsNumeric(Discount, 12, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "整单折扣|";
            msgText = msgText + "整单折扣输入有误，请输入有效的数值！|";
        }
    }
    if (StartDate != "" && EndDate != "") {
        if (CompareDate(StartDate, EndDate) == '1') {
            isFlag = false;
            fieldText = fieldText + "开始、截止日期|";
            msgText = msgText + "开始日期不能晚于截止日期|";
        }
    }

    //验证费用明细
    var signFrame1 = findObj("dg_Log2", document);

    for (i = 1; i < signFrame1.rows.length; i++) {
        if (signFrame1.rows[i].style.display != "none") {
            var rowid = signFrame1.rows[i].id;

            var FeeType = $("#FeeType" + rowid).val()//费用类型
            var FeeTotal = $("#FeeTotal" + rowid).val()//费用金额    
            if (FeeType == "") {
                isFlag = false;
                fieldText = fieldText + "销售费用明细（行号：" + i + "）|";
                msgText = msgText + "请选择费用类别|";
            }
            if (FeeTotal == "") {
                isFlag = false;
                fieldText = fieldText + "售费用明细（行号：" + i + "）|";
                msgText = msgText + "请输入金额|";
            }
            else {
                if (!IsNumeric(FeeTotal, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "售费用明细（行号：" + i + "）|";
                    msgText = msgText + "金额输入有误，请输入有效的数值！|";
                }

            }

        }
    }

    //验证明细信息
    var signFrame = findObj("dg_Log", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            iCount = iCount + 1;
            var rowid = signFrame.rows[i].id;
            var ProductID = $("#ProductID" + rowid).val();   //物品ID（对应物品表ID）
            var ProductCount = $("#ProductCount" + rowid).val(); //数量                                              
            var UnitPrice = $("#UnitPrice" + rowid).val();   //单价                                     
            var TaxPrice = $("#TaxPrice" + rowid).val();    //含税价                                   
            var Discount = $("#Discount" + rowid).val();    //折扣（%）                                
            var TaxRate = $("#TaxRate" + rowid).val();     //税率（%）      
            //var SendTime = $("#SendTime" + rowid).val();    //发货期限

            var TotalFee = $("#TotalFee" + rowid).val();         //含税金额                        
            var TotalPrice = $("#TotalPrice" + rowid).val();       //金额                            
            var TotalTax = $("#TotalTax" + rowid).val();         //税额

            if (!IsNumeric(TotalFee, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "含税金额超出范围|";
            }

            if (!IsNumeric(TotalPrice, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "金额超出范围|";
            }

            if (!IsNumeric(TotalTax, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "税额超出范围|";
            }
            
            if (TaxRate == "") {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "请输入税率！|";
            }
            else {
                if (!IsNumeric(TaxRate, 12, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "订单明细（行号：" + i + "）|";
                    msgText = msgText + "税率输入有误，请输入有效的数值！|";
                }
            }
            if (TaxPrice == "") {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "请输入含税价！|";
            }
            else {
                if (!IsNumeric(TaxPrice, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "订单明细（行号：" + i + "）|";
                    msgText = msgText + "含税价输入有误，请输入有效的数值！|";
                }
            }
            if (Discount == "") {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "请输入折扣！|";
            }
            else {
                if (!IsNumeric(Discount, 12, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "订单明细（行号：" + i + "）|";
                    msgText = msgText + "折扣输入有误，请输入有效的数值！|";
                }
            }

            if (UnitPrice == "") {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "请输入单价！|";
            }
            else {
                if (!IsNumeric(UnitPrice, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "订单明细（行号：" + i + "）|";
                    msgText = msgText + "单价输入有误，请输入有效的数值！|";
                }
            }
            if (ProductCount == "") {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "请输入数量！|";
            }
            else {
                if (!IsNumeric(ProductCount, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "订单明细（行号：" + i + "）|";
                    msgText = msgText + "数量输入有误，请输入有效的数值！|";
                }
            }
//            if (SendTime == "") {
//                isFlag = false;
//                fieldText = fieldText + "交货期限（行号：" + i + "）|";
//                msgText = msgText + "请输入交货期限！|";
//            }
//            else {
//                if (!IsNumeric(SendTime, 8, precisionLength)) {
//                    isFlag = false;
//                    fieldText = fieldText + "交货期限（行号：" + i + "）|";
//                    msgText = msgText + "交货期限输入有误，请输入有效的数值！|";
//                }
//            }
            if (ProductID == "") {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "请选择物品！|";
            }
        }
    }
    if (iCount == 0) {
        isFlag = false;
        fieldText = fieldText + "订单明细|";
        msgText = msgText + "报价单明细不能为空！|";
    }
    if (!isFlag) {
        //注：两个方法显示弹出提示信息层,方法一是对字段用红色处理，方法二是所有的提示信息传入未处理颜色

        //方法一
        popMsgObj.Show(fieldText, msgText);

        //方法二
        //popMsgObj.ShowMsg('所有的错误信息字符串');
    }

    return isFlag;
}


//提交审批成功和审批成功后调用一个函数0:提交审批成功  1:审批成功
function Fun_FlowApply_Operate_Succeed(values) {
    if (values != 2 && values != 3) {
        $("#FromType").attr("disabled", "disabled");
        $("#btn_save").css("display", "none");
        $("#imgUnSave").css("display", "inline");
        $("#imgAdd").css("display", "none");
        $("#AddMore").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        if(hidCodeBarObj=='1')
        {
            $("#GetGoods").css("display", "none");
            $("#UnGetGoods").css("display", "inline");
        }
        
        $("#imgDel").css("display", "none");
        $("#imgUnDel").css("display", "inline");
        $("#AddFee").css("display", "none");
        $("#UnAddFee").css("display", "inline");
        $("#DelFee").css("display", "none");
        $("#UnDelFee").css("display", "inline");
    }
    else {
        $("#btn_save").css("display", "inline");
        $("#imgUnSave").css("display", "none");
        $("#imgAdd").css("display", "inline");
        $("#AddMore").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        if(hidCodeBarObj=='1')
        {
            $("#GetGoods").css("display", "inline");
            $("#UnGetGoods").css("display", "none");
        }
        
        $("#imgDel").css("display", "inline");
        $("#imgUnDel").css("display", "none");
        $("#FromType").removeAttr("disabled");
        $("#AddFee").css("display", "inline");
        $("#UnAddFee").css("display", "none");
        $("#DelFee").css("display", "inline");
        $("#UnDelFee").css("display", "none");

    }
}

//打印功能
function fnPrintOrder() {
    var OrderNo = $.trim($("#OfferNo").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '') {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存单据再打印！");
            return;
        }
        window.open('../../../Pages/PrinttingModel/SellManager/PrintSellOrder.aspx?no=' + OrderNo);
}


//去除全选按钮
function fnUnSelect1(obj) {

    if (!obj.checked) {
        $("#checkall1").removeAttr("checked");
        return;
    }
    else {
        //验证明细信息
        var signFrame = findObj("dg_Log2", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 1; i < signFrame.rows.length; i++) {
            if (signFrame.rows[i].style.display != "none") {
                iCount = iCount + 1;
                var rowid = signFrame.rows[i].id;
                if ($("#chk2" + rowid).attr("checked")) {
                    checkCount = checkCount + 1;
                }
            }
        }
        if (checkCount == iCount) {

            $("#checkall1").attr("checked", "checked");
        }

    }
}


//去除全选按钮
function fnUnSelect(obj) {

    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }

    else {
        //验证明细信息
        var signFrame = findObj("dg_Log", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 1; i < signFrame.rows.length; i++) {
            if (signFrame.rows[i].style.display != "none") {
                iCount = iCount + 1;
                var rowid = signFrame.rows[i].id;
                if ($("#chk" + rowid).attr("checked")) {
                    checkCount = checkCount + 1;
                }
            }
        }
        if (checkCount == iCount) {
            
            $("#checkall").attr("checked", "checked");
        }
       
    }
}

//---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
/*
 参数：商品ID，商品编号，商品名称，去税售价，单位ID，单位名称，销项税率（%），含税售价，销售折扣，
规格，类型名称，类型ID，含税进价，去税进价，进项税率(%)，标准成本
*/
//根据条码获取的商品信息填充数据
function GetGoodsDataByBarCode(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax, Discount, Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,a,b,c,d,e,ColorName)
{
    if(!IsExist(ID))//如果重复记录，就不增加
    {
       AddSignRow();//插入行
       var txtTRLastIndex = findObj("txtTRLastIndex", document);
       var rowID = parseInt(txtTRLastIndex.value);
       //$("#rowIndex").val(rowID);
       popTechObj.InputObj=rowID;
       //填充数据
       Fun_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax, Discount, Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,a,b,c,d,e,'','','','','',ColorName);
       $("#ProductCount"+rowID).val(1);
       fnGetPriceByRate1(rowID);
    }
    else
    {
        document.getElementById("ProductCount"+rerowID).value=parseFloat(document.getElementById("ProductCount"+rerowID).value)+1;
        fnTotalInfo();
    }
    
}

var rerowID="";
//判断是否有相同记录有返回true，没有返回false
function IsExist(ID)
{
     var signFrame = findObj("dg_Log", document);
     if((typeof(signFrame)=="undefined")|| signFrame==null)
     {
        return false;
     }
     for (i = 1; i < signFrame.rows.length; i++) {//判断商品是否在明细列表中
         if (signFrame.rows[i].style.display != "none") {
              var rowid = signFrame.rows[i].id;
              var ProductID = $("#ProductID" + rowid).attr("title"); //商品ID（对应商品表ID）
              if(ProductID==ID)
               {
                   rerowID=i;
                   return true;
               }
            }
        }
     return false;
}
/*判断是否有相同记录有返回true，没有返回false*/
//var rerowID = "";
function IsExist2(prodNo,rows) {
    var signFrame = findObj("dg_Log", document);
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var prodNo1 = document.getElementById("ProductID" + i).value;
        if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
            if (i != rows) {
                rerowID = i;
                return true;
            }
        }
    }
    return false;
}
//---------------------------------------------------条码扫描END------------------------------------------------------------------------------------
/*库存快照*/
function ShowSnapshot()
{
       var intProductID = 0;
       var detailRows = 0;
       var snapProductName = '';
       var snapProductNo = '';
       var signFrame = findObj("dg_Log",document); 
      for(var i=1;i<signFrame.rows.length;i++)
       {
            if(signFrame.rows[i].style.display!="none")
            {
                var rowid = signFrame.rows[i].id;
                if(document.getElementById('chk'+rowid).checked)
                {
                    detailRows ++;
                    intProductID = $("#hiddProductID" + rowid).val();
                    snapProductName= $("#ProductName"+rowid).val();
                    snapProductNo = $("#ProductID"+rowid).val();
                }
            }
            
       }
       
       if(detailRows==1)
       {
           if(intProductID<=0 || strlen(cTrim(intProductID,0))<=0)
           {
              popMsgObj.ShowMsg('选中的明细行中没有添加物品');
              return false;  
           }
            ShowStorageSnapshot(intProductID,snapProductName,snapProductNo);
           
       }
       else
       {
          popMsgObj.ShowMsg('请选择单个物品查看库存快照');
          return false;
       }   
}

//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
//    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
//    var arrKey = strKey.split('|');
//    
//    if(typeof(data)!='undefined')
//    {
//       $.each(data,function(i,item){
//            for (var t = 0; t < arrKey.length; t++) {
//                //不为空的字段名才取值
//                if ($.trim(arrKey[t]) != '') {
//                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);
//                }
//            }

//       });
//    }
     try{
        $("#ExtField1").val(data.ExtField1);
        $("#ExtField2").val(data.ExtField2);
        $("#ExtField3").val(data.ExtField3);
        $("#ExtField4").val(data.ExtField4);
        $("#ExtField5").val(data.ExtField5);
        $("#ExtField6").val(data.ExtField6);
        $("#ExtField7").val(data.ExtField7);
        $("#ExtField8").val(data.ExtField8);
        $("#ExtField9").val(data.ExtField9);
        $("#ExtField10").val(data.ExtField10);
        }
    catch(Error){}
}

/*焦点2：鼠标失去焦点时，匹配数据库物品信息*/
function SetMatchProduct(rows, values) {
    popTechObj.InputObj = '';
    var ProdNo = values;

    if (values != "") {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../../Handler/Office/SupplyChain/BatchProductInfo.ashx?ProdNo=" +escape(ProdNo), //目标地址
            cache: false,
            success: function(msg) {
                var rowsCount = 0;
                var rowsExistCount = 0;

                if (typeof (msg.dataBatch) != 'undefined') {
                    $.each(msg.dataBatch, function(i, item) {

                        if (!IsExist2(item.ProdNo,rows)) {
                            rowsCount++;
                            //$("#rowIndex").val(rows);
                            popTechObj.InputObj=rows;
                            Fun_FillParent_Content(item.ID, item.ProdNo, item.ProductName, item.StandardSell, item.UnitID,
                                            item.CodeName, item.TaxRate, item.SellTax, item.Discount, item.Specification,
                                            item.CodeTypeName, item.TypeID, item.StandardBuy, item.TaxBuy, item.InTaxRate,
                                            item.StandardCost, item.GroupUnitNo, item.SaleUnitID, 'saleUnitName', item.InUnitID,
                                            'inUnitName', item.StockUnitID, 'stockUnitName', item.MakeUnitID, 'maxkUnitName',
                                            item.IsBatchNo, item.ColorName);
                            document.getElementById('ExistProduct_' + rows).value = '1';
                        }
                        else {
                            rowsExistCount++;
                        }
                    });
                }
                if (rowsCount == 0) {
                
                    if (rowsExistCount > 0) {
                        popMsgObj.Show("销售合同计划明细|", "明细中不允许存在重复记录");
                    }
                    else {
                        popMsgObj.Show("物品编号" + rows + "|", "不存在此" + values + "物品，请重新输入或选择物品");
                    }
                    ClearOneProductInfo(rows);
                }

            },
            error: function() { popMsgObj.ShowMsg('匹配物品数据时发生请求异常!'); },
            complete: function() { }
        });
    }
    else {
        var productID=document.getElementById('ProductID'+rows).title;////物品ID
        if (productID !="undefined" && productID != "") {
            popMsgObj.Show("物品编号" + values + "|", "请重新输入或选择物品");
        }
    }
}

/*手工输入物品编号不存在时，清除行信息(之前输入一个存在的物品，之后再输入一个不存在的物品)*/
function ClearOneProductInfo(rows) {
    document.getElementById('ProductID' + rows).value = '';
    document.getElementById('ProductID' + rows).title = '';
    document.getElementById('hiddProductID'+rows).value='';
    document.getElementById('Specification' + rows).value = '';
    document.getElementById('ProductName' + rows).value = '';
    document.getElementById('ExistProduct_' + rows).value = '0';

    if ($("#txtIsMoreUnit").val() == "1") {
        document.getElementById('BaseUnitID' + rows).title = '';
        document.getElementById('BaseUnitID' + rows).value = '';
        document.getElementById('UnitID'+rows).innerHTML = '';
    }
    else {
        document.getElementById('UnitID' + rows).title = '';
        document.getElementById('UnitID' + rows).value = '';
    }
}
