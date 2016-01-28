//采购合同

var DtlS_Item = new Array();
var Dtl_Item2 = new Array();
var DtlCount = 0;

///周末增加
var page = "";
var ContractNo ;
var Title ;
var TypeID ;
var DeptID  ;
var Seller  ;
var FromType  ;
var ProviderID  ;
var BillStatus ;
var UsedStatus ;
var Isliebiao ;

$(document).ready(function()
{
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
    }
//    alert( document.referrer.toString());
//debugger;
    requestobj = GetRequest();
    var intMasterPurchaseContractID = document.getElementById("txtIndentityID").value;
    if(intMasterPurchaseContractID==0)
    {
        GetFlowButton_DisplayControl();
    }
    recordnoparam = intMasterPurchaseContractID.toString();
    var Isyinyong = requestobj['Isyinyong'];
    var Danjuzhuangtai = requestobj['Danjuzhuangtai'];
    var Shenpizhuangtai = requestobj['Shenpizhuangtai'];

    var contractNo = requestobj['ContractNo'];
    if(contractNo != 0)
    {
        document.getElementById("txtIsliebiaoNo").value = contractNo;
    }
    page = requestobj['Pages'];
    var ContractNo1 = requestobj['ContractNo1'];
    if(ContractNo1 == undefined)
    {
        ContractNo1="";
    }
    var Title = requestobj['Title'];
    if(Title == undefined)
    {
        Title="";
    }
    var TypeID = requestobj['TypeID'];
    var DeptID = requestobj['DeptID'];
    if(DeptID == undefined)
    {
        DeptID="";
    }
    var DeptName = requestobj['DeptName'];
    var Seller = requestobj['Seller'];
    if(Seller == undefined)
    {
        Seller="";
    }
    var SellerName = requestobj['SellerName'];
    var FromType = requestobj['FromType'];
    var ProviderID = requestobj['ProviderID'];
    if(ProviderID == undefined)
    {
        ProviderID="";
    }
    var ProviderName = requestobj['ProviderName'];
    var BillStatus = requestobj['BillStatus'];
    var UsedStatus = requestobj['UsedStatus'];
    var Isliebiao = requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var pageCount = requestobj['pageCount'];
    var FromTypeName = requestobj['FromTypeName'];
    var Rate = requestobj['Rate'];
    var intFromType = requestobj['intFromType'];
    var ListModuleID = requestobj['ListModuleID'];
     var EFIndex = requestobj['EFIndex'];
    var EFDesc = requestobj['EFDesc'];
    if(typeof(intFromType) != "undefined")
    {
        document.getElementById("hidintFromType").value = intFromType;
        document.getElementById("hidListModuleID").value = ListModuleID;
    }
    
    if(typeof(Isliebiao)=="undefined")
    {
        document.getElementById("chkIsZzs").checked = true;
        document.getElementById("chkisAddTaxText1").style.display="inline";
        
    }
    else
    {
        if(Isliebiao == 1)////
        {
             $("#btn_back").show();//桌面返回修改
             document.getElementById("hidIsliebiao").value = Isliebiao;
             document.getElementById("hidFromTypeName").value = FromTypeName;
             document.getElementById("hidRate").value = Rate; 
         }
    } 
    //    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
var URLParams ="EFIndex="+escape(EFIndex)+"&EFDesc="+escape(EFDesc)+ "&Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&ContractNo1="+escape(ContractNo1)+"&Title="+escape(Title)+"&TypeID="+escape(TypeID)+"&DeptName="+escape(DeptName)+"&DeptID="+escape(DeptID)+"&Seller="+escape(Seller)+"&SellerName="+escape(SellerName)+"&FromType="+escape(FromType)+"&ProviderName="+escape(ProviderName)+"&ProviderID="+escape(ProviderID)+"&BillStatus="+escape(BillStatus)+"&UsedStatus="+escape(UsedStatus)+"&PageIndex="+escape(PageIndex)+"&pageCount="+escape(pageCount)+""; 
    document.getElementById("hidSearchCondition").value = URLParams;
    
    DealPage(recordnoparam);
//    fnGetExtAttr();
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

//处理加载页面
function DealPage(recordnoparam)
{
//显示返回按钮
        ////
//        if(typeof(document.getElementById("hidIsliebiao").value)!="undefined")
//        {
//            $("#btn_back").show();
    if (recordnoparam != 0) {
        $("#btn_back").show(); //桌面返回修改
        //isnew="2";
        //$("#hfLoveID").attr("value",recordnoparam);//ID记录在隐藏域中
        document.getElementById("txtAction").value = "2";
        document.getElementById("divTitle").innerText = "采购合同";

        //        //显示返回按钮
        //        $("#btn_back").show();
        GetContractInfo(recordnoparam);
    }
    else {
        GetExtAttr('officedba.PurchaseContract', null);
    }
          
//        }
        
}

//返回
function Back()
{ 

//  var backurlstr = document.referrer.toString();
//  var backurl =  new URL(backurlstr);
//  var currenturl =   window.location.toString();
//  var str = backurlstr.split('?')[0];
//  var ModuleID = backurl.keys("ModuleID").value;
//  var currurltemp = new URL(currenturl)  ;
//  
//  var sdfsdaffads = currurltemp.url.indexOf("?");
//  
//  var str0 =currurltemp.url.substring(currurltemp.url.indexOf("?")+1,currurltemp.url.length);
//  var str1 = str0.substring(0,str0.indexOf("ModuleID")+9);
//  var str2 = str0.substring(str0.indexOf("ModuleID")+9,str0.length);
//  
//  var str3 = str2.substring(str2.indexOf("&"),str2.length);
//  str+= str1+ModuleID+str3;
//  
//   window.location.href  =str ;
  //window.open(str,'_self');
  


    if(document.getElementById("hidIsliebiao").value.Trim() == "")
    {
        ListModuleID = document.getElementById("hidListModuleID").value.Trim();
        
        if(document.getElementById("hidintFromType").value.Trim() == '3')
        {
            window.location.href='../../Personal/WorkFlow/FlowMyApply.aspx?ModuleID=ListModuleID';
        }
        else if(document.getElementById("hidintFromType").value.Trim() == '4')
        {
            window.location.href='../../Personal/WorkFlow/FlowMyProcess.aspx?ModuleID=ListModuleID';
        }
        
        else if(document.getElementById("hidintFromType").value.Trim() == '5')
        {
            window.location.href='../../Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=ListModuleID';
        }
        else
        {
            window.location.href='../../../Desktop.aspx';
        }
    }
    else
    {
var URLParams = document.getElementById("hidSearchCondition").value;
 
    window.location.href='PurchaseContractInfo.aspx?'+URLParams;
    } 
    
    
}


function GetContractInfo(ID)
{
//       document.getElementById("divPurchaseContractNo").innerHTML=ContractNo;
//       document.getElementById("divPurchaseContractNo").display="block";
//       document.getElementById("divInputNo").style.display="none";
     var contractNo= document.getElementById("txtIsliebiaoNo").value.Trim();
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/PurchaseManager/PurchaseContractEdit.ashx",//目标地址
//       data:'ContractNo='+escape(ContractNo),
//       data:"ContractNo="+contractNo+"&ID="+ID+"",
       data:"ID="+ID+"",
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        //基本信息
                        //$("#CodingRuleControl1").attr("value",item.ContractNo);//合同编号
                        $("#CodingRuleControl1_txtCode").attr("value",item.ContractNo);//合同编号
                        $("#divPurchaseContractNo").attr("value",item.ContractNo);//合同编号
                        $("#txtTitle").attr("value",item.Title);//主题
                        $("#ddlFromType").attr("value",item.FromType);//源单类型id
                        //$("#ddlFromType").attr("title",item.FromTypeName);//源单类型名称
                        $("#txtProviderID").attr("value",item.ProviderName);//供应商名称
                        $("#txtHidProviderID").attr("value",item.ProviderID);//供应商ID
                        $("#DrpTypeID").attr("value",item.TypeID);//采购类别
                        $("#UsertxtSeller").attr("value",item.SellerName);//采购员名称
                        $("#txtHidOurUserID").attr("value",item.Seller);//采购员id
                        $("#DeptDeptID").attr("value",item.DeptName);//部门名称
                        $("#HidDeptID").attr("value",item.DeptID);//部门id
//                        $("#chkIsZzs").attr("value",item.isAddTax);//是否是增值税
                        if (item.isAddTax ==1)
                        {
                            document.getElementById("chkIsZzs").checked = true;
                            document.getElementById("chkisAddTaxText1").style.display="inline";
                        }
                        else
                        {
                            document.getElementById("chkIsZzs").checked = false;
                            document.getElementById("chkisAddTaxText2").style.display="inline";
                        }
                        $("#txtSignDate").attr("value",item.SignDate);//签约时间
                        $("#txtOppositerUserID").attr("value",item.TheyDelegate);//供应商签约人(直接名称)
                        $("#txtOurUserID").attr("value",item.OurUserName);//我方签约人名称
                        $("#txtHidOurUserID1").attr("title",item.OurUserID);//我方签约人id 
                        $("#txtAddress").attr("value",item.SignAddr);//签约地点
                            var takeType=item.TakeType;
                          if (takeType==""|!jsSelectIsExitItem('DrpTakeType',takeType))
                             {
                                            takeType="0";
                               }
                               
                        $("#DrpTakeType").attr("value",takeType);//交货方式
                         var carrytype=item.CarryType;
                          if (carrytype==""|!jsSelectIsExitItem('DrpCarryType',carrytype))
                             {
                                            carrytype="0";
                               }
                        
                        $("#DrpCarryType").attr("value",carrytype);//运送方式
                        var paytype=item.PayType;
                          if (paytype==""|!jsSelectIsExitItem('DrpPayType',paytype))
                             {
                                            paytype="0";
                               }
                            var moneytype=item.MoneyType;
                               if (moneytype==""|!jsSelectIsExitItem('DrpMoneyType',moneytype))
                             {
                                            moneytype="0";
                               }
                        $("#DrpPayType").attr("value",paytype);//结算方式
                             $("#DrpMoneyType").attr("value",moneytype);//支付方式
                        $("#txtRate").attr("value", item.Rate);//汇率
                        $("#CurrencyTypeID").attr("value",item.CurrencyType);//币种ID
//                        var hhhh = item.CurrencyType+"_"+item.Rate;
//                          $("#drpCurrencyType").attr("value",hhhh);//币种
                          
                          for(var i=0;i<document.getElementById("drpCurrencyType").options.length;++i)
                            {
                                if(document.getElementById("drpCurrencyType").options[i].value.split('_')[0]== item.CurrencyType)
                                {
                                  document .getElementById ("drpCurrencyType").options[i].selected =true ;     
//                                    $("#drpCurrencyType").attr("selectedIndex",i);
                                    break;
                                }
                            }
                                                
                        //合计信息
                       
                        $("#txtCountTotal").attr("value",(parseFloat(item.CountTotal)).toFixed($("#HiddenPoint").val()));//采购数量合计
                        $("#txtTotalMoney").attr("value",(parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()));//金额合计
                        $("#txtTotalTaxHo").attr("value", (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val()));//税额合计
                        $("#txtTotalFeeHo").attr("value",(parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val()));//含税金额合计
                        $("#txtDiscounth").attr("value",(parseFloat(item.Discount)).toFixed($("#HiddenPoint").val()));//整单折扣
                        $("#txtDiscountTotal").attr("value",(parseFloat(item.DiscountTotal)).toFixed($("#HiddenPoint").val()));//折扣金额
                        $("#txtRealTotal").attr("value",(parseFloat(item.RealTotal)).toFixed($("#HiddenPoint").val()));//折后含税金额
                        
                        //备注信息
                        $("#txtCreator").attr("value",item.CreatorID);//制单人id
                        $("#txtCreatorReal").attr("value",item.CreatorName);//制单人
                        $("#txtCreateDate").attr("value",item.CreateDate);//制单时间
                        $("#ddlBillStatus").attr("value",item.BillStatus);//单据状态
                        $("#txtConfirmor").attr("value",item.ConfirmorName);//确认人
                        $("#txtConfirmor").attr("title",item.Confirmor);//确认人
                        $("#txtConfirmDate").attr("value",item.ConfirmDate);//确认时间
                        $("#txtModifiedUserID").attr("value",item.ModifiedUserID);//最后更新人
                        $("#txtModifiedUserIDReal").attr("value",item.ModifiedUserID);//最后更新人id显示
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//最后更新时间
                        $("#txtCloser").attr("value",item.Closer);//结单人
                        $("#txtCloseDate").attr("value",item.CloseDate);//结单日期
                        $("#hfPageAttachment").attr("value",item.Attachment);//附件
                        $("#txtRemark").attr("value",item.Remark);//备注
                        
                        if(item.Attachment!="")
                        {
                            //下载删除显示
                            document.getElementById("divDealResume").style.display = "block";
                            //上传不显示
                            document.getElementById("divUploadResume").style.display = "none"
                        }
                        
                        document.getElementById("divPurchaseContractNo").innerHTML=item.ContractNo;
                        document.getElementById("divPurchaseContractNo").style.display="block";
                        document.getElementById("divInputNo").style.display="none";
                        
                        $("#FlowStatus").val(item.UsedStatus);
                        $("#Isyinyong").val(item.IsCite);
                        fnStatus($("#ddlBillStatus").val(), $("#Isyinyong").val());
                        var TableName = "officedba.PurchaseContract";
                        GetExtAttr(TableName, msg.data);
               });
               $.each(msg.data2,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                        var FromTypeName = document.getElementById("hidFromTypeName").value.Trim();
                        var Rate = document.getElementById("hidRate").value.Trim();
                        if(FromTypeName =="采购计划")
                        {
                            document.getElementById('txtProviderID').disabled=true;
                            document.getElementById('drpCurrencyType').disabled=true;
                        }
                        if(FromTypeName =="采购询价单")
                        {
                            document.getElementById('txtProviderID').disabled=true;
                            document.getElementById('drpCurrencyType').disabled=true;
                        }
                        var index = AddSignRow();
                        
                        $("#txtProductID"+index).attr("value",item.ProductID);
                        $("#txtProductNo"+index).attr("value",item.ProductNo);
                        $("#txtProductName" + index).attr("value", item.ProductName);

                        $("#DtlSColor" + index).attr("value", item.ColorName);
//                   alert (item.hiddProductCount);
                          $("#hiddProductCount"+index).attr("value", (parseFloat(item.hiddProductCount)).toFixed($("#HiddenPoint").val()));
                          
                        $("#txtstandard"+index).attr("value",item.standard);
                        $("#txtUnitID2"+index).attr("value",item.UnitID);
                        $("#txtUnitID"+index).attr("value",item.UnitName);  
                        
                        $("#txtProductCount"+index).attr("value",(parseFloat(item.ProductCount)).toFixed($("#HiddenPoint").val()));
                        $("#txtUnitPrice"+index).attr("value",(parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val()));
                        var unitPrice=(parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val());
                        $("#hiddUnitPrice"+index).attr("value", (parseFloat(item.UnitPrice*Rate)).toFixed($("#HiddenPoint").val()));
                        var taxPrice=(parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val());
                        
                        $("#txtTaxPrice"+index).attr("value", (parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val()));
                        $("#hiddTaxPrice"+index).attr("value",(parseFloat(item.TaxPrice*Rate)).toFixed($("#HiddenPoint").val()));
//                        $("#txtDiscount"+index).attr("value",item.Discount);
                        $("#txtTaxRate"+index).attr("value", (parseFloat(item.TaxRate)).toFixed($("#HiddenPoint").val()));
                        $("#hiddTaxRate"+index).attr("value",(parseFloat(item.HidTaxRate)).toFixed($("#HiddenPoint").val()));
                        $("#txtTotalPrice"+index).attr("value",(parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()));
                        $("#txtTotalFee"+index).attr("value",(parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val()));
                        $("#txtTotalTax"+index).attr("value", (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val()));
                        $("#txtRequireDate"+index).attr("value",item.RequireDate);
                        $("#txtApplyReason"+index).attr("value",item.ApplyReason);
                        $("#txtRemark"+index).attr("value",item.Remark);
                        $("#txtFromBillID"+index).attr("value",item.FromBillID);
                        $("#txtFromBillNo"+index).attr("value",item.FromBillNo);
                        $("#txtFromLineNo"+index).attr("value",item.FromLineNo);
                        $("#txtOrderCount"+index).attr("value",(parseFloat(item.OrderCount)).toFixed($("#HiddenPoint").val()));
                      
                        if($("#HiddenMoreUnit").val()=="True")
                          {    var issign="";
                          
                               if (item.FromBillNo!="")
                        {
                        issign="Bill";
                        }
                        if (item.UsedUnitCount=="")
                        {
                           $("#UsedUnitCount"+index).attr("value",(parseFloat("0")).toFixed($("#HiddenPoint").val()));
                        }
                        else
                        {
                           $("#UsedUnitCount"+index).attr("value",(parseFloat(item.UsedUnitCount)).toFixed($("#HiddenPoint").val()));
                        }
                        
                           if (item.UsedPrice=="")
                           {
                              $("#UsedPrice"+index).attr("value",  (parseFloat("0")).toFixed($("#HiddenPoint").val())); 
                           }
                           else
                           {
                              $("#UsedPrice"+index).attr("value",  (parseFloat(item.UsedPrice)).toFixed($("#HiddenPoint").val())); 
                           }
                        
                        
 
        GetUnitGroupSelectEx(item.ProductID,"InUnit","SignItem_TD_UnitID_Select" + index,"ChangeUnit(this.id,"+index+","+unitPrice+","+taxPrice+")","unitdiv" + index,"","FillSelect('"+index +"','"+item .UsedUnitID+"','"+issign+"')");//绑定单位组 
                         }    
                        
                        
                        
                        
                    }
               });
               if(msg.IsCite[0].IsCite == "True")
               {//如果被引用，保存按钮隐藏
                    $("#Isyinyong").val("True");
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline"); 
                    $("#btnGetGoods").css("display", "none");//条码扫描按钮
               }
               GetFlowButton_DisplayControl();
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
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


