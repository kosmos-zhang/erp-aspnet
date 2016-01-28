 
 
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
String.prototype.Trim = function() {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
var AllSign=false ;
var canPage=false ;
function FormatAfterDotNumber(value, num) //四舍五入
{
    if (value != '') {
        var a_str = formatnumber(value, num);
        var a_int = parseFloat(a_str);
        if (value.toString().length > a_str.length) {
            var b_str = value.toString().substring(a_str.length, a_str.length + 1)
            var b_int = parseFloat(b_str);
            if (b_int < 5) {
                return a_str
            }
            else {
                var bonus_str, bonus_int;
                if (num == 0) {
                    bonus_int = 1;
                }
                else {
                    bonus_str = "0."
                    for (var i = 1; i < num; i++)
                        bonus_str += "0";
                    bonus_str += "1";
                    bonus_int = parseFloat(bonus_str);
                }
                a_str = formatnumber(a_int + bonus_int, num)
            }
        }
        return a_str
    }
    else {
        return '0.00';
    }
}


function formatnumber(value, num) //直接去尾
{
    var a, b, c, i
    a = value.toString();
    b = a.indexOf('.');
    c = a.length;
    if (num == 0) {
        if (b != -1)
            a = a.substring(0, b);
    }
    else {
        if (b == -1) {
            a = a + ".";
            for (i = 1; i <= num; i++)
                a = a + "0";
        }
        else {
            a = a.substring(0, b + num + 1);
            for (i = c; i <= b + num; i++)
                a = a + "0";
        }
    }
    return a
}

function addLoadEvent(func) {
  var oldonload = window.onload;
  if (typeof window.onload != 'function') {
    window.onload = func;
  } else {
    window.onload = function() {
      if (oldonload) {
        oldonload();
      }
      func();
    }
  }
}
addLoadEvent(sddf);
function sddf()
{
    var requestObj = GetRequest(location.search);
      $("#HiddenURLParams").val(location.search.substr(1));
      
    var SourcePage = requestObj['SourcePage'];//如果不为空，表示从其他页面链接过来的
    if(SourcePage == "Info")
    {//不是从菜单栏进入的
        $("#imgBack").css("display","inline");
    } 
//    var intFromType = requestObj["intFromType"];
//    if(intFromType != null)
//    {//个人桌面进入
//        $("#imgBack").css("display","inline");
//    }
//alert ("2");
    var ID = requestObj['ID'];
    if(ID !=null)
    {//需填充
        FillPurchaseApplyByID(ID);
    }
    else {
   GetExtAttr('officedba.PurchaseApply', null);
    }
    
  
    //setTimeout("GetFlowButton_DisplayControl()",1000);
   GetFlowButton_DisplayControl();
}
//$(document).ready(function() {
//  
//    
//// alert ("1");
//    
//    
//    var requestObj = GetRequest(location.search);
//      $("#HiddenURLParams").val(location.search.substr(1));
//      
//    var SourcePage = requestObj['SourcePage'];//如果不为空，表示从其他页面链接过来的
//    if(SourcePage == "Info")
//    {//不是从菜单栏进入的
//        $("#imgBack").css("display","inline");
//    } 
////    var intFromType = requestObj["intFromType"];
////    if(intFromType != null)
////    {//个人桌面进入
////        $("#imgBack").css("display","inline");
////    }
////alert ("2");
//    var ID = requestObj['ID'];
//    if(ID !=null)
//    {//需填充
//        FillPurchaseApplyByID(ID);
//    }
//    else {
//   GetExtAttr('officedba.PurchaseApply', null);
//    }
//    
//  
//    //setTimeout("GetFlowButton_DisplayControl()",1000);
//   GetFlowButton_DisplayControl();
//  
//    
////    fnGetExtAttr();
//    //BillChoosefnGetExtAttr();//物品扩展字段
//});

function DoBack()
{ 
if (!canPage) return ;
    var URLParams = document.getElementById("HiddenURLParams").value;
    var requestObj2 = GetRequest(URLParams);
    var intFromType = requestObj2["intFromType"];
    if(intFromType != null)
    {//来源个人桌面
        var ModuleID = requestObj2["ListModuleID"];
        URLParams = "ModuleID="+ModuleID;
        switch(intFromType)
        {
            case "2":
                //来源FlowMyApply.aspx
                window.location.href='../../../DeskTop.aspx';
                break;
            case "3":
                //来源FlowMyApply.aspx
                window.location.href='../../Personal/WorkFlow/FlowMyApply.aspx?'+URLParams;
                break;
            case "4":
                //FlowMyProcess.aspx
                window.location.href='../../Personal/WorkFlow/FlowMyProcess.aspx?'+URLParams;
                break;
            case "5":
                //FlowWaitProcess.aspx
                window.location.href='../../Personal/WorkFlow/FlowWaitProcess.aspx?'+URLParams;
                break;
            default:
                break;
        }
    }
    else
    {//来源列表    
        URLParams = URLParams.replace("ModuleID=2041301","ModuleID=2041302");
        URLParams = URLParams.replace("SourcePage=Info","SourcePage=Add");

        window.location.href='PurchaseApplyInfo.aspx?'+URLParams;
    }
}




function ShowProdInfo()
{ 
 popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"','Check');
}


function fnPrint()
{ 
    var ID = $("#ThisID").val();
    if(parseInt(ID) == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("../../../Pages/PrinttingModel/PurchaseManager/PurchaseApplyPrint.aspx?ID="+ID);
}

function FillPurchaseApplyByID(ID)
{ 
//alert ("3");
    var URLParams = "ID="+ID;
    var ActionApply = "Fill";
    URLParams +="&ActionApply="+ActionApply;
    $.ajax(
    { 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
        dataType:'json',//返回json格式数据
        cache:false,
        data:URLParams,
        beforeSend:function()
        {
//            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(msg) 
        {
            //将隐藏的已计划数量显示出来
            $("#PlanedCount").css("display","inline");
            //设置HiddenAction为Update
            $("#HiddenAction").val("Update");
            //设置标题为 采购申请单，将 新建采购申请单隐藏
            $("#AddTitle").css("display","none");
            $("#UpdateTitle").css("display","inline");
            
                    $.each(msg.PurchaseApplyDetail,function(i,itemDetail)
            {
                if(itemDetail.ID!= null && itemDetail.ID != "")
                {//填充明细
                    fnFillPurchaseApplyDetail(itemDetail);
                }
            });
            
             $.each(msg.PurchaseApplySource,function(i,itemSource)
            {
                if(itemSource.ID!= null && itemSource.ID != "")
                {//填充明细来源
                    fnFillPurchaseApplySource(itemSource);
                }
            });
            
            $.each(msg.PurchaseApplyPrimary,function(i,itemPrimary)
            { 
                if(itemPrimary.ID!= null && itemPrimary.ID != "")
                {//填充主表
                    fnFillPurchaseApply(itemPrimary);
                }
            });
            
           
            
            
          
           
 

           
         GetFlowButton_DisplayControl();
//               setTimeout("GetFlowButton_DisplayControl()",5000);
            //判断单据的按钮状态
            fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
            
        },
        complete:function(){
//        hidePopup();
canPage=true ;
        }
    }); 
}

function fnFillPurchaseApplyDetail(item)
{ 
    var rowID = AddDtlSignRow();
        if (document.readyState=="complete")
        {
    document.getElementById("DtlSortNo"+rowID).innerHTML = item.SortNo;
    $("#DtlProdID"+rowID).attr("value",item.ProductID);
    $("#DtlProdNo"+rowID).attr("value",item.ProductNo);
    $("#DtlProdName"+rowID).attr("value",item.ProductName);
    $("#DtlSpecification"+rowID).attr("value",item.Specification);
    
    $("#DtlUnitID"+rowID).attr("value",item.UnitID);
    $("#DtlUnitName"+rowID).attr("value",item.UnitName);
    $("#DtlPlanCount"+rowID).attr("value",FormatAfterDotNumber(item.ProductCount,2));
    $("#DtlRequireDate"+rowID).attr("value",item.RequireDate);
//    $("#DtlApplyReason"+rowID).attr("value",item.ApplyReason);
    
//    $("#DtlRemark"+rowID).attr("value",item.Remark);
    
    //已计划数量显示
    $("#newPlanedCount"+rowID).css("display","inline");
    $("#DtlPlanedCount"+rowID).val(FormatAfterDotNumber(item.PlanedCount,2));
    }
}

//多选明细方法

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
                        if(!IsExistCheck(item.ProdNo))
                        {
                            
                          var Index = AddDtlSSignRow();  
                        $("#DtlSProdID"+Index).attr("value",item.ID);
                        $("#DtlSProdNo"+Index).attr("value",item.ProdNo);
                        $("#DtlSProdName"+Index).attr("value",item.ProductName);
                        $("#DtlSUnitID"+Index).attr("value",item.UnitID);
                        $("#DtlSUnitName"+Index).attr("value",item.CodeName); 
                        $("#DtlSSpecification"+Index).attr("value",item.Specification);
 
                        }
                   });
                     
                      },
             
               complete:function(){}//接收数据完毕
           });
      closeProductdiv();
}  

