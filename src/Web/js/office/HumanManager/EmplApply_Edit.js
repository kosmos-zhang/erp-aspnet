$(document).ready(function()
{
      requestobj = GetRequest();
      ID=requestobj['ID'];
     // StatusName=requestobj['StatusName'];

      if(typeof(ID)!="undefined")
        InitEmplApplyInfo(ID);
      else
            GetFlowButton_DisplayControl();
 });
function DoPrint()
{
  if(document.getElementById("hidIdentityID").value=="0")
  {
     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请保存后打印");
     return;
  }  
  window.open("../../PrinttingModel/HumanManager/EmplApplyPrint.aspx?Id=" + document.getElementById("hidIdentityID").value);
}
//查看、修改调职申请单
function InitEmplApplyInfo(id)
{
           var retval=id;
           var action="GetEmplApplyInfoById";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:"../../../Handler/Office/HumanManager/EmplApply_Edit.ashx",//目标地址
           data:'ID='+escape(retval)+'&action='+action,
           cache:false,
           beforeSend:function()
           {//AddPop();
           },//发送数据之前           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    $.each(msg.data,function(i,item)
                    {
                        var StatusName=item.FlowStatus;
                        if(StatusName.Trim()=="待审批" || StatusName.Trim()=="审批中" || StatusName.Trim()=="审批通过" || StatusName.Trim()=="审批不通过")
                        { 
                            document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
                            document.getElementById("btnSave").onclick = no;
                        }

                          if (item.Status == "0")//未办理
                            {
                                $("#hidIdentityID").val(id);
                                $("#hiddenBillStatus").val(1);
                                $("#hidBillNo").val(item.EmplApplyNo);
                            }
                            else
                            {
                                $("#hidIdentityID").val(id);
                                $("#hiddenBillStatus").val(2);
                                $("#hidBillNo").val(item.EmplApplyNo);
                            }
                            $("#divCodeNo").show();
                            $("#divCodeRule").hide();
                            //自动生成编号的控件设置为不可见
                              document.getElementById("divCodeNo").value=item.EmplApplyNo;
                              $("#txtTitle").val(item.Title);
                              $("#UserApply").val(item.EmployeeName);
                              $("#hidUserApply").val(item.EmployeeID);
                              $("#txtEnterDate").val(item.EnterDate);
                              $("#txtApplyDate").val(item.ApplyDate);
                              $("#txtHopeDate").val(item.HopeDate);
                              $("#txtApplyType").val(item.ApplyType);
                              $("#txtNowWage").val(item.NowWage);
                              $("#DeptNow").val(item.NowDeptName);
                              $("#hidDeptNow").val(item.NowDeptID);
                              $("#ddlNowQuarter").val(item.NowQuarterID);
                              $("#ctNowQuaterAdmin").val(item.NowAdminLevelID);
                              $("#txtNewWage").val(item.NewWage);
                              $("#DeptNew").val(item.NewDeptName);
                              $("#hidDeptNew").val(item.NewDeptID);
                              $("#ddlNewQuarter").val(item.NewQuarterID);
                              $("#ctNewQuaterAdmin").val(item.NewAdminLevelID);
                              $("#txtReason").val(item.Reason);
                              $("#txtRemark").val(item.Remark);
                              GetFlowButton_DisplayControl();
                   });
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();}//接收数据完毕
           });
}
/*
* 保存按钮
*/
function DoSave()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckInput())
    {
        return;
    }
    //获取人员基本信息参数
    postParams = GetPostParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/EmplApply_Edit.ashx",
        data : postParams,
        dataType:'json',//返回json格式数据
        cache:false,
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
                //设置ID 
                document.getElementById("hidIdentityID").value = data.info;
                document.getElementById("hidBillNo").value = data.data;
                /* 设置编号的显示 */ 
                //显示招聘申请的编号 招聘申请编号DIV可见              
                document.getElementById("divCodeNo").style.display = "block";
                //设置招聘申请编号
                document.getElementById("divCodeNo").value = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                $("#hiddenBillStatus").val(1);
                GetFlowButton_DisplayControl();
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        } 
    }); 
}
function Fun_FlowApply_Operate_Succeed(operateType)
{
    try{
        if(operateType == "0")
        {//提交审批成功后,不可改
            //document.getElementById("btnConfirm").src = "../../../Images/Button/UnClick_qr.jpg";
            document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
            //document.getElementById("btnSave").attachEvent("onclick", "");
            document.getElementById("btnSave").onclick = no;
            
        }
        else if(operateType == "1")
        {//审批成功后，不可改
            document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
            document.getElementById("btnSave").onclick = no;
        }
        else if(operateType == "2")
        {//审批不通过
            if(document.getElementById("txtBillStatusID").value == "1")
            {//如果单据状态为制单，则可改
//                $("#imgSave").css("display", "inline");
//                $("#imgUnSave").css("display", "none");
            }
        }
        else if(operateType == "3")
        {//撤销审批
            document.getElementById("btnSave").src = "../../../Images/Button/Bottom_btn_save.jpg";
            document.getElementById("btnSave").onclick = DoSave;

        }
    }catch(e){
    }
}
function no(){}
/*
* 输入信息校验
*/
function CheckInput()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
     //获取ID
    id = document.getElementById("hidIdentityID").value;
    //新建时，编号选择手工输入时
    if (id == "0")
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codeRule_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            codeNo = document.getElementById("codeRule_txtCode").value;
            //编号必须输入
            if (codeNo == "")
            {
                isErrorFlag = true;
                fieldText += "调职申请编号|";
   		        msgText += "请输入调职申请编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "调职申请编号|";
                    msgText += "调职申请编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    
    //主题
    title = document.getElementById("txtTitle").value;
    if (title == "" || title == null)
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "请输入主题|";  
    }
    //申请人必须输入
    applyUser = document.getElementById("hidUserApply").value;
    if (applyUser == "" || applyUser == null)
    {
        isErrorFlag = true;
        fieldText += "申请人|";
        msgText += "请输入申请人|";  
    }
    //入职时间必须输入
    enterDate = document.getElementById("txtEnterDate").value;
    if (enterDate == "" || enterDate == null)
    {
        isErrorFlag = true;
        fieldText += "入职时间|";
        msgText += "请输入入职时间|";  
    }
    //申请日期必须输入
    applyDate = document.getElementById("txtApplyDate").value;
    if (applyDate == "" || applyDate == null)
    {
        isErrorFlag = true;
        fieldText += "申请日期|";
        msgText += "请输入申请日期|";  
    }
    //目前部门
    deptNow = document.getElementById("hidDeptNow").value;
    if (deptNow == "" || deptNow == null)
    {
        isErrorFlag = true;
        fieldText += "目前部门|";
        msgText += "请输入目前部门|";  
    }
    //目前工资
