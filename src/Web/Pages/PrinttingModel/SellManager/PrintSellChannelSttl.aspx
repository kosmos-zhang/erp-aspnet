<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintSellChannelSttl.aspx.cs" Inherits="Pages_PrinttingModel_SellManager_PrintSellChannelSttl" 
    ValidateRequest="false" %>

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

    <title>委托代销结算单</title>

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
            <input type="button" id="btnSet" value=" 打印模板设置 " onclick="ShowPrintSetting();" class="busBtn" />
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
                                <img src="../../../images/default/0420close.gif" onclick='ClosePrintSetting();' style="cursor: hand;" />
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
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSttlNo" id="ckSttlNo" value="SttlNo" /><input type="text"
                                                                id="txtSttlNo" value="单据编号：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckTitle" id="ckTitle" value="Title" /><input type="text"
                                                                id="txtTitle" value="主题：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckFromTypeText" id="ckFromTypeText" value="FromTypeText" /><input
                                                                type="text" id="txtFromTypeText" value="源单类型：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSendNo" id="ckSendNo" value="SendNo" /><input
                                                                type="text" id="txtSendNo" value="源单编号：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCustName" id="ckCustName" value="CustName" /><input type="text"
                                                                id="txtCustName" value="客户名称：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckTel" id="ckTel" value="Tel" /><input
                                                                type="text" id="txtTel" value="客户电话：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckDeptName" id="ckDeptName" value="DeptName" /><input type="text"
                                                                id="txtDeptName" value="销售部门：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSellerName" id="ckSellerName" value="SellerName" /><input
                                                                type="text" id="txtSellerName" value="业务员：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckPayTypeName" id="ckPayTypeName" value="PayTypeName" /><input
                                                                type="text" id="txtPayTypeName" value="结算方式：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMoneyTypeName" id="ckMoneyTypeName" value="MoneyTypeName" /><input
                                                                type="text" id="txtMoneyTypeName" value="支付方式：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCurrencyName" id="ckCurrencyName" value="CurrencyName" /><input type="text"
                                                                id="txtCurrencyName" value="币种：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckRate" id="ckRate" value="Rate" /><input
                                                                type="text" id="txtRate" value="汇率：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSttlDate" id="ckSttlDate" value="SttlDate" /><input
                                                                type="text" id="txtSttlDate" value="结算日期：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckExtField1" id="ckExtField1" value="ExtField1" style="display: none" /><input
                                                                type="text" id="txtExtField1" value="" size="20" style="display: none" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            
                                                        </td>
                                                    </tr>
                                                    <!--START 扩展属性-->
                                                    <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField2" id="ckExtField2" value="ExtField2" style="display: none" /><input
                                                                    type="text" id="txtExtField2" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField3" id="ckExtField3" value="ExtField3" style="display: none" /><input
                                                                    type="text" id="txtExtField3" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField4" id="ckExtField4" value="ExtField4" style="display: none" /><input
                                                                    type="text" id="txtExtField4" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField5" id="ckExtField5" value="ExtField5" style="display: none" /><input
                                                                    type="text" id="txtExtField5" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField6" id="ckExtField6" value="ExtField6" style="display: none" /><input
                                                                    type="text" id="txtExtField6" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField7" id="ckExtField7" value="ExtField7" style="display: none" /><input
                                                                    type="text" id="txtExtField7" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="ckExtField8" value="ExtField8" style="display: none" /><input
                                                                    type="text" id="txtExtField8" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="ckExtField9" value="ExtField9" style="display: none" /><input
                                                                    type="text" id="txtExtField9" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="ckExtField10" value="ExtField10" style="display: none" /><input
                                                                    type="text" id="txtExtField10" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <!--END 扩展属性-->
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            结算信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCountTotal" id="ckCountTotal" value="CountTotal" /><input
                                                                type="text" id="txtCountTotal" value="代销数量合计：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckTotalFee" id="ckTotalFee" value="TotalFee" /><input
                                                                type="text" id="txtTotalFee" value="代销金额合计：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckPushMoneyPercent" id="ckPushMoneyPercent" value="PushMoneyPercent" /><input
                                                                type="text" id="txtPushMoneyPercent" value="代销提成率(%)：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckPushMoney" id="ckPushMoney" value="PushMoney" /><input
                                                                type="text" id="txtPushMoney" value="代销提成额：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckHandFeeTotal" id="ckHandFeeTotal" value="HandFeeTotal" /><input
                                                                type="text" id="txtHandFeeTotal" value="手续费合计：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckSttlTotal" id="ckSttlTotal" value="SttlTotal" /><input
                                                                type="text" id="txtSttlTotal" value="应结算金额合计：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            相关单据状态
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckIsOpenbillText" id="ckIsOpenbillText" value="IsOpenbillText" /><input type="text"
                                                                id="txtIsOpenbillText" value="建单情况：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckIsAccText" id="ckIsAccText" value="IsAccText" /><input type="text"
                                                                id="txtIsAccText" value="结算状态：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckYAccounts" id="ckYAccounts" value="YAccounts" /><input type="text"
                                                                id="txtYAccounts" value="已结算金额：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckBillStatusText" id="ckBillStatusText" value="BillStatusText" /><input
                                                                type="text" id="txtBillStatusText" value="单据状态：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCreatorName" id="ckCreatorName" value="CreatorName" /><input
                                                                type="text" id="txtCreatorName" value="制单人：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCreateDate" id="ckCreateDate" value="CreateDate" /><input
                                                                type="text" id="txtCreateDate" value="制单日期：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmorName" id="ckConfirmorName" value="ConfirmorName" /><input
                                                                type="text" id="txtConfirmorName" value="确认人：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmDate" id="ckConfirmDate" value="ConfirmDate" /><input
                                                                type="text" id="txtConfirmDate" value="确认日期：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCloserName" id="ckCloserName" value="CloserName" /><input
                                                                type="text" id="txtCloserName" value="结单人：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ckCloseDate" value="CloseDate" /><input
                                                                type="text" id="txtCloseDate" value="结单日期：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckModifiedUserID" id="ckModifiedUserID" value="ModifiedUserID" /><input
                                                                type="text" id="txtModifiedUserID" value="最后更新人：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckModifiedDate" id="ckModifiedDate" value="ModifiedDate" /><input
                                                                type="text" id="txtModifiedDate" value="最后更新日期：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckRemark" id="ckRemark" value="Remark" /><input type="text"
                                                                id="txtRemark" value="备注：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            代销结算单明细
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table width="100%" border="0" cellspacing="1" id="listSetDetail">
                                                                <tr>
                                                                    <%--<td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdSortNo" id="ckdSortNo" value="SortNo" /><input type="text"
                                                                            id="txtDSortNo" value="序号" size="4" />
                                                                    </td>--%>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdProdNo" id="ckdProdNo" value="ProdNo" /><input type="text"
                                                                            id="txtDProductNo" value="物品编号" size="12" readonly="readonly" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdProductName" id="ckdProductName" value="ProductName" /><input
                                                                            type="text" id="txtDProductName" value="物品名称" size="12" readonly="readonly" />
                                                                    </td>
                                                                    <%if (UserInfo.IsMoreUnit.ToString().Trim() == "True")
                                                                      {%>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdUnitName" id="ckdUnitName" value="UnitName" /><input
                                                                                type="text" id="txtDUnitName" value="基本单位" size="12" readonly="readonly" />
                                                                        </td>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdSttlNumber" id="ckdSttlNumber" value="SttlNumber" /><input
                                                                                type="text" id="txtDSttlNumber" value="基本数量" size="12" readonly="readonly" />
                                                                        </td>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdBaseUnitName" id="ckdUsedUnitName" value="UsedUnitName" /><input readonly="readonly" 
                                                                                type="text" id="txtUsedUnitName" value="单位" size="12" />
                                                                        </td>
                                                                      <%}
                                                                      else
                                                                      {%>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdUnitName" id="ckdUnitName" value="UnitName" /><input
                                                                                type="text" id="txtDUnitName" value="单位" size="12" readonly="readonly" />
                                                                        </td>
                                                                      <%} %>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdProductCount" id="ckdProductCount" value="ProductCount" /><input
                                                                            type="text" id="txtDProductCount" value="代销数量" size="10" readonly="readonly" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdSpecification" id="ckdSpecification" value="Specification" /><input
                                                                            type="text" id="txtDSpecification" value="规格" size="7" readonly="readonly" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdSttlCount" id="ckdSttlCount" value="SttlCount" /><input
                                                                            type="text" id="txtDSttlCount" value="已结算数量" size="10" readonly="readonly" />
                                                                    </td>
                                                                    <%if (UserInfo.IsMoreUnit.ToString().Trim() == "True")
                                                                      {%>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdUsedUnitCount" id="ckdUsedUnitCount" value="UsedUnitCount" /><input
                                                                                type="text" id="txtDUsedUnitCount" value="本次结算数量" size="10" readonly="readonly" />
                                                                        </td>
                                                                    <%}
                                                                      else
                                                                      { %>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdSttlNumber" id="ckdSttlNumber" value="SttlNumber" /><input
                                                                                type="text" id="txtDSttlNumber" value="本次结算数量" size="10" readonly="readonly" />
                                                                        </td>
                                                                    <%} %>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdUnitPrice" id="ckdUnitPrice" value="UnitPrice"  /><input
                                                                            type="text" id="txtDUnitPrice" value="单价" size="10" readonly="readonly" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdTotalPrice" id="ckdTotalPrice" value="TotalPrice" /><input
                                                                            type="text" id="txtDTotalPrice" value="本次结算代销金额" size="14" readonly="readonly" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdRemark" id="ckdRemark" value="Remark" /><input
                                                                            type="text" id="txtDRemark" value="备注" size="5" readonly="readonly" />
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
                                    <input type="hidden" id="hiddckSellSttlNo" runat="server" />
                                    <!-- End 打印模板设置 -->
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <input type="button" id="btnPrintSave" name="btnPrintSave" value=" 保 存 " class="busBtn"
                                    onclick="SavePrintSetting();" />
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

