<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectChannels.ascx.cs" Inherits="UserControl_Human_SelectChannels" %>
<div id="sellModuleEmployee">
    <!--提示信息弹出详情start-->
    <a name="pageEmpDataList1Mark"></a>
    <div id="divSellTemplateSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeTemplatediv()" style="text-align: right; cursor: pointer">关闭</a>
                    <input   id="hidOBJ" type="hidden" />
                     <input id="hidObject1" type="hidden" />
    <input id="hidObject2" type="hidden" />  
                                   <a onclick="fnClose()" style="text-align: right; cursor: pointer">清空</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="templateEmpList"
            bgcolor="#999999">
                  <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" >
                                            选择 
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('ProxyCompanyCD','oC0');return false;">
                                                企业编号<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('ProxyCompanyName','oC1');return false;">
                                                企业名称<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('ContactName','oC2');return false;">
                                                联系人<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('ContactTel','oC3');return false;">
                                                固定电话<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('ContactMobile','oC4');return false;">
                                                移动电话<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('ContactWeb','oC14');return false;">
                                                网络通讯<span id="oC14" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('Important','oC5');return false;">
                                                重要程度<span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('Cooperation','oC6');return false;">
                                                合作关系<span id="oC6" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="templateEmpOrderBy('UsedStatus','oC7');return false;">
                                                启用状态<span id="oC7" class="orderTip"></span>
                                            </div>
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
                                <div id="pageTemplateEmpcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="templateEmpList_Pager" class="PagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellEmpPage">
                                    <span id="pagetemplateEmpList_Total"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowTemplateEmpPageCount" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToSellEmpPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeTemplateEmpPageCountIndex($('#ShowTemplateEmpPageCount').val(),$('#ToSellEmpPage').val());" />
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
var popTaskObj=new Object();
popTaskObj.InputObj1 = null;
popTaskObj.InputObj2 = null;

var pop1="";
var pop2="";
popTaskObj.ShowList=function(objInput1,objInput2)
{
document .getElementById ("hidOBJ").value =objInput1;
    popTaskObj.InputObj1 = objInput1;

    popTaskObj.InputObj2 = objInput2;
    document .getElementById ("hidObject1").value=objInput1;
      document .getElementById ("hidObject2").value=objInput2;
 
    document.getElementById('divSellTemplateSelect').style.display='block';
    SellTemplateTurnToPage(currentTemplateEmpPageIndex,objInput1,objInput2);
}
function fnClose( )
{
 pop1= document .getElementById ("hidOBJ").value ;
    $("#"+pop1).val('');
    $("#"+pop1).attr("title", '');
    $("#"+pop2).val( '');
    $("#"+pop2).attr("title", '');
    closeTemplatediv();
}
  
    var pageTemplateEmpcount = 10;//每页计数
    var totalTemplateEmpRecord = 0;
    var pagerTemplateEmpStyle = "flickr";//jPagerBar样式
    
    var currentTemplateEmpPageIndex = 1;
    var actiontemplateEmp = "";//操作
    var orderTemplateEmpBy ="";//排序字段
    //jQuery-ajax获取JSON数据
    function SellTemplateTurnToPage(pageIndex,objInput1,objInput2)
    {
           currentTemplateEmpPageIndex = pageIndex;   
            var postParam ="pageIndex="+pageIndex+"&PageCount="+pageTemplateEmpcount+"&Action=GetInfo&ProxyCompanyName=&Important=&Cooperation=&UsedStatus=&ProxyCompanyCD=&OrderBy="+orderTemplateEmpBy ; 
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/HumanManager/HRProxy_Query.ashx?'+postParam,//目标地址
           cache:false,
           beforeSend:function(){$("#templateEmpList_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#templateEmpList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                   
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                       +" <input type=\"radio\" name=\"radioEmp_\"  id=\"radioEmp_"+item.ID+"\" value=\""+item.ProxyCompanyName+"\" onclick=\"fnSelectTemplateEmp("+item.ID+",'"+item.ProxyCompanyName+"','"+objInput1+"','"+objInput2+"');\" />" + "</td>" //选择框
                        + "<td height='22' align='center'> " + item.ProxyCompanyCD + " </td>" //编号
                        + "<td height='22' align='center'>" + item.ProxyCompanyName + "</td>" //企业名称
                        + "<td height='22' align='center'>" + item.ContactName + "</td>" //联系人
                        + "<td height='22' align='center'>" + item.ContactTel + "</td>"  //固定电话 
                        + "<td height='22' align='center'>" + item.ContactMobile + "</td>"  //移动电话  
                        + "<td height='22' align='center'>" + item.ContactWeb + "</td>" //网络通讯
                        + "<td height='22' align='center'>" + item.Important + "</td>"//重要程度
                        + "<td height='22' align='center'>" + item.Cooperation + "</td>"// 合作关系
                        + "<td height='22' align='center'>" + item.UsedStatus + "</td>").appendTo($("#templateEmpList tbody"));
                   });
                    //页码
                    ShowPageBar("templateEmpList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerTemplateEmpStyle,mark:"pageEmpDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageTemplateEmpcount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"SellTemplateTurnToPage({pageindex},'"+objInput1+"','"+objInput2+"') ;return false;"}//[attr]
                    );
                  totalTemplateEmpRecord = msg.totalCount;
                  $("#ShowTemplateEmpPageCount").val(pageTemplateEmpcount);
                  ShowTotalPage(msg.totalCount,pageTemplateEmpcount,pageIndex,$("#pageTemplateEmpcount"));
                  $("#ToSellEmpPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#templateEmpList_Pager").show();pageTemplateCustDataList1("templateEmpList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    function ass()
    {
    alert (document .getElementById ("templateEmpList_Pager").innerHTML );
    }
    //table行颜色
function pageTemplateCustDataList1(o,a,b,c,d)
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


//改变每页记录数及跳至页数
function ChangeTemplateEmpPageCountIndex(newPageCount,newPageIndex)
{
 
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalTemplateEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
 
        this.pageTemplateEmpcount=parseInt(newPageCount);
         SellTemplateTurnToPage(parseInt(newPageIndex),  document .getElementById ("hidObject1").value.Trim()  ,document .getElementById ("hidObject2").value.Trim() );
    }
}
//排序
function templateEmpOrderBy(orderColum,orderTip)
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
    orderTemplateEmpBy = orderColum+"_"+ordering;
 
      SellTemplateTurnToPage(1,  document .getElementById ("hidObject1").value.Trim()  ,document .getElementById ("hidObject2").value.Trim() );
}
   
function closeTemplatediv()
{
    document.getElementById("divSellTemplateSelect").style.display="none";
}

function fnSelectTemplateEmp(TemplateNo,TemplateName,objInput1,objInput2)
{
 var num=document .getElementById ("radioEmp_"+TemplateNo ).value;

//alert (i);
//alert (TemplateName);

    $("#"+objInput1).val(TemplateName);
    $("#"+objInput1).attr("title",num);
    $("#"+objInput2).val(TemplateName);
    $("#"+objInput2).attr("title",num);
    closeTemplatediv();
}
</script>