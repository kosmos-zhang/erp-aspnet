<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeReasonType.aspx.cs" Inherits="Pages_Office_SupplyChain_CodeReasonType" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>原因代码</title>
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
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <div id="div_Add" style="border: solid 10px #898989; background: #fff; padding: 10px;
        width: 450px; z-index: 21; position: absolute; top: 53%; left: 65%; margin: -200px 0 0 -400px;
        display: none">
        <%--<iframe id="aaaa" style="position: absolute; z-index: -1; width:400px; height:10px;" frameborder="1">  </iframe>--%>
        <table width="92%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
            style="margin-left: 38px">
            <tr>
                <td height="28" bgcolor="#FFFFFF">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="InsertCodeReasonFee();"
                        id="btnSave" runat="server" visible="false" />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidSearchCondition" runat="server" />
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
            <tr>
                <td align="right">
                    原因名称：<span class="redbold">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txt_name" Width="200px" runat="server" specialworkcheck="原因名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    原因类别：<span class="redbold">*</span>
                </td>
                <td>
                    <select id="sel_type" style="width: 206px">
                        <option value="1">出入库原因</option>
                        <option value="2">借货原因</option>
                        <option value="3">库存调整原因</option>
                        <option value="4">库存调拨原因</option>
                        <option value="5">库存报损原因</option>
                        <option value="6">不合格原因</option>
                        <option value="20">销售退货原因</option>
                        <option value="21">采购退货原因</option>
                        <option value="22">采购原因</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right">
                    描述信息：
                </td>
                <td>
                    <asp:TextBox ID="txt_Description" Width="200px" runat="server" TextMode="MultiLine"
                        specialworkcheck="描述信息"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    启用状态：
                </td>
                <td>
                    启用<input id="rd_use" type="radio" value="1" name="RadUsedStatus" checked="checked" />
                    停用<input id="rd_notuse" type="radio" value="0" name="RadUsedStatus" />
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
                                        原因名称
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txt_typeflagname" class="tdinput" type="text" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        原因类别
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select id="seltype" name="D1">
                                            <option value="0">--请选择--</option>
                                            <option value="1">出入库原因</option>
                                            <option value="2">借货原因</option>
                                            <option value="3">库存调整原因</option>
                                            <option value="4">库存调拨原因</option>
                                            <option value="5">库存报损原因</option>
                                            <option value="6">不合格原因</option>
                                            <option value="20">销售退货原因</option>
                                            <option value="21">采购退货原因</option>
                                            <option value="22">采购原因</option>
                                        </select>
                                    </td>
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
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_UserInfo()' id="btnQuery" visible="false" runat="server" />
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
                <input id="hf_TableName" type="hidden" value="officedba.CodeReasonType" />
                <asp:HiddenField ID="txtPlanNoHidden" runat="server" />
                <asp:HiddenField ID="hfunitcy" runat="server" />
                <asp:HiddenField ID="hfcodename" runat="server" />
                原因信息列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img alt="" src="../../../Images/Button/Bottom_btn_new.jpg" onclick="Show();" runat="server"
                                visible="false" id="btnNew" /><img alt="" src="../../../Images/Button/Main_btn_delete.jpg"
                                    onclick="DelCodePubInfo();" id="btnDel" runat="server" visible="false" />
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
                                <div class="orderClick" onclick="OrderBy('CodeName','oGroup');return false;">
                                    原因名称<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Flag','oC1');return false;">
                                    原因类别<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UsedStatus','oC3');return false;">
                                    启用状态<span id="Span3" class="orderTip"></span></div>
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

<script src="../../../js/office/SupplyChain/CodeReasonFee.js" type="text/javascript"></script>

