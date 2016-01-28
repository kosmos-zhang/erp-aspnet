
$(document).ready(function() {

    IsDiplayOther('GetBillExAttrControl1_SelExtValue');//扩展属性
});


/*重写toFixed*/
Number.prototype.toFixed = function(d) {
    return FormatAfterDotNumber(this, parseInt($("#hidSelPoint").val()));
}

var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
        var action = "";//操作
        
 //翻页
function TurnToPage(pageIndex)
{

    document.getElementById("txtPageIndex").value=pageIndex;
    document.getElementById("txtPageSize").value=pageCount;
    
   //扩展属性
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
      //Start验证输入
   var fieldText = "";
   var msgText = "";
   var isErrorFlag = false;
       var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isErrorFlag = true;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + "不能含有特殊字符|";
    }
       if (isErrorFlag) {
        popMsgObj.Show(fieldText, msgText);
        return ;
    }
     var Para="pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+
                     "&BorrowNo="+document.getElementById("tboxBorrowNo").value+
                    "&Title="+document.getElementById("tboxTitle").value+
                    "&Borrower="+document.getElementById("txtBorrower").value+
                    "&DeptID="+document.getElementById("txtBorrowDeptID").value+
                    "&OutDeptID="+document.getElementById("txtOutDept").value+
                    "&StorageID="+document.getElementById("ddlDepot").value+
                    "&Transactor="+document.getElementById("txtTransactor").value+
                    "&StartTime="+document.getElementById("tboxBorrowStartTime").value+
                    "&EndTime="+document.getElementById("tboxBorrowEndTime").value+
                    "&BillStatus="+document.getElementById("ddlBillStatus").value+
                    "&EFIndex="+escape(EFIndex)+
                    "&EFDesc="+escape(EFDesc)+
                    "&SubmitStatus="+document.getElementById("ddlSubmitStatus").value
     document.getElementById("txtSearchPara").value=Para; 
     var url="../../../Handler/Office/StorageManager/StorageBorrowList.ashx";
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url: url,//目标地址
           cache:false,
           data: Para,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},
           success: function(msg)
           {    
                    document.getElementById("chk_StorageList").checked=false;
                    $("#tblStoragelist tbody").find("tr.newrow").remove();
                   $.each(msg.data,function(i,item){
    
                        $("<tr class='newrow'></tr>").append(
                            "<td align=\"center\" >"+setCheckBoxStatsu(item)+"</td>"+
                            "<td align=\"center\" ><a href=\"StorageBorrowAdd.aspx?ModuleID="+document.getElementById("MoudleID").value+"&action=EDIT&id="+item.ID+"&BorrowRealNo="+item.BorrowNo+"\">"+item.BorrowNo+"</a></td>"+
                             "<td align=\"center\"  title=\""+item.Title+"\">"+fnjiequ(item.Title,LTitleLength)+"</td>"+
                             "<td align=\"center\" >"+item.DeptID+"</td>"+
                             "<td align=\"center\" >"+item.Borrower+"</td>"+
                             "<td align=\"center\" >"+item.OutDeptID+"</td>"+
                             "<td align=\"center\" >"+item.StorageID+"</td>"+
                             "<td align=\"center\" >"+parseFloat( item.CountTotal).toFixed(2)+"</td>"+
                             "<td align=\"center\" >"+parseFloat( item.TotalPrice).toFixed(2)+"</td>"+
                             "<td align=\"center\" >"+item.Transactor+"</td>"+
                             "<td align=\"center\" >"+item.OutDate+"</td>"+
                             "<td align=\"center\" >"+item.BillStatus+"</td>"+
                             "<td align=\"center\">"+getFlowStatus(item.FlowStatus)+"</td>" ).appendTo($("#tblStoragelist tbody")); 
                   });
                     //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  //document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
           },
           error: function(){  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");},
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();pageDataList1("tblStoragelist","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
                   });
     
     
     
} 
/*返回审批状态*/
function getFlowStatus(flowstatus)
{
    switch(flowstatus)
    {
        case "1":
            return "待审批";
        case "2":
             return "审批中";
        case "3":
            return "审批通过";
        case "4":
            return "审批不通过";
        case "5":
            return "撤销审批";
        default:
            return "";   
    }
}

