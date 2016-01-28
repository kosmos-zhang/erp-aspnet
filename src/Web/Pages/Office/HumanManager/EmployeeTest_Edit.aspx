<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeTest_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeTest_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建考试记录</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/EmployeeTest_Edit.js" type="text/javascript"></script>
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
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
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建考试记录</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                        <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" onclick="DoSave();"/>
                        <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="false" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand"/>
                                 </td>
                                <td align="right" class="tdColInput">
                                <img src="../../../Images/Button/Main_btn_print.jpg"  visible="false" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand" />
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
                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
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
            <table>
                <tr>
                    <td colspan="2" height="4">
                        <input type="hidden" id="hidEditFlag" runat="server" />
                        <input type="hidden" id="hidModuleID" runat="server" />
                        <input type="hidden" id="hidSearchCondition" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                <tr>
                    <td class="tdColTitle" width="10%">考试编号<span class="redbold">*</span></td>
                    <td class="tdColInput" width="23%">
                        <div id="divCodeRule" runat="server">
                            <uc1:CodingRule ID="codeRule" runat="server" />
                        </div>
                        <%--<div id="divCodeNo" runat="server" class="tdinput"></div>--%>
                        <asp:TextBox ID="divCodeNo" CssClass="tdinput" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdColTitle" width="10%">主题<span class="redbold">*</span></td>
                    <td class="tdColInput" width="23%">
                        <asp:TextBox ID="txtTitle" runat="server" SpecialWorkCheck="主题" Width="90%" MaxLength="50" CssClass="tdinput"></asp:TextBox>
                    </td>
                    <td class="tdColTitle" width="10%">考试负责人<span class="redbold">*</span></td>
                    <td class="tdColInput" width="24%">
                        <asp:TextBox ID="UserTeacher" runat="server"  Width="90%" onclick="alertdiv('UserTeacher,txtTeacher');" ReadOnly="true" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="txtTeacher" runat="server" /> 
                    </td>
                </tr>
                <tr>
                    <td class="tdColTitle">开始时间<span class="redbold">*</span></td>
                    <td class="tdColInput" >
                        <asp:TextBox ID="txtStartDate" runat="server"  Width="90%" ReadOnly="true" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>
                    </td>
                    <td class="tdColTitle" >结束时间<span class="redbold">*</span></td>
                    <td class="tdColInput">
                        <asp:TextBox ID="txtEndDate" runat="server"  Width="90%" ReadOnly="true" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"></asp:TextBox>
                    </td>
                    <td class="tdColTitle">考试地点<span class="redbold">*</span></td>
                    <td class="tdColInput">
                        <asp:TextBox ID="txtAddress" runat="server" SpecialWorkCheck="考试地点"  Width="90%" MaxLength="50" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="25" class="tdColTitle">考试状态<span class="redbold">*</span></td>
                    <td class="tdColInput">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="tdinput">
                            <asp:ListItem Value="0" Text="未开始"></asp:ListItem>
                            <asp:ListItem Value="1" Text="已结束"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdColTitle">附件</td>
                    <td class="tdColInput">
                        <table>
                            <tr>
                                <td>
                                    <div id="divUploadAttachment" runat="server">
                                        <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                                    </div>
                                    <div id="divDealAttachment" runat="server" style="display:none;">
                                        <a href="#" onclick="DealAttachment('download');">
                                            <span id='spanAttachmentName' runat="server"></span>
                                        </a>&nbsp;
                                        <a href="#" onclick="DealAttachment('clear');">删除附件</a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfAttachment" runat="server" />
                        <asp:HiddenField ID="hfPageAttachment" runat="server" />
                    </td>
                    <td class="tdColTitle">缺考人数</td>
                    <td class="tdColInput">
                        <asp:TextBox ID="txtAbsenceCount" runat="server"  Width="90%" MaxLength="3" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdColTitle" >参与人员<span class="redbold">*</span></td>
                    <td class="tdColInput" colspan="5">
                        <asp:TextBox ID="UserJoinName" Height="50" ReadOnly="true" onclick="alertdiv('UserJoinName,txtJoinUserID,2');" TextMode="MultiLine" runat="server" Width="90%" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" runat="server" id="txtJoinUserID" />
                        <input type="hidden" runat="server" id="txtJoinUserID4Modify" />
                    </td>
                </tr>
                <tr>
                    <td class="tdColTitle" >考试内容摘要</td>
                    <td class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtContent" Height="50" TextMode="MultiLine" runat="server" Width="90%" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdColTitle">考试结果</td>
                    <td class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtTestResult" runat="server"  Width="90%"  MaxLength="50" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdColTitle" >备注</td>
                    <td class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtRemark" runat="server" Width="90%" MaxLength="250" CssClass="tdinput"></asp:TextBox>
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
                        <span class="Blue">考试成绩</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divTestResult'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblTestResult','divTestResult')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblTestResult" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="28" class="tdColInput">
                        <img src="../../../images/Button/Show_EditScore.jpg" alt="录入成绩" id="EnterScore" visible="false" runat="server" style="cursor:hand" onclick="SetTestResult();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divTestResultDetail" runat="server"></div>
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