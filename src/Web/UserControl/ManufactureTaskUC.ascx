<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufactureTaskUC.ascx.cs"
    Inherits="UserControl_ManufactureTaskUC" %>
<div id="ManufactureTask">
    <a name="pageOffDataList1Mark"></a>
    <div id="divzhezhao" style="filter: Alpha(opacity=0); width: 740px; height: 500px;
        z-index: 1000; position: absolute; display: none; top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <iframe style="border: 0; width: 700px; height: 100%; position: absolute;"></iframe>
    </div>
    <!--提示信息弹出详情start-->
    <div id="divManufactureTask" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px; overflow-x: auto;">
        <table width="100%">
            <tr>
                <td>
                    <img alt="关闭" id="btn_Close_a1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
                        onclick='closeManufactureTaskdiv();' />
                    <img alt="确定" id="btn_OK" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: pointer;'
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
                                <input name="txtTaskNo_UC" id="txtTaskNo_UC" type="text" class="tdinput" runat="server"
                                    size="13" specialworkcheck="单据编号" style="width: 95%" />
                            </td>
                            <td width="10%" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                                单据主题
                            </td>
                            <td width="23%" bgcolor="#FFFFFF">
                                <input name="txtTitle_UC" id="txtTitle_UC" type="text" class="tdinput" runat="server"
                                    size="19" specialworkcheck="单据主题" style="width: 95%" />
                            </td>
                            <td width="10%" bgcolor="#E7E7E7" align="right" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <img alt="检索" id="btn_Serch" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                    onclick='UCSearch();' />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="offerDataList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择<input type="checkbox" visible="false" name="ckAll" id="ckAll" onclick="ckAll1
                        ()" value="checkbox" />
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('TaskNo','oGroupOff');return false;">
                            生产任务单<span id="oGroupOff" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('Subject','oCOff1');return false;">
                            主题<span id="oCOff1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('Principal','oCOff5');return false;">
                            负责人<span id="oCOff5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('DeptID','oCOff6');return false;">
                            生产部门<span id="oCOff6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('ManufactureType','oCOff4');return false;">
                            加工类型<span id="oCOff4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('CreateDate','oCOff7');return false;">
                            创建日期<span id="oCOff7" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('ProductNo','oCOff8');return false;">
                            物品编号<span id="oCOff8" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('ProductName','oCOff9');return false;">
                            物品名称<span id="oCOff9" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('ProductCount','oCOff12');return false;">
                            数量<span id="oCOff12" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('InCount','oCOff12');return false;">
                            已入库数量<span id="oCOff12" class="orderTip"></span></div>
                    </th>
                    <th id="th_UnitName" runat="server" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                <div class="orderClick" onclick="OrderOffBy('InCount','oCOff12');return false;">
                        基本单位<span id="Span1" class="orderTip"></span></div>
                </th>
                <th id="th_JeBenCount" runat="server" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('InCount','oCOff12');return false;">
                        基本数量<span id="Span2" class="orderTip"></span></div>
                </th>
                <th id="th_UnitPrice" runat="server" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="OrderOffBy('InCount','oCOff12');return false;">
                        基本单价<span id="Span3" class="orderTip"></span></div>
                </th>
                    <%--                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div onclick="OrderOffBy('ID','oCOff13');return false;">
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
var popManufactureTaskObj=new Object();
popManufactureTaskObj.InputObj = null;


