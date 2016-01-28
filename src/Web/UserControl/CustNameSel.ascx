<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustNameSel.ascx.cs" Inherits="UserControl_CustNameSel" %>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<style>

	.OfficeThingsListCss
    {
	    <%--position:absolute;top:250px;left:250px;
	    border-width:1pt;border-color:#EEEEEE;border-style:solid;
	    width:800px;
	    display:none;
	    height:220px;
	    z-index:21;--%>
	}
</style>
<script type="text/javascript">
    var pageCountUc = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc = 1;
    var action = "";//操作
    var orderByUc = "";//排序字段    
    var ifdelUc = "0";

 function TurnToPageUc(pageIndex)
    {
           currentPageIndexUc = pageIndex;
           var CustNo =document.getElementById("txtUcCustNo").value;           
           var CustName =document.getElementById("txtUcCustNameS").value;
           var CustShort = document.getElementById("txtUcCustShort").value;
          
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustName.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc+"&action="+action+"&orderbyuc="+orderByUc+"&UcCustNo="+escape(CustNo)+"&UcCustName="+escape(CustName)+
                    '&UcCustShort='+escape(CustShort),//数据
           beforeSend:function(){AddPop();$("#pageDataListUc_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")                      
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input  onclick=\"GetCust('"+item.id+"','"+item.CustNo+"','"+item.CustName+"')\" id='Checkbox1' value="+item.ID+"  type='radio'/>"+"</td>"+
                                                                                                       // onclick=\"DeferMeeting('"+item.ID+"'"+",'"+item.MeetingNo+"'"+",'"+item.Title+"'"+",'"+item.StartDate+"'"+",'"+item.MeetingStatus+"');
                        "<td height='22' align='center'>" + item.CustNo + "</td>"+
                        "<td height='22' align='center'>" + item.CustName + "</td>"+                       
                        "<td height='22' align='center'>" + item.CustShort + "</td>"+                       
                        "<td height='22' align='center'>" + item.TypeName + "</td>"+                        
                        "<td height='22' align='center'>"+item.Tel+"</td>").appendTo($("#pageDataListUc tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataListUc_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc,currentPageIndex:currentPageIndexUc,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataListUc_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowPageCountUc").val(pageCountUc);
                  ShowTotalPage(msg.totalCount,pageCountUc,pageIndex,$("#pagecountUc"));
                  $("#ToPageUc").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_Pager").show();IfshowUc(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出客户信息
function SearchCustData()
{
    if(!CheckCustName())
    {
        return;
    }
    ifdelUc = "0";
    search="1";
        
    TurnToPageUc(1);  
   openRotoscopingDiv(false,"divCustNameS","ifmCustNameS");//弹遮罩层
    document.getElementById("HolidaySpan").style.display= "block";
}
    
function IfshowUc(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pagecountUc").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pagecountUc").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc(newPageCount,newPageIndex)
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc = "0";
        this.pageCountUc=parseInt(newPageCount);
        TurnToPageUc(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc(orderColum,orderTip)
{
    if (totalRecord == 0) 
     {
        return;
     }
    ifdelUc = "0";
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
    orderByUc = orderColum+"_"+ordering;
    TurnToPageUc(1);
}
function GetCust(CustID,CustNo,CustName)
{
    try
    {
        LinkManClear();//每次选择客户时，使客户联系人为空
    }
    catch(err)
    {
        
    }    

    if(document.getElementById('selllorderL')!=undefined)  //add by  linfei  销售报表-客户应收款查询
    {
        if(document.getElementById('selllorderL').value=='1')
        {
            document.getElementById('txtUcCustName').value=CustName;
            document.getElementById('txtUcCustID').value=CustID;
        }
        if(document.getElementById('selllorderL').value=='2')
        {
            document.getElementById('txtUcCustName').value=CustNo;
            document.getElementById('CustName').value=CustName;
            document.getElementById('hiddenCustIDL').value=CustID;
        }
        if(document.getElementById('selllorderL').value=='3')
        {
            document.getElementById('hiddenCustIDL').value=CustID;
            document.getElementById("hfCustID").value = CustID;              
            document.getElementById("hfCustNo").value = CustNo;
            document.getElementById("txtUcCustName").value = CustName;
        }
        else
        {
        document.getElementById('txtUcCustName').value=CustNo;
        
        }
    }
    else
    {
        document.getElementById("hfCustID").value = CustID;        
        document.getElementById("CustNameSel1_hfCustID_Ser").value = CustID;   
        document.getElementById("hfCustNo").value = CustNo;
        document.getElementById("txtUcCustName").value = CustName;
        try
        {
            LoadBomInfo(CustID,CustNo,CustName);
        }
        catch(e)
        {}
    }
    document.getElementById('HolidaySpan').style.display = "none";
    closeRotoscopingDiv(false,"divCustNameS");//关闭遮罩层
//    try
//    {
//        GetSomeLinkInfoByID(CustID);
//    }
//    catch(err)
//    {
//        
//    }
}
//主表单验证
function CheckCustName()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var txtUcCustNo = document.getElementById('txtUcCustNo').value;//客户编号
    var txtUcCustName = document.getElementById('txtUcCustNameS').value;//客户名称
    

    if(txtUcCustNo.length>0 && txtUcCustNo.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "客户编号输入不正确|";
    }    
    if(txtUcCustName.length>0 && ValidText(txtUcCustName) == false)
    {
        isFlag = false;       
	    msgText = msgText + "客户简称输入不正确|";
    }
    
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

function DivCustNameClose()
{
    document.getElementById("txtUcCustNo").value = "";
    document.getElementById("txtUcCustNameS").value = "";
    document.getElementById("txtUcCustShort").value = "";
    closeRotoscopingDiv(false,"divCustNameS");//关闭遮罩层
    document.getElementById('HolidaySpan').style.display='none'; 
}

function CustClear()
{
    document.getElementById("hfCustID").value = "";
    document.getElementById("CustNameSel1_hfCustID_Ser").value = "";  
    document.getElementById("hfCustNo").value = "";
    document.getElementById("txtUcCustName").value = "";
    if(document.getElementById('selllorderL')!=undefined)  //add by  linfei  销售报表-客户应收款查询
    {
        if(document.getElementById('selllorderL').value=='1')
        {
            document.getElementById('txtUcCustName').value='';
            document.getElementById('txtUcCustID').value='0';
        }
        if(document.getElementById('selllorderL').value=='2')
        {
            document.getElementById('txtUcCustName').value='';
            document.getElementById('CustName').value='';
            document.getElementById('hiddenCustIDL').value='0';
        }
    }
    try
    {
        document.getElementById("hidCustID_Tree").value = "";//客户ID
        document.getElementById("hidCustNo_Tree").value = "";//客户No
    }
    catch(e)
    {}
    DivCustNameClose();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
    LinkManClear();
}

 </script>

<input onclick="SearchCustData();" id="txtUcCustName" style="width:95%"  type="text" class="tdinput" readonly/>

<div id="divCustNameS" style="display:none">
<iframe id="ifmCustNameS" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivCustNameClose();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="CustClear();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right"> 客户编号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="txtUcCustNo" id="txtUcCustNo"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align="right">客户名称</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="txtUcCustNameS"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td width="10%" bgcolor="#E7E7E7" align="right">
                拼音缩写</td>
            <td bgcolor="#FFFFFF" style="width: 24%">
                <input name="txtUcCustShort" id="txtUcCustShort"  class="tdinput"  type="text"  style="width:95%"  /></td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchCustData()' /> 
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('CustNo','oCustNo');return false;">客户编号<span id="oCustNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('CustName','oCustName');return false;">客户名称<span id="oCustName" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('CustShort','oCustShort');return false;">拼音缩写<span id="oCustShort" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('TypeName','oTypeName');return false;">所在区域<span id="oTypeName" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('Tel','oTel');return false;">联系电话<span id="oTel" class="orderTip"></span></div></th>
        
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecountUc"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc($('#ShowPageCountUc').val(),$('#ToPageUc').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfCustID" type="hidden"  />
<input id="hfCustID_Ser" type="hidden" runat="server" />
<input id="hfCustNo" type="hidden"  /></td>
        </tr>
    </table>
</div>
