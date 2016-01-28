//=================================== Start  ================================
$(document).ready(function()
{
//新增页面运送方式不可选
    $("#ddlCarryType_ddlCodeType").attr("disabled",true);
    $("#SearchCondition").val(location.search);
    var requestobj = GetRequest(location.search);
    if(requestobj['Page'] == "List")
    {//从列表页面过来的
        $("#imgBack").css("display","inline");      
    }
    var ID = requestobj['ID'];
    var OutSttl = requestobj['OutSttl'];
    if(ID > 0)
    {
        fnFill(ID,OutSttl);
    }
 });

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

//=================================== End   界 面 取 值 ================================

//=================================== Start 界 面 取 值 ================================
//基本信息
function fnGetBaseInfo()
{
    var Info = "";
    var Action=$("#HiddenAction").val();
    if(Action == "Add")
    {//新增状态判断但据是否输入
        var CodeRule = $("#OrderNo_ddlCodeRule").val();
        //手工输入的传编号，自动生成的传生成规则
        if(CodeRule == "")
        {
            var OrderNo = $("#OrderNo_txtCode").val();
            Info += "&OrderNo="+$.trim(OrderNo);
        }
        else
        {
            Info += "&CodeRule="+CodeRule;
        }
    }
    else if(Action == "Update")
    {//此处有些问题，回头处理
        Info += "&OrderNo=" + $("#OrderNo_txtCode").val();        
    }
    Info += "&ID="+$("#ThisID").val();
    Info += "&Title="+escape($.trim($("#txtTitle").val()));
    Info += "&DeptID="+$("#hidDeptID").val();
    Info += "&SendMode="+$("#ddlSendMode").val();
    Info += "&Seller="+$("#hidSellerID").val();
    Info += "&CustName="+escape($.trim($("#txtCustName").val()));
    
    Info += "&CustTel="+escape($.trim($("#txtCustTel").val()));
    Info += "&CustMobile="+escape($.trim($("#txtCustMobile").val()));
    Info += "&CurrencyType="+$("#CurrencyTypeID").val();
    Info += "&Rate="+$("#txtRate").val();
    
    Info += "&OrderMethod="+$("#ddlOrderMethod_ddlCodeType").val();
    Info += "&TakeType="+$("#ddlTakeType_ddlCodeType").val();
    Info += "&PayType="+$("#ddlPayType_ddlCodeType").val();
    Info += "&MoneyType="+$("#ddlMoneyType_ddlCodeType").val();
    Info += "&OrderDate="+$("#txtOrderDate").val();
    var isAddTax = $("#chkisAddTax").attr("checked")?"1":"0";
    Info += "&isAddTax="+isAddTax;
    Info += "&Action="+$("#HiddenAction").val();
    return Info;
}
function fnAddSignRow(table)
{
    var signFrame = findObj(table,document);
    var rowID = signFrame.rows.length;    
    var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
    newTR.id = "DetailSignItem" + rowID; 
    var i = 0;
    var newNameXH=newTR.insertCell(i++);//添加列:选择    
    newNameXH.className="cell";
    newNameXH.innerHTML="<input name='Dtlchk' id='Dtlchk" + rowID + "' value="+rowID+" type='checkbox' size='20'  />";   

    
    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="cell";
    newNameTD.id="SortNo" + rowID; 
    
    var newProductID=newTR.insertCell(i++);//添加列:物品ID
    newProductID.style.display = "none";    
    newProductID.innerHTML = "<input id='ProductID" + rowID + "' type='text' style='width:98%' />";
    
    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="cell";  
    newProductNo.innerHTML = "<input id='ProductNo" + rowID + "' type='text' style='width:98%' onclick=\"GetProduct("+rowID+")\" />"; 
    
    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="cell";
    newProductName.innerHTML = "<input id='ProductName" + rowID + "' type='text'  style='width:98%' />";
    $("#ProductName"+rowID).attr("disabled",true);
    
    var newSpecification=newTR.insertCell(i++);//添加列:规格
    newSpecification.className="cell";
    newSpecification.innerHTML = "<input id='Specification" + rowID + "' type='text'  style='width:98%;'/>";    
    $("#Specification"+rowID).attr("disabled",true);
    
    var newStorage=newTR.insertCell(i++);//添加列:仓库ID
    newStorage.className="cell";
    newStorage.innerHTML = "<select class='tdinput' id='StorageID" + rowID + "'>" + document.getElementById("StorageHid").innerHTML + "</select>";
    if($("#ddlSendMode").val() == "1")
    {//分店发货仓库不可选
        $("#StorageID"+rowID).attr("disabled",true);
    }
       
    var newProductCount=newTR.insertCell(i++);//添加列:采购数量
    newProductCount.className="cell";
    newProductCount.innerHTML = "<input id='ProductCount" + rowID + "' type='text'  onblur=\"Number_round(this,2);fnTotal(1);\" style='width:98%;'/>";
    
    var newProductCountTH=newTR.insertCell(i++);//添加列:采购数量
    newProductCountTH.className="cell";
    newProductCountTH.innerHTML = "<input id='ProductCountTH" + rowID + "' type='text' disabled=\"disabled\" style='width:98%;'/>";
    
    
    var newUnitID=newTR.insertCell(i++);//添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input id='UnitID" + rowID + "' type='hidden' style='width:98%;'/>";
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input  id='UnitName" + rowID + "'type='text'  style='width:98%'   />";
    $("#UnitName"+rowID).attr("disabled",true);
    
    var newUnitPrice=newTR.insertCell(i++);//添加列:单价
    newUnitPrice.className="cell";
    newUnitPrice.innerHTML = "<input id='UnitPrice" + rowID + "'type='text'  style='width:98%' disabled='disabled'  />";
    
    var newTaxPric=newTR.insertCell(i++);//添加列:含税价
    newTaxPric.className="cell";
    newTaxPric.innerHTML = "<input id='TaxPrice" + rowID + "'type='text' disabled='disabled'   style='width:98%'   />";
    
    
    var newTaxPricHide=newTR.insertCell(i++);//添加列:含税价隐藏
    newTaxPricHide.style.display = "none";
    newTaxPricHide.innerHTML = "<input id='TaxPriceHide" + rowID + "'type='text'  style='width:98%'   />";
    
    var newDiscount=newTR.insertCell(i++);//添加列:折扣
    newDiscount.className="cell";
    newDiscount.innerHTML = "<input id='Discount" + rowID + "'type='text' disabled='disabled'   style='width:98%'   />";
    
    var newTaxRate=newTR.insertCell(i++);//添加列:税率
    newTaxRate.className="cell";
    newTaxRate.innerHTML = "<input id='TaxRate" + rowID + "'type='text' disabled='disabled'  style='width:98%'   />";
//    $("#TaxRate"+rowID).attr("disabled",true);
    
    var newTaxRateHide=newTR.insertCell(i++);//添加列:税率隐藏
    newTaxRateHide.style.display = "none";
    newTaxRateHide.innerHTML = "<input id='TaxRateHide" + rowID + "'type='text'  style='width:98%'   />";
    
    var newTotalPrice=newTR.insertCell(i++);//添加列:金额
    newTotalPrice.className="cell";
    newTotalPrice.innerHTML = "<input id='TotalPrice" + rowID + "'type='text'  style='width:98%'  readonly />";
    $("#TotalPrice"+rowID).attr("disabled",true);
    
    var newTotalFee=newTR.insertCell(i++);//添加列:含税金额
    newTotalFee.className="cell";
    newTotalFee.innerHTML = "<input id='TotalFee" + rowID + "'type='text'  style='width:98%' readonly  />";
    $("#TotalFee"+rowID).attr("disabled",true);
    
    var newTotalTax=newTR.insertCell(i++);//添加列:税额
    newTotalTax.className="cell";
    newTotalTax.innerHTML = "<input id='TotalTax" + rowID + "'type='text'  style='width:98%'  readonly />";
    $("#TotalTax"+rowID).attr("disabled",true);
    
    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.className="cell";
    newRemark.innerHTML = "<input id='Remark" + rowID + "'type='text'  style='width:98%'   />";
    fnGenerateNo(table);
    return rowID;
}
//生成序号,参数：操作的表格的id
function fnGenerateNo(table)
{
    var signFrame = findObj(table,document);
    var Edge = signFrame.rows.length;
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
//发货信息
function fnGetSendInfo()
{
    var Info = "";
    if($("#txtPlanOutDate").val() != "")
    {
        Info += "&PlanOutDate="+$("#txtPlanOutDate").val()+" "+$("#ddlPlanOutHour").val()+":"+$("#ddlPlanOutMin").val();
    }
    else
    {
        Info += "&PlanOutDate=";
    }
    if($("#txtOutDate").val() != "")
    {
        Info += "&OutDate="+$("#txtOutDate").val()+" "+$("#ddlOutHour").val()+":"+$("#ddlOutMin").val();
    }
    else
    {
        Info += "&OutDate=";
    }
    Info += "&CarryType="+$("#ddlCarryType_ddlCodeType").val();
    Info += "&BusiStatus="+$("#hidBusiStatus").val();
    Info += "&OutDeptID="+$("#hidOutDeptID").val();
    Info += "&OutUserID="+$("#hidOutUserID").val();
    Info += "&CustAddr="+escape($("#txtCustAddr").val());
    return Info;
}
//安装信息
function fnGetSetupInfo()
{
    var Info = "";
    var NeedSetup = $("#chkNeedSetup").attr("checked")?"1":"0";
    Info += "&NeedSetup="+ NeedSetup;
    if($("#txtPlanSetDate").val() != "")
    {
        Info += "&PlanSetDate="+$("#txtPlanSetDate").val()+" "+$("#ddlPlanSetHour").val()+":"+$("#ddlPlanSetMin").val();
    }
    else
    {
        Info += "&PlanSetDate=";
    }
    if($("#txtSetDate").val() != "")
    {
        Info += "&SetDate="+$("#txtSetDate").val()+" "+$("#ddlSetHour").val()+":"+$("#ddlSetMin").val();
    }
    else
    {
        Info += "&SetDate=";
    }
    Info += "&SetUserID="+$("#hidSetUserID").val();
    return Info;
}
//合计信息
function fnGetTotalInfo()
{
    var Info = "";
    Info += "&CountTotal="+$("#txtCountTotal").val();
    Info += "&TotalPrice="+$("#txtTotalPrice").val();
    Info += "&TotalTax="+$("#txtTotalTax").val();
    Info += "&TotalFee="+$("#txtTotalFee").val();
    Info += "&Discount="+$("#txtDiscount").val();
    
    Info += "&DiscountTotal="+$("#txtDiscountTotal").val();
    Info += "&RealTotal="+$("#txtRealTotal").val();
    var ThisPayedTotal=0;
    if($("#txtThisPayed").val() != "")
    {
        ThisPayedTotal = parseFloat($("#txtThisPayed").val());
    }
    var PayedTotal = FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val())+ThisPayedTotal,2);
    Info += "&PayedTotal="+PayedTotal;
    var WairPayTotal = FormatAfterDotNumber(parseFloat($("#txtWairPayTotal").val())-ThisPayedTotal,2);
    Info += "&WairPayTotal="+WairPayTotal;
    return Info;
}
//备注信息
function fnGetRemarkInfo()
{
    var Info = "";
    Info += "&Creator="+$("#txtCreatorID").val();
    Info += "&CreateDate="+$("#txtCreateDate").val();
    Info += "&BillStatus="+$("#txtBillStatusID").val();
    Info += "&Confirmor="+$("#txtConfirmorID").val();
    Info += "&ConfirmorDate="+$("#txtConfirmorDate").val();
    
    Info += "&Closer="+$("#txtCloserID").val();
    Info += "&CloseDate="+$("#txtCloseDate").val();
    Info += "&ModifiedUserID="+$("#txtModifiedUserID").val();
    Info += "&ModifiedDate="+$("#txtModifiedDate").val();
    Info += "&Remark="+escape($("#txtRemark").val());
    Info += "&Attachment="+escape($("#hfPageAttachment").val().replace(/\\/g,"\\\\"));
    return Info;
}
//结算信息
function fnGetSettInfo()
{
    var Info = "";
    var isOpenBill = $("#chkisOpenBill").checked?"1":"0";
    Info += "&isOpenBill="+isOpenBill;
}
//明细信息
function fnGetDetailInfo()
{   
    var signFrame = findObj("DetailTable",document);
    var Edge = signFrame.rows.length;
    var Info = "";
    var Index = 0;
    for(var i=1;i<Edge;++i)
    {
        if(signFrame.rows[i].style.display != "none")
        {
            Info += "&SortNo"+Index+"="+$("#SortNo"+i).html();
            
            Info += "&ProductID"+Index+"="+$("#ProductID"+i).val();
            Info += "&StorageID"+Index+"="+$("#StorageID"+i).val();
            Info += "&ProductCount"+Index+"="+$("#ProductCount"+i).val();
            Info += "&UnitID"+Index+"="+$("#UnitID"+i).val();
            Info += "&UnitPrice"+Index+"="+$("#UnitPrice"+i).val();
            
            Info += "&TaxPrice"+Index+"="+$("#TaxPrice"+i).val();
            Info += "&Discount"+Index+"="+$("#Discount"+i).val();
            Info += "&TaxRate"+Index+"="+$("#TaxRate"+i).val();
            Info += "&TotalFee"+Index+"="+$("#TotalFee"+i).val();
            Info += "&TotalPrice"+Index+"="+$("#TotalPrice"+i).val();
            
            Info += "&TotalTax"+Index+"="+$("#TotalTax"+i).val();
            Info += "&Remark"+Index+"="+escape($("#Remark"+i).val());
            
            ++Index;
        }        
    }    
    Info += "&length="+Index;
    return Info;
}
//=================================== End   界 面 取 值 ================================




