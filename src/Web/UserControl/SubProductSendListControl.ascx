<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubProductSendListControl.ascx.cs" Inherits="UserControl_SubProductSendListControl" %>
<%@ Register src="ProductDiyAttr.ascx" tagname="ProductDiyAttr" tagprefix="uc1" %>
<div id="divModuleSubProdcut">
    <!--提示信息弹出详情start-->
    <a name="pageSubProductMark"></a>
    <div id="divGetSubProduct" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 70%; z-index: 300; position: absolute; display: none;" >
        <table width="99%">
            <tr>
            <td >
    <%--        <a onclick="clearSend();" style="text-align: right; cursor: pointer">清空</a>     --%>
            
            <img alt="清除" id="ClearInputProduct" src="../../../images/Button/Bottom_btn_del.jpg"
                        style='cursor: pointer' onclick='clearSend();' />
            
              <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor:pointer;'
                        onclick='closeSubProductdiv();'  />
           <%-- <img src="../../../Images/Button/closelabel.gif" style="float:right; cursor:pointer;"  alt="关闭" onclick="closeSubProductdiv()"/>--%></td>
            </tr>
        </table>

        <table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClick'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                    </div>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" class="Blue">
                    <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%" border="0" align="center" cellpadding="1" id="searchtable" cellspacing="0"
                        bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                    id="tblInterviewInfo">
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            配送单编号
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input id="txtSendNoUC" type="text" class="tdinput tboxsize" maxlength="25" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                           要货分店
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                          <asp:DropDownList ID="ddlSubStore" runat="server">
                                        </asp:DropDownList>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                           配送部门
                                        </td>
                                        <td height="20" class="tdColInput" width="24%">
                                             <input type="text" id="DeptOutDeptID" readonly onclick="alertdiv('DeptOutDeptID,txtOutDeptID')"
                                            class="tdinput tboxsize"  />
                                        <input type="hidden" id="txtOutDeptID" />
                                        </td>
                                    </tr>
                                   
                               
                                    <tr>
                                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                                onclick='SubProductTurnToPage(1);' id="img_btn_search" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
             <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="divSubProductList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="SubCreateSort('SendNo');return false;">
                                    配送单编号<span id="SendNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="SubCreateSort('ApplyUserIDName');return false;">
                                    配送申请人<span id="ApplyUserIDName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="SubCreateSort('ApplyDeptIDName');return false;">
                                    要货分店<span id="ApplyDeptIDName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="SubCreateSort('OutDeptIDName');return false;">
                                    配送部门<span id="OutDeptIDName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="SubCreateSort('SendCount');return false;">
                                    配送数量合计<span id="SendCount" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="SubCreateSort('SendFeeSum');return false;">
                                    配送金额合计<span id="SendFeeSum" class="orderTip"></span></div>
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
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="28%">
                                <div id="pageSubProductcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="getproductlist_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSubProductPage">
                                    <span id="pageSubProduct_Total"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowSubProductPageCount" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToSubProductPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeSubProductPageCountIndex($('#ShowSubProductPageCount').val(),$('#ToSubProductPage').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                </td>
            </tr>
        </table>
        
        <input type="hidden" id="txtSubProductOrderBy"  value="ID DESC"/>
    </div>
    <!--提示信息弹出详情end-->
    
</div>

<script type="text/javascript">
var popSubProductObj=new Object();
popSubProductObj.InputObj = null;

