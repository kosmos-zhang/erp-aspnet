 var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var orderBy = "CheckNo_d";//排序字段
    var action = "";//操作
 //翻页
function TurnToPage(pageIndex)
{


         if(cTrim(document.getElementById("txtCheckNo").value,0)!="")
        {
            if(!CheckSpecialWord(document.getElementById("txtCheckNo").value))
            {
                popMsgObj.Show("盘点单列表|","盘点单编号中含有特殊字符|");
                return;
            }
        }
        if(cTrim(document.getElementById("txtTitle").value,0)!="")
        {
            if(!CheckSpecialWord(document.getElementById("txtTitle").value))
            {
                popMsgObj.Show("盘点单列表|","盘点单主题中含有特殊字符|");
                return;
            }
        }
       
    document.getElementById("txtPageIndex").value=pageIndex;
    document.getElementById("txtPageSize").value=pageCount;
       var Para="pageIndex="+pageIndex+"&pageCount="+pageCount+"&action=GET&orderby="+orderBy+
                      "&CheckNo="+document.getElementById("txtCheckNo").value+
                      "&Title="+document.getElementById("txtTitle").value+
                      "&DeptID="+document.getElementById("txtDeptID").value+
                      "&StorageID="+document.getElementById("ddlStorageID").value+
                      "&CheckType="+document.getElementById("ddlCheckType").value+
                      "&CheckStartDate="+document.getElementById("txtStartDate").value+
                      "&DiffCountStart="+document.getElementById("txtDiffCountStart").value+
                      "&DiffCountEnd="+document.getElementById("txtDiffCountEnd").value+
                      "&BillStatus="+document.getElementById("ddlBillStatus").value+
                      "&FlowStatus="+document.getElementById("ddlConfirmStatus").value+
                      "&Transactor="+document.getElementById("txtTransactor").value;
       var url="../../../Handler/Office/StorageManager/StorageCheckList.ashx";
       document.getElementById("txtIsSearch").value=Para;
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url: url,//目标地址
           cache:false,
           data: Para,//数据
           beforeSend:function(){openRotoscopingDiv(true,'divPageMask','PageMaskIframe')},
           success: function(msg)
           {    
                    $("#tblStoragelist tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item!=null && item!="")
                        {
                        $("<tr class='newrow'></tr>").append(
                            "<td align=\"center\" >"+checkStatus(item)+
                            "<td align=\"center\" ><a href=\"../../office/StorageManager/StorageCheckSave.aspx?ModuleID="+document.getElementById("MoudleID").value+"&action=EDIT&CheckID="+item.ID+"&CheckNo="+item.TransferNo+getFlowStatus(item.FlowStatus)+"\">"+item.CheckNo+"</a></td>"+
                             "<td align=\"center\" title=\""+item.Title+"\">"+fnjiequ(item.Title,12)+"</td>"+
                             "<td align=\"center\" >"+item.DeptName +"</td>"+
                             "<td align=\"center\" >"+item.StorageName +"</td>"+
                             "<td align=\"center\" >"+item.TransactorName +"</td>"+
                             "<td align=\"center\" >"+item.CheckStartDate +"</td>"+
                             "<td align=\"center\" >"+item.CheckType  +"</td>"+
                             "<td align=\"center\" >"+parseFloat(item.DiffCount).toFixed(2)  +"</td>"+
                             "<td align=\"center\" >"+item.BillStatus  +"</td>"+
                             "<td align=\"center\" >"+checkStatusValue(item.FlowStatus)  +"</td>" ).appendTo($("#tblStoragelist tbody")); 
                                 
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
           error: function(){},
             complete:function(){closeRotoscopingDiv(true,'divPageMask');$("#pageDataList1_Pager").show();pageDataList1("tblStoragelist","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
                   });
     
     
     
} 
//将流程状态传递给查看页面
function getFlowStatus(value)
{
    if(value=="")
    {
        return "&FlowStatus=0";
    }
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
       unDelList.length=0;
      if(item.BillStatus!="制单" || (item.FlowStatus!="" && item.FlowStatus!="5"))
      {
            unDelList.push(item.ID);
      }

       return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"   onclick=\"unSelectAll('"+item.ID+"');\"/>";


//    if(item.BillStatus=="制单")
//    {
//        if(item.FlowStatus=="")
//            return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"   onclick=\"unSelectAll('"+item.ID+"');\"/></td>";
//        else
//              return "<input type=\"checkbox\" id=\"chk_list_"+item.ID+"\" name=\"chk\" value=\""+item.ID+"\"  disabled=\"true\"  onclick=\"unSelectAll('"+item.ID+"');\"/></td>"
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
            return "<a href=\"../../office/StorageManager/StorageTransferOut.aspx?TransferID="+item.ID+"\">"+item.BusiStatus+"</a>";
        }
        else if(item.BusiStatus=="调拨入库")
        {
            return "<a href=\"../../office/StorageManager/StorageTransferIn.aspx?TransferID="+item.ID+"\">"+item.BusiStatus+"</a>";
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

function Fun_Search_StorageInPurchase(aa)
{
      search="1";
      TurnToPage(1);
}
  //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
    
         var title="";
         var msg="";
         if(!IsNumOrFloat(newPageCount,true))
        {
            title=title+"每页显示|";
            msg=msg+"请输入正整数|"
        }
         if(!IsNumOrFloat(newPageIndex,true))
        {
            title=title+"转到页面|";
            msg=msg+"请输入正整数|"
        }
        if(msg!="" && title!="")
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
            if(objlistchk[i].disabled==false)
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
    
    
    
    if(!confirm('确认删除选中项？'))
        return;
  var para="id="+idlist+"&action=DEL";
  var url="../../../Handler/Office/StorageManager/StorageCheckList.ashx";
  
   $.ajax({
           type: "POST",//用POST方式传输
           dataType:"string",
           url: url,//目标地址
           cache:false,
           data: para,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},
           success: function(msg)
           {      
                    if(msg=="True")
                   {   
                        popMsgObj.Show("删除|","删除成功|");
                        TurnToPage(parseInt(document.getElementById("ToPage").value));
                   }
                   else
                        popMsgObj.Show("删除|","删除失败|");
           },
           error: function(){hidePopup(); },
             complete:function(){hidePopup();}//接收数据完毕
            });
}



//重置按钮
function Fun_ClearInput()
{
    document.getElementById("txtCheckNo").value="";
   document.getElementById("txtTitle").value="";
   document.getElementById("UserTransactor").value="";
   document.getElementById("txtTransactor").value="";
   document.getElementById("DeptDept").value="";
   document.getElementById("txtDeptID").value="";
   document.getElementById("ddlStorageID").value="-1";
   document.getElementById("ddlCheckType").value="-1";
   document.getElementById("txtStartDate").value="";
   document.getElementById("txtDiffCountStart").value="";
   document.getElementById("ddlBillStatus").value="-1";
   document.getElementById("ddlConfirmStatus").value="-1";
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
                popMsgObj.Show("每页显示|","请输入正整数|");
               // obj.value=10;
            }
            else if(type==2)
            {
                popMsgObj.Show("转到页面|","请输入正整数|");
                //obj.value=1;
            }
            
        }
}

/*新建*/
function createNew()
{
    window.location="StorageCheckSave.aspx?ModuleID="+document.getElementById("MoudleID").value+"&IsBack=1";
}