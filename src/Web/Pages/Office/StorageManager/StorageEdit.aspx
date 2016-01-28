<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageEdit.aspx.cs" Inherits="Pages_Office_StorageManager_StorageEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>生产管理</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
    <div id="popupContent">
    </div>
    <div class="divbox" style="width: 700px;">
        <div class="divboxtitle">
            <span>仓库信息_修改</span><div class="clearbox">
            </div>
        </div>
        <div class="divbox" style="width: 700px;">
            <div id="BtnArea">
                <span style="font-size: 14px; font-weight: bold">修改仓库信息</span>
                <div id="divError" style="display: none; clear: both; float: none; position: relative;
                    left: 25px; background: #fbe3e4 url('http://www.ebankon.com/person/image/eva_img/error.png') no-repeat 3px 3px;
                    width: 60%; margin: 2px 0px; border: 1px solid #fbc2c4; color: #d12f19; padding: 2px 4px 2px 21px;
                    line-height: 150%; text-align: justify">
                </div>
            </div>
            <div class="divboxtitle">
                <span>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/Button/Button_edit.jpg"
                        OnClientClick="return CheckWorkCenterInput();" OnClick="btnEdit_Click" />
                </span>
                <div class="clearbox">
                </div>
            </div>
            <div class="divboxbody">
                <div class="divboxbodyleft">
                    <table width="100%" border="1" cellspacing="0" cellpadding="0" style="border: 1pt solid #CCCCCC;">
                        <tr align="right">
                            <td align="right" style="background-color: #D9D9D9">
                                仓库代码<font color="red">*</font>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="txtStorageNo" MaxLength="10" runat="server" CssClass="input3" Width="150px"
                                    ReadOnly></asp:TextBox>
                            </td>
                            <td align="right" style="background-color: #D9D9D9">
                                仓库名称<font color="red">*</font>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="txtStorageName" MaxLength="50" runat="server" CssClass="input3" Width="150px"
                                    onblur="CheckWorkCenter_WCName(this);"></asp:TextBox>
                            </td>
                            <td align="right" style="background-color: #D9D9D9">
                                拼音缩写
                            </td>
                            <td align="left" height="26">
                                <font color="red">
                                    <asp:TextBox ID="txtPYShort" runat="server" Width="150px" onblur="CheckWorkCenter_PYShort(this);"></asp:TextBox>
                                </font>
                            </td>
                        </tr>
                        <tr align="right">
                            <td style="background-color: #D9D9D9">
                                仓库类型
                            </td>
                            <td align="left" class="style1">
                                <font color="red">
                                    <select id="sltType" name="sltType" runat="server">
                                        <option value="0">一般库</option>
                                        <option value="1">委托代销库</option>
                                        <option value="2">贵重物品库</option>
                                    </select></font>
                            </td>
                            
                            <td width="11%" style="background-color: #D9D9D9">
                                启用状态
                            </td>
                            <td align="left" height="26">
                                <font color="red">
                                    <select id="sltUsedStatus" name="sltUsedStatus" runat="server">
                                        <option value="1">是</option>
                                        <option value="0">否</option>
                                    </select>
                                </font>
                            </td>
                        </tr>
                        <tr align="right">
                            <td style="background-color: #D9D9D9">
                                备注
                            </td>
                            <td align="left" height="26" colspan="5">
                                <font color="red">
                                    <asp:TextBox ID="txtRemark" runat="server" Width="300px"></asp:TextBox>
                                </font>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script src="../../../js/common/Date.js" type="text/javascript"></script>

<script src="../../../js/common/Check.js" type="text/javascript"></script>

<script src="../../../js/office/ProductionManager/WorkCenterAdd.js" type="text/javascript"></script>

--%>