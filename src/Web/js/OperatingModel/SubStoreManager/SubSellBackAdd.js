var page = "";
var Isliebiao ;

$(document).ready(function()
{

    $(":text").each(function(){ 
        this.disabled=true; 
    });
    //所有checkbox不可用
    $(":checkbox").each(function(){ 
        this.disabled=true; 
    });
    document.getElementById('txtInDate').disabled=true;
    document.getElementById('ddlInDateHour').disabled=true;
    document.getElementById('ddlInDateMin').disabled=true;
    document.getElementById('UserInUserID').disabled=true;
    document.getElementById('txtSttlDate').disabled=true;
    document.getElementById('ddlSttlDateHour').disabled=true;
    document.getElementById('ddlSttlDateMin').disabled=true;
    document.getElementById('UserSttlUserID').disabled=true;
    document.getElementById('drpFromType').disabled=true;
    document.getElementById('ddlOutDateHour').disabled=true;
      document.getElementById('ddlOutDateMin').disabled=true;
      document.getElementById('ddlSendMode').disabled=true;
      document.getElementById('ddlBackDateHour').disabled=true;
      document.getElementById('ddlBackDateMin').disabled=true;
      document.getElementById('ddlSttlDateHour').disabled=true;
      document.getElementById('ddlSttlDateMin').disabled=true;
      document.getElementById('drpCurrencyType').disabled=true;
   document.getElementById('checkall').disabled=true;
    requestobj = GetRequest();
    var intMasterSubSellBackID = document.getElementById("txtIndentityID").value;
    recordnoparam = intMasterSubSellBackID.toString();
    
    
    var BackNo = requestobj['BackNo'];
    var Title = requestobj['Title'];
    var OrderNo = requestobj['OrderNo'];
    var OrderID = requestobj['OrderID'];
    var CustName = requestobj['CustName'];
    var CustTel = requestobj['CustTel'];
    var DeptID = requestobj['DeptID'];
    var DeptName = requestobj['DeptName'];
    var Seller = requestobj['Seller'];
    var SellerName = requestobj['SellerName'];
    var BusiStatus = requestobj['BusiStatus'];
    var BillStatus = requestobj['BillStatus'];
    var CustAddr = requestobj['CustAddr'];
    var BillStatusName = requestobj['BillStatusName'];
    var BusiStatusName = requestobj['BusiStatusName'];
    var IsliebiaoSendMode = requestobj['IsliebiaoSendMode'];
    var IsliebiaoFromType = requestobj['IsliebiaoFromType'];
    var IsliebiaoCurrencyType = requestobj['IsliebiaoCurrencyType'];
    var pageCount=requestobj['pageCount'];
    var pageIndex=requestobj['pageIndex'];
    
    
    var Isliebiao = requestobj['Isliebiao'];
    
    if(typeof(BillStatusName) != "undefined")
    {
        document.getElementById("hidBillStatusName").value = BillStatusName;
    }
    if(typeof(BusiStatusName) != "undefined")
    {
        document.getElementById("hidBusiStatusName").value = BusiStatusName;
    }
    if(typeof(IsliebiaoSendMode) != "undefined")
    {
        document.getElementById("hidIsliebiaoSendMode").value = IsliebiaoSendMode;
        if(IsliebiaoSendMode =="1")
        {//分店发货
            document.getElementById("storagedetail").style.display = "none";
        }
        else
        {//总店发货
            document.getElementById("storagedetail").style.display = "inline";
        }
    }
    if(typeof(IsliebiaoFromType) != "undefined")
    {
        document.getElementById("hidIsliebiaoFromType").value = IsliebiaoFromType;
        if(IsliebiaoFromType =="0")
        {//无来源
            document.getElementById("divIsbishuxiang").style.display="none";
            document.getElementById("txtOrderID").style.display = "none";
        }
        else
        {//销售订单
             document.getElementById("divIsbishuxiang").style.display="inline";
             document.getElementById("txtOrderID").style.display = "inline";
        }
    }
//    if(typeof(IsliebiaoCurrencyType) != "undefined")
//    {
//        document.getElementById("hidIsliebiaoCurrencyType").value = IsliebiaoCurrencyType;
//    }
    
    
    if(typeof(Isliebiao)=="undefined")
    {
        document.getElementById("chkisAddTax").checked = true;
        document.getElementById("chkisAddTaxText1").style.display="inline";
    }
    else
    {
        $("#btn_back").show();
        document.getElementById("hidIsliebiao").value = Isliebiao;
    }
    
    var ModuleID = document.getElementById("hidModuleID").value;
var URLParams = "Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&BackNo="+escape(BackNo)+"&Title="+escape(Title)+"&OrderNo="+escape(OrderNo)+"&OrderID="+escape(OrderID)+
                     "&CustName="+escape(CustName)+"&CustTel="+escape(CustTel)+"&DeptID="+escape(DeptID)+"&DeptName="+escape(DeptName)+"&Seller="+escape(Seller)+
                     "&SellerName="+escape(SellerName)+"&BusiStatus="+escape(BusiStatus)+"&BillStatus="+escape(BillStatus)+"&CustAddr="+escape(CustAddr)+"&pageCount="+pageCount+"&pageIndex="+pageIndex; 
    document.getElementById("hidSearchCondition").value = URLParams;
    
    
    DealPage(recordnoparam);
    
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
        
//        if(typeof(document.getElementById("hidIsliebiao").value)!="undefined")
//        {
            
//            if(typeof(recordnoparam)!="undefined")
            if(recordnoparam !=0)
            {
//                $("#btn_back").show();
                //isnew="2";
                //$("#hfLoveID").attr("value",recordnoparam);//ID记录在隐藏域中
                document.getElementById("txtAction").value="2";
//                document.getElementById("divTitle").innerText="采购退货单";//加载完再控制标题
//                GetFlowButton_DisplayControl();
        //        //显示返回按钮
        //        $("#btn_back").show();
                GetSubSellBack(recordnoparam);
                
                
                
                //页面加载完对页面进行设置
                //var IsBusiStatus = document.getElementById("drpBusiStatus").value;//异步问题
                var IsBillStatusName = document.getElementById("hidBillStatusName").value;

                if(IsBillStatusName != "制单")
                {
                    
                    //设置所有文本控件只读
//                    $(":text").each(function(){ 
//                    this.disabled=true; 
//                    }); 
//                    document.getElementById('drpFromType').disabled=true;
//                    document.getElementById('ddlSendMode').disabled=true;
//                    document.getElementById('ddlOutDateHour').disabled=true;
//                    document.getElementById('ddlOutDateMin').disabled=true;
//                    document.getElementById('chkisAddTax').disabled=true;
//                    document.getElementById('ddlBackDateHour').disabled=true;
//                    document.getElementById('ddlBackDateMin').disabled=true;
//                    document.getElementById('drpCurrencyType').disabled=true;
//                    document.getElementById('txtBackReason').disabled=true;
//                    document.getElementById('txtRemark').disabled=true;
//                    document.getElementById('divUploadResume').disabled=true;
//                    document.getElementById('divDeleteResume').disabled=true;
                    var IsBusiStatusName = document.getElementById("hidBusiStatusName").value;
                    if(IsBusiStatusName =="入库")
                    {
                     
                        document.getElementById("divTitle").innerText ="销售退货管理--入库";
                        document.getElementById("divInDate").style.display = "inline";
                        document.getElementById("divInUserID").style.display = "inline";
                        //默认的入库人和入库时间显示
                        document.getElementById("UserInUserID").value = document.getElementById("UserName").value;
                        document.getElementById("HidInUserID").value = document.getElementById("UserID").value;
                        var InDate = document.getElementById("SystemTime2").value;
                        var InDate1 = InDate.split(' ')[0];
                        var InDatehour = InDate.split(' ')[1].split(':')[0];
                        var InDatemin = InDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtInDate").value = InDate1;
                        document.getElementById("ddlInDateHour").value = InDatehour;
                        document.getElementById("ddlInDateMin").value = InDatemin;
                        //设置部分控件可用
//                        document.getElementById('txtInDate').disabled=true;
//                        document.getElementById('ddlInDateHour').disabled=true;
//                        document.getElementById('ddlInDateMin').disabled=true;
//                        document.getElementById('UserInUserID').disabled=true;
//                        document.getElementById('txtWairPayTotal').disabled=true;
////                        document.getElementById('txtPayedTotal').disabled=true; 
//                        document.getElementById('txtSettleTotal').disabled=true;
//                        document.getElementById('txtWairPayTotalOverage').disabled=true;  
                    }
                    else if(IsBusiStatusName == "结算")
                    {
                      
                        document.getElementById("divTitle").innerText ="销售退货管理--结算";
                        document.getElementById("divSttlDate").style.display = "inline";
                        document.getElementById("divSttlUserID").style.display = "inline";
                        document.getElementById("divInDate").style.display = "inline";
                        document.getElementById("divInUserID").style.display = "inline";
                        //默认的结算人和结算时间显示
                        document.getElementById("UserSttlUserID").value = document.getElementById("UserName").value;
                        document.getElementById("HidSttlUserID").value = document.getElementById("UserID").value;
                        var SttlDate = document.getElementById("SystemTime2").value;
                        var SttlDate1 = SttlDate.split(' ')[0];
                        var SttlDatehour = SttlDate.split(' ')[1].split(':')[0];
                        var SttlDatemin = SttlDate.split(' ')[1].split(':')[1];
                        document.getElementById("txtSttlDate").value = SttlDate1;
                        document.getElementById("ddlSttlDateHour").value = SttlDatehour;
                        document.getElementById("ddlSttlDateMin").value = SttlDatemin;
                        //设置部分控件可用
                        document.getElementById('txtWairPayTotal').disabled=true;
//                        document.getElementById('txtPayedTotal').disabled=true; 
                        document.getElementById('txtSettleTotal').disabled=true;
//                        document.getElementById('txtWairPayTotalOverage').disabled=true;  
                    }
                    else if(IsBusiStatusName =="完成")
                    {
                        document.getElementById("divTitle").innerText ="销售退货管理--完成";
                    }
                }
             
            }
//    }
}

//返回
function Back()
{ 
   // window.location.href=page+'?CustName='+CustName+'&LoveType='+LoveType+
   //                     '&LoveBegin='+LoveBegin+'&LoveEnd='+LoveEnd+'&Title='+Title+'&CustLinkMan='+CustLinkMan;
    
//    window.location.href=page+'?rejectNo1='+rejectNo1+'&Title='+Title+
//                        '&TypeID='+TypeID+'&DeptID='+DeptID+'&Seller='+Seller+'&FromType='+FromType+'&ProviderID='+ProviderID+'&BillStatus='+BillStatus+'&UsedStatus='+UsedStatus;

var URLParams = document.getElementById("hidSearchCondition").value;

    window.location.href='SubSellBackList.aspx?'+URLParams;
}

function GetSubSellBack(ID)
{
 
//       document.getElementById("divSubSellBackNo").innerHTML=rejectNo;
//       document.getElementById("divSubSellBackNo").display="block";
//       document.getElementById("divInputNo").style.display="none";
//     var rejectNo= document.getElementById("txtIsliebiaoNo").value;
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/SubStoreManager/SubSellBackEdit.ashx",//目标地址
//       data:'ID='+escape(ID),
        data:"ID="+ID+"",
//        data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&RoleID="+escape(RoleID)+"",//数据
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                        //基本信息
                        //$("#CodingRuleControl1").attr("value",item.rejectNo);//编号
                        $("#CodingRuleControl1_txtCode").attr("value",item.BackNo);//编号
                        $("#divSubSellBackNo").attr("value",item.BackNo);//合同编号
                        $("#txtTitle").attr("value",item.Title);//主题
                        $("#txtDeptName").attr("value",item.DeptName);//分店名称
                        $("#HidDeptID").attr("value",item.DeptID);//分店ID
                        $("#HidDeptID").attr("value",item.DeptID);//部门id
                        $("#drpFromType").attr("value",item.FromType);//源单类型
                        $("#txtOrderID").attr("value",item.OrderNo);//对应订单编号
                        $("#HidOrderID").attr("value",item.OrderID);//对应订单ID
                        $("#ddlSendMode").attr("value",item.SendMode);//发货模式
                        if(item.OutDate != "")
                        {//发货时间
                            var OutDate = item.OutDate;
                            var OutDate1 = OutDate.split(' ')[0];
                            var OutDateHour = OutDate.split(' ')[1].split(':')[0];
                            var OutDateMin = OutDate.split(' ')[1].split(':')[1];
                            document.getElementById("txtOutDate").value = OutDate1;
                            document.getElementById("ddlOutDateHour").value = OutDateHour;
                            document.getElementById("ddlOutDateMin").value = OutDateMin;
                        }
                        $("#UserOutUserID").attr("value",item.OutUserIDName);//发货人名称
                        $("#HidOutUserID").attr("value",item.OutUserID);//发货人ID
//                        $("#chkIsZzs").attr("value",item.isAddTax);//是否是增值税
                        if (item.isAddTax ==1)
                        {
                            document.getElementById("chkisAddTax").checked = true;
                            document.getElementById("chkisAddTaxText1").style.display="inline";
                        }
                        else
                        {
                            document.getElementById("chkisAddTax").checked = false;
                            document.getElementById("chkisAddTaxText2").style.display="inline";
                        }
                        $("#txtCustName").attr("value",item.CustName);//客户名称
                        $("#txtCustTel").attr("value",item.CustTel);//联系电话
                        $("#txtCustMobile").attr("value",item.CustMobile);//客户手机号
                        $("#drpCurrencyType").attr("title",item.CurrencyTypeName);//币种下拉框显示
                        $("#txtRate").attr("value",item.Rate);//汇率
                        for(var i=0;i<document.getElementById("drpCurrencyType").options.length;++i)
                            {
                                if(document.getElementById("drpCurrencyType").options[i].value.split('_')[0]== item.CurrencyType)
                                {
                                    $("#drpCurrencyType").attr("selectedIndex",i);
                                    break;
                                }
                            }
                        $("#drpBusiStatus").attr("value",item.BusiStatus);//业务状态
                        if(item.BusiStatus == "2")
                        {
                            document.getElementById("divTitle").innerText ="销售退货管理--入库";
                        }
                        if(item.BusiStatus == "3")
                        {
                            document.getElementById("divTitle").innerText ="销售退货管理--结算";
                        }
                        if(item.BusiStatus == "4")
                        {
                            document.getElementById("divTitle").innerText ="销售退货管理--完成";
                        }
                        if(item.BackDate != "")
                        {//退货时间
                            var BackDate = item.BackDate;
                            var BackDate1 = BackDate.split(' ')[0];
                            var BackDateHour = BackDate.split(' ')[1].split(':')[0];
                            var BackDateMin = BackDate.split(' ')[1].split(':')[1];
                            document.getElementById("txtBackDate").value = BackDate1;
                            document.getElementById("ddlBackDateHour").value = BackDateHour;
                            document.getElementById("ddlBackDateMin").value = BackDateMin;
                        }
                        $("#UserSeller").attr("value",item.SellerName);//退货处理人名称
                        $("#HidSeller").attr("value",item.Seller);//退货处理人id
                        if(item.InDate != "")
                        {//入库时间
                            var InDate = item.InDate;
                            var InDate1 = InDate.split(' ')[0];
                            var InDateHour = InDate.split(' ')[1].split(':')[0];
                            var InDateMin = InDate.split(' ')[1].split(':')[1];
                            document.getElementById("txtInDate").value = InDate1;
                            document.getElementById("ddlInDateHour").value = InDateHour;
                            document.getElementById("ddlInDateMin").value = InDateMin;
                        }
                        if(item.InUserIDName != "")
                        {
                            $("#UserInUserID").attr("value",item.InUserIDName);//入库人名称
                            $("#HidInUserID").attr("value",item.InUserID);//入库人ID 
                        }
                        if(item.SttlDate != "")
                        {//结算时间
                            var SttlDate = item.SttlDate;
                            var SttlDate1 = SttlDate.split(' ')[0];
                            var SttlDateHour = SttlDate.split(' ')[1].split(':')[0];
                            var SttlDateMin = SttlDate.split(' ')[1].split(':')[1];
                            document.getElementById("txtSttlDate").value = SttlDate1;
                            document.getElementById("ddlSttlDateHour").value = SttlDateHour;
                            document.getElementById("ddlSttlDateMin").value = SttlDateMin;
                        }
                        if(item.SttlUserIDName != "")
                        {
                            $("#UserSttlUserID").attr("value",item.SttlUserIDName);//结算人名称
                            $("#HidSttlUserID").attr("value",item.SttlUserID);//结算人ID
                        }
                        $("#txtCustAddr").attr("value",item.CustAddr);//客户地址
                        $("#txtBackReason").attr("value",item.BackReason);//退货理由描述
                        
                        //合计信息
                        $("#txtCountTotal").attr("value",item.CountTotal);//退货数量合计
                        $("#txtTotalMoney").attr("value",item.TotalPrice);//金额合计
                        $("#txtTotalTaxHo").attr("value",item.Tax);//税额合计
                        $("#txtTotalFeeHo").attr("value",item.TotalFee);//含税金额合计
                        $("#txtDiscount").attr("value",item.Discount);//整单折扣
                        $("#txtDiscountTotal").attr("value",item.DiscountTotal);//折扣金额
                        $("#txtRealTotal").attr("value",item.RealTotal);//折后含税金额
                        $("#txtWairPayTotal").attr("value",item.WairPayTotal);//应退款金额 
                        $("#txtPayedTotal").attr("value",item.PayedTotal);//已退款金额
//                        $("#txtSettleTotal").attr("value",item.SettleTotal);//(本次)结算货款
                        $("#txtWairPayTotalOverage").attr("value",item.WairPayTotalOverage);//应退货款金额
                                      
                        //备注信息
                        $("#txtCreator").attr("value",item.Creator);//制单人id
                        $("#txtCreatorReal").attr("value",item.CreatorName);//制单人名称
                        $("#txtConfirmDate").attr("value",item.CreateDate);//制单时间
                        $("#ddlBillStatus").attr("value",item.BillStatus);//单据状态
                        $("#txtConfirmor").attr("value",item.Confirmor);//确认人id
                        $("#txtConfirmorReal").attr("value",item.ConfirmorName);//确认人名称
                        $("#txtConfirmDate").attr("value",item.ConfirmDate);//确认时间
                        $("#txtCloser").attr("value",item.Closer);//结单人id
                        $("#txtCloserReal").attr("value",item.CloserName);//结单人名称
                        $("#txtCloseDate").attr("value",item.CloseDate);//结单日期
                        $("#txtModifiedUserID").attr("value",item.ModifiedUserID);//最后更新人id
                        $("#txtModifiedUserIDReal").attr("value",item.ModifiedUserID);//最后更新人id显示
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//最后更新时间
                        $("#hfPageAttachment").attr("value",item.Attachment);//附件
                        $("#txtRemark").attr("value",item.Remark);//备注
                        if(item.Attachment!="")
                        {
                            //下载删除显示
                            document.getElementById("divDealResume").style.display = "block";
                            //上传不显示
                            document.getElementById("divUploadResume").style.display = "none"
                        }
                        
                        document.getElementById("divSubSellBackNo").innerHTML=item.BackNo;
                        document.getElementById("divSubSellBackNo").style.display="block";
                        document.getElementById("divInputNo").style.display="none";
                        
                    }
                        
               });
               $.each(msg.data2,function(i,item){
//                    if(item.rejectNo != null && item.rejectNo != "")
                        if(item.ID != null && item.ID != "")
                        {
                            var index = AddSignRow();
                            $("#txtProductID"+index).attr("value",item.ProductID);
                            $("#txtProductNo"+index).attr("value",item.ProductNo);
                            $("#txtProductName"+index).attr("value",item.ProductName);
                            $("#txtstandard"+index).attr("value",item.standard);
                            $("#txtUnitID"+index).attr("value",item.UnitID);
                            $("#txtUnitName"+index).attr("value",item.UnitName);
                            $("#txtProductCount"+index).attr("value",item.ProductCount);
                            $("#txtYBackCount"+index).attr("value",item.YBackCount);
                            $("#txtBackCount"+index).attr("value",item.BackCount);
                            $("#txtUnitPrice"+index).attr("value",item.UnitPrice);
                            $("#txtTaxPrice"+index).attr("value",item.TaxPrice);
                            $("#hiddTaxPrice"+index).attr("value",item.TaxPrice);
                            $("#txtDiscount"+index).attr("value",item.Discount);
                            $("#txtTaxRate"+index).attr("value",item.TaxRate);
                            $("#hiddTaxRate"+index).attr("value",item.TaxRate);
                            $("#txtTotalPrice"+index).attr("value",item.TotalPrice);
                            $("#txtTotalFee"+index).attr("value",item.TotalFee);
                            $("#txtTotalTax"+index).attr("value",item.TotalTax);
                            $("#txtStorageID"+index).attr("value",item.StorageID);
                            $("#txtFromBillID"+index).attr("value",item.FromBillID);
                            $("#txtFromBillNo"+index).attr("value",item.FromBillNo);
                            $("#txtFromLineNo"+index).attr("value",item.FromLineNo);
                            $("#txtRemark"+index).attr("value",item.Remark);
                            
                        }
               });
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}








