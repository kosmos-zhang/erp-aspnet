var ModuleID="";
$(document).ready(function() 
{
    requestobj = GetRequest();
    ModuleID=requestobj['ModuleID'];
    LoadBillType();
    TurnToPage(1);
});

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式

var currentPageIndex = 1;
var orderBy = ""; //排序字段
var isShow="0";
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) 
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
     var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
     if(!isFlag)
        {
            popMsgObj.Show(fieldText,msgText);
            return;
        }
    currentPageIndex = pageIndex;
    $("#checkall").removeAttr("checked")
    var EFDesc = $.trim($("#EFDesc").val());
    var ModelNo = $("#ModelNo").val();
    
    var FormType=$.trim($("#FormType").val());//源单类型
    var FunctionModule=$("#FunctionModule").val();//功能模块
    
    var action = "list";
    var strUrl = 'EFDesc=' + escape(EFDesc) + '&ModelNo=' + escape(ModelNo) + '&FormType=' + escape(FormType) + '&FunctionModule=' + escape(FunctionModule) + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy) +
               '&action=' + escape(action);
    document.getElementById("txtSearchPara").value=strUrl;
    $("#hiddUrl").val(strUrl);

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx", //目标地址
        cache: false,
        data: strUrl,

        beforeSend: function() { AddPop();
        $("#pageDataList1_Pager").hide(); }, //发送数据之前
        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ModelNo != null && item.ModelNo != "") {
                    $("<tr class='newrow'></tr>").append(
                    "<td height='22' align='center'>" + "<input id='chk" + i + "'  value='" + item.ModelNo + "' onclick = 'fnUnSelect(this)'   type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'>" + item.FunctionModule + "</td>" +
//                    "<td height='22' align='center'><a href='#' onclick=TurnToEditPage('" + item.ModelNo + "')>" + item.ModelNo + "</a></td>" +
                    "<td height='22' align='center'><a href='#' onclick=TurnToEditPage('" + item.ModelNo + "')>" + item.TabName + "</a></td>"
                    ).appendTo($("#pageDataList1 tbody"));
                }
            });

            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", 
                    preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            $("#Text2").val(msg.totalCount);
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误");
        },
        complete: function() { if(isShow!="1"){hidePopup();
        } 
        $("#pageDataList1_Pager").show(); Ifshow($("#Text2").val()); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}
function TurnToEditPage(ModelNo)
{
    window.location.href='AddExtAttribute.aspx?ModuleID='+escape(ModuleID)+'&ModelNo='+escape(ModelNo);
}
//更新打开窗口
function fnUpdate(ID, EFDesc, EFIndex, EFType, EFValueList) {
    $("#hiddAction").val('update');
    openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
    document.getElementById("div_Add").style.zIndex = "2";
    document.getElementById("divBackShadow").style.zIndex = "1";
    document.getElementById('div_Add').style.display = 'block';
    $("#hiddID").val(ID);
    $("#hiddEFIndex").val(EFIndex);
    $("#EFDescUC").val(EFDesc);
    $("#EFTypeUC").val(EFType);
    $("#EFValueListUC").val(EFValueList);
    fnChange();
}


//去除全选按钮
function fnUnSelect(obj) {

    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }

    else {
        //验证明细信息
        var signFrame = findObj("pageDataList1", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 0; i < signFrame.rows.length - 1; i++) {

            iCount = iCount + 1;

            if ($("#chk" + i).attr("checked")) {
                checkCount = checkCount + 1;
            }

        }
        if (checkCount == iCount) {

            $("#checkall").attr("checked", "checked");
        }

    }
}

