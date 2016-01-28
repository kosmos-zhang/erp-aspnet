<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactDefer_Info.aspx.cs" Inherits="Pages_Office_CustManager_ContactDeferInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户联络延期告警</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>    
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/ContactDeferInfo.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

<table width="95%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
   <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
      </td>
    <td rowspan="2" align="right" valign="top">
    &nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户联络延期告警</td>
  </tr>
  <tr>
    <td height="35" colspan="2" valign="top">
      </td>
  </tr>
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
    <tbody>
      <tr>
      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('custno','ocustno');return false;">客户编号<span id="ocustno" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('custname','ocustname');return false;">客户名称<span id="ocustname" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('custtype','ocusttype');return false;">客户类型<span id="ocusttype" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('linkcycle','olinkcycle');return false;">联络期限(天)<span id="olinkcycle" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick"  onclick="OrderBy('days','odays');return false;">延期天数(天)<span id="odays" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('linkdate','olinkdate');return false;">最近联络时间<span id="olinkdate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('EmployeeName','oEmployeeName');return false;">最近联络人<span id="oEmployeeName" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecount"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage">
              <input name="text" type="text" id="Text2" style="display:none" />
              <span id="pageDataList1_Total"></span>每页显示
              <input name="text" type="text" id="ShowPageCount" maxlength="4"/>
              条  转到第
              <input name="text" type="text" id="ToPage"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark"></a>
<span id="Forms" class="Spantype"></span>
</form>
</body>
</html>