///添加行
 function AddSignRow()
 { 
        //读取最后一行的行号，存放在txtTRLastIndex文本框中 
        var Flag1 = document.getElementById("ddlSendMode").value;
        var Flag2 = document.getElementById("drpFromType").value;
        
        var hidIsliebiao = document.getElementById("hidIsliebiao").value //为1表示从列表页面过来
        var BillStatusName = document.getElementById("hidBillStatusName").value;//单据状态
        var IsliebiaoSendMode = document.getElementById("hidIsliebiaoSendMode").value;//发货模式
        var IsliebiaoFromType = document.getElementById("hidIsliebiaoFromType").value;//源单类型
 
        var txtTRLastIndex = findObj("txtTRLastIndex",document);
    
        var rowID = parseInt(txtTRLastIndex.value);
       // alert("rowID="+rowID)
        
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
       // alert("signFrame.rows.length"+signFrame.rows.length)
        newTR.id = "ID" + rowID;
        var i=0;
      
        var newNameTD=newTR.insertCell(i++);//添加列:序号
        newNameTD.className="cell";
//        newNameTD.style.display = "none"; 
        newNameTD.id = "newNameTD"+rowID;
//        newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
        newNameTD.innerHTML =GenerateNo(rowID);
              
        var newNameXH=newTR.insertCell(i++);//添加列:选择
        newNameXH.className="tdColInputCenter";
        newNameXH.innerHTML="<input name='chk' id='chk" + rowID + "' size='10' value="+rowID+" type='checkbox'  disabled  onclick=\"Isquanxuan();\"  />";
        
        var newProductNo=newTR.insertCell(i++);//添加列:物品ID
        newProductNo.style.display = "none";      
        newProductNo.innerHTML = "<input name='txtProductID" + rowID + "'  id='txtProductID" + rowID + "' type='text' style='width:100%'  />";//添加列内容
        
        if(hidIsliebiao == "1")
        {
            if(BillStatusName =="制单")
            {//判断从列表页面传来的源单类型
                if(IsliebiaoFromType=="0")
                {//无来源
                    var newProductNo=newTR.insertCell(i++);//添加列:物品编号popProductInfoSubUC.ShowList
                    newProductNo.className="proID"; 
                    newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly   onclick=\"GetProduct("+rowID+")\"  type='text' style='width:100%'  readonly />";//添加列内容
                }
                else
                {//销售订单
                    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
                    newProductNo.className="proID"; 
                    newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' type='text' style='width:100%'  readonly disabled/>";//添加列内容
                }
            }
            else if(BillStatusName =="")
            {
                var newProductNo=newTR.insertCell(i++);//添加列:物品编号
                newProductNo.className="proID";        
        //        newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly onclick=\"popTechObj.ShowList('txtProductNo"+rowID+"');\" type='text' style='width:100%'  readonly />";//添加列内容
                if(Flag2 == "1")
                {
                    newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' type='text' style='width:100%'  readonly disabled/>";//添加列内容
                }
                else if(Flag2 != "1")
                {
                    newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly  onclick=\"GetProduct("+rowID+")\"  type='text' style='width:100%'  readonly />";//添加列内容
                }  
            }
            else
            {
                var newProductNo=newTR.insertCell(i++);//添加列:物品编号
                newProductNo.className="proID"; 
                newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' type='text' style='width:100%'  readonly disabled/>";//添加列内容
            }
        }
        else
        {
            var newProductNo=newTR.insertCell(i++);//添加列:物品编号
            newProductNo.className="proID";        
    //        newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly onclick=\"popTechObj.ShowList('txtProductNo"+rowID+"');\" type='text' style='width:100%'  readonly />";//添加列内容
            if(Flag2 == "1")
            {
                newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' type='text' style='width:100%'  readonly disabled/>";//添加列内容
            }
            else if(Flag2 != "1")
            {
                newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' readonly onclick=\"GetProduct("+rowID+")\"  type='text' style='width:100%'  readonly />";//添加列内容
            }  
        }
        
        var newProductName=newTR.insertCell(i++);//添加列:物品名称
        newProductName.className="proID";
        newProductName.innerHTML = "<input name='txtProductName" + rowID + "' id='txtProductName" + rowID + "' type='text'  style='width:100%'  readonly  disabled/>";//添加列内容
        
        var newProductName=newTR.insertCell(i++);//添加列:规格(从物品表中带来显示，不往明细表中存)
        newProductName.className="proID";
        newProductName.innerHTML = "<input name='txtstandard" + rowID + "' id='txtstandard" + rowID + "' type='text'  style='width:100%'  readonly  disabled/>";//添加列内容
        
        var newUnitID=newTR.insertCell(i++);//添加列:单位ID
        newUnitID.style.display = "none";
        newUnitID.innerHTML = "<input name='txtUnitID"+rowID+"' id='txtUnitID" + rowID + "'style='width:10%' type='text'  class='tdinput' readonly />";//添加列内容
        
        var newUnitID=newTR.insertCell(i++);//添加列:单位
        newUnitID.className="proID";
        newUnitID.innerHTML = "<input name='txtUnitName" + rowID + "' id='txtUnitName" + rowID + "' type='text' style='width:100%' onclick=\"popUnitObj.ShowList("+rowID+");\"  readonly disabled/>";//添加列内容
//        newUnitID.innerHTML = "<input name='txtUnitName" + rowID + "' id='txtUnitName" + rowID + "' type='text' style='width:100%'  readonly/>";//添加列内容
        
        var newProductCount=newTR.insertCell(i++);//添加列:发货数量
        newProductCount.className="proID";
        newProductCount.innerHTML = "<input name='txtProductCount" + rowID + "'  id='txtProductCount" + rowID + "'style='width:100%'  readonly  disabled type='text'/>";//添加列内容
        
        var newYBackCount=newTR.insertCell(i++);//添加列:已退货数量
//        newYBackCount.className="proID";
        newYBackCount.style.display = "none";
        newYBackCount.innerHTML = "<input name='txtYBackCount" + rowID + "'  id='txtYBackCount" + rowID + "'style='width:100%'  disabled type='text'/>";//添加列内容
        
        
        if(hidIsliebiao == "1")
        {
            if(BillStatusName =="制单")
            {//判断从列表页面传来的源单类型
                var newBackCount=newTR.insertCell(i++);//添加列:退货数量
                newBackCount.className="proID";
                newBackCount.innerHTML = "<input name='txtBackCount" + rowID + "'  id='txtBackCount" + rowID + "'  disabled onchange=\"Number_round(this,2)\"   onblur=\"fnTotalInfo();\" style='width:100%'  type='text'/>";//添加列内容 
            }
            else if(BillStatusName =="")
            {
                var newBackCount=newTR.insertCell(i++);//添加列:退货数量
                newBackCount.className="proID";
                newBackCount.innerHTML = "<input name='txtBackCount" + rowID + "'  id='txtBackCount" + rowID + "' disabled  onchange=\"Number_round(this,2)\"   onblur=\"fnTotalInfo();\" style='width:100%'  type='text'/>";//添加列内容 
            }
            else
            {
                var newBackCount=newTR.insertCell(i++);//添加列:退货数量
                newBackCount.className="proID";
                newBackCount.innerHTML = "<input name='txtBackCount" + rowID + "'  id='txtBackCount" + rowID + "'  style='width:100%'  readonly  disabled  type='text'/>";//添加列内容 
            }
        }
        else
        {
            var newBackCount=newTR.insertCell(i++);//添加列:退货数量
            newBackCount.className="proID";
            newBackCount.innerHTML = "<input name='txtBackCount" + rowID + "'  id='txtBackCount" + rowID + "'  disabled  onchange=\"Number_round(this,2)\"   onblur=\"fnTotalInfo();\" style='width:100%'  type='text'/>";//添加列内容 
        }
            
        var newUnitPrice=newTR.insertCell(i++);//添加列:单价
        newUnitPrice.className="proID";
        newUnitPrice.innerHTML = "<input name='txtUnitPrice" + rowID + "' id='txtUnitPrice" + rowID + "' type='text'   style='width:100%'      disabled/>";//添加列内容
        
        var newUnitPrice=newTR.insertCell(i++);//添加列:含税价
        newUnitPrice.className="proID";
        newUnitPrice.innerHTML = "<input name='txtTaxPrice" + rowID + "' id='txtTaxPrice" + rowID + "' type='text'   style='width:100%'      disabled/> <input type=\"hidden\"  id='hiddTaxPrice" + rowID + "'/>";//添加列内容
        
//        var newTaxPricHide=newTR.insertCell(i++);//添加列:含税价隐藏
//        newTaxPricHide.style.display = "none";
//        newTaxPricHide.innerHTML = "<input id='OrderTaxPriceHide" + rowID + "'type='text'  style='width:98%'   />";
        
        var newUnitPrice=newTR.insertCell(i++);//添加列:折扣
        newUnitPrice.className="proID";
        newUnitPrice.innerHTML = "<input name='txtDiscount" + rowID + "' id='txtDiscount" + rowID + "' type='text'   style='width:100%'    readonly  disabled/>";//添加列内容
        
        var newUnitPrice=newTR.insertCell(i++);//添加列:税率
        newUnitPrice.className="proID";
        newUnitPrice.innerHTML = "<input name='txtTaxRate" + rowID + "' id='txtTaxRate" + rowID + "' type='text'   style='width:100%'    readonly  disabled/> <input type=\"hidden\"  id='hiddTaxRate" + rowID + "'/>";//添加列内容
        
        var newTotalPrice=newTR.insertCell(i++);//添加列:金额
        newTotalPrice.className="proID";
        newTotalPrice.innerHTML = "<input name='txtTotalPrice" + rowID + "' id='txtTotalPrice" + rowID + "' type='text'  style='width:100%'   readonly  disabled/>";//添加列内容
        $("#txtTotalPrice" + rowID).onfocus = "TotalPrice1();"
        
        var newUnitPrice=newTR.insertCell(i++);//添加列:含税金额
        newUnitPrice.className="proID";
        newUnitPrice.innerHTML = "<input name='txtTotalFee" + rowID + "' id='txtTotalFee" + rowID + "' type='text'   style='width:100%'    readonly  disabled/>";//添加列内容
        
        var newUnitPrice=newTR.insertCell(i++);//添加列:税额
        newUnitPrice.className="proID";
        newUnitPrice.innerHTML = "<input name='txtTotalTax" + rowID + "' id='txtTotalTax" + rowID + "' type='text'   style='width:100%'    readonly  disabled/>";//添加列内容     
        
        if(hidIsliebiao == "1")
        {
            if(BillStatusName =="制单")
            {//判断从列表页面传来的源单类型
                if(IsliebiaoSendMode=="1")
                {//分店发货
                    var newApplyReason=newTR.insertCell(i++);//仓库下拉选择
                    newApplyReason.className="proID";
                    newApplyReason.innerHTML="<select  style='width:100%' class='tdinput' id='txtStorageID" + rowID + "'     disabled>" + document.getElementById("drpStorageID").innerHTML + "</select>";
                }
                else
                {//总部发货
                    var newApplyReason=newTR.insertCell(i++);//仓库下拉选择
                    newApplyReason.className="proID"; 
                    newApplyReason.innerHTML="<select  style='width:100%' class='tdinput'disabled id='txtStorageID" + rowID + "'>" + document.getElementById("drpStorageID").innerHTML + "</select>"; 
                }
            }
            else if(IsliebiaoSendMode=="")
            {
                 var newApplyReason=newTR.insertCell(i++);//仓库下拉选择
                newApplyReason.className="proID";
        //        newApplyReason.innerHTML="<select  style='width:100%' class='tdinput' id='txtStorageID" + rowID + "'>" + document.getElementById("drpStorageID").innerHTML + "</select>";
                if(Flag1 == "2")
                {      
                    newApplyReason.innerHTML="<select  style='width:100%' class='tdinput' disabled id='txtStorageID" + rowID + "'>" + document.getElementById("drpStorageID").innerHTML + "</select>"; 
                }
                else if(Flag1 != "2")
                {
                    newApplyReason.innerHTML="<select  style='width:100%' class='tdinput' disabled id='txtStorageID" + rowID + "' >" + document.getElementById("drpStorageID").innerHTML + "</select>";
                }  
            }
            else
            {
                var newApplyReason=newTR.insertCell(i++);//仓库下拉选择
                newApplyReason.className="proID"; 
                newApplyReason.innerHTML="<select  style='width:100%' class='tdinput' id='txtStorageID" + rowID + "'     disabled>" + document.getElementById("drpStorageID").innerHTML + "</select>";
            }
        }
        else
        {
            var newApplyReason=newTR.insertCell(i++);//仓库下拉选择
            newApplyReason.className="proID";
    //        newApplyReason.innerHTML="<select  style='width:100%' class='tdinput' id='txtStorageID" + rowID + "'>" + document.getElementById("drpStorageID").innerHTML + "</select>";
            if(Flag1 == "2")
            {      
                newApplyReason.innerHTML="<select  style='width:100%' class='tdinput' disabled id='txtStorageID" + rowID + "'>" + document.getElementById("drpStorageID").innerHTML + "</select>"; 
            }
            else if(Flag1 != "2")
            {
                newApplyReason.innerHTML="<select  style='width:100%' class='tdinput' disabled id='txtStorageID" + rowID + "' >" + document.getElementById("drpStorageID").innerHTML + "</select>";
            }  
        }
        
        var newFromBillID=newTR.insertCell(i++);//添加列:源单ID
        newFromBillID.style.display = "none";
        newFromBillID.innerHTML = "<input name='txtFromBillID" + rowID + "' id='txtFromBillID" + rowID + "' type='text' style='width:100%'   readonly />";//添加列内容
        
        var newFromBillID=newTR.insertCell(i++);//添加列:源单编号
        newFromBillID.className = "proID";
        newFromBillID.innerHTML = "<input name='txtFromBillNo" + rowID + "' id='txtFromBillNo" + rowID + "' type='text'  style='width:100%'  readonly   disabled/>";//添加列内容
        
        var newFromLineNo=newTR.insertCell(i++);//添加列:源单序号
        newFromLineNo.className="proID";
        newFromLineNo.innerHTML = "<input name='txtFromLineNo" + rowID + "' id='txtFromLineNo" + rowID + "' type='text'  style='width:100%'  readonly   disabled/>";//添加列内容

        var newRemark=newTR.insertCell(i++);//添加列:备注
        newRemark.className = "proID";
        newRemark.innerHTML = "<input name='txtRemark" + rowID + "' id='txtRemark" + rowID + "' type='text' style='width:100%' />";//添加列内容
        
        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
        return rowID;
}



