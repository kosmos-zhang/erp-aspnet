var signForget = false;


$(document).ready(function() {
    if ($("#HiddenMoreUnit").val() == "False") {
    }
    else {
        document.getElementById("SpProductCount").innerText = "基本数量";
        document.getElementById("spCount").style.display = "none";
        document.getElementById("spUsedUnitCount").style.display = "block";
        document.getElementById("SpProductCount2").innerText = "基本数量";
        document.getElementById("spUsedUnitCount2").style.display = "block";
        document.getElementById("spUnitID2").innerText = "基本单位";
        document.getElementById("spUsedUnitID2").style.display = "block";
        document.getElementById("spUnitID").innerText = "基本单位";
        document.getElementById("spUsedUnitID").style.display = "block";


    }

    var requestobj = GetRequest(location.search);
    var SourcePage = requestobj['SourcePage'];
    fnGetSearchCondition(requestobj);
    if (SourcePage == "PlanInfo") {
        $("#btn_back").css("display", "inline");


    }
    else if (SourcePage == "PurRequire") {//采购需求页面进入，生成采购计划                                                                                                         
        //填写隐藏域，用于返回     
        $("#btn_back").css("display", "inline");
        var URLParams = location.search;
        var Action = "FromPlan";
        URLParams += "&ActionPlan=" + Action;


        document.getElementById("HiddenURLParams").value = URLParams;
        document.getElementById("BackFlagHidden").value = "PurRequireInfo";

        var Length = requestobj['Length']; //需生成计划的名明细个数  
        $("#ddlFromType").val("2");
        $("#ddlFromType").attr("disabled", true);
        fnChangeFromType("2");
        for (var i = 0; i < Length; ++i) {//明细来源                                                                                                                             
            var j = AddDtlSSignRow();
            document.getElementById("DtlSProductID" + j).value = requestobj['ProductID' + i];
            document.getElementById("DtlSProductNo" + j).value = requestobj['ProductNo' + i];
            document.getElementById("DtlSProductName" + j).value = requestobj['ProductName' + i];
            document.getElementById("DtlSUnitID" + j).value = requestobj['UnitID' + i];
            document.getElementById("DtlSUnitName" + j).value = requestobj['UnitName' + i];
            document.getElementById("DtlSSpecification" + j).value = requestobj['Specification' + i];
            document.getElementById("DtlSColor" + j).value = requestobj['ColorName' + i];


            //            document.getElementById("DtlSUnitPrice"+j).value = requestobj[''+i];                                                              
            document.getElementById("DtlSRequireCount" + j).value = requestobj['RequireCount' + i];
            document.getElementById("DtlSRequireDate" + j).value = requestobj['RequireDate' + i];
            document.getElementById("DtlSPlanCount" + j).value = requestobj['RequireCount' + i];
            document.getElementById("DtlSPlanTakeDate" + j).value = requestobj['RequireDate' + i];
            document.getElementById("DtlSFromBillID" + j).value = requestobj['ID' + i];
            document.getElementById("DtlSFromBillNo" + j).value = requestobj['MRPNo' + i];
            if ($("#HiddenMoreUnit").val() == "True") {
                GetUnitGroupSelectEx(requestobj['ProductID' + i], "InUnit", "SignItem_TD_UnitID_Select" + j, "ChangeUnit(this.id," + j + "," + (parseFloat(0)).toFixed($("#HiddenPoint").val()) + ")", "unitdiv" + j, '', "FillApplyContent(" + j + "," + (parseFloat(0)).toFixed($("#HiddenPoint").val()) + "," + requestobj['RequireCount' + i] + ")"); //绑定单位组

            }
        }

        fnStatus($("#txtBillStatusID").val(), $("#IsCite").val());
        fnMergeDetail();
        GetFlowButton_DisplayControl();
    }
    var intFromType = requestobj["intFromType"];
    if (intFromType != null) {//个人桌面进入
        $("#btn_back").css("display", "inline");
        $("#HiddenURLParams2").val(location.search);
    }

    var ID = requestobj['ID'];
    if (ID != null) {//表示点击单据编号链接过来的，需要填充
        FillPurchasePlan(ID);

    }
    else {
        GetExtAttr('officedba.PurchasePlan', null);
        signForget = true;
    }
    GetFlowButton_DisplayControl();
    //    fnGetExtAttr();

});

function DoBack()
{
    var TargetPage = document.getElementById("HiddenTargetPage").value;
    var URLParams = document.getElementById("HiddenURLParams").value;
      

    if(TargetPage == "PlanInfo")
    {
        URLParams = URLParams.replace("ModuleID=2041501","ModuleID=2041502");
        window.location.href='PurchasePlanInfo.aspx?'+URLParams;
    }
    else if(TargetPage == "PurRequire")
    {
        URLParams = URLParams.replace("ModuleID=2041501","ModuleID=2041401");
        window.location.href='PurchaseRequireInfo.aspx?'+URLParams;
    }
    if($("#HiddenURLParams2").val() != "0")
    {//来源个人桌面
        var URLParams = document.getElementById("HiddenURLParams2").value;
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
    }
}



//将列表页面的检索条件拼成字符串,然后放进隐藏域，用于传到列表页面
//将来源页面放入隐藏域，用于返回
function fnGetSearchCondition(requestobj)
{
    //检索条件
    var PlanNo=requestobj['No'];
    var PlanTitle =requestobj['Title'];
    var PlanUser =requestobj['PlanUser'];
    var PlanUserName =requestobj['PlanUserName'];
    var TotalMoneyMin =requestobj['TotalMoneyMin'];
    var TotalMoneyMax =requestobj['TotalMoneyMax'];
    var DeptID =requestobj['DeptID'];
    var DeptName =requestobj['DeptName'];
    var StartPlanDate =requestobj['StartPlanDate'];
    var EndPlanDate =requestobj['EndPlanDate'];
    var FlowStatus =requestobj['FlowStatus'];
    var BillStatus =requestobj['BillStatus'];
    var ModuleID=requestobj['ModuleID'];
        var EFDesc =requestobj['EFDesc'];
    var EFIndex=requestobj['EFIndex'];
    
    
    var strParams = "No="+escape(PlanNo);
    strParams += "&Title="+escape(PlanTitle);
    strParams += "&PlanUser="+escape(PlanUser);
    strParams += "&PlanUserName="+escape(PlanUserName);
    strParams += "&TotalMoneyMin="+escape(TotalMoneyMin);
    strParams += "&TotalMoneyMax="+escape(TotalMoneyMax);
    strParams += "&DeptID="+escape(DeptID);
    strParams += "&DeptName="+escape(DeptName);
    strParams += "&StartPlanDate="+escape(StartPlanDate);
    strParams += "&EndPlanDate="+escape(EndPlanDate);
    strParams += "&FlowStatus="+escape(FlowStatus);
    strParams += "&BillStatus="+escape(BillStatus);
    strParams += "&ModuleID="+escape(ModuleID);
    
        strParams += "&EFDesc="+escape(EFDesc);
    strParams += "&EFIndex="+escape(EFIndex);
    //页码
    var pageCount = requestobj['pageCount'];
    var pageIndex = requestobj['pageIndex'];
    strParams += "&pageCount="+escape(pageCount);
    strParams += "&pageIndex="+escape(pageIndex);
    
    //来源页面，就是本页面
    var SourcePage = "ApplyAdd";
    strParams += "&SourcePage="+escape(SourcePage);
    $("#HiddenURLParams").val(strParams);
    
    //设置目标页面，用于返回时使用
    var TargetPage = requestobj['SourcePage'];
    $("#HiddenTargetPage").val(TargetPage);
}

