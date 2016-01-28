<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductExtList.aspx.cs" Inherits="Pages_Office_SupplyChain_ProductExtList" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>计量单位代码</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmMain" runat="server">
    <input id="HdEFDesc" type="hidden" />
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <div id="div_Add" style="border: solid 10px #898989; background: #fff; padding: 10px;
        width: 550px; z-index: 21; position: absolute; top: 50%; left: 60%; margin: -200px 0 0 -400px;
        display: none">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right" style="padding: 5px; width: 30%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px; border-left: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    物品特性描述<span class="redbold">*</span>
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;">
                    <input id="EFDescUC" class="tdinput" type="text" style="width: 95%" maxlength="10" />
                </td>
            </tr>
            <tr>
                <td align="right" style="padding: 5px; width: 30%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px; border-left: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    物品特性类型<span class="redbold">*</span>
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;">
                    <select id="EFTypeUC" style="width: 120px;" onchange="fnChange()">
                        <option selected="selected" value="1">编辑框</option>
                        <option value="2">选择框</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding: 5px; width: 30%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px; border-left: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    选择列表值列表(请以“|”分隔)
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;">
                    <textarea id="EFValueListUC" rows="3" cols="100" style="width: 95%;" disabled="disabled"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" style="border: solid #9C9A9C 1px">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="Insert();"
                        id="btnSave" runat="server" style="padding-right: 4px; margin-top: 4px;" />
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; border-bottom: solid #9C9A9C 1px;">
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();"
                        style="padding-left: 5px; margin-top: 5px;" />&nbsp;&nbsp;
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
                                        物品特性描述
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                   
                                        <input id="EFDesc" class="tdinput"   specialworkcheck="商品特性描述"   type="text" style="width: 95%" maxlength="10" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        物品特性类型
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select id="EFType" style="width: 120px;">
                                            <option selected="selected" value="">--请选择--</option>
                                            <option value="1">编辑框</option>
                                            <option value="2">选择框</option>
                                        </select>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        &nbsp;
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='TurnToPage(1)' id="btnQuery" runat="server"  visible="false" />
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
                物品特性设置
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg" onclick="Show();" runat="server"  visible="false"
                                id="btnNew" />
                            <img alt="删除" src="../../../Images/Button/Main_btn_delete.jpg" onclick="fnDel();"  runat="server"  visible="false"
                                id="btnDel"  />
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
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" style=" width:70px">
                                选择<input type="checkbox" id="checkall"  onclick="selectall();" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" style=" width:150px">
                                <div class="orderClick" onclick="OrderBy('EFDesc','oGroup');return false;">
                                    物品特性描述<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" style=" width:150px">
                                <div class="orderClick" onclick="OrderBy('EFType','oC1');return false;">
                                    物品特性类型<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('EFValueList','oC3');return false;">
                                    选择列表值<span id="Span3" class="orderTip"></span></div>
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
    </table>   、
    <input type="hidden" id="hiddAction" />
     <input type="hidden" id="hiddID"  value="0"/>
     <input type="hidden" id="hiddUrl" />
      <input type="hidden" id="hiddEFIndex"  value="0" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
  
</body>
</html>

<script src="../../../js/office/SupplyChain/ProductExtList.js" type="text/javascript"></script>