function ()
{

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
                    $("#txtProductCount"+i).val(tempcount);/*基本数量根据出库数量和比率算出*/ 
                }
            }
        }
    }
    
    
    
}
//新增合同
function InsertPurchaseContract() 
{ 
CountBaseNum();
if(!CheckBaseInfo())return;

        var action = null;

         var fieldText = "";
         var msgText = "";
         var isFlag = true;
         var contractNo = "";
         var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
         //基本信息
         //contractNo=document.getElementById("divPurchaseContractNo").innerHTML;
         if (CodeType == "")
         {
            contractNo=$("#CodingRuleControl1_txtCode").val();
         }
         var title=document.getElementById('txtTitle').value.Trim(); //合同主题
         var fromType=$("#ddlFromType").val(); //源单类型
         var providerID=$("#txtHidProviderID").val();//供应商
         var typeID=$("#DrpTypeID").val();//采购类别
         var seller=$("#txtHidOurUserID").val();//采购员弹出
         var deptid=$("#HidDeptID").val();//部门ID
         if(seller != "")
         {
             var index=seller.indexOf("_");
             seller=seller.substring(index+1);
         }
         else
         {
            seller="";
         }
        if(document.getElementById("txtAction").value.Trim()=="1")
        {
            action="Add";
        }
        else
        {
             action="Update";
        }
        var isaddtax="";
        if(document.getElementById("chkIsZzs").checked)
        {
            isaddtax=1;
        }
        else
        {
            isaddtax=0;
        }
         
         var signDate=$("#txtSignDate").val();//签约时间
         var oppositerUserID=$("#txtOppositerUserID").val();//供应商签约人
//         var ourUserID=$("#txtOurUserID").attr("title");
         var ourUserID=$("#txtHidOurUserID1").val();//我方签约人 选择窗口
         if(ourUserID == null)
         {
            ourUserID=0
         }
         var address=$("#txtAddress").val(); //签约地点
         var takeType=$("#DrpTakeType").val();//交货方式
         var carryType=$("#DrpCarryType").val();//运送方式
         var payeType=$("#DrpPayType").val();//结算方式
         var moneyType=$("#DrpMoneyType").val();//支付方式
         var currencyType = $("#CurrencyTypeID").val();//币种取隐藏域的值
         var rate = $("#txtRate").val();//汇率
         
         //合计信息
         var totalmoney = document.getElementById('txtTotalMoney').value.Trim();//采购金额合计
         var counttotal = document.getElementById('txtCountTotal').value.Trim();//采购数量合计
         var totaltax = document.getElementById('txtTotalTaxHo').value.Trim();//税额合计
         var totalfee = document.getElementById('txtTotalFeeHo').value.Trim();//含税金额合计合计
         var discounth = document.getElementById('txtDiscounth').value.Trim();//整单折扣(%）合计
         var discounttotal = document.getElementById('txtDiscountTotal').value.Trim();//折扣金额合计
         var realtotal = document.getElementById('txtRealTotal').value.Trim();//折后含税金额合计
 
         
         //备注信息
         var creator=$("#txtCreator").val(); //制单人
         var createDate=$("#txtCreateDate").val(); //制单时间
         var fflag2 = "";
         if(document.getElementById("ddlBillStatus").value.Trim() =="2")
        {
            document.getElementById("ddlBillStatus").value="1";
            fflag2 = 1;
        }
        else
        {
            fflag2 = 2;
        }
         var billStatus=$("#ddlBillStatus").val();//单据状态
         var confirmor=$("#txtConfirmor").val();//确认人
         var confirmDate=$("#txtConfirmDate").val();//确认时间
         var modifiedUserID=$("#txtModifiedUserID").val();//最后更新人
         var closer=$("#txtCloser").val();//结单人
         var closeDate=$("#txtCloseDate").val();//结单日期
         var modifiedDate=$("#txtModifiedDate").val();//最后更新日期
//         var attachment=$("#hfPageAttachment").val();//附件
         var attachment = document.getElementById("hfPageAttachment").value.replace(/\\/g,"\\\\");//附件  
         var remark=$("#txtRemark").val(); //备注
         
         var dtlSInfo = "";         
         dtlSInfo =  DtlS_Item.join("|");
         var dtlInfo = "";  
         dtlInfo = Dtl_Item2.join("|");
     
        
        var DetailID = new Array();
        var Detailchk = new Array();
        var DetailProductID = new Array();
        var DetailProductNo = new Array();
        var DetailProductName = new Array();
        var DetailStandard = new Array();
        var DetailUnitID = new Array();
        var DetailProductCount = new Array();
        var DetailUnitPrice = new Array();
        var DetailTaxPrice = new Array();
        var DetailDiscount = new Array();
        var DetailTaxRate = new Array();
        var DetailTotalPrice = new Array();
        var DetailTotalFee = new Array();
        var DetailTotalTax = new Array();
        var DetailRequireDate = new Array();
        var DetailApplyReason = new Array();
        var DetailRemark = new Array();
        var DetailFromBillID = new Array();
        var DetailFromBillNo = new Array();
        var DetailFromLineNo = new Array();
    
     var DetailUsedUnitID = new Array();
        var DetailUsedUnitCount = new Array();
        var DetailUsedPrice = new Array();
        var DetailExRate= new Array();
        
        var length = 0;
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
        
        var signFrame = findObj("dg_Log",document); 
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            
            if(signFrame.rows[i+1].style.display!="none")
            {
                
                var objID = 'ID'+(i+1);
                var objchk = 'chk'+(i+1);
                var objProductID = 'txtProductID'+(i+1);
                var objProductNo = 'txtProductNo'+(i+1);
                var objProductName = 'txtProductName'+(i+1);
                var objStandard = 'txtstandard'+(i+1);
                var objUnitID = 'txtUnitID2'+(i+1);
                var objProductCount = 'txtProductCount'+(i+1);
                var objUnitPrice = 'txtUnitPrice'+(i+1);
                var objTaxPrice = 'txtTaxPrice'+(i+1);
                var objDiscount = 'txtDiscount'+(i+1);
                var objTaxRate = 'txtTaxRate'+(i+1);
                var objTotalPrice = 'txtTotalPrice'+(i+1);
                var objTotalFee = 'txtTotalFee'+(i+1);
                var objTotalTax = 'txtTotalTax'+(i+1);
                var objRequireDate = 'txtRequireDate'+(i+1);
                var objApplyReason = 'txtApplyReason'+(i+1);
                var objRemark = 'txtRemark'+(i+1);
                var objFromBillID = 'txtFromBillID'+(i+1);
                var objFromBillNo = 'txtFromBillNo'+(i+1);
                var objFromLineNo = 'txtFromLineNo'+(i+1);
                
                //DetailID.push(document.getElementById(objID.toString()).value);
                Detailchk.push(document.getElementById(objchk).value.Trim());
                DetailProductID.push(document.getElementById(objProductID.toString()).value.Trim());
                DetailProductNo.push(document.getElementById(objProductNo.toString()).value.Trim());
                DetailProductName.push(document.getElementById(objProductName.toString()).value.Trim());
                DetailStandard.push(document.getElementById(objStandard.toString()).value.Trim());
                DetailUnitID.push(document.getElementById(objUnitID.toString()).value.Trim());
                DetailProductCount.push(document.getElementById(objProductCount.toString()).value.Trim());
                DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value.Trim());
                DetailTaxPrice.push(document.getElementById(objTaxPrice.toString()).value.Trim());
//                DetailDiscount.push(document.getElementById(objDiscount.toString()).value.Trim());
                DetailTaxRate.push(document.getElementById(objTaxRate.toString()).value.Trim());
                DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value.Trim());
                DetailTotalFee.push(document.getElementById(objTotalFee.toString()).value.Trim());
                DetailTotalTax.push(document.getElementById(objTotalTax.toString()).value.Trim());
                DetailRequireDate.push(document.getElementById(objRequireDate.toString()).value.Trim());
                DetailApplyReason.push(document.getElementById(objApplyReason.toString()).value.Trim());
                DetailRemark.push(document.getElementById(objRemark.toString()).value.Trim());
                DetailFromBillID.push(document.getElementById(objFromBillID.toString()).value.Trim());
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
                
                  if($("#HiddenMoreUnit").val()=="True")
                 {
                     var ExRate= $("#SignItem_TD_UnitID_Select"+(i+1)).val().split('|')[1].toString();
                   var UsedUnitID= $("#SignItem_TD_UnitID_Select"+(i+1)).val().split('|')[0].toString();
                 DetailUsedUnitID .push (UsedUnitID);
                 DetailUsedUnitCount .push ($("#UsedUnitCount"+(i+1)+"").val());
                 DetailUsedPrice .push ($("#UsedPrice"+(i+1)+"").val());
                 DetailExRate.push (ExRate);
                 
                 }
                
                
                
                
                
                length++;
                
            }
        }
//        if(length == 0)
//        {
//            length = undefined;
//        }
         if(contractNo.length<=0)
         {
            isFlag = false;
            fieldText = fieldText + "合同编号|";
   		    msgText = msgText +  "合同编号不允许为空|";
         }
         
         if(title.length<=0)
         {
            isFlag = false;
            fieldText = fieldText + "合同主题|";
   		    msgText = msgText +  "合同主题不允许为空|";
         }
         var no=  document.getElementById("divPurchaseContractNo").innerHTML
         var txtIndentityID = $("#txtIndentityID").val();

         
 

    $.ajax({ 
              type: "POST", 
              url: "../../../Handler/Office/PurchaseManager/PurchaseContractAdd.ashx",
              dataType:'json',//返回json格式数据
              data: "contractNo="+escape(contractNo)+"&title="+escape(title)+"&fromType="+escape(fromType)+"&providerID="+escape(providerID)+"&typeID="+escape(typeID)+" &seller="+escape(seller)+"&signDate="+escape(signDate)+"&oppositerUserID="+escape(oppositerUserID)+"&ourUserID="+escape(ourUserID)+"&address="+escape(address)+"&takeType="+escape(takeType)+"&carryType="+escape(carryType)+"&payeType="+escape(payeType)+"&moneyType="+escape(moneyType)+"&creator="+escape(creator)+"&createDate="+escape(createDate)+"&billStatus="+escape(billStatus)+"&confirmor="+escape(confirmor)+"&confirmDate="+escape(confirmDate)+"&modifiedUserID="+escape(modifiedUserID)+"&closer="+escape(closer)+"&closeDate="+escape(closeDate)+"&modifiedDate="+escape(modifiedDate)+"&attachment="+escape(attachment)+"&remark="+escape(remark)+"&dtlSInfo="+escape(dtlSInfo)+"&currencyType="+escape(currencyType)+"&rate="+escape(rate)+"&dtlInfo="+escape(dtlInfo)+"&action="+escape(action)+"&DetailID="+escape(DetailID)+"&DetailProductID="+escape(DetailProductID)+"&DetailProductNo="+escape(DetailProductNo)+"&DetailProductName="+escape(DetailProductName)+"&DetailStandard="+escape(DetailStandard)+"&DetailUnitID="+escape(DetailUnitID)+"&DetailProductCount="+escape(DetailProductCount)+"&DetailUnitPrice="+escape(DetailUnitPrice)+"&DetailTaxPrice="+escape(DetailTaxPrice)+"&DetailDiscount="+escape(DetailDiscount)+"&DetailTaxRate="+escape(DetailTaxRate)+"&DetailTotalPrice="+escape(DetailTotalPrice)+"&DetailTotalFee="+escape(DetailTotalFee)+"&DetailTotalTax="+escape(DetailTotalTax)+"&DetailRequireDate="+escape(DetailRequireDate)+"&DetailApplyReason="+escape(DetailApplyReason)+"&totalmoney="+escape(totalmoney)+"&counttotal="+escape(counttotal)+"&totaltax="+escape(totaltax)+"&totalfee="+escape(totalfee)+"&discounth="+escape(discounth)+"&discounttotal="+escape(discounttotal)+"&realtotal="+escape(realtotal)+"&DetailRemark=" + escape(DetailRemark) + "&DetailFromBillID=" + escape(DetailFromBillID) + "&DetailFromBillNo=" + escape(DetailFromBillNo) + "&DetailUsedUnitID =" + escape(DetailUsedUnitID )+ "&DetailUsedUnitCount=" + escape(DetailUsedUnitCount ) + "&DetailUsedPrice =" + escape(DetailUsedPrice ) + "&DetailExRate=" + escape(DetailExRate) +"&DetailFromLineNo="+escape (DetailFromLineNo)+ "&length=" + escape(length) + "&deptid=" + escape(deptid) + "&isaddtax=" + escape(isaddtax) + "&CodeType=" + escape(CodeType) + "&ID=" + escape(txtIndentityID) + "&fflag2=" + escape(fflag2) + "&cno=" + no + GetExtAttrValue(), //数据
            
              cache:false,
              beforeSend:function()
              { 
                  //AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() {
            
              
            }, 
            success:function(data) 
            {
                  if (data.sta==0)
            {
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.info);
            }
               else  if(data.sta>0) 
                { 
                    $("#CodingRuleControl1_txtCode").val(data.data);
                    $("#CodingRuleControl1_txtCode").attr("readonly","readonly");
                    $("#CodingRuleControl1_ddlCodeRule").attr("disabled","false");
                    
                    if(action=="Add")
                    {
                        popMsgObj.ShowMsg(data.info);
                        document.getElementById('txtIndentityID').value = data.sta;
                        
                        
                        document.getElementById("txtAction").value="2";
                       if(CodeType!="")
                       {
                            isnew="edit";
                            document.getElementById("divPurchaseContractNo").innerHTML=data.data; 
                            document.getElementById("divPurchaseContractNo").style.display="block";
                            document.getElementById("divInputNo").style.display="none";

                            document.getElementById("CodingRuleControl1_txtCode").value = data.data;
                            
                            //设置源单类型不可改
                            $("#ddlFromType").attr("disabled","disabled");
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val());
                            GetFlowButton_DisplayControl();//审批处理
                       }
                       else
                       {
                           document.getElementById("divPurchaseContractNo").innerHTML=contractNo;
                           document.getElementById("divPurchaseContractNo").style.display="block";
                           document.getElementById("divInputNo").style.display="none";
                           
                            document.getElementById("CodingRuleControl1_txtCode").value = data.data;    
                            
                            //设置源单类型不可改
                            $("#ddlFromType").attr("disabled","disabled");    
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());  
                            fnFlowStatus($("#FlowStatus").val());   
                            GetFlowButton_DisplayControl();//审批处理          
                       }
                     
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                        document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                        document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                        document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                        fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());  
                        fnFlowStatus($("#FlowStatus").val()); 
                        GetFlowButton_DisplayControl();//审批处理  
                    }
                  
                         
                    } 
                else 
                { 
                  hidePopup();
                  popMsgObj.ShowMsg(data.info);
                } 
                
                
            } 
           });  


     
} 

function Fun_Clear_Input()
{
action="Add";
    window.location='PurchaseContract_Add.aspx'; 
}

//全选
function SelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

