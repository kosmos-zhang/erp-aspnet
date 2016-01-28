var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;
var action = "";//操作
var orderBy = "";//排序字段
var isSearch=0; 

window.onload=function()
{
    IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
}

function SelectCust()
{
    var url="../../../Pages/Office/StorageManager/StorageReportCust.aspx";
    var returnValue = window.showModalDialog(url, "", "dialogWidth=300px;dialogHeight=450px");
    if(returnValue!="" && returnValue!=null && returnValue!='clear')
    {
        var  value=returnValue.split("|");
        document.getElementById("txtCustName").value=value[1].toString();
        document.getElementById('hiddenCustID').value=value[0].toString();
    }
    else if(returnValue=='clear')
    {
        document.getElementById("txtCustName").value='';
        document.getElementById('hiddenCustID').value="0";
    }
}

function TurnToPage(pageIndex)
{//检索
   pageIndex=parseFloat(pageIndex);
    document.getElementById('isreturn').value='1';
    if(document.getElementById('ShowPageCount').value!='10' && document.getElementById('ShowPageCount').value!='')
      pageCount=document.getElementById('ShowPageCount').value;
    currentPageIndex = pageIndex;
    //检索条件
    var txtInNo = document.getElementById("txtInNo").value;    
    var txtTitle = document.getElementById("txtTitle").value;
    var txtChecker = document.getElementById("HiddenUser").value;
    var hiddenCustID = document.getElementById("hiddenCustID").value;
    var txtCheckDept = document.getElementById("HiddenDept").value;
    var BeginCheckDate = document.getElementById("BeginCheckDate").value;
    var EndCheckDate=document.getElementById("EndCheckDate").value;
    var BillType = document.getElementById("BillStatus").value;
    var txtCheckType = document.getElementById("txtCheckType").value; 
    var txtCheckMode = document.getElementById("txtCheckMode").value;
    var FromType = document.getElementById("FromType").value;
    var ddlFlowStatus=document.getElementById("ddlFlowStatus").value;
    currentPageIndex = pageIndex;    
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
     
    var URLParams = "pageIndex="+pageIndex
                    +"&pageCount="+pageCount
                    +"&action="+action
                    +"&orderBy="+orderBy
                    +"&txtInNo="+escape(txtInNo)
                    +"&txtTitle="+escape(txtTitle)
                    +"&txtChecker="+escape(txtChecker)
                    +"&hiddenCustID="+escape(hiddenCustID)
                    +"&txtCheckDept="+escape(txtCheckDept)
                    +"&BeginCheckDate="+escape(BeginCheckDate)
                    +"&EndCheckDate="+EndCheckDate
                    +"&BillType="+escape(BillType)
                    +"&txtCheckType="+escape(txtCheckType)
                    +"&txtCheckMode="+escape(txtCheckMode)
                    +"&FromType="+escape(FromType)
                    +"&FlowStatus="+escape(ddlFlowStatus)
                    +"&EFIndex="+escape(EFIndex)
                    +"&EFDesc="+escape(EFDesc); 

    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageQualityCheckList.ashx?',//目标地址
           cache:false,
            data: URLParams,//数据
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
                        var testApplyNo=item.ApplyNo;
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

                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='cb"+item.ID+"' onclick=\"IfSelectAll('Checkbox1','CheckboxList');\"  name='Checkbox1'  value="+item.ID+'|'+item.BillStatusID+'|'+item.FlowStatusID+"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+
                        "<td height='22' align='center'> <a title=\""+item.ApplyNo+"\" href="+EditQuality(item.ApplyNo,item.FromTypeID,item.ID,item.BillStatusID)+">" + testApplyNo + "</td>"+
                        "<td height='22' align='center'><span title=\""+item.Title+"\">"+ testTitle +"</span></a></td>"+
                        "<td height='22' align='center'>"+ item.FromType +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CustName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CustBigType +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CheckMode +"</a></td>"+
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
                        totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                        onclick:"TurnToPage({pageindex});return false;"}//[attr]
                        );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                      document.getElementById("Text2").value=msg.totalCount;
                      $("#ShowPageCount").val(pageCount);
                      ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                      $("#ToPage").val(pageIndex);
                      ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                      },
               error: function() {}, 
               complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
               });
}

function Fun_Search_QualityCheck(aa)
{
    search="1";
    document.getElementById('hiddenBackValue').value='1';
    var txtInNo= document.getElementById("txtInNo").value;
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    if(txtInNo.length>0)
    {
         if(!IsValidSpecialStr(txtInNo))
          {
            isFlag = false;
            fieldText = fieldText + "质检申请单编号|";
            msgText = msgText +  "只允许是字符和数字格式|";
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
    else
    {
        TurnToPage(1);
    }
}

function EditQuality(a,b,c,d)
{
    var URLParams = "ApplyNo="+a+"&ModuleID=2071101&FromType="+b+"&BillStatusID="+d+"&ID="+c+"&action=edit";               
    return 'StorageQualityCheckAdd.aspx?'+URLParams;
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
            TurnToPage(parseInt(newPageIndex));
        }
    }
}
function storageQualityDelete()
{
  if(!window.confirm("确认执行删除操作吗？"))
  {
    return false;
  }
    var cb=document.getElementsByName('Checkbox1');
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
                 popMsgObj.ShowMsg('只有单据状态为"制单"且审批状态为空的记录才可以被删除！');
                 return false;
            }           
        }
    }
    if(!theFlag)
    {
        popMsgObj.ShowMsg('请选择要删除的质检申请单!');
        return false;
    }

        var para="id="+OptionCheckArray.toString()+"&action=DELETE";
        var url="../../../Handler/Office/StorageManager/StorageQualityEdit.ashx";
       $.ajax({
               type: "POST",//用POST方式传输
               dataType:"string",
               url: url,//目标地址
               cache:false,
               data: para,//数据
               beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},
               success: function(data)
               {    
                   popMsgObj.ShowMsg('删除成功！');
                      TurnToPage(1);
               },
               error: function(){ },
                 complete:function(){}//接收数据完毕
                });
 
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
   
    document.getElementById('HiddenOrderID').value=orderBy;//存储列表排序的字段-导出时用到
    TurnToPage(1);
}
    
//全选
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
function IsOut() //判断第一次导出
{     
    if(!parseFloat(totalRecord)>0)
    {       
        alert('请先检索！');
        return false;
    }
}
