$(document).ready(function() {

    $("#CustType_ddlCodeType").css("width", "120px");
    $("#AreaID_ddlCodeType").css("width", "120px");

    $("#CustNo_ddlCodeRule").css("width", "80px");
    $("#CustNo_txtCode").css("width", "80px");
    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031101', 'ModuleID=2031102');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (flag != null) {
            $("#ibtnBack").css("display", "inline");

        }
    }
});

//获取查询参数
function GetRequestToHidden() {
    var url = location.search;
    return url;
}

//返回按钮
function fnBack() {
    var URLParams = document.getElementById("HiddenURLParams").value;
    window.location.href = 'AdversaryInfoList.aspx' + URLParams;
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
    
}

function fnSelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
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

    var str = $("#hiddAcction").val();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/AdversaryInfo_Add.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=' + escape(str),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                if ($("#hiddAcction").val() == "insert") {
                    $("#hiddAcction").val("update")
                }
                $("#CustNo_txtCode").val(data.data);
                $("#CustNo_txtCode").attr("disabled", "disabled");
                $("#CustNo_ddlCodeRule").css("display", "none");
                $("#CustNo_txtCode").css("width", "95%");
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
            }
            else {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存失败,请确认！");
            }
        }
    });
}

//获取主表信息
function fnGetInfo() {
    var strInfo = '';
    var CodeType = $("#CustNo_ddlCodeRule").val(); //竞争对手编号产生的规则
    var CustNo = $("#CustNo_txtCode").val(); //竞争对手编号
    var BigType ='3'; //往来单位大类（1客户，2供应商，3竞争对手，4银行，5外协加工厂，6运输商，7其他），此处固定为3竞争对手
    var CustType = $("#CustType_ddlCodeType").val(); //竞争对手类别(分类代码表定义，竞争对手类别)                                                                      
    var CustClass = $("#CustClass").attr("title"); //竞争对手细分(往来单位分类表)                                                                                                                                                                       
    var CustName = $("#CustName").val(); //竞争对手名称
    var ShortNam = $("#ShortNam").val(); //竞争简称                                                                                                        
    var PYShort = $("#PYShort").val(); //拼音缩写
    var AreaID = $("#AreaID_ddlCodeType").val(); //所在区域（分类代码表定义，区域代码）                                                                              
    var SetupDate = $("#SetupDate").val(); //成立时间                                                                                                       
    var ArtiPerson = $("#ArtiPerson").val(); //法人代表                                                                                                      
    var CompanyType = $("#CompanyType").val(); //单位性质（1事业，2企业，3社团，4自然人，5其他）                                                              
    var StaffCount = $("#StaffCount").val(); //员工总数（个）                                                                                                
    var SetupMoney = $("#SetupMoney").val(); //注册资本(万元)                                                                                                
    var SetupAddress = $("#SetupAddress").val(); //注册地址                                                                                                    
    var website = $("#website").val(); //公司网址                                                                                                         
    var CapitalScale = $("#CapitalScale").val(); //资产规模(万元)                                                                                              
    var SellArea = $("#SellArea").val(); //经营范围                                                                                                        
    var SaleroomY = $("#SaleroomY").val(); //年销售额(万元)                                                                                                 
    var ProfitY = $("#ProfitY").val(); //年利润额(万元)                                                                                                   
    var TaxCD = $("#TaxCD").val(); //税务登记号                                                                                                         
    var BusiNumber = $("#BusiNumber").val(); //营业执照号                                                                                                    
    var IsTax = $("#IsTax").val(); //一般纳税人（0否，1是）                                                                                             
    var Address = $("#Address").val(); //地址                                                                                                             
    var Post = $("#Post").val(); //邮编                                                                                                                
    var ContactName = $("#ContactName").val(); //联系人                                                                                                       
    var Tel = $("#Tel").val(); //电话                                                                                                                 
    var Mobile = $("#Mobile").val(); //手机                                                                                                              
    var email = $("#email").val(); //邮件                                                                                                               
    var CustNote = $("#CustNote").val(); //竞争对手对手简介                                                                                                
    var Product = $("#Product").val(); //主打产品                                                                                                         
    var Market = $("#Market").val(); //市场占有率                                                                                                        
    var SellMode = $("#SellMode").val(); //销售模式                                                                                                        
    var Project = $("#Project").val(); //竞争产品/方案                                                                                                    
    var Power = $("#Power").val(); //竞争能力                                                                                                           
    var Advantage = $("#Advantage").val(); //竞争优势                                                                                                       
    var disadvantage = $("#disadvantage").val(); //竞争劣势                                                                                                    
    var Policy = $("#Policy").val(); //应对策略                                                                                                          
    var Remark = $("#Remark").val(); //备注                                                                                                              
    var UsedStatus = $("#UsedStatus").val(); //启用状态（0停用，1启用）

    strInfo = 'CodeType=' + escape(CodeType) + '&CustNo=' + escape(CustNo) + '&BigType=' + escape(BigType)
            + '&CustType=' + escape(CustType) + '&CustClass=' + escape(CustClass) + '&CustName=' + escape(CustName)
            + '&ShortNam=' + escape(ShortNam) + '&PYShort=' + escape(PYShort) + '&AreaID=' + escape(AreaID)
            + '&SetupDate=' + escape(SetupDate) + '&ArtiPerson=' + escape(ArtiPerson) + '&CompanyType=' + escape(CompanyType)
            + '&StaffCount=' + escape(StaffCount) + '&SetupMoney=' + escape(SetupMoney) + '&SetupAddress=' + escape(SetupAddress)
            + '&website=' + escape(website) + '&CapitalScale=' + escape(CapitalScale) + '&SellArea=' + escape(SellArea)
            + '&SaleroomY=' + escape(SaleroomY) + '&ProfitY=' + escape(ProfitY) + '&TaxCD=' + escape(TaxCD)
            + '&BusiNumber=' + escape(BusiNumber) + '&IsTax=' + escape(IsTax) + '&Address=' + escape(Address)
            + '&Post=' + escape(Post) + '&ContactName=' + escape(ContactName) + '&Tel=' + escape(Tel)
            + '&Mobile=' + escape(Mobile) + '&email=' + escape(email) + '&CustNote=' + escape(CustNote)
            + '&Product=' + escape(Product) + '&Market=' + escape(Market) + '&SellMode=' + escape(SellMode)
            + '&Project=' + escape(Project) + '&Power=' + escape(Power) + '&Advantage=' + escape(Advantage)
            + '&disadvantage=' + escape(disadvantage) + '&Policy=' + escape(Policy) + '&Remark=' + escape(Remark)
            + '&UsedStatus=' + escape(UsedStatus);
    return strInfo;

}

