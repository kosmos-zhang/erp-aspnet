$(document).ready(function()
{
   var FlowNo=  document.getElementById('hf_flowid').value;
   if(FlowNo!="")
    {
        LoadFlowDetailInfo(FlowNo);
        isnew="Edit";
         requestobj = GetRequest();
         recordnoparamflag= requestobj['typeflag'];
        recordnoparamcode = requestobj['typecode'];
         recordnoparamuse = requestobj['usestatus'];
         if(typeof(recordnoparamcode)!="undefined")
         {
         document.getElementById("hidfcode").value=recordnoparamcode;
         document.getElementById('hd_typeflag').value=recordnoparamflag;
         document.getElementById("sel_status").value==recordnoparamuse;
         }
    }
});
var isnew="Add";
function Hide()
{
try{
    var stastus=$("#sel_status").val();
        switch (stastus)
        {
        case "0":
        document.getElementById("btn_publish").style.display = "block";
        document.getElementById("btn_stop").style.display = "none";
        break;
        case "1":
        document.getElementById("btn_publish").style.display = "block";
        document.getElementById("btn_stop").style.display = "none";
        break;
        case "2":
        document.getElementById("btn_publish").style.display = "none";
        document.getElementById("btn_stop").style.display = "block";
        }
        }catch(e)
        {}
}
function Fun_Save_Flow()
{
    var  txt_FlowID="";
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var txt_FlowName = $("#txt_FlowName").val();
    var DeptName = document.getElementById('DeptID').value;
    var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
    var Ddp_BillType = $("#hf_typeflag").val();
    var txt_BillTypeName = $("#hf_typecode").val();
    var sel_status = $("#sel_status").val();
    var sel_IsMobileNotice = $("#sel_SendMS").val();
       if (CodeType == "")
         {
             txt_FlowID=trim($("#CodingRuleControl1_txtCode").val());
             if(txt_FlowID=="")
             {
                isFlag = false;
                fieldText += "流程编号|";
                msgText += "请输入流程编号|";
             }
              if(strlen(txt_FlowID)>50)
             {
                isFlag = false;
                fieldText += "流程编号|";
                msgText += "流程编号仅限于50个字符以内|";
             }
             if(strlen(txt_FlowID)>0){
            if(!CodeCheck(txt_FlowID))
            {
               isFlag = false;
               fieldText = fieldText + "流程编号|";
   		       msgText = msgText +  "流程编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
            }
            }
         }
     var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(txt_FlowName.length<=0)
    {
        isFlag = false;
        fieldText = fieldText + "流程名称|";
   		msgText = msgText +  "流程名称不允许为空|";
    }
    if(txt_BillTypeName.length<=0||txt_BillTypeName=="")
    {
        isFlag = false;
        fieldText = fieldText + "单据分类|";
   		msgText = msgText +  "单据分类不允许为空|";
    }
     if(Ddp_BillType.length<=0||Ddp_BillType=="0")
    {
        isFlag = false;
        fieldText = fieldText + "单据分类标志|";
   		msgText = msgText +  "单据分类标志不允许为空|";
    }
    if(sel_status=="")
    {
        isFlag = false;
        fieldText = fieldText + "启用状态|";
   		msgText = msgText +  "启用状态不允许为空|";
    }
    
    if(strlen(txt_FlowName)>50)
    {
        isFlag = false;
        fieldText = fieldText + "流程名称|";
   		msgText = msgText +  "流程名称仅限于50个字符以内|";
    }
    
  //Start 验证明细列表输入
    var table = document.getElementById("dg_Log");
    var count = table.rows.length;
    if(count==1)
    {
        isFlag = false;
        fieldText = fieldText + "流程步骤|";
   		msgText = msgText +  "请填写流程步骤|";
    }
    for(var i=0;i<count-1;i++)
    {
                var objFlowStepName = 'txtFlowStepName'+(i+1);               //运行时间
                var objFlowStepActor = 'txtUserFlowNameID'+(i+1);                   //单位计时工资
                var FlowStepName = document.getElementById(objFlowStepName.toString()).value;
                var FlowStepActor = document.getElementById(objFlowStepActor.toString()).value;
                if(FlowStepName=='')
                {
                        isFlag = false;
                        fieldText = fieldText + "步骤名称"+(i+1)+"|";
   		                msgText = msgText +  "请填写步骤名称|";
                }
                if(FlowStepActor=='')
                {
                        isFlag = false;
                        fieldText = fieldText + "处理人"+(i+1)+"|";
   		                msgText = msgText +  "请选择处理人|";
                }

        }
    if(isnew=="Edit")
    {
      if(sel_status=="2")
      {
        isFlag = false;
        fieldText = fieldText + "状态错误|";
   		msgText = msgText +  "流程状态为草稿或停止时才可以修改";
      }
      txt_FlowID=trim($("#CodingRuleControl1_txtCode").val());
    }
    if(isnew=="Add")
    {
//    if(txt_FlowID.length>0)
//    {
//     if(document.getElementById('hf_flag').value!="2")
//     {
//        popMsgObj.ShowMsg('流程编号已经存在，请重新输入！');
//        return;
//     }
//      }
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    else
    {
        var DetailStepNo = new Array(); //步骤编号
        var DetailStepName = new Array(); //步骤名称
        var DetailActor = new Array(); //步骤处理人
        
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
        var FlowFrame = findObj("dg_Log",document); 
        var table = document.getElementById("dg_Log");
	    var count = table.rows.length; 
	     var Name="";

        for(var i=0;i<count-1;i++)
        {
            var objFlowStepName = 'txtFlowStepName'+(i+1);
            var objFlowStepActor= document.getElementById("txtUserFlowNameID" + (i + 1)).value;
            var objFlowStepActorName= document.getElementById("UserFlowName" + (i + 1)).value;
            var objFlowStepNo=table.rows[i+1].cells[1].innerText;
            DetailStepName.push(document.getElementById(objFlowStepName.toString()).value);
            DetailActor.push(objFlowStepActor);
            DetailStepNo.push(objFlowStepNo);
            Name +=objFlowStepActorName+',';
        }
        var cflowname=Name.substr(0,Name.length-1);
        var ccname=cflowname.split(',');
       var nary=ccname.sort();
//       for(var i=0;i<nary.length-1;i++) 
//{if (nary[i]==nary[i+1]) 
//{
//    popMsgObj.ShowMsg('步骤处理人不能重复！重复人：'+nary[i]);
//    return;
//} 
//}

        
        var UrlParam = "FlowNo="+escape(txt_FlowID)+
                        "&FlowName="+escape(txt_FlowName)+
                        "&DeptName="+escape(DeptName)+
                        "&BillType="+escape(Ddp_BillType)+
                        "&BillTypeName="+escape(txt_BillTypeName)+
                        "&CodeType="+escape(CodeType)+
                        "&action="+escape(isnew)+
                        "&DetailStepNo="+escape(DetailStepNo).toString()+
                        "&DetailStepName="+escape(DetailStepName).toString()+
                        "&DetailActor=" + escape(DetailActor).toString() +
                        "&IsMobileNotice=" + sel_IsMobileNotice +
                        "&status="+sel_status;
                        
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/SystemManager/ApprovalFlowSetAdd.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
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
                       document.getElementById('hf_flag').value="0"
                        popMsgObj.ShowMsg(data.info);
                        Hide();
                        document.getElementById('CodingRuleControl1_ddlCodeRule').style.display='none';
                        document.getElementById('CodingRuleControl1_txtCode').style.display='block';
                        if(isnew=="Add")
                        {
                         document.getElementById('CodingRuleControl1_txtCode').value=data.data;
                          document.getElementById('CodingRuleControl1_txtCode').disabled=true;
                        }
                        document.getElementById('CodingRuleControl1_txtCode').className='tdinput';
                        document.getElementById('CodingRuleControl1_txtCode').style.width='90%';
                        isnew="Edit";

                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
    }
}


function LoadFlowDetailInfo(FlowNo)
{
try{
document.getElementById('hf_flag').value="0";
   $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/SystemManager/ApprovalFlowList.ashx?FlowNo="+FlowNo,//目标地址
       cache:false,          
       success: function(msg){
                var rowsCount = 0;
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
//                        document.getElementById('txt_FlowID').value = item.FlowNo;
                        document.getElementById('txt_FlowName').value = item.FlowName;
                        document.getElementById('DeptName').value = item.DeptID;
                        document.getElementById('DeptID').value=item.DeptmentID
                        document.getElementById('hf_typecode').value= item.typecode
                        document.getElementById('hf_typeflag').value = item.BillTypeFlag;
                        document.getElementById('txt_flag').value=item.billflag;
                        document.getElementById('txt_BillTypeName').value = item.BillTypeCode;
                        document.getElementById('sel_status').value = item.UsedStatus;
                        document.getElementById('sel_SendMS').value = item.IsMobileNotice;
                        document.getElementById('CodingRuleControl1_ddlCodeRule').style.display='none';
                        document.getElementById('CodingRuleControl1_txtCode').style.display='block';
                        document.getElementById('CodingRuleControl1_txtCode').value=item.FlowNo;
                        document.getElementById('CodingRuleControl1_txtCode').className='tdinput';
                        document.getElementById('CodingRuleControl1_txtCode').style.width='90%';
                        if(item.FlowNo!=null && item.FlowNo!='')
                        {
                            rowsCount ++ ;
                            //填充明细列表
                           FillFlowRow(i,item.StepNo,item.StepName,item.Actor,item.actorname);
                        }
                        
               });
               document.getElementById('txtTRLastIndex').value = rowsCount+1;    
               Hide();
              },
       error: function() {}, 
       complete:function(){}
       });
       }
       catch(e)
       {}
       }


