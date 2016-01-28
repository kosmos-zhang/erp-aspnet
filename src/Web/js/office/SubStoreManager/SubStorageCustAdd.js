$(document).ready(function()
{
    requestQuaobj = GetRequest();   
     var action=requestQuaobj['myAction'];
     var from=requestQuaobj['from'];
    if(action=='edit' || from=='list')
    {
        document.getElementById('btnReturn').style.display='';
        
    }
    if(CustID>0)
    { 
        LoadCustInfo(CustID);
    }
    else
    {
        
    }   

});
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
function LoadCustInfo(CustID)
{      
  
       var rowsCount=0;
      // ClearSignRow();
       $.ajax({
       type: "POST",//用POST方式传输ss
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/SubStoreManager/SubStorageCustInfo.ashx?ID="+CustID,//目标地址
       cache:false,          
       success: function(msg){
                var rowsCount = 0;
                var countTotals = 0;
                //数据获取完毕，填充页面据显示

                    if(typeof(msg.dataReport)!='undefined')
                    {
                       
                        $.each(msg.dataReport,function(i,item)           
                        {
                            document.getElementById('hiddenCustID').value=item.ID; 
                            document.getElementById('CustName').value=item.CustName;  //
                            document.getElementById('CustTel').value=item.CustTel;                            
                            document.getElementById('CustPhone').value=item.CustMobile;  
                            document.getElementById('CustAddr').value=item.CustAddr;                            
                           

                        });
                    }          


 

              },
       error: function() {alert('加载数据时发生请求异常');}, 
       complete:function(){}
       });
}

