<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProjectSelectControl.ascx.cs" Inherits="UserControl_Common_ProjectSelectControl" %>
<div id="divPopProjectShadow" style="display: none">
    <iframe id="PopProjectShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divStandardProject" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 600px; height: 200px; overflow: scroll; z-index: 1000;
        position: absolute; display: none; top: 50%; left: 70%; margin: 5px 0 0 -400px;
        scrollbar-face-color: #ffffff; scrollbar-highlight-color: #ffffff; scrollbar-shadow-color: COLOR:#000000;
        scrollbar-3dlight-color: #ffffff; scrollbar-darkshadow-color: #ffffff;">
        <table>
            <tr>
                <td>
                    <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                        onclick="CloseProject();" />
                        
                        <img id="btnClear" alt="清除" src="../../../images/Button/Bottom_btn_del.jpg" style='cursor: hand;'
                        onclick="ClearProject();" />
                </td>
            </tr>
        </table>
        <div style="width: 600px">
            <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                bgcolor="#CCCCCC">
                <tr>
                    <td bgcolor="#FFFFFF">
                        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                            class="table">
                            <tr class="table-item">
                                <td width="10%" height="20" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                                    项目编号
                                </td>
                                <td width="24%" bgcolor="#FFFFFF">
                                    <input id="ControlProjectNo" type="text" specialworkcheck="项目编号" class="tdinput" size="13" />
                                </td>
                                <td width="10%" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                                    项目名称
                                </td>
                                <td width="23%" bgcolor="#FFFFFF">
                                    <input id="ControlProjectName" type="text" specialworkcheck="项目名称" class="tdinput" size="19" />
                                </td>
                                <td width="10%" bgcolor="#E7E7E7" align="left" colspan="2">&nbsp;
                                <img alt="检索" id="btn_Serch" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                        onclick='TurnToPageProject(1);' />
                                </td>
                            </tr>

                        </table>
                        <br />
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListProject"
                bgcolor="#999999">
                <tbody>
                    <tr>
                        <th width="30" height="20" align="center" background="../../../images/Main/Table_bg.jpg"
                            bgcolor="#FFFFFF">
                            选择
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderByProject('ProjectNo','oGroup');return false;">
                                项目编号<span id="oGroup" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderByProject('ProjectName','oC1');return false;">
                                项目名称<span id="oC1" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderByProject('StartDate','oC2');return false;">
                                开始时间<span id="oC2" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderByProject('EndDate','oC3');return false;">
                                结束时间<span id="oC3" class="orderTip"></span></div>
                        </th>
                    </tr>
                </tbody>
            </table>
            <br />
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                class="PageList">
                <tr>
                    <td height="28" background="../../../images/Main/PageList_bg.jpg">
                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                            <tr>
                                <td height="28" background="../../../images/Main/PageList_bg.jpg" width="25%">
                                    <div id="pagecountProject">
                                    </div>
                                </td>
                                <td height="28" align="right">
                                    <div id="pageDataListProject_Pager" class="jPagerBar">
                                    </div>
                                </td>
                                <td height="28" align="right">
                                    <div id="divpageProject">
                                        <input name="text" type="text" id="TextProject" style="display: none" />
                                        <span id="pageDataListProject_Total"></span>每页显示
                                        <input name="text" type="text" id="ShowPageCountProject" size="5" />
                                        条 转到第
                                        <input name="text" type="text" id="ToPageProject" size="5" />
                                        页
                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                            width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexProject($('#ShowPageCountProject').val(),$('#ToPageProject').val());" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    <!--提示信息弹出详情end-->
</div>

