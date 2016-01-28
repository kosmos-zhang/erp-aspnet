    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var ifshow="0";
    var currentPageIndex = 1;
    var orderBy = "ModifiedDate_d";//排序字段
    var reset="1";
    var action="";


$(document).ready(function()
{
 
   
//fnGetSelectCondition();
      var requestobj = GetRequest();   
      
      var SourcePage = requestobj['SourcePage']; 

      if((SourcePage!=null)&&(SourcePage !=""))  
      {
          fnSetSelectCondition(requestobj);
          fnGetSelectCondition();
          TurnToPage(currentPageIndex);
      }
      IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
    });



//全选
function SelectAll() {
    $.each($("#PurPlanBill :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

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

//************************************************Start 界面取值与赋值**************************************************
//将检索条件放入隐藏域
function fnGetSelectCondition()
{
    var strSelect = "";
    var PlanNo = document.getElementById("txtPlanNo").value.Trim();
    var PlanTitle = document.getElementById("txtPlanTitle").value.Trim();
    var PlanUser = document.getElementById("PlanUserID").value.Trim();
    var PlanUserName = document.getElementById("UserPlanUser").value.Trim();
    var TotalMoneyMin = document.getElementById("txtTotalMoneyMin").value.Trim();
    var TotalMoneyMax = document.getElementById("txtTotalMoneyMax").value.Trim();
    var DeptID = document.getElementById("txtDeptID").value.Trim();
    var DeptName = $("#DeptName").val();
    var StartPlanDate = document.getElementById("txtStartPlanDate").value.Trim();
    var EndPlanDate = document.getElementById("txtEndPlanDate").value.Trim();
    var FlowStatus = document.getElementById("ddlFlowStatus").value.Trim();
    var BillStatus = document.getElementById("ddlBillStatus").value.Trim();
    
    strSelect += "&No="+PlanNo;
    strSelect += "&Title="+PlanTitle;
    strSelect += "&PlanUser="+PlanUser;
    strSelect += "&PlanUserName="+PlanUserName;
    strSelect += "&TotalMoneyMin="+TotalMoneyMin;
    strSelect += "&TotalMoneyMax="+TotalMoneyMax;
    strSelect += "&DeptID="+DeptID;
    strSelect += "&DeptName="+DeptName;
    strSelect += "&StartPlanDate="+StartPlanDate;
    strSelect += "&EndPlanDate="+EndPlanDate;
    strSelect += "&FlowStatus="+FlowStatus;
    strSelect += "&BillStatus="+BillStatus;
      var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;
           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;

 strSelect += "&EFIndex="+escape(EFIndex);
    strSelect += "&EFDesc="+escape(EFDesc); 
    
    $("#SearchCondition").val(strSelect);  
}

function fnSetSelectCondition(requestobj)
{
 
    var PlanNo=requestobj['No'];
    var PlanTitle=requestobj['Title'];  
    var PlanUserName=requestobj['PlanUserName'];      
    var PlanUser=requestobj['PlanUser'];      
    var TotalMoneyMin=requestobj['TotalMoneyMin'];      
    var TotalMoneyMax=requestobj['TotalMoneyMax'];      
    var DeptName=requestobj['DeptName'];      ;
    var DeptID=requestobj['DeptID'];      
    var StartPlanDate=requestobj['StartPlanDate'];      
    var EndPlanDate=requestobj['EndPlanDate'];      
    var FlowStatus=requestobj['FlowStatus'];      
    var BillStatus=requestobj['BillStatus'];
    pageCount=parseInt(requestobj['pageCount']);
    currentPageIndex=parseInt(requestobj['pageIndex']);
    if (PlanNo!="undefined")
    document.getElementById("txtPlanNo").value = PlanNo;
       if (PlanTitle!="undefined")
    document.getElementById("txtPlanTitle").value = PlanTitle;
       if (PlanUserName!="undefined")
    document.getElementById("UserPlanUser").value = PlanUserName;
       if (PlanUser!="undefined")
    document.getElementById("PlanUserID").value = PlanUser;
       if (TotalMoneyMin!="undefined")
    document.getElementById("txtTotalMoneyMin").value = TotalMoneyMin;
       if (TotalMoneyMax!="undefined")
    document.getElementById("txtTotalMoneyMax").value = TotalMoneyMax;
       if (DeptName!="undefined")
    document.getElementById("DeptName").value = DeptName
       if (DeptID!="undefined")
    document.getElementById("txtDeptID").value = DeptID;
       if (StartPlanDate!="undefined")
    document.getElementById("txtStartPlanDate").value = StartPlanDate;
       if (EndPlanDate!="undefined")
    document.getElementById("txtEndPlanDate").value = EndPlanDate;
       if (FlowStatus!="undefined")
    document.getElementById("ddlFlowStatus").value = FlowStatus;
       if (BillStatus!="undefined")
    document.getElementById("ddlBillStatus").value = BillStatus;
}

//************************************************End   界面取值与赋值**************************************************


// 
function fnCheck()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    
    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
	        msgText = msgText +RetVal+  "不能含有特殊字符|";
	         if(!isFlag)
      {
         popMsgObj.Show(fieldText,msgText);
           } 
           return isFlag;  
    }

   
   if(CompareDate($("#txtStartPlanDate").val(), $("#txtEndPlanDate").val())==1)
   {
        isFlag=false;
        fieldText=fieldText + "计划时间|";
        msgText = msgText +  "起始时间不能大于终止时间|";
   }
   if(($("#txtTotalMoneyMin").val()!="") && (!IsNumberOrNumeric($("#txtTotalMoneyMin").val(),12,2)))
   {
        isFlag=false;
        fieldText=fieldText + "预控金额下限|";
        msgText = msgText +  "请输入正确的预控金额下限|";
   }
   if(($("#txtTotalMoneyMax").val()!="") && (!IsNumberOrNumeric($("#txtTotalMoneyMax").val(),12,2)))
   {
        isFlag=false;
        fieldText=fieldText + "预控金额上限|";
        msgText = msgText +  "请输入正确的预控金额上限|";
   }
   if(IsNumberOrNumeric($("#txtTotalMoneyMin").val(),12,2) && IsNumberOrNumeric($("#txtTotalMoneyMax").val(),12,2) 
   && parseFloat($("#txtTotalMoneyMin").val()) > parseFloat($("#txtTotalMoneyMax").val()))
   {
        isFlag=false;
        fieldText=fieldText + "预控金额|";
        msgText = msgText +  "预控金额下限不能大于预控金额上限|";
   }
   if(!isFlag)
   {
        popMsgObj.Show(fieldText,msgText);
   } 
   return isFlag;   
}

//将隐藏域中的检索条件和分页信息返回，作为URL
function fnGetAllCondition(pageIndex)
{
    var strSelect = $("#SearchCondition").val();
    strSelect += "&pageCount="+pageCount;
    strSelect += "&pageIndex="+pageIndex;
    strSelect += "&orderBy="+orderBy;
    strSelect += "&ActionPlan=Select";
    strSelect += "&SourcePage=PlanInfo";
    return strSelect;
}

function DoSearch()
{
    fnGetSelectCondition();//将检索条件放入隐藏域

    TurnToPage(1);
}

function TurnToPage(pageIndex)
{//检索
    if(!fnCheck())
    return;
    currentPageIndex = pageIndex;
    //检索条件
    document.getElementById("checkall").checked = false;
    var URLParams = fnGetAllCondition(pageIndex);
    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchasePlanInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#PurPlanBill tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox"+j+"' onclick=\"IfSelectAll('Checkbox1','checkall')\"  name='Checkbox1'  value="+item.ID+"  class="+item.PlanNo+" type='checkbox'/>"+"</td>"+
//                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+
                        "<td height='22' align='center' title=\""+item.PlanNo+"\"> <a href='#' onclick=SelectPurchasePlan('"+item.ID+"')>" + GetStandardString(item.PlanNo,25) + "</a></td>"+
                        "<td height='22' align='center' title=\""+item.PlanTitle+"\">"+ GetStandardString(item.PlanTitle,15) +"</td>"+
                        "<td height='22' align='center'>"+ item.PlanUserName +"</td>"+ 
                        "<td height='22' align='center'>"+    (parseFloat(item.PlanMoney)).toFixed($("#HiddenPoint").val())+"</td>"+
                        "<td height='22' align='center'>"+ item.PlanDate +"</td>"+
                        "<td height='22' align='center' style='display:none'>"+ item.BillStatus +"</td>"+
                        "<td height='22' align='center' id='BillStatusName"+j+"'>"+ item.BillStatusName +"</td>"+
                        "<td height='22' align='center' id='FlowStatusName"+j+"'>"+ item.FlowStatusName +"</td>"
                        ).appendTo($("#PurPlanBill tbody"));
                        ++j;
                   });
                    //页码
                   ShowPageBar("PurPlanPage1",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  //$("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagePurPlancount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.getElementById("Text2").value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}



function Confirm()
{
    var ck = document.getElementsByName("Checkbox1");
    
    var table=document.getElementById("PurPlanBill");
    var URLParams = "";  
    var ActionPlan = "Confirm";
    URLParams+="ActionPlan="+ActionPlan;
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            if(table.rows[ck[i].value].cells[7].innerText !='1'&&table.rows[ck[i].value].cells[7].innerText !='3')
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能确认制单和变更状态的单据！");
                return;
            }
            URLParams+="&PlanNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;//PlanNo
            
        }       
    }
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一条或几条数据来确认！");
        return;
    }
    URLParams+="&Length="+index+"";
        
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchasePlanInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    searchdata();
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){}
           });

}