function IsExistCheck(prodNo)
{ 
    var sign=false ;
    var signFrame = findObj("DetailSTable",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
          var prodNo1 = document.getElementById("DtlSProdNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}



function fnFillPurchaseApplySource(item)
{ 
    var rowID = AddDtlSSignRow();
    if (document.readyState=="complete")
    {
    document.getElementById("DtlSSortNo"+rowID).innerHTML = item.SortNo;
    $("#DtlSProdID"+rowID).attr("value",item.ProductID);
    $("#DtlSProdNo"+rowID).attr("value",item.ProductNo);
    $("#DtlSProdName"+rowID).attr("value",item.ProductName);
    $("#DtlSUnitID"+rowID).attr("value",item.UnitID);
    
    $("#DtlSUnitName"+rowID).attr("value",item.UnitName);
    $("#DtlSPlanCount"+rowID).attr("value",FormatAfterDotNumber(item.PlanCount,2)); 
    $("#DtlSRequireDate"+rowID).attr("value",item.RequireDate);
    $("#DtlSPlanTakeDate"+rowID).attr("value",item.PlanTakeDate);
    $("#DtlSApplyReason"+rowID).val(item.ApplyReason);
    
    $("#DtlSFromBillID"+rowID).attr("value",item.FromBillID);
    $("#DtlSFromBillNo"+rowID).attr("value",item.FromBillNo);
    $("#DtlSFromLineNo"+rowID).attr("value",item.FromLineNo);
//    $("#DtlSRemark"+rowID).attr("value",item.Remark);
   
    $("#DtlSSpecification"+rowID).attr("value",item.Specification);
    }
    
    AllSign=true ;
}

function fnFillPurchaseApply(item)
{ 
    document.getElementById("PurApplyNo_txtCode").value = item.ApplyNo;
    document.getElementById('PurApplyNo_txtCode').className='tdinput';
    document.getElementById('PurApplyNo_txtCode').style.width='90%';
    document.getElementById('PurApplyNo_ddlCodeRule').style.display='none';
    
    
    $("#ThisID").val(item.ID);
    $("#IsCite").val(item.IsCite);
    $("#FlowStatus").val(item.FlowStatus);
    $("#txtTitle").attr("value",item.Title);
    if(item.TypeID != 0)
    $("#ddlTypeID_ddlCodeType").attr("value",item.TypeID);
    $("#UserApplyUserName").attr("value",item.ApplyUserName);
    $("#txtApplyUserID").attr("value",item.ApplyUserID);
    $("#DeptName").attr("value",item.DeptName);
    $("#txtDeptID").attr("value",item.ApplyDeptID);
    $("#ddlFromType").attr("value",item.FromType);
    
     if(item.FromType == "0")
    {//源单为无来源时，从源单选择明细不可用
         $("#imgGetDtl").css("display", "none");
         $("#imgUnGetDtl").css("display", "inline"); 
         $("#imgExport").show();
         $("#imgUnExport").hide();
         
    }
    else
    {
        $("#imgGetDtl").css("display", "inline");
        $("#imgUnGetDtl").css("display", "none");
        $("#imgExport").hide();
         $("#imgUnExport").show();
    }
    $("#txtApplyDate").attr("value",item.ApplyDate);
    $("#txtAddress").attr("value",item.Address);
    
    $("#txtCountTotal").attr("value",item.CountTotal);
    
    $("#txtCreatorName").attr("value",item.CreatorName);
    $("#txtCreatorID").attr("value",item.Creator);    
    $("#txtCreateDate").attr("value",item.CreateDate);
    $("#txtBillStatusID").attr("value",item.BillStatus);
    $("#txtBillStatusName").attr("value",item.BillStatusName);
    $("#txtConfirmorID").attr("value",item.Confirmor);
    $("#txtConfirmorName").attr("value",item.ConfirmorName);
    $("#txtConfirmDate").attr("value",item.ConfirmDate);
    
    $("#txtModifiedUserID").attr("value",item.ModifiedUserID);
    $("#txtModifiedUserName").attr("value",item.ModifiedUserID);
    $("#txtModifiedDate").attr("value",item.ModifiedDate);
    $("#txtCloserID").attr("value",item.Closer);
    $("#txtCloserName").attr("value",item.CloserName);
    $("#txtCloseDate").attr("value",item.CloseDate);
    $("#txtRemark").attr("value",item.Remark);
         var TableName = "officedba.PurchaseApply";
                GetExtAttr(TableName, item);
}

function SavePurApply()
{ 
if(fnCheckInfo())
return;
//fnCalculateTotal();
    var URLParams = fnGetBaseInfo();
    URLParams += fnGetDtlSInfo();
    URLParams += fnGetDtlInfo()+GetExtAttrValue();
    
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
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
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
                if(document.getElementById("HiddenAction").value == "Add")
                {
                    //设置编辑模式
                    document.getElementById("HiddenAction").value = "Update";
//                    /* 设置编号的显示 */ 
//                    //显示采购计划的编号 采购计划编号DIV可见              
//                    document.getElementById("divPurApplyNo").style.display = "block";
//                    //设置采购计划编号
//                    
//                    document.getElementById("divPurApplyNo").innerHTML = data.data.split('#')[0];
//                    $("#")
                    document.getElementById("ThisID").value = data.data.split('#')[1];
                    
                    $("#txtModifiedUserName").val(data.data.split('#')[3]);
                    $("#txtModifiedUserID").val(data.data.split('#')[3])
                    $("#txtModifiedDate").val(data.data.split('#')[2]);
                   //编码规则DIV不可见
//                    document.getElementById("divCodeRule").style.display = "none";
                    
                    
                    document.getElementById("PurApplyNo_txtCode").value = data.data.split('#')[0];
                    document.getElementById('PurApplyNo_txtCode').className='tdinput';
                    document.getElementById('PurApplyNo_txtCode').style.width='90%';
                    document.getElementById('PurApplyNo_ddlCodeRule').style.display='none';
                    
                   
                    
                   
//                    //设置源单类型不可改
//                    $("#ddlFromType").attr("disabled","disabled");
                } 
                else
                {  if($("#txtBillStatusID").val() == "2")
                    {
                        $("#txtBillStatusID").val("3");
                        $("#txtBillStatusName").val("变更");
                    }  
                    $("#txtModifiedUserName").val(data.data.split('#')[3]);
                    $("#txtModifiedUserID").val(data.data.split('#')[3])
                    $("#txtModifiedDate").val(data.data.split('#')[2]);                  
                }
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                GetFlowButton_DisplayControl();
                //最后更新人
                
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



function ChooseSourceBill()
{ 
    var Flag= document.getElementById("ddlFromType").value.Trim();
    if(Flag == 0)
    {//无来源
        
    }
    else if(Flag==1)
    {//销售订单
        
        popBillObj.ShowList();
    }
    else if(Flag==2)
    {//物料需求计划
        popMaterialObj.ShowList();
    }
}
function fnGetBaseInfo()
{
    var ActionApply = document.getElementById("HiddenAction").value;
    var strParams = "ActionApply=" + ActionApply;//编辑标识
    if(ActionApply == "Add")
    {//新增时
        codeRule = escape(document.getElementById("PurApplyNo_ddlCodeRule").value.Trim());
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            strParams += "&PurApplyNo=" + escape(document.getElementById("PurApplyNo_txtCode").value.Trim());
        }
        //编码规则ID
        strParams += "&CodeRuleID=" + escape(document.getElementById("PurApplyNo_ddlCodeRule").value.Trim());
    }
    else if(ActionApply == "Update")
    {
        strParams += "&PurApplyNo=" + escape(document.getElementById("PurApplyNo_txtCode").value.Trim());
        strParams += "&ID="+escape(document.getElementById("ThisID").value.Trim());
    }
    strParams += "&Title=" + escape(document.getElementById("txtTitle").value.Trim());
    strParams += "&PurchaseType=" + escape(document.getElementById("ddlTypeID_ddlCodeType").value.Trim());
    strParams += "&ApplyUserID=" + escape(document.getElementById("txtApplyUserID").value.Trim());
    strParams += "&DeptID=" + escape(document.getElementById("txtDeptID").value.Trim());
    strParams += "&FromType=" + escape(document.getElementById("ddlFromType").value.Trim());  
          
    strParams += "&ApplyDate=" + escape(document.getElementById("txtApplyDate").value.Trim());    
    strParams += "&Address=" + escape(document.getElementById("txtAddress").value.Trim());    
    strParams += "&CountTotal=" + escape(document.getElementById("txtCountTotal").value.Trim());
    
    //备注信息
    strParams += "&CreatorID=" + escape(document.getElementById("txtCreatorID").value.Trim());    
    strParams += "&CreateDate=" + escape(document.getElementById("txtCreateDate").value.Trim());
    strParams += "&BillStatusID=" + escape(document.getElementById("txtBillStatusID").value.Trim());
    strParams += "&ConfirmorID=" + escape(document.getElementById("txtConfirmorID").value.Trim());
    strParams += "&ConfirmorDate=" + escape(document.getElementById("txtConfirmDate").value.Trim());
    
    strParams += "&ModifiedUserID=" + escape(document.getElementById("txtModifiedUserID").value.Trim());    
    strParams += "&ModifiedDate=" + escape(document.getElementById("txtModifiedDate").value.Trim());
    strParams += "&CloserID=" + escape(document.getElementById("txtCloserID").value.Trim());
    strParams += "&CloseDate=" + escape(document.getElementById("txtCloseDate").value.Trim());
    strParams += "&Remark=" + escape(document.getElementById("txtRemark").value.Trim());
    
    return strParams;
}

function fnGetDtlSInfo()
{ 
    var strParams = "";
    var signFrame = findObj("DetailSTable",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
            if (signFrame.rows[i].style.display != "none") 
            {
                strParams +="&DtlSSortNo"+DetailLength+"="+escape(document.getElementById("DtlSSortNo" + i).innerHTML);
                strParams +="&DtlSProdID"+DetailLength+"="+escape(document.getElementById("DtlSProdID" + i).value.Trim());
                strParams +="&DtlSProdNo"+DetailLength+"="+escape(document.getElementById("DtlSProdNo" + i).value.Trim());
                strParams +="&DtlSProdName"+DetailLength+"="+escape(document.getElementById("DtlSProdName" + i).value.Trim());
                strParams +="&DtlSUnitID"+DetailLength+"="+escape(document.getElementById("DtlSUnitID" + i).value.Trim());
                
//                strParams +="&DtlSProductCount"+DetailLength+"="+document.getElementById("DtlSProductCount" + i).value;
                strParams +="&DtlSPlanCount"+DetailLength+"="+escape(document.getElementById("DtlSPlanCount" + i).value.Trim());
//                strParams +="&DtlSRequireDate"+DetailLength+"="+document.getElementById("DtlSRequireDate" + i).value;
                strParams +="&DtlSPlanTakeDate"+DetailLength+"="+escape(document.getElementById("DtlSPlanTakeDate" + i).value.Trim());
                strParams += "&DtlSApplyReason"+DetailLength+"="+escape($("#DtlSApplyReason"+i).val());
                strParams +="&DtlSFromBillID"+DetailLength+"="+escape(document.getElementById("DtlSFromBillID" + i).value.Trim());
                
                strParams +="&DtlSFromLineNo"+DetailLength+"="+escape(document.getElementById("DtlSFromLineNo" + i).value.Trim());
                strParams +="&DtlSRemark"+DetailLength+"="+escape(document.getElementById("DtlSRemark" + i).value.Trim());
                
                DetailLength++;
            }
        }
    }
    strParams +="&DetailSLength="+DetailLength+"";
    return strParams;
}
function fnMergeDetail()
{//明细汇总
    //被引用则不能改
     
    if($("#IsCite").val() == "True")
    return;

//已计划数量显示
    var newstyle = $("#PlanedCount").css("display");
//    $("#DtlPlanedCount"+rowID).val(item.PlanedCount);
    fnDropDtlSignRow();
    var fieldText = "";
    var msgText = "";
    var isCorrectFlag = true;
    
    var signFrame = findObj("DetailSTable",document);
    var Length = signFrame.rows.length;
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < Length; i++) 
        {
            if (signFrame.rows[i].style.display != "none") 
            {
                var rowID = fnCanMerge(i);
                if(rowID>0)
                {//可以合并
                    document.getElementById("DtlPlanCount"+rowID).value =FormatAfterDotNumber(
                     (parseFloat(document.getElementById("DtlPlanCount"+rowID).value.Trim())+parseFloat(document.getElementById("DtlSPlanCount"+i).value.Trim())),2);
                }
                else
                {//不可以合并
                    var Index = AddDtlSignRow();
                    document.getElementById("DtlProdID"+Index).value = document.getElementById("DtlSProdID"+i).value.Trim();
                    document.getElementById("DtlProdNo"+Index).value = document.getElementById("DtlSProdNo"+i).value.Trim();
                    document.getElementById("DtlProdName"+Index).value = document.getElementById("DtlSProdName"+i).value.Trim();
                    document.getElementById("DtlSpecification"+Index).value = document.getElementById("DtlSSpecification"+i).value.Trim();
                    document.getElementById("DtlUnitID"+Index).value = document.getElementById("DtlSUnitID"+i).value.Trim();
                    document.getElementById("DtlUnitName"+Index).value = document.getElementById("DtlSUnitName"+i).value.Trim();
                    document.getElementById("DtlPlanCount"+Index).value = document.getElementById("DtlSPlanCount"+i).value.Trim();
                    document.getElementById("DtlRequireDate"+Index).value = document.getElementById("DtlSPlanTakeDate"+i).value.Trim();
                    if(newstyle == "inline")
                    {//已订购数量显示
                        $("#newPlanedCount"+Index).css("display","inline");
                        $("#DtlPlanedCount"+Index).val(0.00);
                    }
//                    document.getElementById("DtlRemark"+Index).value = document.getElementById("DtlSRemark"+i).value;
                }
            }
        }
    }
    else
    {
        isCorrectFlag = false;
        fieldText += "采购申请明细来源|";
        msgText += "请输入采购申请明细来源|";
        popMsgObj.Show(fieldText,msgText);
    }
    return isCorrectFlag;
}

