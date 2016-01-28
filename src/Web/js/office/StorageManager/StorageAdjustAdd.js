$(document).ready(function()
{
    fnGetExtAttr();
    IsDisplayBaseUnit();
    GetFlowButton_DisplayControl();
    // document.getElementById('btnPageFlowConfrimUn').style.display='none';
    requestQuaobj = GetRequest();   
     var action=requestQuaobj['myAction'];
     var from=requestQuaobj['from'];
    if(action=='edit' || from=='list')
    {
        document.getElementById('btnReturn').style.display='';
        
    }

    if(AdjustID>0)
    { 
        LoadAdjustInfo(AdjustID);
    }
    else
    {
        GetExtAttr('officedba.StorageAdjust',null);  
    }   
});
function IsDisplayBaseUnit()
{
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

//当前页面更新时，清除行，只留标题
function ClearSignRow()
{
       document.getElementById('txtTRLastIndex').value ="1";
       //移除所有的tr和td
       var signFrame = findObj("dg_Log",document);
       var rowNum=signFrame.rows.length;
       if(rowNum>1)
       {
           for (i=1;i<rowNum;i++)
           {
              signFrame.deleteRow(i);
              rowNum=rowNum-1;
              i=i-1;
            }
        }  
}
function LoadAdjustInfo(AdjustID)
{      
  
       var rowsCount=0;
       ClearSignRow();
       $.ajax({
       type: "POST",//用POST方式传输ss
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/StorageManager/StorageAdjustInfo.ashx?ID="+AdjustID,//目标地址
       cache:false,    
            beforeSend:function(){AddPop();},//发送数据之前             
       success: function(msg){
                var rowsCount = 0;
                var countTotals = 0;
                //数据获取完毕，填充页面据显示
                    //生产任务单信息
                     GetExtAttr('officedba.StorageAdjust',msg.dataAdjust[0]);
                    if(typeof(msg.dataAdjust)!='undefined')
                    {
                       // GetFlowButton_DisplayControl();
                        $.each(msg.dataAdjust,function(i,item)           
                        {
                            document.getElementById('lbInfoNo').value = item.AdjustNo;
                            document.getElementById('hiddenAdjustID').value=item.ID; 
  
                            document.getElementById('txtSubject').value=item.Title;  //
                            document.getElementById('UserExecutor').value=item.ExecutorName;                            
                            document.getElementById('hiddentxtExecutor').value=item.Executor;  
                            document.getElementById('DeptID').value=item.DeptName;                            
                            document.getElementById('hiddenDeptID').value = item.DeptID;  
                      
                            document.getElementById('ddlInStorage').value=item.StorageID;                            
                            document.getElementById('ddlReasonType').value=item.ReasonType;   
                            document.getElementById('AdjustDate').value=item.AdjustDate.substring(0,10); //                                                        
                            document.getElementById('Summary').value=item.Summary;  //                            
                            document.getElementById('TotalPrice').value=parseFloat(item.TotalPrice).toFixed($("#HiddenPoint").val()); // 
                            document.getElementById('CountTotal').value=parseFloat(item.CountTotal).toFixed($("#HiddenPoint").val());                            
                           
                            if(item.BillStatus>=2)
                             {  
                                try
                                {
                                   document.getElementById('btnSave').src='../../../images/Button/UnClick_bc.jpg';     
                                }
                                catch(e){}
                             }
                             document.getElementById('ddlBillStatus').value=item.BillStatus;
                          
                              if(item.BillStatus=='1')
                             {
                                 document.getElementById('tbBillStatus').value='制单';
                             }
                             if(item.BillStatus=='2')
                             {
                                 document.getElementById('tbBillStatus').value='执行';
                             }
                              if(item.BillStatus=='3')
                             {
                                 document.getElementById('tbBillStatus').value='变更';
                             }
                              if(item.BillStatus=='4')
                             {
                                 document.getElementById('tbBillStatus').value='手工结单';
                             }
                             
                            document.getElementById('tbCreater').value=item.CreatorName;                            
                            document.getElementById('txtCreateDate').value=item.CreateDate.substring(0,10);                             
                            document.getElementById('txtConfirmor').value=item.ConfirmorName; 
                            document.getElementById('txtConfirmDate').value=item.ConfirmDate.substring(0,10); 
                            document.getElementById('txtCloserReal').value=item.CloserName;                            
                            document.getElementById('txtCloseDate').value=item.CloseDate.substring(0,10);
                            document.getElementById('txtModifiedUserID').value=item.ModifiedUserIDName;   

                            document.getElementById('txtModifiedDate').value=item.ModifiedDate.substring(0,10);                            
                            document.getElementById('txtRemark').value=item.Remark;  
                            
                            var testurl=item.Attachment.replace(/,/g,"\\");   
                            var testurl2=testurl.lastIndexOf('\\');
                            testurl2=testurl.substring(testurl2+1,testurl.lenght);
                            document.getElementById('attachname').innerHTML=testurl2;
                            document.getElementById('hfPageAttachment').value=testurl;                                      
                            document.getElementById('hfPageAttachment').value=testurl;
                             $("#hiddenBillstatus").val(item.BillStatus);
                                if (document.getElementById('hfPageAttachment').value != "")
                                {            
                                
                                    //下载删除显示
                                    document.getElementById("divDealAttachment").style.display = "";
                                    //上传不显示
                                    document.getElementById("divUploadAttachment").style.display = "none";
                                }  
                            document.getElementById('divCodeRuleUC').style.display='none';
                            document.getElementById('divTaskNo').style.display='';

                        });
                    }             
                    if(typeof(msg.dataDetail)!='undefined')
                    {
                       $.each(msg.dataDetail,function(i,item){
                            if(item.ID != null && item.ID != "")
                            {
                                rowsCount ++ ; 
                                FillSignRow(i,item.ProNo,item.ProductName,item.ProductID,item.CodeName,item.UnitID,item.AdjustType,item.AdjustCount,item.CostPrice,item.CostPriceTotal,item.Remark,item.BatchNo,item.UsedUnitID,item.UsedUnitCount,item.UsedPrice,item.ExRate,item.IsBatchNo);
                            
                            }
                       });
                    }
                    try
                    {
                        GetFlowButton_DisplayControl();
                     }
                     catch(e){}
               document.getElementById('txtTRLastIndex').value = rowsCount+1; 
               var BillStatus=document.getElementById('ddlBillStatus').value;
               if(BillStatus=='2')
                {
                document.getElementById('txtConfirmor').style.display='';
                document.getElementById('txtConfirmDate').style.display='';
                }
                if(BillStatus=='4')
                {
                document.getElementById('txtConfirmor').style.display='';
                document.getElementById('txtConfirmDate').style.display='';
                document.getElementById('txtCloseDate').style.display='';
                document.getElementById('txtCloser').style.display='';
                }   

              },
       error: function() {alert('加载数据时发生请求异常');}, 
       complete:function(){hidePopup();}
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
//---------End   销售入库保存 ------
function GetFloat(a)
{
   if(a!=null && a!='')
   {
        var testFlaot=a.toString().indexOf('.');
        var myValue=a;
        if(testFlaot!=-1)
        {
            var length=a.length;
            for(var i=0;i<length;i++)
            {
                
            }
            
            var testvalue=a.toString().split('.');
            var myvalue=testvalue[1];
            
            if(myvalue=='00' || myvalue=='0' || myvalue =='000' || myvalue=='0000')
            {
                myValue= testvalue[0];
            } 
            if(myvalue.toString().length>2 && myvalue!='000' && myvalue!='0000')
            {
                myValue= parseFloat(a.toString()).toFixed($("#HiddenPoint").val());
            }       
            
        }
        return myValue;
    }
}
//删除 行
function DeleteTheSignRow()
{
    var signFrame = findObj("dg_Log",document);        
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        if(signFrame.rows[i+1].style.display!="none")
        {
            var objRadio = 'Chk_Option_'+(i+1);
            if(document.getElementById(objRadio.toString()).checked)
            {
                signFrame.rows[i+1].style.display = 'none';
                CountAll(i+1);
            }
        }
    }
}

//-------------------------保存Start
function Fun_Save_Adjust()
{
    CountTotalAll();
   if(document.getElementById('hiddenAdjustID').value>0)
   {
      if(document.getElementById('ddlBillStatus').value>=2)
      {       
        return false;
      }
   }
  if(CheckInput())
  {

    var AdjustNo="";
    var UrlParam='';
    var myAction='add';
    var bmgz='';
    var myMethod=document.getElementById('hiddenAdjustID').value;   //该单据ID
    if(myMethod=='0')  // 新建
    {
      if($("#checkNo_ddlCodeRule").val()=="")//手工输入
       {
        AdjustNo=$("#checkNo_txtCode").val();
        bmgz="sd";
       }
     else
       {
        AdjustNo=$("#checkNo_ddlCodeRule").val();
        bmgz="zd";
       }
    }
    else   //编辑
    {
        AdjustNo=document.getElementById('lbInfoNo').value;
        myAction='edit';
    }

    var Title=document.getElementById('txtSubject').value;
    var Executor=document.getElementById('hiddentxtExecutor').value;   //-------------------------经办人
    var DeptID=document.getElementById('hiddenDeptID').value; //
    var StorageID=document.getElementById('ddlInStorage').value; //    
    var ReasonID=document.getElementById('ddlReasonType').value;  //  
    var AdjustDate=document.getElementById('AdjustDate').value;
    var Summary=document.getElementById('Summary').value;
    var TotalPrice=document.getElementById('TotalPrice').value; //金额合计
    var CountTotal=document.getElementById('CountTotal').value;//数量合计 
    var Remark=$("#txtRemark").val(); 
    var Attachment=document.getElementById('hfPageAttachment').value;
    var myBillStatus=document.getElementById('ddlBillStatus').value;
    
    
   
    
    //-----------------------------------------------------------明细Start
    var DetailSortNo=new Array();        //序号
    var DetailProNo=new Array();     //原因
  
    var DetailUnitID=new Array();    //单位
    var DetailAdjustType=new Array();   //
    var DetailProID=new Array(); 
    var DetailAdjustCount=new Array();//数量
    var DetailCostPrice=new Array();//单价
    var DetailCostPriceTotal=new Array();
    var DetailRemark=new Array();
    
    var DetailBaseUnitID = new Array();//基本单位
    var DetailBaseCount = new Array();//基本数量
    var DetailBasePrice = new Array();//基本单价
    var DetailExtRate = new Array();//比率
    var DetailBatchNo = new Array();//批次
    
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        {   
             var theProNo=document.getElementById('TD_ProNo_'+(i)).value;     //
             var theProID=document.getElementById('TD_ProID_'+(i)).value; //
             //var theUnit=document.getElementById('TD_UnitID_'+(i)).value;    //实际单位
             var theAdjustType=document.getElementById('TD_AdjustType'+(i)).value;   //
             var theAdjustCount=document.getElementById('TD_AdjustCount_'+(i)).value;//实际数量
             
             var theCostPrice=document.getElementById('TD_CostPrice_'+(i)).value;    //实际单价
             var theAdjustType=document.getElementById('TD_AdjustType'+(i)).value;   //
             var theCostPriceTotal=document.getElementById('TD_CostPriceTotal_'+(i)).value;
             var theRemark=document.getElementById('TD_Remark_'+(i)).value;
             
             var theUnit='TD_UnitID_'+(i);//实际单位   
             
            var objBaseUnitID='BaseUnit_SignItem_TD_Text_'+(i);//基本单位
            var objBaseCount='BaseCount_SignItem_TD_Text_'+(i);//基本数量
            var objBasePrice='baseprice_td'+(i);//基本单价
            var objExtRate='UnitID_SignItem_TD_Text_'+(i);//比率
            var objBatchNo='BatchNo_SignItem_TD_Text_'+(i);//批次
            
              DetailSortNo.push(i);
              DetailProNo.push(theProNo);     //原因
              DetailProID.push(theProID);
              //DetailUnitID.push(theUnit);
              DetailAdjustType.push(theAdjustType);
              //DetailAdjustCount.push(theAdjustCount);
             // DetailCostPrice.push(theCostPrice);
              DetailCostPriceTotal.push(theCostPriceTotal);
              DetailRemark.push(theRemark);
              
               if($("#HiddenMoreUnit").val()=="False")//未启用时(实际使用的存入基本单位中)
                {
                    DetailBaseUnitID.push(document.getElementById(theUnit.toString()).value);//单位                
                    DetailBaseCount.push(theAdjustCount);//数量
                    DetailBasePrice.push(theCostPrice);//单价
                }     
                else
                {
                    DetailBaseUnitID.push(document.getElementById(objBaseUnitID.toString()).title);//基本单位                
                    DetailBaseCount.push(document.getElementById(objBaseCount.toString()).value);//基本数量
                    DetailBasePrice.push(document.getElementById(objBasePrice.toString()).value);//基本单价
                    
                    DetailUnitID.push(document.getElementById(theUnit.toString()).value.split('|')[0].toString());//实际单位ID  
                    DetailAdjustCount.push(theAdjustCount);//实际数量 
                    DetailCostPrice.push(theCostPrice);//实际单价
                    DetailExtRate.push(document.getElementById(theUnit.toString()).value.split('|')[1].toString());//比率
                } 
                DetailBatchNo.push(document.getElementById(objBatchNo.toString()).value);//批次
        }        
    }  
    //------------------------------明细End

                                     
UrlParam="Title="+escape(Title)+                                          
        "&Executor="+Executor+ 
        "&DeptID="+DeptID+   
        "&myBillStatus="+myBillStatus+                              
        "&StorageID="+StorageID+                                       
        "&ReasonID="+ReasonID+  
        "&Remark="+escape(Remark)+ 
        "&Attachment="+escape(Attachment)+                              
        "&AdjustDate="+AdjustDate+                              
        "&Summary="+escape(Summary)+                            
        "&TotalPrice="+TotalPrice+                      
        "&CountTotal="+CountTotal+   
        "&DetailSortNo="+DetailSortNo.toString()+         
        "&DetailProNo="+DetailProNo.toString()+       
        "&DetailProID="+DetailProID.toString()+  
        "&DetailUnitID="+DetailUnitID.toString()+  
        "&DetailAdjustType="+DetailAdjustType.toString()+       
        "&DetailAdjustCount="+DetailAdjustCount.toString()+  
        "&DetailCostPrice="+DetailCostPrice.toString()+       
        "&DetailCostPriceTotal="+DetailCostPriceTotal.toString()+     
        "&DetailRemark="+escape(DetailRemark.toString())+               
        "&myAction="+myAction+
        "&DetailBaseUnitID=" + escape(DetailBaseUnitID.toString())+//
        "&DetailBaseCount=" + escape(DetailBaseCount.toString())+//
        "&DetailBasePrice=" + escape(DetailBasePrice.toString())+//
        "&DetailExtRate=" + escape(DetailExtRate.toString())+//
        "&DetailBatchNo=" + escape(DetailBatchNo.toString())+//
        "&bmgz="+bmgz+
        "&AdjustNo="+AdjustNo+                                        
        "&ID="+myMethod+GetExtAttrValue();                                            

        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageAdjustAdd.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('保存库存调整单时请求发生错误');
                  }, 
                  success:function(data) 
                  { 
                     var reInfo=data.data.split('|');

                     if(reInfo.length > 1)
                       {   
                            popMsgObj.ShowMsg(data.info);
                            document.getElementById('lbInfoNo').value = reInfo[1];
                            document.getElementById('hiddenAdjustID').value=reInfo[0];
                            if(myMethod>0)
                            {
                                if(myBillStatus=="2")
                                {
                                   document.getElementById('tbBillStatus').value='变更';
                                   document.getElementById('ddlBillStatus').value='3';
                                }
                                document.getElementById('txtModifiedDate').value=reInfo[2].substring(0,9);
                                document.getElementById('txtModifiedUserID').value=reInfo[3];         
                            }
                            document.getElementById('divCodeRuleUC').style.display='none';                       
                            document.getElementById('divTaskNo').style.display=''; 
             
                      }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                    GetFlowButton_DisplayControl();
                  } 
              }); 
    }
}
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var txtProcessNo = '';  
    
    var thetxtSubject = document.getElementById('txtSubject').value;
    var theExecutor=document.getElementById('UserExecutor').value;    //经办人
    var theDeptID=document.getElementById('DeptID').value;  //经办部门
    var theStorage=document.getElementById('ddlInStorage').value; // 调整仓库
    var theReasonType=document.getElementById('ddlReasonType').value;//调整原因
    var TotalPrice=document.getElementById('TotalPrice').value;//调整数量合计
    var CountTotal=document.getElementById('CountTotal').value;//调整金额合计
    var AdjustDate=document.getElementById('AdjustDate').value;
        var BeginTime=document.getElementById('BeginTime');
    var EndTime=document.getElementById('EndTime');
    //获取编码规则下拉列表选中项
    codeRule = document.getElementById("checkNo_ddlCodeRule").value; 
    //如果选中的是 手工输入时，校验编号是否输入
    if (codeRule == "")
    {
        //获取输入的编号
        txtProcessNo = document.getElementById("checkNo_txtCode").value;
        //编号必须输入
        if(parseFloat(document.getElementById('hiddenAdjustID').value)>0)
        {
            if(document.getElementById('lbInfoNo').value=='')
            {
                 isFlag = false;
                fieldText += "单据编号|";
	            msgText += "请输入单据编号|";
            }
        }
        else
        {
            if (txtProcessNo == "")
            {
                isFlag = false;
                fieldText += "单据编号|";
	            msgText += "请输入单据编号|";
            }
                    else
            {
                if (isnumberorLetters(txtProcessNo))
                {
                    isFlag = false;
                    fieldText += "单据编号|";
                    msgText += "单据编号只能包含字母或数字！|";
                }
            }
        }

    }
   

