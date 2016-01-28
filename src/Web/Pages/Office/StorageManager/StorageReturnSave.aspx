<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageReturnSave.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageReturnSave" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/StorageProductList.ascx" TagName="StorageProductList"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/StorageBorrowList.ascx" TagName="StorageBorrowList"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc6" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建借货返还单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/office/StorageManager/StorageReturnSave.js" type="text/javascript"></script>

    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

    <style type="text/css">
        .tboxsize
        {
            width: 90%;
            height: 99%;
        }
        .textAlign
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <input id="action" value="ADD" type="hidden" runat="server" />
    <input id="txtReturnID" value="" type="hidden" runat="server" />
    <input id="txtBillStatus" value="" type="hidden" />
    <input id="txtFromBillID" value="" type="hidden" />
    <input id="txtPara" value="" type="hidden" runat="server" />
    <input id="txtPageIndex" value="" type="hidden" runat="server" />
    <input id="txtCurrentDate" type="hidden" runat="server" />
    <input id="txtCurrentUserID" type="hidden" runat="server" />
    <input id="txtCurrentUserName" type="hidden" runat="server" />
    <input id="txtIsBack" runat="server" type="hidden" />
    <input id="txtIsMoreUnit" runat="server" type="hidden" />
    <input type="hidden" id="hidSelPoint" runat="server" />
    <uc4:FlowApply ID="FlowApply1" runat="server" />
    <div id="divPageMask" style="display: none">
        <iframe id="PageMaskIframe" frameborder="0" width="100%"></iframe>
    </div>
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc5:StorageBorrowList ID="StorageBorrowList1" runat="server" />
    <uc1:Message ID="msgError" runat="server" />
    <table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" align="center" colspan="2" class="Title">
                <div id="divTitle" runat="server">
                    新建借货返还单
                </div>
            </td>
        </tr>
        <tr>
            <td height="40" valign="top" colspan="2">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="30" class="tdColInput" style="width: 100%">
                            <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" style="cursor: pointer;
                                float: left" id="imgNew" onclick="NewPage();" />
                            <span id="span_btn_save" runat="server" visible="false">
                                <img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;
                                    float: left" id="imgSave" onclick="Save();" />
                                <img src="../../../Images/Button/UnClick_bc.jpg" alt="保存" style="cursor: pointer;
                                    float: left; display: none" id="imgUnSave" />
                            </span><span runat="server" visible="false" id="span_btn_confirm">
                                <img src="../../../Images/Button/UnClick_qr.jpg" alt="确认" style="cursor: pointer;
                                    float: left;" id="imgUnConfirm" />
                                <img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确认" style="cursor: pointer;
                                    float: left;" id="imgConfirm" onclick="Fun_ConfirmOperate()" /></span> <span id="span_btn_close"
                                        runat="server" visible="false">
                                        <img src="../../../Images/Button/Button_jd.jpg" alt="结单" style="cursor: pointer;
                                            float: left;" id="imgUnClose" />
                                        <img src="../../../Images/Button/Main_btn_Invoice.jpg" alt="结单" style="cursor: pointer;
                                            float: left; display: none;" id="imgClose" onclick="Fun_CompleteOperate(true);" />
                                    </span><span id="span_btn_unclose" runat="server" visible="false">
                                        <img src="../../../Images/Button/Button_qxjd.jpg" alt="取消结单" style="cursor: pointer;
                                            float: left;" id="imgUnReopen" />
                                        <img src="../../../Images/Button/Main_btn_qxjd.jpg" alt="取消结单" style="cursor: pointer;
                                            float: left; display: none;" id="imgReopen" onclick="Fun_CompleteOperate(false);" /></span>
                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回列表" style="cursor: pointer;
                                float: left;" id="imgBack" onclick="backtolsit();" />
                            <img src="../../../Images/Button/Bottom_btn_Yd.jpg" alt="源单总览" style="cursor: pointer;
                                float: left; display: none;" id="imgSourceView" onclick="getStorageBorrowList();" />
                            <span id="GlbFlowButtonSpan" style="float: left"></span>
                            <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style="cursor: pointer;
                                float: right" id="imgPrint" onclick="PrintBill();" />
                            <input type="hidden" id="GlbFlowIsDefine" name="GlbFlowIsDefine" value="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
                    <tr>
                        <td colspan="2">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td>
                                                    基本信息
                                                </td>
                                                <td align="right">
                                                    <div id='divInterviewInfo'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblInterviewInfo1','divInterviewInfo')" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblInterviewInfo1">
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货返还单编号<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <div runat="server" id="div_InNo_uc">
                                            <uc6:CodingRuleControl ID="txtRuleCodeNo" runat="server" />
                                        </div>
                                        <div id="div_InNo_Lable" runat="server" style="display: none;">
                                            <input type="text" id="txtRerunNo" runat="server" class="tdinput tboxsize" disabled="true"
                                                maxlength="25" />
                                        </div>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货返还单主题
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtReturnTitle" type="text" runat="server" class="tdinput tboxsize" onblur="checkControlValue('txtReturnTitle','主题')"
                                            maxlength="50" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        源单类型
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <asp:DropDownList ID="ddlFromType" runat="server" Enabled="false">
                                            <asp:ListItem Value="1">借货单</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        借货单<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtBorrowNo" class="tdinput tboxsize" readonly onclick="getStorageBorrowList();" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        借货部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtBorrowDept" class="tdinput tboxsize" readonly />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        借货人
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtBorrower" class="tdinput tboxsize" readonly />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        借货日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtBorrowDate" class="tdinput tboxsize" readonly />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        被借部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtOutDept" class="tdinput tboxsize" readonly />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        返还仓库
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlStorage" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        返还人<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="UserReturner" type="text" class="tdinput tboxsize" onclick="alertdiv('UserReturner,txtReturnerID')"
                                            readonly />
                                        <input type="hidden" id="txtReturnerID" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        返还时间
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtReturnDate" value='<%=DateTime.Now.ToString("yyyy-MM-dd") %>'
                                            class="tdinput tboxsize" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        返还部门<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="DeptReturnDept" onclick="alertdiv('DeptReturnDept,txtReturnDeptID')"
                                            class="tdinput tboxsize" />
                                        <input type="hidden" id="txtReturnDeptID" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        入库人<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="UserTransactor" onclick="alertdiv('UserTransactor,txtTransactor')"
                                            class="tdinput tboxsize" />
                                        <input type="hidden" id="txtTransactor" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                    </td>
                                    <td height="20" class="tdColInput">
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        &nbsp;
                                    </td>
                                    <td height="20" class="tdColInput">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        摘要
                                    </td>
                                    <td height="20" class="tdColInput" colspan="5">
                                        <asp:TextBox ID="tboxSummary" runat="server" MaxLength="100" TextMode="MultiLine"
                                            Width="85%" onblur="checkControlValue('tboxSummary','摘要')"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <uc2:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td>
                                                    合计信息
                                                </td>
                                                <td align="right">
                                                    <div id='divTotal'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblTotal','divTotal')" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblTotal">
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        返还数量合计
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtTotalCount" type="text" disabled="true" class="tdinput tboxsize" runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        返还金额合计
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtTotalPrice" type="text" disabled="true" class="tdinput tboxsize" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                            <tr onclick="oprItem('tblRemark','divRemark')" title="展开收起栏目" style="cursor: pointer">
                                                <td>
                                                    备注信息
                                                </td>
                                                <td align="right">
                                                    <div id='divRemark'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblRemark','divRemark')" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblRemark">
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        制单人
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="tboxCreator" type="text" disabled="true" class="tdinput tboxsize" runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        制单日期
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input type="text" value="<%=DateTime.Now.ToString("yyyy-MM-dd")%>" id="tboxCreateDate"
                                            class="tdinput tboxsize" disabled="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        单据状态
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <asp:DropDownList ID="ddlBillStatus" runat="server" Enabled="false">
                                            <asp:ListItem Text="制单" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="执行" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="变更" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="手工结单" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="自动结单" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        备注
                                    </td>
                                    <td height="20" class="tdColInput" colspan="5">
                                        <asp:TextBox ID="tboxRemark" runat="server" MaxLength="250" TextMode="MultiLine"
                                            Width="85%" onblur="checkControlValue('txtReturnTitle','备注')"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        确认人
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxConfirmor" type="text" runat="server" class="tdinput tboxsize" disabled="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        确认日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxConfirmorDate" readonly type="text" class="tdinput tboxsize" disabled="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        结单人
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxCloser" type="text" runat="server" class="tdinput tboxsize" disabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        结单日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxCloseDate" type="text" runat="server" class="tdinput tboxsize" disabled="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        最后更新人
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tbxoModifiedUser" type="text" runat="server" class="tdinput tboxsize"
                                            disabled="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        最后更新日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxModifiedDate" type="text" runat="server" class="tdinput tboxsize"
                                            disabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td valign="top">
                                        <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                                    </td>
                                    <td align="right" valign="top" style="width: auto">
                                    </td>
                                    <td width="8">
                                        <img src="../../../images/Main/LineR.jpg" width="122" height="7" style="float: right" />
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                            <tr>
                                                <td>
                                                    借货返还单明细
                                                </td>
                                                <td align="right">
                                                    <div id='divLendInfo'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('divlendList','divLendInfo')" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div id="divlendList">
                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td height="28" bgcolor="#FFFFFF">
                                            <img src="../../../images/Button/Show_del.jpg" style="cursor: hand; float: left"
                                                id="imgDelReturnDetail" onclick="DelRow();" alt="删除" runat="server" visible="false" />
                                            <img src="../../../images/Button/Bottom_btn_From.jpg" style="cursor: hand; float: left"
                                                id="btnChooseDetail" alt="选择明细" onclick="getStorageBorrowList();" runat="server" visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="99%" border="0" id="tblBorrowDetailList" style="behavior: url(../../../draggrid.htc)"
                                    align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                    <tbody>
                                        <tr>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                选择<input type="checkbox" visible="false" name="checkall" id="chkReturnDetail" onclick="selall();"
                                                    value="checkbox" />
                                            </td>
                                            <td height="20" align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                序号
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                物品编号
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                物品名称
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                            批次
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                规格
                                            </td>
                                             <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicUnitTd">
                                                基本单位
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicNumTd">
                                                基本数量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                单位
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                应还数量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                已返还数量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                实还数量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                返还单价
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                返还金额
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                                备注
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle"">
                                                借货单编号
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                源单行号
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <table>
                                <tr>
                                    <td colspan="2" height="4">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
