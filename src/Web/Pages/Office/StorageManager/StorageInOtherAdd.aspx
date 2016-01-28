<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInOtherAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageInOtherAdd" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/StorageSellBack.ascx" TagName="StorageSellBack"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/Common/StorageSnapshot.ascx" TagName="StorageSnapshot"
    TagPrefix="uc7" %>
<%@ Register src="../../../UserControl/BatchRuleControl.ascx" tagname="BatchRuleControl" tagprefix="uc8" %>
<%@ Register src="../../../UserControl/Common/ProjectSelectControl.ascx" tagname="ProjectSelectControl" tagprefix="uc9" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>其他入库单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
        .userListCss
        {
            position: absolute;
            top: 100px;
            left: 500px;
            border-width: 1pt;
            border-color: #EEEEEE;
            border-style: solid;
            width: 200px;
            display: none;
            z-index: 100;
        }
    </style>
</head>
<body>
    <form id="EquipAddForm" runat="server">
    <div id="popupContent">
    </div>
    <div style="display: none">
        <asp:DropDownList runat="server" ID="ddlStorageInfo">
        </asp:DropDownList>
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />
    <uc3:StorageSellBack ID="StorageSellBack1" runat="server" />
    <uc4:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <uc7:StorageSnapshot ID="StorageSnapshot1" runat="server" />
    <div style="margin-left: 80px">
        <uc6:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    </div>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_Add" style="display: none">
                    <tr>
                        <td height="30" align="center" class="Title">
                            新建其他入库单
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_Edit" style="display: none">
                    <tr>
                        <td height="30" align="center" class="Title">
                            其他入库单
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save" runat="server"
                                            style="cursor: hand" border="0" onclick="Fun_Save_StorageInOther();" visible="false" />
                                        <img src="../../../Images/Button/UnClick_bc.jpg" alt="保存" id="btn_UnClick_bc" runat="server"
                                            style="display: none; cursor: pointer" border="0" visible="false" />
                                        <img src="../../../images/Button/Bottom_btn_Confirm.jpg" id="Confirm" runat="server"
                                            alt="确认" onclick="ConfirmBill()" style="display: none; cursor: pointer" visible="false" />
                                        <img id="btnPageFlowConfrim" src="../../../images/Button/UnClick_qr.jpg" alt="确认"
                                            style="display: none" visible="false" />
                                        <img src="../../../images/Button/Main_btn_Invoice.jpg" id="btn_Close" runat="server"
                                            alt="结单" onclick="CloseBill()" style="display: none; cursor: pointer" border="0"
                                            visible="false" />
                                        <img id="btn_Unclic_Close" runat="server" src="../../../images/Button/Button_jd.jpg"
                                            alt="结单" style="display: none;" border="0" visible="false" />
                                        <img src="../../../images/Button/Main_btn_qxjd.jpg" id="btn_CancelClose" runat="server"
                                            alt="取消结单" onclick="CancelCloseBill()" style="display: none; cursor: pointer"
                                            border="0" visible="false" />
                                        <img id="btn_Unclick_CancelClose" runat="server" src="../../../images/Button/Button_qxjd.jpg"
                                            alt="取消结单" style="display: none;" border="0" visible="false" />
                                        <input type="hidden" id="txtIndentityID" name="txtIndentityID" value="0" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" runat="server" id="btnBack"
                                            visible="false" onclick="DoBack();" style="cursor: hand" />
                                             <input type="hidden" id="hidMoreUnit" runat="server" value="" />                                         
                                         <input type="hidden" id="hidSelPoint" runat="server" value="" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="right">
                                        <img src="../../../images/Button/Main_btn_print.jpg" id="btnPrint" runat="server"
                                            style="cursor: pointer" onclick="BillPrint();" />
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
                <div style="overflow-y: auto;">
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            基础信息
                                        </td>
                                        <td align="right">
                                            <div id='div11'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','div11')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="Tb_01" style="display: ">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                入库单编号<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <div runat="server" id="div_InNo_uc">
                                    <uc2:CodingRuleControl ID="txtInNo" runat="server" />
                                </div>
                                <div id="div_InNo_Lable" runat="server" style="display: none;">
                                    <asp:Label runat="server" ID="lbInNo" Text=""></asp:Label>
                                </div>
                                <%--<input name="txtInNo" id="txtInNo" type="text" class="tdinput" size="15" onblur="checkonly();" />--%>
                                <input type="hidden" id="txtInNoHidden" name="txtInNoHidden" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                入库单主题
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTitle" id="txtTitle" maxlength="100" type="text" specialworkcheck="入库单主题"
                                    class="tdinput" size="15" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                <div runat="server" id="divBatchTitle">
                            批次<span class="redbold">*</span>
                            </div></td>
                            <td bgcolor="#FFFFFF">
                                <div runat="server" id="divBatchNo">
                                <uc8:BatchRuleControl ID="BatchRuleControl1" runat="server" />
                                </div> 
                             <div runat="server" id="divBatchNoShow"></div> </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                源单编号<span id="spanhide" style="display: none" class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="txtFromBillID" id="txtFromBillID" class="tdinput" type="text" size="15"
                                    title="" readonly="readonly" onclick="fnSelectSBInfo()" style="width: 95%" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                源单类型<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                               
                                <asp:DropDownList ID="ddlFromType" runat="server">
                                    <asp:ListItem Value="0" Text="无来源"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="销售退货单"></asp:ListItem>
                                </asp:DropDownList>
                               
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                往来单位
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                 <input name="txtOtherCorp" id="txtOtherCorp" class="tdinput" type="text" size="15"
                                    readonly="readonly" style="width: 95%" />
                                <input name="txtOtherCorpID" id="txtOtherCorpID" type="hidden" size="15" /></td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                往来单位类型
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                
                                <select name="sltCorpBigType" id="sltCorpBigType" class="tdinput" width="119px" id="Select1"
                                    disabled="disabled">
                                    <option value="">无来源</option>
                                    <option value="1">客户</option>
                                    <option value="2">供应商</option>
                                    <option value="3">竞争对手</option>
                                    <option value="4">银行</option>
                                    <option value="5">外协加工厂</option>
                                    <option value="6">运输商</option>
                                    <option value="7">其他</option>
                                </select></td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                交货人
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="UserTaker" id="UserTaker" type="text" class="tdinput" size="19" readonly="readonly"
                                    onclick="alertdiv('UserTaker,txtTakerID');" style="width: 95%" />
                                <input name="txtTakerID" id="txtTakerID" type="hidden" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                验收人
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="UserChecker" id="UserChecker" type="text" class="tdinput" size="19"
                                    readonly="readonly" onclick="alertdiv('UserChecker,txtCheckerID');" style="width: 95%" />
                                <input name="txtCheckerID" id="txtCheckerID" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                入库人<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="UserExecutor" id="UserExecutor" type="text" class="tdinput" size="19"
                                    runat="server" readonly="readonly" onclick="alertdiv('UserExecutor,txtExecutorID');"
                                    style="width: 95%" />
                                <input name="txtExecutorID" id="txtExecutorID" type="hidden" runat="server" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                入库时间<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:TextBox runat="server" name="txtEnterDate" ID="txtEnterDate" class="tdinput"
                                    size="15" ReadOnly="true" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDate')})"
                                    Width="95%" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                入库部门
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="DeptName" id="DeptName" type="text" class="tdinput" size="19" readonly="readonly"
                                    onclick="alertdiv('DeptName,txtDeptID');" style="width: 95%" />
                                <input name="txtDeptID" id="txtDeptID" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                入库原因
                            </td>
                            <td bgcolor="#FFFFFF">
                                &nbsp;<asp:DropDownList runat="server" name="ddlReasonType" ID="ddlReasonType" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                摘要
                            </td>
                            <td bgcolor="#FFFFFF">
                                <input name="txtSummary" id="txtSummary" class="tdinput" type="text" size="15" style="width: 95%" /></td>
                            <td align="right" bgcolor="#E6E6E6">
                                所属项目</td>
                            <td bgcolor="#FFFFFF">
                                <uc9:ProjectSelectControl ID="ProjectSelectControl1" runat="server"  />
                                <input id="txtSelProject" class="tdinput"  onclick="ShowProjectInfo('txtSelProject','HidProjectID');"
                                    readonly="readonly" size="19" type="text" style="width: 95%" />
                                <input type="hidden" id="HidProjectID" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                               可查看该入库信息的人员</td>
                            <td bgcolor="#FFFFFF" colspan="5">
                                <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
                        </tr>
                    </table>
                    <uc5:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
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
                                            <div id='Div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_02" style="display: ">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                入库数量合计
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTotalCount" id="txtTotalCount" type="text" class="tdinput" size="15"
                                    disabled="disabled" value="0.00" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                <div style="display: <%=GetIsDisplayPrice()%>">
                                    入库金额合计
                                </div>
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTotalPrice" id="txtTotalPrice" type="text" class="tdinput" size="15"
                                    disabled="disabled" value="0.00" style="width: 95%; display: <%=GetIsDisplayPrice()%>" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                            </td>
                            <td bgcolor="#FFFFFF" width="24%">
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
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_03" style="display: ">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                制单人
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <asp:TextBox ID="txtCreator" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                                <%--<input name="txtCreator" id="txtCreator" type="text" class="tdinput" size="15" />--%>
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                制单日期
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <asp:TextBox ID="txtCreateDate" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" width="10%">
                                单据状态
                            </td>
                            <td bgcolor="#FFFFFF">
                                <%--<asp:TextBox ID="txtBillStatus" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>--%>
                                <select name="sltBillStatus" class="tdinput" width="119px" id="sltBillStatus" disabled="disabled">
                                    <option value="1">制单</option>
                                    <option value="2">执行</option>
                                    <option value="3">变更</option>
                                    <option value="4">手工结单</option>
                                    <option value="5">自动结单</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                确认人
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtConfirmor" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                确认日期
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtConfirmDate" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结单人
                            </td>
                            <td bgcolor="#FFFFFF">
                                <%--<input name="txtCloser" id="txtCloser" type="text" class="tdinput" size="15" />--%>
                                <asp:TextBox ID="txtCloser" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                结单日期
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtCloseDate" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                最后更新人
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtModifiedUserID" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                                <%--<input name="txtModifiedUserID" id="txtModifiedUserID" type="text" class="tdinput"
                                size="15" />--%>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新时间
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtModifiedDate" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                                <%--<input id="txtModifiedDate" ; class="tdinput" name="txtModifiedDate" 
                                size="15" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                备注
                            </td>
                            <td height="20" colspan="5" bgcolor="#FFFFFF">
                                <textarea name="txtRemark" id="txtRemark" class="tdinput" cols="50" rows="5"></textarea>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td valign="top">
                                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                            </td>
                            <td align="right" valign="top">
                                <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" valign="top">
                                &nbsp;<span class="Blue"> 入库单明细</span>
                            </td>
                            <td align="right" valign="top">
                                <div id='searchClick4'>
                                    <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('dg_Log','searchClick4')" /></div>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">
                                <img id="btn_AddRow" runat="server" src="../../../images/Button/Show_add.jpg" style="cursor: pointer"
                                    visible="false" />&nbsp;
                                <img src="../../../images/Button/Show_del.jpg" id="btnDele" runat="server" style="cursor: hand"
                                    onclick="DeleteSignRow();" visible="false" />
                                <img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods" style="cursor: pointer;
                                    display: <%=GetIsDisplayPrice()%>" onclick="GetGoodsInfoByBarCode()" runat="server"
                                    visible="false" />
                                <img src="../../../images/Button/btn_kckz.jpg" style="cursor: pointer" onclick="ShowSnapshot();"
                                    alt="库存快照" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" id="dg_Log" style="behavior: url(../../../draggrid.htc)"
                        align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 4%">
                                <input type="checkbox" visible="false" name="checkall" id="checkall" onclick="SelectAll()"
                                    value="checkbox" />
                            </td>
                            <td height="20" align="center" bgcolor="#E6E6E6" >
                                序号
                            </td>
                            <%--<td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 4%">
                                查看
                            </td>--%>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                物品编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                物品名称
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                颜色
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                规格
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                <div id="divJiBendw" runat="server" >基本单位</div><div id="divDanWei" runat="server" style="display:none;">单位</div>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" id="tdDanWei" runat="server">
                                单位
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                仓库<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                源单数量
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                已入库数量
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                未入库数量
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                <div id="divSpan" runat="server" >基本数量</div><div id="divRedSpan" runat="server" style="display:none;">入库数量<span class="redbold">*</span></div>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" id="tdShuLiang" runat="server">
                                入库数量<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 6%; display: <%=GetIsDisplayPrice()%>">
                                单价<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 6%; display: <%=GetIsDisplayPrice()%>">
                                金额
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                源单编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                源单行号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                备注
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="2" bgcolor="#999999">
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
            </td>
        </tr>
    </table>
    <div id="zhezhaoTree" style="filter: Alpha(opacity=0); width: 204px; height: 500px;
        z-index: 0; position: absolute; display: none; top: 100px; left: 500px; background-color: Red">
        <iframe style="border: 0; width: 204px; height: 100%; position: absolute; background-color: Blue">
        </iframe>
    </div>
    <div id="divTree" style="overflow-x: hidden; overflow-y: auto; width: 200px; height: 500px;
        z-index: 100; position: absolute; display: none; top: 100px; left: 500px;">
        <table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" bgcolor="#999999">
            <tr>
                <td height="20" bgcolor="#E6E6E6" class="Blue">
                    <table width="100%" border="0" cellspacing="0" cellpadding="">
                        <tr>
                            <td width="45%">
                                往来单位选择
                            </td>
                            <td align="right">
                                <img src="../../../Images/Pic/Close.gif" title="关闭" style="cursor: pointer" onclick="cleareCompany()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#F4F0ED" height="200" valign="top">
                    <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        var IsDisplayPrice = '<%=GetIsDisplayPrice() %>';
        var IsBarCode = '<%=GetIsBarCode()%>';
    </script>

    <script src="../../../js/office/StorageManager/StorageInOtherAdd.js" type="text/javascript"></script>
<input name='hidShowP' type='hidden' id='hidShowP' value="0" />
<input name='hidZero' type='hidden' id='hidZero' runat="server" />
    </form>
</body>
</html>
