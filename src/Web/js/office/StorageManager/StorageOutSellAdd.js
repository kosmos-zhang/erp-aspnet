//---------Start 其他出库详细信息和明细列表 --------
var OutNoID = document.getElementById('txtIndentityID').value;
$(document).ready(function()
{
    if(OutNoID>0)
    {
    
        LoadDetailInfo(OutNoID);
        document.getElementById("t_Edit").style.display="";
    }
    else
    {
        GetExtAttr('officedba.StorageOutSell',null);  
        document.getElementById("t_Add").style.display="";
        try
        {
            $("#btn_save").show();
            $("#btnPageFlowConfrim").show();
            $("#btn_Unclic_Close").show();
            $("#btn_Unclick_CancelClose").show();
        }
        catch(e){}  
    }
    IsDisplayPriceControl();
});
//是否显示单价金额
function IsDisplayPriceControl()
{
    if($("#IsDisplayPrice").val()=="true")
    {
    
        $("#TotalMoneyFlag").show();
        $("#txtTotalPrice").show();
        $("#td_outprict").show();
        $("#td_outmoney").show();
    }
    else
    {
        $("#TotalMoneyFlag").hide();
        $("#txtTotalPrice").hide();
        $("#td_outprict").hide();
        $("#td_outmoney").hide();
    }
    if($("#HiddenMoreUnit").val()=="False")
        {
            $("#baseuint").hide();
            $("#basecount").hide();       
        }
        else
        {
            $("#baseuint").show();
            $("#basecount").show();
        }
}

function LoadDetailInfo(OutNoID)
{
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/StorageManager/StorageOutSellInfo.ashx?Act=&ID="+OutNoID,//目标地址
       cache:false,          
       success: function(msg){
                //数据获取完毕，填充页面据显示
                        GetExtAttr('officedba.StorageOutSell',msg.data[0]); //获取扩展属性并填充值 
                        document.getElementById('lbOutNo').innerHTML=msg.data[0].OutNo;
                        document.getElementById('txtTitle').value = msg.data[0].Title;
                        document.getElementById('ddlFromType').value = msg.data[0].FromType;
                        $("#txtFromBillID").attr("title",msg.data[0].FromBillID);//
                        $("#txtFromBillID").val(msg.data[0].FromBillNo);
                        $("#txtCust").val(msg.data[0].CustName);//客户
                        $("#txtSellDept").val(msg.data[0].SellDeptName);//销售部门
                        $("#txtSeller").val(msg.data[0].SellerName);
                        $("#txtSendAddress").val(msg.data[0].SendAddr);
                        $("#txtArrAddress").val(msg.data[0].ReceiveAddr);
                        
                        $("#txtSenderID").val(msg.data[0].Sender);
                        $("#UserSender").val(msg.data[0].SenderName);
                        document.getElementById('UserTransactor').value = msg.data[0].TransactorName;
                        $("#txtTransactorID").val(msg.data[0].Transactor);//出库人ID
                        document.getElementById('txtOutDate').value = msg.data[0].OutDate;
                        $("#txtDeptID").val(msg.data[0].DeptID);
                        $("#DeptName").val(msg.data[0].DeptName);
                        $("#txtSummary").val(msg.data[0].Summary);//.ToFixed($("#HiddenPoint").val());
                        document.getElementById('txtTotalPrice').value = (parseFloat(msg.data[0].A_TotalPrice)).toFixed($("#HiddenPoint").val());//出库金额合计
                        document.getElementById('txtTotalCount').value = parseFloat(msg.data[0].CountTotal).toFixed($("#HiddenPoint").val());
                        document.getElementById('txtRemark').value = msg.data[0].Remark;
                        document.getElementById('txtCreator').value = msg.data[0].CreatorName;
                        $("#txtCreator").attr("title",msg.data[0].Creator);
                        document.getElementById('txtCreateDate').value = msg.data[0].CreateDate;
                        document.getElementById('sltBillStatus').value = msg.data[0].BillStatus;
                        document.getElementById('txtConfirmor').value = msg.data[0].ConfirmorName;
                        document.getElementById('txtConfirmDate').value = msg.data[0].ConfirmDate;
                        document.getElementById('txtCloser').value = msg.data[0].CloserName;
                        $("#txtCloser").attr("title",msg.data[0].Closer);
                        document.getElementById('txtCloseDate').value = msg.data[0].CloseDate;
                        document.getElementById('txtModifiedUserID').value = msg.data[0].ModifiedUserName;
                        document.getElementById('txtModifiedDate').value = msg.data[0].ModifiedDate;
                        document.getElementById('txtIndentityID').value = msg.data[0].ID;
                        
                        var aaa = msg.data[0].CanViewUser.replace(",","");
                        var bbb = aaa.lastIndexOf(",");
                        var ccc = aaa.slice(0,bbb);                        
                        $("#txtCanUserID").val(ccc);
                        $("#txtCanUserName").val(msg.data[0].CanViewUserName);
                        
                $.each(msg.data,function(i,item){
//                        if(item.ProductID!=null && item.ProductID!='')
//                        {
                            FillSignRow(i,item.OutNo,item.ProductID,item.ProductNo
                            ,item.ProductName,item.Specification,item.Package,item.UnitID,item.HiddenUnitID,item.UnitPrice,item.B_TotalPrice
                            ,item.FromBillCount,item.NotOutCount,item.ProductCount,item.FromBillNo
                            ,item.FromLineNo,item.DetaiRemark,item.StorageID,item.MinusIs,item.UseCount,item.OutCount,item.BatchNo,item.UsedUnitID,item.UsedUnitCount,item.UsedPrice,item.ExRate,item.IsBatchNo);
//                        }
               });
               if(document.getElementById('sltBillStatus').value==1)//根据单据状态显示按钮
               {
                    try
                    {
                        $("#btn_save").show();
                        $("#Confirm").show();
                        $("#btn_Unclic_Close").show();
                        $("#btn_Unclick_CancelClose").show();
                    }
                    catch(e){}    
               }
               else if(document.getElementById('sltBillStatus').value==2)
                {
                    try
                    {
                        $("#btn_save").hide();
                        $("#Confirm").hide();
                        $("#btn_UnClick_bc").show();
                        $("#btnPageFlowConfrim").show();
                        $("btn_Unclick_CancelClose").show();
                        $("#btn_Close").show();
                         $("#btn_Unclic_Close").hide();
                         $("#btn_CancelClose").hide();
                         $("#btn_Unclick_CancelClose").show();
                     }
                     catch(e){}    
                }
               else
               {
                    try
                    {
                        $("#btn_save").hide();
                        $("#Confirm").hide();
                        $("#btn_UnClick_bc").show();
                        $("#btnPageFlowConfrim").show();
                        $("#btn_Close").hide();
                        $("#btn_Unclic_Close").show();
                        $("#btn_CancelClose").show();
                        $("#btn_Unclick_CancelClose").hide();
                    }
                    catch(e){}  
                    
               }
              },
       error: function() {},
       complete:function(){}
       });
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
//    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
//    alert(data.ExtField1);
//    var arrKey = strKey.split('|');
//    if(typeof(data)!='undefined')
//    {
//       $.each(data,function(i,item){
//            for (var t = 0; t < arrKey.length; t++) {
//                //不为空的字段名才取值
//                if ($.trim(arrKey[t]) != '') {
//                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);

//                }
//            }

//       });
//    }
//}
}

//---------End 其他出库详细信息和明细列表 ----------

//---------Start 其他出库保存 ----------
function Fun_Save_StorageOutSell()
{   
    CountBaseNum();
    var bmgz="";
    var Flag = true;

        var fieldText = "";
        var msgText = "";
        var isFlag = true;
    
         var txtIndentityID = $("#txtIndentityID").val();
         if(txtIndentityID=="0")//新建
         {
            if($("#txtOutNo_ddlCodeRule").val()=="")//手工输入
            {
                txtOutNo=$("#txtOutNo_txtCode").val();
                bmgz="sd";
                if (txtOutNo == "")
                {
                    isFlag = false;
                    fieldText += "单据编号|";
   		            msgText += "请输入单据编号|";
                }
                else if(!CodeCheck($("#txtOutNo_txtCode").val()))
                {
                    isFlag = false;
                    fieldText = fieldText + "单据编号|";
	                msgText = msgText +  "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
                }
            }
            else
            {
                
                txtOutNo=$("#txtOutNo_ddlCodeRule").val();
                bmgz="zd";
            }
         }
         else
         {
            txtOutNo=document.getElementById("lbOutNo").innerHTML;
         } 
         var txtTitle=$("#txtTitle").val(); 
         var ddlFromType=$("#ddlFromType").val(); 
         var txtFromBillID=$("#txtFromBillID").attr("title");//源单ID
         var txtSender=$("#txtSenderID").val();//经办人
         var txtOutDept=$("#txtDeptID").val();//部门ID
         var txtTransactor=$("#txtTransactorID").val();//出库人ID
         var txtOutDate=$("#txtOutDate").val();
         var txtSummary=$("#txtSummary").val();
         var txtTotalPrice=$("#txtTotalPrice").val();
         var txtTotalCount=$("#txtTotalCount").val();
         var txtRemark=$("#txtRemark").val();
         var sltBillStatus=$("#sltBillStatus").val();
         var hiddenValue = $("#txtOutNoHidden").val();
         //可查看人员
        var CanViewUser = $("#txtCanUserID").val();
        var CanViewUserName = $("#txtCanUserName").val(); 

    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    } 
    
    if(hiddenValue == '0')
    {
        isFlag=false;
        fieldText=fieldText + "单据编号|";
        msgText = msgText +  "单据编号不允许重复|";
    } 
   
    if(strlen(txtOutNo)>50)
    {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
   		msgText = msgText +  "仅限于50个字符以内|";
    }