function fnCanMerge(rowID)
{//确定该条明细来源是否可以合并,可以合并返回行号，不可以合并返回0
    
    var ProductNo = document.getElementById("DtlSProdNo"+rowID).value.Trim();
    var RequireDate = document.getElementById("DtlSPlanTakeDate"+rowID).value.Trim();
    
    var signFrame = document.getElementById("DetailTable");
    if((typeof(signFrame) == "undefined")&&(signFrame==null))
    {
        return 0;
    }
    else
    {
        for (var i = 1; i < signFrame.rows.length; i++) 
        {
            if (signFrame.rows[i].style.display != "none") 
            {
                var ThisProductNo = document.getElementById("DtlProdNo"+i).value.Trim();
                var ThisRequireDate = document.getElementById("DtlSPlanTakeDate"+i).value.Trim();
                if((ProductNo == ThisProductNo)&&(RequireDate == ThisRequireDate))
                return i;
            }
        }
    }
    return 0;
}

function fnChangeSourceBill(FromType)
{ 
    fnDropDtlSSignRow();
    fnDropDtlSignRow();
    try
    {
    if(FromType == "0")
    {//源单为无来源时，从源单选择明细不可用
         $("#imgGetDtl").css("display", "none");
         $("#imgUnGetDtl").css("display", "inline"); 
         $("#imgExport").show();
         $("#imgUnExport").hide();
         
    }
    else
    {
        $("#imgGetDtl").css("display", "inline");
        $("#imgUnGetDtl").css("display", "none");
        $("#imgExport").hide();
         $("#imgUnExport").show();
    }
    }
    catch (e) {
    }
}