////获取明细数据
function getDropValue() {
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;

           
            var Dynamic = $("#Dynamic" + rowid).val()   //物品ID（对应物品表ID）


            SendOrderFit_Item[j] = [[Dynamic]];
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

    var newNameXH = newTR.insertCell(0); //添加列:序号
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' value=" + rowID + " type='checkbox'  />";

    var newFitDesctd = newTR.insertCell(1); //添加列: 动态
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input  class='tdinput' type='text' id='Dynamic" + rowID + "' maxlength='100' style=' width:94%;'/>"; //添加列内容
    txtTRLastIndex.value = rowID; //将行号推进下一行
}


//唯一性验证
function checkonly() {
    var OrderNo = $("#CustNo_txtCode").val(); //竞争对手编号
    var tablename = "officedba.AdversaryInfo";
    var columname = "CustNo";
    var CodeType = $("#CustNo_ddlCodeRule").val(); //竞争对手编号产生的规则

    //添加时验证编号
    if ($("#hiddAcction").val() == "insert") {
        if (CodeType == "") {
            if (OrderNo != "") {
                if (isnumberorLetters(OrderNo)) {
                    CheckInput();
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "../../../Handler/CheckOnlyOne.ashx?strcode='" + OrderNo + "'&colname=" + columname + "&tablename=" + tablename + "",
                        dataType: 'json', //返回json格式数据
                        cache: false,
                        success: function(data) {
                            if (data.sta == 1) {
                                InsertSellOfferData();
                            }
                            else {
                             
                                popMsgObj.Show('对手编号|', '对手编号已存在|');

                            }

                        }
                    });
                }

            }
            else {
                InsertSellOfferData();
            }
        }
        else {
            InsertSellOfferData();
        }
    }
    else {
        InsertSellOfferData();
    }

}