function GetProduct(rowID)
{
    if($("#ddlSendMode").val() == "1")
    {//分店发货
        popProductInfoSubUC.ShowList(rowID)
    }
    else
    {
        popTechObj.ShowList(rowID);
    }
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




  /********************
 * 取窗口滚动条高度 
 ******************/
function getScrollTop()
{
    var scrollTop=0;
    if(document.documentElement&&document.documentElement.scrollTop)
    {
        scrollTop=document.documentElement.scrollTop;
    }
    else if(document.body)
    {
        scrollTop=document.body.scrollTop;
    }
    return scrollTop;
}



function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)//选择供应商带出部分信息
{  
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    document.getElementById("txtHiddenProviderID1").value = providername;//当选择源单类型时，用于显示供应商并且不允许再修改
    document.getElementById("drpTakeType").value = taketype;
    document.getElementById("drpCarryType").value = carrytype;
    document.getElementById("drpPayType").value= paytype;
    
    closeProviderdiv();
}


//选择分店销售订单带出相关信息
function FillFromSubSellOrder(id,orderno,sendmode,outdate,outuserid,outusername,custname,custtel,custmobile,custaddr)
{  
if(document.getElementById("txtOrderID").value == "")
    {
        document.getElementById("HidOrderID").value = id;
        document.getElementById("txtOrderID").value = orderno;
        document.getElementById("ddlSendMode").value = sendmode; 
        if(outdate != "")
        {
            var outdate1 = outdate.split(' ')[0];
            var outdatehour = outdate.split(' ')[1].split(':')[0];
            var outdatemin = outdate.split(' ')[1].split(':')[1];
            document.getElementById("txtOutDate").value = outdate1;
            document.getElementById("ddlOutDateHour").value = outdatehour;
            document.getElementById("ddlOutDateMin").value = outdatemin;
        }
        document.getElementById("HidOutUserID").value = outuserid;
        document.getElementById("UserOutUserID").value = outusername;
        document.getElementById("txtCustName").value = custname;
        document.getElementById("txtCustTel").value = custtel;
        document.getElementById("txtCustMobile").value = custmobile;
        document.getElementById("txtCustAddr").value = custaddr;
        
        closeSubSellOrderdiv();
    }
    else
    {
        if(document.getElementById("txtOrderID").value == orderno)
        {
            closeSubSellOrderdiv();
        }
        else
        {
            DeleteSignRow100();
            fnTotalInfo();
            document.getElementById("HidOrderID").value = id;
            document.getElementById("txtOrderID").value = orderno;
            document.getElementById("ddlSendMode").value = sendmode; 
            if(outdate != "")
            {
                var outdate1 = outdate.split(' ')[0];
                var outdatehour = outdate.split(' ')[1].split(':')[0];
                var outdatemin = outdate.split(' ')[1].split(':')[1];
                document.getElementById("txtOutDate").value = outdate1;
                document.getElementById("ddlOutDateHour").value = outdatehour;
                document.getElementById("ddlOutDateMin").value = outdatemin;
            }
            document.getElementById("HidOutUserID").value = outuserid;
            document.getElementById("UserOutUserID").value = outusername;
            document.getElementById("txtCustName").value = custname;
            document.getElementById("txtCustTel").value = custtel;
            document.getElementById("txtCustMobile").value = custmobile;
            document.getElementById("txtCustAddr").value = custaddr;
            
            closeSubSellOrderdiv();
        }
    }
}


