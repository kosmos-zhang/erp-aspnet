    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    
    
    
$(document).ready(function() {
   TurnToPage(1);
});
    
    
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
        
    
//    window.location.href="CustTypeManageNewList.aspx?XValue=" + CustSeleTypeID +"&CustSeleType=" + CustSeleType + "&CustType=" + CustType +
//         "&CustClass=" + CustClass + "&Area=" + Area + "&RelaGrade=" + RelaGrade + "&CreditGrade=" + CreditGrade +
//         "&BeginDate="+ BeginDate +"&EndDate=" + EndDate;

         action = "ReportCustSeleType";//客户分类对比
         currentPageIndex = pageIndex;
         requestobj = GetRequest(); 
         var CustSeleTypeID = requestobj['XValue'];
         var CustSeleType = requestobj['CustSeleType'];
         var CustType = requestobj['CustType'];
         var CustClass = requestobj['CustClass'];
         var Area = requestobj['Area'];
         var RelaGrade = requestobj['RelaGrade'];
         var CreditGrade = requestobj['CreditGrade'];
         var BeginDate = requestobj['BeginDate'];
         var EndDate = requestobj['EndDate'];
         
                  
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/CustManager/CustTypeManageNewList.ashx',//目标地址
           cache:false,
           data: "pageIndex="+currentPageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&CustSeleTypeID="+reescape(CustSeleTypeID)+"&CustSeleType="+reescape(CustSeleType)+
                    "&CustType="+reescape(CustType)+"&CustClass="+reescape(CustClass)+"&Area="+reescape(Area)+"&RelaGrade="+reescape(RelaGrade)+"&CreditGrade="+reescape(CreditGrade)+
                    "&BeginDate="+reescape(BeginDate)+"&EndDate="+reescape(EndDate),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var RelaGrade,UsedSta;
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")
                      {                          
                            $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center'>" + item.CustNo + "</td>"+
                            "<td height='22' align='center' Title='"+item.CustName+"'>" + SubValue(12,item.CustName) + "</td>"+
                            "<td height='22' align='center'>"+ item.CustNam +"</td>"+
                            "<td height='22' align='center'>" + item.CustShort + "</td>"+
                            "<td height='22' align='center'>" + item.CodeName + "</td>"+
                            "<td height='22' align='center'>" + item.TypeName + "</td>"+
                            "<td height='22' align='center'>" + item.Area + "</td>"+
                            "<td height='22' align='center'>" + item.Manager + "</td>"+
                            "<td height='22' align='center'>" + item.CreditGrade + "</td>"+
                            "<td height='22' align='center'>" + item.RelaGrade + "</td>"+
                            "<td height='22' align='center'>" + item.Creator + "</td>"+
                             "<td height='22' align='center'>" + item.CreatedDate + "</td>"+
                            "<td height='22' align='center'>"+ item.UsedStatus +"</td>").appendTo($("#pageDataList1 tbody"));
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
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                 
                  $("#ToPage").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){{hidePopup();}$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function SelectDept(custid)
{
    var CustNo =document.getElementById("txtCustNo").value;
    var CustNam =document.getElementById("txtCustNam").value;
    //var CustClass =document.getElementById("CustClassDrpControl1_ddlCustClass").value;
    var CustClass = $("#CustClassDrpControl1_CustClassHidden").val();
    var CustClassName = $("#CustClassDrpControl1_CustClass").val();
        
    window.location.href='Cust_Edit.aspx?custid='+custid+'&Pages=Cust_Info.aspx&CustNam='+CustNam+'&CustNo='+CustNo+'&CustClass='
    +CustClass+'&CustClassName='+CustClassName+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021101';
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{

    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
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
        ifdel = "0";
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
    //排序
    function OrderBy(orderColum,orderTip)
    {
        if (totalRecord == 0) 
         {
            return;
         }

        ifdel = "0";
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
        $("#hiddExpOrder").val(orderBy);
        TurnToPage(1);
    }
   
//删除
 function DelCustInfo()
{

    if(confirm("删除后不可恢复，确认删除吗！"))
    {
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";
        var CustNos="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value.split(',')[0]+',';
               CustNos += ck[i].value.split(',')[1]+',';              
            }
        }
        
        var custids = ck2.substring(0,ck2.length-1);
        CustNos = CustNos.substring(0,CustNos.length-1);
        x = ck2.split(',');
        if(x.length-1<=0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条客户信息后再删除！");
            return;
        }
        else
        {
            //删除
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/CustManager/CustInfoDel.ashx",
                  data:"allcustid="+reescape(custids)+"&AllCustNO="+reescape(CustNos),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      AddPop();
                  }, 
                //complete :function(){hidePopup();},
                error: function() 
                { 
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                }, 
                success:function(data) 
                { 
                    if(data.sta==1) 
                    { 
                        //成功
                        TurnToPage(1);
                        ifdel = "1";
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                        return;
                    }
                     if(data.sta==2) 
                    { 
                         showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","有关联数据，删除失败！");                     
                    } 
                    else
                    {
                        //popMsgObj.Show('删除失败！');
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                    } 
                } 
               });
        }
    }
}

  //是否可以导出
function IfExp() {
    if (totalRecord == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先检索出数据，才可以导出！");
        return false;
    }
    
    document.getElementById("hiddCustClass").value = $("#CustClassDrpControl1_CustClassHidden").val(); //客户分类
    return true;
}


