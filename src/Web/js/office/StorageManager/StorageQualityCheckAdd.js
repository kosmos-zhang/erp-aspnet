var requestQuaobj= new Object();
var ApplyNo = "";
var FromType="";
var ID="";
var Action="";
var BillStatusID='';


//选择往来客户
window.onload=function(){
    GetFlowButton_DisplayControl();
    if(myFrom=='List')
    {
        document.getElementById('btnReturn').style.display='block';
    }
}


$(document).ready(function()
{
    var myIndentityID=document.getElementById('hiddeniqualityd');      
    requestQuaobj = GetRequest();   
    ID=requestQuaobj['ID']; 
    fnGetExtAttr();
    $("#CountTotal1").val(FormatAfterDotNumber(0,selPoint));
    if(typeof(ID)!='undefined')
    {    
        myIndentityID.value=ID;
    }     
    if(myIndentityID.value>0)
    {   
        document.getElementById('addQuality').style.display='none';
        document.getElementById('editQuality').style.display='';    
        document.getElementById('btnReturn').style.display='';   
        GetQualityInfo(ID); 
    }
    else
    {
        //加载拓展属性(这里放在新建的时候)
        GetExtAttr("officedba.QualityCheckApplay", null);
        document.getElementById('editQuality').style.display=='none';
    }  
});

