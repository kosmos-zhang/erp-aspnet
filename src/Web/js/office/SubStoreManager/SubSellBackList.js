var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式

var currentPageIndex = 1;
var currentpageCount = 10;
var action = "";//操作
var orderBytt = "BackNo ASC";//排序字段
var Isliebiao ;

var ifdel="0";//是否删除
var issearch="";
    
$(document).ready(function()
{
    IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
    requestobj = GetRequest(location.search); 
    if(requestobj["BackNo"] != null)
    {
        fnSetSearchCondition(requestobj);
        SearchSubSellBack();
    }
});

function fnGetSearchCondition()
{
    var Info = "";
    Info += "BackNo="+$("#txtBackNo").val();
    Info += "&Title="+escape($("#txtTitle").val());
    Info += "&OrderNo="+$("#txtOrderID").val();
    Info += "&OrderID="+$("#HidOrderID").val();
    Info += "&CustName="+escape($("#txtCustName").val());
    
    Info += "&CustTel="+$("#txtCustTel").val();
    Info += "&DeptID="+$("#HidDeptID").val();
    Info += "&DeptName="+escape($("#DeptDeptID").val());
    Info += "&Seller="+$("#HidSeller").val();
    
    Info += "&SellerName="+escape($("#UserSeller").val());
    Info += "&BusiStatus="+$("#drpBusiStatus").val();
    Info += "&BillStatus="+$("#ddlBillStatus").val();
    Info += "&CustAddr="+escape($("#txtCustAddr").val());
    
    $("#SearchCondition").val(Info);  
}

function fnSetSearchCondition(requestobj)
{
    $("#txtBackNo").val(requestobj['BackNo']);
    $("#txtTitle").val(requestobj['Title']);
    $("#txtOrderID").val(requestobj['OrderNo']);
    $("#HidOrderID").val(requestobj['OrderID']);
    $("#txtCustName").val(requestobj['CustName']);
    
    $("#txtCustTel").val(requestobj['CustTel']);
    $("#HidDeptID").val(requestobj['DeptID']);    
    $("#DeptDeptID").val(requestobj['DeptName']);
    $("#HidSeller").val(requestobj['Seller']);
    $("#UserSeller").val(requestobj['SellerName']);
    
    $("#drpBusiStatus").val(requestobj['BusiStatus']);
    $("#ddlBillStatus").val(requestobj['BillStatus']);
    $("#txtCustAddr").val(requestobj['CustAddr']);
    
    currentPageIndex = requestobj["pageIndex"];
    currentpageCount = requestobj["pageCount"];
} 

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
  
  
  
function fnToEdit(ID,BusiStatusName,BillStatusName)
{
    var URLParams = $("#SearchCondition").val();
    URLParams += "&ID="+ID;
    URLParams += "&ModuleID=2121203";
    var Page = "List";
    URLParams += "&Page="+Page; 
    URLParams += "&pageIndex="+currentPageIndex;
    URLParams += "&pageCount="+currentpageCount;
    URLParams += "&orderBy="+orderBy;  //2121204
    URLParams+="&BusiStatusName="+BusiStatusName;
    URLParams+="&BillStatusName="+BillStatusName;
    window.location.href = 'SubSellBackAdd.aspx?'+URLParams;
}  
    //jQuery-ajax获取JSON数据
