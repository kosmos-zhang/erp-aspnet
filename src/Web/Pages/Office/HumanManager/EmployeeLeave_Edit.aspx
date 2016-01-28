<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeLeave_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeLeave_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建离职单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/EmployeeLeave_Edit.js" type="text/javascript"></script>
    <script src="../../../js/common/Flow.js" type="text/javascript"></script>
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
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建离职通知单</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"  onclick="DoSave();"/>
                                    <img src="../../../Images/Button/UnClick_qr.jpg" runat="server" visible="false" alt="确认" id="btnConfirm" style="cursor:hand" />
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="false" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand" />
                                    <span id="GlbFlowButtonSpan"></span>
                                </td>
                                <td align="right" class="tdColInput">
                                    <img src="../../../Images/Button/Main_btn_print.jpg" visible="false" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td>
<!-- <div style="height:500px;overflow-y:scroll;"> -->
<table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
    <tr>
        <td  colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                        <table width="100%" align="center" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>基本信息</td>
                                <td align="right">
                                    <div id='divBaseInfo'>
                                        <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblBaseInfo','divBaseInfo')"/>
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
                        <input type="hidden" id="hidFastModuleID" runat="server" />
                        <input type="hidden" id="hidFromPage" runat="server" />
                        <input type="hidden" id="hidSearchCondition" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                <tr>
                    <td height="20" class="tdColTitle" >离职单编号<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" >
                        <div id="divCodeRule" runat="server">
                            <uc1:CodingRule ID="codeRule" runat="server" />
                        </div>
                        <%--<div id="divCodeNo" runat="server" class="tdinput" style="display:none;width:5%"></div>--%>
                        <asp:TextBox ID="divCodeNo" CssClass="tdinput" Enabled="false" runat="server"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle" >离职单主题<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtTitle" runat="server"  MaxLength="50" CssClass="tdinput" ></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle" >对应申请单</td>
                    <td height="20" class="tdColInput" >
                        <asp:DropDownList ID="ddlApply" runat="server" Width="150px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">员工编号<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtEmployeeNo" onclick="SelectEmployeeInfo();"  ReadOnly="true" Width="95%" runat="server" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="txtEmployeeID"  runat="server" />
                    </td>
                    <td height="20" class="tdColTitle">员工姓名</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtEmployeeName" Width="95%" runat="server" CssClass="tdinput" Enabled="false"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle">入职时间</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtEnterDate" Width="95%" runat="server" Enabled="false"  CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">所属部门</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtDeptName" CssClass="tdinput" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle">岗位</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtQuarterName" CssClass="tdinput" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle">岗位职等</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtAdminLevelName" CssClass="tdinput" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">离职日期<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtOutDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOutDate')})"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle"></td>
                    <td height="20" class="tdColInput"></td>
                    <td height="20" class="tdColTitle"></td>
                    <td height="20" class="tdColInput"></td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">离职事由<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtReason" MaxLength="100" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">工作交接</td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtJobNote" MaxLength="100" Width="95%" runat="server" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">备注信息</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divRemarkInfo'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblRemarkInfo','divRemarkInfo')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblRemarkInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="20" class="tdColTitle" width="10%">制单人</td>
                    <td height="20" class="tdColInput" width="23%">
                        <asp:TextBox ID="UserCreator" onclick="alertdiv('UserCreator,txtCreator');" Enabled="false" Width="95%" CssClass="tdinput" runat="server"></asp:TextBox>
                        <input type="hidden" id="txtCreator" runat="server"/>
                    </td>
                    <td height="20" class="tdColTitle" width="10%">制单日期</td>
                    <td height="20" class="tdColInput" width="23%">
                        <asp:TextBox ID="txtCreateDate" Width="95%" runat="server" Enabled="false" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCreateDate')})"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle" width="10%"></td>
                    <td height="20" class="tdColInput" width="24%"></td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle" >确认人</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="UserConfirmor" onclick="alertdiv('UserConfirmor,txtConfirmor');" Enabled="false" Width="95%" CssClass="tdinput" runat="server"></asp:TextBox>
                        <input type="hidden" id="txtConfirmor" runat="server"/>
                    </td>
                    <td height="20" class="tdColTitle">确认日期</td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtConfirmDate" Width="95%" runat="server" Enabled="false" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtConfirmDate')})"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle" ></td>
                    <td height="20" class="tdColInput"></td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">备注</td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtRemark" runat="server" MaxLength="100" Width="95%" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>  
</table>
<!-- </div> -->
</td>
</tr>
</table>
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
</form>
</body>
</html>