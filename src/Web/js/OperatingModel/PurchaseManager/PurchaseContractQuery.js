var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";
    /* 分页相关变量定义 */

    $(document).ready(function()
{
//    requestobj = GetRequest(); 
//    var ProductName = requestobj['ProductName'];
//    var ProductID = requestobj['ProductID1'];
//    if(ProductID == undefined)
//    {
//        ProductID ="";
//    }
//    var StartPurchaseDate = requestobj['StartPurchaseDate'];
//    var EndPurchaseDate = requestobj['EndPurchaseDate'];
//    var Isliebiao =requestobj['Isliebiao'];
//    
//       
//    
////    if(typeof(ContractNo1)!="undefined")
//    if(typeof(Isliebiao)!="undefined")
//    { 
//       $("#txtProductName").attr("value",ProductName);
//       $("#HidProductID").attr("value",ProductID);
//       $("#txtStartPurchaseDate").attr("value",StartPurchaseDate);
//       $("#txtEndPurchaseDate").attr("value",EndPurchaseDate);
//       SearchPurchaseContractQuery();
//    }
});

    

//打印
function fnPrint() {
    
//    if(ordering=="a")
//    {
//        ordering="asc";
//    }
//    else
//    {
//        ordering="desc"
//    }
//    orderBy=orderBy.split('_')[0];
    
    var ProviderID = document.getElementById("txtHidProviderID").value;
//   var ProductID = document.getElementById("HidProductID").value.Trim();
   var StartConfirmDate = document.getElementById("txtStartConfirmDate").value;
   var EndConfirmDate = document.getElementById("txtEndConfirmDate").value;
            
//    var strRul = $("#hiddSearch").val();
    window.open("PurchaseContractQueryPrint.aspx?pageIndex="+currentPageIndex+"&pageCount="+pageCount+"&orderby="+orderBy+"&ProviderID="+escape(ProviderID)+
                 "&StartConfirmDate="+escape(StartConfirmDate)+"&EndConfirmDate="+escape(EndConfirmDate)+"");
                 
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
//           var ProviderID = document.getElementById("txtHidProviderID").value;
//           var StartConfirmDate = document.getElementById("txtStartConfirmDate").value;
//           var EndConfirmDate = document.getElementById("txtEndConfirmDate").value;
 
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/PurchaseManager/PurchaseContractQuery.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+currentpageCount+"&orderby="+orderBy+
                 document.getElementById("hidSearchCondition").value,
           beforeSend:function(){AddPop();$("#pageDataList1_PagerList").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    //if(msg.totalCount=="0")
                        
                    $.each(msg.data,function(i,item){
                        if(item.ProviderID != null && item.ProviderID != "")
                        {
                        var ProviderName = item.ProviderName;
                        if (ProviderName != null) {
                        if (ProviderName.length > 10) {
                            ProviderName = ProviderName.substring(0, 10) + '...';
                            }
                        }
                        var Title = item.Title;
                        if (Title != null) {
                        if (Title.length > 10) {
                            Title = Title.substring(0, 10) + '...';
                            }
                        }
                            $("<tr class='newrow'></tr>").append(
//                        "<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+j+"  type='checkbox'/>"+"</td>"+
//                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+
//                        "<td height='22' align='center'><a href='" + GetLinkParam(ProductID) +"&ProductID=" + item.ProductID + " ')>"+item.ProductNo+"</a></td>"+
                

                        "<td height='22' align='center'>"+ item.ContractNo +"</a></td>"+
                        "<td height='22' align='center'><span title=\"" + item.Title + "\">"+ Title +"</a></td>"+
                        "<td height='22' align='center'>"+ item.TypeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.EmployeeName +"</a></td>"+
                        "<td height='22' align='center'><span title=\"" + item.ProviderName + "\">"+ ProviderName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.RealTotal +"</a></td>").appendTo($("#pageDataList1 tbody"));
                        }
                    
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
function GetLinkParam(ProductID)
{
//    //获取模块功能ID
//    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时

    linkParam = "PurchaseHistoryAskPriceShow.aspx?PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&ProductID1=" + ProductID + "&" + searchCondition + "&Flag=" + flag;
    //返回链接的字符串
    return linkParam;
 
}

function fnCheck()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    if(document.getElementById("txtStartConfirmDate").value == "")
    {
        isFlag = false;
        fieldText += "起始日期|";
   		msgText +=  "起始日期不能为空|";  
    }
    if(document.getElementById("txtEndConfirmDate").value == "")
    {
        isFlag = false;
        fieldText += "结束日期|";
   		msgText +=  "结束日期不能为空|";  
    }
    if(!isFlag)
    {
    popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
}


//采购历史价格列表
function SearchPurchaseContractQuery()
    {
    if(!fnCheck())
    return;
//检索条件
issearch=1;
    document.getElementById("btnPrint").style.display="inline";
    
    
    var ProviderID = document.getElementById("txtHidProviderID").value;
//    var ProductID = document.getElementById("HidProductID").value.Trim();
    var StartConfirmDate = document.getElementById("txtStartConfirmDate").value;
    var EndConfirmDate = document.getElementById("txtEndConfirmDate").value;
    if(StartConfirmDate > EndConfirmDate)
    {
        alert("起始时间不能大于结束时间!");
        return;
    }
    
//    currentPageIndex = pageIndex;
    var Isliebiao = 1;
    var URLParams = "&Isliebiao="+escape(Isliebiao)+"&ProviderID="+escape(ProviderID)+
                     "&StartConfirmDate="+escape(StartConfirmDate)+"&EndConfirmDate="+escape(EndConfirmDate)+""; 
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
        orderBy = orderColum+"_"+ordering;
        TurnToPage(1);
    }
   


function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{
    document.getElementById("HidProductID").value = id;
    document.getElementById("txtProductName").value = productname;
}


function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{  
   
    document.getElementById("txtProviderID").value = providerno;
    document.getElementById("txtHidProviderID").value = providerid;
    
    closeProviderdiv();
}

function clearProviderdiv()
{
    document.getElementById("txtProviderID").value = "";
    document.getElementById("txtHidProviderID").value = "";
}