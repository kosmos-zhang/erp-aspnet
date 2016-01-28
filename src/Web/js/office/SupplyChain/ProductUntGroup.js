//计量单位组设置

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var flag = "";
var ActionFlag = ""
var str = "";
var currentPageIndex = 1;
var action = ""; //操作
var orderBy = ""; //排序字段

// 界面加载时
$(document).ready(function() {
    SearchData();
    requestobj = GetRequest();
    recordnoparam = requestobj['intOtherCorpInfoID'];
    InitGroupUnit(recordnoparam);

});

//初始化各业务计量单位
function InitGroupUnit(recordnoparam) {
    var BaseUnitID = $("#sel_UnitID").val();
    var unitBase = document.getElementById("sel_UnitID");
    var unitStorageObj = document.getElementById("selStorageUnit");
    var unitSellObj = document.getElementById("selSellUnit");
    var unitPurchaseObj = document.getElementById("selPurchseUnit");
    var unitProductObj = document.getElementById("selProductUnit");

    /*非编辑时初始时业务计量单位*/
    if (!(recordnoparam > 0)) {
        /*计量单位组编号*/
        $("#HdGroupNo").val("");
        $("#txtUnitGroup").val("");


        if (document.getElementById("sel_UnitID").length > 0) {
            /*取出基本计量单位的名称*/
            var BaseUnitName = unitBase.options[unitBase.selectedIndex].text;
            /*清空库存计量单位*/
            for (i = unitStorageObj.options.length; i > 0; i--) {
                unitStorageObj.options.remove(i - 1);
            }
            /*清空销售计量单位*/
            for (i = unitSellObj.options.length; i > 0; i--) {
                unitSellObj.options.remove(i - 1);
            }
            /*清空采购计量单位*/
            for (i = unitPurchaseObj.options.length; i > 0; i--) {
                unitPurchaseObj.options.remove(i - 1);
            }
            /*清空生产计量单位*/
            for (i = unitProductObj.options.length; i > 0; i--) {
                unitProductObj.options.remove(i - 1);
            }

            /*添加库存计量单位*/
            unitStorageObj.options.add(new Option(BaseUnitName, BaseUnitID));
            /*添加销售计量单位*/
            unitSellObj.options.add(new Option(BaseUnitName, BaseUnitID));
            /*添加采购计量单位*/
            unitPurchaseObj.options.add(new Option(BaseUnitName, BaseUnitID));
            /*添加生产计量单位*/
            unitProductObj.options.add(new Option(BaseUnitName, BaseUnitID));
        }
    }


    /*停用多计量单位时，让其为不可编辑*/
    if (!isMoreUnit) {
        document.getElementById('txtUnitGroup').disabled = true;
        unitStorageObj.disabled = true;
        unitSellObj.disabled = true;
        unitPurchaseObj.disabled = true;
        unitProductObj.disabled = true;
    }

}


//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    var ProdUnit = $("#sel_UnitID").val(); //物品的基本单位
    var para = "action=search"
            + "&pageIndex=" + pageIndex
            + "&pageCount=" + pageCount
            + "&orderby=" + orderBy
            + "&GroupUnitNo=" + escape($("#txtSGUNO").val())
            + "&GroupUnitName=" + escape($("#txtSGUName").val())
            + "&BaseUnitID=" + escape(ProdUnit);
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SupplyChain/UnitGroupAdd.ashx', //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() {
            AddPop();
            $("#pageDataList1_Pager").hide();
        }, //发送数据之前
        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioTech\" id=\"radioTech_" + item.ID + "\" value=\"" + item.GroupUnitNo + "\" onclick=\"Fun_FillParent_Content('" + item.GroupUnitName + "','" + item.GroupUnitNo + "');closeProductUnitdiv();\" />" + "</td>" +
                    "<td height='22' align='center'>" + item.GroupUnitNo + "</td>" +
                    "<td height='22' align='center'>" + item.GroupUnitName + "</td>" +
                    "<td height='22' align='center'>" + item.CodeName + "</td>" +
                    "<td height='22' align='center'>" + item.Remark + "</td>").appendTo($("#pageDataList1 tbody"));
            });
            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
            "<%= Request.Url.AbsolutePath %>", //[url]
            {style: pagerStyle, mark: "pageDataList1Mark",
            totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
            onclick: "TurnToPage({pageindex});return false;"}//[attr]
            );
            totalRecord = msg.totalCount;
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            document.getElementById('Text4').value = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex);
            $("#ToPage").val(pageIndex);
        },
        error: function() {
            howPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        complete: function() {
            hidePopup();
            $("#pageDataList1_Pager").show();
            Ifshow(document.getElementById('Text4').value);
            pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
            $("#btnAll").attr("checked", false);
        } //接收数据完毕
    });
}

//table行颜色
function pageDataList1(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 1; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
    }
}

// 查询数据
function SearchData() {
    TurnToPage(1);
}


