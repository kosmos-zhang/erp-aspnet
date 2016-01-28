<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseReject_Add.aspx.cs"
    Inherits="Pages_Office_PurchaseManager_PurchaseReject_Add" %>

<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/ProviderSelect.ascx" TagName="ProviderSelect"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/PurchaseArriveSelect.ascx" TagName="PurchaseArriveSelect"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/PurchaseManager/PurchaseStorageSelect.ascx"
    TagName="PurchaseStorageSelect" TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc15" %>
<%@ Register Src="../../../UserControl/Common/StorageSnapshot.ascx" TagName="StorageSnapshot"
    TagPrefix="uc12" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/Common/ProjectSelectControl.ascx" TagName="ProjectSelectControl"
    TagPrefix="uc10" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购退货单</title>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <style type="text/css">
        .style1
        {
            border-width: 0pt;
            background-color: #ffffff;
            height: 9px;
            width: 102px;
        }
        .style2
        {
            background-color: #E6E6E6;
            text-align: right;
        }
        .style3
        {
            background-color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div id="divPBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="PBackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <uc1:Message ID="Message1" runat="server" />
    <uc3:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc6:PurchaseArriveSelect ID="PurchaseArriveSelect1" runat="server" />
    <uc2:PurchaseStorageSelect ID="PurchaseStorageSelect1" runat="server" />
    <uc7:FlowApply ID="FlowApply1" runat="server" />
    <uc5:ProviderSelect ID="ProviderSelect1" runat="server" />
    <br />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <div id="divTitle" runat="server">
                                新建采购退货单</div>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;<img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" alt="保存"
                                            id="imgSave" style="cursor: pointer" onclick="InsertPurchaseReject();" visible="false"
                                            runat="server" />
                                        <img alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="imgUnSave" style="display: none;"
                                            visible="false" runat="server" /><span id="GlbFlowButtonSpan" visible="false" runat="server"></span><span
                                                id="Forms" class="Spantype"></span>
                                        <%--<img src="../../../Images/Button/Bottom_btn_Yd.jpg" alt="源单总览" id="Get_Potential" onclick="PurchaseRejectSelect();" style="cursor: hand;" border="0" /><span lang="zh-cn"></span>
                                        <img src="../../../Images/Button/Bottom_btn_uYd.jpg" alt="源单总览" id="Get_UPotential" style="cursor: hand; display:none;"  border="0" />--%>
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor: hand;
                                            display: none;" onclick="Back();" />
                                    </td>
                                    <td align="right">
                                        <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style="float: right;
                                            cursor: pointer;" id="imgPrint" onclick="PurchaseRejectPrint();" />
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <!-- End 流程处理-->
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <input type="hidden" id="txtIsliebiaoNo" value="0" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        基本信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr>
                        <td align="right" class="style2" width="10%">
                            单据编号<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <div id="divInputNo" runat="server">
                                <uc4:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <div id="divPurchaseRejectNo" runat="server" class="tdinput" style="display: none">
                            </div>
                        </td>
                        <td align="right" class="style2" width="10%">
                            单据主题
                        </td>
                        <td class="style3" width="23%">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="tdinput" Width="95%" SpecialWorkCheck="单据主题"></asp:TextBox>
                        </td>
                        <td align="right" class="style2" width="10%">
                            源单类型
                        </td>
                        <td class="style3" width="24%">
                            <select name="drpFromType" class="tdinput" id="drpFromType" onchange="DeleteAll();">
                                <option value="0" selected="selected">无来源</option>
                                <option value="1">采购到货单</option>
                                <option value="2">采购入库</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购类别
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="drpTypeID" class="tdinput" runat="server" id="drpTypeID">
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            部门
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="DeptDeptID" onclick="alertdiv('DeptDeptID,HidDeptID');" runat="server"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="HidDeptID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            供应商<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtProviderID" runat="server" onclick="PopPurProviderInfo()" ReadOnly="true"
                                CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtHidProviderID" runat="server" />
                            <input name="txtHiddenProviderID1" id="txtHiddenProviderID1" type="text" class="tdinput"
                                size="15" disabled="disabled" style="display: none" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            交货方式
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="drpTakeType" class="tdinput" width="119px" runat="server" id="drpTakeType">
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            运送方式
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="drpCarryType" class="tdinput" width="119px" runat="server" id="drpCarryType">
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            结算方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="drpPayType" class="tdinput" width="119px" runat="server" id="drpPayType">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            币种<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <select name="drpCurrencyType" class="tdinput" width="119px" runat="server" onchange="ChangeCurreny()"
                                id="drpCurrencyType">
                            </select>
                            <input type="text" runat="server" id="CurrencyTypeID" name="CurrencyTypeID" class="tdinput"
                                style="display: none" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            汇率
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtRate" runat="server" CssClass="tdinput" Width="95%" ReadOnly="true"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            是否为增值税
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="checkbox" id="chkIsAddTax" checked="true" onclick="fnChangeAddTax();" /><label
                                id="AddTax">是增值税</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            收货人<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtReceiveMan" runat="server" CssClass="tdinput" Width="95%" SpecialWorkCheck="收货人"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            收货人联系电话
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtReceiveTel" runat="server" CssClass="tdinput" Width="95%" SpecialWorkCheck="收货人联系电话"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            支付方式
                        </td>
                        <td class="tdColInput" width="24%">
                            <select name="drpMoneyType" class="tdinput" width="119px" runat="server" id="drpMoneyType">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            采购员<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="UserPurchaser" runat="server" onclick="alertdiv('UserPurchaser,HidPurchaser');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="HidPurchaser" runat="server" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            退货时间<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtRejectDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEquipCheckDate')})"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            是否建单
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtisOpenbill" runat="server" CssClass="tdinput" disabled Width="95%"
                                Text="" ReadOnly Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            所属项目
                        </td>
                        <td class="tdColInput" width="23%">
                            <!-- 项目控件-->
                            <asp:TextBox ID="txtProject" runat="server" onclick="ShowProjectInfo('txtProject','hidProjectID');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="hidProjectID" runat="server" />
                            <uc10:ProjectSelectControl ID="ProjectSelectControl1" runat="server" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                        </td>
                        <td class="tdColInput" width="23%">
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                        </td>
                        <td class="tdColInput" width="24%">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            发货地址
                        </td>
                        <%--<td class="tdColInput" width="23%">
                                
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            &nbsp;</td>--%>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea name="txtSendAddress" id="txtSendAddress" rows="3" cols="80" style="width: 95%"></textarea>
                        </td>
                        <%--<td height="20" align="right" class="tdColTitle" width="10%">
                            &nbsp;</td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="TextBox15" runat="server" onfocus="alertdiv('UserPrincipal,txtPrincipal');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="Hidden5" runat="server" />
                        </td>--%>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            收货地址
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea name="txtReceiveOverAddress" id="txtReceiveOverAddress" rows="3" cols="80"
                                style="width: 95%"></textarea>
                        </td>
                    </tr>
                </table>
                <uc8:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        合计信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonTotal'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','divButtonTotal')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_02" style="display: block">
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            退货数量合计
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtCountTotal" runat="server" ReadOnly="true" CssClass="tdinput"
                                Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            金额合计
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtTotalMoney" runat="server" ReadOnly="true" CssClass="tdinput"
                                Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            税额合计
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtTotalTaxHo" runat="server" ReadOnly="true" CssClass="tdinput"
                                Width="95%" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="Hidden6" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            含税金额合计
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtTotalFeeHo" runat="server" ReadOnly="true" CssClass="tdinput"
                                Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            整单折扣(%)
                        </td>
                        <td class="tdColInput" width="23%">
                            <input type="text" id="txtDiscount" onchange="Number_round(this,<%=SelPoint %>)"
                                onblur="fnTotalInfo();" class="tdinput" width="95%" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            折扣金额合计
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtDiscountTotal" runat="server" ReadOnly="true" CssClass="tdinput"
                                Width="95%" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="Hidden7" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            折后含税金额
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtRealTotal" runat="server" ReadOnly="true" CssClass="tdinput"
                                Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            抵应付账款
                        </td>
                        <td class="tdColInput" width="23%">
                            <input type="text" id="txtTotalDyfzk" onchange="Number_round(this,<%=SelPoint %>)"
                                onblur="fnTotalInfo();" class="tdinput" width="95%" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            应退货款合计
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtTotalYthkhj" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        备注信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonNote'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','divButtonNote')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: block">
                    <tr>
                        <td class="tdColTitle" width="10%">
                            制单人
                        </td>
                        <td class="tdColInput" width="23%">
                            <input type="text" id="txtCreatorReal" name="txtCreatorReal" runat="server" cssclass="tdinput"
                                class="tdinput" width="95%" readonly disabled="disabled" />
                            <input type="hidden" id="txtCreator" name="txtCreator" class="tdinput" runat="server"
                                readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtCreateDate" name="txtCreateDate" class="tdinput" runat="server"
                                disabled="disabled" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单据状态
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="ddlBillStatus" class="tdinput" width="119px" id="ddlBillStatus" disabled="disabled">
                                <option value="1">制单</option>
                                <option value="2">执行</option>
                                <option value="3">变更</option>
                                <option value="4">手工结单</option>
                                <option value="5">自动结单</option>
                            </select>
                            <input type="hidden" id="txtBillStatus" name="txtBillStatus" class="tdinput" value="1" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            确认人
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txtConfirmorReal" name="txtConfirmorReal" class="tdinput"
                                disabled="disabled" runat="server" readonly />
                            <input type="hidden" id="txtConfirmor" name="txtConfirmor" class="tdinput" runat="server"
                                readonly />
                            <input name="UserName" id="UserName" runat="server" style="display: none" class="tdinput"
                                type="text" size="15" readonly="readonly" />
                            <input name="UserID" id="UserID" runat="server" style="display: none" class="tdinput"
                                type="text" size="15" readonly="readonly" />
                            <input name="SystemTime" id="SystemTime" runat="server" style="display: none" class="tdinput"
                                type="text" size="15" readonly="readonly" />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td height="20" align="left" class="tdinput">
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="tdinput" disabled Width="95%"
                                Text="" ReadOnly Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            结单人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="text" id="txtCloserReal" name="txtCloserReal" class="tdinput" runat="server"
                                disabled="disabled" readonly />
                            <input type="hidden" id="txtCloser" name="txtCloser" class="tdinput" runat="server"
                                readonly />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            结单日期
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txtCloseDate" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                disabled Text="" ReadOnly Enabled="False"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            最后更新人
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="Hidden4" name="txtConfirmor" value="0" class="tdinput" runat="server"
                                readonly />
                            <input type="text" id="txtModifiedUserIDReal" name="txtModifiedUserIDReal" class="tdinput"
                                disabled="disabled" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" class="tdinput"
                                runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID2" name="txtModifiedUserID2" class="tdinput"
                                runat="server" readonly />
                            <input type="hidden" id="hidintFromType" name="hidintFromType" class="tdinput" runat="server"
                                readonly />
                            <input type="hidden" id="hidListModuleID" name="hidListModuleID" class="tdinput"
                                runat="server" readonly />
                        </td>
                        <td class="tdColTitle">
                            最后更新时间
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" runat="server" CssClass="tdinput" disabled Width="95%"
                                Text="" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="txtModifiedDate2" name="txtModifiedDate2" class="tdinput"
                                runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                            <textarea name="txtremark" id="txtremark" rows="3" cols="80" style="width: 95%"></textarea>
                            <input name='usernametemp' type='hidden' id='usernametemp' runat="server" />
                            <input name='datetemp' type='hidden' id='datetemp' runat="server" />
                            <input id="txtAction" type="hidden" value="1" />
                            <input type="hidden" id="hidIsliebiao" name="hidIsliebiao" runat="server" />
                            <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                            <input type='hidden' id='hfPageAttachment' runat="server" />
                            <input type="hidden" id='Isyinyong' runat="server" />
                            <input type="hidden" id='FlowStatus' runat="server" />
                            <input type="hidden" id="hidModuleID" runat="server" />
                            <input type="hidden" id="hidFromTypeName" name="txtConfirmor" class="tdinput" runat="server"
                                readonly />
                            <input type="hidden" id="hidRate" name="hidRate" class="tdinput" runat="server" readonly />
                            <asp:DropDownList ID="drpApplyReason" runat="server" Width="0" Height="0">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="right" valign="top">
                            <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
                        </td>
                        <td width="8">
                        </td>
                    </tr>
                    <tr>
                        <td height="25" valign="top">
                            &nbsp; <span class="Blue">采购退货单明细</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick3'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('Tb_04','searchClick3')" /></div>
                        </td>
                        <td width="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="Tb_04">
                <!-- Start 销售订单选择 -->
                <!-- End 销售订单选择 -->
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF" valign="bottom">
                            <img src="../../../images/Button/Show_add.jpg" style="cursor: hand" id="imgAdd" onclick="ShowProdInfo();"
                                visible="false" runat="server" />
                            <img alt="" src="../../../Images/Button/UnClick_tj.jpg" style="display: none;" id="imgUnAdd"
                                visible="false" runat="server" />
                            <img src="../../../images/Button/Show_del.jpg" style="cursor: hand" id="imgDel" onclick="DeleteSignRowReject();"
                                visible="false" runat="server" />
                            <img alt="" src="../../../Images/Button/UnClick_del.jpg" style="display: none;" id="imgUnDel"
                                visible="false" runat="server" />
                            <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();"
                                alt="库存快照" />
                            <img alt="从源单选择明细" src="../../../Images/Button/Bottom_btn_From.jpg" id="Get_Potential"
                                style="cursor: hand" onclick="PurchaseRejectSelect()" visible="false" runat="server" />
                            <img alt="从源单选择明细" src="../../../Images/Button/Unclick_From.jpg" id="Get_UPotential"
                                style="display: none" visible="false" runat="server" />
                            <img alt="条码扫描" visible="false" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods"
                                style="cursor: pointer" onclick="GetGoodsInfoByBarCode()" runat="server" />
                            <uc15:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
                        </td>
                    </tr>
                </table>
                <!-- Start Product Info-->
                <!-- End Product Info -->
                <input type="hidden" id="DetailCount" runat="server" value="1" />
                <div id="divDetail" style="width: 99%; overflow-x: auto; overflow-y: auto; height: 180px;">
                    <table width="150%" border="0" id="dg_Log" align="center" cellpadding="0" cellspacing="1"
                        bgcolor="#999999">
                        <tr>
                            <td bgcolor="#E6E6E6" class="Blue" width="50" align="center">
                                <input type="checkbox" name="checkall" id="checkall" onclick="SelectAll()" title="全选"
                                    style="cursor: hand" />
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" align="center">
                                序号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                物品编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                物品名称
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                规格
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                颜色
                            </td>
                            <% if (IsMoreUnit)
                               {%>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                基本单位
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                基本数量
                            </td>
                            <% } %>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                单位
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                源单数量
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                退货数量<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                退货原因<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                单价<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                含税价<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                税率(%)<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                金额
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                含税金额
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                税额
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                备注
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                源单编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                源单序号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                出库数量
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="display: none">
                                入库数量
                            </td>
                        </tr>
                    </table>
                    <!-- Start 库存快照控件 -->
                    <uc12:StorageSnapshot ID="StorageSnapshot1" runat="server" />
                    <!-- End 库存快照控件 -->
                </div>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="2" bgcolor="#999999">
                        </td>
                    </tr>
                </table>
                <br />
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script language="javascript">
var intMasterRejectID = <%=intMasterRejectID %>;
var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.BILL_TYPEFLAG_PURCHASE %>;
var glb_BillTypeCode = <%=XBase.Common.ConstUtil.BILL_TYPEFLAG_PURCHASE_REJECT %>;
var glb_BillID = intMasterRejectID;//单据ID
var glb_IsComplete = true;
var FlowJS_HiddenIdentityID ='txtIndentityID';
var FlowJs_BillNo ='CodingRuleControl1_txtCode';
var FlowJS_BillStatus ='ddlBillStatus';
var isMoreUnit = <%= IsMoreUnit.ToString().ToLower() %>;// 多计量单位控制参数
var selPoint = <%= SelPoint %>;// 小数位数
</script>

<script src="../../../js/office/PurchaseManager/PurchaseRejectAdd.js" type="text/javascript"></script>

<script src="../../../js/common/Flow.js" type="text/javascript"></script>

