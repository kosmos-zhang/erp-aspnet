var treeview1 = null;
var treeview2 = null;

$(document).ready(function() {
    init();   
    $("#chanceTypeUC_ddlCodeType").css("width", "120px");
    $("#HapSourceUC_ddlCodeType").css("width", "120px");
    $("#FeasibilityUC_ddlCodeType").css("width", "120px");

 GetExtAttr("officedba.SellChance",null);//销售计划扩展属性

    //是否显示返回按钮
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var orderID = requestObj['id']; //销售机会Id
        var requestobj = GetRequestToHidden();
        requestobj = requestobj.replace('ModuleID=2031201', 'ModuleID=2031202');
        document.getElementById("HiddenURLParams").value = requestobj;
        if (orderID != null) {
            fnGetInfo(orderID);
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

//获取销售机会详细信息
function fnGetInfo(orderID) {

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
                $("#ChanceNo").val(data.data[0].ChanceNo); //机会编号
                $("#CustID").attr("title", data.data[0].CustID);     //客户ID（对应客户表ID）
                $("#CustID").val(data.data[0].CustName);     //客户ID（对应客户表ID）
                $("#CustType").attr("title", data.data[0].CustType);   //客户类型ID
                $("#CustType").val(data.data[0].TypeName);   //客户类型ID
                $("#CustTel").val(data.data[0].CustTel);    //客户联系电话
                $("#Title").val(data.data[0].Title);      //销售机会主题
                $("#chanceTypeUC_ddlCodeType").val(data.data[0].ChanceType); //销售机会类型(对应分类代码表ID)
                $("#HapSourceUC_ddlCodeType").val(data.data[0].HapSource);  //销售机会来源ID(对应分类代码表ID)
                $("#hiddSeller").val(data.data[0].Seller);     //业务员(对应员工表ID)
                $("#hiddDeptID").val(data.data[0].SellDeptId); //部门(对部门表ID)
                $("#FindDate").val(data.data[0].FindDate);   //发现日期
                $("#ProvideMan").val(data.data[0].ProvideMan); //提供人
                $("#Requires").val(data.data[0].Requires);   //需求描述
                $("#IntendDate").val(data.data[0].IntendDate); //预计签单日期
                $("#IntendMoney").val(FormatAfterDotNumber(data.data[0].IntendMoney, precisionLength)); //预期金额
                $("#Remark").val(data.data[0].Remark);     //备注
                $("#IsQuoted").val(data.data[0].IsQuotedText);   //是否被报价(0-否，1-是)
                $("#UserSeller").val(data.data[0].EmployeeName); //业务员
                $("#DeptId").val(data.data[0].DeptName); //部门
                $("#Creator").val(data.data[0].CreatorName); //制单人
                $("#ModifiedUserID").val(data.data[0].ModifiedUserID); //最后更新人
                $("#CreateDate").val(data.data[0].CreateDate); //制单人
                $("#ModifiedDate").val(data.data[0].ModifiedDate); //制单人
                $("#CanViewUserNameHidden").val(data.data[0].CanViewUser); //制单人
                $("#CanViewUserName").val(data.data[0].CanViewUserName); //制单人
                if(data.data[0].RemindTime !="" && data.data[0].RemindTime!=null)
                {
                    var temp=data.data[0].RemindTime;
                    var timearr=new Array();
                    timeArr= temp.split(' ');
                    $("#txtRemind").val(data.data[0].ReimndTimeOnly); //提醒时间
                    if(timearr[1]!="" && timeArr[1]!=null)
                    {
                        $("#selCompleteDateHour").val(timeArr[1].split(':')[0]);//小时
                    }
                    $("#RemindMTel").val(data.data[0].RemindMTel); //提醒手机号
                    $("#RemindContent").val(data.data[0].RemindContent); //提醒内容
                    $("#UserReceiverName").val(data.data[0].ReceiverName); //接收人名称
                    $("#hiddReceiverID").val(data.data[0].ReceiverID); //接收人ID
                }
                if(data.data[0].IsMobileNotice=="1")
                {
                    document.getElementById("cbIsMobileNotice").checked = true;//设置手机提醒
                }
                else
                {
                    document.getElementById("cbIsMobileNotice").checked = false;
                }
                SetIsMobileNotice(document.getElementById("cbIsMobileNotice"));//

                ExtAttControl_FillValue(data.data[0]);//扩展属性
                
                fnGetPush(data.data[0].ChanceNo);
            }
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


function InsertChanceData() {
    if (!CheckInput()) {
        return;
    }
    var CodeType = ''; //销售机会编号产生的规则
    var ChanceNo = $("#ChanceNo").val(); //机会编号
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
    var IntendDate = $("#IntendDate").val(); //预计签单日期                    
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
    var str = $("#hiddAcction").val();
    var IsMobileNotice=0;
    if( document.getElementById("cbIsMobileNotice").checked == true  )
    {
       IsMobileNotice="1" ;     
    }
    else
    {
       IsMobileNotice= "0";  
    }
    //var RemindTime=document.getElementById("txtRemind").value+" "+document.getElementById("selCompleteDateHour").value+":00";//提醒时间
    var RemindTime="";
    var txtRemindTime=document.getElementById("txtRemind").value;
    if(txtRemindTime!="" && txtRemindTime!=null && txtRemindTime!="undefined" && txtRemindTime!="点此请选择提醒时间")
    {
        RemindTime= document.getElementById("txtRemind").value+" "+document.getElementById("selCompleteDateHour").value+":00";//提醒时间
    }
    var RemindMTel=$.trim($("#RemindMTel").val());//提醒手机号
    var RemindContent=$.trim($("#RemindContent").val());//提醒内容
    var ReceiverID=$("#hiddReceiverID").val();//接收人 
    var requestObj = GetRequest(); //参数列表 
    var billID=requestObj['id']; //销售机会Id

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
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功！");
                fnGetPush(data.no);
                //是否显示返回按钮
                //var requestObj = GetRequest(); //参数列表
                //var orderID = requestObj['id']; //销售机会Id
                $("#sellChanceID").val(data.id);
                fnGetInfo(data.id);
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
                        "<td height='22' align='center'>" + item.PushDate + "</td>" +
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                        "<td height='22' align='center'>" + item.StateName + "</td>" +
                        "<td height='22' align='center'>" + item.TypeName + "</td>" +
                        "<td height='22' align='center'>" + item.Remark + "</td>").appendTo($("#chanceList tbody"));
                $("#PushDate").val(item.PushDate);   //日期
                $("#hiddSeller1").val(item.Seller);     //业务员ID(对应员工表ID)
                $("#UserSeller1").val(item.EmployeeName);     //业务员ID(对应员工表ID)
                $("#Phase").val(item.Phase);      //阶段（1初期沟通、2立项评估、3需求分析、4方案指定、5招投标/竞争、6商务谈判、7合同签约）
                $("#State").val(item.State);      //状态（1跟踪2成功3失败4搁置5失效）
                $("#FeasibilityUC_ddlCodeType").val(item.Feasibility); //机会可能性ID(对应分类代码表ID)
                $("#DelayDate").val(item.DelayDate);  //阶段滞留时间(天)
                $("#Remark1").val(item.Remark);     //阶段备注       
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
    //var Title = $.trim($("#Title").val()); //机会主题
    var CustName = $.trim($("#CustID").val()); //客户名称
    var SellerName = $.trim($("#UserSeller").val()); //业务员

    var FindDate = $.trim($("#FindDate").val()); //发现日期
    var SellDeptId = $("#hiddDeptID").val();      //部门(对部门表ID)
    var IntendDate = $.trim($("#IntendDate").val());  //  预计签单日
    var IntendMoney = $.trim($("#IntendMoney").val());  //预计金额

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
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
        var OrderNo = $.trim($("#ChanceNo").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '') {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存您所填的信息！");
        }
        else {

            window.open('../../../Pages/PrinttingModel/SellManager/SellChancePrint.aspx?no=' + OrderNo);
        }
   // }
}

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
  