//质检保存------Start-------
function SaveStorageQualityInfo() 
{  
  if(CheckAddInfo())
  { 
   if(document.getElementById('isflow').value>0)
   {     
       return false;  
   }
   if(document.getElementById('isupdate').value>0)
   {         
       return false;  
   }
    var bmgz="";
    var isFlag = true;
    var txtTitle=$("#txtTitle").val();
    var fieldText = "";
    var msgText = "";
    var txtInNo="";
    var ddlFromType=document.getElementById('ddlFromType').value;
    var hiddeniqualityd =document.getElementById('hiddeniqualityd').value;
    if(hiddeniqualityd=="0")//新建
    {
      if($("#txtInNo_ddlCodeRule").val()=="")//手工输入
       {
        txtInNo=$("#txtInNo_txtCode").val();
        bmgz="sd";
          if (txtInNo == "")
          {
            isFlag = false;
            fieldText += "单据编号|";
   	        msgText += "请输入单据编号|";
          }
       }
     else
       {
        txtInNo=$("#txtInNo_ddlCodeRule").val();
        bmgz="zd";
       }
   }
   else
   {
      txtInNo=document.getElementById("lbInNo").value;
   } 
  
    var ddlFromType=$("#ddlFromType").val();
    var CustID='0';  //默认值
    var CustBigTypeID='0'; //默认值

    CustID=document.getElementById('txtPeople').value; //生产负责人
    CustBigTypeID=document.getElementById("txtDep").value; //生产部门   

    var txtCustNameNo='0';  
    var CustBigType='0';
    txtCustNameNo=document.getElementById("hiddenCustID").value;
    CustBigType=document.getElementById("hiddenCustBigType").value;
    var CheckType=document.getElementById("CheckType").value;
    var CheckMode=document.getElementById("CheckMode").value;
    var Checker=document.getElementById("txtChecker").value;
    var checkChecker=document.getElementById("txtChecker").value
    var CheckDeptId=document.getElementById("CheckDeptId").value;
    var checkCheckDeptId=document.getElementById("CheckDeptId").value;
    var txtEnterDate=document.getElementById("txtEnterDate").value;
    var txtCreator=document.getElementById("txtCreator").value;
    var txtCreateDate=document.getElementById("txtCreateDate").value;
    var txtRemark=$("#txtRemark").val();
    var Attachment=document.getElementById("hfPageAttachment").value;
    var txtCloseDate=document.getElementById("txtCloseDate").value;
    var txtModifiedUserID=document.getElementById("tbModifiedUserID").value;
    var txtModifiedDate=document.getElementById("txtModifiedDate").value;         
    var hiddenValue = document.getElementById("txtInNoHidden").value;
    var ddlBillStatus=document.getElementById("ddlBillStatus").value;
    var txtCustName=document.getElementById("txtCustNameNo").value; 
    var myCountTotal=document.getElementById('CountTotal1').value;
    var DetailFromBillID=new Array();
    var DetailProID = new Array();
    var DetailSortNo = new Array();//序号
    var DetailUnitID = new Array();
    var DetailQuaCheckCount = new Array(); //报检 数量
    var DetailQuaCheckCount2=0;  //报检数量 总和
    var DetailFromBillID = new Array();  //源单编号
    var DetailProductCount=new Array();  //到货数量
    var DetailFromBillNO = new Array(); //源单行号
    var DetailCheckedCount = new Array(); //已检数量
    var DetailUsedUnitID=new Array();// 基本计量单位
    var DetailUsedUnitCount=new Array();// 基本计量数量
    var DetailRemark=new Array();
    var CountTotal=0;                       //报检数量总和
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    var DetailProductCount2=0;
         for(var i=1;i<count;i++)
         {
            if(signFrame.rows[i].style.display!="none")
            {
                var objProductNo = 'hidProductName'+(i);  //物品名称
                var objUnitID = 'UnitID'+(i);          //单位
                var objCheckCount ='CheckedCounted_'+(i);    //报检数量
                var objFromBillID='FromBillID'+(i);     //源单ID
                var objFromBillLineID = 'FromLineNo'+(i);  //源单行号
                var objCheckedCount = 'ProductCount'+(i);   // 已检数量
                var objRemark = 'Remark'+(i);                 //备注
                var objProductCount='ProductCount1'+(i);    //到货数量
                var objRealCheckedCount='RealCheckedCount'+(i);
                if(ddlFromType=="1")
                {
                    DetailProductCount2=parseFloat(DetailProductCount2)+parseFloat(document.getElementById(objProductCount.toString()).value);
                }
                DetailCheckedCount.push(document.getElementById(objCheckedCount.toString()).value);
                DetailQuaCheckCount2=parseFloat(DetailQuaCheckCount2)+parseFloat(document.getElementById(objCheckCount.toString()).value);
                DetailProID.push(document.getElementById(objProductNo).value);
                DetailSortNo.push(i);
                if(ddlFromType!='0')
                {
                    DetailFromBillID.push(document.getElementById(objFromBillID).value);
                    DetailFromBillNO.push(document.getElementById(objFromBillLineID.toString()).value);
                }
                DetailRemark.push(document.getElementById(objRemark.toString()).value);
                if(isMoreUnit)
                {// 启用多计量单位
                    DetailUsedUnitID.push(document.getElementById(objUnitID).value);// 基本计量单位
                    DetailUsedUnitCount.push(document.getElementById(objCheckCount.toString()).value);// 基本计量数量
                    DetailQuaCheckCount.push($("#UsedUnitCount"+i).val());
                    DetailUnitID.push($("#UsedUnitID"+i).val());
                }
                else
                {
                    DetailUnitID.push(document.getElementById(objUnitID).value);
                    DetailQuaCheckCount.push(document.getElementById(objCheckCount.toString()).value);
                }
              }
        }

         var UrlParam = "txtInNo="+escape(txtInNo)+
                        "&txtTitle="+escape(txtTitle)+
                        "&ddlFromType="+ddlFromType+
                        "&txtCustNameNo="+escape(txtCustNameNo)+
                        "&CustBigType="+CustBigType+
                        "&CheckType="+CheckType+
                        "&CustID="+CustID+
                        "&myCountTotal="+myCountTotal+
                        "&txtCustName="+escape(txtCustName)+
                        "&ddlBillStatus="+ddlBillStatus+                   
                        "&CustBigTypeID="+CustBigTypeID+
                        "&CheckMode="+CheckMode+
                        "&Checker="+Checker+
                        "&CheckDeptId="+CheckDeptId+
                        "&txtEnterDate="+txtEnterDate+                      
                        "&txtCreator="+txtCreator+                      
                        "&txtCreateDate="+txtCreateDate+
                        "&txtRemark="+escape(txtRemark)+
                        "&Attachment="+escape(Attachment)+
                        "&txtCloseDate="+txtCloseDate+
                        "&txtModifiedUserID="+txtModifiedUserID+  
                        "&DetailProID="+DetailProID.toString()+
                        "&DetailSortNo="+DetailSortNo.toString()+                     
                        "&DetailUnitID="+DetailUnitID.toString()+                     
                        "&DetailQuaCheckCount="+DetailQuaCheckCount.toString()+
                        "&DetailFromBillID="+DetailFromBillID.toString()+
                        "&DetailFromBillNO="+DetailFromBillNO.toString()+
                        "&DetailCheckedCount="+DetailCheckedCount.toString()+
                        "&DetailUsedUnitID="+DetailUsedUnitID.toString()+
                        "&DetailUsedUnitCount="+DetailUsedUnitCount.toString()+
                        "&DetailRemark="+escape(DetailRemark)+
                        "&txtModifiedDate="+txtModifiedDate+
                        "&bmgz="+bmgz+
                        "&ID="+hiddeniqualityd+     
                        "&action=add"+ 
                        GetExtAttrValue(); 


                  $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageQualityCheckAdd.ashx",
                  dataType:'json',//返回json格式数据
                  data:UrlParam,
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  //complete :function(){ //hidePopup();},
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {                        
                        var reInfo=data.data.split('|');
                        var myBillStatus=document.getElementById('ddlBillStatus').value;
                        if(reInfo.length > 1)
                        {   
                            popMsgObj.ShowMsg(data.info);
                            if(reInfo[0]=='none')
                            {
                                return false;
                            }
                            document.getElementById('lbInNo').value = reInfo[1];
                            document.getElementById('div_InNo_uc').style.display="none";
                            document.getElementById('div_InNo_Lable').style.display="";
                            if(hiddeniqualityd>0)
                            {
                                 if(myBillStatus=='2')
                                 {
                                    document.getElementById('ddlBillStatus').value='3';
                                    document.getElementById('tbBillStatus').value='变更';
                                 }
                                 document.getElementById('txtModifiedDate').value=reInfo[2].substring(0,9);
                                 document.getElementById('tbModifiedUserID').value=reInfo[3]; 
                            }
                            if(parseInt(hiddeniqualityd)<=0)
                            {
                                document.getElementById('hiddeniqualityd').value= reInfo[0];
                            }
                        }                      
                      
                        
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
//质检保存------End------

function GetAllCheckedCount()      //计算报检数量和
{
    var FromType=document.getElementById('ddlFromType');
    if(FromType!='0')
    {
        var myAllProductCount=document.getElementById('CountTotal1');
        var signFrame = findObj("dg_Log",document); 
        var count = signFrame.rows.length;//有多少行
        var testDetailCount='0';
        for(var i=1;i<count;i++)
        {
           
            if(signFrame.rows[i].style.display!='none')
            {
                 var CheckedCount='CheckedCounted_'+(i); //明细报检数量
                if(parseFloat(document.getElementById(CheckedCount).value)>=0)
                {
                    testDetailCount=parseFloat(testDetailCount)+parseFloat(document.getElementById(CheckedCount).value);
                }
            }
        }
        myAllProductCount.value=FormatAfterDotNumber(testDetailCount,selPoint);   
    } 
}
function CheckAddInfo()
{
    var isFlag=true;
    var fieldText='';
    var msgText='';
    var ApplyNo='';
    var FromType=document.getElementById('ddlFromType').value;
    var myTitle=document.getElementById('txtTitle').value;
    var myChecker=document.getElementById('txtChecker').value;
    var myCheckDeptId=document.getElementById("CheckDeptId").value;
    var txtRemark=document.getElementById('txtRemark').value;
    //先检验页面上的特殊字符
      var RetVal=CheckSpecialWords();
      if(RetVal!="")
      {
          isFlag = false;
          fieldText = fieldText + RetVal+"|";
          msgText = msgText +RetVal+  "不能含有特殊字符|";
      }
       //获取编码规则下拉列表选中项
    codeRule = document.getElementById("txtInNo_ddlCodeRule").value; 
    if(document.getElementById('hiddeniqualityd').value=='0')
    {

        //如果选中的是 手工输入时，校验编号是否输入        
        if (codeRule == "")
        {
            //获取输入的编号
            ApplyNo = document.getElementById("txtInNo_txtCode").value;
            //编号必须输入
            if (ApplyNo == "")
            {
                isFlag = false;
                fieldText += "单据编号|";
	            msgText += "请输入单据编号|";
            }
            else
            {
                if (isnumberorLetters(ApplyNo))
                {
                    isFlag = false;
                    fieldText += "单据编号|";
                    msgText += "单据编号只能包含字母或数字！|";
                }
            }
        }
    }
    if(strlen(myTitle)>100)
    {
        isFlag = false;
        fieldText = fieldText + "主题|";
   		msgText = msgText +  "最多只允许输入100个字符|";
    }
    
    if(strlen(myTitle)>0)
    {
         if(!CheckSpecialWord(myTitle))
        {
            isFlag = false;
            fieldText = fieldText + "主题|";
   		    msgText = msgText +  "单据主题不能含有特殊字符 |";
        }
    }
    if(strlen(txtRemark)>800)
    {
               isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText +  "最多只允许输入800个字符|"; 
    }   
    if(myChecker=='0')
    {
        isFlag = false;
        fieldText = fieldText + "报检人|";
   		msgText = msgText +  "请输入报检人|"; 
    }
    if(myCheckDeptId=='0')
    {
        isFlag = false;
        fieldText = fieldText + "报检部门|";
   		msgText = msgText +  "请输入报检部门|"; 
    }
    
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行

    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        {            
              var CheckedCount='CheckedCounted_'+(i); //明细报检数量
              var myFromType=document.getElementById('ddlFromType').value;
              var ProductID='hidProductName'+(i);
              var DetailRemark='Remark'+(i);
              if(strlen(document.getElementById(CheckedCount).value)<=0)
               {
                  isFlag = false;
                  fieldText = fieldText + "明细报检数量|";
                  msgText = msgText +  "请输入明细数量|";
               }
              if(strlen(document.getElementById(CheckedCount).value)>0)
               {
                 if(!IsNumeric(document.getElementById(CheckedCount.toString()).value,10,selPoint))
                  {
                     isFlag = false;
                     fieldText = fieldText + "明细报检数量|";
                     msgText = msgText +  "明细报检数量格式不正确|";
                  }
               } 
              if(document.getElementById(ProductID).value=='0')
              {
                     isFlag = false;
                     fieldText = fieldText + "明细物品名称|";
                     msgText = msgText +  "请输入明细物品名称|";
              }
              if(myFromType=='1')
                {
                  var ProductCount='ProductCount1'+(i); //源单类型为采购时  到货数量
                  if(parseFloat(document.getElementById(CheckedCount.toString()).value)>parseFloat(document.getElementById(ProductCount.toString()).value))
                  {
                        isFlag = false;
                        fieldText = fieldText + "明细报检数量|";
                        msgText = msgText +  "报检数量不能大于到货数量|";
                  }
              }
              if(myFromType=='2')
              {
                 var ProductCount='InCount'+(i);         //源单类型为 生产任务单  入库数量               
                  if(parseFloat(document.getElementById(CheckedCount.toString()).value)>parseFloat(document.getElementById(ProductCount.toString()).value))
                  {
                        isFlag = false;
                        fieldText = fieldText + "明细报检数量|";
                        msgText = msgText +  "报检数量不能大于入库数量|";
                  }
              }
              if(strlen(DetailRemark)>0)
              {
                 if(!CheckSpecialWord(DetailRemark))
                {
                    isFlag = false;
                    fieldText = fieldText + "备注|";
   		            msgText = msgText +  "备注不能含有特殊字符 |";
                }
              }
        }
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
function GetQualityInfo(a)
{
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/StorageManager/StorageQualityEdit.ashx?ID="+a,//目标地址
       data:' ',
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前
       success: function(msg){
       var index=1;
                //数据获取完毕，填充页面据显示
             if(typeof(msg.datap)!='undefined')
              {
                $.each(msg.datap,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                        try
                        {
                            if(msg.IsFlow>0 || msg.IsTransfer>0)
                            {
                                document.getElementById('btnSave').src='../../../images/Button/UnClick_bc.jpg';
                            }
                        }
                        catch(e)
                        {}
                
                        document.getElementById('isflow').value=msg.IsFlow;
                        document.getElementById('isupdate').value=msg.IsTransfer;
                        document.getElementById('div_InNo_uc').style.display="none";
                        document.getElementById('hiddeniqualityd').value=item.ID;
                        document.getElementById('div_InNo_Lable').style.display="";
                        document.getElementById('addQuality').style.display='none';
                        document.getElementById('editQuality').style.display='';
                        document.getElementById('lbInNo').value=item.ApplyNo;                  
                        document.getElementById("txtTitle").value=item.Title;//                        
                        document.getElementById('ddlFromType').value=item.FromType;//
                        document.getElementById("txtCustNameNo").value=item.CustName;
                        document.getElementById("hiddenCustID").value=item.CustID;
                        document.getElementById("hiddenCustBigType").value=item.CustBigType;
                        document.getElementById("CheckType").value=item.CheckType; //                       
                        if(item.FromType!='1')
                        {
                            document.getElementById("UserPrincipal").value=item.PrincipalName;//
                            
                            document.getElementById("txtPeople").value=item.Principal;//
                            document.getElementById("DeptName").value=item.DeptName;//
                            document.getElementById("txtDep").value=item.DeptID;//
                           
                        }
                        document.getElementById("CheckMode").value=item.CheckMode;//
                        document.getElementById("UserChecker").value=item.CheckerName;//
                        document.getElementById("txtChecker").value=item.CheckerID;
                        document.getElementById("DeptCheckDept").value=item.CheckDeptName;
                        document.getElementById("CheckDeptId").value=item.CheckDeptId;
                        document.getElementById("txtEnterDate").value=item.CheckDate.substring(0,10);
                        document.getElementById("CountTotal1").value=FormatAfterDotNumber(item.CountTotal,selPoint);
                        document.getElementById("tbCreator").value=item.CreatorName;
                        document.getElementById("txtCreator").value=item.Creator;
                        document.getElementById('txtCreateDate').value=item.CreateDate.substring(0,10);
                        document.getElementById('ddlBillStatus').value=item.BillStatus;
                        document.getElementById('tbBillStatus').value=item.BillStatusName;
                        document.getElementById("txtRemark").value=item.Remark;                
                        document.getElementById("tbConfirmor").value=item.ConfirmorName;
                        document.getElementById("txtConfirmor").value=item.ConfirmorID;
                        document.getElementById("txtConfirmDate").value=item.ConfirmDate.substring(0,10);//
                        document.getElementById("txtCloser").value=item.Closer;//
                        document.getElementById("tbCloser").value=item.CloserName;//
                        document.getElementById('CustBigType').value=item.CustBigTypeName;
                        document.getElementById('hiddenCustBigType').value=item.CustBigTypeID;
                        document.getElementById("txtCloseDate").value=item.CloseDate.substring(0,10);//
                        document.getElementById("txtModifiedUserID").value=item.ModifiedUserID;//
                        document.getElementById("tbModifiedUserID").value=item.ModifiedUserID;
                        document.getElementById('hiddeniqualityd').value=item.ID;
                        
                        document.getElementById('hfPageAttachment').value=item.Attachment.replace(/,/g,"\\");
                        var testurl=item.Attachment.replace(/,/g,"\\");   
                        var testurl2=testurl.lastIndexOf('\\');
                        testurl2=testurl.substring(testurl2+1,testurl.length    );
                        document.getElementById('attachname').innerHTML=testurl2;
                        
                        document.getElementById('txtModifiedDate').value=item.ModifiedDate.substring(0,10);
                         var theddlFromType=document.getElementById('ddlFromType').value;
                         
                         
                        if (document.getElementById('hfPageAttachment').value != "")
                        {          
                           //下载删除显示
                           document.getElementById("divDealAttachment").style.display = "";
                           //上传不显示
                           document.getElementById("divUploadAttachment").style.display = "none";
                        }
                        // 填充扩展属性
                        GetExtAttr("officedba.QualityCheckApplay", item);
                        var BillStatus=document.getElementById('ddlBillStatus').value;
                        if(BillStatus=='2')
                        {                            
                            document.getElementById('divConfirmor').style.display='';
                            document.getElementById('divConfirmDate').style.display='';
                        }
                        if(BillStatus=='4')
                        {
                            document.getElementById('divConfirmor').style.display='';
                            document.getElementById('divConfirmDate').style.display='';
                            document.getElementById('divCloseDate').style.display='';
                            document.getElementById('divCloser').style.display='';
                        }         
                     try
                     {
                          if(theddlFromType=='0')
                          {
                              document.getElementById('btn_select').style.display='none';
                              document.getElementById('unbtn_select').style.display='';
                          }
                          else
                          {
                                   document.getElementById('btn_select').style.display='';
                                   document.getElementById('unbtn_select').style.display='none';
                          }
                            GetFlowButton_DisplayControl();
                     }
                     catch(e)
                     {}
                  
                   }     
               });
             }
             if(typeof(msg.data)!='undefined')
             {
               $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                      $("#ddlFromType").val(item.FromType);
                      if(item.FromType=='1')  //采购类型
                      {  
                           AddPurNewRow1(item.ID,item.ProductName,item.ProdNo,item.CodeName,item.FromBillID
                                    ,item.FromLineNo,item.CheckedCount,item.Remark,item.UnitID,item.ProID
                                    ,item.ProductCount,item.ApplyCheckCount,item.FromBillNo,item.ProductCount2
                                    ,item.UsedUnitID,item.UsedUnitCount);               
                      }                  
                      else if(item.FromType=='2')   //生产任务类型                   
                      {
                           FillManQua1(item.InCount,item.CheckedCount,item.ProductName,item.ProdNo,item.CodeName,item.FromBillID,item.FromLineNo,item.ApplyCheckCount,item.Principal,item.EmployeeName,item.DeptID,item.DeptName,item.UnitID,item.ProductID,'noUC',item.ProductCount,item.FromBillNo,item.Remark,item.UsedUnitName,item.UsedUnitID,item.UsedUnitCount);
                      }
                      else if(item.FromType=='0')  //无来源时  
                      {
                            $("#QuaFromLineNo").css("display","none");
                            $("#QuaFromBillNo").css("display","none");
                            $("#QuaCheckedCount").css("display","none");
                            $("#QuaInCount").css("display","none");
                            var iRow = AddOneRow();
                            $("#ProName"+iRow).val(item.ProductName);
                            $("#hidProductName"+iRow).val(item.ProductID);
                            $("#ProductNo"+iRow).val(item.ProdNo);
                            if(isMoreUnit)
                            {// 启用多计量单位
                                $("#UsedUnitName"+iRow).val(item.CodeName);
                                $("#UsedUnitID"+iRow).val(item.UnitID);
                                $("#UsedUnitCount"+iRow).val(FormatAfterDotNumber(item.ProductCount,selPoint));
                                
                                $("#CheckedCounted_"+iRow).val(FormatAfterDotNumber(item.UsedUnitCount,selPoint));
                                $("#ProductCount"+iRow).val(FormatAfterDotNumber(item.CheckedCount,selPoint));
                                $("#RealCheckedCount"+iRow).val(FormatAfterDotNumber(item.RealCheckedCount,selPoint));
                                GetUnitGroupSelect(item.ProductID, 'MakeUnit', 'UnitID'+iRow, 'ChangeUnit(this,'+iRow+')', 'tdUnitName'+iRow,item.UsedUnitID);
                                $("#CheckedCounted_"+iRow).attr("onblur",'ChangeUnit(this,'+iRow+')'); 
                            }
                            else
                            {
                                $("#UnitName"+iRow).val(item.CodeName);
                                $("#UnitID"+iRow).val(item.UnitID);
                                $("#ProductCount"+iRow).val(FormatAfterDotNumber(item.CheckedCount,selPoint));
                                $("#CheckedCounted_"+iRow).val(FormatAfterDotNumber(item.ProductCount,selPoint));
                                $("#RealCheckedCount"+iRow).val(FormatAfterDotNumber(item.RealCheckedCount,selPoint));
                            }
                            $("#Remark"+iRow).val(item.Remark);
                      }
                    }
               });
               GetFlowButton_DisplayControl();
             }
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}

