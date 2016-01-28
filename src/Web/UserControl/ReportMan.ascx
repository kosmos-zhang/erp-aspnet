<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportMan.ascx.cs" Inherits="UserControl_ReportMan" %>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divReportMan" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 800px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -600px;">       
        
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="White" align="left" style="width: 50%;">
                  <img onclick="closeReportMandiv()" id="Button1" src="../../../Images/Button/Bottom_btn_close.jpg"  />
                     <img id="Button2" onclick="removeAll3();" src="../../../Images/Button/Bottom_btn_del.jpg"  />
                </td>
           
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    单据编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myBillNo66" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    单据主题
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myBillTitle66" class="tdinput" maxlength="50" type="text" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                </td>
                <td width="21%" bgcolor="#FFFFFF">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" bgcolor="#FFFFFF">                   
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                        onclick='TurnToPageReport(1)' />&nbsp;
                </td>
            </tr>
        </table>
        
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1ReportMan"
            bgcolor="#999999">
            <tbody>
                <tr>
                     <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                     </th>
                 
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMan('Subject','Subject3');return false;">
                            主题<span id="Subject3" class="orderTip"></span></div>
                     </th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMan('TaskNo','TaskNo3');return false;">
                            单据编号<span id="TaskNo3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                       <div class="orderClick" onclick="OrderByMan('PrincipalName','PrincipalName3');return false;">
                            生产负责人<span id="PrincipalName3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                     <div class="orderClick" onclick="OrderByMan('DeptName','DeptName3');return false;">
                            生产部门<span id="DeptName3" class="orderTip"></span></div>
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
                                <div id="pagecountReportMan">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageReport_PagerMan" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageReportMan">
                                    <input name="text" type="text" id="txt_pageProductMan" style="display: none" />
                                    <span id="pageDataList_TotalReportMan"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountReportMan" size="5" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageReportMan" size="5" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexMan($('#ShowPageCountReportMan').val(),$('#ToPageReportMan').val());" />
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
var popReportManObj=new Object();
var myMethod='';
popReportManObj.ShowList=function(method)
{
    myMethod=method;
    document.getElementById('divReportMan').style.display='block';
        openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    TurnToPageReport(currentPageIndexReportMan);
}

    var pagecountReportMan = 10;//每页计数
    var totalRecordReportMan = 0;
    var pagerStyleReport = "flickr";//jPagerBar样式
    
    var currentPageIndexReportMan = 1;
    var actionReport = "";//操作
    var orderByReport = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageReport(pageIndexReportMan)
    {
           currentPageIndexReportMan = pageIndexReportMan;
           var TheBillNo=document.getElementById('myBillNo66').value;
           var TheBillTitle=document.getElementById('myBillTitle66').value;
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
            if(TheBillNo!='')
            {
                if(strlen(TheBillNo)>0)
                {
                    if(!CheckSpecialWord(TheBillNo))
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
           url:  '../../../Handler/Office/StorageManager/ReportManufacture.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexReportMan+"&method="+myMethod+"&TheBillTitle="+escape(TheBillTitle)+"&TheBillNo="+escape(TheBillNo)+"&pageCount="+pagecountReportMan+"&action="+actionReport+"&orderby="+orderByReport+"",//数据
           beforeSend:function(){$("#pageReport_PagerMan").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1ReportMan tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioReport_"+item.ID+"\" id=\"radioReport_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"FillReportMan('"+item.TaskNo+"','"+item.PrincipalID+"','"+item.PrincipalName+"','"+item.DeptID+"','"+item.DeptName+"','"+item.ID+"');\" />"+"</td>"+
                        "<td height='22' align='center'>"+item.Subject+"</td>"+
                        "<td height='22' align='center'>"+item.TaskNo+"</td>"+
                        "<td height='22' align='center'>"+item.PrincipalName+"</td>"+
                        "<td height='22' align='center'>"+item.DeptName+"</td>").appendTo($("#pageDataList1ReportMan tbody"));
                   });
                   
                   //页码
                    ShowPageBar("pageReport_PagerMan",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleReport,mark:"pageDataListProductMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecountReportMan,
                    currentPageIndex:pageIndexReportMan,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageReport({pageindex});return false;"}//[attr]
                    );
                  totalRecordReportMan = msg.totalCount;
                  $("#ShowPageCountReportMan").val(pagecountReportMan);  
                  ShowTotalPage(msg.totalCount,pagecountReportMan,currentPageIndexReportMan);               
                  $("#ToPageReportMan").val(pageIndexReportMan);
                  ShowTotalPage(msg.totalCount,pagecountReportMan,currentPageIndexReportMan,$("#pagecountReportMan"));                  
                   
                 
                  },
           error: function() {}, 
           complete:function(){$("#pageReport_PagerMan").show();IfshowProduct(document.all["txt_pageProductMan"].value);pageDataList1ReportMan("pageDataList1ReportMan","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
function removeAll3()
{
    if(myMethod=='1')
{
   var TitleName=document.getElementById('ApplyID'); //源单编号
   var PrincipalName=document.getElementById('UserPrincipal');
   var PrincipalID=document.getElementById('hiddentxPrincipal');
   var MantbDeptName=document.getElementById('Depttxt');
   var MantbDeptID=document.getElementById('hiddentbDept');
   
   TitleName.value='';
   PrincipalID.value='0';
   PrincipalName.value='';
   MantbDeptID.value='0';
   MantbDeptName.value='';
   document.getElementById('hiddenApplyNo').value='0'; 
   document.getElementById('hiddenApplyID').value='0';
}
else
{
    document.getElementById('tbReportID').value='';
    document.getElementById('tbReportNo').value='';
}
   closeReportMandiv();
}
function FillReportMan(a,b,c,d,e,f)
{
if(myMethod=='1')
{
   var TitleName=document.getElementById('ApplyID'); //源单编号
   var PrincipalName=document.getElementById('UserPrincipal');
   var PrincipalID=document.getElementById('hiddentxPrincipal');
   var MantbDeptName=document.getElementById('Depttxt');
   var MantbDeptID=document.getElementById('hiddentbDept');
   
   TitleName.value=a;
   PrincipalID.value=b;
   PrincipalName.value=c;
   MantbDeptID.value=d;
   MantbDeptName.value=e;
   document.getElementById('hiddenApplyNo').value=a; 
   document.getElementById('hiddenApplyID').value=f;
  // document.getElementById('hiddenDetailID').value=f;
}
else
{
    document.getElementById('tbReportID').value=f;
    document.getElementById('tbReportNo').value=a;
}
   closeReportMandiv();
   
}
    //table行颜色
function pageDataList1ReportMan(o,a,b,c,d){
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
            document.all["divpageReportMan"].style.display = "none";
            document.all["pagecountReportMan"].style.display = "none";
        }
        else
        {
            document.all["divpageReportMan"].style.display = "block";
            document.all["pagecountReportMan"].style.display = "block";
        }
      }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexMan(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordReportMan-1)/newPageCount)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportMan=parseInt(newPageCount);
            TurnToPageReport(parseInt(newPageIndex));
        }
    }
    //排序
   function OrderByMan(orderColum,orderTip)
    {
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
        orderByReport = orderColum+"_"+ordering;
        TurnToPageReport(1);
    }
 
function closeReportMandiv()
{
    document.getElementById("divReportMan").style.display="none";
    document.getElementById('myBillNo66').value='';
    document.getElementById('myBillTitle66').value='';
    closeRotoscopingDiv(false,'divBackShadow');

}

</script>