//-------------------------保存Start
function Fun_Save_Cust()
{
  if(CheckInput())
  {
    var UrlParam='';
    var myAction='add';
    var myMethod=document.getElementById('hiddenCustID').value;   //该单据ID
    if(parseFloat(myMethod)>0)   //编辑
    {       
        myAction='edit';        
    }
    var CustName=document.getElementById('CustName').value; //客户名称
    var CustTel=document.getElementById('CustTel').value;   //客户电话
    var CustPhone=document.getElementById('CustPhone').value; //客户手机
    var CustAddr=document.getElementById('CustAddr').value; //  客户地址
                                     
UrlParam="CustName="+escape(CustName)+                                          
        "&CustTel="+escape(CustTel) + 
        "&CustPhone="+escape(CustPhone)+   
        "&CustAddr="+escape(CustAddr)+                              
        "&myAction="+escape(myAction)+
        "&ID="+myMethod;                                            

        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/SubStoreManager/SubStorageCustAdd.ashx",
                  dataType:'json',//返回json格式数据
                  cache:false,
                  data:UrlParam,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('保存分店客户时请求发生错误');
                  }, 
                  success:function(data) 
                  { 
                     var reInfo=data.data.split('|');

                     if(reInfo.length > 1)
                       {   
                            popMsgObj.ShowMsg(data.info);
                            document.getElementById('hiddenCustID').value=reInfo[0];
             
                      }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                                     } 
              }); 
    }
}
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var CustName=document.getElementById('CustName').value; //客户名称
    var CustTel=document.getElementById('CustTel').value;   //客户电话
    var CustPhone=document.getElementById('CustPhone').value; //客户手机
    var CustAddr=document.getElementById('CustAddr').value; //  客户地址
    
        if(strlen(CustName)<=0)
        {
            isFlag = false;
            fieldText = fieldText + "客户名称|";
   		    msgText = msgText +  "请输入单据客户名称 |";
        }
        if(strlen(CustName)>0)
        {
             if(trim(CustName)=='')
            {
                isFlag = false;
                fieldText = fieldText + "客户名称|";
   		        msgText = msgText +  "不能输入空客户名称 |";
            }
        }
        if(!CheckSpecialWord(CustName))
        {
            isFlag = false;
            fieldText = fieldText + "客户名称|";
   		    msgText = msgText +  "客户名称不能含有特殊字符 |";
        }
        if(!CheckSpecialWord(CustTel))
        {
            isFlag = false;
            fieldText = fieldText + "客户电话|";
   		    msgText = msgText +  "客户电话不能含有特殊字符 |";
        }
        if(!CheckSpecialWord(CustPhone))
        {
            isFlag = false;
            fieldText = fieldText + "客户手机号|";
   		    msgText = msgText +  "客户手机号不能含有特殊字符 |";
        }
        if(!CheckSpecialWord(CustAddr))
        {
            isFlag = false;
            fieldText = fieldText + "送货地址|";
   		    msgText = msgText +  "送货地址不能含有特殊字符 |";
        }
  
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
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
        
        var colUnitID=newTR.insertCell(3);//添加列:单位
        colUnitID.className="cell";
        colUnitID.innerHTML = "<input type=\"text\" size=\"5\" readOnly  id='TD_Unit_"+rowID+"'  class=\"tdinput\"   /><input id=\"TD_UnitID_"+rowID+"\" type=\"hidden\">";
        
        var colSpecification=newTR.insertCell(4);//添加列:调整类型
        colSpecification.className="cell";
        colSpecification.innerHTML = "<select id='TD_AdjustType"+rowID+"'><option value=\"1\">调增</option><option selected value=\"0\">调减</option></select>";
        
        var colProductCount=newTR.insertCell(5);//添加列:调整数量
        colProductCount.className="cell";
        colProductCount.innerHTML = "<input size=\"15\" specialworkcheck='调整数量' onchange=\"Number_round(this,2);\" onblur=\"CountAll('"+rowID+"');\"  id='TD_AdjustCount_"+rowID+"' type=\"text\" class=\"tdinput\" />";
        
        
        var colBom=newTR.insertCell(6);//添加列:成本单价
        colBom.className="cell";      
        colBom.innerHTML = "<input type=\"text\" readOnly size=\"5\" onchange=\"Number_round(this,2);\"  id='TD_CostPrice_"+rowID+"' class=\"tdinput\"  />";
      
        
        var colRouteID=newTR.insertCell(7);//添加列:调整金额
        colRouteID.className="cell";
        colRouteID.innerHTML = "<input type=\"text\" specialworkcheck='调整金额' size=\"15\" onchange=\"Number_round(this,2);\"  class=\"tdinput\" id='TD_CostPriceTotal_"+rowID+"' />";
          
               
        var colRemark=newTR.insertCell(8);//添加列:备注
        colRemark.className="cell";
        colRemark.innerHTML = "<input type=\"text\"  size=\"15\" specialworkcheck='备注'  id='TD_Remark_"+rowID+"' value=\"\"  class=\"tdinput\"  />";
        
        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
        
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
        CostPriceTotal.value=parseFloat(CostPrice.value*TD_AdjustCount.value).toFixed(2);
    }
    else
    {
        CostPriceTotal.value='0';
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
          
          testPrice= parseFloat(parseFloat(testPrice)+parseFloat(myCostPriceTotal)).toFixed(2);
          testCount=parseFloat(parseFloat(testCount)+parseFloat(myAdjustCount)).toFixed(2);
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
 function Fun_FillParent_Content(id,prodNo,prodName,standardSell,unitID,codeName,taxRate,sellTax,discount,specification,codeTypeName,typeID,StandardCost)
 {
            if(popTechObj.InputObj!=null)
            {
                   document.getElementById('TD_ProNo_'+popTechObj.InputObj).value=prodNo;
                   document.getElementById('TD_Pro_'+popTechObj.InputObj).value=prodName;
                   document.getElementById('TD_ProID_'+popTechObj.InputObj).value=id;
                   document.getElementById('TD_Unit_'+popTechObj.InputObj).value=codeName;
                   document.getElementById('TD_UnitID_'+popTechObj.InputObj).value=unitID;
                   document.getElementById('TD_CostPrice_'+popTechObj.InputObj).value=StandardCost;
                   document.getElementById('divStorageProduct').style.display='none';
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
            var FileName=document.getElementById("spanAttachmentName").innerHTML; 
            DeleteUploadFile(FilePath,FileName);
    }
    //下载附件
    else if ("download" == flag)
    {
            //获取附件路径
            attachUrl = document.getElementById("tbAttachment").value;
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
        document.getElementById("tbAttachment").value = url;
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
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    var DetailAdjustType=new Array();   //
    var DetailProID=new Array(); //数量
    var DetailAdjustCount=new Array();
    var DetailSortNo=new Array();        //序号
    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        {   
            
              var theProID=document.getElementById('TD_ProID_'+(i)).value; //             
              var theAdjustType=document.getElementById('TD_AdjustType'+(i)).value;   //
              var theAdjustCount=document.getElementById('TD_AdjustCount_'+(i)).value;         
              DetailSortNo.push(i);
              DetailProID.push(theProID);
              DetailAdjustType.push(theAdjustType);              
              DetailAdjustCount.push(theAdjustCount);      
        }        
    } 
    var UrlParam = "myAction=Confirm&StorageID="+StorageID+"&DetailSortNo="+DetailSortNo+"&DetailAdjustCount="+DetailAdjustCount+"&DetailProID="+DetailProID+"&DetailAdjustType="+DetailAdjustType+"&ID="+hiddeniqreportid.toString()+"";
             
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
//    if(!window.confirm('确认要进行取消确认单操作吗?'))
//     {
//         return false;
//    }
//    var hiddeniqreportid = document.getElementById('hiddenAdjustID').value;
//     var StorageID=document.getElementById('ddlInStorage').value; //    
//    var signFrame = findObj("dg_Log",document); 
//    var count = signFrame.rows.length;//有多少行
//    var DetailAdjustType=new Array();   //
//    var DetailProID=new Array(); //数量
//    var DetailAdjustCount=new Array();
//    var DetailSortNo=new Array();        //序号
//    for(var i=1;i<count;i++)
//    {
//        if(signFrame.rows[i].style.display!='none')
//        {   
//            
//              var theProID=document.getElementById('TD_ProID_'+(i)).value; //             
//              var theAdjustType=document.getElementById('TD_AdjustType'+(i)).value;   //
//              var theAdjustCount=document.getElementById('TD_AdjustCount_'+(i)).value;         
//              DetailSortNo.push(i);
//              DetailProID.push(theProID);
//              DetailAdjustType.push(theAdjustType);              
//              DetailAdjustCount.push(theAdjustCount);      
//        }        
//    } 
//    var UrlParam = "myAction=UnConfirm&StorageID="+StorageID+"&DetailSortNo="+DetailSortNo+"&DetailAdjustCount="+DetailAdjustCount+"&DetailProID="+DetailProID+"&DetailAdjustType="+DetailAdjustType+"&ID="+hiddeniqreportid.toString()+"";
//             
//    $.ajax({ 
//                  type: "POST",
//                  url: "../../../Handler/Office/StorageManager/StorageAdjustAdd.ashx?"+UrlParam,
//                  dataType:'json',//返回json格式数据
//                  cache:false,
//                  beforeSend:function()
//                  { 
//                     //AddPop();
//                  },
//                 
//                  error: function() 
//                  {
//                   
//                    popMsgObj.ShowMsg('请求发生错误');
//                    
//                  }, 
//                  success:function(data) 
//                  { 
//                    var reInfo=data.data.split('|');
//                    if(data.sta==1) 
//                    {
//                        popMsgObj.ShowMsg(data.info);
//                        if(parseInt(hiddeniqreportid)>0)
//                        {
//                            document.getElementById('txtModifiedUserID').value=reInfo[0];
//                            document.getElementById('txtModifiedDate').value=reInfo[1].substring(0,11);
//                        }
//                        document.getElementById('ddlBillStatus').value="1";
//                        document.getElementById('tbBillStatus').value='制单';
//                        document.getElementById('UnbtnSave').style.display='';
//                        document.getElementById('btnSave').style.display='none';
//                        document.getElementById('txtConfirmor').style.display='';
//                        document.getElementById('txtConfirmDate').style.display='';
//                        GetFlowButton_DisplayControl();
//                    }
//                    else
//                    {
//                        popMsgObj.ShowMsg(data.info);
//                    }
//                  } 
//               });
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
        var PageBillID = document.getElementById('hiddenAdjustID').value;
        var PageBillStatus = document.getElementById('ddlBillStatus').value;
        if(PageBillID>0)
        {

            if(PageBillStatus=='2' || PageBillStatus=='3' || PageBillStatus=='4')
            {
                //单据状态：变更和手工结单状态
                document.getElementById('UnbtnSave').style.display='';
                document.getElementById('btnSave').style.display='none';
            }
            else
            {
                if(PageBillStatus==1 && (flowStatus ==1 || flowStatus==2 || flowStatus ==3))
                {
                    //单据状态+审批状态：制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
                    document.getElementById('UnbtnSave').style.display='';
                    document.getElementById('btnSave').style.display='none';
                }
                else
                {
                   document.getElementById('UnbtnSave').style.display='none';
                    document.getElementById('btnSave').style.display='';
                    
                    
                }
            }
        }
    

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
    txtTRLastIndex.value = "0";
}

function DoChange()
{
    fnDelRow();
}

// 打印
function PrintCust()    
{
    var CustID=document.getElementById('hiddenCustID').value;
    if(CustID=="" || parseInt(CustID) < 1)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("../../../Pages/Office/SubStoreManager/PrintSubStorageCust.aspx?ID="+CustID);
    
}