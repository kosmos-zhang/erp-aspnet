<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufactureTask.ascx.cs"
    Inherits="UserControl_ManufactureTask" %>
<div id="divPopTaskShadow" style="display: none">
    <iframe id="PopTaskShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divTask" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 600px; z-index: 1000; position: absolute; display: none; top: 50%; left: 70%;
        margin: 5px 0 0 -400px;">
        <table width="99%">
            <tr>
                <td width="15">
                    <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;' onclick="document.getElementById('divTask').style.display='none';closeRotoscopingDiv(false,'divPopTaskShadow');"  />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListTask"
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
                            制单人<span id="oC4" class="orderTip"></span></div>
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
                                <div id="pagecountTask">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataListTask_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageTask">
                                    <input name="text" type="text" id="TextTask" style="display: none" />
                                    <span id="pageDataListTask_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountTask" style="width: 25px" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageTask" style="width: 25px" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexTask($('#ShowPageCountTask').val(),$('#ToPageTask').val());" />
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
    var popTaskObj=new Object();
    popTaskObj.InputObj = null;

  
    var pageCountTask = 10;//每页计数
    var totalRecordTask = 0;
    var pagerStyleTask = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var actionTask = "";//操作
    var orderByTask = "ModifiedDate_d";//排序字段
    
    popTaskObj.ShowList=function(objInput)
    {
        popTaskObj.InputObj = objInput;
        openRotoscopingDiv(false,'divPopTaskShadow','PopTaskShadowIframe');
        document.getElementById('divTask').style.display='block';
        TurnToPageTask(currentPageIndex);
    }

    //jQuery-ajax获取JSON数据
    function TurnToPageTask(pageIndexTask)
    {
           currentPageIndex = pageIndexTask;
           var TaskNo = '';
           var Subject='';
           var ManufactureType="-1";
           var Principal = '';
           var DeptID = '';
           var BillStatus = '2';
           var FlowStatus = '-1';
           var CreateDate = '';
           var ProjectID = 0;
           var EFIndex = "";
           var EFDesc = "";
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/ProductionManager/ManufactureTaskList.ashx',//目标地址
           cache:false,
           data: "pageIndex=" + pageIndexTask + "&pageCount=" + pageCountTask + "&action=" + actionTask + "&orderby=" + orderByTask + "&TaskNo=" + escape(TaskNo) + "&Subject=" + escape(Subject) + "&ManufactureType=" + escape(ManufactureType) + "&Principal=" + escape(Principal) + "&DeptID=" + escape(DeptID) + "&BillStatus=" + escape(BillStatus) + "&FlowStatus=" + escape(FlowStatus) + "&CreateDate=" + escape(CreateDate) + "&EFIndex=" + escape(EFIndex) + "&EFDesc=" + escape(EFDesc) + "&ProjectID=" + escape(ProjectID) + "", //数据
           beforeSend:function(){AddPopTask();$("#pageDataListTask_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListTask tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){  
  
                    if(item.ID != null && item.ID != "")
                    {
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"OptionCheck_"+item.ID+"\" id=\"OptionCheck_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillParent_TaskContent("+item.ID+",'"+item.TaskNo+"',"+item.ProjectID+",'"+item.ProjectName+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.TaskNo + "</td>"+
                        "<td height='22' align='center'>" + item.Subject +"</td>"+
                        "<td height='22' align='center'>" + item.PrincipalReal + "</td>"+
                        "<td height='22' align='center'>" + item.DeptName + "</td>"+
                        "<td height='22' align='center'>" + item.CreatorReal + "</td>").appendTo($("#pageDataListTask tbody"));
                    }
                   });
                     //页码
                      ShowPageBar("pageDataListTask_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleTask,mark:"pageTaskDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountTask,
                    currentPageIndex:pageIndexTask,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageTask({pageindex});return false;"}//[attr]
                    );
                      totalRecordTask = msg.totalCount;
                      document.getElementById('TextTask').value=msg.totalCount;
                      $("#ShowPageCountTask").val(pageCountTask);
                      ShowTotalPage(msg.totalCount,pageCountTask,pageIndexTask);
                      $("#ToPageTask").val(pageIndexTask);
                      ShowTotalPage(msg.totalCount,pageCountTask,currentPageIndex,$("#pagecountTask"));
                      },
           error: function() {}, 
           complete:function(){hidePopupTask();$("#pageDataListTask_Pager").show();IfshowTask(document.getElementById('TextTask').value);pageDataListTask("pageDataListTask","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataListTask(o,a,b,c,d){
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

function IfshowTask(count)
    {
        if(count=="0")
        {
            document.all["divpageTask"].style.display = "none";
            document.all["pagecountTask"].style.display = "none";
        }
        else
        {
            document.all["divpageTask"].style.display = "block";
            document.all["pagecountTask"].style.display = "block";
        }
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexTask(newPageCount,newPageIndex)
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
            if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordTask-1)/newPageCount)+1 )
            {
                popMsgObj.Show("转到页数|","超出查询范围|");
                return false;
            }
            else
            {
                this.pageCountTask=parseInt(newPageCount);
                TurnToPageTask(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function OrderByTask(orderColum,orderTip)
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
   
function AddPopTask()
{
    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
 
function hidePopupTask()
{
    //document.all.Forms.style.display = "none";
}

</script>