function  fnDropDtlSSignRow()
{ 
    var signFrame = document.getElementById("DetailSTable");  
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=signFrame.rows.length-1;i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
}

function fnDropDtlSignRow()
{ 
    var signFrame = document.getElementById("DetailTable");  
    if((typeof(signFrame) != "undefined")&&(signFrame!=null))
    {
        for( var i = signFrame.rows.length-1 ; i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
}

function fnGetProduct(rowID)
{ 
    if(document.getElementById("DtlSFromBillID"+rowID).value.Trim() != "")
    {
        return;
    }
    else
    {
        popTechObj.ShowList(rowID);
//        popProductInfoUC.ShowList("DtlSProdID"+rowID+"","DtlSProdNo"+rowID+"","DtlSProdName"+rowID+"","DtlSSpecification"+rowID+"","DtlSUnitID"+rowID+"","DtlSUnitName"+rowID+"");
    }
}

function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{ 
    var RowID = popTechObj.InputObj;
    $("#DtlSProdID"+RowID).val(id);
    $("#DtlSProdNo"+RowID).val(no);
    $("#DtlSProdName"+RowID).val(productname);
    $("#DtlSSpecification"+RowID).val(standard);
    $("#DtlSUnitID"+RowID).val(unitid);
    $("#DtlSUnitName"+RowID).val(unit);
    fnMergeDetail();
}

function Fun_Bill007(ID,OrderNo,SortNo,ProductID,ProductNo,ProductName,UnitID,UnitName,ProductCount,SendDate)
{ 
    if(!ExistFromBill(OrderNo,SortNo))
    {
        var Index = AddDtlSSignRow();
        var objFromBillID = 'DtlSFromBillID'+Index;
        var objFromBillNo = 'DtlSFromBillNo'+Index;
        var objFromLineNo = 'DtlSFromLineNo'+Index;
        var objProductID = 'DtlSProdID'+Index;
        var objProductNo = 'DtlSProdNo'+Index;
        var objProductName = 'DtlSProdName'+Index;
        var objUnitID = 'DtlSUnitID'+Index;
        var objUnit = 'DtlSUnitName'+Index;
        var objProductCount = 'DtlSPlanCount'+Index;
        var objPlanCount = 'DtlSPlanCount'+Index;        
//        var objRequireDate = 'DtlSRequireDate'+Index;
        var objPlanTakeDate = 'DtlSPlanTakeDate'+Index;
        
        
        document.getElementById(objFromBillID).value = ID;
        document.getElementById(objFromBillNo).value = OrderNo;
        document.getElementById(objFromLineNo).value = SortNo;
        document.getElementById(objProductID).value = ProductID;
        document.getElementById(objProductNo).value = ProductNo;
        document.getElementById(objProductName).value = ProductName;
        document.getElementById(objUnitID).value = UnitID;
        document.getElementById(objUnit).value = UnitName;
        document.getElementById(objProductCount).value = FormatAfterDotNumber(ProductCount,2);
        document.getElementById(objPlanCount).value = FormatAfterDotNumber(ProductCount,2);
//        document.getElementById(objRequireDate).value = SendDate;
        document.getElementById(objPlanTakeDate).value = SendDate;
        
        fnCalculateTotal();
    }
}

function FillByMRP(id,mrpno,sortno,productid,productno,productname,unitid,unit,plancount,processedcount,totalprice)
{ 
    if(!ExistFromBill(mrpno,sortno))
    {
        var Index = AddDtlSSignRow();
        var objFromBillID = 'DtlSFromBillID'+Index;
        var objFromBillNo = 'DtlSFromBillNo'+Index;
        var objFromLineNo = 'DtlSFromLineNo'+Index;
        var objProductID = 'DtlSProdID'+Index;
        var objProductNo = 'DtlSProdNo'+Index;
        var objProductName = 'DtlSProdName'+Index;
        var objUnitID = 'DtlSUnitID'+Index;
        var objUnit = 'DtlSUnitName'+Index;
        var objProductCount = 'DtlSPlanCount'+Index;
        var objRequireDate = 'DtlSRequireDate'+Index;
        
        
        document.getElementById(objFromBillID).value = id;
        document.getElementById(objFromBillNo).value = mrpno;
        document.getElementById(objFromLineNo).value = sortno;
        document.getElementById(objProductID).value = productid;
        document.getElementById(objProductNo).value = productno;
        document.getElementById(objProductName).value = productname;
        document.getElementById(objUnitID).value = unitid;
        document.getElementById(objUnit).value = unit;
        document.getElementById(objProductCount).value = FormatAfterDotNumber(plancount,2);
//        document.getElementById(objRequireDate).value = SendDate;

        fnCalculateTotal();
        hidePopup();
    }
}
//判断是否有相同记录有返回true，没有返回false
function ExistFromBill(orderno,sortno)
{ 
    var signFrame = document.getElementById("DetailSTable");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var frombillNo = document.getElementById("DtlSFromBillNo"+i).value.Trim();
        var fromlineno = document.getElementById("DtlSFromLineNo"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(frombillNo==orderno)&&(fromlineno == sortno))
        {
            return true;
        } 
    }
    return false;
}

function fnSelectAll()
{ 
    $.each($("#DetailSTable :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

function AddDtlSSignRow()
{ //读取最后一行的行号，存放在txtTRLastIndex文本框中 
 
    var signFrame = findObj("DetailSTable",document);
    var rowID = signFrame.rows.length
    var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
    newTR.id = "SignItem" + rowID;
    var i=0;

    var newNameXH=newTR.insertCell(i++);//添加列:选择
    newNameXH.className="tdColInputCenter";
    newNameXH.innerHTML="<input name='chk' id='chk" + rowID + "' onclick=\"IfSelectAll('chk','checkall')\"  value="+rowID+" type='checkbox' size='20'   />";
    
    var newFlag=newTR.insertCell(i++);//添加列:手否可更改的标志
    newFlag.style.display = "none";
    newFlag.id="Flag" + rowID; 
    newFlag.innerHTML="can";
    
    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="cell";
    newNameTD.id="DtlSSortNo" + rowID; 
    newNameTD.innerHTML =GenerateNo(rowID);

    var newProductNo=newTR.insertCell(i++);//添加列:物品ID
    newProductNo.style.display = "none";
    newProductNo.innerHTML = "<input  id='DtlSProdID" + rowID + "'  type='text' class=\"tdinput\" style='width:90%;' />";//添加列内容


    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="cell";        
    newProductNo.innerHTML = "<input  id='DtlSProdNo" + rowID + "'readonly  onclick=\"fnGetProduct("+rowID+");\" type='text' class=\"tdinput\" style='width:90%;' />";//添加列内容

    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="cell";
    newProductName.innerHTML = "<input id='DtlSProdName" + rowID + "'onclick=\"fnGetProduct("+rowID+");\"  type='text' class=\"tdinput\"  style='width:90%;' disabled />";//添加列内容

    var newSSpecification=newTR.insertCell(i++);//添加列:物品规格
    newSSpecification.className="cell";
    newSSpecification.innerHTML = "<input id='DtlSSpecification" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;' disabled />";//添加列内容

    var newUnitName=newTR.insertCell(i++);//添加列:单位ID
    newUnitName.style.display = "none";
    newUnitName.innerHTML = "<input   id='DtlSUnitID" + rowID + "' type='hidden' style='width:90%;'/>";//添加列内容

    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input id='DtlSUnitName" + rowID + "' type='text' class=\"tdinput\" style='width:90%;' disabled  />";//添加列内容
    
//    var newProductCount=newTR.insertCell(i++);//添加列:需求数量
//    newProductCount.className="cell";
//    newProductCount.innerHTML = "<input  id='DtlSProductCount" + rowID + "' onchange=\"fnCalculateTotal();\" type='text' class=\"tdinput\" style='width:90%;'  disabled />";//添加列内容

    var newPlanCount=newTR.insertCell(i++);//添加列:申请数量
    newPlanCount.className="cell";
    newPlanCount.innerHTML = "<input  id='DtlSPlanCount"+ rowID+"' onblur=\"Number_round(this,2);fnCalculateTotal();\"  type='text' class=\"tdinput\" style='width:90%;'  />";//添加列内容
         
//    var newRequireDate=newTR.insertCell(i++);//添加列:需求日期
//    newRequireDate.className="cell";
//    newRequireDate.innerHTML = "<input  id='DtlSRequireDate" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'  disabled/>";//添加列内容
//    $("#DtlSRequireDate" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#DtlSRequireDate" + rowID)})}); 

    var newPlanTakeDate=newTR.insertCell(i++);//添加列:申请交货日期
    newPlanTakeDate.className="cell";
    newPlanTakeDate.innerHTML = "<input id='DtlSPlanTakeDate" + rowID + "' readonly=\"readonly\"    onclick=\"WdatePicker()\"  onpropertychange =\"fnCalculateTotal();\"  type='text' class=\"tdinput\"  style='width:90%;' />";//添加列内容
//    $("#DtlSPlanTakeDate" + rowID).focus(function(){
//    WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#DtlSPlanTakeDate" + rowID)})
//    });        

    var newApplyReasonName=newTR.insertCell(i++);//添加列:申请原因
    newApplyReasonName.className="cell";
    newApplyReasonName.innerHTML = "<select class='tdinput' id='DtlSApplyReason" + rowID + "'>" + document.getElementById("ddlApplyReason").innerHTML + "</select>";

//    var newApplyReason=newTR.insertCell(i++);//添加列:来源单据ID
//    newApplyReason.style.display = "none";
//    newApplyReason.innerHTML = "<input  id='DtlSApplyReason" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;' />";//添加列内容
    
    var newFromBillID=newTR.insertCell(i++);//添加列:来源单据ID
    newFromBillID.style.display = "none";
    newFromBillID.innerHTML = "<input  id='DtlSFromBillID" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;' />";//添加列内容

    var newFromBillID=newTR.insertCell(i++);//添加列:来源单据
    newFromBillID.className="cell";
    newFromBillID.innerHTML = "<input  id='DtlSFromBillNo" + rowID + "' type='text' class=\"tdinput\" style='width:90%;' disabled />";//添加列内容

    var newFromLineNo=newTR.insertCell(i++);//添加列:来源单据行号
    newFromLineNo.className="cell";
    newFromLineNo.innerHTML = "<input  id='DtlSFromLineNo" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;'  disabled/>";//添加列内容

    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.style.display = "none";
    newRemark.innerHTML = "<input id='DtlSRemark" + rowID + "'   type='text' class=\"tdinput\" style='width:90%;'   />";//添加列内容
    
    return rowID;
}

function GenerateNo(Edge)
{//生成序号
     
    var signFrame = findObj("DetailSTable",document);
    var SortNo = 1;//起始行号
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<Edge;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                document.getElementById("DtlSSortNo"+i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0;//明细表不存在时返回0
}

function GenerateDtlNo(Edge)
{//生成序号
     
    var signFrame = findObj("DetailTable",document);
    var SortNo = 1;//起始行号
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<Edge;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                document.getElementById("DtlSortNo"+i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0;//明细表不存在时返回0
}

function fnGetDtlInfo()
{ 
    var strParams = "";
    var signFrame = findObj("DetailTable",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
            if (signFrame.rows[i].style.display != "none") 
            {
                strParams +="&DtlSortNo"+DetailLength+"="+escape(document.getElementById("DtlSortNo" + i).innerHTML);
                strParams +="&DtlProdID"+DetailLength+"="+escape(document.getElementById("DtlProdID" + i).value.Trim());
                strParams +="&DtlProdNo"+DetailLength+"="+escape(document.getElementById("DtlProdNo" + i).value.Trim());
                strParams +="&DtlProdName"+DetailLength+"="+escape(document.getElementById("DtlProdName" + i).value.Trim());
                strParams +="&DtlUnitID"+DetailLength+"="+escape(document.getElementById("DtlUnitID" + i).value.Trim());
                
                strParams +="&DtlPlanCount"+DetailLength+"="+escape(document.getElementById("DtlPlanCount" + i).value.Trim());
                strParams +="&DtlRequireDate"+DetailLength+"="+escape(document.getElementById("DtlRequireDate" + i).value.Trim());
//                strParams +="&DtlApplyReason"+DetailLength+"="+document.getElementById("DtlApplyReason" + i).value;
                strParams +="&DtlRemark"+DetailLength+"="+escape(document.getElementById("DtlRemark" + i).value.Trim());
                
                DetailLength++;
            }
        }
    }
    strParams +="&DetailLength="+DetailLength+"";
    return strParams;
}

function AddDtlSignRow()
{ 
    var signFrame = findObj("DetailTable",document);
    var rowID = signFrame.rows.length
    var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
    newTR.id = "SignItem" + rowID;
    var i=0;

    var newNameXH=newTR.insertCell(i++);//添加列:选择
    newNameXH.className="tdColInputCenter";
    newNameXH.innerHTML="<input name='Dtlchk' id='Dtlchk" + rowID + "'  onclick=\"IfSelectAll('Dtlchk','checkall')\"  value="+rowID+" type='checkbox' size='20'  />";
    
    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="cell";
    newNameTD.id="DtlSortNo" + rowID; 
    newNameTD.innerHTML =GenerateDtlNo(rowID);

    var newProductNo=newTR.insertCell(i++);//添加列:物品ID
    newProductNo.style.display = "none";
    newProductNo.innerHTML = "<input  id='DtlProdID" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'disabled />";//添加列内容


    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="cell";        
    newProductNo.innerHTML = "<input  id='DtlProdNo" + rowID + "'readonly  type='text' class=\"tdinput\" style='width:90%;' disabled/>";//添加列内容

    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="cell";
    newProductName.innerHTML = "<input id='DtlProdName" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;'  disabled/>";//添加列内容

    var newSpecification=newTR.insertCell(i++);//添加列:物品规格
    newSpecification.className="cell";
    newSpecification.innerHTML = "<input id='DtlSpecification" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;' disabled />";//添加列内容

    var newUnitName=newTR.insertCell(i++);//添加列:单位ID
    newUnitName.style.display = "none";
    newUnitName.innerHTML = "<input   id='DtlUnitID" + rowID + "' type='hidden' style='width:90%;'disabled/>";//添加列内容

    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input id='DtlUnitName" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'   disabled/>";//添加列内容
    
    var newPlanCount=newTR.insertCell(i++);//添加列:申请数量
    newPlanCount.className="cell";
    newPlanCount.innerHTML = "<input  id='DtlPlanCount" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'   SpecialWorkCheck='申请数量' disabled/>";//添加列内容
    
    var newPlanTakeDate=newTR.insertCell(i++);//添加列:申请交货日期
    newPlanTakeDate.className="cell";
    newPlanTakeDate.innerHTML = "<input id='DtlRequireDate" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;' readonly disabled/>";//添加列内容
//    $("#DtlRequireDate" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#DtlRequireDate" + rowID)})});        

//    var newApplyReasonName=newTR.insertCell(i++);//添加列:申请原因
//    newApplyReasonName.className="cell";
//    newApplyReasonName.innerHTML = "<select class='tdinput' id='DtlApplyReason" + rowID + "'>" + document.getElementById("ddlApplyReasonD").innerHTML + "</select>";

    
//    var newApplyReason=newTR.insertCell(i++);//添加列:申请原因
//    newApplyReason.style.display = "none";
//    newApplyReason.innerHTML = "<input  id='DtlApplyReason" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'  />";//添加列内容
//   
    var newPlanedCount=newTR.insertCell(i++);//添加列:备注
//    newPlanedCount.style.display = "none";
    newPlanedCount.id = "newPlanedCount"+rowID;
    $("#newPlanedCount"+rowID).css("display","none");
    newPlanedCount.innerHTML = "<input id='DtlPlanedCount" + rowID + "'  type='text' class=\"tdinput\" style='width:90%;' disabled=\"disabled\"   />";//添加列内容    
     
    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.style.display = "none";
    newRemark.innerHTML = "<input id='DtlRemark" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'   />";//添加列内容
    
    return rowID;
}

function DeleteDtlSSignRow()
{//删除明细行，需要将序号重新生成
   
    var signFrame = findObj("DetailSTable",document);        
    var ck = document.getElementsByName("chk");
    for( var i = 0; i<ck.length;i++ )
    {
        var rowID = i+1;
        if ( ck[i].checked )
        {
           signFrame.rows[rowID].style.display="none";
        }
        document.getElementById("DtlSSortNo"+rowID).innerHTML = GenerateNo(rowID);
    }
    fnCalculateTotal();
}
function SelectAllDetailS()
{
    $.each($("#DetailSTable :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}


//全选
function SelectAll() {
 
    $.each($("#PurAskPriceInfo :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

function fnCalculateTotal()
{ 
if (!AllSign ) return ;
    if (document.readyState!="complete") return ;
 
    fnDropDtlSignRow();
    fnMergeDetail();
    var signFrame = findObj("DetailSTable",document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        var Total = 0;
        for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                if($("#DtlSPlanCount"+i).val() == "")
                {
                    $("#DtlSPlanCount"+i).val(0)
                }
                Total+=parseFloat(document.getElementById("DtlSPlanCount"+i).value.Trim());
            }
        }
    }
    document.getElementById("txtCountTotal").value = Total;
}

function fnCheckInfo()
{ 
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
 
    var ActionApply = document.getElementById("HiddenAction").value;
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isErrorFlag = true;
        var tmpKeys = RetVal.split(',');
        for (var i = 0; i < tmpKeys.length; i++) {
            isErrorFlag = true;
            fieldText = fieldText + tmpKeys[i].toString() + "|";
            msgText = msgText + "不能含有特殊字符|";
        }
    }
    //新建时，编号选择手工输入时
    if ("Add" == ActionApply)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("PurApplyNo_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            var PurApplyNo = document.getElementById("PurApplyNo_txtCode").value.Trim();
            //编号必须输入
            if (PurApplyNo == "")
            {
                isErrorFlag = true;
                fieldText += "编号|";
   		        msgText += "请输入编号|";
            }
            else if(!CodeCheck(PurApplyNo))
            {
                isErrorFlag = true;
                fieldText = fieldText + "编号|";
	            msgText = msgText +  "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
            }
            else if(strlen(PurApplyNo) >50)
            {
                isErrorFlag = true;
                fieldText += "编号|";
   		        msgText += "编号长度不大于50|";
            }
            
        }
    }
    //主题不空
//    if (document.getElementById("txtTitle").value.Trim() == "")
//    {
//        isErrorFlag = true;
//        fieldText += "主题|";
//        msgText += "请输入主题|";
//    }
//    else 
    if(strlen(document.getElementById("txtTitle").value.Trim()) >100)
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "主题长度不大于100|";
    }
//    //采购类别不空
//    if (document.getElementById("ddlTypeID_ddlCodeType").value == "")
//    {
//        isErrorFlag = true;
//        fieldText += "采购类别|";
//        msgText += "请选择采购类别|";
//    }
    //申请人不空
    if (document.getElementById("txtApplyUserID").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "申请人|";
        msgText += "请输入申请人|";
    }
    //申请部门不空
    if (document.getElementById("txtDeptID").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "申请部门|";
        msgText += "请输入申请部门|";
    }
    //申请日期是否正确
    if (document.getElementById("txtApplyDate").value.Trim() == "") {
    
        isErrorFlag = true;
            fieldText += "申请日期|";
            msgText += "请输入正确的申请日期|"; 
    }
    else
    {
        if(!isDate(document.getElementById("txtApplyDate").value.Trim()))
        {
            isErrorFlag = true;
            fieldText += "申请日期|";
            msgText += "请输入正确的申请日期|";  
        }
    }
    //备注长度不大于200
    if(strlen($("#txtRemark").val())>200)
    {
        isErrorFlag = true;
        fieldText += "备注|";
        msgText += "备注长度不大于200|";  
    }
    
    //明细来源的校验
    var signFrame = document.getElementById("DetailSTable");
    if((typeof(signFrame) == "undefined")||signFrame==null)
    {
        isErrorFlag = true;
        fieldText +="明细来源|";
        msgText +="请输入明细来源|";
    }
    else
    {
        var COUNT = 0;
        for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                COUNT++;
//                var ProductCount = "DtlSProductCount"+i;
//                var no = document.getElementById(ProductCount).value;
//                if(IsNumeric(no,12,4) == false)
//                {
//                    isErrorFlag = true;
//                    fieldText +="明细来源|";
//                    msgText +="请输入正确的需求数量|";
//                }
                var PlanCount = "DtlSPlanCount"+i;
                var noc = document.getElementById(PlanCount).value.Trim();
                if(IsNumeric(noc,12,4) == false)
                {
                    isErrorFlag = true;
                    fieldText +="明细来源|";
                    msgText +="请输入正确的申请数量|";
                }
                else
                {
                if (noc>0)
                {
                }
                else
                {
                  isErrorFlag = true;
                    fieldText +="明细来源|";
                    msgText +=" 申请数量需大于零|";
                
                }
                }
                
//                var RequireDate = "DtlSRequireDate"+i;
//                var reqdate = document.getElementById(RequireDate).value;
//                if(isDate(reqdate) == false)
//                {
//                    isErrorFlag = true;
//                    fieldText +="明细来源|";
//                    msgText +="请输入正确的需求日期|";
//                }
                var PlanTakeDate = "DtlSPlanTakeDate"+i;
                var plandate = document.getElementById(PlanTakeDate).value.Trim();
                if(isDate(plandate) == false)
                {
                    isErrorFlag = true;
                    fieldText +="明细来源|";
                    msgText +="请输入正确的需求日期|";
                }
                var Remark = $("#DtlSRemark"+i).val();
                if(strlen(Remark) > 200)
                {
                    isErrorFlag = true;
                    fieldText +="明细来源|";
                    msgText +="备注长度不能超过200|";
                }
            }
        }
        if(COUNT == 0)
        {
            isErrorFlag = true;
            fieldText +="明细来源|";
            msgText +="请输入明细来源|";
        }
        
    }
    var signFrame2 = document.getElementById("DetailTable");
    for(var j=1;j<signFrame2.rows.length;++j)
    {
        if(signFrame.rows[j].style.display!="none")
        {
            var Remark = $("#DtlRemark"+j).val();
            if(strlen(Remark) > 200)
            {
                isErrorFlag = true;
                fieldText +="明细|";
                msgText +="备注长度不能超过200|";
            }
        }
    }
    if(isErrorFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isErrorFlag;
}

function Fun_ConfirmOperate()
{//确认
    //更改状态
    
    var ActionApply ="Confirm";
    var ID = $("#ThisID").attr("value");
        
    var URLParams = "ActionApply="+ActionApply;
    URLParams += "&ID="+ID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
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
            var aaa = msg.data.split('#');
            $("#txtConfirmorID").attr("value",aaa[0]);
            $("#txtConfirmorName").attr("value",aaa[1]);
            $("#txtConfirmorDate").attr("value",aaa[2]);
                $("#txtModifiedUserName").val(aaa[3]);
                $("#txtModifiedUserID").val(aaa[3])
                $("#txtModifiedDate").val(aaa[2]);
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！"); 
            $("#txtBillStatusID").attr("value","2");
            $("#txtBillStatusName").attr("value","执行");
            fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
            GetFlowButton_DisplayControl();
        } 
    }); 
}

function Fun_FlowApply_Operate_Succeed(operateType)
{ 
    try{
        if(operateType == "0")
        {//提交审批成功后,不可改
            $("#imgUnSave").css("display", "inline");
            $("#imgSave").css("display", "none");
            
            
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#imgGetDtl").css("display", "none");
            $("#imgUnGetDtl").css("display", "inline"); 
            $("#btnGetGoods").css("display", "none");//条码扫描
        }
        else if(operateType == "1")
        {//审批成功后，不可改
            $("#imgUnSave").css("display", "inline");
            $("#imgSave").css("display", "none");
            
            
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#imgGetDtl").css("display", "none");
            $("#imgUnGetDtl").css("display", "inline"); 
            $("#btnGetGoods").css("display", "none");//条码扫描
        }
        else if(operateType == "2")
        {//审批不通过
            $("#imgSave").css("display", "inline");
            $("#imgUnSave").css("display", "none");
            
            
            $("#imgAdd").css("display", "inline");
            $("#imgUnAdd").css("display", "none");
            $("#imgDel").css("display", "inline");
            $("#imgUnDel").css("display", "none");
            $("#imgGetDtl").css("display", "inline");
            $("#imgUnGetDtl").css("display", "none"); 
            $("#btnGetGoods").css("display", "inline");//条码扫描
        }
        else if(operateType == "3")
        {//撤销审批
            $("#imgSave").css("display", "inline");
            $("#imgUnSave").css("display", "none");
            $("#imgAdd").css("display", "inline");
            $("#imgUnAdd").css("display", "none");
            $("#imgDel").css("display", "inline");
            $("#imgUnDel").css("display", "none");
            $("#imgGetDtl").css("display", "inline");
            $("#imgUnGetDtl").css("display", "none");
            $("#btnGetGoods").css("display", "inline");//条码扫描  
        }
    }catch(e){
    }
}


function Fun_CompleteOperate(isComplete)
{ 
    if(isComplete)
    {
    if(!confirm("是否执行结单操作？"))
    return;
        $("#txtBillStatusID").attr("value","4");
        $("#txtBillStatusName").attr("value","手动结单");
        
        $("#HiddenAction").attr("value","Complete");
        var ActionApply = $("#HiddenAction").attr("value");
        var ID = $("#ThisID").attr("value");
            
        var URLParams = "ActionApply="+ActionApply;
        URLParams += "&ID="+ID;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
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
                var aaa = msg.data.split('#');
                $("#txtCloserID").attr("value",aaa[0]);
                $("#txtCloserName").attr("value",aaa[1]);
                $("#txtCloseDate").attr("value",aaa[2]);
                $("#txtModifiedUserName").val(aaa[3]);
                $("#txtModifiedUserID").val(aaa[3])
                $("#txtModifiedDate").val(aaa[2]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单成功！");
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                GetFlowButton_DisplayControl();
            } 
        }); 
    }
    else
    {
    if(!confirm("是否执行取消结单操作？"))
    return;
        $("#txtBillStatusID").attr("value","2");
        $("#txtBillStatusName").attr("value","执行");
        
        $("#HiddenAction").attr("value","ConcelComplete");
        var ActionApply = $("#HiddenAction").attr("value");
        var ID = $("#ThisID").attr("value");
            
        var URLParams = "ActionApply="+ActionApply;
        URLParams += "&ID="+ID;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
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
            var aaa = msg.data.split('#');
                $("#txtCloserID").attr("value","");
                $("#txtCloserName").attr("value","");
                $("#txtCloseDate").attr("value","");
                $("#txtModifiedUserName").val(aaa[1]);
                $("#txtModifiedUserID").val(aaa[1])
                $("#txtModifiedDate").val(aaa[0]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消结单成功！");
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                GetFlowButton_DisplayControl();
            } 
        }); 
    }
    
}
function Fun_ConfirmOperate()
{//确认
 
if(!confirm("是否执行确认操作？"))
    return;
    var ID = document.getElementById("ThisID").value.Trim();
    ActionApply ="Confirm";
    var URLParams = "ActionApply="+ActionApply;
    URLParams += "&ID="+ID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
           
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
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
           
                var aaa = data.data.split('#');
                $("#txtConfirmorID").attr("value",aaa[0]);
                $("#txtConfirmorName").attr("value",aaa[1]);
                $("#txtConfirmDate").attr("value",aaa[2]);
                $("#txtModifiedUserName").val(aaa[3]);
                $("#txtModifiedUserID").val(aaa[3])
                $("#txtModifiedDate").val(aaa[2]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
                GetFlowButton_DisplayControl();
                
                $("#txtBillStatusID").val("2");
                $("#txtBillStatusName").val("执行");
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认失败！");
            } 
        } 
    }); 
}