function FillFlowRow(i,StepNo,StepName,Actor,ActorName)
 {
      var tempTimeUnit = '';
      var tempIsoutsource = '';
      var tempCheckWay ='';
      var tempIsCharge ='';
      var rowID = parseInt(i)+1;
        table = document.getElementById("dg_Log");
	     var rowID = table.rows.length;        
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "FlowStep" + rowID;
        
         var newNameXH=newTR.insertCell(0);//添加列:选择
        newNameXH.className="cell";
        newNameXH.innerHTML="<input id='chkSelect_" + rowID + "' value='"+rowID+"' type='checkbox' size='20' />";
        
        var newNameTD=newTR.insertCell(1);//添加列:步骤编号
        newNameTD.className="cell";
	    newNameTD.id = "NO_" + rowID;
        newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
       
        var newFitNametd=newTR.insertCell(2);//添加列:步骤描述
        newFitNametd.className="cell";
        newFitNametd.innerHTML = "<input id='txtFlowStepName" + rowID + "'  specialworkcheck='步骤描述' type='text' value=\""+StepName+"\" style='width:80%'  class='tdinput' />";//添加列内容
        
        
          var newFitDesctd=newTR.insertCell(3);//添加列:处理人ID
         newFitDesctd.className="cell";
        newFitDesctd.innerHTML = "<input id='txtUserFlowNameID" + rowID + "'' value=\""+Actor+"\"   type='hidden' /><input  id='UserFlowName" + rowID + "' readonly type='text'  style='width:90%' class='tdinput' value=\""+ActorName+"\"  onclick=javascript:alertdiv('UserFlowName" + rowID + ",txtUserFlowNameID" + rowID + "') />";//添加列内容
        
 }


