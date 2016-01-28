/* Create at 2010-01-28 
   Version:1
*/
function LoadCommonPrintParameterSet(billTypeFlag, printTypeFlag,isSeted, dbBase, dbDetail,dbSecondDetail) {
    /*主表：勾选中的字段*/
    var strBaseFields = '';
    /*明细表：勾选中的字段*/
    var strDetailFields = '';
    /*明细表2：勾选中的字段*/
    var strDetailSecondFields = '';

    if (isSeted == 0) {
            
        /*未设置过打印模板设置的，缺省选中所有*/
        for (var x = 0; x < dbBase.length; x++) {
            document.getElementById(dbBase[x][0]).checked = true;
        }
        if (dbDetail != null){
            for (var y = 0; y < dbDetail.length; y++) {
                document.getElementById(dbDetail[y][0]).checked = true;
            }
        }
        if (dbSecondDetail != null) {
            for (var y = 0; y < dbSecondDetail.length; y++) {
                document.getElementById(dbSecondDetail[y][0]).checked = true;
            }
        }
    }
    else {

        /*获取打印模板设置信息*/
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../../Handler/Common/PrintParameterSettingInfo.ashx?BillTypeFlag=" + billTypeFlag + "&PrintTypeFlag=" + printTypeFlag,
            cache: false,
            success: function(msg) {
                if (typeof (msg.dataPrint) != 'undefined') {
                    $.each(msg.dataPrint, function(i, item) {
                        strBaseFields = item.BaseFields;
                        strDetailFields = item.DetailFields;
                        strDetailSecondFields = item.DetailSecondFields;


                        /*主表复选框是否选中处理*/
                        var arrBase = strBaseFields.split('|');
                        for (var m = 0; m < arrBase.length; m++) {
                            for (var x = 0; x < dbBase.length; x++) {
                                if (arrBase[m] == dbBase[x][1]) {
                                    document.getElementById(dbBase[x][0]).checked = true;
                                }
                            }
                        }
                        /*明细表复选框是否选中处理*/
                        if (dbDetail != null){/*modify by Moshenlin 没有明细时的单据判断*/
                        var arrDetail = strDetailFields.split('|');
                        for (var n = 0; n < arrDetail.length; n++) {
                            for (var y = 0; y < dbDetail.length; y++) {
                                if (arrDetail[n] == dbDetail[y][1]) {
                                    document.getElementById(dbDetail[y][0]).checked = true;
                                }
                            }
                        }
                        }

                        /*明细表复选框2是否选中处理*/
                        if (dbSecondDetail != null) {
                            var arrSecondDetail = strDetailSecondFields.split('|');
                            for (var n = 0; n < arrSecondDetail.length; n++) {
                                for (var y = 0; y < dbSecondDetail.length; y++) {
                                    if (arrSecondDetail[n] == dbSecondDetail[y][1]) {
                                        document.getElementById(dbSecondDetail[y][0]).checked = true;
                                    }
                                }
                            }
                        }

                    }
                   );
                }
            },
            error: function() { },
            complete: function() { }
        });
    }

}


function SaveCommonPrintParameterSet(baseFields,detailFields,detailSecondFields,billTypeFlag,printTypeFlag,locations) {
    var Action = 'Add';
    var detailThreeFields = '';
    var detailFourFields = '';
    var UrlParam = '';
    UrlParam = "Action=" + Action + "&strBaseFields=" + baseFields + "\
                                     &strDetailFields=" + detailFields + "\
                                     &strDetailSecondFields=" + detailSecondFields + "\
                                     &strDetailThreeFields=" + detailThreeFields + "\
                                     &strDetailFourFields=" + detailFourFields + "\
                                     &BillTypeFlag=" + billTypeFlag + "\
                                     &PrintTypeFlag=" + printTypeFlag;
    $.ajax({
        type: "POST",
        dataType: 'json',
        data: UrlParam,
        url: "../../../Handler/Common/PrintParameterSetting.ashx",
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            alert('当前的请求发生错误');
        },
        success: function(data) {
            if (data.sta > 0) {
                //alert(data.info);
                document.getElementById('div_InInfo').style.display = 'none';
                closeRotoscopingDiv(false, 'divPageMask');
                window.parent.location = locations;
            }
            else {
                alert(data.info);
            }
        }
    });
}



/*扩展属性名称*/
function LoadExtTableName(tableName) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx",
        cache: false,
        data: 'action=all&TableName=' + tableName,
        beforeSend: function() { },
        success: function(msg) {
            if (parseInt(msg.totalCount) > 0) {
                $.each(msg.data, function(i, item) {
                    document.getElementById('txtExtField' + (i + 1)).value = item.EFDesc + "：";
                    document.getElementById('txtExtField' + (i + 1)).style.display = '';
                    document.getElementById('ckExtField' + (i + 1)).style.display = '';

                });
            }
        },
        error: function() { },
        complete: function() { }
    });
}




/*
注：以上为早期版本，后面对代码进行了修改，实现的功能一样
*/




















/* Edit at 2010-01-28 
   Version:2
*/

var printSetObj = new Object();
printSetObj.TableName = '';
printSetObj.ToLocation = '';
printSetObj.BillTypeFlag = '';
printSetObj.PrintTypeFlag = '';
printSetObj.ArrayDB = new Array();


//弹出单据显示信息
function ShowSet(objDiv) {

    document.getElementById(objDiv).style.display = 'block';
    CenterToDocument(objDiv, true);
    openRotoscopingDiv(false, "divPageMask", "PageMaskIframe");
    document.getElementById('divSet').style.display = 'block';
    initSetPage(printSetObj.TableName);


}