function Fun_UnConfirmOperate()
{ 
    if(!confirm("是否执行取消确认操作？！"))
        return;
    var URLParams = "ActionApply=CancelConfirm";
    URLParams += "&ID="+$("#ThisID").val();
    URLParams += "&No="+$("#PurApplyNo_txtCode").val();
//    URLParams += "&FromType="+document.getElementById("ddlFromType").value;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
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
            if(msg.sta == 1)
            {
                fnAfterConcelConfirm(msg.sta,msg.data.split('#'));
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认成功！");
                GetFlowButton_DisplayControl();
            }
            else if(msg.sta == 2)
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该单据已被其它单据调用了，不允许取消确认！");
            }
        } 
    });
}

function fnAfterConcelConfirm(flag,BackValue)
{ 
    switch(flag)
    {
        case 1://取消结单成功
            //单据状态修改
            $("#txtConfirmorID").attr("value","");
            $("#txtConfirmorName").attr("value","");
            $("#txtConfirmDate").attr("value","");
            $("#txtModifiedUserName").val(BackValue[1]);
            $("#txtModifiedUserID").val(BackValue[1])
            $("#txtModifiedDate").val(BackValue[0]);
            
            $("#txtBillStatusID").val("1");
            $("#txtBillStatusName").val("制单");
            $("#FlowStatus").val("5");
            $("#IsCite").val("False");
            //按钮控制
            fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
            //调用审批流程按钮控制方法
            GetFlowButton_DisplayControl();
            break;
        case 2:
            break;
    }
}

