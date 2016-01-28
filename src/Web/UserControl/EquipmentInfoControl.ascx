<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EquipmentInfoControl.ascx.cs" Inherits="UserControl_EquipmentInfoControl" %>
<div id="divPopBomShadow" style="display: none">
    <iframe id="PopBomShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<style>
.AlarmStype
{
	filter:progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
	position:absolute;top:250px;left:209px;
	border-width:1pt;border-color:#666666;border-style:solid;
	width:250px;
	height:120px;
	display:none;
}
.userListCss
{
    position: absolute;
    top: 35%;
    left:60%;
    border-width: 1pt;
    border-color: #EEEEEE;
    border-style: solid;
    width: 200px;
    display: none;
    height: 220px;
    z-index: 100;
}
</style>
<script type="text/javascript">
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    var ifshow="0";
    var flag="";
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex,flag)
    {
           currentPageIndex = pageIndex;
           var equipno=$("#equipno").val();//设备编号
           var equipname=$("#equipname").val();//设备名称
           var equipsup="";//供应商
           
           var equipindex="";//设备序列号
           var equiptype=$("#txtEquipType").val();//设备分类
           var equipbuydate=$("#txtbuydate").val();//购入日期
           var equipcurruser="";//当前使用人
           var equipcurrdept="";//当前部门
           var equipstatus=$("#txtEquipstatus").val();//设备状态
           var action="getinfobyflag";
           var fieldText = "";
           var msgText = "";
           var RetVal=CheckSpecialWords();
           if(RetVal!="")
           {
                fieldText = fieldText + RetVal+"|";
   		        msgText = msgText +RetVal+  "不能含有特殊字符|";
   		        popMsgObj.Show(fieldText,msgText);
   		        return;
           }
           var para="pageIndex="+pageIndex+"&pageCount="+pageCount+
                  "&equipindex="+equipindex+"&equipstatus="+equipstatus+
                  "&equipcurrdept="+equipcurrdept+"&equiptype="+equiptype+
                  "&equipbuydate="+equipbuydate+"&equipcurruser="+equipcurruser+
                  "&action="+action+"&orderby="+orderBy+"&equipno="+escape(equipno)+
                  "&equipname="+escape(equipname)+"&equipsup="+escape(equipsup)+"&flag="+escape(flag)+""
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/AdminManager/EquipmentInfo.ashx',//目标地址
           cache:false,
           data: para,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                        var date=item.BuyDate.replace(" 00:00:00","").replace(" 0:00:00","");
                                if(date=="1900-01-01" || date=="1900-1-1")date="";
                     
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input onclick=\"GetEquipmentNo('"+item.EquipmentNo+"')\" id='Checkbox1' value="+item.id+"  type='radio'/>"+"</td>"+
                            "<td height='22' align='center'>"+ item.EquipmentNo +"</td>"+
                            "<td height='22' align='center'>"+item.EquipmentName+"</td>"+
                            "<td height='22' align='center'>"+item.CodeName+"</td>"+
                            "<td height='22' align='center'>"+date+"</td>"+
                            "<td height='22' align='center'>"+item.FittingFlag+"</td>"+
                            "<td height='22' align='center'>"+item.Status+"</td>").appendTo($("#pageDataList1 tbody"));
                        }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex},flag);return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
},
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });


    }
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
function GetEquipmentNo(EquipmentNo)
{
    document.getElementById("Txt_EquipmentInfo").value = EquipmentNo;
    document.getElementById('EquipmentInfoSpan').style.display = "none";
    closeRotoscopingDiv(false,'divPopBomShadow');
}
    function SearchEquipData(aa)
    {
      search="1";
      ifshow="0";
      TurnToPage(1,flag);
    }
    function ClearInput()
    {
        $(":text").each(function(){ 
        this.value=""; 
        }); 
    }
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        var r = /^\+?[1-9][0-9]*$/;　　//正整数 
        if(!r.test(newPageCount))
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
            return;
        } 
        if(!r.test(newPageIndex))
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
            return;
        } 
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1)
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转至页数超出查询范围！");
        }
        else
        {
             ifshow="0";
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex,flag));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
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
        orderBy = orderColum+"_"+ordering;
        TurnToPage(1,flag);
    }
function CloseEquipDiv()
{
    document.getElementById("EquipmentInfoSpan").style.display= "none";
    closeRotoscopingDiv(false,'divPopBomShadow');
}
//弹出设备信息框
function OpenEquipmentDiv(Param)
{
    flag=Param;
        openRotoscopingDiv(false,'divPopBomShadow','PopBomShadowIframe');

    TurnToPage(1,flag);
    document.getElementById("EquipmentInfoSpan").style.display= "block";
}
function SelectedNodeChanged(code_text,type_code,typeflag)
{   
   document.getElementById("TypeIDHidden").value=code_text;
   document.getElementById("txtEquipType").value=type_code;
   hideUserList();
}
function hidetxtUserList()
{
   hideUserList();
}
function showUserList()
{
   document.getElementById("userList").style.display = "block";
}
function hideUserList()
{
 document.getElementById("userList").style.display = "none";
}
 </script>
 <!--class="tdinput" -->