function Ifshow(count) {
    if (count == "0") {
        document.getElementById('divpage').style.display = "none";
        document.getElementById('pagecount').style.display = "none";
    }
    else {
        document.getElementById('divpage').style.display = "block";
        document.getElementById('pagecount').style.display = "block";
    }
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    if (!IsZint(newPageCount)) {
        popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
        return;
    }
    if (!IsZint(newPageIndex)) {
        popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
        return;
    }
    if (newPageCount <= 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "显示页数超出显示范围！");
        return false;
    }
    if (newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
        return false;
    }
    else {
        this.pageCount = parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
        $("#btnAll").attr("checked", false);
    }
}
// 排序
function OrderBy(orderColum, orderTip) {
    var ordering = "a";
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
    orderBy = orderColum + "_" + ordering;
    TurnToPage(1);
}


//关闭遮挡界面
function CloseDiv() {
    closeRotoscopingDiv(false, 'divBackShadow');
}




// 添加空白行
function AddBlankRow() {
    var iRow = $("#hidRow").val();
    AddOneRow(iRow);
    $("#hidRow").val(iRow + 1);
    OrderLine();
    return iRow;
}

// 排序
function OrderLine() {
    $("#dg_Log tbody").find("tr.newrow").each(function(i) {
        $(this).find("input[name='trLine']").val(i + 1);
    });
}


// 获得非基本单位的下拉框
function GetUnitDDL() {
    $("#selUnitHid").html($("#selUnit").html());
    $("#selUnitHid option[selected]").remove();
}

function CheckData() {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var temp = $("#txtGUNO").val();
    var iRow;
    if (temp == "") {
        isFlag = false;
        fieldText = fieldText + "计量单位组编号|";
        msgText = msgText + "请输入计量单位组编号|";
    }
    if (strlen(temp) > 50) {
        isFlag = false;
        fieldText = fieldText + "计量单位组编号|";
        msgText = msgText + "计量单位组编号仅限于50个字符以内|";
    }
    var temp = $("#txtGUName").val();
    if (temp == "") {
        isFlag = false;
        fieldText = fieldText + "计量单位组名称|";
        msgText = msgText + "请输入计量单位组名称|";
    }
    if (strlen(temp) > 50) {
        isFlag = false;
        fieldText = fieldText + "计量单位组名称|";
        msgText = msgText + "计量单位组名称仅限于50个字符以内|";
    }
    temp = "";
    $("#dg_Log tbody").find("tr.newrow").each(function(i) {
        iRow = $(this).find("input[type='hidden']").val();
        if (temp.indexOf($("#trSel" + iRow).val() + ",") > -1) {
            isFlag = false;
            fieldText = fieldText + $("#trSel" + iRow).find("option:selected").text() + "|";
            msgText = msgText + "计量单位名称[" + $("#trSel" + iRow).find("option:selected").text() + "]重复|";
        }
        else {
            temp += $("#trSel" + iRow).val() + ",";
        }
        if ($("#trRate" + iRow).val() == "") {
            isFlag = false;
            fieldText = fieldText + "换算比率|";
            msgText = msgText + "请输入第" + (i + 1) + "行换算比率|";
        }
    });
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return false;
    }
    return true;
}
// 查询界面全选
function OptionCheckAll() {
    if (document.getElementById("btnAll").checked) {
        var ck = document.getElementsByName("Checkbox1");
        for (var i = 0; i < ck.length; i++) {
            ck[i].checked = true;
        }
    }
    else {
        var ck = document.getElementsByName("Checkbox1");
        for (var i = 0; i < ck.length; i++) {
            ck[i].checked = false;
        }
    }
}

/*********************************填充组名**************************************/
function Fun_FillParent_Content(UnitName, UnitNo) {
    $("#txtUnitGroup").val(UnitNo + '_' + UnitName);
    $("#HdGroupNo").val(UnitNo);

    document.getElementById("selStorageUnit").options.length = 0;
    document.getElementById("selSellUnit").options.length = 0;
    document.getElementById("selPurchseUnit").options.length = 0;
    document.getElementById("selProductUnit").options.length = 0;

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/SupplyChain/ProductInfo.ashx?action=LoadUnit&GroupUnitNo=' + UnitNo, //目标地址
        cache: false,
        data: '', //数据
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
            $.each(msg.data, function(i, item) {
                document.getElementById("selStorageUnit").options.add(new Option(item.CodeName, item.UnitID));
                document.getElementById("selSellUnit").options.add(new Option(item.CodeName, item.UnitID));
                document.getElementById("selPurchseUnit").options.add(new Option(item.CodeName, item.UnitID));
                document.getElementById("selProductUnit").options.add(new Option(item.CodeName, item.UnitID));
            });
        },
        error: function() {
            howPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        complete: function() {

        } //接收数据完毕
    });
}
/*********************************关闭单位组层**************************************/
function closeProductUnitdiv() {
    $("#DivUnitGroup").hide();
}
/*********************************显示组层**************************************/
function ShowUnitGroup() {
    $("#DivUnitGroup").show();
    SearchData();
}