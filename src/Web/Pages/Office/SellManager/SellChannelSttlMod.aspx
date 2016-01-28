<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellChannelSttlMod.aspx.cs"
    Inherits="Pages_Office_SellManager_SellChannelSttlMod" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc9" %>
<%@ Register Src="../../../UserControl/SelectSellSendUC.ascx" TagName="SelectSellSendUC"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/SelectSellSendDetailUC.ascx" TagName="SelectSellSendDetailUC"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc14" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>委托代销结算单</title>
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

    <script src="../../../js/common/Common.js" type="text/javascript"> </script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/office/SellManager/SellChannelSttlMod.js" type="text/javascript"></script>
    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

</head>
<body onload="formatNumLen()">
    <form id="EquipAddForm" runat="server">
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc7:Message ID="Message1" runat="server" />
    <input type="hidden" id="hiddenEquipCode" value="" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id='hiddBillStatus' value='1' />
    <asp:HiddenField ID="hiddSeller" runat="server" />
    <input type="hidden" id="hiddDeptID" />
    <input type="hidden" id="hiddOrderID" value='0' />
    <input type='hidden' id="rowIndex" />
    <input type="hidden" id="HiddenURLParams" runat="server" />
    <uc9:FlowApply ID="FlowApply1" runat="server" />
    <uc1:SelectSellSendUC ID="SelectSellSendUC1" runat="server" />
    <uc6:SelectSellSendDetailUC ID="SelectSellSendDetailUC1" runat="server" />
    <input type ="hidden" id="txtIsMoreUnit" runat="server" /><!--是否启用多单位-->

    <script type="text/javascript">
    var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.CODING_RULE_SELL %>;
    var glb_BillTypeCode = <%=XBase.Common.ConstUtil.CODING_RULE_SELLCHANNELSTTL_NO %>;
    var glb_BillID = 0;                                //单据ID
    var glb_IsComplete = true;                                          //是否需要结单和取消结单(小写字母)
    var FlowJS_HiddenIdentityID ='hiddOrderID';                      //自增长后的隐藏域ID
    var FlowJs_BillNo ='OfferNo';          //当前单据编码名称
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
        $("#TotalFee").val(lengthstr);
        $("#PushMoney").val(lengthstr);
        $("#HandFeeTotal").val(lengthstr);
        $("#SttlTotal").val(lengthstr);
        $("#huikuan").val(lengthstr);
        $("#CountTotal").val(lengthstr);
        $("#PushMoneyPercent").val(lengthstr2);
    }
    </script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>
    <table style="width: 95%;" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
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
                            委托代销结算单
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                        <table width="100%">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                        <img  runat="server" visible="false"  src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save" style="cursor: hand;"
                                onclick="InsertSellOfferData();" />
                            <img  runat="server" visible="false"  alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="imgUnSave" style="display: none;" />
                            <span  runat="server" visible="false"  id="GlbFlowButtonSpan"></span>
                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="ibtnBack" style="cursor: hand"
                                onclick="fnBack();" />
                                        
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
                            <input type="text" class="tdinput" disabled="disabled" style="
                                width: 95%;" id="OfferNo" />
                        </td>
                        <td align="right" bgcolor="#E6E6E6" style="width: 10%;">
                            主题
                        </td>
                        <td bgcolor="#FFFFFF" style="width: 23%;">
                            <input id="Title" specialworkcheck="主题" type="text" style="width: 95%;" class="tdinput"
                                maxlength="50" />
                        </td>
                        <td align="right" bgcolor="#E6E6E6" style="width: 10%;">
                            源单类型
                        </td>
                        <td bgcolor="#FFFFFF">
                            发货通知单
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            源单编号<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <input id="FromBillID" class="tdinput" type="text" readonly="readonly" style="width: 95%;"
                                onclick="popSellSendObj.ShowList('2','protion')" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            客户名称
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <input id="CustID" class="tdinput" type="text" readonly="readonly" style="width: 95%;" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            客户电话
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <input id="CustTel"  readonly="readonly" class="tdinput" type="text" style="width: 95%;"
                                maxlength="25" />
                        </td>
                    </tr>
                    <tr>
                       <td height="20" align="right" bgcolor="#E6E6E6">
                            销售部门
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <input id="DeptId" readonly="readonly"  onclick="alertdiv('DeptId,hiddDeptID');" style="width: 95%;" class="tdinput" type="text" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            业务员
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="UserSeller" Width="95%" ReadOnly="true" onclick="alertdiv('UserSeller,hiddSeller');" class="tdinput" runat="server"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            结算方式
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <uc2:CodeTypeDrpControl ID="PayTypeUC" runat="server" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" >
                        支付方式
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" >
                                <uc2:CodeTypeDrpControl ID="MoneyTypeUC" runat="server" />
                                
                                
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" >
                            币种
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" >
                            <input id="CurrencyType" class="tdinput" type="text" readonly="readonly" style="width: 95%;"
                                maxlength="100" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" >
                            汇率
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" >
                            <input id="Rate" style="width: 95%;" class="tdinput" type="text" disabled="disabled"
                                value="" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" >
                            结算日期<span class="redbold">*</span>
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" >
                            <asp:TextBox ID="SttlDate" runat="server" Width="95%" CssClass="tdinput" ReadOnly="True"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SttlDate')})"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" >
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" >
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" >
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" >
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
                                        结算信息
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
                    id="Tb_02" >
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            代销数量合计
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <input id="CountTotal" value="" disabled="disabled" style="width: 95%;" class="tdinput"
                                type="text" size="15" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            代销金额合计
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <input id="TotalFee" value="" disabled="disabled" style="width: 95%;" class="tdinput"
                                type="text" size="15" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            代销提成率(%)<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <input id="PushMoneyPercent" onchange="Number_round(this,<%=SelPoint %>)"  value="100.00" onblur="fnCheckPer(this)" style="width: 95%;"
                                class="tdinput" type="text" maxlength="7" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                            代销提成额
                        </td>
                        <td height="20" bgcolor="#FFFFFF" style="width: 22%;">
                            <input id="PushMoney" value="" disabled="disabled" style="width: 95%;" class="tdinput"
                                type="text" size="15" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                            手续费合计
                        </td>
                        <td height="20" bgcolor="#FFFFFF" style="width: 22%;">
                            <input id="HandFeeTotal" onchange="Number_round(this,<%=SelPoint %>)"  onblur="fnCheck(this,'手续费合计')" style="width: 95%;" class="tdinput"
                                type="text" maxlength="8" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                            应结算金额合计
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <input id="SttlTotal" disabled="disabled" style="width: 95%;" value="" class="tdinput"
                                type="text" size="15" />
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
                                        相关单据状态
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
                    id="Tb_05" >
                    <tr>
                        <td height="28" align="right" bgcolor="#E6E6E6" style="width: 11%">
                            建单情况
                        </td>
                        <td height="28" align="left" bgcolor="#FFFFFF" style="width: 22%">
                            <input id="isOpenbillText" readonly="readonly" style="width: 95%;" class="tdinput"
                                type="text" size="15" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%">
                            结算状态
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%">
                            <input id="IsAccText" readonly="readonly" style="width: 95%;" class="tdinput" type="text"
                                size="15" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%">
                            已结算金额
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <input id="huikuan" disabled="disabled" style="width: 95%;" class="tdinput" value="0.00"
                                type="text" />
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
                    id="Tb_03" >
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                            单据状态
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                            <label id="BillStatus">
                                制单</label>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            制单人
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="Creator" Width="120px" runat="server" CssClass="tdinput" Enabled="false"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            制单日期
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="CreateDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            确认人
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="Confirmor" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            确认日期
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="ConfirmDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            结单人
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="Closer" runat="server" Width="120px" Enabled="false" CssClass="tdinput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                            结单日期
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                            <asp:TextBox ID="CloseDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                            最后更新人
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                            <asp:TextBox ID="ModifiedUserID" runat="server" Width="120px" CssClass="tdinput"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                            最后更新日期
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="ModifiedDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                            备注
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" style="width: 89%;">
                            <input id="Remark" class="tdinput" specialworkcheck="备注" maxlength="100" style="width: 95%"
                                type="text" />
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
                                        <span class="Blue">代销结算单明细</span>
                                    </td>
                                    <td align="right" valign="top">
                                        <div id='searchClick3'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Table2','searchClick3')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                    id="Table2" >
                    <tr>
                        <td>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" colspan="11" style="padding-top: 5px; padding-left: 5px;">
                                        <img  runat="server" visible="false"  src="../../../Images/Button/Bottom_btn_From.jpg" id="btnFromBill" alt="选择明细"
                                            style="cursor: hand;" onclick="fnSelectOrderList()" />
                                        <img runat="server" visible="false"  src="../../../Images/Button/Unclick_From.jpg" id="btnUnFromBill" alt="选择明细"
                                            style="display: none;" />
                                        <img  runat="server" visible="false" src="../../../images/Button/Show_del.jpg" id="imgDel" style="cursor: hand" onclick="fnDelOneRow()"
                                            alt="删除明细" />
                                        <img  runat="server" visible="false" src="../../../Images/Button/UnClick_del.jpg" style="display: none;" id="imgUnDel"
                                            alt="删除明细" />
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
                                    <td align="center" bgcolor="#E6E6E6" style="width: 8%" class="ListTitle">
                                        物品编号
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 8%" class="ListTitle">
                                        物品名称
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        规格
                                    </td>
                                    <td align="center" id="BaseUnitD" runat="server" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        基本单位
                                    </td>
                                    <td align="center" id="BaseCountD" runat="server" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        基本数量
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        单位
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        代销数量
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        已结算数量
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        未结算数量
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        本次结算数量<span class="redbold">*</span>
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 6%" class="ListTitle">
                                        代销单价
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 7%" class="ListTitle">
                                        本次结算代销金额
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                        备注
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        源单类型
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 7%" class="ListTitle">
                                        源单ID
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                        源单行号
                                    </td>
                                </tr>
                            </table>
                            </div>
                            <div style="height: 20px; width: 100%">
                                        </div>
                                    </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
