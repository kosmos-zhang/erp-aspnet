

$(document).ready(function()
{
      alerDeptQuarter();
      requestobj = GetRequest();
      ID=requestobj['ID'];
      if(typeof(ID)=="undefined")
            GetFlowButton_DisplayControl();
});

   String.prototype.length2 = function() {
    var cArr = this.match(/[^\x00-\xff]/ig);
    return this.length + (cArr == null ? 0 : cArr.length);}

 //alert(document.getElementById('hiddenBillStatus').value);
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
    else if(CheckGoalInfo())
    {
        return;
     
    } 
    //获取人员基本信息参数
    postParams ="Action=Save&" +GetPostParams();
      postParams += GetGoalParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectApply_Edit.ashx",
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
                //设置编辑模式 data.info
                document.getElementById("hidEditFlag").value = "UPDATE";
                /* 设置编号的显示 */ 
                //显示招聘申请的编号 招聘申请编号DIV可见              
                document.getElementById("divRectApplyNo").style.display = "block";
                //设置招聘申请编号
                document.getElementById("divRectApplyNo").innerHTML = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                //设置ID 
              
                document.getElementById("txtIndentityID").value = data.info;
                 
                document.getElementById("hidBillNo").value = data.data;
                $("#hiddenBillStatus").val(1);
                //alert(document.getElementById('txtIndentityID').value);
                GetFlowButton_DisplayControl();
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请选择未使用的编号!");
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        } 
    }); 
}

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
     //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
        var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isErrorFlag = true;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    //新建时，编号选择手工输入时
    if ("INSERT" == editFlag)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codruleRectApply_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            codeNo = document.getElementById("codruleRectApply_txtCode").value.Trim();
            //编号必须输入
            if (codeNo == "")
            {
                isErrorFlag = true;
                fieldText += "申请编号|";
   		        msgText += "请输入申请编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "申请编号|";
                    msgText += "申请编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    
    //申请部门必须输入
    deptID = document.getElementById("hidDeptID").value.Trim();
    if (deptID == "" || deptID == null)
    {
        isErrorFlag = true;
        fieldText += "申请部门|";
        msgText += "请输入申请部门|";  
    }
//    //招聘人数
//    rectCount = document.getElementById("txtRectCount").value;
//    if (rectCount == "" || rectCount == null)
//    {
//        isErrorFlag = true;
//        fieldText += "需求人数|";
//        msgText += "请输入需求人数|";  
//    }
//    else if (!IsNumber(rectCount))
//    {
//        isErrorFlag = true;
//        fieldText += "需求人数|";
//        msgText += "请输入正确的需求人数|";  
//    }
  
    
       NowNum = document.getElementById("txtNowNum").value.Trim();
    if (NowNum == "" || NowNum == null)
    {
    }
    else if (!IsNumber(NowNum))
    {
        isErrorFlag = true;
        fieldText += "现有人数|";
        msgText += "请输入正确的现有人数|";  
    }
         MaxNum = document.getElementById("txtMaxNum").value.Trim();
    if (MaxNum == "" || MaxNum == null)
    {
    }
    else if (!IsNumber(MaxNum))
    {
        isErrorFlag = true;
        fieldText += "编制定额|";
        msgText += "请输入正确的编制定额|";  
    }
    
//   if (textcontrol('txtWorkNeeds','500'))
//  {
//              isErrorFlag = true;
//        fieldText += "工作要求|";
//        msgText += "工作要求最多只允许输入500个字符！|"; 
//  }
  
 
    
    
//       if (textcontrol('txtOtherAbility','500'))
//  {
//              isErrorFlag = true;
//        fieldText += "其他能力|";
//        msgText += "其他能力最多只允许输入500个字符！|"; 
//  }
    
    
//       if (textcontrol('txtRemark','250'))
//  {
//              isErrorFlag = true;
//        fieldText += "其他能力|";
//        msgText += "其他能力最多只允许输入250个字符！|"; 
//  }
     var txtRemark = document.getElementById("txtRemark").value.Trim();
    if(strlen(txtRemark)> 500)
    {
        isFlag = true;
        fieldText = fieldText + "备注|";
   		msgText = msgText + "备注最多只允许输入500个字符|";
    }
    
//         if (textcontrol('txtRemark','100'))
//  {
//              isErrorFlag = true;
//        fieldText += "招聘原因|";
//        msgText += "招聘原因最多只允许输入100个字符！|"; 
//  }
 var txtRequstReason = document.getElementById("txtRequstReason").value.Trim();
    if(strlen(txtRequstReason)> 200)
    {
        isFlag = true;
        fieldText = fieldText + "招聘原因|";
   		msgText = msgText + "招聘原因最多只允许输入200个字符|";
    }
  
  
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
    }
    
    return isErrorFlag;
}


  function textcontrol(taId,maxSize) 
  {   
                // 默认 最大字符限制数   
                var defaultMaxSize = 250;   
                var ta = document.getElementById(taId);   
                // 检验 textarea 是否存在   
               if(!ta) {   
                          return;   
                }   
                // 检验 最大字符限制数 是否合法   
                if(!maxSize) {   
                   maxSize = defaultMaxSize;   
               } else {   
                    maxSize = parseInt(maxSize);   
                    if(!maxSize || maxSize < 1) {   
                        maxSize = defaultMaxSize;   
                   }   
               }   
               　　 if (ta.value.length2() > maxSize) {   
                   ta.value=ta.value.substring(0,maxSize);   
                   return true ;
               }    
 } 
