    var pagecount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var ifshow="0";
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    var reset="1";
    var action="";
    var isSearch=0;

window.onload=function()
{
    IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
    if(document.getElementById('isreturn').value=='1')
    {
        SearchStorageReport(document.getElementById('myPageIndex').value);
    }
}
function FistSearchReport()
{   
      if(CheckInput())
      {
      SearchStorageReport(1);
      }
     
}
function GetFloat(a)
{
   if(a!=null && a!='')
   {
        var testFlaot=a.toString().indexOf('.');
        var myValue=a;
        if(testFlaot!=-1)
        {
            var length=a.length;
            for(var i=0;i<length;i++)
            {
                
            }            
            var testvalue=a.toString().split('.');
            var myvalue=testvalue[1];
            
            if(myvalue=='00' || myvalue=='0' || myvalue =='000' || myvalue=='0000')
            {
                myValue= testvalue[0];
            } 
            if(myvalue.toString().length>2 && myvalue!='000' && myvalue!='0000')
            {
                myValue= parseFloat(a.toString()).toFixed(2);
            }       
            
        }
        return myValue;
    }
}
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var ReportNo=document.getElementById('txtReportNo').value;
    var Title=document.getElementById('txtSubject').value;
    //先检验页面上的特殊字符
      var RetVal=CheckSpecialWords();
      if(RetVal!="")
      {
          isFlag = false;
          fieldText = fieldText + RetVal+"|";
          msgText = msgText +RetVal+  "不能含有特殊字符|";
      }
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
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
function SearchStorageReport(pageIndex)
{//检索
    //检索条件
    pageIndex=parseFloat(pageIndex);
    
    document.getElementById('myPageIndex').value=parseFloat(pageIndex);
    document.getElementById('isreturn').value='1';
    if(document.getElementById('ShowPageCount').value!='10' && document.getElementById('ShowPageCount').value!='')
      pagecount=document.getElementById('ShowPageCount').value;
    var txtReportNo =document.getElementById("txtReportNo").value;  //源单编号//
    var txtTitle = document.getElementById("txtSubject").value; //   
    var sltFromType = document.getElementById("sltFromType").value;
    var ReportID = document.getElementById("tbReportNo").value;//    
    var sltCheckType = document.getElementById("sltCheckType").value;    
    var sltCheckMode = document.getElementById("sltCheckMode").value;    
    var Checker=document.getElementById("hiddenChecker").value; //   
    var CheckDept = document.getElementById("hiddenCheckDept").value; //   
    var FlowStatus = document.getElementById("ddlFlowStatus").value;    
    var BillStatus = document.getElementById("sltBillStatus").value;    
    var BeginTime=document.getElementById('BeginTime').value;
    var EndTime=document.getElementById('EndTime').value;
    currentPageIndex = pageIndex;
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值

    var MyData = "pageIndex="+pageIndex
                +"&pageCount="+pagecount
                +"&action="+action
                +"&orderBy="+orderBy
                +"&txtReportNo="+escape(txtReportNo)
                +"&txtTitle="+escape(txtTitle)
                +"&sltFromType="+escape(sltFromType)
                +"&ReportID="+escape(ReportID)
                +"&sltCheckType="+escape(sltCheckType)
                +"&sltCheckMode="+escape(sltCheckMode)
                +"&Checker="+Checker
                +"&CheckDept="+escape(CheckDept)
                +"&FlowStatus="+escape(FlowStatus)
                +"&BillStatus="+escape(BillStatus)
                +"&BeginTime="+escape(BeginTime)
                +"&EndTime="+escape(EndTime)
                +"&EFIndex="+escape(EFIndex)
                +"&EFDesc="+escape(EFDesc); 

    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageReportList.ashx',//目标地址
           cache:false,
            data: MyData,//数据
           beforeSend:function(){AddPop();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                 
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                        isSearch=1;
                        var testReportNo=item.ReportNo;
                        var testTitle=item.Title;
                        if(testTitle!=null)
                        {
                            if(testTitle.length>15)
                            {
                                testTitle=testTitle.substring(0,15)+'...';
                            }
                        }
                        if(testReportNo!=null)
                        {
                            if(testReportNo.length>15)
                            {
                                testReportNo=testReportNo.substring(0,15)+'...';
                            }
                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input onclick=\"IfSelectAll('cbboxall','cbSelectAll');\" id='cbboxall"+item.ID+"' name='cbboxall'  value="+item.ID+'|'+item.BillStatusID+'|'+item.FlowStatusID+"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+                                                
                        "<td height='22' align='center'><a title=\""+item.ReportNo+"\"  href=StorageCheckReportAdd.aspx?myAction=edit&ModuleID=2071201&ID="+item.ID+"&BillStatusID="+item.BillStatusID+">"+testReportNo +"</a></td>"+
                        "<td height='22' align='center'><span title=\""+item.Title+"\">" +testTitle+ "</span></td>"+
                        "<td height='22' align='center'>"+ item.FromTypeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.OtherCorpName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.BigTypeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CheckTypeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CheckModeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.EmployeeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.DeptName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CheckDate +"</a></td>"+
                        "<td height='22' align='center'>"+ item.BillStatus +"</a></td>"+                       
                        "<td height='22' align='center'>"+ item.FlowStatus +"</a></td>").appendTo($("#pageDataList1 tbody"));}
                   });
                    //页码
                       ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                       "<%= Request.Url.AbsolutePath %>",//[url]
                        {style:pagerStyle,mark:"pageDataList1Mark",
                        totalCount:msg.totalCount,showPageNumber:3,pageCount:pagecount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                        onclick:"SearchStorageReport({pageindex});return false;"}//[attr]
                        );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                      document.getElementById("Text2").value=msg.totalCount;
                      $("#ShowPageCount").val(pagecount);
                      ShowTotalPage(msg.totalCount,pagecount,pageIndex);
                      $("#ToPage").val(pageIndex);
                      ShowTotalPage(msg.totalCount,pagecount,pageIndex,$("#pagecount"));
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow1(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
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
function storageQualityDelete()
{
  var idlist=getchecked();
  
  if(idlist,length<=0)
    {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选中需要删除的项");
    }
  var para="id="+idlist+"&action=DELETE";
  var url="../../../Handler/Office/StorageManager/StorageQualityEdit.ashx";
  
   $.ajax({
           type: "POST",//用POST方式传输
           dataType:"string",
           url: url,//目标地址
           cache:false,
           data: para,//数据
           beforeSend:function(){AddPopReport();$("#pageDataList1_Pager").hide();},
           success: function(msg)
           {    
                  SearchStorageReport(1);
           },
           error: function(){ },
             complete:function(){}//接收数据完毕
            });
}


function EditQuality(a,b,c)
{
    var MyData = "ApplyNo="+a+"&FromType="+b+"&ID="+c+"&action=edit";               
    window.location.href='StorageQualityCheckAdd.aspx?'+MyData;
}
function selectAll()
{
   var objchk=document.getElementById("CheckboxList");
   var objlistchk=document.getElementsByName("Checkbox1");
   var chk=objchk.checked;
   var flag=objchk.checked;
       for(var i=0;i<objlistchk.length;i++)
       {
           objlistchk[i].checked=flag;
       }

}
function Ifshow1(count)
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
    
//    //改变每页记录数及跳至页数
//    function ChangePageCountIndex(newPageCount,newPageIndex)
//    {
//        if(!IsNumber(newPageIndex))
//        {
//            alert('跳转页面必须为正整数格式!');
//            return false;
//        }
//        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
//        {
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请输入正确的页数！");
//            return false;
//        }
//        else
//        {
//            this.pagecount=parseInt(newPageCount);
//            SearchStorageReport(parseInt(newPageIndex));
//        }
//    }
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
                SearchStorageReport(parseInt(newPageIndex));
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
        SearchStorageReport(1);
    }

