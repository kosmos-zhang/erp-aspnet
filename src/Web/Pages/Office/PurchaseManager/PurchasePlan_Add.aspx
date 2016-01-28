<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchasePlan_Add.aspx.cs"
    Inherits="Pages_Office_PurchaseManager_PurchasePlan_Add" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/PurchaseManager/PurchaseApplyUC.ascx" TagName="PurchaseApplyUC"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/Department.ascx" TagName="Department" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/ApplyReason.ascx" TagName="ApplyReason" TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc11" %>
<%@ Register Src="../../../UserControl/PurchaseManager/PurchasePlanUC.ascx" TagName="PurchasePlanUC"
    TagPrefix="uc13" %>
<%@ Register Src="../../../UserControl/ProductInfoUC.ascx" TagName="ProductInfoUC"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/PurchaseManager/PurchaseRequireUC.ascx" TagName="PurchaseRequireUC"
    TagPrefix="uc9" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc10" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc12" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc15" %> 
     <%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc18" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建采购计划</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
   <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>
    <script src="../../../js/office/PurchaseManager/PurchasePlanAdd.js" type="text/javascript"></script>
 
    <style type="text/css">
        </style>
</head>
<body>
    <form id="PurchasePlan_Add" runat="server">
    <input type="hidden" id="IsDisplayPrice" runat="server" value="" />
    <input id="HiddenPoint" type="hidden" runat="server" />
    <input id="HiddenMoreUnit" type="hidden" runat="server" />

    <uc9:PurchaseRequireUC ID="PurchaseRequireUC1" runat="server" />
    <uc10:Message ID="Message1" runat="server" />
    <uc12:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc6:PurchaseApplyUC ID="PurchaseApplyUC1" runat="server" />
    <uc13:PurchasePlanUC ID="PurchasePlanUC1" runat="server" />
    <uc8:ProviderInfo ID="ProviderInfo1" runat="server" />
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
            <td height="30" colspan="2" valign="top" >
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title" id="AddTitle">
                            新建采购计划单
                         
                            
                        </td>
                        <td height="30" align="center" class="Title" id="UpdateTitle" style="display: none">
                            采购计划单
                        </td>
                        <input type="hidden" id="HiddenAction" runat="server" value="Add" />
                        <input type="hidden" name='ThisID' id='ThisID' runat="server" value="0" />
                        <input type="hidden" id='BackFlagHidden' runat="server" />
                        <input type="hidden" id='HiddenURLParams' runat="server" />
                        <input type="hidden" id='HiddenURLParams2' runat="server" value="0"  />
                        <input type="hidden" id='FlowStatus' runat="server" value="0" />
                        <input type="hidden" id='IsCite' runat="server" value="False" />
                        <input type="hidden" id='HiddenTargetPage' runat="server" />
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
                                            onclick="SavePurPlan();" style="cursor: pointer" title="保存" />
                                        <img runat="server" visible="false" alt="保存" src="../../../Images/Button/UnClick_bc.jpg"
                                            id="imgUnSave" style="display: none;" /><span id="GlbFlowButtonSpan" runat="server"
                                                visible="false"></span>
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor: hand;
                                            display: none" onclick="DoBack();" />
                                        <!-- Start 审批 -->
                                        <!-- End 审批 -->
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印"  onclick="fnPrint();" />
                                        <uc11:FlowApply ID="FlowApply1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <!-- End 流程处理-->
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr height="20" bgcolor="#F4F0ED" class="Blue">
                        <td  >&nbsp;&nbsp;
                            基本信息
                        </td>
                        <td align="right">
                            <div id='searchClick1'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick1')" /></div>
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
                                <uc1:CodingRuleControl ID="PurPlanNo" runat="server" />
                            </div>
                            <%--<div id="divPurOrderNo" runat="server" class="tdinput" style="display: none">
                            </div>--%>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            主题 
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input name="txtTitle" id="txtTitle" class="tdinput" type="text" style="width: 90%" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购类别<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc3:CodeTypeDrpControl ID="PurchaseType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            计划员<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input name="UserPlanName" id="UserPlanName" onclick="alertdiv('UserPlanName,txtPlanID');"
                                class="tdinput" type="text" style="width: 90%" />
                            <input type="hidden" id="txtPlanID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购员<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input name="UserPurchaserName" id="UserPurchaserName" onclick="alertdiv('UserPurchaserName,txtPurchaserID');"
                                class="tdinput" type="text" style="width: 90%" />
                            <input type="hidden" id="txtPurchaserID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购部门<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input name="DeptName" id="DeptName" onclick="alertdiv('DeptName,txtDeptID');" class="tdinput"
                                type="text" style="width: 99%" />
                            <input type="hidden" id="txtDeptID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            源单类型
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="ddlFromType" class="tdinput" width="119px" id="ddlFromType" onchange="fnChangeFromType(this.value);">
                                <option value="0" selected="selected">无来源</option>
                                <option value="1">采购申请单</option>
                                <option value="2">采购需求</option>
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            计划日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input name="txtPlanDate" id="txtPlanDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtPlanDate')})"
                                class="tdinput" type="text" style="width: 90%"  readonly="readonly" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                        </td>
                    </tr>
                </table>
                <uc4:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
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
                            <input type="text" id="txtPlanCnt" name="txtPlanCnt" class="tdinput" value="0.00"   runat="server"
                                readonly disabled />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            金额合计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtPlanMoney" name="txtPlanMoney" maxlength="50" class="tdinput"
                                value="0.00" readonly disabled runat="server" />
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
                                disabled />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtCreateDate" name="txtCreatorID" class="tdinput" runat="server"
                                disabled />
                        </td>
                        <td class="tdColTitle" width="10%">
                            单据状态
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" class="tdinput"
                                runat="server" value="1" />
                            <input type="text" id="txtBillStatusName" name="txtBillStatusName" runat="server"
                                class="tdinput" value="制单" readonly disabled />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            确认人
                        </td>
                        <td class="tdColInput" width="23%">
                            <input type="hidden" id="txtConfirmorID" name="txtConfirmorID" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtConfirmorName" name="txtConfirmorName" class="tdinput"
                                disabled runat="server" readonly />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            确认日期
                        </td>
                        <td align="left" class="tdinput" width="23%">
                            <asp:TextBox ID="txtConfirmorDate" MaxLength="50" runat="server" CssClass="tdinput"
                                disabled Width="95%" Text=""></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            最后更新人
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtModifiedUserName" name="txtModifiedUserName" class="tdinput"
                                disabled runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            结单人
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="hidden" id="txtCloserID" name="txtCloserID" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtCloserName" name="txtCloserName" class="tdinput" runat="server"
                                disabled readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            结单日期
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtCloseDate" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                disabled Text=""></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            最后更新日期
                        </td>
                        <td align="left" class="tdinput" width="24%">
                            <input type="text" id="txtModifiedDate" name="txtModifiedDate" class="tdinput" runat="server"
                                disabled />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100">
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="tdinput" Width="99%"></asp:TextBox>
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
                            <span class="Blue">采购计划来源明细</span>
                            <uc5:ProductInfoUC ID="ProductInfoUC1" runat="server" />
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick3'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('divDetailTbl1','searchClick3')" /></div>
                        </td>
                        <td width="12">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF" valign="bottom">
                            <img runat="server" visible="false" src="../../../images/Button/Show_add.jpg" style="cursor: hand"
                                id="imgAdd" onclick="ShowProdInfo();" />
                            <img runat="server" visible="false" src="../../../images/Button/Show_del.jpg" style="cursor: hand"
                                id="imgDel" onclick="DelDtlSRow();" />
                                          <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();" alt="库存快照" />
                            <img runat="server" visible="false" src="../../../Images/Button/Bottom_btn_From.jpg"
                                style="cursor: hand; display: none;" id="imgGetDtl" onclick="GetSource();" />
                            <img runat="server" visible="false" alt="" src="../../../Images/Button/UnClick_tj.jpg"
                                style="display: none;" id="imgUnAdd" />
                            <img runat="server" visible="false" alt="" src="../../../Images/Button/UnClick_del.jpg"
                                style="display: none;" id="imgUnDel" />
                            <img runat="server" visible="false" alt="从源单选择明细" src="../../../Images/Button/Unclick_From.jpg"
                                id="imgUnGetDtl" />
                            <img alt="条码扫描" visible="false" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods"  style="cursor: pointer" onclick="GetGoodsInfoByBarCode()" runat="server"   />
                              <span id="spanExport" runat="server" visible="false" >
                             <img src="../../../images/Button/btn_drmx.jpg" style="cursor: pointer" id="imgExport" alt="导入明细"  onclick="ExportExcel.Show();" />
                              <img src="../../../images/Button/btn_drmxu.jpg" style="cursor: pointer; display:none;" id="imgUnExport" alt="导入明细"  />
                               
                              
                             </span>
                           <uc15:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
                            <input  type="button" onclick ="ssd()" style=" display:none "/>
                        </td>
                    </tr>
                </table>
                <div style="display: none">
                    <asp:DropDownList ID="ddlApplyReasonHidden" runat="server">
                    </asp:DropDownList>
                </div>
                <%-- <div id="divDetail" style="width: 99%; overflow-x: auto; overflow-y: auto; height: 80px;">--%>
                <div id="divDetailTbl1" runat="server">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    id="Tb_DtlS">
                    <tr>
                        <td align="center" bgcolor="#E6E6E6">
                            全选<input type="checkbox" visible="false" name="CheckAllDtlS" id="CheckAllDtlS" onclick="SelectAllDtlS()"
                                value="checkbox" />
                        </td>
                        <td align="center" bgcolor="#E6E6E6" align="center">
                            序号
                        </td>
                        <td align="center" bgcolor="#E6E6E6">
                            物品编号<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6">
                            物品名称
                        </td>
                        <td align="center" bgcolor="#E6E6E6" style="width:4%">
                            规格
                        </td>
                        <td align="center" bgcolor="#E6E6E6" style="width:4%">
                          颜色
                        </td>
                      <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:3%;  ">
                           <span id="spUnitID">单位</span>  
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:3%; display:none " id="spUsedUnitID">
                           单位 
                        </td> 
                           <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" id="spUnitPrice" >
                            含税价<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:7%; display:none " id="spUsedPrice" >
                            含税价<span class="redbold">*</span>
                        </td>
                        
                       
                   <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width:7%">
                            <span id="SpProductCount">计划数量</span><span class="redbold" id="spCount">*</span>
                        </td>
                          <td align="center" bgcolor="#E6E6E6" class="ListTitle"  valign="middle" style="width:7%; display:none " id="spUsedUnitCount">
                            计划数量 <span class="redbold">*</span>
                        </td>
                        
                     
                        <td align="center" bgcolor="#E6E6E6">
                            计划金额
                        </td>
                     
                        <td align="center" bgcolor="#E6E6E6">
                            计划交货日期<span class="redbold">*</span>
                        </td>
                        
                        <td align="center" bgcolor="#E6E6E6">
                            源单编号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" style ="width:4%">
                            源单序号
                        </td>
                        <td align="center" style="width:8%" bgcolor="#E6E6E6">
                            供应商<span class="redbold">*</span>
                        </td>
                    
                    </tr>
                </table>
                     <!-- Start 库存快照控件 -->
                <uc18:StorageSnapshot ID="StorageSnapshot1" runat="server" />
                <!-- End 库存快照控件 -->
                </div>
                <%--</div>--%>
                <table width="100%">
                    <tr>
                        <td height="25" valign="top">
                            <span class="Blue">采购计划明细</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='DtlDiv'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('divDetailTbl2','DtlDiv')" /></div>
                        </td>
                        <td width="5">
                        </td>
                    </tr>
                </table>
                <div id="divDetailTbl2" runat="server">
                <table width="99%" border="0" id="Tb_Dtl" align="center" cellpadding="0" cellspacing="1"
                    bgcolor="#999999">
                    <tr>
                        <td height="20" align="center" bgcolor="#E6E6E6" style="width: 30px">
                            序号
                        </td>
                        <td align="center" bgcolor="#E6E6E6">
                            物品编号
                        </td>
                        <td align="center" bgcolor="#E6E6E6">
                            物品名称
                        </td>
                        <td align="center" bgcolor="#E6E6E6">
                            规格
                        </td>
                           <td align="center" bgcolor="#E6E6E6">
                          颜色
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:7%;  ">
                               <span id="spUnitID2">单位</span> 
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:3%; display:none " id="spUsedUnitID2">
                           单位 
                        </td> 
                        <td align="center" bgcolor="#E6E6E6">
                            供应商
                        </td>
                         <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width:7%">
                            <span id="SpProductCount2">计划数量</span> 
                        </td>
                          <td align="center" bgcolor="#E6E6E6" class="ListTitle"  valign="middle" style="width:7%; display:none " id="spUsedUnitCount2">
                            计划数量 <span class="redbold">*</span>
                        </td> 
                        <td align="center" bgcolor="#E6E6E6">
                            计划交货日期
                        </td>
                           <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" id="spUnitPrice2" >
                            计划采购单价
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:7%; display:none " id="spUsedPrice2" >
                            计划采购单价
                        </td> 
                        <td align="center" bgcolor="#E6E6E6">
                            计划采购金额
                        </td>
                        <td align="center" bgcolor="#E6E6E6">
                            已订购数量
                        </td>
                    </tr>
                </table>
                </div>
                <br />
            </td>
        </tr>
    </table>
         <div id="divExcelRotoscoping" style="display:none">
