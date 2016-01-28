$(document).ready(function(){
Fun_Search_UserInfo();
    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var flag="";
     var ActionFlag=""
     var str="";
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
   
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {    
          document.getElementById("btnAll").checked=false;
          var TypeFlag=document.getElementById("hf_TableName").value;
           var serch=document.getElementById("hidSearchCondition").value;
           ActionFlag="Search"
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/CodeReasonFeeList.ashx?str='+str,//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&TypeFlag="+escape(TypeFlag)+"&ActionFlag="+escape(ActionFlag)+"&" + serch,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                     var tempReason='';
                        if(item.ID != null && item.ID != "")
                        {   
                         if(TypeFlag=="officedba.CodeReasonType")
                          {
                         if(item.Flag==1)
                            tempReason='出入库原因';
                            else if(item.Flag==2)
                        tempReason='借货原因';
                           else if(item.Flag==3)
                        tempReason='库存调整原因';
                           else if(item.Flag==4)
                        tempReason='库存调拨原因';
                           else if(item.Flag==5)
                        tempReason='库存报损原因';
                           else if(item.Flag==20)
                        tempReason='销售退货原因';
                           else if(item.Flag==21)
                        tempReason='采购退货原因';
                         else if(item.Flag==22)
                        tempReason='采购原因';
                         else if(item.Flag==6)
                        tempReason='不合格原因';
                        }
                        else if(TypeFlag=="officedba.CodeUnitType")
                        {
                          if(item.Flag==1)
                            tempReason='数量';
                            else if(item.Flag==2)
                        tempReason='重量';
                           else if(item.Flag==3)
                        tempReason='长度';
                           else if(item.Flag==4)
                        tempReason='面积';
                        else if(item .Flag==5)
                        tempReason='体积';
                        }
                        else if(TypeFlag=="officedba.CodeFeeType")
                        {
                         tempReason=item.Flag;
                        } 
                        if(item.UsedStatus=='0')
                        item.UsedStatus='停用';
                        else if(item.UsedStatus=='1')
                        item.UsedStatus='启用';
                        if(TypeFlag=="officedba.CodeUnitType")
                        {
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' title=\""+item.CodeName+"\"><a href='#' onclick=\"Show('"+TypeFlag+"','"+item.CodeName+"','"+tempReason+"','"+item.Description+"','"+item.UsedStatus+"','"+item.ID+"','"+item.Publicflag+"','"+item.CodeSymbol+"');\">"+ fnjiequ(item.CodeName,10)+"</a></td>"+
                        "<td height='22' align='center'>" + tempReason + "</td>"+
                         "<td height='22' align='center'>" + item.UsedStatus + "</td>"+
                        "<td height='22' align='center'>"+item.CodeSymbol+"</td>").appendTo($("#pageDataList1 tbody"));
                        }
                        else if(TypeFlag=="officedba.CodeReasonType")
                        $("<tr class='newrow'></tr>").append("<td height='22'width='80' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' title=\""+item.CodeName+"\"><a href='#' onclick=\"Show('"+TypeFlag+"','"+item.CodeName+"','"+tempReason+"','"+item.Description+"','"+item.UsedStatus+"','"+item.ID+"','"+item.Publicflag+"');\">"+fnjiequ(item.CodeName,10)+"</a></td>"+
                        "<td height='22' align='center'>" + tempReason + "</td>"+
                        "<td height='22' align='center'>"+item.UsedStatus+"</td>").appendTo($("#pageDataList1 tbody"));
                        else if(TypeFlag=="officedba.CodeFeeType")
                        $("<tr class='newrow'></tr>").append("<td height='22'width='80' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' title=\""+item.CodeName+"\"><a href='#' onclick=\"Show('"+TypeFlag+"','"+item.CodeName+"','"+tempReason+"','"+item.Description+"','"+item.UsedStatus+"','"+item.ID+"','"+item.Publicflag+"','"+item.SubjectsName+"','"+item.FeeSubjectsNo+"');\">"+fnjiequ(item.CodeName,10)+"</a></td>"+
                        "<td height='22' align='center' title=\""+item.SubjectsName+"\">"+fnjiequ(item.SubjectsName,10)+"</td>"+
                        "<td height='22' align='center'>" + tempReason + "</td>"+
                        "<td height='22' align='center'>"+item.UsedStatus+"</td>").appendTo($("#pageDataList1 tbody"));
                        }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                   ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                   
                    document.getElementById('Text2').value=msg.totalCount;
//                   document.getElementById('Text2').value=msg.totalCount;
//                  document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=1;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Fun_Search_UserInfo(aa)
{
  var TypeFlag=document.getElementById("hf_TableName").value;
 var name='';
 if(TypeFlag=="officedba.CodeReasonType")
 {
 name="原因名称不能为特殊字符！";
 }
   else if(TypeFlag=="officedba.CodeUnitType")
   {
    name="计量单位名称不能为特殊字符！";
   }
     else if(TypeFlag=="officedba.CodeFeeType")
     {
     name="费用名称不能为特殊字符！";
     }
     if(document.getElementById("txt_typeflagname").value!="")
    if(!CheckSpecialWord(document.getElementById("txt_typeflagname").value))
    {
     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",name);
           return;
    }
    var search = "";
          var name= document.getElementById("txt_typeflagname").value;
           var UsedStatus=document .getElementById("UsedStatus").value;
           var seltype=document.getElementById("seltype").value;
           var SubNo=$("#HiddenSubNo").val();
          search+="TypeName="+escape(name);
          search+="&SubNo="+escape(SubNo);
          search+="&UsedStatus="+escape(UsedStatus); 
          search+="&seltype="+escape(seltype); 
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    TurnToPage(1);
}
 function DelCodePubInfo()
   {
   
        var c=window.confirm("确认执行删除操作吗？")
        if(c==true)
        {
         var TypeFlag=document.getElementById("hf_TableName").value;
        var ck = document.getElementsByName("Checkbox1");
        var TableName=TypeFlag;
        var x=Array(); 
        var ck2 = "";
        var str="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
            }
        }
        x=ck2;
        var str = ck2.substring(0,ck2.length-1);
        if(x.length-1<1)
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除列表信息至少要选择一项！");
        else
          {
         ActionFlag="Del";
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/SupplyChain/CodeReasonFeeList.ashx?str="+str,
                  dataType:'json',//返回json格式数据
                  data: "ActionFlag="+escape(ActionFlag)+"&TableName="+escape(TableName),//数据
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
                        popMsgObj.ShowMsg(data.info);
                        Fun_Search_UserInfo();
                        document.getElementById("btnAll").checked=false;
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
            
          }
        }  
        else   return   false;   
   
   }
