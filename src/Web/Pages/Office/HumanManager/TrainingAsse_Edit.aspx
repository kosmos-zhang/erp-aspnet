<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrainingAsse_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_TrainingAsse_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建培训考核</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/TrainingAsse_Edit.js" type="text/javascript"></script>
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
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建培训考核</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
            
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" onclick="SaveTrainingRessInfo();"/>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="false" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand" />
                                </td>
                                <td align="right" class="tdColInput">
                                    <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" onclick="DoPrint();" alt="打印" id="btnPrint" style="cursor:hand" />
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
                                <td>考评基本信息</td>
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
                        <input type="hidden" id="hidFrom" runat="server" />
                        <input type="hidden" id="hidModuleIDTraining" runat="server" />
                        <input type="hidden" id="hidSearchCondition" runat="server" />
                        <div style="display:none">
                            <asp:DropDownList ID="ddlAsseLevel" runat="server">
                                <asp:ListItem Value="">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">不及格</asp:ListItem>
                                <asp:ListItem Value="2">及格</asp:ListItem>
                                <asp:ListItem Value="3">良好</asp:ListItem>
                                <asp:ListItem Value="4">优秀</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">考核编号<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%">
                        <div id="divCodeRule" runat="server">
                            <uc1:CodingRule ID="codruleTrainingAsse" runat="server" />
                        </div>
                        <div id="divTrainingAsseNo" runat="server" class="tdinput"></div>
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%">培训名称<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%">
                        <asp:DropDownList ID="ddlTraining" runat="server" CssClass="tdinput"></asp:DropDownList>
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%">考核人<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="24%">
                        <asp:TextBox ID="UserCheckPerson" runat="server" MaxLength="25" readonly onclick="alertdiv('UserCheckPerson,txtIDCheckPerson');" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="txtIDCheckPerson" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle">考核方式<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" >
                        <!--<uc1:CodeType ID="ddlCheckWay" runat="server" />-->
                        <asp:TextBox ID="txtCheckWay" runat="server" CssClass="tdinput" MaxLength="25" Width="95%" specialworkcheck="考核方式" ></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle" >考核时间<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <input type="hidden" id="hidTodayDate" runat="server" />
                        <asp:TextBox ID="txtCheckDate" runat="server" ReadOnly="true" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCheckDate')})"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle">填写人</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtFillUserName" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" runat="server" id="hidFillUserID" />
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >培训规划</td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtTrainingPlan" Height="50" runat="server" Width="90%" 
                            MaxLength="500" CssClass="tdinput" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >领导意见</td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtLeadViews" Height="50" runat="server" Width="90%" 
                            MaxLength="500" CssClass="tdinput" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="right" class="tdColTitle" >说明</td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtDescription" Height="50" runat="server" Width="90%" 
                            MaxLength="500" CssClass="tdinput" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>
    <tr>
        <td height="25" valign="top" colspan="2">        
                      
            <table width="100%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">考核结果</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divRsseResult'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblRsseResult','divRsseResult')"/>
                        </div>
                    </td>
                </tr>
            </table>
            
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="100%" border="0" id="tblRsseResult" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td colspan="2">
                        <div id="divRsseResultDetail" runat="server"></div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>
    <tr>
        <td height="25" valign="top" colspan="2">
        
            <table width="100%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">考核总评</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divContactRemark'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblContactRemark','divContactRemark')"/>
                        </div>
                    </td>
                </tr>
            </table>
            
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="100%" border="0" id="tblContactRemark" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">考核总评</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtGeneralComment" runat="server" TextMode="MultiLine" Width="90%" Height="50" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>
    <tr>
        <td height="25" valign="top" colspan="2" >
        
            <table width="100%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">备注</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divRemark'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblRemark','divRemark')"/>
                        </div>
                    </td>
                </tr>
            </table>
            
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="100%" border="0" id="tblRemark" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">考核备注</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtCheckRemark" runat="server" TextMode="MultiLine" Width="90%" Height="50" CssClass="tdinput"></asp:TextBox>
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

