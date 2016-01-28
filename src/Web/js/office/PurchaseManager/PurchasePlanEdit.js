$(document).ready(function()
{
//alert(document.getElementById("hiddenAction").value);
      requestobj = GetRequest();
      requestparam1=requestobj['retval'];
     FillPurchasePlan(requestparam1);
    });

//获取url中"?"符后的字串
function GetRequest()
 {
   var url = location.search; 
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
  
function FillPurchasePlan(PlanNo)
{
    $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/PurchaseManager/PurchasePlanEdit.ashx",//目标地址
       data:'PlanNo='+escape(PlanNo),
       cache:false,
       beforeSend:function()
       {
       },          
       success: function(msg){
                $.each(msg.datap,function(i,item){
                    if(item.PlanNo!= null && item.PlanNo != "")
                    {
//                        $("#txtPlanNo").attr("value",item.PlanNo);
                        document.getElementById("divPlanNo").innerHTML = item.PlanNo;
                        $("#txtTitle").attr("value",item.Title);//
              $("#PurchaseType_ddlCodeType").attr("value",item.TypeID);//
                        $("#txtPlanUserName").attr("value",item.PlanUserName);//
                        $("#txtPlanUserName").attr("title",item.PlanUserID);
                        $("#txtPurchaserName").attr("value",item.PurchaserName);//
                        $("#txtPurchaserName").attr("title",item.PurchaserID);
                        $("#txtPlanDeptName").attr("value",item.PlanDeptName);//
                        $("#txtPlanDeptName").attr("title",item.PlanDeptID);//
                        $("#ddlFromType").attr("value",item.FromType);//
                        
                        $("#txtPlanCnt").attr("value",item.CountTotal);//
                        $("#txtPlanMoney").attr("value",item.PlanMoney);
                        
                        $("#txtCreatorName").attr("value",item.CreatorName);
                        $("#txtCreatorID").attr("value",item.Creator);
                        $("#txtCreateDate").attr("value",item.CreateDate);//
                        $("#txtBillStatus").attr("value",item.BillStatusName);//
                        $("#txtBillStatusID").attr("value",item.BillStatus);//
                        $("#txtConfirmorName").attr("value",item.ConfirmorName);//
                        $("#txtConfirmorID").attr("value",item.Confirmor);//
                        $("#txtConfirmDate").attr("value",item.ConfirmDate);//
                        $("#txtModifiedUserName").attr("value",item.ModifiedUserName);//
                        $("#txtModifiedUserID").attr("value",item.ModifiedUserID);//
                        $("#txtRemark").attr("value",item.Remark);//
                       
                    }                     

                  
               });
                $.each(msg.data,function(i,item){
                    if(item.ProductNo != null && item.ProductNo != "")
                    {
                        var index = AddDtlSSignRow();
                        $("#DtlSProductID"+index).attr("value",item.ProductID);
                        $("#DtlSProductNo"+index).attr("value",item.ProductNo);
                        $("#DtlSProductName"+index).attr("value",item.ProductName);
                  $("#DtlSSpecification"+index).attr("value",item.Specification);
                        $("#DtlSUnitID"+index).attr("value",item.UnitID);
                        $("#DtlSUnitName"+index).attr("value",item.UnitName);
                        $("#DtlSUnitPrice"+index).attr("value",item.UnitPrice);
                        $("#DtlSRequireCount"+index).attr("value",item.RequireCount);
                        
                        $("#DtlSPlanCount"+index).attr("value",item.PlanCount);
                        $("#DtlSRequireDate"+index).attr("value",item.RequireDate);
                        $("#DtlSPlanTakeDate"+index).attr("value",item.PlanTakeDate);
                        $("#DtlSApplyReasonID"+index).attr("value",item.ApplyReasonID);
                        
                        $("#DtlSApplyReasonName"+index).attr("value",item.ApplyReasonName);
                        $("#DtlSFromBillID"+index).attr("value",item.FromBillID);
                        $("#DtlSFromBillNo"+index).attr("value",item.FromBillNo);
                        $("#DtlSFromLineNo"+index).attr("value",item.FromSortNo);
                        
                        $("#DtlSProviderID"+index).attr("value",item.ProviderID);
                        $("#DtlSProviderName"+index).attr("value",item.ProviderName);
                        $("#DtlSRemark"+index).attr("value",item.Remark);
                        $("#DtlSTotalPrice"+index).attr("value",item.TotalPrice);
                    }                     

                  
               });
               $.each(msg.data2,function(i,item){
                    if(item.ProductNo != null && item.ProductNo != "")
                    {
                        var index = AddDtlSignRow();
                        $("#DtlProductID"+index).attr("value",item.ProductID);
                        
                        $("#DtlProductNo"+index).attr("value",item.ProductNo);
                        $("#DtlProductName"+index).attr("value",item.ProductName);
          $("#DtlSpecification"+index).attr("value",item.Specification);
                        $("#DtlUnitID"+index).attr("value",item.UnitID);
                        
                        $("#DtlUnitName"+index).attr("value",item.UnitName);
                        $("#DtlProductCount"+index).attr("value",item.ProductCount);
                        $("#DtlRequireDate"+index).attr("value",item.RequireDate);
                        $("#DtlProviderID"+index).attr("value",item.ProviderID);
                        
                        $("#DtlProviderName"+index).attr("value",item.ProviderName);
                        $("#DtlUnitPrice"+index).attr("value",item.UnitPrice);
                        $("#DtlTotalPrice"+index).attr("value",item.TotalPrice);
                        $("#DtlRemark"+index).attr("value",item.Remark);
                    }
               });
              },
       error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}