//    if(strlen(thetxtSubject)<=0)
//    {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//   		msgText = msgText +  "请输入单据主题 |";
//    }
//    if(strlen(thetxtSubject)>0)
//    {
//         if(trim(thetxtSubject)=='')
//        {
//            isFlag = false;
//            fieldText = fieldText + "主题|";
//   		    msgText = msgText +  "不能输入空主题 |";
//        }
//    }
//    if(!CheckSpecialWord(thetxtSubject))
//    {
//        isFlag = false;
//        fieldText = fieldText + "主题|";
//   		msgText = msgText +  "单据主题不能含有特殊字符 |";
//    }
    if(strlen(thetxtSubject)>100)
    {
        isFlag = false;
        fieldText = fieldText + "主题|";
   		msgText = msgText +  "仅限于100个字符以内|";
    }
    if(strlen(theExecutor)<=0)
    {
        isFlag = false;
        fieldText = fieldText + "经办人|";
   		msgText = msgText +  "请输入经办人|";
    }
    if(strlen(theDeptID)<=0)
    {
         isFlag = false;
        fieldText = fieldText + "调整部门|";
   		msgText = msgText +  "请输入调整部门 |";
    } 
 
    if(document.getElementById('ddlInStorage').options.length==1)
    {
        isFlag = false;
        fieldText = fieldText + "调整仓库|";
   		msgText = msgText +  "请先添加调整仓库后再进行该操作 |";
    }
    else
    {
          if(theStorage=='0')
          {
             isFlag = false;
             fieldText = fieldText + "调整仓库|";
   		     msgText = msgText +  "请选择调整仓库 |";
          } 
    }
    if(document.getElementById('ddlReasonType').options.length==1)
    {
        isFlag = false;
        fieldText = fieldText + "调整原因|";
   		msgText = msgText +  "请先添加调整原因后再进行该操作 |";
    }
    else
    {
        if(theReasonType=='0') 
        {
             isFlag = false;
            fieldText = fieldText + "调整原因|";
   		    msgText = msgText +  "请选择调整原因 |";
        } 
    } 
      var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + "不能含有特殊字符|";
    }
   //Start 验证明细列表输入
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
//    var point=$("#");

    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        if(signFrame.rows[i+1].style.display!="none")    
        {
                var objProNo= 'TD_ProNo_'+(i+1);         //物品编号
                var objAdjustCount= 'TD_AdjustCount_'+(i+1);          //调整数量
                var objCostPrice= 'TD_CostPrice_'+(i+1);                //成本单价numeric(14,4)
                var objCostPriceTotal= 'TD_CostPriceTotal_'+(i+1);                    //调整金额
                var objRemark='TD_Remark_'+(i+1);
                var myobjProNo = document.getElementById(objProNo.toString()).value;
                var myobjAdjustCount = document.getElementById(objAdjustCount.toString()).value;
                var myobjCostPrice = document.getElementById(objCostPrice.toString()).value;
                var myobjCostPriceTotal = document.getElementById(objCostPriceTotal.toString()).value;
                var myobjRemark=document.getElementById(objRemark.toString()).value;
                if(strlen(myobjProNo)<=0)
                {
                    isFlag = false;
                    fieldText = fieldText + "明细物品编号"+(i+1)+"|";
                    msgText = msgText +  "请输入明细物品编号|";
                }
                if(strlen(myobjAdjustCount)<=0)
                {
                    isFlag = false;
                    fieldText = fieldText + "明细调整数量"+(i+1)+"|";
                    msgText = msgText +  "请输入明细调整数量|";
                }
                if(!CheckSpecialWord(myobjAdjustCount))
                {
                    isFlag = false;
                    fieldText = fieldText + "明细调整数量|";
   		            msgText = msgText +  "调整数量不能含有特殊字符 |";                  
                }
                if(strlen(myobjAdjustCount)>0)
                {
                   if(parseFloat(myobjAdjustCount)<=0)
                   {
                        isFlag = false;
                        fieldText = fieldText + "明细调整数量"+(i+1)+"|";
                        msgText = msgText +  "明细调整数量输入有误，请输入有效的数值（大于0）！|"; 
                   }
                } 
                 if(strlen(myobjAdjustCount)>0)
                {
                   if(!IsNumeric(myobjAdjustCount))
                   {
                        isFlag = false;
                        fieldText = fieldText + "明细调整数量"+(i+1)+"|";
                        msgText = msgText +  "明细调整数量格式不正确！|"; 
                   }
                }     
                if(strlen(myobjCostPriceTotal)<=0)
                {
                    isFlag = false;
                    fieldText = fieldText + "明细调整金额"+(i+1)+"|";
                    msgText = msgText +  "请输入明细调整金额|";
                }
                if(!CheckSpecialWord(myobjCostPriceTotal))
                {
                    isFlag = false;
                    fieldText = fieldText + "明细调整金额|";
   		            msgText = msgText +  "明细调整金额不能含有特殊字符 |";
                }
                 if(strlen(myobjCostPriceTotal)>0)
                {
                   if(parseFloat(myobjCostPriceTotal)<=0)
                   {
                        isFlag = false;
                        fieldText = fieldText + "明细调整金额"+(i+1)+"|";
                        msgText = msgText +  "明细调整金额输入有误，请输入有效的数值（大于0）！|";
                   }
                } 
                if(!CheckSpecialWord(myobjRemark))
                {
                    isFlag = false;
                    fieldText = fieldText + "明细备注|";
   		            msgText = msgText +  "明细备注不能含有特殊字符 |";
                }              
         }
    }

    var signFrame = findObj("dg_Log",document);
    if(signFrame.rows.length<=1)
      {
         isFlag = false;
         fieldText = fieldText +  "明细信息|";
         msgText = msgText + "明细信息不能为空|";  
      }

    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
