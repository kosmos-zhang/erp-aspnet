<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseContract_Add.aspx.cs" Inherits="Pages_Office_PurchaseManager_PurchaseContract_Add" %>


<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRuleControl" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc2" %>
<%@ Register src="../../../UserControl/PurchaseAskPriceSelectUC.ascx" tagname="PurchaseAskPriceSelectUC" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/PurchasePlanSelectUC.ascx" tagname="PurchasePlanSelectUC" tagprefix="uc4" %>
<%@ Register src="../../../UserControl/ProviderSelect.ascx" tagname="ProviderSelect" tagprefix="uc5" %>
<%@ Register src="../../../UserControl/PurchaseApplySelectUC.ascx" tagname="PurchaseApplySelectUC" tagprefix="uc6" %>
<%@ Register src="../../../UserControl/ProductInfoControl.ascx" tagname="ProductInfoControl" tagprefix="uc7" %>


<%@ Register src="../../../UserControl/FlowApply.ascx" tagname="FlowApply" tagprefix="uc8" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode" TagPrefix="uc15" %> 
        <%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc12" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc9" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购合同</title>
    
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
<body onload="document.body.style.display='';" style="display: none">
    <form id="Form1" runat="server">
    
    <input type="hidden" id="IsDisplayPrice" runat="server" value="" />
    <input id="HiddenPoint" type="hidden" runat="server" />