//    if(txtTitle.Trim().length == "")
//    {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//   		msgText = msgText +  "请输入主题|";
//    }
    
    if(strlen(txtTitle)>100)
    {
        isFlag = false;
        fieldText = fieldText + "主题|";
   		msgText = msgText +  "仅限于100个字符以内|";
    }
    
    if($("#txtFromBillID").val()==""||$("#txtFromBillID").attr("title")=="undefined")
    {
        isFlag = false;
        fieldText = fieldText + "源单编号|";
        msgText = msgText + "请选择源单|";
    }
    
    if(txtTransactor=="")
    {
        isFlag=false;
        fieldText=fieldText + "出库人|";
        msgText =msgText+"请选择出库人|";
    }
    
    if(txtOutDate=="")
    {
        isFlag=false;
        fieldText=fieldText + "出库时间|";
        msgText =msgText+"请选择出库时间|";
    }
    if(txtOutDate!="")
    {
        if(StringToDate(txtOutDate)>StringToDate($("#HiddenNow").val()))
        {
            isFlag=false;
            fieldText=fieldText + "出库时间|";
            msgText =msgText+"出库时间不能大于当前时间|";
        }
    }
    if(strlen(txtSummary)>200)
    {
        isFlag = false;
        fieldText = fieldText + "摘要|";
   		msgText = msgText +  "仅限于200个字符以内|";
    }
    
    if(strlen(txtRemark)>800)
    {
        isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText +  "仅限于800个字符以内|";
    }
    //明细验证
    var signFrame = findObj("dg_Log",document);
    if(signFrame.rows.length<=1)
      {
         isFlag = false;
         fieldText = fieldText +  "明细信息|";
         msgText = msgText + "明细信息不能为空|";  
      }
    var ifProductCount="0";
    var ifRemark="0";
    var ifbig = "0";//是否大于应收数量
    var iszero = "0"; //是否大于应收数量
    var zeronum = null; //第几行单价为零
    var rownum=null;//第几行大于源单数量
    for(i=1;i<signFrame.rows.length;i++)
        {         
            var ProductCount="OutCount_SignItem_TD_Text_" + i;
            var Remark="Remark_SignItem_TD_Text_"+i;
            if(document.getElementById(ProductCount).value==""||parseFloat(document.getElementById(ProductCount).value)<=0||document.getElementById(ProductCount).value=="0.00")
            {
                 ifProductCount="1";
            }
            if(strlen($("#"+Remark).val())>200)
            {
                ifRemark="1";
            }
        }
    for(i=1;i<signFrame.rows.length;i++)
    {
        var ProductCount="OutCount_SignItem_TD_Text_" + i;
        var NotOutCount="NotOutCount_SignItem_TD_Text_" + i;
        if(parseFloat(document.getElementById(ProductCount).value)>parseFloat(document.getElementById(NotOutCount).value))
        {
            ifbig="1";rownum=i;
            break;
        }
    }
    if($("#HiddenIsZero").val()=="False")
    {
            for (i = 1; i < signFrame.rows.length; i++) 
            {
                var Price = document.getElementById("UnitPrice_SignItem_TD_Text_" + i).value;
                if (parseFloat(Price) <= 0) 
                {
                    iszero = "1"; zeronum = i;
                    break;
                }
             }
     }
    if(ifProductCount=="1")
    {
        isFlag = false;
        fieldText = fieldText +  "出库数量|";
        msgText = msgText + "请输入有效的数值（大于0）|";  
    }
    if(ifbig=="1")
    {
        isFlag = false;
        fieldText = fieldText +  "出库数量|";
        msgText = msgText + "第"+rownum+"行出库数量不能大于未出库数量|";  
    }
    if($("#IsDisplayPrice").val()=="true")
    {
        if (iszero == "1") 
        {
            isFlag = false;
            fieldText = fieldText + "出库单价|";
            msgText = msgText + "第" + zeronum + "行出库单价不允许为零|";
        }
    }
    
    if(ifRemark=="1")
    {
        isFlag = false;
        fieldText = fieldText +  "明细备注|";
        msgText = msgText + "仅限于200个字符以内|";  
    }
    
    
    var List_TB=findObj("dg_Log",document);
    var rowlength=List_TB.rows.length;
    for(var i=1;i<rowlength-1;i++)
    {
        for(var j=i+1;j<rowlength;j++)
        {
            var ProductNoi="ProductNo_SignItem_TD_Text_" + i;
            var ProductNoj="ProductNo_SignItem_TD_Text_" + j;
            var StorageIDi=" StorageID_SignItem_TD_Text_"+i;
            var StorageIDj=" StorageID_SignItem_TD_Text_"+j;
            if($("#"+ProductNoi).val()==$("#"+ProductNoj).val()&&$("#"+StorageIDi).val()==$("#"+StorageIDj).val())
            {
                isFlag = false;
                fieldText = fieldText +  "明细信息|";
                msgText = msgText + "明细中不允许存在重复记录|"; 
                break;
            }
        }
    }
    
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    
    else
    {
    
        //明细数组
      
        var DetailProductID = new Array();
        var DetailSortNo = new Array();//序号
        var DetailStorageID = new Array();
        var DetailFromLineNo = new Array();
        var DetailProductCount = new Array();//实际数量
        var DetailUnitID = new Array();//实际单位(改造前就没加)
        var DetailUnitPrice = new Array();//实际单价
        var DetailTotalPrice = new Array();
        var DetailRemark = new Array();
        var DetailPackage = new Array();
        
        var DetailBaseUnitID = new Array();//基本单位
        var DetailBaseCount = new Array();//基本数量
        var DetailBasePrice = new Array();//基本单价
        var DetailExtRate = new Array();//比率
        
        var DetailBatchNo = new Array();//批次
        
        /*---------明细数组-----------*/
        
        var signFrame = findObj("dg_Log",document); 
        var count = signFrame.rows.length;//有多少行
        for(var i=1;i<count;i++)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                var objProductNo = 'ProductNo_SignItem_TD_Text_'+(i);
                var objSortNo = 'SignItem_TD_Index_'+(i);
                var objStorageID='StorageID_SignItem_TD_Text_'+(i);
                var objProductCount = 'OutCount_SignItem_TD_Text_'+(i);
                var objUnitPrice = 'UnitPrice_SignItem_TD_Text_'+(i);
                var objTotalPrice = 'TotalPrice_SignItem_TD_Text_'+(i);
                var objRemark = 'Remark_SignItem_TD_Text_'+(i);
                var objFromLineNo = 'FromLineNo_SignItem_TD_Text_'+(i);
                var objRadio = 'chk_Option_'+(i);
                var objPackage='Package_SignItem_TD_Text_'+(i);//包装
                
                var objUnitID='UnitID_SignItem_TD_Text_'+(i);//实际单位                
                var objBaseUnitID='BaseUnit_SignItem_TD_Text_'+(i);//基本单位
                var objBaseCount='BaseCount_SignItem_TD_Text_'+(i);//基本数量
                var objBasePrice='baseprice_td'+(i);//基本单价
                var objExtRate='UnitID_SignItem_TD_Text_'+(i);//比率
                
                var objBatchNo='BatchNo_SignItem_TD_Text_'+(i);//批次
                //$("#BatchNo_SignItem_TD_Text_"+rowid).val().split('|')[1].toString()
                
                
                
                DetailProductID.push($("#"+objProductNo).attr("title"));
                DetailSortNo.push(document.getElementById(objSortNo.toString()).innerHTML);
                DetailStorageID.push(document.getElementById(objStorageID.toString()).value);
                DetailFromLineNo.push(document.getElementById(objFromLineNo.toString()).value);
                
                DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value);
                DetailRemark.push(document.getElementById(objRemark.toString()).value);
                DetailPackage.push(document.getElementById(objPackage.toString()).value);  
                              
                if($("#HiddenMoreUnit").val()=="False")//未启用时
                {
                    DetailBaseUnitID.push(document.getElementById(objUnitID.toString()).title);//单位                
                    DetailBaseCount.push(document.getElementById(objProductCount.toString()).value);//数量
                    DetailBasePrice.push(document.getElementById(objUnitPrice.toString()).value);//单价
                    
                    
                }     
                else
                {
                    DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value);//实际单价
                    DetailBaseUnitID.push(document.getElementById(objBaseUnitID.toString()).title);//基本单位                
                    DetailBaseCount.push(document.getElementById(objBaseCount.toString()).value);//基本数量
                    DetailUnitID.push(document.getElementById(objUnitID.toString()).value.split('|')[0].toString());//实际单位ID  
                    DetailProductCount.push(document.getElementById(objProductCount.toString()).value);//实际数量 
                    DetailBasePrice.push(document.getElementById(objBasePrice.toString()).value);//基本单价
                    DetailExtRate.push(document.getElementById(objExtRate.toString()).value.split('|')[1].toString());//比率
                } 
                DetailBatchNo.push(document.getElementById(objBatchNo.toString()).value);//批次
                
            }
        }
        var UrlParam = "&txtOutNo="+escape(txtOutNo)+
                        "&txtTitle="+escape(txtTitle)+
                        "&ddlFromType="+escape(ddlFromType)+
                        "&txtFromBillID="+escape(txtFromBillID)+
                        "&txtSender="+escape(txtSender)+
                        "&txtTotalPrice="+escape(txtTotalPrice)+
                        "&txtTotalCount="+escape(txtTotalCount)+
                        "&txtOutDept="+escape(txtOutDept)+
                        "&txtTransactor="+escape(txtTransactor)+
                        "&txtOutDate="+escape(txtOutDate)+
                        "&txtSummary="+escape(txtSummary)+
                        "&txtRemark="+escape(txtRemark)+
                        "&CanViewUser="+escape(CanViewUser)+
                        "&CanViewUserName="+escape(CanViewUserName)+
                        "&sltBillStatus="+escape(sltBillStatus)+
                        "&DetailProductID="+escape(DetailProductID.toString())+
                        "&DetailSortNo="+escape(DetailSortNo.toString())+
                        "&DetailStorageID="+escape(DetailStorageID.toString())+
                        "&DetailFromLineNo="+escape(DetailFromLineNo.toString())+
                        "&DetailProductCount="+escape(DetailProductCount.toString())+
                        "&DetailUnitPrice="+escape(DetailUnitPrice.toString())+
                        "&DetailTotalPrice="+escape(DetailTotalPrice.toString())+
                        "&DetailRemark="+escape(DetailRemark.toString())+
                        "&DetailPackage="+escape(DetailPackage.toString())+
                        "&DetailUnitID="+escape(DetailUnitID.toString())+//
                        "&DetailBaseUnitID="+escape(DetailBaseUnitID.toString())+//
                        "&DetailBaseCount="+escape(DetailBaseCount.toString())+//
                        "&DetailBasePrice="+escape(DetailBasePrice.toString())+//
                        "&DetailExtRate="+escape(DetailExtRate.toString())+//
                        "&DetailBatchNo="+escape(DetailBatchNo.toString())+//
                        "&Act=edit&bmgz="+bmgz+
                        "&ID="+txtIndentityID.toString()+GetExtAttrValue();
                        //alert(UrlParam);     
                        
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageOutSellAdd.ashx",
                  data:UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     AddPop();
                  }, 
                  complete :function(){ hidePopup();},
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
                        var reInfo=data.data.split('|');
                        if(reInfo.length > 1)
                        {   
                            
                            document.getElementById('div_OutNo_uc').style.display="none";
                            document.getElementById('div_OutNo_Lable').style.display="";
                            if(parseInt(txtIndentityID)<=0)
                            {
                                document.getElementById('lbOutNo').innerHTML = reInfo[1];
                                document.getElementById('txtIndentityID').value= reInfo[0];
                                document.getElementById('txtModifiedUserID').value= reInfo[2];
                                document.getElementById('txtModifiedDate').value= reInfo[3];
                            }
                            else
                            {
                                document.getElementById('txtModifiedUserID').value= reInfo[0];
                                document.getElementById('txtModifiedDate').value= reInfo[1];
                            }
                        }
                        try
                        {
                            $("#btnPageFlowConfrim").hide();
                            $("#Confirm").show();
                        }catch(e){}
                        //fnDelRow();
                        //LoadDetailInfo(document.getElementById('txtIndentityID').value);//重新加载页面，为了保存后，直接点击确认，而无法验证出库量是否大于现有存量
                    }
                    popMsgObj.ShowMsg(data.info);
                    document.getElementById("t_Edit").style.display="";
                    document.getElementById("t_Add").style.display="none";
                  } 
               });
    }
}


