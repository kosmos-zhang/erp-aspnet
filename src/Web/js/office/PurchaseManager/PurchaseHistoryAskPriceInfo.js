var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderByVV = "ProductNo_a";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";
    
    $(document).ready(function()
{
    fnGetExtAttr();
    requestobj = GetRequest(); 
    var ProductName = requestobj['ProductName'];
    var ProductID = requestobj['ProductID1'];
    if(ProductID == undefined)
    {
        ProductID ="";
    }
    var StartPurchaseDate = requestobj['StartPurchaseDate'];
    var EndPurchaseDate = requestobj['EndPurchaseDate'];
    var Isliebiao =requestobj['Isliebiao'];
    
    var PageIndex = requestobj['PageIndex'];
    var pageCount = requestobj['pageCount'];
    
       
    
//    if(typeof(ContractNo1)!="undefined")
    if(typeof(Isliebiao)!="undefined")
    { 
       $("#txtProductName").attr("value",ProductName);
       $("#HidProductID").attr("value",ProductID);
       $("#txtStartPurchaseDate").attr("value",StartPurchaseDate);
       $("#txtEndPurchaseDate").attr("value",EndPurchaseDate);
       currentPageIndex = PageIndex;
       currentpageCount = pageCount;
       SearchPurchaseHistoryAskPrice();
    }
});

    
    function ClearPkroductInfo()
{
document .getElementById ("txtProductName").value="";
document .getElementById ("HidProductID").value="";
closeProductdiv();
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
    
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
//           var ProductName = document.getElementById("txtProductName").value;
//           var ProductID = document.getElementById("HidProductID").value;
//           if(ProductID == undefined)
//           {
//                ProductID == "";
//           }
//           var StartPurchaseDate = document.getElementById("txtStartPurchaseDate").value;
//           var EndPurchaseDate = document.getElementById("txtEndPurchaseDate").value;
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseHistoryAskPrice.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+currentpageCount+"&orderby="+orderByVV+document.getElementById("hidSearchCondition").value,
//                 "&StartPurchaseDate="+escape(StartPurchaseDate)+"&EndPurchaseDate="+escape(EndPurchaseDate)+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerList").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){
//                        if(item.ID != null && item.ID != "")
//                        {
                            $("<tr class='newrow'></tr>").append(
//                        "<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+j+"  type='checkbox'/>"+"</td>"+
//                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+
                        "<td height='22' align='center'><a href='" + GetLinkParam() +"&ProductID=" + item.ProductID + " ')>"+item.ProductNo+"</a></td>"+
                

                        "<td height='22' align='center'>"+ item.ProductName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.Specification +"</a></td>"+
                        "<td height='22' align='center'>"+ item.UnitName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.LargeTaxPrice +"</a></td>"+
                        "<td height='22' align='center'>"+ item.SmallTaxPrice +"</a></td>"+
                        "<td height='22' align='center'>"+ item.AverageTaxPrice +"</a></td>"+
                        "<td height='22' align='center'>"+ item.NewTaxPrice +"</a></td>").appendTo($("#pageDataList1 tbody"));
//                        }
                    
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:currentpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
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

function SelectAll() {
        $.each($("#pageDataList1 :checkbox"), function(i, obj) {
            obj.checked = $("#checkall").attr("checked");
        });
    }

/*
* 获取链接的参数
*/
function GetLinkParam()
{
//    //获取模块功能ID
//    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时

    linkParam = "PurchaseHistoryAskPriceShow.aspx?PageIndex=" + currentPageIndex + "&PageCount=" + currentpageCount + "&" + searchCondition + "&Flag=" + flag;
    //返回链接的字符串
    return linkParam;
 
}


function fnPrint()
{
    var URLParams = "orderby="+orderByVV+ document.getElementById("hidSearchCondition").value;

    
    window.open("PurchaseHistoryAskPriceInfoPrint.aspx?"+URLParams);
}

//采购历史价格列表
function SearchPurchaseHistoryAskPrice()
    {
//检索条件
issearch=1;
    $("#btnPrint").css("display","inline");
    
    var ProductID = document.getElementById("HidProductID").value.Trim();
           if(ProductID == undefined)
           {
                ProductID == "";
           }
           var StartPurchaseDate = document.getElementById("txtStartPurchaseDate").value.Trim();
           var EndPurchaseDate = document.getElementById("txtEndPurchaseDate").value.Trim();
           
    
    var ProductName = document.getElementById("txtProductName").value.Trim();
//    var ProductID = document.getElementById("HidProductID").value;
//    var StartPurchaseDate = document.getElementById("txtStartPurchaseDate").value;
//    var EndPurchaseDate = document.getElementById("txtEndPurchaseDate").value;
    if(StartPurchaseDate > EndPurchaseDate)
    {
        alert("起始时间不能大于终止时间!");
        return;
    }
    
//    currentPageIndex = pageIndex;
    var Isliebiao = 1;
    var URLParams = "&Isliebiao="+escape(Isliebiao)+"&ProductName="+escape(ProductName)+"&ProductID="+escape(ProductID)+"&StartPurchaseDate="+escape(StartPurchaseDate)+"&EndPurchaseDate="+escape(EndPurchaseDate)+""; 
    //设置检索条件
    document.getElementById("hidSearchCondition").value = URLParams;
    
      search="1";
      TurnToPage(currentPageIndex);
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
    
    function SelectDept(retval)
    {
        alert(retval);
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
    if(issearch=="")
            return;
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
        orderByVV = orderColum+"_"+ordering;
        TurnToPage(1);
    }
   
//function AddPop()
//{
//    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
//}
//function showPopup(img,img1,retstr)
//{
//	document.all.Forms.style.display = "block";
//	document.all.Forms.innerHTML = Create_Div(img,img1,true);
//	document.all.FormContent.innerText = retstr;
//}  
//function hidePopup()
//{
//    document.all.Forms.style.display = "none";
//}
//function Create_Div(img,img1,bool)
//{
//	FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
//	FormStr += "<tr>"
//	FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
//	FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
//	if(bool)
//	{
//		FormStr += "<img src='" + img +"' style='cursor:hand;display:block;' id='CloseImg' onClick=document.all['Forms'].style.display='none';>"
//	}
//	FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
//	FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
//	FormStr += "<tr>"
//	FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
//	FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
//	FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
//	FormStr += "</tr></table>"
//	FormStr += "</td></tr></table>"
//	return FormStr;
//} 

function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{
    document.getElementById("HidProductID").value = id;
    document.getElementById("txtProductName").value = productname;
}