/*点击添加行按钮*/
function AddSignRow()
{        
        var txtTRLastIndex = findObj("txtTRLastIndex",document);
        var rowID = parseInt(txtTRLastIndex.value);
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "Row_" + rowID;
        var colChoose=newTR.insertCell(0);//添加列:选择
        colChoose.className="cell";
        colChoose.align="center";
        colChoose.innerHTML="<input name='Chk' onclick=\"IfSelectAll('Chk','CheckAll');\"  id='Chk_Option_"+rowID+"'  value=\"0\" type='checkbox' />";

//        var colNum=newTR.insertCell(1);//添加列:序号
//        colNum.className="cell";
//        colNum.innerHTML = "<input type=\"text\"  style='width:60%' id=\"TD_Num_"+rowID+"\" value=\""+rowID+"\"  class=\"tdinput\"  readonly />";
        
        
        var colProductNo=newTR.insertCell(1);//添加列 物品编号
        colProductNo.className="cell";
        colProductNo.innerHTML = "<input type=\"text\" size=\"15\" readOnly onfocus=\"popTechObj.ShowListSpecial('"+rowID+"','d');\" id='TD_ProNo_"+rowID+"' name='TD_ProNo_"+rowID+"' class=\"tdinput\" />";
        
        var colProductName=newTR.insertCell(2);//添加列:物品名称
        colProductName.className="cell";
        colProductName.innerHTML = "<input type=\"text\"  size=\"15\" readOnly  id=\"TD_Pro_"+rowID+"\" class=\"tdinput\"    /><input id=\"TD_ProID_"+rowID+"\" type=\"hidden\" >";
        
        /*增加批次列：2010.4.13*/
        var newBatchNotd=newTR.insertCell(3);//添加列:批次
        newBatchNotd.className="cell";
        newBatchNotd.id = 'SignItem_TD_BatchNo_'+rowID;
        newBatchNotd.innerHTML ="<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_"+rowID+"' />";//添加列内容
        
        /*增加基本单位、基本数量列：2010.4.13*/
        var newBaseUnit=newTR.insertCell(4);//添加列:基本单位
        newBaseUnit.className="cell";
        newBaseUnit.id = 'SignItem_TD_BaseUnit_'+rowID;
        newBaseUnit.innerHTML ="<input id='BaseUnit_SignItem_TD_Text_"+rowID+"' type='text'  class=\"tdinput\"  style='width:90%' readonly />";;//添加列内容
        
        var DefaultPoint="0.00";
        var newBaseCount=newTR.insertCell(5);//添加列：基本数量
        newBaseCount.className="cell";
        newBaseCount.id = 'SignItem_TD_BaseCount_'+rowID;
        newBaseCount.innerHTML ="<input id='BaseCount_SignItem_TD_Text_"+rowID+"'   type='text'  class=\"tdinput\"  readonly style='width:90%' />";;//添加列内容

        if($("#HiddenMoreUnit").val()=="False")
        {
            newBaseUnit.style.display="none"
            newBaseCount.style.display="none"        
            var colUnitID=newTR.insertCell(6);//添加列:单位
            colUnitID.className="cell";
            colUnitID.innerHTML = "<input type=\"text\" size=\"5\" readOnly  id='TD_Unit_"+rowID+"'  class=\"tdinput\"   /><input id=\"TD_UnitID_"+rowID+"\" type=\"hidden\">";
        }
        else
        {
            $("#baseuint").show();
            $("#basecount").show();
            var newFitNametd=newTR.insertCell(6);//添加列:单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
        }
        
        var colSpecification=newTR.insertCell(7);//添加列:调整类型
        colSpecification.className="cell";
        colSpecification.innerHTML = "<select id='TD_AdjustType"+rowID+"'><option value=\"1\">调增</option><option selected value=\"0\">调减</option></select>";
        
        var point=$("#HiddenPoint").val();
        if($("#HiddenMoreUnit").val()=="False")
        {
            var colProductCount=newTR.insertCell(8);//添加列:调整数量
            colProductCount.className="cell";
            colProductCount.innerHTML = "<input size=\"15\" specialworkcheck='调整数量' onchange=\"Number_round(this,'"+point+"');\" onblur=\"CountAll('"+rowID+"');\"  id='TD_AdjustCount_"+rowID+"' type=\"text\" class=\"tdinput\" />";
        }
        else
        {
            var newFitNametd=newTR.insertCell(8);//添加列:调整数量(根据基本数量计算)
            newFitNametd.className="tdinput";
            newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"TD_AdjustCount_"+rowID+"\" class=\"tdinput\"  style='width:90%' onblur=\"Number_round(this,'"+point+"');TotalPrice_MoreUnit(this.id,"+rowID+");\"/>";//添加列内容
        }
        if($("#HiddenMoreUnit").val()=="False")
        {
            var colBom=newTR.insertCell(9);//添加列:成本单价
            colBom.className="cell";      
            colBom.innerHTML = "<input type=\"text\" readOnly size=\"5\" onchange=\"Number_round(this,'"+point+"');\"  id='TD_CostPrice_"+rowID+"' class=\"tdinput\"  />";
        }
        else
        {
            var newFitDesctd=newTR.insertCell(9);//添加列:出库单价(根据基本单价计算,隐藏基本单价，比率)
            newFitDesctd.className="cell";
            newFitDesctd.id = 'SignItem_TD_UnitPrice_'+rowID;
            newFitDesctd.innerHTML = "<input type='hidden' id='baseprice_td"+rowID+"'><input type='hidden' id='BaseExRate"+rowID+"'><input name='chk' id='TD_CostPrice_"+rowID+"'  type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";//添加列内容
        }
        
        var colRouteID=newTR.insertCell(10);//添加列:调整金额
        colRouteID.className="cell";
        colRouteID.innerHTML = "<input type=\"text\" specialworkcheck='调整金额' size=\"15\" onchange=\"Number_round(this,'"+point+"');\"  class=\"tdinput\" id='TD_CostPriceTotal_"+rowID+"' />";
          
               
        var colRemark=newTR.insertCell(11);//添加列:备注
        colRemark.className="cell";
        colRemark.innerHTML = "<input type=\"text\"  size=\"15\" specialworkcheck='备注'  id='TD_Remark_"+rowID+"' value=\"\"  class=\"tdinput\"  />";
        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
        
}
/*从列表进入查看页面填充明细*/
function FillSignRow(a,b,c,d,e,f,g,h,i,j,k,
BatchNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,IsBatchNo
)
{        
        if(UsedPrice=="")UsedPrice="0.00";
        if(UsedUnitID=="")UsedUnitID="0.00";
        if(UsedUnitCount=="")UsedUnitCount="0.00";
        if(ExRate=="")ExRate="0.00";
        if(h!="")h=parseFloat(h).toFixed($("#HiddenPoint").val());
        if(j!="")j=parseFloat(j).toFixed($("#HiddenPoint").val());
        if(UsedUnitCount!="")UsedUnitCount=parseFloat(UsedUnitCount).toFixed($("#HiddenPoint").val());
        if(UsedPrice!="")UsedPrice=parseFloat(UsedPrice).toFixed($("#HiddenPoint").val());

        var StorageID=document.getElementById('ddlInStorage').value;

        var rowID = parseInt(a)+1;
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行

        newTR.id = "Row_" + rowID;

        var colChoose=newTR.insertCell(0);//添加列:选择
        colChoose.className="cell";
        colChoose.align="center";
        colChoose.innerHTML="<input name='Chk' onclick=\"IfSelectAll('Chk','CheckAll');\"  id='Chk_Option_"+rowID+"'  value=\"0\" type='checkbox' />";

//        var colNum=newTR.insertCell(1);//添加列:序号
//        colNum.className="cell";
//        colNum.innerHTML = "<input type=\"text\"  style='width:20%'  id=\"TD_Num_"+rowID+"\" value=\""+rowID+"\"  class=\"tdinput\"  readonly />";

        var colProductNo=newTR.insertCell(1);//添加列 物品编号
        colProductNo.className="cell";
        colProductNo.innerHTML = "<input type=\"text\" size=\"15\" onclick=\"popTechObj.ShowListSpecial("+rowID+",'d');\" id='TD_ProNo_"+rowID+"' value=\""+b+"\" name='TD_ProNo_"+rowID+"' class=\"tdinput\" />";

        var colProductName=newTR.insertCell(2);//添加列:物品名称
        colProductName.className="cell";
        colProductName.innerHTML = "<input type=\"text\"  size=\"15\" value=\""+c+"\" id=\"TD_Pro_"+rowID+"\" class=\"tdinput\"    /><input id=\"TD_ProID_"+rowID+"\" value=\""+d+"\" type=\"hidden\" >";
        
        
        //增加批次列：2010.04.13
        var newBatchNotd=newTR.insertCell(3);//添加列:批次
        newBatchNotd.className="cell";
        newBatchNotd.id = 'SignItem_TD_BatchNo_'+rowID;
        newBatchNotd.innerHTML ="<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_"+rowID+"' />";//添加列内容
        /*绑定批次*/
        var ListControlID="BatchNo_SignItem_TD_Text_"+rowID;
        var StorageControlID="ddlInStorage";
        GetBatchList(d,StorageControlID,ListControlID,IsBatchNo,BatchNo);

        //增加基本单位、基本数量列：2010.04.12
        var newBaseUnit=newTR.insertCell(4);//添加列:基本单位
        newBaseUnit.className="cell";
        newBaseUnit.id = 'SignItem_TD_BaseUnit_'+rowID;
        newBaseUnit.innerHTML ="<input id='BaseUnit_SignItem_TD_Text_"+rowID+"' value=\""+e+"\" title=\""+f+"\" type='text' class=\"tdinput\"  style='width:90%' />";;//添加列内容
        
        var newBaseCount=newTR.insertCell(5);//添加列：基本数量
        newBaseCount.className="cell";
        newBaseCount.id = 'SignItem_TD_BaseCount_'+rowID;
        newBaseCount.innerHTML ="<input id='BaseCount_SignItem_TD_Text_"+rowID+"' value=\""+h+"\" type='text' class=\"tdinput\"  style='width:90%' />";;//添加列内容
   
       if($("#HiddenMoreUnit").val()=="False")
        {
            newBaseUnit.style.display="none"
            newBaseCount.style.display="none"   
            var colUnitID=newTR.insertCell(6);//添加列:单位
            colUnitID.className="cell";
            colUnitID.innerHTML = "<input type=\"text\" size=\"5\" value=\""+e+"\"  id='TD_Unit_"+rowID+"'  class=\"tdinput\"   /><input id=\"TD_UnitID_"+rowID+"\" value=\""+f+"\" type=\"hidden\">";
        }
        else
        {
            $("#baseuint").show();
            $("#basecount").show();
            var newFitNametd=newTR.insertCell(6);//添加列:单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
        }
        
        var colSpecification=newTR.insertCell(7);//添加列:调整类型
        colSpecification.className="cell";
        colSpecification.innerHTML = "<select id='TD_AdjustType"+rowID+"'><option value=\"1\">调增</option><option selected value=\"0\">调减</option></select>";
        document.getElementById('TD_AdjustType'+rowID.toString()).value=g;
        
        if($("#HiddenMoreUnit").val()=="False")
        {
            var colProductCount=newTR.insertCell(8);//添加列:调整数量
            colProductCount.className="cell";
            colProductCount.innerHTML = "<input size=\"15\" specialworkcheck='调整数量' onchange=\"Number_round(this,2);\" onblur=\"CountAll('"+rowID+"');\" id='TD_AdjustCount_"+rowID+"' value=\""+parseFloat(h).toFixed($("#HiddenPoint").val())+"\" type=\"text\" class=\"tdinput\" />";
        }
        else
        {
            var newFitNametd=newTR.insertCell(8);//添加列:调整数量
            newFitNametd.className="tdinput";
            newFitNametd.id = 'SignItem_TD_ProductCount_'+rowID;
            newFitNametd.innerHTML = "<input type=\"text\" id=\"TD_AdjustCount_"+rowID+"\" value=\""+UsedUnitCount+"\"  class=\"tdinput\"  style='width:90%' onblur=\"TotalPrice_MoreUnit(this.id,"+rowID+");\"/>";//添加列内容
        }
        
        if($("#HiddenMoreUnit").val()=="False")
        {
            var colBom=newTR.insertCell(9);//添加列:成本单价
            colBom.className="cell";      
            colBom.innerHTML = "<input type=\"text\" onchange=\"Number_round(this,2);\" size=\"5\" value=\""+parseFloat(i).toFixed($("#HiddenPoint").val())+"\"  id='TD_CostPrice_"+rowID+"' class=\"tdinput\"  />";
        }
        else
        {
            UsedPrice=(parseFloat(UsedPrice)).toFixed($("#HiddenPoint").val());
            var newFitDesctd=newTR.insertCell(9);//添加列:出库单价(根据基本单价计算,隐藏基本单价，比率)
            newFitDesctd.className="cell";
            newFitDesctd.id = 'SignItem_TD_UnitPrice_'+rowID;
            newFitDesctd.innerHTML = "<input type='hidden' value=\""+i+"\" id='baseprice_td"+rowID+"'><input type='hidden' id='BaseExRate"+rowID+"' value=\""+ExRate+"\"><input name='chk' id='TD_CostPrice_"+rowID+"' value=\""+UsedPrice+"\"  type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />";//添加列内容
        }
        if($("#HiddenMoreUnit").val()=="True")//绑定单位组
            GetUnitGroupSelectEx(d,"StockUnit", "TD_UnitID_" + rowID,"ChangeUnit(this.id,"+rowID+","+i+")","unitdiv" + rowID,UsedUnitID,"LoadUnitContent("+rowID+","+UsedUnitID+")");//

        var colRouteID=newTR.insertCell(10);//添加列:调整金额
        colRouteID.className="cell";
        colRouteID.innerHTML = "<input type=\"text\"  onchange=\"Number_round(this,2);\" specialworkcheck='调整金额' size=\"15\" value=\""+parseFloat(j).toFixed($("#HiddenPoint").val())+"\"  class=\"tdinput\" id='TD_CostPriceTotal_"+rowID+"' />";
          
               
        var colRemark=newTR.insertCell(11);//添加列:备注
        colRemark.className="cell";
        colRemark.innerHTML = "<input type=\"text\"  size=\"15\" specialworkcheck='备注' id='TD_Remark_"+rowID+"' value=\""+k+"\"  class=\"tdinput\"  />";
        
        document.getElementById('txtTRLastIndex').value = (rowID + 1).toString() ;//将行号推进下一行
        
}
/*选择物品控件物品 赋值*/
 function Fun_FillParent_Content(id,prodNo,prodName,standardSell,unitID,codeName,taxRate,sellTax,discount,specification,codeTypeName,typeID,StandardCost
                            ,Source,GroupUnitNo,SaleUnitID,SaleUnitName,InUnitID,InUnitName, StockUnitID,StockUnitName,MakeUnitID,MakeUnitName,IsBatchNo,BatchNo)
 {
    if(taxRate!="")taxRate=parseFloat(taxRate).toFixed($("#HiddenPoint").val());
    if(sellTax!="")sellTax=parseFloat(sellTax).toFixed($("#HiddenPoint").val());
    if(discount!="")discount=parseFloat(discount).toFixed($("#HiddenPoint").val());
    if(StandardCost!="")StandardCost=parseFloat(StandardCost).toFixed($("#HiddenPoint").val());
     /*popTechObj.InputObj:行号*/
    if(popTechObj.InputObj!=null)
    {
       document.getElementById('TD_ProNo_'+popTechObj.InputObj).value=prodNo;
       document.getElementById('TD_Pro_'+popTechObj.InputObj).value=prodName;
       document.getElementById('TD_ProID_'+popTechObj.InputObj).value=id;
        //绑定批次
        var ListControlID="BatchNo_SignItem_TD_Text_"+popTechObj.InputObj;
        var StorageControlID="ddlInStorage";
        GetBatchList(id,StorageControlID,ListControlID,IsBatchNo,BatchNo);
        //多计量单位
        if($("#HiddenMoreUnit").val()=="True")
        {
           var BasePriceControl="baseprice_td"+popTechObj.InputObj;
           var BaseUnitControl="BaseUnit_SignItem_TD_Text_"+popTechObj.InputObj;
           $("#"+BaseUnitControl).val(codeName);
           $("#" + BaseUnitControl).attr("title", unitID);
           $("#"+BasePriceControl).val(StandardCost);
           GetUnitGroupSelectEx(id,"StockUnit", "TD_UnitID_" + popTechObj.InputObj,"ChangeUnit(this.id,"+popTechObj.InputObj+","+StandardCost+")","unitdiv" + popTechObj.InputObj,'',"FillContent("+popTechObj.InputObj+","+StandardCost+")");
        }//
        else
        {
           document.getElementById('TD_Unit_'+popTechObj.InputObj).value=codeName;
           document.getElementById('TD_UnitID_'+popTechObj.InputObj).value=unitID;
           document.getElementById('TD_CostPrice_'+popTechObj.InputObj).value=StandardCost;
           document.getElementById('divStorageProduct').style.display='none';
        }
   }

 }
 
