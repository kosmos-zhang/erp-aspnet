<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubSellBackAdd.aspx.cs" Inherits="Pages_Office_SubStoreManager_SubSellBackAdd" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/SubSellOrderSelect.ascx" TagName="SubSellOrderSelect"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/SubSellOrderSelectUC.ascx" TagName="SubSellOrderSelectUC"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/ProductSubSellOrder.ascx" TagName="ProductSubSellOrder"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/SubGetGoodsInfoByBarCode.ascx" TagName="SubGetGoodsInfoByBarCode"
    TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc9" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>销售退货管理</title>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/Office/SubStoreManager/SubSellBackAdd.js" type="text/javascript"></script>

    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="txtIsMoreUnit" runat="server" />
    <input type="hidden" id="hidSelPoint" runat="server" />
    <input type="hidden" id="hidCompanyCD" runat="server" />
    <div id="divSBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="SBackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <uc3:ProductSubSellOrder ID="ProductSubSellOrder1" runat="server" />
    <uc2:Message ID="Message1" runat="server" />
    <uc4:SubSellOrderSelect ID="SubSellOrderSelect1" runat="server" />
    <uc5:SubSellOrderSelectUC ID="SubSellOrderSelectUC1" runat="server" />
    <uc6:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc7:SubGetGoodsInfoByBarCode ID="SubGetGoodsInfoByBarCode1" runat="server" />
    <uc9:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <span id="Forms" class="Spantype" name="Forms"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <div style="display: none">
                <input type="text" id="ThisID" />
                <%--当前单据的ID--%>
                <input type="text" id="HiddenAction" />
                <%--当前可操作状态，Add，Update--%>
                <input type="text" id="hfPageAttachment" />
                <%--附件--%>
                <select id="StorageHid" runat="server">
                </select>
            </div>
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
                                新建销售退货单</div>
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
                                        <img id="imgSave" src="../../../images/Button/Bottom_btn_save.jpg" onclick="InsertSubSellBack();"
                                            style="cursor: pointer" title="保存销售退货单" visible="false" runat="server" />
                                        <img alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="imgUnSave" style="display: none;"
                                            visible="false" runat="server" />
                                        <img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确认" id="btn_confirm"
                                            style="cursor: hand; display: none" border="0" onclick="Fun_ConfirmOperate();"
                                            visible="false" runat="server" />
                                        <img src="../../../Images/Button/UnClick_qr.jpg" alt="确认" id="btn_Unconfirm" style="cursor: hand;
                                            display: inline" border="0" visible="false" runat="server" />
                                        <img src="../../../Images/Button/btn_qxmk.jpg" alt="取消确认" id="btn_Qxconfirm" style="cursor: hand;
                                            display: none" border="0" onclick="Fun_QxConfirmOperate();" visible="false" runat="server" />
                                        <img src="../../../Images/Button/btn_uqxqr.jpg" alt="取消确认" id="btn_UnQxconfirm" style="cursor: hand;
                                            display: none" border="0" visible="false" runat="server" />
                                        <img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确认" id="btn_ruku" style="cursor: hand;
                                            display: none" border="0" onclick="Fun_RukuOperate();" visible="false" runat="server" />
                                        <img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确认" id="btn_jiesuan"
                                            style="cursor: hand; display: none" border="0" onclick="Fun_JiesuanOperate();"
                                            visible="false" runat="server" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor: hand;
                                            display: none;" onclick="Back();" />
                                        <!-- Start 审批 -->
                                        <!-- End 审批 -->
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印" onclick="PrintSellBack();" />
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
                            <div id="divInputNo" runat="server">
                                <uc1:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <div id="divSubSellBackNo" runat="server" class="tdinput" style="display: none">
                            </div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单据主题
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="tdinput" Width="95%" SpecialWorkCheck="单据主题"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            销售分店<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtDeptName" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                Enabled="False" ReadOnly="True"></asp:TextBox>
                            <input type="hidden" id="HidDeptID" runat="server" />
                            <input type="hidden" id="HidDeptID2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            源单类型<div id="divyuandan" runat="server" style="display: inline">
                                <span class="redbold">*</span></div>
                        </td>
                        <td class="tdColInput" width="23%">
                            <select name="drpFromType" class="tdinput" id="drpFromType" onchange="DeleteAll();">
                                <option value="0">无来源</option>
                                <option value="1" selected="selected">销售订单</option>
                            </select>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            对应分店销售订单<div id="divIsbishuxiang" runat="server" style="display: inline">
                                <span class="redbold">*</span></div>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtOrderID" MaxLength="25" runat="server" onclick="popSubSellOrderBBBB()"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="HidOrderID" runat="server" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            发货模式<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="24%">
                            <select name="ddlSendMode" class="tdinput" id="ddlSendMode" onchange="SendModeSelect();">
                                <option value="1" selected="selected">分店发货</option>
                                <option value="2">总部发货</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            发货时间
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtOutDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOutDate')})"
                                ReadOnly="true" CssClass="tdinput" Width="35%"></asp:TextBox>
                            <select id="ddlOutDateHour">
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
                            <select id="ddlOutDateMin">
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
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="UserOutUserID" runat="server" onclick="alertdiv('UserOutUserID,HidOutUserID');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="HidOutUserID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            是否增值税
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="checkbox" id="chkisAddTax" runat="server" onclick="fnChangeAddTax()" /><label
                                id="labAddTax">非增值税</label>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户名称
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtCustName" runat="server" MaxLength="100" CssClass="tdinput" Width="95%"
                                SpecialWorkCheck="客户名称"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户联系电话
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtCustTel" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"
                                SpecialWorkCheck="客户联系电话"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户手机号
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtCustMobile" runat="server" MaxLength="50" CssClass="tdinput"
                                Width="95%" SpecialWorkCheck="客户手机号"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            币种<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="drpCurrencyType" class="tdinput" runat="server" onchange="ChangeCurreny()"
                                id="drpCurrencyType">
                            </select>
                            <input type="text" runat="server" id="CurrencyTypeID" name="CurrencyTypeID" class="tdinput"
                                style="display: none" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            汇率
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtRate" runat="server" CssClass="tdinput" Width="95%" ReadOnly="true"
                                Enabled="False"></asp:TextBox><input type="hidden" id="hiddenRate" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            业务状态
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="drpBusiStatus" class="tdinput" id="drpBusiStatus" disabled="disabled">
                                <option value="1" selected="selected">退单</option>
                                <option value="2">入库</option>
                                <option value="3">结算</option>
                                <option value="4">完成</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            退货时间<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtBackDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtBackDate')})"
                                ReadOnly="true" CssClass="tdinput" Width="35%"></asp:TextBox>
                            <select id="ddlBackDateHour">
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
                            <select id="ddlBackDateMin">
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
                        <td align="right" class="tdColTitle" width="10%">
                            退货处理人<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="UserSeller" runat="server" onclick="alertdiv('UserSeller,HidSeller');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="HidSeller" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            入库时间<div id="divInDate" runat="server" style="display: none">
                                <span class="redbold">*</span></div>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtInDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtInDate')})"
                                ReadOnly="true" CssClass="tdinput" Width="35%"></asp:TextBox>
                            <select id="ddlInDateHour">
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
                            <select id="ddlInDateMin">
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
                        <td align="right" class="tdColTitle" width="10%">
                            入库人<div id="divInUserID" runat="server" style="display: none">
                                <span class="redbold">*</span></div>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="UserInUserID" runat="server" onclick="alertdiv('UserSeller,HidInUserID');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="HidInUserID" runat="server" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            结算时间<div id="divSttlDate" runat="server" style="display: none">
                                <span class="redbold">*</span></div>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtSttlDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtSttlDate')})"
                                ReadOnly="true" CssClass="tdinput" Width="35%"></asp:TextBox>
                            <select id="ddlSttlDateHour">
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
                            <select id="ddlSttlDateMin">
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
                            结算人<div id="divSttlUserID" runat="server" style="display: none">
                                <span class="redbold">*</span></div>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="UserSttlUserID" runat="server" onclick="alertdiv('UserSttlUserID,HidSttlUserID');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="HidSttlUserID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户地址
                        </td>
                        <td height="20" class="tdColInput" width="90%" colspan="5">
                            <asp:TextBox ID="txtCustAddr" MaxLength="100" runat="server" CssClass="tdinput" Width="95%"
                                SpecialWorkCheck="客户地址"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            退货理由描述
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea name="txtBackReason" id="txtBackReason" rows="3" cols="80" style="width: 95%"></textarea>
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
                            <input type="text" id="txtDiscount" onchange="Number_round(this,<%=hidSelPoint.Value %>)"
                                onblur="fnTotalInfo1();" class="tdinput" width="95%" />
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
                            应退货款
                        </td>
                        <td class="tdColInput" width="23%">
                            <input type="text" id="txtWairPayTotal" onchange="Number_round(this,<%=hidSelPoint.Value %>)"
                                onblur="fnTotalInfo1();" class="tdinput" width="95%" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            结算货款
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="text" id="txtSettleTotal" onchange="Number_round(this,<%=hidSelPoint.Value %>)"
                                onblur="fnTotalInfo1();" class="tdinput" width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            已退货款
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPayedTotal" runat="server" ReadOnly="true" CssClass="tdinput"
                                Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            应退货款余额
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtWairPayTotalOverage" runat="server" ReadOnly="true" CssClass="tdinput"
                                Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                        </td>
                        <td class="tdColInput" width="24%">
                            &nbsp;
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
                        <td align="right" class="tdColTitle">
                            确认人
                        </td>
                        <td class="tdColInput">
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
                            <input name="SystemTime2" id="SystemTime2" runat="server" style="display: none" class="tdinput"
                                type="text" size="25" readonly="readonly" />
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <input name='usernametemp' type='hidden' id='usernametemp' runat="server" />
                            <input name='datetemp' type='hidden' id='datetemp' runat="server" />
                            <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                            <input type="hidden" id="hidIsliebiao" name="hidIsliebiao" runat="server" />
                            <input type="hidden" id="hidBusiStatusName" name="hidBusiStatusName" runat="server" />
                            <input type="hidden" id="hidBillStatusName" name="hidBillStatusName" runat="server" />
                            <input type="hidden" id="hidIsliebiaoSendMode" name="hidIsliebiaoSendMode" runat="server" />
                            <input type="hidden" id="hidIsliebiaoFromType" name="hidIsliebiaoFromType" runat="server" />
                            <input type="hidden" id="hidIsliebiaoCurrencyType" name="hidIsliebiaoCurrencyType"
                                runat="server" />
                            <input id="txtAction" type="hidden" value="1" />
                        </td>
                        <td align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td align="left" class="tdinput">
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="tdinput" disabled Width="95%"
                                Text="" ReadOnly Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle">
                            最后更新人
                        </td>
                        <td class="tdColInput">
                            <input type="text" id="txtModifiedUserIDReal" name="txtModifiedUserIDReal" class="tdinput"
                                disabled="disabled" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" class="tdinput"
                                runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID2" name="txtModifiedUserID2" class="tdinput"
                                runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            结单人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="text" id="txtCloserReal" name="txtCloserReal" class="tdinput" runat="server"
                                disabled="disabled" readonly />
                            <input type="hidden" id="txtCloser" name="txtCloser" class="tdinput" runat="server"
                                readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            结单日期
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txtCloseDate" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                disabled Text="" ReadOnly Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle">
                            最后更新日期
                        </td>
                        <td align="left" class="tdinput">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                disabled Width="95%" Text=""></asp:TextBox>
                            <input type="hidden" id="txtModifiedDate2" name="txtModifiedDate2" class="tdinput"
                                runat="server" readonly />
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
                                <div id="divDownLoadResume" style="display: inline">
                                    <a href="#" onclick="DealResume('download');">下载附件</a></div>
                                <div id="divDeleteResume" style="display: inline">
                                    <a href="#" onclick="DealResume('clear');">删除附件</a></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100">
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                            <textarea name="txtRemark" id="txtRemark" rows="3" cols="80" style="width: 95%"></textarea>
                            <input type="hidden" id="hidModuleID" runat="server" />
                            <asp:DropDownList ID="drpStorageID" runat="server" Width="0" Height="0">
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
                            &nbsp; <span class="Blue">销售退货单明细</span>
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
                            <img src="../../../images/Button/Show_add.jpg" id="imgAdd" onclick="AddSignRow();"
                                style="display: none; cursor: hand" visible="false" runat="server" />
                            <img alt="" src="../../../Images/Button/UnClick_tj.jpg" id="imgUnAdd" visible="false"
                                runat="server" />
                            <img src="../../../images/Button/Show_del.jpg" style="cursor: hand" id="imgDel" onclick="DeleteSignRowSubSellBack();"
                                visible="false" runat="server" />
                            <img alt="" src="../../../Images/Button/UnClick_del.jpg" style="display: none;" id="imgUnDel"
                                visible="false" runat="server" />
                            <img alt="从源单选择明细" src="../../../Images/Button/Bottom_btn_From.jpg" id="Get_Potential"
                                style="cursor: hand" onclick="SubSellBackSelect()" visible="false" runat="server" />
                            <img alt="从源单选择明细" src="../../../Images/Button/Unclick_From.jpg" id="Get_UPotential"
                                style="display: none" visible="false" runat="server" />
                            <img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" visible="false" id="btnGetGoods"
                                style="cursor: pointer; display: none" onclick="GetGoodsInfo()" runat="server" />
                            <img alt="条码扫描" visible="false" runat="server" src="../../../Images/Button/btn_tmsmu.jpg"
                                id="unbtnGetGoods" />
                        </td>
                    </tr>
                </table>
                <!-- Start Product Info-->
                <!-- End Product Info -->
                <table width="99%" border="0" id="dg_Log" align="center" cellpadding="0" cellspacing="1"
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
                            产品编号<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            产品名称
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            批次
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            规格
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicUnitTd">
                            基本单位
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicNumTd">
                            基本数量
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            单位
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            发货数量
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            退货数量<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            单价
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            含税价
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            折扣
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            税率
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
                            仓库<div id="storagedetail" runat="server" style="display: none">
                                <span class="redbold">*</span></div>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            源单编号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            源单序号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            备注
                        </td>
                    </tr>
                </table>
                <br />
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
                <input name='TempData' type='hidden' id='TempData' />
            </td>
        </tr>
    </table>
    <div>
    </div>
    </form>
</body>
</html>