/*
* 获取提交的基本信息
*/
function GetPostParams()
{
       
    editFlag = document.getElementById("hidEditFlag").value;
    var strParams = "EditFlag=" + editFlag;//编辑标识
    var no = "";
    //更新的时候
    if ("UPDATE" == editFlag)
    {
        //编号
        no = document.getElementById("divRectApplyNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codruleRectApply_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘申请编号
            no = document.getElementById("codruleRectApply_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("codruleRectApply_ddlCodeRule").value.Trim();
        }
    }
    //编号
    strParams += "&RectApplyNo=" +  no;
        strParams += "&ID=" +document.getElementById("txtIndentityID").value.Trim();
    //部门ID
    strParams += "&DeptID=" +  escape ( document.getElementById("hidDeptID").value.Trim());
    //现有人数
    strParams += "&NowNum=" + escape ( document.getElementById("txtNowNum").value.Trim());
   //编制定额
    strParams += "&MaxNum=" + escape ( document.getElementById("txtMaxNum").value.Trim());
//       //招聘岗位
//    strParams += "&JobName=" +escape (  document.getElementById("DeptQuarter").value);
//        strParams += "&JobID=" +escape (  document.getElementById("DeptQuarterHidden").value);
       //需求人数
    strParams += "&RectCount=" +escape ( document.getElementById("txtRequireNum").value.Trim());
       //招聘原因
    strParams += "&RequstReason=" +escape (  document.getElementById("txtRequstReason").value.Trim());
     
       //备注
    strParams += "&Remark=" + escape ( document.getElementById("txtRemark").value.Trim());
    
 
            strParams += "&Principal=" + escape ( document.getElementById("txtPrincipal").value.Trim());
    

    return strParams;
}

function Back()
{ 
   // window.location.href=page+'?CustName='+CustName+'&LoveType='+LoveType+
   //                     '&LoveBegin='+LoveBegin+'&LoveEnd='+LoveEnd+'&Title='+Title+'&CustLinkMan='+CustLinkMan;
    
//    window.location.href=page+'?ContractNo1='+ContractNo1+'&Title='+Title+
//                        '&TypeID='+TypeID+'&DeptID='+DeptID+'&Seller='+Seller+'&FromType='+FromType+'&ProviderID='+ProviderID+'&BillStatus='+BillStatus+'&UsedStatus='+UsedStatus;

    if(document.getElementById("hidIsliebiao").value.Trim() == "")
    {
        window.location.href='../../../Desktop.aspx';
    }
    else
    {
var URLParams = document.getElementById("hidSearchCondition").value;
    window.location.href='PurchaseContractInfo.aspx?'+URLParams;
    }
}
/*
* 返回按钮
*/
function DoBack()
{
  var requestObj = GetRequest(); //参数列表
    if (requestObj != null) {
        var flag = requestObj['hidIsliebiao']; //是否从列表页面进入
        if (flag != null && typeof (flag) != "undefined") {
         var searchCondition = document.getElementById("hidSearchCondition").value;
    var ModuleID = document.getElementById("hidModuleID").value;
    window.location.href = "RectApply_Info.aspx?ModuleID=" + ModuleID + searchCondition;
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
//   if(document.getElementById("hidIsliebiao").value == "")
//    {
//        window.location.href='../../../Desktop.aspx';
//    }
//    else
//    {
//    //获取查询条件
//    var searchCondition = document.getElementById("hidSearchCondition").value;
//    //获取模块功能ID
//    var ModuleID = document.getElementById("hidModuleID").value;
//    window.location.href = "RectApply_Info.aspx?ModuleID=" + ModuleID + searchCondition;
//    }
}

function Fun_UnConfirmOperate()
{

    var ID=$("#txtIndentityID").val();
     
  var status ='1';
       var postParams = "Action=UpdateMoveApplyCancelConfirmInfo&ID="+ID +"&Status=" + status + "&ReportNo=" + document.getElementById("divRectApplyNo").innerHTML;
    //审批处理
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectApply_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
           if(data.sta==1) 
                { 
                      //document .getElementById ("btnSave").style.display="block";
                      
                        document.getElementById("btnSave").src = "../../../Images/Button/Bottom_btn_save.jpg";
                        document.getElementById("btnSave").attachEvent("onclick", DoSave);
        
                     CancelConfirm(data.info);
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认成功！");
                }else
                {
                      hidePopup();
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认失败！");
                } 
        
        
        //显示保存按钮
//           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认成功！");
//           CancelConfirm(id)
        } 
    }); 




}

