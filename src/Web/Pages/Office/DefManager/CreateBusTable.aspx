<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateBusTable.aspx.cs" Inherits="Pages_Office_DefManager_CreateBusTable" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>创建表单</title>
    <script src="../../../js/office/FinanceManager/BillingAdd.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/office/DefManager/CreateBusTable.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <input name='DetailCount' type='hidden' id='DetailCount' value="1" />
    <asp:HiddenField ID="Hidpagestate" runat="server" /><!--记录页码和当前页号-->
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <div id="divTitle" runat="server">创建表单</div>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img id="btnBilling_Save" src="../../../images/Button/Bottom_btn_save.jpg" onclick="SaveTable()"  runat="server" style="cursor: pointer"/>
                                        <img id="Img1" src="../../../images/Button/Bottom_btn_createtable.jpg" onclick="InitTable()"  runat="server" style="cursor: pointer"/>
                                        <img id="Img2" alt="返回按钮" src="../../../images/Button/Bottom_btn_back.jpg" onclick="BackToPage()"  runat="server" style="cursor: pointer"/>      <b><span id="Prompt" style="font-size:14px;color:Red;">生成表之后将不能再修改该表单的信息</span></b>
                                    </td>
                                    <td align="right"> &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        基本信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick')" />
                                            </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: block">
                    <tr>
                        <td class="tdColTitle" width="10%">
                          表单别名
                        </td>
                        <td class="tdColInput">
                             <asp:TextBox ID="txtAliasTableName" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                             <asp:HiddenField ID="hidID" runat="server" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="tdColTitle" width="10%">
                           表单名
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtCustomTableName" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="tdColTitle" width="10%">
                          从属于
                        </td>
                        <td class="tdColInput">
                            <asp:DropDownList ID="dllParentID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="10%">
                          表类型
                        </td>
                        <td class="tdColInput">
                            <asp:DropDownList ID="ddlTableType" runat="server">
                            <asp:ListItem Value="0">业务表</asp:ListItem>
                            <asp:ListItem Value="1">字典表</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="10%">
                          小计项设置
                        </td>
                        <td class="tdColInput">
                            <asp:DropDownList ID="totalFlag" runat="server">
                            <asp:ListItem Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="10%">
                          默认行
                        </td>
                        <td class="tdColInput">
                           <asp:TextBox ID="txtColumnNumber" runat="server" Text='1' CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                <br />
                 <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF" valign="middle">
                            <img runat="server" src="../../../images/Button/Show_add.jpg" style="cursor: hand" id="imgAdd" onclick="AddOrderSignRow();" />
                            <img runat="server" src="../../../images/Button/Show_del.jpg"  style="cursor: hand" id="imgDel" onclick="DeleteOrderSignRow();" />
                        </td>
                        <td style="height:28px; width:40%" bgcolor="#FFFFFF" valign="middle">
                            <span style="font-weight:bold; color:Red">“列表显示”列至少保持一个被选中</span>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" id="DetailTable"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td bgcolor="#E6E6E6" class="Blue" width="50" align="center">
                            <input type="checkbox" name="checkall" id="checkall" onclick="fnSelectAll()" title="全选"
                                style="cursor: hand" />
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            字段标题<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            字段代码<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            字段类型<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            控件类型<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            字段长度<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            控件长度<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            主键
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            显示
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            允许空
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            检索
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            合计
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            列表显示
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            排序<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            表达式
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    <span id="Forms" class="Spantype"></span>
    <input id="hidSearchCondition" type="hidden" runat="server" />
    <uc7:Message ID="Message1" runat="server" />
    </form>
</body>
</html>
