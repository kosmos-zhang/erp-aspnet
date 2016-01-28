<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseOrderAdd.aspx.cs"
    Inherits="Handler_Office_PurchaseManager_PurchaseOrderAdd" %>

<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/PurchaseManager/PurchaseApplyUC.ascx" TagName="PurchaseApplyUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/PurchaseManager/PurchasePlanUC.ascx" TagName="PurchasePlanUC"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/PurchaseAskPriceSelectUC.ascx" TagName="PurchaseAskPriceSelectUC"
    TagPrefix="uc6" %>
    <%@ Register src="../../../UserControl/PurchaseManager/PurchaseContractUC2.ascx" tagname="PurchaseContractUC2" tagprefix="uc7" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc9" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc10" %>
<%@ Register src="../../../UserControl/ProductInfoControl.ascx" tagname="ProductInfoControl" tagprefix="uc8" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode" TagPrefix="uc15" %> 
       <%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc12" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc11" %>
<%@ Register src="../../../UserControl/Common/ProjectSelectControl.ascx" tagname="ProjectSelectControl" tagprefix="uc13" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>采购订单</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>



    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/office/PurchaseManager/PurchaseOrderAdd.js" type="text/javascript"></script> 
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <input type="hidden" id="IsDisplayPrice" runat="server" value="" />
    <input id="HiddenPoint" type="hidden" runat="server" />