function TurnToPage(pageIndex)
{
    
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
    var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
    currentPageIndex = pageIndex;
    var URLParams = "pageIndex="+pageIndex
                    +"&pageCount="+currentpageCount
                    +"&action="+action
                    +"&orderby="+orderBytt
                    +"&"+document.getElementById("SearchCondition").value
                    +"&EFIndex="+escape(EFIndex)
                    +"&EFDesc="+escape(EFDesc);
    $.ajax({
    type: "POST",//用POST方式传输
    dataType:"json",//数据格式:JSON
    url:  '../../../Handler/Office/SubStoreManager/SubSellBackInfo.ashx',//目标地址
    cache:false,
    data: URLParams,
    beforeSend:function(){AddPop();$("#pageDataList1_PagerList").hide();},//发送数据之前

    success: function(msg){
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            
            $.each(msg.data,function(i,item){
                var j=i+1;
                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox"+j+"' name='Checkbox'   value="+item.ID+"  type='checkbox'  onclick=IfSelectAll('Checkbox','checkall')  />"+"</td>"+
                
                "<td height='22' align='center' id=\"backno"+j+"\" title=\""+item.BackNo+"\"><a href='#' onclick=fnToEdit('"+item.ID+"','"+item.BusiStatusName+"','"+item.BillStatusName+"')>"+ GetStandardString(item.BackNo,15) +"</td>"+
                "<td height='22' align='center'><span title=\"" + item.Title + "\">"+ GetStandardString(item.Title,15) +"</a></td>"+
                "<td height='22' align='center'><span title=\"" + item.OrderNo + "\">"+ GetStandardString(item.OrderNo,15) +"</a></td>"+
                "<td height='22' align='center'><span title=\"" + item.CustName + "\">"+ GetStandardString(item.CustName,15) +"</a></td>"+
                "<td height='22' align='center'><span title=\"" + item.CustTel + "\">"+ GetStandardString(item.CustTel,15) +"</a></td>"+
                "<td height='22' align='center'><span title=\"" + item.CustAddr + "\">"+ GetStandardString(item.CustAddr,15) +"</a></td>"+
                "<td height='22' align='center'>"+ item.DeptName +"</a></td>"+
                "<td height='22' align='center'>"+ item.SellerName +"</a></td>"+
                "<td height='22' align='center' style='display:none'>"+ item.SendMode +"</a></td>"+
                "<td height='22' align='center' style='display:none'>"+ item.FromType +"</a></td>"+
                "<td height='22' align='center' style='display:none'>"+ item.CurrencyType +"</a></td>"+
                "<td height='22' align='center'  id='BillStatusName"+j+"' class=\""+ item.BillStatusName +"\">"+ item.BillStatusName +"</a></td>"+
                "<td height='22' align='center' id='BusiStatusName"+j+"' class=\""+ item.BusiStatusName +"\"><a href='#' onclick=fnToEdit('"+item.ID+"','"+item.BusiStatusName+"','"+item.BillStatusName+"')>"+item.BusiStatusName+"</a></td>").appendTo($("#pageDataList1 tbody"));
           });
            //页码
           ShowPageBar("pageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
           "<%= Request.Url.AbsolutePath %>",//[url]
            {style:pagerStyle,mark:"pageDataList1Mark",
            totalCount:msg.totalCount,showPageNumber:3,pageCount:currentpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
            onclick:"TurnToPage({pageindex});return false;"}//[attr]
            );document.getElementById("checkall").checked = false;
          totalRecord = msg.totalCount;
          document.getElementById("Text2").value=msg.totalCount;
          $("#ShowPageCount").val(currentpageCount);
          ShowTotalPage(msg.totalCount,currentpageCount,pageIndex,$("#pagecount"));
          $("#ToPage").val(pageIndex);
        },
    error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
    complete:function(){hidePopup();$("#pageDataList1_PagerList").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
    });
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

function SelectAll() {
        $.each($("#pageDataList1 :checkbox"), function(i, obj) {
            obj.checked = $("#checkall").attr("checked");
        });
    }




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
    
    if(!isFlag)
    {
    popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
}

function SearchSubSellBack()
{
    if(!fnCheck())
        return;
    fnGetSearchCondition()
    TurnToPage(currentPageIndex);
}
    
function ClearInput()
{
    $("#pageDataList1 tbody").find("tr.newrow").remove();
    document.getElementById("txtContractNo").value = "";
    document.getElementById("txtTitle").value = "";
    document.getElementById("DrpTypeID").value = "";
    document.getElementById("txtHidOurDept").value = "";
    document.getElementById("txtDeptID").value = "";
    document.getElementById("txtSeller").value = "";
    document.getElementById("txtSeller").title = "";
    document.getElementById("ddlFromType").value = "0";
    document.getElementById("txtHidProviderID").value = "";
    document.getElementById("txtProviderID").value = "";
    document.getElementById("ddlBillStatus").value = "0";
    document.getElementById("ddlUsedStatus").value = "0";
    
    document.getElementById("pagecount").style.display = "none";
    document.getElementById("pageDataList1_PagerList").style.display = "none";
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
            document.getElementById("divpage").style.display = "block";
            document.getElementById("pagecount").style.display = "block";
        }
    }
    
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数必须输入正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数必须输入正整数！');
          return;
       }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return;
        }
        else
        {
            currentpageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
    //排序
function OrderBy(orderColum,orderTip)
{
        if(parseFloat(totalRecord)<1)
        {
          return false;
        }
    var ordering = "ASC";
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "DESC";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBytt = orderColum+" "+ordering;
    SearchSubSellBack();
}
 
    //新建门店销售退货单
function CreateSubSellBack()
{
    var URLParams = $("#SearchCondition").val();
    URLParams += "&ModuleID=2121203"; 
    var Page = "List";
    URLParams += "&Page="+Page;
    URLParams += "&pageIndex="+currentPageIndex;
    URLParams += "&pageCount="+currentpageCount;
    URLParams += "&orderBy="+orderBy;  //2121204
    
    window.location.href = 'SubSellBackAdd.aspx?'+URLParams;
}


function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{  
   
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    
    closeProviderdiv();
}



function  DelSubSellBack()
{ 
    var c=window.confirm("确认执行删除操作吗？")
    if(c==true)
    {
        var signFrame = document.getElementById("pageDataList1");
        var IDs = "";
        var BackNos = "";
        
        var isErrorFlag = false;
        var msgText = "";
        
        var Count = 0;
        
        for(var i=1;i<signFrame.rows.length;++i)
        {
            if(document.getElementById("Checkbox"+i).checked == true)
            {
                var aaa =document.getElementById("BillStatusName"+i+"").className;
                var bbb = document.getElementById("BusiStatusName"+i+"").className;
                if(( aaa== "制单")&&( bbb== "退单"))
                {//可以删
                    IDs += document.getElementById("Checkbox"+i).value;
                    IDs += ",";
                    BackNos +="'";
                    BackNos +=document.getElementById("backno"+i).title;
                    BackNos +="'";
                    BackNos +=",";
                    Count++;
                } 
                else
                {//不可以删
                    isErrorFlag = true;
    //                msgText += "已确认后的单据不允许删除！";
                }           
            }
            
        }
        IDs=IDs.slice(0,IDs.length-1);
        BackNos=BackNos.slice(0,BackNos.length-1);
        if(isErrorFlag)
        {msgText += "已确认后的单据不允许删除！";
            popMsgObj.ShowMsg(msgText);
            return;
        }
        if(Count == 0)
        {
            msgText +="请选择要删除的销售退单！";
            popMsgObj.ShowMsg(msgText);
            return;
        }
        
        
        var Action = "Delete";
        var URLParams = "";
        URLParams += "&Action="+Action;
        URLParams += "&IDs="+IDs;
        URLParams += "&BackNos="+BackNos;
        $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  '../../../Handler/Office/SubStoreManager/SubSellBackInfo.ashx?'+URLParams,//目标地址
               cache:false,
               beforeSend:function(){},//发送数据之前
                error: function() 
                    {
                       popMsgObj.ShowMsg('请求发生错误！');
                       return;
                    }, 
               success: function(msg){
                        SearchSubSellBack();
                        popMsgObj.ShowMsg('删除成功！');
                      },
               error: function() {}, 
               complete:function(){}
               });
    }
}



function clearProviderdiv()
{
    document.getElementById("txtProviderID").value = "";
    document.getElementById("txtHidProviderID").value = "";
}



//选择分店销售订单带出相关信息
function FillFromSubSellOrder(id,orderno,sendmode,outdate,outuserid,outusername,custname,custtel,custmobile,custaddr)
{  
    document.getElementById("HidOrderID").value = id;
    document.getElementById("txtOrderID").value = orderno;
    
    closeSubSellOrderdiv();
}

function IsOut() //判断第一次导出
{     
    if(!parseFloat(totalRecord)>0)
    {       
        alert('请先检索！');
        return false;
    }
}

function clearSubSellOrderdiv()
{
    $("#txtOrderID").val("");
    $("#HidOrderID").val("");
    closeSubSellOrderdiv();
}