//根据单据状态决定页面按钮操作
function fnStatus(BillStatus,IsCite) {
 
    try{
        switch (BillStatus) { //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
            case '1': //制单
                $("#HiddenAction").val('Update');
                fnFlowStatus($("#FlowStatus").val())
                break;
            case '2': //执行
                if(IsCite == "True")
                {//被引用不可编辑
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#imgGetDtl").css("display", "none");
                    $("#imgUnGetDtl").css("display", "inline"); 
                    $("#btnGetGoods").css("display", "none");//条码扫描
                }
                else
                {
                    $("#HiddenAction").val('Update');
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    
                    $("#imgAdd").css("display", "inline");
                    $("#imgUnAdd").css("display", "none");
                    $("#imgDel").css("display", "inline");
                    $("#imgUnDel").css("display", "none");
                    $("#imgGetDtl").css("display", "inline");
                    $("#imgUnGetDtl").css("display", "none"); 
                    $("#btnGetGoods").css("display", "inline");//条码扫描
                }
                break;
            case '3': //变更
                $("#HiddenAction").val('Update');
                $("#FromType").attr("disabled", "disabled");
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                
                $("#imgAdd").css("display", "inline");
                $("#imgUnAdd").css("display", "none");
                $("#imgDel").css("display", "inline");
                $("#imgUnDel").css("display", "none");
                $("#imgGetDtl").css("display", "inline");
                $("#imgUnGetDtl").css("display", "none"); 
                $("#btnGetGoods").css("display", "inline");//条码扫描
                break;
            case '4': //手工结单
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#imgGetDtl").css("display", "none");
                $("#imgUnGetDtl").css("display", "inline"); 
                $("#btnGetGoods").css("display", "none");//条码扫描
                break;

            case '5':
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#imgGetDtl").css("display", "none");
                $("#imgUnGetDtl").css("display", "inline"); 
                $("#btnGetGoods").css("display", "none");//条码扫描
                break;
        }
    }catch(e){
    }
}

