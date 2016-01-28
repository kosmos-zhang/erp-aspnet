
var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式

var currentPageIndex = 1;
var action = "";//操作
var orderBy = "RequireDate_d";//排序字段
$(document).ready(function()
{
    var requestobj = GetRequest();
 
    var Action = requestobj['ActionPlan'];
    if(Action == "FromPlan")
    {//从生成计划返回
        fnSetSltCnd(requestobj);
            $("#SearchCondition").val(fnGetSltCnd());
        TurnToPage(currentPageIndex);
   
     

    TurnToPage(1);
    }
    
});

//全选
function SelectAll() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

function ClearPkroductInfo()
{
$("#txtProductID").val('');
    $("#txtProductNo").val('');
    $("#txtProductName").val('');
    closeProductdiv();
}

function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{
    var RowID = popTechObj.InputObj;
    $("#txtProductID").val(id);
    $("#txtProductNo").val(no);
    $("#txtProductName").val(productname);
//    $("#"+RowID).val(price);
//    $("#DtlSUnitID"+RowID).val(unitid);
//    $("#DtlSUnitName"+RowID).val(unit);
//    $("#DtlSUnitPrice"+RowID).val(taxrate);
//    $("#"+RowID).val(taxprice);
//    $("#"+RowID).val(discount);
//    $("#DtlSSpecification"+RowID).val(standard);
//    fnMergeDetail();
}

function fnGetSltCnd()
{
    var ProductTypeID = $("#txtProductTypeID").val();
    var ProductTypeName = $("#txtProductTypeName").val();
    var ProductID = $("#txtProductID").val();
    var ProductName = $("#txtProductName").val();
    var CreateCondition = $("#ddlCreate").val();
    var StartDate = $("#txtStartRequireDate").val();
    var EndDate = $("#txtEndRequireDate").val();
    
    var strParams = "&ProductTypeID="+ProductTypeID;
    strParams += "&ProductTypeName="+ProductTypeName;
    strParams += "&ProductID="+ProductID;
    strParams += "&ProductName="+ProductName;
    strParams += "&CreateCondition="+CreateCondition;
    
    strParams += "&StartDate="+StartDate;
    strParams += "&EndDate="+EndDate;
    
    
    return strParams;
}

function fnMergeDetail()
{
}

