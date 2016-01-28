<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubStorageInit.aspx.cs" Inherits="Pages_Office_SubStoreManager_SubStorageInit" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/BatchRuleControl.ascx" TagName="BatchRuleControl"
    TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>分店期初库存录入</title>
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

    <script src="../../../js/office/SubStoreManager/SubStorageInit.js" type="text/javascript"></script>

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
<body>
    <form id="Form1" runat="server">
    <input type="hidden" id="txtIsMoreUnit" runat="server" />
    <input type="hidden" id="hidSelPoint" runat="server" />
    <uc1:Message ID="Message1" runat="server" />
    <uc3:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc4:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <table width="95%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" />
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
                                分店期初库存录入</div>
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
                                            id="imgSave" style="cursor: pointer" onclick="InsertSubStorageIn();" visible="false"
                                            runat="server" />
                                        <img alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="imgUnSave" style="display: none;
                                            height: 25px" /><span id="Forms" class="Spantype"></span><span id="AlertPop" style="display: none"></span>
                                        <img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确认" id="btn_confirm"
                                            style="cursor: hand; display: none" border="0" onclick="Fun_ConfirmOperate();"
                                            visible="false" runat="server" />
                                        <img src="../../../Images/Button/UnClick_qr.jpg" alt="确认" id="btn_Unconfirm" style="cursor: hand;
                                            display: inline" border="0" runat="server" visible="false" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor: hand;
                                            display: none;" onclick="Back();" />
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印" onclick="PrintInit();" />
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
                            入库单编号<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <div id="divInputNo" runat="server">
                                <uc2:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <div id="divSubStorageInNo" runat="server" class="tdinput" style="display: none">
                            </div>
                        </td>
                        <td align="right" class="style2" width="10%">
                            入库单主题
                        </td>
                        <td class="style3" width="23%">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="tdinput" Width="95%" SpecialWorkCheck="入库单主题"></asp:TextBox>
                        </td>
                        <td align="right" class="style2" width="10%">
                            分店名称<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="24%">
                            <asp:TextBox ID="txtDeptName" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                Enabled="False" ReadOnly="True"></asp:TextBox>
                            <input type="hidden" id="HidDeptID" runat="server" />
                            <input type="hidden" id="HidDeptID2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style2" width="10%">
                            批次<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <div runat="server" id="divBatchNo">
                                <uc6:BatchRuleControl ID="BatchRuleControl1" runat="server" />
                            </div>
                            <div runat="server" id="divBatchNoShow">
                            </div>
                        </td>
                        <td align="right" class="style2" width="10%">
                        </td>
                        <td class="style3" width="23%">
                        </td>
                        <td align="right" class="style2" width="10%">
                        </td>
                        <td class="style3" width="24%">
                        </td>
                    </tr>
                </table>
                <uc5:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
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
                            最后更新人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="hidden" id="Hidden4" name="txtConfirmor" value="0" class="tdinput" runat="server"
                                readonly />
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
                            最后更新时间
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" runat="server" CssClass="tdinput" disabled Width="95%"
                                Text="" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="txtModifiedDate2" name="txtModifiedDate2" class="tdinput"
                                runat="server" readonly />
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
                            <input type="hidden" id="hidBillStatusName" name="hidBillStatusName" runat="server" />
                        </td>
                    </tr>
                </table>
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
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
                            &nbsp; <span class="Blue">产品信息</span>
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
                                runat="server" visible="false" />
                            <img alt="" src="../../../Images/Button/UnClick_tj.jpg" style="display: none;" id="imgUnAdd"
                                runat="server" visible="false" />
                            <img src="../../../images/Button/Show_del.jpg" style="cursor: hand" id="imgDel" onclick="DeleteSignRowSubStorageIn();"
                                runat="server" visible="false" />
                            <img alt="" src="../../../Images/Button/UnClick_del.jpg" style="display: none;" id="imgUnDel"
                                runat="server" visible="false" />
                            <img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" visible="false" id="btnGetGoods"
                                style="cursor: pointer;" onclick="GetGoodsInfoByBarCode()" runat="server" />
                            <img alt="条码扫描" visible="false" runat="server" style="display: none" src="../../../Images/Button/btn_tmsmu.jpg"
                                id="unbtnGetGoods" />
                        </td>
                    </tr>
                </table>
                <!-- Start Product Info-->
                <!-- End Product Info -->
                <input type="hidden" id="DetailCount" runat="server" value="1" />
                <table width="99%" border="0" id="dg_Log" align="center" cellpadding="0" cellspacing="1"
                    bgcolor="#999999">
                    <tr>
                        <td bgcolor="#E6E6E6" class="Blue" align="center">
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
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicUnitTd">
                            基本单位
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicNumTd">
                            基本数量
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            单位
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            <span class="redbold">*</span>入库数量
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