<script language="javascript">
    var popProjectObj = new Object();
    popProjectObj.ControlID = null;
    popProjectObj.ControlVal = null;
    
    //custID,custName这两个参数为费用管理模块调用时用。
   var custID="";//客户ID
   var custName="";//客户名称


    var pageCountProject = 10; //每页计数
    var totalRecordProject = 0;
    var pagerStyleProject = "flickr"; //jPagerBar样式

    var currentPageIndex = 1;
    var actionProject = "GetProjectList"; //操作
    var orderByProject = ""; //排序字段

    function ShowProjectInfo(Control_TextID, Control_ValueID) {

        

        popProjectObj.ControlID = Control_TextID;
        popProjectObj.ControlVal = Control_ValueID;
        $("#ControlProjectNo").val("");  //项目编号
        $("#ControlProjectName").val(""); //项目名称
        openRotoscopingDiv(false, 'divPopProjectShadow', 'PopProjectShadowIframe');
        document.getElementById('divStandardProject').style.display = 'block';
        document.getElementById('divStandardProject').style.position = 'absolute';
        pageCountProject = 10;
        
        TurnToPageProject(1);
    }
    //费用管理模块专用 2010-5-12 add by hexw
    function ShowProjectInfoExp(Control_TextID, Control_ValueID,Control_CustID,Control_CustName) {

        popProjectObj.ControlID = Control_TextID;
        popProjectObj.ControlVal = Control_ValueID;
        custID=Control_CustID;
        custName=Control_CustName;
        $("#ControlProjectNo").val("");  //项目编号
        $("#ControlProjectName").val(""); //项目名称
        openRotoscopingDiv(false, 'divPopProjectShadow', 'PopProjectShadowIframe');
        document.getElementById('divStandardProject').style.display = 'block';
        document.getElementById('divStandardProject').style.position = 'absolute';
        pageCountProject = 10;
        
        TurnToPageProject(1);
    }
    //费用管理模块专用
    
    function TurnToPageProject(pageIndexProject) {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;
        var RetVals = CheckSpecialWords();
        
        if (RetVals != "") {
            isFlag = false;
            fieldText = fieldText + RetVals + "|";
            msgText = msgText + RetVals + "不能含有特殊字符|";
        }
        if (!isFlag) {
            popMsgObj.Show(fieldText, msgText);
            return;
        }
        currentPageIndex = pageIndexProject;
        var ProjectNo = $("#ControlProjectNo").val();  //项目编号
        var ProjectName = $("#ControlProjectName").val(); //项目名称
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/ProjectManager/ProjectInfo.ashx', //目标地址
            data: "pageIndex=" + pageIndexProject +
             "&pageCount=" + pageCountProject +
             "&action=" + actionProject +
             "&orderby=" + orderByProject +
             "&ProjectNo=" + escape(ProjectNo) +
             "&ProjectName=" + escape(ProjectName) + "", //数据
            beforeSend: function() {  $("#pageDataListProject_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
            //数据列表
                $("#pageDataListProject tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {


                    if (item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='CheckboxProject' name='CheckboxProject'  value=" + item.ID + "  type='radio'  onclick=\"SetResult('" + Trim(item.ProjectName, "g") + "','" + Trim(item.ID, "g") + "','" + Trim(item.CustID, "g") + "','" + Trim(item.CustName, "g") + "')\"  />" + "</td>" +
                        "<td height='22' align='center'>" + item.ProjectNo + "</td>" +
                        "<td height='22' align='center'>" + item.ProjectName + "</td>" +
                        "<td height='22' align='center'>" + item.StartDate + "</td>" +
                        "<td height='22' align='center'>" + item.EndDate + "</td>").appendTo($("#pageDataListProject tbody"));
                });
                //页码
                ShowPageBar("pageDataListProject_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyleProject, mark: "pageProjectDataListMark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageCountProject,
                    currentPageIndex: pageIndexProject,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "TurnToPageProject({pageindex});return false;"}//[attr]
                    );
                totalRecord = msg.totalCount;
                document.getElementById("TextProject").value = msg.totalCount;
                $("#ShowPageCountProject").val(pageCountProject);
                ShowTotalPage(msg.totalCount, pageCountProject, pageIndexProject);
                $("#ToPageProject").val(pageIndexProject);
                ShowTotalPage(msg.totalCount, pageCountProject, currentPageIndex, $("#pagecountProject"));
            },
            error: function() { },
            complete: function() {  $("#pageDataListProject_Pager").show(); IfshowProject(document.getElementById('TextProject').value); pageDataListProject("pageDataListProject", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
    //选中设置
    function SetResult(ProjectName, ProjectID,CustID,CustName) {
        $("#" + popProjectObj.ControlID).val(ProjectName);
        $("#" + popProjectObj.ControlVal).val(ProjectID);
        //费用管理模块用Start 
        try
        {
            if(custID!="")
            {
                $("#" + custID).val(CustID);
                $("#" + custName).val(CustName);
                $("#"+custName).attr("disabled", "disabled");
            }
        }
        catch(e){}
        //费用管理模块用END 
        CloseProject();
    }

    function ClearProject() {
        $("#ControlProjectNo").val("");  //项目编号       
        $("#ControlProjectName").val(""); //项目名称
        $("#" + popProjectObj.ControlID).val("");
        $("#" + popProjectObj.ControlVal).val("");

        //费用管理模块专用Start
        try
        {
            if(custID!="")
            {
                 $("#" + custID).val("");
                 $("#" + custName).val("");
                 $("#"+custName).attr("disabled", "");
            }
        }catch(e){}
        //费用管理模块专用End
        document.getElementById('divStandardProject').style.display = 'none';
        closeRotoscopingDiv(false, 'divPopProjectShadow');
    }

    function CloseProject() {

        document.getElementById('divStandardProject').style.display = 'none'; 
        closeRotoscopingDiv(false, 'divPopProjectShadow');
    }

    //table行颜色
    function pageDataListProject(o, a, b, c, d) {

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

    function IfshowProject(count) {
        if (count == "0") {
            document.getElementById('divpageProject').style.display = "none";
            document.getElementById('divpageProject').style.display = "none";
        }
        else {
            document.getElementById('divpageProject').style.display = "block";
            document.getElementById('divpageProject').style.display = "block";
        }
    }

    //改变每页记录数及跳至页数
    function ChangePageCountIndexProject(newPageCount, newPageIndex) {
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
            if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
                popMsgObj.Show("转到页数|", "超出查询范围|");
                return false;
            }
            else {
                this.pageCountProject = parseInt(newPageCount);
                TurnToPageProject(parseInt(newPageIndex));
            }
        }

    }
    //排序
    function OrderByProject(orderColum, orderTip) {
        var ordering = "a";
        //var orderTipDOM = $("#"+orderTip);
        var allOrderTipDOM = $(".orderTip");
        if ($("#" + orderTip).html() == "↓") {
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↑");
        }
        else {
            ordering = "d";
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↓");
        }
        orderByProject = orderColum + "_" + ordering;
        TurnToPageProject(1);
    }

</script>

