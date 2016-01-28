<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInpurchasePrint1.aspx.cs"
    Inherits="Pages_PrinttingModel_StorageManager_StorageInpurchasePrint1" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <link href="../../../css/PrintCss.css" rel="stylesheet" type="text/css" />
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
            width: 14%;
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
            width: 12%;
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
        }
    </style>
    <title>采购入库单</title>

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
        <span class="onlyShow" style="text-align: center; margin-top: 4px; width: 640px;">
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
      display: none "><%-- --%>
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
                                <%----%><div id="divSet" style="display: none; width: 796px; overflow-x: auto; overflow-y: auto;
                                    height: 400px; scrollbar-face-color: #E7E7E7; scrollbar-highlight-color: #ffffff;
                                    scrollbar-shadow-color: COLOR:#000000; scrollbar-3dlight-color: #ffffff; scrollbar-darkshadow-color: #ffffff;">
                                    <!-- Start 打印模板设置 -->
                                    <table border="0" cellspacing="1" bgcolor="#CCCCCC" style="font-size: 12px;">
                                        <tr>
                                            <td bgcolor="#FFFFFF" align="left">
                                                <table width="100%" border="0" align="center" cellspacing="0">
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            基本信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckInNo" value="InNo" />
                                                            <input type="text" id="txtInNo" readonly value="入库单编号：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckTitle" value="Title" />
                                                            <input type="text" id="txtTitle" value="入库单主题：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ckFromTypeName" value="FromTypeName" />
                                                            <input type="text" id="txtFromTypeName" value="源单类型：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckArriveNo" value="" />
                                                            <input type="text" id="txtArriveNo" value="采购到货单：" readonly size="10" />
                                                        </td>
                                                        <td width="20%">
                                                            <input type="checkbox" id="ckProviderName" value="" />
                                                            <input type="text" id="txtProviderName" value="供应商：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ckPurchaserName" value="" />
                                                            <input type="text" id="txtPurchaserName" value="采购员：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckDeptName" value="" />
                                                            <input type="text" id="txtDeptName" value="采购部门：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckTakerName" value="" />
                                                            <input type="text" id="txtTakerName" value="交货人：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ckCheckerName" value="" />
                                                            <input type="text" id="txtCheckerName" value="验收人：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckInPutDeptName" value="" />
                                                            <input type="text" id="txtInPutDeptName" value="入库部门：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckExecutorName" value="" />
                                                            <input type="text" id="txtExecutorName" value="入库人：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ckEnterDate" value="" />
                                                            <input type="text" id="txtEnterDate" value="入库时间：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckSummary" value="" />
                                                            <input type="text" id="txtSummary" value="摘要：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckUsertxtExecutor" id="ckBatchNo" value="CustName" />
                                                            <input type="text" id="txtBatchNo" value="批次：" size="10" readonly />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField1" id="ckExtField1" value="ExtField1" style="display: none" />
                                                                <input type="text" id="txtExtField1" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField2" id="ckExtField2" value="ExtField2" style="display: none" />
                                                                <input type="text" id="txtExtField2" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField3" id="ckExtField3" value="ExtField3" style="display: none" />
                                                                <input type="text" id="txtExtField3" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField4" id="ckExtField4" value="ExtField4" style="display: none" />
                                                                <input type="text" id="txtExtField4" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField5" id="ckExtField5" value="ExtField5" style="display: none" />
                                                                <input type="text" id="txtExtField5" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField6" id="ckExtField6" value="ExtField6" style="display: none" />
                                                                <input type="text" id="txtExtField6" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField7" id="ckExtField7" value="ExtField7" style="display: none" />
                                                                <input type="text" id="txtExtField7" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="ckExtField8" value="ExtField8" style="display: none" />
                                                                <input type="text" id="txtExtField8" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField9" id="ckExtField9" value="ExtField9" style="display: none" />
                                                                <input type="text" id="txtExtField9" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField10" id="ckExtField10" value="ExtField10" style="display: none" />
                                                                <input type="text" id="txtExtField10" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
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
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckCountTotal" value="" />
                                                            <input type="text" id="txtCountTotal" value="数量合计：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left" style="display: <%=GetIsDisplayPrice() %>">
                                                            <input type="checkbox" id="ckB_TotalPrice" value="" />
                                                            <input type="text" id="txtB_TotalPrice" value="金额合计：" readonly size="10" />
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
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckBillStatus" value="" />
                                                            <input type="text" id="txtBillStatus" value="单据状态：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ckCreatorReal" value="" />
                                                            <input type="text" id="txtCreatorReal" value="制单人：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCreateDate" id="ckCreateDate" value="CreateDate" />
                                                            <input type="text" id="txtCreateDate" value="制单日期：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmorReal" id="ckConfirmorReal" value="ConfirmorReal" />
                                                            <input type="text" id="txtConfirmorReal" value="确认人：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmDate" id="ckConfirmDate" value="ConfirmDate" />
                                                            <input type="text" id="txtConfirmDate" value="确认日期：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCloserReal" id="ckCloserReal" value="CloserReal" />
                                                            <input type="text" id="txtCloserReal" value="结单人：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ckCloseDate" value="CloseDate" />
                                                            <input type="text" id="txtCloseDate" value="结单日期：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckModifiedUserID" id="ckModifiedUserID" value="ModifiedUserID" />
                                                            <input type="text" id="txtModifiedUserID" value="最后更新人：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckModifiedDate" id="ckModifiedDate" value="ModifiedDate" />
                                                            <input type="text" id="txtModifiedDate" value="最后更新日期：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckRemark" id="ckRemark" value="Remark" />
                                                            <input type="text" id="txtRemark" value="备注：" readonly size="10" />
                                                        </td>
                                                        <td width="20%">
                                                        
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            明细信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="100%" border="0" cellspacing="1" bgcolor="#000000" id="pageDataList1">
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckSortNo" value="" /><input type="text" id="txtSortNo"
                                                                            value="序号" size="5" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckProductNo" value="" /><input type="text" id="txtProductNo"
                                                                            value="物品编号" readonly size="10" />
                                                                    </td>                                                                    
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckProductName" value="ProductName" /><input type="text"
                                                                            id="txtProductName" value="物品名称" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckColorName" value="" /><input type="text" id="txtColorName"
                                                                            value="颜色" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckSpecification" value="Type" /><input type="text" id="txtSpecification"
                                                                            value="规格" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckUnitID" value="" /><input type="text" id="txtUnitID"
                                                                            value="基本单位" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" id="td_UsedUnitName" runat="server">
                                                                        <input type="checkbox" name="ckdUnitName" id="ckUsedUnitName" value="UnitName" /><input
                                                                            type="text" id="txtUsedUnitName" value="单位" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckStorageName" value="" /><input type="text" id="txtStorageName"
                                                                            value="仓库" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckFromBillCount" value="" /><input type="text" id="txtFromBillCount"
                                                                            value="通知数量" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckInCount" value="" /><input type="text" id="txtInCount"
                                                                            value="已入库数量" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckNotInCount" value="" /><input type="text" id="txtNotInCount"
                                                                            value="未入库数量" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckProductCount" /><input type="text" id="txtProductCount"
                                                                            value="基本数量" readonly size="10" />
                                                                    </td>
                                                                     <td bgcolor="#FFFFFF" id="td_UsedUnitCount" runat="server">
                                                                        <input type="checkbox" name="ckdUnitName" id="ckUsedUnitCount" value="UnitName" /><input
                                                                            type="text" id="txtUsedUnitCount" value="数量" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice() %>">
                                                                        <input type="checkbox" id="ckUnitPrice" value="" /><input type="text" id="txtUnitPrice"
                                                                            value="单价" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice() %>">
                                                                        <input type="checkbox" id="ckTaxRate" value="" /><input type="text" id="txtTaxRate"
                                                                            value="税率" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice() %>">
                                                                        <input type="checkbox" id="ckTotalPrice" value="" /><input type="text" id="txtTotalPrice"
                                                                            value="金额" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ckBackCount" value="" /><input type="text" id="txtBackCount"
                                                                            value="已退货数量" readonly size="10" />
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
                                    <input type="hidden" id="HiddenMoreUnit" runat="server" /><!--是否显示多计量单位-->
                                    <!-- End 打印模板设置 -->
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center">
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

