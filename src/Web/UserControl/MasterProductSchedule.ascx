<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MasterProductSchedule.ascx.cs"
    Inherits="UserControl_MasterProductSchedule" %>
<div id="divPopMasterShadow" style="display: none">
    <iframe id="PopMasterShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divMaster" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 600px; z-index: 1000; position: absolute; display: none; top: 30%; left: 70%;
        margin: 5px 0 0 -400px;">
        <table width="99%">
            <tr>
                <td align="left">
                <span>
                <img id="btnClear" alt="清空" src="../../../images/Button/Bottom_btn_del.jpg" style='cursor: hand;display:none;float:left;' onclick="ClearMasterControl();"  />
                    <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;float:left;' onclick="document.getElementById('divMaster').style.display='none';closeRotoscopingDiv(false,'divPopMasterShadow');"   />
                    </span>
                    <span id="tdMode" style="display: none" > <img  id="OptionCheckArrayBtn" name="OptionCheckArrayBtn"alt="全选" src="../../../images/Button/Bottom_btn_allchoose.jpg" style='cursor: hand;' onclick="OptionCheckAlls();"   />
                     <img id="btnSure" alt="确认" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;' onclick="Fun_Option_MasterContent();"  /></span>
                </td>

            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListMaster"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            单据编号<span id="oID" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            主题<span id="oC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            负责人<span id="oC2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            部门<span id="oC3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            数量合计<span id="oC4" class="orderTip"></span></div>
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
                                <div id="pagecountMaster">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataListMaster_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageMaster">
                                    <input name="text" type="text" id="TextMaster" style="display: none" />
                                    <span id="pageDataListMaster_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountMaster" style="width: 25px" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageMaster" style="width: 25px" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexMaster($('#ShowPageCountMaster').val(),$('#ToPageMaster').val());" />
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
    var popMasterObj=new Object();
    popMasterObj.InputObj = null;
    popMasterObj.IsMore = false;

  
    var pageCountMaster = 10;//每页计数
    var totalRecordMaster = 0;
    var pagerStyleMaster = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var actionMaster = "";//操作
    var orderByMaster = "ModifiedDate_d";//排序字段

    popMasterObj.ShowList = function(objInput, isMore,isList) {
        popMasterObj.InputObj = objInput;
        popMasterObj.IsMore = isMore;
        if (!isMore && isList) {
            document.getElementById('btnClear').style.display = '';
        }
        OptionCheckArray = new Array();
        document.getElementById('tdMode').style.display = isMore == true ? '' : 'none';
        openRotoscopingDiv(false, 'divPopMasterShadow', 'PopMasterShadowIframe');
        document.getElementById('divMaster').style.display = 'block';
        TurnToPageMaster(currentPageIndex);
    }

    //jQuery-ajax获取JSON数据
    function TurnToPageMaster(pageIndexMaster)
    {
           currentPageIndex = pageIndexMaster;
           var PlanNo= "";
           var Subject="";
           var Principal="";
           var DeptID = "";
           var FromBillID = "";
           var BillStatus = "2";
           var FlowStatus = "-1";
           var EFIndex = "";
           var EFDesc = "";

           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/ProductionManager/MasterProductScheduleList.ashx',//目标地址
           cache:false,
           data: "pageIndex=" + pageIndexMaster + "&pageCount=" + pageCountMaster + "&action=" + actionMaster + "&orderby=" + orderByMaster + "&PlanNo=" + escape(PlanNo) + "&Subject=" + escape(Subject) + "&Principal=" + escape(Principal) + "&DeptID=" + escape(DeptID) + "&FromBillID=" + escape(FromBillID) + "&BillStatus=" + escape(BillStatus) + "&FlowStatus=" + escape(FlowStatus) + "&EFIndex=" + escape(EFIndex) + "&EFDesc=" + escape(EFDesc) + "", //数据
           beforeSend:function(){AddPopMaster();$("#pageDataListMaster_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListMaster tbody").find("tr.newrow").remove();
                    OptionCheckArrayAll=new Array();
                    
                    $.each(msg.data,function(i,item){  
                         if(item.ID != null && item.ID != "")
                         {
                            OptionCheckArrayAll.push(item.ID); 
                            var tempClickParam = popMasterObj.IsMore==true?"OptionCheckOnes(this);":"Fun_FillParent_MasterContent("+item.ID+",'"+item.PlanNo+"');";   
                            var tempType = popMasterObj.IsMore ==true?"checkbox":"radio";
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\""+tempType+"\" name=\"OptionCheck_"+item.ID+"\" id=\"OptionCheck_"+item.ID+"\" value=\""+item.ID+"\" onclick=\""+tempClickParam+"\" />"+"</td>"+
                            "<td height='22' align='center'>" + item.PlanNo + "</td>"+
                            "<td height='22' align='center'>" + item.Subject +"</td>"+
                            "<td height='22' align='center'>" + item.PrincipalReal + "</td>"+
                            "<td height='22' align='center'>" + item.DeptName + "</td>"+
                            "<td height='22' align='center'>" + item.TotalCounts + "</td>").appendTo($("#pageDataListMaster tbody"));
                         }
                       
                   });
                    //页码
                      ShowPageBar("pageDataListMaster_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleMaster,mark:"pageMasterDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountMaster,
                    currentPageIndex:pageIndexMaster,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageMaster({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_TotalProduct").html(msg.totalCount);//记录总条数
                      document.getElementById("TextMaster").value=msg.totalCount;
                      $("#ShowPageCountMaster").val(pageCountMaster);
                      ShowTotalPage(msg.totalCount,pageCountMaster,pageIndexMaster);
                      $("#ToPageMaster").val(pageIndexMaster);
                          ShowTotalPage(msg.totalCount,pageCountMaster,currentPageIndex,$("#pagecountMaster"));
                      //document.getElementById('tdResult').style.display='block';
                      },
           error: function() {}, 
           complete:function(){hidePopupMaster();$("#pageDataListMaster_Pager").show();IfshowMaster(document.getElementById('TextMaster').value);pageDataListMaster("pageDataListMaster","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataListMaster(o,a,b,c,d){
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

function IfshowMaster(count)
    {
        if(count=="0")
        {
            document.getElementById('divpageMaster').style.display = "none";
            document.getElementById('divpageMaster').style.display = "none";
        }
        else
        {
            document.getElementById('divpageMaster').style.display = "block";
            document.getElementById('divpageMaster').style.display = "block";
        }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexMaster(newPageCount,newPageIndex)
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
                this.pageCountMaster=parseInt(newPageCount);
                TurnToPageMaster(parseInt(newPageIndex));
            }
        }

    }
    //排序
    function OrderByMaster(orderColum,orderTip)
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
   
function AddPopMaster()
{
    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
 
function hidePopupMaster()
{
    //document.all.Forms.style.display = "none";
}



//选择项操作
var OptionCheckArrayAll=new Array();
var OptionCheckArrayAllSign=false;
function OptionCheckAlls()
{
    OptionCheckArrayAllSign=!OptionCheckArrayAllSign;
    for(var i=0;i<OptionCheckArrayAll.length;i++)
        document.getElementById('OptionCheck_'+OptionCheckArrayAll[i]).checked=OptionCheckArrayAllSign;
    
    if(OptionCheckArrayAllSign)
    {
        OptionCheckArray=OptionCheckArrayAll;
        document.getElementById('OptionCheckArrayBtn').value="取消";
    }else{
        OptionCheckArray=new Array();
        document.getElementById('OptionCheckArrayBtn').value="全选";
    }
}

var OptionCheckArray=new Array();
function OptionCheckOnes(obj)
{
    with(obj)
    {
        if(checked)
        {
            OptionCheckArray.push(value);
        }
        else
        {
            var OptionCheckArrayTM=new Array();
            for(var i=0;i<OptionCheckArray.length;i++)
            {
                if(OptionCheckArray[i]!=value)
                {
                    OptionCheckArrayTM.push(OptionCheckArray[i]);
                }
            }
            OptionCheckArray=OptionCheckArrayTM;
        }        
    }
}

/*清空主生产计划选择,物料需求计划列表用到*/
function ClearMasterControl() {
    document.getElementById('txtPlanName').value = '';
    document.getElementById('txtPlanID').value = '';
    document.getElementById('divMaster').style.display = 'none';
    closeRotoscopingDiv(false, 'divPopMasterShadow');
}

</script>

