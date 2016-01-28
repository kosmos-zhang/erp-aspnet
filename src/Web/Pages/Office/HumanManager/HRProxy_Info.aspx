<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRProxy_Info.aspx.cs" Inherits="Pages_Office_HumanManager_HRProxy_Info" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人才代理列表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/HRProxy_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
</head>
<body>
<form id="frmMain" runat="server">
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">编号</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtProxyNo" id="txtProxyNo" maxlength="50" type="text" style="width:95%;" class="tdinput"  SpecialWorkCheck="编号"  runat="server" />
                                    </td>
                                    <td class="tdColTitle" width="10%">企业名称</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtProxyName" id="txtProxyName" maxlength="25" type="text" style="width:95%;" SpecialWorkCheck="企业名称"  class="tdinput"  runat="server" />
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">重要程度</td>
                                    <td class="tdColInput" width="24%">
                                        <asp:DropDownList ID="ddlImportant" runat="server">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">不重要</asp:ListItem>
                                            <asp:ListItem Value="2">普通</asp:ListItem>
                                            <asp:ListItem Value="3">重要</asp:ListItem>
                                            <asp:ListItem Value="4">关键</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">合作关系</td>
                                    <td class="tdColInput">
                                        <asp:DropDownList ID="ddlCooperation" runat="server">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">付费服务</asp:ListItem>
                                            <asp:ListItem Value="2">一般服务</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdColTitle">启用状态</td>
                                    <td class="tdColInput">
                                        <asp:DropDownList ID="ddlUsedStatus" runat="server">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">已启用</asp:ListItem>
                                            <asp:ListItem Value="0">未启用</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdColTitle"></td>
                                    <td class="tdColInput"></td>
                                </tr>             
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF"> 
                                        <uc1:Message ID="Message1" runat="server" />
                                        <input type="hidden" id="hidModuleID" name="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='SearchProxyInfo()' />
                                       
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr><td colspan="2" height="5"></td></tr>
        <tr>
            <td colspan="2">

                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr><td colspan="2" height="2"></td></tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">人才代理列表</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand" onclick="DoNew();"/>
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DeleteProxyInfo()" style='cursor:pointer;' />
                                        <asp:ImageButton ID="btnImport" runat="server" ImageUrl="~/Images/Button/Main_btn_out.jpg" onclick="btnImport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%--<div style="height:252px;overflow-y:scroll;">--%>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" >
                                            选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect')">
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('ProxyCompanyCD','oC0');return false;">
                                                企业编号<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('ProxyCompanyName','oC1');return false;">
                                                企业名称<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('ContactName','oC2');return false;">
                                                联系人<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('ContactTel','oC3');return false;">
                                                固定电话<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('ContactMobile','oC4');return false;">
                                                移动电话<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('ContactWeb','oC14');return false;">
                                                网络通讯<span id="oC14" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('Important','oC5');return false;">
                                                重要程度<span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('Cooperation','oC6');return false;">
                                                合作关系<span id="oC6" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" >
                                            <div class="orderClick" onclick="OrderBy('UsedStatus','oC7');return false;">
                                                启用状态<span id="oC7" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <%--</div>--%>
                            <br/>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
                                <tr>
                                    <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  >
                                                    <div id="pagecount"></div>
                                                </td>
                                                <td height="28"  align="right">
                                                    <div id="divPageClickInfo" class="jPagerBar"></div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="divPage">
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" size="3" maxlength="4" />条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
                                                    </div>
                                                 </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br/>
                        </td>
                    </tr>
                </table>            
            </td>
        </tr>
    </table>
<a name="DetailListMark"></a>
<span id="Forms" class="Spantype" name="Forms"></span>
<input id="hiddExpOrder" type="hidden" runat="server" />
</form>
</body>
</html>