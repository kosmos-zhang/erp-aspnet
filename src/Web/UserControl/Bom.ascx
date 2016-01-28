<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Bom.ascx.cs" Inherits="UserControl_Bom" %>
<div id="divPopBomShadow" style="display: none">
    <iframe id="PopBomShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divBom" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 600px; z-index: 1000; position: absolute; display: none; top: 20%; left: 70%;
        margin: 5px 0 0 -400px;">
        <table width="99%">
            <tr>
                <td>
                    <img id="btnClear" alt="清空" src="../../../images/Button/Bottom_btn_del.jpg" style='cursor: hand;' onclick="ClearBomControl();"  />
                    <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;' onclick="document.getElementById('divBom').style.display='none';closeRotoscopingDiv(false,'divPopBomShadow');"   />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListBom"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            Bom编号<span id="oBomNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            上级Bom<span id="oParentBom" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            父件<span id="oProductName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            Bom类型<span id="oType" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            工艺路线<span id="oRouteName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            启用状态<span id="oUsedStatus" class="orderTip"></span></div>
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
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="pagecountBom">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataListBom_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageBom">
                                    <input name="text" type="text" id="TextBom" style="display: none" />
                                    <span id="pageDataListBom_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountBom" style="width: 25px" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageBom" style="width: 25px" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexBom($('#ShowPageCountBom').val(),$('#ToPageBom').val());" />
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
var popBomObj=new Object();
popBomObj.InputObj = null;
popBomObj.BomID = 0;     //修改BOM时，过滤掉引用当前BOM为上级BOM的BOM
popBomObj.ProductID = 0; //新建或修改生产任务时，筛选父件为当前明细中物品的BOM

popBomObj.ShowList=function(objInput,objBom,objProduct)
{
    popBomObj.InputObj = objInput;
    popBomObj.BomID =0;
    popBomObj.ProductID = 0;
    openRotoscopingDiv(false,'divPopBomShadow','PopBomShadowIframe');
    if(objBom!=null)
    {
        popBomObj.BomID = document.getElementById(objBom).value;
    }

    if(objProduct!=null)
    {
        
        popBomObj.ProductID = document.getElementById(objProduct).value; 
        if(strlen(cTrim(popBomObj.ProductID,0))<=0)
        {
            closeRotoscopingDiv(false,'divPopBomShadow');
            popMsgObj.ShowMsg('没有选择物品!');
            return false;
        }
    }
    document.getElementById('divBom').style.display='block';
    TurnToPageBom(1);
}   
    var pageCountBom = 10;//每页计数
    var totalRecordBom = 0;
    var pagerStyleBom = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var actionBom = "";//操作
    var orderByBom = "ModifiedDate_d";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageBom(pageIndexBom)
    {
           currentPageIndexBom= pageIndexBom;
           var BomNo= '';
           var Type='';
           var RouteID=0;
           var UsedStatus ='1';
           var QueryType = 'BomControl';
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/ProductionManager/BomList.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexBom+"&pageCount="+pageCountBom+"&action="+actionBom+"&orderby="+orderByBom+"&QueryType="+QueryType+"&BomID="+popBomObj.BomID+"&ProductID="+popBomObj.ProductID+"&BomNo="+escape(BomNo)+"&Type="+escape(Type)+"&RouteID="+escape(RouteID)+"&UsedStatus="+escape(UsedStatus)+"",//数据
           beforeSend:function(){AddPopBom();$("#pageDataListBom_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListBom tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){                       
                        if(item.ID != null && item.ID != "")
                        {
                            var tempUsedStatus = null;
                            var tempType =null;
                            if(item.UsedStatus==1){tempUsedStatus='启用';}else{tempUsedStatus='停用';}
                            if(item.Type==0) tempType='工程Bom';
                            if(item.Type==1) tempType='生产Bom';
                            if(item.Type==2) tempType='销售Bom';
                            if(item.Type==3) tempType='成本Bom';
                            
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillParent_BomContent("+item.ID+",'"+item.BomNo+"',"+item.RouteID+",'"+item.RouteName+"');\" />"+"</td>"+
                            "<td height='22' align='center'>" + item.BomNo + "</td>"+
                            "<td height='22' align='center'>" + item.ParentBom +"</td>"+
                            "<td height='22' align='center'>" + item.ProductName +"</td>"+
                            "<td height='22' align='center'>"+tempType+"</td>"+
                            "<td height='22' align='center'>"+item.RouteName+"</td>"+
                            "<td height='22' align='center'>"+tempUsedStatus+"</td>").appendTo($("#pageDataListBom tbody"));
                        }
                   });
                     //页码
                      ShowPageBar("pageDataList1_PagerBom",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleBom,mark:"pageprodDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountBom,
                    currentPageIndex:pageIndexBom,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageBom({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_TotalProduct").html(msg.totalCount);//记录总条数
                      document.getElementById("TextBom").value=msg.totalCount;
                      $("#ShowPageCountBom").val(pageCountBom);
                      ShowTotalPage(msg.totalCount,pageCountBom,pageIndexBom);
                      $("#ToPageBom").val(pageIndexBom);
                      ShowTotalPage(msg.totalCount,pageCountBom,currentPageIndexBom,$("#pagecountBom"));
                      },
           error: function() {}, 
           complete:function(){hidePopupBom();$("#pageDataListBom_Pager").show();IfshowBom(document.getElementById("TextBom").value);pageDataListBom("pageDataListBom","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataListBom(o,a,b,c,d){
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

function IfshowBom(count)
    {
        if(count=="0")
        {
            document.getElementById("divpageBom").style.display = "none";
            document.getElementById("pagecountBom").style.display = "none";
        }
        else
        {
            document.getElementById("divpageBom").style.display = "block";
            document.getElementById("pagecountBom").style.display = "block";
        }
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexBom(newPageCount,newPageIndex)
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
                this.pageCountBom=parseInt(newPageCount);
                TurnToPageBom(parseInt(newPageIndex));
            }
        }

    }
    //排序
    function OrderByBom(orderColum,orderTip)
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
   
function AddPopBom()
{
    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
 
function hidePopupBom()
{
    //document.all.Forms.style.display = "none";
}


</script>