//---------End   销售出库保存 ----------




//计算明细中出库数量总合以及价格总合
function CountNum()
{
    var List_TB=findObj("dg_Log",document);
    var rowlength=List_TB.rows.length;
    var countnum = 0;
    var countprice = 0;
    for(var i=1;i<rowlength;i++)
    {
        if(List_TB.rows[i].style.display!="none")
        {
            countnum += parseFloat(document.getElementById('OutCount_SignItem_TD_Text_'+(i)).value,10);
            countprice +=parseFloat(document.getElementById('TotalPrice_SignItem_TD_Text_'+(i)).value,10);
        }
    }
    
    document.getElementById('txtTotalCount').value = (parseFloat(countnum)).toFixed($("#HiddenPoint").val());
    document.getElementById('txtTotalPrice').value = (parseFloat(countprice)).toFixed($("#HiddenPoint").val());
    
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
            countnum += parseFloat(document.getElementById('OutCount_SignItem_TD_Text_'+(i)).value,10);
            countprice +=parseFloat(document.getElementById('TotalPrice_SignItem_TD_Text_'+(i)).value,10);
            
             if($("#HiddenMoreUnit").val()=="True")//启用时(重新计算)
             {
                var EXRate = $("#UnitID_SignItem_TD_Text_"+i).val().split('|')[1].toString(); /*比率*/
                var OutCount = $("#OutCount_SignItem_TD_Text_"+i).val();/*出库数量*/
                if (OutCount != '')
                {
                    var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val());
                    $("#BaseCount_SignItem_TD_Text_"+i).val(tempcount);/*基本数量根据出库数量和比率算出*/
                }
            }
        }
    }
    
    document.getElementById('txtTotalCount').value = (parseFloat(countnum)).toFixed($("#HiddenPoint").val());
    document.getElementById('txtTotalPrice').value = (parseFloat(countprice)).toFixed($("#HiddenPoint").val());
    
}

//全选
function SelectAll()
{
    var signFrame = findObj("dg_Log",document);        
    var txtTRLastIndex = signFrame.rows.length;
    if(document.getElementById("checkall").checked)
    {
        for(var i=1;i<txtTRLastIndex;i++)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                var objRadio = 'chk_Option_'+(i);
                document.getElementById(objRadio.toString()).checked = true;
            }
        }
    }
    else
    {
        for(var i=1;i<txtTRLastIndex;i++)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                var objRadio = 'chk_Option_'+(i);
                document.getElementById(objRadio.toString()).checked = false;
            }
        }
    }
}


 //删除行,才是最后版本 
function DeleteDetailRow()
{
    //获取表格
    table = document.getElementById("dg_Log");
	var count = table.rows.length;
	for (var i = count - 1; i > 0; i--)
	{
        var select = document.getElementById("chk_Option_" + i);
        if (select.checked)
        {
            DeleteRow(table, i);
        }
	}    
}

function DeleteRow(table,row)
{    
    //获取表格
	var count = table.rows.length - 1;
	table.deleteRow(row);
	if (row < count)//
	{
	    for(var j = parseInt(row) + 1; j <= count; j++)
	    {
			var no = j - 1;
			document.getElementById("SignItem_TD_Index_" + j).innerHTML = parseInt(document.getElementById("SignItem_TD_Index_" + j).innerHTML,10) - 1;
			document.getElementById("SignItem_TD_Index_" + j).id = "SignItem_TD_Index_" + no;
			document.getElementById("chk_Option_" + j).id = "chk_Option_" + no;
			document.getElementById("SignItem_TD_Check_" + j).id = "chk" + no;
			document.getElementById("ProductNo_SignItem_TD_Text_" + j).id = "ProductNo_SignItem_TD_Text_" + no;
			document.getElementById("ProductName_SignItem_TD_Text_" + j).id = "ProductName_SignItem_TD_Text_" + no;
			document.getElementById("Specification_SignItem_TD_Text_" + j).id = "Specification_SignItem_TD_Text_" + no;
			document.getElementById("UnitID_SignItem_TD_Text_" + j).id = "UnitID_SignItem_TD_Text_" + no;
			document.getElementById("StorageID_SignItem_TD_Text_" + j).id = "StorageID_SignItem_TD_Text_" + no;
			document.getElementById("FromBillCount_SignItem_TD_Text_" + j).id = "FromBillCount_SignItem_TD_Text_" + no;
			document.getElementById("Package_SignItem_TD_Text_" + j).id = "Package_SignItem_TD_Text_" + no;
			
			document.getElementById("NotOutCount_SignItem_TD_Text_" + j).id = "NotOutCount_SignItem_TD_Text_" + no;
			document.getElementById("OutCount_SignItem_TD_Text_" + j).id = "OutCount_SignItem_TD_Text_" + no;
			document.getElementById("UnitPrice_SignItem_TD_Text_" + j).id = "UnitPrice_SignItem_TD_Text_" + no;
			document.getElementById("TotalPrice_SignItem_TD_Text_" + j).id = "TotalPrice_SignItem_TD_Text_" + no;
			document.getElementById("FromBillID_SignItem_TD_Text_" + j).id = "FromBillID_SignItem_TD_Text_" + no;
			document.getElementById("FromLineNo_SignItem_TD_Text_" + j).id = "FromLineNo_SignItem_TD_Text_" + no;
			document.getElementById("Remark_SignItem_TD_Text_" + j).id = "Remark_SignItem_TD_Text_" + no;
			
			document.getElementById("MinusIs_SignItem_TD_Text_" + j).id = "MinusIs_SignItem_TD_Text_" + no;
			document.getElementById("UseCount_SignItem_TD_Text_" + j).id = "UseCount_SignItem_TD_Text_" + no;
			document.getElementById("OutedCount_SignItem_TD_Text_" + j).id = "OutedCounted_SignItem_TD_Text_" + no;
			
			
	    }
	}
}