function fnPrint()
{
    var ID = $("#ThisID").val();
    if(parseInt(ID) == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("../../../Pages/PrinttingModel/PurchaseManager/PurchasePlanPrint.aspx?ID="+ID);
}


function FillPurchasePlan(ID)
{
var URLParams = "ID="+ID;

$.ajax({
    type: "POST", //用POST方式传输
    dataType: "json", //数据格式:JSON
    url: "../../../Handler/Office/PurchaseManager/PurchasePlanEdit.ashx", //目标地址
    //       data:'PlanNo='+escape(PlanNo),
    cache: false,
    data: URLParams,
    beforeSend: function() {
    },
    success: function(msg) {

   
        $.each(msg.data, function(i, item) {
            if (item.ProductNo != null && item.ProductNo != "") {
                var index = AddDtlSSignRow();
                $("#DtlSProductID" + index).attr("value", item.ProductID);
                $("#DtlSProductNo" + index).attr("value", item.ProductNo);
                $("#DtlSProductName" + index).attr("value", item.ProductName);
                $("#DtlSSpecification" + index).attr("value", item.Specification);
                $("#DtlSUnitID" + index).attr("value", item.UnitID);
                $("#DtlSUnitName" + index).attr("value", item.UnitName);
                $("#DtlSColor" + index).attr("value", item.ColorName);
                $("#DtlSUnitPrice" + index).attr("value", (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val()));
                $("#DtlSRequireCount" + index).attr("value", (parseFloat(item.RequireCount)).toFixed($("#HiddenPoint").val()));

                $("#DtlSPlanCount" + index).attr("value", (parseFloat(item.PlanCount)).toFixed($("#HiddenPoint").val()));
                $("#DtlSRequireDate" + index).attr("value", item.RequireDate);
                $("#DtlSPlanTakeDate" + index).attr("value", item.PlanTakeDate);
                $("#DtlSApplyReason" + index).attr("value", item.ApplyReasonID);
                $("#DtlSFromBillID" + index).attr("value", item.FromBillID);
                $("#DtlSFromBillNo" + index).attr("value", item.FromBillNo);
                if (item.FromSortNo == "0") {
                    $("#DtlSFromLineNo" + index).attr("value", "");
                }
                else {
                    $("#DtlSFromLineNo" + index).attr("value", item.FromSortNo);
                }

                $("#DtlSProviderID" + index).attr("value", item.ProviderID);
                $("#DtlSProviderName" + index).attr("value", item.ProviderName);
                $("#DtlProviderID" + index).attr("value", item.ProviderID);

                $("#DtlProviderName" + index).attr("value", item.ProviderName);
                $("#DtlSRemark" + index).attr("value", item.Remark);

                $("#DtlSTotalPrice" + index).attr("value", (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()));



                if ($("#HiddenMoreUnit").val() == "True") {
                    $("#UsedUnitCount" + index).attr("value", (parseFloat(item.UsedUnitCount)).toFixed($("#HiddenPoint").val()));
                    $("#UsedPrice" + index).attr("value", (parseFloat(item.UsedPrice)).toFixed($("#HiddenPoint").val()));
                    var unitPrice = (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val());
                    var sign = "";
                    if (item.FromBillNo != "") {
                        sign = "Bill";
                    }

                    GetUnitGroupSelectEx(item.ProductID, "InUnit", "SignItem_TD_UnitID_Select" + index, "ChangeUnit(this.id," + index + "," + unitPrice + ")", "unitdiv" + index, "", "FillSelect('" + index + "','" + item.UsedUnitID + "','" + sign + "')"); //绑定单位组 
                }
            }


        });

        $.each(msg.data2, function(i, item) {
            if (item.ProductNo != null && item.ProductNo != "") {

                var index = AddDtlSignRow();
                $("#DtlProductID" + index).attr("value", item.ProductID);

                $("#DtlProductNo" + index).attr("value", item.ProductNo);
                $("#DtlProductName" + index).attr("value", item.ProductName);
                $("#DtlSpecification" + index).attr("value", item.Specification);
                $("#DtlUnitID" + index).attr("value", item.UnitID);

                $("#DtlUnitName" + index).attr("value", item.UnitName);

                $("#DtlProductCount" + index).attr("value", (parseFloat(item.ProductCount)).toFixed($("#HiddenPoint").val()));
                $("#DtlRequireDate" + index).attr("value", item.RequireDate);
                $("#DtlProviderID" + index).attr("value", item.ProviderID);


                $("#DtlSSColor" + index).attr("value", item.ColorName);
                
                $("#DtlProviderName" + index).attr("value", item.ProviderName);

                $("#DtlUnitPrice" + index).attr("value", (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val()));
                $("#DtlTotalPrice" + index).attr("value", (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()));
                $("#DtlRemark" + index).attr("value", item.Remark);
                $("#DtlOrderCount" + index).val((parseFloat(item.OrderCount)).toFixed($("#HiddenPoint").val()));
                if ($("#HiddenMoreUnit").val() == "True") {

                    $("#Used2UnitCount" + index).attr("value", (parseFloat(item.UsedUnitCount)).toFixed($("#HiddenPoint").val()));
                    $("#Used2Price" + index).attr("value", (parseFloat(item.UsedPrice)).toFixed($("#HiddenPoint").val()));
                    $("#Hidden_TD2_Text_Exrate_" + index).attr("value", item.ExRate);
                    $("#TD2_Text_Used2UnitCount_" + index).val(item.UsedUnitName);
                    $("#Hidden_TD2_Text_Used2UnitCount_" + index).val(item.UsedUnitID);

                }



            }
        });
        
        $.each(msg.datap, function(i, item) {
            if (item.PlanNo != null && item.PlanNo != "") {
                //                        $("#txtPlanNo").attr("value",item.PlanNo);
                document.getElementById("AddTitle").style.display = "none";
                document.getElementById("UpdateTitle").style.display = "block";

                document.getElementById("PurPlanNo_txtCode").value = item.PlanNo;
                document.getElementById('PurPlanNo_txtCode').className = 'tdinput';
                document.getElementById('PurPlanNo_txtCode').style.width = '90%';
                document.getElementById('PurPlanNo_ddlCodeRule').style.display = 'none'

                $("#ThisID").attr("value", item.ID); //
                $("#txtTitle").attr("value", item.Title); //
                if (item.TypeID != 0)
                    $("#PurchaseType_ddlCodeType").attr("value", item.TypeID); //
                $("#UserPlanName").val(item.PlanUserName); //
                $("#txtPlanID").val(item.PlanUserID);
                $("#UserPurchaserName").val(item.PurchaserName); //
                $("#txtPurchaserID").val(item.PurchaserID);
                $("#DeptName").val(item.PlanDeptName); //

                $("#txtDeptID").val(item.PlanDeptID); //
                $("#txtPlanDate").attr("value", item.PlanDate);
                $("#ddlFromType").attr("value", item.FromType); //

                $("#txtPlanCnt").attr("value", (parseFloat(item.CountTotal)).toFixed($("#HiddenPoint").val()));
                $("#txtPlanMoney").attr("value", (parseFloat(item.PlanMoney)).toFixed($("#HiddenPoint").val()));

                $("#txtCreatorName").attr("value", item.CreatorName);
                $("#txtCreatorID").attr("value", item.Creator);
                $("#txtCreateDate").attr("value", item.CreateDate); //
                $("#txtBillStatusName").attr("value", item.BillStatusName); //
                $("#txtBillStatusID").attr("value", item.BillStatus); //
                $("#txtConfirmorName").attr("value", item.ConfirmorName); //
                $("#txtConfirmorID").attr("value", item.Confirmor); //
                $("#txtConfirmorDate").attr("value", item.ConfirmDate); //
                $("#txtModifiedUserName").attr("value", item.ModifiedUserID); //
                $("#txtModifiedUserID").attr("value", item.ModifiedUserID); //
                $("#txtModifiedDate").val(item.ModifiedDate);
                $("#txtRemark").attr("value", item.Remark); //
                $("#OrderCount").css("display", "inline");
                //根据单据状态确定隐藏域HiddenAction的值
                //                        if(item.BillStatus == '1')
                //                        {
                document.getElementById("HiddenAction").value = "Update";
                //                        }
                $("#FlowStatus").val(item.FlowStatus);
                $("#IsCite").val(item.IsCite);
                fnStatus($("#txtBillStatusID").val(), $("#IsCite").val());
            }
            var TableName = "officedba.PurchasePlan";
            GetExtAttr(TableName, msg.datap);

        });


    },
    error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
    complete: function() { hidePopup(); signForget = true; GetFlowButton_DisplayControl(); } //接收数据完毕
});
       
}


 function FillSelect(rowID,UsedUnitID,sign)
 { if (sign=="Bill")
 {
 document .getElementById ("SignItem_TD_UnitID_Select"+rowID ).disabled=true ;
 }
      jsSelectItemFromSelect(document .getElementById ("SignItem_TD_UnitID_Select"+rowID ),UsedUnitID);
 }
function jsSelectItemFromSelect(objSelect, objItemValue) {        
    //判断是否存在        
 
        for (var i = 0; i < objSelect.options.length; i++) 
        {        
        var sx=objSelect.options[i].value.split('|')[0].toString() ;
            if (sx== objItemValue)
             {        
                objSelect.options[i].selected =true ;        
                break;        
            }        
        }        
         
       
}  

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
  
  function CountBaseNum()
{

    var List_TB=findObj("Tb_DtlS",document);
    var rowlength=List_TB.rows.length;
    var countnum = 0;
    var countprice = 0;
    for(var i=1;i<rowlength;i++)
    {
        if(List_TB.rows[i].style.display!="none")
        { 
            
             if($("#HiddenMoreUnit").val()=="True")//启用时(重新计算)
             {
                var EXRate = $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString(); /*比率*/
                var OutCount = $("#UsedUnitCount"+i).val();/*采购数量*/
                 
                if (OutCount != '')
                {
                    var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val()); 
                    $("#DtlSPlanCount"+i).val(tempcount);/*基本数量根据出库数量和比率算出*/ 
                }
            }
        }
    }
    
    
    
}

function SavePurPlan()
{
    if(CheckPurPlan())
    {//校验
        return;
    }
    //主表信息
    var URLParams = GetBaseInfo();
    
    //明细来源信息
    URLParams += GetDtlSInfo();
    
    //明细信息
    URLParams += GetDtlInfo()+GetExtAttrValue();

    
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchasePlan_Add.ashx",
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
                //设置编辑模式
                if(document.getElementById("HiddenAction").value == "Add")
                {
                    document.getElementById("HiddenAction").value = "Update";
                    
                    document.getElementById("PurPlanNo_txtCode").value = data.data.split('#$@')[0];
                    document.getElementById('PurPlanNo_txtCode').className='tdinput';
                    document.getElementById('PurPlanNo_txtCode').style.width='90%';
                    document.getElementById('PurPlanNo_ddlCodeRule').style.display='none'
                    
                    document.getElementById("ThisID").value = data.data.split('#$@')[1];
                    
                    //填写制单人、制单日期
                    $("#txtCreatorName").val(data.data.split('#$@')[3]);
                    $("#txtCreatorID").val(data.data.split('#$@')[2]);
                    $("#txtCreateDate").val(data.data.split('#$@')[4]);
                }
                //填写最后修改人、最后修改日期
                $("#txtModifiedUserName").val(data.data.split('#$@')[5]);
//                $("#txtModifiedUserID").val(data.data.split('#$@')[3]);
                $("#txtModifiedDate").val(data.data.split('#$@')[4]);
                
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
            else if(data.sta == 3)
            {
                //填写最后修改人、最后修改日期
                $("#txtModifiedUserName").val(data.data.split('#$@')[0]);
                $("#txtModifiedDate").val(data.data.split('#$@')[1]);
                if($("#txtBillStatusID").val() == "2")
                {//执行状态的保存改变单据状态
                    $("#txtBillStatusID").val("3")
                    $("#txtBillStatusName").val("变更")
                    
                    
                    $("#txtConfirmorID").attr("value","");
                    $("#txtConfirmorName").attr("value","");
                    $("#txtConfirmorDate").attr("value","");
                }
                
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if (data.sta==0)
            {
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.info);
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
            GetFlowButton_DisplayControl();
        } 
    }); 
    
}
//基础信息