//暂时不用
 function LoadUnitContent(rowid,usedunit)
 {
//   var exrate=$("#BaseExRate"+rowid).val();
//   var usedunitvalue=usedunit+"|"+exrate;
//   $("#UnitID_SignItem_TD_Text_"+rowid).val(usedunitvalue); /*比率*/
 }
 
//本行小计，，数量变动时根据比率算出基本数量
function TotalPrice_MoreUnit(id,rowid)
{
    try{
       var EXRate = $("#TD_UnitID_"+rowid).val().split('|')[1].toString(); /*比率*/
       var AcCount = $("#TD_AdjustCount_"+rowid).val(); /*调整数量*/
       var unitprice=document.getElementById('TD_CostPrice_'+rowid).value;//单价
        document.getElementById('TD_CostPriceTotal_'+rowid).value=(AcCount*unitprice).toFixed($("#HiddenPoint").val());
        if(EXRate!="0")
            document.getElementById('BaseCount_SignItem_TD_Text_'+rowid).value=(AcCount*EXRate).toFixed($("#HiddenPoint").val());
        CountAll(rowid);
       }
    catch(Error){}
}
//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,RowID/*行号*/,UnitPrice/*基本单价*/)
 {
    var EXRate = $("#TD_UnitID_"+RowID).val().split('|')[1].toString(); /*比率*/
    var ProductCount = $("#TD_AdjustCount_"+RowID).val();/*调整数量*/
    if (EXRate != '')
    {
        var tempcount=parseFloat(ProductCount*EXRate).toFixed($("#HiddenPoint").val());
        var tempprice=parseFloat(UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
        $("#BaseCount_SignItem_TD_Text_"+RowID).val(tempcount);/*基本数量根据报损数量和比率算出*/
        $("#TD_CostPrice_"+RowID).val(tempprice);/*单价根据基本单价和比率算出*/
        $("#TD_CostPriceTotal_"+RowID).val(parseFloat(ProductCount*tempprice).toFixed($("#HiddenPoint").val()));/*金额*/
    }
    CountAll(RowID);
 }
 /*点击物品后填充单价根据基本单价和比率*/
 function FillContent(RowID,UnitPrice)
 {
     var EXRate = $("#TD_UnitID_"+RowID).val().split('|')[1].toString(); /*比率*/
     if (EXRate != '')
     {
        var tempprice=parseFloat(UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
        $("#TD_CostPrice_"+RowID).val(tempprice);/*单价根据基本单价和比率算出*/
     }
    CountAll(RowID);
 }
function  CountAll(a)
{
    var TotalPrice=document.getElementById('TotalPrice');  //数量
    var CountTotal=document.getElementById('CountTotal');  //调整金额
    var CostPrice=document.getElementById('TD_CostPrice_'+a.toString());         //单价
    var TD_AdjustCount=document.getElementById('TD_AdjustCount_'+a.toString());  //调整数量
    var CostPriceTotal=document.getElementById('TD_CostPriceTotal_'+a.toString());
    
    if(parseFloat(TD_AdjustCount.value)>=0 && parseFloat(CostPrice.value)>=0)
    {
        CostPriceTotal.value=parseFloat(CostPrice.value*TD_AdjustCount.value).toFixed($("#HiddenPoint").val());
    }
    else
    {
        CostPriceTotal.value='';
    }
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
    var testPrice=0;
    var testCount=0;
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        if(signFrame.rows[i+1].style.display!="none")    
        {
          var myCostPriceTotal=document.getElementById('TD_CostPriceTotal_'+(i+1)).value;
          var myAdjustCount=document.getElementById('TD_AdjustCount_'+(i+1)).value;
          
          testPrice= parseFloat(parseFloat(testPrice)+parseFloat(myCostPriceTotal)).toFixed($("#HiddenPoint").val());
          testCount=parseFloat(parseFloat(testCount)+parseFloat(myAdjustCount)).toFixed($("#HiddenPoint").val());
        }
    }
    if(parseFloat(testCount)>0)
    {
        TotalPrice.value=testCount;
    }
    else
    {
        TotalPrice.value='0';
    }
    if(parseFloat(testPrice)>0)
    {
        CountTotal.value=testPrice;
    }
    else
    {
        CountTotal.value='0';
    }
}
//保存时重新计算
function  CountTotalAll()
{
   if($("#HiddenMoreUnit").val()=="True")//启用时(重新计算)
     {
            var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
            var signFrame = findObj("dg_Log",document); 
            var testPrice=0;
            var testCount=0;
            for(var i=0;i<txtTRLastIndex-1;i++)
            {
                var RowID=i+1;
                if(signFrame.rows[i+1].style.display!="none")    
                {
                    var EXRate = $("#TD_UnitID_"+RowID).val().split('|')[1].toString(); /*比率*/
                    var ProductCount = $("#TD_AdjustCount_"+RowID).val();/*调整数量*/
                    if (ProductCount != '')
                    {
                        var tempcount=parseFloat(ProductCount*EXRate).toFixed($("#HiddenPoint").val());
                        //var tempprice=parseFloat(EXRate*UnitPrice).toFixed($("#HiddenPoint").val());
                        $("#BaseCount_SignItem_TD_Text_"+RowID).val(tempcount);/*基本数量根据报损数量和比率算出*/
                       // $("#TD_CostPrice_"+RowID).val(tempprice);/*单价根据基本单价和比率算出*/
                       // $("#TD_CostPriceTotal_"+RowID).val(parseFloat(ProductCount*tempprice).toFixed($("#HiddenPoint").val()));/*金额*/
                    }
                }
            }
    }
}

   /*
* 附件处理
*/

function DealAttachment(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传附件
    else if ("upload" == flag)
    {
        ShowUploadFile();
    }
    //清除附件
    else if ("clear" == flag)
    {
            var FilePath=document.getElementById("hfPageAttachment").value;
            var FileName=document.getElementById("attachname").innerHTML; 
            DeleteUploadFile(FilePath,FileName);
    }
    //下载附件
    else if ("download" == flag)
    {
            //获取附件路径
            attachUrl = document.getElementById("hfPageAttachment").value;
            window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + attachUrl, "_blank");
    }
}

