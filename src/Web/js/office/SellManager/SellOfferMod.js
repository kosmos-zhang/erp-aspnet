
$(document).ready(function() {
fnGetExtAttr();
GetExtAttr("officedba.SellOffer",null);//扩展属性

    var pakeageUC = document.getElementById("PackageUC_ddlCodeType");
    pakeageUC.style.display = "none";

    $("#sellType_ddlCodeType").css("width", "120px");
    $("#PayType_ddlCodeType").css("width", "120px");
    $("#MoneyType_ddlCodeType").css("width", "120px");
    $("#CarryType_ddlCodeType").css("width", "120px");
    $("#TakeType_ddlCodeType").css("width", "120px");

    //获取URl参数
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var orderID = requestObj['id']; //销售机会Id
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031301', 'ModuleID=2031302');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (orderID != null) {
            $("#hiddOrderID").val(orderID);
            fnGetOrderInfo(orderID); //获取报价单详细信息         
        }
    }

});

//获取报价单详细信息
function fnGetOrderInfo(orderID) {
    fnDelRow();
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOfferList.ashx",
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
                fnGetDetail(data.data[0].OfferNo); //获取报价单明细信息
                fnGetSellOfferHistory(data.data[0].OfferNo); //获取报价单历史记录
                fnIsRef(); //单据是否被其他单据引用
                fnStatus(data.data[0].BillStatus); //根据单据状态决定页面按钮操作
                fnGetBillStatus('5', '1', orderID); //获取审批状态，判断操作按钮

            }
        }
    });
}