//=================================== Start 保      存 ================================

//=================================== End   保      存 ================================


//=================================== Start 确      认 ================================



//=================================== Start 校      验 ================================

//=================================== End   校      验 ================================





//=================================== Start 合      计 ================================
//Flag为点击的是否是增值税，若是则为1，不是为0
//点击的是增值税则将含税价，税率的隐藏域的值赋给显示域，否则将显示域的值赋给隐藏域
function fnTotal(Flag)
{
var isAddTax = document.getElementById("chkisAddTax").checked;
if(Flag == 1)
{
    if(isAddTax)
    {
        document.getElementById("AddTax").innerHTML = "是增值税";
    }
    else
    {
        document.getElementById("AddTax").innerHTML = "非增值税";
    }
}
    var signFrame = findObj("DetailTable",document);
    var RowCount = signFrame.rows.length;
    var CountTotal = 0;//数量合计
    var TotalPrice = 0;//金额合计
    var Tax = 0;       //税额合计
    var TotalFee = 0;  //含税金额合计
    var DiscountTotal = 0; //折扣金额
    var RealTotal = 0;     //折后含税金额
    
    for(var i=1;i<RowCount;++i)
    {
        if(signFrame.rows[i].style.display != "none")
        {
            //数量
//            $("#ProductCount"+i).val(FormatAfterDotNumber($("#ProductCount"+i).val(),2));
            if(!IsNumberOrNumeric($("#ProductCount"+i).val(),12,2))
            {   
                $("#ProductCount"+i).val("0.00");
            }
            else
            {
                $("#ProductCount"+i).val(FormatAfterDotNumber($("#ProductCount"+i).val(),2));
            }
            //单价
//            $("#UnitPrice"+i).val(FormatAfterDotNumber($("#UnitPrice"+i).val(),2));
            if(!IsNumberOrNumeric($("#UnitPrice"+i).val(),12,2))
            {   
                $("#UnitPrice"+i).val("0.00");
            }
            if(Flag != 0)
            {//不是点击增值税激发的            
                if(isAddTax == true)
                {
                    //含税价
//                    $("#TaxPrice"+i).val(FormatAfterDotNumber($("#TaxPrice"+i).val(),2));
                    if(!IsNumberOrNumeric($("#TaxPrice"+i).val(),12,2))
                    {   
                        $("#TaxPrice"+i).val("0.00");
                    }
                    //隐藏的含税价
                    $("#TaxPriceHide"+i).val($("#TaxPrice"+i).val());
                    
                    //税率
//                    $("#TaxRate"+i).val(FormatAfterDotNumber($("#TaxRate"+i).val(),2));
                    if(!IsNumberOrNumeric($("#TaxRate"+i).val(),12,2))
                    {   
                        $("#TaxRate"+i).val("0.00");
                    }
                    $("#TaxRateHide"+i).val($("#TaxRate"+i).val());
                }
                else if(isAddTax == false)
                {
                    $("#TaxPrice"+i).val("0.00");
                    $("#TaxRate"+i).val("0.00");                        
                }
            }
            else
            {//点击增值税激发的，将隐藏域中的值赋给显示域
                if(isAddTax == true)
                {
                    
                    $("#TaxPrice"+i).val($("#TaxPriceHide"+i).val());
                    $("#TaxRate"+i).val($("#TaxRateHide"+i).val());
                }
                else
                {
                    $("#TaxPrice"+i).val("0.00");
                    $("#TaxRate"+i).val("0.00");
                }
            }
            //折扣
//            $("#Discount"+i).val(FormatAfterDotNumber($("#Discount"+i).val(),2));
            if(!IsNumberOrNumeric($("#Discount"+i).val(),12,2))
            {   
                $("#Discount"+i).val(100);
            }
            $("#TotalPrice"+i).val(FormatAfterDotNumber($("#ProductCount"+i).val()*$("#UnitPrice"+i).val()*$("#Discount"+i).val()/100,2));
            $("#TotalFee"+i).val(FormatAfterDotNumber($("#ProductCount"+i).val()*$("#TaxPrice"+i).val()*$("#Discount"+i).val()/100,2));
            $("#TotalTax"+i).val(FormatAfterDotNumber($("#TotalPrice"+i).val()*$("#TaxRate"+i).val()/100,2));
            
            CountTotal  = parseFloat(CountTotal) + parseFloat($("#ProductCount"+i).val());
            TotalPrice  = parseFloat(TotalPrice) + parseFloat($("#TotalPrice"+i).val());
            Tax = parseFloat(Tax) + parseFloat($("#TotalTax"+i).val());
            TotalFee = parseFloat(TotalFee) + parseFloat($("#TotalFee"+i).val());
            DiscountTotal = parseFloat(DiscountTotal) + parseFloat($("#ProductCount"+i).val()*$("#TaxPrice"+i).val()*(100 - $("#Discount"+i).val())/100);
//            RealTotal = parseFloat(RealTotal) + parseFloat($("#ProductCount"+i).val()*$("#UnitPrice"+i).val()*$("#Discount"+i).val()/100);
        }
    }
    DiscountTotal += TotalFee * (100 - $("#txtDiscount").val())/100;
    RealTotal = TotalFee * $("#txtDiscount").val()/100;
    $("#txtCountTotal").val(FormatAfterDotNumber(CountTotal,2));
    $("#txtTotalPrice").val(FormatAfterDotNumber(TotalPrice,2));
    $("#txtTotalTax").val(FormatAfterDotNumber(Tax,2));
    $("#txtTotalFee").val(FormatAfterDotNumber(TotalFee,2));
    $("#txtDiscountTotal").val(FormatAfterDotNumber(DiscountTotal,2));
    $("#txtRealTotal").val(FormatAfterDotNumber(RealTotal,2));
    $("#txtWairPayTotal").val(FormatAfterDotNumber(RealTotal-parseFloat($("#txtPayedTotal").val()),2));
//    if(!IsNumberOrNumeric($("#txtThisPayed").val(),12,2))
//    {        
//        $("#txtThisPayed").val($("#txtThisPayedHid").val());
//    }
//    $("#txtPayedTotal").val(FormatAfterDotNumber(parseFloat($("#txtPayedTotal").val())+parseFloat($("#txtThisPayed").val()-parseFloat($("#txtThisPayedHid").val())),2));
//    var WairPayTotal = $("#txtRealTotal").val()-$("#txtPayedTotal").val()
//    $("#txtWairPayTotal").val(FormatAfterDotNumber(WairPayTotal,2));
////    $("#txtThisPayedHid").val($("#txtThisPayed").val());
//    $("#txtThisPayedHid").val($("#txtThisPayed").val());
//    $("#txtThisPayed").val(0);
}

