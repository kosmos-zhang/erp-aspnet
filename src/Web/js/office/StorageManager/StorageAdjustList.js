 var point="";
$(document).ready(function()
{
    point=$("#HiddenPoint").val();
    IsDiplayOther('GetBillExAttrControl2_SelExtValue');//扩展属性

});
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var ifshow="0";
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    var reset="1";
    var action="";
    var isSearch=0;

function FistSearchAdjust()
{
      document.getElementById('isreturn').value='1';
      SearchStorageAdjustData(1);
    
}
function getchecked()
{
   var chkList=document.getElementsByName("Checkbox1");
   var idlist=new Array();
   for(var i=0;i<chkList.length;i++)
   {
      if(chkList[i].checked)
      idlist.push(chkList[i].value);
   }
   return idlist;
}

function SearchStorageAdjustData(pageIndex)
{//检索
    //检索条件
    if(CheckInput())
    {   
     pageIndex=parseFloat(pageIndex);
    if(document.getElementById('ShowPageCount').value!='10' && document.getElementById('ShowPageCount').value!='')
      pageCount=document.getElementById('ShowPageCount').value;
//    if(document.getElementById('ToPage').value!='1' && document.getElementById('ToPage').value!='')
//      document.getElementById('ToPage').value=currentPageIndex;
    var txtAdjustNo =document.getElementById("txtReportNo").value;  //单据编号//
    var txtTitle = document.getElementById("txtSubject").value; //   
    var StorageID = document.getElementById("hiddenStorageID").value; //仓库ID
    var Executor=document.getElementById('hiddenExecutor').value;     //经办人
    var DeptID=document.getElementById('hiddenDeptID').value;         //部门ID
    var Reason=document.getElementById('ddlReason').value;            //原因
    var BillStatus=document.getElementById('BillStatus').value;
    var FlowStatus=document.getElementById('FlowStatus').value;  
    var BeginTime=document.getElementById('BeginTime').value;  
    var EndTime=document.getElementById('EndTime').value;
      //扩展属性
    var EFIndex=document.getElementById("GetBillExAttrControl2_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl2_TxtExtValue").value;//扩展属性文本框值
    
    var BatchNo=$("#txtBatchNo").val();//批次
    if(!compareDate(BeginTime,EndTime))    
    {
                  popMsgObj.ShowMsg('起始时间不能大于终止时间！');
                  return;
    }
    currentPageIndex = pageIndex;
      
    var MyData = "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderBy="+orderBy+
                     "&txtAdjustNo="+escape(txtAdjustNo)+"&txtTitle="+escape(txtTitle)+"&StorageID="+escape(StorageID)+"&Executor="+Executor+"&Reason="+Reason+
                     "&DeptID="+escape(DeptID)+"&BillStatus="+escape(BillStatus)+"&FlowStatus="+FlowStatus+"&BeginTime="+BeginTime+"&EFIndex="+escape(EFIndex)+"&EFDesc="+escape(EFDesc)+"&BatchNo="+escape(BatchNo)+"&EndTime="+EndTime;
                 
   
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageAdjustList.ashx',//目标地址
           cache:false,
            data: MyData,//数据
           beforeSend:function(){AddPop();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                 
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != ""){
                        var testApplyNo=item.AdjustNo;
                        isSearch=1;
                        var testTitle=item.Title;
                        if(testTitle!=null)
                        {
                            if(testTitle.length>15)
                            {
                                testTitle=testTitle.substring(0,15)+'...';
                            }
                        }
                        if(testApplyNo!=null)
                        {
                            if(testApplyNo.length>15)
                            {
                                testApplyNo=testApplyNo.substring(0,15)+'...';
                            }
                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input onclick=\"IfSelectAll('cbboxall','cbSelectAll');\" id='cbboxall"+item.ID+"' name='cbboxall'  value="+item.ID+'|'+item.BillStatus+'|'+item.FlowStatusID+"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+
                        "<td height='22' align='center'><a href='StorageAdjustAdd.aspx?ModuleID=2051601&myAction=edit&ID="+item.ID+"' title=\""+item.AdjustNo+"\">"+testApplyNo +"</a></td>"+
                        "<td height='22' align='center'><span title=\""+item.Title+"\">" + testTitle + "</td>"+
//                        "<td height='22' align='center'><span title=\""+item.BatchNo+"\">" + BatchNo + "</td>"+
                        "<td height='22' align='center'>"+ item.StorageName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.EmployeeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.DeptName +"</a></td>"+
                        "<td height='22' align='center'>"+ GetAdjustDate(item.AdjustDate)+"</a></td>"+
                        "<td height='22' align='center'>"+ item.CodeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.BillStatusName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.FlowStatus +"</a></td>").appendTo($("#pageDataList1 tbody"));}
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"SearchStorageAdjustData({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById('Text2').value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagePurPlancount"));
                  $("#ToPage").val(pageIndex);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  },
           error: function() {}, 
           complete:function(){hidePopup();Checkcbx();$("#pageDataList1_Pager").show();Ifshow(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
           }
}
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var ReportNo=document.getElementById('txtReportNo').value;
    var Title=document.getElementById('txtSubject').value;