//通过销售退货单带出明细
/*带出基本数量，比率，基本单价，算出数量，单价，数量手工修改时，改变基本数量*/
 function AddSignRow(i,SendNo,ProductID,ProductNo
        ,ProductName,Specification, UnitID/*基本单位名称（改造后的）*/,HiddenUnitID/*基本单位ID（改造后）*/
        ,UnitPrice,FromBillCount/*源单数量、基本数量*/,NotOutCount,FromLineNo,Remark,OutedCount,StorageID
        ,IsBatchNo,UsedUnitID,UsedPrice)
 { //读取最后一行的行号，存放在txtTRLastIndex文本框中 
        
        //alert(UsedPrice);
        if(FromBillCount!="")FromBillCount=parseFloat(FromBillCount).toFixed($("#HiddenPoint").val());
        if(NotOutCount!="")NotOutCount=parseFloat(NotOutCount).toFixed($("#HiddenPoint").val());
        if(OutedCount!="")OutedCount=parseFloat(OutedCount).toFixed($("#HiddenPoint").val());
        if(UnitPrice!="") UnitPrice=parseFloat(UnitPrice).toFixed($("#HiddenPoint").val());  
        if(UsedPrice!="") UsedPrice=parseFloat(UsedPrice).toFixed($("#HiddenPoint").val());
        if(UnitPrice=="")UnitPrice="0.00"; 
        if(UsedPrice=="")UsedPrice="0.00"; 

        var signFrame = findObj("dg_Log",document);
        var rowID=signFrame.rows.length;
        var newTR = signFrame.insertRow(rowID);//添加行
        newTR.id = "SignItem_Row_" + rowID;
        
        var newNameXH=newTR.insertCell(0);//添加列:选择
        newNameXH.className="cell";
        newNameXH.id = 'SignItem_TD_Check_'+rowID;
        newNameXH.innerHTML="<input name='chk1' id='chk_Option_"+rowID+"' value=\"\" type='checkbox' onclick=\"IfSelectAll('chk1','checkall');\"/>";

        var newNameTD=newTR.insertCell(1);//添加列:序号
        newNameTD.className="cell";
        newNameTD.id = 'SignItem_TD_Index_'+rowID;
        newNameTD.innerHTML = newTR.rowIndex.toString();
               
        var newFitNametd=newTR.insertCell(2);//添加列:物品编号
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_ProductNo_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_"+rowID+"\" value=\""+ProductNo+"\" title=\""+ProductID+"\" class=\"tdinput\"  readonly=\"readonly\" style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(3);//添加列:物品名称
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_ProductName_'+rowID;
        newFitDesctd.innerHTML ="<input id='ProductName_SignItem_TD_Text_"+rowID+"' value=\""+ProductName+"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />";;//添加列内容
        
        var newBatchNotd=newTR.insertCell(4);//添加列:批次
        newBatchNotd.className="cell";
        newBatchNotd.id = 'SignItem_TD_BatchNo_'+rowID;
        newBatchNotd.innerHTML ="<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_"+rowID+"' />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(5);//添加列:物品规格
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Specification_'+rowID;
        newFitDesctd.innerHTML ="<input id='Specification_SignItem_TD_Text_"+rowID+"' value=\""+Specification+"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />";;//添加列内容
             
        var newPackage=newTR.insertCell(6);//添加列包装要求
        newPackage.className="cell";
        newPackage.id = 'SignItem_TD_Package_'+rowID;
        newPackage.innerHTML ="<input id='Package_SignItem_TD_Text_"+rowID+"' specialworkcheck='包装要求' type='text' class=\"tdinput\"  style='width:90%' />";;//添加列内容
        
        var newBaseUnit=newTR.insertCell(7);//添加列:基本单位
        newBaseUnit.className="cell";
        newBaseUnit.id = 'SignItem_TD_BaseUnit_'+rowID;
        newBaseUnit.innerHTML ="<input id='BaseUnit_SignItem_TD_Text_"+rowID+"' type='text' value=\""+UnitID+"\" title=\""+HiddenUnitID+"\" class=\"tdinput\"  style='width:90%' readonly />";;//添加列内容
        
        var newBaseCount=newTR.insertCell(8);//添加列：基本数量
        newBaseCount.className="cell";
        newBaseCount.id = 'SignItem_TD_BaseCount_'+rowID;
        newBaseCount.innerHTML ="<input id='BaseCount_SignItem_TD_Text_"+rowID+"' type='text' value=\""+FromBillCount+"\" class=\"tdinput\"  readonly style='width:90%' />";;//添加列内容
        
        if($("#HiddenMoreUnit").val()=="False")
        {
            newBaseUnit.style.display="none"
            newBaseCount.style.display="none"        
            var newFitNametd=newTR.insertCell(9);//添加列:单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_"+rowID+"\"  value=\""+UnitID+"\" title=\""+HiddenUnitID+"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        }
        else
        {
            $("#baseuint").show();
            $("#basecount").show();
            var newFitNametd=newTR.insertCell(9);//添加列:单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
        }
        
        var ListControlID="BatchNo_SignItem_TD_Text_"+rowID;
        var StorageControlID="StorageID_SignItem_TD_Text_"+rowID;
        var newFitNametd=newTR.insertCell(10);//添加列:仓库ID
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_StorageID_'+rowID;
        newFitNametd.innerHTML = "<select class='tdinput' onchange=\"GetBatchList('"+ProductID+"','"+StorageControlID+"','"+ListControlID+"','"+IsBatchNo+"')\" id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>"+ document.getElementById("ddlStorageInfo").innerHTML + "</select>";
        document.getElementById("StorageID_SignItem_TD_Text_" + rowID ).value=StorageID;//赋值当前的的仓库
        /*绑定批次*/
       
        GetBatchList(ProductID,StorageControlID,ListControlID,IsBatchNo);
        
        var newFitNametd=newTR.insertCell(11);//添加列:原单数量
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
        newFitNametd.innerHTML = "<input type='hidden' id=\"FromBillCount_SignItem_TD_Text_Hidden"+rowID+"\" value=\""+FromBillCount+"\"><input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_"+rowID+"\" value=\""+FromBillCount+"\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>";//添加列内容
        
        var newFitNametd=newTR.insertCell(12);//添加列:已出库数量
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_OutedCounted_'+rowID;
        newFitNametd.innerHTML = "<input type='hidden' id=\"OutedCount_SignItem_TD_Text_Hidden"+rowID+"\" value=\""+OutedCount+"\" > <input type=\"text\" id=\"OutedCount_SignItem_TD_Text_"+rowID+"\" value=\""+OutedCount+"\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>";//添加列内容
        
        var newFitNametd=newTR.insertCell(13);//添加列:未出库数量
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
        newFitNametd.innerHTML = "<input type='hidden' id=\"NotOutCount_SignItem_TD_Text_Hidden"+rowID+"\" value=\""+NotOutCount+"\"> <input type=\"text\" id=\"NotOutCount_SignItem_TD_Text_"+rowID+"\" value=\""+NotOutCount+"\"  class=\"tdinput\"  style='width:90%' readonly=\"readonly\"/>";//添加列内容
        
        var point=$("#HiddenPoint").val();
        if($("#HiddenMoreUnit").val()=="False")
        {
            var newFitNametd=newTR.insertCell(14);//添加列:出库数量
            newFitNametd.className="tdinput";
            newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_"+rowID+"\" value=\""+NotOutCount+"\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_round(this,'"+point+"');TotalPrice(this.id);CountNum();\"/>";//添加列内容
        }
        else
        {
            var newFitNametd=newTR.insertCell(14);//添加列:出库数量(根据基本数量计算)
            newFitNametd.className="tdinput";
            newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_"+rowID+"\"  class=\"tdinput\" value=\""+NotOutCount+"\"   style='width:90%' onblur=\"check(this.id);Number_round(this,'"+point+"');TotalPrice_MoreUnit(this.id,"+rowID+");CountNum();\"/>";//添加列内容
        }
        if($("#HiddenMoreUnit").val()=="False")
        {
            var newFitDesctd=newTR.insertCell(15);//添加列:单价
            newFitDesctd.className="cell";
            newFitDesctd.id = 'SignItem_TD_UnitPrice_'+rowID;
            newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_"+rowID+"' value=\""+UnitPrice+"\" type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";//添加列内容
        }
        else
        {
            var newFitDesctd=newTR.insertCell(15);//添加列:出库单价(根据基本单价计算,隐藏基本单价，比率)
            newFitDesctd.className="cell";
            newFitDesctd.id = 'SignItem_TD_UnitPrice_'+rowID;
            newFitDesctd.innerHTML = "<input type='hidden' value=\""+UnitPrice+"\" id='baseprice_td"+rowID+"'><input type='hidden' id='BaseExRate"+rowID+"'><input name='chk' id='UnitPrice_SignItem_TD_Text_"+rowID+"'  type='text' value=\""+UsedPrice+"\" class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";//添加列内容
        }

        //绑定单位组
       if($("#HiddenMoreUnit").val()=="True")
            GetUnitGroupSelectEx(ProductID,"StockUnit","UnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID,"FillContent("+rowID+","+UnitPrice+","+NotOutCount+")");//
       // alert(document.getElementById("UnitID_SignItem_TD_Text_"+rowID).value);
        //ChangeUnit("UnitID_SignItem_TD_Text_" + rowID,rowID,UsedPrice);/*初始调用赋值*/
        //CalCulateNum("UnitID_SignItem_TD_Text_" + rowID,"OutCount_SignItem_TD_Text_"+rowID,"BaseCount_SignItem_TD_Text_"+rowID,"UnitPrice_SignItem_TD_Text_"+rowID,UsedPrice);


        var newFitNametd=newTR.insertCell(16);//添加列:物品总价（金额）
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_TotalPrice_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_"+rowID+"\" value=\"\" class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"  />";//添加列内容
        
        var productcount=document.getElementById("OutCount_SignItem_TD_Text_" + rowID).value;//计算总额
        var unitprice=document.getElementById("UnitPrice_SignItem_TD_Text_" + rowID).value;
        document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value=(productcount*unitprice).toFixed($("#HiddenPoint").val());
       
        if($("#IsDisplayPrice").val()=="true")
        {
            document.getElementById("SignItem_TD_UnitPrice_"+rowID).style.display="";
            document.getElementById("SignItem_TD_TotalPrice_"+rowID).style.display="";
        }
        else
        {
            document.getElementById("SignItem_TD_UnitPrice_"+rowID).style.display="none";
            document.getElementById("SignItem_TD_TotalPrice_"+rowID).style.display="none";
        }
       
        var newFitNametd=newTR.insertCell(17);//添加列:原单编号
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_TotalPrice_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_"+rowID+"\" value=\""+SendNo+"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(18);//添加列:原单行号
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='FromLineNo_SignItem_TD_Text_"+rowID+"' value=\""+FromLineNo+"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(19);//添加列:备注
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_"+rowID+"' value=\""+Remark+"\" type='text' class=\"tdinput\"  style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(20);//添加列:可否为负库存（0不允许，1允许）
        newFitDesctd.className="cell";
        newFitDesctd.style.display="none"
        newFitDesctd.id = 'SignItem_TD_MinusIs_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='MinusIs_SignItem_TD_Text_"+rowID+"' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(21);//添加列:可用存量
        newFitDesctd.className="cell";
        newFitDesctd.style.display="none"
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='UseCount_SignItem_TD_Text_"+rowID+"' value=\"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        CountNum();
        
 }
 //查看时加载
function FillSignRow(i,OutNo,ProductID,ProductNo
        ,ProductName,Specification,Package,UnitID,HiddenUnitID,UnitPrice,TotalPrice
        ,FromBillCount,NotOutCount,ProductCount,FromBillNo,FromLineNo,Remark,StorageID,MinusIs,UseCount,OutedCount
        ,BatchNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,IsBatchNo)
 { //读取最后一行的行号，存放在txtTRLastIndex文本框中 
        
        //alert(ProductCount);
        if(UsedPrice=="")UsedPrice="0.00";
        if(UsedUnitID=="")UsedUnitID="0.00";
        if(UsedUnitCount=="")UsedUnitCount="0.00";
        
        if(FromBillCount=="")FromBillCount="0.00";
        if(NotOutCount=="")NotOutCount="0.00";
        if(ProductCount=="")ProductCount="0.00";
        if(UnitPrice=="")UnitPrice="0.00";
        
        if(ExRate=="")ExRate="0.00";
        UnitPrice=parseFloat(UnitPrice).toFixed($("#HiddenPoint").val());
        TotalPrice=parseFloat(TotalPrice).toFixed($("#HiddenPoint").val());
        FromBillCount=parseFloat(FromBillCount).toFixed($("#HiddenPoint").val());
        NotOutCount=parseFloat(NotOutCount).toFixed($("#HiddenPoint").val());
        ProductCount=parseFloat(ProductCount).toFixed($("#HiddenPoint").val());
        UseCount=parseFloat(UseCount).toFixed($("#HiddenPoint").val());
        OutedCount=parseFloat(OutedCount).toFixed($("#HiddenPoint").val());
        UsedUnitCount=parseFloat(UsedUnitCount).toFixed($("#HiddenPoint").val());
        if(UsedPrice!="")UsedPrice=parseFloat(UsedPrice).toFixed($("#HiddenPoint").val());
        
        
        var signFrame = findObj("dg_Log",document);
        var rowID=signFrame.rows.length;
        var newTR = signFrame.insertRow(rowID);//添加行
        newTR.id = "SignItem_Row_" + rowID;
        
        var newNameXH=newTR.insertCell(0);//添加列:选择
        newNameXH.className="cell";
        newNameXH.id = 'SignItem_TD_Check_'+rowID;
        newNameXH.innerHTML="<input name='chk1' id='chk_Option_"+rowID+"' value=\"\" type='checkbox' onclick=\"IfSelectAll('chk1','checkall');\"/>";
        
        var newNameTD=newTR.insertCell(1);//添加列:序号
        newNameTD.className="cell";
        newNameTD.id = 'SignItem_TD_Index_'+rowID;
        newNameTD.innerHTML = newTR.rowIndex.toString();
                
        var newFitNametd=newTR.insertCell(2);//添加列:物品编号
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_ProductNo_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_"+rowID+"\" value=\""+ProductNo+"\" title=\""+ProductID+"\" class=\"tdinput\"  readonly=\"readonly\" style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(3);//添加列:物品名称
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_ProductName_'+rowID;
        newFitDesctd.innerHTML ="<input id='ProductName_SignItem_TD_Text_"+rowID+"' value=\""+ProductName+"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />";;//添加列内容
        
        
        var newBatchNotd=newTR.insertCell(4);//添加列:批次
        newBatchNotd.className="cell";
        newBatchNotd.id = 'SignItem_TD_BatchNo_'+rowID;
        newBatchNotd.innerHTML ="<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_"+rowID+"' />";//添加列内容
               
        var newFitDesctd=newTR.insertCell(5);//添加列:物品规格
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Specification_'+rowID;
        newFitDesctd.innerHTML ="<input id='Specification_SignItem_TD_Text_"+rowID+"' value=\""+Specification+"\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />";;//添加列内容
       
        var newPackage=newTR.insertCell(6);//添加列包装要求
        newPackage.className="cell";
        newPackage.id = 'SignItem_TD_Package_'+rowID;
        newPackage.innerHTML ="<input id='Package_SignItem_TD_Text_"+rowID+"' specialworkcheck='包装要求' value=\""+Package+"\" type='text' class=\"tdinput\"  style='width:90%' />";;//添加列内容
            
        var newBaseUnit=newTR.insertCell(7);//添加列:基本单位
        newBaseUnit.className="cell";
        newBaseUnit.id = 'SignItem_TD_BaseUnit_'+rowID;
        newBaseUnit.innerHTML ="<input id='BaseUnit_SignItem_TD_Text_"+rowID+"' value=\""+UnitID+"\" title=\""+HiddenUnitID+"\" type='text' class=\"tdinput\"  style='width:90%' />";;//添加列内容
        
        var newBaseCount=newTR.insertCell(8);//添加列：基本数量
        newBaseCount.className="cell";
        newBaseCount.id = 'SignItem_TD_BaseCount_'+rowID;
        newBaseCount.innerHTML ="<input id='BaseCount_SignItem_TD_Text_"+rowID+"' value=\""+ProductCount+"\" type='text' class=\"tdinput\"  style='width:90%' />";;//添加列内容
       
       if($("#HiddenMoreUnit").val()=="False")
        {
            newBaseUnit.style.display="none"
            newBaseCount.style.display="none"        
            var newFitNametd=newTR.insertCell(9);//添加列:单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_"+rowID+"\"  value=\""+UnitID+"\" title=\""+HiddenUnitID+"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        }
        else
        {
            $("#baseuint").show();
            $("#basecount").show();
            var newFitNametd=newTR.insertCell(9);//添加列:单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
        }
        
        var ListControlID="BatchNo_SignItem_TD_Text_"+rowID;
        var StorageControlID="StorageID_SignItem_TD_Text_"+rowID;
        var newFitNametd=newTR.insertCell(10);//添加列:仓库ID
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_StorageID_'+rowID;
        newFitNametd.innerHTML = "<select class='tdinput' onchange=\"GetBatchList('"+ProductID+"','"+StorageControlID+"','"+ListControlID+"','"+IsBatchNo+"')\" id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>"+ document.getElementById("ddlStorageInfo").innerHTML + "</select>";
        document.getElementById("StorageID_SignItem_TD_Text_" + rowID ).value=StorageID;//赋值当前的的仓库
        
        /*绑定批次*/
        GetBatchList(ProductID,StorageControlID,ListControlID,IsBatchNo,BatchNo);
        
        var newFitNametd=newTR.insertCell(11);//添加列:原单数量
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
        newFitNametd.innerHTML = "<input type='hidden' id=\"FromBillCount_SignItem_TD_Text_Hidden"+rowID+"\" value=\""+FromBillCount+"\"><input type=\"text\" id=\"FromBillCount_SignItem_TD_Text_"+rowID+"\" value=\""+FromBillCount+"\"  class=\"tdinput\"  style='width:90%' readonly=\"readonly\"/>";//添加列内容
        
        
        var newFitNametd=newTR.insertCell(12);//添加列:已出库数量
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_OutedCount_'+rowID;
        newFitNametd.innerHTML = "<input type='hidden' id=\"OutedCount_SignItem_TD_Text_Hidden"+rowID+"\" value=\""+OutedCount+"\" ><input type=\"text\" id=\"OutedCount_SignItem_TD_Text_"+rowID+"\" value=\""+OutedCount+"\"  class=\"tdinput\"  style='width:90%'  readonly=\"readonly\"/>";//添加列内容
        
        
        var newFitNametd=newTR.insertCell(13);//添加列:未出库数量
        newFitNametd.className="tdinput";
        newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
        newFitNametd.innerHTML = "<input type='hidden' id=\"NotOutCount_SignItem_TD_Text_Hidden"+rowID+"\" value=\""+NotOutCount+"\"><input type=\"text\" id=\"NotOutCount_SignItem_TD_Text_"+rowID+"\" value=\""+NotOutCount+"\"  class=\"tdinput\"  style='width:90%' readonly=\"readonly\"/>";//添加列内容

        var point=$("#HiddenPoint").val();
        if($("#HiddenMoreUnit").val()=="False")
        {
            var newFitNametd=newTR.insertCell(14);//添加列:出库数量
            newFitNametd.className="tdinput";
            newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_"+rowID+"\" value=\""+ProductCount+"\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_round(this,'"+point+"');TotalPrice(this.id);CountNum();\"/>";//添加列内容
        }
        else
        {
            var newFitNametd=newTR.insertCell(14);//添加列:出库数量
            newFitNametd.className="tdinput";
            newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"OutCount_SignItem_TD_Text_"+rowID+"\" value=\""+UsedUnitCount+"\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_round(this,'"+point+"');TotalPrice_MoreUnit(this.id,"+rowID+");CountNum();\"/>";//添加列内容

        }
        if($("#HiddenMoreUnit").val()=="False")
        {
            var newFitDesctd=newTR.insertCell(15);//添加列:单价
            newFitDesctd.className="cell";
            newFitDesctd.id = 'SignItem_TD_UnitPrice_'+rowID;
            newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_"+rowID+"' value=\""+UnitPrice+"\" type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";//添加列内容
        }
        else
        {
            UsedPrice=(parseFloat(UsedPrice)).toFixed($("#HiddenPoint").val());
            var newFitDesctd=newTR.insertCell(15);//添加列:出库单价(根据基本单价计算,隐藏基本单价，比率)
            newFitDesctd.className="cell";
            newFitDesctd.id = 'SignItem_TD_UnitPrice_'+rowID;
            newFitDesctd.innerHTML = "<input type='hidden' value=\""+UnitPrice+"\" id='baseprice_td"+rowID+"'><input type='hidden' id='BaseExRate"+rowID+"' value=\""+ExRate+"\"><input name='chk' id='UnitPrice_SignItem_TD_Text_"+rowID+"' value=\""+UsedPrice+"\"  type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";//添加列内容
        }
        if($("#HiddenMoreUnit").val()=="True")//绑定单位组
            GetUnitGroupSelectEx(ProductID,"StockUnit", "UnitID_SignItem_TD_Text_" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,UsedUnitID,"LoadUnitContent("+rowID+","+UsedUnitID+")");//

        var newFitNametd=newTR.insertCell(16);//添加列:物品总价（金额）
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_TotalPrice_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_"+rowID+"\" value=\"\" class=\"tdinput\"  style='width:90%' readonly=\"readonly\"  />";//添加列内容
        
        var productcount=document.getElementById("OutCount_SignItem_TD_Text_" + rowID).value;//计算总额
        var unitprice=document.getElementById("UnitPrice_SignItem_TD_Text_" + rowID).value;
        document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value=(productcount*unitprice).toFixed($("#HiddenPoint").val());
       
        if($("#IsDisplayPrice").val()=="true")
        {
            document.getElementById("SignItem_TD_UnitPrice_"+rowID).style.display="";
            document.getElementById("SignItem_TD_TotalPrice_"+rowID).style.display="";
        }
        else
        {
            document.getElementById("SignItem_TD_UnitPrice_"+rowID).style.display="none";
            document.getElementById("SignItem_TD_TotalPrice_"+rowID).style.display="none";
        }
        
        var newFitNametd=newTR.insertCell(17);//添加列:原单编号
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_TotalPrice_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"FromBillID_SignItem_TD_Text_"+rowID+"\" value=\""+FromBillNo+"\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(18);//添加列:原单行号
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='FromLineNo_SignItem_TD_Text_"+rowID+"' value=\""+FromLineNo+"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(19);//添加列:备注
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='Remark_SignItem_TD_Text_"+rowID+"' value=\""+Remark+"\" type='text' class=\"tdinput\"  style='width:90%'  />";//添加列内容
       
        var newFitDesctd=newTR.insertCell(20);//添加列:可否为负库存（0不允许，1允许）
        newFitDesctd.className="cell";
        newFitDesctd.style.display="none"
        newFitDesctd.id = 'SignItem_TD_MinusIs_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='MinusIs_SignItem_TD_Text_"+rowID+"' value=\""+MinusIs+"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(21);//添加列:可用存量
        newFitDesctd.className="cell";
        newFitDesctd.style.display="none"
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='UseCount_SignItem_TD_Text_"+rowID+"' value=\""+UseCount+"\" type='text' class=\"tdinput\" readonly=\"readonly\"  style='width:90%'  />";//添加列内容
         
 }
 //暂时不用
 function LoadUnitContent(rowid,usedunit)
 {
    $("#UnitID_SignItem_TD_Text_"+rowid).attr("disabled",true);
//   var exrate=$("#BaseExRate"+rowid).val();
//   var usedunitvalue=usedunit+"|"+exrate;
//   $("#UnitID_SignItem_TD_Text_"+rowid).val(usedunitvalue); /*比率*/
 }
 
//本行小计，，数量变动时根据比率算出基本数量
function TotalPrice_MoreUnit(id,rowid)
{
    var idstr=id.substring(9,id.length);
    var count=id;
    var productcount=document.getElementById(count).value;
    var EXRate = $("#UnitID_SignItem_TD_Text_"+rowid).val().split('|')[1].toString(); /*比率*/
    var AcCount = $("#OutCount_SignItem_TD_Text_"+rowid).val(); /*数量*/
    var unitprice=document.getElementById('UnitPrice_'+idstr).value;
    document.getElementById('TotalPrice_'+idstr).value=(productcount*unitprice).toFixed($("#HiddenPoint").val());
    if(EXRate!="0")
        document.getElementById('BaseCount_SignItem_TD_Text_'+rowid).value=(AcCount*EXRate).toFixed($("#HiddenPoint").val());
}
function TotalPrice(id)
{
    var idstr=id.substring(9,id.length);
    var count=id;
    var productcount=document.getElementById(count).value;
    var unitprice=document.getElementById('UnitPrice_'+idstr).value;
    document.getElementById('TotalPrice_'+idstr).value=(productcount*unitprice).toFixed($("#HiddenPoint").val());;
}
//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,RowID/*行号*/,UsedPrice/*基本单价*/)
 {
    var EXRate = $("#UnitID_SignItem_TD_Text_"+RowID).val().split('|')[1].toString(); /*比率*/
    //var UsedUnitCount = $("#BaseCount_SignItem_TD_Text_"+RowID).val();/*基本数量*/
    var OutCount = $("#OutCount_SignItem_TD_Text_"+RowID).val();/*出库数量*/
    
    var FromBillCount=$("#FromBillCount_SignItem_TD_Text_Hidden"+RowID).val();/*通知数量转换前隐藏域的值*/
    var OutedCount=$("#OutedCount_SignItem_TD_Text_Hidden"+RowID).val();/*已出库数量转换前隐藏域的值*/
    var NotOutCount=$("#NotOutCount_SignItem_TD_Text_Hidden"+RowID).val();/*未出库数量转换前隐藏域的值*/
    if (EXRate != '')
    {
        var tempcount=parseFloat(OutCount/EXRate).toFixed($("#HiddenPoint").val());
        var tempprice=parseFloat(EXRate*UsedPrice).toFixed($("#HiddenPoint").val());
        $("#BaseCount_SignItem_TD_Text_"+RowID).val(tempcount);/*基本数量根据出库数量和比率算出*/
        $("#UnitPrice_SignItem_TD_Text_"+RowID).val(tempprice);/*单价根据基本单价和比率算出*/
        $("#TotalPrice_SignItem_TD_Text_"+RowID).val(parseFloat(OutCount*tempprice).toFixed($("#HiddenPoint").val()));/*金额*/
        
        $("#FromBillCount_SignItem_TD_Text_"+RowID).val((FromBillCount*EXRate).toFixed($("#HiddenPoint").val()));
        $("#OutedCount_SignItem_TD_Text_"+RowID).val((OutedCount*EXRate).toFixed($("#HiddenPoint").val()));
        $("#NotOutCount_SignItem_TD_Text_"+RowID).val((NotOutCount*EXRate).toFixed($("#HiddenPoint").val()));
    }
     CountNum();
 }
 ////////选择源单时计算各项
 function FillContent(RowID,UsedPrice,UsedCount)
 {
    $("#UnitID_SignItem_TD_Text_"+RowID).attr("disabled",true);
    var EXRate = $("#UnitID_SignItem_TD_Text_"+RowID).val().split('|')[1].toString(); /*比率*/
//    var UsedUnitCount = $("#BaseCount_SignItem_TD_Text_"+RowID).val();/*基本数量*/
    var outcount=$("#OutCount_SignItem_TD_Text_"+RowID).val(UsedCount.toFixed($("#HiddenPoint").val()));/*出库数量*/
//    var FromBillCount=$("#FromBillCount_SignItem_TD_Text_Hidden"+RowID).val();/*通知数量转换前隐藏域的值*/
//    var OutedCount=$("#OutedCount_SignItem_TD_Text_Hidden"+RowID).val();/*已出库数量转换前隐藏域的值*/
//    var NotOutCount=$("#NotOutCount_SignItem_TD_Text_Hidden"+RowID).val();/*未出库数量转换前隐藏域的值*/
    if (EXRate != '')
    {
          $("#BaseCount_SignItem_TD_Text_"+RowID).val((UsedCount*EXRate).toFixed($("#HiddenPoint").val()));
//        var tempcount=parseFloat(EXRate*UsedUnitCount).toFixed($("#HiddenPoint;").val());
//        var tempprice=parseFloat(EXRate*UsedPrice).toFixed($("#HiddenPoint").val());
//        $("#OutCount_SignItem_TD_Text_"+RowID).val(tempcount);/*出库数量根据基本数量和比率算出*/
//        $("#UnitPrice_SignItem_TD_Text_"+RowID).val(tempprice);/*单价根据基本数量和比率算出*/
//        $("#TotalPrice_SignItem_TD_Text_"+RowID).val(parseFloat(tempcount*tempprice).toFixed($("#HiddenPoint").val()));/*金额*/
//        
//        $("#FromBillCount_SignItem_TD_Text_"+RowID).val((FromBillCount*EXRate).toFixed($("#HiddenPoint").val()));
//        $("#OutedCount_SignItem_TD_Text_"+RowID).val((OutedCount*EXRate).toFixed($("#HiddenPoint").val()));
//        $("#NotOutCount_SignItem_TD_Text_"+RowID).val((NotOutCount*EXRate).toFixed($("#HiddenPoint").val()));
    }
 }
//---------Start  明细处理 ----------
function findObj(theObj, theDoc)
{
    var p, i, foundObj; 
    if(!theDoc) theDoc = document; 
    if( (p = theObj.indexOf("?")) > 0 && parent.frames.length) 
    {    
        theDoc = parent.frames[theObj.substring(p+1)].document;    
        theObj = theObj.substring(0,p); 
    }
             
    if(!(foundObj = theDoc[theObj]) && theDoc.all) foundObj = theDoc.all[theObj]; 
            for (i=0; !foundObj && i < theDoc.forms.length; i++)     
            foundObj = theDoc.forms[i][theObj]; for(i=0; !foundObj && theDoc.layers && i < theDoc.layers.length; i++)     
            foundObj = findObj(theObj,theDoc.layers[i].document); 
            if(!foundObj && document.getElementById) foundObj = document.getElementById(theObj);   
            return foundObj;
} 


function SelectStorage(id,value)
{
    document.getElementById('StorageID_'+id).value=value;
}


function Fun_FillParent_Content(id,ProNo,ProdName,UnitPrice,UnitID)
{
    var hiddenObj = 'Hidden_' + popTechObj.InputObj;
    var Price='UnitPrice_'+popTechObj.InputObj;
    var ProductNo='ProductNo_'+popTechObj.InputObj;
    var ProductName='ProductName_'+popTechObj.InputObj;
    var unitID='UnitID_'+popTechObj.InputObj;
    
    document.getElementById(hiddenObj).value = id;
    document.getElementById(ProductNo).value = ProNo;
    $("#"+ProductNo).attr("title",id);
    document.getElementById(ProductName).value = ProdName;
    document.getElementById(Price).value = UnitPrice;
    document.getElementById(unitID).value = UnitID;
    document.getElementById('divStorageProduct').style.display='none';
    
}

var isqueren="";
function ConfirmBill()
{
   if(isqueren!=""||confirm('确认之后不可修改，你确定要确认吗？'))
   {
//        var signFrame = findObj("dg_Log",document);
//        var ifbig="0";
//        for(i=1;i<signFrame.rows.length;i++)
//            {   
//                var NotOutCount="NotOutCount_SignItem_TD_Text_" + i;
//                var MinusIs="MinusIs_SignItem_TD_Text_"+i;   
//                var OutCount="OutCount_SignItem_TD_Text_" + i;
//                var UseCount="UseCount_SignItem_TD_Text_"+i;
//                if(parseFloat(document.getElementById(OutCount).value)>parseFloat(document.getElementById(NotOutCount).value))
//                {
//                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","出库数量不能大于未出库数量！");
//                    return false;
//                }
//                if(document.getElementById(MinusIs).value=="0")
//                {
//                    if(parseFloat(document.getElementById(OutCount).value)>parseFloat(document.getElementById(UseCount).value))
//                    {
//                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","明细中第"+i+"行\n出库数量不能大于现有存量\n（现有存量:"+document.getElementById(UseCount).value+"）！");
//                        return false;
//                    }
//                }
//                else
//                {
//                    if(parseFloat(document.getElementById(OutCount).value)>parseFloat(document.getElementById(UseCount).value))
//                    {
//                        if(confirm('明细中第'+i+'行\n出库数量超出了当前可用存量，是否继续确认？'))
//                        {
//                            continue;
//                        }
//                        else
//                        {
//                            return false;
//                        }
//                        
//                    }
//                }
//                
//            }
        
        var txtIndentityID = $("#txtIndentityID").val();
        var act="Confirm";
        var UrlParam = "&Act=Confirm&ID="+txtIndentityID.toString()+"&isqueren="+isqueren;
        $.ajax({ 
                      type: "POST",
                      url: "../../../Handler/Office/StorageManager/StorageOutSellAdd.ashx",
                      data:UrlParam,
                      dataType:'json',//返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                         AddPop();
                      }, 
                      complete :function(){ hidePopup();},
                      error: function() 
                      {
                        popMsgObj.ShowMsg('请求发生错误');
                        
                      }, 
                      success:function(data) 
                      { 
                        if(data.sta==1) 
                        {
                            var reInfo=data.data.split('|');
                            if(reInfo.length > 1)
                             {
                                document.getElementById('txtModifiedUserID').value= reInfo[0];
                                document.getElementById('txtModifiedDate').value= reInfo[1];
                                document.getElementById('txtConfirmor').value= reInfo[0];
                                document.getElementById('txtConfirmDate').value= reInfo[1];
                             }
                            document.getElementById('sltBillStatus').value=2;
                            try
                            {
                                 $("#btn_save").hide();
                                 $("#Confirm").hide();
                                 $("#btn_UnClick_bc").show();
                                 $("#btnPageFlowConfrim").show();
                                 $("#btn_Close").show();
                                 $("#btn_Unclic_Close").hide();
                                 $("#btn_CancelClose").hide();
                                 $("#btn_Unclick_CancelClose").show();
                             }
                             catch(e){}    
                         }
                         if(data.sta==2)
                         {
                            
                            var reInfo=data.info.split('|');
                            if(confirm('行'+reInfo[0]+'超过现有库存,现有库存为'+reInfo[1]+'\n   是否继续确认'))
                            {
                                isqueren="Yes";
                                ConfirmBill();
                                return;
                            }
                            else
                            {
                                return false;
                            }
                         }
//                         fnDelRow();
//                         LoadDetailInfo($("#txtIndentityID").val());
                         popMsgObj.ShowMsg(data.info);
                      } 
                   });
     }
     else
     {
        return false;
     }
}

//结单

function CloseBill()
{
    if(confirm('确认要进行结单操作吗？'))
   {
        var txtIndentityID = $("#txtIndentityID").val();
        var UrlParam = "&Act=Close&ID="+txtIndentityID.toString()+"";
        $.ajax({ 
                      type: "POST",
                      url: "../../../Handler/Office/StorageManager/StorageOutSellInfo.ashx?"+UrlParam,
                      dataType:'json',//返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                         AddPop();
                      }, 
                      complete :function(){ hidePopup();},
                      error: function() 
                      {
                        popMsgObj.ShowMsg('请求发生错误');
                        
                      }, 
                      success:function(data) 
                      { 
                        if(data.sta==1) 
                        {
                            var reInfo=data.data.split('|');
                            if(reInfo.length > 1)
                             {
                                document.getElementById('txtModifiedUserID').value= reInfo[0];
                                document.getElementById('txtModifiedDate').value= reInfo[1];
                                document.getElementById('txtCloser').value= reInfo[0];
                                document.getElementById('txtCloseDate').value= reInfo[1];
                             }
                            document.getElementById('sltBillStatus').value=4;
                            try
                            {
                                 $("#btn_save").hide();
                                 $("#Confirm").hide();
                                 $("#btn_UnClick_bc").show();
                                 $("#btnPageFlowConfrim").show();
                                 $("#btn_Close").hide();
                                 $("#btn_Unclic_Close").show();
                                 $("#btn_CancelClose").show();
                                 $("#btn_Unclick_CancelClose").hide();
                             }
                             catch(e){}    
                         }
                        popMsgObj.ShowMsg(data.info);
                      } 
                   });
    }
    else
    {
        return false;
    }
}


//取消结单

function CancelCloseBill()
{
   if(confirm('确认要进行取消结单操作吗？'))
     {
        var txtIndentityID = $("#txtIndentityID").val();
        var UrlParam = "&Act=CancelClose\
                            &ID="+txtIndentityID.toString()+"";
        $.ajax({ 
                      type: "POST",
                      url: "../../../Handler/Office/StorageManager/StorageOutSellInfo.ashx?"+UrlParam,
                      dataType:'json',//返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                         AddPop();
                      }, 
                      complete :function(){ hidePopup();},
                      error: function() 
                      {
                        popMsgObj.ShowMsg('请求发生错误');
                        
                      }, 
                      success:function(data) 
                      { 
                        var reInfo=data.data.split('|');
                        if(reInfo.length > 1)
                         {
                            document.getElementById('txtModifiedUserID').value= reInfo[0];
                            document.getElementById('txtModifiedDate').value= reInfo[1];
                            document.getElementById('txtCloser').value= "";
                            document.getElementById('txtCloseDate').value="";
                         }
                        document.getElementById('sltBillStatus').value=2;
                        try
                        {
                             $("#btn_save").hide();
                             $("#Confirm").hide();
                             $("#btn_UnClick_bc").show();
                             $("#btnPageFlowConfrim").show();
                             $("#btn_Close").show();
                             $("#btn_Unclic_Close").hide();
                             $("#btn_CancelClose").hide();
                             $("#btn_Unclick_CancelClose").show();
                         }
                         catch(e){}    
                        popMsgObj.ShowMsg(data.info);
                      } 
                   });
    }
    else
    {
        return false;
    }
}