//=================================== End   合      计 ================================



//=================================== Start 返      回 ================================
function fnBack()
{
    var URLParams = $("#SearchCondition").val();
    URLParams = URLParams.replace("Page=List","Page=Add");
    URLParams = URLParams.replace("ModuleID=33333333","ModuleID=3121201");
    window.location.href = 'SubSellOrderList.aspx'+URLParams;
}




function fnFillCustInfo(CustName,CustTel,CustMobile,CustAddr)
{
    $("#txtCustName").val(CustName);
    $("#txtCustTel").val(CustTel);
    $("#txtCustMobile").val(CustMobile);
    $("#txtCustAddr").val(CustAddr);
    fnCloseCust();
}

//=================================== End   客户弹出层处理 ================================

//=================================== Start   列表页面进入填充 ================================
function fnFill(ID,OutSttl)
{
    var URLParams = "Action=Fill&ID="+ID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubSellOrderAdd.ashx",
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
            $.each(msg.SubSellOrderPri,function(i,item)
            {
                if(item.ID!= null && item.ID != "")
                {
                    fnFillSubSellOrder(item);
                }
            });
            $.each(msg.SubSellOrderDetail,function(i,item)
            {
                if(item.ID!= null && item.ID != "")
                {
                    fnFillSubSellOrderDetail(item);
                }
            });
            fnStatus();
//            if(OutSttl == "OutSttl")
//            {//点击出库或结算
                if($("#txtBillStatusID").val() == "1")
                {
                    document.getElementById("divTitle").innerHTML = "销售订单";
                }
                if(($("#txtBillStatusID").val() == "2")&&($("#hidBusiStatus").val() == "2"))
                {//销售发货时界面上除了发货信息可改，其他信息不可改
                    document.getElementById("divTitle").innerHTML = "销售订单";
                    fnStatusOut();            
                }
                else if(($("#txtBillStatusID").val() == "2")&&($("#hidBusiStatus").val() == "3"))
                {//结算
                    document.getElementById("divTitle").innerHTML = "销售订单";
                    fnStatusAfterOut();
                }
                else if($("#hidBusiStatus").val() == "4")
                {
                    document.getElementById("divTitle").innerHTML = "销售订单";
                    //更换标题
                    $("#AddTitle").css("display","none");
                    $("#UpdateTitle").css("display","none");
                    $("#OutTitle").css("display","none");
                    $("#SettTitle").css("display","inline");
                    Disableinput();
              
                }
//            }
//            else
//            {//点击编号
//                fnStatusFromBill();
//            }
        },   
        complete:function()
        {
            hidePopup(); 
        }//接收数据完毕
    });
    
}
function Disableinput()
{
      
                    //按钮控制
                    //所有text不可用
                    $(":text").each(function(){ 
                        this.disabled=true; 
                    });
                    //所有checkbox不可用
                    $(":checkbox").each(function(){ 
                        this.disabled=true; 
                    });
                    
                    //下拉列表不可用
                    $("#ddlSendMode").attr("disabled",true);
                    $("#ddlCurrencyType").attr("disabled",true);
                    $("#ddlMoneyType_ddlCodeType").attr("disabled",true);
                    $("#ddlTakeType_ddlCodeType").attr("disabled",true);
                    $("#ddlPayType_ddlCodeType").attr("disabled",true);
                    $("#ddlOrderMethod_ddlCodeType").attr("disabled",true);
                    $("#ddlMoneyType_ddlCodeType").attr("disabled",true);
                    $("#ddlPlanSetHour").attr("disabled",true);
                    $("#ddlPlanSetMin").attr("disabled",true);
                    $("#ddlSetHour").attr("disabled",true);
                    $("#ddlSetMin").attr("disabled",true);
                    
                    //发货信息不可用
                    $("#txtPlanOutDate").attr("disabled",true);
                    $("#ddlPlanOutHour").attr("disabled",true);
                    $("#ddlPlanOutMin").attr("disabled",true);
                    $("#ddlCarryType_ddlCodeType").attr("disabled",true);
                    $("#DeptOutDeptName").attr("disabled",true);
                    
                    $("#txtOutDate").attr("disabled",true);
                    $("#ddlOutHour").attr("disabled",true);
                    $("#ddlOutMin").attr("disabled",true);
                    $("#UserOutUserName").attr("disabled",true);
                    $("#txtCustAddr").attr("disabled",true);
                    
                    //安装信息不可用
                    $("#chkNeedSetup").attr("disabled",true);
                    fnChangeNeedSetup();
                    
                    
                    $("#ColCust").css("display","none");
                    $("#ColCust2").css("display","inline");
}
//填充基本信息
function fnFillBaseInfo(item)
{
    $("#HiddenAction").val("Update");

    //基本信息
    $("#AddTitle").css("display","none");
    $("#UpdateTitle").css("display","inline");
    
    document.getElementById('OrderNo_txtCode').className='tdinput';
    document.getElementById('OrderNo_txtCode').style.width='90%';
    $("#OrderNo_txtCode").attr("disabled",true);
    $('#OrderNo_ddlCodeRule').css("display","none");
    $("#OrderNo_txtCode").val(item.OrderNo);
    $("#ThisID").val(item.ID);
    $("#txtTitle").val(item.Title);
    $("#hidDeptID").val(item.DeptID);
    $("#txtDeptName").val(item.DeptName);
    $("#ddlSendMode").val(item.SendMode);
    $("#hidSellerID").val(item.Seller);
    $("#UserSellerName").val(item.SellerName);
    $("#txtCustName").val(item.CustName);
    
    $("#txtCustTel").val(item.CustTel);
    $("#txtCustMobile").val(item.CustMobile);
    $("#txtCustAddr").val(item.CustAddr);
    $("#CurrencyTypeID").val(item.CurrencyType);
    $("#txtRate").val(item.Rate);
    
    $("#ddlOrderMethod_ddlCodeType").val(item.OrderMethod);
    $("#ddlTakeType_ddlCodeType").val(item.TakeType);
    $("#ddlPayType_ddlCodeType").val(item.PayType);
    $("#ddlMoneyType_ddlCodeType").val(item.MoneyType);
    $("#txtOrderDate").val(item.OrderDate);
    $("#chkisAddTax").checked = item.isAddTax == "1"?true:false;
}