function Uncheck()
{

var status ='2';
     var postParams = "Action=FlowOperate&Dostatus=3&Status=" + status + "&ReportNo=" + document.getElementById("divRectApplyNo").innerHTML;
    //审批处理
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectApply_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
        $("#hidCloser").attr("value",'');
                $("#txtCloser").attr("value",'');
                $("#txtCloseDate").attr("value",'');
         $("#txtBillStatus").val("执行");
           var ID=$("#txtIndentityID").val();
      CancelUnConfirm(ID);
//           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单成功！");
       
//           $("#hiddenBillStatus").val(4);
//            GetFlowButton_DisplayControl();
        } 
    }); 
    
 
}
function CancelUnConfirm(id)
{
    $("#hidIdentityID").val(id);
    $("#hiddenBillStatus").attr("value","2");
    //alert(document.getElementById('hiddenBillStatus').value);
    $("#txtBillStatus").val("执行");
    GetFlowButton_DisplayControl();
}

//取消确认后处理
function CancelConfirm(id)
{
    $("#hidIdentityID").val(id);
    $("#hiddenBillStatus").attr("value","1");
    $("#txtBillStatus").val("制单");
         $("#hidConfirmor").attr("value",'');
                $("#txtConfirmor").attr("value",'');
                $("#txtConfirmDate").attr("value",'');
    GetFlowButton_DisplayControl();
}

function     Fun_ConfirmOperate()
 { 
    if(!confirm(" 是否确认?"))
      {
      return ;
      }
   var status ='2';
     var postParams = "Action=FlowOperate&Dostatus=1&Status=" + status + "&ReportNo=" + document.getElementById("divRectApplyNo").innerHTML;
    //审批处理
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectApply_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
                var aaa = data.data.split('#');
                $("#hidConfirmor").attr("value",aaa[0]);
                $("#txtConfirmor").attr("value",aaa[1]);
                $("#txtConfirmDate").attr("value",aaa[2]);
                
                
                
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
           $("#hiddenBillStatus").val(2);
           $("#txtBillStatus").val("执行");
            GetFlowButton_DisplayControl();
        } 
    }); 
 }
 
 
 function Close()
 {  var status ='4';
     var postParams = "Action=FlowOperate&Dostatus=2&Status=" + status + "&ReportNo=" + document.getElementById("divRectApplyNo").innerHTML;
    //审批处理
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectApply_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
                        var aaa = data.data.split('#');
                $("#hidCloser").attr("value",aaa[0]);
                $("#txtCloser").attr("value",aaa[1]);
                $("#txtCloseDate").attr("value",aaa[2]);
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单成功！");
           $("#txtBillStatus").val("手工结单");
           $("#hiddenBillStatus").val(4);
            GetFlowButton_DisplayControl();
        } 
    }); 
    }


 function Fun_CompleteOperate(flag)
 {
    if(flag)
    {
        if(confirm("是否确认结单？"))
           Close();
    }
   else 
   {
        if(confirm("是否确认取消结单？"))
            Uncheck();
   }
 }

 