function Isquanxuan()
{
    var signFrame = findObj("dg_Log",document);
    var quanxuan = true;
    for(var i=1;i<signFrame.rows.length;++i)
        {
            if(document.getElementById("chk"+i).checked == false)
            {
                quanxuan = false;
            }
        }
    if(quanxuan)
    {
        document.getElementById("checkall").checked = true;
    }
    else
    {
        document.getElementById("checkall").checked = false;
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
  
                          var Index = AddSignRow();  
                        $("#txtProductID"+Index).attr("value",item.ID);
                        $("#txtProductNo"+Index).attr("value",item.ProdNo);
                        $("#txtProductName"+Index).attr("value",item.ProductName);
                        $("#txtUnitID2"+Index).attr("value",item.UnitID);
                        $("#txtUnitID"+Index).attr("value",item.CodeName); 
                        $("#txtstandard"+Index).attr("value",item.Specification);

                        $("#DtlSColor" + Index).attr("value", item.ColorName); //进项税率
                        
                        $("#txtUnitPrice"+Index).attr("value", (parseFloat(item.TaxBuy)).toFixed($("#HiddenPoint").val()));//去税进价
                         $("#txtTaxPrice"+Index).attr("value", (parseFloat(item.StandardBuy)).toFixed($("#HiddenPoint").val()));//含税进价 txtTotalFee
                         $("#txtTaxRate"+Index).attr("value", (parseFloat(item.InTaxRate)).toFixed($("#HiddenPoint").val()));//进项税率
                              
                         $("#hiddUnitPrice"+Index).attr("value",(parseFloat(item.TaxBuy)).toFixed($("#HiddenPoint").val()));
                         $("#hiddTaxPrice"+Index).attr("value", (parseFloat(item.StandardBuy)).toFixed($("#HiddenPoint").val()));
                         $("#hiddTaxRate"+Index).attr("value",(parseFloat(item.InTaxRate)).toFixed($("#HiddenPoint").val()));
                         if($("#HiddenMoreUnit").val()=="True")
                          {
        GetUnitGroupSelectEx(item.ID,"InUnit","SignItem_TD_UnitID_Select" + Index,"ChangeUnit(this.id,"+Index+","+item.TaxBuy+","+item.StandardBuy+")","unitdiv" + Index,'',"FillContent("+Index+","+item.TaxBuy+","+item.StandardBuy+")");//绑定单位组
        
                         }   
                         
                         
                         
                        }
                   });
                     
                      },
             
               complete:function(){}//接收数据完毕
           });
      closeProductdiv();
}  
function FillContent(rowID,UnitPrice,hanshuiPrice)
{ 

 var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
           $("#UsedPrice"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
           $("#UsedPricHid"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
             $("#txtTaxPrice"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#hiddTaxPrice"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              var planCount = $("#UsedUnitCount"+rowID).val();/*采购数量*/

           if (planCount !='')
           {
           var tempcount=parseFloat(planCount*EXRate).toFixed($("#HiddenPoint").val());
           var totalPrice=parseFloat(planCount*UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
           var taxRate=$("#txtTaxRate"+rowID).val();
           var TotalFee=parseFloat(planCount*hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
             var TotalTax=parseFloat(totalPrice*taxRate /100).toFixed($("#HiddenPoint").val());
            $("#txtTotalTax"+rowID).attr("value", (parseFloat(TotalTax)).toFixed($("#HiddenPoint").val()));
            $("#txtTotalFee"+rowID).attr("value", (parseFloat(TotalFee)).toFixed($("#HiddenPoint").val()));
            $("#txtTotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
           
          $("#txtProductCount"+rowID).attr("value", (parseFloat(tempcount)).toFixed($("#HiddenPoint").val()));
          
           }
       
           fnTotalInfo();
}

//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,RowID/*行号*/,UsedPrice/*基本单价*/,hanshuiPrice)
 {
 
    var EXRate = $("#SignItem_TD_UnitID_Select"+RowID).val().split('|')[1].toString(); /*比率*/
  
           $("#UsedPrice"+RowID).attr("value", (parseFloat(UsedPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
     
           $("#UsedPricHid"+RowID).attr("value", (parseFloat(UsedPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
             $("#txtTaxPrice"+RowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#hiddTaxPrice"+RowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));

    var OutCount = $("#UsedUnitCount"+RowID).val();/*采购数量*/
    
    if (OutCount != '')
    {
        var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val());
        var tempprice=parseFloat(UsedPrice*EXRate).toFixed($("#HiddenPoint").val());
           var totalPrice=parseFloat(OutCount*tempprice).toFixed($("#HiddenPoint").val());
           var TaxPrice =parseFloat(hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
           $("#txtTaxPrice"+RowID).val(TaxPrice);/*含税价根据原始含税价和比率算出*/
           $("#hiddTaxPrice"+RowID).val(TaxPrice);
            
        $("#txtProductCount"+RowID).val(tempcount);/*基本数量根据计划数量和比率算出*/
        $("#UsedPrice"+RowID).val(tempprice);/*实际单价根据基本单价和比率算出*/
             $("# txtTotalPrice"+RowID).val(tempprice);/*实际总价根据实际单价和实际数量算出*/
            
    
    }
    fnTotalInfo();
 }
function IsExistCheck(prodNo)
{
    var sign=false ;
    var signFrame = findObj("dg_Log",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
          var prodNo1 = document.getElementById("txtProductNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}
///添加行
 function AddSignRow(flag)
 { 
        //读取最后一行的行号，存放在txtTRLastIndex文本框中 
 
        var txtTRLastIndex = findObj("txtTRLastIndex",document);
    
        var rowID = parseInt(txtTRLastIndex.value.Trim());
       // alert("rowID="+rowID)
        
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
       // alert("signFrame.rows.length"+signFrame.rows.length)
        newTR.id = "ID" + rowID;
        var i=0;
              
        var newNameXH=newTR.insertCell(i++);//添加列:选择
        newNameXH.className="tdColInputCenter";
        newNameXH.innerHTML="<input name='chk' id='chk" + rowID + "' size='10' value="+rowID+" type='checkbox'  onclick=\"Isquanxuan();\"  />";
        
        var newNameTD=newTR.insertCell(i++);//添加列:序号
        newNameTD.className="cell";
//        newNameTD.style.display = "none"; 
        newNameTD.id = "newNameTD"+rowID;
//        newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
        newNameTD.innerHTML =GenerateNo(rowID);
        
        var newProductNo=newTR.insertCell(i++);//添加列:物品ID
        newProductNo.style.display = "none";      
        newProductNo.innerHTML = "<input name='txtProductID" + rowID + "'  id='txtProductID" + rowID + "' type='text' style='width:100%'  />";//添加列内容

        var SetFunNo = "SetEventFocus('txtProductNo','txtProductCount'," + rowID + ",false);";
        if ($("#HiddenMoreUnit").val() == 'True') {
            SetFunNo = "SetEventFocus('txtProductNo','UsedUnitCount'," + rowID + ",false);";
        }
        
        
        
        if (flag =="Bill")
        {
           var newProductNo=newTR.insertCell(i++);//添加列:物品编号
        newProductNo.className="tdColInputCenter";
        newProductNo.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                                "<tr><td><input id=\"txtProductNo" + rowID + "\"    onkeydown=\"" + SetFunNo + "\"  class=\"tdinput\" size=\"7\" type=\"text\" readonly> </td>" +
                                "<td align=\"right\"><img style=\"cursor: hand\"   align=\"absMiddle\" src=\"../../../Images/search.gif\" height=\"21\"></td>" +
                             "</tr></table>"; //添加列内容


        
        }
        else
        {
        var newProductNo=newTR.insertCell(i++);//添加列:物品编号
        newProductNo.className="tdColInputCenter";
        newProductNo.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                                "<tr><td><input id=\"txtProductNo" + rowID + "\" onblur=\"SetMatchProduct(" + rowID + ",this.value);\"  onkeydown=\"" + SetFunNo + "\"  class=\"tdinput\" size=\"7\" type=\"text\"></td>" +
                                "<td align=\"right\"><img style=\"cursor: hand\" onclick=\"popTechObj.ShowList('" + rowID + "');\" align=\"absMiddle\" src=\"../../../Images/search.gif\" height=\"21\"></td>" +
                             "</tr></table>"; //添加列内容
    }

   
        
        
        
        
        var newProductName=newTR.insertCell(i++);//添加列:物品名称
        newProductName.className="tdColInputCenter";
        newProductName.innerHTML = "<input name='txtProductName" + rowID + "' id='txtProductName" + rowID + "' type='text'  style='width:95%;border:0px'  readonly disabled/>";//添加列内容
        
        var newProductName=newTR.insertCell(i++);//添加列:规格
        newProductName.className="tdColInputCenter";
        newProductName.innerHTML = "<input name='txtstandard" + rowID + "' id='txtstandard" + rowID + "' type='text'  style='width:95%;border:0px'  readonly disabled/>";//添加列内容

        var newColorName = newTR.insertCell(i++); //添加列:颜色
        newColorName.className = "tdColInputCenter";
        newColorName.innerHTML = "<input name='DtlSColor" + rowID + "' id='DtlSColor" + rowID + "' type='text'  style='width:95%;border:0px'  readonly disabled/>"; //添加列内容   
         
         
         
         
           if($("#HiddenMoreUnit").val()=="False")
        {
        var newUnitID=newTR.insertCell(i++);//添加列:单位ID
        newUnitID.style.display = "none";
        newUnitID.innerHTML = "<input name='txtUnitID2"+rowID+"' id='txtUnitID2" + rowID + "'style='width:10%;border:0px' type='text'  class='tdinput' readonly />";//添加列内容
        
        var newUnitID=newTR.insertCell(i++);//添加列:单位
        newUnitID.className="tdColInputCenter";
        newUnitID.innerHTML = "<input name='txtUnitID" + rowID + "' id='txtUnitID" + rowID + "' type='text' style='width:95%;border:0px' onclick=\"popUnitObj.ShowList("+rowID+");\"  readonly disabled/>";//添加列内容
        }
        else
        {
        document .getElementById ("spUnitID").innerText="基本单位"; 
    document .getElementById ("spUsedUnitID").style.display="block";
    
         var newUnitID=newTR.insertCell(i++);//添加列:单位ID
        newUnitID.style.display = "none";
        newUnitID.innerHTML = "<input name='txtUnitID2"+rowID+"' id='txtUnitID2" + rowID + "'style='width:10%;border:0px' type='text'  class='tdinput' readonly />";//添加列内容
        
        var newUnitID=newTR.insertCell(i++);//添加列:单位
         newUnitID.className="cell";
        newUnitID.innerHTML = "<input name='txtUnitID" + rowID + "' id='txtUnitID" + rowID + "' type='text' style='width:95%;border:0px' onclick=\"popUnitObj.ShowList("+rowID+");\"  readonly disabled/>";//添加列内容
    
         var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
    
        }
        
        
         if($("#HiddenMoreUnit").val()=="False")
        { 
        var newProductCount=newTR.insertCell(i++);//添加列:采购数量
        newProductCount.className="tdColInputCenter";
        newProductCount.innerHTML = "<input name='txtProductCount" + rowID + "'  id='txtProductCount" + rowID + "'  onchange=\"Number_round(this," + $("#HiddenPoint").val() + ");\"   onblur=\"fnTotalInfo();\" style='width:95%;border:0px' type='text'  onkeydown=\"SetEventFocus('txtProductCount','txtProductNo'," + rowID + ",true);\"/> <input type=\"hidden\"  id='hiddProductCount" + rowID + "'/>"; //添加列内容
        }
        else
        {   
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block";
         var newProductCount=newTR.insertCell(i++);//添加列:基本数量
        newProductCount.className="tdColInputCenter";
        newProductCount.innerHTML = "<input name='txtProductCount" + rowID + "'  id='txtProductCount" + rowID + "'  onchange=\"Number_round(this," + $("#HiddenPoint").val() + "); \"   onblur=\"fnTotalInfo();\" style='width:95%;border:0px' type='text' readonly='readonly'/> <input type=\"hidden\"  id='hiddProductCount" + rowID + "'/>";//添加列内容
    
        var newUsedUnitCount=newTR.insertCell(i++);//添加列:采购数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='UsedUnitCount" + rowID + "' type='text' class=\"tdinput\"  value=''  style='width:90%;' onkeydown=\"SetEventFocus('UsedUnitCount','txtProductNo'," + rowID + ",true);\"  onblur=\"Number_round(this," + $("#HiddenPoint").val() + "); fnTotalInfo();\"  />"; 
        
        }
            if($("#HiddenMoreUnit").val()=="False")
        {
        var newUnitPrice=newTR.insertCell(i++);//添加列:单价
        newUnitPrice.className="tdColInputCenter";
        newUnitPrice.innerHTML = "<input name='txtUnitPrice" + rowID + "' id='txtUnitPrice" + rowID + "' type='text'   onchange=\"Number_round(this," + $("#HiddenPoint").val() + "); \"    onblur=\"fnTotalInfo();\"  style='width:95%;border:0px' type='text'/> <input type=\"hidden\"  id='hiddUnitPrice" + rowID + "'/>";//添加列内容
        }
        else
        {
        document .getElementById ("spUnitPrice").style.display="none";
document .getElementById ("spUsedPrice").style.display="block";
  var newUnitPrice=newTR.insertCell(i++);//添加列:基本单价
        newUnitPrice.style.display = "none";
        newUnitPrice.innerHTML = "<input name='txtUnitPrice" + rowID + "' id='txtUnitPrice" + rowID + "' type='text'   onchange=\"Number_round(this," + $("#HiddenPoint").val() + "); \"    onblur=\"fnTotalInfo();\"  style='width:95%;border:0px' type='text'/> <input type=\"hidden\"  id='hiddUnitPrice" + rowID + "'/>";//添加列内容
       
        var newUsedPrice=newTR.insertCell(i++);//添加列:实际单价
    newUsedPrice.className="cell"; 
    newUsedPrice.innerHTML = "<input id='UsedPrice" + rowID + "'type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnTotalInfo();\"   style='width:80%;'   />";
     var newUsedPricHid=newTR.insertCell(i++);//添加列:实际单价
    newUsedPricHid.style.display = "none";
    newUsedPricHid.innerHTML = "<input id='UsedPricHid" + rowID + "'type='text' class=\"tdinput\"   style='width:80%;'   />";
        }
        var newUnitPrice=newTR.insertCell(i++);//添加列:含税价
        newUnitPrice.className="tdColInputCenter";
        newUnitPrice.innerHTML = "<input name='txtTaxPrice" + rowID + "' id='txtTaxPrice" + rowID + "' type='text'   onchange=\"Number_round(this," + $("#HiddenPoint").val() + "); \"   onblur=\"fnTotalInfo();\"  style='width:95%;border:0px' type='text' /> <input type=\"hidden\"  id='hiddTaxPrice" + rowID + "'/>";//添加列内容
        
//        var newUnitPrice=newTR.insertCell(i++);//添加列:折扣
//        newUnitPrice.className="tdColInputCenter";
//        newUnitPrice.innerHTML = "<input name='txtDiscount" + rowID + "' id='txtDiscount" + rowID + "' type='text'   style='width:95%;border:0px'     />";//添加列内容
        
        var newUnitPrice=newTR.insertCell(i++);//添加列:税率
        newUnitPrice.className="tdColInputCenter";
        newUnitPrice.innerHTML = "<input name='txtTaxRate" + rowID + "' id='txtTaxRate" + rowID + "' type='text'   onchange=\"Number_round(this," + $("#HiddenPoint").val() + "); \"   onblur=\"fnTotalInfo();\"  style='width:95%;border:0px' type='text' /> <input type=\"hidden\"  id='hiddTaxRate" + rowID + "'/>";//添加列内容
        
        var newTotalPrice=newTR.insertCell(i++);//添加列:金额
        newTotalPrice.className="tdColInputCenter";
        newTotalPrice.innerHTML = "<input name='txtTotalPrice" + rowID + "' id='txtTotalPrice" + rowID + "' type='text'  style='width:95%;border:0px'   readonly disabled/>";//添加列内容
        $("#txtTotalPrice" + rowID).onfocus = "TotalPrice1();"
        
        var newUnitPrice=newTR.insertCell(i++);//添加列:含税金额
        newUnitPrice.className="tdColInputCenter";
        newUnitPrice.innerHTML = "<input name='txtTotalFee" + rowID + "' id='txtTotalFee" + rowID + "' type='text'   style='width:95%;border:0px'    readonly  disabled/>";//添加列内容
        
        var newUnitPrice=newTR.insertCell(i++);//添加列:税额
        newUnitPrice.className="tdColInputCenter";
        newUnitPrice.innerHTML = "<input name='txtTotalTax" + rowID + "' id='txtTotalTax" + rowID + "' type='text'   style='width:95%;border:0px'    readonly disabled/>";//添加列内容
        
        var newRequireDate=newTR.insertCell(i++);//添加列:交货日期
        newRequireDate.className="tdColInputCenter";
        newRequireDate.innerHTML = "<input name='txtRequireDate" + rowID + "' id='txtRequireDate" + rowID + "' type='text' style='width:95%;border:0px' readonly  onclick=\"J.calendar.get();\" />";//添加列内容
//        $("#txtRequireDate" + rowID).click(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#txtRequireDate" + rowID)})}); 
        
//        var newTotalPrice=newTR.insertCell(i++);//添加列:申请原因ID
//        newTotalPrice.style.display = "none";
//        newTotalPrice.innerHTML = "<input name='txtApplyReasonID" + rowID + "' id='txtApplyReasonID" + rowID + "' type='text'  style='width:100%'   readonly />";//添加列内容    
        
        var newTotalPrice=newTR.insertCell(i++);//添加列:申请原因
        newTotalPrice.className="tdColInputCenter";
//        newTotalPrice.innerHTML = "<input name='txtApplyReason" + rowID + "' id='txtApplyReason" + rowID + "' type='text' style='width:100%'/>"+ document.getElementById("drpApplyReason").innerHTML + "</select>";//添加列内容 
        newTotalPrice.innerHTML="<select  style='width:95%' class='tdinput' id='txtApplyReason" + rowID + "'>" + document.getElementById("drpApplyReason").innerHTML + "</select>";    //添加列内容  

        var newRemark=newTR.insertCell(i++);//添加列:备注
        newRemark.className = "tdColInputCenter";
        newRemark.innerHTML = "<input name='txtRemark" + rowID + "' id='txtRemark" + rowID + "' type='text' style='width:95%;border:0px'  />";//添加列内容

        var newFromBillID=newTR.insertCell(i++);//添加列:源单ID
        newFromBillID.style.display = "none";
        newFromBillID.innerHTML = "<input name='txtFromBillID" + rowID + "' id='txtFromBillID" + rowID + "' type='text' style='width:95%;border:0px'   readonly disabled/>";//添加列内容
        
        var newFromBillID=newTR.insertCell(i++);//添加列:源单编号
        newFromBillID.className = "tdColInputCenter";
        newFromBillID.innerHTML = "<input name='txtFromBillNo" + rowID + "' id='txtFromBillNo" + rowID + "' type='text'  style='width:95%;border:0px'  readonly />";//添加列内容
        
        var newFromLineNo=newTR.insertCell(i++);//添加列:源单序号
        newFromLineNo.className="tdColInputCenter";
        newFromLineNo.innerHTML = "<input name='txtFromLineNo" + rowID + "' id='txtFromLineNo" + rowID + "' type='text' style='width:95%;border:0px'  readonly disabled/>";//添加列内容
        
        var newFromLineNo=newTR.insertCell(i++);//添加列:已订购数量(被订单回写)
        newFromLineNo.className="tdColInputCenter";
        newFromLineNo.innerHTML = "<input name='txtOrderCount" + rowID + "' id='txtOrderCount" + rowID + "' type='text' style='width:95%;border:0px'  readonly disabled/>";//添加列内容
        
        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
        return rowID;
}

function GenerateNo(Edge)
{//生成序号
    DtlSCnt = findObj("txtTRLastIndex",document);//明细来源新增行号
    var signFrame = findObj("dg_Log",document);
    var SortNo = 1;//起始行号
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<Edge;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                document.getElementById("newNameTD"+i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0;//明细表不存在时返回0
}

function DeleteSignRowContract()
{//删除明细行，需要将序号重新生成
    var signFrame = findObj("dg_Log",document);        
    var ck = document.getElementsByName("chk");
    for( var i = 0; i<ck.length;i++ )
    {
        var rowID = i+1;
        if ( ck[i].checked )
        {
           signFrame.rows[rowID].style.display="none";
        }
        document.getElementById("newNameTD"+rowID).innerHTML = GenerateNo(rowID);
    }
    var Flag = document.getElementById("ddlFromType").value.Trim();
    
    //判断是否有明细了，若没有了，则将供应商设为可选
    var totalSort = 0;//明细行数
    for( var i = 0; i<ck.length;i++ )
    {
        var rowID = i+1;
        if (signFrame.rows[rowID].style.display!="none")
        {
           totalSort++;
           break;
        }
    }
    if(totalSort !=0)
    {//明细行数大于0
    }
    else
    {//无明细，供应商可选
//        $("#txtProviderName").removeAttr("disabled");
        document.getElementById("txtProviderID").style.display="block";
        document.getElementById("txtHiddenProviderID1").style.display="none";
        document.getElementById("checkall").checked = false;
        
        document.getElementById('txtProviderID').disabled=false;
        document.getElementById('drpCurrencyType').disabled=false;
    }
    
    fnTotalInfo();
}

//计算各种合计信息
function fnTotalInfo() 
{
   
try
{

    var CountTotal = 0; //数量合计
    var TotalPrice = 0; //金额合计
    var Tax = 0; //税额合计
    var TotalFee = 0; //含税金额合计
    var Discount = $("#txtDiscounth").val(); //整单折扣
    var DiscountTotal = 0; //折扣金额
    var RealTotal = 0; //折后含税金额
    var TotalDyfzk = $("#txtTotalDyfzk").val();; //抵应付账款
//    var TotalYthkhj = 0; //应退货款合计
    var Zhekoumingxi = 0;//明细中折扣合计
    
    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        {
     
            var rowid = i;
            var pCountDetail =0;
              if($("#HiddenMoreUnit").val()=="True")
             {
                   pCountDetail=($("#UsedUnitCount" + rowid).val()=="")?0:$("#UsedUnitCount" + rowid).val(); //实际数量
               var EXRate = $("#SignItem_TD_UnitID_Select"+rowid).val().split('|')[1].toString(); /*比率*/
                   $("#txtProductCount" + rowid).attr("value",(parseFloat (pCountDetail *EXRate).toFixed($("#HiddenPoint").val())));     
                   
            }
            else
            {
            pCountDetail= $("#txtProductCount" + rowid).val(); //数量
            }
            
            if(pCountDetail == "")
            {
            
                pCountDetail=(parseFloat(0)).toFixed($("#HiddenPoint").val());
            }


            if(IsNumberOrNumeric(pCountDetail,14,$("#HiddenPoint").val()) == false)
            {
                alert("【采购数量】格式不正确！");
                return;
            }
          
            
//            var ssdasdsadsad = document.getElementById("txtProductCount" + rowid).value =   (parseFloat(pCountDetail)).toFixed($("#HiddenPoint").val());
            var UnitPriceDetail =0;
              if($("#HiddenMoreUnit").val()=="True")
               {
                   UnitPriceDetail=    ($("#UsedPrice" + rowid).val()=="")?0:$("#UsedPrice" + rowid).val(); //单价
               }
               else
               {
               UnitPriceDetail= $("#txtUnitPrice" + rowid).val(); //单价验证  此值不用了 
               }
            
            if(UnitPriceDetail == "")
            {
                UnitPriceDetail = (parseFloat(0)).toFixed($("#HiddenPoint").val());
            }
            if(IsNumberOrNumeric(UnitPriceDetail,14,$("#HiddenPoint").val()) == false)
            {
                alert("【单价】格式不正确！");
                return;
            }
            //判断是否是增值税，不是增值税含税价始终等于单价
            if(!document.getElementById('chkIsZzs').checked)
            {
                $("#txtTaxPrice" + rowid).val($("#txtUnitPrice" + rowid).val());
            }
            var TaxPriceDetail = $("#txtTaxPrice" + rowid).val(); //含税价验证
            if(TaxPriceDetail == "")
            {
              
                TaxPriceDetail = (parseFloat(0)).toFixed($("#HiddenPoint").val());
            }
            if(IsNumberOrNumeric(TaxPriceDetail,22,$("#HiddenPoint").val()) == false)
            {
                alert("【含税价】格式不正确！");
                return;
            }
//            var DiscountDetail = $("#txtDiscount" + rowid).val(); //折扣
            var TaxRateDetail = $("#txtTaxRate" + rowid).val(); //税率验证
            if(TaxRateDetail == "")
            {
                TaxRateDetail =(parseFloat(0)).toFixed($("#HiddenPoint").val());
            }
            if(IsNumberOrNumeric(TaxRateDetail,12,$("#HiddenPoint").val()) == false)
            {
                alert("【税率】格式不正确！");
                return;
            }
          
            var TotalPriceDetail =  (parseFloat(UnitPriceDetail * pCountDetail)).toFixed($("#HiddenPoint").val()); //金额=数量*单价*折扣
//            var TotalTaxDetail = FormatAfterDotNumber((TotalFeeDetail * TaxRateDetail / 100), 2); //税额
            var TotalTaxDetail = (parseFloat(TotalPriceDetail * TaxRateDetail / 100)).toFixed($("#HiddenPoint").val()); //税额=金额 *税率
//            var TotalFeeDetail = FormatAfterDotNumber((TaxPriceDetail * pCountDetail * DiscountDetail / 100), 2); //含税金额
//            var TotalFeeDetail = FormatAfterDotNumber((TotalPriceDetail + TotalTaxDetail), 2); //含税金额=金额+税额 //修改
            var TotalFeeDetail = (parseFloat( TaxPriceDetail * pCountDetail)).toFixed($("#HiddenPoint").val()); //含税金额=数量*含税单价*折扣
            
           
            $("#txtTotalPrice" + rowid).val(  (parseFloat( TotalPriceDetail)).toFixed($("#HiddenPoint").val())); //金额
            $("#txtTotalTax" + rowid).val(  (parseFloat( TotalTaxDetail)).toFixed($("#HiddenPoint").val())); //税额
             $("#txtTotalFee" + rowid).val( (parseFloat( TotalFeeDetail)).toFixed($("#HiddenPoint").val())); //含税金额
            TotalPrice += parseFloat(TotalPriceDetail);//金额
            Tax += parseFloat(TotalTaxDetail);//税额
            TotalFee += parseFloat(TotalFeeDetail);//含税金额
            CountTotal += parseFloat(pCountDetail);//数量合计 
            Zhekoumingxi +=  0;//明细折扣金额=含税价*数量*（1-折扣）
        }
    }
    
    
    $("#txtTotalMoney").val((parseFloat( TotalPrice)).toFixed($("#HiddenPoint").val()));//金额合计
    $("#txtTotalTaxHo").val( (parseFloat( Tax)).toFixed($("#HiddenPoint").val()));//税额合计
    $("#txtTotalFeeHo").val( (parseFloat( TotalFee)).toFixed($("#HiddenPoint").val()));//含税金额合计
    $("#txtCountTotal").val( (parseFloat( CountTotal)).toFixed($("#HiddenPoint").val())); //数量合计
    $("#txtDiscountTotal").val( (parseFloat((TotalFee * (100 - Discount) / 100) + Zhekoumingxi)).toFixed($("#HiddenPoint").val())); //折扣金额
    $("#txtRealTotal").val(  (parseFloat( (TotalFee * Discount / 100))).toFixed($("#HiddenPoint").val())); //折后含税金额 
    }
    catch ( Error )
    {
    }
}



function fnChangeAddTax()
{//改变是否为增值税
    var isAddTax = document.getElementById("chkIsZzs").checked;
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
    
    if(isAddTax == true)
    {
        document.getElementById("chkisAddTaxText1").style.display="inline";
        document.getElementById("chkisAddTaxText2").style.display="none";
    }
    else
    {
        document.getElementById("chkisAddTaxText1").style.display="none";
        document.getElementById("chkisAddTaxText2").style.display="inline";
    }
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
//            var rowIndex = signFrame.rows[i];
            var rowIndex = i;
            if(isAddTax == true)
            {//是增值税则
//                document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("hiddTaxPrice"+rowIndex).value;//含税价等于隐藏域含税价
                document.getElementById("txtTaxPrice"+rowIndex).value=Numb_roundPur(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,2);//含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();//税率等于隐藏域税率
                
            }
            else
            {
//                document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("txtUnitPrice"+rowIndex).value;//含税价等于单价
                document.getElementById("txtTaxPrice"+rowIndex).value=Numb_roundPur(document.getElementById("txtUnitPrice"+rowIndex).value.Trim()/Rate,2);//含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                
            }
        }
    }
    fnTotalInfo();
}


//获取明细信息
function getDtlSValue()
{
arrlength=0;
 var signFrame = document.getElementById("dg_Log");
 if((typeof(signFrame)!="undefined")&&(signFrame!=null))
 {
    for(i=1;i<signFrame.rows.length;i++)
    {         
      if(signFrame.rows[i].style.display!="none")
       {
       alert("i="+i);
       var temp = new Array();
        signFrame.rows[i].cells[0].innerHTML = i.toString();
        rowid =parseInt(signFrame.rows[i].cells[0].innerText);
        temp.push(document.getElementById("chk" + rowid).value.Trim());//0
        temp.push(document.getElementById("txtProductNo" + rowid).value.Trim());//
        temp.push(document.getElementById("txtProductName" + rowid).value.Trim());//2
        temp.push(document.getElementById("txtProductCount" + rowid).value.Trim());//3
        temp.push(document.getElementById("txtUnitID" + rowid).value.Trim());//4
        temp.push(document.getElementById("txtRequireDate" + rowid).value.Trim());//5
        temp.push(document.getElementById("txtUnitPrice"+rowid).value.Trim());//6
        temp.push(document.getElementById("txtTotalPrice" + rowid).value.Trim());//7
        temp.push(document.getElementById("txtTotalPrice" + rowid).value.Trim());//8
        temp.push(document.getElementById("txtApplyReason" + rowid).value.Trim());//9需求日期
        temp.push(document.getElementById("txtRemark" + rowid).value.Trim());//10
        temp.push(document.getElementById("txtFromBillID" + rowid).value.Trim());//11
        temp.push(document.getElementById("txtFromLineNo" + rowid).value.Trim());//12
        DtlS_Item[i-1] = temp;  
        arrlength++;
      }
      showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","已缓存配件信息，如需保存请提交！");
   }
   }
}
function FillProvider(providerid,providerno,providername)
{ 
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHiddProviderID").value = providerid;
    closeProviderdiv();
}

/*
* 附件处理
*/
function DealResume(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传简历
    else if ("upload" == flag)
    {
        ShowUploadFile();
    }
    //清除简历
    else if ("clear" == flag)
    {
            //设置简历路径
            document.getElementById("hfPageAttachment").value = "";
            //下载删除不显示
            document.getElementById("divDealResume").style.display = "none";
            //上传显示 
            document.getElementById("divUploadResume").style.display = "block";
    }
    //下载简历
    else if ("download" == flag)
    {
     //获取简历路径
            resumeUrl = document.getElementById("hfPageAttachment").value.Trim();
            window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + resumeUrl, "_blank");
    }
}

function AfterUploadFile(url)
{
    if (url != "")
    {
       // alert(url);
//        //设置简历路径
//        document.getElementById("hfPageResume").value = url;
        //下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        //上传不显示
        document.getElementById("divUploadResume").style.display = "none";
        
        
        //设置简历路径
        document.getElementById("hfPageAttachment").value = url;
    }
}


function ChangeCurreny()
{//选择币种带出汇率
    var IDExchangeRate = document.getElementById("drpCurrencyType").value.Trim();
    document.getElementById("CurrencyTypeID").value = IDExchangeRate.split('_')[0];
    document.getElementById("txtRate").value = IDExchangeRate.split('_')[1];
    
    var isAddTax = document.getElementById("chkIsZzs").checked;
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
    
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true)
            {//是增值税则
                document.getElementById("txtTaxPrice"+rowIndex).value=Numb_roundPur(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,2);//含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();//税率等于隐藏域税率
                document.getElementById("chkisAddTaxText1").style.display="inline";
                document.getElementById("chkisAddTaxText2").style.display="none";
            }
            else
            {
                document.getElementById("txtTaxPrice"+rowIndex).value=Numb_roundPur(document.getElementById("txtUnitPrice"+rowIndex).value.Trim()/Rate,2);//含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                document.getElementById("chkisAddTaxText1").style.display="none";
                document.getElementById("chkisAddTaxText2").style.display="inline";
            }
        }
    }
    
    fnTotalInfo();
}

function DeleteAll()
{
 
     var Flag= document.getElementById("ddlFromType").value.Trim();
     if(Flag == 0 || Flag == 1)
     {
         DeleteSignRow100();
         fnTotalInfo();
         document.getElementById("txtProviderID").style.display="block";
         document.getElementById("txtHiddenProviderID1").style.display="none";
         document.getElementById('txtProviderID').disabled=false;
        document.getElementById('drpCurrencyType').disabled=false;
     }
     else
     {   DeleteSignRow100();
         fnTotalInfo();
        document.getElementById('txtProviderID').disabled=false;
        document.getElementById('drpCurrencyType').disabled=false;
      
         
     }
}


function DeleteSignRow100()
{
    var signFrame = document.getElementById("dg_Log");  
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=signFrame.rows.length-1;i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    findObj("txtTRLastIndex",document).value = 1;
}






//验证

/*
* 基本信息校验
*/
function CheckBaseInfo()
{
    
     var fieldText = "";
      var msgText = "";
      var isFlag = true;   
      
      //先检验页面上的特殊字符
//      var RetVal=CheckSpecialWords();
//      if(RetVal!="")
//      {
//          isFlag = false;
//          fieldText = fieldText + RetVal+"|";
//          msgText = msgText +RetVal+  "不能含有特殊字符|";
//      }
      var ssw = CheckSpecialWords();
      if (ssw != "") {
          isFlag = false;
          var tmpKeys = ssw.split(',');
          for (var i = 0; i < tmpKeys.length; i++) {
              isFlag = false;
              fieldText = fieldText + tmpKeys[i].toString() + "|";
              msgText = msgText + "不能含有特殊字符|";
          }
      }


    //新建时，编号选择手工输入时
    if (document.getElementById("txtAction").value.Trim()=="1")
    {
//        获取编码规则下拉列表选中项
        codeRule = document.getElementById("CodingRuleControl1_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            employeeNo = document.getElementById("CodingRuleControl1_txtCode").value.Trim();
            //编号必须输入
            if (employeeNo == "")
            {
                isFlag = false;
                fieldText += "合同编号|";
   		        msgText += "请输入合同编号|";
            }
            else
            {
                if(!CodeCheck($.trim($("#CodingRuleControl1_txtCode").val())))
                {
                    isFlag = false;
                    fieldText = fieldText + "单据编号|";
   		            msgText = msgText +  "单据编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
                }
                else if(strlen($.trim($("#CodingRuleControl1_txtCode").val())) > 50)
                {
                    isErrorFlag = true;
                    fieldText += "单据编号|";
   		            msgText += "单据编号长度不大于50|";
   		        }
   		        
            }
        }    
    }         

    //整单折扣验证是否为数字
    if(document.getElementById("txtDiscounth").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtDiscounth").value.Trim(),12,$("#HiddenPoint").val()) == false)
        {
            isFlag = false;
            fieldText += "整单折扣|";
            msgText += "请输入正确的整单折扣|";
        }
    }
    if(document.getElementById("txtTotalMoney").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtTotalMoney").value.Trim(),22,$("#HiddenPoint").val()) == false)
        {
            isFlag = false;
            fieldText += "金额合计|";
            msgText += "请输入正确的金额合计|";
        }
    }
    if(document.getElementById("txtTotalTaxHo").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtTotalTaxHo").value.Trim(),22,$("#HiddenPoint").val()) == false)
        {
            isFlag = false;
            fieldText += "税额合计|";
            msgText += "请输入正确的税额合计|";
        }
    }
    if(document.getElementById("txtTotalFeeHo").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtTotalFeeHo").value.Trim(),22,$("#HiddenPoint").val()) == false)
        {
            isFlag = false;
            fieldText += "含税金额合计|";
            msgText += "请输入正确的含税金额合计|";
        }
    }
    if(document.getElementById("txtDiscountTotal").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtDiscountTotal").value.Trim(),22,$("#HiddenPoint").val()) == false)
        {
            isFlag = false;
            fieldText += "折扣金额合计|";
            msgText += "请输入正确的折扣金额合计|";
        }
    }
    if(document.getElementById("txtRealTotal").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtRealTotal").value.Trim(),22,$("#HiddenPoint").val()) == false)
        {
            isFlag = false;
            fieldText += "折后含税金额|";
            msgText += "请输入正确的折后含税金额|";
        }
    }
    if(document.getElementById("txtTitle").value.Trim() == "")
    {
//        isFlag = false;
//        fieldText += "合同主题|";
//        msgText += "请输入合同主题|";
    }
    else
    {
        if(strlen(document.getElementById("txtTitle").value.Trim())>100)
        {
            isFlag = false;
            fieldText +=  "合同主题|";
   		    msgText +=  "合同主题仅限于100个字符以内|";      
        }
    }
    if(document.getElementById("txtProviderID").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "供应商|";
        msgText += "请输入供应商|";
    }
    if(document.getElementById("UsertxtSeller").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "采购员|";
        msgText += "请输入采购员|";
    }
    if(document.getElementById("txtSignDate").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "签约时间|";
        msgText += "请输入签约时间|";
    }
    
    //限制字数
    var Remark=document.getElementById("txtRemark").value.Trim();//备注
    if(strlen(Remark)>200)
    {
        isFlag = false;
        fieldText += "备注|";
   		msgText +=  "备注仅限于200个字符以内|";      
    }
 

    //明细来源的校验
     
  //  var signFrame = document.getElementById("dg_Log");
   var signFrame = findObj("dg_Log",document);
   var RealCount=0;
    if((typeof(signFrame) == "undefined")||signFrame==null)
    {
        isFlag = false;
        fieldText +="明细来源|";
        msgText +="明细来源不能为空|";
    }
    else
    {
      
         for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
            RealCount++;
                var ProductCount = "txtProductCount"+i;
               var fromProductCount="hiddProductCount"+i;
             var   OrderCount='txtOrderCount'+i;
                var no = document.getElementById(ProductCount).value.Trim();//需要采购的数量
                var  fp=document.getElementById(fromProductCount).value.Trim();//原单数量
                var oc=document.getElementById(OrderCount).value.Trim();//已订购数量 
                var remarks = "txtRemark"+i;
                var Remarks = document.getElementById(remarks).value.Trim();
                var ProductNo1 = "txtProductNo"+i;
                var ProductNo2 = document.getElementById(ProductNo1).value.Trim();
                if(IsNumeric(no,14,$("#HiddenPoint").val()) == false)
                {
                    isFlag = false;
                    fieldText +="采购数量|";
                    msgText +="请输入正确的物品编号为"+ProductNo2+"的采购数量|";
                    break ;
                }
                else
                {
                if (no>0)
                {}
                else
                {
                      isFlag = false;
                    fieldText +="采购数量|";
                    msgText +=" 物品编号为"+ProductNo2+"的采购数量需大于零|";
                
                break ;
                
                }
                }
                var UnitPrice = "txtUnitPrice" + i;
                var no1 = document.getElementById(UnitPrice).value.Trim();
                if(IsNumeric(no1,14,$("#HiddenPoint").val()) == false)
                {
                    isFlag = false;
                    fieldText +="单价|";
                    msgText +="请输入正确的物品编号为"+ProductNo2+"的单价数量";
                    break ;
                }
                var TotalPrice = "txtTotalPrice" + i;
                var no2 = document.getElementById(TotalPrice).value.Trim();
                if(IsNumeric(no2,22,$("#HiddenPoint").val()) == false)
                {
                    isFlag = false;
                    fieldText +="金额小计|";
                    msgText +="请输入正确的物品编号为"+ProductNo2+"的金额小计数量";
                    break ;
                }
                var TaxPrice = "txtTaxPrice" + i;
                var no3 = document.getElementById(TaxPrice).value.Trim();
                if(IsNumeric(no3,22,$("#HiddenPoint").val()) == false)
                {
                    isFlag = false;
                    fieldText +="含税价|";
                    msgText +="请输入正确的物品编号为"+ProductNo2+"的含税价数量";
                    break ;
                }
                var TaxRate = "txtTaxRate" + i;
                var no4 = document.getElementById(TaxRate).value.Trim();
                if(IsNumeric(no4,12,$("#HiddenPoint").val()) == false)
                {
                    isFlag = false;
                    fieldText +="税率|";
                    msgText +="请输入正确的物品编号为"+ProductNo2+"的税率数量";
                    break ;
                }
                if(strlen(Remarks)>200)
                {
                    isFlag = false;
                    fieldText += "备注|";
   		            msgText +=  "物品编号为"+ProductNo2+"的备注仅限于200个字符以内|";    
   		            break ;  
                }
               
                var signType=document .getElementById ("ddlFromType").value;//判断是何种类型
                if (signType=="2")
                {
          
                  
                var temp= (parseFloat(fp)).toFixed($("#HiddenPoint").val())- (parseFloat(oc)).toFixed($("#HiddenPoint").val());
                if (temp <     (parseFloat(no)).toFixed($("#HiddenPoint").val()))
                {
                   isFlag = false;
                    fieldText += "采购数量|";
   		            msgText +=  "物品编号为"+ProductNo2+"的采购数量大于可采购数量："+temp +"|";    
   		            break ;  
                }
                }
                
                
            }
        }
    }
    
   if(RealCount == 0)
        {
             isFlag = false;
        fieldText +="明细来源|";
        msgText +="明细来源不能为空|";
        }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}


function PurchasePlanSelect001()
{
    var Flag= document.getElementById("ddlFromType").value.Trim();
    
    if(Flag == 0)
    {//无来源
        document.getElementById("txtProviderID").style.display="none";
        document.getElementById("txtHiddenProviderID1").style.display="block";
    }
    else if(Flag==1)
    {//采购申请  
        var ProviderID = document.getElementById("txtHidProviderID").value.Trim();
        if(ProviderID =="")
        {
            ProviderID = 0;
        }
        popApplySelect.ShowList('',ProviderID);
    }
    else if(Flag==2)
    {//采购计划
        var ProviderID = document.getElementById("txtHidProviderID").value.Trim();
        var CurrencyTypeID = document.getElementById("CurrencyTypeID").value.Trim();
        var Rate = document.getElementById("txtRate").value.Trim();
        
        if(ProviderID =="")
        {
//            alert("请先选择供应商！");
//            return;
            ProviderID = 0;
        }
        popPlanSelectObj.ShowList('',ProviderID);
    }
    
    else if(Flag==3)
    {//采购询价单
        var ProviderID = document.getElementById("txtHidProviderID").value.Trim();
        var CurrencyTypeID = document.getElementById("CurrencyTypeID").value.Trim();
        var Rate = document.getElementById("txtRate").value.Trim();
        var signFrame = findObj("dg_Log",document);        
        var ck = document.getElementsByName("chk");
        var totalSort = 0;//明细行数
        for( var i = 0; i<ck.length;i++ )
        {
            var rowID = i+1;
            if (signFrame.rows[rowID].style.display!="none")
            {
               totalSort++;
               break;
            }
        }
        if(totalSort == 0)
        {
            Rate = 0;
        }
        if(ProviderID =="")
        {
//            alert("请先选择供应商！");
//            return;
            ProviderID = 0;
        }
        popAskPrice.ShowList('',ProviderID,CurrencyTypeID,Rate);
    }
}
function jsSelectIsExitItem(objSelectID, objItemValue) {    
     var objSelect=document .getElementById (objSelectID)    ;
    var isExit = false;        
    for (var i = 0; i < objSelect.options.length; i++) {        
        if (objSelect.options[i].value == objItemValue) {        
            isExit = true;        
            break;        
        }        
    }        
    return isExit;        
} 

function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{  
   
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    document.getElementById("txtHiddenProviderID1").value = providername;//当选择源单类型时，用于显示供应商并且不允许再修改
     if (taketype==""|!jsSelectIsExitItem('DrpTakeType',taketype))
    {
    taketype="0";
    }
        if (carrytype==""|!jsSelectIsExitItem('DrpCarryType',carrytype))
    {
    carrytype="0";
    }
        if (paytype==""|!jsSelectIsExitItem('DrpPayType',paytype))
    {
    paytype="0";
    }
    document.getElementById("DrpTakeType").value = taketype;
    document.getElementById("DrpCarryType").value = carrytype;
    document.getElementById("DrpPayType").value= paytype;
    
    closeProviderdiv();
}


function GetValuePPurApp()
{
    var ck = document.getElementsByName("ChkBoxPPurApp");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
           
          if($("#HiddenMoreUnit").val()=="True")
          {
              FillFromApply( $("#PPurAppProductID"+i).html(), $("#PPurAppProductNo"+i).html(), $("#PurAppProductName"+i).html(), $("#PPurAppstandard"+i).html(), $("#PPurAppUnitID"+i).html(), 
            $("#PPurAppUnit"+i).html(), $("#PPurAppProductCount"+i).html(), $("#PPurAppUnitPrice"+i).html(), $("#PPurAppTaxPrice"+i).html(),100.00, 
            $("#PPurAppTaxRate"+i).html(), $("#PPurAppTotalPrice"+i).html(), $("#PPurAppRequireDate"+i).html(), $("#PPurAppApplyReasonID"+i).html(), $("#PPurAppApplyReason"+i).html(), 
            $("#PPurAppFromBillID"+i).html(), $("#PPurAppTypeID"+i).html(), $("#PPurAppSeller"+i).html(), $("#PPurAppSellerName"+i).html(), $("#PPurAppDeptID"+i).html(),
            $("#PPurAppDeptIDName" + i).html(), $("#PPurAppFromBillNo" + i).html(), $("#PPurAppFromLineNo" + i).html(), $("#PPurAppUsedUnitID" + i).html(), $("#PPurAppUsedUnitCount" + i).html(), $("#PPurAppColorName" + i).html());
          }
          else
          {
            FillFromApply( $("#PPurAppProductID"+i).html(), $("#PPurAppProductNo"+i).html(), $("#PurAppProductName"+i).html(), $("#PPurAppstandard"+i).html(), $("#PPurAppUnitID"+i).html(), 
            $("#PPurAppUnit"+i).html(), $("#PPurAppProductCount"+i).html(), $("#PPurAppUnitPrice"+i).html(), $("#PPurAppTaxPrice"+i).html(),100.00, 
            $("#PPurAppTaxRate"+i).html(), $("#PPurAppTotalPrice"+i).html(), $("#PPurAppRequireDate"+i).html(), $("#PPurAppApplyReasonID"+i).html(), $("#PPurAppApplyReason"+i).html(), 
            $("#PPurAppFromBillID"+i).html(), $("#PPurAppTypeID"+i).html(), $("#PPurAppSeller"+i).html(), $("#PPurAppSellerName"+i).html(), $("#PPurAppDeptID"+i).html(),
            $("#PPurAppDeptIDName" + i).html(), $("#PPurAppFromBillNo" + i).html(), $("#PPurAppFromLineNo" + i).html(), $("#PPurAppColorName" + i).html());
            }
        }
    }  
}




function FillFromApply(productid, productno, productname, standard, unitid, unit, productcount, unitprice, taxprice, discount, taxrate, totalprice, requiredate, applyreasonid, applyreason, frombillid, typeid, seller, sellername, deptid, deptidname, frombillno, fromLineno, UsedUnitID, UsedUnitCount, ColorName)
{
    
    if(!ExistFromBill(productid,fromLineno))
        {
            var Index = findObj("txtTRLastIndex",document).value;
            AddSignRow("Bill");
            var ProductID = 'txtProductID'+Index;
            var ProductNo = 'txtProductNo'+Index;
            var ProductName = 'txtProductName'+Index;
            var Standard = 'txtstandard'+Index;
            var UnitID = 'txtUnitID2'+Index;
            var Unit = 'txtUnitID'+Index;
            var ProductCount = 'txtProductCount'+Index;
            var UnitPrice = 'txtUnitPrice'+Index;
            var TaxPrice = 'txtTaxPrice'+Index;
            var Discount = 'txtDiscount'+Index;
            var TaxRate = 'txtTaxRate'+Index;
            var TotalPrice = 'txtTotalPrice'+Index;
            var RequireDate = 'txtRequireDate'+Index;
            var ApplyReason = 'txtApplyReason'+Index;
            var FromBillID = 'txtFromBillID'+Index;
            var FromBillNo = 'txtFromBillNo'+Index;
            var FromLineNo = 'txtFromLineNo'+Index;
            var HiddTaxPrice = 'hiddTaxPrice'+Index;
            var HiddTaxRate = 'hiddTaxRate'+Index;
            var hgColorName = 'DtlSColor' + Index;

            if ($("#HiddenMoreUnit").val() == "True") {
                document.getElementById(hgColorName).value = ColorName;

            }
            else {

                document.getElementById(hgColorName).value = UsedUnitID;
            }
            document.getElementById(ProductID).value = productid;
            document.getElementById(ProductNo).value = productno;
            document.getElementById(ProductName).value = productname;
            document.getElementById(Standard).value = standard;
            document.getElementById(UnitID).value = unitid;
            document.getElementById(Unit).value = unit;
              
            document.getElementById(ProductCount).value = (parseFloat(productcount)).toFixed($("#HiddenPoint").val());
            var ProductCount1= (parseFloat(productcount)).toFixed($("#HiddenPoint").val());
            document.getElementById(UnitPrice).value = (parseFloat(unitprice)).toFixed($("#HiddenPoint").val());
              var UnitPrice1=(parseFloat(unitprice)).toFixed($("#HiddenPoint").val());
            document.getElementById(TaxPrice).value = (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
            var taxPrice1=(parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
           // document.getElementById(Discount).value = discount;
            document.getElementById(TaxRate).value = (parseFloat(taxrate)).toFixed($("#HiddenPoint").val());
            document.getElementById(TotalPrice).value = (parseFloat(totalprice)).toFixed($("#HiddenPoint").val());
            document.getElementById(RequireDate).value = requiredate;
            document.getElementById(ApplyReason).value = applyreasonid;
            document.getElementById(FromBillID).value = frombillid;
            document.getElementById(FromBillNo).value = frombillno;
            document.getElementById(FromLineNo).value = fromLineno;
            document.getElementById(HiddTaxPrice).value =  (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
            document.getElementById(HiddTaxRate).value = (parseFloat(taxrate)).toFixed($("#HiddenPoint").val());
            
            document.getElementById("DrpTypeID").value = typeid;
            document.getElementById("txtHidOurUserID").value=seller;
            document.getElementById("UsertxtSeller").value=sellername;
            document.getElementById("HidDeptID").value=deptid;
            document.getElementById("DeptDeptID").value=deptidname;
             if($("#HiddenMoreUnit").val()=="False")
            {
                  
            }
            else
            {
            
             GetUnitGroupSelectEx( productid,"InUnit","SignItem_TD_UnitID_Select" + Index,"ChangeUnit(this.id,"+Index+","+UnitPrice1+","+taxPrice1+")","unitdiv" + Index,'',"FillApplyContent("+Index+","+UnitPrice1+","+taxPrice1+","+ProductCount1+","+UsedUnitCount +",'"+UsedUnitID +"','Bill')");//绑定单位组
            }
        }
        var isAddTax = document.getElementById("chkIsZzs").checked;
        var signFrame = findObj("dg_Log",document);
        for (i = 1; i < signFrame.rows.length; i++)
        {
            if (signFrame.rows[i].style.display != "none")
            {
                var rowIndex = i;
                if(isAddTax == true)
                {//是增值税则
                    document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("hiddTaxPrice"+rowIndex).value.Trim();//含税价等于隐藏域含税价
                    document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();//税率等于隐藏域税率
                    document.getElementById("chkisAddTaxText1").style.display="inline";
                    document.getElementById("chkisAddTaxText2").style.display="none";
                }
                else
                {
                    document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("txtUnitPrice"+rowIndex).value.Trim();//含税价等于单价
                    document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                    document.getElementById("chkisAddTaxText1").style.display="none";
                    document.getElementById("chkisAddTaxText2").style.display="inline";
                }
            }
        }
//        document.getElementById("txtProviderID").style.display="none";
//        document.getElementById("txtHiddenProviderID1").style.display="block";
        closeApplydiv();
         
}

function FillApplyContent(rowID,UnitPrice,hanshuiPrice,ProductCount,UsedUnitCount,UsedUnitID,sign)
{ 
 
if (UsedUnitID!='')
{
FillSelect(rowID,UsedUnitID,"Bill");
}

 var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
           $("#UsedPrice"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
           $("#UsedPricHid"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
             $("#txtTaxPrice"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#hiddTaxPrice"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              if (UsedUnitCount!='')
              {
                 $("#UsedUnitCount"+rowID).attr("value", (parseFloat(UsedUnitCount)).toFixed($("#HiddenPoint").val()));
              }
              else
              {
                  $("#UsedUnitCount"+rowID).attr("value", (parseFloat(ProductCount)/parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
                  }
              var planCount = $("#UsedUnitCount"+rowID).val();/*采购数量*/

           if (planCount !='')
           {
           
           var totalPrice=parseFloat(planCount*UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
           var taxRate=$("#txtTaxRate"+rowID).val();
           var TotalFee=parseFloat(planCount*hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
             var TotalTax=parseFloat(totalPrice*taxRate /100).toFixed($("#HiddenPoint").val());
            $("#txtTotalTax"+rowID).attr("value", (parseFloat(TotalTax)).toFixed($("#HiddenPoint").val()));
            $("#txtTotalFee"+rowID).attr("value", (parseFloat(TotalFee)).toFixed($("#HiddenPoint").val()));
            $("#txtTotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
           
    
          
           }
           fnTotalInfo();
}
///遍历选择的是否都是同一个供应商
function CheckIsOneProvider()
{
    var ck = document.getElementsByName("ChkBoxPPurPlan");
    var  getCheck=new Array ();
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
                  getCheck.push (  $("#PPurPlanProviderID"+i).html());
        }
    }  
    if (getCheck.length >1)
    {
         
          for (var a=0;a<getCheck.length;a++)
          {
          if (getCheck [a]==getCheck [0])
          {
          continue ;
          }
          else
          {
          return false;
          break ;
          }
          }
    }
   return true ;
}



 


function GetValuePPurPlan()
{ 
 if ( !CheckIsOneProvider())
   {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的明细只能来自一个供应商！");
        return ;
   }
    var ck = document.getElementsByName("ChkBoxPPurPlan");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
        
         if($("#HiddenMoreUnit").val()=="True") {


             FillFromPlan($("#PPurPlanColorName" + i).html(), $("#PPurPlanProductID" + i).html(), $("#PPurPlanProductNo" + i).html(), $("#PPurPlanProductName" + i).html(), $("#PPurPlanstandard" + i).html(), $("#PPurPlanUnitID" + i).html(), 
            $("#PPurPlanUnit"+i).html(), $("#PPurPlanProductCount"+i).html(), $("#PPurPlanUnitPrice"+i).html(), $("#PPurPlanTaxPrice"+i).html(), $("#PPurPlanDiscount"+i).html(), 
            $("#PPurPlanTaxRate"+i).html(), $("#PPurPlanTotalPrice"+i).html(), $("#PPurPlanRequireDate"+i).html(), $("#PPurPlanApplyReasonID"+i).html(), $("#PPurPlanApplyReason"+i).html(), 
            $("#PPurPlanFromBillID"+i).html(), $("#PPurPlanFromBillNo"+i).html(), $("#PPurPlanFromLineNo"+i).html(), $("#PPurPlanTypeID"+i).html(), $("#PPurPlanSeller"+i).html(), 
            $("#PPurPlanSellerName"+i).html(), $("#PPurPlanDeptID"+i).html(), $("#PPurPlanDeptIDName"+i).html(), $("#PPurPlanOrderCount"+i).html(), $("#PPurPlanProviderID"+i).html(), $("#PPurPlanProviderName"+i).html(), $("#PPurPlanUsedUnitID"+i).html(), $("#PPurPlanUsedUnitCount"+i).html(), $("#PPurPlanUsedPrice"+i).html() );
      
 }
 else
 {
     FillFromPlan($("#PPurPlanColorName" + i).html(), $("#PPurPlanProductID" + i).html(), $("#PPurPlanProductNo" + i).html(), $("#PPurPlanProductName" + i).html(), $("#PPurPlanstandard" + i).html(), $("#PPurPlanUnitID" + i).html(), 
            $("#PPurPlanUnit"+i).html(), $("#PPurPlanProductCount"+i).html(), $("#PPurPlanUnitPrice"+i).html(), $("#PPurPlanTaxPrice"+i).html(), $("#PPurPlanDiscount"+i).html(), 
            $("#PPurPlanTaxRate"+i).html(), $("#PPurPlanTotalPrice"+i).html(), $("#PPurPlanRequireDate"+i).html(), $("#PPurPlanApplyReasonID"+i).html(), $("#PPurPlanApplyReason"+i).html(), 
            $("#PPurPlanFromBillID"+i).html(), $("#PPurPlanFromBillNo"+i).html(), $("#PPurPlanFromLineNo"+i).html(), $("#PPurPlanTypeID"+i).html(), $("#PPurPlanSeller"+i).html(), 
            $("#PPurPlanSellerName"+i).html(), $("#PPurPlanDeptID"+i).html(), $("#PPurPlanDeptIDName"+i).html(), $("#PPurPlanOrderCount"+i).html(), $("#PPurPlanProviderID"+i).html(), $("#PPurPlanProviderName"+i).html() );
      
      }
      
        }
    }  
}




function FillFromPlan(ColorName,productid,productno,productname,standard,unitid,unit,productcount,unitprice,taxprice,discount,taxrate,totalprice,requiredate,applyreasonid,applyreason,frombillid,frombillno,fromLineno,typeid,seller,sellername,deptid,deptidname,ordercount,providerid,providername,UsedUnitID,UsedUnitCount,UsedPrice)
{
    if(!ExistFromBill(productid,fromLineno))
        {
            var Index = findObj("txtTRLastIndex",document).value;
            AddSignRow("Bill");
            var ProductID = 'txtProductID'+Index;
            var ProductNo = 'txtProductNo'+Index;
            var ProductName = 'txtProductName'+Index;
            var Standard = 'txtstandard'+Index;
            var UnitID = 'txtUnitID2'+Index;
            var Unit = 'txtUnitID'+Index;
            var ProductCount = 'txtProductCount'+Index;
            var UnitPrice = 'txtUnitPrice'+Index;
            var TaxPrice = 'txtTaxPrice'+Index;
            var Discount = 'txtDiscount'+Index;
            var TaxRate = 'txtTaxRate'+Index;
            var TotalPrice = 'txtTotalPrice'+Index;
            var RequireDate = 'txtRequireDate'+Index;
            var ApplyReason = 'txtApplyReason'+Index;
            var FromBillID = 'txtFromBillID'+Index;
            var FromBillNo = 'txtFromBillNo'+Index;
            var FromLineNo = 'txtFromLineNo'+Index;
            var HiddTaxPrice = 'hiddTaxPrice'+Index;
            var HiddTaxRate = 'hiddTaxRate'+Index;
            var HiddUnitPrice = 'hiddUnitPrice'+Index;
            var txtOrderCount='txtOrderCount'+Index 
             var hiddProductCount='hiddProductCount'+Index ;
             var hiddColorName = 'DtlSColor' + Index;

             document.getElementById(hiddColorName).value = ColorName;
            document.getElementById(ProductID).value = productid;
            document.getElementById(ProductNo).value = productno;
            document.getElementById(ProductName).value = productname;
            document.getElementById(Standard).value = standard;
            document.getElementById(UnitID).value = unitid;
            document.getElementById(Unit).value = unit;
       
               
            document.getElementById(ProductCount).value = (parseFloat(productcount)).toFixed($("#HiddenPoint").val());
            var ProductCount1=(parseFloat(productcount)).toFixed($("#HiddenPoint").val());
             document.getElementById(hiddProductCount).value = (parseFloat(productcount)).toFixed($("#HiddenPoint").val());
            document.getElementById(UnitPrice).value =(parseFloat(unitprice)).toFixed($("#HiddenPoint").val());
            var UnitPrice1=(parseFloat(unitprice)).toFixed($("#HiddenPoint").val());
            document.getElementById(TaxPrice).value = (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
            var taxPrice1=(parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
            
//            document.getElementById(Discount).value = "100.00";
            document.getElementById(TaxRate).value = (parseFloat(taxrate)).toFixed($("#HiddenPoint").val());
            document.getElementById(TotalPrice).value =   (parseFloat(totalprice)).toFixed($("#HiddenPoint").val());
            document.getElementById(RequireDate).value = requiredate;
            document.getElementById(ApplyReason).value = applyreasonid;
            document.getElementById(FromBillID).value = frombillid;
            document.getElementById(FromBillNo).value = frombillno;
            document.getElementById(FromLineNo).value = fromLineno;
            document.getElementById(HiddTaxPrice).value = (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
            document.getElementById(HiddTaxRate).value =   (parseFloat(taxrate)).toFixed($("#HiddenPoint").val());
            document.getElementById(HiddUnitPrice).value =   (parseFloat(unitprice)).toFixed($("#HiddenPoint").val());
            document.getElementById(txtOrderCount).value=(parseFloat(ordercount)).toFixed($("#HiddenPoint").val());
            
            document.getElementById("DrpTypeID").value = typeid;
            document.getElementById("txtHidOurUserID").value=seller;
            document.getElementById("UsertxtSeller").value=sellername;
            document.getElementById("HidDeptID").value=deptid;
            document.getElementById("DeptDeptID").value=deptidname;
            
            //带出供应商及币种等信息
            document.getElementById("txtHidProviderID").value = providerid;
            document.getElementById("txtProviderID").value = providername;
            document.getElementById("txtHiddenProviderID1").value = providername;
            document.getElementById('txtProviderID').disabled=true;
//            document.getElementById('drpCurrencyType').disabled=true;
              if($("#HiddenMoreUnit").val()=="False")
            {
                  
            }
            else
            {
            
             GetUnitGroupSelectEx( productid,"InUnit","SignItem_TD_UnitID_Select" + Index,"ChangeUnit(this.id,"+Index+","+UnitPrice1+","+taxPrice1+")","unitdiv" + Index,'',"FillApplyContent("+Index+","+UnitPrice1+","+taxPrice1+","+ProductCount1+",'"+UsedUnitCount +"','"+UsedUnitID+"','Bill')");//绑定单位组
            }
            }
            var isAddTax = document.getElementById("chkIsZzs").checked;
            //新加币种的汇率问题
            var Rate = document.getElementById("txtRate").value.Trim();
            
            var signFrame = findObj("dg_Log",document);
            for (i = 1; i < signFrame.rows.length; i++)
            {
                if (signFrame.rows[i].style.display != "none")
                {
                    var rowIndex = i;
                    if(isAddTax == true)
                    {//是增值税则
                        document.getElementById("txtTaxPrice"+rowIndex).value=Numb_roundPur(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,2);//含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                        document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//单价则改为隐藏域里的单价除以汇率
                        document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();//税率等于隐藏域税率
                        document.getElementById("chkisAddTaxText1").style.display="inline";
                        document.getElementById("chkisAddTaxText2").style.display="none";
                    }
                    else
                    {
                        document.getElementById("txtTaxPrice"+rowIndex).value=Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//含税价等于单价 再除以汇率(币种要求修改)
                        document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//单价则改为隐藏域里的单价除以汇率
                        document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                        document.getElementById("chkisAddTaxText1").style.display="none";
                        document.getElementById("chkisAddTaxText2").style.display="inline";
                    }
                }
            }
//        document.getElementById("txtProviderID").style.display="none";
//        document.getElementById("txtHiddenProviderID1").style.display="block";
        closePlandiv();
     
}


//判断是否有相同记录有返回true，没有返回false
function ContractExistFromAskPrice(frombillno,fromLineno)
{
    var signFrame = document.getElementById("dg_Log");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var frombillid = document.getElementById("txtFromBillNo"+i).value.Trim();
        var fromlineno = document.getElementById("txtFromLineNo"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(frombillid==frombillno)&&(fromlineno == fromLineno))
        {
            return true;
        } 
    }
    return false;
}
//判断是否有相同记录有返回true，没有返回false
function ExistFromBill(productid,fromLineno)
{
    var signFrame = document.getElementById("dg_Log");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var frombillid = document.getElementById("txtProductID"+i).value.Trim();
        var fromlineno = document.getElementById("txtFromLineNo"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(frombillid==productid)&&(fromlineno == fromLineno))
        {
            return true;
        } 
    }
    return false;
}

//写在用户控件中
//function FillFromAskPrice(productid,productno,productname,standard,unitid,unit,productcount,unitprice,taxprice,discount,taxrate,totalprice,totalfee,totaltax,
//                            requiredate,applyreasonid,applyreason,remark,frombillid,frombillno,fromLineno,seller,sellername,deptid,deptidname,isaddtax)
//{

//    if(!ExistFromBill(productid,fromLineno))
//        {
//            var Index = findObj("txtTRLastIndex",document).value;
//            AddSignRow();
//            var ProductID = 'txtProductID'+Index;
//            var ProductNo = 'txtProductNo'+Index;
//            var ProductName = 'txtProductName'+Index;
//            var Standard = 'txtstandard'+Index;
//            var UnitID = 'txtUnitID2'+Index;
//            var Unit = 'txtUnitID'+Index;
//            var ProductCount = 'txtProductCount'+Index;
//            var UnitPrice = 'txtUnitPrice'+Index;
//            var TaxPrice = 'txtTaxPrice'+Index;
//            var Discount = 'txtDiscount'+Index;
//            var TaxRate = 'txtTaxRate'+Index;
//            var TotalPrice = 'txtTotalPrice'+Index;
//            var TotalFee = 'txtTotalFee'+Index;
//            var TotalTax = 'txtTotalTax'+Index;
//            var RequireDate = 'txtRequireDate'+Index;
//            var ApplyReason = 'txtApplyReason'+Index;
//            var Remark = 'txtRemark'+Index;
//            var FromBillID = 'txtFromBillID'+Index;
//            var FromBillNo = 'txtFromBillNo'+Index;
//            var FromLineNo = 'txtFromLineNo'+Index;
//           
//            
//            document.getElementById(ProductID).value = productid;
//            document.getElementById(ProductNo).value = productno;
//            document.getElementById(ProductName).value = productname;
//            document.getElementById(Standard).value = standard;
//            document.getElementById(UnitID).value = unitid;
//            document.getElementById(Unit).value = unit;
//            document.getElementById(ProductCount).value = FormatAfterDotNumber(productcount,0);
//            document.getElementById(UnitPrice).value = unitprice;
//            document.getElementById(TaxPrice).value = taxprice;
//            document.getElementById(Discount).value = discount;
//            document.getElementById(TaxRate).value = taxrate;
//            document.getElementById(TotalPrice).value = totalprice;
//            document.getElementById(TotalFee).value = totalfee;
//            document.getElementById(TotalTax).value = totaltax;
//            document.getElementById(RequireDate).value = requiredate;
//            document.getElementById(ApplyReason).value = applyreasonid;
//            document.getElementById(Remark).value = remark;
//            document.getElementById(FromBillNo).value = frombillno;
//            document.getElementById(FromLineNo).value = fromLineno;
//            
//            document.getElementById("txtHidOurUserID").value=seller;
//            document.getElementById("UsertxtSeller").value=sellername;
//            document.getElementById("HidDeptID").value=deptid;
//            document.getElementById("DeptDeptID").value=deptidname;
//            document.getElementById("chkIsZzs").value=isaddtax;
//            
//            
//        }
//        document.getElementById("txtProviderID").style.display="none";
//        document.getElementById("txtHiddenProviderID1").style.display="block";
//        closeMaterialdiv();
//        fnTotalInfo();
//}
/*手工输入物品编号不存在时，清除行信息(之前输入一个存在的物品，之后再输入一个不存在的物品)*/
function ClearOneProductInfo(rows) {


    var ProductNo = 'txtProductNo' + rows;
    var ProductName = 'txtProductName' + rows;
    var UnitID = 'txtUnitID2' + rows;
    var Unit = 'txtUnitID' + rows;
    var Price = 'txtUnitPrice' + rows;
    var Standard = 'txtstandard' + rows;
    var ProductID = 'txtProductID' + rows;
    var TaxPrice = 'txtTaxPrice' + rows;
    var Discount = 'txtDiscount' + rows;
    var TaxRate = 'txtTaxRate' + rows;
    var HiddTaxPrice = 'hiddTaxPrice' + rows;
    var HiddTaxRate = 'hiddTaxRate' + rows;
    var HiddUnitPrice = 'hiddUnitPrice' + rows;


    document.getElementById(TaxRate).value = "";
    document.getElementById(TaxPrice).value = ""; 
    document.getElementById(Standard).value = "";
    document.getElementById(UnitID).value = "";
    document.getElementById(Unit).value = "";
    document.getElementById(ProductID).value = "";
    document.getElementById(ProductNo).value = "";
    document.getElementById(ProductName).value = "";
    document.getElementById(Price).value = "";
    document.getElementById(HiddTaxPrice).value = "";
    document.getElementById(HiddTaxRate).value = "";
    document.getElementById(HiddUnitPrice).value = "";
    document.getElementById('DtlSColor' + rows).value = '';



    if ($("#HiddenMoreUnit").val() == 'True') {
        document.getElementById('UsedPrice' + rows).value = '';
//        document.getElementById('UsedPriceHid' + rows).value = '';
        document.getElementById('unitdiv' + rows).innerHTML = '';
        document.getElementById('UsedUnitCount' + rows).value = '';
    }
    else {

//        document.getElementById('Hidden_TD_Text_UnitID_' + rows).value = '';
//        document.getElementById('TD_Text_UnitID_' + rows).value = '';
    }
}

 function Fun_FillParent_Content(id,no,productname,dddf,unitid,unit,df,sdfge,discount,standard,fg,fgf,taxprice,price,taxrate)
{
 
//    var temp = popTechObj.InputObj;
    //    var index = parseInt(temp.split('o')[2]);
    var index = popTechObj.InputObj;
    if (index == '') {
        return;
    }
    if (price == "") {
        price = 0;
    }
    if (taxprice == "") {
        taxprice = 0;
    }

    if (taxrate == "") {
        taxrate = 0;
    }
//    alert(tt);
//    return;
    var ProductNo = 'txtProductNo'+index;
    var ProductName='txtProductName'+index;
    var UnitID = 'txtUnitID2'+index;
    var Unit='txtUnitID'+index;
    var Price='txtUnitPrice'+index;
    var Standard='txtstandard'+index;
    var ProductID = 'txtProductID'+index;
    var TaxPrice = 'txtTaxPrice'+index;
    var Discount = 'txtDiscount'+index;
    var TaxRate = 'txtTaxRate'+index;
    var HiddTaxPrice = 'hiddTaxPrice'+index;
    var HiddTaxRate = 'hiddTaxRate'+index;
    var HiddUnitPrice = 'hiddUnitPrice'+index;
   
    document.getElementById(TaxRate).value =  (parseFloat(taxrate)).toFixed($("#HiddenPoint").val()) ;
//    document.getElementById(Discount).value = discount;
    document.getElementById(TaxPrice).value = (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
   var  taxPrice=(parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
    document.getElementById(Standard).value=standard;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(Unit).value=unit;
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    document.getElementById(Price).value =  (parseFloat(price)).toFixed($("#HiddenPoint").val());
  var  unitPrice= (parseFloat(price)).toFixed($("#HiddenPoint").val());
    document.getElementById(HiddTaxPrice).value =  (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
    document.getElementById(HiddTaxRate).value = (parseFloat(taxrate)).toFixed($("#HiddenPoint").val()) ;
            
            
    document.getElementById(HiddUnitPrice).value =  (parseFloat(price)).toFixed($("#HiddenPoint").val());
//    document.getElementById('divProviderInfo').style.display='none';
    
    //document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value/Rate,2);//单价则改为隐藏域里的单价除以汇率
      if($("#HiddenMoreUnit").val()=="False")
            {}
            else
            {
             GetUnitGroupSelectEx(id,"InUnit","SignItem_TD_UnitID_Select" + index,"ChangeUnit(this.id,"+index+","+unitPrice+","+taxPrice+")","unitdiv" + index,'',"FillContent("+index+","+unitPrice+","+taxPrice+")");//绑定单位组
            }
    var isAddTax = document.getElementById("chkIsZzs").checked;
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
            
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true)
            {//是增值税则
                document.getElementById("txtTaxPrice"+rowIndex).value=Numb_roundPur(document.getElementById("hiddTaxPrice"+rowIndex).value.Trim()/Rate,2);//含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//单价则改为隐藏域里的单价除以汇率
//                document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("hiddTaxPrice"+rowIndex).value;//含税价等于隐藏域含税价
                document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value.Trim();//税率等于隐藏域税率
                document.getElementById("chkisAddTaxText1").style.display="inline";
                document.getElementById("chkisAddTaxText2").style.display="none";
            }
            else
            {
                document.getElementById("txtTaxPrice"+rowIndex).value=Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice"+rowIndex).value = Numb_roundPur(document.getElementById("hiddUnitPrice"+rowIndex).value.Trim()/Rate,2);//单价则改为隐藏域里的单价除以汇率
//                document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("txtUnitPrice"+rowIndex).value;//含税价等于单价
                document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                document.getElementById("chkisAddTaxText1").style.display="none";
                document.getElementById("chkisAddTaxText2").style.display="inline";
            }
        }
    }
    
}


function FillUnit(unitid,unitname)
{
    var i = popUnitObj.InputObj;
    var UnitID = "txtUnitID2"+i;
    var UnitName = "txtUnitID"+i;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(UnitName).value = unitname;
}


//确认
function Fun_ConfirmOperate()
{
var c=window.confirm("确认执行确认操作吗？")
    if(c==true)
    {

    var ActionArrive = document.getElementById("txtAction").value.Trim()
        
        if(ActionArrive == "1")
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存再确认！");
            return;
        }
        
    //    document.getElementById("txtBillStatus").value = "执行";
   
        
        glb_BillID = document.getElementById('txtIndentityID').value;
        document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value.Trim();
        document.getElementById("txtConfirmor").value = document.getElementById("UserID").value.Trim();
        document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value.Trim();
        action ="Confirm";
        
        var DetailProductCount = new Array();//采购数量
	    var DetailFromBillNo = new Array();
	    var DetailFromLineNo = new Array();
	    var DetailUsedUnitCount = new Array();
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value.Trim();
        var length = 0;
        var signFrame = findObj("dg_Log",document); 
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objProductCount = 'txtProductCount'+(i+1);
                var objFromBillNo = 'txtFromBillNo'+(i+1);
                var objFromLineNo = 'txtFromLineNo' + (i + 1);
                var objUsedUnitCount = 'UsedUnitCount' + (i + 1);
                if ($("#HiddenMoreUnit").val() == "True") {

                    DetailUsedUnitCount.push(document.getElementById(objUsedUnitCount.toString()).value.Trim());
                }
                DetailProductCount.push(document.getElementById(objProductCount.toString()).value.Trim());
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
                length++;
            }
        }
        
        var confirmor = document.getElementById("txtConfirmor").value.Trim();
        var contractNo = document.getElementById("divPurchaseContractNo").innerHTML;
        var fromType = document.getElementById("ddlFromType").value.Trim();

        var strParams = "action=" + action + "&confirmor=" + confirmor + "&contractNo=" + contractNo + "&fromType=" + fromType + "&DetailProductCount=" + DetailProductCount + "&DetailFromBillNo=" + DetailFromBillNo + "&DetailFromLineNo=" + DetailFromLineNo + "&length=" + length + "&ID=" + glb_BillID + "&DetailUsedUnitCount=" + DetailUsedUnitCount;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseContractAdd.ashx?"+strParams,
               
            dataType:'json',//返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                      }, 
                      error: function() 
                      {
                       //popMsgObj.ShowMsg('sdf');
                      }, 
                      success:function(data) 
                      { 
                        if(data.sta>0)
                        {     document.getElementById("ddlBillStatus").value = "2";
                            popMsgObj.ShowMsg('确认成功');
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val());
                            GetFlowButton_DisplayControl();//审批处理
                        }
                        else
                        {
                              popMsgObj.ShowMsg(data.data);
                        }
                      } 
                   });   
               
      }
}



//取消确认
function Fun_UnConfirmOperate()
{
var c=window.confirm("确认执行取消确认操作吗？")
    if(c==true)
    {

        
    //    document.getElementById("txtBillStatus").value = "执行";
     
        
        glb_BillID = document.getElementById('txtIndentityID').value;
        document.getElementById("txtConfirmorReal").value = "";
        document.getElementById("txtConfirmor").value = "";
        document.getElementById("txtConfirmDate").value = "";
        action ="CancelConfirm";
        
        var DetailProductCount = new Array();//采购数量
	    var DetailFromBillNo = new Array();
	    var DetailFromLineNo = new Array();

	    var DetailUsedUnitCount = new Array();
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
        var length = 0;
        var signFrame = findObj("dg_Log",document); 
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objProductCount = 'txtProductCount'+(i+1);
                var objFromBillNo = 'txtFromBillNo'+(i+1);
                var objFromLineNo = 'txtFromLineNo'+(i+1);
                var objUsedUnitCount = 'UsedUnitCount' + (i + 1);
                if ($("#HiddenMoreUnit").val() == "True") {

                    DetailUsedUnitCount.push(document.getElementById(objUsedUnitCount.toString()).value.Trim());
                }
                
                DetailProductCount.push(document.getElementById(objProductCount.toString()).value.Trim());
                DetailFromBillNo.push(document.getElementById(objFromBillNo.toString()).value.Trim());
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value.Trim());
                length++;
            }
        }
        
        var confirmor = document.getElementById("txtConfirmor").value.Trim();
        var confirmdate = document.getElementById("txtConfirmDate").value.Trim();
        var contractNo = document.getElementById("divPurchaseContractNo").innerHTML;
        var fromType = document.getElementById("ddlFromType").value.Trim();

        var strParams = "action=" + action + "&confirmor=" + confirmor + "&confirmdate=" + confirmdate + "&contractNo=" + contractNo + "&fromType=" + fromType + "&DetailProductCount=" + DetailProductCount + "&DetailFromBillNo=" + DetailFromBillNo + "&DetailFromLineNo=" + DetailFromLineNo + "&length=" + length + "&ID=" + glb_BillID + "&DetailUsedUnitCount=" + DetailUsedUnitCount;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseContractAdd.ashx?"+strParams,
               
            dataType:'json',//返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                      }, 
                      error: function() 
                      {
                       //popMsgObj.ShowMsg('sdf');
                      }, 
                      success:function(data) 
                      { 
                        if(data.sta>0)
                        {   document.getElementById("ddlBillStatus").value = "1";
                            popMsgObj.ShowMsg('取消确认成功');
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                            GetFlowButton_DisplayControl();
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val());
                            GetFlowButton_DisplayControl();//审批处理
                        }
                        else
                        {
                            popMsgObj.ShowMsg('该单据已被其它单据调用了，不允许取消确认！');
//                              popMsgObj.ShowMsg(data.data);
                        }
                      } 
                   });   
               
      }
}



//结单或取消结单按钮操作
function Fun_CompleteOperate(isComplete)
{
if(isComplete)
{
var c=window.confirm("确认执行结单操作吗？")
    if(c==true)
    {
        glb_BillID = document.getElementById('txtIndentityID').value;
        document.getElementById("txtCloserReal").value = document.getElementById("UserName").value.Trim();
        document.getElementById("txtCloser").value = document.getElementById("UserID").value.Trim();
        document.getElementById("txtCloseDate").value = document.getElementById("SystemTime").value.Trim();
        var closer = document.getElementById("txtCloser").value.Trim();
        var contractNo = document.getElementById("divPurchaseContractNo").innerHTML;
        
        if(isComplete)
        {
            action ="Close";
        }
        else
        {
            action ="CancelClose";
        }
        //结单操作
            
            var strParams = "action="+action+"&closer="+closer+"&contractNo="+contractNo+"&ID="+glb_BillID+"";
            $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseContractAdd.ashx?"+strParams,
               
            dataType:'json',//返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                      }, 
                      error: function() 
                      {
                       //popMsgObj.ShowMsg('sdf');
                      }, 
                      success:function(data) 
                      { 
                        if(data.sta>0)
                        {
                            if(data.sta == 1)
                            {//表示结单成功
                                document.getElementById("ddlBillStatus").value = "4";
                                document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                                document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                                document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                                fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                                fnFlowStatus($("#FlowStatus").val());
                            }
                            else
                            {//为2表示取消结单成功
                                document.getElementById("ddlBillStatus").value = "2";
                                document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                                document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                                document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                                fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                                fnFlowStatus($("#FlowStatus").val());
                            }
    //                        popMsgObj.ShowMsg('确认成功');
                            popMsgObj.ShowMsg(data.info);
                            GetFlowButton_DisplayControl();//审批处理
                        }
                      } 
                   });   
      }
}
else
{
var c=window.confirm("确认执行取消结单操作吗？")
    if(c==true)
    {
        glb_BillID = document.getElementById('txtIndentityID').value;
        document.getElementById("txtCloserReal").value = document.getElementById("UserName").value.Trim();
        document.getElementById("txtCloser").value = document.getElementById("UserID").value.Trim();
        document.getElementById("txtCloseDate").value = document.getElementById("SystemTime").value.Trim();
        var closer = document.getElementById("txtCloser").value.Trim();
        var contractNo = document.getElementById("divPurchaseContractNo").innerHTML;
        
        if(isComplete)
        {
            action ="Close";
        }
        else
        {
            action ="CancelClose";
        }
        //结单操作
            
            var strParams = "action="+action+"&closer="+closer+"&contractNo="+contractNo+"&ID="+glb_BillID+"";
            $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseContractAdd.ashx?"+strParams,
               
            dataType:'json',//返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                      }, 
                      error: function() 
                      {
                       //popMsgObj.ShowMsg('sdf');
                      }, 
                      success:function(data) 
                      { 
                        if(data.sta>0)
                        {
                            if(data.sta == 1)
                            {//表示结单成功
                                document.getElementById("ddlBillStatus").value = "4";
                                document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                                document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                                document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                                fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                                fnFlowStatus($("#FlowStatus").val());
                            }
                            else
                            {//为2表示取消结单成功
                                document.getElementById("ddlBillStatus").value = "2";
                                document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                                document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                                document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                                fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                                fnFlowStatus($("#FlowStatus").val());
                            }
    //                        popMsgObj.ShowMsg('确认成功');
                            popMsgObj.ShowMsg(data.info);
                            GetFlowButton_DisplayControl();//审批处理
                        }
                      } 
                   });   
      }
}

}