//删除明细中所有数据
function fnDelRow()
{
    var dg_Log = findObj("dg_Log",document);
    var rowscount = dg_Log.rows.length;
    for(i=rowscount - 1;i > 0; i--)
    {//循环删除行,从最后一行往前删除
        dg_Log.deleteRow(i);
    }
    var txtTRLastIndex = findObj("txtTRLastIndex",document);//重置最后行号为
    txtTRLastIndex.value = "0";
}

function DoChange()
{
  var InType=document.getElementById('ddlFromType').value;
  if(InType!="0")
  {
     ShowList();
  }
  else
  {
     closeSellSenddiv();
  }
  
  
}

  
  
  /*----------------     Start弹出销售发货明细选择层JS-------------------------------------*/


  

function fnSelectInfo()
{
    var strSSNo="";
    var DetailIDList="";
    var signFrame = findObj("offerDataList",document);
    
    var sameNo = "";
    var isSame = true;
    for(i=1;i<signFrame.rows.length;i++)
    {         
        
        var chk= $("#ckSellSend_"+ i).attr("checked");
        //chkSlected = document.getElementById("ckPurchaseArrive_"+i);
        if(chk)
        {
            var colValue = signFrame.rows[i].cells[1].innerHTML;
            if (sameNo != "" && sameNo != colValue)
            {
               isSame = false;
               break; 
            }
            else if(sameNo == "")//取第一个选中的ID赋值给sameNo
            {
            
                sameNo = colValue; 
            }
            var DetailID =  signFrame.rows[i].cells[7].innerHTML;
             if($("#HiddenMoreUnit").val()=="True")
             {
                DetailID =  signFrame.rows[i].cells[10].innerHTML;
             }
            
            DetailIDList += DetailID + ","; 
            strSSNo=colValue;
        }
    }
    if(strSSNo=="")
    {
        return false;
        closeSellSenddiv();
    }
    if(isSame==false)
    {   
        alert("请选择同一销售发货单中的明细！");
        //popMsgObj.ShowMsg('请选择同一个采购单中的明细！');
        return;
    }
    
    var paUrl="Act=list&DetailIDList="+DetailIDList+"&strSSNo="+strSSNo;
     $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageOutSellInfo.ashx?'+paUrl,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageOffList_Pager").hide();},//发送数据之前
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    if(msg.SSList!=null)
                    {
                        //FillBasicInfo(TaskNo,FromBillID,ProcessType,ProcessDeptID,ProcessDeptName,Processor,ProcessorName)
                        $("#offerDataList tbody").find("tr.newrow").remove();
                        //填充页面上的基本信息数据
                        document.getElementById('txtFromBillID').value=msg.SSList[0].SendNo;
                        $("#txtFromBillID").attr("title",msg.SSList[0].ID);
                        document.getElementById('txtCust').value=msg.SSList[0].CustName;
                        document.getElementById('txtSellDept').value=msg.SSList[0].SellDeptName;
                        document.getElementById('txtSeller').value=msg.SSList[0].SellerName;
                        document.getElementById('txtSendAddress').value=msg.SSList[0].SendAddr;
                        document.getElementById('txtArrAddress').value=msg.SSList[0].ReceiveAddr;
                        //循环加载明细信息
                        $.each(msg.SSList,function(i,item)
                        {
                            AddSignRow(i,item.SendNo,item.ProductID,item.ProdNo,
                                item.ProductName,item.Specification,item.UnitID,item.HiddenUnitID,item.UnitPrice,
                                item.ProductCount,item.NotOutCount,item.SortNo,item.Remark,item.OutCount,item.StorageID,item.IsBatchNo,item.UsedUnitID,item.UsedPrice)
                        });
                        
                     } 
                    },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageOffList_Pager").show();offerDataList("offerDataList","#E7E7E7","#FFFFFF","#cfc","cfc");CountNum();}//接收数据完毕
           });
               
               //删除明细中所有数据
    fnDelRow();      
    closeSellSenddiv();
    
}