//全选
function selectall() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//table行颜色
function pageDataList1(o, a, b, c, d) {
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


function Ifshow(count) {
    if (count == "0") {
        document.getElementById('divpage').style.display = "none";
        document.getElementById('pagecount').style.display = "none";
    }
    else {
        document.getElementById('divpage').style.display = "block";
        document.getElementById('pagecount').style.display = "block";
    }
}


//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    $("#checkall").removeAttr("checked")
    
    if (!IsNumber(newPageCount) || newPageCount == 0) {
        
        isFlag = false;
        fieldText = fieldText + "每页显示|";
   		msgText = msgText + "每页显示必须是正整数|";
    }
    if (!IsNumber(newPageIndex) || newPageIndex == 0) 
    {
        isFlag = false;
        fieldText = fieldText + "转到第|";
   		msgText = msgText + "转到第几页必须是正整数|";
    }
    if (!isFlag) {
       popMsgObj.Show(fieldText,msgText);
    }
    else {
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
            {
            fieldText = fieldText + "转到第|";
   		    msgText = msgText + "转到页数超出查询范围|";
            popMsgObj.Show(fieldText,msgText);
            return false;
            }
        }
        else {
            this.pageCount = parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
}

//排序
function OrderBy(orderColum, orderTip) 
{
    isShow="0";
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
    var ordering = "d";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM = $(".orderTip");
    if ($("#" + orderTip).html() == "↑") {
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↓");
    }
    else {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↑");
    }
    orderBy = orderColum + "_" + ordering;

    TurnToPage(1);
}

//新建时显示弹出层
//function Show() {
//    $("#hiddAction").val('insert');
//    openRotoscopingDiv(false, 'divBackShadow', 'BackShadowIframe');
//    document.getElementById("div_Add").style.zIndex = "2";
//    document.getElementById("divBackShadow").style.zIndex = "1";
//    document.getElementById('div_Add').style.display = 'block';
//}
function Show()
{
    window.location.href='AddExtAttribute.aspx?ModuleID='+escape(ModuleID);
}

//隐藏弹出层
function Hide() {
    CloseDiv();
    document.getElementById('div_Add').style.display = 'none';
    Clear();
}

//清空弹出层内容
function Clear() {
    $("#EFDescUC").val('');
    $("#EFTypeUC").val('1');
    $("#EFValueListUC").val('');
    fnChange();
}

//关闭遮罩层
function CloseDiv() {
    closeRotoscopingDiv(false, 'divBackShadow');
}

//
function fnChange() {
    var value = $("#EFTypeUC").val();
    //下拉列表时时输入框可用
    if (value == '2') {
        $("#EFValueListUC").removeAttr("disabled");
    }
    else {
        $("#EFValueListUC").val('');
        $("#EFValueListUC").attr("disabled", "disabled");
    }
}

///删除销售机会
var DetailID = '';
function fnDel() 
{
    DetailID="";
    var pageDataList1 = findObj("pageDataList1", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            DetailID += $("#chk" + i).val() + ',';

        }
    }
    if (DetailID == '') {
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条记录后再删除");
    }
    else 
    {
       if(confirm("删除后不可恢复，确认删除吗！"))
        {CallBackDel()}
    }
}
function CallBackDel()
{
        $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx",
                    data: "action=del&orderNos=" + escape(DetailID),
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {

                    },

                    error: function() {

                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误");
                    },
                    success: function(data) {
                        
                        if (data.sta == 1)
                        {
                            isShow="1";
                            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.Msg);
                            TurnToPage(1);    

                        }
                        if (data.data.length != 0) {
                           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.Msg);
                        }
                        else {
                           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.Msg);
                        }
                    }
                });
}
//保存数据
function Insert() {
    if (!CheckInput()) {
        return;
    }
    var ID = $("#hiddID").val();
    var action = $("#hiddAction").val();
    var EFDesc = $.trim($("#EFDescUC").val());
    var EFType = $("#EFTypeUC").val();
    var EFValueList = $.trim($("#EFValueListUC").val().replace(' ',''));
    var EFIndex = $("#hiddEFIndex").val();


    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx",
        data: 'action=' + escape(action) + '&ID=' + escape(ID) + '&EFDesc=' + escape(EFDesc) + '&EFType=' + escape(EFType) +
        '&EFValueList=' + escape(EFValueList) + '&EFIndex=' + escape(EFIndex),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){},
        error: function() { showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误");
        },
        success: function(data) {
            

            popMsgObj.ShowMsg(data.Msg);
        }

    });
    Hide();
    TurnToPage(1);
}



