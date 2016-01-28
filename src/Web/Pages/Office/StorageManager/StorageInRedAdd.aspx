<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInRedAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageInRedAdd" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/StorageInType.ascx" TagName="StorageInType"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/Common/StorageSnapshot.ascx" TagName="StorageSnapshot"
    TagPrefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>红冲入库单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" rel="stylesheet" type="text/css" />
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

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <div id="popupContent">
    </div>
    <div style="display: none">
        <asp:DropDownList runat="server" ID="ddlStorageInfo">
        </asp:DropDownList>
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
                            新建红冲入库单
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_Edit" style="display: none">
                    <tr>
                        <td height="30" align="center" class="Title">
                            红冲入库单
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
                                            style="cursor: pointer" border="0" onclick="Fun_Save_StorageInRed();" visible="false" />
                                        <img src="../../../Images/Button/UnClick_bc.jpg" alt="保存" id="btn_UnClick_bc" runat="server"
                                            style="display: none; cursor: pointer;" border="0" visible="false" />
                                        <img src="../../../images/Button/Bottom_btn_Confirm.jpg" id="Confirm" runat="server"
                                            alt="确认" onclick="ConfirmBill()" style="display: none; cursor: pointer" border="0"
                                            visible="false" />
                                        <img id="btnPageFlowConfrim" runat="server" src="../../../images/Button/UnClick_qr.jpg"
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
                                            title="打印" style="cursor: pointer" onclick="BillPrint();" />
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
                                <input name="txtTitle" id="txtTitle" maxlength="100" specialworkcheck="入库单主题" type="text"
                                    class="tdinput" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                原始入库单<span class="redbold">*</span>
                                 </td>
                            <td bgcolor="#FFFFFF" width="24%">                                
                                
                                <input name="txtFromBillID" id="txtFromBillID" class="tdinput" type="text" size="15"
                                    title="" readonly="readonly" onclick="fnSelectInStorage()" style="width: 95%" /></td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                源单类型<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:DropDownList ID="ddlFromType" runat="server">
                                    <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="采购入库单"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="生产完工入库单"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="其他入库单"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                原始入库人
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="txtFromExecutor" id="txtFromExecutor" class="tdinput" type="text" size="15"
                                    title="" disabled="disabled" /></td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                原始入库时间
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="txtFromEnterDate" id="txtFromEnterDate" class="tdinput" type="text"
                                    size="15" disabled="disabled" /></td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                原始入库部门
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="txtDept" id="txtDept" class="tdinput" type="text" title="" size="15"
                                    disabled="disabled" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                原始摘要
                            </td>
                            <td bgcolor="#FFFFFF">
                                <input name="txtFromSummary" id="txtFromSummary" class="tdinput" type="text" size="15"
                                    disabled="disabled" style="width: 95%" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                入库人<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="UserExecutor" id="UserExecutor" type="text" class="tdinput" size="19"
                                    runat="server" readonly="readonly" onclick="alertdiv('UserExecutor,txtExecutorID');"
                                    style="width: 95%" />
                                <input name="txtExecutorID" id="txtExecutorID" type="hidden" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                入库时间<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:TextBox runat="server" name="txtEnterDate" ID="txtEnterDate" class="tdinput"
                                    type="text" size="15" ReadOnly="true" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDate')})"
                                    Style="width: 95%" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                入库原因
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:DropDownList runat="server" name="ddlReasonType" ID="ddlReasonType">
                                </asp:DropDownList>
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                摘要
                            </td>
                            <td bgcolor="#FFFFFF">
                                <input name="txtSummary" id="txtSummary" class="tdinput" type="text" size="15" style="width: 95%" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                可查看该入库信息的人员</td>
                            <td height="20" bgcolor="#FFFFFF" colspan="5">
                                <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
                        </tr>
                    </table>
                    <uc4:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
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
                                红冲数量合计
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTotalCount" id="txtTotalCount" type="text" class="tdinput" size="15"
                                    disabled="disabled" value="0.00" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                <div style="display: <%=GetIsDisplayPrice()%>">
                                    红冲金额合计</div>
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
                    <!-- Start ZheZhao -->
                    <span id="Forms" class="Spantype"></span>
                    <uc3:StorageInType ID="StorageInType1" runat="server" />
                    <!-- End ZheZhao -->
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
                                <input name="txtCloseDate" id="txtCloseDate" type="text" class="tdinput" size="15"
                                    disabled="disabled" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                最后更新人
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtModifiedUserID" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新时间
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtModifiedDate" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
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
                                &nbsp;<span class="Blue">入库单明细</span>
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
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                物品编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                物品名称
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                批次
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
                                原始入库量
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                <div id="divSpan" runat="server" >基本数量</div><div id="divRedSpan" runat="server" style="display:none;">红冲数量<span class="redbold">*</span></div>
                            </td>
                             <td align="center" bgcolor="#E6E6E6" class="ListTitle" id="tdShuLiang" runat="server">
                                红冲数量<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 6%; display: <%=GetIsDisplayPrice()%>">
                                红冲单价
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 6%; display: <%=GetIsDisplayPrice()%>">
                                红冲金额
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                源单编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
                                源单行号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" >
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
    <uc1:Message ID="Message1" runat="server" />
    <uc5:StorageSnapshot ID="StorageSnapshot1" runat="server" />

    <script type="text/javascript">
        var IsDisplayPrice = '<%=GetIsDisplayPrice() %>';
        var IsBarCode = '<%=GetIsBarCode()%>';
    </script>

    <script src="../../../js/office/StorageManager/StorageInRedAdd.js" type="text/javascript"></script>
<input name='hidZero' type='hidden' id='hidZero' runat="server" />
    </form>
</body>
</html>
