<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductInfoAdd.aspx.cs" Inherits="Pages_Office_SupplyChain_ProductInfoAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物品档案</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadPhoto.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <style type="text/css">
        #pageDataList1 TD
        {
            color: #333333;
        }
        #pageDataList1 A
        {
            color: White;
        }
        #userList
        {
            border: solid 1px #111111;
            width: 200px;
            z-index: 11;
            display: none;
            position: absolute;
            background-color: White;
        }
        #editPanel
        {
            width: 400px;
            background-color: #fefefe;
            position: absolute;
            border: solid 1px #000000;
            padding: 5px;
        }
    </style>
    <%-- <style type="text/css">
        .drp{ z-index}
    </style>--%>

    <script type="text/javascript">

        function SelectedNodeChanged(code_text, type_code, typeflag) {
            document.getElementById("txt_BigType").value = typeflag;
            document.getElementById("txt_TypeID").value = code_text;
            document.getElementById("txt_Code").value = type_code;

            switch (typeflag) {
                case "1":
                    document.getElementById("txt_BigTypeName").value = "成品";
                    break;
                case "2":
                    document.getElementById("txt_BigTypeName").value = "原材料";
                    break;
                case "3":
                    document.getElementById("txt_BigTypeName").value = "固定资产";
                    break;
                case "4":
                    document.getElementById("txt_BigTypeName").value = "低值易耗";
                    break;
                case "5":
                    document.getElementById("txt_BigTypeName").value = "包装物";
                    break;
                case "6":
                    document.getElementById("txt_BigTypeName").value = "服务产品";
                    break;
            }
            hideUserList();
        }
        function hidetxtUserList() {
            hideUserList();
            document.getElementById("txt_TypeID").value = "";
        }
        function ClearProductClass() {

            document.getElementById("txt_BigType").value = "";
            document.getElementById("txt_TypeID").value = "";       /*物品分类*/
            document.getElementById("txt_Code").value = "";        /*所属大类ID*/
            document.getElementById("txt_BigTypeName").value = ""; /*所属大类名称*/
            hideUserList();

        }
        function getChildNodes(nodeTable) {
            if (nodeTable.nextSibling == null)
                return [];
            var nodes = nodeTable.nextSibling;

            if (nodes.tagName == "DIV") {
                return nodes.childNodes; //return childnodes's nodeTables;
            }
            return [];
        }
        function showUserList() {
            var list = document.getElementById("userList");

            if (list.style.display != "none") {
                list.style.display = "none";
                return;
            }

            var pos = elePos(document.getElementById("txt_TypeID"));

            list.style.left = pos.x;
            list.style.top = pos.y + 20;
            document.getElementById("userList").style.display = "block";
        }


        function hideUserList() {
            document.getElementById("userList").style.display = "none";
        }
    </script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <style type="text/css">
        #sel_UsedStatus
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
        #sel_BigType0
        {
            width: 109px;
        }
        #Select2
        {
            width: 106px;
        }
        #sel_CheckStatus
        {
            width: 105px;
        }
        .style1
        {
            width: 23%;
            background-color: #FFFFFF;
        }
    </style>