//    nowWage = document.getElementById("txtNowWage").value;
//    if (nowWage == "" || nowWage == null)
//    {
//        isErrorFlag = true;
//        fieldText += "目前工资|";
//        msgText += "请输入目前工资|";  
//    }
//    else if (!IsNumeric(nowWage, 6, 2))
//    {
//        isErrorFlag = true;
//        fieldText += "目前工资|";
//        msgText += "请输入正确的目前工资|";  
//    }
    //调至部门
    deptNew = document.getElementById("hidDeptNew").value;
    if (deptNew == "" || deptNew == null)
    {
        isErrorFlag = true;
        fieldText += "调至部门|";
        msgText += "请输入调至部门|";  
    }
    //调至工资
//    newWage = document.getElementById("txtNewWage").value;    
//    if (newWage == "" || newWage == null)
//    {
//        isErrorFlag = true;
//        fieldText += "调至工资|";
//        msgText += "请输入调至工资|";  
//    }
//    else if (!IsNumeric(newWage, 6, 2))
//    {
//        isErrorFlag = true;
//        fieldText += "调至工资|";
//        msgText += "请输入正确的调至工资|";  
//    }
    //事由
    reason = document.getElementById("txtReason").value;
    if (reason == "" || reason == null)
    {
        isErrorFlag = true;
        fieldText += "事由|";
        msgText += "请输入事由|";  
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
    }
    
    return isErrorFlag;
}

