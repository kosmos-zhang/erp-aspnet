$(document).ready(function()
{
      requestobj = GetRequest();
      requestparam1=requestobj['retval'];
      
//     requestparam1='CGSQD00000123';
     Fill(requestparam1);
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

var DtlS_Item = new Array();
var Dtl_Item = new Array();
var Dtl_Item2 = new Array();
var DtlCount = 0;
var actionApply = "Update";
function Fill(ApplyNo)
{
      $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/PurchaseManager/PurchaseApplyEdit.ashx",//目标地址
       data:'ApplyNo='+escape(ApplyNo),
       cache:false,
       beforeSend:function()
       {
       },          
       success: function(msg){
                $.each(msg.datap,function(i,item){
                    if(item.ApplyNo!= null && item.ApplyNo != "")
                    {
                        $("#CodingRuleControl1_txtCode").attr("value",item.ApplyNo);//
                        $("#txtTitle").attr("value",item.Title);//
                        $("#ddlTypeID_ddlCodeType").attr("value",item.TypeID);//
                        $("#txtApplyUserID").attr("value",item.ApplyUserName);//
                    $("#txtApplyUserID").attr("title",item.ApplyUserID);
                        $("#txtApplyDeptID").attr("value",item.ApplyDeptName);//
                    $("#txtApplyDeptID").attr("title",item.ApplyDeptID);
                        $("#ddlFromType").attr("value",item.FromType);//
                        $("#txtApplyDate").attr("value",item.ApplyDate);//
                        $("#txtAddress").attr("value",item.Address);//
                        $("#txtCreator").attr("value",item.CreatorName);//
                    
                        $("#txtCreateDate").attr("value",item.CreateDate);//
                        $("#ddlBillStatus").attr("value",item.BillStatus);//
                        $("#txtConfirmor").attr("value",item.ConfirmorName);//
                        $("#txtConfirmDate").attr("value",item.ConfirmDate);//
                        $("#txtCloser").attr("value",item.CloserName);//
                        $("#txtCloseDate").attr("value",item.CloseDate);//
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//
                        $("#txtRemark").attr("value",item.Remark);//
                       
                    }                     

                  
               });
                $.each(msg.data,function(i,item){
                    if(item.ProductNo != null && item.ProductNo != "")
                    {
                        var temp = new DtlSInfo(item.ProductID,item.ProductNo,item.ProductName,item.UnitID,item.UnitName,item.UnitPrice,
                                                item.ProductCount,item.PlanCount,item.TotalPrice,item.RequireDate,item.PlanTakeDate,
                                                item.FromDeptID,item.FromDeptName,item.FromBillID,item.FromBillNo,item.FromLineNo,
                                                item.ProviderID,item.ProviderName,item.Remark,"");
                        
                        
                        AddSignRow1(temp);   
                    }                     

                  
               });
               $.each(msg.data2,function(i,item){
                    if(item.ProductNo != null && item.ProductNo != "")
                    {
                        var temp = new DtlInfo(item.ProductID,item.ProductNo,item.ProductName,item.UnitID,item.UnitName,item.PlanCount,
                                                item.RequireDate,item.ApplyReasonID,item.ApplyReasonName,item.Remark);
                        AddSignRow3(temp);
                    }
               });
              },
       error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}

var dtls001 = new Array();
var dtl001 = new Array();
function InsertPurchaseApplyData() 
{
if(CheckBaseInfo())
return;
MrgDtlS();
var signFrame = document.getElementById("dg_Log");
if((typeof(signFrame)=="undefined")||(signFrame==null))
return;
    var applyNo = requestparam1;
     var title=$("#txtTitle").val(); 
     var applyUserID=$("#txtApplyUserID").attr("title");
     var applyDeptID=$("#txtApplyDeptID").attr("title");
     var applyDate=$("#txtApplyDate").val(); 
     var typeID=$("#ddlTypeID_ddlCodeType").val(); 
     var address=$("#txtAddress").val(); 
     var fromType=$("#ddlFromType").val(); 
     //备注信息
     var creator=$("#txtCreator").val(); 
     var createDate=$("#txtCreateDate").val(); 
     var billStatus=$("#ddlBillStatus").val();
     var remark=$("#txtRemark").val(); 
     var confirmor=$("#txtConfirmor").val(); 
     var confirmDate=$("#txtConfirmDate").val(); 
     var modifiedDate=$("#txtModifiedDate").val(); 
     var modifiedUserID=$("#txtModifiedUserID").val();
     

    //明细来源信息
    getDtlSValue();
     var dtlSInfo = "";
     for(var i=0;i<DtlS_Item.length;++i)
     {
        var dtlSV001 = new Array();
        dtlSV001.push(i+1);
        dtlSV001.push(DtlS_Item[i].ProductID);
        dtlSV001.push(DtlS_Item[i].ProductNo);
        dtlSV001.push(DtlS_Item[i].ProductName);
        dtlSV001.push(DtlS_Item[i].UnitID);
        dtlSV001.push(DtlS_Item[i].UnitPrice);
        dtlSV001.push(DtlS_Item[i].ProductCount);
        dtlSV001.push(DtlS_Item[i].PlanCount);
        dtlSV001.push(DtlS_Item[i].TotalPrice);
        dtlSV001.push(DtlS_Item[i].RequireDate);
        dtlSV001.push(DtlS_Item[i].PlanTakeDate);
        dtlSV001.push(DtlS_Item[i].FromDeptID);
        dtlSV001.push(DtlS_Item[i].FromBillID);
        dtlSV001.push(DtlS_Item[i].FromLineNo);
        dtlSV001.push(DtlS_Item[i].ProviderID);
        dtlSV001.push(DtlS_Item[i].Remark);
        dtlSV001.push(fromType);
        dtls001.push(dtlSV001);
     } 

     dtlSInfo =  dtls001.join("|");
    
     //明细信息
//     GetDtlValue();

//alert(Dtl_Item)
     var dtlInfo = "";  
     for(var j=0;j<Dtl_Item.length;++j)
     {
        var dtlV001 = new Array();
        dtlV001.push(j+1);
        dtlV001.push(Dtl_Item[j].ProductID);
        dtlV001.push(Dtl_Item[j].ProductNo);
        dtlV001.push(Dtl_Item[j].ProductName);
        dtlV001.push(Dtl_Item[j].PlanCount);
        dtlV001.push(Dtl_Item[j].UnitID);
        dtlV001.push(Dtl_Item[j].RequireDate);
        dtlV001.push(Dtl_Item[j].ApplyReason);
        dtlV001.push(Dtl_Item[j].Remark);
        dtl001.push(dtlV001);
     }
     
     dtlInfo = dtl001.join("|");
     var str = "dtlSAdd";          
     $.ajax({ 
          type: "POST",
          url: "../../../Handler/Office/PurchaseManager/PurchaseApplyEdit2.ashx",
          data:'ApplyNo='+escape(applyNo)
              +'&strtitle='+escape(title)
              +'&strapplyUserID='+escape(applyUserID)
              +'&strapplyDeptID='+escape(applyDeptID)
              +'&strapplyDate='+escape(applyDate)
              +'&strtypeID='+escape(typeID)
              +'&straddress='+escape(address)
              +'&strfromType='+escape(fromType)
              +'&strcreator='+escape(creator)
              +'&strcreateDate='+escape(createDate)
              +'&strbillStatus='+escape(billStatus)
              +'&strremark='+escape(remark)
//              +'&strconfirmor='+escape(confirmor)
//              +'&strconfirmDate='+escape(confirmDate)
              +'&strmodifiedDate='+escape(modifiedDate)
              +'&strmodifiedUserID='+escape(modifiedUserID)
              +'&dtlSInfo='+escape(dtlSInfo)
              +'&dtlInfo='+escape(dtlInfo) 
              +'&actionApply='+escape(actionApply),
          dataType:'json',//返回json格式数据
          cache:false,
          beforeSend:function()
          { 
//              AddPop();
          }, 
        //complete :function(){hidePopup();},
        error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
        success:function(data) 
        {        
            if(data.sta==1) 
            {
                document.getElementById("Add_PurchaseApply").style.display = "block";  
                $("#CodingRuleControl1_txtCode").val(data.data);
                $("#CodingRuleControl1_txtCode").attr("readonly","readonly");
                $("#CodingRuleControl1_ddlCodeRule").attr("disabled","false");
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");

            } 
            else 
            { 
              hidePopup();
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        } 
       });  
     
} 

//确认
function Confirm()
{
if(CheckBaseInfo()||actionApply=="Add")
return;
    actionApply = "Confirm";
    document.getElementById("txtConfirmor").value=document.getElementById("usernametemp").value.Trim();
    document.getElementById("txtConfirmDate").value=document.getElementById("datetemp").value.Trim();
    document.getElementById("ddlBillStatus").value = 2;
}

//新建
function Fun_Clear_Input()
{
//    actionApply="Add";
    window.location='PurchaseApplyAdd.aspx'; 
}

//获取明细来源信息
function getDtlSValue()
{
DtlS_Item.length = 0;
 var signFrame = document.getElementById("dg_Log");
 
 if((typeof(signFrame)!="undefined")&&(signFrame!=null))
 {
    for(i=1;i<signFrame.rows.length;i++)
    {         
        if(signFrame.rows[i].style.display!="none")
        {
            signFrame.rows[i].cells[0].innerHTML = i.toString();
            rowid =parseInt(signFrame.rows[i].cells[0].innerText);
            var Chk       = document.getElementById("chk" + rowid).value.Trim()                                
            var ProductID = document.getElementById("ProdID_SignItem_TD_Sequ_Text_" + rowid).value.Trim() ;     
            var ProductNo = document.getElementById("ProdNo_SignItem_TD_Sequ_Text_" + rowid).value.Trim() ;
            var ProductName  = document.getElementById("ProductName_SignItem_TD_Sequ_Text_" + rowid).value.Trim();
            var UnitID     = document.getElementById("UnitID_SignItem_TD_Sequ_Text_" + rowid).value.Trim() ;
            var UnitName  =document.getElementById("Unit_SignItem_TD_Sequ_Text_" + rowid).value.Trim();
            var UnitPrice  =document.getElementById("UnitPrice_SignItem_TD_Sequ_Text_" + rowid).value.Trim() ;
            
            var ProductCount =document.getElementById("txtProductCount"+rowid).value.Trim() ;
            var PlanCount  =document.getElementById("txtPlanCount" + rowid).value.Trim()   ;
            var TotalPrice =document.getElementById("txtTotalPrice" + rowid).value.Trim()  ;
            var RequireDate  =document.getElementById("txtRequireDate" + rowid).value.Trim() ;
            var PlanTakeDate =document.getElementById("txtPlanTakeDate" + rowid).value.Trim();
            var FromDeptID =document.getElementById("txtFromDeptID" + rowid).value.Trim() ;
            var FromDeptName =document.getElementById("txtFromDeptName" + rowid).value.Trim() ;
            var FromBillID  =document.getElementById("txtFromBillID" + rowid).value.Trim() ; 
            var FromBillNo  = document.getElementById("txtFromBillNo" + rowid).value.Trim() ;
            var FromLineNo =document.getElementById("txtFromLineNo" + rowid).value.Trim()  ;                    
            var ProviderID =document.getElementById("txtProviderID" + rowid).value.Trim() ;
            var ProviderName   =document.getElementById("txtProviderName" + rowid).value.Trim() ;                     
            var Remark  =document.getElementById("txtRemark" + rowid).value.Trim() ;                         
            var FromType =document.getElementById("ddlFromType").value.Trim();                                
                    
                           
            var dtlS = new DtlSInfo(ProductID, ProductNo, ProductName, UnitID, UnitName,UnitPrice,ProductCount,PlanCount,TotalPrice,RequireDate,PlanTakeDate 
                                                    ,FromDeptID,FromDeptName,FromBillID,FromBillNo,FromLineNo,ProviderID,ProviderName,Remark,FromType)            

            DtlS_Item.push(dtlS);
        }
    }
  }
  else
  {
    return;
  }
}

function DtlSInfo(ProductID, ProductNo, ProductName, UnitID, UnitName,UnitPrice
                ,ProductCount,PlanCount,TotalPrice,RequireDate,PlanTakeDate 
                ,FromDeptID,FromDeptName,FromBillID,FromBillNo,FromLineNo,ProviderID,ProviderName,Remark,FromType)
{
    this.ProductID      =ProductID    ;
    this.ProductNo      =ProductNo    ;
    this.ProductName    =ProductName  ;
    this.UnitID         =UnitID       ;
    this.UnitName       =UnitName     ;
    this.UnitPrice      =UnitPrice    ;
    this.ProductCount   =ProductCount ;
    this.PlanCount      =PlanCount    ;
    this.TotalPrice     =TotalPrice   ;
    this.RequireDate    =RequireDate  ;
    this.PlanTakeDate   =PlanTakeDate ;
    this.FromDeptID     =FromDeptID   ;
    this.FromDeptName   =FromDeptName ;
    this.FromBillID     =FromBillID   ;
    this.FromBillNo     =FromBillNo   ;
    this.FromLineNo     =FromLineNo   ;
    this.ProviderID     =ProviderID   ;
    this.ProviderName   =ProviderName ;
    this.Remark         =Remark       ;
    this.FromType       =FromType     ;

}
function MrgDtlS()
{
getDtlSValue();
var signFrame = document.getElementById("dg_Log");
if((typeof(signFrame)=="undefined")||(signFrame==null))
return;
    //第一条直接赋值
    var tempdtls = DtlS_Item[0];
    var tempdtl = new DtlInfo(tempdtls.ProductID,tempdtls.ProductNo,tempdtls.ProductName,tempdtls.UnitID,tempdtls.UnitName,tempdtls.PlanCount,
                                tempdtls.RequireDate,"","","");
        Dtl_Item.push(tempdtl);
      var Count = 1;  
    for(var i=1;i<DtlS_Item.length;++i)
    {
        
        var tempdtls2 = DtlS_Item[i];
        var j = 0;
        for(j=0;j<Count;++j)
        {
            var tempdtl2 = Dtl_Item[j];
            if((tempdtl2.ProductID==tempdtls2.ProductID)&&(tempdtl2.RequireDate==tempdtls2.RequireDate))
            {
                Dtl_Item[j].PlanCount =parseInt(tempdtls2.PlanCount)+parseInt(Dtl_Item[j].PlanCount);
                break;
            }
        }
        if(j==Count)
        {
            var temp = new DtlInfo(tempdtls2.ProductID,tempdtls2.ProductNo,tempdtls2.ProductName,tempdtls2.UnitID
                                   ,tempdtls2.UnitName,tempdtls2.PlanCount,tempdtls2.RequireDate,"","","")
            Dtl_Item.push(temp);
            ++Count;
        }
    }
}
function DtlInfo(ProductID,ProductNo,ProductName,UnitID,UnitName,PlanCount,RequireDate,ApplyReasonID,ApplyReasonName,Remark)
{
    this.ProductID      =ProductID    ;
    this.ProductNo      =ProductNo    ;
    this.ProductName    =ProductName  ;
    this.UnitID         =UnitID       ;
    this.UnitName       =UnitName     ;
    this.PlanCount      =PlanCount     ;
    this.RequireDate    =RequireDate ;
    this.ApplyReasonID =ApplyReasonID;
    this.ApplyReasonName  =ApplyReasonName ;
    this.Remark       =Remark     ;
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

//判断明细来源中的数据是否符合要求
function IsRightDtlS()
{
    var signFrame = document.getElementById("dg_Log");
    if((typeof(signFrame) == "undefined")||(signFrame==null))
    return false;
    var NonHidd = 0;//是否全是隐藏的
     for(var i=1;i<signFrame.rows.length;++i)
     {
        if(signFrame.rows[i].style.display!="none")
        {
            ++NonHidd;
            var ProductNo = 'ProdNo_SignItem_TD_Sequ_Text_'+i;
            var ProductCount = 'txtProductCount'+i;
            
            var prodno =document.getElementById(ProductNo).value.Trim()
            var prodcnt =document.getElementById(ProductCount).value.Trim()
            if((prodno == '')||(prodno == 'undefined')||(prodcnt == '')||(prodcnt == 'undefined'))
            return false;
        }
     } 
     
     if(NonHidd == 0)
     {
        return false;
     }
     return true;
}





//将汇总信息显示在界面上
function DspDtl()
{
DeleteSignRow200();
if(!IsRightDtlS())
{
    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请输入正确的明细来源！")
    return;
}
    MrgDtlS();
    for(var i=0;i<Dtl_Item.length;++i)
    {   
        var temp =Dtl_Item[i]; 
        AddSignRow3(temp);
        
//        document.getElementById("txtProductID2"+rowid).value =temp.ProductID;
//        document.getElementById("txtProductNo2" + rowid).value = temp.ProductNo;
//        document.getElementById("txtProductName2" + rowid).value = temp.ProductName;
//        document.getElementById("txtUnitID2" + rowid).value = temp.UnitID;
//        document.getElementById("txtPlanCount2" + rowid).value = temp.PlanCount;
//        document.getElementById("txtRequireDate2" + rowid).value = temp.RequireDate;
//        
//        document.getElementById("txtUnitName2"+rowid).value = temp.UnitName;
    }
}

function EmptySignFrame()
{
var signFrame = document.getElementById("dg_Log");
    if((typeof(signFrame) == "undefined")||(signFrame==null))
    return true;
    for(var i=1;i<signFrame.rows.length;++i)
    {
        if(signFrame.rows[i].style.display!="none")
        return false;
    }
    return true;
}

function GetDtlValue()
{
var signFrame = document.getElementById("dg_Log2");
Dtl_Item2.length = 0;
     for(var i=1;i<signFrame.rows.length;++i)
     {
        if(signFrame.rows[i].style.display!="none")
        {
            var SortNo = i;
            var ProductID = "txtProductID2"+i;
            var ProductNo = "txtProductNo2"+i;
            var ProductName = "txtProductName2"+i;
            var PlanCount = "txtPlanCount2"+i;
            var UnitID = "txtUnitID2"+i;
            var RequireDate = "txtRequireDate2"+i;
            var ApplyReasonID ="txtApplyReasonID2"+i;
            var Remark ="txtRemark2"+i;
            
            var temp = new Array();
            temp.push(SortNo);
            temp.push(document.getElementById(ProductID).value.Trim());
            temp.push(document.getElementById(ProductNo).value.Trim());
            temp.push(document.getElementById(ProductName).value.Trim());
            temp.push(document.getElementById(PlanCount).value.Trim());
            temp.push(document.getElementById(UnitID).value.Trim());
            temp.push(document.getElementById(RequireDate).value.Trim());
            temp.push(document.getElementById(ApplyReasonID).value.Trim());
            temp.push(document.getElementById(Remark).value.Trim());
            
            Dtl_Item2.push(temp);
        }
     }
}



function DeleteAll()
{
    DeleteSignRow100();
    DeleteSignRow200();
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
    DtlS_Item.length=0;
}


function DeleteSignRow200()
{
//    var signFrame = findObj("dg_Log2",document);
    var signFrame = document.getElementById("dg_Log2");  
    if((typeof(signFrame) != "undefined")&&(signFrame!=null))
    {
        for( var i = signFrame.rows.length-1 ; i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    findObj("DtlCount",document).value = 1;
    Dtl_Item.length = 0;
    dtlSInfo = "";
}

function DelRow()
{
        var signFrame = findObj("dg_Log",document);        
        var ck = document.getElementsByName("chk");
        for( var i = 0; i<ck.length;i++ )
        {
            if ( ck[i].checked )
            {
              
               signFrame.rows[i+1].style.display="none";
            }
        }
         DspDtl();
}

 
 /*
* 基本信息校验
*/
function CheckBaseInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;

//    //新建时，编号选择手工输入时
//    if ("Add" == actionApply)
//    {
//        //获取编码规则下拉列表选中项
//        codeRule = document.getElementById("CodingRuleControl1_ddlCodeRule").value;
//        //如果选中的是 手工输入时，校验编号是否输入
//        if (codeRule == "")
//        {
//            //获取输入的编号
//            employeeNo = document.getElementById("CodingRuleControl1_txtCode").value;
//            //编号必须输入
//            if (employeeNo == "")
//            {
//                isErrorFlag = true;
//                fieldText += "编号|";
//   		        msgText += "请输入编号|";
//            }
//        }
//    }
    //主题不空
//    if (document.getElementById("txtTitle").value.Trim() == "")
//    {
//        isErrorFlag = true;
//        fieldText += "主题|";
//        msgText += "请输入主题|";
//    }


    
    //申请日期是否正确
    if(document.getElementById("txtApplyDate").value.Trim() == "")
    {
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
    
    //明细来源的校验
    var signFrame = document.getElementById("dg_Log");
    if((typeof(signFrame) == "undefined")||signFrame==null)
    {
        isErrorFlag = true;
        msgText +="明细来源|";
        msgText +="明细来源不能为空|";
    }
    else
    {
        for(var i=1;i<signFrame.rows.length;++i)
        {
            var ProductCount = "txtProductCount"+i;
            var no = document.getElementById(ProductCount).value.Trim();
            if(IsNumeric(no,12,4) == false)
            {
                isErrorFlag = true;
                msgText +="明细来源|";
                msgText +="请输入正确的需求数量|";
            }
//            var PlanCount = "txtPlanCount"+i;
//            if()
            var RequireDate = "txtRequireDate"+i;
            var reqdate = document.getElementById(RequireDate).value.Trim();
            if(isDate(reqdate) == false)
            {
                isErrorFlag = true;
                msgText +="明细来源|";
                msgText +="请输入正确的需求日期|";
            }
            var PlanTakeDate = "txtPlanTakeDate"+i;
            var plandate = document.getElementById(PlanTakeDate).value.Trim();
            if(isDate(plandate) == false)
            {
                isErrorFlag = true;
                msgText +="明细来源|";
                msgText +="请输入正确的申请交货日期|";
            }
        }
        
    }
    if(msgText!="")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);
    }
    return isErrorFlag;
}

 
    
//新增明细来源行
function AddSignRow1(temp)
{
            var txtTRLastIndex = findObj("txtTRLastIndex",document);
        var rowID = parseInt(txtTRLastIndex.value.Trim());
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "SignItem" + rowID;
        var i = 0;
        var newNameTD=newTR.insertCell(i++);//添加列:序号
        newNameTD.className="cell";
        newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
        
        var newNameXH=newTR.insertCell(i++);//添加列:选择
        newNameXH.className="cell";
        newNameXH.innerHTML="<input name='chk' id='chk" + rowID + "' value="+rowID+" type='checkbox' style='width:98%' size='20'/>";
        
        var newProductID=newTR.insertCell(i++);//添加列:物品ID
        newProductID.style.display = "none";
        newProductID.innerHTML = "<input name='ProdID_SignItem_TD_Sequ_Text_" + rowID + "' id='ProdID_SignItem_TD_Sequ_Text_" + rowID + "' value='"+temp.ProductID+"' type='text'  class='tdinput'  style='width:98%' readonly/>";//添加列内容
    
        var newProductNo=newTR.insertCell(i++);//添加列:物品编号
        newProductNo.className="cell";
        newProductNo.innerHTML = "<input name='ProdNo_SignItem_TD_Sequ_Text_" + rowID + "' id='ProdNo_SignItem_TD_Sequ_Text_" + rowID + "'value='"+temp.ProductNo+"' type='text'  class='tdinput'  style='width:98%' readonly/>";//添加列内容
        
        var newProductName=newTR.insertCell(i++);//添加列:物品名称
        newProductName.className="cell";
        newProductName.innerHTML = "<input name='ProductName_SignItem_TD_Sequ_Text_" + rowID + "' id='ProductName_SignItem_TD_Sequ_Text_" + rowID + "' value='"+temp.ProductName+"'type='text'  class='tdinput'style='width:98%'  readonly/>";//添加列内容
        
        var newUnitID=newTR.insertCell(i++);//添加列:单位ID
        newUnitID.style.display = "none";
        newUnitID.innerHTML = "<input name='UnitID_SignItem_TD_Sequ_Text_'"+rowID+" id='UnitID_SignItem_TD_Sequ_Text_" + rowID + "' value='"+temp.UnitID+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newUnitName=newTR.insertCell(i++);//添加列:单位
        newUnitName.className="cell";
        newUnitName.innerHTML = "<input name='Unit_SignItem_TD_Sequ_Text_'"+rowID+" id='Unit_SignItem_TD_Sequ_Text_" + rowID + "'value='"+temp.UnitName+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
              
        var newUnitPrice=newTR.insertCell(i++);//添加列:单价
        newUnitPrice.className="cell";
        newUnitPrice.innerHTML = "<input name='UnitPrice_SignItem_TD_Sequ_Text_" + rowID + "' id='UnitPrice_SignItem_TD_Sequ_Text_" + rowID + "'value='"+temp.UnitPrice+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newProductCount=newTR.insertCell(i++);//添加列:需求数量
        newProductCount.className="cell";
        newProductCount.innerHTML = "<input name='txtProductCount" + rowID + "' id='txtProductCount" + rowID + "' value='"+temp.ProductCount+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newPlanCount=newTR.insertCell(i++);//添加列:申请数量
        newPlanCount.className="cell";
        newPlanCount.innerHTML = "<input name='txtPlanCount" + rowID + "' id='txtPlanCount" + rowID + "' value='"+temp.PlanCount+"' type='text'  class='tdinput' style='width:98%' />";//添加列内容
        
        var newTotalPrice=newTR.insertCell(i++);//添加列:金额
        newTotalPrice.className="cell";
        newTotalPrice.innerHTML = "<input name='txtTotalPrice" + rowID + "' id='txtTotalPrice" + rowID + "'value='"+temp.TotalPrice+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
                      
        var newRequireDate=newTR.insertCell(i++);//添加列:需求日期
        newRequireDate.className="cell";
        newRequireDate.innerHTML = "<input name='txtRequireDate" + rowID + "' id='txtRequireDate" + rowID + "' value='"+temp.RequireDate+"' type='text'  class='tdinput'style='width:98%'  readonly/>";//添加列内容
        
        var newPlanTakeDate=newTR.insertCell(i++);//添加列:申请交货日期
        newPlanTakeDate.className="cell";
        newPlanTakeDate.innerHTML = "<input name='txtPlanTakeDate" + rowID + "' id='txtPlanTakeDate" + rowID + "' value='"+temp.PlanTakeDate+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newFromDeptName=newTR.insertCell(i++);//添加列:来源部门ID
        newFromDeptName.style.display = "none";
        newFromDeptName.innerHTML = "<input name='txtFromDeptID" + rowID + "' id='txtFromDeptID" + rowID + " 'value='"+temp.FromDeptID+"' type='text'  class='tdinput' readonly/>";//添加列内容
    
        var newFromDeptName=newTR.insertCell(i++);//添加列:来源部门
        newFromDeptName.className="cell";
        newFromDeptName.innerHTML = "<input name='txtFromDeptName" + rowID + "' id='txtFromDeptName" + rowID + "' value='"+temp.FromDeptName+"' type='text'  class='tdinput' readonly/>";//添加列内容
        
        var newFromBillID=newTR.insertCell(i++);//添加列:来源单据ID
        newFromBillID.style.display = "none";
        newFromBillID.innerHTML = "<input name='txtFromBillID" + rowID + "' id='txtFromBillID" + rowID + "' value='"+temp.FromBillID+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
    
        var newFromBillNo=newTR.insertCell(i++);//添加列:来源单据No
        newFromBillNo.className="cell";
        newFromBillNo.innerHTML = "<input name='txtFromBillNo" + rowID + "' id='txtFromBillNo" + rowID + "' value='"+temp.FromBillNo+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newFromLineNo=newTR.insertCell(i++);//添加列:来源单据行号
        newFromLineNo.className="cell";
        newFromLineNo.innerHTML = "<input name='txtFromLineNo" + rowID + "' id='txtFromLineNo" + rowID + "' value='"+temp.FromLineNo+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newProviderID=newTR.insertCell(i++);//添加列:供应商ID
        newProviderID.style.display = "none";
        newProviderID.innerHTML = "<input name='txtProviderID" + rowID + "' id='txtProviderID" + rowID + "' value='"+temp.ProviderID+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
    
        var newProviderName=newTR.insertCell(i++);//添加列:供应商
        newProviderName.className="cell";
        newProviderName.innerHTML = "<input name='txtProviderName" + rowID + "' id='txtProviderName" + rowID + "' value='"+temp.ProviderName+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newRemark=newTR.insertCell(i++);//添加列:备注
        newRemark.className="cell";
        newRemark.innerHTML = "<input name='txtRemark" + rowID + "' id='txtRemark" + rowID + "' type='text'  value='"+temp.Remark+"' class='tdinput' style='width:98%' readonly/>";//添加列内容

        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
}

//添加明细行
function AddSignRow3(temp)
{
    var txtTRLastIndex2 = findObj("DtlCount",document);

    var rowID = parseInt(txtTRLastIndex2.value.Trim());
    var signFrame2 = findObj("dg_Log2",document);
    var newTR = signFrame2.insertRow(signFrame2.rows.length);//添加行
    newTR.id = "SignItem2" + rowID;
    var i = 0;
    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="cell";
    newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
    
    var newProductID=newTR.insertCell(i++);//添加列:产品ID
    newProductID.style.display = "none";
    newProductID.innerHTML = "<input name='txtProductID2" + rowID + "' id='txtProductID2" + rowID + "' value='"+temp.ProductID+"'  type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容

    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="cell";
    newProductNo.innerHTML = "<input name='txtProductNo2" + rowID + "' id='txtProductNo2" + rowID + "' value='"+temp.ProductNo+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容

    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="cell";
    newProductName.innerHTML = "<input name='txtProductName2" + rowID + "' id='txtProductName2" + rowID + "'value='"+temp.ProductName+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容

    var newUnitID=newTR.insertCell(i++);//添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input name='txtUnitID2" + rowID + "' id='txtUnitID2" + rowID + "' value='"+temp.UnitID+"'  type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容

    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input name='txtUnitName2"+rowID+"' id='txtUnitName2" + rowID + "'value='"+temp.UnitName+"' type='text'  class='tdinput'  style='width:98%' readonly/>";//添加列内容

    var newProductCount=newTR.insertCell(i++);//添加列:计划数量
    newProductCount.className="cell";
    newProductCount.innerHTML = "<input name='txtPlanCount2" + rowID + "' id='txtPlanCount2" + rowID + "'value='"+temp.PlanCount+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容

    var newRequireDate=newTR.insertCell(i++);//添加列:需求日期
    newRequireDate.className="cell";
    newRequireDate.innerHTML = "<input name='txtRequireDate2" + rowID + "' id='txtRequireDate2" + rowID + "'value='"+temp.RequireDate+"' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容

    var newApplyReasonID=newTR.insertCell(i++);//添加列:理由ID
    newApplyReasonID.style.display = "none";
    newApplyReasonID.innerHTML = "<input name='txtApplyReasonID2" + rowID + "' id='txtApplyReason2" + rowID + "' value='"+temp.ApplyReasonID+"'  type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容

    var newProviderName=newTR.insertCell(i++);//添加列:申请原因
    newProviderName.className="cell";
    newProviderName.innerHTML = "<input name='txtApplyReason2" + rowID + "' id='txtApplyReason2" + rowID + "'value='"+temp.ApplyReasonName+"' type='text'  class='tdinput' style='width:98%' onclick=\"popApplyReasonObj.ShowList("+rowID+");\" readonly/>";//添加列内容

    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.className="cell";
    newRemark.innerHTML = "<input name='txtRemark2" + rowID + "' id='txtRemark2" + rowID + "' value='"+temp.Remark+"'  type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
    
    txtTRLastIndex2.value = (rowID + 1).toString() ;//将行号推进下一行
}

function AddSignRow()
 { //读取最后一行的行号，存放在txtTRLastIndex文本框中 
        txtTRLastIndex = findObj("txtTRLastIndex",document);
    
        var rowID = parseInt(txtTRLastIndex.value.Trim());
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "SignItem" + rowID;
        var i=0;
      
        var newNameTD=newTR.insertCell(i++);//添加列:序号
        newNameTD.className="cell";
        newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
              
       var newNameXH=newTR.insertCell(i++);//添加列:选择
        newNameXH.className="cell";
        newNameXH.innerHTML="<input name='chk' id='chk" + rowID + "' value="+rowID+" type='checkbox' style='width:98%' size='20'/>";
        
        var newProductID=newTR.insertCell(i++);//添加列:物品ID
        newProductID.style.display = "none";
        newProductID.innerHTML = "<input name='ProdID_SignItem_TD_Sequ_Text_" + rowID + "' id='ProdID_SignItem_TD_Sequ_Text_" + rowID + "'  type='text'  class='tdinput'  style='width:98%'/>";//添加列内容
    
        var newProductNo=newTR.insertCell(i++);//添加列:物品编号
        newProductNo.className="cell";
        newProductNo.innerHTML = "<input name='ProdNo_SignItem_TD_Sequ_Text_" + rowID + "' id='ProdNo_SignItem_TD_Sequ_Text_" + rowID + "' type='text'  class='tdinput' onclick=\"popTechObj.ShowList('SignItem_TD_Sequ_Text_"+rowID+"');\"  style='width:98%' />";//添加列内容
        
        var newProductName=newTR.insertCell(i++);//添加列:物品名称
        newProductName.className="cell";
        newProductName.innerHTML = "<input name='ProductName_SignItem_TD_Sequ_Text_" + rowID + "' id='ProductName_SignItem_TD_Sequ_Text_" + rowID + "' type='text'  class='tdinput'style='width:98%' readonly />";//添加列内容
        
        var newUnitID=newTR.insertCell(i++);//添加列:单位ID
        newUnitID.style.display = "none";
        newUnitID.innerHTML = "<input name='UnitID_SignItem_TD_Sequ_Text_'"+rowID+" id='UnitID_SignItem_TD_Sequ_Text_" + rowID + "'  type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newUnitName=newTR.insertCell(i++);//添加列:单位
        newUnitName.className="cell";
        newUnitName.innerHTML = "<input name='Unit_SignItem_TD_Sequ_Text_'"+rowID+" id='Unit_SignItem_TD_Sequ_Text_" + rowID + "' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
              
        var newUnitPrice=newTR.insertCell(i++);//添加列:单价
        newUnitPrice.className="cell";
        newUnitPrice.innerHTML = "<input name='UnitPrice_SignItem_TD_Sequ_Text_" + rowID + "' id='UnitPrice_SignItem_TD_Sequ_Text_" + rowID + "'type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newProductCount=newTR.insertCell(i++);//添加列:需求数量
        newProductCount.className="cell";
        newProductCount.innerHTML = "<input name='txtProductCount" + rowID + "' id='txtProductCount" + rowID + "'  type='text'  class='tdinput' style='width:98%' />";//添加列内容
        
        var newPlanCount=newTR.insertCell(i++);//添加列:申请数量
        newPlanCount.className="cell";
        newPlanCount.innerHTML = "<input name='txtPlanCount" + rowID + "' id='txtPlanCount" + rowID + "'  type='text'  class='tdinput' onblur=\"ClcltMny(this.id)\"  style='width:98%' />";//添加列内容
        
        var newTotalPrice=newTR.insertCell(i++);//添加列:金额
        newTotalPrice.className="cell";
        newTotalPrice.innerHTML = "<input name='txtTotalPrice" + rowID + "' id='txtTotalPrice" + rowID + "' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
                      
        var newRequireDate=newTR.insertCell(i++);//添加列:需求日期
        newRequireDate.className="cell";
        newRequireDate.innerHTML = "<input name='txtRequireDate" + rowID + "' id='txtRequireDate" + rowID + "' type='text'  class='tdinput'style='width:98%'  readonly/>";//添加列内容
        $("#txtRequireDate" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#txtRequireDate" + rowID)})});
        
        var newPlanTakeDate=newTR.insertCell(i++);//添加列:申请交货日期
        newPlanTakeDate.className="cell";
        newPlanTakeDate.innerHTML = "<input name='txtPlanTakeDate" + rowID + "' id='txtPlanTakeDate" + rowID + "'  type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        $("#txtPlanTakeDate" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#txtPlanTakeDate" + rowID)})});
       
        var newFromDeptName=newTR.insertCell(i++);//添加列:来源部门ID
        newFromDeptName.style.display = "none";
        newFromDeptName.innerHTML = "<input name='txtFromDeptID" + rowID + "' id='txtFromDeptID" + rowID + " ' type='text'  class='tdinput' readonly/>";//添加列内容
    
        var newFromDeptName=newTR.insertCell(i++);//添加列:来源部门
        newFromDeptName.className="cell";
        newFromDeptName.innerHTML = "<input name='txtFromDeptName" + rowID + "' id='txtFromDeptName" + rowID + "'  type='text'  class='tdinput' readonly/>";//添加列内容
        
        var newFromBillID=newTR.insertCell(i++);//添加列:来源单据ID
        newFromBillID.style.display = "none";
        newFromBillID.innerHTML = "<input name='txtFromBillID" + rowID + "' id='txtFromBillID" + rowID + "' type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
    
        var newFromBillNo=newTR.insertCell(i++);//添加列:来源单据No
        newFromBillNo.className="cell";
        newFromBillNo.innerHTML = "<input name='txtFromBillNo" + rowID + "' id='txtFromBillNo" + rowID + "'  type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newFromLineNo=newTR.insertCell(i++);//添加列:来源单据行号
        newFromLineNo.className="cell";
        newFromLineNo.innerHTML = "<input name='txtFromLineNo" + rowID + "' id='txtFromLineNo" + rowID + "' v type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
        
        var newProviderID=newTR.insertCell(i++);//添加列:供应商ID
        newProviderID.style.display = "none";
        newProviderID.innerHTML = "<input name='txtProviderID" + rowID + "' id='txtProviderID" + rowID + "'  type='text'  class='tdinput' style='width:98%' readonly/>";//添加列内容
    
        var newProviderName=newTR.insertCell(i++);//添加列:供应商
        newProviderName.className="cell";
        newProviderName.innerHTML = "<input name='txtProviderName" + rowID + "' id='txtProviderName" + rowID + "' type='text'  class='tdinput' style='width:98%' onclick=\"popProviderObj.ShowList('txtProviderName"+rowID+"');\"/>";//添加列内容
        
        var newRemark=newTR.insertCell(i++);//添加列:备注
        newRemark.className="cell";
        newRemark.innerHTML = "<input name='txtRemark" + rowID + "' id='txtRemark" + rowID + "' type='text' class='tdinput' style='width:98%' readonly/>";//添加列内容

        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
               
}

function AddSignRow2()
{
    var txtTRLastIndex2 = document.getElementById("DtlCount");
    var rowID = parseInt(txtTRLastIndex2.value.Trim());
    var signFrame2 = findObj("dg_Log2",document);
    var newTR = signFrame2.insertRow(signFrame2.rows.length);//添加行
    newTR.id = "SignItem2" + rowID;
    var i = 0;
    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="cell";
    newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
    
    var newProductID=newTR.insertCell(i++);//添加列:物品ID
    newProductID.style.display = "none";
    newProductID.innerHTML = "<input name='txtProductID2" + rowID + "' id='txtProductID2" + rowID + "' type='text' />";//添加列内容
    
    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="pro2";
    newProductNo.innerHTML = "<input name='txtProductNo2" + rowID + "' id='txtProductNo2" + rowID + "' type='text'  class='tdinput' readonly/>";//添加列内容

    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="cell";
    newProductName.innerHTML = "<input name='txtProductName2" + rowID + "' id='txtProductName2" + rowID + "' type='text'  class='tdinput' readonly/>";//添加列内容

    var newUnitID=newTR.insertCell(i++);//添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input name='txtUnitID2"+rowID+"' id='txtUnitID2" + rowID + "' type='text'  class='tdinput' readonly/>";//添加列内容

    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="unitName";
    newUnitName.innerHTML = "<input name='txtUnitName2"+rowID+"' id='txtUnitName2" + rowID + "' type='text'  class='tdinput' readonly/>";//添加列内容

    var newProductCount=newTR.insertCell(i++);//添加列:计划数量
    newProductCount.className="unitName";
    newProductCount.innerHTML = "<input name='txtPlanCount2" + rowID + "' id='txtPlanCount2" + rowID + "' type='text'  class='tdinput' readonly/>";//添加列内容

    var newRequireDate=newTR.insertCell(i++);//添加列:需求日期
    newRequireDate.className="proID";
    newRequireDate.innerHTML = "<input name='txtRequireDate2" + rowID + "' id='txtRequireDate2" + rowID + "' type='text'  class='tdinput' readonly/>";//添加列内容
    $("#txtRequireDate2" + rowID).focus(function(){WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$("#txtRequireDate2" + rowID)})});
    
    var newApplyReasonID=newTR.insertCell(i++);//添加列:申请原因
    newApplyReasonID.style.display = "none";
    newApplyReasonID.innerHTML = "<input name='txtApplyReasonID2" + rowID + "' id='txtApplyReasonID2" + rowID + "' type='text'  class='tdinput' />";//添加列内容

    var newApplyReason=newTR.insertCell(i++);//添加列:申请原因
    newApplyReason.className="remark";
    newApplyReason.innerHTML = "<input name='txtApplyReason2" + rowID + "' id='txtApplyReason2" + rowID + "' type='text' onclick=\"popApplyReasonObj.ShowList("+rowID+");\" class='tdinput' />";//添加列内容

    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.className="remark";
    newRemark.innerHTML = "<input name='txtRemark2" + rowID + "' id='txtRemark2" + rowID + "' type='text'  class='tdinput' />";//添加列内容

    txtTRLastIndex2.value = (rowID + 1).toString() ;//将行号推进下一行
}

 function Fun_FillParent_Content(id,no,productname,price,unitid,unit)
{
    if(!ExistFromDtl(no))
    {
        var hiddenObj = 'ProdNo_' + popTechObj.InputObj;
        var ProductName='ProductName_'+popTechObj.InputObj;
        var UnitID = 'UnitID_'+popTechObj.InputObj;
        var Unit='Unit_'+popTechObj.InputObj;
        var Price='UnitPrice_'+popTechObj.InputObj;
        var ProductID = 'ProdID_'+popTechObj.InputObj;
        document.getElementById(UnitID).value = unitid;
        document.getElementById(Unit).value=unit;
        document.getElementById(ProductID).value = id;
        document.getElementById(hiddenObj).value = no;
        document.getElementById(ProductName).value = productname;
        document.getElementById(Price).value = price;
        document.getElementById('divStorageProduct').style.display='none';
    }
    
}


function Fun_Bill007(billid,orderno,sortno,productid,productno,productname,unitid,unit,productcount,unitprice,totalprice,fromdeptid,fromdeptname)
{
alert(ExistFromBill(orderno,sortno))
    if(!ExistFromBill(orderno,sortno))
    {
    
        var Index = findObj("txtTRLastIndex",document).value;
        AddSignRow();
        var FromBillID = 'txtFromBillID'+Index;
        var OrderNo = 'txtFromBillNo'+Index;
        var SortNo = 'txtFromLineNo'+Index;
        var ProductID = 'ProdID_SignItem_TD_Sequ_Text_'+Index;
        var ProductNo = 'ProdNo_SignItem_TD_Sequ_Text_'+Index;
        var ProductName = 'ProductName_SignItem_TD_Sequ_Text_'+Index;
        var UnitID = 'UnitID_SignItem_TD_Sequ_Text_'+Index;
        var Unit = 'Unit_SignItem_TD_Sequ_Text_'+Index;
        var ProductCount = 'txtProductCount'+Index;
        var UnitPrice = 'UnitPrice_SignItem_TD_Sequ_Text_'+Index;
        var TotalPrice = 'txtTotalPrice'+Index;
        var FromDeptID = 'txtFromDeptID'+Index;
        var FromDeptName = 'txtFromDeptName'+Index;
        
        
        document.getElementById(FromBillID).value = billid;
        document.getElementById(OrderNo).value = orderno;
        document.getElementById(SortNo).value = sortno;
        document.getElementById(ProductID).value = productid;
        document.getElementById(ProductNo).value = productno;
        document.getElementById(ProductName).value = productname;
        document.getElementById(UnitID).value = unitid;
        document.getElementById(Unit).value = unit;
        document.getElementById(ProductCount).value = productcount;
        document.getElementById(UnitPrice).value = unitprice;
        document.getElementById(TotalPrice).value = totalprice;
        document.getElementById(ProductID).value = productid;
        document.getElementById(ProductName).value = productname;
//        document.getElementById(FromDeptID).value = fromdeptid;
//        document.getElementById(FromDeptName).value = fromdeptname;
    }
}

//判断是否有相同记录有返回true，没有返回false
function ExistFromBill(orderno,sortno)
{
    var signFrame = document.getElementById("dg_Log");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var frombillno = document.getElementById("txtFromBillNo"+i).value.Trim();
        var fromlineno = document.getElementById("txtFromLineNo"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(frombillno==orderno)&&(fromlineno == sortno))
        {
            return true;
        } 
    }
    return false;
}

function ExistFromDtl(productno)
{
    var signFrame = document.getElementById("dg_Log");
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var ProductNo = document.getElementById("ProdNo_SignItem_TD_Sequ_Text_"+i).value.Trim();
        var frombillid = document.getElementById("txtFromBillID"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(ProductNo==productno)&&((frombillid == '')||(frombillid == 'undefined')))
        {
            return true;
        } 
    }
    return false;
}

function FillByMRP(id,mrpno,sortno,productid,productno,productname,unitid,unit,plancount,processedcount,totalprice)
{

//alert("id="+id+"mrpno="+mrpno+"sortno="+sortno+"productid="+productid+"productno="+productno+"productname="+productname+"unitid="+unitid+"unit="+unit+"plancount="+plancount+"processedcount="+processedcount+"totalprice="+totalprice);
    if(!ExistFromBill(mrpno,sortno))
    {
        var Index = findObj("txtTRLastIndex",document).value;
        AddSignRow();
        var MRPNo = 'txtFromBillNo'+Index;
        var SortNo = 'txtFromLineNo'+Index;
        var ProductID = 'ProdID_SignItem_TD_Sequ_Text_'+Index;
        var ProductNo = 'ProdNo_SignItem_TD_Sequ_Text_'+Index;
        var ProductName = 'ProductName_SignItem_TD_Sequ_Text_'+Index;
        var UnitID = 'UnitID_SignItem_TD_Sequ_Text_'+Index;
        var Unit = 'Unit_SignItem_TD_Sequ_Text_'+Index;
        var ProductCount = 'txtProductCount'+Index;
        var TotalPrice = 'txtTotalPrice'+Index;
        
        document.getElementById(MRPNo).value = mrpno;
        document.getElementById(SortNo).value = sortno;
        document.getElementById(ProductID).value=productid;
        document.getElementById(ProductNo).value = productno;
        document.getElementById(ProductName).value = productname;
        document.getElementById(UnitID).value = unitid;
        document.getElementById(Unit).value = unit;
        document.getElementById(ProductCount).value = plancount;
    }
}

function FillApplyReason(applyreasonid,applyreasontitle,applyreason)
{
    var i = popApplyReasonObj.InputObj;
    var ReasonID = "txtApplyReasonID2"+i;
    var ReasonTitle = "txtApplyReason2"+i;
    document.getElementById(ReasonID).value = applyreasonid;
    document.getElementById(ReasonTitle).value = applyreasontitle;
    closeApplyReasondiv();
}

function FillUnit(unitid,unitname)
{
    var i = popUnitObj.InputObj;
    var UnitID = "UnitID_SignItem_TD_Sequ_Text_"+i;
    var UnitName = "Unit_SignItem_TD_Sequ_Text_"+i;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(UnitName).value = unitname;
}

function ClcltMny(o)
{
    var i = o.split('o')[1];
    var index = parseInt(i.split('t')[1]);
    var unitprice = document.getElementById("UnitPrice_SignItem_TD_Sequ_Text_"+index).value.Trim();
//    if(!((IsNumber(unitprice))||(IsNumeric(unitprice,12,4))))
    if(!IsNumberOrNumeric(unitprice,12,4))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请输入正确的单价！");
        return;
    }
    
    var count = document.getElementById("txtPlanCount"+index).value.Trim();
    if(!((IsNumber(count))||(IsNumeric(count,12,4))))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请输入正确的数量！");
        return;
    }
    var total = parseInt(unitprice)*parseInt(count);
    document.getElementById("txtTotalPrice"+index).value=total;
}

function FillProvider(providerid,providerno,providername)
{    
    var ProviderName = popProviderObj.InputObj;
    var index = ProviderName.split('e');
    var i = parseInt(index[2]);
    var ProviderID ="txtProviderID"+i;
    
    document.getElementById(ProviderName).value = providername;
    document.getElementById(ProviderID).value = providerid;
    closeProviderdiv();
}