//选择分店销售订单明细来源
function FillFromSubSellOrderUC(deptid,deptname,orderno,fromLineno,frombillno,frombillid,productid,productno,productname,standard,
                       unitid,unitname,unitprice,taxprice,discount,taxrate,storageid,storagename,productcount,remark,ybackcount)
{
    if(!ExistFromBill(productid,frombillid,fromLineno))
        {
            var Index = findObj("txtTRLastIndex",document).value;
            AddSignRow();
            var ProductID = 'txtProductID'+Index;
            var ProductNo = 'txtProductNo'+Index;
            var ProductName = 'txtProductName'+Index;
            var Standard = 'txtstandard'+Index;
            var UnitID = 'txtUnitID'+Index;
            var UnitName = 'txtUnitName'+Index;
            var ProductCount = 'txtProductCount'+Index;
            var UnitPrice = 'txtUnitPrice'+Index;
            var TaxPrice = 'txtTaxPrice'+Index;
            var Discount = 'txtDiscount'+Index;
            var TaxRate = 'txtTaxRate'+Index;
            var Remark = 'txtRemark'+Index;
            var FromBillID = 'txtFromBillID'+Index;
            var FromBillNo = 'txtFromBillNo'+Index;
            var FromLineNo = 'txtFromLineNo'+Index;
            var StorageID = 'txtStorageID'+Index;
            var HiddTaxPrice = 'hiddTaxPrice'+Index;
            var HiddTaxRate = 'hiddTaxRate'+Index;
            var YBackCount = 'txtYBackCount'+Index;
            
            document.getElementById(ProductID).value = productid;
            document.getElementById(ProductNo).value = productno;
            document.getElementById(ProductName).value = productname;
            document.getElementById(Standard).value = standard;
            document.getElementById(UnitID).value = unitid;
            document.getElementById(UnitName).value = unitname;
            document.getElementById(ProductCount).value = FormatAfterDotNumber(productcount,2);
            document.getElementById(UnitPrice).value = FormatAfterDotNumber(unitprice,2);
            document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice,2);
            document.getElementById(Discount).value = FormatAfterDotNumber(discount,2);
            document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate,2);
            document.getElementById(Remark).value = remark;
            document.getElementById(FromBillID).value = frombillid;
            document.getElementById(FromBillNo).value = frombillno;
            document.getElementById(FromLineNo).value = fromLineno;
            document.getElementById(StorageID).value = StorageID;
            document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice,2);
            document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate,2);
            document.getElementById(YBackCount).value = FormatAfterDotNumber(ybackcount,2);
            
           
        }