function DeletePurPlan()
{

var signFrame = document.getElementById("PurPlanBill");
//    var IDs = "(";
//    var PlanNos = "(";
    var IDs = "";
    var PlanNos = "";
    
    var isErrorFlag = false;
    var msgText = "";
    
    var Count = 0;
    
    for(var i=1;i<signFrame.rows.length;++i)
    {
        if(document.getElementById("Checkbox"+i).checked == true)
        {
            var aaa =document.getElementById("BillStatusName"+i).innerHTML;
            var bbb = document.getElementById("FlowStatusName"+i).innerHTML;
            if(( aaa== "制单")&&( bbb== ""))
            {//可以删
                IDs += document.getElementById("Checkbox"+i).value;
                IDs += ",";
                PlanNos +="'";
                PlanNos +=document.getElementById("Checkbox"+i).className;
                PlanNos +="'";
                PlanNos +=",";
                Count++;
            } 
            else
            {//不可以删
                isErrorFlag = true;
                
            }           
        }
        
    }
    IDs=IDs.slice(0,IDs.length-1);
    PlanNos=PlanNos.slice(1,PlanNos.length-2);
    
//    IDs +=")";
//    PlanNos +=")";
    if(isErrorFlag)
    {msgText += "已提交审批或已确认后的单据不允许删除！";
        popMsgObj.ShowMsg(msgText);
        return;
    }
    if(Count == 0)
    {
        msgText +="请选择要删除的采购计划！";
        popMsgObj.ShowMsg(msgText);
        return;
    }
    if(!confirm("确认执行删除操作么？"))
    return;
    var ActionPlan = "Delete";
    var URLParams = "";
    URLParams += "&ActionPlan="+ActionPlan;
    URLParams += "&IDs="+IDs;
    URLParams += "&PlanNos="+PlanNos;

    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchasePlanInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    searchdata();
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){TurnToPage(1); popMsgObj.ShowMsg("删除成功！"); }
           });
}

