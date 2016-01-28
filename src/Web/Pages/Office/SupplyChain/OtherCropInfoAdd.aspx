<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OtherCropInfoAdd.aspx.cs"
    Inherits="Pages_Office_SupplyChain_OtherCropInfoAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>其他往来单位信息</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script type="text/javascript">
    function ChangeValue()
    {
     if(document.getElementById("chk_isTax").checked)
     {
      document.getElementById("lblmsg").innerHTML="是"; 
     }
     else
     {
      document.getElementById("lblmsg").innerHTML="否"; 
     }
    }
    </script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <style type="text/css">
        #Select1
        {
            width: 106px;
        }
        #sel_UsedStatus
        {
            width: 106px;
        }
        #sel_AreaID
        {
            width: 106px;
        }
        #sel_AreaID0
        {
            width: 106px;
        }
        #sel_PayType
        {
            width: 106px;
        }
        #sel_PayType0
        {
            width: 106px;
        }
        #sel_BillType
        {
            width: 106px;
        }
        #sel_BigType
        {
            width: 109px;
        }
    </style>
</head>
<body>
    <br />
    <form id="Form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <input type="hidden" id="txtIndentityID" value="0" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class=""
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
                               { %>其他往来单位
                            <%}
                               else
                               { %>
                            新建其他往来单位档案
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
                                        <img id="btnAdd" src="../../../images/Button/Bottom_btn_save.jpg" onclick="Fun_Save_OtherCorpInfo();"
                                            style="cursor: pointer" title="保存其他往来单位信息" runat="server" visible="false" /><span
                                                id="GlbFlowButtonSpan"></span> <a onclick="DoBack();">
                                                    <img src="../../../images/Button/Bottom_btn_back.jpg" border="0" /></a>
                                    </td>
                                    <td align="right">
                                        &nbsp;
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
                                        <div id='searchClick1'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick1')" /></div>
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
                            往来单位编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_CustNo" name="txtConfirmorReal0" class="tdinput" runat="server"
                                onblur="checkonly();" maxlength="50" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            往来单位名称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_CustName" specialworkcheck="往来单位名称" onmouseout="LoadPYShort();"
                                MaxLength="50" runat="server" CssClass="tdinput" Width="73%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            往来单位简称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_CorpNam" specialworkcheck="往来单位简称" name="txtConfirmorReal1"
                                class="tdinput" runat="server" maxlength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            拼音代码
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_PYShort" specialworkcheck="拼音代码" name="txtConfirmorReal0"
                                class="tdinput" runat="server" maxlength="50" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单位简介
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <textarea id="txt_CustNote" specialworkcheck="单位简介" cols="20" name="S1" rows="2"
                                cssclass="tdinput" runat="server"></textarea>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            所在区域<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:DropDownList ID="sel_AreaID" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单位性质
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_CompanyType" specialworkcheck="单位性质" name="txtConfirmorReal0"
                                class="tdinput" runat="server" maxlength="50" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            人员规模
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_StaffCount" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="72%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            成立时间
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_SetupDate" name="txtConfirmorReal1" class="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txt_SetupDate')})"
                                readonly runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100" align="left">
                            往来单位大类<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" colspan="5">
                            <select id="sel_BigType" name="D2" runat="server">
                                <option value="5">外协加工厂</option>
                                <option value="6">运输商</option>
                                <option value="7">其他</option>
                            </select>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        注册信息
                                        <input id="txtPlanNoHidden" type="hidden" />
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
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            法人代表
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_ArtiPerson" specialworkcheck="法人代表" name="txtConfirmorReal0"
                                class="tdinput" runat="server" maxlength="50" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            注册资本
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_SetupMoney" onkeydown='FractionDigits(this)' MaxLength="50"
                                runat="server" CssClass="tdinput" Width="72%"></asp:TextBox><span>万元</span>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            注册地址
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="hidden" id="Hidden3" runat="server" />
                            <input type="text" id="txt_SetupAddress" specialworkcheck="注册地址" name="txtConfirmorReal1"
                                class="tdinput" runat="server" maxlength="200" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            资产规模
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_CapitalScale" onkeydown='FractionDigits(this)' name="txtConfirmorReal0"
                                class="tdinput" runat="server" /><span>万元</span>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            年销售额
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_SaleroomY" MaxLength="50" onkeydown='FractionDigits(this)' runat="server"
                                CssClass="tdinput" Width="72%"></asp:TextBox><span>万元</span>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            年利润额
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txt_ProfitY" onkeydown='FractionDigits(this)' runat="server" CssClass="tdinput"
                                Width="61%"></asp:TextBox><span>万元</span>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            税务登记号
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_TaxCD" specialworkcheck="税务登记号" name="txtConfirmorReal0"
                                class="tdinput" runat="server" maxlength="50" /></div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            营业执照号
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_BusiNumber" specialworkcheck="营业执照号" MaxLength="50" runat="server"
                                CssClass="tdinput" Width="72%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            是否一般纳税人
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input id="chk_isTax" type="checkbox" checked onclick="ChangeValue();" runat="server" /><asp:Label
                                ID="lblmsg" runat="server" Text="是"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            经营范围
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            &nbsp;<textarea id="txt_SellArea" specialworkcheck="经营范围" cols="20" name="S2" rows="2"
                                runat="server"></textarea>
                        </td>
                        <td height="20" align="right" class="tdColInput" width="10%" colspan="4">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        通讯信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonTotal'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','divButtonTotal')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            国家地区<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:DropDownList ID="sel_CountryID" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            省份
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_Province" specialworkcheck="省份" MaxLength="50" runat="server"
                                CssClass="tdinput" Width="72%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            市（县）
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_City" specialworkcheck=" 市（县）" name="txtConfirmorReal1"
                                class="tdinput" runat="server" maxlength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            邮编
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_Post" specialworkcheck="邮编" name="txtConfirmorReal0" class="tdinput"
                                runat="server" maxlength="50" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            联系人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_ContactName" specialworkcheck="联系人" MaxLength="50" runat="server"
                                CssClass="tdinput" Width="120px"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            电话
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txt_Tel" runat="server" MaxLength="50" CssClass="tdinput" Width="61%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            传真
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_Fax" name="txtConfirmorReal0" class="tdinput" runat="server"
                                maxlength="50" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            手机
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_Mobile" MaxLength="50" runat="server" CssClass="tdinput" Width="72%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            邮件
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txt_email" runat="server" CssClass="tdinput" Width="61%" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            地址
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_Addr" specialworkcheck="地址" name="txtConfirmorReal2" class="tdinput"
                                runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColInput" width="10%" colspan="4">
                            &nbsp;
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
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','divButtonNote')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_04" style="display: block">
                    <tr>
                        <td class="tdColTitle" width="10%">
                            发票类型<span class="redbold">*</span>&nbsp;
                        </td>
                        <td class="tdColInput" width="23%">
                            <select id="sel_BillType" name="D3" runat="server">
                                <option value="1">增值税发票</option>
                                <option value="2">普通地税</option>
                                <option value="3">普通国税</option>
                                <option value="4">收据</option>
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            结算方式
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:DropDownList ID="sel_PayType" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            支付方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:DropDownList ID="sel_MoneyType" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            结算币种 <span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:DropDownList ID="sel_CurrencyType" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            启用状态<span class="redbold">*</span>&nbsp;
                        </td>
                        <td height="20" align="left" class="tdColInput" width="106px">
                            <select id="sel_UsedStatus" name="D1" runat="server">
                                <option value="1">启用</option>
                                <option value="0">停用</option>
                            </select>
                        </td>
                        <td class="tdColTitle">
                            备注
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txt_Remark" runat="server" CssClass="tdinput" Width="95%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle">
                            建档人：<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="113px" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="txtPrincipal" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            <%--    <input type="hidden" id="txt_Creator" runat="server" />--%>
                            建档日期
                        </td>
                        <td height="20" align="left" class="tdColInput" colspan="3">
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            <asp:HiddenField ID="hidSearchCondition" runat="server" />
                            <asp:TextBox ID="txt_CreateDate" runat="server" CssClass="tdinput" Width="99%" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txt_CreateDate')})"
                                Enabled="False"></asp:TextBox>
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
    </form>
</body>
</html>

<script language="javascript">
var intOtherCorpInfoID = <%=intOtherCorpInfoID %>;
</script>

<script src="../../../js/office/SupplyChain/OtherCorpInfo.js" type="text/javascript"></script>

