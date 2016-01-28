<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cust_Info.aspx.cs" Inherits="Pages_Office_CustManager_Cust_Info" %>
<%@ Register src="../../../UserControl/CustClassDrpControl.ascx" tagname="CustClassDrpControl" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link rel="stylesheet" type="text/css" href="../../../css/default.css" />
<script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<script src="../../../js/office/CustManager/CustInfo.js" type="text/javascript"></script>
<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/common/page.js" type="text/javascript"></script>
<script src="../../../js/common/Common.js" type="text/javascript"></script>
<script src="../../../js/common/TreeView.js" language="javascript"  type="text/javascript" ></script>
<title>客户信息列表</title>
</head>
<body>
<form id="form1" runat="server">

<table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
  <tr>
    <td valign="top">
    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td rowspan="2" align="right" valign="top">
    <div id='searchClick'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('searchtable','searchClick')"/></div>&nbsp;&nbsp;
    </td>
  </tr>
  <tr>
    <td  valign="top" class="Blue">
    <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件    
      </td>
  </tr>
  <tr>
    <td colspan="2"  >
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align=right> 客户编号</td>
            <td width="10%" bgcolor="#FFFFFF"><input name="txtCustNo" id="txtCustNo"  class="tdinput"  type="text" SpecialWorkCheck="客户编号" style="width:95%;" runat="server" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align=right>客户名称</td>
            <td width="10%" bgcolor="#FFFFFF"><input name="txtCustName" id="txtCustName"  class="tdinput" SpecialWorkCheck="客户名称" type="text" style="width:95%;" runat="server" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align=right>客户简称</td>
            <td width="10%" bgcolor="#FFFFFF"><input name="txtCustNam" id="txtCustNam"  class="tdinput" SpecialWorkCheck="客户简称" type="text" style="width:95%;" runat="server" /></td>
            <td width="10%" bgcolor="#E7E7E7" align=right>客户细分</td>
            <td width="10%" bgcolor="#FFFFFF">
                <uc1:CustClassDrpControl ID="CustClassDrpControl1" runat="server" />
              </td>
          </tr>
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align=right> 拼音缩写</td>
            <td width="10%" bgcolor="#FFFFFF"><input name="txtCustShort" id="txtCustShort" SpecialWorkCheck="拼音缩写" class="tdinput"  type="text" style="width:95%;" runat="server" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align=right>所在区域</td>
            <td width="10%" bgcolor="#FFFFFF"><asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList></td>
            
            <td width="10%" bgcolor="#E7E7E7" align=right>客户优质级别</td>
            <td width="10%" bgcolor="#FFFFFF"><asp:DropDownList ID="ddlCreditGrade" runat="server"></asp:DropDownList></td>
            <td width="10%" bgcolor="#E7E7E7" align=right>关系等级</td>
            <td width="10%" bgcolor="#FFFFFF">
               <select  name="seleRelaGrade" width="20px" id="seleRelaGrade" runat="server">
                        <option value="0">--请选择--</option>
                                  <option value="1">密切</option>
                                  <option value="2">较好</option>
                                  <option value="3">一般</option>
                                  <option value="4">较差</option>
                                </select></td>
          </tr>
          
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align=right> 分管业务员</td>
            <td width="10%" bgcolor="#FFFFFF"><input name="txtManager" id="txtManager" SpecialWorkCheck="分管业务员" class="tdinput"  type="text" style="width:95%;" runat="server" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align=right>建档日期</td>
            <td width="10%" bgcolor="#FFFFFF" colspan="3" style="width: 20%">
            <input id="txtCreatedBegin" type="text"  runat="server" class="tdinput" size="10"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCreatedBegin')})" /> 
                    至<input  id="txtCreatedEnd" type="text" runat="server" class="tdinput" size="10"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCreatedEnd')})" /> </td>
            
            <td width="10%" bgcolor="#E7E7E7" align=right>启用状态</td>
            <td width="10%" bgcolor="#FFFFFF">
                <select  name="seleUsedStatus" width="20px" id="seleUsedStatus" runat="server">
                                    <option value="-1">--请选择--</option>
                                  <option value="1">启用</option>
                                  <option value="0">停用</option>
                                </select></td>
          </tr>
         <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align=right> 电话</td>
            <td width="10%" bgcolor="#FFFFFF"><input id="txtTel" class="tdinput" SpecialWorkCheck="电话" type="text" style="width:95%;" runat="server" /></td>
            
            <td width="10%" bgcolor="#E7E7E7" align=right>客户大类</td>
            <td width="10%" bgcolor="#FFFFFF"><select  name="seleUsedStatus" width="20px" id="selCustBig" runat="server">
                                    <option value="0">--请选择--</option>
                                  <option value="1">企业</option>
                                  <option value="2">会员</option>
                                </select></td>
            
            <td width="10%" bgcolor="#E7E7E7" align=right>&nbsp;</td>
            <td width="10%" bgcolor="#FFFFFF">&nbsp;</td>
            <td width="10%" bgcolor="#E7E7E7" align=right>&nbsp;</td>
            <td width="10%" bgcolor="#FFFFFF">
                &nbsp;</td>
          </tr>
          <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"  visible="false" runat="server" style='cursor:hand;' onclick='SearchCustData(1)'/> </td>
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
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户档案列表</td></tr>
  <tr>
    <td height="35" colspan="2" valign="top"><table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="28" bgcolor="#FFFFFF"><img id="btn_create"  src="../../../images/Button/Bottom_btn_new.jpg" visible="false" runat="server" alt="新建" style='cursor:hand;' onclick="CreateCust()"  />
          <img id="btn_del"  src="../../../Images/Button/Main_btn_delete.jpg" alt="删除" style='cursor:hand;' visible="false" runat="server" onclick="DelCustInfo()"  />
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
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustNo','oCustNo');return false;">客户编号<span id="oCustNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustName','oCustName');return false;">客户名称<span id="oCustName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustBig','oCustBig');return false;">客户大类<span id="oCustBig" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustShort','oCustShort');return false;">拼音缩写<span id="oCustShort" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CodeName','oCodeName');return false;">客户细分<span id="oCodeName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('TypeName','oTypeName');return false;">客户类别<span id="oTypeName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Area','oArea');return false;">所在区域<span id="oArea" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Manager','oManager');return false;">分管业务员<span id="oManager" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CreditGrade','oCreditGrade');return false;">客户优质级别<span id="oCreditGrade" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('RelaGrade','oRelaGrade');return false;">客户关系等级<span id="oRelaGrade" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Creator','oCreator');return false;">创建人<span id="oCreator" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CreatedDate','oCreatedDate');return false;">创建日期<span id="oCreatedDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('UsedStatus','oUsedStatus');return false;">启用状态<span id="oUsedStatus" class="orderTip"></span></div></th>
        
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
<a name="pageDataList1Mark"></a><uc2:Message ID="Message1" runat="server" />
<span id="Forms" class="Spantype"></span>
<input id="hiddExpOrder" type="hidden" runat="server" />
<input id="hiddCustClass" type="hidden" runat="server" />
</form>
</body>

</html>