//        document.getElementById("txtProviderID").style.display="none";
//        document.getElementById("txtHiddenProviderID1").style.display="block";
        closeSubSellOrderUCdiv();
        
        var isAddTax = document.getElementById("chkisAddTax").checked;
        var signFrame = findObj("dg_Log",document);
        for (i = 1; i < signFrame.rows.length; i++)
        {
            if (signFrame.rows[i].style.display != "none")
            {
                var rowIndex = i;
                if(isAddTax == true)
                {//是增值税则
                    document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("hiddTaxPrice"+rowIndex).value;//含税价等于隐藏域含税价
                    document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value;//税率等于隐藏域税率
                    document.getElementById("chkisAddTaxText1").style.display="inline";
                    document.getElementById("chkisAddTaxText2").style.display="none";
                }
                else
                {
                    document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("txtUnitPrice"+rowIndex).value;//含税价等于单价
                    document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                    document.getElementById("chkisAddTaxText1").style.display="none";
                    document.getElementById("chkisAddTaxText2").style.display="inline";
                }
            }
        }
}





//判断是否有相同记录有返回true，没有返回false
function ExistFromBill(productid,frombillid,fromLineno)
{
    var signFrame = document.getElementById("dg_Log");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var productid1 = document.getElementById("txtProductID"+i).value;
        var frombillid1 = document.getElementById("txtFromBillID"+i).value;
        var fromlineno1 = document.getElementById("txtFromLineNo"+i).value;
        
        if((signFrame.rows[i].style.display!="none")&&(productid1 == productid)&&(frombillid1 == frombillid)&&(fromlineno1 == fromLineno))
        {
            return true;
        } 
    }
    return false;
}


