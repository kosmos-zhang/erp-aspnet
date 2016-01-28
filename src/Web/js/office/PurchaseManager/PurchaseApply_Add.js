
var TableName = "officedba.PurchaseApply";
var AllSign=false ;
    var ActionApply = 'Add';
/*初始化加载*/
$(document).ready(function() {

if($("#HiddenMoreUnit").val()=="False")
        { 
}
else
{
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    
    document .getElementById ("spUsedUnitCount").style.display="block";
        document .getElementById ("spUnitID").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID").style.display="block";
    
    document .getElementById ("spUnitID2").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID2").style.display="block";
       document .getElementById ("SpProductCount2").innerText="基本数量";
 
    document .getElementById ("spUsedUnitCount2").style.display="block";
    
    }
 
  var requestObj = GetRequest(location.search);
      $("#HiddenURLParams").val(location.search.substr(1));
   var SourcePage = requestObj['SourcePage'];//如果不为空，表示从其他页面链接过来的
    if(SourcePage == "Info")
    {//不是从菜单栏进入的
        $("#btnBack").css("display","inline");
    } 
    if (intApplyID > 0) {

        LoadApplyInfos(intApplyID);

    }
    else {
         document.getElementById('imgGetDtl').style.display = 'none';
        document.getElementById('imgUnGetDtl').style.display = '';
        document.getElementById('imgExport').style.display = '';
        document.getElementById('imgUnExport').style.display = 'none';
        GetExtAttr(TableName, null);
        GetFlowButton_DisplayControl();
        AllSign=true ;
    }
});

