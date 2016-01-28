<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceSellAnnal_Info.aspx.cs" Inherits="Pages_Office_CustManager_ServiceSellAnnal_Info" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc2" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc1" %>

<%@ Register src="../../../UserControl/ProductInfoControl.ascx" tagname="ProductInfoControl" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>产品销售记录</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>    
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/ServiceSellAnnalInfo.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="isMoreUnitID" type="hidden" runat="server" /><!--是否启用多计量单位-->
<uc2:ProductInfoControl 
            ID="ProductInfoControl1" runat="server" />
            <uc2:Message ID="Message1" runat="server" />
<table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
      </td>
    <td rowspan="2" align="right" valign="top"><div id='searchClick'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('searchtable','searchClick')"/></div>&nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td  valign="top" class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
      
      </td>
  </tr>
  <tr>
    <td colspan="2"  >
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right" style="width:10%">客户名称</td>
            <td width="10%" bgcolor="#FFFFFF" style="width:23%">
                <uc1:CustNameSel ID="CustNameSel1" runat="server" />
              </td>            
            <td width="10%" bgcolor="#E7E7E7" align="right" style="width:10%">物品名称</td>
            <td width="10%" bgcolor="#FFFFFF" style="width:23%">
                <input type="text" id="txtProductName" class="tdinput" runat="server" readonly style="width:95%" onfocus="popTechObj.ShowList('','txtProductName','hfProductID')"  />
                <input id="hfProductID" type="hidden" />
              </td>
            <td width="10%" bgcolor="#E7E7E7"  align="right" style="width:10%">销售日期</td>            
            <td bgcolor="#FFFFFF" style="width: 25%" style="width:24%">
                    <input name="txtTalkBegin" id="txtTalkBegin" type="text" class="tdinput" size="12"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtTalkBegin')})" /> 
                    至<input name="txtTalkEnd" id="txtTalkEnd" type="text" class="tdinput" size="12"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtTalkEnd')})" /> </td>
            
          </tr>
          <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" runat="server" visible="false"  src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick="SearchSell(1);" /> </td>
          </tr>
        </table></td>
      </tr>
    </table>
    </td>
  </tr>
</table>
<table width="95%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">产品销售记录</td>
  </tr>
  
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListSell" bgcolor="#999999">
    <tbody>
      <tr>
      <th style="height:20px" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('custname','ocustnam');return false;">客户名称<span id="ocustnam" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('ProductName','oProductName');return false;">物品名称<span id="oProductName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('OrderNo','oOrderNo');return false;">订单编号<span id="oOrderNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('title','otitle');return false;">订单名称<span id="otitle" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('orderDate','oorderDate');return false;">下单日期<span id="oorderDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('EmployeeName','oEmployeeName');return false;">业务员<span id="oEmployeeName" class="orderTip"></span></div></th>
        <th id="BaseUnit" runat="server" visible="false" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('UnitName','oUnitName');return false;">基本单位<span id="oUnitName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('UsedUnitName','oUsedUnitName');return false;">单位<span id="oUsedUnitName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('UnitPrice','oUnitPrice');return false;">单价<span id="oUnitPrice" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('ProductCount','oProductCount');return false;">订购数量<span id="oProductCount" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('SendCount','oSendCount');return false;">已通知数量<span id="oSendCount" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('OutCount','oOutCount');return false;">已出库数量<span id="oOutCount" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('BackCount','oBackCount');return false;">已退货数量<span id="oBackCount" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('TotalPrice','oTotalPrice');return false;">总金额<span id="oTotalPrice" class="orderTip"></span></div></th>
        <%--<th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('YAccounts','oYAccounts');return false;">已付金额<span id="oYAccounts" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('NAccounts','oNAccounts');return false;">应付金额<span id="oNAccounts" class="orderTip"></span></div></th>--%>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('MaxCreditDate','oMaxCreditDate');return false;">帐期天数<span id="oMaxCreditDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('days','odays');return false;">延期天数<span id="odays" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecountSell"></div></td>
            <td height="28"  align="right"><div id="pageDataListSell_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpageSell">
              <input name="text" type="text" id="Text2" style="display:none" />
              <span id="pageDataList1_Total"></span>每页显示
              <input name="text" style="width:28px;" type="text" id="ShowPageCountSell"  maxlength="4"/>
              条  转到第
              <input name="text" type="text" style="width:28px;"  id="ToPageSell"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexSell($('#ShowPageCountSell').val(),$('#ToPageSell').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark"></a>
<span id="Forms" class="Spantype"></span>
<input id="hiddUserId" type="hidden" runat="server" />
</form>
</body>
</html>