</head>
<body text="100">
    <form id="frmMain" runat="server">
    <br />
    <uc1:Message ID="Message1" runat="server" />
    <input type="hidden" id="txtIndentityID" value="0" runat="server" />
    <!-- 小数位数 -->
    <input type="hidden" id="hidPoint" value="<%=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).SelPoint.ToString() %>" />
    
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
                               { %>物品档案<%}
                               else
                               { %>
                            新建物品档案
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
                                        <img id="product_btn_Add" src="../../../images/Button/Bottom_btn_save.jpg" onclick="Fun_Save_ProductInfo();"
                                            style="cursor: pointer; float: left" title="保存物品信息" runat="server" visible="false" /><span
                                                id="GlbFlowButtonSpan"></span>
                                        <img alt="确认" src="../../../Images/Button/Bottom_btn_confirm.jpg" id="product_btn_AD"
                                            onclick="ChangeStatus();" style="display: none; float: left" runat="server" visible="false" />
                                        <img alt="无法确认" src="../../../Images/Button/UnClick_qr.jpg" id="product_btnunsure"
                                            style="float: left" runat="server" visible="false" />
                                        <img src="../../../images/Button/Bottom_btn_back.jpg" border="0" style="float: left;"
                                            id="product_btnback" onclick="DoBack();" runat="server" />
                                        <font color="red">物品需确认之后才可以进行库存操作！</font>
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
                            物品编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <div id="divInputNo" runat="server" style="float: left">
                                <uc2:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <div id="divNo" runat="server" class="tdinput" style="float: left">
                            </div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            物品名称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="style1" width="23%">
                            <asp:TextBox ID="txt_ProductName" specialworkcheck="物品名称" onmouseout="LoadPYShort();"
                                MaxLength="50" runat="server" CssClass="tdinput" Width="90%"></asp:TextBox>
                        </td>
                        <td colspan="2" rowspan="8" align="center" class="tdColInput" width="34%">
                            <table>
                                <tr>
                                    <td colspan="3" width="80%">
                                        <img id="imgPhoto" runat="server" src="~/Images/Pic/Pic_Nopic.jpg" height="165" width="220" />
                                        <asp:HiddenField ID="hfPhotoUrl" runat="server" />
                                        <asp:HiddenField ID="hfPagePhotoUrl" runat="server" />
                                        <input type="hidden" id="uploadKind" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="#" onclick="DealEmployeePhoto('upload');">上传相片</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick="DealEmployeePhoto('clear');">清除相片</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            拼音缩写
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_PYShort" specialworkcheck="拼音缩写" name="txtConfirmorReal0"
                                class="tdinput" runat="server" style="width: 90%" />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            名称简称
                        </td>
                        <td height="20" class="style1">
                            <input type="text" id="txt_ShortNam" specialworkcheck="名称简称" name="txtConfirmorReal3"
                                class="tdinput" runat="server" size="20" style="width: 90%" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            条码
                        </td>
                        <td height="20" class="tdColInput">
                            <input id="txt_BarCode" specialworkcheck="条码" cols="20" maxlength="50" name="S1"
                                class="tdinput" runat="server" style="width: 90%" />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            物品分类
                        </td>
                        <td height="20" class="style1">
                            &nbsp;
                            <input type="text" id="txt_TypeID" readonly onclick="showUserList()" class="tdinput"
                                runat="server" style="width: 90%" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            基本单位<span class="redbold">*</span>&nbsp;
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:DropDownList ID="sel_UnitID" runat="server" Width="106px" onchange="InitGroupUnit();">
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            所属大类
                        </td>
                        <td height="20" class="style1">
                            <input id="txt_Code" type="hidden" runat="server" />
                            <input type="text" runat="server" id="txt_BigTypeName" readonly class="tdinput" style="width: 90%" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            计量单位组
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txtUnitGroup" class="tdinput" runat="server" style="width: 90%"
                                onclick="ShowUnitGroup();" />
                            <input id="HdGroupNo" type="hidden" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            规格型号
                        </td>
                        <td height="20" class="style1">
                            <input type="text" id="txt_Specification" name="txtConfirmorReal2" class="tdinput"
                                runat="server" style="width: 95%" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            采购计量单位
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:DropDownList ID="selPurchseUnit" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            颜色
                        </td>
                        <td height="20" class="style1">
                            <asp:DropDownList ID="sel_ColorID" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            销售计量单位
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:DropDownList ID="selSellUnit" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            品牌
                        </td>
                        <td height="20" class="style1">
                            <asp:DropDownList ID="sel_Brand" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            库存计量单位
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:DropDownList ID="selStorageUnit" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            档次级别
                        </td>
                        <td height="20" class="style1">
                            <asp:DropDownList ID="sel_GradeID" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            生产计量单位
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:DropDownList ID="selProductUnit" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            尺寸
                        </td>
                        <td height="20" class="style1">
                            <input type="text" id="txt_Size" specialworkcheck="尺寸" name="txt_Size" class="tdinput"
                                runat="server" style="width: 90%" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            是否启用批次
                        </td>
                        <td width="24%" height="20" class="tdColInput">
                            &nbsp; 是<input id="RdUseBatch" type="radio" value="1" validationgroup="5" runat="server"
                                name="ta" />
                            否<input id="RdNotUseBatch" type="radio" checked value="0" validationgroup="5" runat="server"
                                name="ta" />
                        </td>
                    </tr>
                </table>
                <uc3:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        价格信息
                                        <input id="txtPlanNoHidden" type="hidden" />
                                        <input id="txt_TypeCode" type="hidden" />
                                    </td>
                                    <td align="right">
                                        <div id='searchClick2'>
                                            <input id="Hidden1" type="hidden" />
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
                            ABC分类
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            &nbsp;<asp:DropDownList ID="sel_ABCType" runat="server" Width="106px">
                                <asp:ListItem Value="">--请选择--</asp:ListItem>
                                <asp:ListItem Value="A">A类</asp:ListItem>
                                <asp:ListItem Value="B">B类</asp:ListItem>
                                <asp:ListItem Value="C">C类</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            成本核算计价方法<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:DropDownList ID="sel_CalcPriceWays" runat="server" Width="106px">
                                <asp:ListItem Value="1">加权平均法</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            标准成本(元)
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_StandardCost" name="txtConfirmorReal1" class="tdinput"
                                runat="server" style="width: 90%" onblur='Number_round(this,$("#hidPoint").val())' />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            去税售价(元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_StandardSell" runat="server" class="tdinput" Width="175px" onblur='Number_round(this,$("#hidPoint").val());LoadSellTaxNew(true);'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            销项税率(%)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_TaxRate" MaxLength="50" runat="server" CssClass="tdinput" Width="80%"
                                onblur='Number_round(this,$("#hidPoint").val());LoadSellTaxNew(true);'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            含税售价(元)
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_SellTax" name="txtConfirmorReal4" class="tdinput" width="24%"
                                runat="server" size="20" style="width: 90%" onblur='Number_round(this,$("#hidPoint").val());LoadSellTaxNew(false);' />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            零售价(元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_SellPrice" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="76%" onblur='Number_round(this,$("#hidPoint").val());'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            销售折扣率(%)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_Discount" MaxLength="50" runat="server" CssClass="tdinput" Width="80%"
                                onblur='Number_round(this,$("#hidPoint").val());'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            调拨单价(元)
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_TransfrePrice" name="txtConfirmorReal5" class="tdinput"
                                runat="server" size="20" style="width: 90%" onblur='Number_round(this,$("#hidPoint").val());' />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            去税进价(元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            &nbsp;<asp:TextBox ID="txt_TaxBuy" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="74%" onblur='Number_round(this,$("#hidPoint").val());LoadSellTax(true);'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            &nbsp;进项税率(%)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_InTaxRate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="80%" onblur='Number_round(this,$("#hidPoint").val());LoadSellTax(true);'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            含税进价(元)
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_StandardBuy" name="txtConfirmorReal6" class="tdinput"
                                runat="server" size="20" style="width: 90%" onblur='Number_round(this,$("#hidPoint").val());LoadSellTax(false);' />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        库存信息
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
                            是否计入库存
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            是<input id="rd_StockIs" type="radio" value="1" checked validationgroup="4" runat="server" />
                            否<input id="rd_notStockIs" type="radio" value="0" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            是否允许负库存
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:RadioButton ID="rd_MinusIs" runat="server" ValidationGroup="6" GroupName="6" />是
                            <asp:RadioButton ID="rd_notMinusIs" runat="server" ValidationGroup="6" Checked GroupName="6" />否
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            主放仓库 <span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:DropDownList ID="sel_StorageID" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            安全库存量
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_SafeStockNum"  name="txtConfirmorReal0"
                                class="tdinput" runat="server" style="width: 90%" onblur='Number_round(this,$("#hidPoint").val());' />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            最低库存量
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_MinStockNum"  MaxLength="50"
                                runat="server" CssClass="tdinput" Width="80%" onblur='Number_round(this,$("#hidPoint").val());'></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            最高库存量
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txt_MaxStockNum" runat="server"
                                CssClass="tdinput" Width="80%" onblur='Number_round(this,$("#hidPoint").val());'></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="storage" style="display: none" runat="server">
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            当前库存量
                        </td>
                        <td height="20" class="tdColInput" width="23%" colspan="6">
                            <input type="text" id="txt_Storage" name="txtConfirmorReal0"
                                class="tdinput" runat="server" readonly style="width: 90%" onblur='Number_round(this,$("#hidPoint").val());' />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        附加信息
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
                    id="Tb_04">
                    <tr>
                        <td class="tdColTitle" width="10%">
                            物品来源分类
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:DropDownList ID="sel_Source" runat="server" Width="106px">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="0">自制</asp:ListItem>
                                <asp:ListItem Value="1">外购</asp:ListItem>
                                <asp:ListItem Value="2">委外</asp:ListItem>
                                <asp:ListItem Value="3">虚拟件</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            产地
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_FromAddr" specialworkcheck="产地" runat="server" CssClass="tdinput"
                                Width="178px" Height="21px"></asp:TextBox>
                        </td>
                        <td height="20" class="tdColTitle" width="10%">
                            图号
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txt_DrawingNum" specialworkcheck="图号" runat="server" CssClass="tdinput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            启用状态
                        </td>
                        <td height="20" class="tdColInput">
                            <select id="sel_UsedStatus" name="D2" runat="server" disabled="disabled">
                                <option value=""></option>
                                <option value="1">启用</option>
                                <option value="0">停用</option>
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            批准文号
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txt_FileNo" specialworkcheck="批准文号" runat="server" CssClass="tdinput"
                                Width="90%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            价格策略
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txt_PricePolicy" specialworkcheck="价格策略" runat="server" CssClass="tdinput"
                                Width="71%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle">
                            技术参数
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txt_Params" runat="server" CssClass="tdinput" Width="120px"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            常见问题
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txt_Questions" runat="server" CssClass="tdinput" Width="176px"></asp:TextBox>
                        </td>
                        <td class="tdColTitle" width="100" align="left">
                            替代品名称
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txt_ReplaceName" specialworkcheck="替代品名称" runat="server" CssClass="tdinput"
                                Width="50%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            物品描述信息
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_Description" runat="server" CssClass="tdinput" Width="77%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            厂家
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txt_Manufacturer" specialworkcheck="厂家" runat="server" CssClass="tdinput"
                                Width="63%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle">
                            材质
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txtPrincipal" runat="server" />
                            <asp:DropDownList ID="sel_Material" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            建档日期<span class="redbold">*</span>
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txt_CreateDate" runat="server" CssClass="tdinput" Width="77%" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            建档人<span class="redbold">*</span> &nbsp;&nbsp;
                        </td>
                        <td height="20" align="left" class="tdColInput" width="106px">
                            &nbsp;<asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="180px" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            审核状态
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txt_CheckUser" runat="server" />
                            <select id="sel_CheckStatus" name="D1" runat="server" disabled>
                                <option value="0">草稿</option>
                                <option value="1">已审</option>
                            </select>
                        </td>
                    </tr>
                    <tr id="divConfirmor" style="display: none" runat="server">
                        <td align="right" class="tdColTitle">
                            审核日期
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txt_CheckDate" runat="server" CssClass="tdinput" Width="77%" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            审核人
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txt_CheckUserName" runat="server" CssClass="tdinput" Width="71%"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            <input type="hidden" id="hidSearchCondition" runat="server" />
                            <input type="hidden" id="hidFromPage" runat="server" />
                            备注
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txt_Remark" runat="server" CssClass="tdinput" Width="90%" TextMode="MultiLine"></asp:TextBox>
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
    <div id="userList" style="display: none;">
        <iframe id="aaaa" style="position: absolute; z-index: -1; width: 200px; height: 100px;"
            frameborder="0"></iframe>
        <div style="background-color: Silver; padding: 3px; height: 20px; padding-left: 50px;
            padding-top: 1px">
            <table width="100%">
                <tr>
                    <td align="right">
                        <a href="javascript:ClearProductClass()">清空</a>
                    </td>
                    <td width="20%" align="right">
                        <a href="javascript:hidetxtUserList()">关闭</a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 5px; height: 270px; width: 200px; overflow: auto; margin-top: 1px">
            <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
            </asp:TreeView>
        </div>
    </div>
    <p>
        &nbsp;</p>
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="hiddKey" />
    <asp:HiddenField ID="hidListModuleID" runat="server" />
    <asp:HiddenField ID="hidModuleID" runat="server" />
    <asp:HiddenField ID="txt_PlanCost" runat="server" />
    <asp:HiddenField ID="txt_SellMax" runat="server" />
    <asp:HiddenField ID="txt_SellMin" runat="server" />
    <asp:HiddenField ID="txt_BigType" runat="server" />
    <asp:HiddenField ID="txt_BuyMax" runat="server" />
    <asp:HiddenField ID="txt_IsConfirmProduct" runat="server" Value="" />
    </form>
    <p>
        &nbsp;</p>
    <div id="DivUnitGroup" style="display: none; border: solid 8px #93BCDD; background: #fff;
        padding: 10px; width: 70%; top: 20%; z-index: 200; position: absolute;">
        <div id="divBackShadow" style="display: none">
            <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
                width="100%"></iframe>
        </div>
        <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td>
                    <%-- <a onclick="closeProductdiv('divStorageProduct')" style="text-align: right; cursor: pointer">关闭</a>--%>
                    <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                        onclick='closeProductUnitdiv();' />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClick'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" class="Blue">
                    <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                    <uc1:Message ID="Message3" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                        bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                    class="table">
                                    <tr class="table-item">
                                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                            计量单位组编号
                                        </td>
                                        <td width="20%" bgcolor="#FFFFFF">
                                            <input id="txtSGUNO" class="tdinput" type="text" />
                                        </td>
                                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                            计量单位组名称
                                        </td>
                                        <td width="20%" bgcolor="#FFFFFF" colspan="3">
                                            <input id="txtSGUName" class="tdinput" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                onclick='SearchData()' id="btnSearch" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td align="center" valign="top">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="30" colspan="2" align="center" valign="top" class="Title">
                    计量单位组列表
                </td>
            </tr>
            <tr>
                <td height="35" colspan="2" valign="top">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">
                                <img alt="" src="../../../Images/Button/Bottom_btn_new.jpg" onclick="ShowAdd(true);"
                                    runat="server" id="btnAdd" visible="false" style="cursor: hand;" />
                                <img alt="" src="../../../Images/Button/Main_btn_delete.jpg" onclick="DeleteData();"
                                    id="btnDel" runat="server" visible="false" style="cursor: hand;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                        bgcolor="#999999">
                        <tbody>
                            <tr>
                                <th height="20" width="80px" align="center" background="../../../images/Main/Table_bg.jpg"
                                    bgcolor="#FFFFFF">
                                    选择
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderBy('GroupUnitNo','oGroupUnitNo');return false;">
                                        计量单位组编号<span id="oGroupUnitNo" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderBy('GroupUnitName','oGroupUnitName');return false;">
                                        计量单位组名称<span id="oGroupUnitName" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderBy('BaseUnitID','oBaseUnitID');return false;">
                                        基本计量单位<span id="oBaseUnitID" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="OrderBy('Remark','oRemark');return false;">
                                        备注<span id="oRemark" class="orderTip"></span></div>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg">
                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                    <tr>
                                        <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                            <div id="pagecount">
                                            </div>
                                        </td>
                                        <td height="28" align="right">
                                            <div id="pageDataList1_Pager" class="jPagerBar">
                                            </div>
                                        </td>
                                        <td height="28" align="right">
                                            <div id="divpage">
                                                <input name="text" type="text" id="Text4" style="display: none" />
                                                <span id="pageDataList1_Total"></span>每页显示
                                                <input name="text" type="text" id="ShowPageCount" />
                                                条 转到第
                                                <input name="text" type="text" id="ToPage" />
                                                页
                                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                    width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                            </div>
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
        <a name="pageDataList1Mark"></a><span id="Span1" class="Spantype"></span>
        <input id="hidRow" type="hidden" value="1" />
        <select id="selUnitHid" style="visibility: hidden">
        </select>
    </div>
</body>
</html>

<script language="javascript">
var intOtherCorpInfoID = <%=intOtherCorpInfoID %>;
var isMoreUnit = <%= IsMoreUnit.ToString().ToLower() %>;// 多计量单位控制参数
var glb_SelPoint =  '<%=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).SelPoint.ToString() %>';/*小数精度*/
var SysModuleID = '<%=SysModuleID %>';
</script>

<script src="../../../js/office/SupplyChain/ProductInfoAdd.js" type="text/javascript"></script>

<script src="../../../js/office/SupplyChain/ProductUntGroup.js" type="text/javascript"></script>

