<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SourceNoSel.ascx.cs" Inherits="UserControl_FinanceManager_SourceNoSel" %>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

<script type="text/javascript">
    var pageCountUc1 = 10;//每页计数
    var totalRecord1 = 0;
    var pagerStyle1 = "flickr";//jPagerBar样式
    
    var currentPageIndexUc1 = 1;
    var action1 = "SourceNoSel";//操作
    var orderByUc1 = "";//排序字段    
    var ifdelUc1 = "0";

 function TurnToPageUc1(pageIndex)
    {
           currentPageIndexUc1 = pageIndex;
           //单据编号
           var UcSourceNo =document.getElementById("UctxtSourceNo").value;  
           //往来单位名称         
           var UcContactUnitName =document.getElementById("UctxtContactUnitName").value;
           //单据类型
           var UcContactType = document.getElementById("UcselFeesType").value;
          
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/FinanceManager/FeesList.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc1+"&action="+action1+"&orderbyuc="+orderByUc1+"&UcSourceNo="+escape(UcSourceNo)+"&UcContactUnitName="+escape(UcContactUnitName)+
                    '&UcContactType='+escape(UcContactType),//数据
           beforeSend:function(){AddPop();$("#pageDataListUc_Pager1").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    var index=1;
                    $("#pageDataListUc1 tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input  onclick=\"GetCust1('"+item.ID+"','"+item.BillNo+"','"+item.CustID+"','"+item.CustName+"','"+item.TotalPrice+"','"+index+"')\" id=\"Checkbox"+index+"\"  value="+item.ID+"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center'>" + item.BillNo + "</td>"+
                        "<td height='22' align='center'>" + item.CustName + "</td>"+                       
                        "<td height='22' align='center'>" + item.CreateDate + "</td>"+                       
                        "<td height='22' align='center'>" + item.TotalPrice + "</td>"+
                        "<td height='22' align='center'>"+item.IsFeesAccount+"</td>").appendTo($("#pageDataListUc1 tbody"));
                        index=index+1;
                   });
                    //页码
                   ShowPageBar("pageDataListUc_Pager1",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle1,mark:"pageDataList1MarkUc1",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc1,currentPageIndex:currentPageIndexUc1,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc1({pageindex});return false;"}//[attr]
                    );
                  totalRecord1 = msg.totalCount;
                 // $("#pageDataListUc_Total1").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc21").value=msg.totalCount;
                  $("#ShowPageCountUc1").val(pageCountUc1);
                  ShowTotalPage(msg.totalCount,pageCountUc1,pageIndex,$("#pagecountUc1"));
                  $("#ToPageUc1").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc1=="0"){hidePopup();}$("#pageDataListUc_Pager1").show();IfshowUc(document.getElementById("TextUc21").value);pageDataList1("pageDataListUc1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }

//弹出来源单据列表控件
function SearchBillOpen()
{
    if(document.getElementById("selFeesType").value == "0")
    {
        return;
    }
    else
    {
        //使控件中的单据类型与页面一致
        document.getElementById("UcselFeesType").value = document.getElementById("selFeesType").value;
        SearchBillData();
        
        //源单类型改写时，清空“来源单据编号”“票据总金额”“往来单位ID”
        //document.getElementById("SourceNoSel1_txtUcCustName").value = "";
        document.getElementById("txtUcCustName1").value = "";
        document.getElementById("txtTotalPrice").value = "0";
        document.getElementById("hfCustID").value = "";
    }   
}
    
    
//弹出客户信息
function SearchBillData()
{
    if(!CheckCustName())
    {
        return;
    }
    ifdelUc1 = "0";
    search="1";
            
    TurnToPageUc1(1);  
   openRotoscopingDiv(false,"divCustNameS1","ifmCustNameS1");//弹遮罩层
    document.getElementById("HolidaySpan1").style.display= "block";
}
    
function IfshowUc(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage1").style.display = "none";
        document.getElementById("pagecountUc1").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage1").style.display = "block";
        document.getElementById("pagecountUc1").style.display = "block";
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord1-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc1 = "0";
        this.pageCountUc1=parseInt(newPageCount);
        TurnToPageUc1(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc(orderColum,orderTip)
{
    if (totalRecord1 == 0) 
     {
        return;
     }
    ifdelUc1 = "0";
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
    orderByUc1 = orderColum+"_"+ordering;
    TurnToPageUc1(1);
}

//用来存储选中的往来单位是否相同
var IsSame = true;
//勾选时
function GetCust1(ID,BillNo,CustID,CustName,TotalPrice,index)
{   
    if(document.getElementById("hfCustID").value == "")
    {
        IsSame = true;
        document.getElementById("hfCustID").value = CustID;
        document.getElementById("UserCustName_WLY").value = CustName;
        //源单编号
        //document.getElementById("txtUcCustName1").value = BillNo +","+ document.getElementById("txtUcCustName1").value;
    }
    else
    {
        //如果值相等，则选择的为同一往来单位
        if(document.getElementById("hfCustID").value == CustID)
        {
            IsSame = true;
            document.getElementById("UserCustName_WLY").value = CustName;
            //源单编号
            //document.getElementById("txtUcCustName1").value = BillNo +","+ document.getElementById("txtUcCustName1").value;
           
        }
        else
        {
            IsSame = false;
            alert("只可对同一往来单位选择单据。");
            document.getElementById("Checkbox"+ index).checked = false;
            IsSame = true;
        }
    }
}

//确定
function OK()
{
    if(IsSame == false)
    {
        //弹出提示“只可对同一往来单位”
        document.getElementById("hfCustID").value = "";
        document.getElementById("UserCustName_WLY").value = "";
        document.getElementById("txtUcCustName1").value = "";
        alert("请重新选择");
    }
    else
    {
    
    ///////////////////////////////
        var signFrame = findObj("pageDataListUc1",document);
        for(i=1;i<signFrame.rows.length;i++)
        {   
            var chk= $("#Checkbox"+ i).attr("checked");
            if(chk)
            {
                var BillNo = signFrame.rows[i].cells[1].innerHTML;
                document.getElementById("txtUcCustName1").value = BillNo +","+ document.getElementById("txtUcCustName1").value;
            }
        }
        if(document.getElementById("txtUcCustName1").value =="")
        {
            return false;           
        }
    ///////////////////////////////    
        //源单类型
        document.getElementById("selFeesType").value = document.getElementById("UcselFeesType").value;
        //使页面中的单据类型与控件一致
        document.getElementById("selFeesType").value = document.getElementById("UcselFeesType").value;
    
        switch(document.getElementById("selFeesType").value)
        {
        case "1":
        case "2":
        case "3":
        case "9":
         //显示供应商选择控件，其他控件隐藏
         //......
        document.getElementById("selContactType").value = "1";
        break;
                
        case "4":
        document.getElementById("selContactType").value = "2";
        //选择销售订单时，另取销售费用明细
        GetSellFeeDetail(document.getElementById("txtUcCustName1").value);
        break;
        case "5":
        case "6":
        case "8":
        //显示客户选择控件，其他控件隐藏
        //......
        document.getElementById("selContactType").value = "2";
        break;
        
        case"7":
         //选择费用报销单时，另取费用报销单明细
        GetFYDetail(document.getElementById("txtUcCustName1").value);
        document.getElementById("selContactType").value = "3";
        break;
        
        default:
        document.getElementById("selContactType").value = "3";
        break;
        }
    }

    document.getElementById('HolidaySpan1').style.display = "none";
    closeRotoscopingDiv(false,"divCustNameS1");//关闭遮罩层
}


//主表单验证
function CheckCustName()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    //单据编号
    var UctxtSourceNo = document.getElementById('UctxtSourceNo').value;
    //往来单位名称
    var UcCustNameS = document.getElementById('UctxtContactUnitName').value;
    

    if(UctxtSourceNo.length>0 && UctxtSourceNo.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "单据编号输入不正确|";
    }    
    if(UcCustNameS.length>0 && ValidText(UcCustNameS) == false)
    {
        isFlag = false;       
	    msgText = msgText + "往来单位名称输入不正确|";
    }
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

function DivCustNameClose1()
{
    document.getElementById("UctxtSourceNo").value = "";
    document.getElementById("UctxtContactUnitName").value = "";
    
    closeRotoscopingDiv(false,"divCustNameS1");//关闭遮罩层
    document.getElementById('HolidaySpan1').style.display='none'; 
}

function CustClear11()
{
    document.getElementById("txtUcCustName1").value = "";
    document.getElementById("UctxtContactUnitName").value = "";
    
    DivCustNameClose1();
}
</script>

<input onclick="SearchBillOpen();" id="txtUcCustName1" style="width:95%"  type="text" class="tdinput" readonly/>

<div id="divCustNameS1" style="display:none">
<iframe id="ifmCustNameS1" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan1" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute; top: 20%; left: 60%; margin: 5px 0 0 -400px;display: none;"><!---->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivCustNameClose1();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="CustClear11();" /> 
        <img id="Img1" alt="确定" src="../../../Images/Button/Bottom_btn_Confirm.jpg" style='cursor:hand;' onclick="OK();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right"> 单据编号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="UctxtSourceNo" id="UctxtSourceNo"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align="right">往来单位名称</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="UctxtContactUnitName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td width="10%" bgcolor="#E7E7E7" align="right">
                往来单位类型</td>
            <td bgcolor="#FFFFFF" style="width: 24%">
              <select  style="width: 120px;margin-top:2px;margin-left:2px;" id="UcselFeesType" 
                                    name="D3" >                                    
                                    <option value="1">采购订单</option>
                                    <option value="2">采购到货单</option>
                                    <option value="3">采购退货单</option>
                                    <option value="4">销售订单</option>
                                    <option value="5">销售发货通知单</option>
                                    <option value="6">销售退货单</option>
                                    <option value="7">费用报销单</option>
                                    <option value="8">销售出库单</option>
                                    <option value="9">其他出库单</option>
                                </select></td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchBillData()' /> 
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc1" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('BillNo','oBillNo');return false;">单据编号<span id="oBillNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('CustName','oCustName');return false;">往来单位<span id="oCustName" class="orderTip"></span></div></th>                
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('CreateDate','oCreateDate');return false;">日期<span id="oCreateDate" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('TotalPrice','oTotalPrice');return false;">金额<span id="oTotalPrice" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('IsFeesAccount','oIsFeesAccount');return false;">是否开票<span id="oIsFeesAccount" class="orderTip"></span></div></th>
        
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecountUc1"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_Pager1" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage1">
              <input name="text" type="text" id="TextUc21" style="display:none" />
              <span id="pageDataListUc_Total1"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc1"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc1" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc($('#ShowPageCountUc1').val(),$('#ToPageUc1').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc1"></a>
        </td>
        </tr>
    </table>
</div>