/*
* 上传文件后回调处理
*/
function AfterUploadFile(url)
{
    if (url != "")
    {
        //设置附件路径
        document.getElementById("hfPageAttachment").value = url;
        var testurl=url.lastIndexOf('\\');
        testurl=url.substring(testurl+1,url.lenght);
        document.getElementById('attachname').innerHTML=testurl;
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "";
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
    }
}
//确认 
function Fun_ConfirmOperate()
{
        if(!window.confirm('确认要进行确认单操作吗?'))
        {
            return false;
        }
        
          
        
        
    var hiddeniqreportid = document.getElementById('hiddenAdjustID').value;
    var StorageID=document.getElementById('ddlInStorage').value; //    
    var InfoNo=document.getElementById('lbInfoNo').value ; // 编号
       
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    var DetailAdjustType=new Array();   //
    var DetailProID=new Array(); 
    var DetailAdjustCount=new Array();//数量
    var DetailSortNo=new Array();        //序号
    var DetailBatchNo=new Array();        //序号
    var DetailUnitPrice=new Array();//单价
   // var DetailAdjustType=new Array();//调整类型
    
    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        {   
            
              var theProID=document.getElementById('TD_ProID_'+(i)).value; //             
              var theAdjustType=document.getElementById('TD_AdjustType'+(i)).value;   //
              var theAdjustCount=document.getElementById('TD_AdjustCount_'+(i)).value;    
              var objBatchNo=document.getElementById('BatchNo_SignItem_TD_Text_'+(i)).value;//批次 
              var objCostPrice=document.getElementById('TD_CostPrice_'+(i)).value;//单价 
              var AdjustType=document.getElementById('TD_AdjustType'+(i)).value;//类型 
              
              var objBaseCount='BaseCount_SignItem_TD_Text_'+(i);//基本数量
                  
              DetailSortNo.push(i);
              DetailProID.push(theProID);
              DetailAdjustType.push(theAdjustType);              
              //DetailAdjustCount.push(theAdjustCount);      
              DetailBatchNo.push(objBatchNo);      
              DetailUnitPrice.push(objCostPrice);    
              
               if($("#HiddenMoreUnit").val()=="False")//未启用时(实际使用的存入基本单位中)
                {
                    DetailAdjustCount.push(theAdjustCount);//数量
                }     
                else
                {
                    DetailAdjustCount.push(document.getElementById(objBaseCount.toString()).value);//基本数量
                }   
        }        
    } 
    var UrlParam = "myAction=Confirm&StorageID="+StorageID+"&DetailSortNo="+DetailSortNo+"&DetailAdjustCount="+DetailAdjustCount+"&DetailProID="+DetailProID+"&DetailAdjustType="+DetailAdjustType+"&DetailBatchNo="+DetailBatchNo+"&DetailUnitPrice="+DetailUnitPrice+"&InfoNo="+InfoNo+"&ID="+hiddeniqreportid.toString()+"";
             
    $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageAdjustAdd.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  },
                 
                  error: function() 
                  {
                   
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    var reInfo=data.data.split('|');
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
                         document.getElementById('UnbtnSave').style.display='';
                        document.getElementById('btnSave').style.display='none'; 
                        if(parseInt(hiddeniqreportid)>0)
                        {
                            document.getElementById('txtModifiedUserID').value=reInfo[2];
                            document.getElementById('txtModifiedDate').value=reInfo[1].substring(0,11);
                            document.getElementById('txtConfirmor').value=reInfo[0];
                            document.getElementById('txtConfirmDate').value=reInfo[1].substring(0,11);
                        }
                        document.getElementById('ddlBillStatus').value="2";
                        document.getElementById('tbBillStatus').value='执行';
                        document.getElementById('txtConfirmor').style.display='';
                        document.getElementById('txtConfirmDate').style.display='';
                        GetFlowButton_DisplayControl();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
}

