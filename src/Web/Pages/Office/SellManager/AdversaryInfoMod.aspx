<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdversaryInfoMod.aspx.cs"
    Inherits="Pages_Office_SellManager_AdversaryInfoMod" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建竞争对手档案</title>
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

    <script src="../../../js/office/SellManager/AdversaryInfoMod.js" type="text/javascript"></script>

    <style type="text/css">
        .userListCss
        {
            position: absolute;
            top: 200px;
            left: 400px;
            border-width: 1pt;
            border-color: #EEEEEE;
            border-style: solid;
            width: 400px;
            display: none;
            height: 220px;
            z-index: 100;
        }
    </style>

    <script type="text/javascript">
        function SelectedNodeChanged(code_text, type_code) {
            $("#CustClass").val(code_text);
            $("#CustClass").attr("title", type_code);

            hideUserList();
        }
        function hidetxtUserList() {
            hideUserList();
        }
        function showUserList() {
            document.getElementById("userList").style.display = "block";
        }
        function hideUserList() {
            document.getElementById("userList").style.display = "none";
        }
    </script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
</head>
<body>
    <form id="Form1" runat="server">
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
                            <td height="30" align="center" class="Title">
                                <div id="divTitle" runat="server">
                                    竞争对手档案</div>
                            </td>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                        <img runat="server" visible="false" src="../../../images/Button/Bottom_btn_save.jpg"
                                            alt="保存" id="btn_save" style="cursor: hand" onclick="InsertSellOfferData();" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="ibtnBack" style="cursor: hand;
                                            display: inline;" onclick="fnBack();" />
                                    </td>
                                    <td align="right" bgcolor="#FFFFFF" style="padding-top: 5px; width: 70px;">
                                        <img id="btnPrint" alt="打印" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印" onclick="fnPrintOrder()" />
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
                                        对手基本信息
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
                    id="Tb_01" >
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            对手编号<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <uc2:CodingRuleControl ID="CustNo" runat="server" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            对手类别
                        </td>
                        <td class="tdColInput" width="23%">
                            <uc3:CodeTypeDrpControl ID="CustType" runat="server" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            对手细分<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="CustClass" readonly="readonly" class="tdinput" style="width: 95%;" onclick="showUserList()"
                                type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            对手名称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="CustName" style="width: 95%;" specialworkcheck="对手名称" class="tdinput"
                                onblur="LoadPYShort();" maxlength="50" type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            对手简称
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="ShortNam" style="width: 95%;" class="tdinput" specialworkcheck="对手简称"
                                maxlength="25" type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            对手拼音代码
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="PYShort" style="width: 95%;" specialworkcheck="对手拼音代码" class="tdinput"
                                type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="20" class="tdColTitle" width="10%">
                            对手所在区域
                        </td>
                        <td class="tdColInput" style="height: 20px; width: 23%">
                            <uc3:CodeTypeDrpControl ID="AreaID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="23%">
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
                                        对手辅助信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonTotal'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','divButtonTotal')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_02" >
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            成立时间
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="SetupDate" class="tdinput" readonly="readonly" style="width: 95%;" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SetupDate')})"
                                type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            法人代表
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" style="width: 95%;" specialworkcheck="法人代表" class="tdinput" maxlength="10"
                                id="ArtiPerson" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单位性质<span class="redbold">*</span>&nbsp;
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select id="CompanyType" style="width: 120px;margin-top:2px;margin-left:2px;">
                                <option selected="selected" value="1">事业</option>
                                <option value="2">企业</option>
                                <option value="3">社团</option>
                                <option value="4">自然人</option>
                                <option value="5">其他</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            员工总数
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="StaffCount" maxlength="8" style="width: 95%;" class="tdinput" type="text" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            注册资本(万元)
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="SetupMoney" onchange="Number_round(this,2)" maxlength="10" style="width: 95%;"
                                class="tdinput" type="text" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            注册地址
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="SetupAddress" maxlength="50" specialworkcheck="注册地址" style="width: 95%;"
                                class="tdinput" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            公司网址(http://)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="website" maxlength="50" specialworkcheck="公司网址" style="width: 95%;" class="tdinput"
                                type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            资产规模(万元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="CapitalScale" onchange="Number_round(this,2)" maxlength="10" style="width: 95%;"
                                class="tdinput" type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            经营范围
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="SellArea" maxlength="100" specialworkcheck="经营范围" style="width: 95%;"
                                class="tdinput" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            年销售额(万元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="SaleroomY" onchange="Number_round(this,2)" maxlength="10" style="width: 95%;"
                                class="tdinput" type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            年利润额(万元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="ProfitY" onchange="Number_round(this,2)" maxlength="10" style="width: 95%;"
                                class="tdinput" type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            税务登记号
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="TaxCD" maxlength="30" specialworkcheck="税务登记号" style="width: 95%;" class="tdinput"
                                type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            营业执照号
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="BusiNumber" maxlength="30" specialworkcheck="营业执照号" style="width: 95%;"
                                class="tdinput" type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            一般纳税人<span class="redbold">*</span>&nbsp;
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select id="IsTax" style="width: 120px;margin-top:2px;margin-left:2px;">
                                <option value="1" selected="selected">是</option>
                                <option value="0">否</option>
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            地址
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="Address" maxlength="50" specialworkcheck="地址" style="width: 95%;" class="tdinput"
                                type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            邮编
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="Post" maxlength="10" specialworkcheck="邮编" style="width: 95%;" class="tdinput"
                                type="text" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            联系人
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="ContactName" maxlength="25" specialworkcheck="联系人" style="width: 95%;"
                                class="tdinput" type="text" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            电话
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="Tel" maxlength="25" specialworkcheck="电话" style="width: 95%;" class="tdinput"
                                type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            手机
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="Mobile" maxlength="25" specialworkcheck="手机" style="width: 95%;" class="tdinput"
                                type="text" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            邮件
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="email" maxlength="25" specialworkcheck="邮件" class="tdinput" style="width: 95%;"
                                type="text" />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                        </td>
                        <td class="tdColInput" width="23%">
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
                                        对手分析及对策
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
                    id="Tb_03" >
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            对手简介
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="CustNote" rows="3" specialworkcheck="对手简介" cols="100" style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            主打产品
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="Product" rows="3" cols="100" specialworkcheck="主打产品" style="width: 95%"></textarea>
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
                            <textarea id="Power" rows="3" cols="100" specialworkcheck="竞争能力" style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            竞争优势
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="Advantage" rows="3" cols="100" specialworkcheck="竞争优势" style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            竞争劣势
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="disadvantage" rows="3" cols="100" specialworkcheck="竞争劣势" style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            应对策略
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea id="Policy" rows="3" cols="100" specialworkcheck="应对策略" style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            市场占有率(%)
                        </td>
                        <td class="tdColInput" colspan="2" width="40%">
                            <textarea id="Market" rows="3" cols="100" specialworkcheck="市场占有率" style="width: 95%"></textarea>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            销售模式
                        </td>
                        <td class="tdColInput" width="40%" colspan="2">
                            <textarea id="SellMode" rows="3" cols="100" specialworkcheck="销售模式" style="width: 95%"></textarea>
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
                                        对手备注信息
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
                            启用状态<span class="redbold">*</span>&nbsp;
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%;">
                            <select id="UsedStatus" style="width: 120px;margin-top:2px;margin-left:2px;" name="D1">
                                <option value="1" selected="selected">启用</option>
                                <option value="0">停用</option>
                            </select>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%;">
                            制单人
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%;">
                            <asp:TextBox ID="Creator" Width="120px" runat="server" CssClass="tdinput" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%;">
                            制单日期
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="CreateDate" runat="server" Width="120px" CssClass="tdinput" ReadOnly="True"
                                Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            最后更新人
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="ModifiedUserID" runat="server" Width="120px" CssClass="tdinput"
                                ReadOnly="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            最后更新日期
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <asp:TextBox ID="ModifiedDate" runat="server" Enabled="False" Width="120px" CssClass="tdinput"
                                ReadOnly="True"></asp:TextBox>
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
                            <textarea id="Remark" rows="3" cols="100" specialworkcheck="备注" style="width: 95%"></textarea>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="99%" id="TableDTB" border="0" align="center" cellpadding="2" cellspacing="1"
                    bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        对手动态信息
                                    </td>
                                    <td align="right">
                                        <div id='divDongTai'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('TableDTN','divDongTai')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                    id="TableDTN" >
                    <tr>
                        <td style="width: 100%">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" colspan="11" style="padding-top: 5px; padding-left: 5px;">
                                        <img runat="server" visible="false" src="../../../images/Button/Show_add.jpg" id="imgAdd"
                                            style="cursor: hand" onclick="AddSignRow();" />
                                        <img runat="server" visible="false" src="../../../images/Button/Show_del.jpg" id="imgDel"
                                            style="cursor: hand" onclick="fnDelOneRow()" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" id="dg_Log" style="behavior: url(../../../draggrid.htc)"
                                align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 6%">
                                        选择<input type="checkbox" visible="false" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                                    </td>
                                    <td bgcolor="#E6E6E6" style="width: 95%; text-align: left; padding-left: 20px;" class="ListTitle">
                                        对手动态
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input type='hidden' id="rowIndex" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id="HiddenURLParams" runat="server" />
    <div id="userList" class="userListCss" style="overflow-x: hidden; left: 200px; z-index: 110;
        overflow-y: scroll">
        <table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" bgcolor="#999999">
            <tr>
                <td height="20" bgcolor="#E6E6E6" class="Blue">
                    <table width="100%" border="0" cellspacing="0" cellpadding="">
                        <tr>
                            <td width="45%">
                                竞争对手类别细分选择
                            </td>
                            <td align="right">
                                <img src="../../../Images/Pic/Close.gif" alt="关闭" style="cursor: pointer" onclick="hideUserList();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
    <uc1:Message ID="Message1" runat="server" />
    <asp:HiddenField ID="hiddAcction" runat="server" Value="update" />
    </form>
</body>
</html>
