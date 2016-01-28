<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellProductEdit.aspx.cs" Inherits="Pages_Office_SellReport_SellProductEdit" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建项目预算表</title>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/message.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/office/SellReport/SellProductInfoAdd.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <asp:HiddenField ID="HidID" Value="0" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
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
                            <div id="divTitle" runat="server">产品档案管理</div>
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
                                        <img id="btn_save" alt="保存" src="../../../images/Button/Bottom_btn_save.jpg" onclick="DataOP()" visible="false"  runat="server" style="cursor: pointer"/>
                                        <img id="btnBack" alt="返回按钮" src="../../../images/Button/Bottom_btn_back.jpg" onclick="window.location.href='SellProductInfo.aspx?ModuleID=2032301'"  runat="server" style="cursor: pointer"/>
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
                          产品编号<span style="color:Red">*</span>
                        </td>
                        <td class="tdColInput" style="width:10%">
                             <asp:TextBox ID="productNo" specialworkcheck="产品编号" runat="server" CssClass="tdinput" Width="99%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle" width="10%">
                          产品名称<span style="color:Red">*</span>
                        </td>
                        <td class="tdColInput" style="width:30%">
                            <asp:TextBox ID="productname" specialworkcheck="产品名称" runat="server" CssClass="tdinput" Width="99%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle" width="10%">
                           价格
                        </td>
                        <td class="tdColInput" style="width:10%">
                            <asp:TextBox ID="price"  runat="server" CssClass="tdinput" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="10%">
                          产品说明
                        </td>
                        <td class="tdColInput" style="width:20%" colspan="5">
                             <asp:TextBox ID="brief" TextMode="MultiLine" MaxLength="10" Height="60px"  runat="server" CssClass="tdinput" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="10%">
                          备注
                        </td>
                        <td class="tdColInput" style="width:20%" colspan="5">
                             <asp:TextBox ID="memo" TextMode="MultiLine" MaxLength="10" Height="60px" runat="server" CssClass="tdinput" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