/***************************************************
*切换单位
***************************************************/
function ChangeUnit(own, rowid) 
{
    CalCulateNum('UnitID' + rowid, "CheckedCounted_" + rowid, "UsedUnitCount" + rowid, "", "", selPoint);
    GetAllCheckedCount();
}

//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data)
{
    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
    //有扩展属性才取值
    if (strKey != '')
    {
        var arrKey = strKey.split('|');
        for (var t = 0; t < arrKey.length; t++)
        {
            //不为空的字段名才取值
            if ($.trim(arrKey[t]) != '')
            {
                $("#" + $.trim(arrKey[t])).val(data[$.trim(arrKey[t])]);
            }
        }
    }
}


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
                myValue= parseFloat(a.toString()).toFixed(2);
            }       
            
        }
        return myValue;
    }
}
// 物品选择填充
function Fun_FillParent_Content(id,prodNo,prodName,standardSell,unitID,codeName,taxRate,sellTax
                                ,discount,specification,codeTypeName,typeID,StandardCost,GroupUnitNo
                                ,SaleUnitID,SaleUnitName,InUnitID,InUnitName,StockUnitID,StockUnitName
                                ,MakeUnitID,MakeUnitName,IsBatchNo)
{
    if(popTechObj.InputObj!=null)
    {   
        document.getElementById('ProName'+popTechObj.InputObj).value=prodName;
        document.getElementById('hidProductName'+popTechObj.InputObj).value=id;
        document.getElementById('ProductNo'+popTechObj.InputObj).value=prodNo;
        if(isMoreUnit)
        {// 启用多计量单位
            document.getElementById('UsedUnitName'+popTechObj.InputObj).value=codeName;
            document.getElementById('UsedUnitID'+popTechObj.InputObj).value=unitID;
            GetUnitGroupSelect(id, 'MakeUnit', 'UnitID'+popTechObj.InputObj, 'ChangeUnit(this,'+popTechObj.InputObj+')', 'tdUnitName'+popTechObj.InputObj,'');
            $("#CheckedCounted_"+popTechObj.InputObj).attr("onblur",'ChangeUnit(this,'+popTechObj.InputObj+')');               
        }
        else
        {
            document.getElementById('UnitName'+popTechObj.InputObj).value=codeName;
            document.getElementById('UnitID'+popTechObj.InputObj).value=unitID;
        }
    }
}