function AddPopReport()
{
    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
function showPopup(img,img1,retstr)
{
	document.all.Forms.style.display = "";
	document.all.Forms.innerHTML = Create_Div(img,img1,true);
	document.all.FormContent.innerText = retstr;
}  
function hidePopupReport()
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
function Fun_Delete_Report()
{
    if(!window.confirm('确认要进行删除操作吗?'))
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
            if(cb[i].value.split('|')[1]!='1'|| cb[i].value.split('|')[2]!='0')
            {
               
                 popMsgObj.ShowMsg('只有单据状态为“制单”且审批状态为空的记录才可以被删除！');
                 return false;
            }           
        }
    }
    if(!theFlag)
    {
        popMsgObj.ShowMsg('请选择要删除的质检报告单!');
        return false;
    }
    var Action='Del';
    var UrlParam = "Action="+Action+"&strID="+OptionCheckArray.toString();
                   
        $.ajax({ 
              type: "POST",
              url: "../../../Handler/Office/StorageManager/StorageCheckReportAdd.ashx",
              dataType:'json',//返回json格式数据
              data:UrlParam,
              cache:false,
              beforeSend:function()
              { 
                 //AddPopReport();
              }, 
              error: function() 
              {
                popMsgObj.ShowMsg('删除质检报告单时请求发生错误');
                
              }, 
              success:function(data) 
              { 
                popMsgObj.ShowMsg(data.info);
                
                if(data.sta>0)
                {
                    SearchStorageReport(1);
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