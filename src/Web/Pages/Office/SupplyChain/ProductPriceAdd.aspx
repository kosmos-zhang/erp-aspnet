<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPriceAdd.aspx.cs"
    Inherits="Pages_Office_SupplyChain_ProductPriceAdd" %>

<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物品售价变更档案</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <style type="text/css">
        #Select1
        {
            width: 88px;
        }
        #TextArea1
        {
            width: 853px;
        }
        #txt_Remark
        {
            width: 595px;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <span id="Span1" class="Spantype"></span>
    <!-- 小数位数 -->
    <input type="hidden" id="hidPoint" value="<%=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).SelPoint.ToString() %>" />
    <br />
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="txtIndentityID" value="0" runat="server" />
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
                            <% if (this.intOtherCorpInfoID > 0)
                               { %>物品售价变更单
                            <%}
                               else
                               { %>
                            新建物品售价变更单
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
                                    <td>
                                        <img id="btnAdd" src="../../../images/Button/Bottom_btn_save.jpg" onclick="Fun_Save_ProductPriceInfo();"
                                            style="cursor: pointer; float: left" title="保存物品信息" runat="server" visible="false" />
                                        <img alt="确认" id="btn_AD" onclick="ChangeStatus();" runat="server" src="../../../Images/Button/Bottom_btn_confirm.jpg"
                                            style="width: 51px; height: 25px; display: none; float: left" visible="false" />
                                        <img alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="btnsave" style="width: 51px;
                                            height: 27px; float: left; display: none" runat="server" visible="false" /><img alt="确认"
                                                id="btnsure" runat="server" src="../../../Images/Button/UnClick_qr.jpg" style="float: left;
                                                display: block" visible="false" />
                                        <a onclick="DoBack();">
                                            <img src="../../../images/Button/Bottom_btn_back.jpg" border="0" style="float: left;"
                                                id="btnback" runat="server" /></a>
                                    </td>
                                    <td align="right">
                                        <img alt="打印" style="float: right;" src="../../../Images/Button/Main_btn_print.jpg"
                                            onclick="PrintProductPrice()" />
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
                                        <input id="txtPlanNoHidden" type="hidden" />
                                        <input id="txt_TypeCode" type="hidden" />
                                    </td>
                                    <td align="right">
                                        <div id='searchClick2'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','searchClick2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_02" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="13%">
                            变更单编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="20%">
                            <asp:TextBox ID="txt_ChangeNo" specialworkcheck="变更单编号" runat="server" class="tdinput"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="13%">
                            变更单主题<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="20%">
                            <asp:TextBox ID="txt_Title" specialworkcheck="变更单主题" runat="server" class="tdinput"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="12%">
                            物品名称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_ProductName" name="txtConfirmorReal1" class="tdinput"
                                runat="server" width="95%" onclick="popTechObj.ShowList()" /><asp:HiddenField ID="hf_ProductID"
                                    runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            当前去税售价(元)
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_StandardSell" runat="server" class="tdinput" disabled></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            当前含税售价(元)
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_SellTax" runat="server" class="tdinput" disabled></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            当前销项税率(%)
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_TaxRate" runat="server" class="tdinput" disabled></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            当前折扣(%)
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_Discount" runat="server" class="tdinput" disabled></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            调整后去税售价(元)<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_StandardSellNew" MaxLength="12" runat="server" class="tdinput"
                                onblur='Number_round(this,$("#hidPoint").val());LoadSellTaxNew();'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            调整后销项税率(%)<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_TaxRateNew" runat="server" onkeydown='FractionDigits(this)'
                                class="tdinput" onblur='Number_round(this,$("#hidPoint").val());LoadSellTaxNew();'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            调整后折扣(%)<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_DiscountNew" runat="server" class="tdinput" onblur='Number_round(this,$("#hidPoint").val());'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            调整后含税售价(元)<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_SellTaxNew" name="txtConfirmorReal2" class="tdinput" runat="server"
                                size="20" onblur='Number_round(this,$("#hidPoint").val());' />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            申请人
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="UserChengerName" runat="server" onclick="alertdiv('UserChengerName,txtChenger');"
                                ReadOnly="true" CssClass="tdinput" Width="42%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            申请日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_ChangeDate" MaxLength="50" ReadOnly onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txt_ChangeDate')})"
                                runat="server" CssClass="tdinput" Width="121px"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            单据状态
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="hidden" id="txtChenger" runat="server" />
                            <select id="sel_BillStatus" name="D1" runat="server" disabled>
                                <option value="1">制单</option>
                                <option value="5">结单</option>
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            制单人
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:HiddenField ID="hf_Creator" runat="server" />
                            <asp:TextBox ID="txt_CreatorName" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="121px" ReadOnly Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            制单日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            &nbsp;<asp:TextBox ID="txt_CreateDate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="121px" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            调整原因
                        </td>
                        <td height="20" class="tdColInput" colspan="3">
                            <textarea id="txt_Remark" name="S1" rows="2" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr id="divConfirmor" style="display: none" runat="server">
                        <td height="20" align="right" class="tdColTitle">
                            确认人
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_ConfirmorName" name="txtConfirmorReal0" class="tdinput"
                                runat="server" disabled="disabled" /><asp:HiddenField ID="hf_Confirmor" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            确认日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" colspan="3">
                            <asp:HiddenField ID="hidSearchCondition" runat="server" />
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            <asp:TextBox ID="txt_ConfirmDate" Enabled="False" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="121px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            最后更新日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            最后更新用户ID
                        </td>
                        <td height="20" class="tdColInput" colspan="3">
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="93%" disabled Text=""></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <uc2:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc1:Message ID="Message2" runat="server" />
    </form>
</body>
</html>

<script language="javascript">
var intOtherCorpInfoID = <%=intOtherCorpInfoID %>;
var glb_SelPoint =  '<%=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).SelPoint.ToString() %>';/*小数精度*/
</script>

<script src="../../../js/office/SupplyChain/ProductPrice.js" type="text/javascript"></script>

