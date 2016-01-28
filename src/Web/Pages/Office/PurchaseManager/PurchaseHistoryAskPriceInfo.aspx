<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseHistoryAskPriceInfo.aspx.cs" Inherits="Pages_Office_PurchaseManager_PurchaseHistoryAskPriceInfo" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<%@ Register src="../../../UserControl/ProductInfoControl.ascx" tagname="ProductInfoControl" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
   
    <script src="../../../js/office/PurchaseManager/PurchaseHistoryAskPriceInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>
    
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>


    <title>采购价格统计</title>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message  ID="Message1" runat="server" />
    <uc2:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <!-- 销售订单-->
    <!-- 销售订单-->
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
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
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        物品名称
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtProductName" MaxLength="50" runat="server" onclick = "popTechObj.ShowList('','txtProductName','HidProductID');" CssClass="tdinput" ReadOnly="true" Width="95%"></asp:TextBox>
                                        <input type="hidden" id="HidProductID" runat="server" />
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                <span style="color:Red ">*</span>        采购时间
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <%--<asp:TextBox ID="txtStartPurchaseDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEquipCheckDate')})" ReadOnly="true" runat="server" CssClass="tdinput" Width="35%"></asp:TextBox>--%>
                                         <input name="txtStartPurchaseDate" id="txtStartPurchaseDate" type="text" class="tdinput" 
                                            style="width:35%;" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartPurchaseDate')})"
                        runat="server" />
                                        <asp:TextBox ID="txtZhi" Text="至" ReadOnly="true" runat="server"  CssClass="tdinput" Width="10%"></asp:TextBox>
                                        <%--<asp:TextBox ID="txtEndPurchaseDate"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEquipCheckDate')})" ReadOnly="true" runat="server" CssClass="tdinput" Width="35%"></asp:TextBox>--%>
                                        <input name="txtEndPurchaseDate" id="txtEndPurchaseDate" type="text" class="tdinput" 
                                            style="width:35%;" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndPurchaseDate')})"
                        runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                        
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                    <input type="hidden" id="hidModuleID" runat="server" />
                                    <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img id="btnQuery" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='SearchPurchaseHistoryAskPrice()' runat="server"/>
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
                采购价格统计
            </td>
        </tr>
        <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" 
                                bgcolor="#999999" style="margin-bottom: 0px">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <asp:ImageButton ID="btnPrint" runat="server" 
                                            ImageUrl="~/Images/Button/Main_btn_out.jpg" onclick="btnPrint_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
        <tr>
            <td colspan="2" id="tdResult">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <%--<th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" visible="false" id="checkall" onclick="SelectAll()" value="checkbox" /></th>--%>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('ProductNo','oProductNo');return false;">物品编号<span id="oProductNo" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('ProductName','oProductName');return false;">物品名称<span id="oProductName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Specification','oSpecification');return false;">规格<span id="oSpecification" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('UnitName','oUnitName');return false;">单位<span id="oUnitName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('LargeTaxPrice','oLargeTaxPrice');return false;">最高采购价<span id="oLargeTaxPrice" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('SmallTaxPrice','oSmallTaxPrice');return false;">最低采购价<span id="oSmallTaxPrice" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('AverageTaxPrice','oAverageTaxPrice');return false;">平均采购价<span id="oAverageTaxPrice" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('NewTaxPrice','oNewTaxPrice');return false;">最近采购价<span id="oNewTaxPrice" class="orderTip"></span></div></th>
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
                                        <div id="pageDataList1_PagerList" class="jPagerBar">
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
    </table>
    </form>
</body>
</html>