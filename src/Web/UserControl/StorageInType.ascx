<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageInType.ascx.cs"
    Inherits="UserControl_StorageInType" %>
<div id="InType">
    <!--提示信息弹出详情start-->
    <div id="divzhezhao" style="filter: Alpha(opacity=0); width: 740px; height: 500px;
        z-index: 1000; position: absolute; display: none; top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <iframe style="border: 0; width: 700px; height: 100%; position: absolute;"></iframe>
    </div>
    <div id="divInType" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 700px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -400px; overflow-x: auto;">
        <table width="100%">
            <tr>
                <td>
                    <img alt="关闭" id="btn_Close_a6" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
                        onclick='closeInTypediv();' />
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
                                <input name="txtInNo" id="txtInNo" type="text" class="tdinput" runat="server" size="13"
                                    specialworkcheck="单据编号" style="width: 95%" />
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
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div>
                            入库编号<span id="Span1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div>
                            入库类别<span id="Span2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div>
                            入库主题<span id="Span3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div>
                            入库数量<span id="Span4" class="orderTip"></span></div>
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
                                <div id="pageOffcount">
                                </div>
                            </td>
                            <td height="28" align="center">
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
                                    <img src="../../../Images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
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
var popInTypeObj=new Object();
popInTypeObj.InputObj = null;


function fnSelectInfo(InNo)
{
    var InType=document.getElementById('ddlFromType').value;
    
    
     $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageInType.ashx',//目标地址
           cache:false,
           data: "action=list&InType="+InType+"&InNo="+InNo,//数据
           beforeSend:function(){AddPop();$("#pageOffList_Pager").hide();},//发送数据之前
           success: function(msg)
           {
                    
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    if(msg.InList!=null)
                    {
                            FillBasicInfo(msg.InList[0].InNo,msg.InList[0].ID,msg.InList[0].Executor,
                            msg.InList[0].EnterDate,msg.InList[0].Summary,msg.InList[0].DeptName,msg.InList[0].DeptID)//填充页面基本信息
                        $.each(msg.InList,function(i,item){                        
                            AddSignRow(i,item.InNo,item.ProductID,item.ProdNo,item.ProductName,item.Specification,item.CodeName,
                            item.UnitPrice,item.ProductCount,item.SortNo,item.Remark,item.StorageID,item.MinusIs,
                            item.UseCount,item.UnitID,item.IsBatchNo,item.UsedUnitID,item.BatchNo,item.UsedPrice)
                        });
                        CountNum();
                        
                     } 
                    },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageOffList_Pager").show();offerDataList("offerDataList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
               
               //删除明细中所有数据
    fnDelRow();      
    closeInTypediv();
}
popInTypeObj.ShowList=function(objInput)
{
    popInTypeObj.InputObj = objInput;
    document.getElementById('divInType').style.display='block';
    document.getElementById('divzhezhao').style.display='block';
    TurnToOffPage(currentOffPageIndex);
}
  
    var pageOffCount = 10;//每页计数
    var totalOffRecord = 0;
    var pagerOffStyle = "flickr";//jPagerBar样式
    
    var currentOffPageIndex = 1;
    var action = "";//操作
    var orderByOff = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToOffPage(pageIndex)
    {
           var InType = document.getElementById('ddlFromType').value;
           currentOffPageIndex = pageIndex;        
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageInType.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageOffCount="+pageOffCount+"&action=getinfo&orderByOff="+orderByOff+"&InType="+InType+"&txtInNo="+escape(document.getElementById("StorageInType1_txtInNo").value)+"&txtTitle="+escape(document.getElementById("StorageInType1_txtTitle").value),//数据
           beforeSend:function(){$("#pageOffList_Pager").show();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    var index=1;
                    $("#offerDataList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"rd\" id=\"rdInType_"+index+"\" value=\""+item.ID+"\" onclick=\"fnSelectInfo('"+item.InNo+"')\" />"+"</td>"+
                             "<td height='22' align='center'>"+ item.InNo +"</td>"+
                             "<td height='22' align='center'>"+ item.InType +"</a></td>"+
                             "<td height='22' align='center'>"+item.Title+"</td>"+
                             "<td height='22' align='center'>"+item.InNumber+"</td>").appendTo($("#offerDataList tbody"));
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


function closeInTypediv()
{
    document.getElementById("divInType").style.display="none";
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