//给页面赋值
function fnInitPage(data) {
    $("#OfferNo").val(data.OfferNo);        //报价单编号
    $("#CustID").val(data.CustName);         //客户ID（关联客户信息表）
    $("#CustID").attr("title", data.CustID);         //客户ID（关联客户信息表）
    $("#CustTel").val(data.CustTel);        //客户联系电话
    $("#FromType").val(data.FromType);       //源单类型（0无来源，1销售机会）
    $("#FromBillID").val(data.ChanceNo);     //销售机会ID
    $("#FromBillID").attr("title", data.FromBillID);     //销售机会ID
    $("#Title").val(data.Title);          //报价主题
    $("#UserSeller").val(data.SellerName);         //业务员(对应员工表ID)
    $("#hiddSeller").val(data.Seller);         //业务员(对应员工表ID)
    $("#hiddDeptID").val(data.SellDeptId);     //部门(对部门表ID)
    $("#DeptId").val(data.DeptName);     //部门(对部门表ID)
    $("#sellType_ddlCodeType").val(data.SellType);       //销售类别ID（对应分类代码表ID）
    $("#BusiType").val(data.BusiType);       //业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨）
    $("#PayType_ddlCodeType").val(data.PayType);        //结算方式ID（对应分类代码表ID）
    $("#MoneyType_ddlCodeType").val(data.MoneyType);      //支付方式ID（对应分类代码表ID）
    $("#CarryType_ddlCodeType").val(data.CarryType);      //运送方式ID（对应分类代码表ID）
    $("#TakeType_ddlCodeType").val(data.TakeType);       //交货方式ID（对应分类代码表ID）
    $("#ExpireDate").val(data.ExpireDate);     //有效截止日期
    $("#TotalPrice").val(FormatAfterDotNumber(data.TotalPrice, precisionLength));     //金额合计
    $("#TotalTax").val(FormatAfterDotNumber(data.TotalTax, precisionLength));       //税额合计
    $("#TotalFee").val(FormatAfterDotNumber(data.TotalFee, precisionLength));       //含税金额合计
    $("#Discount").val(FormatAfterDotNumber(data.Discount, precisionLength));       //整单折扣（%）
    $("#DiscountTotal").val(FormatAfterDotNumber(data.DiscountTotal, precisionLength));  //折扣金额
    $("#RealTotal").val(FormatAfterDotNumber(data.RealTotal, precisionLength));      //折后含税金额

    if (data.isAddTax == '0') {
        $("#NotAddTax").attr("checked", "checked"); //是否增值税（0否,1是
        $("#isAddTax").removeAttr("checked", "checked"); //是否增值税（0否,1是
    }
    else {
        $("#isAddTax").attr("checked", "checked"); //是否增值税（0否,1是
        $("#NotAddTax").removeAttr("checked", "checked"); //是否增值税（0否,1是
    }

    $("#CountTotal").val(FormatAfterDotNumber(data.CountTotal,precisionLength));     //产品数量合计
    $("#CurrencyType").attr("title", data.CurrencyType); //币种
    $("#CurrencyType").val(data.CurrencyName);
    $("#Rate").val(data.Rate);           //汇率
    $("#hiddBillStatus").val(data.BillStatus); //单据状态
    $("#QuoteTime").val(data.QuoteTime);      //报价次数
    $("#OfferDate").val(data.OfferDate);      //报价日期
    $("#PayRemark").val(data.PayRemark);      //付款说明
    $("#DeliverRemark").val(data.DeliverRemark);  //交付说明
    $("#PackTransit").val(data.PackTransit);    //包装运输说明
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
    
    $("#CanViewUser").val(data.CanViewUser); //可查看人员
    $("#UserCanViewUserName").val(data.CanViewUserName); //可查看人员名称
    GetFlowButton_DisplayControl();
    
     ExtAttControl_FillValue(data);//扩展属性

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



//获取报价单明细信息
function fnGetDetail(ChanceNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOfferList.ashx",
        data: "action=detail&orderNo=" + escape(ChanceNo),
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
                    $("#ProductID" + rowID).attr("title", item.ProductID); //商品ID
                    $("#ProductName" + rowID).val(item.ProductName); //商品名称
                    //$("#UnitID" + rowID).val(item.CodeName); //单位名称
                    //$("#UnitID" + rowID).attr("title", item.UnitID); //单位ID

                    //$("#UnitPrice" + rowID).val(FormatAfterDotNumber(item.UnitPrice, precisionLength)); //单价
                    $("#Specification" + rowID).val(item.Specification); //规格
                    $("#TaxPrice" + rowID).val(FormatAfterDotNumber(item.TaxPrice, precisionLength)); //含税价
                    $("#hiddTaxPrice" + rowID).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                    $("#hiddBaseTaxPrice" + rowID).val(FormatAfterDotNumber(item.SellTax, precisionLength)); //含税价
                    $("#Discount" + rowID).val(FormatAfterDotNumber(item.Discount, precisionLength)); //折扣
                    $("#TaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate, precisionLength)); //税率
                    $("#hiddTaxRate" + rowID).val(FormatAfterDotNumber(item.TaxRate1, precisionLength)); //税率
                    $("#TotalFee" + rowID).val(FormatAfterDotNumber(item.TotalFee, precisionLength));    //含税金额
                    $("#TotalPrice" + rowID).val(FormatAfterDotNumber(item.TotalPrice, precisionLength));  //金额
                    $("#TotalTax" + rowID).val(FormatAfterDotNumber(item.TotalTax, precisionLength));    //税额
                    //$("#ProductCount" + rowID).val(item.ProductCount);    //数量
                    $("#SendTime" + rowID).val(item.SendTime);    //交货期限
                    $("#Package" + rowID).val(item.Package);     //包装要求ID
                    $("#Remark" + rowID).val(item.Remark);      //备注
                    $("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardCost, precisionLength));
                    //如果是增值税
                    if ($("#isAddTax").attr("checked")) {
                        $("#TaxRate" + rowID).removeAttr("readonly");
                        $("#TaxRate" + rowID).css("background-color", "#FFFFFF");
                    }
                    //如果不是增值税
                    //如果是增值税
                    if ($("#NotAddTax").attr("checked")) {
                        $("#TaxRate" + rowID).attr("readonly", "readonly");
                        $("#TaxRate" + rowID).val(FormatAfterDotNumber(0,precisionLength));
                        $("#TaxRate" + rowID).css("background-color", "#CCCCCC");
                    }
                    //debugger;
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
                    //$("#hiddUnitPrice" + rowID).val(FormatAfterDotNumber(item.StandardSell, 2)); //单价
                    if($("#txtIsMoreUnit").val() == "1")
                    {   
                        //关联单位  注：返回的value值为：单位ID和换算率的组合，以“|”分隔
                       GetUnitGroupSelectEx(item.ProductID, 'SaleUnit', 'selectUnitID' + rowID, 'ChangeUnit('+rowID+')', "UnitID" + rowID, item.UsedUnitID,'');
                    }
                    txtTRLastIndex.value = rowID; //将行号推进下一行
                });
               // fnTotalInfo();
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
                $("#imgUnAdd").css("display", "inline");
                $("#GetGoods").css("display", "none");
                $("#UnGetGoods").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#FromType").attr("disabled", "disabled");
                $("#btnZCBJ").css("display", "none");
                $("#btnUnZCBJ").css("display", "inline");
                $("#FromType").attr("disabled", "disabled");
                break;

            case '4': //手工结单
                $("#btn_save").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#GetGoods").css("display", "none");
                $("#UnGetGoods").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#FromType").attr("disabled", "disabled");
                $("#btnZCBJ").css("display", "none");
                $("#btnUnZCBJ").css("display", "inline");
                break;

            case '5':
                $("#btn_save").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#GetGoods").css("display", "none");
                $("#UnGetGoods").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#btnZCBJ").css("display", "none");
                $("#btnUnZCBJ").css("display", "inline");
                $("#FromType").attr("disabled", "disabled");
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
            $("#FromType").attr("disabled", "disabled");
            $("#btn_save").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#GetGoods").css("display", "none");
            $("#UnGetGoods").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#btnZCBJ").css("display", "none");
            $("#btnUnZCBJ").css("display", "inline");
        } catch (e) { }
        $("#isRef").val("被引用");
    }
}

