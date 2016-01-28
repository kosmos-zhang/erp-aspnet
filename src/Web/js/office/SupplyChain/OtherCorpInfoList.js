$(document).ready(function(){
//debugger;
//var ModuleID = document.getElementById("hidModuleID").value;
//document.getElementById("hidSearchCondition").value= "OtherCorpInfo.aspx?orderby=" + orderBy + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag+"&ModuleID="+ModuleID;
//  TurnToPage(1);
//Fun_Search_OtherCorpInfo();
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
           currentPageIndex = pageIndex;
           var serch=document.getElementById("hidSearchCondition").value;
//           var CustNo= document.getElementById("txt_CustNo").value;
//           var BigType=document.getElementById("sel_BigType").value;
//           var UsedStatus=document.getElementById("UsedStatus").value;
//           var CustName = document.getElementById("txt_CustName").value;
//           var BillType = document.getElementById("sel_BillType").value;
//            var isTax = document.getElementById('chk_isTax').checked ? "1" : "0"; 
           //Start验证输入

//           if(!CheckInput())
//           {
               $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  '../../../Handler/Office/SupplyChain/OtherCorpInfoList.ashx',//目标地址
               cache:false,
               data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&" + serch,//数据
               beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
               
               success: function(msg){
                        //数据获取完毕，填充页面据显示
                        //数据列表
                        $("#pageDataList1 tbody").find("tr.newrow").remove();
                        $.each(msg.data,function(i,item){
                            if(item.ID != null && item.ID != "")
                            {
                              var BigType = '';
                              var UsedStatus;
                            if(item.BigType==5)BigType='外协加工厂';
                            if(item.BigType==6)BigType='运输商';
                            if(item.BigType==7)BigType='其他';
                                if(item.UsedStatus=='0')UsedStatus='停用';
                             if(item.UsedStatus=='1')UsedStatus='启用';
                              $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>"+"</td>"+
                            "<td height='22' align='center' title=\""+item.CustNo+"\"><a href='" + GetLinkParam() + "&intOtherCorpInfoID=" + item.ID + "')>"+fnjiequ(item.CustNo,10)+"</a></td>"+
                            "<td height='22' align='center' title=\""+item.CustName+"\">"+ fnjiequ(item.CustName,10) +"</td>"+
                            "<td height='22' align='center'>"+item.Area+"</td>"+
                            "<td height='22' align='center'>"+item.CompanyType+"</td>"+
                            "<td height='22' align='center'>"+item.ContactName+"</td>"+
                            "<td height='22' align='center'>"+item.Tel+"</td>"+
                              "<td height='22' align='center'>"+BigType+"</td>"+
                                "<td height='22' align='center'>"+UsedStatus+"</td>"+
                            "<td height='22' align='center'>"+item.EmployeeName+"</td>"+
                            "<td height='22' align='center'>"+item.CreateDate.substring(0,10)+"</td>").appendTo($("#pageDataList1 tbody"));
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
                     
                      document.getElementById('Text2').value=msg.totalCount;
                       $("#ShowPageCount").val(pageCount);
                      ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                      $("#ToPage").val(pageIndex);
                         ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                      //document.getElementById('tdResult').style.display='block';
                      },
               error: function() {}, 
               complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
               });
//           }
           //End验证输入

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

function Fun_Search_OtherCorpInfo(currPage)
{
  var fieldText = "";
   var msgText = "";
   var isFlag = true;
       var search = "";
       var CustNo= trim(document.getElementById("txt_CustNo").value);
       var BigType=document.getElementById("sel_BigType").value;
       var UsedStatus=document.getElementById("UsedStatus").value;
       var CustName = trim(document.getElementById("txt_CustName").value);
       var BillType = document.getElementById("sel_BillType").value;
       var isTax = document.getElementById('chk_isTax').value;
      search+="CustNo="+escape(CustNo);
      search+="&BigType="+escape(BigType); 
      search+="&UsedStatus="+escape(UsedStatus); 
      search+="&CustName="+escape(CustName); 
      search+="&BillType="+escape(BillType); 
      search+="&isTax="+escape(isTax); 
    //设置检索条件
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
        return ;
    }
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
function GetLinkParam()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    linkParam = "OtherCropInfoAdd.aspx?orderby=" + orderBy + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag+"&ModuleID="+ModuleID;
    //返回链接的字符串
    return linkParam;
}
function Ifshow(count)
    {
       if(count=="0")
        {
        document.getElementById('divpage').style.display = "none";
        document.getElementById('pagecount').style.display = "none";
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
        if(newPageCount <=0 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","显示页数超出显示范围！");
            return false;
        }
        if(newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
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

var OptionCheckArray=new Array();
function OptionCheckOnes(obj)
{
    with(obj)
    {
        if(checked)
        {
            OptionCheckArray.push(value);
        }
        else
        {
            var OptionCheckArrayTM=new Array();
            for(var i=0;i<OptionCheckArray.length;i++)
            {
                if(OptionCheckArray[i]!=value)
                {
                    OptionCheckArrayTM.push(OptionCheckArray[i]);
                }
            }
            OptionCheckArray=OptionCheckArrayTM;
        }        
    }
}

function Fun_Delete_OtherCorpInfo()
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除银行信息至少要选择一项！");
       else
       {
         var UrlParam ='';
       var UrlParam = "&str="+str+"\
                     &Action="+Action
        $.ajax({ 
                type: "POST",
                url:  '../../../Handler/Office/SupplyChain/OtherCorpInfo.ashx?str='+UrlParam,//目标地址
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
                    Fun_Search_OtherCorpInfo();
                }
              } 
           });
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
function OptionCheckAll()
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