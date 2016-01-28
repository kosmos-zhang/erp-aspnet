<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustContact_Info.aspx.cs" Inherits="Pages_Office_CustManager_CustContact_Info" %>

<%@ Register src="../../../UserControl/EmployeeSel.ascx" tagname="EmployeeSel" tagprefix="uc1" %>

<%@ Register src="../../../UserControl/CustClassDrpControl.ascx" tagname="CustClassDrpControl" tagprefix="uc2" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户联络列表</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>    
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/CustContactInfo.js" type="text/javascript"></script> 
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>   
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
            <td width="23%" bgcolor="#FFFFFF">
                <uc4:CustNameSel ID="CustNameSel1" runat="server" />
              </td>
            
            <td width="10%" bgcolor="#E7E7E7" align="right">我方联络人</td>
            <td width="23%" bgcolor="#FFFFFF">
                <input id="UserCustLinkMan" type="text" class="tdinput" readonly onclick="alertdiv('UserCustLinkMan,txtCustLinkMan');"  />  
                 <input type="hidden" id="txtCustLinkMan" runat="server" />              
                </td>
            <td width="10%" bgcolor="#E7E7E7"  align="right">联络时间</td>            
            <td bgcolor="#FFFFFF" style="width: 24%">
                    <input name="txtLinkDateBegin" id="txtLinkDateBegin" runat="server" type="text" class="tdinput" size="10"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtLinkDate')})" /> 
                    至<input name="txtLinkDateEnd" id="txtLinkDateEnd" runat="server" type="text" class="tdinput" size="10"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtLinkDate')})" /> </td>
            
          </tr>
          <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"  visible="false" runat="server"  style='cursor:hand;' onclick='SearchContactData(1)' /> </td>
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
    <td rowspan="2" align="right" valign="top">&nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户联络列表</td>
  </tr>
  <tr>
    <td height="35" colspan="2" valign="top"><table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="28" bgcolor="#FFFFFF"><img id="btn_create"  src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" runat="server" style='cursor:hand;' onclick="CreateLink()" />
          <img id="btn_del"  src="../../../Images/Button/Main_btn_delete.jpg" alt="删除" style='cursor:hand;' onclick="DelContactInfo()" visible="false" runat="server" />
          <asp:ImageButton ID="btnImport" runat="server" ImageUrl="~/Images/Button/Main_btn_out.jpg" onclick="btnImport_Click" /> </td>
        </tr>
      </table>
      </td>
  </tr>
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" visible="false" name="checkall" id="checkall" onclick="AllSelect('checkall','Checkbox1');" value="checkbox" /></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('ContactNo','oContactNo');return false;">联络单编号<span id="oContactNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Title','oTitle');return false;">联络主题<span id="Span1" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustNam','oCustNam');return false;">客户名称<span id="oCustNam" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Linker','oLinker');return false;">我方联络人<span id="oLinker" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('LinkDate','oLinkDate');return false;">联络时间<span id="oLinkDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('LinkManName','oLinkManName');return false;">客户联络人<span id="oLinkManName" class="orderTip"></span></div></th>       
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
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark"></a><uc3:Message ID="Message1" runat="server" />
<span id="Forms" class="Spantype"></span>
<input id="hiddExpOrder" type="hidden" runat="server" />
<input id="hiddCustID" type="hidden" runat="server" />
<input id="hiddUserId" type="hidden" runat="server" />
</form>
</body>
</html>