var unDelList=new Array();
//根据单据状态是否启用 checkbox
function setCheckBoxStatsu(item)
{
    //alert(item.BillStatus);
       //unDelList.length=0; 
      if(item.BillStatus!="制单" || item.FlowStatus!="")
      {
            //return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"  onclick=\"unSelectAll('"+item.ID+"');\" disabled=\"true\"/>";
            unDelList.push(item.ID);
      }
      //else
     // {
           //return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"   onclick=\"unSelectAll('"+item.ID+"');\"/>";
           
     // }
       return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"   onclick=\"unSelectAll('"+item.ID+"');\"/>";
    
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

function Fun_Search_StorageInPurchase(aa)
{
      search="1";
      TurnToPage(1);
}
  //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
          var msg="";
          var title="";
         if(!IsNumOrFloat(newPageCount))
         {
            msg=msg+"只允许输入正整数|";
            title=title+"每页显示|";
         }
         if(!IsNumOrFloat(newPageIndex))
         {
            msg=msg+"只允许输入正整数|";
            title=title+"跳转页面|";
         }
        
        if(msg!="" || title!="")
        {
            popMsgObj.Show(title,msg);
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
    //排序
    function OrderBy(orderColum,orderTip)
    {
        var ordering = "a";
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
        }
        orderBy = orderColum+"_"+ordering;
        document.getElementById("txtOrderBy").value=orderBy;
        if (document.getElementById("txtSearchPara").value == "" || document.getElementById("txtSearchPara").value == null) return;
        TurnToPage(1);
    }

//添加提示
function AddPop()
{
    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}




window.onload=function(){
if(document.getElementById("txtSearchPara").value!="")
{
    pageCount=parseInt(document.getElementById("txtPageSize").value);
    orderBy=document.getElementById("txtOrderBy").value;
    TurnToPage(parseInt(document.getElementById("txtPageIndex").value));
}
}
 
 
 
 //生成div提示
 function Create_Div(img,img1,bool)
{
	FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
	FormStr += "<tr>"
	FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
	FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
	if(bool)
	{
		FormStr += "<img src='" + img +"' style='cursor:hand;display:block;' id='CloseImg' onClick=document.all['Forms'].style.display='none';>"
	}
	FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
	FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
	FormStr += "<tr>"
	FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
	FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
	FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
	FormStr += "</tr></table>"
	FormStr += "</td></tr></table>"
	return FormStr;
} 
//显示提示
 function showPopup(img,img1,retstr)
{
	document.all.Forms.style.display = "block";
	document.all.Forms.innerHTML = Create_Div(img,img1,true);
	document.all.FormContent.innerText = retstr;
}  

//隐藏提示
function hidePopup()
{
    document.all.Forms.style.display = "none";
}

//全选
function selectAll()
{
   var objchk=document.getElementById("chk_StorageList");
   var objlistchk=document.getElementsByName("chk");
   var chk=objchk.checked;
   var flag=objchk.checked;
       for(var i=0;i<objlistchk.length;i++)
       {
             // if(objlistchk[i].disabled==false)
                  objlistchk[i].checked=flag;
       }

}
//取消全选
function unSelectAll(chkid)
{
   var objchk=document.getElementById("chk_StorageList");
   var objlistchk=document.getElementsByName("chk");
   var flag=true;
   for(var i=0;i<objlistchk.length;i++)
   {
       if(!objlistchk[i].checked)
           flag=false;
   }
   if(flag)
    objchk.checked=true;
    else
    objchk.checked=false;
}



//返回选中的列表项
function getchecked()
{
   var chkList=document.getElementsByName("chk");
   var idlist=new Array();
   for(var i=0;i<chkList.length;i++)
   {
      if(chkList[i].checked)
      idlist.push(chkList[i].value);
   }
   return idlist;
}

//删除库存
function storageDelete()
{
 
  var idlist=getchecked();
  
  if(idlist.length<=0)
    {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选中需要删除的项");
           return;
    }
    
    var flag=false;
    for(var i=0;i<idlist.length;i++)
    {
        for(var j=0;j<unDelList.length;j++)
        {
            if(idlist[i]==unDelList[j])
            {
                flag=true;
            }
        }
    }
    
    if(flag)
    {
        popMsgObj.Show("借货单列表|","已提交审批或已确认后的单据不可删除|");
        return;
    }
    
    
  if(!confirm("确认删除选中的借货申请单吗？"))
  {
    return;
  }
  var para="id="+idlist+"&action=DELETE";
  var url="../../../Handler/Office/StorageManager/StroageBorrowSave.ashx";
  
   $.ajax({
           type: "POST",//用POST方式传输
           dataType:"string",
           url: url,//目标地址
           cache:false,
           data: para,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},
           success: function(msg)
           {    
                 if(msg=="1")
                 {
                    popMsgObj.Show("删除|","删除成功|");
                    TurnToPage(1);
                 }
                  
           },
           error: function(){ },
             complete:function(){}//接收数据完毕
            });
}