//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var EFValueList = $.trim($("#EFValueListUC").val()); //应对策略
    var value = $("#EFTypeUC").val();
    var EFDesc = $.trim($("#EFDescUC").val());


    if (EFDesc == '') {
        isFlag = false;
        fieldText = fieldText + "物品特性描述|";
        msgText = msgText + "请输入物品特性描述|";
    }

    //下拉列表时时输入框可用
    if (value == '2') {
        if (EFValueList == '') {
            isFlag = false;
            fieldText = fieldText + "选择列表值列表|";
            msgText = msgText + "请输入选择列表值列表|";
        }
    }

    if (!fnCheckStrLen(EFValueList, 256)) {
        isFlag = false;
        fieldText = fieldText + "选择列表值列表|";
        msgText = msgText + "应对策略最多只允许输入256个字符|";
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

window.onload=function(){
if(document.getElementById("txtSearchPara").value!="")
{
    TurnToPage(parseInt(document.getElementById("ToPage").value));
}
}
//选择功能模块填充单据类型
var ManagerArray=new Array();
function LoadBillType()
{
    ClearDDL();
    if(ModuleID=="2081306")
    {}
    else
    {
        $("#FunctionModule").val("8")
        $("#FunctionModule").attr("disabled",true);
    }
    var FunctionModule=$("#FunctionModule").val();
    if(FunctionModule=="1")
    {
        ManagerArray=[
        [["--请选择--"],[""]],
        [["销售计划"],["officedba.SellPlan"]],
        [["销售机会"],["officedba.SellChance"]],
        [["销售报价单"],["officedba.SellOffer"]],
        [["销售合同"],["officedba.SellContract"]],
        [["销售订单"],["officedba.SellOrder"]],
        [["销售发货通知单"],["officedba.SellSend"]],
        [["回款计划"],["officedba.SellGathering"]],
        [["销售退货单"],["officedba.SellBack"]],
        [["委托代销结算单"],["officedba.SellChannelSttl"]]
        ]
    }
    else if(FunctionModule=="2")
    {
        ManagerArray=[
        [["--请选择--"],[""]],
        [["采购申请单"],["officedba.PurchaseApply"]],
        [["采购计划单"],["officedba.PurchasePlan"]],
        [["采购询价单"],["officedba.PurchaseAskPrice"]],
        [["采购合同"],["officedba.PurchaseContract"]],
        [["采购订单"],["officedba.PurchaseOrder"]],
        [["采购到货单"],["officedba.PurchaseArrive"]],
        [["采购退货单"],["officedba.PurchaseReject"]]
        ]
    }
    else if(FunctionModule=="3")
    {
        ManagerArray=[
        [["--请选择--"],[""]],
        [["采购入库单"],["officedba.StorageInPurchase"]],
        [["生产完工入库单"],["officedba.StorageInProcess"]],
        [["其他入库单"],["officedba.StorageInOther"]],
        [["红冲入库单"],["officedba.StorageInRed"]],
        [["销售出库单"],["officedba.StorageOutSell"]],
        [["其他出库单"],["officedba.StorageOutOther"]],
        [["红冲出库单"],["officedba.StorageOutRed"]],
        [["借货单"],["officedba.StorageBorrow"]],
        [["借货返还单"],["officedba.StorageReturn"]],
        [["库存调拨单"],["officedba.StorageTransfer"]],
        [["库存调整单"],["officedba.StorageAdjust"]],
        [["盘点单"],["officedba.StorageCheck"]],
        [["库存报损单"],["officedba.StorageLoss"]]
        ]
    }
    else if(FunctionModule=="4")
    {
        ManagerArray=[
        [["--请选择--"],[""]],
        [["主生产计划"],["officedba.MasterProductSchedule"]],
        [["物料需求计划"],["officedba.MRP"]],
        [["生产任务单"],["officedba.ManufactureTask"]],
        [["生产任务单汇报"],["officedba.ManufactureReport"]],
        [["领料单"],["officedba.TakeMaterial"]],
        [["退料单"],["officedba.BackMaterial"]]
        ]
    }
    else if(FunctionModule=="5")
    {
        ManagerArray=[
        [["--请选择--"],[""]],
        [["质检申请单"],["officedba.QualityCheckApplay"]],
        [["质检报告单"],["officedba.QualityCheckReport"]],
        [["不合格品处置单"],["officedba.CheckNotPass"]]
        ]
    }
    else if(FunctionModule=="6")
    {
        ManagerArray=[
        [["--请选择--"],[""]],
        [["配送单"],["officedba.SubDeliverySend"]],
        [["配送退货单"],["officedba.SubDeliveryBack"]],
        [["分店调拨单"],["officedba.SubDeliveryTrans"]]
        ]
    }
    else if(FunctionModule=="7")
    {
        ManagerArray=[
        [["--请选择--"],[""]],
        [["分店入库单"],["officedba.SubStorageIn"]],
        [["分店销售订单"],["officedba.SubSellOrder"]],
        [["分店销售退货单"],["officedba.SubSellBack"]]
        ]
    }
    else if(FunctionModule=="8")
    {
        ManagerArray=[
        [["--请选择--"],[""]],
        [["项目档案"],["officedba.ProjectInfo"]]
        ]
    }
    else ManagerArray=[
        [["--请选择--"],[""]]
        ]
    
    for(var i=0;i<ManagerArray.length;i++)//填充单据类型
    {    
        document.getElementById('FormType').options[i]=new Option(ManagerArray[i][0],ManagerArray[i][1]);
    }
}
//清空选项
 function ClearDDL()
 {
    var length=document.getElementById('FormType').options.length;
    if(length>1)
    {
        for(i=length-1;i>=0;i--)   
        {   
          document.getElementById('FormType').remove(i);   
          length--;
        }   
    }
 }
