    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var ifshow="0";
    var currentPageIndex = 1;
    var orderBy = "ModifiedDate_d";//排序字段
    var reset="1";
    var action="";

$(document).ready(function()
{

    var requestobj = GetRequest(location.search);
    var SourcePage = requestobj['SourcePage'];
    if( SourcePage == "Add")
    {//编辑页面进入
        if(requestobj['No'] != null)
        {//询价编号传过去了，说明之前点击了检索，设置检索条件，然后检索
            //设置检索条件
            fnSetSelectCondition(requestobj);
            //将检索条件放入隐藏域
            fnGetSelectCondition();
            //检索
            TurnToPage(currentPageIndex);
        }
        else
        {//将检索条件设为空，并且不进行检索操作
            
        }
    }
    else if(SourcePage == null)
    {//左侧菜单栏进入
        
    }
       IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
});

//全选
function SelectAll() {
    $.each($("#PurOrderInfo :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
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

//排序
    function OrderByAsk(orderColum,orderTip)
    {
    if($("#SearchCondition").val() == "")
        return;
        ifshow="0";
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
        TurnToPage(1);
    }
function TurnToPage(pageIndex)
{
currentPageIndex = pageIndex;
    //获取检索条件
    document.getElementById("checkall").checked = false;
//    fnGetSelectCondition();
    var URLParams = $("#SearchCondition").val();
    
    //分页和排序信息
    URLParams += fnGetPageInfo();

    URLParams += "&ActionOrder=SelectSD";
   // URLParams = "&No=&Title=&TypeID=&DeptID=&DeptName=&PurchaseID=&PurchaseName=&FromType=10&ProviderID=&ProviderName=&BillStatus=0&FlowStatus=0&ProjectID=&ProjectName=&EFIndex=&EFDesc=&pageCount=10&pageIndex=1&orderBy=ModifiedDate_d&ActionOrder=Select ";
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseOrderInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#PurOrderInfo tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        var isOpenBillName;
                        //是否已建单
                        if(item.isOpenBill == 0)
                        {
                            isOpenBillName = "否";
                        }
                        else
                        {
                            isOpenBillName = "是";
                        }
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox"+(i + 1)+"' name='Checkbox' onclick=\"IfSelectAll('Checkbox','checkall')\" value="+item.ID+"   type='checkbox'/>"+"</td>"+
//                        "<td height='22' align='center'><a href='#' onclick=SelectDept('"+item.ApplyIndex+"')>"+ item.ApplyNo +"</a></td>"+
                        "<td height='22' align='center' id='OrderNo"+ (i + 1) +"' title=\""+item.OrderNo+"\" ><a href='#' onclick=SelectPurchaseOrder('"+item.ID+"')>"+ GetStandardString(item.OrderNo,25) +"</a></td>"+
                        "<td height='22' align='center' id='ordernohidden"+(i+1)+"' style='display:none'>"+ item.OrderNo +"</td>"+
                        "<td height='22' align='center' title=\""+item.OrderTitle+"\">"+ GetStandardString(item.OrderTitle,15) +"</td>"+
                        "<td height='22' align='center'>"+ item.TypeName +"</td>"+
                        "<td height='22' align='center'>"+ item.PurchaserName +"</td>"+
                        "<td height='22' align='center'>"+ item.ProviderName +"</td>"+ 
                        "<td height='22' align='center'>"+   (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val())+"</td>"+
                        "<td height='22' align='center'>"+ (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val()) +"</td>"+
                        "<td height='22' align='center'>"+ (parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val())+"</td>"+
                        "<td height='22' align='center' id='BillStatusName"+(i + 1)+"'>"+ item.BillStatusName +"</td>"+
                        "<td height='22' align='center' id='FlowStatusName"+(i + 1)+"'>"+ item.FlowStatusName +"</td>"+
                        "<td height='22' align='center' id='FlowStatusName"+(i + 1)+"'>"+ isOpenBillName +"</td>"
                        ).appendTo($("#PurOrderInfo tbody")
                        
                        );
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
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("PurOrderInfo","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
      });
}
//********************************************Start 界面取值和赋值*********************************************************
//界面取值放入隐藏域
function fnGetSelectCondition()
{
    var strParams = "";
    var OrderNo = document.getElementById("txtOrderNo").value.Trim();
    var OrderTitle = document.getElementById("txtOrderTitle").value.Trim();
    var TypeID = document.getElementById("ddlTypeID_ddlCodeType").value.Trim();
    var DeptID = document.getElementById("hidDeptID").value.Trim();
    var DeptName = $("#DeptName").val();
    var PurchaseID = document.getElementById("hidPurchaseID").value.Trim();
    var PurchaseName = $("#UserPurchaseName").val();
    var FromType = document.getElementById("ddlFromType").value.Trim();
    var ProviderID = document.getElementById("hidProviderID").value.Trim();
    var ProviderName = $("#txtProviderName").val();
    var BillStatus = document.getElementById("ddlBillStatus").value.Trim();
    var FlowStatus = document.getElementById("ddlFlowStatus").value.Trim();
    
    strParams += "&No="+OrderNo;
    strParams += "&Title="+OrderTitle;
    strParams += "&TypeID="+TypeID;
    strParams += "&DeptID="+DeptID;
    strParams += "&DeptName="+DeptName;
    strParams += "&PurchaseID="+PurchaseID;
    strParams += "&PurchaseName="+PurchaseName;
    strParams += "&FromType="+FromType;
    strParams += "&ProviderID="+ProviderID;
    strParams += "&ProviderName="+ProviderName;
    strParams += "&BillStatus="+BillStatus;
    strParams += "&FlowStatus="+FlowStatus;
        var ProjectID = $("#hidProjectID").val();
         strParams += "&ProjectID="+ProjectID;
         strParams += "&ProjectName="+$("#txtProject").val();
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;
           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;

 strParams += "&EFIndex="+escape(EFIndex);
    strParams += "&EFDesc="+escape(EFDesc); 
    $("#SearchCondition").val(strParams);
}

//界面赋值
function fnSetSelectCondition(requestObj)
{
    $("#txtOrderNo").val(requestObj['No']);
    $("#txtOrderTitle").val(requestObj['Title']);
    $("#ddlTypeID_ddlCodeType").val(requestObj['TypeID']);
    $("#hidDeptID").val(requestObj['DeptID']);
    $("#DeptName").val(requestObj['DeptName']);
    
    $("#hidPurchaseID").val(requestObj['PurchaseID']);
    $("#UserPurchaseName").val(requestObj['PurchaseName']);
    $("#ddlFromType").val(requestObj['FromType']);
    $("#hidProviderID").val(requestObj['ProviderID']);
    $("#txtProviderName").val(requestObj['ProviderName']);
    $("#ddlBillStatus").val(requestObj['BillStatus']);
    $("#ddlFlowStatus").val(requestObj['FlowStatus']);
    
     $("#hidProjectID").val(requestObj['ProjectID']);
    $("#txtProject").val(requestObj['ProjectName']);
    
     
        
         
    pageCount = parseInt(requestObj['pageCount']);
    currentPageIndex = parseInt(requestObj['pageIndex']);
}
//********************************************End   界面取值和赋值*********************************************************





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

function pageDataList1(o,a,b,c,d)
{
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		//t[i].onclick=function(){//鼠标点击
			//if(this.x!="1"){
				//this.x="1";//
				//this.style.backgroundColor=d;
			//}else{
			//	this.x="0";
				//this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
			//}
		//}
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    //判断是否是数字
    if (!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    }
    if (!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
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

//********************************************Start 检    索*********************************************************
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


function DoSearch()
{
    if(!fnCheck())
    return;
    //界面取值放入隐藏域
    fnGetSelectCondition();
    
    TurnToPage(1);
}

//获取页码和排序信息
function fnGetPageInfo()
{
    var Info = "&pageCount="+pageCount;
    Info += "&pageIndex="+currentPageIndex;
    Info += "&orderBy="+orderBy;
    return Info;
}

//********************************************End   检    索*********************************************************


function DoNew()
{
    var URLParams = $("#SearchCondition").val();
    
    URLParams += fnGetPageInfo();
    
    URLParams += "&ActionOrder=Add";
    
    URLParams += "&SourcePage=Info";
    URLParams += "&ModuleID=2041801";

    window.location.href='PurchaseOrderAdd.aspx?'+URLParams;
}

function SelectPurchaseOrder(ID)
{//链接
    var URLParams = $("#SearchCondition").val();
    
    URLParams += fnGetPageInfo();
    
    URLParams += "&ActionOrder=Fill";
    URLParams += "&SourcePage=Info";
    URLParams += "&ID="+ID;
    
    URLParams += "&ModuleID=2041801";
    
    window.location.href='PurchaseOrderAdd.aspx?'+URLParams;
}

function DeletePurOrder()
{
    var signFrame = document.getElementById("PurOrderInfo");
    var IDs = "(";
    var OrderNos = "(";
    
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
                OrderNos +="'";
                OrderNos +=document.getElementById("ordernohidden"+i).innerHTML;
                OrderNos +="'";
                OrderNos +=",";
                Count++;
            } 
            else
            {//不可以删
                isErrorFlag = true;
                
            }           
        }
        
    }
    IDs=IDs.slice(0,IDs.length-1);
    OrderNos=OrderNos.slice(0,OrderNos.length-1);
    IDs +=")";
    OrderNos +=")";
    if(isErrorFlag)
    {
//    alert(msgText)
msgText += "已提交审批或已确认后的单据不允许删除！";
        popMsgObj.ShowMsg(msgText);
        return;
    }
    if(Count == 0)
    {
        msgText +="请选择要删除的采购订单！";
        popMsgObj.ShowMsg(msgText);
        return;
    }
    if(!confirm("确认执行删除操作么？"))
    return;
    var ActionOrder = "Delete";
    var URLParams = "";
    URLParams += "&ActionOrder="+ActionOrder;
    URLParams += "&IDs="+IDs;
    URLParams += "&OrderNos="+OrderNos;
    
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseOrderInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());
                msgText +="删除成功！";
                popMsgObj.ShowMsg(msgText);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("PurOrderInfo","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
      });
    
}