function Fun_FlowApply_Operate_Succeed(operateType)
{
try{
    if(operateType == "0")
    {//提交审批成功后,不可改
        $("#imgUnSave").css("display", "inline");//保存灰
        $("#imgSave").css("display", "none");//保存
        
        $("#imgAdd").css("display", "none");//明细添加
        $("#imgUnAdd").css("display", "inline");//明细添加灰
        $("#imgDel").css("display", "none");//明细删除
        $("#imgUnDel").css("display", "inline");//明细删除灰
        $("#Get_Potential").css("display", "none");//源单总览
        $("#Get_UPotential").css("display", "inline"); //源单总览灰
        $("#btnGetGoods").css("display", "none");//条码扫描按钮
    }
    else if(operateType == "1")
    {//审批成功后，不可改
        $("#imgUnSave").css("display", "inline");
        $("#imgSave").css("display", "none");
        
        
        $("#imgAdd").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        $("#imgDel").css("display", "none");
        $("#imgUnDel").css("display", "inline");
        $("#Get_Potential").css("display", "none");
        $("#Get_UPotential").css("display", "inline"); 
        $("#btnGetGoods").css("display", "none");//条码扫描按钮
    }
    else if(operateType == "2")
    {//审批不通过
        $("#imgUnSave").css("display", "none");
        $("#imgSave").css("display", "inline");
        
        
        $("#imgAdd").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        $("#imgDel").css("display", "inline");
        $("#imgUnDel").css("display", "none");
        $("#Get_Potential").css("display", "inline");
        $("#Get_UPotential").css("display", "none"); 
        $("#btnGetGoods").css("display", "inline");//条码扫描按钮
    }
   }
   catch(e)
   {}
}

