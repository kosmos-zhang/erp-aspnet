$(document).ready(function(){
   requestobj = GetRequest();
       recordnoparam = requestobj['Search'];
       if(typeof(recordnoparam)!="undefined")
       {
       }
    });    
  var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var flag="";
     var str="";
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {   
     document.getElementById("btnAll").checked=false;
           var serch=document.getElementById("hidSearchCondition").value;
           currentPageIndex = pageIndex;
           var Subquey="";
//           var ChangeNo= document.getElementById("txt_ChangeNo").value;
//           var Title=document.getElementById("txt_Title").value;
//           if(document.getElementById("txt_ProductName").value!="")
//           var ProductID=document.getElementById("hf_ProductID").value;
//           else
//            var ProductID="";
//           var Chenger = document.getElementById("txtChenger").value;
//           var OpenDate = document.getElementById("txtOpenDate").value;
//           var CloseDate = document.getElementById("txtCloseDate").value;
           //Start验证输入
//           if(!CheckInput())
//           {
               $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  '../../../Handler/Office/SupplyChain/ProductPriceList.ashx',//目标地址
               cache:false,
               data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&" + serch,//数据
               beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
               
               success: function(msg){
                        //数据获取完毕，填充页面据显示
                        //数据列表
                        $("#pageDataList1 tbody").find("tr.newrow").remove();
                        $.each(msg.data,function(i,item){
                            if(item.ID != null && item.ID != "")
                            { Subquey=item.Title;
                                if(item.ConfirmDate.substring(0,10)=='1900-01-01')
                                item.ConfirmDate='';
                                if(item.BillStatus=="1") item.BillStatus="制单"
                                if(item.BillStatus=="5") item.BillStatus="结单"
                              $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll')  type='checkbox'/>"+"</td>"+
                            "<td height='22' align='center' title=\""+item.ChangeNo+"\"><a href='" + GetLinkParam() + "&intOtherCorpInfoID=" + item.ID + "')>"+fnjiequ(item.ChangeNo,10)+"</a></td>"+
                            "<td height='22'align='center' title=\""+item.Title+"\">"+fnjiequ(item.Title,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductID+"\">"+fnjiequ(item.ProductID,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductName+"\">"+fnjiequ(item.ProductName,10)+"</td>"+
                            "<td height='22' align='center'>"+item.TaxRateNew+'%'+"</td>"+
                            "<td height='22' align='center'>"+item.StandardSellNew+"</td>"+
                            "<td height='22' align='center'>"+item.SellTaxNew+"</td>"+
                            "<td height='22' align='center'>"+item.DiscountNew+'%'+"</td>"+
                            "<td height='22' align='center'>"+item.ChangeDate.substring(0,10)+"</td>"+
                             "<td height='22' align='center'>"+item.ConfirmDate.substring(0,10)+"</td>"+
                            "<td height='22' align='center'>"+item.BillStatus+"</td>").appendTo($("#pageDataList1 tbody"));
                            }
                           
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
                      //document.getElementById('tdResult').style.display='block';
                      },
               error: function() {}, 
               complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
               });
//           }
           //End验证输入

    }
    
    
function GetLinkParam()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    linkParam = "ProductPriceAdd.aspx?orderby=" + orderBy + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag+"&ModuleID="+ModuleID;
    //返回链接的字符串
    return linkParam;
}
//function CheckInput()
//{
//   var PlanNo= document.getElementById("txtPlanNo").value;
//   var Subject=document.getElementById("txtSubject").value;
//   var Principal=document.getElementById("txtPrincipal").value;
//   var DeptID = document.getElementById("txtDeptID").value;
//   var FromBillID = document.getElementById("txtFromBillID").value;
//   var BillStatus = document.getElementById("sltBillStaus").value;
//   var FlowStatus = document.getElementById('sltFlowStatus').value;
//    
//   //Start验证输入
//   var fieldText = "";
//   var msgText = "";
//   var isErrorFlag = false;
//   if(PlanNo.length>0)
//   {
//        if (isnumberorLetters(PlanNo))
//        {
//            isErrorFlag = true;
//            fieldText += "单据编号|";
//            msgText += "单据编号只能包含字母或数字！|";
//        }
//   }
//    if(strlen(Subject)>100)
//    {
//        isErrorFlag = true;
//        fieldText = fieldText + "主题|";
//   		msgText = msgText +  "仅限于100个字符以内|";
//    }
//    if(isErrorFlag)
//    {
//        popMsgObj.Show(fieldText,msgText);
//    }
//    else
//    {
//        return isErrorFlag;
//    }
//   //End验证输入 

    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=1;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function selall()
{
    var j=0;
    var m=parseInt(j);
    var ck2 = document.getElementsByName("Checkbox1");
    for( var i = 0; i < ck2.length; i++ )
    {
    if(ck2[i].checked)
    {
     j++
    }
    }
    if(j==ck2.length)
    {
    document.getElementById("btnAll").checked=true;
    }
    else
    {
     document.getElementById("btnAll").checked=false;
    }
}

