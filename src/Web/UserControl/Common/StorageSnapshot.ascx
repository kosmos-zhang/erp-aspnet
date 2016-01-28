<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageSnapshot.ascx.cs"
    Inherits="UserControl_Common_StorageSnapshot" %>
<!-- Start 物品库存快照 -->
<div id="divSnapshot" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
    width: 310px; height: 200px; overflow: scroll; z-index: 1001; position: absolute;
    display: none; top: 50%; left: 70%; margin: 5px 0 0 -400px; scrollbar-face-color: #ffffff;
    scrollbar-highlight-color: #ffffff; scrollbar-shadow-color: COLOR:#000000; scrollbar-3dlight-color: #ffffff;
    scrollbar-darkshadow-color: #ffffff;">
    <table width="300">
        <tr>
            <td>
                <strong>物品库存快照</strong>
            </td>
            <td align="right">
                <img src="../../../Images/Button/closelabel.gif" onclick="document.getElementById('divSnapshot').style.display='none';closeRotoscopingDiv(false,'divPopSnapshotShadow');" />
            </td>
        </tr>
    </table>
    <table width="300" border="0" cellspacing="1" bgcolor="#CCCCCC">
        <tr>
            <td width="80" bgcolor="#EFEFEF">
                物品名称：
            </td>
            <td width="220" bgcolor="#FFFFFF" id="shotProductName">
            </td>
        </tr>
        <tr>
            <td width="80" bgcolor="#EFEFEF">
                物品编号：
            </td>
            <td width="220" bgcolor="#FFFFFF" id="shotProductNo">
            </td>
        </tr>
        <tr>
            <td width="80" bgcolor="#EFEFEF">
                安全存量：
            </td>
            <td width="220" bgcolor="#FFFFFF" id="shotSafeCount">
                0
            </td>
        </tr>
        <tr>
            <td width="80" bgcolor="#EFEFEF">
                <% if (((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString() == "True")
                   {%>基本单位<%}
                   else
                   { %>单位<%} %>：
            </td>
            <td width="220" bgcolor="#FFFFFF" id="shotUnitName">
            </td>
        </tr>
    </table>
    <br />
    <div id="divList">
    </div>
</div>
<!-- End 物品库存快照 -->
<div id="divPopSnapshotShadow" style="display: none">
    <iframe id="PopSnapshotShadowIframe" frameborder="0" width="100%"></iframe>
</div>

<script language="javascript">
    /*
    调用方法：
    ShowStorageSnapshot(productID,productName,productNo);
           
    备    注：在调用之前，需要判断是否选中一条明细，只能看一个物品的库存快照
    */

    /*显示物品库存快照*/
    function ShowStorageSnapshot(intProductID, snapProductName, snapProductNo) {
        document.getElementById('shotProductName').innerHTML = snapProductName;
        document.getElementById('shotProductNo').innerHTML = snapProductNo;

        $.ajax({
            type: "GET", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: "../../../Handler/Office/ProductionManager/MRPSnapshot.ashx?ProductID=" + intProductID, //目标地址
            cache: false,
            success: function(msg) {
            var divHtml = "<table width=\"300\" border=\"0\" cellspacing=\"1\" bgcolor=\"#CCCCCC\"><tr><td width=\"150\" bgcolor=\"#93BCDD\">仓库名称</td><td width=\"150\" bgcolor=\"#93BCDD\">批次</td><td width=\"150\" bgcolor=\"#93BCDD\">现有存量</td></tr>";
                var tmpHtml = '';
                var bgColor = '';
                var rowsCount = 0;
                //库存快照
                if (typeof (msg.dataSto) != 'undefined') {
                    $.each(msg.dataSto, function(i, item) {
                        rowsCount++;
                        if (i == 0) {
                            document.getElementById('shotProductName').innerHTML = item.ProductName;
                            document.getElementById('shotProductNo').innerHTML = item.ProductNo;
                            document.getElementById('shotSafeCount').innerHTML = item.SafeCount;
                            document.getElementById('shotUnitName').innerHTML = item.UnitName;
                        }
                        if (i % 2 == 0) {
                            bgColor = '#FFFFFF';
                        }
                        else {
                            bgColor = '#EFEFEF';
                        }
                        tmpHtml = tmpHtml + '<tr><td width=\"150\" bgcolor=\"' + bgColor + '\">' + item.StorageName + '</td><td width=\"150\" bgcolor=\"' + bgColor + '\">' + item.BatchNo + '</td><td width=\"150\" bgcolor=\"' + bgColor + '\">' + item.ProductCount + '</td></tr>';

                    });
                    divHtml = divHtml + tmpHtml + '</table>';
                    if (rowsCount > 0) {
                        document.getElementById('divList').innerHTML = divHtml;
                    }

                }
            },
            error: function() { },
            complete: function() { }
        });
        openRotoscopingDiv(false, 'divPopSnapshotShadow', 'PopSnapshotShadowIframe');
        document.getElementById('divSnapshot').style.display = 'block';
        CenterToDocument('divSnapshot', false)
    }
</script>

