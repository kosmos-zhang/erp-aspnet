var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式
var ifshow="0";
var currentPageIndex = 1;
var orderBy = "";//排序字段
var reset="1";
var action="";
var CheckResult=0;
var isSearch=0;

$(document).ready(function()
{
    fnGetExtAttr();// 物资的扩展属性
    IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
    if(document.getElementById('isreturn').value!='0')
    {
        SearchStorageNoPassData(document.getElementById('ToPage').value);
    }

});
 
function FistSearchNoPass()
{
    if(CheckInput())
    {
        SearchStorageNoPassData(1);
    }
}
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var ReportNo=document.getElementById('txtReportNo').value;
    var Title=document.getElementById('txtSubject').value;
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    
    if(strlen(ReportNo)>0)
    {
        if(!CheckSpecialWord(ReportNo))
        {
            isFlag = false;
            fieldText = fieldText + "单据编号|";
   		    msgText = msgText +  "单据编号不能含有特殊字符 |";
   		}
    }
    if(strlen(Title)>0)
    {
        if(!CheckSpecialWord(Title))
        {
            isFlag = false;
            fieldText = fieldText + "单据主题|";
   		    msgText = msgText +  "单据主题不能含有特殊字符 |";
   		}
    }
    if(EFIndex!="" && EFDesc!="")
    {
        if(!CheckSpecialWord(EFDesc))
        {
            isFlag=false;
            fieldText=fieldText + "其他条件|";
            msgText = msgText +  "其他条件不能含有特殊字符|";
        }
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
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
function storageNoPassDelete()
{
  var idlist=getchecked();
  
  if(idlist,length<=0)
    {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选中需要删除的项");
    }
  var para="id="+idlist+"&action=DELETE";
  var url="../../../Handler/Office/StorageManager/StorageNoPassList.ashx";
  
   $.ajax({
           type: "POST",//用POST方式传输
           dataType:"string",
           url: url,//目标地址
           cache:false,
           data: para,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},
           success: function(msg)
           {    
                  SearchStorageNoPassData(1);
           },
           error: function(){ },
             complete:function(){}//接收数据完毕
            });
}
function SearchStorageNoPassData(pageIndex)
{//检索
    //检索条件
    pageIndex=parseFloat(pageIndex);
    if(document.getElementById('isreturn').value='1')
    if(document.getElementById('ShowPageCount').value!='')
    pageCount=document.getElementById('ShowPageCount').value;
    var txtNoPassNo =document.getElementById("txtReportNo").value;  //单据编号//
    var txtTitle = document.getElementById("txtSubject").value; //   
    var ReportID = document.getElementById("hiddenFromReportID").value; //质检报告单ID
    var ProductID=document.getElementById('hiddenProductID').value;
    var BillStatus=document.getElementById('sltBillStatus').value;
    var FlowStatus=document.getElementById('ddlFlowStatus').value;  
    var BeginTime=document.getElementById('BeginTime').value;  
    var EndTime=document.getElementById('EndTime').value;
    
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    
    currentPageIndex = pageIndex;
      
    var MyData = "pageIndex="+pageIndex
                +"&pageCount="+pageCount
                +"&action="+action
                +"&orderBy="+orderBy
                +"&txtNoPassNo="+escape(txtNoPassNo)
                +"&txtTitle="+escape(txtTitle)
                +"&ReportID="+escape(ReportID)
                +"&ProductID="+escape(ProductID)
                +"&BillStatus="+escape(BillStatus)
                +"&FlowStatus="+FlowStatus
                +"&BeginTime="+BeginTime
                +"&EndTime="+EndTime
                +"&EFIndex="+escape(EFIndex)
                +"&EFDesc="+escape(EFDesc);
                 
   
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageNoPassList.ashx',//目标地址
           cache:false,
            data: MyData,//数据
           beforeSend:function(){AddPop();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                 
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){                   
                        if(item.ID != null && item.ID != "" && parseFloat(item.ID)>0)  {
                        CheckResult=msg.totalCount;    
                        isSearch=1;                   
                        var testProcessNo=item.ProcessNo;
                        var testTitle=item.Title;
                        if(testTitle!=null)
                        {
                            if(testTitle.length>15)
                            {
                                testTitle=testTitle.substring(0,15)+'...';
                            }
                        }
                        if(testProcessNo!=null)
                        {
                            if(testProcessNo.length>15)
                            {
                                testProcessNo=testProcessNo.substring(0,15)+'...';
                            }
                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input onclick=\"IfSelectAll('cbboxall','cbSelectAll');\" id='cbboxall"+item.ID+"' name='cbboxall'  value="+item.ID+'|'+item.BillStatusID+'|'+item.FlowStatus+"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+
                        "<td height='22' align='center'><a title=\""+item.ProcessNo+"\" href='StorageCheckNotPassAdd.aspx?ModuleID=2071301&myAction=edit&ID="+item.ID+"'>"+testProcessNo+"</a></td>"+
                        "<td height='22' align='center'><span title=\""+item.Title+"\">" +testTitle+ "</td>"+
                        "<td height='22' align='center'>"+ item.FromTypeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.ReportNo +"</a></td>"+
                        "<td height='22' align='center'>"+ item.ProNo +"</a></td>"+
                        "<td height='22' align='center'>"+ item.ProductName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.EmployeeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.ProcessDate +"</a></td>"+
                        "<td height='22' align='center'>"+ item.BillStatus +"</a></td>"+                   
                        "<td height='22' align='center'>"+ item.FlowStatus +"</a></td>").appendTo($("#pageDataList1 tbody"));}
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"SearchStorageNoPassData({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  //$("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById('Text2').value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagePurPlancount"));
                  $("#ToPage").val(pageIndex);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
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
                this.pagecount=parseInt(newPageCount);
//                alert(this.pagecount);alert(this.CheckResult);
//                if(parseFloat(this.pagecount)>parseFloat(this.CheckResult))
//                {
//                       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
//                       return false;
//                }
                SearchStorageNoPassData(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
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
        SearchStorageNoPassData(1);
    }

function Fun_Delete_NoPass()
{
  if(!window.confirm("确认执行删除操作吗？"))
  {
    return false;
  }
    var cb=document.getElementsByName('cbboxall');
    var OptionCheckArray='';
    var theFlag=false;
    for(var i=0;i<cb.length;i++)
    {
        if(cb[i].checked)
        {

            OptionCheckArray=OptionCheckArray+','+cb[i].value.split('|')[0];
            theFlag=true;  
            if(cb[i].value.split('|')[1]!='1' || cb[i].value.split('|')[2]!='0')
            {
                 popMsgObj.ShowMsg('只有单据状态为“制单”且审批状态为空的记录才可以被删除！');
                 return false;
            }           
        }
    }
    if(!theFlag)
    {
        popMsgObj.ShowMsg('请选择要删除的不合格品处置单!');
        return false;
    }
    var Action='Del';

    var UrlParam = "Action="+Action+"&strID="+OptionCheckArray.toString();
                   
        $.ajax({ 
              type: "POST",
              url: "../../../Handler/Office/StorageManager/StorageNoPassAdd.ashx?"+UrlParam,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                 //AddPop();
              }, 
              error: function() 
              {
                popMsgObj.ShowMsg('删除不合格品处置单时请求发生错误');
                
              }, 
              success:function(data) 
              { 
                popMsgObj.ShowMsg(data.info);
                
                if(data.sta>0)
                {
                    SearchStorageNoPassData(1);
                }
              } 
           });
    
}
function IsOut() //判断第一次导出
{     
    if(!parseFloat(totalRecord)>0)
    {       
        alert('请先检索！');
        return false;
    }
}