popSubProductObj.ShowList=function(pageIndex)
{
    document.getElementById('divGetSubProduct').style.display='block';
    CenterToDocument("divGetSubProduct",true);
    SubProductTurnToPage(pageIndex);
}
  
    var pageSubProductcount = 10;//每页计数
    var totalSellEmpRecord = 0;
    var pagerSellEmpStyle = "flickr";//jPagerBar样式
    
    var currentSellEmpPageIndex = 1;
    var actionSellEmp = "";//操作
    var orderSellEmpBy = "";//排序字段
    //jQuery-ajax获取JSON数据
     var pageCount = 10;//每页计数
     var totalRecord = 0;
    function SubProductTurnToPage(pageIndex)
    {

        /*构造参数*/
        var para="action=GETLIST&"+
                    "&BusiStatus=4&BillStatus=-1&FlowStatus=-1"+
                    "&pageIndex="+pageIndex+"&PageSize="+pageCount+
                    "&OrderBy="+document.getElementById("txtSubProductOrderBy").value+
                    "&SendNo="+escape(document.getElementById("txtSendNoUC").value)+
                   "&DeptID="+document.getElementById("SubProductSendListControl1_ddlSubStore").value+
                   "&OutDeptID="+document.getElementById("txtOutDeptID").value;
                  
    /*异步页面*/
    var url="../../../Handler/Office/LogisticsDistributionManager/SubDeliverySendList.ashx";
           currentSellEmpPageIndex = pageIndex;     
           $.ajax({
            type: "POST",
            dataType:"json",
            url: url,
            cache:false,
            data: para,
            beforeSend:function(){ openRotoscopingDiv(false,"divPageMask","PageMaskIframe");},
            success: function(msg){
                   $("#divSubProductList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID!=null && item!="")
                        {
                        $("<tr class='newrow'></tr>").append(createTd("<input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"getSendList("+item.ID+",'"+item.SendNo +"','"+item.ApplyDeptID+"');\" />")+createTd(item.SendNo)+
                        createTd(item.ApplyUserIDName)+
                        createTd(item.ApplyDeptIDName)+
                        createTd(item.OutDeptIDName)+
                        createTd(parseFloat(item.SendCount).toFixed(2))+
                        createTd(parseFloat( item.SendPrice).toFixed(2))).appendTo($("#divSubProductList tbody"));                    
                   } });
                    //页码
                    ShowPageBar("getproductlist_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerSellEmpStyle,mark:"pageSubProductMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"SubProductTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellEmpRecord = msg.totalCount;
                  $("#ShowSubProductPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageSubProductcount,pageIndex,$("#pageSubProductcount"));
                  $("#ToSubProductPage").val(pageIndex);
                  },
           error: function(msg) {}, 
           complete:function(){$("#getproductlist_Pager").show();pageSubProductDataList1("divSubProductList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageSubProductDataList1(o,a,b,c,d)
{
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


/*判断 退货价格 */
function checkBackPrice(item)
{
    if(item.BackPrice=="")
    {
        if(item.BackPriceDefault=="")
            return "0";
        else
            return parseFloat(item.BackPriceDefault).toFixed(2);
    }
    else
        return parseFloat(item.BackPrice).toFixed(2);
}

//改变每页记录数及跳至页数
function ChangeSubProductPageCountIndex(newPageCount,newPageIndex,rowid)
{
    
     if(!IsNumOrFloat(newPageCount,true))
        {
            popMsgObj.Show("配送单列表|","每页显示数必须为数值|");
            document.getElementById("ShowPageCount").value="10";
            return;
        }
        if(!IsNumOrFloat(newPageIndex,true))
        {
            popMsgObj.Show("配送单列表|","页数必须为数值|");
            document.getElementById("ToPage").value="1";
            return;
        }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalSellEmpRecord-1)/newPageCount)+1 )
        {
            popMsgObj.Show("配送单列表|","转到页数超出查询范围！|");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount);
             SubProductTurnToPage(parseInt(newPageIndex))
        }
}
//排序
/*设置排序字段*/
function SubCreateSort(control)
{
        var ordering=document.getElementById("txtSubProductOrderBy");
        var obj=document.getElementById(control);
        var allOrderTipDOM  = $(".orderTip");
        allOrderTipDOM.empty();
        if(ordering.value==(control+ " ASC"))
        {
            ordering.value=control+ " DESC";
            obj.innerHTML="↓";
        }
        else
        {
            ordering.value=control+ " ASC";
            obj.innerHTML="↑";
        }
        SubProductTurnToPage(1);
}

   
function closeSubProductdiv()
{
    document.getElementById("divGetSubProduct").style.display="none";
    closeRotoscopingDiv(false,"divPageMask");
}

/*格式化折扣*/
function getDiscount(discount)
{
    if(discount!="1.00")
        return discount;
    else
        return "";
}


/*判断是默认配送还是分店*/
function getSubStore(name)
{
    if(name=="")
        name="默认";
    return name;
}

/*构造td*/
function createTd(value)
{
    return "<td align=\"center\" >"+value+"</td>";
}
</script>