function Fun_UnConfirmOperate()
{

}
function Fun_CompleteOperate(isComplete)   //结单
{
    var myMethod='0';//0结单 1取消结单
    if(!isComplete)
    {
        myMethod='1';
        if(!window.confirm('确认要进行取消结单操作吗?'))
        {
            return false;
        }
    }
    else
    {
        if(!window.confirm('确认要进行结单操作吗?'))
        {
            return false;
        }
    }
    var hiddeniqreportid =document.getElementById('hiddenAdjustID').value
    var UrlParam = "myAction=Close&myMethod="+myMethod+"&ID="+hiddeniqreportid.toString()+"";
                       
    $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageAdjustAdd.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  },
                 
                  error: function() 
                  {
                   
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    var reInfo=data.data.split('|');
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        document.getElementById('UnbtnSave').style.display='';
                        document.getElementById('btnSave').style.display='none';
                        if(isComplete)
                        {
                            document.getElementById('ddlBillStatus').value="4";
                            document.getElementById('txtCloserReal').value=reInfo[0];
                            document.getElementById('txtCloseDate').value=reInfo[1].substring(0,11);
                            document.getElementById('txtCloserReal').style.display='';
                            document.getElementById('txtCloseDate').style.display='';
                        }
                        else 
                        {
                            document.getElementById('ddlBillStatus').value="2";
                            document.getElementById('tbBillStatus').value='结单';
                            document.getElementById('txtCloserReal').style.display='none';
                            document.getElementById('txtCloseDate').style.display='none';
                        }
                        if(parseInt(hiddeniqreportid)>0)
                        {
                            document.getElementById('txtModifiedUserID').value=reInfo[2];
                            document.getElementById('txtModifiedDate').value=reInfo[1].substring(0,11);
                        }
                        GetFlowButton_DisplayControl();
                      
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
}


