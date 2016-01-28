<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubGetGoodsInfoByBarCode.ascx.cs"
    Inherits="UserControl_SubGetGoodsInfoByBarCode" %>

<script type="text/javascript">
    var SubStorageID = 0;
    $(window).keydown(function(event) {
        if (document.getElementById('SubScanBarCode').style.display == "none")
            return;
        else {
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
            if (el.id != "txtSubBarCode") {
                return;
            }
            else {
                var code = theEvent.keyCode || theEvent.which;
                if (code == 13) {
                    SubGetGoodsinfo();
                }
            }
        } 
    });
    //用来判断是IE或者FireFox
    //用来判断浏览器的类型。
    function IsBrowser() {
        var isBrowser;
        if (window.ActiveXObject) {
            isBrowser = "IE";
        } else if (window.XMLHttpRequest) {
            isBrowser = "FireFox";
        }
        return isBrowser;
    }
    //弹出扫描窗口
    function SubGetGoodsInfoByBarCode(ID) {
        SubStorageID = ID;
        if (typeof (ID) == "undefined")
            SubStorageID = 0;
        $("#SubScanBarCode").show();
        document.getElementById("txtSubBarCode").value = "";
        document.getElementById("txtSubBarCode").focus();
    }
    //根据扫描得到的条目获取商品信息
    function SubGetGoodsinfo() {
        var Barcode = $("#txtSubBarCode").val().Trim(); //条码
        if (Barcode == "") {
            $("#SubErrMsg").html("<font color='red'>商品条码不能为空</font>");
            $("#SubErrMsg").show();
            document.getElementById("txtSubBarCode").focus();
            return;
        }
        var action = "GetGoodsInfoByBarcode";
        var txtRate = document.getElementById('txtRate').value;
        var LastRate = document.getElementById('hiddenRate').value;
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SubStoreManager/ProductInfoSubUC.ashx",
            data: 'Action=' + escape(action) + '&Barcode=' + escape(Barcode) + '&Rate=' + escape(txtRate) + '&LastRate=' + escape(LastRate),
            dataType: 'json', //返回json格式数据
            cache: false,
            error: function() {
                document.getElementById("txtSubBarCode").value = "";
                $("#SubErrMsg").show();
                $("#SubErrMsg").html("<font color='red'>获取条码数据出错</font>");
                document.getElementById("txtSubBarCode").focus();
            },
            success: function(item) {
                if (item.data[0].ID != "") {
                    document.getElementById("txtSubBarCode").value = "";
                    document.getElementById("txtSubBarCode").focus();
                    try {
                        $("#SubErrMsg").html("");
                        $("#SubErrMsg").hide();
                        SubGetGoodsDataByBarCode(item.data[0].ProductID, item.data[0].ProductNo, item.data[0].ProductName,
                                                  item.data[0].UnitID, item.data[0].UnitName, item.data[0].Specification,
                                                  item.data[0].SubPriceTax, item.data[0].SubPrice, item.data[0].SubTax,
                                                  item.data[0].Discount, item.data[0].IsBatchNo);
                    }
                    catch (Error) {

                    }
                }
                else {
                    document.getElementById("txtSubBarCode").value = "";
                    $("#SubErrMsg").show();
                    $("#SubErrMsg").html("<font color='red'>没找到相关条码数据</font>");
                    document.getElementById("txtSubBarCode").focus();
                }
            }
        });
    }
    function CloseSubBarCodeDiv() {
        $("#SubErrMsg").html("");
        $("#SubErrMsg").hide();
        $("#SubScanBarCode").hide();
        $("#SubScanBarCode").val("");
    }
</script>

<%--<input id="Button1" type="button" value="button" onclick="GetGoodsInfoByBarCode(5)" />--%>
<div id="SubScanBarCode" style="border: solid 4px #999999; background: #fff; width: 300px;
    z-index: 1000; position: absolute; display: none; left: 60%; top: 50%; margin: 5px 0 0 -400px">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
        <br />
        <tr>
            <td align="center" colspan="2">
                <b>条码读取</b>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input id="txtSubBarCode" type="text" style="width: 252px" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                &nbsp;<div id="SubErrMsg" style="display: none">
                </div>
            </td>
        </tr>
        <tr>
            <td height="1">
            </td>
            <td align="center">
                <img alt="确认" id="SubConfirm" src="../../../images/Button/Bottom_btn_confirm.jpg"
                    style='cursor: pointer;' onclick='SubGetGoodsinfo();' />
                <img alt="关闭" id="SubClose" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
                    onclick='CloseSubBarCodeDiv();' />
            </td>
        </tr>
    </table>
</div>