function GetBaseInfo()
{
    var ActionPlan = document.getElementById("HiddenAction").value;
    var strParams = "ActionPlan=" + ActionPlan;//编辑标识
    //单据编号
    if(ActionPlan =="Add")
    {
        codeRule = document.getElementById("PurPlanNo_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            strParams += "&PurPlanNo=" + document.getElementById("PurPlanNo_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("PurPlanNo_ddlCodeRule").value.Trim();
        }
    }
    else if(ActionPlan == "Update")
    {//此处有些问题，回头处理
        strParams += "&PurPlanNo=" + document.getElementById("PurPlanNo_txtCode").value.Trim();
        strParams += "&ID="+document.getElementById("ThisID").value.Trim();
    }
//    strParams += "&ID="+document.getElementById("ThisID").value.Trim();
    strParams += "&Title=" +escape(document.getElementById("txtTitle").value.Trim());
    strParams += "&PurchaseType=" + document.getElementById("PurchaseType_ddlCodeType").value.Trim();
    strParams += "&PlanUserID=" + document.getElementById("txtPlanID").value.Trim();
    strParams += "&PurchaserID=" + document.getElementById("txtPurchaserID").value.Trim();
    strParams += "&PlanDeptID=" + document.getElementById("txtDeptID").value.Trim();
    strParams += "&FromType=" + document.getElementById("ddlFromType").value.Trim();
    strParams += "&PlanDate="+document.getElementById("txtPlanDate").value.Trim();
    //合计信息
    strParams += "&PlanMoney=" + document.getElementById("txtPlanMoney").value.Trim();
    strParams += "&CountTotal=" + document.getElementById("txtPlanCnt").value.Trim();
    
    
    //备注信息
    strParams += "&CreatorID=" + document.getElementById("txtCreatorID").value.Trim();
    strParams += "&CreateDate=" + document.getElementById("txtCreateDate").value.Trim();
    strParams += "&BillStatusID=" + document.getElementById("txtBillStatusID").value.Trim();
//    strParams += "&ConfirmorID=" + document.getElementById("txtConfirmorID").value.Trim();
//    strParams += "&ConfirmDate=" + document.getElementById("txtConfirmDate").value.Trim();
    strParams += "&ModifiedUserID=" + document.getElementById("txtModifiedUserID").value.Trim();
    strParams += "&ModifiedDate=" + document.getElementById("txtModifiedDate").value.Trim();
//    strParams += "&CloserID=" + document.getElementById("txtCloserID").value;
//    strParams += "&CloseDate=" + document.getElementById("txtCloseDate").value;
    
    return strParams;
}
//从原单选择明细
function GetSource()
{ 
//debugger ;
    //源单类型ID
    var FromType = document.getElementById("ddlFromType").value.Trim();
    
    if(FromType == 0)
    {
    }
    else if(FromType == 1)
    {
        
//          document .getElementById ("imgsure").style.dispaly="Block";
        popPurchaseApplyUC.ShowList();
        
    }
    else if(FromType == 2)
    
    {        
//            document .getElementById ("imgsure2").style.dispaly="Block";
        popPurchaseRequireUC.ShowList();
    } 
}

function ShowProdInfo()
{
 popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"','Check');
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
             type: "POST", //用POST方式传输
             dataType: "json", //数据格式:JSON
             url: '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str=' + str, //目标地址
             cache: false,
             data: '', //数据
             beforeSend: function() { }, //发送数据之前

             success: function(msg) {
                 //数据获取完毕，填充页面据显示
                 //数据列表
                 $.each(msg.data, function(i, item) {

                     //填充物品ID，物品编号，物品名称,单位ID，单位名称，规格，
                     if (!IsExistCheck(item.ProdNo)) {

                         var Index = AddDtlSSignRow();
                         $("#DtlSProductID" + Index).attr("value", item.ID);
                         $("#DtlSProductNo" + Index).attr("value", item.ProdNo);
                         $("#DtlSProductName" + Index).attr("value", item.ProductName);
                         $("#DtlSUnitID" + Index).attr("value", item.UnitID);
                         $("#DtlSUnitName" + Index).attr("value", item.CodeName);
                         $("#DtlSSpecification" + Index).attr("value", item.Specification);
                         $("#DtlSUnitPrice" + Index).attr("value", item.StandardBuy);
                         $("#DtlSColor" + Index).attr("value", item.ColorName);
                        
                         if ($("#HiddenMoreUnit").val() == "True") {
                             GetUnitGroupSelectEx(item.ID, "InUnit", "SignItem_TD_UnitID_Select" + Index, "ChangeUnit(this.id," + Index + "," + item.StandardBuy + ")", "unitdiv" + Index, '', "FillContent(" + Index + "," + item.StandardBuy + ")"); //绑定单位组

                         }


                     }
                 });

             },

             complete: function() { } //接收数据完毕
         });
      closeProductdiv();
}  