/*
* 获取提交的基本信息
*/
function GetPostParams()
{
    //获取ID       
    id = document.getElementById("hidIdentityID").value;
    var strParams = "ID=" + id;//ID
    var no = "";
    //更新的时候
    if (id != "0")
    {
        //编号
        no = document.getElementById("divCodeNo").value;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codeRule_ddlCodeRule").value;
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘申请编号
            no = document.getElementById("codeRule_txtCode").value;
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("codeRule_ddlCodeRule").value;
        }
    }
    //编号
    strParams += "&EmplApplyNo=" + no;
    //主题
    strParams += "&Title=" + escape(document.getElementById("txtTitle").value);
    //申请人
    strParams += "&EmployeeID=" + document.getElementById("hidUserApply").value;
    //入职时间
    strParams += "&EnterDate=" + document.getElementById("txtEnterDate").value;
    //申请日期
    strParams += "&ApplyDate=" + document.getElementById("txtApplyDate").value;
    //希望日期
    strParams += "&HopeDate=" + document.getElementById("txtHopeDate").value;
    //申报类别
    strParams += "&ApplyType=" + escape(document.getElementById("txtApplyType").value);
    //目前部门
    strParams += "&NowDeptID=" + document.getElementById("hidDeptNow").value;
    //目前岗位
    strParams += "&NowQuarterID=" + document.getElementById("ddlNowQuarter").value;
    //目前岗位职等
    strParams += "&NowAdminLevelID=" + document.getElementById("ctNowQuaterAdmin_ddlCodeType").value;
    //目前工资
    strParams += "&NowWage=" + document.getElementById("txtNowWage").value;
    //调至部门
    strParams += "&NewDeptID=" + document.getElementById("hidDeptNew").value;
    //调至岗位
    strParams += "&NewQuarterID=" + document.getElementById("ddlNewQuarter").value;
    //调至岗位职等
    strParams += "&NewAdminLevelID=" + document.getElementById("ctNewQuaterAdmin_ddlCodeType").value;
    //调至工资
    strParams += "&NewWage=" + document.getElementById("txtNewWage").value;
    //事由
    strParams += "&Reason=" + escape(document.getElementById("txtReason").value);
    //备注
    strParams += "&Remark=" + escape(document.getElementById("txtRemark").value); 
    var action="SaveEmplApplyInfo";
    strParams += "&action=" + escape(action); 
    return strParams;
}
/*
* 返回按钮
*/
function DoBack()
{
    var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['Flag']; //是否从列表页面进入
        if (flag != null && typeof (flag) != "undefined") {
//获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    window.location.href = "EmplApply_Info.aspx?ModuleID=" + ModuleID + searchCondition;        }
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
//确认按钮
function Fun_ConfirmOperate() 
{
    if(confirm("是否确认！"))
    {

        var ID=$("#hidIdentityID").val();
        var action="ConfirmEmplApplyInfo";
         $.ajax({ 
              type: "POST",
              url: "../../../Handler/Office/HumanManager/EmplApply_Edit.ashx",
              data:'ID='+ID+'\
                    &action='+escape(action),
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
            success:function(data) 
            { 
                if(data.sta==1) 
                { 
                     //isnew="2";
                     updateconfirm(data.info);
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
                } 
                else
                { 
                  hidePopup();
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认失败！");
                } 
            } 
           });
           }
           else return false;
}
//确认后处理
function updateconfirm(id)
{
    //$("#CarApplyNo_txtCode").attr("value",no);
    $("#hidIdentityID").val(id);
   // $("#hidBillNo").val(item.EmplApplyNo);
    $("#hiddenBillStatus").attr("value","2");
    //$("#BillStatus").attr("value","已处理");//单据状态
    document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
    document.getElementById("btnSave").onclick = no;
    GetFlowButton_DisplayControl();
    //$("#CarNo").attr("disabled",true);
 }
//取消确认
function Fun_UnConfirmOperate()
{
    if(confirm("是否取消确认！"))
    {

        var action="UpdateMoveApplyCancelConfirmInfo";
        var ID=$("#hidIdentityID").val();
        var EmpNo=document.getElementById("divCodeNo").value;
         $.ajax({ 
              type: "POST",
              url: "../../../Handler/Office/HumanManager/EmplApply_Edit.ashx",
              data:'ID='+ID+'&EmpNo='+escape(EmpNo)+'\
                    &action='+escape(action),
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
            success:function(data) 
            { 
                if(data.sta==2) 
                { 
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","此调职申请单已经被引用，不能取消确认");
                     return;
                } 
                if(data.sta==1) 
                { 
                     CancelConfirm(data.info);
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认成功！");
                } 
                else if(data.sta==0)
                { 
                  hidePopup();
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认失败！");
                } 
            } 
           });
           }
           else return false;
}
//取消确认后处理
function CancelConfirm(id)
{
    $("#hidIdentityID").val(id);
    $("#hiddenBillStatus").attr("value","1");
    document.getElementById("btnSave").src = "../../../Images/Button/Bottom_btn_save.jpg";
    document.getElementById("btnSave").onclick = DoSave;

    GetFlowButton_DisplayControl();
}
