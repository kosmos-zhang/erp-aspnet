var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式

var currentPageIndex = 1;
var action = "";//操作
var orderBy = "ModifiedDate_d";//排序字段

$(document).ready(function() {
    //DoSearch();
    var requestObj = GetRequest(location.search);
    var SourcePage = requestObj['SourcePage'];
    if (SourcePage == "Add") {//从编辑页面过来，设置检索条件
        if (requestObj["SourcePage"] != null) {//编号传过去了，说明点击了检索，需要设置检索条件

            fnSetSelectCondition(requestObj);
            fnGetSelectCondition();
            TurnToPage(currentPageIndex);
        }
        else {
        }
    }
    IsDiplayOther('GetBillExAttrControl1_SelExtValue', 'GetBillExAttrControl1_TxtExtValue');
});

function OrderBy(orderColum, orderTip) {
    if($("#SearchCondition").val() =="")
        return;
    var ordering = "a";
    var ooo = "ASC";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM = $(".orderTip");
    if ($("#" + orderTip).html() == "↓") {
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↑");
    }
    else {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↓");
        ooo = "DESC";
    }
    orderBy = orderColum + "_" + ordering;
    ooo = orderColum+" "+ooo;
    $("#hidOrderBy").val(ooo);
    TurnToPage(1);
}


//********************************************Start 界面取值和赋值************************************************
//将检索条件放入隐藏域
function fnGetSelectCondition()
{
    var strSelect = "";
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;
           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;
    strSelect += "&ApplyNo="+document.getElementById("txtapplyno").value.Trim();
    strSelect += "&Title="+document.getElementById("txttitle").value.Trim();
    strSelect += "&ApplyUserID="+document.getElementById("hidApplyID").value.Trim();
    strSelect += "&ApplyUserName="+$("#UserApplyName").val();
    strSelect += "&ApplyDeptID="+document.getElementById("hidDeptID").value.Trim();
    strSelect += "&ApplyDeptName="+$("#DeptName").val();
    strSelect += "&TypeID="+document.getElementById("ddlTypeID_ddlCodeType").value.Trim();
    
    strSelect += "&FromType="+document.getElementById("ddlfromtype").value.Trim();
    strSelect += "&StartApplyDate="+document.getElementById("txtstartapplydate").value.Trim();
    strSelect += "&EndApplyDate="+document.getElementById("txtendapplydate").value.Trim();
    strSelect += "&BillStatus="+document.getElementById("ddlBillStatus").value.Trim();
    strSelect += "&FlowStatus="+document.getElementById("ddlFlowStatus").value.Trim(); 
      strSelect += "&EFIndex="+escape(EFIndex);
    strSelect += "&EFDesc="+escape(EFDesc); 
    $("#SearchCondition").val(strSelect);  
}

//设置检索条件
function fnSetSelectCondition(requestobj)
{ 
    $("#txtapplyno").val(requestobj['ApplyNo']);
    $("#txttitle").val(requestobj['Title']); 
    $("#hidApplyID").val(requestobj['ApplyUserID']); 
    $("#UserApplyName").val(requestobj['ApplyUserName'])
    $("#hidDeptID").val(requestobj['ApplyDeptID']); 
    $("#DeptName").val(requestobj['ApplyDeptName']);
    $("#ddlTypeID_ddlCodeType").val(requestobj['TypeID']); 
    
    $("#ddlfromtype").val(requestobj['FromType']); 
    $("#txtstartapplydate").val(requestobj['StartApplyDate']); 
    $("#txtendapplydate").val(requestobj['EndApplyDate']); 
    $("#ddlbillstatus").val(requestobj['BillStatus']); 
    $("#ddlFlowStatus").val(requestobj['FlowStatus']);
 
      
       $("#GetBillExAttrControl1_SelExtValue").val(requestobj['EFIndex']); 
    $("#GetBillExAttrControl1_TxtExtValue").val(requestobj['EFDesc']);
    pageCount = parseInt(requestobj['pageCount']); 
    currentPageIndex = parseInt(requestobj['pageIndex']);

}
//********************************************End   界面取值和赋值************************************************


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
    }

   
   if(CompareDate($("#txtstartapplydate").val(), $("#txtstartapplydate").val())==1)
   {
        isFlag=false;
        fieldText=fieldText + "申请时间|";
        msgText = msgText +  "起始时间不能大于终止时间|";
   }
   if(!isFlag)
   {
        popMsgObj.Show(fieldText,msgText);
   }
   return isFlag;    
}

//*******************************************Start 检    索***************************************************
function DoSearch()
{
    if(!fnCheck())
    return;

    fnGetSelectCondition();//将检索条件放入隐藏域

    TurnToPage(1);
}