// 添加空白行
function AddOneRow()
{
    // 结构代码
    var rowHtml="";              
    // 行号
    var rowID = parseInt($("#txtTRLastIndex").val());
    // 选择
    rowHtml+="<td class='cell'><input style='text-align:center' name='chk' id='chk_Option"+rowID+"' value='"+rowID+"' type='checkbox'/></td>";
    
    // 物品名称
    rowHtml+="<td class='cell'><input type='text' id='ProName"+rowID+"' readOnly onclick='popTechObj.ShowList("+rowID+");' class='tdinput' style='width:80%'  /><input id='hidProductName"+rowID+"' value='0' type='hidden' /></td>";

    // 物品编号
    rowHtml+="<td class='cell'><input id='ProductNo"+rowID+"'type='text' class='tdinput'  style='width:80%' /></td>";
    
     if(isMoreUnit)
    {// 启用多计量单位
        // 基本单位
        rowHtml+="<td class='cell'><input type='text' disabled size='3' id='UsedUnitName"+rowID+"' readOnly  class='tdinput'   /><input id='UsedUnitID"+rowID+"' type='hidden'  ></td>";
    
        // 基本数量
        rowHtml+="<td class='cell'><input type='text' size='8' maxlength='10' onchange='Number_round(this,"+selPoint+");' id='UsedUnitCount"+rowID+"' class='tdinput' readOnly /></td>";
        
        // 单位
        rowHtml+="<td class='cell' id='tdUnitName"+rowID+"'></td>";
    }
    else
    {
        // 单位
        rowHtml+="<td class='cell'><input type='text' id='UnitName"+rowID+"' class='tdinput'  style='width:80%'  /><input id='UnitID"+rowID+"' type='hidden' /></td>";
    }
    // 报检数量
    rowHtml+="<td class='cell'><input type='text' maxlength='10' onchange='Number_round(this,"+selPoint+");' id='CheckedCounted_"+rowID+"' onblur='GetAllCheckedCount();'  class='tdinput' style='width:80%'  /></td>";
    // 生产数量
    if($("#QuaInCount").css("display")!="none")
    {
        rowHtml+="<td class='cell'><input type='text' size='8' disabled onchange='Number_round(this,"+selPoint+");' readOnly id='InCount"+rowID+"' class='tdinput' /></td>";
    }
    // 已报检数量
    rowHtml+="<td class='cell'><input type='text' disabled onchange='Number_round(this,"+selPoint+");' id='ProductCount"+rowID+"' value='"+FormatAfterDotNumber(0,selPoint)+"'   class='tdinput' style='width:80%'  /></td>";
    // 已检数量
    if($("#PurCheckedCount").css("display")!="none")
    {
        rowHtml+="<td class='cell'><input type='text' value='"+FormatAfterDotNumber(0,selPoint)+"' disabled id='RealCheckedCount"+rowID+"'   class='tdinput' style='width:80%'  /></td>";
    }
    // 到货数量
    if($("#QuaCheckedCount").css("display")!="none")
    {
        rowHtml+="<td class='cell'><input type='text' size='8' onchange='Number_round(this,"+selPoint+");' id='ProductCount1"+rowID+"'   class='tdinput'  disabled /></td>";
    }
    // 源单编号
    if($("#QuaFromBillNo").css("display")!="none")
    {
        rowHtml+="<td class='cell'><input id='FromBillID"+rowID+"'  type='hidden'/><input size='20' disabled type='text' id='FromBillNo"+rowID+"'   class='tdinput'  style='width:90%' readOnly /></td>";
    }
    // 源单行号
    if($("#QuaFromLineNo").css("display")!="none")
    {
        rowHtml+="<td class='cell'><input type='text' id='FromLineNo"+rowID+"'  disabled  class='tdinput'  size='2' readOnly/></td>";
    }
    // 备注
    rowHtml+="<td class='cell'><input id='Remark"+rowID+"' type='text' class='tdinput'  style='width:80%'  /></td>";
    
    $("<tr class='newrow'></tr>").append(rowHtml).appendTo($("#dg_Log tbody"));
    $("#txtTRLastIndex").val(rowID + 1);//将行号推进下一行
    
    return rowID;
}