//填充发货信息
function fnFillSendInfo(item)
{
    if(item.PlanOutDate != "")
    {
        $("#txtPlanOutDate").val(item.PlanOutDate.split(' ')[0]);
        $("#ddlPlanOutHour").val(item.PlanOutDate.split(' ')[1].split(':')[0]);
        $("#ddlPlanOutMin").val(item.PlanOutDate.split(' ')[1].split(':')[1]);  
    }
    if(item.OutDate != "")
    {  
        $("#txtOutDate").val(item.OutDate.split(' ')[0])
        $("#ddlOutHour").val(item.OutDate.split(' ')[1].split(':')[0])
        $("#ddlOutMin").val(item.OutDate.split(' ')[1].split(':')[1]);
    }
    $("#ddlCarryType_ddlCodeType").val(item.CarryType);
    $("#hidBusiStatus").val(item.BusiStatus);
    $("#txtBusiStatus").val(item.BusiStatusName);
    $("#hidOutDeptID").val(item.OutDeptID);
    $("#DeptOutDeptName").val(item.OutDeptName);
    $("#hidOutUserID").val(item.OutUserID);
    $("#UserOutUserName").val(item.OutUserName);
}
//填充安装信息
function fnFillSetupInfo(item)
{
    $("#chkNeedSetup").checked = item.NeedSetup == "1"?true:false;
    if(item.PlanSetDate != "")
    {
        $("#txtPlanSetDate").val(item.PlanSetDate.split(' ')[0])
        $("#ddlPlanSetHour").val(item.PlanSetDate.split(' ')[1].split(':')[0])
        $("#ddlPlanSetMin").val(item.PlanSetDate.split(' ')[1].split(':')[1]);
    }
    if(item.SetDate != "")
    {
        $("#txtSetDate").val(item.SetDate.split(' ')[0])
        $("#ddlSetHour").val(item.SetDate.split(' ')[1].split(':')[0])
        $("#ddlSetMin").val(item.SetDate.split(' ')[1].split(':')[1]);
    }
    $("#hidSetUserID").val(item.SetUserID);
    $("#UserOutUserName").val(item.SetUserName);    
}

