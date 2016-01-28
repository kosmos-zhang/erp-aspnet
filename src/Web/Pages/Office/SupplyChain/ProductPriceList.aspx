<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPriceList.aspx.cs" Inherits="Pages_Office_SupplyChain_ProductPriceList" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/ProductInfoControl.ascx" tagname="ProductInfoControl" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
      <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
     <script src="../../../js/office/SupplyChain/ProductPriceList.js" type="text/javascript"></script>

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
                                       变更单编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                       <input type="text" id="txt_ChangeNo" specialworkcheck="变更单编号" class="tdinput" runat="server"/></td>
                                           <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                      变更单主题
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                            <input type="text" id="txt_Title" specialworkcheck="变更单主题" name="txtConfirmorReal0" class="tdinput"
                                 runat="server" /></td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                      物品名称
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                            <input type="text" id="txt_ProductName" name="txtConfirmorReal2"  readonly  onclick="popTechObj.ShowList('','txt_ProductName','hf_ProductID')"  class="tdinput"
                                 runat="server"  /><asp:HiddenField ID="hf_ProductID" runat="server" />
                                    </td>
                                </tr>
                                 <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                      申请人
                                    </td>
                                     <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="UserReporter" runat="server" onclick="alertdiv('UserReporter,txtChenger');"
                                ReadOnly="true" CssClass="tdinput" Width="48%"></asp:TextBox>
                            <input type="hidden" id="txtChenger" runat="server" />
                        </td>
                                            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        申请日期</td>
                                    <td width="20%" bgcolor="#FFFFFF" colspan="3">
                                        &nbsp;<input id="txtOpenDate" class="tdinput" name="txtOpenDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})"
                                            size="15" type="text" runat="server" />~
                                        <input id="txtCloseDate" class="tdinput" name="txtCloseDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCloseDate')})"
                                            size="15" type="text"  runat="server"/></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_ProductPriceInfo()'  id="btnQuery" visible="false" runat="server" />
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
            	物品售价变更单列表
                  </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                <img  alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg"
                 onclick="Show();" runat="server" visible="false" id="btnNew"/><img alt="" id="btn_del" src="../../../Images/Button/Main_btn_delete.jpg" 
                                 onclick="Fun_Delete_ProductPriceInfo();"  runat="server" visible="false" />                            <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999" style="table-layout:fixed;">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheck();" />
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('ChangeNo','oGroup');return false;">
                            变更单编号<span id="oGroup" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('Title','oC2');return false;">
                            变更单主题<span id="oC2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('ProductID','oC3');return false;">
                            物品编号<span id="oC3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('ProductName','oC5');return false;">
                            物品名称<span id="oC5" class="orderTip"></span></div>
                    </th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('TaxRateNew','Span9');return false;">
                            调整后销项率<span id="Span9" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('StandardSellNew','Span8');return false;">
                            调整后去税价<span id="Span8" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('SellTaxNew','Span5');return false;">
                            调整后含税价<span id="Span5" class="orderTip"></span></div>
                    </th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('DiscountNew','Span5');return false;">
                           调整后折扣<span id="Span4" class="orderTip"></span></div>
                    </th>
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('ChangeDate','Span5');return false;">
                            申请日期<span id="Span1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('ConfirmDate','Span5');return false;">
                            确认日期<span id="Span3" class="orderTip"></span></div>
                    </th>
                       <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBy('BillStatus','Span2');return false;">
                            单据状态<span id="Span2" class="orderTip"></span></div>
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
                                                align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
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
    </table><a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <uc2:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <asp:HiddenField ID="hidSearchCondition" runat="server" />
    <asp:HiddenField ID="hfTitle" runat="server" />
    <asp:HiddenField ID="hidModuleID" runat="server" />
    </form>
</body>
</html>