//根据单据状态决定页面按钮操作
function fnStatus(BillStatus,Isyinyong) {
 
try{
    switch (BillStatus) { //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        case '1': //制单
//            $("#HiddenAction").val('Add');
//            fnFlowStatus($("#FlowStatus").val());//?
            break;
        case '2': //执行
            if(Isyinyong == 'True')
            {//被引用不可编辑
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none");//条码扫描按钮  
            }
            else
            {
               $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
                $("#btnGetGoods").css("display", "none");//条码扫描按钮  
//                $("#HiddenAction").val('Update');
//                $("#imgSave").css("display", "inline");
//                $("#imgUnSave").css("display", "none");
//                $("#imgAdd").css("display", "inline");
//                $("#imgUnAdd").css("display", "none");
//                $("#imgDel").css("display", "inline");
//                $("#imgUnDel").css("display", "none");
//                $("#Get_Potential").css("display", "inline");
//                $("#Get_UPotential").css("display", "none"); 
//                $("#btnGetGoods").css("display", "inline");//条码扫描按钮 
            }
            
            break;
        case '3': //变更
//            $("#HiddenAction").val('Update');
            $("#FromType").attr("disabled", "disabled");
            $("#imgSave").css("display", "inline");
            $("#imgUnSave").css("display", "none");
            break;
        case '4': //手工结单
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#Get_Potential").css("display", "none");
            $("#Get_UPotential").css("display", "inline");
            $("#btnGetGoods").css("display", "none");//条码扫描按钮
            break;

        case '5':
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            break;
    }
   }
   catch(e)
   {}
}

