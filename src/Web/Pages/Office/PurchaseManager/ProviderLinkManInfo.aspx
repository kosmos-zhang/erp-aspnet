<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderLinkManInfo.aspx.cs" Inherits="Pages_Office_PurchaseManager_ProviderLinkManInfo" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<%@ Register src="../../../UserControl/ProviderInfo.ascx" tagname="ProviderInfo" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/PurchaseManager/ProviderLinkManInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>
    
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <title>供应商联系人列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 销售订单-->
    <!-- 销售订单-->
    <uc1:Message  ID="Message1" runat="server" />
    <uc2:ProviderInfo ID="ProviderInfo1" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick1'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tbse','searchClick1')" /></div>
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
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" id="tbse"
                                class="table">
                                <tr class="table-item">
                                    <td height="10" bgcolor="#E7E7E7" align="right" width="10%">
                                        供应商名称</td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtCustName" MaxLength="25" onclick ="popProviderObj.ShowProviderList('txtCustName','txtCustName','txtCustNo')" ReadOnly="true" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        <input type="hidden" id="txtCustNo" runat="server" />
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        联系人姓名
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtLinkManName" MaxLength="10" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="联系人姓名" ></asp:TextBox>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                        手机
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <asp:TextBox ID="txtHandset" MaxLength="10" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="手机"  ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="10" bgcolor="#E7E7E7" align="right" width="10%">
                                        重要程度
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <select name="drpImportant" class="style1" id="drpImportant"  runat="server" >
                                        <option value="0" selected="selected">--请选择--</option>
                                        <option value="1">不重要</option><option value="2">普通</option>
                                        <option value="3">重要</option><option value="4">关键</option></select>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        联系人类型
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <select name="drpLinkType" class="tdinput" width="119px" runat="server" id="drpLinkType"></select>
                                        <input type="hidden" id="Hidden1" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                        生日
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <asp:TextBox ID="txtStartBirthday" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartBirthday')})" ReadOnly="true" CssClass="tdinput" Width="35%"></asp:TextBox>
                                        <asp:TextBox ID="txtZhi" Text="至" ReadOnly="true" runat="server"  CssClass="tdinput" Width="10%"></asp:TextBox><asp:TextBox ID="txtEndBirthday" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndBirthday')})" ReadOnly="true" CssClass="tdinput" Width="35%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" align="center" bgcolor="#FFFFFF">
                                    <input type="hidden" id="hidModuleID" runat="server" />
                                    <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img id="btnQuery" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='SearchProviderLinkManData()' visible="false" runat="server"/>
                                        <%--<img id="btnClear" alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" style='cursor: hand;'
                                            onclick="Fun_ClearInput()" width="52" height="23" />--%>
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
                供应商联系人列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <%--<a href="MasterProductSchedule_Add.aspx">--%>
                                <%--<img id="btnAdd" src="../../../images/Button/Bottom_btn_new.jpg" alt="添加主生产计划单" style='cursor: hand;'
                                    width="51" height="25" border="0" /></a><img id="btnDel" src="../../../images/Button/Main_btn_delete.jpg"
                                        alt="删除主生产计划单" style='cursor: hand;' border="0" onclick="Fun_Delete_MasterProductSchedule();" />--%>
                            <%--                                        <img src="../../../images/Button/Main_btn_submission.jpg" alt="提交审批" />
                                        <img src="../../../images/Button/Main_btn_verification.jpg" alt="审批" />
                                        <img src="../../../images/Button/Bottom_btn_confirm.jpg" alt="确认" />
                                        <img src="../../../images/Button/Main_btn_Invoice.jpg" alt="结单" />
                                        <img src="../../../images/Button/Main_btn_qxjd.jpg" alt="取消结单" />--%>
                            <img id="btn_create"  src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" style="cursor:hand;" onclick="CreateProviderLinkMan()"  runat="server" visible="false" />
                            <img id="btn_del"  src="../../../Images/Button/Main_btn_delete.jpg" alt="删除" style="cursor:hand;" onclick="DelProviderLinkMan()"  runat="server" visible="false" />
                            <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                            <%--<img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" alt="打印" />--%>
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
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" visible="false" id="checkall" onclick="SelectAll()" value="checkbox" /></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('LinkManName','oLinkManName');return false;">联系人姓名<span id="oLinkManName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('CustName','oCustName');return false;">供应商名称<span id="oCustName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('Appellation','oAppellation');return false;">称谓<span id="oAppellation" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('LinkTypeName','oLinkTypeName');return false;">联系人类型<span id="oLinkTypeName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('Important','oImportant');return false;">重要程度<span id="oImportant" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('WorkTel','oWorkTel');return false;">电话<span id="oWorkTel" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('Handset','oHandset');return false;">手机<span id="oHandset" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('MailAddress','oMailAddress');return false;">邮件<span id="oMailAddress" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('MSN','oMSN');return false;">MSN<span id="oMSN" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('QQ','oQQ');return false;">QQ<span id="oQQ" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy1('Birthday','oBirthday');return false;">生日<span id="oBirthday" class="orderTip"></span></div></th>
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
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>


