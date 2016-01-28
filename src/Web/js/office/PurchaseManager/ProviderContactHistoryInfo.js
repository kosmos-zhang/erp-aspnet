var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "LinkDate_d";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";
    
    $(document).ready(function()
{
    requestobj = GetRequest(); 
    var CustName = requestobj['CustName'];
    var CustID = requestobj['CustID'];
    var Linker = requestobj['Linker'];
    var LinkerName = requestobj['LinkerName'];
    var StartLinkDate = requestobj['StartLinkDate'];
    var EndLinkDate = requestobj['EndLinkDate'];
  
    var Isliebiao =requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
    
    
    if(typeof(Isliebiao)!="undefined")
    { 
       $("#txtCustName").attr("value",CustName);
       $("#txtCustID").attr("value",CustID);
       $("#HidLinker").attr("value",Linker);
       $("#UsertxtLinkerName").attr("value",LinkerName);
       $("#txtStartLinkDate").attr("value",StartLinkDate);
       $("#txtEndLinkDate").attr("value",EndLinkDate);
       currentPageIndex = PageIndex;
       currentpageCount = PageCount;
       SearchProviderContactHistoryData();
    }
});

    
    

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
//           var CustName = document.getElementById("txtCustName").value;//供应商名称
//            var CustID = document.getElementById("txtCustID").value;//供应商id
//            if(CustID == undefined)
//            {
//                CustID="";
//            }
//            var Linker = document.getElementById("HidLinker").value;//联系人ID
//            if(Linker == undefined)
//            {
//                Linker="";
//            }
//            var LinkerName = document.getElementById("UsertxtLinkerName").value;//联系人名称
//            var StartLinkDate = document.getElementById("txtStartLinkDate").value;//联络时间 起
//            var EndLinkDate = document.getElementById("txtEndLinkDate").value;//联络时间 止
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/ProviderContactHistoryInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+currentpageCount+"&action="+action+"&orderby="+orderBy+
                  document.getElementById("hidSearchCondition").value,
           beforeSend:function(){AddPop();$("#pageDataList1_PagerList").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        var ContactNo = item.ContactNo;
                        if (ContactNo != null) {
                        if (ContactNo.length > 20) {
                            ContactNo = ContactNo.substring(0, 20) + '...';
                            }
                        }
                        var CustName = item.CustName;
                        if (CustName != null) {
                        if (CustName.length > 6) {
                            CustName = CustName.substring(0, 6) + '...';
                            }
                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+"  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />"+"</td>"+
                        "<td height='22' align='center'><a href='" + GetLinkParam() +"&intMasterProviderContactHistoryID=" + item.ID + "')><span title=\"" + item.ContactNo + "\">"+ContactNo+"</a></td>"+
                        
                        "<td height='22' align='center'><span title=\"" + item.CustName + "\">"+ CustName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CustTypeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.LinkerName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.LinkDate +"</a></td>"+
                        "<td height='22' align='center'>"+ item.LinkManName +"</a></td>").appendTo($("#pageDataList1 tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:currentpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );document.getElementById("checkall").checked = false;
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


/*
* 获取链接的参数
*/
function GetLinkParam()
{
//    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    
    linkParam = "ProviderContactHistory_Add.aspx?ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + currentpageCount + "&" + searchCondition + "&Flag=" + flag;
    //返回链接的字符串
    return linkParam;
 
}

function SearchProviderContactHistoryData()
    {
//检索条件
issearch=1;
    
    var CustName = document.getElementById("txtCustName").value.Trim();//供应商名称
    var CustID = document.getElementById("txtCustID").value.Trim();//供应商id
    if(CustID == undefined)
    {
        CustID="";
    }
    var Linker = document.getElementById("HidLinker").value.Trim();//联系人ID
    if(Linker == undefined)
    {
        Linker="";
    }
    var LinkerName = document.getElementById("UsertxtLinkerName").value.Trim();//联系人名称
    var StartLinkDate = document.getElementById("txtStartLinkDate").value.Trim();//联络时间 起
    var EndLinkDate = document.getElementById("txtEndLinkDate").value.Trim(); //联络时间 止

    if (StartLinkDate != "" && EndLinkDate != "") {
        if (StartLinkDate > EndLinkDate) {
            alert("起始时间不能大于终止时间!");
            return;
        }
    }
    
//    currentPageIndex = pageIndex;
    var Isliebiao = 1;
    var URLParams = "&Isliebiao="+escape(Isliebiao)+"&CustName="+escape(CustName)+"&CustID="+escape(CustID)+"&Linker="+escape(Linker)+
                    "&LinkerName="+escape(LinkerName)+"&StartLinkDate="+escape(StartLinkDate)+"&EndLinkDate="+escape(EndLinkDate)+"";
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
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
//            return false;
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
    function OrderBy1(orderColum,orderTip)
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
    
    //全选
    function SelectAll() {
        $.each($("#pageDataList1 :checkbox"), function(i, obj) {
            obj.checked = $("#checkall").attr("checked");
        });
    }
 
    //新建供应商联络单
   function CreateProviderContactHistory()
   {
 
    var CustName = document.getElementById("txtCustName").value.Trim();//供应商名称
    var CustID = document.getElementById("txtCustID").value.Trim();//供应商id
    if(CustID == undefined)
    {
        CustID="";
    }
    var Linker = document.getElementById("HidLinker").value.Trim();//联系人ID
    if(Linker == undefined)
    {
        Linker="";
    }
    var LinkerName = document.getElementById("UsertxtLinkerName").value.Trim();//联系人名称
    var StartLinkDate = document.getElementById("txtStartLinkDate").value.Trim();//联络时间 起
    var EndLinkDate = document.getElementById("txtEndLinkDate").value.Trim();//联络时间 止
    //    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value.Trim();
//    currentPageIndex = pageIndex;
    var Isliebiao = 1;
    var URLParams = "Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&PageIndex=" + currentPageIndex + "&PageCount="+currentpageCount+"&CustName="+escape(CustName)+"&CustID="+escape(CustID)+"&Linker="+escape(Linker)+
                    "&LinkerName="+escape(LinkerName)+"&StartLinkDate="+escape(StartLinkDate)+"&EndLinkDate="+escape(EndLinkDate)+"";
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = URLParams;
//    currentPageIndex = pageIndex;
    
   
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value.Trim();
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
        window.location.href='ProviderContactHistory_Add.aspx?'+URLParams ;
   }

function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{  
   
    document.getElementById("txtCustName").value = providername;
    document.getElementById("txtCustNo").value = providerno;
    
    closeProviderdiv();
}



function  DelProviderContactHistory()
{ 
     
        var c=window.confirm("确认执行删除操作吗？")
        if(c==true)
        {
               var ck = document.getElementsByName("Checkbox1");
        var x=Array(); 
        var ck2 = "";
        var str="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
            }
        }
        x=ck2;
        var str = ck2.substring(0,ck2.length-1);
        if(x.length-1<1)
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据再删除！");
        else
          {
             CodePublic=str;
        var action = "Delete";
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/PurchaseManager/ProviderContactHistoryInfo.ashx?str="+str,//目标地址
//                  url: "../../../Handler/Office/SystemManager/ItemCodingRule.ashx?str="+str,
                  dataType:'json',//返回json格式数据
                  data: "action="+escape(action),//数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  //complete :function(){ //hidePopup();},
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        SearchProviderContactHistoryData();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
            
          }
        }  
        else   return   false;   

    
}






//选择物品
function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{
    document.getElementById("HidProductID").value = id;
    document.getElementById("txtProductName").value = productname;
    document.getElementById('divProviderInfo').style.display='none';
}