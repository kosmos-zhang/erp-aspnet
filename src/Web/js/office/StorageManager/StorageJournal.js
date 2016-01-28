 
$(document).ready(function(){
      //fnGetExtAttr('trNewAttr');//物品控件拓展属性
        var requestObj = GetRequest(location.search);
     var  Flag=requestObj['Flag'];
     if (typeof (Flag )!="undefined")
     {
 
pageCount=parseInt(requestObj ['pageCount']);
 
Fun_Search_StorageInfo(requestObj ['pageIndex'])
     }
         IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
      
      
    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var currentPageIndex = 1;
   
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
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
                return false;
            }
           var point = document.getElementById("HiddenPoint").value;
           currentPageIndex = pageIndex;
           var ddlStorage= document.getElementById("ddlStorage").value;
           var txtProductNo=document.getElementById("txtProductNo").value;
           //var txtProductName=document.getElementById("txtProductName").value;
           var ProductID=document.getElementById("hiddenProductID").value;
           var StartDate=document.getElementById("txtStartDate").value;
           var EndDate=document.getElementById("txtEndDate").value;
           var ModuleID=document.getElementById("hidModuleID").value;
           var action = "SumInfo";//操作
           
           var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;
           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;
           var ProviderID = document.getElementById("txtProviderID").value;
           var BatchNo = document.getElementById("ddlBatchNo").value;


           var SourceType = document.getElementById("ddlSourceType").value;

           var SourceNo = document.getElementById("txtSourceNo").value;

           var CreatorID = document.getElementById("txtCreatorID").value;
           var CreatorName = document.getElementById("UserCreator").value;

           var ProviderName = document.getElementById("txtProviderName").value;

           var ckbIsM = 0;
           if (document.getElementById("ckbIsM").checked) {
               ckbIsM = 1;
           }
           
           
           if(ProviderID!=""&&ProviderID!=undefined)
           {
               document.getElementById("OutStorage").style.display="none";
               document.getElementById("NowStorage").style.display="none";
           }
           else
           {
               document.getElementById("OutStorage").style.display="block";
               document.getElementById("NowStorage").style.display="block";
           }
           $("#HidStartDate").val($("#txtStartDate").val());
           $("#HidEndDate").val($("#txtEndDate").val());

           var UrlParam = "&pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&Action=" + action +
           "&orderby=" + orderBy + "&ddlStorage=" + escape(ddlStorage) +
           "&txtProductNo=" + escape(txtProductNo) +
           "&EFIndex=" + escape(EFIndex) +
           "&EFDesc=" + escape(EFDesc) +
           "&ProviderID=" + escape(ProviderID) +
           "&ProviderName=" + escape(ProviderName) +
           "&ProductID=" + escape(ProductID) + "&StartDate=" + escape(StartDate) + "&EndDate=" + escape(EndDate) + "&ModuleID=" + escape(ModuleID) + "&BatchNo=" + escape(BatchNo) + "&SourceType=" + escape(SourceType) + "&SourceNo=" + escape(SourceNo) + "&CreatorID=" + escape(CreatorID) + "&CreatorName=" + escape(CreatorName) + "&ckbIsM=" + escape(ckbIsM);
      document.getElementById("hidSearchCondition").value = UrlParam;//这里只是放了一个标志位，说明是点过了检索按钮
       var   myid   =   document.getElementById("GetBillExAttrControl1_SelExtValue");   
           var currSelectText =   myid.options[myid.selectedIndex].text;    
           var sidex= "ExtField"+EFIndex;
        document .getElementById ("hiddenEFIndex").value=EFIndex;
        document .getElementById ("hiddenEFDesc").value=EFDesc;
           if(ProviderID!=""&&ProviderID!=undefined)
           {

               $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  '../../../Handler/Office/StorageManager/StorageJournal.ashx?'+UrlParam,//目标地址
               cache:false,
               beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
               
               success: function(msg){
                        //数据获取完毕，填充页面据显示
                        //数据列表
                        $("#pageDataList1 tbody").find("tr.newrow").remove();
                       if (EFIndex !=-1 && EFDesc!="" )
                        {
                        document .getElementById ("thHole").style.display="block";
                           document .getElementById ("newItem").innerHTML=currSelectText; 
                                $('#divClick').click(function(){  OrderBy(sidex ,'Span8');return false ;});   
                                 document .getElementById ("hiddenEFIndexName").value=currSelectText;
                        $.each(msg.data,function(i,item){
                        $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\"><a href=# onclick=linkParm('" + item.ProductID + "','" + item.StorageID + "','" + item.BatchNo + "') >" + fnjiequ(item.StorageNo, 10) + "</a></td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.BatchNo + "\">" + fnjiequ(item.BatchNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\""+item.ProdNo+"\">"+fnjiequ(item.ProdNo,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductName+"\">"+fnjiequ(item.ProductName,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.Specification+"\">"+fnjiequ(item.Specification,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductSize+"\">"+fnjiequ(item.ProductSize,10)+"</td>"+
                            "<td height='22' align='center' title=\"" + item.FromAddr + "\">" + fnjiequ(item.FromAddr, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductCount + "\">" + jqControl(item.ProductCount) + "</td>" +
                              "<td height='22' align='center' title=\""+item[sidex]+"\">"+ fnjiequ(item[sidex],10) +"</td>").appendTo($("#pageDataList1 tbody"));
                            
                       });
                       }
                       else
                       {
                           document .getElementById ("thHole").style.display="none";
                          $.each(msg.data,function(i,item){  
                            $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\"><a href=# onclick=linkParm('" + item.ProductID + "','" + item.StorageID + "','" + item.BatchNo + "') >" + fnjiequ(item.StorageNo, 10) + "</a></td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.BatchNo + "\">" + fnjiequ(item.BatchNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\""+item.ProdNo+"\">"+fnjiequ(item.ProdNo,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductName+"\">"+fnjiequ(item.ProductName,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.Specification+"\">"+fnjiequ(item.Specification,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductSize+"\">"+fnjiequ(item.ProductSize,10)+"</td>"+
                            "<td height='22' align='center' title=\"" + item.FromAddr + "\">" + fnjiequ(item.FromAddr, 10) + "</td>" + 
                            "<td height='22' align='center' title=\""+item.ProductCount+"\">"+jqControl(item.ProductCount)+"</td>" ).appendTo($("#pageDataList1 tbody"));
                            
                       });
                       }
                     
                      
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
                      ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                      $("#ToPage").val(pageIndex);
                      $("#txtBarCode").val("");//清空条码
                      },
               error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
               complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
               });
           }
           else
           {
                  
               $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  '../../../Handler/Office/StorageManager/StorageJournal.ashx?'+UrlParam,//目标地址
               cache:false,
               beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
               
               success: function(msg){
                        //数据获取完毕，填充页面据显示
                        //数据列表
                        $("#pageDataList1 tbody").find("tr.newrow").remove();
                        
                         
                                
                                   if (EFIndex !=-1 && EFDesc!="" )
                        {
                        document .getElementById ("thHole").style.display="block";
                           document .getElementById ("newItem").innerHTML=currSelectText; 
                                $('#divClick').click(function(){  OrderBy(sidex ,'Span8');return false ;});   
                                    document .getElementById ("hiddenEFIndexName").value=currSelectText;
                                    
                            $.each(msg.data,function(i,item){
                                $("<tr class='newrow'></tr>").append(
                                "<td height='22' align='center' title=\"" + item.StorageNo + "\"><a href=# onclick=linkParm('" + item.ProductID + "','" + item.StorageID + "','" + item.BatchNo + "') >" + fnjiequ(item.StorageNo, 10) + "</a></td>" +
                                "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                                "<td height='22' align='center' title=\"" + item.BatchNo + "\">" + fnjiequ(item.BatchNo, 10) + "</td>" +
                                "<td height='22' align='center' title=\""+item.ProdNo+"\">"+fnjiequ(item.ProdNo,10)+"</td>"+
                                "<td height='22' align='center' title=\""+item.ProductName+"\">"+fnjiequ(item.ProductName,10)+"</td>"+
                                "<td height='22' align='center' title=\""+item.Specification+"\">"+fnjiequ(item.Specification,10)+"</td>"+
                                "<td height='22' align='center' title=\""+item.ProductSize+"\">"+fnjiequ(item.ProductSize,10)+"</td>"+
                                "<td height='22' align='center' title=\"" + item.FromAddr + "\">" + fnjiequ(item.FromAddr, 10) + "</td>" +
                                "<td height='22' align='center' title=\"" + item.InProductCount + "\">" +jqControl(item.InProductCount)   + "</td>" +
                                "<td height='22' align='center' title=\"" + item.OutProductCount + "\">" +jqControl(item.OutProductCount)  + "</td>" +
                                "<td height='22' align='center' title=\"" + item.NowProductCount + "\">" +jqControl(item.NowProductCount) + "</td>" +
                                   "<td height='22' align='center' title=\""+item[sidex]+"\">"+ fnjiequ(item[sidex],10) +"</td>").appendTo($("#pageDataList1 tbody"));
                                
                           });
                       }
                       else
                       {    
                              document .getElementById ("thHole").style.display="none";
                         $.each(msg.data,function(i,item){
                                $("<tr class='newrow'></tr>").append(
                                "<td height='22' align='center' title=\"" + item.StorageNo + "\"><a href=# onclick=linkParm('" + item.ProductID + "','" + item.StorageID + "','" + item.BatchNo + "') >" + fnjiequ(item.StorageNo, 10) + "</a></td>" +
                                "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                                "<td height='22' align='center' title=\"" + item.BatchNo + "\">" + fnjiequ(item.BatchNo, 10) + "</td>" +
                                "<td height='22' align='center' title=\""+item.ProdNo+"\">"+fnjiequ(item.ProdNo,10)+"</td>"+
                                "<td height='22' align='center' title=\""+item.ProductName+"\">"+fnjiequ(item.ProductName,10)+"</td>"+
                                "<td height='22' align='center' title=\""+item.Specification+"\">"+fnjiequ(item.Specification,10)+"</td>"+
                                "<td height='22' align='center' title=\""+item.ProductSize+"\">"+fnjiequ(item.ProductSize,10)+"</td>"+
                                "<td height='22' align='center' title=\""+item.FromAddr+"\">"+fnjiequ(item.FromAddr,10)+"</td>"+
                                "<td height='22' align='center' title=\"" + item.InProductCount + "\">" +jqControl(item.InProductCount) + "</td>" +
                                "<td height='22' align='center' title=\"" + item.OutProductCount + "\">" +jqControl(item.OutProductCount) + "</td>" +
                                "<td height='22' align='center' title=\"" + item.NowProductCount + "\">" +jqControl(item.NowProductCount)  + "</td>").appendTo($("#pageDataList1 tbody"));
                                
                           });
                       
                       }
                     
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
                      ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                      $("#ToPage").val(pageIndex);
                      $("#txtBarCode").val("");//清空条码
                      },
               error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
               complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
               });
           
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

function Fun_Search_StorageInfo(aa)
{
    
      TurnToPage(1);
}

function Ifshow(count)
    {
        if(count=="0")
        {
            document.all["divpage"].style.display = "none";
            document.all["pagecount"].style.display = "none";
        }
        else
        {
            document.all["divpage"].style.display = "block";
            document.all["pagecount"].style.display = "block";
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
            this.pageCount=parseInt(newPageCount,10);
            TurnToPage(parseInt(newPageIndex,10));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
    {
        if (document.getElementById("hidSearchCondition").value == "" || document.getElementById("hidSearchCondition").value == null) return;
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
        $("#txtorderBy").val(orderBy);//把排序字段放到隐藏域中，
        Fun_Search_StorageInfo();
    }
    
//物品控件
function Fun_FillParent_Content(id,ProNo,ProdName)
{
   document.getElementById('txtProductNo').value=ProNo;
   //document.getElementById('txtProductName').value=ProdName;
   document.getElementById('hiddenProductID').value=id;
   
}



 document.onkeydown = ScanBarCodeSearch;
/*列表条码扫描检索*/
function ScanBarCodeSearch()
{
           var evt = event ? event : (window.event ? window.event : null);
           var el;var theEvent
           var browser=IsBrowser();
           if(browser=="IE")
           {
             el = window.event.srcElement;
             theEvent = window.event;
           }
           else
           {
             el=evt.target;
             theEvent = evt;
           }
            if( el.id!= "txtBarCode" )
            {
               return;
            }
            else
            {
            var code = theEvent.keyCode || theEvent.which;
            if (code == 13)
            {
                TurnToPage(1);
                evt.returnValue=false;
                evt.cancel = true;
            }
        }
}

    function IsBrowser()
    {
        var isBrowser ;
        if(window.ActiveXObject){
        isBrowser = "IE";
        }else if(window.XMLHttpRequest){
        isBrowser = "FireFox";
        }
        return isBrowser;
    }
    
    
    
/*
* 返回付款单列表
*/
function linkParm(ProductID,StrongeID,BatchNo)
{
   //获取查询条件
   var searchCondition = document.getElementById("hidSearchCondition").value;
   var StartDate=document.getElementById("txtStartDate").value;
   var EndDate=document.getElementById("txtEndDate").value;
   //获取模块功能ID
   var ModuleID = document.getElementById("hidModuleID").value;
  // alert("StorageJournalDetail.aspx?PID="+ProductID+"&SID="+StrongeID+"&Flag=1&SD="+StartDate+"&ED="+EndDate+ searchCondition);
 
   window.location.href = "StorageJournalDetail.aspx?PID="+ProductID+"&SID="+StrongeID+"&BNo="+BatchNo+"&Flag=1&SD="+StartDate+"&ED="+EndDate+ searchCondition;

}


function jqControl(value) {
    var ret = "";
    var point = document.getElementById("HiddenPoint").value;
    if (value != null && value != "" && value != undefined) {
        ret = parseFloat(value).toFixed(point);
    }
    return ret;
}