function fnSetSltCnd(requestobj)
{
    $("#txtProductTypeID").val(requestobj['ProductTypeID']);
    $("#txtProductTypeName").val(requestobj['ProductTypeName']);
    $("#txtProductID").val(requestobj['ProductID']);
    $("#txtProductName").val(requestobj['ProductName']);
    $("#ddlCreate").val(requestobj['CreateCondition']);
    $("#txtStartRequireDate").val(requestobj['StartDate']);
    $("#txtEndRequireDate").val(requestobj['EndDate']);
    $("#txtProductTypeID").val(requestobj['ProductTypeID']);
    
    pageCount = requestobj['pageCount'];
    currentPageIndex = requestobj['pageIndex'];
}
//获取url中"?"符后的字串
function GetRequest()
 {
   var url = location.search; 
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
  
  
function  SearchPurchaseRequire()
{
    if(fnCheck())
    return;    
    $("#SearchCondition").val(fnGetSltCnd());
    TurnToPage(1);
}



function TurnToPage(pageIndex)
{
currentPageIndex = pageIndex;
    document.getElementById("checkall").checked = false;
    //检索时将检索条件放入隐藏域
    var URLParams = $("#SearchCondition").val();
    URLParams += "&orderby="+orderBy;
    URLParams += "&pageCount="+pageCount;
    URLParams += "&pageIndex="+currentPageIndex;
   
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseRequireInfo.ashx?'+URLParams,//目标地址
           cache:false,
//           data: URLParams,
           beforeSend:function(){AddPop();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j = 1;
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1' onclick=\"IfSelectAll('Checkbox1','checkall')\"  value="+(j++)+"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' id='MRPNo"+i+"'>" + item.MRPNo + "</td>"+
                        "<td height='22' align='center' id='ProdNo"+i+"'>"+ item.ProdNo +"</td>"+
                        "<td height='22' align='center' id='ProductName"+i+"'>"+ item.ProductName +"</td>"+
                        "<td height='22' align='center'>"+ item.ProductTypeName +"</td>"+ 
                        "<td height='22' align='center' id='Specification" + i + "'>" + item.Specification + "</td>" +
                        "<td height='22' align='center' id='ColorName" + i + "'>" + item.ColorName + "</td>" + 
                        "<td height='22' align='center' id='UnitName"+i+"'>"+ item.UnitName +"</td>"+
                        "<td height='22' align='center'>"+  (parseFloat(item.NeedCount)).toFixed($("#HiddenPoint").val()) +"</td>"+
                        "<td height='22' align='center'>"+      (parseFloat(item.HasNum)).toFixed($("#HiddenPoint").val()) +"</td>"+
                        "<td height='22' align='center' id='WantingNum"+i+"'>"+        (parseFloat(item.WantingNum)).toFixed($("#HiddenPoint").val())+"</td>"+
                        
                        "<td height='22' align='center'>"+ item.WaitingDays +"</td>"+
                        "<td height='22' align='center' id='OrderCount"+i+"'>"+     (parseFloat(item.OrderCount)).toFixed($("#HiddenPoint").val()) +"</td>"+
                        "<td height='22' align='center' id='RequireDate"+i+"'>"+ item.RequireDate +"</td>"+
                        "<td height='22' align='center' style='display:none' id='ID"+i+"'>"+ item.ID +"</td>"+
                        
                        "<td height='22' align='center' style='display:none' id='ProductID"+i+"'>"+ item.ProdID +"</td>"+
                        "<td height='22' align='center' style='display:none' id='UnitID"+i+"'>"+ item.UnitID +"</td>").appendTo($("#pageDataList1 tbody"));
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
                  document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagePurRequirecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList4("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}
function GeneratePurPlan()
{
    var ck = document.getElementsByName("Checkbox1");
    var table=document.getElementById("pageDataList1");
    var SourcePage = "PurRequire";
    var URLParams = "SourcePage="+SourcePage;
    URLParams += "&orderby="+orderBy;
    URLParams += "&pageCount="+pageCount;
    URLParams += "&pageIndex="+currentPageIndex;
    URLParams += "&ModuleID=2041501"
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            var OrderCount = $("#WantingNum"+i).html()-$("#OrderCount"+i).html();
            if(OrderCount>0)
            {//没有生成采购计划，可以生成
                URLParams+="&ID"+index+"="+$("#ID"+i).html();//需求 ID
                URLParams+="&MRPNo"+index+"="+$("#MRPNo"+i).html();//需求 ID
                URLParams+="&ProductID"+index+"="+$("#ProductID"+i).html();
                URLParams+="&ProductNo"+index+"="+$("#ProdNo"+i).html();
                URLParams+="&ProductName"+index+"="+escape($("#ProductName"+i).html());
                URLParams+="&Specification"+index+"="+escape($("#Specification"+i).html());
                URLParams+="&UnitID"+index+"="+$("#UnitID"+i).html();
                URLParams+="&UnitName"+index+"="+escape($("#UnitName"+i).html());
                URLParams+="&RequireCount"+index+"="+OrderCount;
                URLParams+="&RequireDate"+index+"="+$("#RequireDate"+i).html();
                URLParams += "&ColorName" + index + "=" + $("#ColorName" + i).html();
                
                index++;
            }
            else
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只有没有生成采购计划的采购需求才能生成采购计划！");
                return;
            }
        }       
    }
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","您还未选择可以生成采购计划的采购需求！");
        return;
    }
    
    URLParams+="&Length="+index;
    //检索条件，用于返回时使用
    URLParams += $("#SearchCondition").val();
    window.location.href='PurchasePlan_Add.aspx?'+URLParams;
}

