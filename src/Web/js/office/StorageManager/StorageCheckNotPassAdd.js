$(document).ready(function()
{
     requestQuaobj = GetRequest();   
     var action=requestQuaobj['myAction'];
     if(action=='fromlist')
     {
         document.getElementById('btnReturn').style.display='';
     }
    
    if(NoPassID>0)
    { 
        document.getElementById('btnReturn').style.display='';
        LoadNoPassInfo(NoPassID);

    }
    else
    {
        // 填充扩展属性
        GetExtAttr("officedba.CheckNotPass", null);
        GetFlowButton_DisplayControl();
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
function LoadNoPassInfo(NoPassID)
{
       var rowsCount=0;
       ClearSignRow();
       $.ajax({
       type: "POST",//用POST方式传输ss
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/StorageManager/StorageNoPassInfo.ashx?ID="+NoPassID,//目标地址
       cache:false,   
           beforeSend:function(){AddPop();},//发送数据之前              
       success: function(msg){
                var rowsCount = 0;
                var countTotals = 0;
                //数据获取完毕，填充页面据显示
                    //生产任务单信息
                    if(typeof(msg.dataReport)!='undefined')
                    {
                      //  GetFlowButton_DisplayControl();
                        $.each(msg.dataReport,function(i,item)           
                        {
                            
                            document.getElementById('lbInfoNo').value = item.ProcessNo;
                            document.getElementById('hiddenNoPassID').value=item.ID;                                                         
                            document.getElementById('txtSubject').value=item.Title;  //
                            document.getElementById('sltFromType').value=item.FromType;  
                                                      
                            document.getElementById('ReportNO').value=item.ReportNo;                                                           
                            document.getElementById('hiddenReportID').value=item.ReportID; 
                                                       
                            document.getElementById('UserExecutor').value = item.EmployeeName;                            
                            document.getElementById('hiddentbExecutorID').value=item.Executor;                            
                            document.getElementById('tbProcessDate').value=item.ProcessDate.substring(0,10);                            
                            document.getElementById('tbProName').value=item.ProductName; //                            
                            document.getElementById('hiddenProID').value=item.ProductID;  //                            
                            document.getElementById('ProductNo').value=item.ProNo; // 
                            document.getElementById('Specification').value=item.Specification;
                            document.getElementById('txtUnit').value=item.UnitName;
                            document.getElementById('hiddentxtUnit').value=item.UnitID;                            
                            document.getElementById('txtNoPassNum').value=FormatAfterDotNumber(item.NoPass,selPoint); 
                            if(item.BillStatus=='1')
                            {document.getElementById('tbBillStatus').value='制单';}
                            if(item.BillStatus=='2')
                            {document.getElementById('tbBillStatus').value='执行';}
                            if(item.BillStatus=='3')
                            {document.getElementById('tbBillStatus').value='变更';}
                            if(item.BillStatus=='4')
                            {document.getElementById('tbBillStatus').value='手工结单';}
                            if(item.BillStatus=='5')
                            {document.getElementById('tbBillStatus').value='自动结单';}                            
                            document.getElementById('ddlBillStatus').value=item.BillStatus; 
                            document.getElementById('tbCreater').value=item.CreatorName; 
                            document.getElementById('txtCreator').value=item.Creator;                            
                            document.getElementById('txtCreateDate').value=item.CreateDate.substring(0,10);
                            document.getElementById('txtConfirmor').value=item.ConfirmorName;  
                            document.getElementById('txtConfirmorReal').value=item.Confirmor;
                            document.getElementById('txtConfirmDate').value=item.ConfirmorDate.substring(0,10);  
                            document.getElementById('txtCloser').value=item.Closer;
                            document.getElementById('txtCloserReal').value=item.CloserName;                            
                            document.getElementById('txtModifiedUserID').value=item.ModifiedUserIDName;
                            document.getElementById('hiddenModifiedUserID').value=item.ModifiedUserID;                            
                            document.getElementById('txtModifiedDate').value=item.ModifiedDate.substring(0,10);                            
                            document.getElementById('txtRemark').value=item.Remark;   //
                            
                            document.getElementById('hfPageAttachment').value=item.Attachment.replace(/,/g,"\\");
                            var testurl=item.Attachment.replace(/,/g,"\\");   
                            var testurl2=testurl.lastIndexOf('\\');
                            testurl2=testurl.substring(testurl2+1,testurl.lenght);
                            document.getElementById('attachname').innerHTML=testurl2;
                            
                            
                            
                            document.getElementById('hiddenBillStatusID').value=item.BillStatus;
                            document.getElementById('hiddenFlowStatusID').value=item.FlowStatus; 
                            document.getElementById('divCodeRuleUC').style.display='none';
                            document.getElementById('divTaskNo').style.display='';                           
                            
                            
                            // 填充扩展属性
                            GetExtAttr("officedba.CheckNotPass", item);
                            
                            var editBillStatus=document.getElementById('hiddenBillStatusID').value;
                            var editFlowStatus=document.getElementById('hiddenFlowStatusID').value;
                            if (document.getElementById('hfPageAttachment').value != "")
                             {            
                                
                              //下载删除显示
                              document.getElementById("divDealAttachment").style.display = "";
                              //上传不显示
                              document.getElementById("divUploadAttachment").style.display = "none";
                            }  

                        });
                    }
             
                    if(typeof(msg.dataDetail)!='undefined')
                    {
                       $.each(msg.dataDetail,function(i,item){
                            if(item.ID != null && item.ID != "")
                            {
                                rowsCount ++ ; 
                                AddSignRowDetail(i,item.ReasonID,item.NotPassNum,item.ProcessWay,item.Rate,item.Remark);
                            }
                       });
                    }
                GetFlowButton_DisplayControl();
               document.getElementById('txtTRLastIndex').value = rowsCount+1; 
               var BillStatus=document.getElementById('ddlBillStatus').value;
               if(BillStatus=='2')
                {
                    document.getElementById('divConfirmor').style.display='';
                    document.getElementById('divConfirmorDate').style.display='';
                }
                if(BillStatus=='4')
                {
                    document.getElementById('divConfirmor').style.display='';
                    document.getElementById('divConfirmorDate').style.display='';
                    document.getElementById('divCloserDate').style.display='';
                    document.getElementById('divCloser').style.display='';
                }   

              },
       error: function() {alert('加载数据时发生请求异常');}, 
       complete:function(){hidePopup();}
       });
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


function AddSignRowDetail(i,a,b,c,d,e,f,g)
{

        var rowID = parseInt(i)+1;
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "Row_" + rowID;
        var colChoose=newTR.insertCell(0);//添加列:选择
        colChoose.className="cell";
        colChoose.align="center";
        colChoose.innerHTML="<input name='Chk'  style='width:90%'  id='Chk_Option_"+rowID+"'  value=\""+i+"\" type='checkbox' />";
        
        var colProductNo=newTR.insertCell(1);//添加列:不合格原因
        colProductNo.className="cell";
        colProductNo.innerHTML = "<select id='TD_Reason_"+rowID+"' name='TD_Reason_"+rowID+"' ></select>";
        var theID='TD_Reason_'+rowID;
        GetReasonCode(theID,a);
        
        var colProductName=newTR.insertCell(2);//添加列:数量
        colProductName.className="cell";
        colProductName.innerHTML = "<input type=\"text\" maxlength=\"10\" onchange=\"Number_round(this,"+selPoint+");\" onblur=\"GetRate('"+rowID+"');\" value=\""+FormatAfterDotNumber(b,selPoint)+"\"  style='width:90%' id=\"TD_CheckNum_"+rowID+"\"  class=\"tdinput\"  />";
        
        var colUnitID=newTR.insertCell(3);//添加列:处置方式 
        colUnitID.className="cell";
        colUnitID.innerHTML = "<select id='TD_ProcessWay_"+rowID+"' ><option value=\"1\">拒收</option><option value=\"2\">报废</option><option value=\"3\">降级</option><option value=\"4\">销毁</option></select>";
        document.getElementById('TD_ProcessWay_'+rowID).value=c;
        var colSpecification=newTR.insertCell(4);//添加列:比率
        colSpecification.className="cell";
        colSpecification.innerHTML = "<input type=\"text\" disabled value=\""+FormatAfterDotNumber(d,selPoint)+"\"  style='width:90%'  id='TD_Rate"+rowID+"' value=\"\" class=\"tdinput\"   />";
        
               
        var colRemark=newTR.insertCell(5);//添加列:备注
        colRemark.className="cell";
        colRemark.innerHTML = "<input type=\"text\" value=\""+e+"\"  style='width:90%'  id='TD_Remark_"+rowID+"' value=\"\"  class=\"tdinput\"  />";
        var colNum=newTR.insertCell(6);//添加列:序号
        colNum.className="cell";
        colNum.innerHTML = "<input style='display:none' type=\"text\" size=\"2\"   id=\"TD_Num_"+rowID+"\" value=\""+rowID+"\"  class=\"tdinput\"  readonly />"; 
        document.getElementById('txtTRLastIndex').value = (rowID + 1).toString() ;//将行号推进下一行
        
}
function GetRate(a)
{
    var Num=document.getElementById('TD_CheckNum_'+(a)).value;
    var Rate=document.getElementById('TD_Rate'+(a));
    var NotPassNum=document.getElementById('txtNoPassNum').value;
    if (NotPassNum!=null && NotPassNum!='')
    {
        Rate.value=FormatAfterDotNumber((Num/NotPassNum)*100,selPoint);
    }
    
}
//-------------------------保存Start
function Fun_Save_NoPass()
{
    
  if(CheckInput())
  {
  
  if(CheckDetailCount())
  {
    var TestNoPassCount=document.getElementById('txtNoPassNum');
    if(parseFloat(TestNoPassCount)==0)
    {
        popMsgObj.ShowMsg('不能保存不合格数量是0的单据！');
        return false;
    }
    var editBillStatus=document.getElementById('hiddenBillStatusID').value;
    var editFlowStatus=document.getElementById('hiddenFlowStatusID').value;

    var ProcessNo="";
    var UrlParam='';
    var myAction='add';
    var bmgz='';
    var myMethod=document.getElementById('hiddenNoPassID').value;   //该单据ID
    if(myMethod=='0')  // 新建
    {
      if($("#checkNo_ddlCodeRule").val()=="")//手工输入
       {
        ProcessNo=$("#checkNo_txtCode").val();
        bmgz="sd";
       }
     else
       {
        ProcessNo=$("#checkNo_ddlCodeRule").val();
        bmgz="zd";
       }
    }
    else   //编辑
    {
        ProcessNo=document.getElementById('lbInfoNo').value;
        myAction='edit';
    }

    var Title=document.getElementById('txtSubject').value;
    var FromType=document.getElementById('sltFromType').value;   //-------------------------源单类型  根据ID 查找需要更新的相应表
    var ReportID=document.getElementById('hiddenReportID').value; //原单 的ID号    -----------
    var ExecutorID=document.getElementById('hiddentbExecutorID').value; //处置人ID    
    var ProcessDate=document.getElementById('tbProcessDate').value;  //处置日期ID  
    var Remark=$("#txtRemark").val();
    var Attachment=document.getElementById('hfPageAttachment').value;
    var BillStatus=document.getElementById('ddlBillStatus').value;

    
    //-----------------------------------------------------------明细Start
    var DetailSortNo=new Array();        //序号
    var DetailReason=new Array();     //原因
    var DetailNum=new Array(); //数量
    var DetailProcessWay=new Array();    //
    var DetailRate=new Array();   //
    var DetailRemark=new Array();
    
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    var detailFlag=true;
    var detailFild='';
    var detailMsg='';
    var testIsPass='1';
    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        {   
                GetFlowButton_DisplayControl();
             var theReason=document.getElementById('TD_Reason_'+(i)).value;     //
             var theNum=document.getElementById('TD_CheckNum_'+(i)).value; //
             var theProcessWay=document.getElementById('TD_ProcessWay_'+(i)).value;    //
             var theRate=document.getElementById('TD_Rate'+(i)).value;   //
             var theRemark=document.getElementById('TD_Remark_'+(i)).value;

            
              DetailSortNo.push(i);
              DetailReason.push(theReason);
              DetailNum.push(theNum);
              DetailProcessWay.push(theProcessWay);
              DetailRate.push(theRate);
              DetailRemark.push(theRemark);           
      
        }        
    }  
    //------------------------------明细End

                                     
UrlParam="Title="+escape(Title)+                                          
        "&FromType="+FromType+ 
        "&ReportID="+ReportID+                                  
        "&ExecutorID="+ExecutorID+                                     
        "&ProcessDate="+ProcessDate+                                       
        "&Remark="+escape(Remark)+ 
        "&BillStatus="+BillStatus+                                
        "&Attachment="+escape(Attachment)+                              
        "&DetailSortNo="+DetailSortNo+                            
        "&DetailReason="+DetailReason+                      
        "&DetailNum="+DetailNum.toString()+   
        "&DetailProcessWay="+DetailProcessWay.toString()+         
        "&DetailRate="+DetailRate.toString()+       
        "&DetailRemark="+escape(DetailRemark.toString())+                
        "&myAction="+myAction+
        "&bmgz="+bmgz+
        "&ProcessNo="+ProcessNo+                                        
        "&ID="+myMethod+ 
        GetExtAttrValue();                                            

        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageNoPassAdd.ashx",
                  dataType:'json',//返回json格式数据
                  data:UrlParam,
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('保存质检报告时请求发生错误');
                  }, 
                  success:function(data) 
                  { 
                     var reInfo=data.data.split('|');

                     if(reInfo.length > 1)
                       {   
                            popMsgObj.ShowMsg(data.info);
                           
                           
                            if(document.getElementById('hiddenNoPassID').value>0)
                            {
                                document.getElementById('txtModifiedUserID').value=reInfo[3];
                                document.getElementById('txtModifiedDate').value=reInfo[2].substring(0,9);  
                                if(document.getElementById('ddlBillStatus').value=='2')
                                {
                                    document.getElementById('ddlBillStatus').value='3';
                                    document.getElementById('tbBillStatus').value='变更';
                                }
                            } 
                             document.getElementById('lbInfoNo').value = reInfo[1];
                            document.getElementById('hiddenNoPassID').value=reInfo[0];       
                            document.getElementById('divCodeRuleUC').style.display='none';                       
                            document.getElementById('divTaskNo').style.display=''; 
                            GetFlowButton_DisplayControl();                
                      }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
              }); 
    }
  }  
}
function CheckInput()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var ProcessNo = ''; 
    var txtSubject = document.getElementById('txtSubject').value;
    var FromNO=document.getElementById('ReportNO').value;
    var UserExecutor=document.getElementById('UserExecutor').value;
    var Remark=document.getElementById('txtRemark').value;
    
    //先检验页面上的特殊字符
    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
      
    //获取编码规则下拉列表选中项
    codeRule = document.getElementById("checkNo_ddlCodeRule").value;
    //如果选中的是 手工输入时，校验编号是否输入
    if(document.getElementById('hiddenNoPassID').value=='0')
    {
        if (codeRule == "")
        {
            //获取输入的编号
            ProcessNo = document.getElementById("checkNo_txtCode").value;
            //编号必须输入
            if (ProcessNo == "")
            {
                isFlag = false;
                fieldText += "单据编号|";
	            msgText += "请输入单据编号|";
            }
            else
            {
                if (isnumberorLetters(ProcessNo))
                {
                    isFlag = false;
                    fieldText += "单据编号|";
                    msgText += "单据编号只能包含字母或数字！|";
                }
            }
        }
    }
     
    if(strlen(txtSubject)>100)
    {
        isFlag = false;
        fieldText = fieldText + "主题|";
   		msgText = msgText +  "仅限于100个字符以内|";
    }
       if(strlen(txtSubject)>0)
    {
         if(!CheckSpecialWord(txtSubject))
        {
            isFlag = false;
            fieldText = fieldText + "主题|";
   		    msgText = msgText +  "单据主题不能含有特殊字符 |";
        }
    }
        if(strlen(FromNO)<=0)
    {
        isFlag = false;
        fieldText = fieldText + "质检报告单|";
   		msgText = msgText +  "请输入质检报告单|";
    }
    if(strlen(UserExecutor)<=0)
    {
        isFlag=false;
        fieldText=fieldText+"处置负责人|";
        msgText=msgText+"请输入处置负责人|";
    }
    if(strlen(Remark)>800)
    {
        isFlag=false;
        fieldText=fieldText+"备注|";
        msgText=msgText+"最多只允许输入800个字符|";
    }
    
   //Start 验证明细列表输入
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        if(signFrame.rows[i+1].style.display!="none")
        {
                var objNum= 'TD_CheckNum_'+(i+1);         //明细数量
                var objProWay='TD_ProcessWay_'+(i+1);
                var objRemark='TD_Remark_'+(i+1);
                var dtNum = document.getElementById(objNum.toString()).value;
                var dtProWay=document.getElementById(objProWay.toString()).value;
                var dtRemark=document.getElementById(objRemark.toString()).value;

                 if(strlen(dtNum)<=0)
                {
                    isFlag = false;
                    fieldText = fieldText + "明细数量"+(i+1)+"|";
                    msgText = msgText +  "请输入明细数量|";
                }
                if(strlen(dtNum)>0)
                {
                    if(!IsNumeric(dtNum,14,4))
                    {
                        isFlag = false;
                        fieldText = fieldText + "明细数量"+(i+1)+"|";
   		                msgText = msgText +  "格式不正确|";
                    }
                }
                if(dtProWay==0)
                {
                    isFlag = false;
                    fieldText = fieldText + "明细处置方式"+(i+1)+"|";
                    msgText = msgText +  "请选择明细处置方式";
                }
                 if(strlen(dtRemark)>800)
                {
                    isFlag = false;
                    fieldText = fieldText + "明细备注"+(i+1)+"|";
                    msgText = msgText +  "最多是允许输入800个字符";
                }           
    
         }
    }
    
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
function FillReportInfo()
{
    var FromType=document.getElementById('sltFromType');
    if(FromType.value=="0")
    {
         popMsgObj.ShowMsg('请先选择一个源单！');
         return false;
    }
    else
    {
        popNoPassObj.ShowList();
    }
}


