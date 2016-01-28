<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustLinkManSel.ascx.cs" Inherits="UserControl_CustLinkManSel" %>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<style>

	.OfficeThingsListCss
    {
	    position:absolute;top:250px;left:250px;
	    border-width:1pt;border-color:#EEEEEE;border-style:solid;
	    width:800px;
	    display:none;
	    height:220px;
	    z-index:21;
	}
</style>
<script type="text/javascript">
    var lmpageCount = 10;//每页计数
    var lmtotalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var lmaction = "";//操作
    var lmorderBy = "";//排序字段    

 function lmTurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
           var CustNo = document.getElementById("hfCustNo").value;
           var LinkMan =document.getElementById("txtLinkMan").value;
           var Appellation =document.getElementById("txtAppellation").value;
           var Department = document.getElementById("txtDepartment").value;
          
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/LinkManSele.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+lmpageCount+"&action="+lmaction+"&orderby="+lmorderBy+"&LinkMan="+escape(LinkMan)+"&Appellation="+escape(Appellation)+
                    "&Department="+escape(Department)+"&CustNo="+escape(CustNo),//数据
           beforeSend:function(){AddPop();$("#lmpageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#lmpageDataList1 tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")                      
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input onclick=\"GetLinkMan('"+item.id+"','"+item.LinkManName+"')\" id='Checkbox1' value="+item.id+"  type='radio'/>"+"</td>"+                                                                                                       
                        "<td height='22' align='center'>" + item.LinkManName + "</td>"+
                        "<td height='22' align='center'>" + item.Appellation + "</td>"+
                        "<td height='22' align='center'>" + item.Department + "</td>").appendTo($("#lmpageDataList1 tbody"));
                   });
                    //页码
                   ShowPageBar("lmpageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:lmpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"lmTurnToPage({pageindex});return false;"}//[attr]
                    );
                  lmtotalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("txt2").value=msg.totalCount;
                  $("#lmShowPageCount").val(lmpageCount);
                  ShowTotalPage(msg.totalCount,lmpageCount,pageIndex,$("#lmpagecount"));
                  $("#lmToPage").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(lmIfDel=="0"){hidePopup();}$("#lmpageDataList1_Pager").show();lmIfshow(document.getElementById("txt2").value);pageDataList1("lmpageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出客户联系人信息
function SearchLinkMan()
{
    //如果客户未选择时
    var CustNo = document.getElementById("hfCustNo").value;
    if(CustNo == "")
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择客户！");
        return;
    }
    if(!CheckLinkName())
    {
        return;
    }
    lmIfDel = "0";
    lmSearch="1";
    lmTurnToPage(1);
    openRotoscopingDiv(false,"divLinkManName","ifmLinkManName")//弹遮罩层
    document.getElementById("lmHolidaySpan").style.display= "block";
}
    
function lmIfshow(count)
{
    if(count=="0")
    {
        document.getElementById("lmdivpage").style.display = "none";
        document.getElementById("lmpagecount").style.display = "none";
    }
    else
    {
        document.getElementById("lmdivpage").style.display = "block";
        document.getElementById("lmpagecount").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function lmChangePageCountIndex(newPageCount,newPageIndex)
{
if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((lmtotalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        lmIfDel = "0";
        this.lmpageCount=parseInt(newPageCount);
        lmTurnToPage(parseInt(newPageIndex));
    }
}
//排序
function OrderBy(orderColum,orderTip)
{
    if (lmtotalRecord == 0) 
     {
        return;
     }
    lmIfDel = "0";
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
    lmorderBy = orderColum+"_"+ordering;
    lmTurnToPage(1);
}
function GetLinkMan(ID,LinkManName)
{
    document.getElementById("hfLinkmanID").value = ID;  
    document.getElementById("txtUcLinkMan").value = LinkManName;
    document.getElementById('lmHolidaySpan').style.display = "none";
    closeRotoscopingDiv(false,"divLinkManName")//关闭遮罩层
}
//主表单验证
function CheckLinkName()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
     var LinkMan =document.getElementById("txtLinkMan").value;
     var Appellation =document.getElementById("txtAppellation").value;
     var Department = document.getElementById("txtDepartment").value;
      
    if(LinkMan.length>0 && ValidText(LinkMan) == false)
    {
        isFlag = false;       
	    msgText = msgText + "联系人姓名输入不正确|";
    }
    if(Appellation.length>0 && ValidText(Appellation) == false)
    {
        isFlag = false;       
	    msgText = msgText + "称谓输入不正确|";
    }
    if(Department.length>0 && ValidText(Department) == false)
    {
        isFlag = false;       
	    msgText = msgText + "部门输入不正确|";
    }
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}
function DivLinkmanClose()
{
    document.getElementById("txtLinkMan").value = "";
    document.getElementById("txtAppellation").value = "";
    document.getElementById("txtDepartment").value = "";
    
    document.getElementById('lmHolidaySpan').style.display='none';    
    closeRotoscopingDiv(false,"divLinkManName")//关闭遮罩层
}
function LinkManClear()
{
    document.getElementById("hfLinkmanID").value = "";  
    document.getElementById("txtUcLinkMan").value = "";
    document.getElementById('lmHolidaySpan').style.display = "";
    DivLinkmanClose();
}
 </script>

<input onclick="SearchLinkMan();" id="txtUcLinkMan"  type="text" readonly="readonly" class="tdinput" /><!--class="tdinput" -->
<input id="hfLinkmanID" type="hidden" />
<div id="divLinkManName" style="display:none">
<iframe id="ifmLinkManName" frameborder="0" width="100%" ></iframe>
</div>
<div id="lmHolidaySpan" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!-- class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="lmTable1"  cellspacing="0" bgcolor="#CCCCCC">
      <tr bgcolor="#E7E7E7">
      <td  style="width:10%">
      <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivLinkmanClose();" /> 
            <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="LinkManClear();" /> 
             </td>
      </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right"> 联系人姓名</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="txtLinkMan"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align="right">称谓</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="txtAppellation"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td width="10%" bgcolor="#E7E7E7" align="right">
                部门</td>
            <td bgcolor="#FFFFFF" style="width: 24%">
                <input id="txtDepartment"  class="tdinput"  type="text"  style="width:95%"  /></td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchLinkMan()'  /> 
            </td></td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="lmpageDataList1" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('LinkManName','oLinkManName');return false;">联系人姓名<span id="oLinkManName" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Appellation','oAppellation');return false;">称谓<span id="oAppellation" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Department','oDepartment');return false;">部门<span id="oDepartment" class="orderTip"></span></div></th>        
                
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="lmpagecount"></div></td>
            <td height="28"  align="right"><div id="lmpageDataList1_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="lmdivpage">
              <input name="text" type="text" id="txt2" style="display:none" />
              <span id="pageDataList1_Total"></span>每页显示
              <input name="text" type="text" size="3" id="lmShowPageCount"/>
              条  转到第
              <input name="text" size="3" type="text" id="lmToPage"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" width="36" height="28" align="absmiddle" onclick="lmChangePageCountIndex($('#lmShowPageCount').val(),$('#lmToPage').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
</div>