function  ClearFlow()
{
 isnew="Add";
}

//验证唯一性
//function checkonly()
//{
////    var flowid=document.getElementById('txt_FlowID').value; 
////    var TableName="Flow";
////    var ColumName="FlowNo";
////   if(flowid.length!=0)
////    {
////    $.ajax({ 
////              type: "POST",
////               url:"../../../Handler/Office/SystemManager/CheckFlow.ashx?FlowNo="+flowid+"&ColumName="+ColumName+"&TableName=+"+TableName,
////              dataType:'json',//返回json格式数据
////              cache:false,
////              beforeSend:function()
////              { 
////              }, 
////            error: function() 
////            {
////            }, 
////          success:function(data) 
////            { 
////                if(data.sta==1) 
////                { 
////                document.getElementById('hf_flag').value="2";
////                }
////                else if(data.sta=="0")
////                {
////                 }            
////            } 
////           });
////    }
//}

    var flag=false;
     ///添加行
       //添加
    function AddSignRowflag()
    {
        flag=true;
        AddSignRow(null,null,null,null);
    }
 function AddSignRow(StepNo,StepName,Actor)
 { //读取最后一行的行号，存放在txtTRLastIndex文本框中 
   if(!flag)
         {
 
        }
        else
        {
          table = document.getElementById("dg_Log");
	     var rowID = table.rows.length;        
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "FlowStep" + rowID;
        
         var newNameXH=newTR.insertCell(0);//添加列:选择
        newNameXH.className="cell";
        newNameXH.innerHTML="<input id='chkSelect_" + rowID + "' value='"+rowID+"' type='checkbox' size='20' />";
        
        var newNameTD=newTR.insertCell(1);//添加列:步骤编号
        newNameTD.className="cell";
	    newNameTD.id = "NO_" + rowID;
        newNameTD.innerHTML = newTR.rowIndex.toString();//添加列内容
       
        var newFitNametd=newTR.insertCell(2);//添加列:步骤描述
        newFitNametd.className="cell";
        newFitNametd.innerHTML = "<input id='txtFlowStepName" + rowID + "' type='text' specialworkcheck='步骤描述' style='width:80%' class='tdinput'  />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(3);//添加列:处理人ID
         newFitDesctd.className="cell";
        newFitDesctd.innerHTML = "<input id='txtUserFlowNameID" + rowID + "'' type='hidden'/><input  id='UserFlowName" + rowID + "'  readonly type='text' style='width:90%' class='tdinput' onclick=javascript:alertdiv('UserFlowName" + rowID + ",txtUserFlowNameID" + rowID + "') />";//添加列内容
        //popSellEmpObj.ShowList('txtFitdes" + rowID + "')
//        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
        }
}


