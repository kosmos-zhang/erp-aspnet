<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellOfferPrint.aspx.cs" Inherits="Pages_PrinttingModel_SellManager_SellOfferPrint" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售报价</title>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
     <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
     <script src="../../../js/common/PrintParameterSetting.js" type="text/javascript"></script>
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
            width: 6%;
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
            width: 8%;
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
            word-break:break-all;
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
            <!-- Start 第二明细 -->
            <br />
            <table width="640px" border="0" cellpadding="0" cellspacing="1">
                <tbody id="tableDetail2" runat="server">
                </tbody>
            </table>
            <!-- End 第二明细 -->
        </div>
        <input type="hidden" id="hiddExcel" runat="server" />
        
    </div>
    <!-- Start 参数设置 -->
    <div align="center" id="div_InInfo" style="width: 75%; z-index: 100; position: absolute;
      display: none  "><%----%>
        <table border="0" cellspacing="1" bgcolor="#999999" style="width: 75%">
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
                                <div id="divSet"  style="display: none;" class="setDiv"><%----%>
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
                                                            <input type="checkbox" name="ckMRPNo" id="ckOfferNo" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="txtMRPNo" value="单据编号：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckTitle" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="txtSubject" value="主题：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckFromTypeText" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="txtPricipalReal" value="源单类型：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckChanceNo" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text1" value="源单编号：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text2" value="客户名称：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustTel" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text3" value="客户电话：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckBusiTypeName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text4" value="业务类型：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckSellTypeName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text5" value="销售类别：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckPayTypeName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text6" value="结算方式：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckMoneyTypeName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text8" value="支付方式：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckTakeTypeName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text9" value="交货方式：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCarryTypeName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text10" value="运送方式：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCurrencyName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text11" value="币种：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckRate" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text12" value="汇率：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckSellerName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text13" value="业务员：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckDeptName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text14" value="部门：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckOfferDate" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text15" value="报价日期：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckExpireDate" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text16" value="有效截止日期：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckQuoteTime" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text17" value="报价次数：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckisAddTaxName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text18" value="是否增值税：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                           <input type="checkbox" name="ckExtField1" id="ckExtField1" value="ExtField1" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField1" value="" size="20" style="display: none" />
                                                        </td>
                                                    </tr>
                                                    
                                                     <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField2" id="ckExtField2" value="ExtField2" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField2" value="" size="20" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField3" id="ckExtField3" value="ExtField3" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField3" value="" size="20" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField4" id="ckExtField4" value="ExtField4" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField4" value="" size="20" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField5" id="ckExtField5" value="ExtField5" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField5" value="" size="20" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField6" id="ckExtField6" value="ExtField6" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField6" value="" size="20" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField7" id="ckExtField7" value="ExtField7" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField7" value="" size="20" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="ckExtField8" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField8" value="" size="20" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="ckExtField9" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField9" value="" size="20" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="ckExtField10" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField10" value="" size="20" style="display: none" />
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
                                                            <input type="checkbox" name="ckMRPNo" id="ckTotalPrice" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text20" value="金额合计：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckTotalTax" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text21" value="税额合计：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckTotalFee" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text22" value="含税金额合计：" size="20" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckDiscount" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text19" value="整单折扣(%)：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckDiscountTotal" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text23" value="折扣金额：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckRealTotal" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text24" value="折后含税金额：" size="20" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCountTotal" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text25" value="产品数量合计：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            
                                                        </td>
                                                        <td align="left">
                                                            
                                                        </td>
                                                    </tr>
                                                    
                                                   
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            备注信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckStrBillStatusText" id="ckBillStatusText" value="strBillStatusText" /><input readonly="readonly" 
                                                                type="text" id="txtStrBillStatusText" value="单据状态：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCreatorReal" id="ckCreatorName" value="CreatorReal" /><input readonly="readonly" 
                                                                type="text" id="txtCreatorReal" value="制单人：" size="20" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCreateDate" id="ckCreateDate" value="CreateDate" /><input readonly="readonly" 
                                                                type="text" id="txtCreateDate" value="制单日期：" size="20" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmorReal" id="ckConfirmorName" value="ConfirmorReal" /><input readonly="readonly" 
                                                                type="text" id="txtConfirmorReal" value="确认人：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmDate" id="ckConfirmDate" value="ConfirmDate" /><input readonly="readonly" 
                                                                type="text" id="txtConfirmDate" value="确认日期：" size="20" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCloserReal" id="ckCloserName" value="CloserReal" /><input readonly="readonly" 
                                                                type="text" id="txtCloserReal" value="结单人：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ckCloseDate" value="CloseDate" /><input readonly="readonly" 
                                                                type="text" id="txtCloseDate" value="结单日期：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckModifiedUserID" id="ckModifiedUserID" value="ModifiedUserID" /><input readonly="readonly" 
                                                                type="text" id="txtModifiedUserID" value="最后更新人：" size="20" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckModifiedDate" id="ckModifiedDate" value="ModifiedDate" /><input readonly="readonly" 
                                                                type="text" id="txtModifiedDate" value="最后更新日期：" size="20" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ckDeliverRemark" value="CloseDate" /><input readonly="readonly" 
                                                                type="text" id="Text7" value="交付说明：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckModifiedUserID" id="ckPackTransit" value="ModifiedUserID" /><input readonly="readonly" 
                                                                type="text" id="Text26" value="包装运输说明：" size="20" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckModifiedDate" id="ckPayRemark" value="ModifiedDate" /><input readonly="readonly" 
                                                                type="text" id="Text27" value="付款说明：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckRemark" id="ckRemark" value="Remark" /><input type="text" readonly="readonly" 
                                                                id="txtRemark" value="备注：" size="20" />
                                                        </td>
                                                        <td width="20%" align="left">
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
                                                        <td colspan="4">
                                                            <table width="100%" border="0" cellspacing="1"  id="listSetDetail">
                                                                <tr>                                                                   
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdProdNo" id="ckProdNo" value="ProdNo" /><input type="text" readonly="readonly" 
                                                                            id="txtDProductNo" value="物品编号" size="12" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdProductName" id="ckProductName" value="ProductName" /><input readonly="readonly" 
                                                                            type="text" id="txtDProductName" value="物品名称" size="12" />
                                                                    </td>
                                                                    <%if (UserInfo.IsMoreUnit.ToString().Trim() == "True")
                                                                      {%>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdGrossCount" id="ckCodeName" value="GrossCount" /><input readonly="readonly" 
                                                                                type="text" id="txtDGrossCount" value="基本单位" size="12" />
                                                                        </td>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdPlanDate" id="ckProductCount" value="PlanDate" /><input readonly="readonly" 
                                                                                type="text" id="txtDPlanDate" value="基本数量" size="12" />
                                                                        </td>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdBaseUnitName" id="ckUsedUnitName" value="UsedUnitName" /><input readonly="readonly" 
                                                                                type="text" id="txtUsedUnitName" value="单位" size="12" />
                                                                        </td>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdUsedUnitCount" id="ckUsedUnitCount" value="UsedUnitCount" /><input readonly="readonly" 
                                                                                type="text" id="txtUsedUnitCount" value="数量" size="12" />
                                                                        </td>
                                                                    <% }
                                                                      else
                                                                      {%>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdGrossCount" id="ckCodeName" value="GrossCount" /><input readonly="readonly" 
                                                                                type="text" id="txtDGrossCount" value="单位" size="12" />
                                                                        </td>
                                                                        <td bgcolor="#FFFFFF">
                                                                            <input type="checkbox" name="ckdPlanDate" id="ckProductCount" value="PlanDate" /><input readonly="readonly" 
                                                                                type="text" id="txtDPlanDate" value="数量" size="12" />
                                                                        </td>       
                                                                    <%} %>
                                                                                                                                      
                                                                </tr>
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdUnitName" id="ckSpecification" value="UnitName" /><input readonly="readonly" 
                                                                            type="text" id="txtDUnitName" value="规格" size="6" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanCount" id="ckSendTime" value="PlanCount" /><input readonly="readonly" 
                                                                            type="text" id="txtDPlanCount" value="交货期限(天)" size="11" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckTypeName" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text28" value="包装要求" size="8" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckUnitPrice" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text29" value="单价" size="6" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckTaxPrice" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text30" value="含税价" size="7" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckDiscount2" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text31" value="折扣(%)" size="7" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckTaxRate" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text32" value="税率(%)" size="7" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckTotalFee2" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text33" value="含税金额" size="8" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckTotalPrice2" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text34" value="金额" size="5" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckTotalTax2" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text35" value="税额" size="5" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckRemark2" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="Text36" value="备注" size="5" />
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
                                    <input type="hidden" id="hidPlanNo" name="planno" runat="server" />
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

