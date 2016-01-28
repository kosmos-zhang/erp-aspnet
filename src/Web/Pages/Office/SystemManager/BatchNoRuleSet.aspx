<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchNoRuleSet.aspx.cs" Inherits="Pages_Office_SystemManager_BatchNoRuleSet" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>批次规则设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/office/SystemManager/BatchNoRuleSet.js" type="text/javascript"></script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <input id="batchRuleID" type="hidden" runat="server" value="" /><!--批次规则ID-->
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            批次规则设置
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" >
                  <tr>
                    <td height="28" bgcolor="#FFFFFF" align="left">
                        &nbsp;<img alt="保存"  src="../../../Images/Button/Bottom_btn_save.jpg" onclick="InsertBatchNoRule();" id="btnSave" runat="server" visible="false"/>
                    </td>
                  </tr>
                 </table>
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellspacing="1" bgcolor="#FFFFFF">
                    <tr>
                        <td bgcolor="#ffffff" align="center" valign="bottom">
                            <!-- Start 基本信息 -->
                            <table width="99%" border="0" align="center" cellpadding="0" id="Tb_01" bgcolor="#FFFFFF">
                                <tr>
                                    <td align="right" style="width:15%" >
                                        批次规则：
                                    </td>
                                    <td bgcolor="#FFFFFF"  align="left" style="width:35%">
                                        <input type="radio" id="dioBatch1" name="dioBatch" runat="server" onclick="showOrHiddenBatchRule();" />
                                        自动编号&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioBatch2" 
                                            name="dioBatch" runat="server" onclick="showOrHiddenBatchRule();" />手工输入
                                    </td>
                                    <td align="right">
                                        <%--<img src="../../../images/Button/Bottom_btn_save.jpg"  alt="保存" id="imgBatchControl"
                                            style="cursor: hand; float: left" border="0" onclick="BatchRuleSetting();" runat="server"
                                            visible="true" />--%>
                                    </td>
                                    <td >
                                    </td>
                                </tr>
                            </table><br />
                            <!-- End 基本信息 -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
            <div id="div_Add" ><!-- style="border: solid 10px #898989; z-index:21; background: #ffffff;  padding: 10px; width: 400px; top: 48%; left: 63%; margin: -200px 0 0 -400px; "-->
                
                 <table width="99%"  border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#ffffff">
                   
                        <tr>
                            <td align="right">批次规则名称：<span class="redbold">*</span></td>
                            <td align="left">
                                <asp:TextBox ID="txt_RuleName"  Width="170px" runat="server" Text="自动编号" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                            <td    align="right">编号前缀：<span class="redbold">*</span></td>
                            <td align="left">
                                <asp:TextBox ID="txt_RulePrefix"  Width="170px" runat="server" onblur="fillRuleExample();" specialworkcheck="编号前缀"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="datetype">
                            <td align="right">日期类型：</td>
                            <td align="left">
                                <input id="rd_year" type="radio" value="1" name="RadUsedStatus"  checked="checked"/>年<input id="rd_yearm" type="radio" value="1" name="RadUsedStatus"/>年月<input id="rd_yearmd" type="radio" value="1" name="RadUsedStatus"/>年月日
                            </td>
                            <td align="right">流水号长度：<span class="redbold">*</span></td>
                            <td align="left">
                                <asp:TextBox ID="txt_RuleNoLen" onblur="fillRuleExample();"  Width="170px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">编号示例：</td>
                            <td align="left">
                                <asp:TextBox ID="txt_RuleExample" runat="server"  Width="170px" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                            <td   align="right">备注：</td>
                            <td  align="left">
                                <asp:TextBox ID="txt_Remark"  Width="170px" runat="server" MaxLength="100" specialworkcheck="备注"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <%--<td align="right">是否为缺省规则：</td>
                            <td align="left">
                                <input id="chk_default" type="checkbox" checked="checked" />
                            </td>--%>
                            
                            <td align="right">最后更新人
                            </td>
                            <td align="left">
                                <input id="txt_ModifiedUserID" type="text" disabled="disabled" style="width:170px"/>
                            </td>
                            <td align="right">最后更新日期：</td>
                            <td align="left">
                                <input id="txt_ModifiedDate" type="text" disabled="disabled" style="width:170px"/>
                            </td>
                            <td align="right">
                        </tr>
                        <tr>
                            <td></td><td></td>
                            <td></td><td></td>
                        </tr>
                    </table>
                 <br />
            </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>