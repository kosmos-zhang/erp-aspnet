<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmplApply_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_EmplApply_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建调职申请单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/EmplApply_Edit.js" type="text/javascript"></script>
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
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建调职申请单</div></td>
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
                                    <span id="GlbFlowButtonSpan" runat="server" visible="false"></span>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="false" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand"  />
                                </td>
                                <td align="right" class="tdColInput">
                                    <img src="../../../Images/Button/Main_btn_print.jpg"   alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand" />
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
                        <input type="hidden" id="hidModuleID" runat="server" />
                        <input type="hidden" id="hidSearchCondition" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                <tr>
                    <td height="20" class="tdColTitle" >调职申请编号<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <div id="divCodeRule" runat="server">
                            <uc1:CodingRule ID="codeRule" runat="server" />
                        </div>
                        <%--<div id="divCodeNo" runat="server" class="tdinput" style="display:none"></div>--%>
                        <asp:TextBox ID="divCodeNo" CssClass="tdinput" Enabled="false" runat="server"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle">主题<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="txtTitle" MaxLength="50" runat="server" CssClass="tdinput"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle" >申请人<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" >
                        <asp:TextBox ID="UserApply" onclick="alertdiv('UserApply,hidUserApply');"  ReadOnly="true" Width="95%" runat="server" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="hidUserApply" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">申请日期<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtApplyDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtApplyDate')})"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle">希望日期</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtHopeDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtHopeDate')})"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle">入职时间<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtEnterDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDate')})"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">目前部门<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="DeptNow" MaxLength="250"  onclick="alertdiv('DeptNow,hidDeptNow');"  ReadOnly="true" Width="95%" runat="server" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="hidDeptNow" runat="server" />
                    </td>
                    <td height="20" class="tdColTitle">目前岗位<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:DropDownList ID="ddlNowQuarter" runat="server" CssClass="tdinput"></asp:DropDownList>
                    </td>
                    <td height="20" class="tdColTitle">目前岗位职等<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <uc1:CodeType ID="ctNowQuaterAdmin" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">调至部门<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="DeptNew" onclick="alertdiv('DeptNew,hidDeptNew');"  ReadOnly="true" Width="95%" runat="server" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="hidDeptNew" runat="server" />
                    </td>
                    <td height="20" class="tdColTitle">调至岗位<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <asp:DropDownList ID="ddlNewQuarter" runat="server" CssClass="tdinput"></asp:DropDownList>
                    </td>
                    <td height="20" class="tdColTitle">调至岗位职等<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <uc1:CodeType ID="ctNewQuaterAdmin" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">目前工资</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtNowWage" runat="server" MaxLength="10" Width="80%"  CssClass="tdinput"></asp:TextBox>元/月
                    </td>
                    <td height="20" class="tdColTitle">调至工资</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtNewWage" runat="server" MaxLength="10" Width="80%"  CssClass="tdinput"></asp:TextBox>元/月
                    </td>
                    <td height="20" class="tdColTitle">申报类别</td>
                    <td height="20" class="tdColInput">
                        <asp:TextBox ID="txtApplyType" MaxLength="25" Width="95%" runat="server" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">事由<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtReason" runat="server" CssClass="tdinput" MaxLength="100" Width="85%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">备注</td>
                    <td height="20" class="tdColInput" colspan="5">
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="tdinput" MaxLength="100" Width="85%"></asp:TextBox>
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
 <!-- 单据状态 -->
<input type="hidden" id="hiddenBillStatus" runat="server" value="1" />
<!-- 单据编号 -->
<input type="hidden" id="hidBillNo"  runat="server"/>
<!-- Start 流程处理-->
<uc1:FlowApply ID="FlowEmplApply" runat="server" />
<!-- End 流程处理-->
<input type="hidden" id="hidIdentityID" value="0" runat="server" />
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
</form>
<script type="text/javascript">
    var glb_BillTypeFlag = <%=XBase.Common.ConstUtil.CODING_TYPE_HUMAN %>;
    var glb_BillTypeCode = <%=XBase.Common.ConstUtil.CODING_HUMAN_ITEM_EMPLAPPLY %>;
    var glb_BillID = document.getElementById("hidIdentityID").value;                                //单据ID
    var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
    var FlowJS_HiddenIdentityID ='hidIdentityID';                      //自增长后的隐藏域ID
    var FlowJs_BillNo ='hidBillNo';          //当前单据编码名称
    var FlowJS_BillStatus ='hiddenBillStatus';                             //单据状态ID
//    GetFlowButton_DisplayControl();
</script>
<script src="../../../js/common/Flow.js" type="text/javascript"></script>
</body>
</html>