function Ifshow(count)
    {
        if(count=="0")
        {
        document.getElementById('divpage').style.display = "none";
        document.getElementById('pagecount').style.display = "none";
        }
        else
        {
         document.getElementById('divpage').style.display = "block";
        document.getElementById('pagecount').style.display = "block";
        }
    }
    

    
//    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
      if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
          return;
       }
       if(newPageCount <=0 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","显示页数超出显示范围！");
            return false;
        }
        if(newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
            document.getElementById("btnAll").checked=false;
        }
    }
//    //排序
    function OrderBy(orderColum,orderTip)
    {
        var ordering = "a";
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
        }
        orderBy = orderColum+"_"+ordering;
        TurnToPage(1);
    }

		function CloseDiv(){
	 closeRotoscopingDiv(false,'divBackShadow');
	}


function Show(TypeFlag,CodeName,Flag,Description,UsedStatus,ID,Publicflag,CodeSymbol,FeeSubjectsNo)
{  
   flag="1";
   openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
   document.getElementById("div_Add").style.zIndex="2";
   document.getElementById("divBackShadow").style.zIndex="1";
   document.getElementById('div_Add').style.display='block';
   document.getElementById("sel_type").selectedIndex=0;
   document.getElementById("sel_type").disabled=false;
    if(typeof(CodeName)!="undefined" )
    {
      document.getElementById("txtPlanNoHidden").value=CodeName;
      document.getElementById("txt_name").value=CodeName;
      document.getElementById("sel_type").value=Publicflag;
      document.getElementById("txt_Description").value=Description;
      document.getElementById("hf_ID").value=ID;
      document.getElementById("sel_type").disabled=true;
      if(TypeFlag=="officedba.CodeUnitType")
      {
        document.getElementById("txt_CodeSymbol").value=CodeSymbol;
        document.getElementById("hfunitcy").value=CodeSymbol;
      }
      
      if(TypeFlag=="officedba.CodeFeeType")
      {
        document.getElementById("TxtSubject").value=CodeSymbol;
        document.getElementById("FeeSubjectsNo").value=FeeSubjectsNo;
      }
      if(UsedStatus=="启用")
      {
      document.getElementById("rd_use").checked=true;
      }
      else if(UsedStatus=="停用")
      {
         document.getElementById("rd_notuse").checked=true;
      }
      flag="2";
    }
}