function ClosePurPlan()
{
    var ck = document.getElementsByName("Checkbox1");
    
    var table=document.getElementById("PurPlanBill");
    var URLParams = "";  
    var ActionPlan = "Close";
    URLParams+="ActionPlan="+ActionPlan;
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            if(table.rows[ck[i].value].cells[7].innerText !='2')
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能对执行状态的单据进行结单操作！");
                return;
            }
            URLParams+="&PlanNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;//PlanNo
            
        }       
    }
    if(index == 0)
    {
        popMsgObj.ShowMsg("请选择一条或几条数据来结单！");
        return;
    }
    URLParams+="&Length="+index+"";
        
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchasePlanInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    searchdata();
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){}
           });
}

function CancelClosePurPlan()
{//取消结单
    var ck = document.getElementsByName("Checkbox1");
    
    var table=document.getElementById("PurPlanBill");
    var URLParams = "";  
    var ActionPlan = "CancelClose";
    URLParams+="ActionPlan="+ActionPlan;
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            if(table.rows[ck[i].value].cells[7].innerText !='4'&&table.rows[ck[i].value].cells[7].innerText !='5')
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能对结单状态的单据进行取消结单操作！");
                return;
            }
            URLParams+="&PlanNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;//PlanNo
            
        }       
    }
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一条或几条数据来取消结单！");
        return;
    }
    URLParams+="&Length="+index+"";
        
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchasePlanInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    searchdata();
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){}
           });
}

