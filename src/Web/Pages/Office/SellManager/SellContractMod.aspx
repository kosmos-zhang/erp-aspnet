<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellContractMod.aspx.cs"
    Inherits="Pages_Office_SellManager_SellContractMod" %>

<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc9" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/SelectSellOfferUC.ascx" TagName="SelectSellOfferUC"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/sellModuleSelectCustUC.ascx" TagName="sellModuleSelectCustUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/SellModuleSelectCurrency.ascx" TagName="SellModuleSelectCurrency"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc8" %>   
 <%@ Register src="../../../UserControl/SelectSellChance.ascx" tagname="SelectSellChance" tagprefix="uc10" %>
<%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc11" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc12" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
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
    <script src="../../../js/common/DeleteFile.js" type="text/javascript"></script>

    <script src="../../../js/office/SellManager/SellContractMod.js" type="text/javascript"></script>
    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>
</head>
<body onload="formatNumLen()">
    <form id="EquipAddForm" runat="server">
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc8:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <uc11:StorageSnapshot ID="StorageSnapshot1" runat="server" />
    <uc1:SelectSellOfferUC ID="SelectSellOfferUC1" runat="server" />
    <uc4:sellModuleSelectCustUC ID="sellModuleSelectCustUC1" runat="server" />
    <uc5:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <input type='hidden' id="rowIndex" />
    <input type="hidden" id="hiddDeptID" />
    <input id="hiddSeller" type="hidden" />
    <input id="hiddOurDelegate" type="hidden" />
    <asp:HiddenField ID="hfAttachment" runat="server" />
    <asp:HiddenField ID="hfPageAttachment" runat="server" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <uc9:FlowApply ID="FlowApply1" runat="server" />
    <input type="hidden" id='hiddBillStatus' value='1' />
    <input type="hidden" id="hiddOrderID" value='0' />
    <input type="hidden" id="HiddenURLParams" runat="server" />
    <uc10:SelectSellChance ID="SelectSellChance1" runat="server" />
    <input type="hidden" id="hiddState" value="1" />
    <input type ="hidden" id="txtIsMoreUnit" runat="server" /><!--是否启用多单位-->

    <script type="text/javascript">
    var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.CODING_RULE_SELL %>;
    var glb_BillTypeCode = <%=XBase.Common.ConstUtil.CODING_RULE_SELLCONTRANCT_NO %>;
    var glb_BillID = 0;                                //单据ID
    var glb_IsComplete = true;                                          //是否需要结单和取消结单(小写字母)
    var FlowJS_HiddenIdentityID ='hiddOrderID';                      //自增长后的隐藏域ID
    var FlowJs_BillNo ='OfferNo';          //当前单据编码名称
    var FlowJS_FromSell = 'hiddState';
    var FlowJS_BillStatus ='hiddBillStatus';                             //单据状态ID
    eventObj.Table = 'dg_Log';//键盘事件用到
    
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
        $("#TotalPrice").val(lengthstr);
        $("#TotalTax").val(lengthstr);
        $("#TotalFee").val(lengthstr);
        $("#DiscountTotal").val(lengthstr);
        $("#RealTotal").val(lengthstr);
        $("#CountTotal").val(lengthstr);
        $("#Discount").val(lengthstr2);
    }
    
    </script>
    <uc7:Message ID="Message1" runat="server" />
    <uc6:SellModuleSelectCurrency ID="SellModuleSelectCurrency1" runat="server" />
    
    <script src="../../../js/common/Flow.js" type="text/javascript"></script>
    
    <div>
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
                                销售合同
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
                                    alt="保存" id="btn_save" style="cursor: hand" border="0" onclick="InsertSellOfferData();" />
                                <img  runat="server" visible="false"   alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="imgUnSave" style="display: none;" />
                                <span runat="server" visible="false"   id="GlbFlowButtonSpan"></span>
                                 <img runat="server" visible="false"  alt="终止合同" id="btn_zzht" src="../../../Images/Button/btn_zzht.jpg" onclick="fnEndOrder();" style="cursor: hand" />
                                <img runat="server" visible="false"  id="btn_Unzzht"
                                    alt="终止合同" src="../../../Images/Button/btn_uzzht.jpg" style="display: none" />
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
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="6">
                            </td>
                        </tr>
                    </table>
                    <table border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        style="height: 27px; width: 99%">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            基本信息                                         </td>
                                        <td align="right">
                                            <div id='Div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('TableHT','Div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="TableHT">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%;">
                                单据编号<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 23%;">
                                <input type="text" class="tdinput" readonly="readonly" style="width: 95%;" id="OfferNo" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%;">
                                主题
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 23%;">
                                <input name="Title" id="Title" specialworkcheck="主题" type="text" style="width: 95%;"
                                    class="tdinput" maxlength="50" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%;">
                                源单类型<span class="redbold">*</span>&nbsp;
                            </td>
                            <td bgcolor="#FFFFFF">
                                <select name="FromType" style="width: 120px;margin-top:2px;margin-left:2px;" id="FromType" onchange="fnFromTypeChange(this)">
                                    <option value="0" selected="selected">无来源</option>
                                     <option value="2">销售机会</option>
                                    <option value="1">销售报价单</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                源单编号
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="FromBillID" onclick="fnSelectOfferInfo()" class="tdinput" type="text"
                                    readonly="readonly" style="width: 95%;" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                客户名称<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="CustID"  class="tdinput" type="text" readonly="readonly" style="width: 95%;"
                                    onclick="fnSelectCustInfo()" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                客户电话
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="CustTel" class="tdinput" specialworkcheck="客户电话" type="text" style="width: 95%;"
                                    maxlength="25" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                业务类型<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <select style="width: 120px;margin-top:2px;margin-left:2px;" id="BusiType">
                                    <option value="1">普通销售</option>
                                    <option value="2">委托代销</option>
                                    <option value="3">直运</option>
                                    <option value="4">零售</option>
                                    <option value="5">销售调拨</option>
                                </select>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                销售类别
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="sellType" runat="server" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结算方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="PayType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                支付方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="MoneyType" runat="server" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                交货方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="TakeType" runat="server" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                运送方式
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <uc2:CodeTypeDrpControl ID="CarryType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                币种<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="CurrencyType" class="tdinput" type="text" onclick="fnSelectCurrency()"
                                    readonly="readonly" style="width: 95%;" maxlength="100" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                汇率
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="Rate" style="width: 95%;" disabled="disabled" class="tdinput" type="text"
                                    readonly="readonly" value="0.0000" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                业务员<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="UserSeller" class="tdinput" onclick="fnSelectSeller()" style="width: 95%;"
                                    readonly="readonly" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                部门<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="DeptId" class="tdinput" onclick="fnSelectDept()" readonly="readonly" style="width: 95%;"
                                    type="text" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                客户方代表
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="TheyDelegate" onclick="" class="tdinput" type="text" style="width: 95%;"
                                    maxlength="25" specialworkcheck="客户方代表" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                我方代表
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="UserOurSignman" readonly="readonly" style="width: 95%;" onclick=" alertdiv('UserOurSignman,hiddOurDelegate');"
                                    class="tdinput" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                签约时间<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="SignDate" runat="server" Width="90%" class="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OfferDate')})"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                签约地点
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="SignAddr" specialworkcheck="签约地点" class="tdinput" type="text" style="width: 95%;"
                                    maxlength="50" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                开始日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input style="width: 95%;" readonly="readonly" id="StartDate" class="tdinput" type="text"
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('StartDate')})" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                截止日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="EndDate" style="width: 95%;" readonly="readonly" class="tdinput" type="text"
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndDate')})" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                洽谈进展
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="TalkProcess" specialworkcheck="洽谈进展" class="tdinput" type="text" style="width: 90%;"
                                    maxlength="100" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                合同状态<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input type="text" id="State" style="width: 95%" class="tdinput" value="执行中" disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                终止原因
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="EndNote" class="tdinput" maxlength="100" specialworkcheck="终止原因" style="width: 98%"
                                    type="text" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                是否增值税<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="isAddTax" checked="checked" onclick="fnAddTax()" type="radio" value="1"
                                    name="tax" />是
                                <input id="NotAddTax" onclick="fnAddTax()" type="radio" value="0" name="tax" />否
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                是否被引用
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input id="isRef" class="tdinput" disabled="disabled" type="text" value="未被引用" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                可查看人员
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" colspan="5">
                                <asp:TextBox ID="UserCanViewUserName" style="width: 98%" ReadOnly="true" CssClass="tdinput" runat="server" onclick="alertdiv('UserCanViewUserName,CanViewUser,2');">
                                </asp:TextBox>
                                <input type="hidden" id="CanViewUser" runat="server" />
                            </td> 
                        </tr>
                    </table>
                    <uc12:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
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
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                金额合计
                            </td>
                            <td height="20" bgcolor="#FFFFFF" style="width: 22%;">
                                <input id="TotalPrice" disabled="disabled" style="width: 95%;" class="tdinput" type="text"
                                    value="0.00" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                税额合计
                            </td>
                            <td height="20" bgcolor="#FFFFFF" style="width: 22%;">
                                <input id="TotalTax" disabled="disabled" style="width: 95%;" class="tdinput" type="text"
                                    value="0.00" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                含税金额合计
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="TotalFee" disabled="disabled" style="width: 95%;" class="tdinput" type="text"
                                    value="0.00" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                整单折扣(%)<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="Discount" onchange="Number_round(this,<%=SelPoint %>)"  value="100.00" onblur="fnCheckPer(this)" style="width: 95%;"
                                    class="tdinput" type="text" maxlength="7" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                折后含税金额
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="RealTotal" disabled="disabled" style="width: 95%;" class="tdinput" type="text"
                                    value="0.00" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                折扣金额合计
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="DiscountTotal" disabled="disabled" style="width: 95%;" class="tdinput"
                                    value="0.00" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                合同数量合计
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="CountTotal" disabled="disabled" style="width: 95%;" class="tdinput" type="text"
                                    value="0.00" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
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
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                制单人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                                <asp:TextBox ID="Creator" Width="95%" runat="server" CssClass="tdinput" Enabled="false"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                制单日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="CreateDate" runat="server" Width="95%" CssClass="tdinput" Enabled="false"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                确认人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="Confirmor" runat="server" Width="95%" CssClass="tdinput" Enabled="false"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                确认日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ConfirmDate" runat="server" Width="95%" CssClass="tdinput" Enabled="false"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结单人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="Closer" runat="server" Width="95%" Enabled="false" ReadOnly="True"
                                    CssClass="tdinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结单日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="CloseDate" runat="server" Width="95%" CssClass="tdinput" Enabled="false"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ModifiedUserID" runat="server" Width="95%" CssClass="tdinput" Enabled="false"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ModifiedDate" runat="server" Width="95%" CssClass="tdinput" Enabled="false"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                附件
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%;">
                                  <div id="divUploadAttachment" runat="server">
                                        <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                                    </div>
                                    <div id="divDealAttachment" runat="server" style="display:none;">
                                        <a href="#" onclick="DealAttachment('download');">
                                            <span id='spanAttachmentName' runat="server"></span>
                                        </a>&nbsp;
                                        <a href="#" onclick="DealAttachment('clear');">删除附件</a>
                                    </div>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%;">
                                备注
                            </td>
                            <td height="20" align="left" colspan="3" bgcolor="#FFFFFF" style="width: 56%;">
                                <textarea id="Remark" specialworkcheck="备注" style="width: 95%" cols="100" rows="3"></textarea>
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
                                            <span class="Blue">合同明细</span>
                                        </td>
                                        <td align="right" valign="top">
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('Table2','searchClick3')" /></div>
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
                                            <img  runat="server" visible="false"  src="../../../images/Button/Show_add.jpg" id="imgAdd" style="cursor: hand" onclick="AddSignRow();" />
                                            <img  runat="server" visible="false" src="../../../images/Button/Show_del.jpg" id="imgDel" style="cursor: hand" onclick="fnDelOneRow()" />
                                            <img runat="server" visible="false"  alt="" src="../../../Images/Button/UnClick_tj.jpg" style="display: none;" id="imgUnAdd" />
                                            <img runat="server" visible="false"  alt="" src="../../../Images/Button/UnClick_del.jpg" style="display: none;" id="imgUnDel" />
                                            <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();" alt="库存快照" />
                                            <img  runat="server"  visible="false"  src="../../../Images/Button/btn_tmsm.jpg" alt="条码扫描" style="cursor: hand;" id="GetGoods" onclick="GetGoodsInfoByBarCode()"/>
                                           <img alt="条码扫描"  runat="server"  visible="false"  src="../../../Images/Button/btn_tmsmu.jpg"  id="UnGetGoods"  style=" display:none"/>
                                            <uc2:CodeTypeDrpControl ID="PackageUC" runat="server" />
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
                                                物品编号<span class="redbold">*</span>
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
                                                数量<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                                交货期限(天) <span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 6%" class="ListTitle">
                                                包装要求
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                                单价<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                                含税价<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                折扣(%)<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 4%" class="ListTitle">
                                                税率(%)<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 8%" class="ListTitle">
                                                含税金额
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 8%" class="ListTitle">
                                                金额
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 5%" class="ListTitle">
                                                税额
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                备注
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div style=" height:20px; width:200%"></div>
                                </div>
                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="2" bgcolor="#999999">
                                        </td>
                                    </tr>
                                </table>
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
