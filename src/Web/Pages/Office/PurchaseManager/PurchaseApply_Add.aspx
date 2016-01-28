<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseApply_Add.aspx.cs"
    Inherits="Pages_Office_PurchaseManager_PurchaseApply_Add" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/Common/StorageSnapshot.ascx" TagName="StorageSnapshot"
    TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/BillChoose.ascx" TagName="BillChoose" TagPrefix="uc9" %>
<%@ Register Src="../../../UserControl/MaterialChoose.ascx" TagName="MaterialChoose"
    TagPrefix="uc10" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购申请(edit by ellen)</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <br />
    <form id="Form1" runat="server">
        <input type="hidden" id="IsDisplayPrice" runat="server" value="" />
    <input id="HiddenPoint" type="hidden" runat="server" />
        <input type="hidden" id="HiddenURLParams" runat="server" />
    <input id="HiddenMoreUnit" type="hidden" runat="server" />
      <input type="hidden" id='IsCite' runat="server" value="False" />
     <div style="display: none">
                                <asp:DropDownList ID="ddlApplyReason" runat="server">
                                </asp:DropDownList>
                            </div>
    <input type="hidden" id="IsExportExcel" runat="server" />
    <!-- Start 消息提示 -->
    <uc1:Message ID="Message1" runat="server" />
    <uc5:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <!-- End 消息提示 -->
    <!-- Start -->
    <uc9:BillChoose ID="BillChoose1" runat="server" />
    <!--End -->
    <!-- Start -->
    <uc10:MaterialChoose ID="MaterialChoose1" runat="server" />
    <!--End -->
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
                            <% if (this.intApplyID > 0)
                               { %>采购申请
                            <%}
                               else
                               { %>
                            新建采购申请
                            <%} %>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td width="50">
                                        <img id="imgSave" src="../../../images/Button/Bottom_btn_save.jpg" onclick="Fun_Save_Apply();"
                                            style="cursor: pointer" title="保存采购申请" visible="false" runat="server" />
                                        <img id="imgUnSave" src="../../../images/Button/UnClick_bc.jpg" style="display: none;
                                            cursor: pointer" title="保存采购申请" visible="false" runat="server" />
                                    </td>
                                    <td align="left">
                                        <span id="GlbFlowButtonSpan" visible="false" runat="server"></span>
                                        <img src="../../../images/Button/Bottom_btn_back.jpg" border="0" runat="server" alt="返回"
                                            id="btnBack" onclick="DoBack();"   style="cursor: hand; display :none "  />
                                    </td>
                                    <td align="right" width="60">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印" onclick="PrintBill();" />
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- Start 流程处理-->
                            <uc3:FlowApply ID="FlowApply1" runat="server" />
                            <!-- End 流程处理-->
                            <!-- End 单据状态值 -->
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <!--返回相关：列表中的搜索条件-->
                            <input type="hidden" id="hidSearchCondition" runat="server" />
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
                            <div id="divCodeRule" runat="server">
                                <uc4:CodingRuleControl ID="codruleApply" runat="server" />
                            </div>
                            <div id="divApplyNo" runat="server" class="tdinput">
                            </div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            主题
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtTitle" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                specialworkcheck="主题"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购类别
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc2:CodeTypeDrpControl ID="ddlTypeID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            申请人<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="UserApplyUserName" MaxLength="50" onclick="alertdiv('UserApplyUserName,txtApplyUserID');"
                                runat="server" CssClass="tdinput" Width="95%" ReadOnly="true"></asp:TextBox>
                            <input type="hidden" id="txtApplyUserID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            申请部门<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="DeptName" MaxLength="50" runat="server" onclick="alertdiv('DeptName,txtDeptID');"
                                CssClass="tdinput" Width="95%" ReadOnly="true"></asp:TextBox>
                            <input type="hidden" id="txtDeptID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            源单类型<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="ddlFromType" class="tdinput" style="width: 119px" id="ddlFromType"
                                onchange="Fun_ChangeSourceBill(this.value);">
                                <option value="0" selected="selected">无来源</option>
                                <option value="1">销售订单</option>
                                <option value="2">物料需求计划</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            申请日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtApplyDate" MaxLength="50" runat="server" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtApplyDate')})"
                                Width="95%" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            到货地址
                        </td>
                        <td height="20" class="tdColInput" colspan="3" width="23%">
                            <asp:TextBox ID="txtAddress" MaxLength="50" runat="server" CssClass="tdinput" specialworkcheck="到货地址"
                                Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <!-- Start 扩展属性 -->
                <uc6:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                <!-- End 扩展属性 -->
                &nbsp;<table width="99%" border="0" align="center" cellpadding="2" cellspacing="1"
                    bgcolor="#999999">
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
                            <input type="text" id="txtCountTotal" name="txtCountTotal" class="tdinput" value="0.00"  runat="server"    readonly ="readonly"/>
                       
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                        </td>
                        <td height="20" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="24%">
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
                            单据状态
                        </td>
                        <td class="tdColInput" width="23%">
                            <input type="hidden" id="txtBillStatus" name="txtBillStatus" class="tdinput" value="1" />
                            <input type="text" id="txtBillStatusReal" name="txtBillStatusReal" class="tdinput"
                                value="制单" readonly disabled />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="hidden" id="txtCreator" name="txtCreator" class="tdinput" runat="server"
                                readonly />
                            <input type="text" id="txtCreatorReal" name="txtCreatorReal" class="tdinput" runat="server"
                                disabled readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单日期
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtCreateDate" MaxLength="50" runat="server" CssClass="tdinput"
                                disabled Width="95%" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            确认人
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="hidden" id="txtConfirmor" name="txtConfirmor" value="0" class="tdinput"
                                runat="server" readonly />
                            <input type="text" id="txtConfirmorReal" name="txtConfirmorReal" class="tdinput"
                                disabled runat="server" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td height="20" align="left" class="tdinput">
                            <asp:TextBox ID="txtConfirmDate" MaxLength="50" runat="server" CssClass="tdinput"
                                disabled Width="95%" Text=""></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            结单人
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txtCloser" name="txtCloser" value="0" class="tdinput" runat="server"
                                readonly />
                            <input type="text" id="txtCloserReal" name="txtCloserReal" class="tdinput" runat="server"
                                disabled readonly />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            结单日期
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txtCloseDate" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                disabled Text=""></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            最后更新人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            最后更新日期
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100">
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                            <textarea id="txtRemark" name="txtRemark" rows="3" style="width: 100%; height: 62px;"
                                class="tdinput"></textarea>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="Tb_04" valign="top">
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
                        <td valign="top" height="20">
                            &nbsp; <span class="Blue">采购申请明细来源</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick3'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('divDetailTbl','searchClick3')" /></div>
                        </td>
                        <td width="10">
                        </td>
                    </tr>
                </table>
                <!-- Start 物品控件 -->
                
                <!-- End 物品控件 -->
                <!-- Start 条码扫描 -->
                <uc7:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
                <!-- End 条码扫描 -->
                <!-- Start 库存快照 -->
                <uc8:StorageSnapshot ID="StorageSnapshot1" runat="server" />
                <!-- End 库存快照 -->
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td bgcolor="#FFFFFF" valign="bottom">
                            <img id="imgAdd" src="../../../images/Button/Show_add.jpg" style="cursor: hand" onclick="ShowProdInfo();"
                                visible="false" runat="server" />
                            <img id="imgDel" src="../../../images/Button/Show_del.jpg" style="cursor: hand" onclick="DeleteSignRow();"
                                visible="false" runat="server" />
                                 <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();" alt="库存快照" />
                            <img id="imgGetDtl" src="../../../Images/Button/Bottom_btn_From.jpg" style="cursor: hand; display: none" onclick="ChooseSourceBill();"  visible="false"   runat="server" />
                            <img id="imgUnGetDtl" src="../../../Images/Button/Unclick_From.jpg" style="cursor: hand;"  visible="false" runat="server" />
                            <img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods" visible="false"  style="cursor: pointer;" onclick="GetGoodsInfoByBarCode()" runat="server" />
                            <span id="spanExport" runat="server">
                                <img src="../../../images/Button/btn_drmx.jpg" style="cursor: pointer" id="imgExport"
                                    alt="导入明细" onclick="Show();" />
                                <img src="../../../images/Button/btn_drmxu.jpg" style="cursor: pointer; display: none;"
                                    id="imgUnExport" alt="导入明细" />
                            </span>
                             <input  type="button" value="dianou" onclick ="sadfdsf()"   style="display :none "  />
                             
                        </td>
                    </tr>
                </table>
                <div id="divDetailTbl" runat="server">
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
                                物品编号<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                物品名称<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                规格 
                            </td> 
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                颜色
                            </td> 
                               <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                           <span id="spUnitID">单位</span>  <span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:3%; display:none " id="spUsedUnitID">
                           单位 
                        </td> 
                               <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width:3%">
                            <span id="SpProductCount">需求数量</span><span class="redbold" id="spCount">*</span>
                        </td>
                          <td align="center" bgcolor="#E6E6E6" class="ListTitle"  valign="middle" style="width:7%; display:none " id="spUsedUnitCount">
                            需求数量 <span class="redbold">*</span>
                        </td>
                        
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                需求日期<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                申请原因
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                源单编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                源单序号
                            </td>
                        </tr>
                    </table>
                  
                </div>
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" runat="server" />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="2" bgcolor="#999999">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="Tb_05" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="10">
                        </td>
                    </tr>
                </table>
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
                        <td valign="top" height="20">
                            &nbsp; <span class="Blue">采购申请明细</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick4'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('divDetailTb2','searchClick4')" /></div>
                        </td>
                        <td width="10">
                        </td>
                    </tr>
                </table>
                <div id="divDetailTb2" runat="server">
                    <table width="99%" border="0" id="dg_LogSecond" align="center" cellpadding="0" cellspacing="1"
                        bgcolor="#999999">
                        <tr>
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
                               <span id="spUnitID2">单位</span> 
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width:3%; display:none " id="spUsedUnitID2">
                           单位 
                        </td> 
                           <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width:3%">
                            <span id="SpProductCount2">申请数量</span> 
                        </td>
                          <td align="center" bgcolor="#E6E6E6" class="ListTitle"  valign="middle" style="width:7%; display:none " id="spUsedUnitCount2">
                            申请数量 <span class="redbold">*</span>
                        </td>
                        
                            
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                需求日期
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                已计划数量
                            </td>
                        </tr>
                    </table>
                </div>
                <input name='txtTRLastIndexSecond' type='hidden' id='txtTRLastIndexSecond' value="1"
                    runat="server" />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!-- Start 导入明细 -->
    <div id="divExcelRotoscoping" style="display: none">
        <iframe id="divExcelIframe" frameborder="0" width="100%"></iframe>
    </div>
    <div id="divExportExcel" style="border: solid 4px #999999; background: #fff; z-index: 1000;
        position: absolute; display: none; left: 60%; top: 50%; width: 30%">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
            <tr style="height: 5px">
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <b>导入明细</b>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <input type="file" id="fileExportExcel" style="width: 70%" name="fileExportExcel"
                        runat="server" />
                    <span style="padding-left: 5px"><a href="采购申请明细导入模板.rar">模板下载</a></span>
                </td>
            </tr>
            <tr style="height: 10px">
                <td align="center" colspan="2">
                    <div id="divErrMsg">
                    </div>
                </td>
            </tr>
            <tr style="height: 10px">
                <td height="10px">
                </td>
                <td align="center">
                    <asp:ImageButton ID="imgExportExcelUC" runat="server" AlternateText="确认" ImageUrl="../../../images/Button/Bottom_btn_confirm.jpg"
                        OnClick="imgExportExcelUC_Click" />
                    <img alt="关闭" id="btn_Close" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
                        onclick='Hide();' />
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" language="javascript">
        function Show() {
            if (document.readyState != "complete") return;
            openRotoscopingDiv(false, "divExcelRotoscoping", "divExcelIframe");
            document.getElementById("divExportExcel").style.display = "";
            $("#divExportExcel").show();
            CenterToDocument("divExportExcel", true);
        }

        function Hide() {
            if (document.readyState != "complete") return;
            $("#divExportExcel").hide();
            closeRotoscopingDiv(false, "divExcelRotoscoping");
        }
    </script>

    <!-- End 导入明细 -->
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
var intApplyID = <%=intApplyID %>;
var glb_BillTypeFlag =6;
var glb_BillTypeCode = 1;
var glb_BillID = intApplyID;//单据ID
var glb_IsComplete = true;
var FlowJS_HiddenIdentityID ='txtIndentityID';
var FlowJs_BillNo ='codruleApply_txtCode';
var FlowJS_BillStatus ='txtBillStatus';
</script>

<script src="../../../js/office/PurchaseManager/PurchaseApply_Add.js" type="text/javascript"></script>

<script src="../../../js/common/Flow.js" type="text/javascript"></script>