function PrintBill()
{
    var ID = intApplyID;
    if(parseInt(ID) == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("../../../Pages/PrinttingModel/PurchaseManager/PurchaseApplyPrint.aspx?ID="+ID);
}

function DoBack()
{ 
 
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

function LoadApplyInfos(intApplyID) {
    var URLParams = "ID=" + intApplyID;
      ActionApply = "Fill";
    URLParams += "&ActionApply=" + ActionApply;
    $.ajax(
    {
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {

        },
        error: function() {

        },
        success: function(msg) {
            var rowsCount = 0;
            var rowsSecondCount = 0;
            $.each(msg.PurchaseApplyPrimary, function(i, item) {

                document.getElementById('codruleApply_txtCode').value = item.ApplyNo;
                document.getElementById('codruleApply_txtCode').className = 'tdinput';
                document.getElementById('codruleApply_txtCode').style.width = '90%';
                document.getElementById('codruleApply_txtCode').disabled = true;
                document.getElementById('txtIndentityID').value = item.ID;
                document.getElementById('txtTitle').value = item.Title;
                    if(item.TypeID != 0)
                document.getElementById('ddlTypeID_ddlCodeType').value = item.TypeID;
                document.getElementById('UserApplyUserName').value = item.ApplyUserName;
                document.getElementById('txtApplyUserID').value = item.ApplyUserID;
                document.getElementById('DeptName').value = item.DeptName;
                document.getElementById('txtDeptID').value = item.ApplyDeptID;
                document.getElementById('ddlFromType').value = item.FromType;
                document.getElementById('txtCreator').value = item.Creator;
                document.getElementById('txtCreatorReal').value = item.CreatorName;
                document.getElementById('txtCreateDate').value = item.CreateDate;
                document.getElementById('txtConfirmor').value = item.Confirmor;
                document.getElementById('txtConfirmorReal').value = item.ConfirmorName;
                document.getElementById('txtConfirmDate').value = item.ConfirmDate;
                document.getElementById('txtCloser').value = item.Closer;
                document.getElementById('txtCloserReal').value = item.CloserName;
                document.getElementById('txtCloseDate').value = item.CloseDate;
                document.getElementById('txtRemark').value = item.Remark;
                document.getElementById('txtBillStatus').value = item.BillStatus;
                document.getElementById('txtBillStatusReal').value = item.BillStatusName;
                document.getElementById('txtModifiedUserID').value = item.ModifiedUserID;
                document.getElementById('txtModifiedDate').value = item.ModifiedDate;
                document.getElementById('txtCountTotal').value = (parseFloat(item.CountTotal)).toFixed($("#HiddenPoint").val());
                 $("#IsCite").val(item.IsCite);
               
 
         
                Fun_ChangeSourceBill(item.FromType);
                GetExtAttr(TableName, msg.PurchaseApplyPrimary);
            });

            $.each(msg.PurchaseApplySource, function(i, itemSource) {
                rowsCount++;
                var rowID = FillSignRow(i, itemSource.ID, itemSource.SortNo, itemSource.ProductID, itemSource.ProductNo, itemSource.ProductName, itemSource.Specification, itemSource.UnitID, itemSource.UnitName, itemSource.PlanCount, itemSource.PlanTakeDate, itemSource.ApplyReason, itemSource.FromBillNo, itemSource.FromBillID, itemSource.FromLineNo, itemSource.UsedUnitID, itemSource.UsedUnitCount, itemSource.ExRate, itemSource.ColorName)
              if($("#HiddenMoreUnit").val()=="True")
                          {
                          var sign="";
                        if (itemSource.FromBillNo!="")
                        {
                        sign="Bill";
                        }
                        
        GetUnitGroupSelectEx(itemSource.ProductID,"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+")","unitdiv" + rowID,"","FillSelect('"+rowID +"','"+itemSource .UsedUnitID+"','"+sign+"')");//绑定单位组 
       
         
    
                         }    
            });

            $.each(msg.PurchaseApplyDetail, function(i, itemDetail) {
                rowsSecondCount++;
                FillSignRowSecond(i, itemDetail.ID, itemDetail.SortNo, itemDetail.ProductID, itemDetail.ProductNo, itemDetail.ProductName, itemDetail.Specification, itemDetail.UnitID, itemDetail.UnitName, itemDetail.ProductCount, itemDetail.RequireDate, itemDetail.PlanedCount, itemDetail.UsedUnitID, itemDetail.UsedUnitCount, itemDetail.ExRate, itemDetail.UsedUnitName, itemDetail.ColorName)
            });

            document.getElementById('txtTRLastIndex').value = rowsCount + 1;
            document.getElementById('txtTRLastIndexSecond').value = rowsSecondCount + 1;

            GetFlowButton_DisplayControl(); //流程审批按钮显示
            document.getElementById('codruleApply_ddlCodeRule').style.display = 'none';
        },
        complete: function() { AllSign=true ;}
    });
}

 function FillSelect(rowID,UsedUnitID,sign)
 {
 if (sign=="Bill")
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
function sadfdsf()
{
debugger ;
var sdsd=document .getElementById ("divDetailTbl").innerHTML;


}

/*保存按钮控制显示*/
function SetSaveButton_DisplayControl(flowStatus) {
    //流程状态：0：待提交   1：待审批   2：审批中   3：审批通过     4：审批不通过   5：撤销审批
    //制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    //制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    //变更和手工结单的不可以修改
    var PageBillID = document.getElementById('txtIndentityID').value;
    var PageBillStatus = document.getElementById('txtBillStatus').value;

    if (PageBillID > 0) {
        try {
            if (PageBillStatus == '2' || PageBillStatus == '3' || PageBillStatus == '4') {
                //单据状态：变更和手工结单状态
                document.getElementById('imgUnSave').style.display = 'block';
                document.getElementById('imgSave').style.display = 'none';
            }
            else {
                if (PageBillStatus == 1 && (flowStatus == 1 || flowStatus == 2 || flowStatus == 3)) {
                    //单据状态+审批状态：制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
                    document.getElementById('imgUnSave').style.display = 'block';
                    document.getElementById('imgSave').style.display = 'none';
                }
                else {
                    document.getElementById('imgUnSave').style.display = 'none';
                    document.getElementById('imgSave').style.display = 'block';
                }
            }
        } catch (e) { }
    }
}



/*添加明细*/
function AddSignRow(productID, productNo, productName, specification, unitID, codeName, ColorName) {
    var txtTRLastIndex = findObj("txtTRLastIndex", document);
    var rowID = parseInt(txtTRLastIndex.value);
//    alert(rowID);
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Item_Row_" + rowID;
var i=0;
    var colChoose = newTR.insertCell(i++); //添加列:选择
    colChoose.align = "center";
    colChoose.className = "cell";
    colChoose.innerHTML = "<input name='chk' id='chk_Option_" + rowID + "' value=\"0\" type='checkbox' onclick=\"ClearAllOption();\" />";

    var colNum = newTR.insertCell(i++); //添加列:序号
    colNum.align = "center";
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\" id=\"TD_Text_SortNo_" + rowID + "\" value=\"" + GenerateSortNo(rowID) + "\"  class=\"tdinput\" size=\"5\" disabled />";

    var colProductNo = newTR.insertCell(i++); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<input type=\"text\" id=\"TD_Text_ProdNo_" + rowID + "\" value=\"" + productNo + "\" class=\"tdinput\" size=\"8\" onclick=\"popTechObj.ShowList('" + rowID + "');\" readonly />";

    var colProductID = newTR.insertCell(i++); //添加列:物品
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"hidden\" id=\"Hidden_TD_Text_ProductID_" + rowID + "\" value=\"" + productID + "\"  /><input type=\"text\" id=\"TD_Text_ProductID_" + rowID + "\" value=\"" + productName + "\" class=\"tdinput\" size=\"10\" onclick=\"popTechObj.ShowList('" + rowID + "');\" readonly />";

    var colProductID = newTR.insertCell(i++); //添加列:规格
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"text\" id=\"TD_Text_Specification_" + rowID + "\" value=\"" + specification + "\" class=\"tdinput\" size=\"8\" readonly />";

    var colDtlSColor = newTR.insertCell(i++); //添加列:颜色
    colDtlSColor.className = "cell";
    colDtlSColor.innerHTML = "<input type=\"text\" id=\"DtlSColor" + rowID + "\" value=\"" + ColorName + "\" class=\"tdinput\" size=\"8\" readonly />";



 if($("#HiddenMoreUnit").val()=="False")
        { 
    var colUnitID = newTR.insertCell(i++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly/>";
}
else
{
    document .getElementById ("spUnitID").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID").style.display="block";
       var colUnitID = newTR.insertCell(i++); //添加列:基本单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly/>";
 
    
      var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
    
}

 if($("#HiddenMoreUnit").val()=="True")
                          {
        GetUnitGroupSelectEx(productID,"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+")","unitdiv" + rowID,'',"FillContent("+rowID+")");//绑定单位组
        
                         }   

if($("#HiddenMoreUnit").val()=="False")
        { 
    var colProductCount = newTR.insertCell(i++); //添加列:需求数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD_Text_ProductCount_" + rowID + "' value=\"\" class=\"tdinput\" size=\"10\"  onblur=\"fnCalculateTotal();\" />";
}
else
{
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block";
      var colProductCount = newTR.insertCell(i++); //添加列:基本数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD_Text_ProductCount_" + rowID + "' value=\"\" class=\"tdinput\" size=\"10\"   readonly='readonly' />";
    
   
    
        var newUsedUnitCount=newTR.insertCell(i++);//添加列:采购数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='UsedUnitCount"+rowID+"' type='text' class=\"tdinput\"  value=''  style='width:90%;' onblur=\"Number_round(this," + $("#HiddenPoint").val() + "); ChangeUnit(this.id,"+rowID+")\"  />"; 
}
    var colStartDate = newTR.insertCell(i++); //添加列:需求日期
    colStartDate.className = "cell";
    colStartDate.innerHTML = "<input type=\"text\" id='TD_Text_StartDate_" + rowID + "' value=\"\"  class=\"tdinput\"  onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('TD_Text_StartDate_" + rowID + "')})\" size=\"10\" readonly=\"readonly\"    onpropertychange =\"fnCalculateTotal();\"/>";

document.getElementById("ddlApplyReason").value="";
    var colReason = newTR.insertCell(i++); //添加列:申请原因
    colReason.className = "cell";
    colReason.innerHTML = "<select class='tdinput' id='TD_Text_Reason_" + rowID + "'>" + document.getElementById("ddlApplyReason").innerHTML + "</select>";
    
//    "<input type=\"text\" id='TD_Text_Reason_" + rowID + "' value=\"0\"  class=\"tdinput\"  size=\"10\" />";

    var colFromBillID = newTR.insertCell(i++); //添加列:源单编号
    colFromBillID.className = "cell";
    colFromBillID.innerHTML = "<input type=\"text\" id='TD_Text_FromBillNo_" + rowID + "' value=\"\" class=\"tdinput\" size=\"10\" readonly disabled /><input type=\"hidden\" id='TD_Text_FromBillID_" + rowID + "' value=\"0\" class=\"tdinput\" size=\"10\" />";

    var colFromLineNo = newTR.insertCell(i++); //添加列:源单行号
    colFromLineNo.className = "cell";
    colFromLineNo.innerHTML = "<input type=\"text\" id='TD_Text_FromLineNo_" + rowID + "' value=\"\" class=\"tdinput\"  size=\"5\" readonly disabled />";

    txtTRLastIndex.value = (rowID + 1).toString(); //将行号推进下一行
    
    return rowID;
}

function FillContent(rowID)
{ 
 
 var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
              var planCount = $("#UsedUnitCount"+rowID).val();/*采购数量*/

           if (planCount !='')
           {
           var tempcount=parseFloat(planCount*EXRate).toFixed($("#HiddenPoint").val());
          $("#TD_Text_ProductCount_"+rowID).attr("value", (parseFloat(tempcount)).toFixed($("#HiddenPoint").val()));
           }
           else
           {
             $("#UsedUnitCount"+rowID).attr("value", (parseFloat( "0")).toFixed($("#HiddenPoint").val()));
           }
    
          fnCalculateTotal();
}

/*添加明细*/
function AddSignRowSecond(productID, productNo, productName, specification, unitID, codeName,ss,ColorName) {
    var txtTRLastIndex2 = findObj("txtTRLastIndexSecond", document);
    var rowID = parseInt(txtTRLastIndex2.value);
    var signFrame = findObj("dg_LogSecond", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Item2_Row_" + rowID;
var i=0;

    var colNum = newTR.insertCell(i++); //添加列:序号
    colNum.align = "center";
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\" id=\"TD2_Text_SortNo_" + rowID + "\" value=" + rowID + "  class=\"tdinput\" size=\"5\" disabled />";

    var colProductNo = newTR.insertCell(i++); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<input type=\"text\" id=\"TD2_Text_ProdNo_" + rowID + "\" value=\"" + productNo + "\" class=\"tdinput\" size=\"8\" onclick=\"popTechObj.ShowList('" + rowID + "');\" readonly disabled />";

    var colProductID = newTR.insertCell(i++); //添加列:物品
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"hidden\" id=\"Hidden_TD2_Text_ProductID_" + rowID + "\" value=\"" + productID + "\"  /><input type=\"text\" id=\"TD2_Text_ProductID_" + rowID + "\" value=\"" + productName + "\" class=\"tdinput\" size=\"10\"   readonly disabled />";

    var colProductID = newTR.insertCell(i++); //添加列:规格
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"text\" id=\"TD2_Text_Specification_" + rowID + "\" value=\"" + specification + "\" class=\"tdinput\" size=\"8\" readonly disabled/>";




    var colDtlSSColor = newTR.insertCell(i++); //添加列:颜色
    colDtlSSColor.className = "cell";
    colDtlSSColor.innerHTML = "<input type=\"text\" id=\"DtlSSColor" + rowID + "\" value=\"" + ColorName + "\" class=\"tdinput\" size=\"8\" readonly disabled/>";
    

 if($("#HiddenMoreUnit").val()=="False")
        { 
    var colUnitID = newTR.insertCell(i++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD2_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";
}
else
{
  document .getElementById ("spUnitID2").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID2").style.display="block";
        var colUnitID = newTR.insertCell(i++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD2_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";

  var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell"; 
            newFitNametd.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_Exrate_" + rowID + "' /><input type=\"hidden\"  id='Hidden_TD2_Text_Used2UnitCount_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD2_Text_Used2UnitCount_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";//添加列内容
}

  if($("#HiddenMoreUnit").val()=="False")
        { 

    var colProductCount = newTR.insertCell(i++); //添加列:申请数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD2_Text_ProductCount_" + rowID + "' value=\"\" class=\"tdinput\" size=\"10\"  disabled/>";

}
else
{
   document .getElementById ("SpProductCount2").innerText="基本数量";
 
    document .getElementById ("spUsedUnitCount2").style.display="block";
   var colProductCount = newTR.insertCell(i++); //添加列:基本数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD2_Text_ProductCount_" + rowID + "' value=\"\" class=\"tdinput\" size=\"10\"  disabled/>";
      var newUsedUnitCount=newTR.insertCell(i++);//添加列:需求数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='Used2UnitCount"+rowID+"' type='text' class=\"tdinput\"  value=''  style='width:90%;'  disabled  />"; 

}
    var colStartDate = newTR.insertCell(i++); //添加列:需求日期
    colStartDate.className = "cell";
    colStartDate.innerHTML = "<input type=\"text\" id='TD2_Text_StartDate_" + rowID + "' value=\"\"  class=\"tdinput\"    size=\"10\"  disabled/>";

    var colPlanedCount = newTR.insertCell(i++); //添加列:已计划数量
    colPlanedCount.className = "cell";
    colPlanedCount.innerHTML = "<input type=\"text\" id='TD2_Text_PlanedCount_" + rowID + "' value=\"\" class=\"tdinput\" size=\"10\" readonly disabled/>";


    txtTRLastIndex2.value = (rowID + 1).toString(); //将行号推进下一行
    
    return rowID;
}

//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,RowID/*行号*/)
 {

    var EXRate = $("#SignItem_TD_UnitID_Select"+RowID).val().split('|')[1].toString(); /*比率*/
    var OutCount = $("#UsedUnitCount"+RowID).val();/*采购数量*/
   
    if (OutCount != '')
    {
        var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val()); 
        $("#TD_Text_ProductCount_"+RowID).val(tempcount);/*基本数量根据计划数量和比率算出*/
    }
    fnCalculateTotal();
 }

 function FillSignRow(i, detailID, sortNo, productID, productNo, productName, specification, unitID, codeName, productCount, requireDate, reason, fromBillNo, fromBillID, fromLineNo, UsedUnitID, UsedUnitCount, ExRate, ColorName) {
 
    var rowID = parseInt(i) + 1;
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Item_Row_" + rowID;
var iw=0;
    var colChoose = newTR.insertCell(iw++); //添加列:选择
    colChoose.className = "cell";
    colChoose.align = "center";
    colChoose.innerHTML = "<input name='chk' id='chk_Option_" + rowID + "' value=" + detailID + " type='checkbox' onclick=\"ClearAllOption();\" />";

    var colNum = newTR.insertCell(iw++); //添加列:序号
    colNum.align = "center";
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\" id=\"TD_Text_SortNo_" + rowID + "\" value=\"" + rowID + "\"  class=\"tdinput\" size=\"5\" disabled />";

    var colProductNo = newTR.insertCell(iw++); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<input type=\"text\" id=\"TD_Text_ProdNo_" + rowID + "\" value=\"" + productNo + "\" class=\"tdinput\" size=\"8\" readonly />";

    var colProductID = newTR.insertCell(iw++); //添加列:物品
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"hidden\" id=\"Hidden_TD_Text_ProductID_" + rowID + "\" value=\"" + productID + "\"  /><input type=\"text\" id=\"TD_Text_ProductID_" + rowID + "\" value=\"" + productName + "\" class=\"tdinput\" size=\"10\" onclick=\"popTechObj.ShowList('" + rowID + "');\" readonly />";

    var colProductID = newTR.insertCell(iw++); //添加列:规格
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"text\" id=\"TD_Text_Specification_" + rowID + "\" value=\"" + specification + "\" class=\"tdinput\" size=\"8\" readonly />";

    var colDtlSColor = newTR.insertCell(iw++); //添加列:颜色
    colDtlSColor.className = "cell";
    colDtlSColor.innerHTML = "<input type=\"text\" id=\"DtlSColor" + rowID + "\" value=\"" + ColorName + "\" class=\"tdinput\" size=\"8\" readonly />";
 
   
   
 if($("#HiddenMoreUnit").val()=="False")
        { 
  var colUnitID = newTR.insertCell(iw++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly/>";
}
else
{
  document .getElementById ("spUnitID").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID").style.display="block";
      var colUnitID = newTR.insertCell(iw++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly/>";

    
      var newFitNametd=newTR.insertCell(iw++);//添加列:实际单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
}


var pro=(parseFloat(productCount)).toFixed($("#HiddenPoint").val());

if($("#HiddenMoreUnit").val()=="False")
        { 
   var colProductCount = newTR.insertCell(iw++); //添加列:需求数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD_Text_ProductCount_" + rowID + "' value=\"" +pro  + "\" class=\"tdinput\" size=\"10\" onblur =\" fnCalculateTotal();\"  />";

}
else
{
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block";
    var colProductCount = newTR.insertCell(iw++); //添加列:基本数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD_Text_ProductCount_" + rowID + "' value=\"" +pro  + "\" class=\"tdinput\" size=\"10\" onblur =\" fnCalculateTotal();\"  />";

 if (UsedUnitCount=="")
 {
 UsedUnitCount=0;
 }
      var sd=  (parseFloat(UsedUnitCount)).toFixed($("#HiddenPoint").val());
        var newUsedUnitCount=newTR.insertCell(iw++);//添加列:采购数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='UsedUnitCount"+rowID+"' type='text' class=\"tdinput\"  value=\"" + sd + "\"  style='width:90%;' onblur=\" Number_round(this," + $("#HiddenPoint").val() + ");ChangeUnit(this.id,"+rowID+")\"  />"; 
}
    var colStartDate = newTR.insertCell(iw++); //添加列:需求日期
    colStartDate.className = "cell";
    colStartDate.innerHTML = "<input type=\"text\" id='TD_Text_StartDate_" + rowID + "' value=\"" + requireDate + "\"  class=\"tdinput\" onpropertychange =\"fnCalculateTotal();\"  onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('TD_Text_StartDate_" + rowID + "')})\" size=\"10\" />";


 document.getElementById("ddlApplyReason").value=reason;
    var colReason = newTR.insertCell(iw++); //添加列:申请原因
    colReason.className = "cell";
    colReason.innerHTML = "<select class='tdinput' id='TD_Text_Reason_" + rowID + "'>" + document.getElementById("ddlApplyReason").innerHTML + "</select>";

//  "<input type=\"text\" id='TD_Text_Reason_" + rowID + "' value=\"" + reason + "\"  class=\"tdinput\"   size=\"10\" />"
    var colFromBillID = newTR.insertCell(iw++); //添加列:源单编号
    colFromBillID.className = "cell";
    colFromBillID.innerHTML = "<input type=\"text\" id='TD_Text_FromBillNo_" + rowID + "' value=\"" + fromBillNo + "\" class=\"tdinput\" size=\"10\" readonly disabled /><input type=\"hidden\" id='TD_Text_FromBillID_" + rowID + "' value=\"" + fromBillID + "\" class=\"tdinput\" size=\"10\" />";

if (fromLineNo==0)
{
fromLineNo='';
}
    var colFromLineNo = newTR.insertCell(iw++); //添加列:源单行号
    colFromLineNo.className = "cell";
    colFromLineNo.innerHTML = "<input type=\"text\" id='TD_Text_FromLineNo_" + rowID + "' value=\"" + fromLineNo + "\" class=\"tdinput\"  size=\"5\" readonly disabled />";
    return rowID ;
}

function FillSignRowSecond(i, detailID, sortNo, productID, productNo, productName, specification, unitID, codeName, productCount, requireDate, planedCount, UsedUnitID, UsedUnitCount, ExRate, UsedUnitName, ColorName) {
 
    var rowID = parseInt(i) + 1;
    var signFrame = findObj("dg_LogSecond", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Item2_Row_" + rowID;
var iw=0;

    var colNum = newTR.insertCell(iw++); //添加列:序号
    colNum.align = "center";
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\" id=\"TD2_Text_SortNo_" + rowID + "\" value=\"" + rowID + "\"  class=\"tdinput\" size=\"5\" disabled />";

    var colProductNo = newTR.insertCell(iw++); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<input type=\"text\" id=\"TD2_Text_ProdNo_" + rowID + "\" value=\"" + productNo + "\" class=\"tdinput\" size=\"8\" readonly  disabled/>";

    var colProductID = newTR.insertCell(iw++); //添加列:物品
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"hidden\" id=\"Hidden_TD2_Text_ProductID_" + rowID + "\" value=\"" + productID + "\"  /><input type=\"text\" id=\"TD2_Text_ProductID_" + rowID + "\" value=\"" + productName + "\" class=\"tdinput\" size=\"10\"   readonly  disabled/>";

    var colProductID = newTR.insertCell(iw++); //添加列:规格
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"text\" id=\"TD2_Text_Specification_" + rowID + "\" value=\"" + specification + "\" class=\"tdinput\" size=\"8\" readonly  disabled/>";



    var colDtlSSColor = newTR.insertCell(iw++); //添加列:颜色
    colDtlSSColor.className = "cell";
    colDtlSSColor.innerHTML = "<input type=\"text\" id=\"DtlSSColor" + rowID + "\" value=\"" + ColorName + "\" class=\"tdinput\" size=\"8\" readonly  disabled/>";
    
    
 if($("#HiddenMoreUnit").val()=="False")
        { 
  var colUnitID = newTR.insertCell(iw++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD2_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";

}
else
{
document .getElementById ("spUnitID2").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID2").style.display="block";
     var colUnitID = newTR.insertCell(iw++); //添加列:基本单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD2_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";

  var newFitNametd=newTR.insertCell(iw++);//添加列:实际单位
            newFitNametd.className="cell"; 
            newFitNametd.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_Exrate_" + rowID + "' value=\"" + ExRate + "\" />    <input type=\"hidden\"  id='Hidden_TD2_Text_Used2UnitCount_" + rowID + "' value=\"" + UsedUnitID + "\" /><input type=\"text\"  id='TD2_Text_Used2UnitCount_" + rowID + "' value=\"" + UsedUnitName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";//添加列内容
}


var pro=(parseFloat(productCount)).toFixed($("#HiddenPoint").val());

 if($("#HiddenMoreUnit").val()=="False")
        { 

    var colProductCount = newTR.insertCell(iw++); //添加列:申请数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD2_Text_ProductCount_" + rowID + "' value=\"" +pro + "\" class=\"tdinput\" size=\"10\" readonly disabled/>";
}
else
{

   document .getElementById ("SpProductCount2").innerText="基本数量";
 
    document .getElementById ("spUsedUnitCount2").style.display="block";
      var colProductCount = newTR.insertCell(iw++); //添加列:申请数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD2_Text_ProductCount_" + rowID + "' value=\"" +pro + "\" class=\"tdinput\" size=\"10\" readonly disabled/>";

    if (isNaN (UsedUnitCount))
    {
    UsedUnitCount='';
    }
      var newUsedUnitCount=newTR.insertCell(iw++);//添加列:需求数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='Used2UnitCount"+rowID+"' type='text' class=\"tdinput\"  value=\"" + UsedUnitCount + "\"  style='width:90%;'  disabled  />"; 

}


    var colStartDate = newTR.insertCell(iw++); //添加列:需求日期
    colStartDate.className = "cell";
    colStartDate.innerHTML = "<input type=\"text\" id='TD2_Text_StartDate_" + rowID + "' value=\"" + requireDate + "\"  class=\"tdinput\"   size=\"10\"  disabled/>";
var plan=(parseFloat(planedCount)).toFixed($("#HiddenPoint").val());
    var colPlanedCount = newTR.insertCell(iw++); //添加列:已计划数量
    colPlanedCount.className = "cell";
    colPlanedCount.innerHTML = "<input type=\"text\" id='TD2_Text_PlanedCount_" + rowID + "' value=\"" + plan + "\" class=\"tdinput\" size=\"10\" readonly disabled />";

return rowID ;
}


function DropSignRowSecond()
{ 
    var signFrame = document.getElementById("dg_LogSecond");  
    if((typeof(signFrame) != "undefined")&&(signFrame!=null))
    {
        for( var i = signFrame.rows.length-1 ; i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    document .getElementById ("txtTRLastIndexSecond").value=1;
}

function DropSignRow()
{ 
    var signFrame = document.getElementById("dg_Log");  
    if((typeof(signFrame) != "undefined")&&(signFrame!=null))
    {
        for( var i = signFrame.rows.length-1 ; i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    document .getElementById ("txtTRLastIndex").value=1;
}
/*自动带出采购申请明细*/
function fnCalculateTotal() {
 
 


 fnMergeDetail();

    var signFrame = findObj("dg_Log",document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        var Total = 0;
        for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
             
                
                if($("#HiddenMoreUnit").val()=="True")
                {
                   if($("#UsedUnitCount"+i).val() == "")
                {
                    $("#UsedUnitCount"+i).val(0)
                }

                Total+=parseFloat(document.getElementById("UsedUnitCount"+i).value.Trim());
                }else
                {
                   if($("#TD_Text_ProductCount_"+i).val() == "")
                {
                    $("#TD_Text_ProductCount_"+i).val(0)
                }else
                {
              document.getElementById("TD_Text_ProductCount_"+i).value= parseFloat(document.getElementById("TD_Text_ProductCount_"+i).value).toFixed($("#HiddenPoint").val());  
                }
                Total+=parseFloat(document.getElementById("TD_Text_ProductCount_"+i).value.Trim());
                }
            }
        }
    }
    document.getElementById("txtCountTotal").value = (parseFloat(Total)).toFixed($("#HiddenPoint").val()); 
}
//保存时重新计算
function CountBaseNum()
{

    var List_TB=findObj("dg_Log",document);
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
                    $("#TD_Text_ProductCount_"+i).val(tempcount);/*基本数量根据出库数量和比率算出*/ 
                }
            }
        }
    }
    
    
    
}
function Fun_Save_Apply() {

CountBaseNum();
    if (fnCheckInfo())
        return;

    var URLParams = fnGetBaseInfo();
    URLParams += fnGetDtlSInfo();
    URLParams += fnGetDtlInfo() + GetExtAttrValue();

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseApply.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
    
             if(data.sta == 1) 
            { 
               if(ActionApply == "Add")
                {
                    //设置编辑模式
                   ActionApply = "Update";
//                    /* 设置编号的显示 */ 
//                    //显示采购计划的编号 采购计划编号DIV可见              
//                    document.getElementById("divPurApplyNo").style.display = "block";
//                    //设置采购计划编号
//                    
//                    document.getElementById("divPurApplyNo").innerHTML = data.data.split('#')[0];
//                    $("#")
                   intApplyID= data.data.split('#')[1];
                    
                     $("#txtCreator").val(data.data.split('#')[4]);
                      $("#txtCreatorReal").val(data.data.split('#')[5]);
                       $("#txtCreateDate").val(data.data.split('#')[2]);
                    $("#txtModifiedUserName").val(data.data.split('#')[3]);
                    $("#txtModifiedUserID").val(data.data.split('#')[3])
                    $("#txtModifiedDate").val(data.data.split('#')[2]);
                   //编码规则DIV不可见
//                    document.getElementById("divCodeRule").style.display = "none";
                    
                    document .getElementById ("txtIndentityID").value=intApplyID ;
                    document.getElementById("codruleApply_txtCode").value = data.data.split('#')[0];
                    document.getElementById('codruleApply_txtCode').className='tdinput';
                    document.getElementById('codruleApply_txtCode').style.width='90%';
                    document.getElementById('codruleApply_ddlCodeRule').style.display='none';
                    
                   
                     GetFlowButton_DisplayControl();
                   
//                    //设置源单类型不可改
//                    $("#ddlFromType").attr("disabled","disabled");
                } 
                else
                {  
                if($("#txtBillStatus").val() == "2")
                    {
                        $("#txtBillStatus").val("3");
                        $("#txtBillStatusReal").val("变更");
                    }  
                    $("#txtModifiedUserName").val(data.data.split('#')[3]);
                    $("#txtModifiedUserID").val(data.data.split('#')[3])
                    $("#txtModifiedDate").val(data.data.split('#')[2]);                  
                }
      
                GetFlowButton_DisplayControl();
                //最后更新人
                
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
         else    if (data.sta==0)
            {
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.info);
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        }
    });
}

function fnGetBaseInfo() {
    var applyID = document.getElementById('txtIndentityID').value;
 
    if (parseInt(applyID) > 0) {
        ActionApply = 'Update';
    }

    var strParams = "ActionApply=" + ActionApply; //编辑标识
    if (ActionApply == "Add") {//新增时
        codeRule = escape(document.getElementById("codruleApply_ddlCodeRule").value.Trim());
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "") {
            //获取输入的编号
            strParams += "&PurApplyNo=" + escape(document.getElementById("codruleApply_txtCode").value.Trim());
        }
        //编码规则ID
        strParams += "&CodeRuleID=" + escape(document.getElementById("codruleApply_ddlCodeRule").value.Trim());
    }
    else if (ActionApply == "Update") {
        strParams += "&PurApplyNo=" + escape(document.getElementById("codruleApply_txtCode").value.Trim());
        strParams += "&ID=" + escape(applyID);
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
    strParams += "&CreatorID=" + escape(document.getElementById("txtCreator").value.Trim());
    strParams += "&CreateDate=" + escape(document.getElementById("txtCreateDate").value.Trim());
    strParams += "&BillStatusID=" + escape(document.getElementById("txtBillStatus").value.Trim());
    strParams += "&ConfirmorID=" + escape(document.getElementById("txtConfirmor").value.Trim());
    strParams += "&ConfirmorDate=" + escape(document.getElementById("txtConfirmDate").value.Trim());

    strParams += "&ModifiedUserID=" + escape(document.getElementById("txtModifiedUserID").value.Trim());
    strParams += "&ModifiedDate=" + escape(document.getElementById("txtModifiedDate").value.Trim());
    strParams += "&CloserID=" + escape(document.getElementById("txtCloser").value.Trim());
    strParams += "&CloseDate=" + escape(document.getElementById("txtCloseDate").value.Trim());
    strParams += "&Remark=" + escape(document.getElementById("txtRemark").value.Trim());

    return strParams;
}


function fnGetDtlSInfo() {
    var strParams = "";
    var signFrame = findObj("dg_Log", document);
    var DetailLength = 0; //来源长度
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (i = 1; i < signFrame.rows.length; i++) {
            if (signFrame.rows[i].style.display != "none") {
//                var tmpRemark = '';
                strParams += "&DtlSSortNo" + DetailLength + "=" + escape(document.getElementById("TD_Text_SortNo_" + i).innerHTML);
                strParams += "&DtlSProdID" + DetailLength + "=" + escape(document.getElementById("Hidden_TD_Text_ProductID_" + i).value.Trim());
                strParams += "&DtlSProdNo" + DetailLength + "=" + escape(document.getElementById("TD_Text_ProdNo_" + i).value.Trim());
                strParams += "&DtlSProdName" + DetailLength + "=" + escape(document.getElementById("TD_Text_ProductID_" + i).value.Trim());
                strParams += "&DtlSUnitID" + DetailLength + "=" + escape(document.getElementById("Hidden_TD_Text_UnitID_" + i).value.Trim());
                strParams += "&DtlSPlanCount" + DetailLength + "=" + escape(document.getElementById("TD_Text_ProductCount_" + i).value.Trim());
                strParams += "&DtlSPlanTakeDate" + DetailLength + "=" + escape(document.getElementById("TD_Text_StartDate_" + i).value.Trim());
                strParams += "&DtlSApplyReason" + DetailLength + "=" + escape($("#TD_Text_Reason_" + i).val());
                strParams += "&DtlSFromBillID" + DetailLength + "=" + escape(document.getElementById("TD_Text_FromBillID_" + i).value.Trim());
                strParams += "&DtlSFromLineNo" + DetailLength + "=" + escape(document.getElementById("TD_Text_FromLineNo_" + i).value.Trim());
          if($("#HiddenMoreUnit").val()=="True")
                 { 
                   strParams += "&UsedUnitCount"+DetailLength+"="+escape($("#UsedUnitCount"+i+"").val());
                  var ExRate= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString();
                   var UsedUnitID=  parseInt (  $("#SignItem_TD_UnitID_Select"+i).val().split('|')[0].toString());
                strParams += "&ExRate"+DetailLength+"="+escape( ExRate);
                strParams += "&UsedUnitID"+DetailLength+"="+escape(UsedUnitID);
                 }
                DetailLength++;
            }
        }
    }
    strParams += "&DetailSLength=" + DetailLength + "";
    return strParams;
}



function fnGetDtlInfo() {
    var strParams = "";
    var signFrame = findObj("dg_LogSecond", document);
    var DetailLength = 0; //明细长度
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (i = 1; i < signFrame.rows.length; i++) {
            if (signFrame.rows[i].style.display != "none") {
                var tmpRemark = '';
                strParams += "&DtlSortNo" + DetailLength + "=" + escape(document.getElementById("TD2_Text_SortNo_" + i).value.Trim());
                strParams += "&DtlProdID" + DetailLength + "=" + escape(document.getElementById("Hidden_TD2_Text_ProductID_" + i).value.Trim());
                strParams += "&DtlProdNo" + DetailLength + "=" + escape(document.getElementById("TD2_Text_ProdNo_" + i).value.Trim());
                strParams += "&DtlProdName" + DetailLength + "=" + escape(document.getElementById("TD2_Text_ProductID_" + i).value.Trim());
                strParams += "&DtlUnitID" + DetailLength + "=" + escape(document.getElementById("Hidden_TD2_Text_UnitID_" + i).value.Trim());
                strParams += "&DtlPlanCount" + DetailLength + "=" + escape(document.getElementById("TD2_Text_ProductCount_" + i).value.Trim());
                strParams += "&DtlRequireDate" + DetailLength + "=" + escape(document.getElementById("TD2_Text_StartDate_" + i).value.Trim());
         if($("#HiddenMoreUnit").val()=="True")
                 {
                 
                   strParams += "&Used2UnitCount"+DetailLength+"="+escape($("#Used2UnitCount"+i+"").val());
                  var ExRate= $("#Hidden_TD2_Text_Exrate_"+i).val();
                   var UsedUnitID= parseInt ( $("#Hidden_TD2_Text_Used2UnitCount_"+i).val());
                strParams += "&Ex2Rate"+DetailLength+"="+escape( ExRate);
                strParams += "&Used2UnitID"+DetailLength+"="+escape(UsedUnitID);
                 }
                DetailLength++;
            }
        }
    }
    strParams += "&DetailLength=" + DetailLength + "";
    return strParams;
}

/*源单切换*/
function Fun_ChangeSourceBill(sourceBill) {
  DropSignRow();
 DropSignRowSecond();
    if (sourceBill > 0) {

        document.getElementById('imgGetDtl').style.display = '';
        document.getElementById('imgUnGetDtl').style.display = 'none';
        document.getElementById('imgExport').style.display = 'none';
        document.getElementById('imgUnExport').style.display = '';
    }
    else {
        document.getElementById('imgGetDtl').style.display = 'none';
        document.getElementById('imgUnGetDtl').style.display = '';
        document.getElementById('imgExport').style.display = '';
        document.getElementById('imgUnExport').style.display = 'none';
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

/*添加物品*/
function ShowProdInfo() {
    popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"', 'Check');
}
function GetValue() {
    var ck = document.getElementsByName("CheckboxProdID");
    var strarr = '';
    var str = "";
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            strarr += ck[i].value + ',';
        }
    }
    str = strarr.substring(0, strarr.length - 1);
    if (str == "") {
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
            $.each(msg.data, function(i, item) {//填充物品ID，物品编号，物品名称，规格，单位名称
                if (!IsExist(item.ProdNo)) {
                    AddSignRow(item.ID, item.ProdNo, item.ProductName, item.Specification, item.UnitID, item.CodeName, item.ColorName);
                    AddSignRowSecond(item.ID, item.ProdNo, item.ProductName, item.Specification, item.UnitID, item.CodeName, '', item.ColorName);

                }
            });

        },

        complete: function() { } //接收数据完毕
    });
    closeProductdiv();
}

/*判断是否有相同记录有返回true，没有返回false*/
var rerowID = "";
function IsExist(prodNo) {
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var prodNo1 = document.getElementById("TD_Text_ProdNo_" + i).value;

        if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
            rerowID = i;
            return true;
        }
    }
    return false;
}


