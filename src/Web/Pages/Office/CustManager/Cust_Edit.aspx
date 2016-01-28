<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cust_Edit.aspx.cs" Inherits="Pages_Office_CustManager_Cust_Edit" %>

<%@ Register src="../../../UserControl/CustClassDrpControl.ascx" tagname="CustClassDrpControl" tagprefix="uc2" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc1" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改客户信息</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>   
    <script src="../../../js/office/CustManager/CustEdit.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
     <script src="../../../js/common/Check.js" type="text/javascript"></script>
     <script src="../../../js/common/TreeView.js" language="javascript"  type="text/javascript" ></script>
     <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
   
</head>
<body onload="alerCustType()">
    <form id="form1" runat="server">
                        <input type="hidden" runat="server" id="txtRecorder" />
                        <input type="hidden" runat="server" id="txtChairman" />
                        <input type="hidden" runat="server" id="txtSender" />
                        <uc3:Message ID="Message1" runat="server" />
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
                            
                            客户档案                            
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">                           
                            <table width="100%">
                                <tr>
                                    <td>                                      
        <img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save" visible="false" runat="server" style="cursor:hand" onclick="SaveCustData();" />
       <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand;" onclick="Back();" />
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer" onclick="PagePrint();"
                                            title="打印" />
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
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>基本信息 </td>
                  <td align="right"><div id='searchClick'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_01','searchClick')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01" >
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户编号<span class="redbold">*</span></td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                            <input name="txtCustNo" id="txtCustNo"  type="text" class="tdinput"  style="width:95%"  disabled="disabled" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户名称<span class="redbold">*</span></td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                     <input name="txtCustName" class="tdinput" id="txtCustName" SpecialWorkCheck="客户名称" type="text" style="width:95%" maxlength="50" onblur="LoadPYShort();"/></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户简称</td>
                <td height="22" bgcolor="#FFFFFF" style="width:24%"><input name="txtCustNam" id="txtCustNam" type="text" SpecialWorkCheck="客户简称" class="tdinput" style="width:95%" maxlength="25" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户管理分类</td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                     <select width="20px" id="SeleCustTypeManage" name="D1">
                                    <option value="0">--请选择--</option>
                                  <option value="1">VIP客户</option>
                                  <option value="2">主要客户</option>
                                  <option value="3">普通客户</option>
                                   <option value="4">临时客户</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户营销分类</td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                    <select width="20px" id="SeleCustTypeSell" name="D2">
                                <option value="0">--请选择--</option>
                                  <option value="1">经济型客户</option>
                                  <option value="2">个性化客户</option>
                                  <option value="3">方便型客户</option>
                                  <option value="4">道德型客户</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">拼音缩写</td>
                <td height="22" bgcolor="#FFFFFF" style="width:24%"><input name="txtCustShort" SpecialWorkCheck="拼音缩写" id="txtCustShort" type="text" class="tdinput"style="width:95%" maxlength="25" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户时间分类</td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                            <select width="20px" id="SeleCustTypeTime" name="D3">
                                <option value="0">--请选择--</option>
                                  <option value="1">老客户</option>
                                  <option value="2">新客户</option>
                                  <option value="3">潜在客户</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户细分</td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                    <uc2:CustClassDrpControl ID="CustClassDrpControl1" runat="server" />
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户类别</td>
                <td height="22" bgcolor="#FFFFFF" style="width:24%"><asp:DropDownList ID="ddlCustType" runat="server">
                    </asp:DropDownList>
                            </td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">客户优质级别</td>
                <td height="22" bgcolor="#FFFFFF">                    
                    <asp:DropDownList ID="ddlCreditGrade" runat="server"></asp:DropDownList>
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6">建档人</td>
                <td height="22" bgcolor="#FFFFFF">
                    <input name="txtCreator" id="txtCreator" type="text" class="tdinput" style="width:95%" runat="server" disabled="disabled" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">建档日期<span class="redbold">*</span></td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtCreatedDate" id="txtCreatedDate" type="text" class="tdinput" disabled="disabled"/></td>
              </tr>
              
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">客户简介</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">                    
                    <textarea name="txtCustNote" id="txtCustNote" rows="3" cols="80" style="width:90%"></textarea>
                            </td>
              </tr>
              </table>
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>业务信息 </td>
                  <td align="right"><div id='searchClick2'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_02','searchClick2')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
               <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_02" ><!--style="display:block"-->
               <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">国家地区</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <asp:DropDownList ID="ddlCountry" runat="server">
                    </asp:DropDownList>
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">区域</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList>
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">省</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                    <input name="txtProvince" id="txtProvince" type="text" class="tdinput"style="width:95%" 
                        maxlength="25" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">市(县)</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtCity" id="txtCity" type="text" class="tdinput"style="width:95%" maxlength="25" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">业务类型</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <select  name="seleBusiType" width="20px" id="seleBusiType">
                                  <option value="0">--请选择--</option>
                                  <option value="1">普通销售</option>
                                  <option value="2">委托代销</option>
                                  <option value="3">直运</option>
                                  <option value="4">零售</option>
                                  <option value="5">销售调拨</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6">分管业务员<span class="redbold">*</span></td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="UserLinker" id="UserLinker" type="text"  runat="server" readonly
                        class="tdinput" style="width:95%" maxlength="50"  onclick="alertdiv('UserLinker,txtJoinUser');" /><input type="hidden" runat="server" id="txtJoinUser" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">联系人</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtContactName" id="txtContactName" type="text" class="tdinput"style="width:95%" 
                        maxlength="25" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">电话</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtTel" id="txtTel" type="text" class="tdinput"style="width:95%" maxlength="25" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">手机</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtMobile" id="txtMobile" type="text" class="tdinput"style="width:95%" maxlength="25" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">传真</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtFax" id="txtFax" type="text" class="tdinput"style="width:95%" maxlength="25" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">在线咨询</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtOnLine" id="txtOnLine" type="text" class="tdinput"style="width:95%" maxlength="50" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">公司网址</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtWebSite" id="txtWebSite" type="text" 
                        class="tdinput" style="width:95%"
                        maxlength="50" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">邮编</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtPost" id="txtPost" type="text" class="tdinput" style="width:95%" 
                        maxlength="10" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">电子邮件</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtemail" id="txtemail" type="text" class="tdinput" style="width:95%" maxlength="25" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">首次交易日期</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input id="txtFirstBuyDate" type="text" class="tdinput" 
                        readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtFirstBuyDate')})" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">运送方式</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <asp:DropDownList ID="ddlCarryType" runat="server">
                    </asp:DropDownList>
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6">交货方式</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <asp:DropDownList ID="ddlTakeType" runat="server">
                    </asp:DropDownList>
                                                    </td>
                <td height="22" align="right" bgcolor="#E6E6E6">联络期限(天)</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <asp:DropDownList ID="ddlLinkCycle" runat="server">
                    </asp:DropDownList>
                            </td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">收货地址</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="3">
                    <input name="txtReceiveAddress" id="txtReceiveAddress" type="text" class="tdinput" style="width:95%" 
                        maxlength="50" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">&nbsp;</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    &nbsp;</td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">经营范围</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtSellArea" id="txtSellArea" rows="3" cols="80" style="width:90%"></textarea></td>
              </tr>
              </table>
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>财务信息 </td>
                  <td align="right"><div id='searchClick4'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_04','searchClick4')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
          <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_04" ><!---->
               <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">允许延期付款<span class="redbold">*</span></td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <select  name="seleCreditManage" width="20px" id="seleCreditManage" onchange="DivMaxcShow();">
                                  <option value="1">否</option>
                                  <option value="2">是</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">信用额度(元)</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input name="txtMaxCredit" id="txtMaxCredit" type="text" class="tdinput"
                        onchange="Number_round(this,2)" maxlength="12" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">帐期天数(天)<span style="display:none;" id="divMaxC" class="redbold">*</span></td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                    <input name="MaxCreditDate" id="txtMaxCreditDate" type="text" class="tdinput"
                        style="width:95%" maxlength="12" /></td>
              </tr>
               <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">结算方式</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <asp:DropDownList ID="ddlPayType" runat="server" >
                    </asp:DropDownList>
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">结算币种</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <asp:DropDownList ID="ddlCurrencyType" runat="server">
                    </asp:DropDownList>
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">发票类型</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                <select  name="seleBillType" width="20px" id="seleBillType">
                                  <option value="0">--请选择--</option>
                                  <option value="1">增值税发票</option>
                                  <option value="2">普通地税</option>
                                  <option value="3">普通国税</option>
                                  <option value="4">收据</option>
                                </select></td>
              </tr>
               <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">支付方式</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <asp:DropDownList ID="ddlMoneyType" runat="server" style="margin-bottom: 0px">
                    </asp:DropDownList></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">开户行</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input name="txtOpenBank" id="txtOpenBank" type="text" class="tdinput" style="width:95%" 
                        maxlength="50" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">户名</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                    <input name="txtAccountMan" id="txtAccountMan" type="text" class="tdinput" style="width:95%" 
                        maxlength="50" /></td>
              </tr>
               <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">账号</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="3">
                    <input id="txtAccountNum" class="tdinput" maxlength="25" 
                        name="txtAccountNum" style="width:95%" type="text" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">&nbsp;</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                    &nbsp;</td>
              </tr>
              </table>
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>辅助信息 </td>
                  <td align="right"><div id='searchClick3'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_03','searchClick3')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_03" ><!---->
              <tr>
                <td height="22" bgcolor="#E6E6E6" colspan="6"> 输入提示：以下五个红色字体标记的客户属性请尽量填入真实数据，将用于决策模式下的客户分类建模。</td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" 
                      style="width:10%; color: #FF0000;">单位性质</td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                                 <select  name="txtCompanyType" width="20px" id="txtCompanyType">    
                                  <option value="0">--请选择--</option>                           
                                 <option value="1">事业</option>
                                  <option value="2">企业</option>
                                  <option value="3">社团</option>
                                  <option value="4">自然人</option>
                                  <option value="5">其他</option>  
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%;color: #FF0000;">资产规模(万元)</td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">                                
                    <input name="txtCapitalScale" id="txtCapitalScale" type="text" class="tdinput"
                        onchange="Number_round(this,2)" maxlength="12" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%;color: #FF0000;">成立时间</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                    <input name="txtSetupDate" id="txtSetupDate" type="text" class="tdinput"style="width:95%" 
                        readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtSetupDate')})" 
                        maxlength="10" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="color: #FF0000;">注册资本(万元)</td>
                <td height="22" bgcolor="#FFFFFF">
                    
                    <input name="txtSetupMoney" id="txtSetupMoney" type="text" class="tdinput" onchange="Number_round(this,2)"
                        maxlength="12" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="color: #FF0000;">员工总数(个)</td>
                <td height="22" bgcolor="#FFFFFF">                                
                    <input id="txtStaffCount" type="text" class="tdinput" maxlength="9" /></td>
                <td height="22"  bgcolor="#E6E6E6" colspan="2"></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">法人代表</td>
                <td height="22" bgcolor="#FFFFFF">
                    
                    <input name="txtArtiPerson" id="txtArtiPerson" type="text" class="tdinput"style="width:95%" 
                        maxlength="10" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">行业</td>
                <td height="22" bgcolor="#FFFFFF">                                
                    <input name="txtTrade" id="txtTrade" type="text" class="tdinput"style="width:95%" maxlength="25" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">营业执照号</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtBusiNumber" id="txtBusiNumber" type="text" class="tdinput" maxlength="25" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6">注册地址 </td>
                <td bgcolor="#FFFFFF" colspan="3">
                    <input name="txtSetupAddress" id="txtSetupAddress" type="text" class="tdinput" style="width:95%" maxlength="50" /></td>
                <td align="right" bgcolor="#E6E6E6">税务登记号</td>
                <td align="left" bgcolor="#FFFFFF">
                    <input name="txtTaxCD" id="txtTaxCD" type="text" class="tdinput"style="width:95%" maxlength="25" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">是否为一般纳税人</td>
                <td height="22" bgcolor="#FFFFFF">
                    <select  name="seleIsTax" width="20px" id="seleIsTax">
                                <option value="0">--请选择--</option>
                                  <option value="1">是</option>
                                  <option value="2">否</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6">客户来源</td>
                <td height="22" bgcolor="#FFFFFF">                                
                    <input name="txtSource" id="txtSource" type="text" class="tdinput"style="width:95%" maxlength="25"  /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">年销售额(万元)</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtSaleroomY" id="txtSaleroomY" type="text" class="tdinput"
                        onchange="Number_round(this,2)" maxlength="12" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">年利润额(万元)</td>
                <td height="22" bgcolor="#FFFFFF">
                    <input name="txtProfitY" id="txtProfitY" value="0.00" type="text" class="tdinput" onchange="Number_round(this,2)"
                        maxlength="12" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">销售模式</td>
                <td height="22" bgcolor="#FFFFFF">                                
                    <input name="txtSellMode" id="txtSellMode" type="text" class="tdinput" style="width:95%;" maxlength="25"  /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">上级客户</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <uc1:CustNameSel ID="CustNameSel1" runat="server" />
                            </td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">价值评估</td>
                <td height="22" bgcolor="#FFFFFF">
                    <select  name="seleMeritGrade" 
                        width="20px" id="seleMeritGrade">
                        <option value="0">--请选择--</option>
                                  <option value="1">高</option>
                                  <option value="2">中</option>
                                  <option value="3">低</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6">阶段</td>
                <td height="22" bgcolor="#FFFFFF">                                
                    <input name="txtPhase" id="txtPhase" type="text" class="tdinput"style="width:95%" maxlength="25" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">热点客户</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <select  name="seleHotIs" 
                        width="20px" id="seleHotIs">
                                <option value="0">--请选择--</option>
                                  <option value="1">是</option>
                                  <option value="2">否</option>
                                </select></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">热度</td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                    <select  name="seleHotHow" 
                        width="20px" id="seleHotHow">
                                <option value="0">--请选择--</option>
                                  <option value="1">低热</option>
                                  <option value="2">中热</option>
                                  <option value="3">高热</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">关系等级</td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">                                
                    <select  name="seleRelaGrade" width="20px" id="seleRelaGrade">
                        <option value="0">--请选择--</option>
                                  <option value="1">密切</option>
                                  <option value="2">较好</option>
                                  <option value="3">一般</option>
                                  <option value="4">较差</option>
                                </select></td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">启用状态<span class="redbold">*</span></td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                    <select  name="seleUsedStatus" width="20px" id="seleUsedStatus">
                                  <option value="1">启用</option>
                                  <option value="0">停用</option>
                                </select></td>
              </tr>             
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">关系描述</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtRelation" id="txtRelation" rows="3" cols="80" style="width:95%"></textarea></td>
              </tr>
              
              <tr>
                <td align="right" bgcolor="#E6E6E6">备注</td>
                <td align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtRemark" id="txtRemark" rows="3" cols="80" style="width:95%"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">最后更新用户</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtModifiedUser" id="txtModifiedUser" type="text" class="tdinput" runat="server" disabled="disabled" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">最后更新日期</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <input name="txtModifiedDate" id="txtModifiedDate" type="text" class="tdinput"  runat="server" disabled="disabled" /></td>
                <td height="22" align="right" bgcolor="#E6E6E6">&nbsp;</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    &nbsp;</td>
              </tr>
            </table>
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        经营信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonSell'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tbSell','divButtonSell')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="tbSell">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            经营理念
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="90%">                      
                            <textarea id="CompanyValues" cols="20" class="tdinput" style="width: 99%; height:40px"  rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            企业口号
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="23%">                           
                             <textarea id="CatchWord" class="tdinput" cols="20" style="width: 99%; height:40px" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            企业文化概述
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="24%">
                        
                              <textarea id="ManageValues" class="tdinput" cols="20" style="width: 99%; height:40px" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            发展潜力
                        </td>
                        <td colspan="5" height="20" class="tdColInput" width="23%">
                          
                             <textarea id="Potential" class="tdinput" cols="20" style="width: 99%; height:40px" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            存在问题
                        </td>
                        <td colspan="5" height="20" class="tdColInput" width="23%">
                         
                              <textarea id="Problem" class="tdinput" cols="20" style="width: 99%; height:40px" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            市场优劣势
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="24%">
                           
                             <textarea id="Advantages" class="tdinput" cols="20" style="width: 99%; height:40px" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            行业地位
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="23%">
                         
                             <textarea id="TradePosition" class="tdinput" cols="20" style="width: 99%; height:40px" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            竞争对手
                        </td>
                        <td colspan="5" height="20" class="tdColInput" width="23%">
                          
                             <textarea id="Competition" class="tdinput" cols="20" style="width: 99%; height:40px" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            合作伙伴
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="24%">
                          
                              <textarea id="Collaborator" class="tdinput" cols="20" style="width: 99%; height:40px" rows="2"></textarea>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        合作策略
                                    </td>
                                    <td align="right">
                                        <div id='mydivToConn'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('mytbCon111','mydivToConn')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="mytbCon111">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            发展计划
                        </td>
                        <td colspan="5"   class="tdColInput">
                            <textarea id="ManagePlan" maxlength="500"  cols="20" style="width:99%; height:30px" class="tdinput" rows="2"></textarea>
                        </td>

                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            合作方法
                        </td>
                        <td colspan="5"   class="tdColInput">
                            <textarea id="Collaborate" maxlength="500"   cols="20" style="width:99%; height:30px" class="tdinput" rows="2"></textarea>
                        </td>

                    </tr>
                    
                </table>
            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>权限信息 </td>
                  <td align="right"><div id='search6'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_06','search6')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
          <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_06" ><!---->
               <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">可查看该客户档案的人员</td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                    <textarea  id="txtCanUserName" rows="3" readonly cols="80" style="width:99%; height:40px" class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                    <input type="hidden" id="txtCanUserID" /></td>
              </tr>
              </table>
            <div id="ExtDiv" style="display: none">
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            扩展信息
                                        </td>
                                        <td align="right">
                                            <div id='div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('ExtTab','div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="ExtTab">
                        <tbody>
                        </tbody>
                    </table>
                </div>
          <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
          <input type="hidden" id="hiddKey" />
          <input id="hCondition" type="hidden" />
          <br />
              
           </td>
        </tr>
    </table> <!--  -->  
    
    </form>
</body>
</html>