<script language="javascript">
    var sellSttlNo = $("#hiddckSellSttlNo").val();
    var glb_IsMoreUnit = '<%=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString() %>';/*是否启用多计量单位*/
    //弹出单据显示信息
    function ShowPrintSetting() {
        
        document.getElementById('div_InInfo').style.display = 'block';
        CenterToDocument("div_InInfo", true);
        openRotoscopingDiv(false, "divPageMask", "PageMaskIframe");
        document.getElementById('divSet').style.display='block';
        initPage();
    }
    
    /*关闭弹出层*/
    function ClosePrintSetting() {
        document.getElementById('div_InInfo').style.display = 'none';
        closeRotoscopingDiv(false, 'divPageMask');
    } 
    
    /*初始化*/
    function initPage()
    {
        /*加载扩展属性名称*/
        LoadExtTableName('officedba.SellChannelSttl');
        /*加载打印模板设置信息*/
        LoadPrintSettingInfo();
    }

    /*2:加载打印模板设置信息*/
    function LoadPrintSettingInfo() {

        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        var hidIsSeted = document.getElementById('isSeted').value;

        /*主表：复选框及其对应的字段*/
        var dbBase = new Array( 
                                ['ckSttlNo', 'SttlNo'],
                                ['ckTitle', 'Title'],
                                ['ckFromTypeText', 'FromTypeText'],
                                ['ckSendNo', 'SendNo'],
                                ['ckCustName', 'CustName'],
                                ['ckTel', 'Tel'],
                                ['ckPayTypeName', 'PayTypeName'],
                                ['ckMoneyTypeName', 'MoneyTypeName'],
                                ['ckCurrencyName', 'CurrencyName'],
                                ['ckRate', 'Rate'],
                                ['ckSellerName', 'SellerName'],
                                ['ckDeptName', 'DeptName'],
                                ['ckSttlDate', 'SttlDate'],
                                ['ckTotalFee', 'TotalFee'],
                                ['ckPushMoneyPercent', 'PushMoneyPercent'],
                                ['ckPushMoney', 'PushMoney'],
                                ['ckHandFeeTotal', 'HandFeeTotal'],
                                ['ckSttlTotal', 'SttlTotal'],
                                ['ckCountTotal', 'CountTotal'],
                                ['ckBillStatusText', 'BillStatusText'],
                                ['ckCreatorName', 'CreatorName'],
                                ['ckCreateDate', 'CreateDate'],
                                ['ckConfirmorName', 'ConfirmorName'],
                                ['ckConfirmDate', 'ConfirmDate'],
                                ['ckCloserName', 'CloserName'],
                                ['ckCloseDate', 'CloseDate'],
                                ['ckModifiedUserID', 'ModifiedUserID'],
                                ['ckModifiedDate', 'ModifiedDate'],
                                ['ckRemark', 'Remark'],
                                ['ckIsOpenbillText', 'IsOpenbillText'],
                                ['ckIsAccText', 'IsAccText'],
                                ['ckYAccounts', 'YAccounts'],
                                ['ckExtField1', 'ExtField1'],
                                ['ckExtField2', 'ExtField2'],
                                ['ckExtField3', 'ExtField3'],
                                ['ckExtField4', 'ExtField4'],
                                ['ckExtField5', 'ExtField5'],
                                ['ckExtField6', 'ExtField6'],
                                ['ckExtField7', 'ExtField7'],
                                ['ckExtField8', 'ExtField8'],
                                ['ckExtField9', 'ExtField9'],
                                ['ckExtField10', 'ExtField10']
                                );
        if(glb_IsMoreUnit=="True")
        {
            /*明细表：复选框及其对应的字段*/
            var dbDetail = new Array(//['ckdSortNo', 'SortNo'],
                                     ['ckdProdNo', 'ProdNo'],
                                     ['ckdProductName', 'ProductName'],
                                     ['ckdSpecification', 'Specification'],
                                     ['ckdUnitName', 'UnitName'],
                                     ['ckdUsedUnitName', 'UsedUnitName'],
                                     ['ckdUsedUnitCount', 'UsedUnitCount'],
                                     ['ckdProductCount', 'ProductCount'],
                                     ['ckdSttlCount', 'SttlCount'],
                                     ['ckdSttlNumber', 'SttlNumber'],
                                     ['ckdUnitPrice', 'UsedPrice'],
                                     ['ckdTotalPrice', 'TotalPrice'],
                                     ['ckdRemark', 'Remark']);                                 

            /*加载打印参数设置
              注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
            */
            LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase, dbDetail,null);            
        }
        else
        {
            /*明细表：复选框及其对应的字段*/
            var dbDetail = new Array(//['ckdSortNo', 'SortNo'],
                                     ['ckdProdNo', 'ProdNo'],
                                     ['ckdProductName', 'ProductName'],
                                     ['ckdSpecification', 'Specification'],
                                     ['ckdUnitName', 'UnitName'],
                                     ['ckdProductCount', 'ProductCount'],
                                     ['ckdSttlCount', 'SttlCount'],
                                     ['ckdSttlNumber', 'SttlNumber'],
                                     ['ckdUnitPrice', 'UnitPrice'],
                                     ['ckdTotalPrice', 'TotalPrice'],
                                     ['ckdRemark', 'Remark']);                                 

            /*加载打印参数设置
              注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
            */
            LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase, dbDetail,null);
        }
        
    }

    /*3:保存打印模板设置*/
    function SavePrintSetting() {

        
        var strBaseFields = "";
        var strDetailFields = "";
        var toLocation='PrintSellChannelSttl.aspx?no=' + sellSttlNo;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        if (document.getElementById('ckSttlNo').checked) strBaseFields = strBaseFields + "SttlNo|";
        if (document.getElementById('ckTitle').checked) strBaseFields = strBaseFields + "Title|";
        if (document.getElementById('ckFromTypeText').checked) strBaseFields = strBaseFields + "FromTypeText|";
        if (document.getElementById('ckSendNo').checked) strBaseFields = strBaseFields + "SendNo|";
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";
        if (document.getElementById('ckTel').checked) strBaseFields = strBaseFields + "Tel|";
        if (document.getElementById('ckPayTypeName').checked) strBaseFields = strBaseFields + "PayTypeName|";
        if (document.getElementById('ckMoneyTypeName').checked) strBaseFields = strBaseFields + "MoneyTypeName|";
        if (document.getElementById('ckCurrencyName').checked) strBaseFields = strBaseFields + "CurrencyName|";
        if (document.getElementById('ckRate').checked) strBaseFields = strBaseFields + "Rate|";
        if (document.getElementById('ckSellerName').checked) strBaseFields = strBaseFields + "SellerName|";
        if (document.getElementById('ckDeptName').checked) strBaseFields = strBaseFields + "DeptName|";
        if (document.getElementById('ckSttlDate').checked) strBaseFields = strBaseFields + "SttlDate|";
        if (document.getElementById('ckTotalFee').checked) strBaseFields = strBaseFields + "TotalFee|";
        if (document.getElementById('ckPushMoneyPercent').checked) strBaseFields = strBaseFields + "PushMoneyPercent|";
        if (document.getElementById('ckPushMoney').checked) strBaseFields = strBaseFields + "PushMoney|";
        if (document.getElementById('ckHandFeeTotal').checked) strBaseFields = strBaseFields + "HandFeeTotal|";
        if (document.getElementById('ckSttlTotal').checked) strBaseFields = strBaseFields + "SttlTotal|";
        if (document.getElementById('ckCountTotal').checked) strBaseFields = strBaseFields + "CountTotal|";
        if (document.getElementById('ckBillStatusText').checked) strBaseFields = strBaseFields + "BillStatusText|";
        if (document.getElementById('ckCreatorName').checked) strBaseFields = strBaseFields + "CreatorName|";
        if (document.getElementById('ckCreateDate').checked) strBaseFields = strBaseFields + "CreateDate|";
        if (document.getElementById('ckConfirmorName').checked) strBaseFields = strBaseFields + "ConfirmorName|";
        if (document.getElementById('ckConfirmDate').checked) strBaseFields = strBaseFields + "ConfirmDate|";
        if (document.getElementById('ckCloserName').checked) strBaseFields = strBaseFields + "CloserName|";
        if (document.getElementById('ckCloseDate').checked) strBaseFields = strBaseFields + "CloseDate|";
        if (document.getElementById('ckModifiedUserID').checked) strBaseFields = strBaseFields + "ModifiedUserID|";
        if (document.getElementById('ckModifiedDate').checked) strBaseFields = strBaseFields + "ModifiedDate|";
        if (document.getElementById('ckRemark').checked) strBaseFields = strBaseFields + "Remark|";
        if (document.getElementById('ckIsOpenbillText').checked) strBaseFields = strBaseFields + "IsOpenbillText|";
        if (document.getElementById('ckIsAccText').checked) strBaseFields = strBaseFields + "IsAccText|";
        if (document.getElementById('ckYAccounts').checked) strBaseFields = strBaseFields + "YAccounts|";
        for(var i=0;i<10;i++)
        {
            if(document.getElementById('ckExtField'+(i+1)).style.display=='block'||document.getElementById('ckExtField'+(i+1)).style.display=='')
            {
                if (document.getElementById('ckExtField'+(i+1)).checked) strBaseFields = strBaseFields + "ExtField"+(i+1)+"|";
            }
            
        }
        /*明细信息*/
       // if (document.getElementById('ckdSortNo').checked) strDetailFields = strDetailFields + "SortNo|";
        if (document.getElementById('ckdProdNo').checked) strDetailFields = strDetailFields + "ProdNo|";
        if (document.getElementById('ckdProductName').checked) strDetailFields = strDetailFields + "ProductName|";
        if (document.getElementById('ckdSpecification').checked) strDetailFields = strDetailFields + "Specification|";
        if (document.getElementById('ckdProductCount').checked) strDetailFields = strDetailFields + "ProductCount|";
        if (document.getElementById('ckdSttlCount').checked) strDetailFields = strDetailFields + "SttlCount|";
        
        //if (document.getElementById('ckdUnitName').checked) strDetailFields = strDetailFields + "UnitName|";
        //if (document.getElementById('ckdSttlNumber').checked) strDetailFields = strDetailFields + "SttlNumber|";
        //多计量单位
        if(glb_IsMoreUnit=="True")
        {
            if (document.getElementById('ckdUnitName').checked) strDetailFields = strDetailFields + "UnitName|";
            if (document.getElementById('ckdSttlNumber').checked) strDetailFields = strDetailFields + "SttlNumber|";  
            if (document.getElementById('ckdUsedUnitName').checked) strDetailFields = strDetailFields + "UsedUnitName|";
            if (document.getElementById('ckdUsedUnitCount').checked) strDetailFields = strDetailFields + "UsedUnitCount|";
            if (document.getElementById('ckdUnitPrice').checked) strDetailFields = strDetailFields + "UsedPrice|";            
        }
        else
        {
            if (document.getElementById('ckdUnitName').checked) strDetailFields = strDetailFields + "UnitName|";
            if (document.getElementById('ckdSttlNumber').checked) strDetailFields = strDetailFields + "SttlNumber|"; 
            if (document.getElementById('ckdUnitPrice').checked) strDetailFields = strDetailFields + "UnitPrice|";            
        } 
        
        if (document.getElementById('ckdTotalPrice').checked) strDetailFields = strDetailFields + "TotalPrice|";
        if (document.getElementById('ckdRemark').checked) strDetailFields = strDetailFields + "Remark|";
        /*保存打印参数设置*/
        SaveCommonPrintParameterSet(strBaseFields,strDetailFields,"",hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }  
    
</script>