function pageDataList4(o,a,b,c,d){
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

function DeletePurRequireInfo()
{
    var ck = document.getElementsByName("Checkbox1");
    var table=document.getElementById("pageDataList1");
    action = "Delete";
    var URLParams = "action="+action;
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            if($("#WantingNum"+i).html() <= $("#OrderCount"+i).html())
            {//已经生成采购计划，可以删除
                URLParams+="&ID"+(index++)+"="+$("#ID"+i).html();
            }
            else
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能删除已生成采购计划的采购需求！");
                return;
            }
        }       
    }
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","您还未选择可以删除的项！");
        return;
    }
    
    URLParams+="&Length="+index+"";
    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseRequireInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    TurnToPage(1);
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){}
           });
}

 function ClearInput()
{
    $("#pageDataList1 tbody").find("tr.newrow").remove();
    document.getElementById("txtMaterialClass").value = "";
    document.getElementById("txtMaterialClass0").value = "";
    document.getElementById("txtMaterialNo").title = "";
    document.getElementById("txtMaterialID").value = "";
    document.getElementById("txtMaterialName").value = "";
    document.getElementById("ddlCreate").value = "2";
    document.getElementById("txtStartRequireDate").value = "";
    document.getElementById("txtEndRequireDate").value = "";
    
    document.getElementById("pagePurRequirecount").style.display = "none";
//    document.getElementById("divpage").style.display = "none";
    document.getElementById("pageDataList1_Pager").style.display = "none";
}
function ShowProductType()
{
    var url="../../../Pages/Office/PurchaseManager/ProductType.aspx";
    var returnValue = window.showModalDialog(url, "ProductType", "dialogWidth=600px;dialogHeight=400px");
      if(returnValue!="" && returnValue!=null)
    {
      var info=returnValue.split("|");
      document.getElementById("txtProductTypeID").value=info[0].toString();
      document.getElementById("txtProductTypeName").value=info[1].toString();
 }
}
  
function Ifshow(count)
{
    if(count=="0")
    {
        document.all["divpage"].style.display = "none";
        document.all["pagePurRequirecount"].style.display = "none";
    }
    else
    {
        document.all["divpage"].style.display = "block";
        document.all["pagePurRequirecount"].style.display = "block";
    }
}

function SelectDept(retval)
{
}


//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    //判断是否是数字
    if (!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示必须为正整数！");
        return;
    }
    if (!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数必须为正整数！");
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
    if($("#SearchCondition").val() == "")
        return;
    var ordering = "a";
    var orderby2="ASC";
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
        orderby2 = "DESC";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+"_"+ordering;
    orderby2 = orderColum+" "+orderby2;
    $("#hidOrderBy").val(orderby2);
    TurnToPage(1);
}

function fnCheck()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
   if($("#txtStartRequireDate").val() !=''  && $("#txtEndRequireDate").val() !='' )
   {
        if($("#txtStartRequireDate").val() > $("#txtEndRequireDate").val())
        {
            isErrorFlag = true;
            msgText += "起始需求时间不能大于终止需求时间！";
        }
    }
    
    
    if(isErrorFlag)
    {
        popMsgObj.ShowMsg(msgText);
    }
    return isErrorFlag;
}



    function shotypeList() {
 
        var list = document.getElementById("Product_type");
 
        if (list.style.display != "none") {
            list.style.display = "none";
            return;
        }
 list.style.display = "block";
        var pos = eleTypePos(document.getElementById("txtProductTypeName"));

        list.style.left = pos.x;
        list.style.top = pos.y + 20;
        document.getElementById("Product_type").style.display = "block";
        document.getElementById("Product_type").style.top = pos.y +100 ;
        document.getElementById("Product_type").style.left = pos.x + 60;
    }
    
    
        function eleTypePos(et) {

        var left = -140;
        var top = -145;
        while (et.offsetParent) {
            left += et.offsetLeft;
            top += et.offsetTop;
            et = et.offsetParent;
        }
        left += et.offsetLeft;
        top += et.offsetTop;
        return { x: left, y: top };
    }

    function hideTypeList() {
     document.getElementById("txtProductTypeID").value = '';
        document.getElementById("txtProductTypeName").value = '';
        document.getElementById("Product_type").style.display = "none";
    }
    
    
        function SelectedTypeChanged1(code_text, type_code) {
        
 
        document.getElementById("txtProductTypeID").value = type_code;
        document.getElementById("txtProductTypeName").value = code_text;
     
        document .getElementById ("Product_type").style.display="none";
    }