<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchase_Add.aspx.cs" Inherits="XBase.Pages.Office.PurchaseManager.Purchase_Add" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"TagPrefix="uc2" %>
<%@ Register src="../../../UserControl/ProductInfoControl.ascx" tagname="ProductInfoControl" tagprefix="uc3" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>添加采购申请</title>
<link rel="stylesheet" type="text/css" href="../../../css/default.css" />
<link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />
<script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
<script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
<script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
<script src="../../../js/common/Common.js" type="text/javascript"></script>
<script src="../../../js/office/PurchaseManager/PurchaseApplyAdd.js" type="text/javascript"></script>
<script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
<script src="../../../js/common/Page.js" type="text/javascript"></script>
<style type="text/css">
  <%--  
    .proID
    {
        width: 75px;
    }
    .unitName
    {
    	width: 30px;
    }
    .remark
    {
    	width:150px;
    }
    .pro2
    {
    	width:90px;
    }--%>
</style>
</head>
<body>
<form id="PurchaseApplyAdd" runat="server">
<span id="Forms" class="Spantype"></span>
  <table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr>
      <td valign="top" colspan="2">
        <img src="../../../images/Main/Line.jpg" width="122" height="7" />
      </td>
    </tr>
    <tr>
      <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">添加采购申请</div></td>
    </tr>
    <tr>
      <td height="40" valign="top" colspan="2">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
          <tr>
            <td height="30" class="tdColInput">
              <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="Add_PurchaseApply" runat="server" style="cursor:hand" height="25" onclick="NewEmployeeInfo();"/>
              <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server"  alt="保存" id="save_PurchaseApply" style="cursor:hand" height="25" onclick="InsertPurchaseApplyData();"/>
              <img src="../../../Images/Button/Bottom_btn_Yd.jpg" alt="明细汇总" id="DspDtl_PurchaseApply"onclick="DspDtl()" style="cursor: hand;" border="0" />
              <img src="../../../images/Button/Bottom_btn_Confirm.jpg" alt="确认" id="Img2" onclick="Confirm_PurchaseApply()"style="cursor: hand" height="25" border="0" />
              <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" id="btnPrint" style="cursor:hand" height="25" />
            </td>
          </tr>
       </table>
       <div>
         <table width="100%" border="0" cellspacing="0" cellpadding="3">
           <tr><td>基础信息</td>
             <td align="right">
               <div id='searchClick'><img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
             </td>
           </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"id="Tb_01" style="display: block">
          <tr>
            <td align="right" bgcolor="#E6E6E6" >申请编号<span class="redbold">*</span></td>
            <td bgcolor="#FFFFFF"><uc2:CodingRuleControl ID="CodingRuleControl1" runat="server" /></td>
            <td align="right" bgcolor="#E6E6E6" >申请单主题</td>
            <td bgcolor="#FFFFFF" ><input name="txtTitle" id="txtTitle" type="text" class="tdinput" size="15" /></td>
            <td align="right" bgcolor="#E6E6E6">采购类别</td>
            <td bgcolor="#FFFFFF">
               <select name="ddlTypeID" class="tdinput" id="ddlTypeID">
                 <option value="1" selected="selected">1</option>
                 <option value="2">2</option>
                 <option value="3">3</option>
               </select>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#E6E6E6" >申请人</td>
            <td height="20" align="left" bgcolor="#FFFFFF"><input name="txtApplyUserID" id="txtApplyUserID" onfocus="alert_div();" class="tdinput"ype="text" /><uc3:ProductInfoControl 
                    ID="ProductInfoControl1" runat="server" /></td>
            <td align="right" bgcolor="#E6E6E6" >申请部门</td>
            <td height="20" align="left" bgcolor="#FFFFFF" ><input name="txtApplyDeptID" id="txtApplyDeptID" class="tdinput" type="text" size="15"readonly="readonly" style="background-color: #CCCCCC" />
              <asp:HiddenField ID="txtHiddenFieldID" runat="server" /></td>
            <td align="right" bgcolor="#E6E6E6">源单类型</td>
            <td align="left" bgcolor="#FFFFFF">&nbsp;
              <select name="ddlFromType" class="tdinput" width="119px" id="ddlFromType">
                <option value="0" selected="selected">无来源</option>
                <option value="1">销售订单</option>
                <option value="2">物料资源计划</option>
              </select>
            </td>
          </tr>
          <tr>
            <td height="20" align="right" bgcolor="#E6E6E6">申请日期</td>
            <td height="20" align="left"  bgcolor="#FFFFFF">
              <input name="txtApplyDate" id="txtApplyDate" class="tdinput" type="text" size="15"onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})" />
            </td>
            <td height="20" align="right" bgcolor="#E6E6E6">到货地址</td>
            <td height="20" bgcolor="#FFFFFF" colspan="5"><input name="txtAddress" id="txtAddress" class="tdinput" type="text" size="15" style="width: 100%" /></td>
          </tr>
         </table>
         <table width="100%" border="0" cellspacing="0" cellpadding="3">
           <tr>
             <td>备注信息</td>
             <td align="right"><div id='searchClick2'><img src="../../../images/Main/Open.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick2')" /></div></td>
           </tr>
         </table>
         <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"id="Tb_03" style="display: block">
           <tr>
             <td align="right" bgcolor="#E6E6E6" >制单人</td>
             <td bgcolor="#FFFFFF">
               <input name="txtCreator" id="txtCreator" class="tdinput" type="text" size="15"  runat="server" readonly="readonly"/>
             </td>
             <td align="right" bgcolor="#E6E6E6" >制单日期</td>
             <td align="left" bgcolor="#FFFFFF">
               <input name="txtCreateDate" id="txtCreateDate" class="tdinput" type="text" size="15" runat="server" readonly="readonly" />
             </td>
             <td align="right" bgcolor="#E6E6E6">单据状态</td>
             <td align="left" bgcolor="#FFFFFF">&nbsp;
               <select name="ddlBillStatus" class="tdinput" width="119px" id="ddlBillStatus"disabled="disabled">
                 <option value="1" selected="selected">制单</option>
                 <option value="2">执行</option>
                 <option value="3">变更</option>
                 <option value="4">手工结单</option>
                 <option value="5">自动结单</option>
               </select>
             </td>
           </tr>
           <tr>
             <td height="20" align="right" bgcolor="#E6E6E6" >确认人</td>
             <td height="20" bgcolor="#FFFFFF" ><input name="txtConfirmor" id="txtConfirmor" class="tdinput" type="text" size="15" readonly="readonly"  /></td>
             <td height="20" align="right" bgcolor="#E6E6E6" >确认日期</td>
             <td height="20" align="left" bgcolor="#FFFFFF" ><input id="txtConfirmDate" class="tdinput" type="text" size="15"readonly="readonly" /></td>
             <td height="20" align="right" bgcolor="#E6E6E6" >最后更新人</td>
             <td height="20" bgcolor="#FFFFFF"><input name="txtModifiedUserID" id="txtModifiedUserID" class="tdinput" type="text"  size="15" runat="server" readonly="readonly"   /></td>
           </tr>
           <tr>
             <td align="right" bgcolor="#E6E6E6" >结单人</td>
             <td bgcolor="#FFFFFF" ><input name="txtCloser" id="txtCloser" class="tdinput" type="text" size="15" readonly="readonly" /></td>
             <td align="right" bgcolor="#E6E6E6">结单日期</td>
             <td align="left" bgcolor="#FFFFFF"><input name="txtCloseDate" id="txtCloseDate" class="tdinput" type="text" size="15"readonly="readonly" /></td>
             <td align="right" bgcolor="#E6E6E6" >最后更新日期</td>
             <td align="left" bgcolor="#FFFFFF" ><input name="txtModifiedDate" id="txtModifiedDate" class="tdinput" type="text" size="15" runat="server" readonly="readonly" /></td>
           </tr>
           <tr>
             <td height="20" class="tdColTitle">备注</td>
             <td height="20" colspan="5" bgcolor="#FFFFFF"><textarea name="txtRemark" id="txtRemark" class="tdinput" rows="5"></textarea>
                <input id="txtCpntrolID" type="text" type="hidden" value="User|txtApplyUserID,Dept|txtApplyDeptID"style="display: none" />
             </td>
           </tr>
         </table>
         <table><tr><td height="2"></td></tr></table>
         <table width="100%" border="0" cellspacing="0" cellpadding="0">
           <tr>
             <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
             <td align="right" valign="top"><img src="../../../images/Main/LineR.jpg" width="122" height="7" /></td>
           </tr>
           <tr>
             <td height="25" valign="top"><span class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />采购申请明细来源</span></td>
             <td align="right" valign="top"><div id='searchClick3'><img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('dg_Log','searchClick3')" /></div></td>
           </tr>
         </table>
         <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
           <tr>
             <td height="28" bgcolor="#FFFFFF">
               <img src="../../../images/Button/Show_add.jpg" id="AddDtlS_PurchaseApply" width="34"height="24" style="cursor: hand" onclick="AddSignRow();" />
               <img src="../../../images/Button/Show_edit.jpg" id="MdfyDtlS_PurchaseApply" width="34"height="24" style="cursor: hand" onclick="getDtlSValue();" />
               <img src="../../../images/Button/Show_del.jpg" id="DltDtlS_PurchaseApply" width="34"height="24" style="cursor: hand" onclick="delrow();" />
                <asp:HiddenField ID="hiddenAction" runat="server" />
                <asp:HiddenField ID="DtlCount" runat="server" />
             </td>
           </tr>
         </table>
         <table width="99%" border="0" id="dg_Log" style="behavior: url(../../../draggrid.htc)"align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
           <tr>
            <td height="20" align="center" bgcolor="#E6E6E6"class="Blue" >序号</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">选择<input type="checkbox" visible="false" name="checkall" id="checkall" onclick="selectall()" value="checkbox" /></td>
            <td witdh="10%" align="center" bgcolor="#E6E6E6" class="Blue">物品编号</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">物品名称</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">单位</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">单价</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">需求数量</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">申请数量</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">金额</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">需求日期</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">请交货日期</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">来源部门</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">来源单据</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">来源单据行号</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">供应商</td>
            <td align="center" bgcolor="#E6E6E6" class="Blue">备注</td>
           </tr>
           <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
          </table>
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
              <td align="right" valign="top"><img src="../../../images/Main/LineR.jpg" width="122" height="7" /></td>
            </tr>
            <tr>
              <td height="25" valign="top"><span class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />采购申请明细</span></td>
              <td align="right" valign="top"><div id='Div1'><img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('dg_Log2','searchClick3')" /></div></td>
            </tr>
          </table>
          <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"></table>
          <table width="99%" border="0" id="dg_Log2" style="behavior": url(../../../draggrid.htc)" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
            <tr>
                <td height="20" align="center" class="Blue"bgcolor="#E6E6E6" style="width:30px">序号</td>
                <td align="center" bgcolor="#E6E6E6" class="Blue" style="width:80">物品编号</td>
                <td align="center" bgcolor="#E6E6E6" class="Blue" style="width:80">物品名称</td>
                <td align="center" bgcolor="#E6E6E6" class="Blue" style="width: 40px">单位</td>
                <td align="center" bgcolor="#E6E6E6" class="Blue" style="width:80">申请数量</td>
                <td align="center" bgcolor="#E6E6E6" class="Blue" style="width:40">需求日期</td>
                <td align="center" bgcolor="#E6E6E6" class="Blue" style="width:80">申请原因</td>
                <td align="center" bgcolor="#E6E6E6" class="Blue" style="width:80">备注</td>
            </tr>
            </table>
       </div>
     </td>
    </tr>
  </table>
  </form>
</body>

</html>