<script language="javascript">
   
     var strPlanNo = document.getElementById("hidPlanNo").value;
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
        LoadExtTableName('officedba.SellOffer');
        /*加载打印模板设置信息*/
        LoadPrintSettingInfo();

    }
    


    /*2:加载打印模板设置信息*/
    function LoadPrintSettingInfo() {

        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        var hidIsSeted = document.getElementById('isSeted').value;


        /*主表：复选框及其对应的字段*/
        var dbBase = new Array( ['ckExtField1', 'ExtField1'],
                                ['ckExtField2', 'ExtField2'],
                                ['ckExtField3', 'ExtField3'],
                                ['ckExtField4', 'ExtField4'],
                                ['ckExtField5', 'ExtField5'],
                                ['ckExtField6', 'ExtField6'],
                                ['ckExtField7', 'ExtField7'],
                                ['ckExtField8', 'ExtField8'],
                                ['ckExtField9', 'ExtField9'],
                                ['ckExtField10', 'ExtField10'],
                                
                                ['ckOfferNo', 'OfferNo'],
                                ['ckTitle', 'Title'],
                                ['ckFromTypeText', 'FromTypeText'],
                                
                                ['ckChanceNo', 'ChanceNo'],
                                ['ckCustName', 'CustName'],
                                ['ckCustTel', 'CustTel'],
                                
                                ['ckBusiTypeName', 'BusiTypeName'],
                                ['ckSellTypeName', 'SellTypeName'],
                                ['ckPayTypeName', 'PayTypeName'],
                                
                                ['ckMoneyTypeName', 'MoneyTypeName'],                                
                                ['ckTakeTypeName', 'TakeTypeName'],
                                ['ckCarryTypeName', 'CarryTypeName'],
                                
                                ['ckCurrencyName', 'CurrencyName'],
                                ['ckRate', 'Rate'],
                                ['ckSellerName', 'SellerName'],
                                
                                ['ckDeptName', 'DeptName'],
                                ['ckOfferDate', 'OfferDate'],
                                ['ckExpireDate', 'ExpireDate'],
                                
                                ['ckQuoteTime', 'QuoteTime'],
                                ['ckisAddTaxName', 'isAddTaxName'],
                                
                                ['ckTotalPrice', 'TotalPrice'],
                                ['ckTotalTax', 'TotalTax'],
                                ['ckTotalFee', 'TotalFee'],                               
                                ['ckDiscount', 'Discount'],
                                ['ckDiscountTotal', 'DiscountTotal'],
                                ['ckRealTotal', 'RealTotal'],                               
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
                                ['ckDeliverRemark', 'DeliverRemark'],
                                
                                ['ckPackTransit', 'PackTransit'],
                                ['ckPayRemark', 'PayRemark'],
                                ['ckRemark', 'Remark']);
                                
        if(glb_IsMoreUnit=="True")
        {
            /*明细表：复选框及其对应的字段*/
            var dbDetail = new Array(['ckProdNo', 'ProdNo'],
                                     ['ckProductName', 'ProductName'],
                                     ['ckSpecification', 'Specification'],
                                     ['ckCodeName', 'CodeName'],
                                     ['ckProductCount', 'ProductCount'],
                                     ['ckUsedUnitName', 'UsedUnitName'],
                                     ['ckUsedUnitCount', 'UsedUnitCount'],
                                     ['ckSendTime', 'SendTime'],
                                     ['ckTypeName', 'TypeName'],
                                     ['ckUnitPrice', 'UsedPrice'],
                                     ['ckTaxPrice', 'TaxPrice'],
                                     ['ckDiscount2', 'Discount'],
                                     ['ckTaxRate', 'TaxRate'],
                                     ['ckTotalFee2', 'TotalFee'],
                                     ['ckTotalPrice2', 'TotalPrice'],
                                     ['ckTotalTax2', 'TotalTax'],
                                     ['ckRemark2', 'Remark']);
            /*加载打印参数设置
              注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
            */
            LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase, dbDetail,null);
        }
        else
        {
            /*明细表：复选框及其对应的字段*/
            var dbDetail = new Array(['ckProdNo', 'ProdNo'],
                                     ['ckProductName', 'ProductName'],
                                     ['ckSpecification', 'Specification'],
                                     ['ckCodeName', 'CodeName'],
                                     ['ckSendTime', 'SendTime'],
                                     ['ckProductCount', 'ProductCount'],
                                     ['ckTypeName', 'TypeName'],
                                     ['ckUnitPrice', 'UnitPrice'],
                                     ['ckTaxPrice', 'TaxPrice'],
                                     ['ckDiscount2', 'Discount'],
                                     ['ckTaxRate', 'TaxRate'],
                                     ['ckTotalFee2', 'TotalFee'],
                                     ['ckTotalPrice2', 'TotalPrice'],
                                     ['ckTotalTax2', 'TotalTax'],
                                     ['ckRemark2', 'Remark']);
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
        var strDetailSecondFields = "";
        var toLocation='SellOfferPrint.aspx?no=' + strPlanNo;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        
        if (document.getElementById('ckOfferNo').checked) strBaseFields = strBaseFields + "OfferNo|";
        if (document.getElementById('ckTitle').checked) strBaseFields = strBaseFields + "Title|";
        if (document.getElementById('ckFromTypeText').checked) strBaseFields = strBaseFields + "FromTypeText|";
        if (document.getElementById('ckChanceNo').checked) strBaseFields = strBaseFields + "ChanceNo|";
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";
        if (document.getElementById('ckCustTel').checked) strBaseFields = strBaseFields + "CustTel|"; 
               
        if (document.getElementById('ckBusiTypeName').checked) strBaseFields = strBaseFields + "BusiTypeName|";
        if (document.getElementById('ckSellTypeName').checked) strBaseFields = strBaseFields + "SellTypeName|";        
        if (document.getElementById('ckPayTypeName').checked) strBaseFields = strBaseFields + "PayTypeName|";        
        if (document.getElementById('ckMoneyTypeName').checked) strBaseFields = strBaseFields + "MoneyTypeName|";
        if (document.getElementById('ckTakeTypeName').checked) strBaseFields = strBaseFields + "TakeTypeName|";
        if (document.getElementById('ckCarryTypeName').checked) strBaseFields = strBaseFields + "CarryTypeName|";
        
        if (document.getElementById('ckCurrencyName').checked) strBaseFields = strBaseFields + "CurrencyName|";        
        if (document.getElementById('ckRate').checked) strBaseFields = strBaseFields + "Rate|";
        if (document.getElementById('ckSellerName').checked) strBaseFields = strBaseFields + "SellerName|";
        if (document.getElementById('ckDeptName').checked) strBaseFields = strBaseFields + "DeptName|";        
        if (document.getElementById('ckOfferDate').checked) strBaseFields = strBaseFields + "OfferDate|";
        if (document.getElementById('ckExpireDate').checked) strBaseFields = strBaseFields + "ExpireDate|";
        
        if (document.getElementById('ckQuoteTime').checked) strBaseFields = strBaseFields + "QuoteTime|";
        if (document.getElementById('ckisAddTaxName').checked) strBaseFields = strBaseFields + "isAddTaxName|";
        if (document.getElementById('ckTotalPrice').checked) strBaseFields = strBaseFields + "TotalPrice|";
        if (document.getElementById('ckTotalTax').checked) strBaseFields = strBaseFields + "TotalTax|";
        if (document.getElementById('ckTotalFee').checked) strBaseFields = strBaseFields + "TotalFee|";
        if (document.getElementById('ckDiscount').checked) strBaseFields = strBaseFields + "Discount|";
        if (document.getElementById('ckDiscountTotal').checked) strBaseFields = strBaseFields + "DiscountTotal|";
         if (document.getElementById('ckRealTotal').checked) strBaseFields = strBaseFields + "RealTotal|";
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
        if (document.getElementById('ckDeliverRemark').checked) strBaseFields = strBaseFields + "DeliverRemark|";
        
         if (document.getElementById('ckPackTransit').checked) strBaseFields = strBaseFields + "PackTransit|";
        if (document.getElementById('ckPayRemark').checked) strBaseFields = strBaseFields + "PayRemark|";
        if (document.getElementById('ckRemark').checked) strBaseFields = strBaseFields + "Remark|";
        for(var i=0;i<10;i++)
        {
            if(document.getElementById('ckExtField'+(i+1)).style.display=='block'||document.getElementById('ckExtField'+(i+1)).style.display=='')
            {
                if (document.getElementById('ckExtField'+(i+1)).checked) strBaseFields = strBaseFields + "ExtField"+(i+1)+"|";
            }
            
        }
        /*明细信息*/
        if (document.getElementById('ckProdNo').checked) strDetailFields = strDetailFields + "ProdNo|";
        if (document.getElementById('ckProductName').checked) strDetailFields = strDetailFields + "ProductName|";
        if (document.getElementById('ckSpecification').checked) strDetailFields = strDetailFields + "Specification|";
        //多计量单位
        if(glb_IsMoreUnit=="True")
        {
            if (document.getElementById('ckCodeName').checked) strDetailFields = strDetailFields + "CodeName|";
            if (document.getElementById('ckProductCount').checked) strDetailFields = strDetailFields + "ProductCount|";  
            if (document.getElementById('ckUsedUnitName').checked) strDetailFields = strDetailFields + "UsedUnitName|";
            if (document.getElementById('ckUsedUnitCount').checked) strDetailFields = strDetailFields + "UsedUnitCount|";            
        }
        else
        {
            if (document.getElementById('ckCodeName').checked) strDetailFields = strDetailFields + "CodeName|";
            if (document.getElementById('ckProductCount').checked) strDetailFields = strDetailFields + "ProductCount|";             
        } 
        if (document.getElementById('ckSendTime').checked) strDetailFields = strDetailFields + "SendTime|";
        if (document.getElementById('ckTypeName').checked) strDetailFields = strDetailFields + "TypeName|";
        //多计量单位
        if(glb_IsMoreUnit=="True")
        {
            if (document.getElementById('ckUnitPrice').checked) strDetailFields = strDetailFields + "UsedPrice|";
        }
        else
        {
            if (document.getElementById('ckUnitPrice').checked) strDetailFields = strDetailFields + "UnitPrice|";
        }
        
        if (document.getElementById('ckTaxPrice').checked) strDetailFields = strDetailFields + "TaxPrice|";
        if (document.getElementById('ckDiscount2').checked) strDetailFields = strDetailFields + "Discount|";
        if (document.getElementById('ckTaxRate').checked) strDetailFields = strDetailFields + "TaxRate|";
        if (document.getElementById('ckTotalFee2').checked) strDetailFields = strDetailFields + "TotalFee|";
        if (document.getElementById('ckTotalPrice2').checked) strDetailFields = strDetailFields + "TotalPrice|"; 
        if (document.getElementById('ckTotalTax2').checked) strDetailFields = strDetailFields + "TotalTax|"; 
        if (document.getElementById('ckRemark2').checked) strDetailFields = strDetailFields + "Remark|"; 
               
      
        /*保存打印参数设置*/
        
        SaveCommonPrintParameterSet(strBaseFields,strDetailFields,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }

    
</script>
