<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubSellOrderAdd.aspx.cs"
    Inherits="Pages_Office_StoreManager_SubSellOrderAdd" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/ProductSubSellOrder.ascx" TagName="ProductSubSellOrder"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/SubGetGoodsInfoByBarCode.ascx" TagName="SubGetGoodsInfoByBarCode"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>销售订单</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/Office/SubStoreManager/SubSellOrderAdd.js" type="text/javascript"></script>

    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="txtIsMoreUnit" runat="server" />
    <input type="hidden" id="hidSelPoint" runat="server" />
    <input type="hidden" id="hidCompanyCD" runat="server" />
    <uc5:ProductSubSellOrder ID="ProductSubSellOrder1" runat="server" />
    <uc6:SubGetGoodsInfoByBarCode ID="SubGetGoodsInfoByBarCode1" runat="server" />
    <uc8:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
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
                            <div id="divTitle">
                            </div>
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
                                        <img runat="server" visible="false" id="imgSave" src="../../../images/Button/Bottom_btn_save.jpg"
                                            onclick="fnSave();" style="cursor: pointer" title="保存销售订单" />
                                        <img runat="server" visible="false" alt="保存" src="../../../Images/Button/UnClick_bc.jpg"
                                            id="imgUnSave" style="display: none;" />
                                        <!-- Start 审批 -->
                                        <!-- End 审批 -->
                                        <img runat="server" visible="false" id="imgConfirm" alt="确认" src="../../../Images/Button/Bottom_btn_confirm.jpg"
                                            style="display: none" onclick="fnConfirm();" />
                                        <img runat="server" visible="false" id="imgUnConfirm" alt="确认" src="../../../Images/Button/UnClick_qr.jpg" />
                                        <img runat="server" visible="false" src="../../../Images/Button/btn_qxmk.jpg" alt="取消确认"
                                            id="btn_Qxconfirm" style="cursor: hand; display: none" border="0" onclick="fnConcelConfirm();" />
                                        <img runat="server" visible="false" src="../../../Images/Button/btn_uqxqr.jpg" alt="取消确认"
                                            id="btn_UnQxconfirm" style="cursor: hand;" border="0" />
                                        <img runat="server" visible="false" id="imgConfirmOut" alt="确认" src="../../../Images/Button/Bottom_btn_confirm.jpg"
                                            onclick="fnConfirmOut();" style="display: none" />
                                        <img runat="server" visible="false" id="imgConfirmSett" alt="确认" src="../../../Images/Button/Bottom_btn_confirm.jpg"
                                            onclick="fnConfirmSett();" style="display: none" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="imgBack" style="cursor: hand;
                                            display: none" onclick="fnBack();" />
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印" onclick="PrintSubSellBill();" />
                                    </td>
                                </tr>
                            </table>
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
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单据编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <uc1:CodingRuleControl ID="OrderNo" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            主题
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtTitle" class="tdinput" style="width: 99%" specialworkcheck="主题" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            销售分店
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txtDeptName" style="width: 99%" class="tdinput" runat="server"
                                disabled="disabled" />
                            <input type="hidden" id="hidDeptID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            发货模式<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <select id="ddlSendMode" style="width: 99%" onchange="fnChangeSendMode()" class="tdinput">
                                <option value="1" selected="selected">分店发货</option>
                                <option value="2">总部发货</option>
                            </select>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            业务员<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <input type="text" id="UserSellerName" style="width: 99%" class="tdinput" readonly="readonly"
                                onclick="alertdiv('UserSellerName,hidSellerID');" />
                            <input type="hidden" id="hidSellerID" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            <a href='#' id="ColCust" onclick="fnShowCust();">客户名称</a> <a id="ColCust2" style="display: none">
                                客户名称</a><span class="redbold">*</span>
                            <input type="hidden" id="hidCustSearch" />
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="text" id="txtCustName" style="width: 99%" class="tdinput" ondblclick="fnShowCust();"
                                specialworkcheck="客户名称" />
                            <div style="display: none">
                                <input type="text" id="ThisID" />
                                <%--当前单据的ID--%>
                                <input type="text" id="HiddenAction" value="Add" />
                                <input type="text" id="ddd" value="111" />
                                <%--当前可操作状态，Add，Update--%>
                                <input type="text" id="hfPageAttachment" />
                                <%--附件--%>
                                <input type="text" id="SearchCondition" />
                                <select id="StorageHid" runat="server">
                                </select>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户联系电话
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtCustTel" class="tdinput" style="width: 99%" specialworkcheck="客户联系电话" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户手机号
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtCustMobile" class="tdinput" style="width: 99%" specialworkcheck="客户手机号" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            下单日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txtOrderDate" style="width: 99%" runat="server" class="tdinput"
                                readonly="readonly" onclick="WdatePicker();" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            币种<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="ddlCurrencyType" class="tdinput" runat="server" width="119px" id="ddlCurrencyType"
                                onchange="fnChangeCurrency()">
                            </select>
                            <input type="text" runat="server" id="CurrencyTypeID" name="CurrencyTypeID" class="tdinput"
                                style="display: none" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            汇率
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtRate" runat="server" class="tdinput" style="width: 99%"
                                disabled="disabled" />
                            <input type="hidden" id="hiddenRate" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            订货方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc2:CodeTypeDrpControl ID="ddlOrderMethod" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            交货方式
                        </td>
                        <td class="tdColInput" width="23%">
                            <uc2:CodeTypeDrpControl ID="ddlTakeType" runat="server" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            结算方式
                        </td>
                        <td class="tdColInput" width="23%">
                            <uc2:CodeTypeDrpControl ID="ddlPayType" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            支付方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc2:CodeTypeDrpControl ID="ddlMoneyType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            是否为增值税
                        </td>
                        <td height="20" class="tdColInput" width="23%" align="left">
                            <input type="checkbox" id="chkisAddTax" onclick="fnTotal(1);" checked="checked" /><label
                                id="AddTax">是增值税</label>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                        </td>
                    </tr>
                </table>
                <uc7:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        发货信息
                                    </td>
                                    <td align="right">
                                        <div id='div1'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Table1','div1')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Table1" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            预约发货时间
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtPlanOutDate" style="width: 35%" class="tdinput" readonly="readonly"
                                onclick="WdatePicker();" />
                            <select id="ddlPlanOutHour">
                                <option value="00">00</option>
                                <option value="01">01</option>
                                <option value="02">02</option>
                                <option value="03">03</option>
                                <option value="04">04</option>
                                <option value="05">05</option>
                                <option value="06">06</option>
                                <option value="07">07</option>
                                <option value="08">08</option>
                                <option value="09">09</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                            </select>
                            <label>
                                时</label>
                            <select id="ddlPlanOutMin">
                                <option value="00">00</option>
                                <option value="01">01</option>
                                <option value="02">02</option>
                                <option value="03">03</option>
                                <option value="04">04</option>
                                <option value="05">05</option>
                                <option value="06">30</option>
                                <option value="07">07</option>
                                <option value="08">08</option>
                                <option value="09">09</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                                <option value="24">24</option>
                                <option value="25">25</option>
                                <option value="26">26</option>
                                <option value="27">27</option>
                                <option value="28">28</option>
                                <option value="29">29</option>
                                <option value="30">30</option>
                                <option value="31">31</option>
                                <option value="32">32</option>
                                <option value="33">33</option>
                                <option value="34">34</option>
                                <option value="35">35</option>
                                <option value="36">36</option>
                                <option value="37">37</option>
                                <option value="38">38</option>
                                <option value="39">39</option>
                                <option value="40">40</option>
                                <option value="41">41</option>
                                <option value="42">42</option>
                                <option value="43">43</option>
                                <option value="44">44</option>
                                <option value="45">45</option>
                                <option value="46">46</option>
                                <option value="47">47</option>
                                <option value="48">48</option>
                                <option value="49">49</option>
                                <option value="50">50</option>
                                <option value="51">51</option>
                                <option value="52">52</option>
                                <option value="53">53</option>
                                <option value="54">54</option>
                                <option value="55">55</option>
                                <option value="56">56</option>
                                <option value="57">57</option>
                                <option value="58">58</option>
                                <option value="59">59</option>
                            </select>
                            <label>
                                分</label>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            运送方式
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <uc2:CodeTypeDrpControl ID="ddlCarryType" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            配送部门
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                            <input type="text" id="DeptOutDeptName" style="width: 99%" class="tdinput" onclick="alertdiv('DeptOutDeptName,hidOutDeptID');"
                                disabled="disabled" />
                            <input type="hidden" id="hidOutDeptID" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            实际发货时间
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtOutDate" style="width: 35%" class="tdinput" onclick="WdatePicker();"
                                disabled="disabled" />
                            <select id="ddlOutHour" disabled="disabled">
                                <option value="00">00</option>
                                <option value="01">01</option>
                                <option value="02">02</option>
                                <option value="03">03</option>
                                <option value="04">04</option>
                                <option value="05">05</option>
                                <option value="06">06</option>
                                <option value="07">07</option>
                                <option value="08">08</option>
                                <option value="09">09</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                            </select>
                            <label>
                                时</label>
                            <select id="ddlOutMin" disabled="disabled">
                                <option value="00">00</option>
                                <option value="01">01</option>
                                <option value="02">02</option>
                                <option value="03">03</option>
                                <option value="04">04</option>
                                <option value="05">05</option>
                                <option value="06">30</option>
                                <option value="07">07</option>
                                <option value="08">08</option>
                                <option value="09">09</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                                <option value="24">24</option>
                                <option value="25">25</option>
                                <option value="26">26</option>
                                <option value="27">27</option>
                                <option value="28">28</option>
                                <option value="29">29</option>
                                <option value="30">30</option>
                                <option value="31">31</option>
                                <option value="32">32</option>
                                <option value="33">33</option>
                                <option value="34">34</option>
                                <option value="35">35</option>
                                <option value="36">36</option>
                                <option value="37">37</option>
                                <option value="38">38</option>
                                <option value="39">39</option>
                                <option value="40">40</option>
                                <option value="41">41</option>
                                <option value="42">42</option>
                                <option value="43">43</option>
                                <option value="44">44</option>
                                <option value="45">45</option>
                                <option value="46">46</option>
                                <option value="47">47</option>
                                <option value="48">48</option>
                                <option value="49">49</option>
                                <option value="50">50</option>
                                <option value="51">51</option>
                                <option value="52">52</option>
                                <option value="53">53</option>
                                <option value="54">54</option>
                                <option value="55">55</option>
                                <option value="56">56</option>
                                <option value="57">57</option>
                                <option value="58">58</option>
                                <option value="59">59</option>
                            </select>
                            <label>
                                分</label>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            发货人
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="UserOutUserName" style="width: 99%" class="tdinput" onclick="alertdiv('UserOutUserName,hidOutUserID');"
                                disabled="disabled" />
                            <input type="hidden" id="hidOutUserID" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            业务状态
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                            <input type="text" id="txtBusiStatus" class="tdinput" value="下单" disabled="disabled" />
                            <input type="hidden" id="hidBusiStatus" value="1" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            发货地址
                        </td>
                        <td class="tdColInput" colspan="5">
                            <input type="text" id="txtCustAddr" class="tdinput" style="width: 99%" specialworkcheck="发货地址" />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        安装信息
                                    </td>
                                    <td align="right">
                                        <div id='div2'>
                                            <img src="../../../images/Main/Close.jpg" alt="" style="cursor: pointer" onclick="oprItem('Table2','div2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Table2" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            是否需要安装
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="checkbox" id="chkNeedSetup" onclick="fnChangeNeedSetup()" /><label id="NeedSetup">不需要安装</label>
                            <uc4:Message ID="Message1" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            预约安装时间
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtPlanSetDate" style="width: 35%" readonly="readonly" class="tdinput"
                                onclick="WdatePicker();" disabled="disabled" />
                            <select id="ddlPlanSetHour" disabled="disabled">
                                <option value="00">00</option>
                                <option value="01">01</option>
                                <option value="02">02</option>
                                <option value="03">03</option>
                                <option value="04">04</option>
                                <option value="05">05</option>
                                <option value="06">06</option>
                                <option value="07">07</option>
                                <option value="08">08</option>
                                <option value="09">09</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                            </select>
                            <label>
                                时</label>
                            <select id="ddlPlanSetMin" disabled="disabled">
                                <option value="00">00</option>
                                <option value="01">01</option>
                                <option value="02">02</option>
                                <option value="03">03</option>
                                <option value="04">04</option>
                                <option value="05">05</option>
                                <option value="06">30</option>
                                <option value="07">07</option>
                                <option value="08">08</option>
                                <option value="09">09</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                                <option value="24">24</option>
                                <option value="25">25</option>
                                <option value="26">26</option>
                                <option value="27">27</option>
                                <option value="28">28</option>
                                <option value="29">29</option>
                                <option value="30">30</option>
                                <option value="31">31</option>
                                <option value="32">32</option>
                                <option value="33">33</option>
                                <option value="34">34</option>
                                <option value="35">35</option>
                                <option value="36">36</option>
                                <option value="37">37</option>
                                <option value="38">38</option>
                                <option value="39">39</option>
                                <option value="40">40</option>
                                <option value="41">41</option>
                                <option value="42">42</option>
                                <option value="43">43</option>
                                <option value="44">44</option>
                                <option value="45">45</option>
                                <option value="46">46</option>
                                <option value="47">47</option>
                                <option value="48">48</option>
                                <option value="49">49</option>
                                <option value="50">50</option>
                                <option value="51">51</option>
                                <option value="52">52</option>
                                <option value="53">53</option>
                                <option value="54">54</option>
                                <option value="55">55</option>
                                <option value="56">56</option>
                                <option value="57">57</option>
                                <option value="58">58</option>
                                <option value="59">59</option>
                            </select>
                            <label>
                                分</label>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            实际安装时间
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                            <input type="text" id="txtSetDate" style="width: 35%" class="tdinput" onclick="WdatePicker();"
                                disabled="disabled" />
                            <select id="ddlSetHour" disabled="disabled">
                                <option value="00">00</option>
                                <option value="01">01</option>
                                <option value="02">02</option>
                                <option value="03">03</option>
                                <option value="04">04</option>
                                <option value="05">05</option>
                                <option value="06">06</option>
                                <option value="07">07</option>
                                <option value="08">08</option>
                                <option value="09">09</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                            </select>
                            <label>
                                时</label>
                            <select id="ddlSetMin" disabled="disabled">
                                <option value="00">00</option>
                                <option value="01">01</option>
                                <option value="02">02</option>
                                <option value="03">03</option>
                                <option value="04">04</option>
                                <option value="05">05</option>
                                <option value="06">30</option>
                                <option value="07">07</option>
                                <option value="08">08</option>
                                <option value="09">09</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                                <option value="24">24</option>
                                <option value="25">25</option>
                                <option value="26">26</option>
                                <option value="27">27</option>
                                <option value="28">28</option>
                                <option value="29">29</option>
                                <option value="30">30</option>
                                <option value="31">31</option>
                                <option value="32">32</option>
                                <option value="33">33</option>
                                <option value="34">34</option>
                                <option value="35">35</option>
                                <option value="36">36</option>
                                <option value="37">37</option>
                                <option value="38">38</option>
                                <option value="39">39</option>
                                <option value="40">40</option>
                                <option value="41">41</option>
                                <option value="42">42</option>
                                <option value="43">43</option>
                                <option value="44">44</option>
                                <option value="45">45</option>
                                <option value="46">46</option>
                                <option value="47">47</option>
                                <option value="48">48</option>
                                <option value="49">49</option>
                                <option value="50">50</option>
                                <option value="51">51</option>
                                <option value="52">52</option>
                                <option value="53">53</option>
                                <option value="54">54</option>
                                <option value="55">55</option>
                                <option value="56">56</option>
                                <option value="57">57</option>
                                <option value="58">58</option>
                                <option value="59">59</option>
                            </select>
                            <label>
                                分</label>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            安装工人及联系电话
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="UserSetUserID" class="tdinput" style="width: 99%" disabled="disabled"
                                specialworkcheck="安装工人" />
                            <input type="hidden" id="hidSetUserID" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                        </td>
                    </tr>
                </table>
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
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            数量总计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtCountTotal" name="txtCountTotal" class="tdinput" disabled="disabled" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            金额合计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtTotalPrice" name="txtTotalPrice" maxlength="50" class="tdinput"
                                disabled="disabled" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            税额合计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                            <input type="text" id="txtTotalTax" name="txtTotalTax" class="tdinput" disabled="disabled" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            含税金额合计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtTotalFee" name="txtTotalFee" class="tdinput" disabled="disabled" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            整单折扣
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtDiscount" name="txtDiscount" maxlength="50" class="tdinput"
                                value="100" onchange="Number_round(this,2);fnTotal(0);" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            折扣金额合计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                            <input type="text" id="txtDiscountTotal" name="txtDiscountTotal" class="tdinput"
                                disabled="disabled" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            折后含税金额
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtRealTotal" name="txtRealTotal" class="tdinput" disabled="disabled" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            已结金额
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtPayedTotal" name="txtPayedTotal" maxlength="50" class="tdinput"
                                disabled="disabled" onkeydown="FractionDigits(this);" onchange="fnTotal(0);" />
                        </td>
                        <td class="tdColTitle">
                            货款余额
                        </td>
                        <td class="tdColInput">
                            <input type="text" id="txtWairPayTotal" class="tdinput" disabled="disabled" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle">
                            本次结算金额
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txtThisPayedHid" value="0" />
                            <input type="text" id="txtThisPayed" class="tdinput" onblur="Number_round(this,$('#hidSelPoint').val());fnTotal(1);" />
                        </td>
                        <td class="tdColTitle">
                        </td>
                        <td class="tdColInput">
                        </td>
                        <td class="tdColTitle">
                        </td>
                        <td class="tdColInput">
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
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="hidden" id="txtCreatorID" name="txtCreatorID" class="tdinput" runat="server"
                                readonly />
                            <input type="text" id="txtCreatorName" name="txtCreatorName" class="tdinput" runat="server"
                                disabled="disabled" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtCreateDate" class="tdinput" runat="server" disabled="disabled" />
                        </td>
                        <td class="tdColTitle" width="10%">
                            单据状态
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" class="tdinput"
                                value="1" />
                            <input type="text" id="txtBillStatusName" name="txtBillStatusName" class="tdinput"
                                value="制单" readonly disabled="disabled" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle">
                            确认人
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txtConfirmorID" name="txtConfirmorID" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtConfirmorName" name="txtConfirmorName" class="tdinput"
                                disabled="disabled" runat="server" readonly />
                        </td>
                        <td align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td align="left" class="tdinput">
                            <input type="text" id="txtConfirmorDate" class="tdinput" runat="server" disabled="disabled" />
                        </td>
                        <td align="right" class="tdColTitle">
                            最后更新人
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtModifiedUserName" name="txtModifiedUserName" class="tdinput"
                                disabled="disabled" runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            结单人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="hidden" id="txtCloserID" name="txtCloserID" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtCloserName" name="txtCloserName" class="tdinput" runat="server"
                                disabled="disabled" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            结单日期
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="text" id="txtCloseDate" class="tdinput" runat="server" disabled="disabled" />
                        </td>
                        <td align="right" class="tdColTitle">
                            最后更新日期
                        </td>
                        <td align="left" class="tdinput">
                            <input type="text" id="txtModifiedDate" class="tdinput" runat="server" disabled="disabled" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100">
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                            <input type="text" id="txtRemark" class="tdinput" runat="server" style="width: 99%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100">
                            附件
                        </td>
                        <td class="tdColInput" colspan="5">
                            <div id="divUploadResume" runat="server">
                                <a href="#" onclick="DealResume('upload');">上传附件</a>
                            </div>
                            <div id="divDealResume" runat="server" style="display: none;">
                                <a href="#" id="attachname" onclick="DealResume('download');">下载附件</a> <a href="#"
                                    onclick="DealResume('clear');">删除附件</a>
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="Sttl" <%--style="display:none"--%>>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            结算信息
                                        </td>
                                        <td align="right">
                                            <div id='div3'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Table3','div3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="Table3" style="display: block">
                        <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">
                                是否已建单
                            </td>
                            <td height="20" align="left" class="tdColInput" width="23%">
                                <input id="chkisOpenbill" name="chkisOpenbill" type="checkbox" onclick="fnChangeOpenBill();" /><label
                                    id="OpenBill">未开票</label>
                            </td>
                            <td height="20" align="right" class="tdColTitle" width="10%">
                                结算人
                            </td>
                            <td height="20" align="left" class="tdColInput" width="23%">
                                <input id="SttlUser" type="text" class="tdinput" style="width: 99%" disabled="disabled" />
                                <input id="SttlUserID" type="hidden" class="tdinput" style="width: 99%" />
                            </td>
                            <td height="20" align="right" class="tdColTitle" width="10%">
                                结算时间
                            </td>
                            <td height="20" align="left" class="tdColInput" width="24%">
                                <input id="SettDate" type="text" class="tdinput" style="width: 99%" disabled="disabled" />
                            </td>
                        </tr>
                    </table>
                </div>
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
                            &nbsp; <span class="Blue">销售订单明细</span>
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
                            <img runat="server" visible="false" src="../../../images/Button/Show_add.jpg" style="cursor: hand"
                                id="imgAdd" onclick="fnAddSignRow('DetailTable');" />
                            <img runat="server" visible="false" src="../../../images/Button/Show_del.jpg" style="cursor: hand"
                                id="imgDel" onclick="fnDelSignRow('DetailTable');" />
                            <img runat="server" visible="false" alt="" src="../../../Images/Button/UnClick_tj.jpg"
                                style="display: none;" id="imgUnAdd" />
                            <img runat="server" visible="false" alt="" src="../../../Images/Button/UnClick_del.jpg"
                                style="display: none;" id="imgUnDel" />
                            <img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" visible="false" id="btnGetGoods"
                                style="cursor: pointer;" onclick="GetGoodsInfo()" runat="server" />
                            <img alt="条码扫描" visible="false" style="display: none" runat="server" src="../../../Images/Button/btn_tmsmu.jpg"
                                id="unbtnGetGoods" />
                            <uc3:ProductInfoControl ID="ProductInfoControl1" runat="server" />
                        </td>
                    </tr>
                </table>
                <!-- Start Product Info-->
                <!-- End Product Info -->
                <table width="99%" border="0" id="DetailTable" align="center" cellpadding="0" cellspacing="1"
                    bgcolor="#999999">
                    <tr>
                        <td bgcolor="#E6E6E6" width="50" align="center" style="width: 5%">
                            选择
                            <input id="checkall" type="checkbox" onclick="fnSelectAll('DetailTable');" />
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" align="center">
                            序号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            物品编号<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            物品名称
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            批次
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            规格
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            仓库
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicUnitTd">
                            基本单位
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicNumTd">
                            基本数量
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            数量<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            退货数量
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            单位
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            单价<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            含税价
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            折扣<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            税率<span class="redbold">*</span>
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
                    </tr>
                </table>
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="0" />
                <input name='TempData' type='hidden' id='TempData' />
            </td>
        </tr>
    </table>
    <div>
    </div>
    <div id="layoutCust" style="display: none">
        <!--提示信息弹出详情start-->
        <div id="divCust">
            <iframe id="frmCust"></iframe>
        </div>
        <div id="divCustInfo" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
            width: 800px; z-index: 100; position: absolute; top: 35%; left: 45%; margin: 5px 0 0 -400px;">
            <table width="100%">
                <tr>
                    <td>
                        <a onclick="fnCloseCust()" style="text-align: right; cursor: pointer">关闭</a>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" border="0" align="left" cellpadding="0" id="searchtable" cellspacing="0"
                            bgcolor="#CCCCCC">
                            <tr>
                                <td bgcolor="#FFFFFF">
                                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                        class="table">
                                        <tr style="height: 21px">
                                            <td width="10%" <%--style=" border:solid 1px red"--%> height="21" bgcolor="#E7E7E7"
                                                align="center">
                                                客户名称
                                            </td>
                                            <td width="15%" bgcolor="#FFFFFF">
                                                <input type="text" id="txtCustNameSelect" specialworkcheck="客户名称" class="tdinput"
                                                    style="width: 90%" />
                                            </td>
                                            <td width="10%" height="21" bgcolor="#E7E7E7" align="center">
                                                客户电话
                                            </td>
                                            <td width="15%" bgcolor="#FFFFFF">
                                                <input type="text" id="txtCustTelSelect" specialworkcheck="客户电话" class="tdinput"
                                                    style="width: 90%" />
                                            </td>
                                            <td width="10%" bgcolor="#E7E7E7" align="center">
                                                客户手机
                                            </td>
                                            <td width="15%" bgcolor="#FFFFFF">
                                                <input type="text" id="txtCustMobileSelect" specialworkcheck="客户手机" class="tdinput"
                                                    style="width: 90%" />
                                            </td>
                                            <td width="10%" bgcolor="#E7E7E7" align="center">
                                                送货地址
                                            </td>
                                            <td width="15%" bgcolor="#FFFFFF">
                                                <input type="text" id="txtCustAddrSelect" specialworkcheck="送货地址" class="tdinput"
                                                    style="width: 90%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 45%" colspan="8" bgcolor="#FFFFFF">
                                                <%--runat="server" visible="false"--%>
                                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                    onclick='fnSelectCust()' id="btn_search" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1CustInfo"
                                        bgcolor="#999999">
                                        <tbody>
                                            <tr>
                                                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                                    选择
                                                </th>
                                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                                    <div class="orderClick" onclick="OrderBy('CustName','Span1');return false;">
                                                        客户名称<span id="Span1" class="orderTip"></span></div>
                                                </th>
                                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                                    <div class="orderClick" onclick="OrderBy('CustTel','Span2');return false;">
                                                        客户电话<span id="Span2" class="orderTip"></span></div>
                                                </th>
                                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                                    <div class="orderClick" onclick="OrderBy('CustMobile','oC2');return false;">
                                                        客户手机号<span id="oC2" class="orderTip"></span></div>
                                                </th>
                                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                                    <div class="orderClick" onclick="OrderBy('CustAddr','Span3');return false;">
                                                        送货地址<span id="Span3" class="orderTip"></span></div>
                                                </th>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br />
                                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                                        class="PageList">
                                        <tr>
                                            <td height="28" background="../../../images/Main/PageList_bg.jpg">
                                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                                    <tr>
                                                        <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                                            <div id="pageCustInfocount">
                                                            </div>
                                                        </td>
                                                        <td height="28" align="right">
                                                            <div id="pageDataList1_PagerCustInfo" class="jPagerBar">
                                                            </div>
                                                        </td>
                                                        <td height="28" align="right">
                                                            <div id="divPageCustInfo">
                                                                <input name="text" type="text" id="Text2CustInfo" style="display: none" />
                                                                <span id="pageDataList1_TotalCustInfo"></span>每页显示
                                                                <input name="text" type="text" id="ShowPageCountCustInfo" style="width: 22px" />条
                                                                转到第
                                                                <input name="text" type="text" id="ToPageCustInfo" style="width: 35px" />页
                                                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                                    width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexCustInfo($('#ShowPageCountCustInfo').val(),$('#ToPageCustInfo').val());" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <!--提示信息弹出详情end-->
    </div>
    </form>
    <span id="Forms" class="Spantype" name="Forms"></span>
</body>
</html>
