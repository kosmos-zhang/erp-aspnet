<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellSendAdd.aspx.cs" Inherits="Pages_Office_SellManager_SellSendAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/sellModuleSelectCustUC.ascx" TagName="sellModuleSelectCustUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/SellModuleSelectTransporter.ascx" TagName="SellModuleSelectTransporter"
    TagPrefix="uc10" %>
<%@ Register Src="../../../UserControl/SelectSellOrderDetailUC.ascx" TagName="SelectSellOrderDetailUC"
    TagPrefix="uc11" %>
<%@ Register Src="../../../UserControl/SelectSellOrderUC.ascx" TagName="SelectSellOrderUC"
    TagPrefix="uc12" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc9" %>
<%@ Register Src="../../../UserControl/SellModuleSelectCurrency.ascx" TagName="SellModuleSelectCurrency"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/Common/StorageSnapshot.ascx" TagName="StorageSnapshot"
    TagPrefix="uc13" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc14" %>
<%@ Register Src="../../../UserControl/Common/ProjectSelectControl.ascx" TagName="ProjectSelectControl" TagPrefix="uc15" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>销售发货单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"> </script>

    <script src="../../../js/office/SellManager/SellSendAdd.js" type="text/javascript"></script>
    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

</head>
<body onload="formatNumLen()">
    <form id="EquipAddForm" runat="server">
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc7:Message ID="Message1" runat="server" />
    <uc8:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <uc13:StorageSnapshot ID="StorageSnapshot1" runat="server" />
    <input type="hidden" id="hiddBusType" />
    <input type="hidden" id="hiddSender" />
    <input id="hiddSeller" type="hidden"  runat="server"/>
    <input type="hidden" id="hiddDeptID" runat="server" />
    <uc4:sellModuleSelectCustUC ID="sellModuleSelectCustUC1" runat="server" />
    <uc5:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <input type='hidden' id="rowIndex" />
    <uc10:SellModuleSelectTransporter ID="SellModuleSelectTransporter1" runat="server" />
    <uc11:SelectSellOrderDetailUC ID="SelectSellOrderDetailUC1" runat="server" />
    <uc12:SelectSellOrderUC ID="SelectSellOrderUC1" runat="server" />
    <uc9:FlowApply ID="FlowApply1" runat="server" />
    <uc6:SellModuleSelectCurrency ID="SellModuleSelectCurrency1" runat="server" />
    <uc15:ProjectSelectControl ID="ProjectSelectControl1" runat="server" /><!--所属项目-->
    <input type="hidden" id='hiddBillStatus' value='1' />
    <input type="hidden" id="hiddOrderID" value='0' />
    <input type="hidden" id="HiddenURLParams" runat="server" />
    <input type ="hidden" id="txtIsMoreUnit" runat="server" /><!--是否启用多单位-->
    <input type="hidden" id="txtIsOverOrder" runat="server" /><!--是否启用超订单发货-->
    <input type="hidden" id="hiddBillingAddModuleid" runat="server" /><!--开票页面ModuleID-->
    <input type="hidden" id="hiddModuleID" runat="server" /><!--销售发货ModuleID-->

    <script type="text/javascript">
    var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.CODING_RULE_SELL %>;
    var glb_BillTypeCode = <%=XBase.Common.ConstUtil.CODING_RULE_SELLSEND_NO %>;
    var glb_BillID = 0;                                //单据ID
    var glb_IsComplete = true;                                          //是否需要结单和取消结单(小写字母)
    var FlowJS_HiddenIdentityID ='hiddOrderID';                      //自增长后的隐藏域ID
    var FlowJs_BillNo ='SendOrderUC_txtCode';          //当前单据编码名称
    var FlowJS_BillStatus ='hiddBillStatus';                             //单据状态ID
    
    var precisionLength=<%=SelPoint %>;//小数精度
    //转换初始化值小数位数
    function formatNumLen()
    {
        var lengthstr="0.";
        var lengthstr2="100.";
        for(var i=0;i<precisionLength;i++)
        {
            lengthstr = lengthstr+"0";
            lengthstr2 = lengthstr2+"0";
        }
        $("#TotalPrice").val(lengthstr);
        $("#Tax").val(lengthstr);
        $("#TotalFee").val(lengthstr);
        $("#DiscountTotal").val(lengthstr);
        $("#RealTotal").val(lengthstr);
        $("#CountTotal").val(lengthstr);
        $("#TransportFee").val(lengthstr);
        $("#Discount").val(lengthstr2);
    }
    </script>
    
    <script src="../../../js/common/Flow.js" type="text/javascript"></script>
    <div>
        <table style="width: 95%;" border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                                新建销售发货单
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                            <table width="100%">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                       
                                        <img runat="server" visible="false" src="../../../images/Button/Bottom_btn_save.jpg"
                                            alt="保存" id="btn_save" style="cursor: hand" onclick="fnCheckProCount('insert');" />
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_update" style="cursor: hand;
                                            margin: 0px; display: none;" border="0" onclick="fnCheckProCount('update');" />
                                        <img runat="server" visible="false" alt="保存" src="../../../Images/Button/UnClick_bc.jpg"
                                            id="imgUnSave" style="display: none;" />
                                        <span runat="server" visible="false" id="GlbFlowButtonSpan"></span>
                                        <img runat="server" visible="false" alt="开票" src="../../../Images/Button/btn_kp.jpg"
                                            id="imgBilling" style="display: none;cursor: hand;" onclick="fnToBilling();"/>
                                        <img runat="server" visible="false" alt="开票" src="../../../Images/Button/btn_kp_un.jpg"
                                            id="imgUnBilling" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" style="display: none; cursor: hand"
                                            alt="返回" id="ibtnBack" onclick="fnBack();" />
                                    </td>
                                    <td align="right" bgcolor="#FFFFFF" style="padding-top: 5px; width: 70px;">
                                        <img id="btnPrint" alt="打印" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印"  onclick="fnPrintOrder()"  />
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
                    <table width="99%" border="0" cellspacing="0" cellpadding="0">
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
                        id="Tb_01">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%;">
                                单据编号<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 23%;">
                                <uc3:CodingRuleControl ID="SendOrderUC" runat="server" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%;">
                                主题
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 23%;">
                                <input id="Title" specialworkcheck="主题" type="text" style="width: 90%;" class="tdinput"
                                    maxlength="50" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%;">
                                源单类型<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF">
                            销售订单
                              <select  style="width: 120px;margin-top:2px;margin-left:2px; display:none;" id="FromType" onchange="fnFromTypeChange(this)">
                              
                                <option value="1">销售订单</option>
                              
                            </select>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                源单编号<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="FromBillID" onclick="fnSelectOfferInfo()" class="tdinput" type="text"
                                    readonly="readonly" style="width: 90%;" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                客户名称<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="CustID" class="tdinput" type="text" readonly="readonly" style="width: 90%;"
                                    onclick="fnSelectCustInfo()" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                业务类型<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <select  style="width: 120px;margin-top:2px;margin-left:2px;" id="BusiType">
                                    <option value="1">普通销售</option>
                                    <option value="2">委托代销</option>
                                    <option value="3">直运</option>
                                    <option value="4">零售</option>
                                    <option value="5">销售调拨</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                销售类别
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="sellTypeUC" runat="server" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结算方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="PayTypeUC" runat="server" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                支付方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="MoneyTypeUC" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                交货方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="TakeType" runat="server" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                运送方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="CarryType" runat="server" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                币种
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="CurrencyType" class="tdinput" type="text" onclick="fnSelectCurrency()"
                                    readonly="readonly" style="width: 90%;" maxlength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                汇率
                            </td>
                            <td align="left" bgcolor="#FFFFFF">
                                <input id="Rate" style="width: 90%;" class="tdinput" type="text" disabled="disabled"
                                    value="0.0000" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                业务员<span class="redbold">*</span>
                            </td>
                            <td align="left" bgcolor="#FFFFFF">
                                <input id="UserSeller" class="tdinput" onclick="fnSelectSeller()" readonly="readonly"
                                    style="width: 90%;" type="text"  runat="server" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                部门<span class="redbold">*</span>
                            </td>
                            <td align="left" bgcolor="#FFFFFF">
                                <input id="DeptId" class="tdinput" onclick="fnSelectDept()" readonly="readonly" style="width: 90%;"
                                    type="text" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                收货人姓名
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="Receiver" class="tdinput" specialworkcheck="收货人姓名" type="text" style="width: 90%;"
                                    maxlength="50" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                发货人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="UserSender" readonly="readonly" style="width: 90%;" class="tdinput" type="text"
                                    onclick="alertdiv('UserSender,hiddSender');" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                预计发货时间
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input style="width: 90%;" readonly="readonly" id="SendDate" class="tdinput" type="text"
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SendDate')})" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                发货地址
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="SendAddr" specialworkcheck="发货地址" class="tdinput" type="text" style="width: 90%;"
                                    maxlength="50" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                收货地址
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="ReceiveAddr" specialworkcheck="收获地址" class="tdinput" type="text" style="width: 90%;"
                                    maxlength="50" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                收货人电话
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="Tel" class="tdinput" specialworkcheck="收货人电话" type="text" style="width: 90%;"
                                    maxlength="25" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                收货人移动电话
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="Modile" class="tdinput" specialworkcheck="收货人移动电话" type="text" style="width: 90%;"
                                    maxlength="25" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                收货人邮编
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="Post" class="tdinput" specialworkcheck="收货人邮编" type="text" style="width: 90%;"
                                    maxlength="7" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                是否增值税
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input type="radio" name="AddTax" id="isAddTax" disabled="disabled" onclick="fnAddTax()" value="1" checked="checked" />是
                                <input type="radio" name="AddTax" id="NotAddTax"  disabled="disabled"  onclick="fnAddTax()" value="0" />否
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                所属项目
                            </td>
                            <td align="left" bgcolor="#FFFFFF">
                                <input id="ProjectID" class="tdinput" type="text" style="width:60%" readonly="readonly" onclick="ShowProjectInfo('ProjectID','hiddProjectID');"/>
                                <%--<a href="#" onclick="fnClearProject();">清除选择</a>--%>
                                <input id="hiddProjectID" type="hidden" runat="server" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                可查看人员
                            </td>
                            <td align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="UserCanViewUserName" style="width: 98%" ReadOnly="true" CssClass="tdinput" runat="server" onclick="alertdiv('UserCanViewUserName,CanViewUser,2');">
                                </asp:TextBox>
                                <input type="hidden" id="CanViewUser" runat="server" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                            </td>
                            <td align="left" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                    </table>
                        <!-- 扩展属性-->
                        <uc14:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            合计信息
                                        </td>
                                        <td align="right">
                                            <div id='searchClick1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','searchClick1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="Tb_02">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                金额合计
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 22%;">
                                <input id="TotalPrice" disabled="disabled" style="width: 90%;" class="tdinput" type="text" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                税额合计
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 22%;">
                                <input id="Tax" disabled="disabled" style="width: 90%;" class="tdinput" type="text" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                含税金额合计
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 23%;">
                                <input id="TotalFee" style="width: 90%;" class="tdinput" type="text" size="15" disabled='disabled' />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                整单折扣(%)<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="Discount" onchange="Number_round(this,<%=SelPoint %>)" value="100.00" onblur="fnCheckPer(this)"
                                    style="width: 90%;" class="tdinput" type="text" maxlength="6" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                折后含税金额
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="RealTotal" style="width: 90%;" class="tdinput" type="text" size="15" disabled='disabled' />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                折扣合计
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="DiscountTotal" style="width: 90%;" class="tdinput" type="text" size="15"
                                    disabled='disabled' />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                发货数量合计
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="CountTotal" style="width: 90%;" class="tdinput" type="text" size="15"
                                    disabled='disabled' />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            运费信息
                                        </td>
                                        <td align="right">
                                            <div id='Div2'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_05','Div2')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_05">
                        <tr>
                            <td height="28" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                运输商
                            </td>
                            <td height="28" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                                <input id="Transporter" readonly="readonly" onclick="popSellTranObj.ShowList('Transporter');"
                                    style="width: 90%;" class="tdinput" type="text" size="15" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                运费合计<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                                <input id="TransportFee" onchange="Number_round(this,<%=SelPoint %>)" style="width: 90%;" value="0.00"
                                    class="tdinput" type="text" size="15" maxlength="8" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                运费结算方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="TransPayType" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            备注信息
                                        </td>
                                        <td align="right">
                                            <div id='searchClick2'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick2')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_03">
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                单据状态
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                                <label id="BillStatus">
                                    制单</label>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                制单人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                                <asp:TextBox ID="Creator" Width="90%" runat="server" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                制单日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="CreateDate" runat="server" Width="90%" CssClass="tdinput" Enabled="false"
                                    R></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                确认人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                                <asp:TextBox ID="Confirmor" runat="server" Width="90%" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                确认日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ConfirmDate" runat="server" Width="90%" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结单人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="Closer" runat="server" Width="90%" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结单日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="CloseDate" runat="server" Width="90%" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ModifiedUserID" runat="server" Width="90%" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ModifiedDate" runat="server" Width="90%" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                交付说明
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="DeliverRemark" specialworkcheck="交付说明" class="tdinput" style="width: 90%"
                                    type="text" maxlength="100" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                包装运输说明
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="PackTransit" class="tdinput" specialworkcheck="包装运输说明" style="width: 90%"
                                    type="text" maxlength="100" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                付款说明
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="PayRemark" class="tdinput" specialworkcheck="付款说明" style="width: 90%"
                                    type="text" maxlength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                备注
                            </td>
                            <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" style="width: 89%;">
                                <input id="Remark" class="tdinput" specialworkcheck="备注" style="width: 90%" type="text"
                                    maxlength="100" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td valign="top" bgcolor="#F4F0ED">
                                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                            </td>
                            <td align="right" valign="top" bgcolor="#F4F0ED">
                                <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td valign="top">
                                            <span class="Blue">发货单明细</span>
                                        </td>
                                        <td align="right" valign="top">
                                            <div id='Div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Table2','Div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                        id="Table2">
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td height="28" bgcolor="#FFFFFF" colspan="11" style="padding-top: 5px; padding-left: 5px;">
                                            
                                            <img runat="server" visible="false" alt="删除" src="../../../images/Button/Show_del.jpg"
                                                id="imgDel" style="cursor: hand" onclick="fnDelOneRow()" />
                                            
                                            <img alt="删除" runat="server" visible="false" src="../../../Images/Button/UnClick_del.jpg"
                                                style="display: none;" id="imgUnDel" />
                                            <img runat="server" visible="false" src="../../../Images/Button/Bottom_btn_From.jpg"
                                                id="btnFromBill" alt="选择明细" style="cursor: hand" onclick="fnSelectOrderList();" />
                                            <img runat="server" visible="false" src="../../../Images/Button/Unclick_From.jpg"
                                                id="btnUnFromBill" style="display: none;" alt="选择明细" />&nbsp;
                                            <img id="btnSubSnapshot" src="../../../images/Button/btn_kckz.jpg" style="cursor: hand"
                                                onclick="ShowSnapshot();" alt="库存快照" />
                                            <uc2:CodeTypeDrpControl ID="PackageUC" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                    <div id="divDetail" style="width: 100%; background-color: #FFFFFF; overflow: scroll;">
                                      <div style="border:none; width:200%">
                                        <table width="100%" border="0" id="dg_Log" style="behavior: url(../../../draggrid.htc);
                                            height: auto;" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                            <tr>
                                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 3%">
                                                    选择<input type="checkbox" visible="false" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 7%" class="ListTitle">
                                                    物品编号<span class="redbold">*</span>
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 7%" class="ListTitle">
                                                    物品名称
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 3%" class="ListTitle">
                                                    规格
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 3%" class="ListTitle">
                                                    颜色
                                                </td>
                                                <td align="center" id="BaseUnitD" runat="server" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                                    基本单位
                                                </td>
                                                <td align="center" id="BaseCountD" runat="server" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                                    基本数量
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 3%" class="ListTitle">
                                                    单位
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                    订单数量
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                    已执行数量
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                    本次发货数量<span class="redbold">*</span>
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 6%" class="ListTitle">
                                                    发货日期<span class="redbold">*</span>
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                    包装要求
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                    单价<span class="redbold">*</span>
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                    含税价<span class="redbold">*</span>
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 3%" class="ListTitle">
                                                    折扣(%)<span class="redbold">*</span>
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 3%" class="ListTitle">
                                                    税率(%)<span class="redbold">*</span>
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 6%" class="ListTitle">
                                                    含税金额
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 6%" class="ListTitle">
                                                    金额
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                    税额
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                    备注
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 3%" class="ListTitle">
                                                    源单类型
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 6%" class="ListTitle">
                                                    源单编号
                                                </td>
                                                <td align="center" bgcolor="#E6E6E6" style="width: 3%" class="ListTitle">
                                                    源单行号
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                        <div style="height: 20px; width: 200%">
                                        </div>
                                    </div>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="2" bgcolor="#999999">
                                        </td>
                                    </tr>
                                </table>
                                <input type='hidden' id='txtTRLastIndex' value="0" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    </form>
</body>
</html>