function ChangeUnit(ObjID/*下拉列表控件（单位）*/,RowID/*行号*/,UsedPrice/*基本单价*/)
 {
  
    var EXRate = $("#SignItem_TD_UnitID_Select"+RowID).val().split('|')[1].toString(); /*比率*/
  
           $("#UsedPrice"+RowID).attr("value", (parseFloat(UsedPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
     
           $("#UsedPricHid"+RowID).attr("value", (parseFloat(UsedPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));

    var OutCount = $("#UsedUnitCount"+RowID).val();/*采购数量*/
    
    if (OutCount != '')
    {
        var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val());
        var tempprice=parseFloat(EXRate*UsedPrice).toFixed($("#HiddenPoint").val());
           var totalPrice=parseFloat(OutCount*tempprice).toFixed($("#HiddenPoint").val());
            
        $("#DtlSPlanCount"+RowID).val(tempcount);/*基本数量根据计划数量和比率算出*/
        $("#UsedPrice"+RowID).val(tempprice);/*实际单价根据基本单价和比率算出*/
             $("# DtlSTotalPrice"+RowID).val(tempprice);/*实际总价根据实际单价和实际数量算出*/
            
    
    }
    fnMergeDetail();
 }
 function FillApplyContent(rowID,UnitPrice,ProductCount,UsedUnitCount,UsedUnitID,signn)
{  
 
if (signn =="Require"||signn =="Goods")
{

}
else
{
         FillSelect(rowID,UsedUnitID,"Bill");
         }
  var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
           $("#UsedPrice"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
           $("#UsedPricHid"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
      
              if (signn =="Require")
{      

 }
                  else
                  {
            
                  $("#UsedUnitCount"+rowID).attr("value", (parseFloat(UsedUnitCount) ).toFixed($("#HiddenPoint").val()));
                  }
                    $("#DtlSPlanCount"+rowID).attr("value", (parseFloat(ProductCount)).toFixed($("#HiddenPoint").val()));
                  
                  if (signn =="Goods")
                  {
                   var planCount = $("#UsedUnitCount"+rowID).val();/*采购数量*/

           if (planCount !='')
           {
           
           var totalPrice=parseFloat(planCount*UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
            $("#DtlSTotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
           
       $("#DtlSPlanCount"+rowID).attr("value", parseFloat(planCount*EXRate).toFixed($("#HiddenPoint").val()));
          
           }
                  }
                  else
                  {
                   var planCount = $("#DtlSPlanCount"+rowID).val();/*采购数量*/

           if (planCount !='')
           {
           
           var totalPrice=parseFloat(planCount*UnitPrice/EXRate).toFixed($("#HiddenPoint").val());
            $("#DtlSTotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
           
       $("#UsedUnitCount"+rowID).attr("value", parseFloat(planCount/EXRate).toFixed($("#HiddenPoint").val()));
          
           }
                  }
                         
                
                   
             
           fnMergeDetail();
}
 
 
 
 
 function FillContent(rowID,UnitPrice)
{ 
 
 var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
           $("#UsedPrice"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
           $("#UsedPricHid"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
          
              var planCount = $("#UsedUnitCount"+rowID).val();/*采购数量*/

           if (planCount !='')
           {
           var tempcount=parseFloat(planCount/EXRate).toFixed($("#HiddenPoint").val());
           var totalPrice=parseFloat(planCount*UnitPrice*EXRate).toFixed($("#HiddenPoint").val());

            $("#DtlSTotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
          $("#DtlSPlanCount"+rowID).attr("value", (parseFloat(tempcount)).toFixed($("#HiddenPoint").val()));
          
           }
       
      fnMergeDetail();
}

function IsExistCheck(prodNo)
{
    var sign=false ;
    var signFrame = findObj("Tb_DtlS",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
          var prodNo1 = document.getElementById("DtlSProductNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}
//
function GetDtlSInfo()
{
    var index = 0;//明细来源条数
    var DtlSParams="";
    var DtlSSignFrame = document.getElementById("Tb_DtlS");
    for( var i =1  ; i<DtlSSignFrame.rows.length;++i)
    {
        if(DtlSSignFrame.rows[i].style.display!="none")
        { 
            DtlSParams+="&DtlSSortNo"+index+"=" +   escape ( document.getElementById("DtlSSortNo" + i).innerHTML);
            DtlSParams+="&DtlSProductID"+index+"=" + escape ( document.getElementById("DtlSProductID" + i).value.Trim());
            DtlSParams+="&DtlSProductNo"+index+"=" +escape (  document.getElementById("DtlSProductNo" + i).value.Trim());
            DtlSParams+="&DtlSProductName"+index+"=" + escape ( document.getElementById("DtlSProductName" + i).value.Trim());
            DtlSParams+="&DtlSUnitID"+index+"=" +escape (  document.getElementById("DtlSUnitID" + i).value.Trim());
            DtlSParams+="&DtlSUnitPrice"+index+"=" +escape (  document.getElementById("DtlSUnitPrice" + i).value.Trim());
            DtlSParams+="&DtlSTotalPrice"+index+"=" + escape ( document.getElementById("DtlSTotalPrice" + i).value.Trim());
            DtlSParams+="&DtlSRequireCount"+index+"=" + escape ( document.getElementById("DtlSRequireCount" + i).value.Trim());
            DtlSParams+="&DtlSRequireDate"+index+"=" + escape ( document.getElementById("DtlSRequireDate" + i).value.Trim());
            DtlSParams+="&DtlSProviderID"+index+"=" +escape (  document.getElementById("DtlSProviderID" + i).value.Trim());
            DtlSParams+="&DtlSApplyReasonID"+index+"=" +escape (  document.getElementById("DtlSApplyReason" + i).value.Trim());
            DtlSParams+="&DtlSPlanCount"+index+"=" +escape (  document.getElementById("DtlSPlanCount" + i).value.Trim());
            DtlSParams+="&DtlSPlanTakeDate"+index+"=" + escape ( document.getElementById("DtlSPlanTakeDate" + i).value.Trim());
            
            DtlSParams+="&DtlSFromBillID"+index+"=" + escape ( document.getElementById("DtlSFromBillID" + i).value.Trim());
            DtlSParams+="&DtlSFromBillNo"+index+"=" + escape ( document.getElementById("DtlSFromBillNo" + i).value.Trim());
            DtlSParams+="&DtlSFromLineNo"+index+"=" + escape ( document.getElementById("DtlSFromLineNo" + i).value.Trim());
          if($("#HiddenMoreUnit").val()=="True") 
          {
             var ExRate= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString();
                   var UsedUnitID= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[0].toString();
                   
            DtlSParams+="&UsedUnitID"+index+"=" + escape ( UsedUnitID);
            DtlSParams+="&UsedUnitCount"+index+"=" + escape ( document.getElementById("UsedUnitCount" + i).value.Trim());
            DtlSParams+="&UsedPrice"+index+"=" + escape ( document.getElementById("UsedPrice" + i).value.Trim());
              DtlSParams+="&ExRate"+index+"=" + escape (ExRate);
            }
            
            ++index;
        }
        
    }
    DtlSParams+="&DtlSLength="+index;
    return DtlSParams;
}

function GetDtlInfo()
{
    var dtlindex = 0;
    var DtlParams="";
    var DtlSignFrame = document.getElementById("Tb_Dtl");
    for(var i=1;i<DtlSignFrame.rows.length;++i)
    {
        if(DtlSignFrame.rows[i].style.display!="none")
        {
            
            DtlParams+="&DtlProductID"+dtlindex+"=" +escape (  document.getElementById("DtlProductID" + i).value.Trim());
            DtlParams+="&DtlProductNo"+dtlindex+"=" + escape ( document.getElementById("DtlProductNo" + i).value.Trim());
            DtlParams+="&DtlProductName"+dtlindex+"=" +escape (  document.getElementById("DtlProductName" + i).value.Trim());
            DtlParams+="&DtlUnitID"+dtlindex+"=" + escape ( document.getElementById("DtlUnitID" + i).value.Trim());
            DtlParams+="&DtlUnitPrice"+dtlindex+"=" +escape (  document.getElementById("DtlUnitPrice" + i).value.Trim());
            DtlParams+="&DtlTotalPrice"+dtlindex+"=" + escape ( document.getElementById("DtlTotalPrice" + i).value.Trim());
            DtlParams+="&DtlProductCount"+dtlindex+"=" + escape ( document.getElementById("DtlProductCount" + i).value.Trim());
            DtlParams+="&DtlRequireDate"+dtlindex+"=" + escape ( document.getElementById("DtlRequireDate" + i).value.Trim());
            DtlParams+="&DtlProviderID"+dtlindex+"=" +escape (  document.getElementById("DtlProviderID" + i).value.Trim());
            DtlParams+="&DtlRemark"+dtlindex+"=" +escape (  document.getElementById("DtlRemark" + i).value.Trim());
                    if($("#HiddenMoreUnit").val()=="True") 
          {
             var ExRate= $("#Hidden_TD2_Text_Exrate_"+i).val();
                   var UsedUnitID= $("#Hidden_TD2_Text_Used2UnitCount_"+i).val();
                   
            DtlParams+="&Used2UnitID"+dtlindex+"=" + escape ( UsedUnitID);
            DtlParams+="&Used2UnitCount"+dtlindex+"=" + escape ( document.getElementById("Used2UnitCount" + i).value.Trim());
            DtlParams+="&Used2Price"+dtlindex+"=" + escape ( document.getElementById("Used2Price" + i).value.Trim());
              DtlParams+="&Ex2Rate"+dtlindex+"=" + escape (ExRate);
            }
            
            ++dtlindex;
        }
        
    }
    DtlParams+="&DtlLength="+dtlindex;
    return DtlParams;
}

function Fun_ConfirmOperate()
{//确认
    var ActionPlan = document.getElementById("HiddenAction").value
    
    if(ActionPlan == "Add")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存再确认！");
        return;
    }
    if(!confirm("是否确定执行确认操作！"))
    return;
    
    document.getElementById("txtBillStatusName").value = "执行";
    document.getElementById("txtBillStatusID").value = "2";
    ActionPlan ="Confirm";
    var ID = document.getElementById("ThisID").value;
    var URLParams = "ActionPlan="+ActionPlan;
    URLParams += "&ID="+ID;
    URLParams += "&No="+$("#PurPlanNo_txtCode").val();
   
    //回写采购申请中已计划数量
    var signFrame = findObj("Tb_DtlS",document);
    var ProductLength = 0;
    var FromType = $("#ddlFromType").val();

    URLParams += "&FromType=" + FromType;
    
    
    
    
    
    
    
    if((signFrame != null)&&(FromType!="0"))
    {
        for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display != "none")
            {
                var FromBillID = $("#DtlSFromBillID"+i).val();
                if((FromBillID!="0")&&(FromBillID!=null))
                {    
                    var FromBillNo = $("#DtlSFromBillNo"+i).val()           
                    var ProductID = $("#DtlSProductID"+i).val();                    
                    var FromLineNo = $("#DtlSFromLineNo"+i).val();
                    var PlanCount = $("#DtlSPlanCount"+i).val();
                    var UsedUnitCount1 = $("#UsedUnitCount" + i).val();
                    
                    URLParams += "&ProductID"+ProductLength+"="+ProductID;
                    URLParams += "&FromBillID"+ProductLength+"="+FromBillID;
                    URLParams += "&FromBillNo"+ProductLength+"="+FromBillNo;
                    URLParams += "&FromLineNo"+ProductLength+"="+FromLineNo;
                    URLParams += "&PlanCount"+ProductLength+"="+PlanCount;
                      if($("#HiddenMoreUnit").val()=="True")
                      {
                       URLParams += "&UsedUnitCount"+ProductLength+"="+UsedUnitCount1;
                      }
                    
                    ProductLength++;
                }
            }
        }
    }
    URLParams += "&ProductLength="+ProductLength;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchasePlan_Add.ashx",
           
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
                $("#txtConfirmorDate").attr("value",aaa[2]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
                GetFlowButton_DisplayControl();
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
            }
            else if(data.sta == 2)
            {
                hidePopup();
                document.getElementById("txtBillStatusName").value = "制单";
                document.getElementById("txtBillStatusID").value = "1";
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.data);
            }
            else 
            { 
                hidePopup();
                document.getElementById("txtBillStatusName").value = "制单";
                document.getElementById("txtBillStatusID").value = "1";
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认失败！");
            } 
        } 
    }); 
    GetFlowButton_DisplayControl();
}

function Fun_UnConfirmOperate()
{
    if(!confirm("是否确定执行取消确认操作！"))
        return;
    var URLParams = "ActionPlan=CancelConfirm";
    URLParams += "&ID="+$("#ThisID").val();
    URLParams += "&PlanNo="+$("#PurPlanNo_txtCode").val();
//    URLParams += "&FromType="+document.getElementById("ddlFromType").value;
    var signFrame = findObj("Tb_DtlS",document);
    var ProductLength = 0;
    var FromType = $("#ddlFromType").val();

    URLParams += "&FromType="+FromType;
    if((signFrame != null)&&(FromType!="0"))
    {
        for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display != "none")
            {
                var FromBillID = $("#DtlSFromBillID"+i).val();
                if((FromBillID!="0")&&(FromBillID!=null))
                {    
                    var FromBillNo = $("#DtlSFromBillNo"+i).val()           
                    var ProductID = $("#DtlSProductID"+i).val();                    
                    var FromLineNo = $("#DtlSFromLineNo"+i).val();
                    var PlanCount = $("#DtlSPlanCount"+i).val();

                    var UsedUnitCount1 = $("#UsedUnitCount" + i).val();
                    URLParams += "&ProductID"+ProductLength+"="+ProductID;
                    URLParams += "&FromBillID"+ProductLength+"="+FromBillID;
                    URLParams += "&FromBillNo"+ProductLength+"="+FromBillNo;
                    URLParams += "&FromLineNo"+ProductLength+"="+FromLineNo;
                    URLParams += "&PlanCount"+ProductLength+"="+PlanCount;
                    if ($("#HiddenMoreUnit").val() == "True") {
                        URLParams += "&UsedUnitCount" + ProductLength + "=" + UsedUnitCount1;
                    }
                    ProductLength++;
                }
            }
        }
    }
    URLParams += "&ProductLength="+ProductLength;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchasePlan_Add.ashx",
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
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
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
        case 1:
            //确认人，确认时间，单据状态
            $("#txtConfirmorID").val("");
            $("#txtConfirmorName").val("");
            $("#txtConfirmorDate").val("");
            $("#txtBillStatusName").val("制单");
            $("#txtBillStatusID").val("1");
            $("#FlowStatus").val("5")
            //最后更新人最后更新时间
            $("#txtModifiedUserName").val(BackValue[1]);
            $("#txtModifiedUserID").val(BackValue[1]);
            $("#txtModifiedDate").val(BackValue[0]);
            break;
        case 2:
            break;
    }
}

function Fun_CompleteOperate(isComplete)
{
    if(isComplete)
    {
        var BillStatusID = document.getElementById("txtBillStatusID").value;
        if(BillStatusID != '2')
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只有执行状态的单据才能结单！");
            return;
        }
        if(!confirm("是否确定执行结单操作？"))
        return;
        document.getElementById("txtBillStatusName").value = "手工结单";
        document.getElementById("txtBillStatusID").value = "4";
        
        ActionPlan ="Complete";
        var ID = $("#ThisID").attr("value");
            
        var URLParams = "ActionPlan="+ActionPlan;
        URLParams += "&ID="+ID;
                        
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchasePlan_Add.ashx",
               
            dataType:'json',//返回json格式数据
            data:URLParams,
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
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单成功！");
                    var aaa = data.data.split('#');
                $("#txtCloserID").attr("value",aaa[0]);
                $("#txtCloserName").attr("value",aaa[1]);
                $("#txtCloseDate").attr("value",aaa[2]);
                $("#txtModifiedUserName").val(aaa[3]);
                $("#txtModifiedDate").val(aaa[2]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单成功！");
                GetFlowButton_DisplayControl();
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                } 
                else
                {
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单失败！");
                }
            } 
        }); 
    }
    
    else if(!isComplete)
    {
        if(!confirm("是否确定执行取消结单操作？"))
        return;
        $("#txtBillStatusID").attr("value","2");
        $("#txtBillStatusName").attr("value","执行");
        
        var ActionPlan = "ConcelComplete";
        var ID = $("#ThisID").attr("value");
            
        var URLParams = "ActionPlan="+ActionPlan;
        URLParams += "&ID="+ID;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchasePlan_Add.ashx",
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
             if (msg .sta !="2")
             {
                var aaa = msg.data.split('#');
                $("#txtCloserID").attr("value","");
                $("#txtCloserName").attr("value","");
                $("#txtCloseDate").attr("value","");
                $("#txtModifiedUserName").val(aaa[1]);
                $("#txtModifiedDate").val(aaa[0]);
                
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消结单成功！");
                GetFlowButton_DisplayControl();
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                }
                else
                {
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消结单失败！");
                }
            } 
        }); 
    }
    GetFlowButton_DisplayControl();
}

