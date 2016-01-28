<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetGoodsInfoByBarCode.ascx.cs" Inherits="UserControl_GetGoodsInfoByBarCode" %>
<script type="text/javascript">
    var StorageID=0;
    document.onkeydown = KeyDown;
function KeyDown(event)
 {
     if(document.getElementById('ScanBarCode').style.display=="none")
        return;
        else
        {
           var evt = event ? event : (window.event ? window.event : null);
           var el;var theEvent
           var browser=IsBrowser();
           if(browser=="IE")
           {
             el = window.event.srcElement;
             theEvent = window.event;
           }
           else
           {
             el=evt.target;
             theEvent = evt;
           }
            if( el.id!= "Txt_Barcode" )
            {
               return;
            }
            else
            {
            var code = theEvent.keyCode || theEvent.which;
            if (code == 13)
            {
               GetGoodsinfo();
            }
        }
      }
 }
    //用来判断是IE或者FireFox
    //用来判断浏览器的类型。
    function IsBrowser()
    {
        var isBrowser ;
        if(window.ActiveXObject){
        isBrowser = "IE";
        }else if(window.XMLHttpRequest){
        isBrowser = "FireFox";
        }
        return isBrowser;
    }
    //弹出扫描窗口
    function GetGoodsInfoByBarCode(ID)
    {
        StorageID=ID;
        if(typeof(ID)=="undefined")
            StorageID=0;
        $("#ScanBarCode").show();
        document.getElementById("Txt_Barcode").value="";
        document.getElementById("Txt_Barcode").focus();
    }
    //根据扫描得到的条目获取商品信息
    function GetGoodsinfo()
    {
        var Barcode = $("#Txt_Barcode").val().Trim();//条码
        if(Barcode=="")
        {
            $("#ErrMsg").html("<font color='red'>商品条码不能为空</font>");
            $("#ErrMsg").show();
            document.getElementById("Txt_Barcode").focus();
            return;
        }
        var action="GetGoodsInfoByBarcode";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx",
            data: 'Action=' + escape(action) + '&Barcode=' + escape(Barcode) + '&StorageID=' + StorageID,
            dataType: 'json', //返回json格式数据
            cache: false,
            error: function() {
                document.getElementById("Txt_Barcode").value = "";
                $("#ErrMsg").show();
                $("#ErrMsg").html("<font color='red'>获取条码数据出错</font>");
                document.getElementById("Txt_Barcode").focus();
            },
            success: function(item) {
                if (item.data[0].ID != "") {
                    document.getElementById("Txt_Barcode").value = "";
                    document.getElementById("Txt_Barcode").focus();
                    try {
                        $("#ErrMsg").html("");
                        $("#ErrMsg").hide();
                        GetGoodsDataByBarCode(item.data[0].ID, item.data[0].ProdNo, item.data[0].ProductName,
                                                  item.data[0].StandardSell, item.data[0].UnitID, item.data[0].CodeName,
                                                  item.data[0].TaxRate, item.data[0].SellTax, item.data[0].Discount,
                                                  item.data[0].Specification, item.data[0].CodeTypeName, item.data[0].TypeID,
                                                  item.data[0].StandardBuy, item.data[0].TaxBuy, item.data[0].InTaxRate,
                                                  item.data[0].StandardCost, item.data[0].IsBatchNo, item.data[0].BatchNo
                                                  , item.data[0].ProductCount, item.data[0].CurrentStore, item.data[0].Source
                                                  , item.data[0].ColorName, item.data[0].MaterialName);
                    }
                    catch (Error) {

                    }
                }
                else {
                    document.getElementById("Txt_Barcode").value = "";
                    $("#ErrMsg").show();
                    $("#ErrMsg").html("<font color='red'>没找到相关条码数据</font>");
                    document.getElementById("Txt_Barcode").focus();
                }
            }
        });
    }
function CloseBarCodeDiv()
{
    $("#ErrMsg").html("");
    $("#ErrMsg").hide();
    $("#ScanBarCode").hide();
}
 </script>
<%--<input id="Button1" type="button" value="button" onclick="GetGoodsInfoByBarCode(5)" />--%>
<div id="ScanBarCode" style="border: solid 4px #999999; background: #fff;
        width: 300px; z-index: 1000; position: absolute;display:none;
        left: 60%;top:50%; margin: 5px 0 0 -400px">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" >
        <br />
        <tr>
        <td align="center" colspan="2" >
        <b>条码读取</b>
        </td>
        </tr>
        <tr>
        <td  align="center" colspan="2">
            <input id="Txt_Barcode" type="text" style="width: 252px" />
        </td>
        </tr>
        <tr>
        <td  align="center" colspan="2">
            &nbsp;<div id="ErrMsg" style="display:none">
            </div>
        </td>
        </tr>
        <tr>
        <td height="1">
        </td>
        <td align="center">
            <img alt="确认" id="Confirm_UC" src="../../../images/Button/Bottom_btn_confirm.jpg" style='cursor:pointer;'
            onclick='GetGoodsinfo();' />
         <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
            onclick='CloseBarCodeDiv();' />
        </td>
        </tr>
        </table>
</div>