<input id="HiddenMoreUnit" type="hidden" runat="server" />

           <uc8:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <div id="divPBackShadow" style="display: none">
    <iframe src="../../../Pages/Common/MaskPage.aspx" id="PBackShadowIframe" frameborder="0"
        width="100%"></iframe></div>
    <uc4:PurchaseApplyUC ID="PurchaseApplyUC1" runat="server" />
     <uc3:ProviderInfo ID="ProviderInfo1" runat="server" />
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
                        <td height="30" align="center" class="Title" id="AddTitle">
                            新建采购订单                      
                         
                            
                        </td>
                        <td height="30" align="center" class="Title" id="UpdateTitle" style="display:none">
                            采购订单                            
                        </td>
                        <input type="hidden" id="HiddenAction" runat="server" value="Add" />
                            <input type="hidden" id="IsCite" runat="server" value="False" />
                            <input type="hidden" id="FlowStatus" runat="server"/>
                            <input type="hidden" id="HiddenURLParams" runat="server" />
                            <input type="hidden" name='ThisID' id='ThisID' runat="server" value="0" />
                            <input type='hidden' id='hfPageAttachment' runat="server" />
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img runat="server" visible="false" id="imgSave" src="../../../images/Button/Bottom_btn_save.jpg" onclick="SavePurchaseOrder();"
                                            style="cursor: pointer" title="保存" />
                                        <img runat="server" visible="false" alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="imgUnSave" style="display: none;" />       
                                            <span id="GlbFlowButtonSpan" runat="server" visible="false"></span>
                                        <img  src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" 
                                            style="cursor: hand;display:none" onclick="BackPurchaseOrder();" />
                                        <!-- Start 审批 -->
                                        <uc10:FlowApply ID="FlowApply1" runat="server" />
                                        <!-- End 审批 -->
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer" 
                                            title="打印"  onclick="fnPrint();"/>
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <!-- End 流程处理-->
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
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
                                        <div id='searchClick2'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick2')" /></div>
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
                            <div id="divCodeRule" runat="server">
                                <uc2:CodingRuleControl ID="PurOrderNo" runat="server" />
                            </div>
                            <div id="divPurOrderNo" runat="server" class="tdinput" style="display: none">
                            </div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            主题 
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                     
                            <asp:TextBox ID="txtTitle" MaxLength="50" runat="server" class="tdinput"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购类别
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc1:CodeTypeDrpControl ID="ddlTypeID" runat="server" />
                        </td> 
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购员<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="UserPurchaserName" MaxLength="50" onclick="alertdiv('UserPurchaserName,txtPurchaserID');"  ReadOnly="true"
                                runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtPurchaserID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            部门<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="DeptName" MaxLength="50" runat="server"  onclick="alertdiv('DeptName,txtDeptID');" ReadOnly="true"
                                CssClass="tdinput" Width="95%" ></asp:TextBox>
                            <input type="hidden" id="txtDeptID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            交货方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc1:CodeTypeDrpControl ID="ddlTakeType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            源单类型
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="ddlFromType" class="tdinput" width="119px" id="ddlFromType" onchange="fnChangeSource(this.value);">
                                <option value="0" selected="selected">无来源</option>
                                <option value="1">采购申请</option>
                                <option value="2">采购计划</option>
                                <option value="3">采购询价</option>
                                <option value="4">采购合同</option>
                            </select>
                        </td>
                        <%--<td height="20" align="right" class="tdColTitle" width="10%">
                            下单日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPurchaseDate" MaxLength="50" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="95%" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtPurchaseDate')})"></asp:TextBox>
                        </td>--%>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            签单日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtOrderDate" MaxLength="50" runat="server"  CssClass="tdinput" ReadOnly="true"
                                Width="95%" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOrderDate')})"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            运送方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc1:CodeTypeDrpControl ID="ddlCarryType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            供应商<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                           
                            <asp:TextBox ID="txtProviderName" MaxLength="50" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="95%" onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName');"></asp:TextBox>
                            <input type="hidden" id="txtProviderID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            供方订单号
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtProviderBillID" MaxLength="50" runat="server" 
                                CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            结算方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc1:CodeTypeDrpControl ID="ddlPayType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            币种
                        </td>
                        <td class="tdColInput" width="23%">
                            <select name="ddlCurrencyType" class="tdinput" runat="server" width="119px" id="ddlCurrencyType"
                                onchange="ChangeCurreny()">
                            </select>
                            <input type="text" runat="server" id="CurrencyTypeID" name="CurrencyTypeID" class="tdinput"
                                style="display: none" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            汇率
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtExchangeRate" MaxLength="50" runat="server" CssClass="tdinput"  Enabled="false"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            支付方式
                        </td>
                        <td class="tdColInput" width="24%">
                            <uc1:CodeTypeDrpControl ID="ddlMoneyType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            我方代表<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="UserOurDelegate" MaxLength="50" runat="server" CssClass="tdinput"   onclick="alertdiv('UserOurDelegate,txtOurDelegateID');" ReadOnly="true"
                                Width="95%"></asp:TextBox>
                            <input type="text" id="txtOurDelegateID" name="txtOurDelegateID" class="tdinput" 
                                style="display: none" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            对方代表
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtTheyDelegate" MaxLength="50" runat="server" 
                                CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            是否为增值税
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="checkbox" id="chkIsAddTax" checked="checked" onclick="fnChangeAddTax();" /><label id="AddTax">是增值税</label>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            所属项目 
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtProject" runat="server" onclick="ShowProjectInfo('txtProject','hidProjectID');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="hidProjectID" runat="server" /> 
                            <uc13:ProjectSelectControl ID="ProjectSelectControl2" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                          
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                       
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                         
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                        
                        </td>
                    </tr>
                </table>
                <uc11:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
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
                            <input type="text" id="txtCountTotal" name="txtCountTotal" class="tdinput" value="0.00"
                                readonly disabled />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            金额合计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtTotalPrice" name="txtTotalPrice" maxlength="50" class="tdinput"
                                value="0.00" readonly disabled />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            税额合计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                            <input type="text" id="txtTotalTax" name="txtTotalTax" class="tdinput" value="0.00"
                                readonly disabled />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            含税金额合计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtTotalFee" name="txtTotalFee" class="tdinput" value="0.00"
                                readonly disabled />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            整单折扣
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtDiscount" name="txtDiscount" maxlength="50" class="tdinput"
                                onblur="Number_round(this,2);fnMergeDetail();" value="100" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            折扣金额
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                            <input type="text" id="txtDiscountTotal" name="txtDiscountTotal" class="tdinput"
                                value="0.00" readonly disabled />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            折后含税金额
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtRealTotal" name="txtRealTotal" class="tdinput" value="0.00"
                                readonly disabled />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            其他费用支出合计
                            
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtOtherTotal" name="txtOtherTotal" maxlength="50" class="tdinput"
                                value="0.00" />
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
                                disabled readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtCreateDate" MaxLength="50" runat="server" CssClass="tdinput"
                                disabled Width="95%" Text=""></asp:TextBox>
                        </td>
                        <td class="tdColTitle" width="10%">
                            单据状态
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" class="tdinput"
                                value="1" />
                            <input type="text" id="txtBillStatusName" name="txtBillStatusName" class="tdinput"
                                value="制单" readonly disabled />
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
                                disabled runat="server" readonly />
                        </td>
                        <td align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td align="left" class="tdinput">
                            <asp:TextBox ID="txtConfirmorDate" MaxLength="50" runat="server" CssClass="tdinput"
                                disabled Width="95%" Text=""></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle">
                            最后更新人
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtModifiedUserName" name="txtModifiedUserName" class="tdinput"
                                disabled runat="server" readonly />
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
                                disabled readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            结单日期
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txtCloseDate" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                disabled Text=""></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle">
                            最后更新日期
                        </td>
                        <td align="left" class="tdinput">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                disabled Width="95%" Text=""></asp:TextBox>
                            <input type="hidden" id="DetailCount" runat="server" value="1" />
                            <uc6:PurchaseAskPriceSelectUC ID="PurchaseAskPriceSelectUC1" runat="server" />
                            <uc5:PurchasePlanUC ID="PurchasePlanUC1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100">
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                            <uc7:PurchaseContractUC2 ID="PurchaseContractUC21" runat="server" />
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="tdinput" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100">
                            附件
                        </td>
                        <td class="tdColInput" colspan="5">
                            <uc9:Message ID="Message1" runat="server" />
                            <div id="divUploadResume" runat="server">
                                    <a href="#" onclick="DealResume('upload');">上传附件</a>
                                </div>
                                <div id="divDealResume" runat="server" style="display:none;">
                                    <a href="#" id="attachname" onclick="DealResume('download');">下载附件</a>
                                    <a href="#" onclick="DealResume('clear');">删除附件</a>
                                </div>
                        </td>
                    </tr>
                           <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">可查看该采购订单的人员</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
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
                            &nbsp; <span class="Blue">采购订单明细</span>
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
                            <img runat="server" visible="false" src="../../../images/Button/Show_add.jpg" style="cursor: hand" id="imgAdd" onclick="ShowProdInfo();" />
                            <img runat="server" visible="false" src="../../../images/Button/Show_del.jpg"  style="cursor: hand" id="imgDel" onclick="DeleteOrderSignRow();" />
                            <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();" alt="库存快照" />
                            <img runat="server" visible="false" src="../../../Images/Button/Bottom_btn_From.jpg" style="cursor: hand; display: none;" id="imgGetDtl" onclick="SelectSource();" />
                            <img runat="server" visible="false" alt="" src="../../../Images/Button/UnClick_tj.jpg" style="display: none;" id="imgUnAdd" />
                            <img runat="server" visible="false" alt="" src="../../../Images/Button/UnClick_del.jpg" style=" display: none;" id="imgUnDel" />
                            <img runat="server" visible="false" alt="从源单选择明细" src="../../../Images/Button/Unclick_From.jpg" id="imgUnGetDtl"  style="display: inline" />
                            <img alt="条码扫描" visible="false" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods"  style="cursor: pointer" onclick="GetGoodsInfoByBarCode()" runat="server"   />
                            <uc15:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
                       
                        </td>
                    </tr>
                </table>
                <!-- Start Product Info-->
                <!-- End Product Info -->
                <div style="width: 99%; overflow-x: auto;  height: 180px;  ">
                <table width="130%" border="0" id="DetailTable" 
                    align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td bgcolor="#E6E6E6" class="Blue" width="50" align="center">
                            <input type="checkbox" name="checkall" id="checkall" onclick="fnSelectAll()" title="全选"
                                style="cursor: hand" />
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" align="center">
                            序号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            物品编号<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            物品名称<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            规格
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            颜色
                        </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            <span id="SpProductCount">采购数量</span><span class="redbold" id="spCount">*</span>
                        </td>
                          <td align="center" bgcolor="#E6E6E6" class="ListTitle"  valign="middle" style="width:5%; display:none " id="spUsedUnitCount">
                            采购数量 <span class="redbold">*</span>
                        </td>
                       <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                           <span id="spUnitID">单位</span>  
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:4%; display:none " id="spUsedUnitID">
                           单位 
                        </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" id="spUnitPrice" >
                            单价<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:5%; display:none " id="spUsedPrice" >
                            单价<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            含税价<span class="redbold">*</span>
                        </td>
             
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            税率(%)
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
                            交货日期<span class="redbold">*</span>
                        </td>
                  <%--      <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            备注
                        </td>--%>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            源单编号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            源单序号
                        </td>
                         <td align="center" bgcolor="#E6E6E6" class="ListTitle" id="OrderCount" style="display:none">
                            已到货数量
                        </td>
                    </tr>
                </table>
                    <!-- Start 库存快照控件 -->
                <uc12:StorageSnapshot ID="StorageSnapshot1" runat="server" />
                <!-- End 库存快照控件 -->
                <br />
                <br />
                <br />
                </div>
                 
                <br />
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
                <input name='TempData' type='hidden' id='TempData' />
            </td>
        </tr>
    </table>
    <div>
    </div>
    </form>
    <span id="Forms" class="Spantype" name="Forms"></span>
</body>
</html>

<script language="javascript">
        var glb_BillTypeFlag = <%=XBase.Common.ConstUtil.CODING_RULE_PURCHASE %>;
        var glb_BillTypeCode = <%=XBase.Common.ConstUtil.BILL_TYPEFLAG_PURCHASE_ORDER %>;
        var glb_BillID = document.getElementById("ThisID").value;//单据ID
        var glb_IsComplete = true;
        var FlowJS_HiddenIdentityID ='ThisID';
        var FlowJs_BillNo ='PurOrderNo_txtCode';
        var FlowJS_BillStatus ='txtBillStatusID';
        eventObj.Table = 'DetailTable';
</script>