function FillFromArrive(productid,productno,productname,standard,unitid,unitname,productcount,unitprice,taxprice,discount,
                       taxrate,totalprice,totalfee,totaltax,remark,frombillid,frombillno,fromLineno)
{
    if(!ExistFromBill(productid,frombillid,fromLineno))
        {
            var Index = findObj("txtTRLastIndex",document).value;
            AddSignRow();
            var ProductID = 'txtProductID'+Index;
            var ProductNo = 'txtProductNo'+Index;
            var ProductName = 'txtProductName'+Index;
            var Standard = 'txtstandard'+Index;
            var UnitID = 'txtUnitID'+Index;
            var UnitName = 'txtUnitName'+Index;
            var ProductCount = 'txtProductCount'+Index;
            var UnitPrice = 'txtUnitPrice'+Index;
            var TaxPrice = 'txtTaxPrice'+Index;
            var Discount = 'txtDiscount'+Index;
            var TaxRate = 'txtTaxRate'+Index;
            var Remark = 'txtRemark'+Index;
            var FromBillID = 'txtFromBillID'+Index;
            var FromBillNo = 'txtFromBillNo'+Index;
            var FromLineNo = 'txtFromLineNo'+Index;
            var HiddTaxPrice = 'hiddTaxPrice'+Index;
            var HiddTaxRate = 'hiddTaxRate'+Index;
           
            
            document.getElementById(ProductID).value = productid;
            document.getElementById(ProductNo).value = productno;
            document.getElementById(ProductName).value = productname;
            document.getElementById(Standard).value = standard;
            document.getElementById(UnitID).value = unitid;
            document.getElementById(UnitName).value = unitname;
            document.getElementById(ProductCount).value = FormatAfterDotNumber(productcount,2);
            document.getElementById(UnitPrice).value = FormatAfterDotNumber(unitprice,2);
            document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice,2);
            document.getElementById(Discount).value = discount;
            document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate,2);
            document.getElementById(Remark).value = remark;
            document.getElementById(FromBillID).value = frombillid;
            document.getElementById(FromBillNo).value = frombillno;
            document.getElementById(FromLineNo).value = fromLineno;
            document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice,2);
            document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate,2);
            
           
        }
        document.getElementById("txtProviderID").style.display="none";
        document.getElementById("txtHiddenProviderID1").style.display="block";
        closeMaterialdiv();
        
        var isAddTax = document.getElementById("chkIsAddTax").checked;
        var signFrame = findObj("dg_Log",document);
        for (i = 1; i < signFrame.rows.length; i++)
        {
            if (signFrame.rows[i].style.display != "none")
            {
                var rowIndex = i;
                if(isAddTax == true)
                {//是增值税则
                    document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("hiddTaxPrice"+rowIndex).value;//含税价等于隐藏域含税价
                    document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value;//税率等于隐藏域税率
                    document.getElementById("chkisAddTaxText1").style.display="inline";
                    document.getElementById("chkisAddTaxText2").style.display="none";
                }
                else
                {
                    document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("txtUnitPrice"+rowIndex).value;//含税价等于单价
                    document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                    document.getElementById("chkisAddTaxText1").style.display="none";
                    document.getElementById("chkisAddTaxText2").style.display="inline";
                }
            }
        }
}




