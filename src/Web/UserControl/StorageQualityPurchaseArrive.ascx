<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageQualityPurchaseArrive.ascx.cs"
    Inherits="UserControl_StorageQualityPurchaseArrive" %>
<div id="PurLayout">
    <!--提示信息弹出详情start-->
    <a name="pagePurDataList1Mark"></a>
    <div id="divPurInfo" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 800px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -550px;">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="White" align="left" style="width: 50%;">
                    <img onclick="closePurdiv()" id="Button2" src="../../../Images/Button/Bottom_btn_close.jpg" />
                    <img onclick="PurSelectInfo()" id="Button1" src="../../../Images/Button/Bottom_btn_ok.jpg" />
                </td>
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    物品编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProNo12" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProductName12" class="tdinput" maxlength="50" type="text" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                </td>
                <td width="21%" bgcolor="#FFFFFF">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" bgcolor="#FFFFFF">
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                        onclick='TurnToPagePro(1)' />&nbsp;
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pagePurDataList1"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('ProductNo','OrderProductNo');return false;">
                            物品编号<span id="OrderProductNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('ProductName','OrderProductName');return false;">
                            物品名称<span id="OrderProductName" class="orderTip"></span></div>
                    </th>
                    <th id="SQPUsedUnitName" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('UsedUnitName','OrderUsedUnitName');return false;">
                            基本单位<span id="pc21" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('CodeName','OrderCodeName');return false;">
                            单位<span id="OrderCodeName" class="orderTip"></span></div>
                    </th>
                    <th id="SQPUsedUnitCount" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('UsedUnitCount','OrderUsedUnitCount');return false;">
                            基本数量<span id="pc22" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('ApplyCheckCount','OrderApplyCheckCount');return false;">
                            已报检数量<span id="OrderApplyCheckCount" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('ProductCount','OrderProductCount');return false;">
                            到货数量<span id="OrderProductCount" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('FromBillNo','OrderFromBillNo');return false;">
                            源单编号<span id="OrderFromBillNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('FromLineNo','OrderFromLineNo');return false;">
                            源单行号<span id="OrderFromLineNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('CheckedCount','OrderCheckedCount');return false;">
                            已检数量<span id="OrderCheckedCount" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('FromTypeName','OrderFromTypeName');return false;">
                            源单类型<span id="OrderFromTypeName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="PurOrderBy('Remark','OrderRemark');return false;">
                            备注<span id="OrderRemark" class="orderTip"></span></div>
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
                                <div id="pagecountPur2">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerPur" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divQualityPurCheckPage">
                                    <span id="pageDataList1_TotalPur"></span>每页显示
                                    <input name="text" type="text" id="TextPur" style="display: none" />
                                    <input name="text" type="text" style="width: 30px;" id="ShowPageCountPur" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToPagePur" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePurPageCountIndex($('#ShowPageCountPur').val(),$('#ToPagePur').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">