//填充合计信息
function fnFillTotalInfo(item)
{
    $("#txtCountTotal").val(FormatAfterDotNumber(item.CountTotal,2));
    $("#txtTotalPrice").val(FormatAfterDotNumber(item.TotalPrice,2));
    $("#txtTotalTax").val(FormatAfterDotNumber(item.Tax,2));
    $("#txtTotalFee").val(FormatAfterDotNumber(item.TotalFee,2));
    $("#txtDiscount").val(FormatAfterDotNumber(item.Discount,2));

    $("#txtDiscountTotal").val(FormatAfterDotNumber(item.DiscountTotal,2));
    $("#txtRealTotal").val(FormatAfterDotNumber(item.RealTotal,2));
    $("#txtPayedTotal").val(FormatAfterDotNumber(item.PayedTotal,2));
    $("#txtWairPayTotal").val(FormatAfterDotNumber(item.WairPayTotal,2));
}

//填充备注信息
function fnFillRemarkInfo(item)
{
    $("#txtCreatorID").val(item.Creator);
    $("#txtCreatorName").val(item.CreatorName);
    $("#txtCreateDate").val(item.CreateDate);
    $("#txtBillStatusID").val(item.BillStatus);
    $("#txtBillStatusName").val(item.BillStatusName);
    $("#txtConfirmorID").val(item.Confirmor);
    $("#txtConfirmorName").val(item.ConfirmorName);
    $("#txtConfirmorDate").val(item.ConfirmDate);
    
    $("#txtCloserID").val(item.Closer);
    $("#txtCloserName").val(item.CloserName);
    $("#txtCloseDate").val(item.CloseDate);
    $("#txtModifiedUserID").val(item.ModifiedUserID);
    $("#txtModifiedUserName").val(item.ModifiedUserID);
    $("#txtModifiedDate").val(item.ModifiedDate);
    $("#txtRemark").val(item.Remark);
    $("#hfPageAttachment").val(item.Attachment);
    
    if(item.Attachment!="")
    {
        //下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        //上传不显示
        document.getElementById("divUploadResume").style.display = "none"
    }
}
//填充结算信息
function fnFillSttlInfo(item)
{
    //结算信息可见
    $("#Sttl").css("display","inline");
    
    var isOpenBill = item.isOpenbill == "1"?true:false;
    $("#chkisOpenbill").attr("checked",isOpenBill);
    fnChangeOpenBill();
    $("#SttlUser").val(item.SttlUserName);
    $("#SttlUserID").val(item.SttlUserID);
    $("#SettDate").val(item.SettDate);
    
}
//填充主表
function fnFillSubSellOrder(item)
{
    fnFillBaseInfo(item);
    fnFillSendInfo(item);
    fnFillSetupInfo(item);
    fnFillTotalInfo(item);
    fnFillRemarkInfo(item)
}

