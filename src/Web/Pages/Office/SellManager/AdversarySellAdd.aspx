<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdversarySellAdd.aspx.cs"
    Inherits="Pages_Office_SellManager_AdversarySellAdd" %>

<%@ Register Src="../../../UserControl/SelectAdversaryUC.ascx" TagName="SelectAdversaryUC"
    TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/SelectSellChance.ascx" TagName="SelectSellChance"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/sellModuleSelectCustUC.ascx" TagName="sellModuleSelectCustUC"
    TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/office/SellManager/AdversarySellAdd.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
</head>
<body>
    <form id="Form1" runat="server">
    <script type="text/javascript">
        var precisionLength=<%=SelPoint %>;//小数精度
    </script>
    <span id="Forms" class="Spantype"></span>
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
                            <div id="divTitle" runat="server">
                                新建销售竞争分析</div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                        <img runat="server" visible="false" src="../../../images/Button/Bottom_btn_save.jpg"
                                            alt="保存" id="btn_save" style="cursor: hand" onclick="InsertSellOfferData();" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" style="display: none;cursor: hand" alt="返回"
                                            id="ibtnBack"  onclick="fnBack();" />
                                        <uc1:Message ID="Message1" runat="server" />
                                        <asp:HiddenField ID="hiddAcction" runat="server" Value="insert" />
                                    </td>
                                    <td  align="right" bgcolor="#FFFFFF" style="padding-top: 5px; width: 70px;">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印"  onclick="fnPrintOrder()"  alt="打印" />
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
            </td>
        </tr>
        <tr>
            <td>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" >
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            对手编号<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="CustNo" class="tdinput" style="width: 98%" onclick="popSellAdObj.ShowList('')"
                                readonly="readonly" type="text" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            销售机会<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="ChanceID" class="tdinput" style="width: 98%" onclick="popSellSendObj.ShowList('all')"
                                readonly="readonly" type="text" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            竞争客户<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="CustID" class="tdinput" style="width: 98%" onclick="popSellCustObj.ShowList('protion')"
                                readonly="readonly" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            对手产品报价
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="Price" onchange="Number_round(this,<%=SelPoint %>)"  class="tdinput" style="width: 98%" maxlength="10" type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            &nbsp;
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            &nbsp;
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            &nbsp;
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            竞争产品/方案
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="Project" rows="3" cols="100" specialworkcheck="竞争产品/方案" style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            竞争能力
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="Power" rows="3" cols="100"  specialworkcheck="竞争能力"  style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            竞争优势
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="Advantage" rows="3" cols="100"  specialworkcheck="竞争优势"  style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            竞争劣势
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                           <textarea id="disadvantage" rows="3" cols="100"  specialworkcheck="竞争劣势"  style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            应对策略
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="Policy" rows="3" cols="100"  specialworkcheck="应对策略"  style="width: 95%"></textarea>
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
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Table1','searchClick2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    id="Table1" >
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%;">
                            制单人
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%;">
                            <asp:TextBox ID="Creator" Width="120px" runat="server" CssClass="tdinput" ReadOnly="True"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%;">
                            制单日期
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%;">
                            <asp:TextBox ID="CreateDate" runat="server" Width="120px" CssClass="tdinput" ReadOnly="True"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%;">
                            最后更新人
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="ModifiedUserID" runat="server" Width="120px" CssClass="tdinput"
                                ReadOnly="True" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            最后更新日期
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="ModifiedDate" runat="server" Width="120px" CssClass="tdinput" ReadOnly="True"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            &nbsp;
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%;">
                            备注
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" style="width: 89%;">
                           <textarea id="Remark" rows="3" cols="100" specialworkcheck="备注" 
                                style="width: 95%"></textarea>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hiddOrderID" value='0' />
    <input type="hidden" id="HiddenURLParams" runat="server" />
    <uc8:SelectAdversaryUC ID="SelectAdversaryUC1" runat="server" />
    <uc4:SelectSellChance ID="SelectSellChance1" runat="server" />
    <uc5:sellModuleSelectCustUC ID="sellModuleSelectCustUC1" runat="server" />
    </form>
</body>
</html>