function fnSelectInfo()
{
    var strMTNo="";
    var DetailIDList="";
    var signFrame = findObj("offerDataList",document);
    
    var sameNo = "";
    var isSame = true;
    for(i=1;i<signFrame.rows.length;i++)
    {         
        
        var chk= $("#ckManufactureTask_"+ i).attr("checked");
        //chkSlected = document.getElementById("ckPurchaseArrive_"+i);
        if(chk)
        {
            var colValue = signFrame.rows[i].cells[1].getAttribute("title");
            if (sameNo != "" && sameNo != colValue)
            {
               isSame = false;
               break; 
            }
            else if(sameNo == "")//取第一个选中的ID赋值给sameNo
            {
            
                sameNo = colValue; 
            }
            var DetailID =  signFrame.rows[i].cells[14].innerHTML;
//            alert("strMTNo="+colValue+"\nDetailID="+DetailID)
            DetailIDList += DetailID + ","; 
            strMTNo=colValue;
        }
    }
    if(isSame==false)
    {   
        alert("请选择同一生产任务单中的明细！");
        //popMsgObj.ShowMsg('请选择同一个采购单中的明细！');
        return;
    }
    if(strMTNo=="")
    {
        closeManufactureTaskdiv();
        return false;
    }
     $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/ManufactureTaskUC.ashx',//目标地址
           cache:false,
           data: "actionOff=list&DetailIDList="+escape(DetailIDList)+"&strMTNo="+escape(strMTNo),//数据
           beforeSend:function(){AddPop();$("#pageOffList_Pager").hide();},//发送数据之前
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    if(msg.MT!=null)
                    {
                        FillBasicInfo(msg.MT[0].TaskNo
                            ,msg.MT[0].ID
                            ,msg.MT[0].ManufactureType,msg.MT[0].DeptName,msg.MT[0].PrincipalName);
                        $("#offerDataList tbody").find("tr.newrow").remove();
                        $.each(msg.MTList,function(i,item){
                            AddSignRow(i,item.TaskNo,item.ProductID,item.ProdNo,
                                item.ProductName,item.Specification,item.UnitID,item.UnitPrice,
                                item.ProductCount,item.FromLineNo,item.FromBillCount,item.InCount,item.StorageID,item.IsBatchNo,item.UsedUnitID);
                        });
                        CountNum();
                     } 
                    },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageOffList_Pager").show();offerDataList("offerDataList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
               
               //删除明细中所有数据
    fnDelRow();      
    closeManufactureTaskdiv();
}

popManufactureTaskObj.ShowList=function(objInput)
{
    popManufactureTaskObj.InputObj = objInput;
    document.getElementById('divManufactureTask').style.display='block';
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
    if(document.getElementById("ManufactureTaskUC1_hidMoreUnit_C").value == "false")
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
           url:  '../../../Handler/Office/StorageManager/ManufactureTaskUC.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageOffCount="+pageOffCount+"&actionOff=getinfo&orderByOff="+orderByOff+"&txtTaskNo_UC="+escape(document.getElementById("ManufactureTaskUC1_txtTaskNo_UC").value)+"&txtTitle_UC="+escape(document.getElementById("ManufactureTaskUC1_txtTitle_UC").value),//数据
           beforeSend:function(){$("#pageOffList_Pager").show();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    var index=1;
                    $("#offerDataList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                            
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"Checkbox\" name=\"ck\" id=\"ckManufactureTask_"+index+"\" value=\""+item.ID+"\" />"+"</td>"+
                             "<td height='22' align='center' title=\""+item.TaskNo+"\">"+ fnjiequ(item.TaskNo,18) +"</td>"+
                             "<td height='22' align='center' title=\""+item.Subject+"\">"+ fnjiequ(item.Subject,18) +"</a></td>"+
                            "<td height='22' align='center'>"+item.PrincipalName+"</td>"+
                            "<td height='22' align='center'>"+item.DeptName+"</td>"+
                            "<td height='22' align='center'>"+item.ManufactureType+"</td>"+
                            "<td height='22' align='center'>"+item.CreateDate+"</td>"+
                            "<td height='22' align='center'>"+item.ProductNo+"</td>"+
                            "<td height='22' align='center'>"+item.ProductName+"</td>"+
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
           complete:function(){hidePopup();$("#pageOffList_Pager").show();offerDataList("offerDataList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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


function closeManufactureTaskdiv()
{
    document.getElementById("divManufactureTask").style.display="none";
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

