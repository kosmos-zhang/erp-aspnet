
$(document).ready(function() {

//    IsDiplayOther('GetBillExAttrControl1_SelExtValue');//扩展属性
      IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');//扩展属性
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
        if(cTrim(document.getElementById("txtTransferNo").value,0)!="")
        {
            if(!CheckSpecialWord(document.getElementById("txtTransferNo").value))
            {
                popMsgObj.Show("调拨单编号|","调拨单编号中含有特殊字符|");
                return;
            }
        }
        if(cTrim(document.getElementById("txtTitle").value,0)!="")
        {
            if(!CheckSpecialWord(document.getElementById("txtTitle").value))
            {
                popMsgObj.Show("调拨单主题|","调拨单主题中含有特殊字符|");
                return;
            }
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
     document.getElementById("txtPageIndex").value=pageIndex;
    document.getElementById("txtPageSize").value=pageCount;
          var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
       var Para="pageIndex="+pageIndex+"&pageCount="+pageCount+"&action=GET&orderby="+orderBy+
                      "&TransferNo="+document.getElementById("txtTransferNo").value+
                      "&TransferTitle="+escape(document.getElementById("txtTitle").value)+
                      "&ApplyUserID="+document.getElementById("txtApplyUserID").value+
                      "&ApplyDeptID="+document.getElementById("txtApplyDeptID").value+
                      "&InStorageID="+document.getElementById("ddlInStorageID").value+
                      "&RequireInDate="+document.getElementById("txtRequireInDate").value+
                      "&OutDeptID="+document.getElementById("txtOutDeptID").value+
                      "&OutStorageID="+document.getElementById("ddlOutStorageID").value+
                      "&ConfirmStatus="+document.getElementById("ddlConfirmStatus").value+
                     "&EFIndex="+escape(EFIndex)+
                    "&EFDesc="+escape(EFDesc)+
                      "&BusiStatus="+document.getElementById("ddlBusiStatus").value+
                      "&BillStatus="+document.getElementById("ddlBillStatus").value;
       var url="../../../Handler/Office/StorageManager/StorageTransferList.ashx";
       document.getElementById("txtIsSearch").value=Para;
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url: url,//目标地址
           cache:false,
           data: Para,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},
           success: function(msg)
           {    //alert(msg.data);
                    document.getElementById("chk_StorageList").checked=false;
                    $("#tblStoragelist tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item!=null && item!="")
                        {
                        $("<tr class='newrow'></tr>").append(
                            "<td align=\"center\" >"+checkStatus(item)+
                            "<td align=\"center\" ><a href=\"StorageTransferSave.aspx?ModuleID="+document.getElementById("MoudleID").value+"&action=EDIT&TransferID="+item.ID+"&TransferNo="+item.TransferNo+getFlowStatus(item.FlowStatus)+"\">"+item.TransferNo+"</a></td>"+
                             "<td align=\"center\" title=\""+item.Title+"\">"+fnjiequ(item.Title,12)+"</td>"+
                             "<td align=\"center\" >"+item.ApplyUserID+"</td>"+
                             "<td align=\"center\" >"+item.ApplyDeptID+"</td>"+
                             "<td align=\"center\" >"+item.InStorageID+"</td>"+
                             "<td align=\"center\" >"+item.RequireInDate+"</td>"+
                             "<td align=\"center\" >"+item.OutDeptID +"</td>"+
                             "<td align=\"center\" >" + item.OutStorageID + "</td>" +
                             "<td align=\"center\" >" + parseFloat(item.TransferCount).toFixed(2) + "</td>" +
                             "<td align=\"center\" >"+parseFloat( item.TransferPrice).toFixed(2) +"</td>"+
                             "<td align=\"center\" >"+item.BillStatus +"</td>"+
                             "<td align=\"center\" >"+checkStatusValue(item.FlowStatus) +"</td>"+
                             "<td align=\"center\" >"+setStatusAction(item)+"</td>").appendTo($("#tblStoragelist tbody")); 
                                 
                                 
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
                 // document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  
           },
           error: function(){  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");},
             complete:function(){hidePopup();$("#pageDataList1_Pager").show();pageDataList1("tblStoragelist","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
                   });
     
     
     
} 

function getFlowStatus(value)
{
    if(value=="")
        return "&FlowStatus=0";
    else
        return "&FlowStatus=1";
}

function checkStatusValue(value) 
{
    switch(value)
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

/*保存不可删除的ID*/
var unDelList=new Array();
//验证状态
function checkStatus(item)
{
      if(item.BillStatus!="制单" || item.FlowStatus!="")
      {
            unDelList.push(item.ID);
      }

       return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"   onclick=\"unSelectAll('"+item.ID+"');\"/>";
    
        


//    if(item.BillStatus=="制单" ||  (item.FlowStauts!="" && item.FlowStatus!="5"))
//    {
//        if(item.FlowStatus=="")
//            return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"   onclick=\"unSelectAll('"+item.ID+"');\"/></td>";
//        else
//            return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"  disabled=\"true\"  onclick=\"unSelectAll('"+item.ID+"');\"/></td>"
//    }
//    else
//        return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"  disabled=\"true\"  onclick=\"unSelectAll('"+item.ID+"');\"/></td>"
}


 function setStatusAction(item)
 {
    if(item.BillStatus!="制单")
    {
        if(item.BusiStatus=="调拨出库")
        {
            return "<a href=\"StorageTransferOut.aspx?ModuleID="+document.getElementById("ModuleInOutID").value+"&TransferID="+item.ID+"\">"+item.BusiStatus+"</a>";
        }
        else if(item.BusiStatus=="调拨入库")
        {
            return "<a href=\"StorageTransferIn.aspx?ModuleID="+document.getElementById("ModuleInOutID").value+"&TransferID="+item.ID+"\">"+item.BusiStatus+"</a>";
        }
        else
            return item.BusiStatus;
    }
    else
    {
        return  item.BusiStatus;
    }

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

window.onload=function(){
    if(document.getElementById("txtIsSearch").value!="")
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
            if(objlistchk[i].disabled==true)
                continue;
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
        popMsgObj.Show("调拨单列表|","已提交审批或已确认后的单据不可删除|");
        return;
    }
    
    
    
    if(!confirm("确认删除选中的调拨单？"))
        return;
  var para="id="+idlist+"&action=DEL";
  var url="../../../Handler/Office/StorageManager/StorageTransferList.ashx";
  
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
                        popMsgObj.Show("删除|","删除成功|");
           },
           error: function(){ },
             complete:function(){}//接收数据完毕
            });
}


////重置按钮
//function Fun_ClearInput()
//{
//   document.getElementById("txtTransferNo").value="";
//   document.getElementById("txtTitle").value="";
//   document.getElementById("UserApplyUser").value="";
//   document.getElementById("txtApplyUserID").value="";
//   document.getElementById("DeptApplyDept").value="";
//   document.getElementById("txtApplyDeptID").value="";
//   document.getElementById("ddlInStorageID").value="-1";
//   document.getElementById("txtRequireInDate").value="";
//   document.getElementById("DeptOutDeptID").value="";
//   document.getElementById("txtOutDeptID").value="";
//   document.getElementById("ddlOutStorageID").value="-1";
//   document.getElementById("ddlConfirmStatus").value="-1";
//   document.getElementById("ddlBusiStatus").value="-1";
//   document.getElementById("ddlBillStatus").value="-1";
//   
//}

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
                popMsgObj.Show("调拨单列表|","每页显示数必须为正整数|");
                obj.value=10;
            }
            else if(type==2)
            {
                popMsgObj.Show("调拨单列表|","页码必须为正整数|");
                obj.value=1;
            }
            
        }
}
/*新建*/
function createNew()
{
    window.location="StorageTransferSave.aspx?ModuleID="+document.getElementById("MoudleID").value+"&IsBack=1";
}