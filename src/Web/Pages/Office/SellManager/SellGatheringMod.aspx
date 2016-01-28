<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellGatheringMod.aspx.cs"
    Inherits="Pages_Office_SellManager_SellGatheringMod" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/SellModuleSelectCurrency.ascx" TagName="SellModuleSelectCurrency"
    TagPrefix="uc13" %>
<%@ Register Src="../../../UserControl/SelectSellSendUC.ascx" TagName="SelectSellSendUC"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/Department.ascx" TagName="Department" TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/sellModuleSelectCustUC.ascx" TagName="sellModuleSelectCustUC"
    TagPrefix="uc4" %>

<%@ Register Src="../../../UserControl/SelectSellOrderUC.ascx" TagName="SelectSellOrderUC"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc14" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>回款计划</title>
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

    <script src="../../../js/office/SellManager/SellGatheringMod.js" type="text/javascript">        function FromType_onclick() {

        }

    </script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc7:Message ID="Message1" runat="server" />
    <uc13:SellModuleSelectCurrency ID="SellModuleSelectCurrency1" runat="server" />
    <uc1:SelectSellSendUC ID="SelectSellSendUC1" runat="server" />
    <uc2:Department ID="Department1" runat="server" />
    <uc4:sellModuleSelectCustUC ID="sellModuleSelectCustUC1" runat="server" />
    <uc6:SelectSellOrderUC ID="SelectSellOrderUC1" runat="server" />
    <uc1:SelectSellSendUC ID="SelectSellSendUC2" runat="server" />
    <input type="hidden" id="hiddenEquipCode" value="" />
    <input type="hidden" id="HiddenURLParams" runat="server" />
    <input type="hidden" id="hiddDeptID" value='' />
    <input type='hidden' id="rowIndex" />
    <input id="hiddSeller" type="hidden" />
    <input type="hidden" id="hiddOrderID" />
    <script type="text/javascript">
        var precisionLength=<%=SelPoint %>;//小数精度
    </script>
    <div >
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
                               回款计划
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
                                    alt="保存" id="btn_save" style="cursor: hand" onclick="InsertSellGatheringData();" />
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
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%">
                                回款计划编号<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 23%">
                                <input type="text" class="tdinput" disabled="disabled" style="width: 95%" id="OfferNo" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%">
                                主题
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 23%">
                                <input id="Title" type="text" style="width: 95%" specialworkcheck="主题" class="tdinput"
                                    maxlength="50" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%">
                                源单类型<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 24%">
                                <select style="width: 120px;margin-top:2px;margin-left:2px;" id="FromType" onchange="fnFromBill(this)">
                                    <option value="0" selected="selected">无来源</option>
                                    <option value="1">销售订单</option>
                                    <option value="2">销售发货单</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                源单编号
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="FromBillID" onclick="fnSelectOfferInfo()" class="tdinput" type="text"
                                    readonly="readonly" style="width: 95%" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                客户名称<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="CustID" class="tdinput" type="text" readonly="readonly" style="width: 95%"
                                    onclick="fnSelectCustInfo()" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                客户类型
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="CustType" class="tdinput" type="text" disabled="disabled" style="width: 95%" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                客户电话
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="CustTel" class="tdinput" specialworkcheck="客户电话" disabled="disabled" type="text"
                                    style="width: 95%" maxlength="100" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                币种<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="CurrencyType" class="tdinput" type="text" onclick="fnSelectCurrency()"
                                    readonly="readonly" style="width: 95%" maxlength="100" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                业务员<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="UserSeller" class="tdinput" onclick="fnSelectSeller()" readonly="readonly"
                                    style="width: 95%" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                部门<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="DeptId" class="tdinput" onclick="fnSelectDept()" readonly="readonly" style="width: 95%"
                                    type="text" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                计划回款金额<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="PlanPrice" onchange="Number_round(this,<%=SelPoint %>)"  class="tdinput" value="" type="text" style="width: 95%"
                                    maxlength="13" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                计划回款时间<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="PlanGatherDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('PlanGatherDate')})"
                                    style="width: 95%" class="tdinput" type="text" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                实际回款金额</td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="FactPrice" onchange="Number_round(this,<%=SelPoint %>)"  value="" class="tdinput" type="text" style="width: 95%"
                                    maxlength="13" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                实际回款时间
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="FactGatherDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('FactGatherDate')})"
                                    style="width: 95%" class="tdinput" type="text" readonly="readonly" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                回款相关单号
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="LinkBillNo" class="tdinput" specialworkcheck="回款相关单号" type="text" style="width: 95%"
                                    maxlength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                期次
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="GatheringTime" class="tdinput" type="text" style="width: 95%" value='1'
                                    maxlength="2" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                状态<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <select  style="width: 120px;margin-top:2px;margin-left:2px;" id="State">
                                    <option value="1" selected="selected">已回款</option>
                                    <option value="2">未回款</option>
                                    <option value="3">部分回款</option>
                                </select>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                &nbsp;
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                &nbsp;
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
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%">
                                制单人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%">
                                <asp:TextBox ID="Creator" Width="120px" runat="server" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%">
                                制单日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%">
                                <asp:TextBox ID="CreateDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%">
                                最后更新人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%">
                                <asp:TextBox ID="ModifiedUserID" runat="server" Width="120px" CssClass="tdinput"
                                    Enabled="false" Height="21px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ModifiedDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                备注
                            </td>
                            <td height="20" colspan="3" align="left" bgcolor="#FFFFFF">
                                <input id="Remark" class="tdinput" specialworkcheck="备注" type="text" style="width: 99%"
                                    maxlength="400" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="2" bgcolor="#999999">
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    
    </form>
</body>
</html>
