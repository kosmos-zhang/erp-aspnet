



function jqControl(value) {
    var ret = "";
    var point = document.getElementById("HiddenPoint").value;
    if (value != null && value != "" && value != undefined) {
        ret = parseFloat(value).toFixed(point);
    }
    return ret;
}

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var currentPageIndex = 1;

var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {

    var point = document.getElementById("HiddenPoint").value;
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return false;
    }


    currentPageIndex = pageIndex;
    var ddlStorage = document.getElementById("ddlStorage").value;

    var ProductID = document.getElementById("hiddenProductID").value;
    var StartDate = document.getElementById("txtStartDate").value;
    var EndDate = document.getElementById("txtEndDate").value;
    var BatchNo = document.getElementById("ddlBatchNo").value;
    var SourceType = document.getElementById("ddlSourceType").value;

    var SourceNo = document.getElementById("txtSourceNo").value;

    var CreatorID = document.getElementById("txtCreatorID").value;

    var EFIndex = document.getElementById("GetBillExAttrControl1_SelExtValue").value;
    var EFDesc = document.getElementById("GetBillExAttrControl1_TxtExtValue").value;

    var Specification = document.getElementById("txt_Specification").value;
    var ColorID = document.getElementById("sel_ColorID").value;
    var Material = document.getElementById("sel_Material").value;
    var Manufacturer = document.getElementById("txt_Manufacturer").value;
    var Size = document.getElementById("txt_Size").value;
    var FromAddr = document.getElementById("txt_FromAddr").value;
    var BarCode = document.getElementById("txt_BarCode").value;

    var ckbIsM = 0;
    if (document.getElementById("ckbIsM").checked) {
        ckbIsM = 1;
    }

    var paramList = "&Specification=" + escape(Specification) + "&ColorID=" + escape(ColorID) + "&Material=" + escape(Material) + "&Manufacturer=" + escape(Manufacturer) + "&Size=" + escape(Size) + "&FromAddr=" + escape(FromAddr) + "&BarCode=" + escape(BarCode) + "&ckbIsM=" + escape(ckbIsM);
    var action = "DetailInfos"; //操作
    var UrlParam = "&pageIndex=" + pageIndex + "&pageCount=" + pageCount + 
           "&orderby=" + orderBy + "&ddlStorage=" + escape(ddlStorage) +
           "&ProductID=" + escape(ProductID) + "&StartDate=" + escape(StartDate) + "&EndDate=" + escape(EndDate) + "&BatchNo=" + escape(BatchNo) + "&SourceType=" + escape(SourceType) + "&SourceNo=" + escape(SourceNo) + "&CreatorID=" + escape(CreatorID) + "&EFIndex=" + escape(EFIndex) + "&EFDesc=" + escape(EFDesc) + paramList;


    var myid = document.getElementById("GetBillExAttrControl1_SelExtValue");
    var currSelectText = myid.options[myid.selectedIndex].text;
    var sidex = "ExtField" + EFIndex;
    document.getElementById("hiddenEFIndex").value = EFIndex;
    document.getElementById("hiddenEFDesc").value = EFDesc;

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/StorageManager/StorageJournal.ashx?Action=DetailInfos' + UrlParam, //目标地址
        cache: false,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            if (EFIndex != -1 && EFDesc != "") {
                document.getElementById("thHole").style.display = "block";
                document.getElementById("newItem").innerHTML = currSelectText;
                $('#divClick').click(function() { OrderBy(sidex, 'Span8'); return false; });
                document.getElementById("hiddenEFIndexName").value = currSelectText;

                $.each(msg.data, function(i, item) {
                    $("<tr class='newrow'></tr>").append(
                "<td height='22' align='center' title=\"" + item.BillNo + "\">" + GetLinkUrl(item.BillNo, item.PageUrl) + "</td>" +
                "<td height='22' align='center' title=\"" + TypeName(item.typeflag) + "\">" + TypeName(item.typeflag) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.BatchNo + "\">" + fnjiequ(item.BatchNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProdNo + "\">" + fnjiequ(item.ProdNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductSize + "\">" + fnjiequ(item.ProductSize, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.FromAddr + "\">" + fnjiequ(item.FromAddr, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.EnterDate + "\">" + fnjiequ(item.EnterDate, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductCount + "\">" + parseFloat(item.ProductCount).toFixed(point) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.NowProductCount + "\">" + parseFloat(item.NowProductCount).toFixed(point) + "</td>" +
                         "<td height='22' align='center' title=\"" + item.CreatorName + "\">" + fnjiequ(item.CreatorName, 10) + "</td>" +
                         "<td height='22' align='center' title=\"" + item.ReMark + "\">" + fnjiequ(item.ReMark, 15) + "</td>" +
                        "<td height='22' align='center' title=\"" + item[sidex] + "\">" + item[sidex] + "</td>").appendTo($("#pageDataList1 tbody"));

                });
            } else {
                document.getElementById("thHole").style.display = "none";
                $.each(msg.data, function(i, item) {
                    $("<tr class='newrow'></tr>").append(
                "<td height='22' align='center' title=\"" + item.BillNo + "\">" + GetLinkUrl(item.BillNo, item.PageUrl) + "</td>" +
                "<td height='22' align='center' title=\"" + TypeName(item.typeflag) + "\">" + TypeName(item.typeflag) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.BatchNo + "\">" + fnjiequ(item.BatchNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProdNo + "\">" + fnjiequ(item.ProdNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductSize + "\">" + fnjiequ(item.ProductSize, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.FromAddr + "\">" + fnjiequ(item.FromAddr, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.EnterDate + "\">" + fnjiequ(item.EnterDate, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductCount + "\">" + parseFloat(item.ProductCount).toFixed(point) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.NowProductCount + "\">" + parseFloat(item.NowProductCount).toFixed(point) + "</td>" +
                         "<td height='22' align='center' title=\"" + item.CreatorName + "\">" + fnjiequ(item.CreatorName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ReMark + "\">" + fnjiequ(item.ReMark, 15) + "</td>").appendTo($("#pageDataList1 tbody"));

                });
            }

            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
            document.all["Text2"].value = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
            $("#txtBarCode").val(""); //清空条码
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() {
            hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.all["Text2"].value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");




            $.ajax({
                type: "POST", //用POST方式传输
                url: '../../../Handler/Office/StorageManager/StorageJournal.ashx?Action=SumJournal' + UrlParam, //目标地址
                dataType: "json", //数据格式:JSON
                cache: false,
                beforeSend: function() {
                }, //发送数据之前
                success: function(data) {

                    if (data.sta == 1) {
                        var messageValue = data.info.split("|");

                        if (EFIndex != -1 && EFDesc != "" && EFIndex != "") {
                            $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                          "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                         "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[1]) + "</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                       "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                        }
                        else {
                            $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                          "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                         "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[1]) + "</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                        }


                    }

                },
                error: function() {
                    popMsgObj.ShowMsg('请求发生错误！');
                },
                complete: function() {
                }
            });







        } //接收数据完毕
    });
}
//table行颜色
function pageDataList1(o, a, b, c, d) {
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

function Fun_Search_StorageInfo(aa) {
    search = "1";
    document.getElementById("hidSearchCondition").value = search; //这里只是放了一个标志位，说明是点过了检索按钮
    TurnToPage(1);
}

function Ifshow(count) {
    if (count == "0") {
        document.all["divpage"].style.display = "none";
        document.all["pagecount"].style.display = "none";
    }
    else {
        document.all["divpage"].style.display = "block";
        document.all["pagecount"].style.display = "block";
    }
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {

    //判断是否是数字
    if (!PositiveInteger(newPageCount)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "每页显示应为正整数！");
        return;
    }
    if (!PositiveInteger(newPageIndex)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数应为正整数！");
        return;
    }
    if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
        return false;
    }
    else {
        this.pageCount = parseInt(newPageCount, 10);
        TurnToPage(parseInt(newPageIndex, 10));
    }
}
//排序
function OrderBy(orderColum, orderTip) {
    if (document.getElementById("hidSearchCondition").value == "" || document.getElementById("hidSearchCondition").value == null) return;
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
    orderBy = orderColum + "_" + ordering;
    $("#txtorderBy").val(orderBy); //把排序字段放到隐藏域中，
    Fun_Search_StorageInfo();
}

//物品控件
function Fun_FillParent_Content(id, ProNo, ProdName) {
    document.getElementById('txtProductNo').value = ProNo;
    document.getElementById('txtProductName').value = ProdName;
    document.getElementById('hiddenProductID').value = id;

}



document.onkeydown = ScanBarCodeSearch;
/*列表条码扫描检索*/
function ScanBarCodeSearch() {
    var evt = event ? event : (window.event ? window.event : null);
    var el; var theEvent
    var browser = IsBrowser();
    if (browser == "IE") {
        el = window.event.srcElement;
        theEvent = window.event;
    }
    else {
        el = evt.target;
        theEvent = evt;
    }
    if (el.id != "txtBarCode") {
        return;
    }
    else {
        var code = theEvent.keyCode || theEvent.which;
        if (code == 13) {
            TurnToPage(1);
            evt.returnValue = false;
            evt.cancel = true;
        }
    }
}

function IsBrowser() {
    var isBrowser;
    if (window.ActiveXObject) {
        isBrowser = "IE";
    } else if (window.XMLHttpRequest) {
        isBrowser = "FireFox";
    }
    return isBrowser;
}



/*
* 返回付款单列表
*/
function DoBack() {
    //获取查询条件
    var searchCondition = document.getElementById("hiddenSearch").value;

    window.location.href = "StorageJournal.aspx?" + searchCondition;

}

function TypeName(flag) {

    var name = "";
    switch (parseInt(flag)) {
        case 1: name = "期初库存录入";
            break;
        case 2: name = "期初库存批量导入";
            break;
        case 3: name = "采购入库单";
            break;
        case 4: name = "生产完工入库单";
            break;
        case 5: name = "其他入库单";
            break;
        case 6: name = "红冲入库单";
            break;
        case 7: name = "销售出库单";
            break;
        case 8: name = "其他出库单";
            break;
        case 9: name = "红冲出库单";
            break;
        case 10: name = "借货申请单";
            break;
        case 11: name = "借货返还单";
            break;
        case 12: name = "调拨出库";
            break;
        case 13: name = "调拨入库";
            break;
        case 14: name = "日常调整单";
            break;
        case 15: name = "期末盘点单";
            break;
        case 16: name = "库存报损单";
            break;
        case 17: name = "领料单";
            break;
        case 18: name = "退料单";
            break;
        case 19: name = "配送单（配送出库）";
            break;
        case 20: name = "配送退货单（验收入库）";
            break;
        case 21: name = "门店销售管理";
            break;
        case 22: name = "门店销售退货";
            break;
        default: name = "采购入库单";
            break;
    }

    return name;
}

function GetLinkUrl(BillNo, PageUrl) {

    //    var sessionSection = "";

    //    var url = document.location.href.toLowerCase();
    //    if (url.indexOf("/(s(") != -1) {
    //        var sidx = url.indexOf("/(s(") + 1;
    //        var eidx = url.indexOf("))") + 2;
    //        //alert(sidx+":"+eidx);
    //        url = document.location.href;

    //        sessionSection = url.substring(sidx, eidx);
    //        sessionSection += "/";
    //    }


    //    var ownInfo = "&intFromType=6&ListModuleID=2051903";
    //    
    //    var ret = "";
    //    if (PageUrl != null && PageUrl != "" && PageUrl != undefined) {
    //        ret = "<a href=/" + sessionSection + PageUrl + ownInfo + " >" + fnjiequ(BillNo, 10) + "</a>";
    //    }
    //    else {
    //        ret = fnjiequ(BillNo, 10);
    //    }
    ret = fnjiequ(BillNo, 10);
    return ret;
}


//物品控件
function Fun_FillParent_Content(id, ProNo, ProdName) {
    document.getElementById('txtProductNo').value = ProNo;
    //document.getElementById('txtProductName').value=ProdName;
    document.getElementById('hiddenProductID').value = id;

}