function fnFlowStatus(FlowStatus)
{
try{
    switch (FlowStatus) {
                case "": //未提交审批         
                    break;
                case "待审批": //当前单据正在待审批
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline"); 
                    $("#btnGetGoods").css("display", "none");//条码扫描按钮                  
                    break;
                case "审批中": //当前单据正在审批中
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#Get_Potential").css("display", "none");
                    $("#Get_UPotential").css("display", "inline");
                    $("#btnGetGoods").css("display", "none");//条码扫描按钮
                    break;
                case "审批通过": //当前单据已经通过审核
                    //制单状态的审批通过单据,不可修改
                    if ($("#ddlBillStatus").val() == "1") {
                        $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#Get_Potential").css("display", "none");
                        $("#Get_UPotential").css("display", "inline");
                        $("#btnGetGoods").css("display", "none");//条码扫描按钮
                    }

                    break;
                case "撤消审批": //当前单据进行取消确认了.
                    //取消确认后，为制单,可以修改
                    if ($("#ddlBillStatus").val() == "1") {
                        $("#imgSave").css("display", "inline");
                        $("#imgUnSave").css("display", "none");
                        $("#imgAdd").css("display", "inline");
                        $("#imgUnAdd").css("display", "none");
                        $("#imgDel").css("display", "inline");
                        $("#imgUnDel").css("display", "none");
                        $("#Get_Potential").css("display", "inline");
                        $("#Get_UPotential").css("display", "none");
                        $("#btnGetGoods").css("display", "inline");//条码扫描按钮
                    }

                    break;
                case "审批不通过": //当前单据审批未通过
                    break;
         }
    }
    catch(e)
    {}
}



