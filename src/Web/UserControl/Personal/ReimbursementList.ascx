<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReimbursementList.ascx.cs" Inherits="UserControl_Personal_ReimbursementList" %>

<div id="divReimbList">
    <a name="pageReimbMark"></a>
    <!--提示信息弹出详情start-->
    <div id="divReimbListSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 800px; z-index: 20; position: absolute; display: none;
        top: 20%; left: 50%; margin: 5px 0 0 -400px;">
        <table width="99%" border="0" align="center" cellpadding="0" id="Table1" cellspacing="0"
            bgcolor="#CCCCCC">
            <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                        class="table">
                        <tr class="table-item">
                            <td height="25" colspan="3" bgcolor="#E7E7E7" align="center" style="width: 50%;">
                                <img id="closed" runat="server" src="../../Images/Button/Bottom_btn_close.jpg" 
                                        alt="关闭" style="cursor:pointer; float:left;" onclick="closeReimbListdiv();" />
                            </td>
                            <td height="25" colspan="3" bgcolor="#E7E7E7" align="center" style="width: 50%;">
                                <img id="clear" runat="server" src="../../Images/Button/Bottom_btn_del.jpg" 
                                        alt="清除" style="cursor:pointer; float:left;" onclick="clearReimbList();" />
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                单据编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="HReimbNo" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                主题
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="HReimbTitle" class="tdinput" maxlength="50" type="text" />
                            </td>
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                申请人
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <asp:DropDownList ID="UserApplyor" runat="server" Width="120px" AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td bgcolor="#E7E7E7" align="right">
                                日期
                            </td>
                            <td bgcolor="#FFFFFF">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                       <td style="width:50%">
                                            <input name="HReimbDate" style="width: 98%;" readonly="readonly" id="HReimbDate" class="tdinput"
                                                    type="text" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('HReimbDate')})"/>
                                            <input type="hidden" id="hiddHReimbDate" runat="server" />
                                        </td>
                                        <td>
                                            至
                                        </td>
                                        <td style="width:50%">
                                            <input name="HReimbDate1" style="width: 98%;" readonly="readonly" id="HReimbDate1" class="tdinput"
                                                 type="text" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('HReimbDate1')})"/>
                                            <input type="hidden" id="hiddHReimbDate1" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                单据状态
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                                <select name="HStatus"  style="width: 120px;margin-top:2px;margin-left:2px;" id="HStatus">
                                            <option value="" selected="selected">--请选择--</option>
                                            <option value="1">制单</option>
                                            <option value="2">执行</option>
                                            <option value="3">作废</option>
                                 </select>
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <input type="hidden" id="hiddSellSearchModel" value="all" />
                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                    onclick='TurnToReimbAllPage(1)' />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="ReimbDataListAll"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ReimbOrderBy('ReimbNo','ReimbNoTip');return false;">
                            报销单编号<span id="ReimbNoTip" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ReimbOrderBy('Title','TitleTip');return false;">
                            主题<span id="TitleTip" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ReimbOrderBy('ApplyorName','ApplyorNameTip');return false;">
                            申请人<span id="ApplyorNameTip" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ReimbOrderBy('ReimbDate','ReimbDateTip');return false;">
                            报销日期<span id="ReimbDateTip" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ReimbOrderBy('ExpAllAmount','ExpAllAmountTip');return false;">
                            申请费用总金额<span id="ExpAllAmountTip" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ReimbOrderBy('ReimbAllAmount','ReimbAllAmountTip');return false;">
                            报销总金额<span id="ReimbAllAmountTip" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ReimbOrderBy('RestoreAllAmount','RestoreAllAmountTip');return false;">
                            归还总金额<span id="RestoreAllAmountTip" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ReimbOrderBy('Status','StatusTextTip');return false;">
                            单据状态<span id="StatusTextTip" class="orderTip"></span></div>
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
                                <div id="pageReimbLCount"></div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageReimbList_Pager1" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divOrderPage">
                                    <span id="pageOrderList_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowReimbPageCount" style="width: 20px;" maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 20px;" id="ReimbToPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeReimbPageCountIndex($('#ShowReimbPageCount').val(),$('#ReimbToPage').val());" />
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
    var popReimbListObj = new Object();
    popReimbListObj.InputObj = null;


    popReimbListObj.ShowList = function(objInput) {
        popReimbListObj.InputObj = objInput;
        ShowPreventReclickDiv();
        $("#HReimbTitle").val('');
        $("#HReimbNo").val('');
        $("#ReimbursementList1_UserApplyor").val('');
        $("#HReimbDate").val('');
        $("#HReimbDate1").val('');
        $("#HStatus").val('');
        $("#hiddSellSearchModel").val(objInput); //查询的模式，all是取出所有的，protion时取出所有执行状态的
        document.getElementById('divReimbListSelect').style.display = 'block';
        TurnToReimbAllPage(currentOrderPageIndex);
    }

    var pageReimbLCount = 10; //每页计数
    var totalReimbRecord = 0;
    var pagerOrderStyle = "flickr"; //jPagerBar样式

    var currentOrderPageIndex = 1;
    var orderBy = ""; //排序字段
    //jQuery-ajax获取JSON数据
    function TurnToReimbAllPage(pageIndex) {
        if (!CheckInputUC()) {
            return;
        }
        currentOrderPageIndex = pageIndex;
        var UserApplyor = $.trim($("#ReimbursementList1_UserApplyor").val());//申请人
        var Title = $.trim($("#HReimbTitle").val());//主题
        var ReimbNo = $.trim($("#HReimbNo").val());//单据编号
        var ReimbDate = $("#HReimbDate").val();//申请日期
        var ReimbDate1 = $("#HReimbDate1").val();
        var Status=$("#HStatus").val();//单据状态
        
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Personal/Expenses/ReimbursementList.ashx', //目标地址
            cache: false,
            data: 'pageIndex=' + pageIndex + '&pageReimbLCount=' + pageReimbLCount + '&action=getHistoryReimbList&orderBy=' + orderBy +
            '&Title=' + escape(Title) + '&ReimbNo=' + escape(ReimbNo) + '&Applyor=' + escape(UserApplyor) +
            '&ReimbDate=' + escape(ReimbDate)+ '&ReimbDate1=' + escape(ReimbDate1)+'&Status='+escape(Status), //数据
            beforeSend: function() { $("#pageReimbList_Pager1").hide();}, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#ReimbDataListAll tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "") {
                        var ReimbNo = item.ReimbNo;
                        var Title = item.Title;
                        var ApplyorName = item.ApplyorName;
                        var ReimbDate=item.ReimbDate;
                        var Status=billStatusToStr(item.Status);
                        
                        if (Title != null) {
                            if (Title.length > 6) {
                                Title = Title.substring(0, 6) + '...';
                            }
                        }
                        if (ApplyorName != null) {
                            if (ApplyorName.length > 6) {
                                ApplyorName = ApplyorName.substring(0, 6) + '...';
                            }
                        }
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\"  id=\"radioExpApply_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"fnSelectReimbList(" + item.ID + ",'" + item.ReimbNo + "');\" name=\"radioExpApply\" />" + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.ReimbNo + "\">" + ReimbNo + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.Title + "\">" + Title + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.ApplyorName + "\">" + ApplyorName + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.ReimbDate + "\">" + ReimbDate + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.ExpAllAmount + "\">" + item.ExpAllAmount + "</td>"+
                         "<td height='22' align='center'><span title=\"" + item.ReimbAllAmount + "\">" + item.ReimbAllAmount + "</td>"+
                         "<td height='22' align='center'><span title=\"" + item.RestoreAllAmount + "\">" + item.RestoreAllAmount + "</td>" +
                         "<td height='22' align='center'><span title=\"" + item.Status  + "\">" + Status+ "</td>" ).appendTo($("#ReimbDataListAll tbody"));
                    }
                });
                //页码
                ShowPageBar("pageReimbList_Pager1", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerOrderStyle
                    , mark: "pageReimbMark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageReimbLCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "TurnToReimbAllPage({pageindex});return false;"}//[attr]
                    );
                totalReimbRecord = msg.totalCount;
                $("#ShowReimbPageCount").val(pageReimbLCount);
                ShowTotalPage(msg.totalCount, pageReimbLCount, pageIndex, $("#pageReimbLCount"));
                $("#ReimbToPage").val(pageIndex);
            },
            error: function() { },
            complete: function() { $("#pageReimbList_Pager1").show(); ReimbDataListAll("ReimbDataListAll", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
  //状态显示  
  function billStatusToStr(billStatus)
  {
    if(billStatus==1)
    {
        return "制单";
    }else if(billStatus==2)
    {
        return "执行";
    }else if(billStatus==4)
    {
        return "作废";
    }
  }
    //table行颜色
    function ReimbDataListAll(o, a, b, c, d) {
        var t = document.getElementById(o).getElementsByTagName("tr");
        for (var i = 0; i < t.length; i++) {
            t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
            t[i].onmouseover = function() {
                if (this.x != "1") this.style.backgroundColor = c;
            }
            t[i].onmouseout = function() {
                if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
            }
        }
    }

    //改变每页记录数及跳至页数
    function ChangeReimbPageCountIndex(newPageCount, newPageIndex) {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;
        if (!IsNumber(newPageIndex) || newPageIndex == 0) {
            isFlag = false;
            fieldText = fieldText + "跳转页面|";
            msgText = msgText + "必须为正整数格式|";
        }
        if (!IsNumber(newPageCount) || newPageCount == 0) {
            isFlag = false;
            fieldText = fieldText + "每页显示|";
            msgText = msgText + "必须为正整数格式|";
        }
        if (!isFlag) {
            popMsgObj.Show(fieldText, msgText);
        }
        else {
            if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalReimbRecord - 1) / newPageCount) + 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
                return false;
            }
            else {
                this.pageReimbLCount = parseInt(newPageCount);
                TurnToReimbAllPage(parseInt(newPageIndex));
            }
        }
    }

    //排序
    function ReimbOrderBy(orderColum, orderTip) {
        var ordering = "d";
        var orderTipDOM = $("#" + orderTip);
        var allOrderTipDOM = $(".orderTip");
        if ($("#" + orderTip).html() == "↓") {
            ordering = "a";
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↑");
        }
        else {
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↓");
        }
        orderBy = orderColum + "_" + ordering;
        TurnToReimbAllPage(1);
    }
    //关闭
    function closeReimbListdiv() {
        obj = document.getElementById("divPreventReclick");
        //隐藏遮挡的DIV
        if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
        document.getElementById("divReimbListSelect").style.display = "none";
    }
    //表单验证
    function CheckInputUC() {
        var fieldText = "";
        var isFlag = true;
        var msgText = "";

        var ReimbNo = $("#HReimbTitle").val();
        var ReimbDate = $("#HReimbDate").val(); //报销日期
        var ReimbDate1 = $("#HReimbDate1").val(); //报销日期
        var RetVal = CheckSpecialWords();
        if (RetVal != "") {
            isFlag = false;
            fieldText = fieldText + RetVal + "|";
            msgText = msgText + RetVal + "不能含有特殊字符|";
        }

        if (ReimbNo != '') {
            if (!CodeCheck(ReimbNo)) {
                isFlag = false;
                fieldText = fieldText + "费用报销单编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }

        if (ReimbDate != '' && ReimbDate1 != '') {
            if (ReimbDate > ReimbDate1) {
                isFlag = false;
                fieldText = fieldText + "报销日期范围|";
                msgText = msgText + "报销日期范围输入错误|";
            }
        }
        if((ReimbDate=='' && ReimbDate1!='')||(ReimbDate!='' && ReimbDate1==''))
        {
            isFlag=false;
            fieldText=fieldText+"报销日期填写不完整|";
            msgText=msgText+"报销日期范围需填写完整|";
        }
        
        if (!isFlag) {
            //注：两个方法显示弹出提示信息层,方法一是对字段用红色处理，方法二是所有的提示信息传入未处理颜色

            //方法一
            popMsgObj.Show(fieldText, msgText);
            //方法二
            //popMsgObj.ShowMsg('所有的错误信息字符串');
        }
        return isFlag;
    }
</script>