function AddOneNewRowNO()                        //源单类型   无来源
{
    if(document.getElementById('isflow').value>0)
    {     
        return false;  
    }
    if(document.getElementById('isupdate').value>0)
    {         
        return false;  
    }

    if($("#ddlFromType").val()=='0')
    { 
        $("#QuaFromLineNo").css("display","none");
        $("#QuaFromBillNo").css("display","none");
        $("#QuaCheckedCount").css("display","none");
        $("#QuaInCount").css("display","none");
        AddOneRow();
    }
}
function ChangeForDetail() //页面切换源单类型时 加载明细需要
{
    var WhatType=document.getElementById('ddlFromType').value;
    if(WhatType=='1') //源单类型为 采购到货通知单
    {
        QualityPurObj.ShowPurList(isMoreUnit);
        document.getElementById('txtPeople').disabled=true;
        document.getElementById('txtDep').disabled=true;
    }
    else if(WhatType=='2')  //源单类型为 生产任务单
    {
        popQualityManObj.ShowProList(isMoreUnit);
    }
}
function FillManQua1(InCount,CheckedCount,ProductName,ProdNo,CodeName,FromBillID,FromLineNo
                    ,ApplyCheckCount,Principal,EmployeeName,DeptID,DeptName,UnitID,ProductID
                    ,uc,ProductCount,FromBillNo,Remark,UsedUnitName,UsedUnitID,UsedUnitCount)  //源单类型是  生产任务单   添加明细
{
    $("#QuaFromLineNo").css("display","");
    $("#QuaFromBillNo").css("display","");
    $("#QuaCheckedCount").css("display","none");
    $("#QuaInCount").css("display","");
    var rowID=AddOneRow();
        
    var txtPeople=document.getElementById('UserPrincipal');  //获取任务负责人
    var txtPeopleID=document.getElementById('txtPeople');
    
    var txtDepID=document.getElementById('txtDep');   //获取负责人部门
    var txtDepName=document.getElementById('DeptName');
    if(uc=='uc')
    {
        txtDepID.value=DeptID;
        txtDepName.value=DeptName;
        txtPeople.value=EmployeeName;
        txtPeopleID.value=Principal;
    }
    txtPeople.readOnly=true;
    txtDepName.readOnly=true;

    document.getElementById('txtCustNameNo').disabled=true;  //供应商不可选
    document.getElementById('CustBigType').disabled=true;     //供应商大类不可选
                       
    //添加列:物品名称
    $("#ProName"+rowID).val(ProductName);
    $("#hidProductName"+rowID).val(ProductID);

    //添加列:物品编号
    $("#ProductNo"+rowID).val(ProdNo);

    if(isMoreUnit)
    {// 启用多计量单位
        //添加列:基本单位
        $("#UsedUnitName"+rowID).val(CodeName);
        $("#UsedUnitID"+rowID).val(UnitID);
        GetUnitGroupSelect(ProductID, 'MakeUnit', 'UnitID'+rowID,  'ChangeUnit(this,'+rowID+')', 'tdUnitName'+rowID,UsedUnitID);
        $("#CheckedCounted_"+rowID).attr("onblur",'ChangeUnit(this,'+rowID+')'); 
        //添加列:基本数量
        $("#UsedUnitCount"+rowID).val(FormatAfterDotNumber(ProductCount,selPoint));
        //添加列:报检数量
        $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(UsedUnitCount,selPoint));
    }
    else
    {
        //添加列:单位
        $("#UnitName"+rowID).val(CodeName);
        $("#UnitID"+rowID).val(UnitID);
        //添加列:报检数量
        $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(ProductCount,selPoint));
    }

    //添加列:入库数量
    $("#InCount"+rowID).val(FormatAfterDotNumber(InCount,selPoint));

    //添加列:已报检数量
    $("#ProductCount"+rowID).val(FormatAfterDotNumber(ApplyCheckCount,selPoint));

    //添加列:已检数量
    $("#RealCheckedCount"+rowID).val(FormatAfterDotNumber(CheckedCount,selPoint));

    //添加列:源单编号
    $("#FromBillID"+rowID).val(FromBillID);
    $("#FromBillNo"+rowID).val(FromBillNo);

    //添加列:源担行号
    $("#FromLineNo"+rowID).val(FromLineNo);

    //添加列:备注
    $("#Remark"+rowID).val(Remark);

    document.getElementById('divManInfo').style.display='none';
       
}
function FillManQua(InCount,CheckedCount,ProductName,ProdNo,CodeName,FromBillID,FromLineNo
                    ,ApplyCheckCount,Principal,EmployeeName,DeptID,DeptName,UnitID,ProductID
                    ,uc,thevalue,FromBillNo,ManCheckCount,UsedUnitID,UsedUnitCount)  //源单类型是  生产任务单   添加明细
{
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document);                   
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        if(signFrame.rows[i+1].style.display!="none")    
        {
            var myFromBillID=document.getElementById('FromBillID'+(i+1)); 
            if(myFromBillID.value==FromBillID)
            {
                document.getElementById('divManInfo').style.display='none';
                popMsgObj.ShowMsg('该明细已选择，请选择其它的明细！');  
                closeRotoscopingDiv(false,'divBackShadow');               
                return false;
            }
        }            
    }
    var myTotalCount=document.getElementById('CountTotal1');
    myTotalCount.value= FormatAfterDotNumber(parseFloat(myTotalCount.value)+parseFloat(ManCheckCount),selPoint);

    $("#QuaFromLineNo").css("display","");
    $("#QuaFromBillNo").css("display","");
    $("#QuaCheckedCount").css("display","none");
    $("#QuaInCount").css("display","");
    var rowID=AddOneRow();


    var txtPeople=document.getElementById('UserPrincipal');  //获取任务负责人
    var txtPeopleID=document.getElementById('txtPeople');

    var txtDepID=document.getElementById('txtDep');   //获取负责人部门
    var txtDepName=document.getElementById('DeptName');
    if(uc=='uc')
    {
        txtDepID.value=DeptID;
        txtDepName.value=DeptName;
        txtPeople.value=EmployeeName;
        txtPeopleID.value=Principal;
    }
    txtPeople.readOnly=true;
    txtDepName.readOnly=true;

    document.getElementById('txtCustNameNo').disabled=true;  //供应商不可选
    document.getElementById('CustBigType').disabled=true;     //供应商大类不可选
       
        //添加列:物品名称
    $("#ProName"+rowID).val(ProductName);
    $("#hidProductName"+rowID).val(ProductID);

    //添加列:物品编号
    $("#ProductNo"+rowID).val(ProdNo);

    if(isMoreUnit)
    {// 启用多计量单位
        //添加列:基本单位
        $("#UsedUnitName"+rowID).val(CodeName);
        $("#UsedUnitID"+rowID).val(UnitID);
        GetUnitGroupSelect(ProductID, 'MakeUnit', 'UnitID'+rowID,  'ChangeUnit(this,'+rowID+')', 'tdUnitName'+rowID,UsedUnitID);
        $("#CheckedCounted_"+rowID).attr("onblur",'ChangeUnit(this,'+rowID+')'); 
        //添加列:基本数量
        $("#UsedUnitCount"+rowID).val(FormatAfterDotNumber(thevalue,selPoint));
        //添加列:报检数量
        $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(UsedUnitCount,selPoint));
    }
    else
    {
        //添加列:单位
        $("#UnitName"+rowID).val(CodeName);
        $("#UnitID"+rowID).val(UnitID);
        
        //添加列:报检数量
        $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(thevalue,selPoint));
    }
    
    //添加列:入库数量
    $("#InCount"+rowID).val(FormatAfterDotNumber(InCount,selPoint));

    //添加列:已报检数量
    $("#ProductCount"+rowID).val(FormatAfterDotNumber(ApplyCheckCount,selPoint));

    //添加列:已检数量
    $("#CheckedCount1"+rowID).val(FormatAfterDotNumber(CheckedCount,selPoint));

    //添加列:源单编号
    $("#FromBillID"+rowID).val(FromBillID);
    $("#FromBillNo"+rowID).val(FromBillNo);

    //添加列:源担行号
    $("#FromLineNo"+rowID).val(FromLineNo);
 
    document.getElementById('divManInfo').style.display='none'; 
    closeRotoscopingDiv(false,'divBackShadow');
}

//全选
function SelectAll()
{
    var signFrame = findObj("dg_Log",document);        
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
    if(document.getElementById("checkall").checked)
    {
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objRadio = 'chk_Option_'+(i+1);
                document.getElementById(objRadio.toString()).checked = true;
            }
        }
    }
    else
    {
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objRadio = 'chk_Option_'+(i+1);
                document.getElementById(objRadio.toString()).checked = false;
            }
        }
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
    txtTRLastIndex.value = "1";
}

//删除行
function DeleteSignRow()
{
      
    var ck = document.getElementsByName("chk");
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 

    for(var i=1;i<txtTRLastIndex;i++)
    {
        if(signFrame.rows[i].style.display!="none")
        {
            var objRadio = 'chk_Option'+(i);
  
            if(document.getElementById(objRadio.toString()).checked)
            {
                signFrame.rows[i].style.display = 'none';
            }
        }
    }
    GetAllCheckedCount();
}

function cbSelectAll()
{
    var chk=document.getElementsByName('chk');
    for(var i=0;i<chk.length;i++)
    {
        if(chk[i].checked)
        {
            chk[i].checked=false;
            
        }
        else
        {
             chk[i].checked=true;
        }
    }
}
function setCountTotal(a,b)
{
    var InCount=document.getElementById(b).value;
    var CheckCount=document.getElementById(a).value;
    var CountTotal=document.getElementById('CountTotal1');
    var CheckBox=document.getElementsByName('chkList');
        if(IsNumberOrNumeric(CheckCount,12,selPoint)==false)
        {              
            popMsgObj.ShowMsg('报检数量包含非法字符或为负！');
            return false;
        } 
        if(parseFloat(CheckCount)> parseFloat(InCount))
        {             
             var mesInfo='报检数量不能大于点收数量:'+InCount;
             popMsgObj.ShowMsg(mesInfo);  
             return;           
        }
        else
        {
             var myValue=0;             
             if( CountTotal.value!='')
             {
               for(var i=0;i<CheckBox.length;i++)
               {                
                 if(document.getElementById('SignItem_Row_'+CheckBox[i].value).style.display!='none')
                 {                   
                   var myCheckCount=document.getElementById('CheckCount'+CheckBox[i].value);                 
                   myValue= myValue+ parseFloat(myCheckCount.value);
                 }
               }
               CountTotal.value=FormatAfterDotNumber(myValue,selPoint);
             }
             else
             {                
                CountTotal.value=FormatAfterDotNumber(CheckCount,selPoint);
             }           
             
        }
}
 //源单类型   采购到货通知单