<input id="HiddenMoreUnit" type="hidden" runat="server" />


    <div id="divPBackShadow" style="display: none">
    <iframe src="../../../Pages/Common/MaskPage.aspx" id="PBackShadowIframe" frameborder="0"
        width="100%"></iframe></div>
    <uc5:ProviderSelect ID="ProviderSelect1" runat="server" />
    <uc6:PurchaseApplySelectUC ID="PurchaseApplySelectUC1" runat="server" />
    <uc7:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc8:FlowApply ID="FlowApply1" runat="server" />
    <uc2:Message ID="Message1" runat="server" />
    <uc3:PurchaseAskPriceSelectUC ID="PurchaseAskPriceSelectUC1" runat="server" />
    <uc4:PurchasePlanSelectUC ID="PurchasePlanSelectUC1" runat="server" />
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
                            <td height="30" align="center" class="Title">
                            <div id="divTitle" runat="server">新建采购合同
                                </div>
                        </td>
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
                                        &nbsp;<img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server"  alt="保存" id="imgSave"  visible="false" runat="server" style="cursor:pointer" onclick="InsertPurchaseContract();"/>
                                        <img alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="imgUnSave" style="display: none;"  visible="false" runat="server"/><span id="GlbFlowButtonSpan" visible="false" runat="server"></span><span id="Forms" class="Spantype"></span>
                                        <%--<img src="../../../Images/Button/Bottom_btn_Yd.jpg" alt="源单总览" id="Get_Potential" onclick="PurchasePlanSelect001();" style="cursor: hand;" border="0" /><span lang="zh-cn"></span>
                                        <img src="../../../Images/Button/Bottom_btn_uYd.jpg" alt="源单总览" id="Get_UPotential" style="cursor: hand; display:none;"  border="0" />--%>
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" onclick="Back();" /></td>
                                    <td align="right">
                                        <img  src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style=" float:right; cursor: pointer;"  id="imgPrint"  onclick="PurchaseContractPrint();" /> 
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <!-- End 流程处理-->
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <input type="hidden" id="txtIsliebiaoNo" value="0" runat="server" />
                            <input name='usernametemp' type='hidden' id='usernametemp' runat="server" />
                            <input name='datetemp' type='hidden' id='datetemp' runat="server" />
                            <input id="txtAction" type="hidden" value="1" />
                            <input type="hidden" id="hidIsliebiao" name="hidIsliebiao" runat="server"/>
                            <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                            <input type='hidden' id='hfPageAttachment' runat="server" />
                            <input type="hidden" id='Isyinyong' runat="server" />
                            <input type="hidden" id='FlowStatus' runat="server" />
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
                                <uc1:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <div id="divPurchaseContractNo" runat="server" class="tdinput"   style="display: none">
                            </div>
                        </td>
                        <td align="right" class="style2" width="10%">
                            合同主题 
                        </td>
                        <td class="style3" width="23%">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="合同主题" ></asp:TextBox>
                        </td>
                        <td align="right" class="style2" width="10%">
                            源单类型
                        </td>
                        <td class="style3" width="24%">
                                        <select name="ddlFromType" class="tdinput" width="119px" id="ddlFromType" onchange="DeleteAll();">
                                        <option value="0" selected="selected">无来源</option>
                                        <option value="1">采购申请</option>
                                        <option value="2">采购计划</option>
                                        <option value="3">采购询价单</option>
                                        </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            供应商<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                             <asp:TextBox ID="txtProviderID" onclick ="PopPurProviderInfo();" runat="server" CssClass="tdinput"  ReadOnly="true"  Width="95%"></asp:TextBox>
                             <input type="hidden" id="txtHidProviderID" runat="server" />
                             <input name="txtHiddenProviderID1"  id="txtHiddenProviderID1" type="text" class="tdinput" size="15"  disabled="disabled" style="display:none" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购类别
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                             <select name="DrpTypeID" class="tdinput" runat="server" id="DrpTypeID"></select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购员<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="UsertxtSeller" runat="server" onclick="alertdiv('UsertxtSeller,txtHidOurUserID');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtHidOurUserID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            部门
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="DeptDeptID" onclick="alertdiv('DeptDeptID,HidDeptID');" runat="server"
                             ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                             <input type="hidden" id="HidDeptID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            是否增值税
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:CheckBox ID="chkIsZzs" runat="server" onclick="fnChangeAddTax();" />
                            <div id="chkisAddTaxText1" runat="server"  style="display:none" >是</div>
                            <div id="chkisAddTaxText2" runat="server"  style="display:none" >否</div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            支付方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="DrpMoneyType" class="tdinput" width="119px" runat="server" id="DrpMoneyType"> </select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            签约时间<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtSignDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtSignDate')})" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            供应商签约人
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtOppositerUserID" MaxLength="25" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            我方签约人
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtOurUserID" runat="server" onclick="alertdiv('txtOurUserID,txtHidOurUserID1');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtHidOurUserID1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            交货方式
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                             <select name="DrpTakeType" class="tdinput" width="119px" runat="server" id="DrpTakeType"> </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            运送方式
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="DrpCarryType" class="tdinput" width="119px" runat="server" id="DrpCarryType"> </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            结算方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="DrpPayType" class="tdinput" width="119px" runat="server" id="DrpPayType"> </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            币种<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                             <select name="drpCurrencyType" class="tdinput" width="119px" runat="server" onchange="ChangeCurreny()" id="drpCurrencyType"> </select>
                            <input type="text" runat="server" id="CurrencyTypeID" name="CurrencyTypeID" class="tdinput" style="display:none"/>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            汇率
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtRate" runat="server"  CssClass="tdinput" Width="95%" 
                                ReadOnly="true" Enabled="False"></asp:TextBox> 
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            签约地点
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <asp:TextBox ID="txtAddress" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <uc9:GetExtAttributeControl 
                                    ID="GetExtAttributeControl1" runat="server" />
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
                            采购数量合计
                        </td>
                        <td class="tdColInput" width="23%">
                                <asp:TextBox ID="txtCountTotal" runat="server" value="0.00" ReadOnly="true" 
                                    CssClass="tdinput" Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            金额合计
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtTotalMoney"  runat="server" ReadOnly="true" value="0.00" 
                                CssClass="tdinput" Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            税额合计
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtTotalTaxHo" runat="server" ReadOnly="true" value="0.00" 
                                CssClass="tdinput" Width="95%" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="Hidden6" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            含税金额合计
                        </td>
                        <td class="tdColInput" width="23%">
                                <asp:TextBox ID="txtTotalFeeHo" runat="server" ReadOnly="true" value="0.00" 
                                    CssClass="tdinput" Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            整单折扣(%)
                        </td>
                        <td class="tdColInput" width="23%">
                            <%--<asp:TextBox ID="txtDiscount" runat="server" value="100" onblur="Zhengdanzhekou();" CssClass="tdinput" Width="95%"></asp:TextBox>--%>
                            <asp:TextBox ID="txtDiscounth" runat="server" value="100" onchange="Number_round(this,2)" onblur="fnTotalInfo();" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            折扣金额合计
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtDiscountTotal" runat="server" ReadOnly="true" value="0.00" 
                                CssClass="tdinput" Width="95%" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="Hidden7" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            折后含税金额
                        </td>
                        <td class="tdColInput" width="23%">
                                <asp:TextBox ID="txtRealTotal" runat="server" ReadOnly="true" value="0.00" 
                                    CssClass="tdinput" Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td class="tdColInput" width="23%">
                            &nbsp;</td>
                        <td align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td class="tdColInput" width="24%">
                            &nbsp;</td>
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
                            <input type="text" id="txtCreatorReal" name="txtCreatorReal" runat="server"  
                                CssClass="tdinput"  class="tdinput" Width="95%"  readonly disabled="disabled" />
                            <input type="hidden" id="txtCreator" name="txtCreator" class="tdinput" runat="server" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtCreateDate" name="txtCreateDate" class="tdinput" 
                                runat="server" disabled="disabled" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单据状态
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="ddlBillStatus" class="tdinput" width="119px" id="ddlBillStatus"disabled="disabled">
                             <option value="1" >制单</option>
                             <option value="2">执行</option>
                             <option value="3">变更</option>
                             <option value="4">手工结单</option>
                             <option value="5">自动结单</option></select>
                             <input type="hidden" id="txtBillStatus" name="txtBillStatus" class="tdinput" value="1" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" >
                            确认人
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txtConfirmorReal" name="txtConfirmorReal" 
                                class="tdinput" disabled="disabled" runat="server" readonly />
                                <input type="hidden" id="hidModuleID" runat="server" />
                            <input type="hidden" id="txtConfirmor" name="txtConfirmor" class="tdinput" runat="server" readonly />
                            <input name="UserName" id="UserName" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"  />
                            <input name="UserID" id="UserID" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"/></td>
                            <input name="SystemTime" id="SystemTime" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"/>
                            <input type="hidden" id="hidFromTypeName" name="hidFromTypeName" class="tdinput" runat="server" readonly />
                            <input type="hidden" id="hidRate" name="hidRate" class="tdinput" runat="server" readonly />
                            <input type="hidden" id="hidintFromType" name="hidintFromType" class="tdinput" runat="server" readonly />
                            <input type="hidden" id="hidListModuleID" name="hidListModuleID" class="tdinput" runat="server" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td height="20" align="left" class="tdinput">
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="tdinput" disabled 
                                Width="95%" Text="" readonly Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            最后更新人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="text" id="txtModifiedUserIDReal" name="txtModifiedUserIDReal" 
                                class="tdinput" disabled="disabled" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" class="tdinput" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID2" name="txtModifiedUserID2" class="tdinput" runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            结单人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="text" id="txtCloserReal" name="txtCloserReal" class="tdinput" 
                                runat="server" disabled="disabled" readonly />
                            <input type="hidden" id="txtCloser" name="txtCloser" class="tdinput" runat="server" readonly />
                        </td>
                        <td class="tdColTitle">
                        结单日期
                        </td>
                        <td class="tdColInput">
                        <input type="hidden" id="Hidden4" name="txtConfirmor" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtCloseDate" name="txtCloseDate" class="tdinput"  
                                runat="server" readonly disabled="disabled" />
                        </td>
                        <td class="tdColTitle">
                        最后更新时间
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" runat="server"  CssClass="tdinput" disabled 
                                Width="95%" Text="" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="txtModifiedDate2" name="txtModifiedDate2" class="tdinput" runat="server" readonly />
                        </td>
                    </tr>   
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            附件
                        </td>
                        <td class="tdColInput" colspan="5">
                                <div id="divUploadResume" runat="server">
                                    <a href="#" onclick="DealResume('upload');">上传附件</a>
                                </div>
                                <div id="divDealResume" runat="server" style="display:none;">
                                    <a href="#" onclick="DealResume('download');">下载附件</a>
                                    <a href="#" onclick="DealResume('clear');">删除附件</a>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                        <textarea name="txtRemark" id="txtRemark" rows="3" cols="80" style="width:95%"></textarea>
                        <asp:DropDownList ID="drpApplyReason" runat="server" Width="0" Height="0"></asp:DropDownList>
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
                            &nbsp; <span class="Blue">采购合同明细</span>
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
                            <img src="../../../images/Button/Show_add.jpg" style="cursor: hand" id="imgAdd" 
                                onclick="ShowProdInfo();"  visible="false" runat="server"/>
                            <img alt="" src="../../../Images/Button/UnClick_tj.jpg" style="display: none;" id="imgUnAdd"  visible="false" runat="server"/>
                            <img src="../../../images/Button/Show_del.jpg" style="cursor: hand" id="imgDel"
                                onclick="DeleteSignRowContract();"  visible="false" runat="server"/>
                            <img alt="" src="../../../Images/Button/UnClick_del.jpg"  style="display: none;" id="imgUnDel"  visible="false" runat="server"/>
                            <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();" alt="库存快照" />
                            <img alt="从源单选择明细" src="../../../Images/Button/Bottom_btn_From.jpg" id="Get_Potential" style="cursor: hand" onclick="PurchasePlanSelect001()"  visible="false" runat="server"/>
                            <img alt="从源单选择明细" src="../../../Images/Button/Unclick_From.jpg" id="Get_UPotential"  style="display: none"  visible="false" runat="server"/>
                            <img alt="条码扫描" visible="false" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods"  style="cursor: pointer" onclick="GetGoodsInfoByBarCode()" runat="server"   />
                            <uc15:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
                            
                        </td>
                    </tr>
                </table>
                <!-- Start Product Info-->
                <!-- End Product Info -->
                <input type="hidden" id="DetailCount" runat="server" value="1"/>
                <div id="divDetail" style="width: 99%; overflow-x: auto; overflow-y: auto; height: 180px;">
                <%--<table width="99%" border="0" id="dg_Log" 
                    align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">--%>
                <table width="150%"   id="dg_Log" 
                    align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td bgcolor="#E6E6E6" class="Blue" width="50" align="center">
                            <input type="checkbox" name="checkall" id="checkall" onclick="SelectAll()" title="全选"
                                style="cursor: hand" />
                        </td>
                        <%--<td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            选择
                        </td>--%>
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
                           <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                           <span id="spUnitID">单位</span>  
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:3%; display:none " id="spUsedUnitID">
                           单位 
                        </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            <span id="SpProductCount">采购数量</span><span class="redbold" id="spCount">*</span>
                        </td>
                          <td align="center" bgcolor="#E6E6E6" class="ListTitle"  valign="middle" style="width:5%; display:none " id="spUsedUnitCount">
                            采购数量 <span class="redbold">*</span>
                        </td>
                        
                         <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" id="spUnitPrice" >
                            单价<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:4%; display:none " id="spUsedPrice" >
                            单价<span class="redbold">*</span>
                        </td>
                         
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            <span class="redbold">*</span>含税价
                        </td>
                      <%--  <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            折扣(%)
                        </td>--%>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            <span class="redbold">*</span>税率(%)
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
                            交货日期
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            申请原因
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
                            已订购数量
                        </td>
                    </tr>
                </table>
                 <!-- Start 库存快照控件 -->
                <uc12:StorageSnapshot ID="StorageSnapshot1" runat="server" />
                <!-- End 库存快照控件 -->
                </div>
                
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1">
                    <tr>
                        <td height="2" bgcolor="#999999">
                        </td>
                    </tr>
                </table>
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
            </td>
        </tr>
    </table>
    <%--<uc5:ProviderSelect ID="ProviderSelect1" runat="server" />
    <uc6:PurchaseApplySelectUC ID="PurchaseApplySelectUC1" runat="server" />
    <uc7:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc8:FlowApply ID="FlowApply1" runat="server" />--%>
    </form>
</body>
</html>

<script language="javascript">
var intMasterPurchaseContractID = <%=intMasterPurchaseContractID %>;
var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.BILL_TYPEFLAG_PURCHASE %>;
var glb_BillTypeCode = <%=XBase.Common.ConstUtil.BILL_TYPEFLAG_PURCHASE_CONTRACT %>;
var glb_BillID = intMasterPurchaseContractID;//单据ID
var glb_IsComplete = true;
var FlowJS_HiddenIdentityID ='txtIndentityID';
var FlowJs_BillNo ='CodingRuleControl1_txtCode';
var FlowJS_BillStatus ='ddlBillStatus';
  eventObj.Table = 'dg_Log';
</script>

<script src="../../../js/office/PurchaseManager/PurchaseContractAdd.js" type="text/javascript"></script>
<script src="../../../js/common/Flow.js" type="text/javascript"></script>