//有错返回true，没有错返回false
function CheckPurPlan()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;

    var ssw = CheckSpecialWords();
    if (ssw != "") {
           isErrorFlag = true;
        var tmpKeys = ssw.split(',');
        for (var i = 0; i < tmpKeys.length; i++) {
              isErrorFlag = true;
            fieldText = fieldText + tmpKeys[i].toString() + "|";
            msgText = msgText + "不能含有特殊字符|";
        }
    }
    
    
    
    var actionPlan = document.getElementById("HiddenAction").value;
    if("Add"==actionPlan)
    {//单据规则
        codeRule = document.getElementById("PurPlanNo_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            var PurPlanNo = document.getElementById("PurPlanNo_txtCode").value.Trim();
            //编号必须输入
            if (PurPlanNo == "")
            {
                isErrorFlag = true;
                fieldText += "编号|";
   		        msgText += "请输入编号|";
            }
            else if(!CodeCheck(PurPlanNo))
            {
                isErrorFlag = true;
                fieldText = fieldText + "编号|";
	            msgText = msgText +  "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
            }
            else if(strlen(PurPlanNo) >50)
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
    if(strlen(document.getElementById("txtTitle").value.Trim())>100)
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "主题不能长于100|";
    }
    
    var PurchaseType = document.getElementById("PurchaseType_ddlCodeType").value.Trim();
    if(PurchaseType == "")
    {
        isErrorFlag = true;
        fieldText += "采购类别|";
        msgText += "请选择采购类别|";
    }
    var PlanUserName = document.getElementById("txtPlanID").value.Trim(); 
    if(PlanUserName =="")
    {
        isErrorFlag = true;
        fieldText += "计划员|";
        msgText += "请输入计划员|";
    }
    
    var PurchaserName = document.getElementById("txtPurchaserID").value.Trim(); 
    if(PurchaserName =="")
    {
        isErrorFlag = true;
        fieldText += "采购员|";
        msgText += "请输入采购员|";
    }
    var PlanDeptName = document.getElementById("txtDeptID").value.Trim(); 
    if(PlanDeptName =="")
    {
        isErrorFlag = true;
        fieldText += "采购部门|";
        msgText += "请输入采购部门|";
    }
    var PlanDate = $("#txtPlanDate").val();
    if(PlanDate == "")
    {
        isErrorFlag = true;
        fieldText += "计划时间|";
        msgText += "请输入计划时间|";
    }
    //备注长度不大于200
    var PlanDeptName = document.getElementById("txtRemark").value.Trim(); 
    if(strlen(PlanDeptName)>200)
    {
        isErrorFlag = true;
        fieldText += "备注|";
        msgText += "备注长度不能大于200|";
    }
    
    var signFrame = findObj("Tb_DtlS",document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        var RealCount = 0;
        for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                RealCount++;
                //明细中物品不能为空
                if (document.getElementById("DtlSProductNo"+i).value.Trim() == "")
                {
                    isErrorFlag = true;
                    fieldText += "物品|";
                    msgText += "请输入物品|";
                }
                //单价符合格式
                var UnitPrice = document.getElementById("DtlSUnitPrice"+i).value.Trim();
                if (document.getElementById("DtlSUnitPrice"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("DtlSUnitPrice"+i).value.Trim(),14,$("#HiddenPoint").val()))
                {
                    isErrorFlag = true;
                    fieldText += "单价|";
                    msgText += "请输入正确的单价|";
                }
                //明细中数量符合格式
                var ProductCount = document.getElementById("DtlSPlanCount"+i).value.Trim();
                if (document.getElementById("DtlSPlanCount"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("DtlSPlanCount"+i).value.Trim(),14,$("#HiddenPoint").val()))
                {
                    isErrorFlag = true;
                    fieldText += "计划数量|";
                    msgText += "请输入正确的计划数量|";
                }
                else
                {
                  if (ProductCount>0)
                  {}
                  else
                  {
                     isErrorFlag = true;
                    fieldText += "计划数量|";
                    msgText += "计划数量需大于零|";
                  }
                }
                //计划交货日期符合格式
                var PlanTakeDate = document.getElementById("DtlSPlanTakeDate"+i).value.Trim();
                if (PlanTakeDate == ""||!isDate(PlanTakeDate,12,4))
                {
                    isErrorFlag = true;
                    fieldText += "计划交货日期|";
                    msgText += "请输入正确的计划交货日期|";
                }
                //计划交货日期符合格式
                var ProviderName = document.getElementById("DtlSProviderName"+i).value.Trim();
                if (ProviderName == "")
                {
                    isErrorFlag = true;
                    fieldText += "供应商|";
                    msgText += "请输入供应商|";
                }
                //备注长度不大于200
                var DtlSRemark = $("#DtlSRemark"+i).val();
                if(strlen(DtlSRemark)>200)
                {
                    isErrorFlag = true;
                    fieldText += "明细来源备注|";
                    msgText += "明细来源备注不能大于200|";
                }
            }
        }
        if(RealCount == 0)
        {
            isErrorFlag = true;
            fieldText += "采购计划明细来源|";
            msgText += "请输入采购计划明细来源|";
        }
    }
    else
    {
        isErrorFlag = true;
        fieldText += "采购计划明细来源|";
        msgText += "请输入采购计划明细来源|";
    }
    var signFrameDtl = findObj("Tb_Dtl",document);
    for(var i=1;i<signFrameDtl.rows.length;++i)
    {
        var DtlRemark = $("#DtlRemark"+i).val();
        if(strlen(DtlRemark)>200)
        {
            isErrorFlag = true;
            fieldText += "明细备注|";
            msgText += "明细备注不能大于200|";
        }
    }
    if(isErrorFlag == true)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isErrorFlag;
}

function fnChangeFromType(FromType)
{
    if(FromType == "0")
    {//无来源置从源单选择明细不可用
        $("#imgGetDtl").css("display","none");
        $("#imgUnGetDtl").css("display","inline");
        $("#imgExport").show();
        $("#imgUnExport").hide();
    }
    else
    {//否则可用
        $("#imgGetDtl").css("display","inline");
        $("#imgUnGetDtl").css("display","none");
        $("#imgExport").hide();
        $("#imgUnExport").show();
    }
    //改变源单类型的时候，明细来源和明细全部删除
    DropDtlSSignRow();
    DropDtlSignRow();
}


//明细来源表全部删除
function DropDtlSSignRow() {

    
    var signFrame = document.getElementById("Tb_DtlS");  
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=signFrame.rows.length-1;i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    DtlSFlag = true;//标识明细来源变化，用于判断是否需要进行明细汇总
}
//选择或不选择所有行
function SelectAllDtlS()
{
    var signFrame = document.getElementById("Tb_DtlS");
    var ck = document.getElementsByName("CheckAllDtlS");
    var ck1 = document.getElementsByName("DtlSchk");
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        if(ck[0].checked)
        {
            for(var j=0;j<ck1.length;j++)
            {  
               if(!(ck1[j].disabled||ck1[j].readonly))
               {
               ck1[j].checked=true;
               }
              
            }
        }
        else
        {
            for(var j=0;j<ck1.length;j++)
            {  
               ck1[j].checked=false;
            }
        }
    }
}

function GenerateNo()
{//生成序号
    var signFrame = findObj("Tb_DtlS",document);
    var Edge = signFrame.rows.length;
    var SortNo = 1;//起始行号
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<Edge;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                document.getElementById("DtlSSortNo"+i).innerHTML = SortNo++;
            }
        }
    }
}


