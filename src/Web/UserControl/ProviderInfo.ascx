<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProviderInfo.ascx.cs"
    Inherits="Handler_Office_PurchaseManager_ProviderInfo" %>
<div id="divProviderInfo" style="display:none ;"><%----%>
    <div id="divProviderInfo2">
    <iframe id="frmProviderInfo2"></iframe></div>
    <div style="border: solid 10px #93BCDD; background: #fff; padding: 10px; width: 800px;
        z-index: 200; position: absolute; top: 20%; left: 45%; margin: 5px 0 0 -400px;">
  <table width="100%">
            <tr>
                <td align="left">
                    <a onclick="closeProviderdiv()" style="text-align: right; cursor: pointer"><img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
                     <a onclick="clearProviderdiv()" id="clearProviderdiv"   style="text-align: right; cursor: pointer;"><img  src="../../../images/Button/Bottom_btn_del.jpg"/></a>
                </td>
                <td align="left">
                   
                </td>
            </tr>
        </table>
        <table width="99%"  border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top" align="left">
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
                <td valign="top" class="Blue" align="left">
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
                                    <tr class="table-item">
                            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                供应商编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input type="text" id="txt_ProviderNo" class="tdinput" specialworkcheck="供应商编号" />
                            </td>
                            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                供应商名称
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input type="text" id="txt_ProviderName" class="tdinput" specialworkcheck="供应商名称" />
                            </td>
                            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <img alt="检索" id="btn_Serch" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                    onclick='Fun_Search_ProviderInfo();' />
                                <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;display:none'
                                    onclick="fnFillProv();"   id="ProvSure" />
                            </td>
                        </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                   <table width="99%" border="0" align="center" cellpadding="0" id="pageDataList1Provider"
                        bgcolor="#999999">
                        <tbody>
                            <tr>
                                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    选择
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick">
                                        供应商编号<span id="Span2" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick">
                                        供应商名称<span id="oC2" class="orderTip"></span></div>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                   <table  width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                                    class="PageList">
                                    <tr>
                                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                                <tr>
                                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                                        <div id="pageProvidercount">
                                                        </div>
                                                    </td>
                                                    <td height="28" align="right">
                                                        <div id="pageDataList1_PagerProvider" class="jPagerBar">
                                                        </div>
                                                    </td>
                                                    <td height="28" align="right">
                                                        <div id="divPageProviderInfoUC">
                                                            <input name="text" type="text" id="Text2Provider" style="display: none" />
                                                            <span id="pageDataList1_TotalProvider"></span>每页显示
                                                            <input name="text" type="text" id="ShowPageCountProvider" style="width: 22px" />条
                                                            转到第
                                                            <input name="text" type="text" id="ToPageProvider" style="width: 35px" />页
                                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexProvider($('#ShowPageCountProvider').val(),$('#ToPageProvider').val());" />
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
        <br />
        
        
    </div>
</div>

<script language="javascript" type="text/javascript">
    var pageCountProvider = 10;//每页计数
    var totalRecordProvider = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var currentPageIndexProvider = 1;
    var orderByProvider = "";//排序字段



var popProviderObj = new Object();
popProviderObj.InputObjID = null;
popProviderObj.InputObjName = null;
popProviderObj.InputObjNo = null;
popProviderObj.InputFlag = null;
popProviderObj.duoxuan = null;
popProviderObj.OrderBy="AskOrder ASC";
popProviderObj.pageIndex=1;
popProviderObj.pageCount=10;
popProviderObj.totalCount=0;

popProviderObj.ShowProviderList=function(objProviderID,objProviderName,objProviderNo,objFlag,duoxuan)
{
    popProviderObj.InputObjID = objProviderID;
    popProviderObj.InputObjName = objProviderName;
    popProviderObj.InputObjNo = objProviderNo;
    popProviderObj.InputFlag = objFlag;
    if(duoxuan == true)
    {
        popProviderObj.duoxuan = duoxuan;
    }
    openRotoscopingDiv(true,"divProviderInfo2","frmProviderInfo2");
    document.getElementById('divProviderInfo').style.display='block';
    TurnToPageProvider(1);
}


function fnFillProv()
{
    var ck = document.getElementsByName("ProvCheck");
    var ProviderID = "";
    var ProviderName = "";
    for(var i=0;i<ck.length;++i)
    {
        if ( ck[i].checked )
        {            
            ProviderID += document.getElementById("ProvUCProviderID"+i+"").innerHTML;
            ProviderID += ",";
            ProviderName += document.getElementById("ProvUCProviderName"+i+"").innerHTML;
            ProviderName += ",";            
        }
    }
    if(ProviderID != "")
    {        
        //移除最后一个逗号
        ProviderID = ProviderID.substring(0,ProviderID.length-1);
        ProviderName = ProviderName.substring(0,ProviderName.length-1);
    }
    document.getElementById(popProviderObj.InputObjID).value = ProviderID;
    document.getElementById(popProviderObj.InputObjName).value = ProviderName;
    closeProviderdiv();
}


popProviderObj.Fill = function(ProviderID,ProviderName,ProviderNo)
{
    if(popProviderObj.InputObjID != null&&popProviderObj.InputObjID != '')
        document.getElementById(popProviderObj.InputObjID).value = ProviderID;
    if(popProviderObj.InputObjName != null&&popProviderObj.InputObjName != '')
        document.getElementById(popProviderObj.InputObjName).value = ProviderName;
    if(popProviderObj.InputObjNo != null&&popProviderObj.InputObjNo != '')
        document.getElementById(popProviderObj.InputObjNo).value = ProviderNo;
    closeProviderdiv();
    if(popProviderObj.InputFlag == "Plan")
    {
        fnMergeDetail();
    }
}