function DeleteDetailRow()
{
    //获取表格
    table = document.getElementById("dg_Log");
	var count = table.rows.length;
	for (var i = count - 1; i > 0; i--)
	{
        var select = document.getElementById("chkSelect_" + i);
        if (select.checked)
        {
            DeleteRow(table, i);
        }
	}    
	document.getElementById("checkall").checked=false;
}

function DeleteRow(table,row)
{    
    //获取表格
	var count = table.rows.length - 1;
	table.deleteRow(row);
	if (row < count)
	{
	    for(var j = parseInt(row) + 1; j <= count; j++)
	    {
			var no = j - 1;
			document.getElementById("NO_" + j).innerHTML = parseInt(document.getElementById("NO_" + j).innerHTML) - 1;
			document.getElementById("NO_" + j).id = "NO_" + no;
			document.getElementById("chkSelect_" + j).id = "chkSelect_" + no;
			document.getElementById("txtFlowStepName" + j).id = "txtFlowStepName" + no;
			document.getElementById("UserFlowName" + j).id = "UserFlowName" + no;
			document.getElementById("txtUserFlowNameID" + j).id = "txtUserFlowNameID" + no;
	    }
	}
}

function SwapRow()
{ 
  var t= 0;
  table = document.getElementById("dg_Log");
  var count = table.rows.length ;
   for (var i = count - 1; i > 0; i--)
	{
        var select = document.getElementById("chkSelect_" + i);
        if (select.checked)
        {
          t=t+1;
        }
	} 
   if(t==0)
	{
	    popMsgObj.ShowMsg('请选择需要操作选项！');
	   return;
	}
	if(t>1)
	{
	   popMsgObj.ShowMsg('一次只能移动一条记录！');
	   return;
	}
   var select = document.getElementById('chkSelect_1');
   if(select.checked)
   {
      popMsgObj.ShowMsg('首记录不能上移！');
	   return;
   }
   for (var i = count - 1; i > 0; i--)
	{
        var select = document.getElementById("chkSelect_" + i);
        if (select.checked)
        {
          SwapRowValue(i,i-1);
          break;
        }
	} 
}