//填充明细
function fnFillSubSellOrderDetail(item)
{
    var i = fnAddSignRow('DetailTable');
    $("#ProductID"+i).val(item.ProductID);
    $("#ProductNo"+i).val(item.ProductNo);
    $("#ProductName"+i).val(item.ProductName);
    $("#StorageID"+i).val(item.StorageID);
    $("#ProductCount"+i).val(FormatAfterDotNumber(item.ProductCount,2));
    $("#ProductCountTH"+i).val(FormatAfterDotNumber(item.BackCount,2));
    $("#UnitID"+i).val(item.UnitID);
    $("#UnitName"+i).val(item.UnitName);
    $("#UnitPrice"+i).val(FormatAfterDotNumber(item.UnitPrice,2));
    
    $("#TaxPrice"+i).val(FormatAfterDotNumber(item.TaxPrice,2));
    $("#TaxPriceHide"+i).val($("#TaxPrice"+i).val());
    $("#Discount"+i).val(FormatAfterDotNumber(item.Discount,2));
    $("#TaxRate"+i).val(FormatAfterDotNumber(item.TaxRate,2));
    $("#TaxRateHide"+i).val($("#TaxRate"+i).val());
    $("#TotalFee"+i).val(FormatAfterDotNumber(item.TotalFee,2));
    $("#TotalPrice"+i).val(FormatAfterDotNumber(item.TotalPrice,2));
    
    $("#TotalTax"+i).val(FormatAfterDotNumber(item.TotalTax,2));
    $("#Remark"+i).val(item.Remark);
}