//    if(strlen(ReportNo)>0)
//    {
//        if(!CheckSpecialWord(ReportNo))
//        {
//            isFlag = false;
//            fieldText = fieldText + "单据编号|";
//   		    msgText = msgText +  "单据编号不能含有特殊字符 |";
//   		}
//    }
//    if(strlen(Title)>0)
//    {
//        if(!CheckSpecialWord(Title))
//        {
//            isFlag = false;
//            fieldText = fieldText + "单据主题|";
//   		    msgText = msgText +  "单据主题不能含有特殊字符 |";
//   		}
//    }
   
       var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + "不能含有特殊字符|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
function GetAdjustDate(a)
{
    if(a!='' && a!=null)
    {
        if(a.substring(0,10)=='1900-01-01')
        {
            a='';
            return a;
        }
       
        else        
        {
            return a.substring(0,10);
        }
    }
    else
    {
        return a;
    }
}
function Checkcbx()
{
    if(document.getElementById('cbSelectAll').checked)
    {
        document.getElementById('cbSelectAll').checked=false;
    }
}
function hidePopup()
{
    document.all.Forms.style.display = "none";
}
function EditQuality(a,b,c)
{
    var MyData = "ApplyNo="+a+"&FromType="+b+"&ID="+c+"&action=edit";               
    window.location.href='StorageQualityCheckAdd.aspx?'+MyData;
}
function SelectAllList()
{
   var objchk=document.getElementById("cbSelectAll");
   var objlistchk=document.getElementsByName("cbboxall");
   var chk=objchk.checked;
   var flag=objchk.checked;
       for(var i=0;i<objlistchk.length;i++)
       {
           objlistchk[i].checked=flag;
       }

}
function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "";
        document.getElementById("pagecount").style.display = "";
    }
}

    function SelectDept(retval)
    {
        alert(retval);
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
        } 
    
 
        else
        {
            if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
            {
               showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
               return false;
            }
            else
            {
                this.pageCount=parseInt(newPageCount);
                SearchStorageAdjustData(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function OrderBy11(orderColum,orderTip)
    {
       if(parseFloat(isSearch)<=0)
       {
         return false;
       }
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
        document.getElementById('hiddenOrder').value=orderBy;
        SearchStorageAdjustData(1);
    }

function AddPop()
{
    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
function showPopup(img,img1,retstr)
{
	document.all.Forms.style.display = "";
	document.all.Forms.innerHTML = Create_Div(img,img1,true);
	document.all.FormContent.innerText = retstr;
}  
function hidePopup()
{
    document.all.Forms.style.display = "none";
}
function Create_Div(img,img1,bool)
{
	FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
	FormStr += "<tr>"
	FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
	FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
	if(bool)
	{
		FormStr += "<img src='" + img +"' style='cursor:hand;display:;' id='CloseImg' onClick=document.all['Forms'].style.display='none';>"
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
function Fun_Delete_Adjust()
{

    var cb=document.getElementsByName('cbboxall');
    var OptionCheckArray='';
    var theFlag=false; 
    for(var i=0;i<cb.length;i++)
    {
        if(cb[i].checked)
         {
            theFlag=true;  
         }
    }
    if(!theFlag)
    {
        popMsgObj.ShowMsg('请选择要删除的库存调整单！');
        return false;
    }
    if(window.confirm("确认执行删除操作吗？"))
    {   
        for(var i=0;i<cb.length;i++)
        {
            if(cb[i].checked)
            {
                OptionCheckArray=OptionCheckArray+','+cb[i].value.split('|')[0];
                var flowstatus=cb[i].value.split('|')[2]; 
                if(cb[i].value.split('|')[1]!='1' || flowstatus!='0')
                {
                     popMsgObj.ShowMsg('已提交审批或者已确认的单据不允许删除！');
                     return false;
                }           
            }
        }
        var Action='Del';
        var UrlParam = "Action="+Action+"&strID="+OptionCheckArray.toString();
                       
            $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/StorageManager/StorageAdjustAdd.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('删除库存调整单时请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    popMsgObj.ShowMsg(data.info);
                    
                    if(data.sta>0)
                    {
                        SearchStorageAdjustData(1);
                    }
                  } 
               });
   }
    
}

function IsOut() //判断第一次导出
{     
    if(!parseFloat(totalRecord)>0)
    {       
        alert('请先检索！');
        return false;
    }
}