function clearProviderdiv()
{
    if(popProviderObj.InputObjID != null&&popProviderObj.InputObjID != '')
        document.getElementById(popProviderObj.InputObjID).value = "";
    if(popProviderObj.InputObjName != null&&popProviderObj.InputObjName != '')
        document.getElementById(popProviderObj.InputObjName).value = "";
    if(popProviderObj.InputObjNo != null&&popProviderObj.InputObjNo != '')
        document.getElementById(popProviderObj.InputObjNo).value = "";
    closeProviderdiv();
    if(popProviderObj.InputFlag == "Plan")
    {
        fnMergeDetail();
    }
}


$(document).ready(function(){

//      TurnToPageProvider(1);
    });    
    var pageCountProvider = 10;//每页计数
    var totalRecordProvider = 0;
    var pagerStyleProvider = "flickr";//jPagerBar样式
    
    var currentPageIndexProvider = 1;
    var actionProvider = "";//操作
    var orderByProvider = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageProvider(pageIndexProvider)
    {

           currentPageIndexProvider = pageIndexProvider;
           var ProviderID= "";
           var ProviderNo= $("#txt_ProviderNo").val();
           var ProviderName= $("#txt_ProviderName").val();
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Common/ProviderInfo.ashx',//目标地址
           cache:false,
           data: "pageIndexProvider="+pageIndexProvider+"&pageCountProvider="+pageCountProvider+"&actionProvider="+actionProvider+"&orderbyProvider="+orderByProvider+"&ProviderID="+escape(ProviderID)+"&ProviderNo="+escape(ProviderNo)+"&ProviderName="+escape(ProviderName)+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerProvider").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Provider tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        var tdChoose = "";
                        if(popProviderObj.duoxuan == true)
                        {
                            //确定按钮显示
                            $("#ProvSure").css("display","inline");
                            //清空显示
                            $("#clearProviderdiv").css("display","inline");
                            tdChoose = "<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ProvCheck\" id=\"ProvCheck"+i+"\" value=\""+item.ID+"\"  />"+"</td>";
                        }
                        else
                        {
                            tdChoose = "<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"popProviderObj.Fill('"+item.ProviderID+"','"+item.ProviderName+"','"+item.ProviderNo+"');\" />"+"</td>";
                        }
                        if(item.ProviderID != null && item.ProviderID != "")
                        $("<tr class='newrow'></tr>").append(tdChoose+
//                        "<td height='22' align='center'><a href=\"StorageProducntAdd.aspx?intProductID="+item.ID+"\">"+ item.ProdNo +"</a></td>"+
                        "<td height='22' id='ProvUCProviderID"+i+"'  align='center' style='display:none'>" + item.ProviderID + "</td>"+
                        "<td height='22' id='ProvUCProviderNo"+i+"' align='center'>" + item.ProviderNo + "</td>"+
                        "<td height='22' id='ProvUCProviderName"+i+"' align='center'>"+item.ProviderName+"</td>").appendTo($("#pageDataList1Provider tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerProvider",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:popProviderObj.pageCount,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPageProvider({pageindex});return false;"}//[attr]
                    );

                  popProviderObj.totalCount = msg.totalCount;
                  totalRecordProvider= msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2Provider").value=msg.totalCount;
                  $("#ShowPageCountProvider").val(pageCountProvider);
                  ShowTotalPage(msg.totalCount,pageCountProvider,pageIndexProvider,$("#pageProvidercount"));
                  $("#ToPageProvider").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function(){hidePopup();    
$("#pageDataList1_PagerProvider").show();IfshowProviderInfoUC(document.getElementById("Text2Provider").value);pageDataList1("pageDataList1Provider","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function Fun_Search_ProviderInfo(aa)
{
    var RetVal=CheckSpecialWords();
    var isErrorFlag;
    var fieldText="";
    var msgText="";
    if(RetVal!="")
    {
        isErrorFlag = true;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(isErrorFlag == true)
    {
        popMsgObj.Show(fieldText,msgText);
        return;
    }
      search="1";
      TurnToPageProvider(1);
}
function IfshowProviderInfoUC(count)
    {
        if(count=="0")
        {
            document.getElementById("divPageProviderInfoUC").style.display = "none";
            document.getElementById("pageProvidercount").style.display = "none";
        }
        else
        {
            document.getElementById("divPageProviderInfoUC").style.display="";
            document.getElementById("pageProvidercount").style.display = "";
        }
    }
    
    function SelectDeptProvider(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexProvider(newPageCountProvider,newPageIndexProvider)
    {
     
       
   
   
var ssss=((totalRecordProvider-1)/newPageCountProvider)+1;
        if(newPageCountProvider <=0 || newPageIndexProvider <= 0 ||  newPageIndexProvider  >ssss  )
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            alert ("转到页数超出查询范围");
            return false;
        }
        else
        {
        ifshow="0";
            this.pageCountProvider=parseInt(newPageCountProvider);
            TurnToPageProvider(parseInt(newPageIndexProvider));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
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
        orderByProvider = orderColum+"_"+ordering;
        TurnToPageProvider(1);
    }
   


function closeProviderdiv() {
    document.getElementById("txt_ProviderNo").value = "";
    document.getElementById("txt_ProviderName").value = "";
    closeRotoscopingDiv(true,"divProviderInfo2");
    document.getElementById("divProviderInfo").style.display="none";
}

function FillProviderInUC()
{
    
}

</script>