//重置按钮
function Fun_ClearInput()
{
  var t= document.getElementsByTagName("input");
  for(var i=0; i<t.length;i++)
  {
     if(t[i].id!="ToPage" && t[i].id!="ShowPageCount")
       t[i].value="";
  }
  
  
  var s=document.getElementsByTagName("select");
  for(var i=0;i<s.length;i++)
  {
     s[i].value=-1;
  }
}



/******************************
*验证页码与每页显示数
******************************/
function checkPage(control,type)
{
        var obj=document.getElementById(control);
        if(!IsNumOrFloat(obj.value,true))
        {
            if(type==1)
            {
                popMsgObj.Show("每页显示|","只允许输入正整数|");
                //obj.value=10;
            }
            else if(type==2)
            {
                popMsgObj.Show("跳转页面|","只允许输入正整数|");
                //obj.value=1;
            }
            
        }
}

/*新建*/
function createNew()
{
    window.location="StorageBorrowAdd.aspx?ModuleID="+document.getElementById("MoudleID").value+"&IsBack=1";
}

/*导出*/
function outputToExcel(pageIndex)
{
     document.getElementById("txtPageIndex").value=pageIndex;
     document.getElementById("txtPageSize").value=pageCount;
     var Para="pageIndex="+pageIndex+"&pageCount="+pageCount+"&action=OUTPUT&orderby="+orderBy+
                     "&BorrowNo="+document.getElementById("tboxBorrowNo").value+
                    "&Title="+document.getElementById("tboxTitle").value+
                    "&Borrower="+document.getElementById("txtBorrower").value+
                    "&DeptID="+document.getElementById("txtBorrowDeptID").value+
                    "&OutDeptID="+document.getElementById("txtOutDept").value+
                    "&StorageID="+document.getElementById("ddlDepot").value+
                    "&Transactor="+document.getElementById("txtTransactor").value+
                    "&StartTime="+document.getElementById("tboxBorrowStartTime").value+
                    "&EndTime="+document.getElementById("tboxBorrowEndTime").value+
                    "&BillStatus="+document.getElementById("ddlBillStatus").value+
                    "&SubmitStatus="+document.getElementById("ddlSubmitStatus").value
     document.getElementById("txtSearchPara").value=Para; 
     var url="../../../Handler/Office/StorageManager/StorageBorrowList.ashx";
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url: url,//目标地址
           cache:false,
           data: Para,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},
           success: function(data)
           {    
                  var  content=data;
                   var newwin=window.open('','','');
                   newwin.opener = null;
                   newwin.document.write(content);
                   newwin.document.close();
            },
           error: function(){  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");},
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();pageDataList1("tblStoragelist","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
                   });
}