function fnCheckInfo() {
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;

    var applyID = document.getElementById('txtIndentityID').value;
    if (parseInt(applyID) > 0) {
        ActionApply = 'Update';
    }


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
    if ("Add" == ActionApply) {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codruleApply_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "") {
            //获取输入的编号
            var PurApplyNo = document.getElementById("codruleApply_txtCode").value.Trim();
            //编号必须输入
            if (PurApplyNo == "") {
                isErrorFlag = true;
                fieldText += "编号|";
                msgText += "请输入编号|";
            }
            else if (!CodeCheck(PurApplyNo)) {
                isErrorFlag = true;
                fieldText = fieldText + "编号|";
                msgText = msgText + "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
            }
            else if (strlen(PurApplyNo) > 50) {
                isErrorFlag = true;
                fieldText += "编号|";
                msgText += "编号长度不大于50|";
            }

        }
    }

    if (strlen(document.getElementById("txtTitle").value.Trim()) > 100) {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "主题长度不大于100|";
    }
    //申请人不空
    if (document.getElementById("txtApplyUserID").value.Trim() == "") {
        isErrorFlag = true;
        fieldText += "申请人|";
        msgText += "请输入申请人|";
    }
    //申请部门不空
    if (document.getElementById("txtDeptID").value.Trim() == "") {
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
    else {
        if (!isDate(document.getElementById("txtApplyDate").value.Trim())) {
            isErrorFlag = true;
            fieldText += "申请日期|";
            msgText += "请输入正确的申请日期|";
        }
    }
    //备注长度不大于200
    if (strlen($("#txtRemark").val()) > 200) {
        isErrorFlag = true;
        fieldText += "备注|";
        msgText += "备注长度不大于200|";
    }

    //明细来源的校验
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        isErrorFlag = true;
        fieldText += "明细来源|";
        msgText += "请输入明细来源|";
    }
    else {
        var COUNT = 0;
        for (var i = 1; i < signFrame.rows.length; ++i) {
            if (signFrame.rows[i].style.display != "none") {
                COUNT++;

                var PlanCount = "TD_Text_ProductCount_" + i;
                var noc = document.getElementById(PlanCount).value.Trim();
                if (IsNumeric(noc, 14, $("#HiddenPoint").val()) == false) {
                    isErrorFlag = true;
                    fieldText += "明细来源|";
                    msgText += "请输入正确的申请数量|";
                }
                else {
                    if (noc > 0) {
                    }
                    else {
                        isErrorFlag = true;
                        fieldText += "明细来源|";
                        msgText += " 申请数量需大于零|";

                    }
                }

                var PlanTakeDate = "TD_Text_StartDate_" + i;
                var plandate = document.getElementById(PlanTakeDate).value.Trim();
                if (isDate(plandate) == false) {
                    isErrorFlag = true;
                    fieldText += "明细来源|";
                    msgText += "请输入正确的需求日期|";
                }
                //                var Remark = $("#DtlSRemark" + i).val();
                //                if (strlen(Remark) > 200) {
                //                    isErrorFlag = true;
                //                    fieldText += "明细来源|";
                //                    msgText += "备注长度不能超过200|";
                //                }
            }
        }
        if (COUNT == 0) {
            isErrorFlag = true;
            fieldText += "明细来源|";
            msgText += "请输入明细来源|";
        }

    }
    //    var signFrame2 = document.getElementById("DetailTable");
    //    for (var j = 1; j < signFrame2.rows.length; ++j) {
    //        if (signFrame.rows[j].style.display != "none") {
    //            var Remark = $("#DtlRemark" + j).val();
    //            if (strlen(Remark) > 200) {
    //                isErrorFlag = true;
    //                fieldText += "明细|";
    //                msgText += "备注长度不能超过200|";
    //            }
    //        }
    //    }
    if (isErrorFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isErrorFlag;
}