closeSellSenddiv()
{
    document.getElementById('divSellSend').style.display='none';
}

function ShowList()
{
    TurnToOffPage(currentOffPageIndex);
    document.getElementById('divSellSend').style.display='';
    document.getElementById("divzhezhao").style.display="";
}
  
    var pageOffCount = 10;//每页计数
    var totalOffRecord = 0;
    var pagerOffStyle = "flickr";//jPagerBar样式
    
    var currentOffPageIndex = 1;
    var action = "";//操作
    var orderByOff = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToOffPage(pageIndex)
    {
           currentOffPageIndex = pageIndex;
           var paUrl="pageIndex="+pageIndex+"&pageOffCount="+pageOffCount+"&Act=getinfo&orderByOff="+orderByOff+"&txtNo="+escape($("#txtNo_UC").val())+"&txtTitle="+escape($("#txtTitle_UC").val());
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageOutSellInfo.ashx?'+paUrl,//目标地址
           cache:false,
           beforeSend:function(){$("#pageOffList_Pager").show();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    var index=1;
                    $("#offerDataList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        
                        if(item.ID != null && item.ID != "")
                        {
                            var td="";
                            if($("#HiddenMoreUnit").val()=="True")
                            {
                                td="<td height='22' align='center'>"+item.CodeName+"</td>"+
                                    "<td height='22' align='center'>"+item.ProductCount+"</td>"+
                                    "<td height='22' align='center'>"+item.UnitPrice+"</td>";
                                    $("#frombaseunit").show();
                                    $("#frombasecount").show();
                                    $("#frombaseprice").show();
                            }
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"Checkbox\" name=\"ck\" id=\"ckSellSend_"+index+"\" value=\""+item.ID+"\" />"+"</td>"+
                             "<td height='22' align='center'>"+ item.SendNo +"</td>"+
                             "<td height='22' align='center'>"+ item.Title +"</a></td>"+
                            "<td height='22' align='center'>"+item.SendDate+"</td>"+
                            "<td height='22' align='center'>"+item.ProductName+"</td>"+
                            td+
                            "<td height='22' align='center'>"+item.ProductCount+"</td>"+//发货数量
                            "<td height='22' align='center'>"+item.OutCount+"</td>"+//已出库数量
                            "<td height='22' align='center' style='display:none'>"+item.DetailID+"</td>").appendTo($("#offerDataList tbody"));
                            index=index+1;
                        }
                   });
                    //页码
                   ShowPageBar("pageOffList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerOffStyle
                    ,mark:"pageOffDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageOffCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    End:"末页",
                    First:"首页",
                    onclick:"TurnToOffPage({pageindex});return false;"}//[attr]
                    );
                  totalOffRecord = msg.totalCount;
                  $("#ShowOffPageCount").val(pageOffCount);
                  ShowTotalPage(msg.totalCount,pageOffCount,pageIndex,$("#pageOffcount"));
                  $("#OffToPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#pageOffList_Pager").show();offerDataList("offerDataList","#E7E7E7","#FFFFFF","#cfc","cfc");
            }//接收数据完毕
           });
    }
    //table行颜色