//点击出库链接过来时，页面显示的状态
function fnStatusOut()
{
    //更换标题
    document.getElementById("divTitle").innerHTML = "销售订单";
//    $("#AddTitle").css("display","none");
//    $("#UpdateTitle").css("display","none");
//    $("#OutTitle").css("display","inline");
//    $("#SettTitle").css("display","none");
    //所有text不可用
 Disableinput();
    
    $("#ColCust").css("display","none");
    $("#ColCust2").css("display","inline");
}

function fnStatusAfterOut()
{//销售发货确认后界面的状态
    //更换标题
    //所有text不可用
    $(":text").each(function(){ 
        this.disabled=true; 
    });
    //所有checkbox不可用
    $(":checkbox").each(function(){ 
        this.disabled=true; 
    });
    
  Disableinput();
    
    $("#ColCust").css("display","none");
    $("#ColCust2").css("display","inline");
}

//结算时
function fnStatusSett()
{
    //更换标题
    document.getElementById("divTitle").innerHTML = "销售订单";
//    $("#AddTitle").css("display","none");
//    $("#UpdateTitle").css("display","none");
//    $("#OutTitle").css("display","none");
//    $("#SettTitle").css("display","inline");
    
   Disableinput();
    
    $("#ColCust").css("display","none");
    $("#ColCust2").css("display","inline");
}