function Fun_Search_ProductPriceInfo(currPage)
{
      var fieldText = "";
      var msgText = "";
      var isFlag = true;
//      if(PlanNo.length>0)
//      {
//             if(!IsValidSpecialStr(PlanNo))
//              {
//                isFlag = false;
//                fieldText = fieldText + "单据编号|";
//   		        msgText = msgText +  "只允许是字符和数字格式|";
//              }
//      }
    var OpenDate = document.getElementById("txtOpenDate").value;
    var CloseDate = document.getElementById("txtCloseDate").value;
   if(strlen(CloseDate)>0&&strlen(OpenDate)>0)
    {
       var  tmpBeginTime = new Date(OpenDate.replace(/-/g,"\/"));
       var  tmpEndTime = new Date(CloseDate.replace(/-/g,"\/"));
        if ( tmpBeginTime > tmpEndTime )
        {
              isFlag = false;
              fieldText = fieldText +  "日期不匹配|";
   		       msgText = msgText + "开始日期不能大于结束日期|";    
         }
     }
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
      else
      {
           var search = "";
           var ChangeNo= document.getElementById("txt_ChangeNo").value;
           var Title=document.getElementById("txt_Title").value;
           if(document.getElementById("txt_ProductName").value!="")
           var ProductID=document.getElementById("hf_ProductID").value;
           else
            var ProductID="";
           var Chenger = document.getElementById("txtChenger").value;
           var OpenDate = document.getElementById("txtOpenDate").value;
           var CloseDate = document.getElementById("txtCloseDate").value;
           var UserReporter=document.getElementById("UserReporter").value;
           var ProductName=document.getElementById("txt_ProductName").value;
          search+="ChangeNo="+escape(ChangeNo);
          search+="&Title="+escape(Title); 
          search+="&ProductName="+escape(ProductName); 
          search+="&UserReporter="+escape(UserReporter); 
          search+="&CloseDate="+escape(CloseDate); 
          search+="&OpenDate="+escape(OpenDate); 
          search+="&Chenger="+escape(Chenger); 
          search+="&ProductID="+escape(ProductID); 
           
           
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    if (currPage == null || typeof(currPage) == "undefined")
    {
        TurnToPage(1);
    }
    else
    {
        TurnToPage(parseInt(currPage));
    }
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
            document.getElementById('divpage').style.display = "block";
            document.getElementById('pagecount').style.display = "block";
        }
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
       if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
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
             document.getElementById("btnAll").checked=false;
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
    {
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

function Fun_Delete_ProductPriceInfo()
{
        var c=window.confirm("确认执行删除操作吗？")
        if(c==true)
        {
        var ck = document.getElementsByName("Checkbox1");
        var x=Array(); 
        var ck2 = "";
        var str="";
        Action="Del";
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除物价变更信息至少要选择一项！");
       else
       {
         var UrlParam ='';
       var UrlParam = "&str="+str+"\
                     &Action="+Action
        $.ajax({ 
                type: "POST",
                url:  '../../../Handler/Office/SupplyChain/ProductPrice.ashx?str='+UrlParam,//目标地址
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                 //AddPop();
              }, 
              error: function() 
              {
                popMsgObj.ShowMsg('请求发生错误');
                
              }, 
              success:function(data) 
              { 
                popMsgObj.ShowMsg(data.info);
                document.getElementById("btnAll").checked=false;
                if(data.sta>0)
                {
                    Fun_Search_ProductPriceInfo();
                }
              } 
           });
           }
    }
}

 function Fun_FillParent_Content(id,no,productname,price,unit,codyt,a,selltax,c,d,TypeCode,name)
{
    document.getElementById("txt_ProductName").value=productname;
     document.getElementById("hf_ProductID").value=id;
    document.getElementById('divStorageProduct').style.display='none';
    
}
function OptionCheck()
{

  if(document.getElementById("btnAll").checked)
  {
     var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=true ;
        }
  }
  else if(!document.getElementById("btnAll").checked)
  {
    var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=false ;
        }
  }
}

  function Show()
    {
        window.location.href = GetLinkParam();
    }