function offerDataList(o,a,b,c,d)
{
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

//改变每页记录数及跳至页数
function ChangeOffPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalOffRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageOffCount=parseInt(newPageCount,10);
        TurnToOffPage(parseInt(newPageIndex,10));
    }
}
//排序
function OrderOffBy(orderColum,orderTip)
{
    var ordering = "d";
    var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↑");
    }
    else
    {
        
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderByOff = orderColum+"_"+ordering;
    TurnToOffPage(1);
}


function closeSellSenddiv()
{
    document.getElementById("divSellSend").style.display="none";
    document.getElementById("divzhezhao").style.display="none";
}

function ckAll1()
{

   var signFrame = findObj("offerDataList",document);
   var ck = document.getElementsByName("ckAll");
   var ck1 = document.getElementsByName("ck");
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
function UCSearch(aa)
{
      TurnToOffPage(1);
}
 /*------------- End    弹出销售发货明细选择层JS-------------------------------------*/
 
 
 /*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    moduleID = document.getElementById("hidModuleID").value;
    window.location.href = "StorageOutSellList.aspx?ModuleID=" + moduleID + searchCondition;
}



/*-----------------明细中输入数据格式验证---------------------*/
function check(str)
{
    fieldText="";var msgText="";var isFlag=true;
    if(document.getElementById(str).value.length>0)
    {
        if(!IsNumeric(document.getElementById(str).value))
            {
                isFlag = false;
                fieldText = fieldText +  "出库数据|";
   		        msgText = msgText + "出库数据输入有误，请输入有效的数值（大于0）！";  
   		        document.getElementById(str).value="0";
   		        document.getElementById(str).focus();
            }
    }
    if(!isFlag)
    {
        popMsgObj.ShowMsg(msgText);
    }

}  


/*单据打印*/
function BillPrint()
{
    var ID=$("#txtIndentityID").val();
    if(ID=="" || ID=="0")
    {
        popMsgObj.Show("打印|","请保存单据再打印|");
        return;
    }
    //if(confirm("请确认您的单据已经保存？"))
       window.open("../../../Pages/PrinttingModel/StorageManager/StorageOutSellPrint.aspx?ID="+ID);
}
//库存快照
function ShowSnapshot()
{

       var intProductID = 0;
       var detailRows = 0;
       var snapProductName = '';
       var snapProductNo = '';
       
       var signFrame = findObj("dg_Log",document); 
       var txtTRLastIndex = signFrame.rows.length;
     
       
        if(txtTRLastIndex>1)
        {
           for(var i=1;i<txtTRLastIndex;i++)
           {
                if(document.getElementById('chk_Option_'+i).checked)
                {
                    detailRows ++;
                    intProductID = document.getElementById('ProductNo_SignItem_TD_Text_'+(i)).title;
                    snapProductName= document.getElementById('ProductName_SignItem_TD_Text_'+(i)).value;
                    snapProductNo = document.getElementById('ProductNo_SignItem_TD_Text_'+(i)).value;
                }
           }
           
           if(detailRows==1)
           {
               if(intProductID<=0 || strlen(cTrim(intProductID,0))<=0)
               {
                  popMsgObj.ShowMsg('选中的明细行中没有添加物品');             
                  return false;  
               }
               
               ShowStorageSnapshot(intProductID,snapProductName,snapProductNo);
           }  else {
            popMsgObj.ShowMsg('请选择单个物品查看库存快照');
            return false;
        }
    }
    else if(txtTRLastIndex ==1){
            popMsgObj.ShowMsg('请选择单个物品查看库存快照');
            return false;
        }  
}