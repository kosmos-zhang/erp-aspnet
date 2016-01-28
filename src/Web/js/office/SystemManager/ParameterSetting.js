
/*编辑参数设置*/
/*
条码：默认启用
多计量单位：默认停用
自动生成凭证：默认停用
自动审核登帐：默认停用
超订单发/到货：默认停用
出入库是否显示价格：默认启用
允许出入库价格为零：默认停用
小数精度设置：默认2位
*/
function ParameterSetting(type, isPoint) {

    $("#td_Msg").html("");
    var StorageUsedStatus = 0;
    var BarcodeUsedStatus = 0;
    var UnitUsedStatus = 0;
    var VoucherUsedStatus = 0; //凭证
    var ApplyUsedStatus = 0; //审核
    var OverOrderUsedStatus = 0; //超订单发货
    var InOutPriceUsedStatus = 0; //出入库价格允许为零

    var SelPoint = $("#SelPoint").val();

    if (document.getElementById('dioBN1').checked) {
        StorageUsedStatus = 1;
    }
    if (document.getElementById('dioCB1').checked) {
        BarcodeUsedStatus = 1;
    }
    if (document.getElementById('dioMU1').checked) {
        UnitUsedStatus = 1;
    }
    if (document.getElementById('radOver1').checked) {
        OverOrderUsedStatus = 1;
    }
    if (document.getElementById('radvoucher1').checked) {
        VoucherUsedStatus = 1;
    }
    if (document.getElementById('radapply1').checked) {
        ApplyUsedStatus = 1;
    }
    if (document.getElementById('dioZero1').checked) {
        InOutPriceUsedStatus = 1;
    }



    var Action = 'set';
    var UrlParam = "";

    switch (parseInt(type)) {
        case 1:
            UrlParam = "action=" + Action + "&UsedStatus=" + StorageUsedStatus;
            break;
        case 2:
            UrlParam = "action=" + Action + "&UsedStatus=" + BarcodeUsedStatus;
            break;
        case 3:
            UrlParam = "action=" + Action + "&UsedStatus=" + UnitUsedStatus;
            break;
        case 5:
            UrlParam = "action=" + Action + "&UsedStatus=0&SelPoint=" + SelPoint;
            break;
        case 6:
            UrlParam = "action=" + Action + "&UsedStatus=" + VoucherUsedStatus;
            break;
        case 7:
            UrlParam = "action=" + Action + "&UsedStatus=" + ApplyUsedStatus;
            break;
        case 8:
            UrlParam = "action=" + Action + "&UsedStatus=" + OverOrderUsedStatus;
            break;
        case 9:
            UrlParam = "action=" + Action + "&UsedStatus=" + InOutPriceUsedStatus;
            break;
    }


    UrlParam += "&FunctionType=" + type;
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SystemManager/ParameterSetting.ashx?",
        data: UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            //  jAlert('请求发生异常');
        },
        success: function(msg) {
            if (msg.result) {
                popMsgObj.ShowMsg(msg.data);
            }
            else {
                popMsgObj.ShowMsg(msg.data);
            }
        }
    });
}