/*
* 提交审批
*/
function Fun_FlowApply_Operate_Succeed(operateType)
{
    var status = "1";
      //提交审批成功后
    if (operateType == 0)
    {
        status = "1";
//         document .getElementById ("btnSave").style.display="none";
        document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
        document.getElementById("btnSave").attachEvent("onclick","" );


    }
    //审批成功后
     else   if (operateType == 1)
    {
        status = "1";
//         document .getElementById ("btnSave").style.display="none";
         document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
         document.getElementById("btnSave").attachEvent("onclick","" );
         
    }//审批不通过 成功后
    else if (operateType == 2)
    {
        status = "1";
//        document .getElementById ("btnSave").style.display="none";
        document.getElementById("btnSave").src = "../../../Images/Button/UnClick_bc.jpg";
        document.getElementById("btnSave").attachEvent("onclick", "");
     
    }//撤消审批 成功后
    else if (operateType == 3)
    {
        status = "1";
       $("#hidCloser").attr("value",'');
                $("#txtCloser").attr("value",'');
                $("#txtCloseDate").attr("value",'');
                    $("#hidConfirmor").attr("value",'');
                $("#txtConfirmor").attr("value",'');
                $("#txtConfirmDate").attr("value",'');
//        document .getElementById ("btnSave").style.display="block";
        document.getElementById("btnSave").src = "../../../Images/Button/Bottom_btn_save.jpg";
        
        
    }
    var postParams = "Action=FlowOperate&Status=" + status + "&Dostatus=3&ReportNo=" + document.getElementById("divRectApplyNo").innerHTML;
    //审批处理
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectApply_Edit.ashx",
        dataType:"json",//返回json格式数据
        data: postParams,
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
      
        } 
    }); 
}

function Print()
{
var RectApplyNo=document .getElementById ("divRectApplyNo").innerHTML.Trim();
if (RectApplyNo ==null || RectApplyNo =='')
{
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请确认页面信息是否已保存！");
}
else
{
   
 window.open( "RectApplyEditReport.aspx?rectApplyID=" + RectApplyNo) ;
 
// window.showModalDialog("RectApplyEditReport.aspx?rectApplyID=" + RectApplyNo,'',"dialogWidth=1600px;dialogHeight=1200px");
}
}

/*
* 删除表格行
*/
function DeleteRows(tableName){
    //获取表格
    table = document.getElementById(tableName);
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById(tableName + "_chkSelect_" + row);
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	    if (chkControl.checked)
	    {
	       //删除行，实际是隐藏该行
	       table.rows[row].style.display = "none";
	    }
	}
}
/*
* 全选择表格行
*/
function SelectAll(tableName){
    //获取表格
    table = document.getElementById(tableName);
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	var isSelectAll = document.getElementById(tableName + "_chkAll").checked;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById(tableName + "_chkSelect_" + row);
	    //全选择
	    if (isSelectAll)
	    {
	        chkControl.checked = true;
	    }
	    else
	    {
	        chkControl.checked = false;
	    }
	}
}

