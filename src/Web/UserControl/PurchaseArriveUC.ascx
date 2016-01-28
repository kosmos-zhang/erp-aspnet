<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseArriveUC.ascx.cs"
    Inherits="UserControl_PurchaseArriveUC" %>
<div id="PurchaseArrive">
    <a name="pageOffDataList1Mark"></a>
    <!--提示信息弹出详情start-->
    <div id="divzhezhao" style="filter: Alpha(opacity=0); width: 810px; height: 500px;
        z-index: 1000; position: absolute; display: none; top: 20%; left: 50%; margin: 5px 0 0 -400px;">
        <iframe style="border: 0; width: 700px; height: 100%; position: absolute;"></iframe>
    </div>
</div>
<div id="divPurchaseArrive" style="border: solid 10px #93BCDD; background: #fff;
    padding: 10px; width: 870px; z-index: 1001; position: absolute; display: none;
    top: 20%; left: 50%; margin: 5px 0 0 -400px; overflow-x: auto;">
    <table width="100%">
        <tr>
            <td>
                <img alt="关闭" id="btn_Close_a2" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                    onclick='closePurchaseArrivediv();' />
                <img alt="确定" id="btn_OK" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                    onclick='fnSelectInfo();' />
                    <input id="hidMoreUnit_C" type="hidden" runat="server" />
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
        bgcolor="#CCCCCC">
        <tr>
            <td bgcolor="#FFFFFF">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                    class="table">
                    <tr class="table-item">
                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                            单据编号
                        </td>
                        <td width="24%" bgcolor="#FFFFFF">
                            <input name="txtArriveNo" id="txtArriveNo" type="text" class="tdinput" runat="server"
                                size="13" specialworkcheck="单据编号" style="width: 95%" />
                        </td>
                        <td width="10%" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                            单据主题
                        </td>
                        <td width="23%" bgcolor="#FFFFFF">
                            <input name="txtTitle" id="txtTitle" type="text" class="tdinput" runat="server" size="19"
                                specialworkcheck="单据主题" style="width: 95%" />
                        </td>
                        <td width="10%" bgcolor="#E7E7E7" align="right" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                            <table>
                                <tr>
                                    <td>
                                        <img alt="检索" id="btn_Serch" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='UCSearch();' />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
            </td>
        </tr>
    </table>
    </td> </tr> </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="offerDataList"
        bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    选择<input type="checkbox" visible="false" name="ckAll" id="ckAll" onclick="ckAll1
                        ()" value="checkbox" />
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('ArriveNo','oGroupOff');return false;">
                        采购到货单<span id="oGroupOff" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('Title','oCOff1');return false;">
                        主题<span id="oCOff1" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('Purchaser','oCOff4');return false;">
                        业务员<span id="oCOff4" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('ProviderID','oCOff5');return false;">
                        供应商<span id="oCOff5" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('ProductName','oCOff9');return false;">
                        物品名称<span id="oCOff9" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('ColorName','oColorName');return false;">
                        颜色<span id="oColorName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('TotalPrice','oCOff11');return false;">
                        金额<span id="oCOff11" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('ProductCount','oCOff12');return false;">
                        计划数量<span id="oCOff12" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('InCount','oCOff12');return false;">
                        已入库数量<span id="oCOff12" class="orderTip"></span></div>
                </th>
                <th id="th_UnitName" runat="server" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('UnitName','oUnitName');return false;">
                        基本单位<span id="oUnitName" class="orderTip"></span></div>
                </th>
                <th id="th_JeBenCount" runat="server" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('JiBenCount','oJiBenCount');return false;">
                        基本数量<span id="oJiBenCount" class="orderTip"></span></div>
                </th>
                <th id="th_UnitPrice" runat="server" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('UnitPrice','oUnitPrice');return false;">
                        基本单价<span id="oUnitPrice" class="orderTip"></span></div>
                </th>
                <%--<th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('InCount','oCOff13');return false;">
                            明细表ID<span id="oCOff13" class="orderTip"></span></div>
                    </th>--%>
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
                            <div id="pageOffcount">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="pageOffList_Pager" class="jPagerBar">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="divOffpage">
                                <span id="pageOffList_Total"></span>每页显示
                                <input name="text" type="text" id="ShowOffPageCount" style="width: 20px;" />
                                条 转到第
                                <input name="text" type="text" style="width: 20px;" id="OffToPage" />
                                页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                    width="36" height="28" align="absmiddle" onclick="ChangeOffPageCountIndex($('#ShowOffPageCount').val(),$('#OffToPage').val());" />
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
var popPurchaseArriveObj=new Object();
popPurchaseArriveObj.InputObj = null;