//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var CodeType = $.trim($("#CustNo_ddlCodeRule").val()); //竞争对手编号产生的规则
    var CustNo = $.trim($("#CustNo_txtCode").val()); //竞争对手编号
    var BigType = $.trim($("#BigType").val()); //往来单位大类（1客户，2供应商，3竞争对手，4银行，5外协加工厂，6运输商，7其他），此处固定为3竞争对手                                                                     
    var CustClass = $.trim($("#CustClass").val()); //竞争对手细分(往来单位分类表)                                                                                                                                                                       
    var CustName = $.trim($("#CustName").val()); //竞争对手名称                                                                                                                                                                                                                                                                   
    var StaffCount = $.trim($("#StaffCount").val()); //员工总数（个）                                                                                                
    var SetupMoney = $.trim($("#SetupMoney").val()); //注册资本(万元)                                                                                                                                                                                                       
    var CapitalScale = $.trim($("#CapitalScale").val()); //资产规模(万元)                                                                                                                                                                                                 
    var SaleroomY = $.trim($("#SaleroomY").val()); //年销售额(万元)                                                                                                 
    var ProfitY = $.trim($("#ProfitY").val()); //年利润额(万元)


    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    
    if (CodeType == "") {
        if (CustNo == "") {
            isFlag = false;
            fieldText = fieldText + "竞争对手编号|";
            msgText = msgText + "请输入对手编号|";
        }
        else {
            if (!CodeCheck(CustNo)) {
                isFlag = false;
                fieldText = fieldText + "竞争对手编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }

    if (CustName == "") {
        isFlag = false;
        fieldText = fieldText + "对手名称|";
        msgText = msgText + "请输入对手名称|";
    }

    if (CustClass == "") {
        isFlag = false;
        fieldText = fieldText + "对手细分|";
        msgText = msgText + "请选择对手细分|";
    }
    if (StaffCount.length > 0) {
        if (!IsZint(StaffCount)) {
            isFlag = false;
            fieldText = fieldText + "员工个数|";
            msgText = msgText + "员工个数只能为整数|";
        }
    }
    if (SetupMoney.length > 0) {
        if (!IsNumeric(SetupMoney, 8, 2)) {
            isFlag = false;
            fieldText = fieldText + "注册资本|";
            msgText = msgText + "注册资本输入有误，请输入有效的数值！|";
        }
    }

    if (CapitalScale.length > 0) {
        if (!IsNumeric(CapitalScale, 8, 2)) {
            isFlag = false;
            fieldText = fieldText + "资产规模|";
            msgText = msgText + "资产规模输入有误，请输入有效的数值！|";
        }
    }
    if (SaleroomY.length > 0) {
        if (!IsNumeric(SaleroomY, 8, 2)) {
            isFlag = false;
            fieldText = fieldText + "年销售额|";
            msgText = msgText + "年销售额输入有误，请输入有效的数值！|";
        }
    }
    if (ProfitY.length > 0) {
        if (!IsNumeric(ProfitY, 8, 2)) {
            isFlag = false;
            fieldText = fieldText + "年利润额|";
            msgText = msgText + "年利润额输入有误，请输入有效的数值！|";
        }
    }

    var CustNote = $("#CustNote").val(); //竞争对手对手简介
    if (!fnCheckStrLen(CustNote,1024)) {
        isFlag = false;
        fieldText = fieldText + "竞争对手简介|";
        msgText = msgText + "竞争对手简介最多只允许输入1024个字符！|";
    }
    var Product = $("#Product").val(); //主打产品
    if (!fnCheckStrLen(Product, 800)) {
        isFlag = false;
        fieldText = fieldText + "主打产品|";
        msgText = msgText + "主打产品最多只允许输入800个字符！|";
    }
    var Market = $("#Market").val(); //市场占有率
    if (!fnCheckStrLen(Market, 50)) {
        isFlag = false;
        fieldText = fieldText + "市场占有率|";
        msgText = msgText + "市场占有率最多只允许输入50个字符！|";
    }
    var SellMode = $("#SellMode").val(); //销售模式
    if (!fnCheckStrLen(SellMode, 50)) {
        isFlag = false;
        fieldText = fieldText + "销售模式|";
        msgText = msgText + "销售模式最多只允许输入50个字符！|";
    }
    var Project = $("#Project").val(); //竞争产品/方案
    if (!fnCheckStrLen(Project, 1024)) {
        isFlag = false;
        fieldText = fieldText + "竞争产品/方案|";
        msgText = msgText + "竞争产品/方案最多只允许输入1024个字符！|";
    }
    var Power = $("#Power").val(); //竞争能力
    if (!fnCheckStrLen(Power, 1024)) {
        isFlag = false;
        fieldText = fieldText + "竞争能力|";
        msgText = msgText + "竞争能力最多只允许输入1024个字符！|";
    }
    var Advantage = $("#Advantage").val(); //竞争优势
    if (!fnCheckStrLen(Advantage, 1024)) {
        isFlag = false;
        fieldText = fieldText + "竞争优势|";
        msgText = msgText + "竞争优势最多只允许输入1024个字符！|";
    }
    var disadvantage = $("#disadvantage").val(); //竞争劣势
    if (!fnCheckStrLen(disadvantage, 1024)) {
        isFlag = false;
        fieldText = fieldText + "竞争劣势|";
        msgText = msgText + "竞争劣势最多只允许输入1024个字符！|";
    }
    var Policy = $("#Policy").val(); //应对策略
    if (!fnCheckStrLen(Policy, 1024)) {
        isFlag = false;
        fieldText = fieldText + "应对策略|";
        msgText = msgText + "应对策略最多只允许输入1024个字符！|";
    }
    var Remark = $("#Remark").val(); //备注
    if (!fnCheckStrLen(Remark, 1024)) {
        isFlag = false;
        fieldText = fieldText + "备注|";
        msgText = msgText + "备注最多只允许输入1024个字符！|";
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


//若是中文则自动填充拼音缩写
function LoadPYShort() {
    var txtCustNam = $("#CustName").val();
    $("#PYShort").val('');
    if (txtCustNam.length > 0 && isChinese(txtCustNam)) {
        $.ajax({
            type: "POST",
            url: "../../../Handler/Common/PYShort.ashx?Text=" + escape(txtCustNam),
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                //AddPop();
            },
            //complete :function(){ //hidePopup();},
            error: function() { },
            success: function(data) {
            document.getElementById('PYShort').value = data.info;
            }
        });
    }
}

//打印功能
function fnPrintOrder() {
    //if (confirm('请确认页面信息是否已保存！')) {
        var OrderNo = $.trim($("#CustNo_txtCode").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '') {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存您所填的信息！");
        }
        else {

            window.open('../../../Pages/PrinttingModel/SellManager/AdversaryPrint.aspx?no=' + OrderNo);
        }
   // }
}