function AddGoal(){
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
    //获取行号
    var no = table.rows.length;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='tblRectGoalDetailInfo_chkSelect_" + no + "'>"
	    + "<input type='hidden' id='hidDeptID_" + no + "' value=''>";
	    
	    
	//岗位
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	      
                     
	objTD.innerHTML = "<input type=\"hidden\" id=\"DeptQuarter" + no + "Hidden\" /> <input id=\"DeptQuarter" + no + "\" type=\"text\"  reado     maxlength =\"30\" class=\"tdinput\"       onclick =\"treeveiwPopUp.show()\" readonly=\"readonly\" />     ";
		//职务说明
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '100' size='10' class='tdinput'  id='txtJobDescripe_" + no + "'  ondblclick   ='alertContent(this.id)'>";
	//需求人数
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '3' size='10' class='tdinput' size=\"3\" id='txtPersonCount_" + no + "' onkeydown='Numeric_OnKeyDown();'    onchange='GetRequireNum();'>";
	//最迟上岗时间
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '10' size='10' readonly class='tdinput' id='txtUsedDate_" + no + "' onclick=\"J.calendar.get();\">";
			//工作地点
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '100' size='10' class='tdinput'  id='txtWorkPlace_" + no + "'  ondblclick   ='alertContent(this.id)'>";
		//工作性质
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
		objTD.innerHTML = "<select class='tdinput'id='ddlWorkNature_" + no + "'>" + document.getElementById("divWorkNature").innerHTML + "</select>";
	//性别
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<select class='tdinput' id='ddlSex_" + no 
	        + "'><option value='3' selected>不限</option><option value='1'>男</option><option value='2'>女</option></select>";
	//起始年龄
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '3' size='3' class='tdinput' id='txtMinAge_" + no + "' onkeydown='Numeric_OnKeyDown();' >";
		//截止年龄
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '3' size='3' class='tdinput' id='txtMaxAge_" + no + "' onkeydown='Numeric_OnKeyDown();' >";
		//专业
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength = '50' class='tdinput' id='ddlProfessional_" + no + "'>";
	objTD.innerHTML = "<select class='tdinput'id='ddlProfessional_" + no + "'>" + document.getElementById("ddlProfessional_ddlCodeType").innerHTML + "</select>";
		//学历       
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<select class='tdinput'id='ddlCultureLevel_" + no + "'>" + document.getElementById("ddlCulture_ddlCodeType").innerHTML + "</select>";
	//工作年限
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
		objTD.innerHTML = "<select class='tdinput'id='ddlWorkAge_" + no + "'>" + document.getElementById("divWorkAge").innerHTML + "</select>";
	//工作要求
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '1000' size='10' class='tdinput' id='txtRequisition_" + no + "' ondblclick   ='alertContent(this.id)'>";
		//其他要求
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '1000' size='10' class='tdinput' id='txtOtherAbility_" + no + "' ondblclick   ='alertContent(this.id)'>";
		//可提供的待遇
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '1000' size='10' class='tdinput' id='txtSalaryNote_" + no + "' ondblclick   ='alertContent(this.id)'>";
	
//	//计划完成时间
//	objTD = objTR.insertCell(-1);
//	objTD.className = "tdColInput";
//	objTD.innerHTML = "<input type='text' maxlength = '10' size='10' readonly class='tdinput' id='txtCompleteDate_" + no + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCompleteDate_" + no + "')})\">";

}

/*
* 招聘目标校验
*/
function GetRequireNum()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var errorRow = 0;
    //开始时间
 var sum=0;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            errorRow++;
    
            
	     
	        //人员数量
            personCount = document.getElementById("txtPersonCount_" + i).value.Trim();        
                
            //判断是否输入
            if(personCount == "")
            {
                 personCount =0;
            }
            else if (!IsZint(personCount))
            {
                isErrorFlag = true;
                fieldText += "人员需求 行：" + errorRow + " 需求人数|";
                msgText += " 需求数量输入有误，请输入有效的数值（整数）|";  
                break ;
            }
            else
            {
            sum =sum +parseInt (personCount ,10);
            }
   
	    }
	}
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }else
    {
    document .getElementById ("txtRequireNum").value=sum ;
    }

 
}


