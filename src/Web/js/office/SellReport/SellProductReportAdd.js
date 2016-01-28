/********************
**销售汇报js
*创建人：hexw
*创建时间：2010-7-6
********************/

$(document).ready(function() {

    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {//是否从列表页面进入
    
        var billID = requestObj['id'];
        isList=billID; 
        var requestobj = GetRequestToHidden();
        document.getElementById("HiddenURLParams").value = requestobj;
        if (billID != null) {
             $("#hiddSellRptID").val(billID);
            fnGetSellRptInfo(billID); //获取销售单详细信
        }
    }
  
});

//获取查询参数
function GetRequestToHidden() {
    var url = location.search;
    return url;
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

//获取费用申请单详细信息
function fnGetSellRptInfo(billID) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellReport/SellProductReport.ashx",
        data: "action=getdatabyid&billID=" + escape(billID),
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
                    if(data.dataDetail != null)
                    {
                        fnSetExpDetail(data); //获取销售单明细信息
                    }
            }
           
        }
    });
}
function fnInitPage(data)
{
    $("#createdate").val(data.createdate);        //日期
    $("#SellDept").val(data.SellDeptName); //部门名称
    $("#hiddSellDeptID").val(data.SellDept);//部门ID
    $("#productName").val(data.productName);//物品名称
    $("#hiddProductName").val(data.productName);
    $("#price").val(data.price);        //单价
    $("#sellNum").val(data.sellNum);//数量
    $("#sellPrice").val(data.sellPrice);          //金额
    $("#memo").val(data.memo);         //备注
    BillButtonShow(data.id);
}

function fnSetExpDetail(data)
{
   fnDelDetailRows();
   if (data.dataDetail != null) {
        $.each(data.dataDetail, function(i, item) {
        
            //读取最后一行的行号，存放在txtTRLastIndex文本框中 
            var txtTRLastIndex = findObj("txtTRLastIndex", document);
            var rowID = parseInt(txtTRLastIndex.value) + 1;
            var signFrame = findObj("tbExpDetails", document);
            var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
            newTR.id = rowID;
            var m=0;

            var newNameXH = newTR.insertCell(m); //添加列:序号
            newNameXH.className = "cell";
            newNameXH.innerHTML = "<input id='chk" + rowID + "'   onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
            m++;
            
            var newFitNotd = newTR.insertCell(m); //添加列:营业员
            newFitNotd.className = "cell";
            newFitNotd.innerHTML = "<input id='UserSellerIDD" + rowID + "' onclick=\"fnSelectSeller('" + rowID + "')\" readonly='readonly' style=' width:98%;' type='text'  class='tdinput' /><input type=\"hidden\" value='' id='hiddSellerID" + rowID + "'/>"; //添加列内容
            m++;
            
            var newFitNametd = newTR.insertCell(m); //添加列:提成比例
            newFitNametd.className = "cell";
            newFitNametd.innerHTML = "<input id='Rate" + rowID + "' style=' width:98%;' type='text' class='tdinput' onchange='Number_round(this,"+precisionLength+")' />"; //添加列内容
            m++;
            
            $("#UserSellerIDD" + rowID).val(item.SellerName); //费用类别名称
            $("#UserSellerIDD"+rowID ).attr("title",item.sellerID);//费用类别ID
            $("#hiddSellerID"+rowID).val(item.sellerID);
            $("#Rate" + rowID).val(FormatAfterDotNumber(item.rate, precisionLength));  //金额
            txtTRLastIndex.value = rowID; //将行号推进下一行 

        });
    }
}

//删除明细中的所有数据
function fnDelDetailRows()
{
     var tbSellDetails = findObj("tbExpDetails", document);
    var rowscount = tbSellDetails.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
        tbSellDetails.deleteRow(i);
    }
    var txtTRLastIndex = findObj("txtTRLastIndex", document); //重置最后行号为
    txtTRLastIndex.value = "0";
}

//选择营业员
function fnSelectSeller(rowid) {
    var str='UserSellerIDD'+rowid+','+'hiddSellerID'+rowid;
    alertdiv(str);
}