function fnMergeDetail()
{
    if (!signForget) {
        return;
     }
  
   try{
DropDtlSignRow();
    $("#txtPlanCnt").val(0.00);
    $("#txtPlanMoney").val(0.00);
    var signFrameS = findObj("Tb_DtlS",document);
    var signFrame = findObj("Tb_Dtl",document);
    var rowIDS = signFrameS.rows.length;
    if((typeof(signFrameS) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<rowIDS;++i)
        {
            if(signFrameS.rows[i].style.display!="none")
            {
                if($("#DtlSUnitPrice"+i).val() == "")
                {
                    $("#DtlSUnitPrice"+i).val((parseFloat(0)).toFixed($("#HiddenPoint").val()));
                }
                else
                {
                
                    $("#DtlSUnitPrice"+i).val( (parseFloat($("#DtlSUnitPrice"+i).val())).toFixed($("#HiddenPoint").val()));
                } 
                
                      if($("#HiddenMoreUnit").val()=="True")
                      {
                       if($("#UsedPrice"+i).val() == "")
                {
                    $("#UsedPrice"+i).val((parseFloat(0)).toFixed($("#HiddenPoint").val()));
                }
                else
                {
                
                    $("#UsedPrice"+i).val( (parseFloat($("#UsedPrice"+i).val())).toFixed($("#HiddenPoint").val()));
                } 
                      }
                
                
                if($("#DtlSPlanCount"+i).val() == "")
                {
               
                    $("#DtlSPlanCount"+i).val( (parseFloat(0)).toFixed($("#HiddenPoint").val()));
                }
                else
                {  
                
                $("#DtlSPlanCount"+i).val((parseFloat($("#DtlSPlanCount"+i).val())).toFixed($("#HiddenPoint").val()));
                }
                
                
                 if($("#HiddenMoreUnit").val()=="True")
                 {
                 var sdsd=$("#UsedPrice"+i).val();
                 var sdfsadf=$("#UsedUnitCount"+i).val(); 
    var OutCount = $("#UsedUnitCount"+i).val();/*采购数量*/ 
                 if (sdfsadf=="")
                 {
                  $("#UsedUnitCount"+i).val(0);
                 }
              
                  if (OutCount != '')
    {
     var EXRate = $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString(); /*比率*/ 
        var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val());  
        $("#DtlSPlanCount"+i).val(tempcount);/*基本数量根据计划数量和比率算出*/  
    }
    
                $("#DtlSTotalPrice"+i).val(
                        (parseFloat(   $("#UsedPrice"+i).val())*parseFloat($("#UsedUnitCount"+i).val())).toFixed($("#HiddenPoint").val())
                );
              
                }
                else
                {
                   $("#DtlSTotalPrice"+i).val(
                        (parseFloat(   $("#DtlSUnitPrice"+i).val())*parseFloat($("#DtlSPlanCount"+i).val())).toFixed($("#HiddenPoint").val())
                );
                }

                var j=1;
                for(;j<signFrame.rows.length;++j)
                {
                
                var sdfd=document .getElementById ("Tb_DtlS").innerHTML;
                    if(($("#DtlSProductID"+i).val()==$("#DtlProductID"+j).val())
                    &&($("#DtlSPlanTakeDate"+i).val()==$("#DtlRequireDate"+j).val())
                    &&($("#DtlSProviderID"+i).val()==$("#DtlProviderID"+j).val()))
                    {//可以合并
                        $("#DtlProductCount"+j).val(    parseFloat(    $("#DtlProductCount"+j).val())+parseFloat($("#DtlSPlanCount"+i).val()  ).toFixed($("#HiddenPoint").val())   );
                        $("#DtlTotalPrice"+j).val(  ( parseFloat(    $("#DtlTotalPrice"+j).val())+parseFloat($("#DtlSTotalPrice"+i).val() )).toFixed($("#HiddenPoint").val())   );
                    if($("#HiddenMoreUnit").val()=="True")
               {
                    document.getElementById("Used2UnitCount"+rowID).value =
                     (parseFloat(document.getElementById("Used2UnitCount"+rowID).value.Trim())+parseFloat(document.getElementById("UsedUnitCount"+i).value.Trim())).toFixed($("#HiddenPoint").val());
               }
                        break;
                    }
                }
                if(j==signFrame.rows.length)
                {//不可以合并
                    var rowID = AddDtlSignRow();
                    $("#DtlProductID"+rowID).val($("#DtlSProductID"+i).val());
                    $("#DtlProductNo"+rowID).val($("#DtlSProductNo"+i).val());
                    $("#DtlProductName"+rowID).val($("#DtlSProductName"+i).val());
                    $("#DtlSpecification"+rowID).val($("#DtlSSpecification"+i).val());
                    $("#DtlUnitID"+rowID).val($("#DtlSUnitID"+i).val());
                    
                    $("#DtlUnitName"+rowID).val($("#DtlSUnitName"+i).val());
                    $("#DtlProviderName"+rowID).val($("#DtlSProviderName"+i).val());
                    $("#DtlProviderID"+rowID).val($("#DtlSProviderID"+i).val());
                    $("#DtlProductCount"+rowID).val($("#DtlSPlanCount"+i).val());
                    $("#DtlRequireDate"+rowID).val($("#DtlSPlanTakeDate"+i).val());
                    $("#DtlSSColor" + rowID).val($("#DtlSColor" + i).val());
                    $("#DtlUnitPrice"+rowID).val($("#DtlSUnitPrice"+i).val());
                    $("#DtlTotalPrice"+rowID).val($("#DtlSTotalPrice"+i).val());
                    $("#DtlOrderCount"+rowID).val(0.00);
                       if($("#HiddenMoreUnit").val()=="True")
                         {
                          var ExRate= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString();
                   var UsedUnitID= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[0].toString();
                   var UsedUnitName = document.getElementById ("SignItem_TD_UnitID_Select"+i).options[document.getElementById ("SignItem_TD_UnitID_Select"+i).selectedIndex].text;   
              var UsedUnitCount=     parseFloat ($("#UsedUnitCount"+i).val()).toFixed($("#HiddenPoint").val());
               if (isNaN (UsedUnitCount))
    {
    UsedUnitCount="0";
    }
                $("#TD2_Text_Used2UnitCount_"+rowID).val(UsedUnitName);
                  $("#Hidden_TD2_Text_Used2UnitCount_"+rowID).val(UsedUnitID);
                    $("#Hidden_TD2_Text_Exrate_ "+rowID).val(ExRate);
                      $("#Used2UnitCount "+rowID).val(UsedUnitCount);
                      $("#Used2Price"+rowID).val($("#UsedPrice"+i).val());
                      
                      $("#Used2UnitCount"+rowID).val($("#UsedUnitCount"+i).val());
                         }
                    
              
              
                } 
           
                    if($("#HiddenMoreUnit").val()=="True")
                    {
                    $("#txtPlanCnt").val(  (parseFloat($("#txtPlanCnt").val())+parseFloat($("#UsedUnitCount"+j).val())).toFixed($("#HiddenPoint").val())      );
                $("#txtPlanMoney").val(  (  parseFloat(    $("#txtPlanMoney").val())+parseFloat($("#DtlSTotalPrice"+j).val())).toFixed($("#HiddenPoint").val())    );
                    }
                    else
                    {
                    $("#txtPlanCnt").val(  (parseFloat($("#txtPlanCnt").val())+parseFloat($("#DtlSPlanCount"+j).val())).toFixed($("#HiddenPoint").val())      );
                $("#txtPlanMoney").val(  (  parseFloat(    $("#txtPlanMoney").val())+parseFloat($("#DtlSTotalPrice"+j).val())).toFixed($("#HiddenPoint").val())    );
                    }
                    
                
                
                
            }
        }
    }
    
    
    }
    catch (Error )
    {}
}

function Fun_FillParent_Content(id,no,productname,dddf,unitid,unit,df,sdfge,discount,standard,fg,fgf,taxprice,price,taxrate)
{    
    var RowID = popTechObj.InputObj;
    $("#DtlSProductID"+RowID).val(id);
    $("#DtlSProductNo"+RowID).val(no);
    $("#DtlSProductName"+RowID).val(productname);
//    $("#"+RowID).val(price);
    $("#DtlSUnitID"+RowID).val(unitid);
    $("#DtlSUnitName"+RowID).val(unit);
  
    $("#DtlSUnitPrice"+RowID).val(  (parseFloat(taxprice)).toFixed($("#HiddenPoint").val()));
//    $("#"+RowID).val(taxprice);
//    $("#"+RowID).val(discount);
    $("#DtlSSpecification"+RowID).val(standard);
    fnGetRcmPrv(id,RowID);
    fnMergeDetail();
}

//参数物品ID
function fnGetRcmPrv(ID,RowID)
{
    var URLParams = "ActionPlan=GetPrv"+"&ID="+ID;
    $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchasePlan_Add.ashx",
               
            dataType:'json',//返回json格式数据
            data:URLParams,
            cache:false,
            beforeSend:function()
            {
            }, 
            error: function()
            {
            }, 
            success:function(data) 
            {
                if(data.sta == 1) 
                {
                    var BackValue = data.data.split('|');
                    $("#DtlSProviderID"+RowID).val(BackValue[0]);
                    $("#DtlSProviderName"+RowID).val(BackValue[1]);
                } 
            }
        }); 
}
//判断采购需求是否是已经添加的
function ExistFromRequire(orderno,sortno)
{
    var signFrame = document.getElementById("Tb_DtlS");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var frombillNo = document.getElementById("DtlSFromBillNo"+i).value.Trim();
        var fromlineno = document.getElementById("DtlSProductNo"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(frombillNo==orderno)&&(fromlineno == sortno))
        {
            return true;
        } 
    }
    return false;
}