function SetSaveButton_DisplayControl(flowStatus)
{
    try{
    switch (parseInt(flowStatus)) {
                case 5: //当前单据进行取消确认了.
                    //取消确认后，为制单,可以修改
                     document.getElementById("imgSave").style.display="inline";
                    document.getElementById("imgUnSave").style.display="none";
                    document.getElementById("imgAdd").style.display="inline";
                    document.getElementById("imgUnAdd").style.display="none";
                    document.getElementById("imgDel").style.display="inline";
                    document.getElementById("imgUnDel").style.display="none";
                    document.getElementById("Get_Potential").style.display="inline";
                    document.getElementById("Get_UPotential").style.display="none";
                    document.getElementById("btnGetGoods").style.display="inline";//条码扫描按钮

                    break;
                case 0: //未提交审批         
                    break;
                case 1: //当前单据正在待审批
                   document.getElementById("imgSave").style.display="none";
                    document.getElementById("imgUnSave").style.display="inline";
                    document.getElementById("imgAdd").style.display="none";
                    document.getElementById("imgUnAdd").style.display="inline";
                    document.getElementById("imgDel").style.display="none";
                    document.getElementById("imgUnDel").style.display="inline";
                    document.getElementById("Get_Potential").style.display="none";
                    document.getElementById("Get_UPotential").style.display="inline"; 
                    document.getElementById("btnGetGoods").style.display="none";//条码扫描按钮                
                    break;
                case 2: //当前单据正在审批中
                   document.getElementById("imgSave").style.display="none";
                    document.getElementById("imgUnSave").style.display="inline";
                    document.getElementById("imgAdd").style.display="none";
                    document.getElementById("imgUnAdd").style.display="inline";
                    document.getElementById("imgDel").style.display="none";
                    document.getElementById("imgUnDel").style.display="inline";
                    document.getElementById("Get_Potential").style.display="none";
                    document.getElementById("Get_UPotential").style.display="inline";
                    document.getElementById("btnGetGoods").style.display="none";//条码扫描按钮
                    break;
                case 3: //当前单据已经通过审核
                    //制单状态的审批通过单据,不可修改
                    document.getElementById("imgSave").style.display="none";
                    document.getElementById("imgUnSave").style.display="inline";
                    document.getElementById("imgAdd").style.display="none";
                    document.getElementById("imgUnAdd").style.display="inline";
                    document.getElementById("imgDel").style.display="none";
                    document.getElementById("imgUnDel").style.display="inline";
                    document.getElementById("Get_Potential").style.display="none";
                    document.getElementById("Get_UPotential").style.display="inline";
                    document.getElementById("btnGetGoods").style.display="none";//条码扫描按钮

                    break;
                
                case 4: //当前单据审批未通过
                    break;
              }
      }
      catch(e)
        {}
}





