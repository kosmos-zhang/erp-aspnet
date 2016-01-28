<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageQualityManufacture.ascx.cs"
    Inherits="UserControl_StorageQualityManufacture" %>
<div id="layout">
    <!--提示信息弹出详情start-->
    <a name="pageManDataList1Mark"></a>
    <div id="divManInfo" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 850px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -600px;">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="White" align="left" style="width: 50%;">
                    <img onclick="closeMandiv()" id="Button1" src="../../../Images/Button/Bottom_btn_close.jpg" />
                </td>
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    物品编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProNo" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProductName" class="tdinput" maxlength="50" type="text" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                </td>
                <td width="21%" bgcolor="#FFFFFF">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" bgcolor="#FFFFFF">
                    <input type="hidden" id="hiddConSearchModel" value="all" />
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                        onclick='TurnToPageMan(1)' />&nbsp;
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListMan"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('ProductName','ProductName3');return false;">
                            物品名称 <span id="ProductName3" class="orderTip"></span>
                        </div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('ProdNo','ProdNo33');return false;">
                            物品编号<span id="ProdNo33" class="orderTip"></span></div>
                    </th>
                    <th id="SQMUsedUnitName" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('UsedUnitName','OrderUsedUnitName');return false;">
                            基本单位<span id="OrderUsedUnitName" class="orderTip"></span></div>
                    </th>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('CodeName','CodeName3');return false;">
                            单位<span id="CodeName3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('FromBillNo','FromBillNo3');return false;">
                            源单编号 <span id="FromBillNo3" class="orderTip"></span>
                        </div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('FromLineNo','FromLineNo3');return false;">
                            源单行号 <span id="FromLineNo3" class="orderTip"></span>
                        </div>
                    </th>
                    <th id="SQMUsedUnitCount" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('UsedUnitCount','OrderUsedUnitCount');return false;">
                            基本数量<span id="OrderUsedUnitCount" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('ApplyCheckCount','ApplyCheckCount3');return false;">
                            已报检数量 <span id="ApplyCheckCount3" class="orderTip"></span>
                        </div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('InCount','InCount3');return false;">
                            生产数量 <span id="InCount3" class="orderTip"></span>
                        </div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('EmployeeName','EmployeeName3');return false;">
                            生产负责人 <span id="EmployeeName3" class="orderTip"></span>
                        </div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ManOrderBy('DeptName','DeptName3');return false;">
                            生产部门 <span id="DeptName3" class="orderTip"></span>
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
                                <div id="pagecountMan">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataListMan_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageMan">
                                    <input name="text" type="text" id="TextMan" style="display: none" />
                                    <span id="pageDataListMan_Total"></span>每页显示
                                    <input name="text" type="text" style="width: 30px;" id="ShowPageCountMan" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToPageMan" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeManPageCountIndex($('#ShowPageCountMan').val(),$('#ToPageMan').val());" />
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
//function ManSelectInfo()
//{
//    var strManNo="";
//    var DetailIDList="";
//   
//    var purNo = "";
//    var isSame = true;
//    var mychk=document.getElementsByName('checkTech');
//    for(i=0;i<mychk.length;i++)
//    {    
//        if(mychk[i].checked==true)
//        {    
//            var DetailID = mychk[i].value;
//            DetailIDList += DetailID +','; 
//        }  
//       
//    }
//     $.ajax({
//           type: "POST",//用POST方式传输
//           dataType:"json",//数据格式:JSON
//           url:  '../../../Handler/Office/StorageManager/StorageQualityManufacture.ashx?Action=Man&DetailIDList='+DetailIDList,//目标地址
//           cache:false,
//           beforeSend:function(){AddPop();$("#pageDataList1_PagerMan").hide();},//发送数据之前
//           success: function(msg)
//           {
//                    //数据获取完毕，填充页面据显示
//                    //数据列表
//                    if(msg.PA!=null)
//                    {   
//                        $("#pageDataList1Man tbody").find("tr.newrow").remove();
//                        $.each(msg.PA,function(i,item){
//                            FillManQua(item.ProductID,item.ProductName,item.ProNo,item.CodeName,item.FromBillNo,item.FromLineNo,item.CheckedCount,item.Remark,item.UnitID,item.ProductID,item.ApplyCheckCount)
//                        });
//                     }                  
//                    },
//           error: function() { }, 
//           complete:function(){hidePopup();$("#pageDataList1_PagerMan").show();pageManDataList1("pageDataList1Man","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
//           });
//               
//               //删除明细中所有数据
//    fnDelRow();      
//    closePurdiv();
//}

var popQualityManObj = new Object();
popQualityManObj.isMoreUnit=null;

    var pageCountMan = 10;//每页计数
    var totaManufactureRecord = 0;
    var pagerManStyle = "flickr";//jPagerBar样式    
    var currentManufacturePageIndex = 1;
    var actionMan = "";//操作
    var orderManBy = "";//排序字段
popQualityManObj.ShowProList=function(isUnit)
{
    popQualityManObj.isMoreUnit=isUnit;
    SQMSetHide();
    openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');  
    var myValue=document.getElementById('ddlFromType').value;
    if(myValue=='2')
    {
        TurnToPageMan(currentManufacturePageIndex);
    }
    else
    {
         document.getElementById('txtCustNameNo').disabled=false;   
         document.getElementById('CustBigType').disabled=false;         
         document.getElementById('txtPeople').value='';   
         document.getElementById('txtDep').value='';
    }
}   