function ExistFromApply(orderno,sortno)
{
    var signFrame = document.getElementById("Tb_DtlS");  
    
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


function ssd()
{
 debugger ;
var sdsd=document .getElementById ("Tb_DtlS").innerHTML;
 
}
//添加明细来源行
function AddDtlSSignRow()
{


    var signFrame = findObj("Tb_DtlS",document);
    var rowID = signFrame.rows.length
    var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
    newTR.id = "DtlSSignItem" + rowID;
    var i=0;
    
    var newNameXH=newTR.insertCell(i++);//添加列:选择
    newNameXH.className="cell";
    newNameXH.innerHTML="<input name='DtlSchk' id='DtlSchk" + rowID + "' value="+rowID+" onclick=\"IfSelectAll('DtlSchk','CheckAllDtlS')\"  type='checkbox' size='20'  />";
    
    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="cell";
    newNameTD.id="DtlSSortNo" + rowID;           
    
    var newProductNo=newTR.insertCell(i++);//添加列:物品ID
    newProductNo.style.display = "none";
    
    newProductNo.innerHTML = "<input name='DtlSProductID" + rowID + "'  id='DtlSProductID" + rowID + "'  type='text' class=\"tdinput\" style='width:90%' />";
    
    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="cell";        
    newProductNo.innerHTML = "<input name='DtlSProductNo" + rowID + "'  id='DtlSProductNo" + rowID + "' readonly onclick=\"popTechObj.ShowList("+rowID+");\" type='text' class=\"tdinput\" style='width:90%' />";
    
    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="cell";
    newProductName.innerHTML = "<input name='DtlSProductName" + rowID + "' id='DtlSProductName" + rowID + "' type='text' class=\"tdinput\"  style='width:90%'  disabled/>";
    
    var newSpecification=newTR.insertCell(i++);//添加列:规格
    newSpecification.className="cell";
    newSpecification.innerHTML = "<input  name='DtlSSpecification'"+rowID+" id='DtlSSpecification" + rowID + "' type='text' class=\"tdinput\"  style='width:80%;' disabled/>";

    var newColor = newTR.insertCell(i++); //添加列:颜色
    newColor.className = "cell";
    newColor.innerHTML = "<input  name='DtlSColor'" + rowID + " id='DtlSColor" + rowID + "' type='text' class=\"tdinput\"  style='width:80%;' disabled/>";
    
    
    
    
    if($("#HiddenMoreUnit").val()=="False")
        { 
    var newUnitName=newTR.insertCell(i++);//添加列:单位ID
    newUnitName.style.display = "none";
    newUnitName.innerHTML = "<input  name='DtlSUnitID'"+rowID+" id='DtlSUnitID" + rowID + "' type='hidden' style='width:80%;'/>";
    
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input name='DtlSUnitName'"+rowID+" id='DtlSUnitName" + rowID + "'type='text' class=\"tdinput\"  style='width:80%'  disabled />";
    
    }
    else
    {
        document .getElementById ("spUnitID").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID").style.display="block";
     var newUnitName=newTR.insertCell(i++);//添加列:单位ID
    newUnitName.style.display = "none";
    newUnitName.innerHTML = "<input  name='DtlSUnitID'"+rowID+" id='DtlSUnitID" + rowID + "' type='hidden' style='width:80%;'/>";
    
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input name='DtlSUnitName'"+rowID+" id='DtlSUnitName" + rowID + "'type='text' class=\"tdinput\"  style='width:80%'  disabled />";
    
        var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
    }
    
    
       if($("#HiddenMoreUnit").val()=="False")
        {
    var newUnitPrice=newTR.insertCell(i++);//添加列:单价
    newUnitPrice.className="cell";
    newUnitPrice.innerHTML = "<input name='DtlSUnitPrice'"+rowID+" id='DtlSUnitPrice" + rowID + "'type='text' class=\"tdinput\"  onblur=\"fnMergeDetail();\" style='width:90%'   />";
    }
    else
    {
     document .getElementById ("spUnitPrice").style.display="none";
document .getElementById ("spUsedPrice").style.display="block";
  var newUnitPrice=newTR.insertCell(i++);//添加列:基本含税价
    newUnitPrice.style.display = "none";
    newUnitPrice.innerHTML = "<input name='DtlSUnitPrice'"+rowID+" id='DtlSUnitPrice" + rowID + "'type='text' class=\"tdinput\"  onblur=\"fnMergeDetail();\" style='width:90%'   />";
  
          var newUsedPrice=newTR.insertCell(i++);//添加列:实际含税价
    newUsedPrice.className="cell"; 
    newUsedPrice.innerHTML = "<input id='UsedPrice" + rowID + "'type='text' class=\"tdinput\" onblur=\"fnMergeDetail();\"   style='width:80%;'   />";
     var newUsedPricHid=newTR.insertCell(i++);//添加列:实际含税价
    newUsedPricHid.style.display = "none";
    newUsedPricHid.innerHTML = "<input id='UsedPricHid" + rowID + "'type='text' class=\"tdinput\"   style='width:80%;'   />";
    }
    
    var newRequireCount=newTR.insertCell(i++);//添加列:需求数量
    newRequireCount.style.display = "none";
    newRequireCount.innerHTML = "<input name='DtlSRequireCount" + rowID + "' id='DtlSRequireCount" + rowID + "'  type='text' class=\"tdinput\" style='width:90%' disabled  />";//添加列内容
    
    if($("#HiddenMoreUnit").val()=="False")
        { 
    var newPlanCount=newTR.insertCell(i++);//添加列:计划数量
    newPlanCount.className="cell";
    newPlanCount.innerHTML = "<input name='DtlSPlanCount" + rowID + "' id='DtlSPlanCount" + rowID + "'   onblur=\"Number_round(this," + $("#HiddenPoint").val() + "); fnMergeDetail();\" type='text' class=\"tdinput\" style='width:90%' />";//添加列内容
    }
    else
    {
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block";
       var newPlanCount=newTR.insertCell(i++);//添加列:基本数量
    newPlanCount.className="cell";
    newPlanCount.innerHTML = "<input name='DtlSPlanCount" + rowID + "' id='DtlSPlanCount" + rowID + "'    type='text' class=\"tdinput\" style='width:90%'  readonly='readonly'/>";//添加列内容
  
     var newUsedUnitCount=newTR.insertCell(i++);//添加列:实际计划数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='UsedUnitCount"+rowID+"' type='text' class=\"tdinput\"  value=''  style='width:90%;' onblur=\" Number_round(this," + $("#HiddenPoint").val() + "); fnMergeDetail();\"  />"; 
  
    }
    var newTotalPrice=newTR.insertCell(i++);//添加列:金额
    newTotalPrice.className="cell";
    newTotalPrice.innerHTML = "<input name='DtlSTotalPrice" + rowID + "' id='DtlSTotalPrice" + rowID + "'  type='text' class=\"tdinput\"  style='width:90%' disabled />";//添加列内容
               
    var newRequireDate=newTR.insertCell(i++);//添加列:需求日期
    newRequireDate.style.display = "none";
    newRequireDate.innerHTML = "<input name='DtlSRequireDate" + rowID + "'  id='DtlSRequireDate" + rowID + "' type='text' class=\"tdinput\"     style='width:90%' disabled  />";//添加列内容
//    $("#DtlSRequireDate" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#DtlSRequireDate" + rowID)})}); 
    
    var newPlanTakeDate=newTR.insertCell(i++);//添加列:计划交货日期
    newPlanTakeDate.className="cell";
    newPlanTakeDate.innerHTML = "<input name='DtlSPlanTakeDate" + rowID + "' id='DtlSPlanTakeDate" + rowID + "'   readonly=\"readonly\" onclick=\"WdatePicker();fnMergeDetail();\"   onPropertyChange=\"fnMergeDetail();\"  type='text' class=\"tdinput\"  style='width:90%'  />";//添加列内容
//    $("#DtlSPlanTakeDate" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#DtlSPlanTakeDate" + rowID)})});        

//    var newApplyReasonID=newTR.insertCell(i++);//添加列:申请原因ID
//    newApplyReasonID.style.display = "none";
//    newApplyReasonID.innerHTML = "<input name='DtlSApplyReasonID" + rowID + "' id='DtlSApplyReasonID" + rowID + "' type='text' class=\"tdinput\"  class='tdinput' />";//添加列内容

    var newApplyReason=newTR.insertCell(i++);//添加列:申请原因
    newApplyReason.style.display = "none";
//    newApplyReason.innerHTML = "<input name='DtlSApplyReason" + rowID + "' id='DtlSApplyReason" + rowID + "' onclick=\"popApplyReasonObj.ShowList('DtlSApplyReasonID"+rowID+"','DtlSApplyReasonName"+rowID+"','');\" type='text' class=\"tdinput\"  class='tdinput' />";//添加列内容
    newApplyReason.innerHTML = "<select class='tdinput' id='DtlSApplyReason" + rowID + "' disabled >" + document.getElementById("ddlApplyReasonHidden").innerHTML + "</select>";
    
    
    var newFromBillID=newTR.insertCell(i++);//添加列:来源单据ID
    newFromBillID.style.display = "none";
    newFromBillID.innerHTML = "<input name='DtlSFromBillID" + rowID + "' id='DtlSFromBillID" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled />";//添加列内容
    
    var newFromBillID=newTR.insertCell(i++);//添加列:来源单据
    newFromBillID.className="cell";
    newFromBillID.innerHTML = "<input name='DtlSFromBillNo" + rowID + "' id='DtlSFromBillNo" + rowID + "' type='text' class=\"tdinput\" style='width:90%' disabled />";//添加列内容
    
    var newFromLineNo=newTR.insertCell(i++);//添加列:来源单据行号
    newFromLineNo.className="cell";
    newFromLineNo.innerHTML = "<input name='DtlSFromLineNo" + rowID + "' id='DtlSFromLineNo" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled />";//添加列内容
     
    var newProviderID=newTR.insertCell(i++);//添加列:供应商ID
    newProviderID.style.display = "none";
    newProviderID.innerHTML = "<input name='DtlSProviderID" + rowID + "' id='DtlSProviderID" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' />";//添加列内容
    
    var newProviderName=newTR.insertCell(i++);//添加列:供应商
    newProviderName.className="cell";
    newProviderName.innerHTML = "<input name='DtlSProviderName" + rowID + "' id='DtlSProviderName" + rowID + "' onclick=\"popProviderObj.ShowProviderList('DtlSProviderID"+rowID+"','DtlSProviderName"+rowID+"','','Plan');\"   type='text' class=\"tdinput\"  style='width:90%' readonly />";//添加列内容
    
    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.style.display = "none";
    newRemark.innerHTML = "<input name='DtlSRemark" + rowID + "' id='DtlSRemark" + rowID + "' type='text' class=\"tdinput\" style='width:90%'   />";//添加列内容
    
    var newRqrID = newTR.insertCell(i++);
    newRqrID.style.display = "none";
    newRqrID.innerHTML = "<input name='RqrID"+rowID+"' id='RqrID"+rowID+"' type='text'/>";
    GenerateNo();
    
 
    return rowID;
}
//删除明细来源选定行，隐藏
function DelDtlSRow()
{
    var signFrame = findObj("Tb_DtlS",document);        
    var ck = document.getElementsByName("DtlSchk");
    for( var i = 0; i<ck.length;i++ )
    {
//        var rowID = i+1;
//            document.getElementById("DtlSSortNo"+rowID).innerHTML = GenerateNo(rowID);
        if ( ck[i].checked )
        {
           signFrame.rows[i+1].style.display="none";
           
        }
    }
    GenerateNo();
    fnMergeDetail();
}

//删除所有明细
function DropDtlSignRow()
{
    var signFrame = document.getElementById("Tb_Dtl");  
    if((typeof(signFrame) != "undefined")&&(signFrame!=null))
    {
        for( var i = signFrame.rows.length-1 ; i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    
}




//增加明细行
function AddDtlSignRow()
{
var signFrame = findObj("Tb_Dtl",document);

    var rowID = signFrame.rows.length;
    var signFrame = findObj("Tb_Dtl",document);
    var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
    newTR.id = "DtlSignItem" + rowID;
    var i=0;
  
    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="cell";
    newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
    
    var newProductNo=newTR.insertCell(i++);//添加列:物品ID
    newProductNo.style.display = "none";
    newProductNo.innerHTML = "<input name='DtlProductID" + rowID + "'  id='DtlProductID" + rowID + "'  type='text' class=\"tdinput\" style='width:90%' disabled/>";
    
    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="cell";        
    newProductNo.innerHTML = "<input name='DtlProductNo" + rowID + "' id='DtlProductNo" + rowID + "' disabled  type='text' class=\"tdinput\" style='width:90%' />";
    
    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="cell";
    newProductName.innerHTML = "<input name='DtlProductName" + rowID + "' id='DtlProductName" + rowID + "'disabled type='text' class=\"tdinput\"  style='width:90%'  />";
    
    var newSpecification=newTR.insertCell(i++);//添加列:规格
    newSpecification.className="cell";
    newSpecification.innerHTML = "<input  name='DtlSpecification'"+rowID+" id='DtlSpecification" + rowID + "'disabled type='text' class=\"tdinput\"  style='width:90%;'/>";


    var newSSColor = newTR.insertCell(i++); //添加列:颜色
    newSSColor.className = "cell";
    newSSColor.innerHTML = "<input  name='DtlSSColor'" + rowID + " id='DtlSSColor" + rowID + "'disabled type='text' class=\"tdinput\"  style='width:90%;'/>";
    
    
    
    
     if($("#HiddenMoreUnit").val()=="False")
     {
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位ID
    newUnitName.style.display = "none";
    newUnitName.innerHTML = "<input  name='DtlUnitID'"+rowID+" id='DtlUnitID" + rowID + "' type='hidden' style='width:90%;' disabled/>";
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input name='DtlUnitName'"+rowID+" id='DtlUnitName" + rowID + "'type='text' class=\"tdinput\" disabled style='width:90%'   />";
    
    }
    else
    {
    document .getElementById ("spUnitID2").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID2").style.display="block";
       var newUnitName=newTR.insertCell(i++);//添加列:单位ID
    newUnitName.style.display = "none";
    newUnitName.innerHTML = "<input  name='DtlUnitID'"+rowID+" id='DtlUnitID" + rowID + "' type='hidden' style='width:90%;' disabled/>";
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input name='DtlUnitName'"+rowID+" id='DtlUnitName" + rowID + "'type='text' class=\"tdinput\" disabled style='width:90%'   />";
    
      var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell"; 
            newFitNametd.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_Exrate_" + rowID + "'/>    <input type=\"hidden\"  id='Hidden_TD2_Text_Used2UnitCount_" + rowID + "' /><input type=\"text\"  id='TD2_Text_Used2UnitCount_" + rowID + "'  class=\"tdinput\"  size=\"6\" readonly disabled/>";//添加列内容
    }
    
    var newProviderName=newTR.insertCell(i++);//添加列:供应商
    newProviderName.className="cell";
    newProviderName.innerHTML = "<input name='DtlProviderName"+rowID+"' id='DtlProviderName" + rowID + "'type='text' class=\"tdinput\" disabled style='width:90%'   />";
    
    var newProviderID=newTR.insertCell(i++);//添加列:供应商
    newProviderID.style.display = "none";
    newProviderID.innerHTML = "<input name='DtlProviderID"+rowID+"' id='DtlProviderID" + rowID + "'type='text' class=\"tdinput\" disabled style='width:90%'   />";
    
        if($("#HiddenMoreUnit").val()=="False")
     {
    var newPlanCount=newTR.insertCell(i++);//添加列:计划数量
    newPlanCount.className="cell";
    newPlanCount.innerHTML = "<input name='DtlProductCount" + rowID + "' id='DtlProductCount" + rowID + "' disabled type='text' class=\"tdinput\" style='width:90%' />";
    }
    else
    {
       document .getElementById ("SpProductCount2").innerText="基本数量";
    document .getElementById ("spUsedUnitCount2").style.display="block";
        var newPlanCount=newTR.insertCell(i++);//添加列:基本数量
    newPlanCount.className="cell";
    newPlanCount.innerHTML = "<input name='DtlProductCount" + rowID + "' id='DtlProductCount" + rowID + "' disabled type='text' class=\"tdinput\" style='width:90%' />";
    
       var newUsedUnitCount=newTR.insertCell(i++);//添加列:实际计划数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='Used2UnitCount"+rowID+"' type='text' class=\"tdinput\"    style='width:90%;'  disabled  />"; 
    
    }
    var newPlanTakeDate=newTR.insertCell(i++);//添加列:计划交货日期
    newPlanTakeDate.className="cell";
    newPlanTakeDate.innerHTML = "<input name='DtlRequireDate" + rowID + "' id='DtlRequireDate" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled />";
    //$("#DtlRequireDate" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#DtlRequireDate" + rowID)})});        
    
             if($("#HiddenMoreUnit").val()=="False")
        {
    var newUnitPrice=newTR.insertCell(i++);//添加列:单价
    newUnitPrice.className="cell";
    newUnitPrice.innerHTML = "<input name='DtlUnitPrice" + rowID + "' id='DtlUnitPrice" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled />";//添加列内容
     
     }
     else
     {
            document .getElementById ("spUnitPrice2").style.display="none";
            document .getElementById ("spUsedPrice2").style.display="block";
              var newUnitPrice=newTR.insertCell(i++);//添加列:单价
      newUnitPrice.style.display = "none";
    newUnitPrice.innerHTML = "<input name='DtlUnitPrice" + rowID + "' id='DtlUnitPrice" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled />";//添加列内容
     
             var newUsedPrice=newTR.insertCell(i++);//添加列:实际单价
    newUsedPrice.className="cell"; 
    newUsedPrice.innerHTML = "<input id='Used2Price" + rowID + "'type='text' class=\"tdinput\"   style='width:80%;'   disabled/>";
     var newUsedPricHid=newTR.insertCell(i++);//添加列:实际单价
    newUsedPricHid.style.display = "none";
    newUsedPricHid.innerHTML = "<input id='Used2PricHid" + rowID + "'type='text' class=\"tdinput\"   style='width:80%;'   disabled/>";
            
     }
    var newTotalPrice=newTR.insertCell(i++);//添加列:金额
    newTotalPrice.className="cell";
    newTotalPrice.innerHTML = "<input name='DtlTotalPrice" + rowID + "' id='DtlTotalPrice" + rowID + "' type='text' class=\"tdinput\"  style='width:90%'  disabled/>";//添加列内容
     
    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.style.display = "none";
    newRemark.innerHTML = "<input name='DtlRemark" + rowID + "' id='DtlRemark" + rowID + "' type='text' class=\"tdinput\" style='width:90%'   />";//添加列内容
    
    var newOrderCount=newTR.insertCell(i++);//添加列:已订购数量
//    newOrderCount.style.display = "none";
    newOrderCount.className="cell";
    newOrderCount.innerHTML = "<input name='DtlOrderCount" + rowID + "' id='DtlOrderCount" + rowID + "'  type='text' class=\"tdinput\" style='width:90%' disabled  />";//添加列内容
    
    return rowID;
}

function Fun_FlowApply_Operate_Succeed(operateType)
{
    try{
        if(operateType == "0")
        {//提交审批成功后,不可改
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
        }
        else if(operateType == "1")
        {//审批成功后，不可改
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
        }
        else if(operateType == "2")
        {//审批不通过
            if(document.getElementById("txtBillStatusID").value.Trim() == "1")
            {//如果单据状态为制单，则可改
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
            }
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
            $("#btnGetGoods").css("display", "inline");//条码扫描按钮 
        }
    }catch(e){
    }
}

//根据单据状态决定页面按钮操作
function fnStatus(BillStatus,IsCite) {
    try{
        switch (BillStatus) { //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
            case '1': //制单
                fnFlowStatus($("#FlowStatus").val())
                break;
            case '2': //执行
                if(IsCite == "True")
                {//被引用不可编辑
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                }
                else
                {
                    $("#HiddenAction").val('Update');
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                }
                break;
            case '3': //变更
                $("#HiddenAction").val('Update');
                $("#FromType").attr("disabled", "disabled");
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                break;
            case '4': //手工结单
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                break;

            case '5':
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
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
                        $("#btnGetGoods").css("display", "none");//条码扫描按钮                  
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
                        $("#btnGetGoods").css("display", "none");//条码扫描按钮
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
                            $("#btnGetGoods").css("display", "none");//条码扫描按钮
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
                        $("#btnGetGoods").css("display", "inline");//条码扫描按钮
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
function GetGoodsDataByBarCode(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, IsBatchNo, BatchNo, ProductCount, CurrentStore, Source, ColorName)
{
 
    if(!IsExist(ID))//如果重复记录，就不增加
    {
       var rowID=AddDtlSSignRow();//插入行
       //填充数据
       //物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
       BarCode_FillParent_Content(ColorName,ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, rowID);
    }
    else
    {
      if($("#HiddenMoreUnit").val()=="False")
      {
    var count=document.getElementById("DtlSPlanCount"+rerowID).value;
    
        document.getElementById("DtlSPlanCount"+rerowID).value=  parseFloat (parseFloat(count)+1 ).toFixed($("#HiddenPoint").val());
        
      
        }
        else
        {
            var count1=document.getElementById("UsedUnitCount"+rerowID).value;
    
        document.getElementById("UsedUnitCount"+rerowID).value=  parseFloat (parseFloat(count1)+1 ).toFixed($("#HiddenPoint").val());
        }
        fnMergeDetail();//更改数量后重新计算
    }
    
}

var rerowID="";
//判断是否有相同记录有返回true，没有返回false
function IsExist(ID)
{
     var signFrame = findObj("Tb_DtlS", document);
     if((typeof(signFrame)=="undefined")|| signFrame==null)
     {
        return false;
     }
     for (i = 1; i < signFrame.rows.length; i++) {//判断商品是否在明细列表中
         if (signFrame.rows[i].style.display != "none") {
              var ProductID = $("#DtlSProductID" + i).val(); //商品ID（对应商品表ID）
              if(ProductID==ID)
               {
                   rerowID=i;
                   return true;
               }
            }
        }
     return false;
}
//物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
function BarCode_FillParent_Content(ColorName,id, no, productname, dddf, unitid, unit, df, sdfge, discount, standard, fg, fgf, taxprice, price, taxrate, RowID) {


    $("#DtlSColor" + RowID).val(ColorName);
    $("#DtlSProductID"+RowID).val(id);
    $("#DtlSProductNo"+RowID).val(no);
    $("#DtlSProductName"+RowID).val(productname);
//    $("#"+RowID).val(price);
    $("#DtlSUnitID"+RowID).val(unitid);
    $("#DtlSUnitName"+RowID).val(unit);
   
    $("#DtlSUnitPrice"+RowID).val(   (parseFloat(taxprice)).toFixed($("#HiddenPoint").val()));
//    $("#"+RowID).val(taxprice);
//    $("#"+RowID).val(discount);
    $("#DtlSSpecification"+RowID).val(standard);
    $("#DtlSPlanCount"+RowID).val(1);
    fnGetRcmPrv(id,RowID);
     if($("#HiddenMoreUnit").val()=="True")
                          {
        GetUnitGroupSelectEx(id,"InUnit","SignItem_TD_UnitID_Select" + RowID,"ChangeUnit(this.id,"+RowID+","+ (parseFloat(taxprice)).toFixed($("#HiddenPoint").val())+")","unitdiv" + RowID,'',"FillApplyContent("+RowID+","+ (parseFloat(taxprice)).toFixed($("#HiddenPoint").val())+",'1','1','','Goods')");//绑定单位组

                         }   else
                         {
                             fnMergeDetail();
                         }
                         

}
//---------------------------------------------------条码扫描END------------------------------------------------------------------------------------

function ShowSnapshot() {
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var ck = document.getElementsByName("DtlSchk");
    var signFrame = findObj("Tb_DtlS", document);
    for (var i = 0; i < ck.length; i++) {

        var rowID = i + 1;
        if (signFrame.rows[rowID].style.display != "none") {
            if (ck[i].checked) {
                detailRows++;
                intProductID = document.getElementById('DtlSProductID' + rowID).value;
                snapProductName = document.getElementById('DtlSProductName' + rowID).value;
                snapProductNo = document.getElementById('DtlSProductNo' + rowID).value;
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