function AddPurNewRow(ID,ProductName,ProductNo,CodeName,QuaCheckedCount
            ,FromBillID,FromLineNo,CheckedCount,UnitID,ProID,ProductCount
            ,ApplyCheckCount,FromBillNo,ProductID,UsedUnitID,UsedUnitCount)
{ 
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document);                   
    for(var i=0;i<txtTRLastIndex-1;i++)
     {
       if(signFrame.rows[i+1].style.display!="none")    
        {
          var myFromBillID=document.getElementById('FromBillID'+(i+1)); 
         
          if(myFromBillID.value==FromBillID)
          {
             document.getElementById('divPurInfo').style.display='none';
             popMsgObj.ShowMsg('该明细已选择，请选择其它的明细！');                 
             return false;
          }
        }            
    }
    $("#QuaFromLineNo").css("display","");
    $("#QuaFromBillNo").css("display","");
    $("#QuaCheckedCount").css("display","");
    $("#QuaInCount").css("display","none");
    var rowID=AddOneRow();
    var txtPeople=document.getElementById('txtPeople');  //获取任务负责人
    var txtDep=document.getElementById('txtDep');   //获取负责人部门
    //添加列:物品名称
    $("#ProName"+rowID).val(ProductName);
    $("#hidProductName"+rowID).val(ProID);

    //添加列:物品编号
    $("#ProductNo"+rowID).val(ProductNo);

    if(isMoreUnit)
    {// 启用多计量单位
        //添加列:基本单位
        $("#UsedUnitName"+rowID).val(CodeName);
        $("#UsedUnitID"+rowID).val(UnitID);
        
        GetUnitGroupSelect(ProductID, 'MakeUnit', 'UnitID'+rowID,  'ChangeUnit(this,'+rowID+')', 'tdUnitName'+rowID,UsedUnitID);
        $("#CheckedCounted_"+rowID).attr("onblur",'ChangeUnit(this,'+rowID+')'); 
        //添加列:基本数量
        $("#UsedUnitCount"+rowID).val(FormatAfterDotNumber(ProductCount,selPoint));
        
        //添加列:报检数量
        $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(UsedUnitCount,selPoint));
    }
    else
    {
        //添加列:单位
        $("#UnitName"+rowID).val(CodeName);
        $("#UnitID"+rowID).val(UnitID);
        //添加列:报检数量
        $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(QuaCheckedCount,selPoint));

    }

    //添加列:已报检数量
    $("#ProductCount"+rowID).val(FormatAfterDotNumber(ApplyCheckCount,selPoint));

    //添加列:已检数量
    $("#RealCheckedCount"+rowID).val(FormatAfterDotNumber(CheckedCount,selPoint));
    
    //添加列:到货数量
    $("#ProductCount1"+rowID).val(FormatAfterDotNumber(ProductCount,selPoint));

    //添加列:源单编号
    $("#FromBillID"+rowID).val(FromBillID);
    $("#FromBillNo"+rowID).val(FromBillNo);

    //添加列:源担行号
    $("#FromLineNo"+rowID).val(FromLineNo);

}
function AddPurNewRow1(ID,ProductName,ProductNo,CodeName,FromBillID,FromLineNo
        ,CheckedCount,Remark,UnitID,ProID,ProductCount,ApplyCheckCount,FromBillNo,ProductCount2
        ,UsedUnitID,UsedUnitCount)
{ 
    $("#QuaFromLineNo").css("display","");
    $("#QuaFromBillNo").css("display","");
    $("#QuaCheckedCount").css("display","");
    $("#QuaInCount").css("display","none");
    var rowID=AddOneRow();
    var txtPeople=document.getElementById('txtPeople');  //获取任务负责人
    var txtDep=document.getElementById('txtDep');   //获取负责人部门
    //添加列:物品名称
    $("#ProName"+rowID).val(ProductName);
    $("#hidProductName"+rowID).val(ProID);

    //添加列:物品编号
    $("#ProductNo"+rowID).val(ProductNo);

    if(isMoreUnit)
    {// 启用多计量单位
        //添加列:基本单位
        $("#UsedUnitName"+rowID).val(CodeName);
        $("#UsedUnitID"+rowID).val(UnitID);
        
        GetUnitGroupSelect(ProID, 'MakeUnit', 'UnitID'+rowID,  'ChangeUnit(this,'+rowID+')', 'tdUnitName'+rowID,UsedUnitID);
        $("#CheckedCounted_"+rowID).attr("onblur",'ChangeUnit(this,'+rowID+')'); 
        //添加列:基本数量
        $("#UsedUnitCount"+rowID).val(FormatAfterDotNumber(ProductCount,selPoint));
        
        //添加列:报检数量
        $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(UsedUnitCount,selPoint));

    }
    else
    {
        //添加列:单位
        $("#UnitName"+rowID).val(CodeName);
        $("#UnitID"+rowID).val(UnitID);
        
        //添加列:报检数量
        $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(ProductCount,selPoint) );
    }

    //添加列:已报检数量
    $("#ProductCount"+rowID).val(FormatAfterDotNumber(ApplyCheckCount,selPoint));

    //添加列:已检数量
    $("#RealCheckedCount"+rowID).val(FormatAfterDotNumber(CheckedCount,selPoint));
    
    //添加列:到货数量
    $("#ProductCount1"+rowID).val(FormatAfterDotNumber(ProductCount2,selPoint));

    //添加列:源单编号
    $("#FromBillID"+rowID).val(FromBillID);
    $("#FromBillNo"+rowID).val(FromBillNo);

    //添加列:源担行号
    $("#FromLineNo"+rowID).val(FromLineNo);

    //添加列:备注
    $("#Remark"+rowID).val(Remark);
}
//确认 
function Fun_ConfirmOperate()
{
    if(!window.confirm('确认要进行确认操作吗?'))
    {
        return false;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
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
        return false;
    }
    
    
    var ID=document.getElementById("hiddeniqualityd").value;
    var FromType=document.getElementById('ddlFromType').value;
        var ApplyNO=document.getElementById('lbInNo').value;
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    var DetailFroBillID=new Array();   //
    var DetailCheckedCount=new Array(); //数量
    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        { 
             if(FromType!="0")
             {  
                var theFromBillID=document.getElementById('FromBillID'+(i)).value; //             
             }
             var theCheckedCount=document.getElementById('CheckedCounted_'+(i)).value;   //
    
              DetailCheckedCount.push(theCheckedCount);              
              DetailFroBillID.push(theFromBillID);      
        }        
    } 

    var UrlParam = "action=Confirm&DetailFroBillID="+DetailFroBillID+"&ApplyNO="+ApplyNO+"&DetailCheckedCount="+DetailCheckedCount+"&ddlFromType="+FromType+"&ID="+ID;
    $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageQualityCheckAdd.ashx",
                  dataType:'json',//返回json格式数据
                  data:UrlParam,
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
                        if(reInfo[0]=='none')
                        {
                            return false;
                        }
                        var FromType=document.getElementById('ddlFromType').value;
                        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
                        var signFrame = findObj("dg_Log",document); 
                        if(FromType=="0")
                        {
                            for(var i=0;i<txtTRLastIndex-1;i++)
                            {
                                if(signFrame.rows[i+1].style.display!="none")    
                                {
                                        var objCheckedCount= 'CheckedCounted_'+(i+1);         //报检数量
                                        var objProductCount='ProductCount'+(i+1);
                                        document.getElementById(objProductCount).value=FormatAfterDotNumber((parseFloat(document.getElementById(objProductCount).value)+parseFloat(document.getElementById(objCheckedCount).value)),selPoint);                                        
                                }
                            }
                        }
                        if(FromType=="1")
                        {
                            for(var i=0;i<txtTRLastIndex-1;i++)
                            {
                                if(signFrame.rows[i+1].style.display!="none")    
                                {
                                        var objCheckedCount=document.getElementById('CheckedCounted_'+(i+1));         //报检数量
                                        var objProductCount=document.getElementById('ProductCount'+(i+1));
                                        objProductCount.value=FormatAfterDotNumber(parseFloat(objProductCount.value)+parseFloat(objCheckedCount.value),selPoint);  
                                }
                            }
                        }
                        if(FromType=="2")
                        {
                            for(var i=0;i<txtTRLastIndex-1;i++)
                            {
                                if(signFrame.rows[i+1].style.display!="none")    
                                {
                                        var objCheckedCount=document.getElementById('CheckedCounted_'+(i+1));         //报检数量
                                        var objProductCount=document.getElementById('ProductCount'+(i+1));
                                        objProductCount.value=FormatAfterDotNumber(parseFloat(objProductCount.value)+parseFloat(objCheckedCount.value),selPoint);  
                                }
                            }
                        }

                        document.getElementById('btnSave').src='../../../images/Button/UnClick_bc.jpg';
                        document.getElementById('isflow').value='1';
                        document.getElementById('isupdate').value='1'; 
                        
                        document.getElementById('ddlBillStatus').value="2";
                        document.getElementById('tbBillStatus').value='执行';
                        document.getElementById('tbConfirmor').value=reInfo[0];
                        document.getElementById('txtConfirmDate').value=reInfo[1];
                        document.getElementById('divConfirmor').style.display='';
                        document.getElementById('divConfirmDate').style.display='';
                        if(reInfo[2]>0 || reInfo[3]>0)
                        {
                           document.getElementById('btnSave').src='../../../images/Button/UnClick_bc.jpg';                        
                           
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
function Fun_UnConfirmOperate()  //取消确认
{
    if(!confirm('确认要进行取消确认操作吗？'))
    {
        return false;
    }
    var ID =document.getElementById('hiddeniqualityd').value
    var FromType=document.getElementById('ddlFromType').value;
    var ApplyNO=document.getElementById('lbInNo').value;
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    var DetailFroBillID=new Array();   //
    var DetailCheckedCount=new Array(); //数量
    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        { 
             if(FromType!="0")
             {  
                var theFromBillID=document.getElementById('FromBillID'+(i)).value; //             
             }
             var theCheckedCount=document.getElementById('CheckedCounted_'+(i)).value;   //
    
              DetailCheckedCount.push(theCheckedCount);              
              DetailFroBillID.push(theFromBillID);      
        }        
    }
    var MyData =  "action=UnConfirm&DetailFroBillID="+DetailFroBillID+"&ApplyNO="+ApplyNO+"&DetailCheckedCount="+DetailCheckedCount+"&ddlFromType="+FromType+"&ID="+ID;  
                       
    $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageQualityCheckAdd.ashx",
                  dataType:'json',//返回json格式数据
                   data:MyData,
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
                        if(reInfo[0]!='no')
                        {
                            document.getElementById('ddlBillStatus').value="1";
                            document.getElementById('tbBillStatus').value='制单';
                            document.getElementById('isupdate').value='0';
                            document.getElementById('isflow').value='0';
                            document.getElementById('btnSave').src='../../../images/Button/Bottom_btn_save.jpg';
                            GetFlowButton_DisplayControl();
                            document.getElementById('divConfirmor').style.display='none';
                            document.getElementById('divConfirmDate').style.display='none';                       
                            document.getElementById('tbModifiedUserID').value=reInfo[0]; //最后更新人
                            document.getElementById('txtModifiedDate').value=reInfo[1];//最后更新时间  
                            
                         
                            var FromType=document.getElementById('ddlFromType').value;
                        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
                        var signFrame = findObj("dg_Log",document); 
                        if(FromType=="0")
                        {
                            for(var i=0;i<txtTRLastIndex-1;i++)
                            {
                                if(signFrame.rows[i+1].style.display!="none")    
                                {
                                        var objCheckedCount= 'CheckedCounted_'+(i+1);         //报检数量
                                        var objProductCount='ProductCount'+(i+1);
                                        document.getElementById(objProductCount).value=FormatAfterDotNumber(parseFloat(document.getElementById(objProductCount).value)-parseFloat(document.getElementById(objCheckedCount).value),selPoint);                                        
                                      
                                }
                            }
                        }
                        if(FromType=="1")
                        {
                            for(var i=0;i<txtTRLastIndex-1;i++)
                            {
                                if(signFrame.rows[i+1].style.display!="none")    
                                {
                                        var objCheckedCount=document.getElementById('CheckedCounted_'+(i+1));         //报检数量
                                        var objProductCount=document.getElementById('ProductCount'+(i+1));
                                        objProductCount.value=FormatAfterDotNumber(parseFloat(objProductCount.value)-parseFloat(objCheckedCount.value),selPoint);  
                                }
                            }
                        }
                        if(FromType=="2")
                        {
                            for(var i=0;i<txtTRLastIndex-1;i++)
                            {
                                if(signFrame.rows[i+1].style.display!="none")    
                                {
                                        var objCheckedCount=document.getElementById('CheckedCounted_'+(i+1));         //报检数量
                                        var objProductCount=document.getElementById('ProductCount'+(i+1));
                                        objProductCount.value=FormatAfterDotNumber(parseFloat(objProductCount.value)-parseFloat(objCheckedCount.value),selPoint);  
                                }
                            }
                        }                    
                        }
                      
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
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
    var ID =document.getElementById('hiddeniqualityd').value
    var UrlParam = "action=Close&myMethod="+myMethod+"&ID="+ID.toString();
                       
    $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageQualityCheckAdd.ashx",
                  dataType:'json',//返回json格式数据
                  data:UrlParam,
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
                        document.getElementById('btnSave').src='../../../images/Button/UnClick_bc.jpg';
                        document.getElementById('isflow').value='1';
                        document.getElementById('isupdate').value='1';
                        popMsgObj.ShowMsg(data.info);
                        if(isComplete)
                        {
                            document.getElementById('ddlBillStatus').value="4";
                            document.getElementById('tbBillStatus').value='手工结单';
                            document.getElementById('tbCloser').value=reInfo[0];
                            document.getElementById('txtCloseDate').value=reInfo[1];
                            document.getElementById('divCloser').style.display='';
                            document.getElementById('divCloseDate').style.display='';
                        }
                        else
                        {
                            document.getElementById('ddlBillStatus').value="2";
                            document.getElementById('tbBillStatus').value='执行';
                            document.getElementById('tbConfirmor').value=reInfo[0];
                            document.getElementById('txtConfirmDate').value=reInfo[1];
                            document.getElementById('divCloser').style.display='none';
                            document.getElementById('divCloseDate').style.display='none';
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
        // 删除成功
        $("#hfPageAttachment").val("");
        // 更新附件字段
        UpDateAttachment();
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
        testurl=url.substring(testurl+1,url.length);
        document.getElementById('attachname').innerHTML=testurl;
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "";
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
        // 更新附件字段
        UpDateAttachment();
    }
}