//选择明细
function fnSelectAll() {
    $.each($("#tbExpDetails :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//删除明细一行
function fnDelOneRow() {
    var tbExpDetails = findObj("tbExpDetails", document);
    var rowscount = tbExpDetails.rows.length;
    for (var i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除

        if ($("#chk" + i).attr("checked")) {
            tbExpDetails.rows[i].style.display = "none";
        }
    }
    $("#checkall").removeAttr("checked");
}

//添加费用明细
function AddRow()
{
     //读取最后一行的行号，存放在txtTRLastIndex文本框中 
    var txtTRLastIndex = findObj("txtTRLastIndex", document);
    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = findObj("tbExpDetails", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m=0;

    var newNameXH = newTR.insertCell(m); //添加列:序号
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "'   onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
    m++;
    
    var newFitNotd = newTR.insertCell(m); //添加列:营业员
    newFitNotd.className = "cell";
    newFitNotd.innerHTML = "<input id='UserSellerIDD" + rowID + "' onclick=\"fnSelectSeller('" + rowID + "')\" readonly='readonly' style=' width:98%;' type='text'  class='tdinput' /><input type=\"hidden\" value='' id='hiddSellerID" + rowID + "'/>"; //添加列内容
    m++;
    
    var newFitNametd = newTR.insertCell(m); //添加列:提成比例
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<input id='Rate" + rowID + "'  style=' width:98%;' type='text' class='tdinput' onchange='Number_round(this,"+precisionLength+")' />"; //添加列内容
    m++;
    
    txtTRLastIndex.value = rowID; //将行号推进下一行    
}

//获取费用明细数据
function getExpensesValue() {
    var Expenses_Item = new Array();
    var signFrame = findObj("tbExpDetails", document);
    var j = 0;
    for (var i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;

            var SellerIDD = $("#hiddSellerID" + rowid).val()//营业员
            var Rate = $("#Rate" + rowid).val()//提成比例             

            Expenses_Item[j] = [[j], [SellerIDD], [Rate]];
        }
    }
    return Expenses_Item;
}

//去除全选按钮
function fnUnSelect(obj) {

    if (!obj.checked) {
        $("#checkall1").removeAttr("checked");
        return;
    }
    else {
        //验证明细信息
        var signFrame = findObj("tbExpDetails", document);
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

            $("#checkall1").attr("checked", "checked");
        }

    }
}

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var createdate = $("#createdate").val(); //日期
    var SellDept =  $.trim($("#SellDept").val()); //部门
    var sellPrice = $("#sellPrice").val();    //金额
    var memo=$("#memo").val();//备注
    var RetVal = CheckSpecialWords();
    
    if (!IsNumeric(sellPrice, 22, 6)) {
        isFlag = false;
        fieldText = fieldText + "金额|";
        msgText = msgText + "金额超出范围|";
    }
    if (SellDept == "") {
        isFlag = false;
        fieldText = fieldText + "部门|";
        msgText = msgText + "部门不能为空|";
    }
    if (createdate == "" ) {
        isFlag = false;
        fieldText = fieldText + "日期|";
        msgText = msgText + "日期不能为空|";
    }
   if(strlen(memo) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText + "备注最多只允许输入500个字符|";
    }
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    //验证营业员提成比例明细
    var rowCount=0;
    var signFrame = findObj("tbExpDetails", document);
    for (var i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            rowCount++;
            var rowid = signFrame.rows[i].id;
            var SellerID = $("#hiddSellerID" + rowid).val()//营业员
            var Rate = $("#Rate" + rowid).val()//提成比例
            if (SellerID == "") {
                isFlag = false;
                fieldText = fieldText + "提成明细（行号：" + i + "）|";
                msgText = msgText + "请选择营业员|";
            }
            if (Rate == "") {
                isFlag = false;
                fieldText = fieldText + "提成明细（行号：" + i + "）|";
                msgText = msgText + "请输入提成比例|";
            }
            else {
                if (!IsNumeric(Rate, 18, 6)) {
                    isFlag = false;
                    fieldText = fieldText + "提成明细（行号：" + i + "）|";
                    msgText = msgText + "提成比例范围为0到1之间，请输入有效的数值！|";
                }
                else if(parseFloat(Rate)<0 || parseFloat(Rate)>1)
                {
                    isFlag = false;
                    fieldText = fieldText + "提成明细（行号：" + i + "）|";
                    msgText = msgText + "提成比例范围为0到1之间，请输入有效的数值！|";
                }
            }
        }
    }
//    if(rowCount==0)
//    {
//        isFlag = false;
//        fieldText = fieldText + "提成明细|";
//        msgText = msgText + "提成明细不可为空，请输入营业员提成明细|";
//    }
    if (!isFlag) {
        //注：两个方法显示弹出提示信息层,方法一是对字段用红色处理，方法二是所有的提示信息传入未处理颜色

        //方法一
        popMsgObj.Show(fieldText, msgText);

        //方法二
        //popMsgObj.ShowMsg('所有的错误信息字符串');
    }

    return isFlag;
}
//获取参数
function getMainParam()
{
    var paramStr="";
    var createdate = $("#createdate").val();        //日期
    //var SellDeptName = $("#SellDept").val(); //部门名称
    var SellDept = $("#hiddSellDeptID").val();//部门ID
    //var productName = $("#productName").val();//物品名称
    var productName = $.trim($("#hiddProductName").val());//物品名称
    var price = $("#price").val();        //单价
    var sellNum = $("#sellNum").val();//数量
    var sellPrice = $("#sellPrice").val();          //金额
    var memo = $("#memo").val();         //备注
    
    paramStr="createdate="+escape(createdate)+"&SellDept="+escape(SellDept)+"&productName="+escape(productName)+
            "&price="+escape(price)+"&sellNum="+escape(sellNum)+"&sellPrice="+escape(sellPrice)+"&memo="+escape(memo);
           
    return paramStr;
}