function CheckDetailCount()
{

    var TheNOPass=document.getElementById('txtNoPassNum').value;          //不合格数量
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document);
    var testCount=0; 
    var falag=true;
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        if(signFrame.rows[i+1].style.display!="none")
        {
            var thevalue=document.getElementById('TD_CheckNum_'+(i+1)).value;
            testCount=testCount+parseFloat(thevalue);
        }
    }
    if(testCount>TheNOPass)
    {
          popMsgObj.ShowMsg('明细数量不能大于总不合格数量!');
          falag=false;
    }

    return falag;
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
        colChoose.innerHTML="<input name='Chk'  style='width:90%'  id='Chk_Option_"+rowID+"'  value=\""+rowID+"\" type='checkbox' />";
      
  
        
        var colProductNo=newTR.insertCell(1);//添加列:不合格原因
        colProductNo.className="cell";
        colProductNo.innerHTML = "<select id='TD_Reason_"+rowID+"' name='TD_Reason_"+rowID+"' ></select>";
        var theID='TD_Reason_'+rowID;
        GetReasonCode(theID,0);
        
        var colProductName=newTR.insertCell(2);//添加列:数量
        colProductName.className="cell";
        colProductName.innerHTML = "<input type=\"text\" maxlength=\"10\" onblur=\"GetRate('"+rowID+"');\" onchange=\"Number_round(this,"+selPoint+");\" style='width:90%'  id=\"TD_CheckNum_"+rowID+"\"  class=\"tdinput\"  />";
        
        var colUnitID=newTR.insertCell(3);//添加列:处置方式 
        colUnitID.className="cell";
        colUnitID.innerHTML = "<select id='TD_ProcessWay_"+rowID+"' ><option value=\"1\">拒收</option><option value=\"2\">报废</option><option value=\"3\">降级</option><option value=\"4\">销毁</option></select>";
        
        var colSpecification=newTR.insertCell(4);//添加列:比率
        colSpecification.className="cell";
        colSpecification.innerHTML = "<input type=\"text\" disabled style='width:90%'  id='TD_Rate"+rowID+"' value=\"\" class=\"tdinput\"   />";
        
               
        var colRemark=newTR.insertCell(5);//添加列:备注
        colRemark.className="cell";
        colRemark.innerHTML = "<input type=\"text\"  style='width:90%'  id='TD_Remark_"+rowID+"' value=\"\"  class=\"tdinput\"  />";
        
        
        var colNum=newTR.insertCell(6);//添加列:序号
        colNum.className="cell";
        colNum.innerHTML = "<input style='display:none' type=\"text\" size=\"2\"   id=\"TD_Num_"+rowID+"\" value=\""+rowID+"\"  class=\"tdinput\"  readonly />";      
        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
        
}
function CountRate(a,b)
{
    if(document.getElementById(a).value!='')
    {
        if(!IsNumber(document.getElementById(a).value))
        {
              popMsgObj.ShowMsg('明细数量只能填写正整数!');
              return false;
        }
    }
    var nopassnum=document.getElementById('txtNoPassNum').value;
    var Count=document.getElementById(a).value;
    if(nopassnum!='' && Count!='')
    {
        document.getElementById(b).value=(parseFloat(Count)/parseFloat(nopassnum)).toFixed(2);
    }
}
function DeleteSignRow()
{
    var signFrame = findObj("dg_Log",document);        
    var ck = document.getElementsByName("Chk");
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
            }
        }
    }
}

