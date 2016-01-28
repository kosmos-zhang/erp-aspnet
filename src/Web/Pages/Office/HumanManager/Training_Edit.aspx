<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Training_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_Training_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建培训</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/Training_Edit.js" type="text/javascript"></script>
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/DeleteFile.js" type="text/javascript"></script>
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
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建培训</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" onclick="SaveTrainingInfo();"/>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="false" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand" />
                                </td>
                                <td align="right" class="tdColInput">
                                    <img src="../../../images/Button/Main_btn_print.jpg"  alt="打印"  onclick="fnPrintOrder()" style="cursor:hand" />
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
<table width="99%" border="0" cellpadding="0" cellspacing="0" id="tblmain" align="center">
    <tr>
        <td  colspan="2">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>培训基本信息</td>
                                <td align="right">
                                    <div id='divTrainingInfo'>
                                        <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblTrainingInfo','divTrainingInfo')"/>
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
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblTrainingInfo" style="display:block">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">培训编号<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%">
                        <div id="divCodeRule" runat="server">
                            <uc1:CodingRule ID="codruleTraining" runat="server" />
                        </div><div id="divTrainingNo" runat="server" class="tdinput"></div>
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%">培训名称<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%">
                        <asp:TextBox ID="txtTrainingName" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%">发起时间<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="24%">
                        <asp:TextBox ID="txtApplyDate" runat="server" ReadOnly="true" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtApplyDate')})"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >发起人<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="UserCreaterName" ReadOnly="true" runat="server" onclick="alertdiv('UserCreaterName,txtCreateID');" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="txtCreateID" runat="server" />
                    </td>
                    <td height="20" align="right" class="tdColTitle">项目编号</td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtProjectNo" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle" >项目名称</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtProjectName" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >培训机构</td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtTrainingOrgan" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle" >预算费用</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtPlanCost" runat="server" MaxLength="25" CssClass="tdinput"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle">培训天数</td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtTrainingCount" runat="server" MaxLength="9" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >培训地点</td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtTrainingPlace" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle" >培训方式</td>
                    <td height="20" class="tdColInput">
                        <uc1:CodeType ID="ddlTrainingWay" runat="server" />
                    </td>
                    <td height="20" align="right" class="tdColTitle">培训老师</td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtTrainingTeacher" runat="server" MaxLength="25" CssClass="tdinput" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >开始时间</td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle" >结束时间</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle">考核人</td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtCheckPerson" runat="server" MaxLength="25" CssClass="tdinput" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle">附件</td>
                    <td height="20" class="tdColInput">
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
                    <td height="20" align="right" class="tdColTitle" >目的</td>
                    <td height="20" class="tdColInput" colspan="3">
                        <asp:TextBox ID="txtGoal" TextMode="MultiLine" runat="server" Width="85%" MaxLength="100" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >参与人员<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="UserJoinUserName" Height="50" ReadOnly="true" onclick="alertdiv('UserJoinUserName,txtJoinUser,2');" TextMode="MultiLine" runat="server" Width="95%" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" runat="server" id="txtJoinUser" />
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >培训备注</td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtTrainingRemark" runat="server" Width="95%" MaxLength="100" rows="3" Height="30"
                            CssClass="tdinput" TextMode="MultiLine"></asp:TextBox>
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
                        <span class="Blue">进度安排</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divSchedule'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSchedule','divSchedule')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="100%" border="0" id="tblSchedule" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="28" class="tdColInput">
                        <img src="../../../images/Button/Show_add.jpg" alt="添加" runat="server" visible="false" style="cursor:hand" onclick="AddScheduleInfo();" id="btnAddMX"/>
                        <img src="../../../images/Button/Show_del.jpg" alt="删除" runat="server" visible="false" style="cursor:hand" onclick="DeleteScheduleInfo();" id="btnDelMX" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divScheduleInfo" runat="server"></div>
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