<iframe id="divExcelIframe" frameborder="0" width="100%" ></iframe>
</div>
<div id="divExportExcel" style="border: solid 4px #999999; background: #fff; z-index: 1000; position: absolute;display:none;left: 60%;top:50%; width:30%">
        
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" >
        <tr>
        <td align="center" colspan="2" >
        <b>导入明细</b>

        </td>
        </tr>
        <tr>
        <td  align="center" colspan="2">
          <input type="file" id="fileExportExcel" style="width:70%" name="fileExportExcel" runat="server"/> <span style="padding-left:5px" ><a href="采购计划明细导入模板.rar">模板下载</a></span>
      
        </td>
        
        </tr>
        <tr style="height:10px">
        <td  align="center" colspan="2">
       
            <div id="divErrMsg" >
            </div>
       
        </td>
        </tr>
        <tr style="height:10px">
        <td height="10px">
        </td>
        <td align="center">
            <asp:ImageButton ID="imgExportExcelUC" runat="server" AlternateText="确认"  ImageUrl="../../../images/Button/Bottom_btn_confirm.jpg" OnClick="imgExportExcelUC_Click"/>
             <img alt="关闭" id="btn_Close" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
            onclick='ExportExcel.Hide();' />
        </td>
        </tr>
        </table>

</div>
<script type="text/javascript" language="javascript">
var ExportExcel=new Object();
ExportExcel.Show=function()
{   
    openRotoscopingDiv(false,"divExcelRotoscoping","divExcelIframe");
    document.getElementById("divExportExcel").style.display="";
    $("#divExportExcel").show();
    CenterToDocument("divExportExcel",true);
}
ExportExcel.Hide=function()
{
    $("#divExportExcel").hide();
    closeRotoscopingDiv(false,"divExcelRotoscoping");
}


</script>
    
    </form>
    <span id="Forms" class="Spantype"></span>
</body>
</html>

<script language="javascript">
        var glb_BillTypeFlag = <%=XBase.Common.ConstUtil.CODING_RULE_PURCHASE %>;
        var glb_BillTypeCode = <%=XBase.Common.ConstUtil.CODING_RULE_PURCHASE_PLAN %>;
        var glb_BillID = document.getElementById("ThisID").value;//单据ID
        var glb_IsComplete = true;
        var FlowJS_HiddenIdentityID ='ThisID';
        var FlowJs_BillNo ='PurPlanNo_txtCode';
        var FlowJS_BillStatus ='txtBillStatusID';
</script>

<script src="../../../js/common/Flow.js" type="text/javascript"></script>

