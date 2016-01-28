<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnitGroupAdd.aspx.cs" Inherits="Pages_Office_SupplyChain_UnitGroupAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>计量单位组设置</title>
    <link href="../../../css/default.css" type="text/css" rel="stylesheet" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/office/SupplyChain/UnitGroupAdd.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    var selPoint = <%= SelPoint %>;// 小数位数
    </script>

</head>
<body>
    <form id="frmMain" runat="server">
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <div id="divAdd" style="border: solid 10px #898989; background: #fff; padding: 10px;
        width: 650px; z-index: 21; position: absolute; top: 53%; left: 50%; margin: -200px 0 0 -400px;
        display: none">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="" bgcolor="#999999"
            style="margin-left: 4px">
            <tr>
                <td height="28" bgcolor="#FFFFFF">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="SaveData();"
                        id="btnSave" runat="server" visible="false" style='cursor: hand;' />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();"
                        style='cursor: hand;' />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
            <tr>
                <td align="right">
                    计量单位组编号<span class="redbold">*</span>：
                </td>
                <td>
                    <input id="hidID" type="hidden" value="" />
                    <asp:TextBox ID="txtGUNO" Width="200px" runat="server" specialworkcheck="计量单位组编号"></asp:TextBox>
                </td>
                <td align="right">
                    计量单位组名称<span class="redbold">*</span>：
                </td>
                <td>
                    <asp:TextBox ID="txtGUName" Width="200px" runat="server" specialworkcheck="计量单位组名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    基本计量单位：
                </td>
                <td>
                    <select id="selUnit" runat="server" onchange="ChangeUnit()" style="width: 206px">
                    </select>
                </td>
                <td align="right">
                    备注：
                </td>
                <td>
                    <asp:TextBox ID="txtRemark" Width="200px" runat="server" specialworkcheck="备注"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td height="2">
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
            <tr>
                <td height="28" bgcolor="#FFFFFF">
                    <img runat="server" id="btnAddRow" src="../../../images/Button/Show_add.jpg" style="cursor: hand;"
                        onclick="AddBlankRow();" />&nbsp;
                    <img id="btnRow" runat="server" src="../../../images/Button/Show_del.jpg" style="cursor: hand"
                        onclick="DelRow();" />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" id="dg_Log" align="center" cellpadding="0" cellspacing="1"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width: 5%">
                        选择<input type="checkbox" name="checkall" id="checkall" onclick="SelectAll()" value="checkbox" />
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width: 10%">
                        序号
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width: 20%">
                        计量单位名称<span class="redbold">*</span>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width: 20%">
                        换算比率<span class="redbold">*</span>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width: 45%">
                        备注
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
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
                                        计量单位组编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txtSGUNO" class="tdinput" type="text" specialworkcheck="计量单位组编号" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        计量单位组名称
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txtSGUName" class="tdinput" type="text" specialworkcheck="计量单位组名称" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        基本计量单位
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <select id="selBU" runat="server" width="139px">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='SearchData()' id="btnSearch" runat="server" />
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
                计量单位组设置
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img alt="" src="../../../Images/Button/Bottom_btn_new.jpg" onclick="ShowAdd(true);"
                                runat="server" id="btnAdd" visible="false" style="cursor: hand;" />
                            <img alt="" src="../../../Images/Button/Main_btn_delete.jpg" onclick="DeleteData();"
                                id="btnDel" runat="server" visible="false" style="cursor: hand;" />
                        </td>
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
                            <th height="20" width="80px" align="center" background="../../../images/Main/Table_bg.jpg"
                                bgcolor="#FFFFFF">
                                选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAll();" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('GroupUnitNo','oGroupUnitNo');return false;">
                                    计量单位组编号<span id="oGroupUnitNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('GroupUnitName','oGroupUnitName');return false;">
                                    计量单位组名称<span id="oGroupUnitName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BaseUnitID','oBaseUnitID');return false;">
                                    基本计量单位<span id="oBaseUnitID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Remark','oRemark');return false;">
                                    备注<span id="oRemark" class="orderTip"></span></div>
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
    </table>
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <input id="hidRow" type="hidden" value="1" />
    <select id="selUnitHid" style="visibility: hidden">
    </select>
    </form>
</body>
</html>