/*关闭弹出层*/
function CloseSet(objDiv) {
    document.getElementById(objDiv).style.display = 'none';
    closeRotoscopingDiv(false, 'divPageMask');
}


/*初始化*/
function initSetPage(tableName) {
    /*加载扩展属性名称*/
    LoadSetExtTable(tableName);
    /*加载打印模板设置信息*/
    LoadSetInfo();

}


/*加载打印模板设置信息*/
function LoadSetInfo() {

    var hidIsSeted = document.getElementById('isSeted').value;
    LoadSet(hidIsSeted);
}


function LoadSet(isSeted) {

    if (isSeted == 0) {

        /*未设置过打印模板设置的，缺省选中所有*/
        for (var x = 0; x < printSetObj.ArrayDB.length; x++) {
            for (var y = 0; y < printSetObj.ArrayDB[x].length; y++) {
                document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).checked = true;
            }
        }
    }
    else {

        /*获取打印模板设置信息*/
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../../Handler/Common/PrintParameterSettingInfo.ashx?BillTypeFlag=" + printSetObj.BillTypeFlag + "&PrintTypeFlag=" + printSetObj.PrintTypeFlag,
            cache: false,
            success: function(msg) {
                if (typeof (msg.dataPrint) != 'undefined') {
                    $.each(msg.dataPrint, function(i, item) {
                        CycleData(item.BaseFields, 0);
                        if (item.DetailFields != null || item.DetailFields != '') {
                            CycleData(item.DetailFields, 1);
                        }
                        if (item.DetailSecondFields != null || item.DetailSecondFields != '') {
                            CycleData(item.DetailSecondFields, 2);
                        }
                        if (item.DetailThreeFields != null || item.DetailThreeFields != '') {
                            CycleData(item.DetailThreeFields, 3);
                        }
                        if (item.DetailFourFields != null || item.DetailFourFields != '') {
                            CycleData(item.DetailFourFields, 4);
                        }
                    }
                   );
                }
            },
            error: function() { },
            complete: function() { }
        });
    }

}

/*处理主表字段或明细字段是否选中*/
function CycleData(data,i) {
    var arrData = data.split('|');
    for (var m = 0; m < printSetObj.ArrayDB.length; m++) {
        if (m == i) {

            for (var x = 0; x < arrData.length; x++) {
                for (var n = 0; n < printSetObj.ArrayDB[m].length; n++) {
                    if (arrData[x] == printSetObj.ArrayDB[m][n]) {
                        document.getElementById('ck' + (m + 1) + printSetObj.ArrayDB[m][n]).checked = true;
                    }
                }
            }
        
        }
    }
}

/*扩展属性名称*/
function LoadSetExtTable(tableName) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx",
        cache: false,
        data: 'action=all&TableName=' + tableName,
        beforeSend: function() { },
        success: function(msg) {
            if (parseInt(msg.totalCount) > 0) {
                $.each(msg.data, function(i, item) {
                    document.getElementById('txtExtField' + (i + 1)).value = item.EFDesc + "：";
                    document.getElementById('txtExtField' + (i + 1)).style.display = '';
                    document.getElementById('ck1ExtField' + (i + 1)).style.display = '';

                });
            }
        },
        error: function() { },
        complete: function() { }
    });
}


/*保存打印模板设置*/
function SaveSet() {

    var baseExt = '';
    var baseFields = '';
    var detailFields = '';
    var detailSecondFields = '';
    var detailThreeFields = '';
    var detailFourFields = '';
    
    for (var x = 0; x < printSetObj.ArrayDB.length; x++) {
        for (var y = 0; y < printSetObj.ArrayDB[x].length; y++) {
            if (x == 0) {

                if (y < 10) {
                    if (document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).style.display == 'block' || document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).style.display == '') {

                        if (document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).checked) baseExt = baseExt + printSetObj.ArrayDB[x][y] + "|";
                    }
                }
                else {
                    if (document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).checked) baseFields = baseFields + printSetObj.ArrayDB[x][y] + "|";
                }
 
            }
            if (x == 1) {
                if (document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).checked) detailFields = detailFields + printSetObj.ArrayDB[x][y] + "|";
            }
            if (x == 2) {
                if (document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).checked) detailSecondFields = detailSecondFields + printSetObj.ArrayDB[x][y] + "|";
            }
            if (x == 3) {
                if (document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).checked) detailThreeFields = detailThreeFields + printSetObj.ArrayDB[x][y] + "|";
            }
            if (x == 4) {
                if (document.getElementById('ck' + (x + 1) + printSetObj.ArrayDB[x][y]).checked) detailFourFields = detailFourFields + printSetObj.ArrayDB[x][y] + "|";
            }
        }
    }
    
    var Action = 'Add';
    var UrlParam = "Action=" + Action + "\
                    &strBaseFields=" + baseFields + baseExt + "\
                    &strDetailFields=" + detailFields + "\
                    &strDetailSecondFields=" + detailSecondFields + "\
                    &strDetailThreeFields=" + detailThreeFields + "\
                    &strDetailFourFields=" + detailFourFields + "\
                    &BillTypeFlag=" + printSetObj.BillTypeFlag + "\
                    &PrintTypeFlag=" + printSetObj.PrintTypeFlag;
                    
    $.ajax({
        type: "POST",
        dataType: 'json',
        data: UrlParam,
        url: "../../../Handler/Common/PrintParameterSetting.ashx",
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            alert('当前的请求发生错误');
        },
        success: function(data) {
            if (data.sta > 0) {
                //alert(data.info);
                document.getElementById('div_InInfo').style.display = 'none';
                closeRotoscopingDiv(false, 'divPageMask');
                window.parent.location = printSetObj.ToLocation;
            }
            else {
                alert(data.info);
            }
        }
    });
}