function PurSelectInfo()
{
    var strPurNo="";
    var DetailIDList="";
    var PurFrame = findObj("pagePurDataList1",document);
    
    var purNo = "";
    var isSame = true;

    for(i=1;i<PurFrame.rows.length;i++)
    {    
      
        var chk= $("#CheckboxPur"+ i).attr("checked");
        if(chk)
        {       

            var DetailID =  PurFrame.rows[i].cells[1].innerHTML;
            DetailIDList += DetailID +','; 
        }       
    }
    if(DetailIDList=='')
    {
     alert('请先选择一项！');
      return false;
    }
     $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageQualityPur.ashx?Action=Pur&DetailIDList='+DetailIDList,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_PagerPur").hide();},//发送数据之前
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                     var myPurCheckedCount='0';//报检数量合计
                    if(msg.PA!=null)
                    {   
                        myPurCheckedCount=document.getElementById('CountTotal1');  
                        $.each(msg.PA,function(i,item){
                            AddPurNewRow(item.PurID,item.ProductName,item.ProductNo,item.CodeName
                            ,item.QuaCheckedCount,item.FromBillID,item.FromLineNo,item.CheckedCount
                            ,item.UnitID,item.ProID,item.ProductCount,item.ApplyCheckCount,item.FromBillNo
                            ,item.ProductID,item.UsedUnitID,item.UsedUnitCount);
                            myPurCheckedCount.value=parseFloat(parseFloat(myPurCheckedCount.value)+parseFloat(item.QuaCheckedCount)).toFixed(2);
                        });
                     } 
                                    
                    },
           error: function() { }, 
           complete:function(){hidePopup();$("#pageDataList1_PagerPur").show();pagePurDataList1("pagePurDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });               
               //删除明细中所有数据  
    closePurdiv();
}

var QualityPurObj = new Object();
QualityPurObj.isMoreUnit=null;
QualityPurObj.ShowPurList=function(isUnit)
{    
    QualityPurObj.isMoreUnit=isUnit;
    SQPSetHide();
    openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    TurnToPagePro(currentPurPageIndex);   
}    

// 隐藏列
function SQPSetHide()
{
    if(!QualityPurObj.isMoreUnit)
    {// 隐藏非多计量单位
        $("#SQPUsedUnitName").hide();
        $("#SQPUsedUnitCount").hide();
    }
}


    var pagePurCount2 = 10;//每页计数
    var totalPurRecord = 0;
    var pagerPurStyle = "flickr";//jPagerBar样式
   
    var currentPurPageIndex = 1;
    var actionPur = "";//操作
    var orderPurBy = "";//排序字段
    
    //jQuery-ajax获取JSON数据
    function TurnToPagePro(pagePurIndex2)
    {
            
           currentPageIndex = pagePurIndex2;     
            var FromBillType1=document.getElementById('ddlFromType').value;
            var myProNo1=document.getElementById('myProNo12').value;
            var myProductName1=document.getElementById('myProductName12').value;
            if(myProductName1!='')
            {
                if(strlen(myProductName1)>0)
                {
                    if(!CheckSpecialWord(myProductName1))
                    {
                       closePurdiv();
                       popMsgObj.ShowMsg('物品名称不能含有特殊字符!');
                       return false;
                    }
                }
            }
            if(myProNo1!='')
            {
                if(strlen(myProNo1)>0)
                {
                    if(!CheckSpecialWord(myProNo1))
                    {
                       closePurdiv();
                       popMsgObj.ShowMsg('物品编号只能包含字母或数字!');
                       return false;
                    }
                }
            }
          
            $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:'../../../Handler/Office/StorageManager/StorageQualityPur.ashx',//目标地址
           cache:false,
           data: "FromType="+FromBillType1
                +"&currentPageIndexPur="+currentPageIndex
                +"&myProNo1="+escape(myProNo1)
                +"&myProductName1="+escape(myProductName1)
                +"&pagecountPur="+pagePurCount2
                +"&orderByPur="+orderPurBy+"",//数据
           beforeSend:function(){$("#pageDataList1_PagerPur").hide();},//发送数据之前
           
           success: function(msg){          
           
                    //数据获取完毕，填充页面据显示
                    //数据列表
                     var index=1;
                    $("#pagePurDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.PurID != null && item.PurID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"Checkbox\" name=\"CheckboxPur"+index+"\" id=\"CheckboxPur"+index+"\" value=\""+item.PurID+"\"  />"+"</td>"+
                         "<td height='22' align='center' style=\"display:none\">" + item.PurID + "</td>"+
                        "<td height='22' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' align='center'>" + item.ProductName + "</td>"+
                        (QualityPurObj.isMoreUnit?(
                        "<td height='22' align='center'>" + item.CodeName + "</td>"+
                        "<td height='22' align='center'>" + item.UsedUnitName + "</td>"+
                        "<td height='22' align='center'>" + FormatAfterDotNumber(item.ApplyCheckCount,selPoint) + "</td>"+
                        "<td height='22' align='center'>" + FormatAfterDotNumber(item.UsedUnitCount,selPoint) + "</td>"
                        ):(
                        "<td height='22' align='center'>" + item.CodeName + "</td>"+
                        "<td height='22' align='center'>" + FormatAfterDotNumber(item.ApplyCheckCount,selPoint) + "</td>"))+
                        "<td height='22' align='center'>" + FormatAfterDotNumber(item.ProductCount,selPoint) + "</td>"+
                        "<td height='22' align='center'>" + item.FromBillNo + "</td>"+
                        "<td height='22' align='center'>" + item.FromLineNo + "</td>"+
                       "<td height='22' align='center'>" + FormatAfterDotNumber(item.CheckedCount,selPoint) + "</td>"+
                       "<td height='22' align='center'>" + item.FromType + "</td>"+
                        "<td height='22' align='center'>"+item.Remark+"</td>").appendTo($("#pagePurDataList1 tbody"));
                     index=index+1;
                   });          
                   //页码
                    ShowPageBar("pageDataList1_PagerPur",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerPurStyle,mark:"pagePurDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagePurCount2,
                    currentPageIndex:pagePurIndex2,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPagePro({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;                 
                      $("#ShowPageCountPur").val(pagePurCount2);
                      ShowTotalPage(msg.totalCount,pagePurCount2,pagePurIndex2);
                      $("#ToPagePur").val(pagePurIndex2);
                      ShowTotalPage(msg.totalCount,pagePurCount2,currentPageIndex,$("#pagecountPur2"));   
                      
                        
                      },
           error: function() {}, 
           complete:function(){ hidePopup();$("#pageDataList1_PagerPur").show();IfshowProduct2(document.getElementById('TextPur').value);pagePurDataList1("pagePurDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
           document.getElementById('divPurInfo').style.display='block'; 
         
    }
  
     //table行颜色
function pagePurDataList1(o,a,b,c,d)
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

function IfshowProduct2(count)
    {

        if(count=="0")
        {
            document.getElementById("divQualityPurCheckPage").style.display = "none";
            document.getElementById("pagecountPur2").style.display = "none";
        }
        else
        {
            document.getElementById("divQualityPurCheckPage").style.display = "block";
            document.getElementById("pagecountPur2").style.display = "block";
        }

    }
//改变每页记录数及跳至页数
function ChangePurPageCountIndex(newPageCount,newPageIndex)
{

     if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
          return;
       }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord -1)/newPageCount)+1 )
        {

           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
           return false;
        }
        else
        {
            this.pagePurCount2=parseInt(newPageCount);
            TurnToPagePro(parseInt(newPageIndex));
        }
}
//排序
function PurOrderBy(orderColum,orderTip)
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
    orderPurBy = orderColum+"_"+ordering;
    TurnToPagePro(1);
}
   
function closePurdiv()
{
    document.getElementById("divPurInfo").style.display="none";
    document.getElementById('myProNo12').value='';
    document.getElementById('myProductName12').value='';
    closeRotoscopingDiv(false,'divBackShadow');
}

</script>