//=================================== End     列表页面进入填充 ================================

//=================================== Start   判断按钮状态 ================================
//从单据编号链接过来时按钮状态
//function fnStatusFromBill()
//{
//    var BillStatus = $("#txtBillStatusID").val()//单据状态
//    var BusiStatus = $("#hidBusiStatus").val()//业务状态
//    alert(BusiStatus)
//    if(BillStatus=="1")
//    {//制单状态
//        //更换标题
//        document.getElementById("divTitle").innerHTML = "销售订单";
//    }
//    else if(BillStatus == "2")
//    {
//        if(BusiStatus == "2")
//        {//出库
//            //更换标题
//            document.getElementById("divTitle").innerHTML = "销售订单";

//        }
//        else if(BusiStatus == "3")
//        {//结算
//            //更换标题
//            document.getElementById("divTitle").innerHTML = "销售订单";
//        }
//    }
//        if(BusiStatus == "4")
//        {
//            //更换标题
//            document.getElementById("divTitle").innerHTML = "销售订单--完成";
//            //所有text不可用
//Disableinput();
//            fnChangeNeedSetup();
//            
//            
//            $("#ColCust").css("display","none");
//            $("#ColCust2").css("display","inline");
//        }
//}
//=================================== End     判断按钮状态 ================================

//是否需要安装
function fnChangeNeedSetup()
{
//    var isNeedSetup = $("#chkNeedSetup").attr("checked");
    var isNeedSetup = document.getElementById("chkNeedSetup").checked;
    if(false == isNeedSetup)
    {
        $("#NeedSetup").html("不需要安装");
        //不需要安装，将安装信息清空
        $("#txtPlanSetDate").val("");
        $("#ddlPlanSetHour").val("00");
        $("#ddlPlanSetMin").val("00");
        $("#txtSetDate").val("");
        $("#ddlSetHour").val("00");
        $("#ddlSetMin").val("00");
        $("#UserSetUserID").val("");
        //安装信息不可用
    }
    else if(true == isNeedSetup)
    {
        $("#NeedSetup").html("需要安装");
    }
}




function fnStatus()
{
    var BillStatus = $("#txtBillStatusID").val();
    var BusiStatus = $("#hidBusiStatus").val();
    var Status = $("#HiddenAction").val();
     Disableinput();
}




