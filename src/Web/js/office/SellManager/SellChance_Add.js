
var treeview1 = null;


$(document).ready(function() {
    init();
    GetExtAttr("officedba.SellChance",null);//扩展属性
    $("#SellChanceUC_ddlCodeRule").css("width", "80px");
    $("#chanceTypeUC_ddlCodeType").css("width", "120px");
    $("#HapSourceUC_ddlCodeType").css("width", "120px");
    $("#FeasibilityUC_ddlCodeType").css("width", "120px");
    $("#SellChanceUC_txtCode").css("width", "80px"); //销售机会编号

    //是否显示返回按钮 
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['islist']; //是否从列表页面进入
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031201', 'ModuleID=2031202');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (flag != null) {
            $("#ibtnBack").css("display", "inline");

        }
    }
});


function init() {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Common/UserDept.ashx?action=loaduserlistwithdepartment",
        dataType: 'string',
        data: '',
        cache: false,
        success: function(data) {
            var result = null;
            eval("result = " + data);

            if (result.result) {
                /// <param name="containerID">TreeView的容器元素的ID</param>
                /// <param name="nodes">节点数组</param>     

                /// <param name="selMode">选择模式0:多选;1:单选</param>
                /// <param name="selNodeType">可选节点类型:0不限制</param>        
                /// <param name="expandLevel">默认展开层级</param>
                /// <param name="mode">弹出(1) OR 平板 显示方式(0)</param>     
                // <param name="valNodeType">取值节点类型</param>
                /// <param name="selDuplicate">取值是否允许重复</param>
               
                treeview1 = new TreeView("treeDiv1", result.data, 0, 0, 4, 1, 2, false);
                //treeview2 = new TreeView("treeDiv2",result.data,1,2,2,1,2,false);
            } else {
                alert(result.data);
            }
        },
        error: function(data) {
            alert(data.responseText);
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
    var URLParams = document.getElementById("HiddenURLParams").value;
    window.location.href = 'SellChanceList.aspx' + URLParams;
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

//唯一性验证
function checkonly() {
    var ChanceNo = $("#SellChanceUC_txtCode").val(); //销售机会编号
    var tablename = "officedba.SellChance";
    var columname = "ChanceNo";
    var CodeType = $("#SellChanceUC_ddlCodeRule").val(); //销售机会编号产生的规则

    if (CodeType == "") {
        if (ChanceNo != "") {
            if (isnumberorLetters(ChanceNo)) {
                CheckInput();
            }
            else {
                //添加时验证编号
                if ($("#hiddAcction").val() == "insert") {
                    $.ajax({
                        type: "POST",
                        url: "../../../Handler/CheckOnlyOne.ashx?strcode='" + ChanceNo + "'&colname=" + columname + "&tablename=" + tablename + "",
                        dataType: 'json', //返回json格式数据
                        cache: false,
                        success: function(data) {
                            if (data.sta == 1) {
                                InsertChanceData();
                            }
                            else {
                                popMsgObj.Show('对手编号|', '对手编号已存在|');
                            }
                        }
                    });
                }
                else {
                    InsertChanceData();
                }
            }
        }
        else {
            InsertChanceData();
        }
    }
    else {
        InsertChanceData();
    }
}


function InsertChanceData() {
    if (!CheckInput()) {
        return;
    }
    var CodeType = $("#SellChanceUC_ddlCodeRule").val(); //销售机会编号产生的规则
    var ChanceNo = $("#SellChanceUC_txtCode").val(); //机会编号
    var CustID = $("#CustID").attr("title");     //客户ID（对应客户表ID）
    var CustType = $("#CustType").attr("title");   //客户类型ID                      
    var CustTel = $("#CustTel").val();    //客户联系电话                              
    var Title = $("#Title").val();      //销售机会主题
    var ChanceType = $("#chanceTypeUC_ddlCodeType").val(); //销售机会类型(对应分类代码表ID)
    var HapSource = $("#HapSourceUC_ddlCodeType").val();  //销售机会来源ID(对应分类代码表ID)
    var Seller = $("#hiddSeller").val();     //业务员(对应员工表ID)
    var SellDeptId = $("#hiddDeptID").val(); //部门(对部门表ID)                
    var FindDate = $("#FindDate").val();   //发现日期                        
    var ProvideMan = $("#ProvideMan").val(); //提供人                          
    var Requires = $("#Requires").val();   //需求描述                        
    var IntendDate = $("#IntendDate").val(); //预期签单日期                    
    var IntendMoney = $("#IntendMoney").val(); //预期金额                        
    var Remark = $("#Remark").val();     //备注
    var IsQuoted = 0;   //是否被报价(0-否，1-是)

    var PushDate = $("#PushDate").val();   //日期
    var Seller1 = $("#hiddSeller1").val();     //业务员ID(对应员工表ID)                                                                
    var Phase = $("#Phase").val();      //阶段（1初期沟通、2立项评估、3需求分析、4方案指定、5招投标/竞争、6商务谈判、7合同签约）
    var State = $("#State").val();      //状态（1跟踪2成功3失败4搁置5失效）
    var Feasibility = $("#FeasibilityUC_ddlCodeType").val(); //机会可能性ID(对应分类代码表ID)                                                        
    var DelayDate = $("#DelayDate").val();  //阶段滞留时间(天)
    var Remark1 = $("#Remark1").val();     //阶段备注
    var CanViewUser = $("#CanViewUserNameHidden").val();  //可查看该销售机会的人员（ID，多个）
    var CanViewUserName = $("#CanViewUserName").val();     //可查看该销售机会的人员姓名  
    var IsMobileNotice=0;
    if( document.getElementById("cbIsMobileNotice").checked == true  )
    {
       IsMobileNotice="1" ;     
    }
    else
    {
       IsMobileNotice= "0";  
    }
    var RemindTime="";
    var txtRemindTime=document.getElementById("txtRemind").value;
    if(txtRemindTime!="" && txtRemindTime!=null && txtRemindTime!="undefined" && txtRemindTime!="点此请选择提醒时间")
    {
        RemindTime= document.getElementById("txtRemind").value+" "+document.getElementById("selCompleteDateHour").value+":00";//提醒时间
    }
    var RemindMTel=$.trim($("#RemindMTel").val());//提醒手机号
    var RemindContent=$.trim($("#RemindContent").val());//提醒内容
    var ReceiverID=$("#hiddReceiverID").val();//接收人                                                                        
    var billID=$("#sellChanceID").val(); //销售机会Id
    var str = $("#hiddAcction").val();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChance_Add.ashx",
        data: 'CodeType=' + escape(CodeType) + '&ChanceNo=' + escape(ChanceNo) + '&CustID=' + escape(CustID)
            + '&CustType=' + escape(CustType) + '&CustTel=' + escape(CustTel) + '&Title=' + escape(Title)
            + '&ChanceType=' + escape(ChanceType) + '&HapSource=' + escape(HapSource) + '&Seller=' + escape(Seller)
            + '&SellDeptId=' + escape(SellDeptId) + '&FindDate=' + escape(FindDate) + '&ProvideMan=' + escape(ProvideMan)
            + '&Requires=' + escape(Requires) + '&IntendDate=' + escape(IntendDate) + '&IntendMoney=' + escape(IntendMoney)
            + '&Remark=' + escape(Remark) + '&IsQuoted=' + escape(IsQuoted) + '&PushDate=' + escape(PushDate)
            + '&Seller1=' + escape(Seller1) + '&Phase=' + escape(Phase) + '&State=' + escape(State)
            + '&Feasibility=' + escape(Feasibility) + '&DelayDate=' + escape(DelayDate) + '&Remark1=' + escape(Remark1)
            + '&CanViewUser=' + escape(CanViewUser) + '&CanViewUserName=' + escape(CanViewUserName) 
            + '&IsMobileNotice=' + escape(IsMobileNotice) + '&RemindTime=' + escape(RemindTime) 
            + '&RemindMTel=' + escape(RemindMTel) + '&RemindContent=' + escape(RemindContent) 
            +'&billID='+escape(billID)+'&ReceiverID='+escape(ReceiverID) 
            + '&action=' + escape(str)+GetExtAttrValue(),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
           
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == 1) {
                $("#SellChanceUC_txtCode").val(data.no);
                $("#sellChanceID").val(data.id);
                $("#SellChanceUC_txtCode").css("width", "95%");
                $("#SellChanceUC_txtCode").attr("readonly", "readonly");
                $("#SellChanceUC_ddlCodeRule").css("display", "none");
                $("#hiddAcction").val("update");
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
                fnGetPush(data.no);
            }
            else {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存失败,请确认！");
            }
        }
    });
}


