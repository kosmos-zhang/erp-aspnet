<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetExtAttributeControl.ascx.cs"
    Inherits="UserControl_Common_GetExtAttributeControl" %>
<div id="ExtDiv" style="display: none; vertical-align: top;">
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
        id="ExtTab">
        <tbody>
        </tbody>
    </table>
    <input type="hidden" id="hiddKey" />
</div>

<script language="javascript" type="text/javascript">
    //--------------------扩展属性操作----------------------------------------------------------------------------//

    /*[修改记录：]2010-01-27:
    新建页面加载调用：GetExtAttr(TableName,null),
    进入到页面查看时GetExtAttr(TableName,data),data即时具体页面的数据源,页面再声明一个函数ExtAttControl_FillValue(data)
    在此函数里显示每个页面的扩展属性。
                           
    */
    /********页面加载时获取此单据需要显示的扩展属性 ---***********/
    function GetExtAttr(TableName, data) {
        document.getElementById('hiddKey').value = '';
        ClearExtTab();
        var strKey = ''; //使用字段集合
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx", //目标地址
            cache: false,
            data: 'action=all&TableName=' + TableName,
            beforeSend: function() { }, //发送数据之前
            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //存在扩展属性显示页面扩展属性表格
                if (parseInt(msg.totalCount) > 0) {
                    $("#ExtDiv").css("display", "");
                    var ControlID = ''; //控件id
                    var Coutrol = ""; //控件的html代码
                    var Cell = ""; //列
                    var iTemp = 0;
                    $.each(msg.data, function(i, item) {
                        strKey += "|" + "ExtField" + item.EFIndex; //使用字段集合
                        ControlID = "ExtField" + item.EFIndex; //控件id
                        //控件的类型，文本框
                        if (item.EFType == '1') {
                            Coutrol = "<input id='" + ControlID + "' specialworkcheck=" + item.EFDesc + " class='tdinput' type='text' style='width: 93%;' maxlength='128' />";
                        }
                        //控件的类型，列表
                        else if (item.EFType == '2') {
                            Coutrol = "<select id='" + ControlID + "' style='width: 120px;'>"
                            Coutrol += "<option selected='selected' value=''>--请选择--</option>";
                            var arr = ("|" + item.EFValueList).split('|');
                            //添加列表的值
                            for (var y = 0; y < arr.length; y++) {
                                //不为空的列表值才添加
                                if ($.trim(arr[y]) != '') {
                                    Coutrol += "<option value='" + $.trim(arr[y]) + "'>" + $.trim(arr[y]) + "</option>";
                                }
                            }
                            Coutrol += "</select>";
                        }
                        //当itemp等于2时，为一行，此时添加新行
                        if (iTemp != 2) {
                            Cell += "<td align='right'  width='10%' bgcolor='#E6E6E6'>" + item.EFDesc + "</td>" +
                            "<td   width='23%'  bgcolor=\"#FFFFFF\" >" + Coutrol + "</td>";
                            iTemp += 1;
                        }
                        else {
                            Cell += "<td align='right'  width='10%' bgcolor='#E6E6E6'>" + item.EFDesc + "</td>" +
                            "<td  width='24%'  bgcolor=\"#FFFFFF\" >" + Coutrol + "</td>";
                            $("<tr></tr>").append("" + Cell + "").appendTo($("#ExtTab tbody"));
                            Cell = "";
                            iTemp = 0;
                        }
                    });
                    //最后一行不足六列的补齐
                    switch (iTemp) {
                        case 1:
                            $("<tr></tr>").append(
                        Cell + "<td align='right' width='10%' bgcolor='#E6E6E6'></td>"
                        + "<td  align=\"right\"  width='23%' bgcolor='#FFFFFF'></td>" + "<td align='right' width='10%' bgcolor='#E6E6E6'></td>"
                        + "<td   align=\"right\"  width='24%' bgcolor='#FFFFFF'></td>"
                        ).appendTo($("#ExtTab tbody"));
                            break;
                        case 2:
                            $("<tr></tr>").append(Cell +
                        "<td align='right'  width='10%' bgcolor='#E6E6E6'></td>"
                        + "<td  width='24%'  bgcolor='#FFFFFF'></td>"
                        ).appendTo($("#ExtTab tbody"));
                            break;
                    }
                }

                $("#hiddKey").val(strKey);
                /****************************从列表页面到编辑页面获取扩展属性值***************************************************/
                try {
                    if (data != null) {
                        //fnSetExtAttrValue(data);
                        ExtAttControl_FillValue(data);
                    }

                } catch (e) { }
            },
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误"); },
            complete: function() { } //接收数据完毕
        });
    }
    /****************************保存获取扩展属性值***************************************************/
    function GetExtAttrValue() {
        var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
        var strValue = "";
        //有扩展属性才取值
        if (strKey != '') {
            var arrKey = strKey.split('|');
            //取得扩展属性值
            for (var y = 0; y < arrKey.length; y++) {
                //不为空的字段名才取值
                if ($.trim(arrKey[y]) != '') {
                    strValue += "&" + $.trim(arrKey[y]) + "=" + escape($("#" + $.trim(arrKey[y])).val());
                }
            }
            strValue += "&keyList=" + escape(strKey);
        }
        return strValue;
    }
    /*页面在刷新时清空扩展属性table，重新填充*/
    function ClearExtTab() {
        //var signFrame = findObj("ExtTab", document);
        var signFrame = document.getElementById("ExtTab");
        var rowNum = signFrame.rows.length;
        if (rowNum > 0) {
            for (i = 0; i < rowNum; i++) {
                signFrame.deleteRow(i);
                rowNum = rowNum - 1;
                i = i - 1;
            }
        }
    }
</script>