//获取报价单历史记录
function fnGetSellOfferHistory(ChanceNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOfferList.ashx",
        data: "action=his&orderNo=" + escape(ChanceNo),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            $("#SellOfferHisReTB tbody").find("tr.newrow").remove();
            $.each(data.data, function(i, item) {
            
                $("<tr class='newrow'></tr>").append(
                 "<td height='22' align='center'><a href='#' onclick=fnHisInfo('" + item.OfferNo + "','" + item.OfferTime + "')>" + item.OfferDate + "</a></td>" +
                 "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                 "<td height='22' align='center'>" + item.OfferTime + "</td>" +
                 "<td height='22' align='center'>" + FormatAfterDotNumber(item.TotalPrice, precisionLength) + "</td>").appendTo($("#SellOfferHisReTB tbody"));
            });
        },
        complete: function() { chanceList("SellOfferHisReTB", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}

//获取报价记录详细信息
function fnHisInfo(OfferNo, OfferTime) {
    document.getElementById('divSellOfferHistory').style.display = 'block';
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOfferList.ashx",
        data: "action=hisDetail&orderNo=" + escape(OfferNo) + "&OfferTime=" + escape(OfferTime),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            $("#hisList tbody").find("tr.newrow").remove();
            if (data.data != null) {
                $.each(data.data, function(i, item) {
                    $("<tr class='newrow'></tr>").append(
                    "<td height='22' align='center'> " + item.ProductName + "</td>" +
                    "<td height='22' align='center'> " + item.OfferDate + "</td>" +
                    "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                    "<td height='22' align='center'>" + item.OfferTime + "</td>" +
                    "<td height='22' align='center'>" + FormatAfterDotNumber(item.TotalPrice, precisionLength) + "</td>").appendTo($("#hisList tbody"));
                });
            }
        },
        complete: function() { chanceList("hisList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}

//table行颜色
function chanceList(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 0; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;

        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
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
            try {
                switch (data.sta) {
                    case 0: //未提交审批         
                        break;
                    case 1: //当前单据正在待审批
                        $("#btn_save").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#btnZCBJ").css("display", "none");
                        $("#btnUnZCBJ").css("display", "inline");
                        $("#FromType").attr("disabled", "disabled");
                        break;
                    case 2: //当前单据正在审批中
                        $("#btn_save").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#GetGoods").css("display", "none");
                        $("#UnGetGoods").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#btnZCBJ").css("display", "none");
                        $("#btnUnZCBJ").css("display", "inline");
                        $("#FromType").attr("disabled", "disabled");
                        break;
                    case 3: //当前单据已经通过审核
                        //制单状态的审批通过单据,不可修改
                        if ($("#BillStatus").html() == "制单") {
                            $("#btn_save").css("display", "none");
                            $("#imgUnSave").css("display", "inline");
                            $("#imgAdd").css("display", "none");
                            $("#imgUnAdd").css("display", "inline");
                            $("#GetGoods").css("display", "none");
                            $("#UnGetGoods").css("display", "inline");
                            $("#imgDel").css("display", "none");
                            $("#imgUnDel").css("display", "inline");
                            $("#btnZCBJ").css("display", "none");
                            $("#btnUnZCBJ").css("display", "inline");
                            $("#FromType").attr("disabled", "disabled");
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
            window.location.href = 'SellOfferList.aspx' + URLParams;
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
   
            alertdiv('DeptId,hiddDeptID');
       
}
//选择币种
function fnSelectCurrency() {

    popSellCurrObj.ShowList('CurrencyType', 'Rate');

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

function fnSelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//选择源单
function fnSelectOfferInfo() {
    if ($("#FromType").val() == 1) {
        if ($("#FromType").attr("disabled") != true) {
            popSellSendObj.ShowList('protion');
        }
    }
}

//清除销售机会
function clearSellChance() {
    fnFromTypeChange(document.getElementById("FromType"));

    closeSellSenddiv();
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

//添加包装要求选择项的值
function fnGetPackage(id) {
    var pakeageUC = document.getElementById("PackageUC_ddlCodeType");
    var obj = document.getElementById(id);
    for (var i = 0; i < pakeageUC.length; i++) {
        var varItem = new Option(pakeageUC.options[i].text, pakeageUC.options[i].value);
        obj.options.add(varItem);
    }
}

//选择业务员
function fnSelectSeller() {
   
        alertdiv('UserSeller,hiddSeller');
   
}

//验证整单折扣是否正确
function fnCheckPer(obj) {
    if (obj.value.length > 0) {
        Number_round(obj, precisionLength);
        if (!IsNumeric(obj.value, 12, precisionLength)) {
            obj.value = Number_round(100, precisionLength);
            //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "代销提成率输入错误,请确认！");
        }
    }
    else {
        obj.value = Number_round(100, precisionLength);
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
                DiscountDetail = 100;
            }
            else {
                Number_round(document.getElementById("Discount" + rowid), precisionLength);
                if (!IsNumeric($("#Discount" + rowid).val(), 12, precisionLength)) {
                    $("#Discount" + rowid).val(FormatAfterDotNumber(100,precisionLength));
                    DiscountDetail = 100;
                }
                else {
                    if (100 < parseFloat(DiscountDetail)) {
                        $("#Discount" + rowid).val(FormatAfterDotNumber(100,precisionLength));
                    }
                    if (parseFloat(DiscountDetail) < 0) {
                        $("#Discount" + rowid).val(FormatAfterDotNumber(100,precisionLength));
                    }
                }
            }
            var TaxRateDetail = $("#TaxRate" + rowid).val(); //税率
            if ($.trim(TaxRateDetail) == '') {
                TaxRateDetail = 100;
            }
            else {
                Number_round(document.getElementById("TaxRate" + rowid), precisionLength);
                if (!IsNumeric($("#TaxRate" + rowid).val(), 12, precisionLength)) {
                    $("#TaxRate" + rowid).val(FormatAfterDotNumber(100,precisionLength));
                    TaxRateDetail = 100;
                }
                else {
                    if (100 < parseFloat(TaxRateDetail)) {
                        $("#TaxRate" + rowid).val(FormatAfterDotNumber(100,precisionLength));
                    }
                    if (parseFloat(TaxRateDetail) < 0) {
                        $("#TaxRate" + rowid).val(FormatAfterDotNumber(100,precisionLength));
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
    $("#TotalTax").val(FormatAfterDotNumber(Tax, precisionLength));
    $("#TotalFee").val(FormatAfterDotNumber(TotalFee, precisionLength));
    $("#CountTotal").val(FormatAfterDotNumber(CountTotal, precisionLength));
    $("#DiscountTotal").val(FormatAfterDotNumber(((TotalFee * (100 - Discount) / 100) + parseFloat(DiscountTotalDetail)), precisionLength));
    $("#RealTotal").val(FormatAfterDotNumber((TotalFee * parseFloat(Discount) / 100), precisionLength));
}

//选择报价单来源时
function fnFromTypeChange(obj) {
    if (obj.selectedIndex == 0) {
        //清除已填写数据
        fnDelRow();
        $("#FromBillID").val(''); //报价单编号
        $("#FromBillID").removeAttr("title"); //报价单ID
        $("#CustID").val(''); //客户名称
        $("#CustID").removeAttr("title"); //客户编号
        $("#BusiType").attr("selectedIndex", 0); //业务类型
        $("#PayType_ddlCodeType").attr("selectedIndex", 0); //结算方式
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").removeAttr("title"); //币种        
        $("#sellType_ddlCodeType").attr("selectedIndex", 0); //销售类别
        $("#Rate").val(FormatAfterDotNumber(0,4)); //汇率           
        $("#UserSeller").val(''); //业务员名称
        $("#hiddSeller").val(''); //业务员编号
        $("#DeptId").val(''); //部门名称
        $("#hiddDeptID").val(''); //部门编号
        $("#MoneyType_ddlCodeType").attr("selectedIndex", 0); //支付方式
        $("#CarryType_ddlCodeType").attr("selectedIndex", 0); //运送方式
        $("#TakeType_ddlCodeType").attr("selectedIndex", 0); //交货方式
        $("#OrderMethod_ddlCodeType").attr("selectedIndex", 0); //订货方式
    }
    if (obj.selectedIndex == 1) {
        fnDelRow();
        //清除已填写数据
        $("#FromBillID").val(''); //报价单编号
        $("#FromBillID").removeAttr("title"); //报价单ID
        $("#CustID").val(''); //客户名称
        $("#CustID").removeAttr("title"); //客户编号
        $("#BusiType").attr("selectedIndex", 0); //业务类型
        $("#PayType_ddlCodeType").attr("selectedIndex", 0); //结算方式
        $("#sellType_ddlCodeType").attr("selectedIndex", 0); //销售类别
        $("#CurrencyType").val(''); //币种
        $("#CurrencyType").removeAttr("title"); //币种
        $("#Rate").val(FormatAfterDotNumber(0,4)); //汇率           
        $("#UserSeller").val(''); //业务员名称
        $("#hiddSeller").val(''); //业务员编号
        $("#DeptId").val(''); //部门名称
        $("#hiddDeptID").val(''); //部门编号  
        $("#MoneyType_ddlCodeType").attr("selectedIndex", 0); //支付方式
        $("#CarryType_ddlCodeType").attr("selectedIndex", 0); //运送方式
        $("#TakeType_ddlCodeType").attr("selectedIndex", 0); //交货方式
        $("#OrderMethod_ddlCodeType").attr("selectedIndex", 0); //订货方式

    }
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
                $("#TaxRate" + rowIndex).css("background-color", "#FFFFFF");
                $("#TaxPrice" + rowIndex).val($("#hiddTaxPrice" + rowIndex).val());
            }
            //如果不是增值税
            if ($("#NotAddTax").attr("checked")) {
                $("#TaxRate" + rowIndex).attr("readonly", "readonly");
                $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(0,precisionLength));
                $("#TaxRate" + rowIndex).css("background-color", "#CCCCCC");
                $("#TaxPrice" + rowIndex).val($("#UnitPrice" + rowIndex).val());
            }
        }
    }
    fnTotalInfo();
}

//选择客户
function fnSelectCustInfo() {
    if ($("#FromType").val() == 0) {
        popSellCustObj.ShowList('protion');
    }
}

//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号
    $("#BusiType").attr("selectedIndex", 0); //业务类型
    $("#PayType_ddlCodeType").attr("selectedIndex", 0); //结算方式
    $("#sellType_ddlCodeType").attr("selectedIndex", 0); //销售类别
    $("#CurrencyType").val(''); //币种
    $("#CurrencyType").removeAttr("title"); //币种
    $("#Rate").val(FormatAfterDotNumber(0,4)); //汇率           

    $("#MoneyType_ddlCodeType").attr("selectedIndex", 0); //支付方式
    $("#CarryType_ddlCodeType").attr("selectedIndex", 0); //运送方式
    $("#TakeType_ddlCodeType").attr("selectedIndex", 0); //交货方式
    $("#OrderMethod_ddlCodeType").attr("selectedIndex", 0); //订货方式
    $("#CustTel").val(''); //客户类型
    closeSellModuCustdiv(); //关闭客户选择控件
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
            $("#PayType_ddlCodeType").val(data.PayType); //结算方式
            $("#Rate").val(data.ExchangeRate); //汇率
            $("#MoneyType_ddlCodeType").val(data.MoneyType); //支付方式
            $("#CurrencyType").val(data.CurrencyName); //币种
            $("#CurrencyType").attr("title", data.CurrencyType); //币种
            try {
                fnGetPriceByRate();
            } catch (e) { }
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}
//再次报价
function fnOfferAgain() {
    
    if (!CheckInput()) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var strfitinfo = getDropValue().join("|");
    var strInfo = fnGetInfo();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOfferAdd.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=again&status='+$("#hiddBillStatus").val(),
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
                fnGetOrderInfo(data.id);
                $("#hiddBillStatus").val('1');
              
               
                GetFlowButton_DisplayControl();
            }
            else {
                hidePopup();

            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
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
    var strInfo = fnGetInfo();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellOfferAdd.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=update',
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
                //fnGetOrderInfo(data.id);
                $("#hiddBillStatus").val('1');
                    
                GetFlowButton_DisplayControl();
            }
            else {
                hidePopup();

            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//确认按钮的调用的函数名称，业务逻辑不同模块各自处理
function Fun_ConfirmOperate() {
    if (confirm("是否确认单据？")) {
        var CodeType = ''; //报价单编号产生的规则
        var OfferNo = $("#OfferNo").val();        //退货单编号
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/SellOfferAdd.ashx",
            data: 'CodeType=' + escape(CodeType) + '&OfferNo=' + escape(OfferNo) + '&action=confirm',
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
                    $("#btn_save").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#GetGoods").css("display", "none");
                    $("#UnGetGoods").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#btnZCBJ").css("display", "none");
                    $("#btnUnZCBJ").css("display", "inline");
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


//结单按钮调用的函数名称，业务逻辑不同模块各自处理
//isFlag=true结单操作，isFlag=false取消结单
function Fun_CompleteOperate(isFlag) {
    var CodeType = ''; //报价单编号产生的规则
    var OfferNo = $("#OfferNo").val();        //退货单编号
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
        url: "../../../Handler/Office/SellManager/SellOfferAdd.ashx",
        data: 'CodeType=' + escape(CodeType) + '&OfferNo=' + escape(OfferNo) + '&action=' + acction,
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
                    $("#FromType").attr("disabled", "true");
                    $("#btn_save").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#GetGoods").css("display", "none");
                    $("#UnGetGoods").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#btnZCBJ").css("display", "none");
                    $("#btnUnZCBJ").css("display", "inline");
                    $("#hiddBillStatus").val('4');

                }
                else {
                    $("#BillStatus").html("执行");
                    $("#Closer").val('');
                    $("#CloseDate").val('');
                    $("#imgUnSave").css("display", "inline");
                    $("#btn_save").css("display", "none");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#GetGoods").css("display", "none");
                    $("#UnGetGoods").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#hiddBillStatus").val('2');
                    $("#btnZCBJ").css("display", "none");
                    $("#btnUnZCBJ").css("display", "inline");
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

//取消确认
function Fun_UnConfirmOperate() {
    if (confirm("是否取消确认？")) {
        var CodeType = ''; //报价单编号产生的规则
        var OfferNo = $("#OfferNo").val();        //退货单编号
        var acction = 'UnConfirm';

        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SellManager/SellOfferAdd.ashx",
            data: 'CodeType=' + escape(CodeType) + '&OfferNo=' + escape(OfferNo) + '&action=' + acction,
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
                    $("#imgUnDel").css("display", "none");
                    $("#GetGoods").css("display", "inline");
                    $("#UnGetGoods").css("display", "none");
                    $("#imgDel").css("display", "inline");
                    $("#Confirmor").val('');
                    $("#ConfirmDate").val('');
                    $("#btnZCBJ").css("display", "inline");
                    $("#btnUnZCBJ").css("display", "none");
                    $("#hiddBillStatus").val('1');

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

//选择物品
function Fun_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, CodeName, TaxRate, SellTax, Discount, Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,GroupUnitNo,SaleUnitID,SaleUnitName,InUnitID,InUnitName,StockUnitID,StockUnitName,MakeUnitID,MakeUnitName,IsBatchNo) {
    var rowIndex = $("#rowIndex").val(); //当前选择物品的行号
    $("#ProductID" + rowIndex).val(ProdNo); //商品编号
    $("#ProductID" + rowIndex).attr("title", ID); //商品ID
    $("#ProductName" + rowIndex).val(ProductName); //商品名称
    //$("#UnitID" + rowIndex).val(CodeName); //单位名称
    //$("#UnitID" + rowIndex).attr("title", UnitID); //单位ID    
    $("#UnitPrice" + rowIndex).val(FormatAfterDotNumber(StandardSell, precisionLength)); //单价
    $("#Specification" + rowIndex).val(Specification); //规格
    $("#TaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
    $("#hiddTaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
    $("#hiddBaseTaxPrice" + rowIndex).val(FormatAfterDotNumber(SellTax, precisionLength)); //含税价
    $("#Discount" + rowIndex).val(FormatAfterDotNumber(Discount, precisionLength)); //折扣
    $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(TaxRate, precisionLength)); //税率
    $("#hiddTaxRate" + rowIndex).val(FormatAfterDotNumber(TaxRate, precisionLength)); //税率
    // $("#FromType" + rowIndex).attr("title", "0"); //单据来源

    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        $("#TaxRate" + rowIndex).removeAttr("readonly");
        $("#TaxRate" + rowIndex).val($("#hiddTaxRate" + rowIndex).val());
        $("#TaxRate" + rowIndex).css("background-color", "#FFFFFF");
        $("#TaxPrice" + rowIndex).val($("#hiddTaxPrice" + rowIndex).val());
    }
    //如果不是增值税
    if ($("#NotAddTax").attr("checked")) {
        $("#TaxRate" + rowIndex).attr("readonly", "readonly");
        $("#TaxRate" + rowIndex).val(FormatAfterDotNumber(0,precisionLength));
        $("#TaxRate" + rowIndex).css("background-color", "#FFFFFF");
        $("#TaxPrice" + rowIndex).val($("#UnitPrice" + rowIndex).val());
    }
//    $("#hiddUnitPrice" + rowIndex).val(FormatAfterDotNumber(StandardCost, precisionLength)); //单价
//    fnGetPriceByRate1(rowIndex);
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
    else
    {
       fnGetPriceByRate1(rowIndex); 
    }
}
//改变单位时计算数量和单价
function ChangeUnit(rowid)
{
    //根据单位和数量计算基本数量。参数（单位下列表ID，数量文本框ID，基本数量文本框ID，单价，基本单价）
    CalCulateNum("selectUnitID"+rowid ,"ProductCount"+rowid ,"BaseCountD"+rowid,"UnitPrice"+rowid,"hiddBaseUnitPrice"+rowid , precisionLength);
    CalCulateNum("selectUnitID"+rowid ,"ProductCount"+rowid ,"BaseCountD"+rowid,"TaxPrice"+rowid,"hiddBaseTaxPrice"+rowid , precisionLength);
    $("#hiddTaxPrice" + rowid).val($("#TaxPrice" + rowid).val());   //把带回的含税价放到隐藏域中
    $("#hiddUnitPrice" + rowid).val($("#UnitPrice" + rowid).val());   //把带回的单价放到隐藏域中
    //fnTotalInfo();
    fnGetPriceByRate1(rowid)
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

//获取主表信息
function fnGetInfo() {
    var strInfo = '';
    var CodeType = ''; //报价单编号产生的规则
    var OfferNo = $("#OfferNo").val(); //报价单编号    
    var CustID = $("#CustID").attr("title");         //客户ID（关联客户信息表）                               
    var CustTel = $("#CustTel").val();        //客户联系电话                                           
    var FromType = $("#FromType").val();       //源单类型（0无来源，1销售机会）                         
    var FromBillID = $("#FromBillID").attr("title");     //销售机会ID                                             
    var Title = $("#Title").val();          //报价主题                                               
    var PayType = $("#PayType_ddlCodeType").val();       //结算方式ID（对应分类代码表ID）
    var Seller = $("#hiddSeller").val();        //业务员(对应员工表ID)                                              
    var SellDeptId = $("#hiddDeptID").val();    //部门(对部门表ID)                                                  
    var MoneyType = $("#MoneyType_ddlCodeType").val();     //支付方式ID                                                        
    var TakeType = $("#TakeType_ddlCodeType").val();      //交货方式ID                                                        
    var CarryType = $("#CarryType_ddlCodeType").val();     //运送方式ID                                     
    var SellType = $("#sellType_ddlCodeType").val();      //销售类别ID（对应分类代码表ID）                          
    var BusiType = $("#BusiType").val();       //业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨）            
    var ExpireDate = $("#ExpireDate").val();     //有效截止日期                                           
    var TotalPrice = $("#TotalPrice").val();     //金额合计                                               
    var TotalTax = $("#TotalTax").val();       //税额合计                                               
    var TotalFee = $("#TotalFee").val();       //含税金额合计                                           
    var Discount = $("#Discount").val();       //整单折扣（%）                                          
    var DiscountTotal = $("#DiscountTotal").val();  //折扣金额                                               
    var RealTotal = $("#RealTotal").val();      //折后含税金额                                                                          
    var CountTotal = $("#CountTotal").val();     //产品数量合计                                           
    var CurrencyType = $("#CurrencyType").attr("title");   //币种ID                                                 
    var Rate = $("#Rate").val();           //汇率                                                                                
    var QuoteTime = $("#QuoteTime").val();      //报价次数                                               
    var OfferDate = $("#OfferDate").val();      //报价日期                                               
    var PayRemark = $("#PayRemark").val();      //付款说明                                               
    var DeliverRemark = $("#DeliverRemark").val();  //交付说明                                               
    var PackTransit = $("#PackTransit").val();    //包装运输说明
    var Remark = $("#Remark").val();
    var CanViewUser=$("#CanViewUser").val();//可查看申请单人员
    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        var isAddTax = 1; //是否增值税（0否,1是 ）    
    }
    //如果不是增值税
    if ($("#NotAddTax").attr("checked")) {
        var isAddTax = 0; //是否增值税（0否,1是 ）   
    }

    strInfo = 'CodeType=' + escape(CodeType) + '&OfferNo=' + escape(OfferNo) + '&CustID=' + escape(CustID)
            + '&CustTel=' + escape(CustTel) + '&FromType=' + escape(FromType) + '&FromBillID=' + escape(FromBillID)
            + '&Title=' + escape(Title) + '&PayType=' + escape(PayType) + '&Seller=' + escape(Seller)
            + '&SellDeptId=' + escape(SellDeptId) + '&MoneyType=' + escape(MoneyType) + '&TakeType=' + escape(TakeType)
            + '&CarryType=' + escape(CarryType)
            + '&SellType=' + escape(SellType) + '&BusiType=' + escape(BusiType) + '&ExpireDate=' + escape(ExpireDate)
            + '&TotalPrice=' + escape(TotalPrice) + '&TotalTax=' + escape(TotalTax) + '&TotalFee=' + escape(TotalFee)
            + '&Discount=' + escape(Discount) + '&DiscountTotal=' + escape(DiscountTotal)
            + '&RealTotal=' + escape(RealTotal) + '&CountTotal=' + escape(CountTotal) + '&CurrencyType=' + escape(CurrencyType)
            + '&Rate=' + escape(Rate) + '&QuoteTime=' + escape(QuoteTime) + '&OfferDate=' + escape(OfferDate)
            + '&PayRemark=' + escape(PayRemark) + '&DeliverRemark=' + escape(DeliverRemark)
            + '&PackTransit=' + escape(PackTransit) + '&isAddTax=' + escape(isAddTax) 
            + '&CanViewUser=' + escape(CanViewUser) +'&Remark=' + escape(Remark)+GetExtAttrValue();
    return strInfo;

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
           // var UnitPrice = $("#UnitPrice" + rowid).val();   //单价                                     
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

//添加新行
function AddSignRow() { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    var txtTRLastIndex = findObj("txtTRLastIndex", document);
    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m=0;

    var newNameXH = newTR.insertCell(m); //添加列:序号
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
    m++;

    var newFitNotd = newTR.insertCell(m); //添加列:物品编号
    newFitNotd.className = "cell";
    newFitNotd.innerHTML = "<input id='ProductID" + rowID + "' readonly='readonly' style=' width:90%;' type='text'  class='tdinput' />"; //添加列内容
    $("#ProductID" + rowID).bind("click", function() {
        $("#rowIndex").val(rowID);
        var v = 'ProductID' + rowID;
        popTechObj.ShowList(v);
    }); //绑定选择物品事件
    m++;

    var newFitNametd = newTR.insertCell(m); //添加列:物品名称
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<input id='ProductName" + rowID + "'  disabled='disabled'  style=' width:90%; ' type='text'  class='tdinput' />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:规格
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input  id='Specification" + rowID + "'  style=' width:80%;' disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
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
        newFitDesctd.innerHTML = "<input id='ProductCount" + rowID + "' onblur=\"fnTotalInfo()\"   maxlength='10' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
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
        newFitDesctd.innerHTML = "<input id='ProductCount" + rowID + "' onblur=\"fnTotalInfo()\"   maxlength='10' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容
        m++;
    }
//    var newFitDesctd = newTR.insertCell(4); //添加列:单位
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='UnitID" + rowID + "' disabled='disabled' type='text' class='tdinput' style=' width:80%;'/>"; //添加列内容

//    var newFitDesctd = newTR.insertCell(6); //添加列:数量
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='ProductCount" + rowID + "' onblur=\"fnTotalInfo()\"   maxlength='10' type='text'  class='tdinput'  style=' width:90%;'/>"; //添加列内容

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
    newFitDesctd.innerHTML = "<input id='UnitPrice" + rowID + "' onblur=\"fnTotalInfo()\"   maxlength='10' type='text' class='tdinput' style=' width:90%;'/> <input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:含税价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='TaxPrice" + rowID + "' onblur=\"fnTotalInfo()\"  maxlength='10'  type='text' class='tdinput' style=' width:90%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseTaxPrice" + rowID + "'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:折扣(%)
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='Discount" + rowID + "' onblur=\"fnTotalInfo()\"  value='100'  maxlength='7' type='text' class='tdinput' style=' width:90%;'/> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列: 税率(%)
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='TaxRate" + rowID + "' onblur=\"fnTotalInfo()\"   type='text' class='tdinput' style=' width:90%;'/> <input type=\"hidden\" value='0.00' id='hiddTaxRate" + rowID + "'/>"; //添加列内容
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

    txtTRLastIndex.value = rowID; //将行号推进下一行
    //如果是增值税
    if ($("#isAddTax").attr("checked")) {
        $("#TaxRate" + rowID).removeAttr("readonly");
        $("#TaxRate" + rowID).css("background-color", "#FFFFFF");
    }
    //如果不是增值税
    //如果是增值税
    if ($("#NotAddTax").attr("checked")) {
        $("#TaxRate" + rowID).attr("readonly", "readonly");
        $("#TaxRate" + rowID).val(FormatAfterDotNumber(0,precisionLength));
        $("#TaxRate" + rowID).css("background-color", "#FFFFFF");
    }

}


//提交审批成功和审批成功后调用一个函数0:提交审批成功  1:审批成功
function Fun_FlowApply_Operate_Succeed(values) {

    if (values != 2 && values != 3) {
        $("#FromType").removeAttr("disabled");
        $("#btn_save").css("display", "none");
        $("#imgUnSave").css("display", "inline");
        $("#imgAdd").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        $("#GetGoods").css("display", "none");
        $("#UnGetGoods").css("display", "inline");
        $("#imgDel").css("display", "none");
        $("#imgUnDel").css("display", "inline");
        $("#btnZCBJ").css("display", "none");
        $("#btnUnZCBJ").css("display", "inline");

    }
    else {
        $("#imgUnSave").css("display", "none");
        $("#btn_save").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        $("#imgAdd").css("display", "inline");
        $("#imgUnDel").css("display", "none");
        $("#GetGoods").css("display", "inline");
        $("#UnGetGoods").css("display", "none");
        $("#imgDel").css("display", "inline");
        $("#btnZCBJ").css("display", "inline");
        $("#btnUnZCBJ").css("display", "none");
        $("#FromType").attr("disabled", "disabled");
    }
}

//打印功能
function fnPrintOrder() {
    //if (confirm('请确认页面信息是否已保存！')) {
        var OrderNo = $.trim($("#OfferNo").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '') {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存您所填的信息！");
        }
        else {

            window.open('../../../Pages/PrinttingModel/SellManager/SellOfferPrint.aspx?no=' + OrderNo);
        }
   // }
}


//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var CustID = $("#CustID").attr("title");         //客户ID（关联客户信息表）                                                                        
    var FromType = $("#FromType").val();       //源单类型（0无来源，1销售机会）                         
    var FromBillID = $("#FromBillID").val();     //销售机会ID                                             
    //var Title = $.trim($("#Title").val());          //报价主题                                               
    var Seller = $("#hiddSeller").val();        //业务员(对应员工表ID)                                              
    var SellDeptId = $("#hiddDeptID").val();    //部门(对部门表ID)                                                         
    var ExpireDate = $("#ExpireDate").val();     //有效截止日期                                                                                    
    var Discount = $.trim($("#Discount").val());       //整单折扣（%）                                                                                   
    var CurrencyType = $("#CurrencyType").val();   //币种ID

    var OfferDate = $("#OfferDate").val();      //报价日期

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    var TotalPrice = $("#TotalPrice").val();    //金额合计
    var Tax = $("#TotalTax").val();           //税额合计                                                  
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
            msgText = msgText + "请选择销售机会|";
        }
    }

//    if (Title == "") {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//        msgText = msgText + "请输入报价单主题|";
//    }

    if (CustID == "") {
        isFlag = false;
        fieldText = fieldText + "客户名称|";
        msgText = msgText + "请选择客户|";
    }
    if (CurrencyType == "") {
        isFlag = false;
        fieldText = fieldText + "币种|";
        msgText = msgText + "请选择币种|";
    }
    if (Seller == "") {
        isFlag = false;
        fieldText = fieldText + "业务员|";
        msgText = msgText + "请选择业务员|";
    }
    if (SellDeptId == "") {
        isFlag = false;
        fieldText = fieldText + "部门|";
        msgText = msgText + "请选择部门|";
    }

    if (OfferDate == "") {
        isFlag = false;
        fieldText = fieldText + "报价日期|";
        msgText = msgText + "请选择报价日期|";
    }
    if (ExpireDate == "") {
        isFlag = false;
        fieldText = fieldText + "有效截止日期|";
        msgText = msgText + "请选择有效截止日期|";
    }

    if (Discount.length > 0) {
        if (!IsNumeric(Discount, 12, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "整单折扣|";
            msgText = msgText + "整单折扣输入有误，请输入有效的数值！|";
        }
    }
    var PayRemark = $("#PayRemark").val(); //竞争优势
    if (!fnCheckStrLen(PayRemark, 200)) {
        isFlag = false;
        fieldText = fieldText + "付款说明|";
        msgText = msgText + "付款说明最多只允许输入200个字符！|";
    }
    var DeliverRemark = $("#DeliverRemark").val(); //竞争优势
    if (!fnCheckStrLen(DeliverRemark, 200)) {
        isFlag = false;
        fieldText = fieldText + "交付说明|";
        msgText = msgText + "交付说明最多只允许输入200个字符！|";
    }
    var PackTransit = $("#PackTransit").val(); //竞争优势
    if (!fnCheckStrLen(PackTransit, 200)) {
        isFlag = false;
        fieldText = fieldText + "包装运输说明|";
        msgText = msgText + "包装运输说明最多只允许输入200个字符！|";
    }
    var Remark = $("#Remark").val(); //竞争优势
    if (!fnCheckStrLen(Remark, 1024)) {
        isFlag = false;
        fieldText = fieldText + "付款说明|";
        msgText = msgText + "备注最多只允许输入1024个字符！|";
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
            var SendTime = $("#SendTime" + rowid).val();    //发货期限

            var TotalFee = $("#TotalFee" + rowid).val();         //含税金额                        
            var TotalPrice = $("#TotalPrice" + rowid).val();       //金额                            
            var TotalTax = $("#TotalTax" + rowid).val();         //税额

            if (!IsNumeric(TotalFee, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "含税金额超出范围|";
            }

            if (!IsNumeric(TotalPrice, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "金额超出范围|";
            }

            if (!IsNumeric(TotalTax, 22, precisionLength)) {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "税额超出范围|";
            }
            
            if (TaxRate == "") {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "请输入税率！|";
            }
            else {
                if (!IsNumeric(TaxRate, 12, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                    msgText = msgText + "税率输入有误，请输入有效的数值！|";
                }
            }
            if (TaxPrice == "") {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "请输入含税价！|";
            }
            else {
                if (!IsNumeric(TaxPrice, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                    msgText = msgText + "含税价输入有误，请输入有效的数值！|";
                }
            }
            if (Discount == "") {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "请输入折扣！|";
            }
            else {
                if (!IsNumeric(Discount, 12, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                    msgText = msgText + "折扣输入有误，请输入有效的数值！|";
                }
            }

            if (UnitPrice == "") {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "请输入单价！|";
            }
            else {
                if (!IsNumeric(UnitPrice, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                    msgText = msgText + "单价输入有误，请输入有效的数值！|";
                }
            }
            if (ProductCount == "") {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "请输入数量！|";
            }
            else {
                if (!IsNumeric(ProductCount, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                    msgText = msgText + "数量输入有误，请输入有效的数值！|";
                }
            }
            if (SendTime == "") {
                isFlag = false;
                fieldText = fieldText + "交货期限（行号：" + i + "）|";
                msgText = msgText + "请输入交货期限！|";
            }
            else {
                if (!IsNumeric(SendTime, 8, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "交货期限（行号：" + i + "）|";
                    msgText = msgText + "交货期限输入有误，请输入有效的数值！|";
                }
            }
            if (ProductID == "") {
                isFlag = false;
                fieldText = fieldText + "报价单明细（行号：" + i + "）|";
                msgText = msgText + "请选择物品！|";
            }
        }
    }
    if (iCount == 0) {
        isFlag = false;
        fieldText = fieldText + "报价单明细|";
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

//---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
//function Fun_FillParent_Content(ID, ProdNo, ProductName, StandardCost, UnitID, CodeName, TaxRate, SellTax, Discount, Specification)
//根据条码获取的商品信息填充数据
function GetGoodsDataByBarCode(ID,ProdNo,ProductName,StandardSell,UnitID,UnitName,TaxRate,SellTax,Discount,Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost)
{
    if(!IsExist(ID))//如果重复记录，就不增加
    {
       AddSignRow();//插入行
       var txtTRLastIndex = findObj("txtTRLastIndex", document);
       var rowID = parseInt(txtTRLastIndex.value);
       $("#rowIndex").val(rowID);
       //填充数据
       Fun_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification);
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
//---------------------------------------------------条码扫描END------------------------------------------------------------------------------------
//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) 
{
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
