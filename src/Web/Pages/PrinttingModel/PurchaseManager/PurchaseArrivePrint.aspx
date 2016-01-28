<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseArrivePrint.aspx.cs"
    Inherits="Pages_PrinttingModel_PurchaseManager_PurchaseArrivePrint" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <style type="text/css">
        @media print
        {
            .onlyShow
            {
                display: none;
            }
            .onlyPrint
            {
                border-bottom: 1px solid #000000;
                page-break-before: always;
            }
        }
    </style>
    <style type="text/css" media="print">
        .noprint
        {
            border: 0px;
        }
        .noprint2
        {
            display: none;
        }
    </style>
    <style type="text/css" id="cssID">
        .busBtn
        {
            background: url(../../../Images/default/btnbg.gif) 0px -5px;
            border: 1px solid #cccccc;
            padding-top: 2px;
            cursor: pointer;
        }
        .trTitle
        {
            text-align: left;
            vertical-align: middle;
            padding-left: 10px;
            height: 36px;
            font-size: 16px;
            border: 1px solid #000000;
        }
        .tdFirstTitleMyLove
        {
            width: 10%;
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            font-weight: bold;
        }
        .tdFirstTitle
        {
            width: 12%;
            border: 1px solid #000000;
            text-align: right;
            border-top: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
        }
        .tdTitle
        {
            border: 1px solid #000000;
            text-align: center;
            border-left: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
        }
        .tdTitle2
        {
            width: 12%;
            border: 1px solid #000000;
            text-align: right;
            border-left: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-top: none;
        }
        .tdContent
        {
            width: 10%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            padding: 8px 0px 8px 5px;
            overflow: visible;
            word-break: break-all;
            font-size: 12px;
            font-weight: bold;
        }
        .tdContent2
        {
            width: 48%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            padding: 8px 0px 8px 5px;
            overflow: visible;
            word-break: break-all;
            font-size: 12px;
        }
        .tdLastContent
        {
            width: 24%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            overflow: visible;
            word-break: break-all;
            padding: 8px 0px 8px 5px;
            font-size: 12px;
        }
        .tdColContent
        {
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            overflow: visible;
            word-break: break-all;
            padding: 8px 0px 8px 5px;
            font-size: 12px;
        }
        .tdDetail
        {
            border: 1px solid #000000;
            text-align: left;
            width: 100%;
            border-bottom: none;
            overflow: visible;
            word-break: break-all;
            padding: 5px 0px 5px 5px;
            font-size: 12px;
        }
        .tdPageLast td
        {
            border: 1px solid #000000;
            text-align: left;
            width: 100%;
            overflow: visible;
            word-break: break-all;
            padding: 5px 0px 5px 5px;
            font-size: 12px;
        }
        .trDetailFirst
        {
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-top: none;
        }
        .trDetail
        {
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-left: none;
            border-top: none;
            word-break: break-all;
        }
        .setDiv
        {
            width: 796px;
            overflow-x: auto;
            overflow-y: auto;
            height: 400px;
            scrollbar-face-color: #E7E7E7;
            scrollbar-highlight-color: #ffffff;
            scrollbar-shadow-color: COLOR:#000000;
            scrollbar-3dlight-color: #ffffff;
            scrollbar-darkshadow-color: #ffffff;
        }
    </style>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <title>采购到货</title>

    <script type="text/javascript">


        //此段js兼容ff的outerHTML，去掉后outerHTML在ff下不可用
        if (typeof (HTMLElement) != "undefined" && !window.opera) {
            HTMLElement.prototype.__defineGetter__("outerHTML", function() {
                var a = this.attributes, str = "<" + this.tagName, i = 0; for (; i < a.length; i++)
                    if (a[i].specified)
                    str += " " + a[i].name + '="' + a[i].value + '"';
                if (!this.canHaveChildren)
                    return str + " />";
                return str + ">" + this.innerHTML + "</" + this.tagName + ">";
            });
            HTMLElement.prototype.__defineSetter__("outerHTML", function(s) {
                var r = this.ownerDocument.createRange();
                r.setStartBefore(this);
                var df = r.createContextualFragment(s);
                this.parentNode.replaceChild(df, this);
                return s;
            });
            HTMLElement.prototype.__defineGetter__("canHaveChildren", function() {
                return !/^(area|base|basefont|col|frame|hr|img|br|input|isindex|link|meta|param)$/.test(this.tagName.toLowerCase());
            });
        }


        //打印的方法
        function pageSetup() {
            try {
                window.print();
            }
            catch (e) {
                alert("您的浏览器不支持此功能,请选择：文件→打印(P)…")
            }
        }

        //获取导出至excel的html的方法
        function fnGetTable() {
            var o_hid = document.getElementById("hiddExcel");
            o_hid.value = "";
            o_hid.value = o_hid.value + document.getElementById("cssID").outerHTML + document.getElementById("divMain").innerHTML;

            return true;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WB" width="0">
        </object>
        <span class="noprint2" style="text-align: center; margin-top: 4px; width: 640px;">
            <input type="button" id="print" value=" 打 印 " onclick="pageSetup();" class="busBtn" />
            <asp:Button ID="btnImport" runat="server" Text=" 导 出 " CssClass="busBtn" OnClientClick="return fnGetTable();"
                OnClick="btnImport_Click" />
            <input type="button" id="btnSet" value=" 打印模板设置 " onclick="ShowSet('div_InInfo');"
                class="busBtn" />
        </span>
        <div id="divMain" align="center">
            <table width="640px" border="0" style="font-size: 12px;">
                <tbody id="tableBase" runat="server">
                </tbody>
            </table>
            <table width="640px" border="0" cellpadding="0" cellspacing="1">
                <tbody id="tableDetail" runat="server">
                </tbody>
            </table>
        </div>
        <input type="hidden" id="hiddExcel" runat="server" />
    </div>
    <!-- Start 参数设置 -->
    <div align="center" id="div_InInfo" style="width: 70%; z-index: 100; position: absolute;
        display: none">
        <table border="0" cellspacing="1" bgcolor="#999999" style="width: 70%">
            <tr>
                <td bgcolor="#EEEEEE" align="center">
                    <table width="100%">
                        <tr>
                            <td align="left" onmousedown="MoveDiv('div_InInfo',event)" title="点击此处可以拖动窗口" onmousemove="this.style.cursor='move';"
                                style="font-size: 12px; font-weight: bold;">
                                &nbsp;&nbsp;打印模板设置
                            </td>
                            <td width="50" align="right">
                                <img src="../../../images/default/0420close.gif" onclick="CloseSet('div_InInfo');"
                                    style="cursor: hand;" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" cellspacing="1" bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF" align="center" valign="top" style="width: 90%">
                                <div id="divSet" style="display: none;" class="setDiv">
                                    <!-- Start 打印模板设置 -->
                                    <table border="0" cellspacing="1" bgcolor="#CCCCCC" style="font-size: 12px;">
                                        <tr>
                                            <td bgcolor="#FFFFFF" align="left">
                                                <table width="100%" border="0" align="left" cellspacing="0">
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            基本信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1ArriveNo" id="ck1ArriveNo" value="ArriveNo" /><input
                                                                type="text" id="txtApplyNo" value="单据编号：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1Title" id="ck1Title" value="Title" /><input type="text"
                                                                id="txtTitle" value="主题：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1FromTypeName" id="ck1FromTypeName" value="FromTypeName" /><input
                                                                type="text" id="txtAddress" value="源单类型：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1TypeIDName" id="ck1TypeIDName" value="TypeIDName" /><input
                                                                type="text" id="txtTypeName" value="采购类别：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1DeptName" id="ck1DeptName" value="DeptName" /><input
                                                                type="text" id="txtDeptName" value="部门：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1ProviderName" id="ck1ProviderName" value="ProviderName" /><input
                                                                type="text" id="txtApplyUserName" value="供应商：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1TakeTypeName" id="ck1TakeTypeName" value="TakeTypeName" /><input
                                                                type="text" id="Text4" value="交货方式：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CarryTypeName" id="ck1CarryTypeName" value="CarryTypeName" /><input
                                                                type="text" id="Text5" value="运送方式：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1PayTypeName" id="ck1PayTypeName" value="PayTypeName" /><input
                                                                type="text" id="Text6" value="结算方式：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CurrencyTypeName" id="ck1CurrencyTypeName" value="CurrencyTypeName" /><input
                                                                type="text" id="Text7" value="币种：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1Rate" id="ck1Rate" value="Rate" /><input type="text"
                                                                id="Text17" value="汇率：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1isAddTaxName" id="ck1isAddTaxName" value="isAddTaxName" /><input
                                                                type="text" id="Text11" value="是否为增值税：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CheckUserName" id="ck1CheckUserName" value="CheckUserName" /><input
                                                                type="text" id="Text24" value="点收人：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CheckDate" id="ck1CheckDate" value="CheckDate" /><input
                                                                type="text" id="txtApplyDate" value="点收日期：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1MoneyTypeName" id="ck1MoneyTypeName" value="MoneyTypeName" /><input
                                                                type="text" id="Text9" value="支付方式：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1PurchaserName" id="ck1PurchaserName" value="PurchaserName" /><input
                                                                type="text" id="txtFromTypeName" value="采购员：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1ArriveDate" id="ck1ArriveDate" value="ArriveDate" /><input
                                                                type="text" id="Text21" value="到货时间：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1SendAddress" id="ck1SendAddress" value="SendAddress" /><input
                                                                type="text" id="Text8" value="发货地址：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1ReceiveOverAddress" id="ck1ReceiveOverAddress" value="ReceiveOverAddress" /><input
                                                                type="text" id="Text10" value="收货地址：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1ProjectName" id="ck1ProjectName" value="ProjectName" /><input
                                                                type="text" id="ck1ProjectName" value="所属项目：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td align="left">
                                                                <input type="checkbox" name="ck1ExtField1" id="ck1ExtField1" value="ExtField1" style="display: none" /><input
                                                                    type="text" id="txtExtField1" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td align="left">
                                                                <input type="checkbox" name="ck1ExtField2" id="ck1ExtField2" value="ExtField2" style="display: none" /><input
                                                                    type="text" id="txtExtField2" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ck1ExtField3" id="ck1ExtField3" value="ExtField3" style="display: none" /><input
                                                                    type="text" id="txtExtField3" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td align="left">
                                                                <input type="checkbox" name="ck1ExtField4" id="ck1ExtField4" value="ExtField4" style="display: none" /><input
                                                                    type="text" id="txtExtField4" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td align="left">
                                                                <input type="checkbox" name="ck1ExtField5" id="ck1ExtField5" value="ExtField5" style="display: none" /><input
                                                                    type="text" id="txtExtField5" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ck1ExtField6" id="ck1ExtField6" value="ExtField6" style="display: none" /><input
                                                                    type="text" id="txtExtField6" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td align="left">
                                                                <input type="checkbox" name="ck1ExtField7" id="ck1ExtField7" value="ExtField7" style="display: none" /><input
                                                                    type="text" id="txtExtField7" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td align="left">
                                                                <input type="checkbox" name="ck1ExtField8" id="ck1ExtField8" value="ExtField8" style="display: none" /><input
                                                                    type="text" id="txtExtField8" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ck1ExtField9" id="ck1ExtField9" value="ExtField9" style="display: none" /><input
                                                                    type="text" id="txtExtField9" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td align="left">
                                                                <input type="checkbox" name="ck1ExtField10" id="ck1ExtField10" value="ExtField10"
                                                                    style="display: none" /><input type="text" id="txtExtField10" value="" size="20"
                                                                        style="display: none" readonly />
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            合计信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CountTotal" id="ck1CountTotal" value="CountTotal" /><input
                                                                type="text" id="txtCountTotal" value="数量总计：" size="20" readonly />
                                                        </td>
                                                        <td align="left" style="display: <%=GetIsDisplayPrice()%>">
                                                            <input type="checkbox" name="ck1TotalMoney" id="ck1TotalMoney" value="TotalMoney" /><input
                                                                type="text" id="Text1" value="金额总计：" size="20" readonly />
                                                        </td>
                                                        <td style="display: <%=GetIsDisplayPrice()%>">
                                                            <input type="checkbox" name="ck1TotalTax" id="ck1TotalTax" value="TotalTax" /><input
                                                                type="text" id="Text12" value="税额合计：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr style="display: <%=GetIsDisplayPrice()%>">
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1TotalFee" id="ck1TotalFee" value="TotalFee" /><input
                                                                type="text" id="Text13" value="含税金额总计：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1Discount" id="ck1Discount" value="Discount" /><input
                                                                type="text" id="Text14" value="整单折扣：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1DiscountTotal" id="ck1DiscountTotal" value="DiscountTotal" /><input
                                                                type="text" id="Text15" value="折扣金额：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr style="display: <%=GetIsDisplayPrice()%>">
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1RealTotal" id="ck1RealTotal" value="RealTotal" /><input
                                                                type="text" id="Text16" value="折后含税额：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1OtherTotal" id="ck1OtherTotal" value="OtherTotal" /><input
                                                                type="text" id="Text18" value="其他费用支出合计：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            备注信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CreatorName" id="ck1CreatorName" value="CreatorName" /><input
                                                                type="text" id="txtCreatorName" value="制单人：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CreateDate" id="ck1CreateDate" value="CreateDate" /><input
                                                                type="text" id="txtCreateDate" value="制单日期：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1BillStatusName" id="ck1BillStatusName" value="BillStatusName" /><input
                                                                type="text" id="txtBillStatusName" value="单据状态：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1ConfirmorName" id="ck1ConfirmorName" value="ConfirmorName" /><input
                                                                type="text" id="txtConfirmorName" value="确认人：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1ConfirmDate" id="ck1ConfirmDate" value="ConfirmDate" /><input
                                                                type="text" id="txtConfirmDate" value="确认日期：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1ModifiedUserID" id="ck1ModifiedUserID" value="ModifiedUserID" /><input
                                                                type="text" id="txtModifiedUserID" value="最后更新人：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CloserName" id="ck1CloserName" value="CloserName" /><input
                                                                type="text" id="txtCloserName" value="结单人：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1CloseDate" id="ck1CloseDate" value="CloseDate" /><input
                                                                type="text" id="txtCloseDate" value="结单日期：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1ModifiedDate" id="ck1ModifiedDate" value="ModifiedDate" /><input
                                                                type="text" id="txtModifiedDate" value="最后更新日期：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1Remark" id="ck1Remark" value="Remark" /><input type="text"
                                                                id="txtRemark" value="备注：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            采购到货明细
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table width="100%" border="0" cellspacing="1" bgcolor="#000000" id="listSetDetail">
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2SortNo" id="ck2SortNo" value="SortNo" /><input type="text"
                                                                            id="Text19" value="序号" size="4" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2ProductNo" id="ck2ProductNo" value="ProductNo" /><input
                                                                            type="text" id="Text20" value="物品编号" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2ProductName" id="ck2ProductName" value="ProductName" /><input
                                                                            type="text" id="txtDProductName" value="物品名称" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2Specification" id="ck2Specification" value="Specification" /><input
                                                                            type="text" id="txtDSpecification" value="规格" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2ColorName" id="ck2ColorName" value="ColorName" /><input
                                                                            type="text" id="txtDColorName" value="颜色" size="4" readonly />
                                                                    </td>
                                                                    <%if (IsMoreUnit)
                                                                      { %>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2UnitName" id="ck2UnitName" value="UnitName" /><input
                                                                            type="text" id="txtDUnitName" value="基本单位" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2ProductCount" id="ck2ProductCount" value="ProductCount" /><input
                                                                            type="text" id="txtDProductCount" value="基本数量" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2UsedUnitName" id="ck2UsedUnitName" value="UsedUnitName" /><input
                                                                            type="text" id="txtUsedUnitName" value="单位" size="4" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2UsedUnitCount" id="ck2UsedUnitCount" value="UsedUnitCount" /><input
                                                                            type="text" id="txtUsedUnitCount" value="到货数量" size="8" readonly />
                                                                    </td>
                                                                    <%}
                                                                      else
                                                                      { %>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2UnitName" id="ck2UnitName" value="UnitName" /><input
                                                                            type="text" id="txtDUnitName" value="单位" size="4" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2ProductCount" id="ck2ProductCount" value="ProductCount" /><input
                                                                            type="text" id="txtDProductCount" value="到货数量" size="8" readonly />
                                                                    </td>
                                                                    <%} %>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice()%>">
                                                                        <input type="checkbox" name="ck2UnitPrice" id="ck2UnitPrice" value="UnitPrice" /><input
                                                                            type="text" id="Text2" value="单价" size="4" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice()%>">
                                                                        <input type="checkbox" name="ck2TaxPrice" id="ck2TaxPrice" value="TaxPrice" /><input
                                                                            type="text" id="Text3" value="含税价" size="6" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice()%>">
                                                                        <input type="checkbox" name="ck2TaxRate" id="ck2TaxRate" value="TaxRate" /><input
                                                                            type="text" id="txtDFromBillNo" value="税率" size="4" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice()%>">
                                                                        <input type="checkbox" name="ck2TotalPrice" id="ck2TotalPrice" value="TotalPrice" /><input
                                                                            type="text" id="txtDPlanCount" value="金额" size="4" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice()%>">
                                                                        <input type="checkbox" name="ck2TotalFee" id="ck2TotalFee" value="TotalFee" /><input
                                                                            type="text" id="txtDFromLineNo" value="含税金额" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice()%>">
                                                                        <input type="checkbox" name="ck2TotalTax" id="ck2TotalTax" value="TotalTax" /><input
                                                                            type="text" id="txtDApplyReasonName" value="税额" size="4" readonly />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <input type="hidden" id="hidBillTypeFlag" name="hidBillTypeFlag" runat="server" />
                                    <input type="hidden" id="hidPrintTypeFlag" name="hidPrintTypeFlag" runat="server" />
                                    <input type="hidden" id="isSeted" value="0" runat="server" />
                                    <!-- End 打印模板设置 -->
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <input type="button" id="btnPrintSave" name="btnPrintSave" value=" 保 存 " class="busBtn"
                                    onclick="SaveSet();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divPageMask" style="display: none">
        <iframe id="PageMaskIframe" frameborder="0" width="100%"></iframe>
    </div>
    <!-- End 参数设置-->
    </form>
    <p>
</body>
</html>

<script src="../../../js/common/PrintParameterSetting.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">

    printSetObj.BillTypeFlag = document.getElementById('hidBillTypeFlag').value;
    printSetObj.PrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
    /*跳转页面*/
    printSetObj.ToLocation ='PurchaseArrivePrint.aspx?ID=' + <%=intMrpID %>;
    /*表名称*/
    printSetObj.TableName = 'officedba.PurchaseArrive';
    
    /*取基本信息及明细信息的字段*/
    printSetObj.ArrayDB = new Array([<%=SBase %>],[<%=SDetail %>]);
    
</script>