// 更新附件字段
function UpDateAttachment()
{
    var Action="";
    var txtIdentityID = $("#hiddeniqualityd").val();                      /*自增长ID*/
    if(txtIdentityID!="" && txtIdentityID!="undefined" && txtIdentityID!=null )
    {
        Action="updateattachment";
    }
    else
    {
        return;
    }
    
    var UrlParam = '';
    var Attachment = $("#hfPageAttachment").val().replace(/\\/g,"\\\\");//附件   
    
    UrlParam ="action="+Action+
              "&Attachment="+escape(Attachment)+
              "&ID="+escape(txtIdentityID);
    $.ajax({ 
              type: "POST",
              dataType:'json',//返回json格式数据
              data:UrlParam,
              url: "../../../Handler/Office/StorageManager/StorageQualityCheckAdd.ashx",
              cache:false,
              beforeSend:function(){ }, 
              error: function(){}, 
              success:function(data){} 
           });
        
}



function SetSaveButton_DisplayControl(flowStatus)
{

    //流程状态：0：待提交   1：待审批   2：审批中   3：审批通过     4：审批不通过   5：撤销审批
    //制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    //制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    //变更和手工结单的不可以修改
    var PageBillID = document.getElementById('hiddeniqualityd').value;
    var PageBillStatus = document.getElementById('ddlBillStatus').value;
    var myddlFromType=document.getElementById('ddlFromType').value;

    if(PageBillID>0)
    {

        if(PageBillStatus=='2' || PageBillStatus=='3' || PageBillStatus=='4')
        {
            //单据状态：变更和手工结单状态
            try
            {
                document.getElementById('btnSave').src='../../../images/Button/UnClick_bc.jpg';
                document.getElementById('btnGetGoods').style.display='none';
                document.getElementById('unbtnGetGoods').style.display='';              
                         
            }
            catch(e){}
            
            document.getElementById('isflow').value='1';
            document.getElementById('isupdate').value='1';
        }
        else
        {
            if(PageBillStatus==1 && (flowStatus ==1 || flowStatus==2 || flowStatus ==3))
            {
               
                try
                {
                    document.getElementById('btnSave').src='../../../images/Button/UnClick_bc.jpg';
                   
                    document.getElementById('btnGetGoods').style.display='none';
                      document.getElementById('unbtnGetGoods').style.display='';
                                
                }
                catch(e){}
                document.getElementById('isflow').value='1';
                document.getElementById('isupdate').value='1';
            }
            else
            {
                try
                {
                    document.getElementById('btnSave').src='../../../images/Button/Bottom_btn_save.jpg';
                    if(myddlFromType=='0')
                    {
                      document.getElementById('btn_add').src='../../../images/Button/Show_add.jpg';
                    }
                    document.getElementById('btnGetGoods').style.display='';
                      document.getElementById('unbtnGetGoods').style.display='none';
                }
                catch(e)
                {}
                  
                                    
                document.getElementById('isflow').value='0';
                document.getElementById('isupdate').value='0';
            }
        }
    }

}
//--------------------------------------------------------------------------------------条码扫描需要Start 
function GetGoodsDataByBarCode(ID,ProdNo,ProductName,
                                StandardSell,UnitID,CodeName,
                                TaxRate,SellTax,Discount,
                                Specification,CodeTypeName,TypeID,
                                StandardBuy,TaxBuy,InTaxRate,
                                StandardCost)
{
    AddOneNewRowNOSearch(ID,ProductName,ProdNo,UnitID,CodeName);
}