function SetSaveButton_DisplayControl(flowStatus)
{

    //流程状态：0：待提交   1：待审批   2：审批中   3：审批通过     4：审批不通过   5：撤销审批
    //制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    //制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    //变更和手工结单的不可以修改
    try
    {
        var PageBillID = document.getElementById('hiddenAdjustID').value;
        var PageBillStatus = document.getElementById('ddlBillStatus').value;
       
        if(PageBillID>0)
        {

            if(PageBillStatus=='2' || PageBillStatus=='3' || PageBillStatus=='4')
            {
                //单据状态：变更和手工结单状态
                 document.getElementById('UnbtnSave').style.display='';
                 document.getElementById('btnGetGoods').style.display='none';
                 document.getElementById('unbtnGetGoods').style.display='';
                 document.getElementById('btnSave').style.display='none';
                          
            }
            else
            {
                if(PageBillStatus==1 && (flowStatus ==1 || flowStatus==2 || flowStatus ==3))
                {
                   
                    
                     document.getElementById('UnbtnSave').style.display='';
                     document.getElementById('btnGetGoods').style.display='none';
                     document.getElementById('unbtnGetGoods').style.display='';
                     document.getElementById('btnSave').style.display='none';
                            
                }
                else
                {
                     document.getElementById('UnbtnSave').style.display='none';
                     document.getElementById('btnGetGoods').style.display='';
                     document.getElementById('unbtnGetGoods').style.display='none';
                     document.getElementById('btnSave').style.display='';
                                      
                }
            }
        }
    }
    catch(e)
    {}
}
//function Fun_FlowApply_Operate_Succeed(myValues)// 0:提交审批成功  1:审批成功
//{
//      if (myValues != 2 && myValues != 3)
//      {
//   
//          document.getElementById('UnbtnSave').style.display='';
//          document.getElementById('btnSave').style.display='none';
//      }
//      else
//      {
//       
//          document.getElementById('UnbtnSave').style.display='none';
//          document.getElementById('btnSave').style.display='';
//      }
//}

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
    txtTRLastIndex.value = "1";
}

