$(document).ready(function(){
  IsDiplayOther('GetBillExAttrControl1_SelExtValue');//扩展属性      
    });
    
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
    
    var StartDate=document.getElementById("txtStartDate").value;
    var EndDate=document.getElementById("txtEndDate").value;
       var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    if(StartDate!="" && EndDate!="")
        if(StartDate>EndDate)
            {
                popMsgObj.Show("检索|","起始日期应小于结束日期|");
                return;
            }
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
       if(document.getElementById("ShowPageCount").value!="10" && document.getElementById("ShowPageCount").value!="")
       {
            pageCount=document.getElementById("ShowPageCount").value;
       }
               
     var Para="pageIndex="+pageIndex+"&pageCount="+pageCount+"&action=GET&orderby="+orderBy+
                     "&ReturnNo="+document.getElementById("txtReturnNo").value+
                     "&ReturnTitle="+escape(document.getElementById("txtReturnTitle").value)+
                     "&BorrowDeptID="+document.getElementById("txtBorrowDeptID").value+
                     "&OutDeptID="+document.getElementById("txtOutDeptID").value+
                     "&StorageID="+document.getElementById("ddlStorage").value+
                     "&EFIndex="+escape(EFIndex)+
                     "&EFDesc="+escape(EFDesc)+
                     "&ReturnPerson="+document.getElementById("txtReturnerID").value+
                     "&BillStatus="+document.getElementById("ddlBillStatus").value+
                     "&StartDate="+StartDate+
                     "&EndDate="+EndDate+
                     "&FromBillID="+document.getElementById("txtFromBillID").value;
     var url="../../../Handler/Office/StorageManager/StorageReturnList.ashx";
     document.getElementById("txtIsSearch").value=Para;
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
                        if(item!=null && item!="")
                        {
                        $("<tr class='newrow'></tr>").append(
                         "<td align=\"center\" >"+checkStatus(item)+
                         "<td align=\"center\" ><a href=\"StorageReturnSave.aspx?ModuleID="+document.getElementById("MoudleID").value+"&action=EDIT&ReturnID="+item.ID+"&ReturnNo="+item.ReturnNo+"\">"+item.ReturnNo+"</a></td>"+
                         "<td align=\"center\" title=\""+item.Title+"\">"+fnjiequ(item.Title,15)+"</td>"+
                         "<td align=\"center\" >"+item.BorrowDept+"</td>"+
                         "<td align=\"center\" >"+item.StorageName+"</td>"+
                         "<td align=\"center\" >"+item.ReturnName+"</td>"+
                         "<td align=\"center\" >"+item.ReturnDate+"</td>"+
                         "<td align=\"center\" >"+item.BillStatus+"</td>"+
                         "<td align=\"center\" >"+item.BorrowNo+"</td>").appendTo($("#tblStoragelist tbody")); 
                                 
                                 
                   }});
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

var unDelList=new Array();
function checkStatus(item)
{
    if(item.BillStatus!="制单")
    {
       // return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"   onclick=\"unSelectAll('"+item.ID+"');\"/></td>";
        unDelList.push(item.ID);
    }
    //else
      //  return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"  disabled=\"true\"  onclick=\"unSelectAll('"+item.ID+"');\"/></td>"
      return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"   onclick=\"unSelectAll('"+item.ID+"');\"/></td>";
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
        if (document.getElementById("txtIsSearch").value == "" || document.getElementById("txtIsSearch").value == null) return;
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
        
        TurnToPage(1);
    }

//添加提示
function AddPop()
{
    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
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
            //if(objlistchk[i].disabled==true)
                //continue;
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
        popMsgObj.Show("借货单列表|","已经确认的借货返还单不可删除|");
        return;
    }
    if(!confirm("确认删除选中的借货返还单？"))
        return;
  var para="id="+idlist+"&action=DEL";
  var url="../../../Handler/Office/StorageManager/StorageReturnList.ashx";
  
   $.ajax({
           type: "POST",//用POST方式传输
           dataType:"string",
           url: url,//目标地址
           cache:false,
           data: para,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},
           success: function(msg)
           {      
                   if(msg=="2")
                   {   
                        popMsgObj.Show("删除|","删除成功|");
                        TurnToPage(parseInt(document.getElementById("ToPage").value));
                   }
                   else
                        popMsgObj.Show("删除|","删除失败|");
           },
           error: function(){ },
             complete:function(){}//接收数据完毕
            });
}

window.onload=function(){
if(document.getElementById("txtIsSearch").value!="")
{
     pageCount=parseInt(document.getElementById("txtPageSize").value);
    orderBy=document.getElementById("txtOrderBy").value;
    TurnToPage(parseInt(document.getElementById("txtPageIndex").value));
}
}

//重置按钮
function Fun_ClearInput()
{
    document.getElementById("txtReturnNo").value="";
    document.getElementById("txtReturnTitle").value="";
    document.getElementById("txtBorrowDeptID").value="";
    document.getElementById("txtOutDeptID").value="";
    document.getElementById("ddlStorage").value="";
    document.getElementById("txtReturnerID").value="";
    document.getElementById("ddlBillStatus").value="";
    document.getElementById("txtStartDate").value="";
    document.getElementById("txtEndDate").value="";
    
    document.getElementById("DeptOutDept").value="";
    document.getElementById("DeptBorrowDept").value="";
    document.getElementById("UserReturner").value="";
}

/*********************************************
*验证页码与每页显示数
*********************************************/
function checkPage(control,msg)
{
    var obj=document.getElementById(control);
    if(!IsNumOrFloat(obj.value,true))
    {
         
         if(msg=="每页显示数量")
            popMsgObj.Show("每页显示|","只允许输入正整数|");
         else
            popMsgObj.Show("转到页面|","只允许输入正整数|");
         return;
    }
    
}




   
   /*******************************
   *读取借货单
   **************************************/
   
     function selectedStorageBorrow(ID,BorrowNo,DeptIDText,BorrowerText,BorrowDate,OutDeptIDText,StorageID,StorageIDText,TotalCount,TotalPrice)
       {
           document.getElementById("txtStorageReturn").value=BorrowNo;
           document.getElementById("txtFromBillID").value=ID;
           closeProductInfodiv();
        }
          function getStorageBorrowList()
       {
            popProductInfoObj.ShowList();
       }
       
       
       /*新建*/
function createNew()
{
    window.location="StorageReturnSave.aspx?ModuleID="+document.getElementById("MoudleID").value+"&IsBack=1";
}