function TurnToPage(pageIndex)
{
    currentPageIndex = pageIndex;
    document.getElementById("checkall").checked = false;
    
//    fnGetSelectCondition();
    var URLParams = $("#SearchCondition").val();
    
    //分页和排序信息
    URLParams += fnGetPageInfo();
    
    URLParams += "&ActionOrder=Select";
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseApplyInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox"+(i + 1)+"' name='Checkbox'  value="+item.ID+"  onclick=\"IfSelectAll('Checkbox','checkall')\" type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' id='ApplyNo"+ (i + 1) +"' title=\""+item.ApplyNo+"\"><a href='#' onclick=DoSelect('"+item.ID+"')>"+ GetStandardString(item.ApplyNo,25) +"</a></td>"+
                        "<td height='22' align='center'style=\"display:none\" id='applynohidden"+(i + 1)+"'>"+ item.ApplyNo +"</td>"+
                        "<td height='22' align='center' title=\""+item.Title+"\">"+ GetStandardString(item.Title,15) +"</td>"+
                        "<td height='22' align='center'>"+ item.TypeName +"</td>"+
                        "<td height='22' align='center'>"+ item.FromTypeName +"</td>"+
                        "<td height='22' align='center' >"+ item.ApplyUserName +"</td>"+
                        "<td height='22' align='center' id='ApplyDeptName"+(i + 1)+"'>"+ item.ApplyDeptName +"</td>"+
                        "<td height='22' align='center'>"+ item.ApplyDate +"</td>"+
                        "<td height='22' align='center'id='BillStatusName"+(i + 1)+"'>"+ item.BillStatusName +"</td>"+
                        "<td height='22' align='center' id='FlowStatusName"+(i + 1)+"'>"+ item.FlowStatusName +"</td>").appendTo($("#pageDataList1 tbody")
                        );
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页", onclick:"TurnToPage({pageindex});return false;"}//[attr]
                      );
                     totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagePurApplycount"));
                  $("#ToPage").val(pageIndex);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
      });
}

//获取页码和排序信息
function fnGetPageInfo()
{
    var Info = "&pageCount="+pageCount;
    Info += "&pageIndex="+currentPageIndex;
    Info += "&orderBy="+orderBy;
    return Info;
}
//*******************************************End   检    索***************************************************


//获取url中"?"符后的字串
function GetRequest(url)
{
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



//全选
function SelectAll() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}





function DoSelect(ID)
{

dodid(ID);
//var state=document.readyState;
//    if (state=="complete")
//   {
// setTimeout("dodid("+ID+")",500);
//   }
//   
}

function dodid(ID)
{

 var URLParams = "PurchaseApply_Add.aspx?";
    URLParams += "ModuleID=2041301&SourcePage=Info";
    URLParams += "&ID="+escape(ID);
    
    URLParams += fnGetPageInfo();
    
    URLParams += $("#SearchCondition").val();

  

 
    window.location.href=URLParams;
}
function pageDataList1(o,a,b,c,d){
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

function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagePurApplycount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagePurApplycount").style.display = "block";
    }
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    if(!IsNumber(newPageIndex) || newPageIndex==0)
    {
        isFlag = false;
        fieldText = fieldText + "跳转页面|";
	    msgText = msgText +  "必须为正整数格式|";
    }
    if(!IsNumber(newPageCount) || newPageCount==0)
    {
        isFlag = false;
        fieldText = fieldText + "每页显示|";
	    msgText = msgText +  "必须为正整数格式|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return;
    }
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}

function DoDelete()
{
    var signFrame = document.getElementById("pageDataList1");
    var IDs = "";
    var ApplyNos = "";
    
    var isErrorFlag = false;
    var msgText = "";
    
    var Count = 0;
    
    for(var i=1;i<signFrame.rows.length;++i)
    {
        if(document.getElementById("Checkbox"+i).checked == true)
        {
            var aaa =document.getElementById("BillStatusName"+i+"").innerHTML;
            var bbb = document.getElementById("FlowStatusName"+i+"").innerHTML;
            if(( aaa== "制单")&&( bbb== ""))
            {//可以删
                IDs += document.getElementById("Checkbox"+i).value;
                IDs += ",";
                ApplyNos +="'";
                ApplyNos +=document.getElementById("applynohidden"+i).innerHTML;
                ApplyNos +="'";
                ApplyNos +=",";
                Count++;
            } 
            else
            {//不可以删
                isErrorFlag = true;
                msgText += "已提交审批或已确认后的单据不允许删除！";
            }           
        }
        
    }
    IDs=IDs.slice(0,IDs.length-1);
    ApplyNos=ApplyNos.slice(1,ApplyNos.length-2);
    if(isErrorFlag)
    {
        popMsgObj.ShowMsg(msgText);
        return;
    }
    if(Count == 0)
    {
        msgText +="请选择要删除的采购申请！";
        popMsgObj.ShowMsg(msgText);
        return;
    }
    
    if(!confirm("确定执行删除操作么？"))
        return;
    var ActionApply = "Delete";
    var URLParams = "";
    URLParams += "&ActionApply="+ActionApply;
    URLParams += "&IDs="+IDs;
    URLParams += "&ApplyNos="+ApplyNos;
    
    
    
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseApplyInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());
                msgText +="删除成功！";
                popMsgObj.ShowMsg(msgText);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
      });
    
}

function DoNew()
{
    var URLParams = $("#SearchCondition").val();
    URLParams += "&SourcePage="+"Info";
    URLParams += "&ModuleID=2041301";
    
    URLParams += fnGetPageInfo();
    window.location.href = 'PurchaseApply_Add.aspx?'+URLParams; 
    //   window.location.href = 'PurchaseApplyAdd.aspx?ModuleID=2041301' ;
}
