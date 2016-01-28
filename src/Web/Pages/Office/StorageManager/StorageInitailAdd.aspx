<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInitailAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageInitailAdd" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc4" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc5" %>
<%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc6" %>
<%@ Register src="../../../UserControl/BatchRuleControl.ascx" tagname="BatchRuleControl" tagprefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>期初库存录入</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <div style="display: none">
        <asp:DropDownList runat="server" ID="ddlStorageInfo">
        </asp:DropDownList>
    </div>
    <div id="popupContent">
        
        <uc6:StorageSnapshot ID="StorageSnapshot1" runat="server" />
        
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
                            新建期初库存录入
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_Edit" style="display: none">
                    <tr>
                        <td height="30" align="center" class="Title">
                            期初库存录入
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_alarm" style="display: none">
                    <tr>
                        <td height="30" align="center">
                            <font color="red">月结之后不可对库存进行初始化</font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF" width="90%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save" runat="server"
                                            style="cursor: pointer" border="0" onclick="Fun_Save_StorageInitail();" visible="false" />
                                        <img src="../../../Images/Button/UnClick_bc.jpg" alt="保存" id="btn_UnClick_bc" runat="server"
                                            style="display: none; cursor: pointer;" border="0" visible="false" />
                                        <img src="../../../images/Button/Bottom_btn_Confirm.jpg" id="Confirm" alt="确认" runat="server"
                                            onclick="ConfirmBill()" style="display: none; cursor: pointer" border="0" visible="false" />
                                        <img id="btnPageFlowConfrim" src="../../../images/Button/UnClick_qr.jpg" runat="server"
                                            alt="确认" style="display: none;" border="0" visible="false" />
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
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" runat="server" id="btnBack"
                                            visible="false" onclick="DoBack();" style="cursor: hand" />
                                        <input type="hidden" id="txtIndentityID" name="txtIndentityID" value="0" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <!-- 参数设置：是否启用条码 -->
                                        <input type="hidden" id="hidCodeBar" runat="server" value="" />
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
                        id="Tb_01" style="display: block">
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
                                <input type="hidden" id="Hid_ifEdit" runat="server" />
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
                            </div>                             
                                </td>
                            <td bgcolor="#FFFFFF" width="24%">
                            
                                <div runat="server" id="divBatchNo">
                                <uc7:BatchRuleControl ID="BatchRuleControl1" runat="server" />
                                </div> 
                             <div runat="server" id="divBatchNoShow"></div>          
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                仓库<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                               <asp:DropDownList ID="sltStorageID" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                 入库部门
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="DeptName" id="DeptName" type="text" class="tdinput" size="19" readonly="readonly"
                                    onclick="alertdiv('DeptName,txtDeptID');" style="width: 95%" />
                                <input name="txtDeptID" id="txtDeptID" type="hidden" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                入库人<span class="redbold">*</span></td>
                            <td bgcolor="#FFFFFF">
                                <input name="UsertxtExecutor" id="UsertxtExecutor" type="text" class="tdinput" size="19"
                                    readonly="readonly" onclick="alertdiv('UsertxtExecutor,txtExecutorID');" style="width: 95%" />
                                <input name="txtExecutorID" id="txtExecutorID" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                入库时间<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF">
                                &nbsp;<asp:TextBox runat="server" name="txtEnterDate" ID="txtEnterDate" class="tdinput"
                                    ReadOnly="true" size="15" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDate')})"
                                    Width="95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                摘要
                            </td>
                            <td bgcolor="#FFFFFF">
                                <input name="txtSummary" id="txtSummary" class="tdinput" type="text" size="15" style="width: 95%" /></td>
                            <td align="right" bgcolor="#E6E6E6">
                            </td>
                            <td bgcolor="#FFFFFF">
                            </td>
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
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','Div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_02" style="display: block">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                入库数量合计
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTotalCount" id="txtTotalCount" value="0.00" type="text" class="tdinput"
                                    size="15" disabled="disabled" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                入库金额合计
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTotalPrice" id="txtTotalPrice" value="0.00" type="text" class="tdinput"
                                    size="15" disabled="disabled" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                            </td>
                            <td bgcolor="#FFFFFF" width="24%">
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
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_03" style="display: block">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                制单人
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <asp:TextBox ID="txtCreator" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                                <input id="HiddCreator" type="hidden" runat="server" />
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
                            <td bgcolor="#FFFFFF" width="24%">
                                <select id="sltBillStatus" name="sltBillStatus" disabled="disabled">
                                    <option value="1">制单</option>
                                    <option value="2">执行</option>
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
                                <input name="txtCloseDate" id="txtCloseDate" type="text" class="tdinput" size="15"
                                    disabled="disabled" />
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
                                <img src="../../../images/Button/Show_add.jpg" id="btnAdd" runat="server" style="cursor: pointer"
                                    onclick="ShowProdInfo();" visible="false" /><img src="../../../images/Button/Show_del.jpg" id="btnDele" runat="server" style="cursor: pointer"
                                    onclick="DeleteDetailRow();" visible="false" /><img 
                            id="btnSubSnapshot" alt="库存快照" onclick="ShowSnapshot();" 
                            src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" /><img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods" style="cursor: pointer"
                                    onclick="GetGoodsInfoByBarCode()" runat="server" visible="false" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" id="dg_Log" align="center" cellpadding="0" cellspacing="1"
                        bgcolor="#999999">
                        <tr>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 4%">
                                <input type="checkbox" visible="false" name="checkall" id="checkall" onclick="SelectAll()"
                                    value="checkbox" />
                            </td>
                            <td height="20" align="center" bgcolor="#E6E6E6" style="width: 6%">
                                序号
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 10%">
                                    物品编号
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 10%">
                                    物品名称
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 10%">
                                    规格
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                    <div id="divJiBendw" runat="server" >基本单位</div><div id="divDanWei" runat="server" style="display:none;">单位</div>
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                    <div id="divSpan" runat="server" >基本数量</div><div id="divRedSpan" runat="server" style="display:none;">入库数量<span class="redbold">*</span></div>
                                </td>
                                <td align="center" id="tdDanWei" runat="server" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%;">
                                   单位
                                </td>
                                <td align="center" id="tdShuLiang" runat="server" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                    数量<span class="redbold">*</span>
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 10%">
                                    入库单价
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 10%">
                                    入库金额
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 20%">
                                    备注
                                </td>
                                <td  style="display:none;">
                                    
                                </td>
                                <td  style="display:none;">
                                   
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
                <%--<input type="hidden" id="hidNoID" runat="server" />--%>
            </td>
        </tr>
    </table>
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />
    <uc3:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc4:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <input name='hidShowP' type='hidden' id='hidShowP' value="0" />
    <input name='hidZero' type='hidden' id='hidZero' runat="server" />
    </form>
</body>
</html>

<script src="../../../js/office/StorageManager/StorageInitailAdd.js" type="text/javascript"></script>

