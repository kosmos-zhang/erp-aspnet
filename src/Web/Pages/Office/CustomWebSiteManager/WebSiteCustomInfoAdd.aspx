<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebSiteCustomInfoAdd.aspx.cs"
    Inherits="Pages_Office_CustomWebSiteManager_WebSiteCustomInfoAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc2" %>

<%@ Register src="../../../UserControl/sellModuleSelectCustUC.ascx" tagname="sellModuleSelectCustUC" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>站点用户设置</title>
    <link href="../../../css/default.css" type="text/css" rel="stylesheet" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadPhoto.js" type="text/javascript"></script>

    <script src="../../../js/office/CustomWebSiteManager/WebSiteCustomInfoAdd.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmMain" runat="server">
    <uc2:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc3:sellModuleSelectCustUC 
                        ID="sellModuleSelectCustUC1" runat="server" />
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <div id="divAdd" style="border: solid 10px #898989; background: #fff; padding: 10px;
        width: 360px; z-index: 21; position: absolute; top: 53%; left: 60%; margin: -200px 0 0 -400px;
        display: none">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="" bgcolor="#999999"
            style="margin-left: 4px">
            <tr>
                <td height="28" bgcolor="#FFFFFF">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="SaveData();"
                        id="btnSave" runat="server" visible="true" style='cursor: hand;' />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();"
                        style='cursor: hand;' />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellspacing="1" bgcolor="#999999">
            <tr>
                <td align="right" class="tdColTitle">
                    客户名称<span class="redbold">*</span>：
                </td>
                <td class="tdColInput">
                    <input id="hidCustomID" type="hidden" value="" />
                    <input id="hidID" type="hidden" value="" />
                    <input type="text" id="txtCustomName" class="tdinput" width="98%" runat="server"
                        readonly="readonly" onclick="SelectCust();" />
                </td>
            </tr>
            <tr>
                <td align="right" class="tdColTitle">
                   会员用户名<span class="redbold">*</span>：
                </td>
                <td class="tdColInput">
                    <input type="text" id="txtName" class="tdinput" width="98%" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" class="tdColTitle">
                    会员密码<span class="redbold">*</span>：
                </td>
                <td class="tdColInput">
                    <input type="password" id="txtPwd" class="tdinput" width="98%" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" class="tdColTitle">
                    VIP会员：
                </td>
                <td class="tdColInput">
                    是<input id="rdUse" type="radio" value="1" name="RadUsedStatus" />
                    否<input id="rdNotUse" type="radio" value="0" name="RadUsedStatus" checked="checked" />
                </td>
            </tr>
            <tr>
                <td align="right" class="tdColTitle">
                    状态：
                </td>
                <td class="tdColInput">
                    <select id="selStatus">
                        
                        <option value="1">启用</option>
                        <option value="0">停用</option>
                    </select>
                </td>
            </tr>
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
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                </div>
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
                                        客户名称
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txtSCustom" class="tdinput" type="text"  specialworkcheck="客户名称"/>
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        会员用户名
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txtSName" class="tdinput" type="text" specialworkcheck="会员用户名" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        VIP会员
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <select id="selUse" runat="server" width="139px">
                                            <option value=''>--请选择--</option>
                                            <option value='1'>是</option>
                                            <option value='0'>否</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        状态
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select id="selSStatus" runat="server" width="139px">
                                            <option value=''>--请选择--</option>
                                            <option value='1'>启用</option>
                                            <option value='0'>停用</option>
                                        </select>
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='SearchData()' id="btnSearch" runat="server"  visible="false" />
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
                会员列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img alt="" src="../../../Images/Button/Bottom_btn_new.jpg" onclick="ShowAdd(true);"
                                runat="server" id="btnNew" visible="false" style="cursor: hand;"  />
                            <img alt="" src="../../../Images/Button/Main_btn_delete.jpg" onclick="DeleteData();"
                                id="btnDel" runat="server" visible="true" style="cursor: hand;" />
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
                                <div class="orderClick" onclick="OrderBy('CustName','oCustName');return false;">
                                    客户名称<span id="oCustName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('LoginUserName','oLoginUserName');return false;">
                                    会员用户名<span id="oLoginUserName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('IsMember','oIsMember');return false;">
                                    VIP会员<span id="oIsMember" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Status','oStatus');return false;">
                                    状态<span id="oStatus" class="orderTip"></span></div>
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
    </form>
</body>
</html>