// 隐藏列
function SQMSetHide()
{
    if(!popQualityManObj.isMoreUnit)
    {// 隐藏非多计量单位
        $("#SQMUsedUnitName").hide();
        $("#SQMUsedUnitCount").hide();
    }
}

    //jQuery-ajax获取JSON数据
    function TurnToPageMan(pageManIndex)
    {
           var MyProductName1=document.getElementById('myProductName').value;
           var MyProNo1=document.getElementById('myProNo').value;
            if(MyProductName1!='')
            {
                if(strlen(MyProductName1)>0)
                {
                    if(!CheckSpecialWord(MyProductName1))
                    {
                       closeMandiv();
                       popMsgObj.ShowMsg('物品名称不能含有特殊字符!');
                       return false;
                    }
                }
            }
            if(MyProNo1!='')
            {
                if(strlen(MyProNo1)>0)
                {
                    if(!CheckSpecialWord(MyProNo1))
                    {
                       closeMandiv();
                       popMsgObj.ShowMsg('物品编号不能含有特殊字符!');
                       return false;
                    }
                }
            }
           currentPageIndex = pageManIndex;           
           var ManID= "";
           var ManNo="";
           var ManName="";      
            $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:'../../../Handler/Office/StorageManager/StorageQualityManufacture.ashx',//目标地址
           cache:false,
           data: "Method="+0
                    +"&currentPageIndexMan="+pageManIndex
                    +"&pageCountMan="+pageCountMan
                    +"&ProductNo="+escape(MyProNo1)
                    +"&ProductName="+escape(MyProductName1)
                    +"&orderByMan="+orderManBy+"",//数据
           beforeSend:function(){$("#pageDataListMan_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListMan tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ManID != null && item.ManID != "")                      
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" onclick=\"FillManQua('"+item.InCount
                                            +"','"+item.CheckedCount+"','"+item.ProductName+"','"+item.ProdNo
                                            +"','"+item.CodeName+"','"+item.FromBillID+"','"+item.FromLineNo
                                            +"','"+item.ApplyCheckCount+"','"+item.Principal+"','"+item.EmployeeName
                                            +"','"+item.DeptID+"','"+item.DeptName+"','"+item.UnitID
                                            +"','"+item.ProductID+"','uc','','"+item.FromBillNo
                                            +"','"+item.ManCheckCount+"','"+item.UsedUnitID+"','"+item.UsedUnitCount+"')\"  name=\"checkTech\" id=\"checkTech_"+i+1+"\" value=\""+item.ManID+"\"  />"+"</td>"+
                        "<td height='22' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' align='center'>" + item.ProdNo + "</td>"+
                        (popQualityManObj.isMoreUnit?(
                        "<td height='22' align='center'>" + item.CodeName + "</td>"+
                        "<td height='22' align='center'>" + item.UsedUnitName + "</td>"+
                        "<td height='22' align='center'>" + item.FromBillNo + "</td>"+
                        "<td height='22' align='center'>" + item.FromLineNo + "</td>"+
                        "<td height='22' align='center'>" +item.ApplyCheckCount+ "</td>"+
                        "<td height='22' align='center'>" +item.UsedUnitCount+ "</td>"
                        ):(
                        "<td height='22' align='center'>" + item.CodeName + "</td>"+
                        "<td height='22' align='center'>" + item.FromBillNo + "</td>"+
                        "<td height='22' align='center'>" + item.FromLineNo + "</td>"+
                        "<td height='22' align='center'>" +item.ApplyCheckCount+ "</td>"))+
                        "<td height='22' align='center'>" +item.InCount + "</td>"+
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>"+
                        "<td height='22' align='center'>"+item.DeptName+"</td>").appendTo($("#pageDataListMan tbody"));
                        
                   });  
                    //页码
                         ShowPageBar("pageDataListMan_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                        "<%= Request.Url.AbsolutePath %>",//[url]
                        {style:pagerManStyle,mark:"pageManDataListMark",
                        totalCount:msg.totalCount,
                        showPageNumber:3,
                        pageCount:pageCountMan,
                        currentPageIndex:pageManIndex,
                        noRecordTip:"没有符合条件的记录",
                        preWord:"上页",
                        nextWord:"下页",
                        First:"首页",
                        End:"末页",                   
                        onclick:"TurnToPageMan({pageindex});return false;"}//[attr]
                        );                    
                      totalRecord = msg.totalCount;
                      $("#ShowPageCountMan").val(pageCountMan);  
                      ShowTotalPage(msg.totalCount,pageCountMan,pageManIndex);               
                      $("#ToPageMan").val(pageManIndex);
                      ShowTotalPage(msg.totalCount,pageCountMan,currentPageIndex,$("#pagecountMan"));
                      },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataListMan_Pager").show();IfshowMan(document.getElementById('TextMan').value);pageManDataList1("pageDataListMan","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
         document.getElementById('divManInfo').style.display='block'; 
    }
  
     //table行颜色
function pageManDataList1(o,a,b,c,d)
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

function IfshowMan(count)
    {
        if(count=="0")
        {
            document.getElementById('divpageMan').style.display = "none";
            document.getElementById('divpageMan').style.display = "none";
        }
        else
        {
            document.getElementById('divpageMan').style.display = "block";
            document.getElementById('divpageMan').style.display = "block";
        }
    }
//改变每页记录数及跳至页数
function ChangeManPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageCountMan=parseInt(newPageCount);
        TurnToPageMan(newPageIndex);
    }
}
//排序
function ManOrderBy(orderColum,orderTip)
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
     orderManBy = orderColum+"_"+ordering;
    TurnToPageMan(1);
}
   
function closeMandiv()
{
    document.getElementById("divManInfo").style.display="none";
    document.getElementById('myProductName').value='';
    document.getElementById('myProNo').value='';
    closeRotoscopingDiv(false,'divBackShadow');
}

</script>