function AddOneNewRowNOSearch(ProID,ProductName,ProNo,UnitID,UnitName)                        //源单类型   无来源
{
        var myFromBillType=document.getElementById('ddlFromType').value;
        if(myFromBillType=='0')
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
                                 var checkPro=document.getElementById('hidProductName'+(i+1));
                                 if(checkPro.value==ProID)   
                                 {
                                     
                                    if($("#CheckedCounted_"+(i+1)).val()=="")
                                    {
                                        $("#CheckedCounted_"+(i+1)).val(FormatAfterDotNumber(1,selPoint));
                                    }
                                    $("#CheckedCounted_"+(i+1)).val(FormatAfterDotNumber(parseFloat($("#CheckedCounted_"+(i+1)).val())+1,selPoint));
                                    ChangeUnit(this,(i+1));
                                    return ;
                                 }
                          }
                     }                       
            }
            
            $("#QuaFromLineNo").css("display","none");
            $("#QuaFromBillNo").css("display","none");
            $("#QuaCheckedCount").css("display","none");
            $("#QuaInCount").css("display","none");
            
            var rowID=AddOneRow();
            $("#hidProductName"+rowID).val(ProID);// 物品ID
            $("#ProName"+rowID).val(ProductName);// 物品名称
            $("#ProductNo"+rowID).val(ProNo);// 物品名称
            //添加列:报检数量
            $("#CheckedCounted_"+rowID).val(FormatAfterDotNumber(1,selPoint));
            if(isMoreUnit)
            {// 启用多计量单位
                //添加列:基本单位
                $("#UsedUnitName"+rowID).val(UnitName);
                $("#UsedUnitID"+rowID).val(UnitID);
                $("#CheckedCounted_"+rowID).attr("onblur",'ChangeUnit(this,'+rowID+')'); 
                GetUnitGroupSelectEx(ProID, 'MakeUnit', 'UnitID'+rowID,  'ChangeUnit(this,'+rowID+')', 'tdUnitName'+rowID,'','ChangeUnit(this,'+rowID+')');
            }
            else
            {
                //添加列:单位
                $("#UnitName"+rowID).val(UnitName);
                $("#UnitID"+rowID).val(UnitID);
            }
            
            $("#ProductCount"+rowID).val(FormatAfterDotNumber(0,selPoint));// 已报检数量
            $("#RealCheckedCount"+rowID).val(FormatAfterDotNumber(0,selPoint));// 已检数量
            
        }
        else
        {
          
            return false;
        }

}

function SelectCust()
{
    var url="../../../Pages/Office/StorageManager/StorageReportCust.aspx";
    var returnValue = window.showModalDialog(url, "", "dialogWidth=300px;dialogHeight=450px");
    if(returnValue!="" && returnValue!=null&&returnValue!='clear')
    {
        var  value=returnValue.split("|");
        document.getElementById("txtCustNameNo").value=value[1].toString();
        document.getElementById('hiddenCustID').value=value[0].toString();
        document.getElementById('CustBigType').value=value[3].toString();
        document.getElementById('hiddenCustBigType').value=value[2].toString();
    }
    else if(returnValue=='clear')
    {
        document.getElementById("txtCustNameNo").value='';
        document.getElementById('hiddenCustID').value='0';
        document.getElementById('CustBigType').value='';
        document.getElementById('hiddenCustBigType').value='0';
    }
}
function SelectPri()
{
    var ddlFromType=document.getElementById('ddlFromType').value;
    if(ddlFromType=='2')
    {
        document.getElementById('DeptName').readOnly=true;
        document.getElementById('UserPrincipal').readOnly=true;
    }
    if(ddlFromType!='1')
    {
        alertdiv('UserPrincipal,txtPeople');
    }
}
function SelectDept1()
{
    var ddlFromType=document.getElementById('ddlFromType').value;
    if(ddlFromType=='2')
    {
        document.getElementById('DeptName').readOnly=true;
        document.getElementById('UserPrincipal').readOnly=true;
        alertdiv('DeptName,txtDep');
    }
    if(ddlFromType!='1')
    {
        alertdiv('DeptName,txtDep');
    }
}
function ChangeValue()
{
    fnDelRow();
    document.getElementById('CountTotal1').value='0.00';
    var ddlFromType=document.getElementById('ddlFromType').value;
    if(ddlFromType=='0' || ddlFromType=='1')
    {
        document.getElementById('UserPrincipal').value='';
        document.getElementById('txtPeople').value='0';
        document.getElementById('DeptName').value='';
        document.getElementById('txtDep').value='0';
    }
    if(ddlFromType!='1')
    {
        document.getElementById('DeptName').readOnly=true;
        document.getElementById('UserPrincipal').readOnly=true;            
    }
    if(ddlFromType=='2')
    {
        document.getElementById('txtCustNameNo').readOnly=true;
    }
    if(ddlFromType=='0')
    {
        document.getElementById('btn_add').src="../../../images/Button/Show_add.jpg";
        try
        {
            document.getElementById('unbtnGetGoods').style.display='none';
            document.getElementById('btnGetGoods').style.display='';
        }
        catch(e){}
        document.getElementById('btn_select').style.display='none';
        document.getElementById('unbtn_select').style.display='';
    }
    else
    {
        document.getElementById('btn_add').src="../../../images/Button/UnClick_tj.jpg";
        document.getElementById('unbtn_select').style.display='none';
        document.getElementById('btn_select').style.display='';
        try
        {
            document.getElementById('unbtnGetGoods').style.display='';
            document.getElementById('btnGetGoods').style.display='none';
        }
        catch(e){}
    }
}
    
function PrintQua()
{
    var hiddeniqualityd=document.getElementById('hiddeniqualityd').value;
    var ApplyNo=document.getElementById('lbInNo').value;
    var FromType=document.getElementById('ddlFromType').value;
    if(parseInt(hiddeniqualityd) < 1)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageQualityCheckPrint.aspx?ID="+hiddeniqualityd+"&ApplyNo="+ApplyNo+"&FromType="+FromType);
}

function BackToPage()
{
    requestQuaobj = GetRequest();   
    var intFromType=requestQuaobj['intFromType'];
    var moduleID=requestQuaobj['ListModuleID'];
    if(intFromType!=null)
    {
        if(intFromType!=''  && intFromType=='2')
        {
            location.href='../../../DeskTop.aspx';
        }
        if(intFromType!='' && intFromType=='3')
        {
            location.href='../../../Pages/Personal/WorkFlow/FlowMyApply.aspx?ModuleID='+moduleID;
        }
        if(intFromType!='' && intFromType=='4')
        {
            location.href='../../../Pages/Personal/WorkFlow/FlowMyProcess.aspx?ModuleID='+moduleID;
        }
        if(intFromType!='' && intFromType=='5')
        {
            location.href='../../../Pages/Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID='+moduleID;
        }
    }
    else
    {
        history.back(); 
    }
}