function ApprovePurPlan()
{//审批

}


function SubmissionApprovePurPlan()
{//提交审批

}
function searchdata()
{
      TurnToPage(1);
}
function Ifshow(count)
{
    if(count=="0")
    {
       document.getElementById("divpage").style.display = "none";
        document.getElementById("pagePurPlancount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagePurPlancount").style.display = "block";
    }
}

function pageDataList1(o,a,b,c,d)
{
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++)
	{
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    //判断是否是数字
    if (!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    }
    if (!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    }
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1)
    {
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转至页数超出查询范围！");
    }
    else
    {
        ifshow="0";
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
    //排序
    function OrderBy(orderColum,orderTip)
    {
        if($("#SearchCondition").val() == "")
            return;
        ifshow="0";
        var ordering = "a";
        var ooo = "ASC ";
        //var orderTipDOM = $("#"+orderTip);
        var allOrderTipDOM  = $(".orderTip");
        if( $("#"+orderTip).html()=="↓")
        {
             allOrderTipDOM.empty();
             $("#"+orderTip).html("↑");
        }
        else
        {
            ordering = "d";
            allOrderTipDOM.empty();
            $("#"+orderTip).html("↓");
            ooo = "DESC";
        }
        orderBy = orderColum+"_"+ordering;
        ooo = orderColum+" "+ooo;
        $("#hidOrderBy").val(ooo); 
        TurnToPage(1);
    }

function ClearInput()
{
    $("#PurPlanBill tbody").find("tr.newrow").remove();
    document.getElementById("txtPlanNo").value = "";
    document.getElementById("txtPlanTitle").value = "";
    document.getElementById("PlanUserID").value = "";
    document.getElementById("UserPlanUser").value = "";
    document.getElementById("txtTotalMoneyMin").value = "";
    document.getElementById("txtTotalMoneyMax").value = "";
    document.getElementById("txtDeptID").value = "";
    document.getElementById("DeptName").value = "";
    document.getElementById("txtStartPlanDate").value = "";
    document.getElementById("txtEndPlanDate").value = "";
    document.getElementById("ddlFlowStatus").value = "0";
    document.getElementById("ddlBillStatus").value = "0";
    
    document.getElementById("pagePurPlancount").style.display = "none";
//    document.getElementById("divpage").style.display = "none";
    document.getElementById("PurPlanPage1").style.display = "none";
}

function SelectPurchasePlan(ID)
{
    var URLParams = $("#SearchCondition").val();
    URLParams += "&ID="+ID;
    URLParams += "&pageCount="+pageCount+"&pageIndex="+currentPageIndex+"&SourcePage=PlanInfo";
    URLParams += "&ModuleID=2041501";
    window.location.href='PurchasePlan_Add.aspx?'+URLParams;
}

function DoNew()
{
    var URLParams = $("#SearchCondition").val();
    URLParams += "&ModuleID=2041501";
    URLParams += "&pageCount="+pageCount;
    URLParams += "&pageIndex="+currentPageIndex;
    URLParams += "&orderBy="+orderBy;
    URLParams += "&ActionApply=Select";
    URLParams += "&SourcePage=PlanInfo";
    window.location.href = 'PurchasePlan_Add.aspx?'+URLParams;
}