function DoChange()
{
    fnDelRow();
}

/*-----------------------------------------------------条码扫描Start-----------------------------------------------------------------*/

var rerowID="";
//判断是否有相同记录有返回true，没有返回false
function IsExist(prodNo)
{
    var signFrame = document.getElementById("dg_Log");  
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var prodNo1 = document.getElementById("TD_ProNo_"+i).value;
        
        if((signFrame.rows[i].style.display!="none")&&(prodNo1 == prodNo))
        {
            rerowID=i;
            return true;
        } 
    }
    return false;
}
//--------------------------------------------------------------------------------------条码扫描需要Start  

function GetGoodsDataByBarCode(ID,ProdNo,ProductName,
                                                  StandardSell,UnitID,CodeName,
                                                  TaxRate,SellTax,Discount,
                                                  Specification,CodeTypeName,TypeID,
                                                  StandardBuy,TaxBuy,InTaxRate,
                                                  StandardCost,IsBatchNo,BatchNo)
                                                  {
                                                     AddSignRow();    
                                                     var txtTRLastIndex = findObj("txtTRLastIndex",document);
                                                     var rowID = parseInt(txtTRLastIndex.value); 
                                                     popTechObj.InputObj=rowID-1;
                                                     Fun_FillParent_Content(ID,ProdNo,ProductName,'',UnitID
                                                     ,CodeName,'','','','','','',StandardCost
                                                     ,'','','','','','', '','','','',IsBatchNo,BatchNo)
//                                                    AddSignRowSearch(ID,ProductName,ProdNo,UnitID,CodeName,StandardCost,IsBatchNo);
                                                  }
   
function AddSignRowSearch(ProID,ProNo,ProName,UnitID,UnitName,StandardCost)
{        
            var signFrame = findObj("dg_Log",document); 
            var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
            if((typeof(signFrame) == "undefined")||signFrame ==null)
            {
                return false;
            }
            else
            {
                   for(var i=0;i<txtTRLastIndex-1;i++)
                     {
                        if(signFrame.rows[i+1].style.display!="none")    
                          {                                    
                                 var checkPro=document.getElementById('TD_ProID_'+(i+1));
                                 if(checkPro.value==ProID)   
                                 {
                                    return false;
                                 }
                          }
                     }
             }
        var txtTRLastIndex = findObj("txtTRLastIndex",document);
        var rowID = parseInt(txtTRLastIndex.value);
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "Row_" + rowID;
        var colChoose=newTR.insertCell(0);//添加列:选择
        colChoose.className="cell";
        colChoose.align="center";
        colChoose.innerHTML="<input name='Chk' onclick=\"IfSelectAll('Chk','CheckAll');\"  id='Chk_Option_"+rowID+"'  value=\"0\" type='checkbox' />";

       
        
        var colProductNo=newTR.insertCell(1);//添加列 物品编号
        colProductNo.className="cell";
        colProductNo.innerHTML = "<input type=\"text\" size=\"15\" value=\""+ProNo+"\" readOnly onfocus=\"popTechObj.ShowListSpecial('"+rowID+"','d');\" id='TD_ProNo_"+rowID+"' name='TD_ProNo_"+rowID+"' class=\"tdinput\" />";
        
        var colProductName=newTR.insertCell(2);//添加列:物品名称
        colProductName.className="cell";
        colProductName.innerHTML = "<input type=\"text\" value=\""+ProName+"\"  size=\"15\" readOnly  id=\"TD_Pro_"+rowID+"\" class=\"tdinput\"    /><input value=\""+ProID+"\" id=\"TD_ProID_"+rowID+"\" type=\"hidden\" >";
        
        var colUnitID=newTR.insertCell(3);//添加列:单位
        colUnitID.className="cell";
        colUnitID.innerHTML = "<input type=\"text\" size=\"5\" readOnly value=\""+UnitName+"\"  id='TD_Unit_"+rowID+"'  class=\"tdinput\"   /><input value=\""+UnitID+"\" id=\"TD_UnitID_"+rowID+"\" type=\"hidden\">";
        var colSpecification=newTR.insertCell(4);//添加列:调整类型
        colSpecification.className="cell";
        colSpecification.innerHTML = "<select id='TD_AdjustType"+rowID+"'><option value=\"1\">调增</option><option selected value=\"0\">调减</option></select>";
        
        var colProductCount=newTR.insertCell(5);//添加列:调整数量
        colProductCount.className="cell";
        colProductCount.innerHTML = "<input size=\"15\" specialworkcheck='调整数量' onchange=\"Number_round(this,2);\" onblur=\"CountAll('"+rowID+"');\"  id='TD_AdjustCount_"+rowID+"' type=\"text\" class=\"tdinput\" />";
        
        
        var colBom=newTR.insertCell(6);//添加列:成本单价
        colBom.className="cell";      
        colBom.innerHTML = "<input type=\"text\" readOnly size=\"5\" onchange=\"Number_round(this,2);\" value=\""+StandardCost+"\" id='TD_CostPrice_"+rowID+"' class=\"tdinput\"  />";
      
        
        var colRouteID=newTR.insertCell(7);//添加列:调整金额
        colRouteID.className="cell";
        colRouteID.innerHTML = "<input type=\"text\" specialworkcheck='调整金额' size=\"15\" onchange=\"Number_round(this,2);\"  class=\"tdinput\" id='TD_CostPriceTotal_"+rowID+"' />";
          
               
        var colRemark=newTR.insertCell(8);//添加列:备注
        colRemark.className="cell";
        colRemark.innerHTML = "<input type=\"text\"  size=\"15\" specialworkcheck='备注'  id='TD_Remark_"+rowID+"' value=\"\"  class=\"tdinput\"  />";
        
        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
        
}
/*库存快照*/
function ShowSnapshot()
{
       var intProductID = 0;
       var detailRows = 0;
       var snapProductName = '';
       var snapProductNo = '';
       var signFrame = findObj("dg_Log",document); 
      for(var i=1;i<signFrame.rows.length;i++)
       {
            if(signFrame.rows[i].style.display!="none")
            {
                var rowid = signFrame.rows[i].id;
                if(document.getElementById('Chk_Option_'+i).checked)
                {
                    detailRows ++;
                    intProductID = $("#TD_ProID_" + i).val();
                    snapProductName= $("#TD_Pro_"+i).val();
                    snapProductNo = $("#TD_ProNo_"+i).val();
                }
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
           
       }
       else
       {
          popMsgObj.ShowMsg('请选择单个物品查看库存快照');
          return false;
       }   
}