function fnFlowStatus(FlowStatus)
{
 
try{
    switch (FlowStatus) {
                case "0": //待提交审批         
                    break;
                case "1": //当前单据正在待审批
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#imgGetDtl").css("display", "none");
                    $("#imgUnGetDtl").css("display", "inline");
                    $("#btnGetGoods").css("display", "none");//条码扫描                   
                    break;
                case "2": //当前单据正在审批中
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#imgGetDtl").css("display", "none");
                    $("#imgUnGetDtl").css("display", "inline");
                    $("#btnGetGoods").css("display", "none");//条码扫描
                    break;
                case "3": //当前单据已经通过审核
                    //制单状态的审批通过单据,不可修改
                    if ($("#txtBillStatusID").val() == "1") {
                        $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#imgGetDtl").css("display", "none");
                        $("#imgUnGetDtl").css("display", "inline");
                        $("#btnGetGoods").css("display", "none");//条码扫描
                    }

                    break;
                case "4": //当前单据审批未通过
                    break;
                case "5":
                    $("#imgSave").css("display", "inline");
                    $("#imgUnSave").css("display", "none");
                    $("#imgAdd").css("display", "inline");
                    $("#imgUnAdd").css("display", "none");
                    $("#imgDel").css("display", "inline");
                    $("#imgUnDel").css("display", "none");
                    $("#imgGetDtl").css("display", "inline");
                    $("#imgUnGetDtl").css("display", "none");
                    $("#btnGetGoods").css("display", "inline");//条码扫描 
                    break;    
    }
}catch(e){
}
}

