<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderInfo_Add.aspx.cs" Inherits="Pages_Office_PurchaseManager_ProviderInfo_Add" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRuleControl" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建供应商档案</title>
    
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/office/PurchaseManager/ProviderInfoAdd.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UploadFile.js" type="text/javascript">function ddlBillStatus_onclick() {

}

</script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <style type="text/css">
         .userListCss
        {
	        position:absolute;top:100px;left:600px;
	        border-width:1pt;border-color:#EEEEEE;border-style:solid;
	        width:200px;
	        display:none;
	        height:220px;
	        z-index:100;
	    }
        .style1
        {
            border-width: 0pt;
            background-color: #ffffff;
            height: 9px;
            width: 100px;
        }
        .style2
        {
            background-color: #E6E6E6;
            text-align: right;
        }
        .style3
        {
            background-color: #FFFFFF;
        }
    </style>
    <script type="text/javascript">
 
function SelectedNodeChanged(code_text,type_code)
{   
   document.getElementById("txtCustClassName").value=code_text;
   document.getElementById("txtCustClass").value=type_code;
   hideUserList();
}
function hidetxtUserList()
{
   hideUserList();
}
function showUserList()
{
  var list = document.getElementById("userList");
  if(list.style.display != "none")
   {
      list.style.display = "none";
      return;
   }
   document.getElementById("userList").style.display = "block";
}
function hideUserList()
{
 document.getElementById("userList").style.display = "none";
}



function clearInfo() {
    document.getElementById("txtCustClassName").value = "";
    document.getElementById("txtCustClass").value = "";
    hideUserList();
}


