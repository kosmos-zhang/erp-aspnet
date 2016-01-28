<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustCall_Info.aspx.cs" Inherits="Pages_Office_CustManager_CustCall_Info" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>来电记录列表页面</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
     <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
      <script src="../../../js/common/Common.js" type="text/javascript"></script>    
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />   
    <script src="../../../js/common/page.js" type="text/javascript"></script>    
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/CustCall_Info.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

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
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">客户名称</td>
            <td width="20%" bgcolor="#FFFFFF">
                <uc1:CustNameSel ID="CustNameSel1" runat="server" />
              </td>
            <td width="10%" bgcolor="#E7E7E7" align="right">来电号码</td>
            <td bgcolor="#FFFFFF" style="width: 15%"><input  runat="server" id="txtTel" type="text" class="tdinput" style="width:95%" maxlength="20" /></td>
            <td width="10%" bgcolor="#E7E7E7"  align="right">来电时间</td>            
            <td bgcolor="#FFFFFF" style="width: 25%">
                    <input name="txtLoveBegin" id="txtDateBegin" runat="server" type="text" class="tdinput" size="12"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtDateBegin')})" /> 
                    至<input name="txtLoveEnd" id="txtDateEnd" runat="server" type="text" class="tdinput" size="12"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtDateEnd')})" /> </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
                <table class="style1">
                    <tr>
                        <td align="right" style="width:55%;">
            <img id="btn_search" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:hand;'  visible="false" runat="server"  onclick='SearchCall(1)' /></td>
                        <td align="right">
                            <a href="../../../Setup/RegOcx.exe" target="_blank">请点击这里下载客户来电显示插件</a></td>
                    </tr>
                </table>
            </td>
           
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
    <td height="30" colspan="2" align="center" valign="top" class="Title">来电记录列表</td>
  </tr>
  <tr>
    <td height="35" colspan="2" valign="top"><table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="28" bgcolor="#FFFFFF">&nbsp;
          <asp:ImageButton ID="btnImport" runat="server" ImageUrl="~/Images/Button/Main_btn_out.jpg" onclick="btnImport_Click" /></td>
        </tr>
      </table>
      </td>
  </tr>
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustName','oCustName');return false;">客户名称<span id="oCustName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Tel','oTel');return false;">来电号码<span id="oTel" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CallTime','oCallTime');return false;">来电时间<span id="oCallTime" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Title','oTitle');return false;">来电标题<span id="oTitle" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Callor','oCallor');return false;">来电人<span id="oCallor" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('EmployeeName','oEmployeeName');return false;">记录人<span id="oEmployeeName" class="orderTip"></span></div></th>
        
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
              <input name="text" type="text" id="ShowPageCount"  maxlength="4"/>
              条  转到第
              <input name="text" type="text" id="ToPage"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark"></a>
<span id="Forms" class="Spantype"></span>
<input id="hiddExpOrder" type="hidden" runat="server" />
<input id="hiddCustID" type="hidden" runat="server" />
</form>
</body>
</html>
