<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInOtherPrint1.aspx.cs"
    Inherits="Pages_PrinttingModel_StorageManager_StorageInOtherPrint1" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

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
    <title>其他入库单</title>

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

        //全选
        function SelectAllCk() {
            $.each($("#div_InInfo :checkbox"), function(i, obj) {
                obj.checked = $("#checkall").attr("checked");
            });
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
                                    style="cursor: pointer;" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" cellspacing="1" bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF" align="center" valign="top" style="width: 90%">
                                <div id="divSet" style="display: none; width: 796px; overflow-x: auto; overflow-y: auto;
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
                                                            <input type="checkbox" id="ck1InNo" value="InNo" />
                                                            <input type="text" id="txtInNo" readonly="readonly" value="入库单编号：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1Title" value="Title" />
                                                            <input type="text" id="txtTitle" value="入库单主题：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ck1FromType" value="FromType" />
                                                            <input type="text" id="txtFromType" value="源单类型：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1BackNo" value="" />
                                                            <input type="text" id="txtBackNo" value="源单编号：" readonly size="10" />
                                                        </td>
                                                        <td width="20%">
                                                            <input type="checkbox" id="ck1OtherCorpName" value="" />
                                                            <input type="text" id="txtOtherCorpName" value="往来单位：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ck1CorpBigType" value="" />
                                                            <input type="text" id="txtCorpBigType" value="往来单位类型：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1TakerName" value="" />
                                                            <input type="text" id="txtTakerName" value="交货人：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1CheckerName" value="" />
                                                            <input type="text" id="txtCheckerName" value="验收人：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1ExecutorName" value="" />
                                                            <input type="text" id="txtExecutorName" value="入库人：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" id="ck1EnterDate" value="" />
                                                            <input type="text" id="txtEnterDate" value="入库时间：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1DeptName" value="" />
                                                            <input type="text" id="txtDeptName" value="入库部门：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1ReasonTypeName" value="" />
                                                            <input type="text" id="txtReasonTypeName" value="入库原因：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" id="ck1Summary" value="" />
                                                            <input type="text" id="txtSummary" value="摘要：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                        <input type="checkbox" name="ckUsertxtExecutor" id="ck1BatchNo" value="CustName" />
                                                            <input type="text" id="txtBatchNo" value="批次：" size="10" readonly />
                                                        </td>
                                                        <td width="20%" align="left">
                                                        </td>
                                                    </tr>
                                                    <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" id="ck1ExtField1" value="" style="display: none" />
                                                                <input type="text" id="txtExtField1" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ck1ExtField2" id="ck1ExtField2" value="ExtField2" style="display: none" />
                                                                <input type="text" id="txtExtField2" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ck1ExtField3" id="ck1ExtField3" value="ExtField3" style="display: none" />
                                                                <input type="text" id="txtExtField3" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ck1ExtField4" id="ck1ExtField4" value="ExtField4" style="display: none" />
                                                                <input type="text" id="txtExtField4" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ck1ExtField5" id="ck1ExtField5" value="ExtField5" style="display: none" />
                                                                <input type="text" id="txtExtField5" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ck1ExtField6" id="ck1ExtField6" value="ExtField6" style="display: none" />
                                                                <input type="text" id="txtExtField6" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ck1ExtField7" id="ck1ExtField7" value="ExtField7" style="display: none" />
                                                                <input type="text" id="txtExtField7" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ck1ExtField8" id="ck1ExtField8" value="ExtField8" style="display: none" />
                                                                <input type="text" id="txtExtField8" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ck1ExtField9" id="ck1ExtField9" value="ExtField9" style="display: none" />
                                                                <input type="text" id="txtExtField9" value="" readonly size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ck1ExtField10" id="ck1ExtField10" value="ExtField10"
                                                                    style="display: none" />
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
                                                            <input type="checkbox" id="ck1CountTotal" value="" />
                                                            <input type="text" id="txtCountTotal" value="数量合计：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left" style="display: <%=GetIsDisplayPrice()%>">
                                                            <input type="checkbox" id="ck1A_TotalPrice" value="" />
                                                            <input type="text" id="txtA_TotalPrice" value="金额合计：" readonly size="10" />
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
                                                            <input type="checkbox" id="ck1BillStatus" value="" />
                                                            <input type="text" id="txtBillStatus" value="单据状态：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1CreatorName" value="" />
                                                            <input type="text" id="txtCreatorName" value="制单人：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ck1CreateDate" value="" />
                                                            <input type="text" id="txtCreateDate" value="制单日期：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1ConfirmorName" value="" />
                                                            <input type="text" id="txtConfirmorName" value="确认人：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1ConfirmDate" value="" />
                                                            <input type="text" id="txtConfirmDate" value="确认日期：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ck1CloserName" value="" />
                                                            <input type="text" id="txtCloserName" value="结单人：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ck1CloseDate" value="" />
                                                            <input type="text" id="txtCloseDate" value="结单日期：" readonly size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1ModifiedUserName" value="" />
                                                            <input type="text" id="txtModifiedUserName" value="最后更新人：" readonly size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="ck1ModifiedDate" value="" />
                                                            <input type="text" id="txtModifiedDate" value="最后更新日期：" readonly size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" id="ck1Remark" value="" />
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
                                                                        <input type="checkbox" id="ck2SortNo" value="" /><input type="text" id="txtSortNo"
                                                                            value="序号" size="5" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2ProductNo" value="" /><input type="text" id="txtProductNo"
                                                                            value="物品编号" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2ProductName" value="ProductName" /><input type="text"
                                                                            id="txtProductName" value="物品名称" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2ColorName" value="ColorName" /><input type="text"
                                                                            id="txtColorName" value="颜色" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2Specification" value="Type" /><input type="text" id="txtSpecification"
                                                                            value="规格" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2UnitID" value="" /><input type="text" id="txtUnitID"
                                                                            value="基本单位" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" id="td_UsedUnitName" runat="server">
                                                                        <input type="checkbox" name="ckdUnitName" id="ck2UsedUnitName" value="UnitName" /><input
                                                                            type="text" id="txtUsedUnitName" value="单位" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2StorageName" value="" /><input type="text" id="txtStorageName"
                                                                            value="仓库" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2FromBillCount" value="" /><input type="text" id="txtFromBillCount"
                                                                            value="源单数量" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2InNumber" value="" /><input type="text" id="txtInNumber"
                                                                            value="已入库数量" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2NotInCount" value="" /><input type="text" id="txtNotInCount"
                                                                            value="未入库数量" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" id="ck2InCount" /><input type="text" id="txtInCount" value="基本数量"
                                                                            readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" id="td_UsedUnitCount" runat="server">
                                                                        <input type="checkbox" name="ckdUnitName" id="ck2UsedUnitCount" value="UnitName" /><input
                                                                            type="text" id="txtUsedUnitCount" value="数量" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice()%>">
                                                                        <input type="checkbox" id="ck2UnitPrice" value="" /><input type="text" id="txtUnitPrice"
                                                                            value="单价" readonly size="10" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF" style="display: <%=GetIsDisplayPrice()%>">
                                                                        <input type="checkbox" id="ck2B_TotalPrice" value="" /><input type="text" id="txtB_TotalPrice"
                                                                            value="金额" readonly size="10" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold;
                                                            color: #5D5D5D;">
                                                            全选<input type="checkbox" id="checkall" value="" onclick="SelectAllCk();" />
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

<script type="text/javascript">
    var BillID = <%=BillID %>;
    printSetObj.BillTypeFlag = '<%=XBase.Common.ConstUtil.BILL_TYPEFLAG_STORAGE %>';
    printSetObj.PrintTypeFlag = '<%=XBase.Common.ConstUtil.PRINTBILL_TYPEFLAG_INOTHER %>';
    /*跳转页面*/
    printSetObj.ToLocation ='StorageInOtherPrint1.aspx?ID=' + BillID;
    /*表名称*/
    printSetObj.TableName = 'officedba.StorageInOther';
    /*取基本信息及明细信息的字段*/
     if(document.getElementById("HiddenMoreUnit").value == "true")
     {
        printSetObj.ArrayDB = new Array(    
        [   
            'ExtField1','ExtField2','ExtField3','ExtField4','ExtField5','ExtField6','ExtField7','ExtField8', 'ExtField9','ExtField10',
            'InNo','Title','FromType','BackNo', 'OtherCorpName','CorpBigType','TakerName','CheckerName',
            'ExecutorName','EnterDate','DeptName','ReasonTypeName','Summary','BatchNo','CountTotal','A_TotalPrice',
            'CreatorName','CreateDate','ConfirmorName','ConfirmDate','CloserName','CloseDate','ModifiedUserName','ModifiedDate','BillStatus','Remark'
        ],
        [   
            'SortNo','ProductNo','ProductName','ColorName','Specification','UnitID','UsedUnitName','StorageName','FromBillCount','InNumber', 'NotInCount','InCount','UsedUnitCount','UnitPrice','B_TotalPrice'
        ]
        );
     }
     else
     {
        printSetObj.ArrayDB = new Array(    
        [   
            'ExtField1','ExtField2','ExtField3','ExtField4','ExtField5','ExtField6','ExtField7','ExtField8', 'ExtField9','ExtField10',
            'InNo','Title','FromType','BackNo', 'OtherCorpName','CorpBigType','TakerName','CheckerName',
            'ExecutorName','EnterDate','DeptName','ReasonTypeName','Summary','BatchNo','CountTotal','A_TotalPrice',
            'CreatorName','CreateDate','ConfirmorName','ConfirmDate','CloserName','CloseDate','ModifiedUserName','ModifiedDate','BillStatus','Remark'
        ],
        [   
            'SortNo','ProductNo','ProductName','ColorName','Specification','UnitID','StorageName','FromBillCount','InNumber', 'NotInCount','InCount','UnitPrice','B_TotalPrice'
        ]
        );
     }
    
    var IsDisplayPrice = '<%=GetIsDisplayPrice() %>';
    if(IsDisplayPrice=="none")
    {
         if(document.getElementById("HiddenMoreUnit").value == "true")
         {
            printSetObj.ArrayDB = new Array(    
                [   
                    'ExtField1','ExtField2','ExtField3','ExtField4','ExtField5','ExtField6','ExtField7','ExtField8', 'ExtField9','ExtField10',
                    'InNo','Title','FromType','BackNo', 'OtherCorpName','CorpBigType','TakerName','CheckerName',
                    'ExecutorName','EnterDate','DeptName','ReasonTypeName','Summary','BatchNo','CountTotal',
                    'CreatorName','CreateDate','ConfirmorName','ConfirmDate','CloserName','CloseDate','ModifiedUserName','ModifiedDate','BillStatus','Remark'
                ],
                [   
                    'SortNo','ProductNo','ProductName','ColorName','Specification','UnitID','UsedUnitName','StorageName','FromBillCount','InNumber', 'NotInCount','InCount','UsedUnitCount'
                ]
            );
         }
         else
         {
            printSetObj.ArrayDB = new Array(    
                [   
                    'ExtField1','ExtField2','ExtField3','ExtField4','ExtField5','ExtField6','ExtField7','ExtField8', 'ExtField9','ExtField10',
                    'InNo','Title','FromType','BackNo', 'OtherCorpName','CorpBigType','TakerName','CheckerName',
                    'ExecutorName','EnterDate','DeptName','ReasonTypeName','Summary','BatchNo','CountTotal',
                    'CreatorName','CreateDate','ConfirmorName','ConfirmDate','CloserName','CloseDate','ModifiedUserName','ModifiedDate','BillStatus','Remark'
                ],
                [   
                    'SortNo','ProductNo','ProductName','ColorName','Specification','UnitID','StorageName','FromBillCount','InNumber', 'NotInCount','InCount'
                ]
            );
         }
         
    }

</script>

