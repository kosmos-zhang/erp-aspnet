<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeContract_Edit.aspx.cs"
    Inherits="Pages_Office_HumanManager_EmployeeContract_Edit" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRule"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeType"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建劳动合同</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/office/HumanManager/EmployeeContract_Edit.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmMain" runat="server">
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
        <tr>
            <td valign="top" colspan="2">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" align="center" colspan="2" class="Title">
                <div id="divTitle" runat="server">
                    新建劳动合同</div>
            </td>
        </tr>
        <tr>
            <td height="40" valign="top" colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="30" class="tdColInput">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false"
                                            alt="保存" id="btnSave" style="cursor: hand" onclick="DoSave();" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="false"
                                            alt="返回" id="btnBack" onclick="DoBack();" style="cursor: hand" />
                                    </td>
                                    <td align="right" class="tdColInput">
                                        <img src="../../../images/Button/Main_btn_print.jpg" alt="打印" onclick="fnPrintOrder()" style="cursor: hand" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <!-- <div style="height:500px;overflow-y:scroll;"> -->
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
                    <tr>
                        <td colspan="2">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" align="center" border="0" cellspacing="0" cellpadding="3">
                                            <tr>
                                                <td>
                                                    基本信息
                                                </td>
                                                <td align="right">
                                                    <div id='divBaseInfo'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblBaseInfo','divBaseInfo')" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" height="0">
                                        <input type="hidden" id="hidIdentityID" value="" runat="server" />
                                        <input type="hidden" id="hidListModuleID" runat="server" />
                                        <input type="hidden" id="hidEnterModuleID" runat="server" />
                                        <input type="hidden" id="hidFromPage" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                                id="tblBaseInfo" style="display: block">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">
                                        合同编号<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <div id="divCodeRule" runat="server">
                                            <uc1:CodingRule ID="codeRule" runat="server" />
                                        </div>
                                        <div id="divCodeNo" runat="server" class="tdinput" style="display: none">
                                        </div>
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">
                                        员工<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <asp:TextBox ID="UserEmployee" runat="server" onclick="alertdiv('UserEmployee,txtEmployeeID');"
                                            ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        <input type="hidden" id="txtEmployeeID" runat="server" />
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">
                                        合同类别<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <uc1:CodeType ID="ctContractName" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">
                                        合同名称<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="tdinput" Width="95%" specialworkcheck="合同名称"  MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td height="20" class="tdColTitle">
                                        合同类型
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlContractType" runat="server" CssClass="tdinput">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">新签合同</asp:ListItem>
                                            <asp:ListItem Value="2">续签合同</asp:ListItem>
                                            <asp:ListItem Value="3">变更合同</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" class="tdColTitle">
                                        合同属性
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlContractProperty" runat="server" CssClass="tdinput">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">试用合同</asp:ListItem>
                                            <asp:ListItem Value="2">正式合同</asp:ListItem>
                                            <asp:ListItem Value="3">临时用工合同</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td height="20" class="tdColTitle">工种</td>
                    <td height="25" class="tdColInput">
                        <asp:DropDownList ID="ddlContractKind" runat="server" CssClass="tdinput">
                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                            <asp:ListItem Value="1">合同工</asp:ListItem>
                            <asp:ListItem Value="2">临时工</asp:ListItem>
                            <asp:ListItem Value="3">兼职</asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                                    <td height="20" class="tdColTitle">
                                        转正标识
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlFlag" runat="server" CssClass="tdinput">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" class="tdColTitle">
                                        合同状态
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlContractStatus" runat="server" CssClass="tdinput">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="0">失效</asp:ListItem>
                                            <asp:ListItem Value="1">有效</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" class="tdColTitle">
                                        合同期限
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlContractPeriod" runat="server" CssClass="tdinput">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">固定期限</asp:ListItem>
                                            <asp:ListItem Value="2">不定期限</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        试用月数
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="txtTestMonth" runat="server" CssClass="tdinput" MaxLength="2" Width="95%"></asp:TextBox>
                                    </td>
                                    <td height="20" class="tdColTitle">
                                        试用工资
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="txtTestWage" runat="server" CssClass="tdinput" MaxLength="13" onchange="Number_round(this,2)"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                    <td height="20" class="tdColTitle">
                                        转正工资
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="txtWage" runat="server" CssClass="tdinput" MaxLength="13" onchange="Number_round(this,2)"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        签约时间<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="txtSigningDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtSigningDate')})"></asp:TextBox>
                                    </td>
                                    <td height="20" class="tdColTitle">
                                        生效时间<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="txtStartDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>
                                    </td>
                                    <td height="20" class="tdColTitle">
                                        失效时间<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="txtEndDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        合同到期提醒人</td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="UserReminder" runat="server" onclick="alertdiv('UserReminder,hidReminder');"
                                            ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                             <input type="hidden" id="hidReminder" runat="server" /></td>
                                    <td height="20" class="tdColTitle">
                                        提前时间</td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="txtAheadDay" runat="server" CssClass="tdinput" Text="7" MaxLength="9" onchange="Number_round(this,0)"
                                            Width="15%"></asp:TextBox>天</td>
                                    <td height="20" class="tdColTitle">
                                        &nbsp;</td>
                                    <td height="20" class="tdColInput">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <%--<td height="20" class="tdColTitle">转正标识</td>
                    <td height="20" class="tdColInput">
                        <asp:DropDownList ID="ddlFlag" runat="server" CssClass="tdinput">
                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                            <asp:ListItem Value="1">是</asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                                    <td height="20" class="tdColTitle">
                                        附件
                                    </td>
                                    <td height="20" class="tdColInput" colspan="3">
                                        <table style="height: 25px;">
                                            <tr>
                                                <td>
                                                    <div id="divUploadAttachment" runat="server">
                                                        <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                                                    </div>
                                                    <div id="divDealAttachment" runat="server" style="display: none;">
                                                        <a href="#" onclick="DealAttachment('download');"><span id='spanAttachmentName' runat="server">
                                                        </span></a>&nbsp; <a href="#" onclick="DealAttachment('clear');">删除附件</a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hfAttachment" runat="server" />
                                        <asp:HiddenField ID="hfPageAttachment" runat="server" />
                                    </td>
                                    <td height="20" class="tdColTitle">
                                    </td>
                                    <td height="20" class="tdColInput">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="10">
                        </td>
                    </tr>
                </table>
                <!-- </div> -->
            </td>
        </tr>
    </table>
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="msgError" runat="server" />
    </form>
</body>
</html>