/*
* 招聘目标校验
*/
function CheckGoalInfo()
{
 
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var errorRow = 0;
    //开始时间
//    var startDate = document.getElementById("txtStartDate").value;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
//	alert (table.rows[i].style.display);
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            errorRow++;
          
            
	        //职务名称
            positionTitile = document.getElementById("DeptQuarter"+i+"Hidden").value.Trim();            
            //判断是否输入
            if(positionTitile == "")
            {
                isErrorFlag = true;
                fieldText += "人员需求 行：" + errorRow + " 岗位名称|";
                msgText += "请选择岗位名称|";  
            }
            
             //职务说明
            JobDescripe = document.getElementById("txtJobDescripe_"+i ).value.Trim();         
            if (JobDescripe != "" && JobDescripe != null && strlen(JobDescripe) > 100)
             {
            isErrorFlag = true;
                 fieldText += "人员需求 行：" + errorRow + " 职务说明|";
            msgText += "职务说明最多只允许输入100个字符！|";  
            }
	        //人员数量
            personCount = document.getElementById("txtPersonCount_" + i).value.Trim();            
            //判断是否输入
            if(personCount == "")
            {
                isErrorFlag = true;
                fieldText += "人员需求 行：" + errorRow + " 需求人数|";
                msgText += "请输入需求人数|";  
            }
            else if (!IsNumber(personCount))
            {
                isErrorFlag = true;
                fieldText += "人员需求 行：" + errorRow + " 需求人数|";
                msgText += "请输入正确的需求数量|";  
            }
                //最迟上岗时间
            completeDate = document.getElementById("txtUsedDate_" + i).value.Trim();            
            //判断是否输入
            if(completeDate != "" && !isDate(completeDate))
            {
                isErrorFlag = true;
                fieldText += "人员需求 行：" + errorRow + " 最迟上岗时间|";
                msgText += "请输入正确的最迟上岗时间|";  
            } 
              //工作地点
            WorkPlace = document.getElementById("txtWorkPlace_"+i ).value.Trim();         
            if (WorkPlace != "" && WorkPlace != null && strlen(WorkPlace) > 100)
             {
            isErrorFlag = true;
      fieldText += "人员需求 行：" + errorRow + " 工作地点|";
            msgText += "工作地点最多只允许输入100个字符！|";  
            }
               //起始年龄
            MinAge = document.getElementById("txtMinAge_" + i).value.Trim();            
            //判断是否输入
            if(MinAge == "")
            { 
            }
            else if (!IsNumber(MinAge))
            {
                isErrorFlag = true;
                fieldText += "人员需求 行：" + errorRow + " 起始年龄|";
                msgText += "请输入正确的起始年龄(整数)|";  
            }
            
                     //截止年龄
            MaxAge = document.getElementById("txtMaxAge_" + i).value.Trim();            
            //判断是否输入
            if(MaxAge == "")
            { 
            }
            else if (!IsNumber(MaxAge))
            {
                isErrorFlag = true;
                fieldText += "人员需求 行：" + errorRow + " 截止年龄|";
                msgText += "请输入正确的截止年龄(整数)|";  
            }
             if(MinAge!="" && MaxAge!="")
             {
                if(parseInt(MinAge)>parseInt(MaxAge))
                    {
                        isErrorFlag = true;
                  fieldText += "人员需求 行：" + errorRow + " 起始年龄|";
                        msgText += "起始年龄不能大于截止年龄|";  
                    }
              }
                  //工作要求
            Requisition = document.getElementById("txtRequisition_"+i ).value.Trim();         
            if (Requisition != "" && Requisition != null && strlen(Requisition) > 1000)
             {
            isErrorFlag = true;
              fieldText += "人员需求 行：" + errorRow + " 工作要求|";
            msgText += "工作要求最多只允许输入1000个字符！|";  
            }
              //其他要求
            OtherAbility = document.getElementById("txtOtherAbility_"+i ).value.Trim();         
            if (OtherAbility != "" && OtherAbility != null && strlen(OtherAbility) > 1000)
             {
            isErrorFlag = true;
        fieldText += "人员需求 行：" + errorRow + " 其他要求|";
            msgText += "其他要求最多只允许输入1000个字符！|";  
            }
               //可提供的待遇
            SalaryNote = document.getElementById("txtSalaryNote_"+i ).value.Trim();         
            if (SalaryNote != "" && SalaryNote != null && strlen(SalaryNote) > 1000)
             {
            isErrorFlag = true;
        fieldText += "人员需求 行：" + errorRow + " 可提供的待遇|";
            msgText += "可提供的待遇最多只允许输入1000个字符！|";  
            }
            
//              //人员数量
//            txtAge = document.getElementById("txtAge_" + i).value;            
//            //判断是否输入
//            if(personCount == "")
//            {
//               
//            }
//            else if (!IsNumber(txtAge))
//            {
//                isErrorFlag = true;
//                fieldText += "招聘目标 行：" + errorRow + " 年龄|";
//                msgText += "请输入正确的年龄|";  
//            }
	    
	    }
	}
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 招聘目标参数
*/
function GetGoalParams()
{
    //获取表格
    table = document.getElementById("tblRectGoalDetailInfo");
    //获取表格行数
    var tableCount = table.rows.length;
    //定义变量
    var count = 0;
    var param = "";
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < tableCount ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长技能数
            count++;
       
 
	        //职务名称 
	        //DeptQuarter" + no + "Hidden\" /> <input id=\"DeptQuarter" + no + "\" t
	        //岗位ID
            param += "&DeptQuarter"+ count + "Hidden=" + escape(document.getElementById("DeptQuarter" + i+"Hidden").value.Trim());
        //   岗位名称
               param += "&DeptQuarter_"+ count + "=" + escape(document.getElementById("DeptQuarter" + i).value.Trim());
               
	        //人员数量
            param += "&PersonCount_" + count + "=" + document.getElementById("txtPersonCount_" + i).value.Trim();
             //职务说明
            param += "&JobDescripe_" + count + "=" +escape (  document.getElementById("txtJobDescripe_" + i).value.Trim());
                //最迟上岗时间
            param += "&UsedDate_" + count + "=" + document.getElementById("txtUsedDate_" + i).value.Trim();
                  //工作地点
            param += "&WorkPlace_" + count + "=" + escape ( document.getElementById("txtWorkPlace_" + i).value.Trim());
                //工作性质
            param += "&WorkNature_" + count + "=" + document.getElementById("ddlWorkNature_" + i).value.Trim();
	        //性别
            param += "&Sex_" + count + "=" + document.getElementById("ddlSex_" + i).value.Trim();
               //起始年龄
            param += "&MinAge_" + count + "=" + document.getElementById("txtMinAge_" + i).value.Trim();
               //截止年龄
            param += "&MaxAge_" + count + "=" + document.getElementById("txtMaxAge_" + i).value.Trim();
             //专业
            param += "&Professional_" + count + "=" + document.getElementById("ddlProfessional_" + i).value.Trim();
	        //学历
            param += "&CultureLevel_" + count + "=" + document.getElementById("ddlCultureLevel_" + i).value.Trim();
            //工作年限
             param += "&WorkAge_" + count + "=" + document.getElementById("ddlWorkAge_" + i).value.Trim();
	        //工作要求
            param += "&Requisition_" + count + "=" + escape(document.getElementById("txtRequisition_" + i).value.Trim());
              //其他要求
            param += "&OtherAbility_" + count + "=" + escape(document.getElementById("txtOtherAbility_" + i).value.Trim());
              //可提供的其他待遇
            param += "&SalaryNote_" + count + "=" + escape(document.getElementById("txtSalaryNote_" + i).value.Trim());
            
	    
	    }
	}
	//table表记录数
    param += "&GoalCount=" + count;
    //返回参数信息
    return param;
}
function alertContent(obj)
{
document .getElementById ("hidEditControl").value=obj;
document .getElementById ("txtEdit").value='';

document .getElementById ("divEditCheckItem").style.display="Block";

var valueContent=document .getElementById (obj).value.Trim();
document .getElementById ("txtEdit").value=valueContent ;
 
}
function outContent()
{
document .getElementById ("txtEdit").value='';
document .getElementById ("hidEditControl").value='';
document .getElementById ("divEditCheckItem").style.display="none";
}
function check()
{
var obj=document .getElementById ("hidEditControl").value.Trim();
document .getElementById (obj).value=document .getElementById ("txtEdit").value.Trim();
document .getElementById ("txtEdit").value='';
document .getElementById ("hidEditControl").value='';
document .getElementById ("divEditCheckItem").style.display="none";
}