</script>
</head>
<body>
    <form id="Form1" runat="server">
    
    <input id="HiddenPoint" type="hidden" runat="server" />
    <uc1:Message   ID="Message1" runat="server" />
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
                                新建供应商档案</div>
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
                                    <td>
                                        &nbsp;<img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server"  alt="保存" id="save_PurchaseReject" style="cursor:pointer" onclick="InsertProviderInfo();" visible="false" runat="server"/>
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" onclick="Back();" /></td>
                                    <td align="right">
                                        <img  src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style=" float:right; cursor: pointer;"  id="imgPrint"  onclick="ProviderInfoPrint();" /> 
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <!-- End 流程处理-->
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <input type="hidden" id="txtIsliebiaoNo" value="0" runat="server" />
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
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr>
                        <td align="right" class="style2" width="10%">
                            供应商编号<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <div id="divInputNo" runat="server">
                                <uc2:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <div id="divProviderInfoNo" runat="server" class="tdinput"  style="display: none">
                            </div>
                        </td>
                        <td align="right" class="style2" width="10%">
                            供应商类别<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <select name="drpCustType" class="tdinput"  width="119px" runat="server" id="drpCustType"></select>
                        </td>
                        <td align="right" class="style2" width="10%">
                            供应商分类
                        </td>
                        <td class="style3" width="24%">
                            <asp:TextBox ID="txtCustClassName" runat="server" onclick="showUserList()" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input id="txtCustClass" type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            供应商名称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                                <asp:TextBox ID="txtCustName" runat="server" MaxLength="50"  CssClass="tdinput" Width="95%" SpecialWorkCheck="供应商名称"  ></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            供应商简称
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtCustNam" runat="server" MaxLength="25"  CssClass="tdinput"  onblur="LoadPYShort();" Width="95%"  SpecialWorkCheck="供应商简称" ></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            供应商拼音代码
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtPYShort" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"></asp:TextBox>
                            <%--<asp:TextBox ID="txtProviderID" runat="server"  CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtHidProviderID" runat="server" />
                            <input name="txtHiddenProviderID1" id="txtHiddenProviderID1" type="text" class="tdinput" size="15" disabled="disabled" style="display:none" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            供应商简介
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <%--<asp:TextBox ID="txtCustNote" MaxLength="512" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>--%>
                            <textarea name="txtCustNote" id="txtCustNote" rows="3" cols="80" style="width:95%"></textarea>
                            
                        </td>
                    </tr>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        业务信息
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
                    id="Tb_03" style="display: block">
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            国家地区
                        </td>
                        <td class="tdColInput" width="23%">
                           <select name="drpCountryID" class="tdinput" width="119px" runat="server" id="drpCountryID"></select>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            省
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtProvince" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%" SpecialWorkCheck="省" ></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            市(县)
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtCity" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="市(县)" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            邮编
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPost" runat="server"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="邮编" ></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            联系人
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtContactName" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="联系人" ></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            电话
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtTel" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="电话" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            传真
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtFax" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="传真" ></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            手机
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtMobile" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="手机" ></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            邮件
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtemail" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="10%">
                            在线咨询
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtOnLine" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            公司网址
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtWebSite" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            交货方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="drpTakeType" class="tdinput" width="119px" runat="server" id="drpTakeType"> </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" >
                            运送方式
                        </td>
                        <td height="20" class="tdColInput">
                            <select name="drpCarryType" class="tdinput" width="119px" runat="server" id="drpCarryType"> </select>
                            <input type="hidden" id="txtConfirmor" name="txtConfirmor" class="tdinput" runat="server" readonly />
                            <input name="UserName" id="UserName" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"  />
                            <input name="UserID" id="UserID" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"/>
                            <input name="SystemTime" id="SystemTime" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"/>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            供应商优质级别
                        </td>
                        <td height="20" align="left" class="tdinput">
                            <select name="drpCreditGrade" class="tdinput" width="119px" runat="server" id="drpCreditGrade"></select>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            热点供应商<span class="redbold">*</span>
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <select name="drpHotIs"  class="tdinput"  id="drpHotIs" >
                                        <option value="2" selected="selected">否</option>
                                        <option value="1">是</option></select>
                        </td>
                    </tr>
                    <%--<tr>
                        <td height="20" align="right" class="tdColTitle">
                            开户行
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txtOpenBank" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            户名
                        </td>
                        <td class="tdColInput">
                             <asp:TextBox ID="txtAccountMan" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            账号
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtAccountNum" runat="server" MaxLength="25" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            启用状态<span class="redbold">*</span>
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <select name="drpUsedStatus"  class="tdinput" id="drpUsedStatus" >
                                        <option value="1" selected="selected">启用</option>
                                        <option value="0">停用</option></select>
                        </td>
                        <td class="tdColTitle">
                            分管采购员
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="UsertxtManager" onclick="alertdiv('UsertxtManager,HidManager');" runat="server"
                             ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                             <input type="hidden" id="HidManager" runat="server" />
                        </td>
                        <td class="tdColTitle">
                            联络期限(天)<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                            <select name="drpLinkCycle" class="tdinput" width="119px" runat="server" id="drpLinkCycle"></select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            所在区域
                        </td>
                        <td class="tdColInput" width="23%">
                           <select name="drpAreaID" class="tdinput" width="119px" runat="server" id="drpAreaID"> </select>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            发货地址
                        </td>
                        <td class="tdColInput" width="57%" colspan="3">
                            <asp:TextBox ID="txtSendAddress" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            经营范围
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea name="txtSellArea" id="txtSellArea" rows="3" cols="80" style="width:95%"></textarea>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        财务信息
                                    </td>
                                    <td align="right">
                                        <div id='divFinance'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','divFinance')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_04" style="display: block">
                            <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            结算方式
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                                <select name="drpPayType" class="tdinput" width="119px" runat="server" id="drpPayType"> </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            币种
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="drpCurrencyType" class="tdinput" width="119px" runat="server" id="drpCurrencyType"> </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            开户行</td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtOpenBank" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="开户行" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            户名
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtAccountMan" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="户名" ></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            帐号
                        </td>
                        <td height="20" class="tdColInput" width="57%" colspan="3">
                            <asp:TextBox ID="txtAccountNum" runat="server" MaxLength="25" CssClass="tdinput" Width="95%"  SpecialWorkCheck="帐号" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
                
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        辅助信息
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
                    id="Tb_02" style="display: block">
                            <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            成立时间
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtSetupDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEquipCheckDate')})" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox> 
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            法人代表
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtArtiPerson" runat="server" MaxLength="10" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            一般纳税人
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="drpIsTax"  class="tdinput"  id="drpIsTax" >
                                        <option value="2" selected="selected">请选择</option>
                                        <option value="1">是</option>
                                        <option value="0">否</option></select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            税务登记号
                        </td>
                        <td class="tdColInput" width="23%">
                             <asp:TextBox ID="txtTaxCD" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            营业执照号
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtBusiNumber" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            热度
                        </td>
                        <td class="tdColInput" width="24%">
                            <select name="drpHotHow"  class="tdinput"  id="drpHotHow" >
                                        <option value="0" selected="selected">请选择</option>
                                        <option value="1">低热</option>
                                        <option value="2">中热</option><option value="3">高热</option></select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            价值评估
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                                <select name="drpMeritGrade"  class="tdinput"  id="drpMeritGrade" >
                                        <option value="0" selected="selected">请选择</option>
                                        <option value="1">高</option>
                                        <option value="2">中</option><option value="3">低</option></select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            员工总数
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtStaffCount" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="帐号" ></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单位性质
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="drpCompanyType"  class="tdinput"  id="drpCompanyType" >
                                        <option value="0" selected="selected">请选择</option>
                                        <option value="1">事业</option><option value="2">企业</option>
                                        <option value="3">社团</option><option value="4">自然人</option>
                                        <option value="5">其他</option></select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            年利润额(万元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtProfitY" runat="server" MaxLength="25" CssClass="tdinput" Width="95%"  onchange="if(this.value!=''){if(!Isfloat(this.value)){this.value='';popMsgObj.ShowMsg('金额输入有误,请重新填写');}else{this.value=parseFloat(this.value).toFixed(document.getElementById('HiddenPoint').value);}}"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            年销售额(万元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtSaleroomY" runat="server" MaxLength="25" CssClass="tdinput" Width="95%"  onchange="if(this.value!=''){if(!Isfloat(this.value)){this.value='';popMsgObj.ShowMsg('金额输入有误,请重新填写');}else{this.value=parseFloat(this.value).toFixed(document.getElementById('HiddenPoint').value)}}"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            注册资本(万元)
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtSetupMoney" runat="server" CssClass="tdinput" Width="95%"  onchange="if(this.value!=''){if(!Isfloat(this.value)){this.value='';popMsgObj.ShowMsg('金额输入有误,请重新填写');}else{this.value=parseFloat(this.value).toFixed(document.getElementById('HiddenPoint').value)}}"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            资产规模(万元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                             <asp:TextBox ID="txtCapitalScale" runat="server" MaxLength="25" CssClass="tdinput" Width="95%" onchange="if(this.value!=''){if(!Isfloat(this.value)){this.value='';popMsgObj.ShowMsg('金额输入有误,请重新填写');}else{this.value=parseFloat(this.value).toFixed(document.getElementById('HiddenPoint').value)}}"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            建档人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtCreatorReal" name="txtCreatorReal" class="tdinput"  
                                runat="server" readonly="true" disabled="disabled" />
                            <input type="hidden" id="HidCreator" name="HidCreator" class="tdinput" runat="server"/>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            建档日期
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txtCreateDate" name="txtCreateDate" class="tdinput" 
                                runat="server" readonly disabled="disabled" />
                        </td>
                    </tr>
                     <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            最后更新日期
                        </td>
                        <td class="tdColInput" width="23%">
                           <asp:TextBox ID="txtModifiedDate" runat="server"  CssClass="tdinput" 
                                readonly="true" Width="95%" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="txtModifiedDate2" name="txtModifiedDate2" class="tdinput" runat="server" readonly />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            最后更新用户
                        </td>
                        <td class="tdColInput" width="23%">
                           <input type="text" id="txtModifiedUserIDReal" name="txtModifiedUserIDReal" 
                                class="tdinput" disabled="disabled" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" class="tdinput" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID2" name="txtModifiedUserID2" class="tdinput" runat="server" readonly />
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            帐期天数
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="text" id="AllowDefaultDays" class="tdinput" />
                        </td>
                     </tr>
                     <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            注册地址
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <asp:TextBox ID="txtSetupAddress" MaxLength="100" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            备注
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea name="txtRemark" id="txtRemark" rows="3" cols="80" style="width:95%"></textarea>
                            <input type='hidden' id='hfPageAttachment' runat="server" />
                                <input name='usernametemp' type='hidden' id='usernametemp' runat="server" />
                                <input name='datetemp' type='hidden' id='datetemp' runat="server" />
                                <input id="txtAction" type="hidden" value="1" />
                                <input type="hidden" id="hidIsliebiao" name="hidIsliebiao" runat="server"/>
                                <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                <input type="hidden" id="hidModuleID" runat="server" />
                                <asp:DropDownList ID="drpApplyReason" runat="server" Width="0" Height="0"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
                
                <table width="99%" id="TableDTB" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        动态信息
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
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="TableDTN" style="display: block">
                   <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            已开票总金额(元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPTotalPrice" MaxLength="50" runat="server" 
                                CssClass="tdinput"  readonly="true"  Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            已付款总金额(元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPYAccounts" MaxLength="50" runat="server" 
                                CssClass="tdinput"  readonly="true"  Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            应付款总金额(元)
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtPNAccounts" MaxLength="50" runat="server" 
                                CssClass="tdinput"  readonly="true"  Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            订单数量合计
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtDCountTotal" MaxLength="50" runat="server" 
                                CssClass="tdinput"  readonly="true"  Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            <%--退货数量合计--%>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <%--<asp:TextBox ID="txtTCountTotal" MaxLength="50" runat="server" 
                                CssClass="tdinput"  readonly="true"  Width="95%" Enabled="False"></asp:TextBox>--%>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            <%--已退款总金额(元)--%>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <%--<asp:TextBox ID="txtTYAccounts" MaxLength="50" runat="server" 
                                CssClass="tdinput"  readonly="true"  Width="95%" Enabled="False"></asp:TextBox>--%>
                        </td>
                    </tr>
                </table>
                
                
               <%-- <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="right" valign="top">
                            <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
                        </td>
                        <td width="8">
                        </td>
                    </tr>
                    <tr>
                        <td height="25" valign="top">
                            &nbsp; <span class="Blue">采购到货通知单明细</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick3'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('Tb_04','searchClick3')" /></div>
                        </td>
                        <td width="10">
                        </td>
                    </tr>
                </table>--%>
            </td>
        </tr>
    </table>
    
<div id="userList" class="userListCss" style="overflow-x:hidden;overflow-y:scroll">
<iframe id="aaaa" style="position: absolute; z-index: -1; width:400px; height:100px;" frameborder="1">  </iframe>
<table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="20" bgcolor="#E6E6E6" class="Blue">
          <table width="100%" border="0" cellspacing="0" cellpadding="">
              <tr>
                <td width="65%">供应商分类&nbsp;&nbsp;&nbsp;<a href="#" onclick="clearInfo();">清空</a> </td>
                <td align="right">
              <img src="../../../Images/Pic/Close.gif" title="关闭" style="CURSOR: pointer"  onclick="document.all['userList'].style.display='none';"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<td>
              </tr>
          </table>
          </td>
        </tr>
        <tr><td bgcolor="#F4F0ED" height="200" valign="top">
            <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
    </asp:TreeView>
</td></tr>
     </table>
</div>

    </form>
</body>
</html>
