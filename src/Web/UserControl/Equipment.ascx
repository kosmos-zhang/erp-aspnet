<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Equipment.ascx.cs" Inherits="UserControl_Equipment" %>

<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divEquipment" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 600px; z-index: 1000; position: absolute; display: none; top: 50%; left: 70%;
        margin: 5px 0 0 -400px;">
        <table width="99%">
            <tr>
                <td width="15">
                    <img id="btnSure" alt="确认" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;' onclick="Fun_FillParent_EquipmentContent(popEquipmentObj.MachineID,popEquipmentObj.MachineNo,popEquipmentObj.MachineName);"  />
                    
                </td>
                <td align="left">
                    <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;' onclick="document.getElementById('divEquipment').style.display='none';"  />
                </td>
            </tr>
        </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListEquipment"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick">
                                    设备编号<span id="oEquipmentNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" >
                                    设备名称<span id="oEquipmentName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" >
                                    规格型号<span id="oNorm" class="orderTip"></span></div>
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
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="30%">
                                        <div id="pagecountEquipment">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataListEquipment_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpageEquipment">
                                            <input name="text" type="text" id="TextEquipment" style="display: none" />
                                            <span id="pageDataListEquipment_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCountEquipment" style="width:25px"/>
                                            条 转到第
                                            <input name="text" type="text" id="ToPageEquipment" style="width:25px"/>
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexEquipment($('#ShowPageCountEquipment').val(),$('#ToPageEquipment').val());" />
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

<script language="javascript">
var popEquipmentObj=new Object();
popEquipmentObj.InputObj = null;
popEquipmentObj.MachineID =0;
popEquipmentObj.MachineNo =null;
popEquipmentObj.MachineName =null;

popEquipmentObj.ShowList=function(objInput)
{
    popEquipmentObj.InputObj = objInput;
    document.getElementById('divEquipment').style.display='block';
    TurnToPageEquipment(currentPageIndex);
}
 
    var pageCountEquipment = 10;//每页计数
    var totalRecordEquipment = 0;
    var pagerStyleEquipment = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var orderByEquipment = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageEquipment(pageIndexEquipment)
    {
           currentPageIndexEquipment= pageIndexEquipment;
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/ProductionManager/ManufactureReportInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexEquipment+"&pageCount="+pageCountEquipment+"&orderby="+orderByEquipment+"",//数据
           beforeSend:function(){AddPopEquipment();$("#pageDataListEquipment_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListEquipment tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){                       
                        if(item.ID != null && item.ID != "")
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioMac_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_Select_EquipmentContent("+item.ID+",'"+item.EquipmentNo+"','"+item.EquipmentName+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.EquipmentNo + "</td>"+
                        "<td height='22' align='center'>" + item.EquipmentName + "</td>"+
                        "<td height='22' align='center'>"+item.Norm+"</td>").appendTo($("#pageDataListEquipment tbody"));
                   });
                    //页码

                      ShowPageBar("pageDataListEquipment_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                     "<%= Request.Url.AbsolutePath %>",//[url]
                     {style:pagerStyleEquipment,mark:"pageEquipmentDataListMark",
                     totalCount:msg.totalCount,
                     showPageNumber:3,
                     pageCount:pageCountEquipment,
                     currentPageIndex:pageIndexEquipment,
                     noRecordTip:"没有符合条件的记录",
                     preWord:"上页",
                     nextWord:"下页",
                     First:"首页",
                     End:"末页",
                     onclick:"TurnToPageEquipment({pageindex});return false;"}//[attr]
                     );
                    
                      totalRecord = msg.totalCount;
                      document.getElementById("TextEquipment").value=msg.totalCount;
                      $("#ShowPageCountEquipment").val(pageCountEquipment);
                      ShowTotalPage(msg.totalCount,pageCountEquipment,pageIndexEquipment);
                      $("#ToPageEquipment").val(pageIndexEquipment);
                      ShowTotalPage(msg.totalCount,pageCountEquipment,currentPageIndex,$("#pagecountEquipment"));
                      },
           error: function() {alert('加载设备时出错');}, 
           complete:function(){hidePopupEquipment();$("#pageDataListEquipment_Pager").show();IfshowEquipment(document.getElementById("TextEquipment").value);pageDataListEquipment("pageDataListEquipment","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataListEquipment(o,a,b,c,d){
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

function IfshowEquipment(count)
    {
        if(count=="0")
        {
            document.all["divpageEquipment"].style.display = "none";
            document.all["pagecountEquipment"].style.display = "none";
        }
        else
        {
            document.all["divpageEquipment"].style.display = "block";
            document.all["pagecountEquipment"].style.display = "block";
        }
    }
    //改变每页记录数及跳至页数
    function ChangePageCountIndexEquipment(newPageCount,newPageIndex)
    {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;
        
        if(!IsNumber(newPageIndex) || newPageIndex==0)
        {
            isFlag = false;
            fieldText = fieldText + "跳转页面|";
	        msgText = msgText +  "必须为正整数格式|";
        }
        if(!IsNumber(newPageCount) || newPageCount==0)
        {
            isFlag = false;
            fieldText = fieldText + "每页显示|";
	        msgText = msgText +  "必须为正整数格式|";
        }
        if(!isFlag)
        {
            popMsgObj.Show(fieldText,msgText);
        }
        else
        {
            if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
            {
                popMsgObj.Show("转到页数|","超出查询范围|");
                return false;
            }
            else
            {
                this.pageCountEquipment=parseInt(newPageCount);
                TurnToPageEquipment(parseInt(newPageIndex));
            }
        
        }

    }
    //排序
    function OrderByEquipment(orderColum,orderTip)
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
   
function AddPopEquipment()
{
    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
 
function hidePopupEquipment()
{
    //document.all.Forms.style.display = "none";
}
function Fun_Select_EquipmentContent(id,no,name)
{
    popEquipmentObj.MachineID = id;
    popEquipmentObj.MachineNo = no;
    popEquipmentObj.MachineName = name;
}

</script>