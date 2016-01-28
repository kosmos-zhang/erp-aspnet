<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageAdd.aspx.cs" Inherits="Pages_Office_StorageManager_StorageAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建仓库</title>
    <script src="../../../js/common/TreeView.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>


    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/office/StorageManager/StorageAdd.js" type="text/javascript"></script>



    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
</head>
<body>

 
    <br />
    <form id="Form1" runat="server">
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_Add" runat="server"
                                style="display: none">
                                <tr>
                                    <td height="30" align="center" class="Title">
                                        新建仓库
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_Edit" runat="server"
                                style="display: none">
                                <tr>
                                    <td height="30" align="center" class="Title">
                                        仓库信息
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_save.jpg" id="btn_save" runat="server"
                                onclick="Fun_Save_Storage();" style="cursor: pointer;" visible="false" />
                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" runat="server" id="btnBack"
                                visible="false" onclick="DoBack();" style="cursor: pointer" />
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <input type="hidden" id="hidModuleID" runat="server" />
                            <input type="hidden" id="hidSearchCondition" runat="server" />
                        </td>
                        <td bgcolor="#FFFFFF" align="right">
                            <img src="../../../images/Button/Main_btn_print.jpg" id="btnPrint" runat="server"
                                alt="打印" visible="false" />
                        </td>
                    </tr>
                </table>
                <%--                <div style="overflow-y: auto; height: 500px;">--%>
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
                                        基础信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            仓库编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" style="display: block;" width="23%">
                            <div runat="server" id="div_StorageNo_uc"  style="width:auto; float:left">
                                <uc2:CodingRuleControl ID="txtStorageNo" runat="server" />
                            </div>
                            <div id="div_StorageNo_Lable" runat="server" style="display: none; float:left ">
                                <asp:Label runat="server" ID="lbStorageNo" Text=""></asp:Label>
                            </div>
                            <%--<asp:TextBox ID="txtStorageNo" MaxLength="10" runat="server" CssClass="tdinput" Width="150px"
                                onblur="checkonly()"></asp:TextBox>--%>
                            <input type="hidden" id="txtStorageNoHidden" name="txtStorageNoHidden" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            仓库名称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtStorageName" MaxLength="100" runat="server" CssClass="tdinput"
                                specialworkcheck="仓库名称" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            启用状态<span class="redbold">*</span>
                        </td>
                        <td height="20" align="left" class="tdColInput" width="24%">
                            <asp:DropDownList ID="sltUsedStatus" runat="server">
                                <%--<asp:ListItem Value="" Text="请选择启用状态"></asp:ListItem>--%>
                                <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                                <asp:ListItem Value="0" Text="停用"></asp:ListItem>
                            </asp:DropDownList>
                            <%--<select id="sltUsedStatus" name="sltUsedStatus" runat="server">
                                <option value="1">已启用</option>
                                <option value="0">未启用</option>
                            </select>--%>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            仓库类型<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:DropDownList ID="sltType" runat="server">
                                <%--<asp:ListItem Value="" Text="请选择仓库类型"></asp:ListItem>--%>
                                <asp:ListItem Value="0" Text="一般库"></asp:ListItem>
                                <asp:ListItem Value="1" Text="委托代销库"></asp:ListItem>
                                <asp:ListItem Value="2" Text="贵重物品库"></asp:ListItem>
                            </asp:DropDownList>
                            <%--<select id="sltType" name="SetPro0" runat="server">
                                <option value="0">一般库</option>
                                <option value="1">委托代销库</option>
                                <option value="2">贵重物品库</option>
                            </select>--%>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            仓库说明
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txtRemark" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" bgcolor="#E6E6E6">
                       仓库管理员 
                        </td>
                        <td bgcolor="#FFFFFF">
                                 <input name="UsertxtExecutor" id="UsertxtExecutor" type="text" class="tdinput" size="19"
                                    readonly="readonly" onclick="alertdiv('UsertxtExecutor,txtExecutorID');" style="width: 95%"   runat="server"/>
                                <input name="txtExecutorID" id="txtExecutorID" type="hidden"  runat="server"/>
                                   
                        </td>
                    </tr>
          <tr>
                        <td height="20" align="right" class="tdColTitle">
                        可查看该仓库的人员
                         
                        </td>
                        <td height="20" class="tdColInput" colspan="5">
                                  <asp:TextBox ID="txtUserList" runat="server" Width="100%"></asp:TextBox>
                        </td>
                       
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1">
                    <tr>
                        <td height="8">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
       <asp:HiddenField ID="txtUserListHidden" runat="server" />
 
    </form>
        <div id="treeDiv1" style="display: none; width: 300px; margin: 0; height: 400px;
        overflow: auto; background: #f1f1f1; border: solid 1px #999999;">
    </div>
</body>
</html>
