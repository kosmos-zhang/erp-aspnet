<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckApplay.ascx.cs" Inherits="UserControl_CheckApplay" %>
<a name="pageDataListProductMark"></a>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divFromReport" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 950px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -600px;">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="White" align="left" width="100%">
       
                   <img id="closeimg" src="../../../Images/Button/Bottom_btn_close.jpg" onclick="closeReportdiv();" /> 
                    <img id="Img1" src="../../../Images/Button/Bottom_btn_del.jpg" onclick="removeAll();" /> 
                </td>
<%--                <td height="25" colspan="3" bgcolor="#E7E7E7" align="center" style="width: 50%;">
                    <input id="Button2" onclick="removeAll();" type="button" value="清除" />
                </td>--%>
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    单据编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myBillNo1" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    单据主题
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myBillTitle1" class="tdinput" maxlength="50" type="text" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                </td>
                <td width="21%" bgcolor="#FFFFFF">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" bgcolor="#FFFFFF">                   
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                        onclick='TurnToPageCheckReport(1)' />&nbsp;
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1ReportFrom"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('ApplyNo','lApplyNo');return false;">
                            单据编号<span id="lApplyNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('Title','lTitle');return false;">
                            申请单主题<span id="lTitle" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('CustName','lCustName');return false;">
                            往来单位<span id="lCustName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('CustBigTypeName','lCustBigTypeName');return false;">
                            往来单位大类<span id="lCustBigTypeName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('PrincipalName','lPrincipalName');return false;">
                            生产负责人<span id="lPrincipalName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('DeptName','lDeptName');return false;">
                            生产部门<span id="lDeptName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('CheckTypeName','lCheckTypeName');return false;">
                            质检类别<span id="lCheckTypeName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('CheckModeName','lCheckModeName');return false;">
                            检验方式<span id="lCheckModeName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('ApplyUserName','lApplyUserName');return false;">
                            报检人员<span id="lApplyUserName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy2('CheckDeptName','lCheckDeptName');return false;">
                            报检部门<span id="lCheckDeptName" class="orderTip"></span></div>
                    </th>
                </tr>
            </tbody>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
            class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="pagecountReportFrom">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageReportFrom_Product" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageReportFrom">
                                    <input name="text" type="text" id="txt_pageProduct" style="display: none" />
                                    <span id="pageDataList_TotalReportFrom"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountReportFrom" size="5" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageReportFrom" size="5" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeReportPageCountIndex($('#ShowPageCountReportFrom').val(),$('#ToPageReportFrom').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">
var popReportObj=new Object();
var myMethod;
popReportObj.ShowList=function(method)
{
   
    document.getElementById('divFromReport').style.display='block';
    openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    myMethod=method;
    TurnToPageCheckReport(currentPageIndexReport);
}

    var pagecountReportFrom = 10;//每页计数
    var totalRecordReport = 0;
    var pagerStyleReport = "flickr";//jPagerBar样式    
    
    var currentPageIndexReport = 1;
    var actionReport = "";//操作
    var orderByReport = "";//排序字段
    
    
    //jQuery-ajax获取JSON数据
    function TurnToPageCheckReport(pageIndexReport)
    {
           currentPageIndexReport = pageIndexReport;
           var TheBillNO=document.getElementById('myBillNo1').value;
           var TheBillTitle=document.getElementById('myBillTitle1').value;
           if(TheBillTitle!='')
            {
                if(strlen(TheBillTitle)>0)
                {
                    if(!CheckSpecialWord(TheBillTitle))
                    {
                       closeMandiv();
                       popMsgObj.ShowMsg('单据主题不能含有特殊字符!');
                       return false;
                    }
                }
            }
            if(TheBillNO!='')
            {
                if(strlen(TheBillNO)>0)
                {
                    if(!CheckSpecialWord(TheBillNO))
                    {
                       closeMandiv();
                       popMsgObj.ShowMsg('单据编号不能含有特殊字符!');
                       return false;
                    }
                }
            }         
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageCheckApplayGet.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexReport+"&method="+myMethod+"&pageCount="+pagecountReportFrom+"&TheBillTitle="+TheBillTitle+"&TheBillNO="+TheBillNO+"&action="+actionReport+"&orderby="+orderByReport,//数据
           beforeSend:function(){$("#pageReportFrom_Product").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1ReportFrom tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                       $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioReport_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillReport('"+item.ID+"','"+item.ApplyNo+"','"+item.CustID+"','"+item.CustName+"','"+item.PrincipalID+"','"+item.PrincipalName+"','"+item.CheckType+"','"+item.CheckMode+"','"+item.ApplyUserID+"','"+item.ApplyUserName+"','"+item.CustBigTypeID+"','"+item.CustBigTypeName+"','"+item.DeptID+"','"+item.DeptName+"','"+item.CheckDeptId+"','"+item.CheckDeptName+"','"+item.ApplyNo+"');\" />"+"</td>"+
                       "<td height='22' align='center'>" + item.ApplyNo + "</td>"+
                        "<td height='22' align='center'>"+item.Title+"</td>"+
                        "<td height='22' align='center'>"+item.CustName+"</td>"+
                        "<td height='22' align='center'>"+item.CustBigTypeName+"</td>"+
                        "<td height='22' align='center'>"+item.PrincipalName+"</td>"+
                        "<td height='22' align='center'>"+item.DeptName+"</td>"+
                        "<td height='22' align='center'>"+item.CheckTypeName+"</td>"+
                        "<td height='22' align='center'>"+item.CheckModeName+"</td>"+
                        "<td height='22' align='center'>"+item.ApplyUserName+"</td>"+
                        "<td height='22' align='center'>"+item.CheckDeptName+"</td>").appendTo($("#pageDataList1ReportFrom tbody"));
                   });
