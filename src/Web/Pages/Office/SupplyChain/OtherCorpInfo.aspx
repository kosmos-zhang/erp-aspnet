<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OtherCorpInfo.aspx.cs" Inherits="Pages_Office_SupplyChain_OtherCorpInfo" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>其他往来单位信息</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script type="text/javascript">
    function ChangeValue()
    {
     if(document.getElementById("chk_isTax").checked)
     {
      document.getElementById("lblmsg").innerHTML="是一般纳税人"; 
     }
     else
     {
      document.getElementById("lblmsg").innerHTML="不是一般纳税人"; 
     }
    }
    </script>
    <script src="../../../js/office/SupplyChain/OtherCorpInfoList.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMain" runat="server">
   
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                <uc1:Message ID="Message1" runat="server" />
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
             
                <uc1:Message ID="Message2" runat="server" />
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
                                        往来单位大类
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                            <select id="sel_BigType" name="D2" runat="server">
                            <option value="">--请选择--</option>
                                <option value="5">外协加工厂</option>
                                   <option value="6">运输商</option>
                                      <option value="7">其他</option>
                            </select></td>
                                           <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                       往来单位编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                            <input type="text" id="txt_CustNo" name="txtConfirmorReal0" class="tdinput"
                                 runat="server" specialworkcheck="往来单位编号"  /></td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        启用状态
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                       <select id="UsedStatus" runat="server" name="SetPro2" width="139px">
                                        <option value="">--请选择--</option>
                           <option value="1">启用</option>
                          <option value="0">停用</option>
                                </select>
                                    </td>
                                </tr>
                                 <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        往来单位名称
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                            <asp:TextBox ID="txt_CustName" specialworkcheck="往来单位名称"  MaxLength="50" runat="server" CssClass="tdinput" 
                                Width="70%" Height="24px"></asp:TextBox>
                                    </td>
                                           <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                               所在区域</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                            <asp:DropDownList ID="sel_BillType" runat="server" Width="106px">
                            </asp:DropDownList>
                                        </td>
                                      <td width="10%" bgcolor="#E7E7E7" align="right">
                                          <asp:Label ID="lblmsg" runat="server" Text="是否一般纳税人"></asp:Label>
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                        <select 
                                            id="chk_isTax" runat="server" name="SetPro3" width="139px">
                                        <option value="">--请选择--</option>
                           <option value="1">是</option>
                          <option value="0">否</option>
                                </select></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_OtherCorpInfo()'  id="btnQuery" visible="false" runat="server" />
                                        </td>
                                    
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
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
                 <input id="hf_ID" type="hidden" />
         	其他往来单位列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
            <img alt=""  src="../../../Images/Button/Bottom_btn_new.jpg" 
              onclick="Show();" runat="server" visible="false" id="btnNew" /><img alt="" id="btn_del" src="../../../Images/Button/Main_btn_delete.jpg" 
                              onclick="Fun_Delete_OtherCorpInfo();" runat="server" visible="false" /><img 
                                alt="导出" src="../../../Images/Button/Main_btn_out.jpg" 
                                /></td>
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
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAll();" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustNo','oGroup');return false;">
                                   往来单位编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustName','oC1');return false;">
                                    往来单位名称<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Area','oC3');return false;">
                                    所在区域<span id="Span3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CompanyType','oC4');return false;">
                                    单位性质<span id="oC4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ContactName','oC4');return false;">
                                    联系人<span id="Span1" class="orderTip"></span></div>
                            </th>
                              <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Tel','oC4');return false;">
                                    联系电话<span id="Span2" class="orderTip"></span></div>
                            </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BigType','Span4BigType');return false;">
                                    往来单位大类<span id="Span4BigType" class="orderTip"></span></div>
                            </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UsedStatus','Span5UsedStatus');return false;">
                                    启用状态<span id="Span5UsedStatus" class="orderTip"></span></div>
                            </th>
                               <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('EmployeeName','oC1');return false;">
                                    建档人<span id="Span8" class="orderTip"></span></div>
                            </th>
                               <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CreateDate','oC1');return false;">
                                    建档日期<span id="Span9" class="orderTip"></span></div>
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
                                            <input name="text" type="text" id="Text2" style="display: none" />
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
    </table> <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <asp:HiddenField ID="hidModuleID" runat="server" />
    <asp:HiddenField ID="hidSearchCondition" runat="server" />
    </form>
</body>
</html>