//===============================================添 加 删 除 行 处 理=======================================
function findObj(theObj, theDoc) {
    var p, i, foundObj;
    if (!theDoc) theDoc = document;
    if ((p = theObj.indexOf("?")) > 0 && parent.frames.length) {
        theDoc = parent.frames[theObj.substring(p + 1)].document;
        theObj = theObj.substring(0, p);
    }

    if (!(foundObj = theDoc[theObj]) && theDoc.all) foundObj = theDoc.all[theObj];
    for (i = 0; !foundObj && i < theDoc.forms.length; i++)
        foundObj = theDoc.forms[i][theObj]; for (i = 0; !foundObj && theDoc.layers && i < theDoc.layers.length; i++)
        foundObj = findObj(theObj, theDoc.layers[i].document);
    if (!foundObj && document.getElementById) foundObj = document.getElementById(theObj);
    return foundObj;
}
/*全选*/
function SelectAll() {
    var signFrame = findObj("dg_Log", document);
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log", document);
    if (document.getElementById("checkall").checked) {
        for (var i = 0; i < txtTRLastIndex - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                var objRadio = 'chk_Option_' + (i + 1);
                document.getElementById(objRadio.toString()).checked = true;
            }
        }
    }
    else {
        for (var i = 0; i < txtTRLastIndex - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                var objRadio = 'chk_Option_' + (i + 1);
                document.getElementById(objRadio.toString()).checked = false;
            }
        }
    }
}