//页码
                    ShowPageBar("pageReportFrom_Product",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleReport,mark:"pageDataListProductMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecountReportFrom,
                    currentPageIndex:pageIndexReport,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageCheckReport({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  $("#ShowPageCountReportFrom").val(pagecountReportFrom);  
                  ShowTotalPage(msg.totalCount,pagecountReportFrom,currentPageIndexReport);               
                  $("#ToPageReportFrom").val(pageIndexReport);
                  ShowTotalPage(msg.totalCount,pagecountReportFrom,currentPageIndexReport,$("#pagecountReportFrom"));

                  },
           error: function() {},         
           complete:function(){hidePopup();$("#pageReportFrom_Product").show();IfshowProduct(document.getElementById("txt_pageProduct").value);pageDataList1ReportFrom("pageDataList1ReportFrom","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
function removeAll()
{
   if(myMethod=='1')
   {
       var TitleName=document.getElementById('ApplyID'); //name
       var TitleID=document.getElementById('hiddenApplyID');//id
       var CustName=document.getElementById('tbCustID');
       var CustID=document.getElementById('hiddentbCustID');  
       var CustBigName=document.getElementById('tbCustBigID');
       var CustBigID=document.getElementById('hiddentbCustBigID');
       var PrincipalName=document.getElementById('UserPrincipal');
       var PrincipalID=document.getElementById('hiddentxPrincipal');
       var MantbDeptName=document.getElementById('Depttxt');
       var MantbDeptID=document.getElementById('hiddentbDept');
       var CheckType=document.getElementById('ddlCheckType');
       //var CheckMode=document.getElementById('ddlCheckMode');
       var CheckerName=document.getElementById('UserChecker');
       var CheckerID=document.getElementById('hiddentbChecker');
       var CheckerDeptName=document.getElementById('DeptChecker');
       var CheckerDeptID=document.getElementById('hiddentbCheckDept');
       
       TitleID.value="0";
       TitleName.value="";
       CustID.value="0";
       CustName.value="";
       PrincipalID.value="0";
       PrincipalName.value="";
       CheckType.value="1";
       //CheckMode.value='1';
       CheckerID.value='0';
       CheckerName.value='';
       CustBigID.value='0';
       CustBigName.value='';
       MantbDeptID.value='0';
       MantbDeptName.value='';
       CheckerDeptID.value='0';
       CheckerDeptName.value='';
       document.getElementById('hiddenApplyNo').value='0';
   }
   else
   {
        document.getElementById('tbReportID').value='0';
        document.getElementById('tbReportNo').value='';
   }
   closeReportdiv();
} 
function Fun_FillReport(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q)
{
   if(myMethod=='1')
   {
       var TitleName=document.getElementById('ApplyID'); //name
       var TitleID=document.getElementById('hiddenApplyID');//id
       var CustName=document.getElementById('tbCustID');
       var CustID=document.getElementById('hiddentbCustID');  
       var CustBigName=document.getElementById('tbCustBigID');
       var CustBigID=document.getElementById('hiddentbCustBigID');
       var PrincipalName=document.getElementById('UserPrincipal');
       var PrincipalID=document.getElementById('hiddentxPrincipal');
       var MantbDeptName=document.getElementById('Depttxt');
       var MantbDeptID=document.getElementById('hiddentbDept');
       var CheckType=document.getElementById('ddlCheckType');
       //var CheckMode=document.getElementById('ddlCheckMode');
       var CheckerName=document.getElementById('UserChecker');
       var CheckerID=document.getElementById('hiddentbChecker');
       var CheckerDeptName=document.getElementById('DeptChecker');
       var CheckerDeptID=document.getElementById('hiddentbCheckDept');
       
       TitleID.value=a;
       TitleName.value=b;
       CustID.value=c;
       CustName.value=d;
       PrincipalID.value=e;
       PrincipalName.value=f;
       CheckType.value=g;
       //CheckMode.value=h;
       CheckerID.value=i;
       CheckerName.value=j;
       CustBigID.value=k;
       CustBigName.value=l;
       MantbDeptID.value=m;
       MantbDeptName.value=n;
       CheckerDeptID.value=o;
       CheckerDeptName.value=p;
       document.getElementById('hiddenApplyNo').value=q;
   }
   else
   {
        document.getElementById('tbReportID').value=a;
        document.getElementById('tbReportNo').value=b;
   }
   closeReportdiv();
   
}
    //table行颜色
function pageDataList1ReportFrom(o,a,b,c,d){
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

function Fun_Search_StorageProduct(aa)
{
      search="1";
      TurnToPage(1);
}
function IfshowProduct(count)
    {
       if(myMethod=='1')
       {
        if(count=="0")
        {
            document.all["divpageReportFrom"].style.display = "none";
            document.all["pagecountReportFrom"].style.display = "none";
        }
        else
        {
            document.all["divpageReportFrom"].style.display = "block";
            document.all["pagecountReportFrom"].style.display = "block";
        }
       }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangeReportPageCountIndex(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportFrom=parseInt(newPageCount);
            TurnToPageCheckReport(parseInt(newPageIndex));
        }
    }
      //排序
    function OrderBy2(orderColum,orderTip)
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
        orderByReport = orderColum+"_"+ordering;
        TurnToPageCheckReport(1);
    }
 
function closeReportdiv()
{
    document.getElementById("divFromReport").style.display="none";
    document.getElementById('myBillNo1').value='';
    document.getElementById('myBillTitle1').value='';
    closeRotoscopingDiv(false,'divBackShadow');

}


</script>