function SelectSubAll()
{
    var signFrame = findObj("dg_Log",document);        
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
    if(document.getElementById("CheckAll").checked)
    {
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objRadio = 'Chk_Option_'+(i+1);
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
                var objRadio = 'Chk_Option_'+(i+1);
                document.getElementById(objRadio.toString()).checked = false;
            }
        }
    }
}
function GetReasonCode(a,b)
{
       var Order="";
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON

       url:"../../../Handler/Office/StorageManager/StorageGetReason.ashx",//目标地址
       data: "&orderby=" +Order,//数据
       cache:false,
       beforeSend:function(){},//发送数据之前
       success: function(msg){
       var index=1;
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")                     
                    var CheckItemSelect=document.getElementById(a);                     
                    CheckItemSelect.options.add(new Option(item.CodeName,item.ID));
                    if(b==item.ID)
                    {
                        CheckItemSelect.value=b;
                    }
                                       
                        
               });
              },
       error: function() 
       {
           
       }, 
       complete:function(){}//接收数据完毕
       });
}
//质检确认 
function Fun_ConfirmOperate()
{
    if(!window.confirm('确认要进行确认结单操作吗?'))
    {
       return false;
    }
    var CheckNum=0;
    var AllNum=0;
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        {   
             var theNum=document.getElementById('TD_CheckNum_'+(i)).value; //
             var theProcessWay=document.getElementById('TD_ProcessWay_'+(i)).value;    //
             if(theProcessWay=='1')
             {
                CheckNum=parseFloat(CheckNum)+parseFloat(theNum);
             }
             AllNum=parseFloat(AllNum)+parseFloat(theNum);
        }
    }    
    
    
    var hiddeniqreportid = document.getElementById('hiddenNoPassID').value;
      var ReportID=document.getElementById('hiddenReportID').value; //原单 的ID号    -----------
    var UrlParam = "myAction=Confirm&ID="+hiddeniqreportid.toString()+"&ReportID="+ReportID+"&AllNum="+AllNum+"&CheckNum="+CheckNum;
                       
    $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageNoPassAdd.ashx",
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
                       document.getElementById('UnbtnSave').style.display='';
                        document.getElementById('btnSave').style.display='none';                      
                        if(reInfo[0].toString()=='none')
                        {
                            return false;
                        }
                        document.getElementById('txtModifiedUserID').value=reInfo[0];
                        document.getElementById('txtModifiedDate').value=reInfo[1].substring(0,9); 
                        document.getElementById('ddlBillStatus').value="2";
                        document.getElementById('tbBillStatus').value='执行';
                        document.getElementById('divConfirmor').style.display='';
                        document.getElementById('divConfirmorDate').style.display='';
                        GetFlowButton_DisplayControl();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
}
function Fun_UnConfirmOperate()//取消确认
{
    if(!window.confirm('确认要进行取消确认结单操作吗?'))
    {
        return false;
    }
        var CheckNum=0;
        var AllNum=0;
    var signFrame = findObj("dg_Log",document); 
    var count = signFrame.rows.length;//有多少行
    for(var i=1;i<count;i++)
    {
        if(signFrame.rows[i].style.display!='none')
        {   
             var theNum=document.getElementById('TD_CheckNum_'+(i)).value; //
             var theProcessWay=document.getElementById('TD_ProcessWay_'+(i)).value;    //
             if(theProcessWay=='1')
             {
                CheckNum=parseFloat(CheckNum)+parseFloat(theNum);
             }
                AllNum=parseFloat(AllNum)+parseFloat(theNum);
        }
    }    
    var ReportID=document.getElementById('hiddenReportID').value; //原单 的ID号    -----------
    var hiddeniqreportid = document.getElementById('hiddenNoPassID').value;
    var UrlParam = "myAction=UnConfirm&AllNum="+AllNum+"&ReportID="+ReportID+"&ID="+hiddeniqreportid.toString()+"&CheckNum="+CheckNum;
                       
    $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageNoPassAdd.ashx",
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
                        document.getElementById('btnSave').style.display='';
                        document.getElementById('UnbtnSave').style.display='none';
                        document.getElementById('txtModifiedUserID').value=reInfo[0];
                        document.getElementById('txtModifiedDate').value=reInfo[1].substring(0,9); 
                        document.getElementById('ddlBillStatus').value="1";
                        document.getElementById('tbBillStatus').value='制单';
                        document.getElementById('divConfirmor').style.display='none';
                        document.getElementById('divConfirmorDate').style.display='none';
                        GetFlowButton_DisplayControl();
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
    var hiddeniqreportid =document.getElementById('hiddenNoPassID').value
    var UrlParam = "myAction=Close&myMethod="+myMethod+"&ID="+hiddeniqreportid.toString()+"";
                       
    $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageNoPassAdd.ashx",
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
                        document.getElementById('UnbtnSave').style.display='';
                        document.getElementById('btnSave').style.display='none';
                        if(isComplete)
                        {
                            document.getElementById('ddlBillStatus').value="4";
                            document.getElementById('tbBillStatus').value='手工结单';
                            document.getElementById('divCloser').style.display='';
                            document.getElementById('divCloserDate').style.display='';
                        }
                        else
                        {
                            document.getElementById('ddlBillStatus').value="2";
                            document.getElementById('tbBillStatus').value='执行';
                            document.getElementById('divCloser').style.display='none';
                            document.getElementById('divCloserDate').style.display='none';
                        }
                        document.getElementById('txtModifiedUserID').value=reInfo[0];
                        document.getElementById('txtModifiedDate').value=reInfo[1].substring(0,9); 
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
function SetSaveButton_DisplayControl(flowStatus)
{

    //流程状态：0：待提交   1：待审批   2：审批中   3：审批通过     4：审批不通过   5：撤销审批
    //制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    //制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    //变更和手工结单的不可以修改
    var PageBillID = document.getElementById('hiddenNoPassID').value;
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