<input id="hfLinkmanID" type="hidden" />
<div id="EquipmentInfoSpan" style="border: solid 1px #999999; background: #fff;
        width: 800px; z-index: 20; position: absolute; display: none;
        top: 20%; left: 50%; margin: 5px 0 0 -400px"><!--class="OfficeThingsListCss"-->
<table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
  <tr>
    <td height="20" align="left" valign="center" class="Title">&nbsp;&nbsp;&nbsp;&nbsp;选择设备
    </td>
    <td  align="right" valign="center" >
     <img src="../../../Images/Pic/Close.gif" title="关闭" style="CURSOR: pointer"  onclick="CloseEquipDiv()"/>&nbsp;&nbsp;&nbsp;
    </td>
      
  </tr>
  <tr>
    <td colspan="2"  >
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align=right> 设备编号</td>
            <td bgcolor="#FFFFFF"><input name="equipno" id="equipno"  type="text" SpecialWorkCheck="设备编号" class="tdinput" size = "10" /></td>
            <td bgcolor="#E7E7E7" align=right>设备名称</td>
            <td bgcolor="#FFFFFF"><input name="equipname" id="equipname" SpecialWorkCheck="设备名称"  type="text" class="tdinput" size = "10" /></td>
            <td bgcolor="#E7E7E7" align=right>设备分类</td>
            <td bgcolor="#FFFFFF"><%--<asp:DropDownList id="EquipType" class="tdinput" runat="server">
                </asp:DropDownList>--%>
                <input id="TypeIDHidden" type="text" readonly class="tdinput" size="15" onclick="showUserList()" />
                            <input id="txtEquipType" type="hidden" class="tdinput" />
                                    </td>
          </tr>   
          <tr class="table-item">
            <td bgcolor="#E7E7E7" width="10%" height="20"   align=right>购入日期</td>
            <td bgcolor="#FFFFFF"><input name="txtbuydate" id="txtbuydate" class="tdinput" readonly type="text" size="15" onclick="J.calendar.get()" /></td>
            <td width="10%" height="20" bgcolor="#E7E7E7" align=right>设备状态</td>
            <td bgcolor="#FFFFFF" ><select name="txtEquipstatus" class="tdinput" id="txtEquipstatus"  width="119px">
              <option value="">--请选择--</option>
              <option value="0">空闲</option>
              <option value="1">使用</option>
              <option value="3">维修</option>
              <option value="5">报废</option>
          </select>
          </td>
          <td colspan="2" align="center" bgcolor="#FFFFFF">
           <img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:pointer;' onclick='SearchEquipData()'/>
           </td>
          </tr>       
        </table></td>
      </tr>
    </table>
    </td>
  </tr>
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('EquipmentNo','oGroup');return false;">设备编号<span id="oGroup" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('EquipmentName','oC2');return false;">设备名称<span id="oC2" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CodeName','oC3');return false;">设备分类<span id="oC3" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('BuyDate','oC5');return false;">购入日期<span id="oC5" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('FittingFlag','oC8');return false;">配件<span id="oC8" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Status','oC9');return false;">设备状态<span id="oC9" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecount"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage">
              <input name="text" type="text" id="Text2" style="display:none" />
              <span id="pageDataList1_Total"></span>每页显示
              <input name="text" type="text" id="ShowPageCount"/>
              条  转到第
              <input name="text" type="text" id="ToPage"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    </td>
  </tr>
</table>
</div>
<div id="userList" class="userListCss" >
        <table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" bgcolor="#999999">
            <tr>
                <td height="20" bgcolor="#E6E6E6" class="Blue">
                    <table width="100%" border="0" cellspacing="0" cellpadding="">
                        <tr>
                            <td width="70%">
                                设备分类选择
                                <a href="#" style="cursor: pointer" onclick="document.getElementById('userList').style.display='none';document.getElementById('TypeIDHidden').value='';document.getElementById('txtEquipType').value=''">清空选择</a>
                            </td>
                            <td align="right">
                                <img src="../../../Images/Pic/Close.gif" title="关闭" style="cursor: pointer" onclick="document.getElementById('userList').style.display='none';"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td >
                    <div style="height:200px; overflow-x: hidden; overflow-y: scroll">
                                            <table  width="100%">
                                            <tr><td bgcolor="#F4F0ED" height="200" valign="top" width="100%">
                                            <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
                    </asp:TreeView>
                                            </td></tr>                                                
                                            </table>
                                        </div>
                    
                </td>
            </tr>
        </table>
    </div>