///交换行数据
function SwapRowValue(x,y)
{ 
	var objX,objY;
	objX = document.getElementById("txtFlowStepName" + x);
	objY = document.getElementById("txtFlowStepName" + y);
    var str = objX.value;
	objX.value = objY.value;
	objY.value = str;


	objX = document.getElementById("chkSelect_" + x);
	objY = document.getElementById("chkSelect_" + y);
	objX.checked=false;
	objY.checked=true;
			
    objX = document.getElementById("UserFlowName" + x);
	objY = document.getElementById("UserFlowName" + y);
	var str1 = objX.value;
	objX.value = objY.value;
	objY.value = str1;
	
	   objX = document.getElementById("txtUserFlowNameID" + x);
	objY = document.getElementById("txtUserFlowNameID" + y);
	var str2 = objX.value;
	objX.value = objY.value;
	objY.value = str2;
}
function SwapRowDown()
{ 
  var t= 0;
  table = document.getElementById("dg_Log");
   var count = table.rows.length ;
   for (var i = count - 1; i > 0; i--)
	{
        var select = document.getElementById("chkSelect_" + i);
        if (select.checked)
        {
          t=t+1;
        }
	} 
	if(t>1)
	{
	   popMsgObj.ShowMsg('一次只能移动一条记录！');
	   return;
	}
	 if(t==0)
	{
	    popMsgObj.ShowMsg('请选择需要操作选项！');
	   return;
	}
	 var select = document.getElementById("chkSelect_"+(count-1));
   if(select.checked)
   {
      popMsgObj.ShowMsg('末记录不能下移！');
	   return;
   }
   for (var i = count - 1; i > 0; i--)
	{
        var select = document.getElementById("chkSelect_" + i);
        if (select.checked)
        {
          SwapRowValue(i,i+1);
          break;
        }
	} 
}

function Publish()
{
var flag=document.getElementById('hf_flag').value;
  var table = document.getElementById("dg_Log");
    var count = table.rows.length;
    if(count==1)
    {
      popMsgObj.ShowMsg('请填写流程步骤！');
      return;
    }
if(flag!="0")
{
  popMsgObj.ShowMsg('请先添加流程！');
  return;
}
   var PubFlowNo=  document.getElementById('hf_flowid').value;
   if(PubFlowNo=="")
   {
     PubFlowNo=trim($("#CodingRuleControl1_txtCode").val());
   }
   if(PubFlowNo.length!=0)
    {
    $.ajax({ 
              type: "POST",
                 url: "../../../Handler/Office/SystemManager/ApprovalFlowSetAdd.ashx?PubFlowNo="+PubFlowNo+"&action=publish",
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
              }, 
            error: function() 
            {
            }, 
          success:function(data) 
            { 
                if(data.sta==1) 
                { 
                  popMsgObj.ShowMsg('发布成功！');
                   document.getElementById("sel_status").value=data.data;
                   Hide();
                }
                        
            } 
           });
    }
}
function stop()
{
var flag=document.getElementById('hf_flag').value;
 var table = document.getElementById("dg_Log");
    var count = table.rows.length;
    if(count==1)
    {
      popMsgObj.ShowMsg('请填写流程步骤！');
      return;
    }
if(flag!="0")
{
  popMsgObj.ShowMsg('请先添加流程！');
  return;
}
   var PubFlowNo=  document.getElementById('hf_flowid').value;
   if(PubFlowNo=="")
   {
     PubFlowNo=trim($("#CodingRuleControl1_txtCode").val());
   }
   if(PubFlowNo.length!=0)
    {
    $.ajax({ 
              type: "POST",
                 url: "../../../Handler/Office/SystemManager/ApprovalFlowSetAdd.ashx?PubFlowNo="+PubFlowNo+"&action=stop",
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
              }, 
            error: function() 
            {
            }, 
          success:function(data) 
            { 
                if(data.sta==1) 
                { 
                  popMsgObj.ShowMsg('流程停止成功！');
                   document.getElementById("sel_status").value=data.data;
                     Hide();
                }
                else if(data.sta==2)
                {
                 popMsgObj.ShowMsg('该流程已经提交审批，不能停止！');
                }
                else if(data.sta==0)
                {
                popMsgObj.ShowMsg('流程停止失败！')
                }
                        
            } 
           });
    }
}
//全选
function selectall() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
function CompareID()
{
    table = document.getElementById("dg_Log");
	     var rowID = table.rows.length; 
	     if(rowID>2)      
	     for(var i=0;i<rowID-1 ;i++)
	     {
	      var Name=document.getElementById("UserFlowName"+(i+1)).value;
          var CName=document.getElementById("UserFlowName"+(i+2)).value;
	     }
   
}