//获取机会推进历史
function fnGetPush(chanceNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellChance_Add.ashx",
        data: 'ChanceNo=' + escape(chanceNo) + '&action=getPush',
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        //complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#chanceList tbody").find("tr.newrow").remove();
            $.each(data.data, function(i, item) {
                $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center'>" + item.PhaseName + "</td>" +
                        "<td height='22' align='center'>" + item.PushDate + "</a></td>" +
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                        "<td height='22' align='center'>" + item.StateName + "</td>" +
                        "<td height='22' align='center'>" + item.TypeName + "</td>" +
                        "<td height='22' align='center'>" + item.Remark + "</td>").appendTo($("#chanceList tbody"));
            });
        },
        complete: function() { chanceList("chanceList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
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

//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号
    $("#CustType").val(''); //客户类型
    $("#CustType").attr("title", ''); //客户类型
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
            $("#CustType").val(data.TypeName); //客户类型
            $("#CustType").attr("title", data.CustType); //客户类型
            $("#CustTel").val(data.Tel); //客户类型
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var CodeType = $.trim($("#SellChanceUC_ddlCodeRule").val()); //销售机会编号产生的规则
    var ChanceNo = $.trim($("#SellChanceUC_txtCode").val()); //机会编号
    //var Title = $.trim($("#Title").val()); //机会主题
    var CustName = $.trim($("#CustID").val()); //客户名称
    var SellerName = $.trim($("#UserSeller").val()); //业务员
    var SellDeptId = $("#hiddDeptID").val();      //部门(对部门表ID)
    var FindDate = $.trim($("#FindDate").val()); //发现日期 

    var IntendDate = $.trim($("#IntendDate").val());  //  预期签单日
    var IntendMoney = $.trim($("#IntendMoney").val());  //预期金额

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (CodeType == "") {
        if (ChanceNo == "") {
            isFlag = false;
            fieldText = fieldText + "销售机会编号|";
            msgText = msgText + "请输入销售机会编号|";
        }
        else {
            if (!CodeCheck(ChanceNo)) {
                isFlag = false;
                fieldText = fieldText + "销售机会编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }

//    if (Title == "") {
//        isFlag = false;
//        fieldText = fieldText + "机会主题|";
//        msgText = msgText + "请输入机会主题|";
//    }

    if (CustName == "") {
        isFlag = false;
        fieldText = fieldText + "客户名称|";
        msgText = msgText + "请选择客户|";
    }
    if (SellerName == "") {
        isFlag = false;
        fieldText = fieldText + "业务员|";
        msgText = msgText + "请选择业务员|";
    }
    if (SellDeptId == "") {
        isFlag = false;
        fieldText = fieldText + "部门|";
        msgText = msgText + "请选择部门|";
    }
    if (IntendDate.length == 0) {
        isFlag = false;
        fieldText = fieldText + "预期签单日|";
        msgText = msgText + "请选择预期签单日|";
    }
    if (IntendMoney.length > 0) {
        if (!IsNumeric(IntendMoney, 14, precisionLength)) {
            isFlag = false;
            fieldText = fieldText + "预期金额|";
            msgText = msgText + "预期金额输入有误，请输入有效的数值！|";
        }
    }
    else {
        isFlag = false;
        fieldText = fieldText + "预期金额|";
        msgText = msgText + "请输入预期金额|";
    }
    if (FindDate.length == 0) {
        isFlag = false;
        fieldText = fieldText + "发现日期|";
        msgText = msgText + "请选择发现日期|";
    }


    var DelayDate = $.trim($("#DelayDate").val());  //滞留时间
    var PushDate = $.trim($("#PushDate").val()); //新建阶段日期
    var SellerName1 = $.trim($("#UserSeller1").val()); //业务员
    if (PushDate.length == 0) {
        isFlag = false;
        fieldText = fieldText + "日期|";
        msgText = msgText + "请选择日期|";
    }
    if (SellerName1 == "") {
        isFlag = false;
        fieldText = fieldText + "业务员|";
        msgText = msgText + "请选择销售机会推进历史业务员|";
    }
    if (DelayDate.length > 0) {
        if (!IsZint(DelayDate)) {
            isFlag = false;
            fieldText = fieldText + "滞留时间|";
            msgText = msgText + "滞留时间输入有误，请输入有效的数值！|";
        }
    }
    else {
        isFlag = false;
        fieldText = fieldText + "滞留时间|";
        msgText = msgText + "请输入滞留时间|";
    }

    var Requires = $("#Requires").val(); //竞争产品/方案
    if (!fnCheckStrLen(Requires, 800)) {
        isFlag = false;
        fieldText = fieldText + "需求描述|";
        msgText = msgText + "需求描述最多只允许输入800个字符|";
    }
    var Remark = $("#Remark").val(); //竞争能力
    if (!fnCheckStrLen(Remark, 1024)) {
        isFlag = false;
        fieldText = fieldText + "销售机会备注|";
        msgText = msgText + "销售机会备注最多只允许输入1024个字符|";
    }
    var Remark1 = $("#Remark1").val(); //竞争优势
    if (!fnCheckStrLen(Remark1, 1024)) {
        isFlag = false;
        fieldText = fieldText + "销售机会推荐备注|";
        msgText = msgText + "销售机会推荐备注最多只允许输入1024个字符|";
    }
    
    if( document.getElementById("cbIsMobileNotice").checked == true  )
    {
        var remindTime=$("#txtRemind").val();//提醒时间
        if(remindTime=="" || remindTime==null || remindTime=="undefined" || remindTime=="点此请选择提醒时间")
        {
            isFlag=false;
            fieldText=fieldText+"提醒时间|";
            msgText=msgText+"设置了手机短信提醒时，提醒时间不能为空|";
        }
        else
        {
            remindTime=remindTime+" "+$("#selCompleteDateHour").val()+":00";
            var nowTime=$("#hiddCurenctTime").val();//当前时间
            if(remindTime<nowTime)
            {
                isFlag=false;
                fieldText=fieldText+"提醒时间|";
                msgText=msgText+"手机提醒时间不能小于当前时间|";
            }
        }
        var remindMTel=$.trim($("#RemindMTel").val());//提醒手机号
        if(remindMTel=="" || remindMTel==null || remindMTel=="undefined")
        {
            isFlag=false;
            fieldText=fieldText+"提醒手机号|";
            msgText=msgText+"设置了手机短信提醒时，提醒手机号不能为空|";
        }
        else
        {    
            if(!(/^1[3|4|5|8][0-9]\d{4,8}$/.test(remindMTel)))//判断是否为合法的手机号
            {
                isFlag = false;
                fieldText = fieldText + "提醒手机号|";
                msgText = msgText + RetVal + "请输入正确的手机号码|";
            }
        }
        var remindContent=$.trim($("#RemindContent").val());//提醒内容
        if(remindContent=="" || remindContent==null || remindContent=="undefined")
        {
            isFlag=false;
            fieldText=fieldText+"提醒内容|";
            msgText=msgText+"设置了手机短信提醒时，提醒内容不能为空|";
        }
    }
    
    if (!isFlag) {
        //注：两个方法显示弹出提示信息层,方法一是对字段用红色处理，方法二是所有的提示信息传入未处理颜色

        //方法一
        popMsgObj.Show(fieldText, msgText);

       
    }
    return isFlag;
}

//打印功能
function fnPrintOrder() {
    //if (confirm('请确认页面信息是否已保存！')) {
        var OrderNo = $.trim($("#SellChanceUC_txtCode").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '') {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存您所填的信息！");
        }
        else {

            window.open('../../../Pages/PrinttingModel/SellManager/SellChancePrint.aspx?no=' + OrderNo);
        }
    //}
}

//手机提醒
 function  SetIsMobileNotice(thisCb){
      if( thisCb.checked == true ){
         document.getElementById("txtRemind").disabled = false;
         document.getElementById("selCompleteDateHour").disabled = false;
         document.getElementById("RemindMTel").disabled = false;
         document.getElementById("RemindContent").disabled = false;
         document.getElementById("UserReceiverName").disabled = false;
      }else{
         document.getElementById("txtRemind").value = "点此请选择提醒时间";
         document.getElementById("txtRemind").disabled = true;
         //置已设置过的提醒为空Start
         document.getElementById("selCompleteDateHour").value = "00";
         document.getElementById("RemindMTel").value = "";
         document.getElementById("RemindContent").value = "";
         document.getElementById("UserReceiverName").value = "";
         document.getElementById("hiddReceiverID").value="";
         //置已设置过的提醒为空End
         document.getElementById("selCompleteDateHour").disabled = true;
         document.getElementById("RemindMTel").disabled = true;
         document.getElementById("RemindContent").disabled = true;
         document.getElementById("UserReceiverName").disabled = true;
      }
  }
  