//---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
/*
 参数：商品ID，商品编号，商品名称，去税售价，单位ID，单位名称，销项税率（%），含税售价，销售折扣，规格，类型名称，类型ID，含税进价，去税进价，进项税率(%)，标准成本
*/
//根据条码获取的商品信息填充数据
function GetGoodsDataByBarCode(ID,ProdNo,ProductName,StandardSell,UnitID,UnitName,TaxRate,SellTax,Discount,Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost)
{
 
    if(!IsExist(ID))//如果重复记录，就不增加
    {
       var rowID=AddDtlSSignRow();//插入行
       //填充数据
       BarCode_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification,rowID);
    }
    else
    {
        document.getElementById("DtlSPlanCount"+rerowID).value=parseFloat(document.getElementById("DtlSPlanCount"+rerowID).value)+1;
        fnCalculateTotal();//更改数量后重新计算
    }
    
}

var rerowID="";
//判断是否有相同记录有返回true，没有返回false
function IsExist(ID)
{
 
     var signFrame = findObj("DetailSTable", document);
     if((typeof(signFrame)=="undefined")|| signFrame==null)
     {
        return false;
     }
     for (i = 1; i < signFrame.rows.length; i++) {//判断商品是否在明细列表中
         if (signFrame.rows[i].style.display != "none") {
              var rowid = signFrame.rows[i].id;
              var rid=rowid.substring(8);
              var ProductID = $("#DtlProdID" + rid).val(); //商品ID（对应商品表ID）
              if(ProductID==ID)
               {
                   rerowID=i;
                   return true;
               }
            }
        }
     return false;
}

function BarCode_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification,rowID)
{    
    $("#DtlSProdID"+rowID).val(ID);//商品ID
    $("#DtlSProdNo"+rowID).val(ProdNo);//商品编号
    $("#DtlSProdName"+rowID).val(ProductName);//商品名称
    $("#DtlSSpecification"+rowID).val(Specification);//规格
    $("#DtlSUnitID"+rowID).val(UnitID);//单位ID
    $("#DtlSUnitName"+rowID).val(UnitName);//单位名称
    $("#DtlSPlanCount"+rowID).val(1);//设置数量为1
    fnMergeDetail();
    fnCalculateTotal();
}
//---------------------------------------------------条码扫描END------------------------------------------------------------------------------------
/*库存快照*/


//var signFrame = findObj("DetailSTable", document);
//var ck = document.getElementsByName("chk");
//for (var i = 0; i < ck.length; i++) {
//    var rowID = i + 1;
//    if (ck[i].checked) {
//        signFrame.rows[rowID].style.display = "none";
//    }
//    document.getElementById("DtlSSortNo" + rowID).innerHTML = GenerateNo(rowID);
//}

//    $("[name='chk']").attr("checked", 'true'); //全选
//    $(this).val()
//   
//    var str="";
//    $("[name='chk'][checked]").each(function(){
//    detailRows++;
//    intProductID = document.getElementById('DtlSProdID' + rowID).value;
//    })
//    
//    
//     })

function ShowSnapshot() {
 
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var ck = document.getElementsByName("chk"); 
    var signFrame = findObj("DetailSTable", document);
    for (var i = 0; i < ck.length; i++) 
    {
    
            var rowID = i + 1;
            if (signFrame.rows[rowID].style.display != "none")
             {
                  if (ck[i].checked) 
                    {
                        detailRows++;
                        intProductID = document.getElementById('DtlSProdID' + rowID).value;
                        snapProductName = document.getElementById('DtlSProdName' + rowID).value;
                        snapProductNo = document.getElementById('DtlSProdNo' + rowID).value; 
                    }
            }

    }

    if (detailRows == 1) {
        if (intProductID <= 0 || strlen(cTrim(intProductID, 0)) <= 0) {
            popMsgObj.ShowMsg('选中的明细行中没有添加物品');
            return false;
        }
        ShowStorageSnapshot(intProductID, snapProductName, snapProductNo);

    }
    else {
        popMsgObj.ShowMsg('请选择单个物品查看库存快照');
        return false;
    }
}

//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
 
    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值

    var arrKey = strKey.split('|');
    if (typeof (data) != 'undefined') {
        $.each(data, function(i, item) {
            for (var t = 0; t < arrKey.length; t++) {
                //不为空的字段名才取值
                if ($.trim(arrKey[t]) != '') {
                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);

                }
            }

        });
    }
}
//--------------------扩展属性操作----------------------------------------------------------------------------//
