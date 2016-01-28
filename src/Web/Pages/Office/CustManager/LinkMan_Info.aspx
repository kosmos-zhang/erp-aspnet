<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkMan_Info.aspx.cs" Inherits="Pages_Office_CustManager_LinkMan_Info" %>



<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc2" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>联系人列表</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/LinkManInfo.js" type="text/javascript"></script> 
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
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
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">对应客户</td>
            <td width="15%" bgcolor="#FFFFFF">
                <uc2:CustNameSel ID="CustNameSel1" runat="server" />
              </td>
            <td width="10%" bgcolor="#E7E7E7" align="right">联系人姓名</td>
            <td width="15%" bgcolor="#FFFFFF"><input name="txtLinkManName" id="txtLinkManName" class="tdinput" runat="server" type="text" SpecialWorkCheck="联系人姓名" maxlength="20" /></td>
            <td width="10%" bgcolor="#E7E7E7" align="right">手机</td>
            <td width="15%" bgcolor="#FFFFFF"><input name="txtHandset" id="txtHandset" runat="server" class="tdinput"  type="text" maxlength="20" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align="right">重要程度</td>
            
            <td width="15%" bgcolor="#FFFFFF">
                    <select  name="seleImportant" width="20px" id="seleImportant" runat="server">
                        <option value="0">--请选择--</option>
                                  <option value="1">不重要</option>
                                  <option value="2">普通</option>
                                  <option value="3">重要</option>
                                  <option value="4">关键</option>
                                </select></td>
            
          </tr>
           <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">联系人类型</td>
            <td width="15%" bgcolor="#FFFFFF">                    
                    <asp:DropDownList ID="ddlLinkType" runat="server">
                    </asp:DropDownList>                    
                            </td>
            <td width="10%" bgcolor="#E7E7E7" align="right">生日</td>
            <td bgcolor="#FFFFFF" colspan="3" style="width: 30%"><input id="txtDateBegin" runat="server" type="text" class="tdinput" size="10"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtDateBegin')})" /> 
                    至<input name="txtTalkEnd" id="txtDateEnd" type="text" class="tdinput" runat="server" size="10"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtDateEnd')})" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align="right">工作电话</td>
            
            <td width="15%" bgcolor="#FFFFFF">
                    <input id="txtWorkTel" runat="server" class="tdinput" type="text" maxlength="20" SpecialWorkCheck="工作电话" /></td>            
          </tr>
          <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearLink(1)'  visible="false" runat="server" /> 
                
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
    <td rowspan="2" align="right" valign="top">&nbsp;&nbsp;</td>
  </tr>
  
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">联系人列表</td>
  </tr>
  <tr>
    <td height="35" colspan="2" valign="top"><table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="28" bgcolor="#FFFFFF"><img id="btn_create"  src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" runat="server" style='cursor:hand;' onclick="CreateLink()" />
          <img id="btn_del"  src="../../../Images/Button/Main_btn_delete.jpg" alt="删除" style='cursor:hand;' onclick="DelLinkInfo()" visible="false" runat="server" />
          <asp:ImageButton ID="btnImport" runat="server" ImageUrl="~/Images/Button/Main_btn_out.jpg" onclick="btnImport_Click" /> 
           </td>
            
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
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('linkmanname','olinkmanname');return false;">联系人姓名<span id="olinkmanname" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustName','oCustName');return false;">对应客户<span id="oCustName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Appellation','oAppellation');return false;">称谓<span id="oAppellation" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('TypeName','oTypeName');return false;">联系人类型<span id="oTypeName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Important','oImportant');return false;">重要程度<span id="oImportant" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('WorkTel','oWorkTel');return false;">工作电话<span id="oWorkTel" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Handset','oHandset');return false;">手机<span id="oHandset" class="orderTip"></span></div></th>
           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('QQ','oQQ');return false;">QQ<span id="oQQ" class="orderTip"></span></div></th>
           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Birthday','oBirthday');return false;">生日<span id="oBirthday" class="orderTip"></span></div></th>
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
<uc1:Message ID="Message1" runat="server" />
<span id="Forms" class="Spantype"></span>
<input id="hiddExpOrder" type="hidden" runat="server" />
<input id="hiddCustID" type="hidden" runat="server" />
<input id="hiddUserId" type="hidden" runat="server" />
</form>
</body>
</html>