/*删除行*/
function DeleteSignRow() {
    var signFrame = findObj("dg_Log", document);
    var ck = document.getElementsByName("chk");
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
 

    for (var i = 0; i < txtTRLastIndex - 1; i++) {
        var rowID = i + 1;
        if (signFrame.rows[i + 1].style.display != "none") {
            var objRadio = 'chk_Option_' + (i + 1);
            if (document.getElementById(objRadio.toString()).checked) {
                signFrame.rows[i + 1].style.display = 'none';
            }
            document.getElementById("TD_Text_SortNo_" + rowID).value = GenerateSortNo(rowID);
        }
    }

    //Fun_CountTotalCount();/*合计计算*/
        fnCalculateTotal();
    ClearAllOption();
}

/*取消选中时，同时取消全选按钮*/
function ClearAllOption() {
    document.getElementById('checkall').checked = false;
}

/*生成行序号*/
function GenerateSortNo(rowID) {
    var signFrame = findObj("dg_Log", document);
    var SortNo = 1; //起始行号
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (var i = 1; i < rowID; ++i) {
            if (signFrame.rows[i].style.display != "none") {
                document.getElementById("TD_Text_SortNo_" + i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0; //明细表不存在时返回0
}


function GenerateSecondSortNo(rowID) {
    var signFrame = findObj("dg_LogSecond", document);
    var SortNo = 1; //起始行号
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (var i = 1; i < rowID; ++i) {
            if (signFrame.rows[i].style.display != "none") {
                document.getElementById("TD2_Text_SortNo_" + i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0; //明细表不存在时返回0
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


function BillChooseFillSignRow(  productID, productNo, productName, specification, unitID, codeName, productCount, requireDate, reason, fromBillNo, fromBillID, fromLineNo,ColorName) {
  var txtTRLastIndex = findObj("txtTRLastIndex", document);
             var rowID = parseInt(txtTRLastIndex.value);

           
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Item_Row_" + rowID;
var i=0;
    var colChoose = newTR.insertCell(i++); //添加列:选择
    colChoose.className = "cell";
    colChoose.align = "center";
    colChoose.innerHTML = "<input name='chk' id='chk_Option_" + rowID + "' value=" + rowID + " type='checkbox' onclick=\"ClearAllOption();\" />";

    var colNum = newTR.insertCell(i++); //添加列:序号
    colNum.align = "center";
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\" id=\"TD_Text_SortNo_" + rowID + "\" value=\"" +GenerateSortNo(rowID) + "\"  class=\"tdinput\" size=\"5\" disabled />";

    var colProductNo = newTR.insertCell(i++); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<input type=\"text\" id=\"TD_Text_ProdNo_" + rowID + "\" value=\"" + productNo + "\" class=\"tdinput\" size=\"8\" readonly disabled />";

    var colProductID = newTR.insertCell(i++); //添加列:物品
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"hidden\" id=\"Hidden_TD_Text_ProductID_" + rowID + "\" value=\"" + productID + "\"  /><input type=\"text\" id=\"TD_Text_ProductID_" + rowID + "\" value=\"" + productName + "\" class=\"tdinput\" size=\"10\" onclick=\"popTechObj.ShowList('" + rowID + "');\" readonly  disabled/>";

    var colProductID = newTR.insertCell(i++); //添加列:规格
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"text\" id=\"TD_Text_Specification_" + rowID + "\" value=\"" + specification + "\" class=\"tdinput\" size=\"8\" readonly  disabled/>";


    var coDtlSColor = newTR.insertCell(i++); //添加列:颜色
    coDtlSColor.className = "cell";
    coDtlSColor.innerHTML = "<input type=\"text\" id=\"DtlSColor" + rowID + "\" value=\"" + ColorName + "\" class=\"tdinput\" size=\"8\" readonly  disabled/>";
    
    
 if($("#HiddenMoreUnit").val()=="False")
        { 
    var colUnitID = newTR.insertCell(i++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";
}
else
{
  document .getElementById ("spUnitID").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID").style.display="block";
        var colUnitID = newTR.insertCell(i++); //添加列:基本单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";

    
      var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
}


if($("#HiddenMoreUnit").val()=="False")
        { 
 var colProductCount = newTR.insertCell(i++); //添加列:需求数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD_Text_ProductCount_" + rowID + "' value=\"" + productCount + "\" class=\"tdinput\" size=\"10\" onblur =\" fnCalculateTotal();\"  />";

}
else
{
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block";
    var colProductCount = newTR.insertCell(i++); //添加列:基本数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD_Text_ProductCount_" + rowID + "' value=\"" + productCount + "\" class=\"tdinput\" size=\"10\" onblur =\" fnCalculateTotal();\"  />";

    
        var newUsedUnitCount=newTR.insertCell(i++);//添加列:采购数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='UsedUnitCount"+rowID+"' type='text' class=\"tdinput\"  value=\"" + 0 + "\"  style='width:90%;' onblur=\"Number_round(this," + $("#HiddenPoint").val() + "); ChangeUnit(this.id,"+rowID+")\"  />"; 
}
   
    var colStartDate = newTR.insertCell(i++); //添加列:需求日期
    colStartDate.className = "cell";
    colStartDate.innerHTML = "<input type=\"text\" id='TD_Text_StartDate_" + rowID + "' value=\"" + requireDate + "\"  class=\"tdinput\" onpropertychange =\"fnCalculateTotal();\"  onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('TD_Text_StartDate_" + rowID + "')})\" size=\"10\" />";

document.getElementById("ddlApplyReason").value=reason ;
    var colReason = newTR.insertCell(i++); //添加列:申请原因
    colReason.className = "cell";
    colReason.innerHTML =  "<select class='tdinput' id='TD_Text_Reason_" + rowID + "'>" + document.getElementById("ddlApplyReason").innerHTML + "</select>";
//    "<input type=\"text\" id='TD_Text_Reason_" + rowID + "' value=\"" + reason + "\"  class=\"tdinput\"  size=\"10\" />";

    var colFromBillID = newTR.insertCell(i++); //添加列:源单编号
    colFromBillID.className = "cell";
    colFromBillID.innerHTML = "<input type=\"text\" id='TD_Text_FromBillNo_" + rowID + "' value=\"" + fromBillNo + "\" class=\"tdinput\" size=\"10\" readonly disabled /><input type=\"hidden\" id='TD_Text_FromBillID_" + rowID + "' value=\"" + fromBillID + "\" class=\"tdinput\" size=\"10\" />";

    var colFromLineNo = newTR.insertCell(i++); //添加列:源单行号
    colFromLineNo.className = "cell";
    colFromLineNo.innerHTML = "<input type=\"text\" id='TD_Text_FromLineNo_" + rowID + "' value=\"" + fromLineNo + "\" class=\"tdinput\"  size=\"5\" readonly disabled />";
     document.getElementById('txtTRLastIndex').value = rowID + 1;
     return rowID;
           
}
function FillApplyContent(rowID,BaseCount,ProuductCount,UsedUnitID)
{  
 
 
  var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
  if (UsedUnitID !="Matrial")
  {
if (UsedUnitID!='')
{
   FillSelect(rowID,UsedUnitID,"Bill");
}
$("#UsedUnitCount"+rowID).attr("value", (parseFloat(ProuductCount)).toFixed($("#HiddenPoint").val()));
                    $("#TD_Text_ProductCount_"+rowID).attr("value", (parseFloat(BaseCount)).toFixed($("#HiddenPoint").val()));
                     
                    
              var planCount = $("#UsedUnitCount"+rowID).val();/*采购数量*/
  if (planCount !='')
           { 
            $("#TD_Text_ProductCount_"+rowID).attr("value", (parseFloat(planCount)*parseFloat (EXRate)).toFixed($("#HiddenPoint").val())); 
           }
}
else
{
 
                    $("#TD_Text_ProductCount_"+rowID).attr("value", (parseFloat(BaseCount)).toFixed($("#HiddenPoint").val()));
                     
                  
              var planCount = $("#TD_Text_ProductCount_"+rowID).val();/*采购数量*/
  if (planCount !='')
           { 
            $("#UsedUnitCount"+rowID).attr("value", (parseFloat(planCount)/parseFloat (EXRate)).toFixed($("#HiddenPoint").val())); 
            
            document .getElementById ("UsedUnitCount"+rowID).disabled=true ;
           }
}

     
              
                  
         
                  fnCalculateTotal();
}

function BillChooseFillSignRowSecond(productID, productNo, productName, specification, unitID, codeName, productCount, requireDate, planedCount, UsedUnitID, UsedUnitName, ExRate, UsedUnitCount, ColorName) {

  var txtTRLastIndex = findObj("txtTRLastIndexSecond", document);
             var rowID = parseInt(txtTRLastIndex.value);
 
    var signFrame = findObj("dg_LogSecond", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Item2_Row_" + rowID;
     var i=0;

    var colNum = newTR.insertCell(i++); //添加列:序号
    colNum.align = "center";
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\" id=\"TD2_Text_SortNo_" + rowID + "\" value=\"" + GenerateSecondSortNo(rowID)  + "\"  class=\"tdinput\" size=\"5\" disabled />";

    var colProductNo = newTR.insertCell(i++); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<input type=\"text\" id=\"TD2_Text_ProdNo_" + rowID + "\" value=\"" + productNo + "\" class=\"tdinput\" size=\"8\" readonly disabled />";

    var colProductID = newTR.insertCell(i++); //添加列:物品
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"hidden\" id=\"Hidden_TD2_Text_ProductID_" + rowID + "\" value=\"" + productID + "\"  /><input type=\"text\" id=\"TD2_Text_ProductID_" + rowID + "\" value=\"" + productName + "\" class=\"tdinput\" size=\"10\"   readonly disabled />";

    var colProductID = newTR.insertCell(i++); //添加列:规格
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"text\" id=\"TD2_Text_Specification_" + rowID + "\" value=\"" + specification + "\" class=\"tdinput\" size=\"8\" readonly disabled/>";

    var colDtlSSColor = newTR.insertCell(i++); //添加列:颜色
    colDtlSSColor.className = "cell";
    colDtlSSColor.innerHTML = "<input type=\"text\" id=\"DtlSSColor" + rowID + "\" value=\"" + ColorName + "\" class=\"tdinput\" size=\"8\" readonly disabled/>";  
    
    
 if($("#HiddenMoreUnit").val()=="False")
        { 
    var colUnitID = newTR.insertCell(i++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD2_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";

}
else
{
document .getElementById ("spUnitID2").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID2").style.display="block";
        var colUnitID = newTR.insertCell(i++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_UnitID_" + rowID + "' value=\"" +  unitID+ "\" /><input type=\"text\"  id='TD2_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";

  var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell"; 
            newFitNametd.innerHTML = "<input type=\"hidden\"  id='Hidden_TD2_Text_Exrate_" + rowID + "' value=\"" + ExRate + "\" />    <input type=\"hidden\"  id='Hidden_TD2_Text_Used2UnitCount_" + rowID + "' value=\"" + UsedUnitID + "\" /><input type=\"text\"  id='TD2_Text_Used2UnitCount_" + rowID + "' value=\"" + UsedUnitName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";//添加列内容
}
 
var pro=(parseFloat(productCount)).toFixed($("#HiddenPoint").val());
if (productCount=='')
{
pro='';
}
  if($("#HiddenMoreUnit").val()=="False")
        { 

    var colProductCount = newTR.insertCell(i++); //添加列:申请数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD2_Text_ProductCount_" + rowID + "' value=\"" + pro + "\" class=\"tdinput\" size=\"10\" readonly  disabled/>";
}
else
{

   document .getElementById ("SpProductCount2").innerText="基本数量";
 
    document .getElementById ("spUsedUnitCount2").style.display="block";
   var colProductCount = newTR.insertCell(i++); //添加列:基本数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD2_Text_ProductCount_" + rowID + "' value=\"" + pro + "\"  class=\"tdinput\" size=\"10\"  disabled/>";
  
    if (isNaN (UsedUnitCount))
    {
    UsedUnitCount='';
    }
      var newUsedUnitCount=newTR.insertCell(i++);//添加列:需求数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='Used2UnitCount"+rowID+"' type='text' class=\"tdinput\"  value=\"" + UsedUnitCount + "\"  style='width:90%;'  disabled  />"; 

}



    var colStartDate = newTR.insertCell(i++); //添加列:需求日期
    colStartDate.className = "cell";
    colStartDate.innerHTML = "<input type=\"text\" id='TD2_Text_StartDate_" + rowID + "' value=\"" + requireDate + "\"  class=\"tdinput\"   size=\"10\"  disabled/>";
    
var plan=(parseFloat(planedCount)).toFixed($("#HiddenPoint").val());
    var colPlanedCount = newTR.insertCell(i++); //添加列:已计划数量
    colPlanedCount.className = "cell";
    colPlanedCount.innerHTML = "<input type=\"text\" id='TD2_Text_PlanedCount_" + rowID + "' value=\"" + plan + "\" class=\"tdinput\" size=\"10\" readonly  disabled/>";
    
    
    
 document.getElementById('txtTRLastIndexSecond').value = rowID + 1;
}


function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{ 
    var RowID = popTechObj.InputObj;

    debugger;
    $("#Hidden_TD_Text_ProductID_"+RowID).val(id);
    $("#TD_Text_ProdNo_"+RowID).val(no);
    $("#TD_Text_ProductID_"+RowID).val(productname);
    $("#TD_Text_Specification_"+RowID).val(standard);
    $("#Hidden_TD_Text_UnitID_"+RowID).val(unitid);
    $("#TD_Text_UnitID_"+RowID).val(unit);
      if($("#HiddenMoreUnit").val()=="False")
            {
              fnMergeDetail();
            }
            else
            {
             GetUnitGroupSelectEx(id,"InUnit","SignItem_TD_UnitID_Select" + RowID,"ChangeUnit(this.id,"+RowID+")","unitdiv" + RowID,'',"FillContent("+RowID+")");//绑定单位组
            }
  

}
function fnCanMerge(rowID)
{//确定该条明细来源是否可以合并,可以合并返回行号，不可以合并返回0
    
    var ProductNo = document.getElementById("TD_Text_ProdNo_"+rowID).value.Trim();
    var RequireDate = document.getElementById("TD_Text_StartDate_"+rowID).value.Trim();
    
    var signFrame = document.getElementById("dg_LogSecond");
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
                var ThisProductNo = document.getElementById("TD2_Text_ProdNo_"+i).value.Trim();
                var ThisRequireDate = document.getElementById("TD2_Text_StartDate_"+i).value.Trim();
                if((ProductNo == ThisProductNo)&&(RequireDate == ThisRequireDate))
                return i;
            }
        }
    }
    return 0;
}

function fnMergeDetail()
{ 

try
{

    if($("#IsCite").val() == "True")
    return;
    DropSignRowSecond();
    var fieldText = "";
    var msgText = "";
    var isCorrectFlag = true;
    
    var signFrame = findObj("dg_Log",document);
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
                
                    document.getElementById("TD2_Text_ProductCount_"+rowID).value =
                     (parseFloat(document.getElementById("TD2_Text_ProductCount_"+rowID).value.Trim())+parseFloat(document.getElementById("TD_Text_ProductCount_"+i).value.Trim())).toFixed($("#HiddenPoint").val());
            
                       if($("#HiddenMoreUnit").val()=="True")
                       {
                            document.getElementById("Used2UnitCount"+rowID).value =
                             (parseFloat(document.getElementById("Used2UnitCount"+rowID).value.Trim())+parseFloat(document.getElementById("UsedUnitCount"+i).value.Trim())).toFixed($("#HiddenPoint").val());
                       }
               
               
               
                }
                else
                {//不可以合并
                   if($("#HiddenMoreUnit").val()=="True")
                 { var ExRate= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString();
                   var UsedUnitID= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[0].toString();
                   var UsedUnitName = document.getElementById ("SignItem_TD_UnitID_Select"+i).options[document.getElementById ("SignItem_TD_UnitID_Select"+i).selectedIndex].text;   
              var UsedUnitCount=     parseFloat ($("#UsedUnitCount"+i).val()).toFixed($("#HiddenPoint").val());
           if (!isNaN (ExRate))
           {
           document .getElementById ("TD_Text_ProductCount_"+i).value= parseFloat ( UsedUnitCount*ExRate ).toFixed($("#HiddenPoint").val());
           }

           BillChooseFillSignRowSecond($("#Hidden_TD_Text_ProductID_" + i).val(), $("#TD_Text_ProdNo_" + i).val(), $("#TD_Text_ProductID_" + i).val(), $("#TD_Text_Specification_" + i).val(), $("#Hidden_TD_Text_UnitID_" + i).val(), $("#TD_Text_UnitID_" + i).val(), parseFloat($("#TD_Text_ProductCount_" + i).val()).toFixed($("#HiddenPoint").val()), $("#TD_Text_StartDate_" + i).val(), 0, UsedUnitID, UsedUnitName, ExRate, UsedUnitCount, document.getElementById("DtlSColor" + i).value.Trim())
                }else
                {
                
        var ProdNo=     document .getElementById ("TD_Text_ProdNo_"+i).value.Trim()      
    var productID= document .getElementById ("Hidden_TD_Text_ProductID_"+i).value.Trim() ;     
    var ProductName= document .getElementById ("TD_Text_ProductID_"+i).value.Trim() ;     
  var Specification= document .getElementById ("TD_Text_Specification_"+i).value.Trim() ;     
   var UnitID= document .getElementById ("Hidden_TD_Text_UnitID_"+i).value.Trim() ;     
   var CodeName= document .getElementById ("TD_Text_UnitID_"+i).value.Trim() ;     
   var ProductCount= document .getElementById ("TD_Text_ProductCount_"+i).value.Trim() ;     
 var StartDate= document .getElementById ("TD_Text_StartDate_"+i).value.Trim() ;  
 var ColorName= document .getElementById ("DtlSColor"+i).value.Trim() ;



 var xx = AddSignRowSecond(productID, ProdNo, ProductName, Specification, UnitID, CodeName, '', ColorName);
               document .getElementById("TD2_Text_ProductCount_"+xx).value=ProductCount;
                    document .getElementById("TD2_Text_StartDate_"+xx).value=StartDate;
                    document .getElementById ("TD2_Text_PlanedCount_"+xx).value="0";
                }
             
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
    catch (Error )
    {}
}


function ShowSnapshot() {
 
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var ck = document.getElementsByName("chk"); 
    var signFrame = findObj("dg_Log", document);
    for (var i = 0; i < ck.length; i++) 
    {
    
            var rowID = i + 1;
            if (signFrame.rows[rowID].style.display != "none")
             {
                  if (ck[i].checked) 
                    {
                        detailRows++;
                        intProductID = document.getElementById('Hidden_TD_Text_ProductID_' + rowID).value;
                        snapProductName = document.getElementById('TD_Text_ProductID_' + rowID).value;
                        snapProductNo = document.getElementById('TD_Text_ProdNo_' + rowID).value; 
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


//---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
/*
 参数：商品ID，商品编号，商品名称，去税售价，单位ID，单位名称，销项税率（%），含税售价，销售折扣，规格，类型名称，类型ID，含税进价，去税进价，进项税率(%)，标准成本
*/
//根据条码获取的商品信息填充数据
function GetGoodsDataByBarCode(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, IsBatchNo, BatchNo, ProductCount, CurrentStore, Source, ColorName)
{
 
    if(!IsExist(ProdNo))//如果重复记录，就不增加
    {
    
    
    var rowID=    GetBarFillSignRow( ColorName, ID, ProdNo, ProductName, Specification, UnitID, UnitName, "1", "", "", "", "", "");
 if($("#HiddenMoreUnit").val()=="True")
                          {
        GetUnitGroupSelectEx(ID,"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+")","unitdiv" + rowID,'',"FillApplyContent("+rowID+","+1+","+1+",'')");//绑定单位组
        
                         }   
                         else
                         {
                            fnCalculateTotal();
                         }
                         
//       var rowID=AddDtlSSignRow();//插入行
//       //填充数据
//       BarCode_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification,rowID);
    }
    else
    {
     if($("#HiddenMoreUnit").val()=="False")
      {
      document.getElementById("TD_Text_ProductCount_"+rerowID).value=    parseFloat (parseFloat(document.getElementById("TD_Text_ProductCount_"+rerowID).value)+1).toFixed($("#HiddenPoint").val());
   
        }
        else
        {
            var count1=document.getElementById("UsedUnitCount"+rerowID).value;
 
        document.getElementById("UsedUnitCount"+rerowID).value=     parseFloat (parseFloat(count1)+1).toFixed($("#HiddenPoint").val());
        }
        
 
        fnCalculateTotal();//更改数量后重新计算
    }
    
}


 function BarCode_FillParent_Content(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification,rowID)
{    

    $("#Hidden_TD_Text_ProductID_"+rowID).val(ID);//商品ID
    $("#TD_Text_ProdNo_"+rowID).val(ProdNo);//商品编号
    $("#TD_Text_ProductID_"+rowID).val(ProductName);//商品名称
    $("#TD_Text_Specification_"+rowID).val(Specification);//规格
    $("#Hidden_TD_Text_UnitID_"+rowID).val(UnitID);//单位ID
    $("#TD_Text_UnitID_"+rowID).val(UnitName);//单位名称
    $("#TD_Text_ProductCount_"+rowID).val(1);//设置数量为1
    fnMergeDetail();
    fnCalculateTotal();
}
function IsExistBarCode(ID)
{
 
     var signFrame = findObj("dg_Log", document);
     if((typeof(signFrame)=="undefined")|| signFrame==null)
     {
        return false;
     }
     for (i = 1; i < signFrame.rows.length; i++) {//判断商品是否在明细列表中
         if (signFrame.rows[i].style.display != "none") {
              var rowid = signFrame.rows[i].id;
              var rid=rowid.substring(8);
              var ProductID = $("#Hidden_TD_Text_ProductID_" + rid).val(); //商品ID（对应商品表ID）
              if(ProductID==ID)
               {
                   rerowID=i;
                   return true;
               }
            }
        }
     return false;
}

function Fun_ConfirmOperate()
{//确认
    //更改状态
    
      ActionApply ="Confirm";
    var ID =intApplyID;
        
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
            $("#txtConfirmor").attr("value",aaa[0]);
            $("#txtConfirmorReal").attr("value",aaa[1]);
            $("#txtConfirmDate").attr("value",aaa[2]);
                $("#txtModifiedUserName").val(aaa[3]);
                $("#txtModifiedUserID").val(aaa[3])
                $("#txtModifiedDate").val(aaa[2]);
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！"); 
            $("#txtBillStatus").attr("value","2");
            $("#txtBillStatusReal").attr("value","执行");
          
            GetFlowButton_DisplayControl();
        } 
    }); 
}


function Fun_UnConfirmOperate()
{ 
    if(!confirm("是否执行取消确认操作？！"))
        return;
    var URLParams = "ActionApply=CancelConfirm";
    URLParams += "&ID="+intApplyID;
    URLParams += "&No="+$("#codruleApply_txtCode").val();
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
            $("#txtConfirmor").attr("value","");
            $("#txtConfirmorReal").attr("value","");
            $("#txtConfirmDate").attr("value","");
            $("#txtModifiedUserName").val(BackValue[1]);
            $("#txtModifiedUserID").val(BackValue[1])
            $("#txtModifiedDate").val(BackValue[0]);
            
            $("#txtBillStatus").val("1");
            $("#txtBillStatusReal").val("制单");
            //调用审批流程按钮控制方法
            GetFlowButton_DisplayControl();
            break;
        case 2:
            break;
    }
}


function Fun_CompleteOperate(isComplete)
{ 
    if(isComplete)
    {
    if(!confirm("是否执行结单操作？"))
    return;
        $("#txtBillStatus").attr("value","4");
        $("#txtBillStatusReal").attr("value","手动结单");
        ActionApply="Complete"; 
        var ID = intApplyID;
            
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
                $("#txtCloser").attr("value",aaa[0]);
                $("#txtCloserReal").attr("value",aaa[1]);
                $("#txtCloseDate").attr("value",aaa[2]);
                $("#txtModifiedUserName").val(aaa[3]);
                $("#txtModifiedUserID").val(aaa[3])
                $("#txtModifiedDate").val(aaa[2]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单成功！");
            fnStatus($("#txtBillStatus").val(),$("#IsCite").val());
                GetFlowButton_DisplayControl();
            } 
        }); 
    }
    else
    {
    if(!confirm("是否执行取消结单操作？"))
    return;
        $("#txtBillStatus").attr("value","2");
        $("#txtBillStatusReal").attr("value","执行");
        
     
        var ActionApply = "ConcelComplete";
        var ID = intApplyID;
            
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
                $("#txtCloser").attr("value","");
                $("#txtCloserReal").attr("value","");
                $("#txtCloseDate").attr("value","");
                $("#txtModifiedUserName").val(aaa[1]);
                $("#txtModifiedUserID").val(aaa[1])
                $("#txtModifiedDate").val(aaa[0]);
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消结单成功！");
                 fnStatus($("#txtBillStatus").val(),$("#IsCite").val());
                GetFlowButton_DisplayControl();
            } 
        }); 
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


function GetBarFillSignRow( ColorName, productID, productNo, productName, specification, unitID, codeName, productCount, requireDate, reason, fromBillNo, fromBillID, fromLineNo) {
  var txtTRLastIndex = findObj("txtTRLastIndex", document);
             var rowID = parseInt(txtTRLastIndex.value);
             
  
    var signFrame = findObj("dg_Log", document);
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = "Item_Row_" + rowID;
var i=0;
    var colChoose = newTR.insertCell(i++); //添加列:选择
    colChoose.className = "cell";
    colChoose.align = "center";
    colChoose.innerHTML = "<input name='chk' id='chk_Option_" + rowID + "' value=" + rowID + " type='checkbox' onclick=\"ClearAllOption();\" />";

    var colNum = newTR.insertCell(i++); //添加列:序号
    colNum.align = "center";
    colNum.className = "cell";
    colNum.innerHTML = "<input type=\"text\" id=\"TD_Text_SortNo_" + rowID + "\" value=\"" +GenerateSortNo(rowID) + "\"  class=\"tdinput\" size=\"5\" disabled />";

    var colProductNo = newTR.insertCell(i++); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<input type=\"text\" id=\"TD_Text_ProdNo_" + rowID + "\" value=\"" + productNo + "\" class=\"tdinput\" size=\"8\" readonly onclick=\"popTechObj.ShowList('" + rowID + "');\" />";

    var colProductID = newTR.insertCell(i++); //添加列:物品
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"hidden\" id=\"Hidden_TD_Text_ProductID_" + rowID + "\" value=\"" + productID + "\"  /><input type=\"text\" id=\"TD_Text_ProductID_" + rowID + "\" value=\"" + productName + "\" class=\"tdinput\" size=\"10\" onclick=\"popTechObj.ShowList('" + rowID + "');\" readonly  disabled/>";

    var colProductID = newTR.insertCell(i++); //添加列:规格
    colProductID.className = "cell";
    colProductID.innerHTML = "<input type=\"text\" id=\"TD_Text_Specification_" + rowID + "\" value=\"" + specification + "\" class=\"tdinput\" size=\"8\" readonly  disabled/>";



    var colDtlSColor = newTR.insertCell(i++); //添加列:颜色
    colDtlSColor.className = "cell";
    colDtlSColor.innerHTML = "<input type=\"text\" id=\"DtlSColor" + rowID + "\" value=\"" + ColorName + "\" class=\"tdinput\" size=\"8\" readonly />";
    
    
 if($("#HiddenMoreUnit").val()=="False")
        { 
    var colUnitID = newTR.insertCell(i++); //添加列:单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";
}
else
{
  document .getElementById ("spUnitID").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID").style.display="block";
        var colUnitID = newTR.insertCell(i++); //添加列:基本单位
    colUnitID.className = "cell";
    colUnitID.innerHTML = "<input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + rowID + "' value=\"" + unitID + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + rowID + "' value=\"" + codeName + "\" class=\"tdinput\"  size=\"6\" readonly disabled/>";

    
      var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
}


if($("#HiddenMoreUnit").val()=="False")
        { 
 var colProductCount = newTR.insertCell(i++); //添加列:需求数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD_Text_ProductCount_" + rowID + "' value=\"" + productCount + "\" class=\"tdinput\" size=\"10\" onblur =\" fnCalculateTotal();\"  />";

}
else
{
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block";
    var colProductCount = newTR.insertCell(i++); //添加列:基本数量
    colProductCount.className = "cell";
    colProductCount.innerHTML = "<input type=\"text\" id='TD_Text_ProductCount_" + rowID + "' value=\"" + productCount + "\" class=\"tdinput\" size=\"10\" onblur =\" fnCalculateTotal();\"  />";

    
        var newUsedUnitCount=newTR.insertCell(i++);//添加列:采购数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='UsedUnitCount"+rowID+"' type='text' class=\"tdinput\"  value=\"" + 0 + "\"  style='width:90%;' onblur=\" Number_round(this," + $("#HiddenPoint").val() + ");ChangeUnit(this.id,"+rowID+")\"  />"; 
}
   
    var colStartDate = newTR.insertCell(i++); //添加列:需求日期
    colStartDate.className = "cell";
    colStartDate.innerHTML = "<input type=\"text\" id='TD_Text_StartDate_" + rowID + "' value=\"" + requireDate + "\"  class=\"tdinput\" onpropertychange =\"fnCalculateTotal();\"  onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('TD_Text_StartDate_" + rowID + "')})\" size=\"10\" />";

document.getElementById("ddlApplyReason").value=reason ;
    var colReason = newTR.insertCell(i++); //添加列:申请原因
    colReason.className = "cell";
    colReason.innerHTML =  "<select class='tdinput' id='TD_Text_Reason_" + rowID + "'>" + document.getElementById("ddlApplyReason").innerHTML + "</select>";
//    "<input type=\"text\" id='TD_Text_Reason_" + rowID + "' value=\"" + reason + "\"  class=\"tdinput\"  size=\"10\" />";

    var colFromBillID = newTR.insertCell(i++); //添加列:源单编号
    colFromBillID.className = "cell";
    colFromBillID.innerHTML = "<input type=\"text\" id='TD_Text_FromBillNo_" + rowID + "' value=\"" + fromBillNo + "\" class=\"tdinput\" size=\"10\" readonly disabled /><input type=\"hidden\" id='TD_Text_FromBillID_" + rowID + "' value=\"" + fromBillID + "\" class=\"tdinput\" size=\"10\" />";

    var colFromLineNo = newTR.insertCell(i++); //添加列:源单行号
    colFromLineNo.className = "cell";
    colFromLineNo.innerHTML = "<input type=\"text\" id='TD_Text_FromLineNo_" + rowID + "' value=\"" + fromLineNo + "\" class=\"tdinput\"  size=\"5\" readonly disabled />";
     document.getElementById('txtTRLastIndex').value = rowID + 1;
     return rowID;
           
}