function fnSelectInfo()
{

    var strPANo="";
    var DetailIDList="";
    var signFrame = findObj("offerDataList",document);
    
    var sameNo = "";
    var isSame = true;
    for(i=1;i<signFrame.rows.length;i++)
    {         
        
        var chk= $("#ckPurchaseArrive_"+ i).attr("checked");
        if(chk)
        {
            var colValue = signFrame.rows[i].cells[1].getAttribute("title");
            if (sameNo != "" && sameNo != colValue)
            {
               isSame = false;
               break; 
            }
            else if(sameNo == "")
            {
            
                sameNo = colValue; 
            }
            var DetailID =  signFrame.rows[i].cells[13].innerHTML;//?
            DetailIDList += DetailID + ","; 
            strPANo=colValue;
        }
    }
    if(isSame==false)
    {   
        alert("请选择同一采购到货单中的明细！");
        //popMsgObj.ShowMsg('请选择同一个采购单中的明细！');
        return;
    }
    if(strPANo=="")
    {
        closePurchaseArrivediv();
        return false;
    }
    
     $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/PurchaseArriveUC.ashx',//目标地址
           cache:false,
           data: "actionOff=list&DetailIDList="+DetailIDList+"&strPANo="+strPANo,//数据
           beforeSend:function(){AddPop();$("#pageOffList_Pager").hide();},//发送数据之前
           success: function(msg)
           {

                    //数据获取完毕，填充页面据显示
                    //数据列表
                    if(msg.PA!=null)
                    {                    
                        FillBasicInfo(msg.PA[0].ArriveNo,msg.PA[0].ID,msg.PA[0].ProviderName,msg.PA[0].PurchaserName,msg.PA[0].DeptName);
                        //FillBasicInfo(ArriveNo,FromBillID,ProviderName,Buyer,DeptName)
                        
                        $("#offerDataList tbody").find("tr.newrow").remove();
                        $.each(msg.PAList,function(i,item){
                            AddSignRow(i,item.InNo,item.ProductID,item.ProductNo,
                            item.ProductName,item.ColorName,item.UnitID,item.DetailUnitID,
                            item.UnitPrice,item.TotalPrice,
                            item.ID,item.ProductCount,
                            item.Remark,item.ID,item.ArriveNo,
                            item.FromLineNo,
                            item.FromTypeName,item.Specification,item.FromBillCount,item.InCount,item.TaxRate,item.StorageID,item.IsBatchNo,item.UsedUnitID)
                        });
                        CountNum();
                     } 
                    },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageOffList_Pager").show();offerDataList("offerDataList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
               
               //删除明细中所有数据
    fnDelRow();      
    closePurchaseArrivediv();
}

popPurchaseArriveObj.ShowList=function(objInput)
{
    popPurchaseArriveObj.InputObj = objInput;
    document.getElementById('divPurchaseArrive').style.display='block';
    document.getElementById('divzhezhao').style.display='block';
    TurnToOffPage(currentOffPageIndex);
}
  
    var pageOffCount = 10;//每页计数
    var totalOffRecord = 0;
    var pagerOffStyle = "flickr";//jPagerBar样式
    
    var currentOffPageIndex = 1;
    var actionOff = "";//操作
    var orderByOff = "";//排序字段