//保存费用申请单
function SaveData()
{
    if (!CheckInput()) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var strfitinfo = getExpensesValue().join("|");
    
    var str = 'save';  //请求操作:保存

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellReport/SellProductReport.ashx",
        data: getMainParam()+'&strfitinfo='+escape(strfitinfo)+'&action='+escape(str),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                if (data.sta == 1) {
                    $("#hiddSellRptID").val(data.id);
                    BillButtonShow(data.id);
                }
                else {
                    hidePopup();
                }
            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//修改费用申请单
function UpdateData()
{
    if (!CheckInput()) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var strfitinfo = getExpensesValue().join("|");
    var billID=$("#hiddSellRptID").val();//单据ID
    var str = 'update';  //请求操作:保存

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellReport/SellProductReport.ashx",
        data: getMainParam()+'&BillID='+escape(billID)+'&strfitinfo=' + escape(strfitinfo) +  '&action=' + escape(str),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                if (data.sta == 1) {
                    $("#hiddSellRptID").val(data.id);
                    BillButtonShow(data.id);
                }
                else {
                    hidePopup();
                }
            }
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
        }
    });
}

//判断按钮的显示情况
function BillButtonShow(id)
{
    if(id !="" || id !=null)//制单状态，可保存修改、确认（其它按钮都为灰色）
    {
        $("#btn_save").css("display","none");
        $("#btn_unupdate").css("display", "none");
        $("#btn_update").css("display", "block");
        $("#imgAdd").css("display","block");
        $("#imgDel").css("display","block");
        $("#un_imgAdd").css("display","none");
        $("#un_imgDel").css("display","none");
    }
    else
    {
        $("#btn_save").css("display","block");
        $("#btn_unupdate").css("display", "none");
        $("#btn_update").css("display", "none");
        $("#imgAdd").css("display","block");
        $("#imgDel").css("display","block");
        $("#un_imgAdd").css("display","none");
        $("#un_imgDel").css("display","none");
    }
}

//删除明细中所有数据
function fnDelDetailAllRow()
{
    var tbExpDetails = findObj("tbExpDetails", document);
    var rowscount = tbExpDetails.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
        tbExpDetails.deleteRow(i);
    }
    var txtTRLastIndex = findObj("txtTRLastIndex", document); //重置最后行号为
    txtTRLastIndex.value = "0";
}

//返回按钮
function fnBack() {
    var URLParams = document.getElementById("HiddenURLParams").value;
    window.location.href = 'SellProductReportList.aspx' + URLParams;
}

////产品勾选框改变时
//function fnPCheckChange()
//{
//    if(document.getElementById('pCheck').checked==true)
//    {
//        document.getElementById('productName').disabled=false;
//        document.getElementById('hiddProductName').value='';
//        document.getElementById('hiddProductName').disabled=true;
//    }
//    else
//    {
//        document.getElementById('productName').value='0';
//        document.getElementById('productName').disabled=true;
//        document.getElementById('hiddProductName').disabled=false;
//    }
//}

//改变选择的产品时，同时填充到文本框中
function fnProdNameChange()
{
    var ddl=document.getElementById('productName');
    var index=ddl.selectedIndex;
    document.getElementById('hiddProductName').value=ddl.options[index].text;
    var productid=ddl.options[index].value;
    
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellReport/SellProductReport.ashx",
        data: 'productID='+escape(productid)+'&action=getprodinfo',
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            //AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); 
                          },
        success: function(data) {
            if (data.data[0] != "" && data.data[0]!=null) {
                
                    $("#price").val(FormatAfterDotNumber(data.data[0].price, precisionLength));
            }
        }
    });
}