function InsertCodeReasonFee()
{
 var TypeFlag=document.getElementById("hf_TableName").value;
   var fieldText = "";
   var msgText = "";
   var isFlag = true;
   var UseStatus=null;
   var cy="";
   var CodeSymbol="";
    if(flag=="1")
    {
      ActionFlag="Add"
    }
   else if(flag=="2")
   {
      ActionFlag="Edit";
   }
  var CodeName=trim(document.getElementById("txt_name").value);
  var Description=document.getElementById("txt_Description").value;
  var ReasonFlag=document.getElementById("sel_type").value;
    if(TypeFlag=="officedba.CodeUnitType")
    {
     if(CodeName=="")
    {
        isFlag = false;
        fieldText = fieldText + "计量单位名称|";
   		msgText = msgText +  "请输入计量单位名称|";
   		
    }
      if(strlen(CodeName)>50)
    {
        isFlag = false;
        fieldText = fieldText + "计量单位名称|";
   		msgText = msgText +  "名计量单位名称仅限于50个字符以内|";      
    }
    }
    else if(TypeFlag=="officedba.CodeFeeType")
  {
    if(CodeName=="")
    {
     isFlag = false;
        fieldText = fieldText + "费用名称|";
   		msgText = msgText +  "请输入费用名称|";
    }
     if(strlen(CodeName)>50)
    {
        isFlag = false;
        fieldText = fieldText + "费用名称|";
   		msgText = msgText +  "名费用名称仅限于50个字符以内|";      
    }
  }
   else if(TypeFlag=="officedba.CodeReasonType")
  {
    if(CodeName=="")
    {
     isFlag = false;
        fieldText = fieldText + "原因名称|";
   		msgText = msgText +  "请输入原因名称|";
    }
      if(strlen(CodeName)>50)
    {
        isFlag = false;
        fieldText = fieldText + "原因名称|";
   		msgText = msgText +  "原因名称仅限于50个字符以内|";      
    }
  }
    if(Description!="")
    {
     if(strlen(Description)>200)
     {
       isFlag = false;
        fieldText = fieldText + "描述信息|";
   		msgText = msgText +  "描述信息仅限于200个字符以内|";   
     }
    }
   if(TypeFlag=="officedba.CodeUnitType")
  {
    cy=document.getElementById("hfunitcy").value;
    CodeSymbol=document.getElementById("txt_CodeSymbol").value;
    if(CodeSymbol!="")
    {
     if(strlen(CodeSymbol)>50)
     {
        isFlag = false;
        fieldText = fieldText + "计量单位符号|";
   		msgText = msgText +  "计量单位符号仅限于50个字符以内|";   
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
      var Description=trim(document.getElementById("txt_Description").value);
     if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return;
    }
  var TableName=TypeFlag;
  var ID= document.getElementById("hf_ID").value;
  var Name=document.getElementById("txtPlanNoHidden").value;
  var FeeSubjectsNo=$("#FeeSubjectsNo").val();//对应会计科目编码
  if(document.getElementById("rd_use").checked)
  {
   UseStatus="1";
  }
  else if(document.getElementById("rd_notuse").checked)
  {
     UseStatus="0";
  }
    $.ajax({ 
                  type: "POST",
                 url: "../../../Handler/Office/SupplyChain/CodeReasonFeeList.ashx?str="+str,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  data: "ActionFlag="+escape(ActionFlag)+"&CodeName="+escape(CodeName)+"&FeeSubjectsNo="+escape(FeeSubjectsNo)+"&Description="+escape(Description)+"&ReasonFlag="+escape(ReasonFlag)+"&UseStatus="+escape(UseStatus)+"&ID="+ID+"&TableName="+TableName+"&CodeSymbol="+escape(CodeSymbol)+"&Name="+escape(Name)+"&cy="+escape(cy),//数据
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
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        Fun_Search_UserInfo();
                        Hide();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
}
    function Hide()
    {
     CloseDiv();
     document.getElementById('div_Add').style.display='none';
     New();
    }
    function New()
    {
     document.getElementById("txt_name").value="";
     document.getElementById("txt_Description").value="";
    document.getElementById("rd_use").checked=true;
    document.getElementById("sel_type").value="";
      var TypeFlag=document.getElementById("hf_TableName").value;
    if(TypeFlag=="officedba.CodeUnitType")
    {
      document.getElementById("txt_CodeSymbol").value="";
    }
   
    }
    function ClearQueryInfo()
    {
        $(":text").each(function(){ 
    this.value=""; 
    }); 
     document.getElementById("UsedStatus").value='1';
     
     
        $("#pageDataList1 tbody").find("tr.newrow").remove();
    }
    function OptionCheckAll()
{

  if(document.getElementById("btnAll").checked)
  {
     var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=true ;
        }
  }
  else if(!document.getElementById("btnAll").checked)
  {
    var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=false ;
        }
  }
}
//选择会计科目
function SelectsAccounts(controlName,controlidCD)
{
   var url="../../../Pages/Office/FinanceManager/SubjectsList.aspx?IsShow=NotShow&CheckNode=CheckNode";
   
   var returnValue = window.showModalDialog(url, "", "dialogWidth=700px;dialogHeight=500px");
   if(returnValue != "" && returnValue != null)
   {
        var info=returnValue.split("|");
        document.getElementById(controlName).value = info[1].toString();
        document.getElementById(controlidCD).value = info[0].toString();
   }
}
function checkonly()
{
 
}
function SelectsAccountsForSearch(controlName,controlidCD)
{
   var url="../../../Pages/Office/FinanceManager/SubjectsList.aspx?IsShow=NotShow&CheckNode=CheckNode";
   
   var returnValue = window.showModalDialog(url, "", "dialogWidth=700px;dialogHeight=500px");
   if(returnValue != "" && returnValue != null)
   {
        var info=returnValue.split("|");
        document.getElementById(controlName).value = info[1].toString();
        document.getElementById(controlidCD).value = info[0].toString();
   }
}
function clearinput(controlName,controlidCD)
{
    document.getElementById(controlName).value = "";
    document.getElementById(controlidCD).value = "";
}