function clearProviderdiv()
{
    document.getElementById("txtProviderID").value = "";
    document.getElementById("txtHidProviderID").value = "";
}



/*单据打印*/
function PurchaseContractPrint() {

//    var intMasterPurchaseContractID = document.getElementById("txtIndentityID").value;
//    if (intMasterPurchaseContractID == 0) {
//        GetFlowButton_DisplayControl();
//    }

    var ID = $("#txtIndentityID").val();
    if (parseInt(ID) == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存！");
        return;
    } 
    
    
       window.open("../../../Pages/PrinttingModel/PurchaseManager/PurchaseContractPrint.aspx?ID="+document.getElementById("txtIndentityID").value);
}




function Numb_roundPur(numberStr,fractionDigits)
{   
    var num=numberStr.toString().split('.');
    if(num[1]!=""&&num[1]!=null&&parseInt(num[1])==0)
    {
         return numberStr;
    }
    else
    {
        if(numberStr!=parseInt(numberStr))
        {
              with(Math)
              {   
                return round(numberStr*pow(10,fractionDigits))/pow(10,fractionDigits);
              }   
        }
        else
        {
               return numberStr+".00";
        }
    }
}   

//供应商控件委琐遮照
/*弹出*/
function PopPurProviderInfo()
{
    openRotoscopingDiv(false,'divPBackShadow','PBackShadowIframe');
    popProviderObj.ShowList()
}
/*关闭*/
function closeProviderdiv()
{   
    closeRotoscopingDiv(false,'divPBackShadow');
    document.getElementById("divProviderInfohhh").style.display="none";
}




function URL(input_url)
 {
  this.url = input_url?input_url:document.location.href;
  this.scriptName = "";
  
  this.params = new Array();
  this.makeParams = function(){
    var tmp_url = this.url;
    var spIdx = tmp_url.indexOf("?");
    if( spIdx == -1){
     this.scriptName = tmp_url;
     return ;
    }
    this.scriptName = tmp_url.substring(0,spIdx-1);
    tmp_url=tmp_url.substring(spIdx+1);
    
    this.params = tmp_url.split("&");
        
   }
  this.makeParams();
 
  this.buildURL = function (){
    var tmp_url = this.scriptName;
    
    tmp_url += "?";
    
    for(var k=0;k<this.params.length;k++)
      tmp_url += this.params[k] +"&";
         
    tmp_url = tmp_url.substring(0,tmp_url.length-1);
    
    this.url = tmp_url;
     
   }
   
  this.addParam = function (key,value){
    for(var i=0;i<this.params.length;i++){
     if(this.params[i].split("=")[0] == key){
      this.editParam(key,value);      
      return;
     }
    }
    this.params[this.params.length] = key+"="+value;
    this.buildURL();
   }
   
  this.delParam = function (key){
    var keyIdx=-1;
    for(var i=0;i<this.params.length;i++)
     if(this.params[i].split("=")[0] == key)
      {
       keyIdx = i;
       break;
      }
    if (keyIdx == -1)
     return;
     
    for(var j=keyIdx+1;j<this.params.length;j++)
     this.params[j-1] = this.params[j];
    
    this.params.length--;
    
    this.buildURL();
   }
   
  this.editParam = function(key,keyV){
    for(var i=0;i<this.params.length;i++){
     if(this.params[i].split("=")[0] == key){
      this.params[i] = key+"="+keyV;
      this.buildURL();
      return;
      }
    }
    
    this.addParam(key,keyV);
    
   }
   
   
  this.keys = function(key){
   for(var i=0;i<this.params.length;i++){
     if(this.params[i].split("=")[0] == key){
      var tmpObj = new Object();
      tmpObj.key = key;
      tmpObj.value = this.params[i].split("=")[1];
      return tmpObj;
     }
   }
   
   return {key:key,value:""};
  }  
 
   
 }
 
 //---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
/*
 参数：商品ID，商品编号，商品名称，去税售价，单位ID，单位名称，销项税率（%），含税售价，销售折扣，规格，类型名称，类型ID，含税进价，去税进价，进项税率(%)，标准成本
*/
//根据条码获取的商品信息填充数据
 function GetGoodsDataByBarCode(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, IsBatchNo, BatchNo, ProductCount, CurrentStore, Source, ColorName)
{
//debugger ;
    if(!IsExist(ID))//如果重复记录，就不增加
    {
  var StandardBuy1=      parseFloat (StandardBuy).toFixed($("#HiddenPoint").val());
  
   var TaxBuy1=      parseFloat (TaxBuy).toFixed($("#HiddenPoint").val());

   var InTaxRate1 = parseFloat(InTaxRate).toFixed($("#HiddenPoint").val());



   var StandardSell1 = parseFloat(StandardSell).toFixed($("#HiddenPoint").val());

   var SellTax1 = parseFloat(SellTax).toFixed($("#HiddenPoint").val());
   var TaxRate1 = parseFloat(TaxRate).toFixed($("#HiddenPoint").val());
   
//         var TaxBuy=  parseFloat (parseFloat(TaxBuy) ).toFixed($("#HiddenPoint").val();
//            var TaxBuy=  parseFloat (parseFloat(TaxBuy) ).toFixed($("#HiddenPoint").val();
            
       var rowID=AddSignRow();//插入行
       //填充数据
       //物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
       BarCode_FillParent_Content(ColorName, ID, ProdNo, ProductName, StandardSell1, UnitID, UnitName, TaxRate1, SellTax1, Discount, Specification, CodeTypeName, TypeID, StandardBuy1, TaxBuy1, InTaxRate1, rowID);
    }
    else
    {
    
     if($("#HiddenMoreUnit").val()=="False")
      {
      document.getElementById("txtProductCount"+rerowID).value=    parseFloat (parseFloat(document.getElementById("txtProductCount"+rerowID).value)+1).toFixed($("#HiddenPoint").val());
   
        }
        else
        {
            var count1=document.getElementById("UsedUnitCount"+rerowID).value;
 
        document.getElementById("UsedUnitCount"+rerowID).value=     parseFloat (parseFloat(count1)+1).toFixed($("#HiddenPoint").val());
        }
        
      
        
        
        
        fnTotalInfo();//更改数量后重新计算
    }
    
}

var rerowID="";
//判断是否有相同记录有返回true，没有返回false
function IsExist(ID)
{
     var signFrame = findObj("dg_Log", document);
     if((typeof(signFrame)=="undefined")|| signFrame==null)
     {
        return false;
     }
     for (i = 1; i < signFrame.rows.length; i++) {//判断商品是否在明细列表中
         if (signFrame.rows[i].style.display != "none") {
              var ProductID = $("#txtProductID" + i).val(); //商品ID（对应商品表ID）
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
function BarCode_FillParent_Content(ColorName,id, no, productname, dddf, unitid, unit, df, sdfge, discount, standard, fg, fgf, taxprice, price, taxrate, index) {
   
    var ProductNo = 'txtProductNo'+index;
    var ProductName='txtProductName'+index;
    var UnitID = 'txtUnitID2'+index;
    var Unit='txtUnitID'+index;
    var Price='txtUnitPrice'+index;
    var Standard='txtstandard'+index;
    var ProductID = 'txtProductID'+index;
    var TaxPrice = 'txtTaxPrice'+index;
    var Discount = 'txtDiscount'+index;
    var TaxRate = 'txtTaxRate'+index;
    var HiddTaxPrice = 'hiddTaxPrice'+index;
    var HiddTaxRate = 'hiddTaxRate'+index;
    var HiddUnitPrice = 'hiddUnitPrice'+index;
    $("#DtlSColor" + index).val(ColorName);
    document.getElementById(TaxRate).value = (parseFloat(taxrate)).toFixed($("#HiddenPoint").val()) ;
    document.getElementById(TaxPrice).value =  (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
   var taxPrice1= (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
   document.getElementById(Standard).value =    standard ;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(Unit).value=unit;
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    var prdsd = (parseFloat(price)).toFixed($("#HiddenPoint").val());
    document.getElementById(Price).value = prdsd; 
    
     
    var UnitPrice1= (parseFloat(price)).toFixed($("#HiddenPoint").val());
    document.getElementById(HiddTaxPrice).value =  (parseFloat(taxprice)).toFixed($("#HiddenPoint").val());
    document.getElementById(HiddTaxRate).value = (parseFloat(taxrate)).toFixed($("#HiddenPoint").val()) ;
 
    
    document.getElementById(HiddUnitPrice).value = (parseFloat(price)).toFixed($("#HiddenPoint").val());
    document.getElementById("txtProductCount"+index).value=  (parseFloat('1')).toFixed($("#HiddenPoint").val());
     
    
    var isAddTax = document.getElementById("chkIsZzs").checked;
    //新加币种的汇率问题
    var Rate = document.getElementById("txtRate").value.Trim();
            
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true) {//是增值税则

          
               // (parseFloat(document.getElementById("hiddTaxRate" + rowIndex).value.Trim() / Rate)).toFixed($("#HiddenPoint").val());

                document.getElementById("txtTaxPrice" + rowIndex).value = (parseFloat(document.getElementById("hiddTaxPrice" + rowIndex).value.Trim() / Rate)).toFixed($("#HiddenPoint").val()); //含税价等于隐藏域含税价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = (parseFloat(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate)).toFixed($("#HiddenPoint").val()); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate" + rowIndex).value = (parseFloat(document.getElementById("hiddTaxRate" + rowIndex).value.Trim()  )).toFixed($("#HiddenPoint").val()); //税率等于隐藏域税率
                document.getElementById("chkisAddTaxText1").style.display="inline";
                document.getElementById("chkisAddTaxText2").style.display="none";
            }
            else
            {
                document.getElementById("txtTaxPrice" + rowIndex).value = (parseFloat(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate)).toFixed($("#HiddenPoint").val()); //含税价等于单价 再除以汇率(币种要求修改)
                document.getElementById("txtUnitPrice" + rowIndex).value = (parseFloat(document.getElementById("hiddUnitPrice" + rowIndex).value.Trim() / Rate)).toFixed($("#HiddenPoint").val()); //单价则改为隐藏域里的单价除以汇率
                document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                document.getElementById("chkisAddTaxText1").style.display="none";
                document.getElementById("chkisAddTaxText2").style.display="inline";
            }
        }
    }
        if($("#HiddenMoreUnit").val()=="False")
            {
                    fnTotalInfo();
            }
            else
            {

                GetUnitGroupSelectEx(id, "InUnit", "SignItem_TD_UnitID_Select" + index, "ChangeUnit(this.id," + index + "," + UnitPrice1 + "," + taxPrice1 + ")", "unitdiv" + index, '', "FillApplyContent(" + index + "," + UnitPrice1 + "," + taxPrice1 + "," + 1 + ",'1','','')"); //绑定单位组

//                GetUnitGroupSelectEx(productid, "InUnit", "SignItem_TD_UnitID_Select" + Index, "ChangeUnit(this.id," + Index + "," + UnitPrice1 + "," + taxPrice1 + ")", "unitdiv" + Index, '', "FillApplyContent(" + Index + "," + UnitPrice1 + "," + taxPrice1 + "," + ProductCount1 + ",'" + UsedUnitCount + "','" + UsedUnitID + "','Bill')"); //绑定单位组
            }
  
}
//---------------------------------------------------条码扫描END------------------------------------------------------------------------------------

function ShowSnapshot() {
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var ck = document.getElementsByName("chk");
    var signFrame = findObj("dg_Log", document);
    for (var i = 0; i < ck.length; i++) {

        var rowID = i + 1;
        if (signFrame.rows[rowID].style.display != "none") {
            if (ck[i].checked) {
                detailRows++;
                intProductID = document.getElementById('txtProductID' + rowID).value;
                snapProductName = document.getElementById('txtProductName' + rowID).value;
                snapProductNo = document.getElementById('txtProductNo' + rowID).value;
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







/*焦点2：鼠标失去焦点时，匹配数据库物品信息*/
function SetMatchProduct(rows, values) {
    popTechObj.InputObj = '';
    var ProdNo = values;
    if (values != "") {
        popTechObj.InputObj = rows;
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../../Handler/Office/SupplyChain/BatchProductInfo.ashx?ProdNo=" + ProdNo, //目标地址
            cache: false,
            success: function(msg) {
                var rowsCount = 0;
                var rowsExistCount = 0;

                if (typeof (msg.dataBatch) != 'undefined') {
                    $.each(msg.dataBatch, function(i, item) {

                        if (!IsExistll(item.ProdNo, rows)) {
                            rowsCount++;
                            Fun_FillParent_Content(item.ID, item.ProdNo, item.ProductName, item.StandardSell, item.UnitID,
                                            item.CodeName, item.TaxRate, item.SellTax, item.Discount, item.Specification,
                                            item.CodeTypeName, item.TypeID, item.StandardBuy, item.TaxBuy, item.InTaxRate,
                                            item.StandardCost, item.GroupUnitNo, item.SaleUnitID, 'saleUnitName', item.InUnitID,
                                            'inUnitName', item.StockUnitID, 'stockUnitName', item.MakeUnitID, 'maxkUnitName',
                                            item.IsBatchNo, item.ColorName);
                        }
                        else {
                            rowsExistCount++;
                        }
                    });
                }
                if (rowsCount == 0) {
                    ClearOneProductInfo(rows);
                    if (rowsExistCount > 0) {
                        popMsgObj.Show("采购合同明细|", "明细中不允许存在重复记录");
                    }
                    else {
                        popMsgObj.Show("物品编号" + rows + "|", "不存在此" + values + "物品，请重新输入或选择物品");
                    }
                }

            },
            error: function() { popMsgObj.ShowMsg('匹配物品数据时发生请求异常!'); },
            complete: function() { }
        });
    }
    else {
        if (document.getElementById('txtProductID' + rows).value != "") {
            popMsgObj.Show("物品编号" + rows + "|", "请重新输入或选择物品");
        }
    }
}


/*判断是否有相同记录有返回true，没有返回false*/
var rerowID = "";
function IsExistll(prodNo, rows) {
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var prodNo1 = document.getElementById("txtProductNo" + i).value;
        if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
            if (i != rows) {
                rerowID = i;
                return true;
            }
        }
    }
    return false;
}