<script type="text/javascript">
    var intMrpID = <%=intMrpID %>;
    //弹出单据显示信息
    function ShowPrintSetting() {
        
        document.getElementById('div_InInfo').style.display = 'block';
        CenterToDocument("div_InInfo", true);
        openRotoscopingDiv(false, "divPageMask", "PageMaskIframe");
        document.getElementById('divSet').style.display='block';
        LoadPrintSettingInfo();
        //LoadBillData();
        LoadExtTableName('officedba.StorageInPurchase');

    }

    function ClosePrintSetting() {
        document.getElementById('div_InInfo').style.display = 'none';
        closeRotoscopingDiv(false, 'divPageMask');
    } 
    /*加载打印模板设置信息*/
    function LoadPrintSettingInfo() {

        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        var hidIsSeted = document.getElementById('isSeted').value;



        /*主表：复选框及其对应的字段*/
        var dbBase = new Array();
        var dbDetail = new Array
        var IsDisplayPrice = '<%=GetIsDisplayPrice() %>';
    if(IsDisplayPrice!="none")
    {
         dbBase = new Array(
                                ['ckExtField1', 'ExtField1'],
                                ['ckExtField2', 'ExtField2'],
                                ['ckExtField3', 'ExtField3'],
                                ['ckExtField4', 'ExtField4'],
                                ['ckExtField5', 'ExtField5'],
                                ['ckExtField6', 'ExtField6'],
                                ['ckExtField7', 'ExtField7'],
                                ['ckExtField8', 'ExtField8'],
                                ['ckExtField9', 'ExtField9'],
                                ['ckExtField10', 'ExtField10'],
                                ['ckInNo', 'InNo'],//出库单编号
                                ['ckTitle', 'Title'],//主题
                                ['ckFromTypeName', 'FromTypeName'],//源单类型
                                ['ckArriveNo', 'ArriveNo'],//源单编号
                                ['ckProviderName', 'ProviderName'],//供应商
                                ['ckPurchaserName', 'PurchaserName'],//采购员
                                ['ckDeptName', 'DeptName'],//采购部门
                                ['ckTakerName', 'TakerName'],//交货人
                                ['ckCheckerName', 'CheckerName'],//验收人
                                ['ckInPutDeptName', 'InPutDeptName'],//入库部门
                                ['ckExecutorName', 'ExecutorName'],//入库人
                                ['ckEnterDate', 'EnterDate'],//入库时间
                                ['ckRemark', 'Remark'],//备注
                                ['ckSummary', 'Summary'],//摘要
                                ['ckBatchNo', 'BatchNo'],//批次
                                ['ckCountTotal', 'CountTotal'],//
                                ['ckB_TotalPrice', 'B_TotalPrice'],//金额合计
                                ['ckCreatorReal', 'CreatorName'],//制单人
                                ['ckCreateDate', 'CreateDate'],//制单日期
                                ['ckConfirmorReal', 'ConfirmorName'],//确认人
                                ['ckConfirmDate', 'ConfirmDate'],//确认日期
                                ['ckCloserReal', 'CloserName'],//结单人
                                ['ckCloseDate', 'CloseDate'],//结单日期
                                ['ckModifiedUserID', 'ModifiedUserName'],//最后更新人
                                ['ckModifiedDate', 'ModifiedDate'],//最后更新日期
                                ['ckBillStatus', 'BillStatus']);//单据状态
                                
        /*明细表：复选框及其对应的字段*/
        if(document.getElementById("HiddenMoreUnit").value == "true")
        {
             dbDetail = new Array(['ckSortNo', 'SortNo'],//序号
                                 ['ckProductNo', 'ProductNo'],//物品编号
                                 ['ckProductName', 'ProductName'],//物品名称
                                 ['ckColorName', 'ColorName'],//颜色
                                 ['ckSpecification', 'Specification'],//规格
                                 ['ckUnitID', 'UnitID'],//单位
                                 ['ckUsedUnitName', 'UsedUnitName'],//单位++                                 
                                 ['ckStorageName', 'StorageName'],//仓库
                                 ['ckFromBillCount', 'FromBillCount'],//通知数量
                                 ['ckInCount', 'InCount'],//已出库数量
                                 ['ckNotInCount', 'NotInCount'],//未出库数量
                                 ['ckProductCount', 'ProductCount'],//出库数量
                                 ['ckUsedUnitCount', 'UsedUnitCount'],//shuliang ++
                                 ['ckUnitPrice', 'UnitPrice'],//单价
                                 ['ckTaxRate', 'TaxRate'],//税率
                                 ['ckTotalPrice', 'TotalPrice'],//金额
                                 ['ckBackCount', 'BackCount']);//退货数量
        }
        else
        {
             dbDetail = new Array(['ckSortNo', 'SortNo'],//序号
                                 ['ckProductNo', 'ProductNo'],//物品编号
                                 ['ckProductName', 'ProductName'],//物品名称
                                 ['ckColorName', 'ColorName'],//颜色
                                 ['ckSpecification', 'Specification'],//规格
                                 ['ckUnitID', 'UnitID'],//单位
                                 ['ckStorageName', 'StorageName'],//仓库
                                 ['ckFromBillCount', 'FromBillCount'],//通知数量
                                 ['ckInCount', 'InCount'],//已出库数量
                                 ['ckNotInCount', 'NotInCount'],//未出库数量
                                 ['ckProductCount', 'ProductCount'],//出库数量
                                 ['ckUnitPrice', 'UnitPrice'],//单价
                                 ['ckTaxRate', 'TaxRate'],//税率
                                 ['ckTotalPrice', 'TotalPrice'],//金额
                                 ['ckBackCount', 'BackCount']);//退货数量
        }
        
        }
        else
        {
             dbBase = new Array(
                                ['ckExtField1', 'ExtField1'],
                                ['ckExtField2', 'ExtField2'],
                                ['ckExtField3', 'ExtField3'],
                                ['ckExtField4', 'ExtField4'],
                                ['ckExtField5', 'ExtField5'],
                                ['ckExtField6', 'ExtField6'],
                                ['ckExtField7', 'ExtField7'],
                                ['ckExtField8', 'ExtField8'],
                                ['ckExtField9', 'ExtField9'],
                                ['ckExtField10', 'ExtField10'],
                                ['ckInNo', 'InNo'],//出库单编号
                                ['ckTitle', 'Title'],//主题
                                ['ckFromTypeName', 'FromTypeName'],//源单类型
                                ['ckArriveNo', 'ArriveNo'],//源单编号
                                ['ckProviderName', 'ProviderName'],//供应商
                                ['ckPurchaserName', 'PurchaserName'],//采购员
                                ['ckDeptName', 'DeptName'],//采购部门
                                ['ckTakerName', 'TakerName'],//交货人
                                ['ckCheckerName', 'CheckerName'],//验收人
                                ['ckInPutDeptName', 'InPutDeptName'],//入库部门
                                ['ckExecutorName', 'ExecutorName'],//入库人
                                ['ckEnterDate', 'EnterDate'],//入库时间
                                ['ckRemark', 'Remark'],//备注
                                ['ckSummary', 'Summary'],//摘要
                                ['ckBatchNo', 'BatchNo'],//批次                                
                                ['ckCountTotal', 'CountTotal'],//
                                ['ckCreatorReal', 'CreatorName'],//制单人
                                ['ckCreateDate', 'CreateDate'],//制单日期
                                ['ckConfirmorReal', 'ConfirmorName'],//确认人
                                ['ckConfirmDate', 'ConfirmDate'],//确认日期
                                ['ckCloserReal', 'CloserName'],//结单人
                                ['ckCloseDate', 'CloseDate'],//结单日期
                                ['ckModifiedUserID', 'ModifiedUserName'],//最后更新人
                                ['ckModifiedDate', 'ModifiedDate'],//最后更新日期
                                ['ckBillStatus', 'BillStatus']);//单据状态
                                
        /*明细表：复选框及其对应的字段*/
         if(document.getElementById("HiddenMoreUnit").value == "true")
         {
            dbDetail = new Array(['ckSortNo', 'SortNo'],//序号
                                 ['ckProductNo', 'ProductNo'],//物品编号
                                 ['ckProductName', 'ProductName'],//物品名称
                                 ['ckColorName', 'ColorName'],//颜色
                                 ['ckSpecification', 'Specification'],//规格
                                 ['ckUnitID', 'UnitID'],//单位
                                 ['ckUsedUnitName', 'UsedUnitName'],//单位++      
                                 ['ckStorageName', 'StorageName'],//仓库
                                 ['ckFromBillCount', 'FromBillCount'],//通知数量
                                 ['ckInCount', 'InCount'],//已出库数量
                                 ['ckNotInCount', 'NotInCount'],//未出库数量
                                 ['ckProductCount', 'ProductCount'],//出库数量
                                 ['ckUsedUnitCount', 'UsedUnitCount'],//shuliang ++
                                 ['ckBackCount', 'BackCount']);//退货数量
         }
         else
         {
            dbDetail = new Array(['ckSortNo', 'SortNo'],//序号
                                 ['ckProductNo', 'ProductNo'],//物品编号
                                 ['ckProductName', 'ProductName'],//物品名称
                                 ['ckColorName', 'ColorName'],//颜色
                                 ['ckSpecification', 'Specification'],//规格
                                 ['ckUnitID', 'UnitID'],//单位
                                 ['ckStorageName', 'StorageName'],//仓库
                                 ['ckFromBillCount', 'FromBillCount'],//通知数量
                                 ['ckInCount', 'InCount'],//已出库数量
                                 ['ckNotInCount', 'NotInCount'],//未出库数量
                                 ['ckProductCount', 'ProductCount'],//出库数量
                                 ['ckBackCount', 'BackCount']);//退货数量
         }
         

}
        /*加载打印参数设置*/
        LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag,hidIsSeted, dbBase, dbDetail,null);
    }

    /*保存打印模板设置*/
    function SavePrintSetting()    {
        
        var strBaseFields = "";
        var strDetailFields = "";
        var toLocation='StorageInPurchasePrint1.aspx?ID=' + intMrpID;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;

        if ($('#ckInNo').attr("checked")) strBaseFields = strBaseFields + "InNo|";
        if ($('#ckTitle').attr("checked")) strBaseFields = strBaseFields + "Title|";
        if ($('#ckFromTypeName').attr("checked")) strBaseFields = strBaseFields + "FromTypeName|";
        if ($('#ckArriveNo').attr("checked")) strBaseFields = strBaseFields + "ArriveNo|";
        if ($('#ckProviderName').attr("checked")) strBaseFields = strBaseFields + "ProviderName|";
        if ($('#ckPurchaserName').attr("checked")) strBaseFields = strBaseFields + "PurchaserName|";
        if ($('#ckDeptName').attr("checked")) strBaseFields = strBaseFields + "DeptName|";
        if ($('#ckTakerName').attr("checked")) strBaseFields = strBaseFields + "TakerName|";
        if ($('#ckCheckerName').attr("checked")) strBaseFields = strBaseFields + "CheckerName|";
        if ($('#ckInPutDeptName').attr("checked")) strBaseFields = strBaseFields + "InPutDeptName|";
        if ($('#ckExecutorName').attr("checked")) strBaseFields = strBaseFields + "ExecutorName|";
        if ($('#ckEnterDate').attr("checked")) strBaseFields = strBaseFields + "EnterDate|";
        if ($('#ckRemark').attr("checked")) strBaseFields = strBaseFields + "Remark|";
        if ($('#ckSummary').attr("checked")) strBaseFields = strBaseFields + "Summary|";
        if ($('#ckBatchNo').attr("checked")) strBaseFields = strBaseFields + "BatchNo|";
        if ($('#ckCountTotal').attr("checked")) strBaseFields = strBaseFields + "CountTotal|";
        var IsDisplayPrice = '<%=GetIsDisplayPrice() %>';
        if(IsDisplayPrice!="none")
        {
        if ($('#ckB_TotalPrice').attr("checked")) strBaseFields = strBaseFields + "B_TotalPrice|";
        }       
        if ($('#ckCreatorReal').attr("checked")) strBaseFields = strBaseFields + "CreatorName|";
        if ($('#ckCreateDate').attr("checked")) strBaseFields = strBaseFields + "CreateDate|";
        if ($('#ckConfirmorReal').attr("checked")) strBaseFields = strBaseFields + "ConfirmorName|";
        if ($('#ckConfirmDate').attr("checked")) strBaseFields = strBaseFields + "ConfirmDate|";
        if ($('#ckCloserReal').attr("checked")) strBaseFields = strBaseFields + "CloserName|";
        if ($('#ckCloseDate').attr("checked")) strBaseFields = strBaseFields + "CloseDate|";
        if ($('#ckModifiedUserID').attr("checked")) strBaseFields = strBaseFields + "ModifiedUserName|";
        if ($('#ckModifiedDate').attr("checked")) strBaseFields = strBaseFields + "ModifiedDate|";
        if ($('#ckBillStatus').attr("checked")) strBaseFields = strBaseFields + "BillStatus|";

        for(var i=0;i<10;i++)
        {
            if(document.getElementById('ckExtField'+(i+1)).style.display=='block'||document.getElementById('ckExtField'+(i+1)).style.display=='')
            {
                if ($('#ckExtField'+(i+1)).attr("checked")) strBaseFields = strBaseFields + "ExtField"+(i+1)+"|";
            }
        }


        if ($('#ckSortNo').attr("checked")) strDetailFields = strDetailFields + "SortNo|";
        if ($('#ckProductNo').attr("checked")) strDetailFields = strDetailFields + "ProductNo|";
        if ($('#ckProductName').attr("checked")) strDetailFields = strDetailFields + "ProductName|";
        if ($('#ckColorName').attr("checked")) strDetailFields = strDetailFields + "ColorName|";
        if ($('#ckSpecification').attr("checked")) strDetailFields = strDetailFields + "Specification|";
        if ($('#ckUnitID').attr("checked")) strDetailFields = strDetailFields + "UnitID|";
        if (document.getElementById('ckUsedUnitName').checked) strDetailFields = strDetailFields + "UsedUnitName|";
        if ($('#ckStorageName').attr("checked")) strDetailFields = strDetailFields + "StorageName|";
        if ($('#ckFromBillCount').attr("checked")) strDetailFields = strDetailFields + "FromBillCount|";
        if ($('#ckInCount').attr("checked")) strDetailFields = strDetailFields + "InCount|";
        if ($('#ckNotInCount').attr("checked")) strDetailFields = strDetailFields + "NotInCount|";
        if ($('#ckProductCount').attr("checked")) strDetailFields = strDetailFields + "ProductCount|";        
        if (document.getElementById('ckUsedUnitCount').checked) strDetailFields = strDetailFields + "UsedUnitCount|";
        if(IsDisplayPrice!="none")
        {
        if ($('#ckUnitPrice').attr("checked")) strDetailFields = strDetailFields + "UnitPrice|";
        if ($('#ckTaxRate').attr("checked")) strDetailFields = strDetailFields + "TaxRate|";
        if ($('#ckTotalPrice').attr("checked")) strDetailFields = strDetailFields + "TotalPrice|";
        } 
        if ($('#ckBackCount').attr("checked")) strDetailFields = strDetailFields + "BackCount|";
//        if (document.getElementById('ckdFromBillSortNo').checked) strDetailFields = strDetailFields + "FromLineNo|";
//        if (document.getElementById('ckdRemark').checked) strDetailFields = strDetailFields + "DetaiRemark|";
//        /*保存打印参数设置*/
        SaveCommonPrintParameterSet(strBaseFields,strDetailFields,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }
</script>

