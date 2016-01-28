<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageNoPass.ascx.cs" Inherits="UserControl_StorageNoPass" %>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divStorageNoPass" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 950px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -600px;">     
        
         <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="2" bgcolor="White" align="left" width="50%">
                      <img onclick="closeNoPassdiv()" id="Button1" src="../../../Images/Button/Bottom_btn_close.jpg" />
                       <img id="Button2" onclick="removeAll();" src="../../../Images/Button/Bottom_btn_del.jpg" />
                </td>
                <td height="25" colspan="2" bgcolor="white" align="center" width="50%">
                    
                </td>
            </tr>
            <tr class="table-item">
                <td width="25%" height="20" bgcolor="#E7E7E7" align="right">
                    单据编号
                </td>
                <td width="25%" bgcolor="#FFFFFF">
                    <input id="myBillNo11" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
   
                <td width="25%" bgcolor="#E7E7E7" align="right">
                  单据主题
                </td>
                <td width="25%" bgcolor="#FFFFFF">
                <input id="myBillTitle11" class="tdinput" maxlength="50" type="text" />
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" bgcolor="#FFFFFF">
                 
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                        onclick='TurnToPageNoPass(1)' />&nbsp;
                </td>
            </tr>
        </table>
        
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1NoPass"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('ReportNo','ReportNoll');return false;">
                            单据编号<span id="ReportNoll" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('Title','Titlell');return false;">
                            单据主题<span id="Titlell" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('ProductName','ProductNamell');return false;">
                            物品名称<span id="ProductNamell" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('ProdNo','ProdNoll');return false;">
                            物品编号<span id="ProdNoll" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('UnitName','UnitNamell');return false;">
                            单位<span id="UnitNamell" class="orderTip"></span></div>
                    </th>                  
                   <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('Specification','Specificationll');return false;">
                            规格<span id="Specificationll" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('NoPass','NoPassll');return false;">
                            不合格数量<span id="NoPassll" class="orderTip"></span></div>
                    </th>
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('NotPassNum','NotPassNumll');return false;">
                            已处置数量<span id="NotPassNumll" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('EmployeeName','EmployeeNamell');return false;">
                            检验人<span id="EmployeeNamell" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport1('DeptName','DeptNamell');return false;">
                            检验部门<span id="DeptNamell" class="orderTip"></span></div>
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
                                <div id="pagecountNoPass">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageNoPass_Product" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageNoPass">
                                    <input name="text" type="text" id="txt_pageProduct" style="display: none" />
                                    <span id="pageDataList_TotalNoPass"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountNoPass" size="5" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageNoPass" size="5" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexNoPass($('#ShowPageCountNoPass').val(),$('#ToPageNoPass').val());" />
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
var popNoPassObj=new Object();
var myMethod;
popNoPassObj.ShowList=function(method)
{
      
    document.getElementById('divStorageNoPass').style.display='block';
    openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    myMethod=method;
    TurnToPageNoPass(currentPageIndexReport2);

}

    var pagecountNoPass = 10;//每页计数
    var totalRecordReport2 = 0;
    var pagerStyleReport = "flickr";//jPagerBar样式
    
    var currentPageIndexReport2 = 1;
    var actionReport = "";//操作
    var orderByReport = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageNoPass(pageIndexReport2)
    {
          
            var myBillNo=document.getElementById('myBillNo11').value;
            var myBillTitle=document.getElementById('myBillTitle11').value;
            if(myBillTitle!='')
            {
                if(strlen(myBillTitle)>0)
                {
                    if(!CheckSpecialWord(myBillTitle))
                    {
                       closeNoPassdiv();
                       popMsgObj.ShowMsg('单据主题不能含有特殊字符!');
                       return false;
                    }
                }
            }
            if(myBillNo!='')
            {
                if(strlen(myBillNo)>0)
                {
                    if(!CheckSpecialWord(myBillNo))
                    {
                       closeNoPassdiv();
                       popMsgObj.ShowMsg('单据编号不能含有特殊字符!');
                       return false;
                    }
                }
            }
          currentPageIndexReport2 = pageIndexReport2;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageReportNoPass.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexReport2+"&method="+myMethod+"&myBillNo="+escape(myBillNo)+"&myBillTitle="+escape(myBillTitle)+"&pageCount="+pagecountNoPass+"&action="+actionReport+"&orderby="+orderByReport+"",//数据
           beforeSend:function(){$("#pageNoPass_Product").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1NoPass tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID !="")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioReport_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillReport1('"+  item.ReportNo+"','"+item.ID+"','"+item.ProductName+"','"+item.ProductID+"','"+item.ProdNo+"','"+item.Specification+"','"+item.UnitName+"','"+item.UnitID+"','"+item.NoPass+"','"+item.Title+"');\" />"+"</td>"+                      
                       "<td height='22' align='center'>" + item.ReportNo + "</td>"+
                        "<td height='22' align='center'>"+item.Title+"</td>"+
                        "<td height='22' align='center'>"+item.ProductName+"</td>"+
                        "<td height='22' align='center'>"+item.ProdNo+"</td>"+
                        "<td height='22' align='center'>"+item.UnitName+"</td>"+
                        "<td height='22' align='center'>"+item.Specification+"</td>"+
                        "<td height='22' align='center'>"+parseFloat(item.NoPass).toFixed(selPoint)+"</td>"+
                        "<td height='22' align='center'>"+parseFloat(item.NotPassNum).toFixed(selPoint)+"</td>"+
                        "<td height='22' align='center'>"+item.EmployeeName+"</td>"+
                        "<td height='22' align='center'>"+item.DeptName+"</td>").appendTo($("#pageDataList1NoPass tbody"));
                   });
                    //页码
                    ShowPageBar("pageNoPass_Product",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleReport,mark:"pageDataListProductMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecountNoPass,
                    currentPageIndex:pageIndexReport2,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageNoPass({pageindex});return false;"}//[attr]
                    );
                  totalRecordReport2 = msg.totalCount;
                  $("#ShowPageCountNoPass").val(pagecountNoPass);  
                  ShowTotalPage(msg.totalCount,pagecountNoPass,currentPageIndexReport2);               
                  $("#ToPageNoPass").val(pageIndexReport2);
                  ShowTotalPage(msg.totalCount,pagecountNoPass,currentPageIndexReport2,$("#pagecountNoPass"));                   
                  
                  },
           error: function() {}, 
           complete:function(){$("#pageNoPass_Product").show();IfshowProduct(document.getElementById("txt_pageProduct").value);pageDataList1NoPass("pageDataList1NoPass","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    

    //table行颜色
function pageDataList1NoPass(o,a,b,c,d){
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
            document.all["divpageNoPass"].style.display = "none";
            document.all["pagecountNoPass"].style.display = "none";
        }
        else
        {
            document.all["divpageNoPass"].style.display = "block";
            document.all["pagecountNoPass"].style.display = "block";
        }
       }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexNoPass(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordReport2-1)/newPageCount)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountNoPass=parseInt(newPageCount);
            TurnToPageNoPass(parseInt(newPageIndex));
        }
    }
    //排序
    function orderByReport1(orderColum,orderTip)
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
        TurnToPageNoPass(1);
    }
 
function closeNoPassdiv()
{
    document.getElementById("divStorageNoPass").style.display="none";
    document.getElementById('myBillNo11').value='';
    document.getElementById('myBillTitle11').value='';
    closeRotoscopingDiv(false,'divBackShadow');

}


</script>