//jQuery-ajax获取JSON数据
function TurnToOffPage(pageIndex)
{

    var th_Other = "style='display:block'";    
    if(document.getElementById("PurchaseArriveUC1_hidMoreUnit_C").value == "false")
    {
        th_Other = "style='display:none'";
    }
    else
    {
        th_Other = "style='display:block'";
    }
              
    
           currentOffPageIndex = pageIndex;
     
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/PurchaseArriveUC.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageOffCount="+pageOffCount+"&actionOff=getinfo&orderByOff="+orderByOff+"&txtArriveNo="+escape(document.getElementById("PurchaseArriveUC1_txtArriveNo").value)+"&txtTitle="+escape(document.getElementById("PurchaseArriveUC1_txtTitle").value),//数据
           beforeSend:function(){$("#pageOffList_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    var index=1;
                    $("#offerDataList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {                            
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"Checkbox\" name=\"ck\" id=\"ckPurchaseArrive_"+index+"\" value=\""+item.ID+"\" />"+"</td>"+
                             "<td height='22' align='center' title=\""+item.ArriveNo+"\">"+ fnjiequ(item.ArriveNo,18) +"</td>"+
                             "<td height='22' align='center' title=\""+item.ArriveNo+"\">"+ fnjiequ(item.Title,18) +"</a></td>"+
                            "<td height='22' align='center'>"+item.Purchaser+"</td>"+
                            "<td height='22' align='center'>"+item.ProviderID+"</td>"+
                            "<td height='22' align='center'>"+item.ProductName+"</td>"+
                            "<td height='22' align='center'>"+item.ColorName+"</td>"+
                            "<td height='22' align='center'>"+item.TotalPrice.slice(0,item.TotalPrice.length-2)+"</td>"+
                            "<td height='22' align='center'>"+item.ProductCount+"</td>"+
                            "<td height='22' align='center'>"+item.InCount+"</td>"+
                             "<td height='22' align='center' "+th_Other+">"+item.UnitName+"</td>"+
                              "<td height='22' align='center' "+th_Other+">"+item.JiBenCount+"</td>"+
                               "<td height='22' align='center' "+th_Other+">"+item.UnitPrice+"</td>"+
                            "<td height='22' align='center' style=\"display:none\">"+item.DetailID+"</td>").appendTo($("#offerDataList tbody"));
                            index=index+1;
                        }
                   });
                    //页码
                   ShowPageBar("pageOffList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerOffStyle
                    ,mark:"pageOffDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageOffCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    End:"末页",
                    First:"首页",
                    onclick:"TurnToOffPage({pageindex});return false;"}//[attr]
                    );
                  totalOffRecord = msg.totalCount;
                  $("#ShowOffPageCount").val(pageOffCount);
                  ShowTotalPage(msg.totalCount,pageOffCount,pageIndex,$("#pageOffcount"));
                  $("#OffToPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#pageOffList_Pager").show();offerDataList("offerDataList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function offerDataList(o,a,b,c,d)
{
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

//改变每页记录数及跳至页数
function ChangeOffPageCountIndex(newPageCount,newPageIndex)
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalOffRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageOffCount=parseInt(newPageCount);
        TurnToOffPage(parseInt(newPageIndex));
    }
}
//排序
function OrderOffBy(orderColum,orderTip)
{
    var ordering = "d";
    var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↑");
    }
    else
    {
        
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderByOff = orderColum+"_"+ordering;
    TurnToOffPage(1);
}


function closePurchaseArrivediv()
{
    document.getElementById("divPurchaseArrive").style.display="none";
    document.getElementById("divzhezhao").style.display="none";
}

function ckAll1()
{

   var signFrame = findObj("offerDataList",document);
   var ck = document.getElementsByName("ckAll");
   var ck1 = document.getElementsByName("ck");
   if(ck[0].checked)
   {
        for(var j=0;j<ck1.length;j++)
        {  
           if(!(ck1[j].disabled||ck1[j].readonly))
           {
           ck1[j].checked=true;
           }
          
        }
   }
   else
   {
     for(var j=0;j<ck1.length;j++)
        {  
           ck1[j].checked=false;
        }
   }
   
}

function UCSearch(aa)
{
      TurnToOffPage(1);
}

</script>

