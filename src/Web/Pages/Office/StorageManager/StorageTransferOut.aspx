<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageTransferOut.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageTransferOut" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>

<%@ Register Src="../../../UserControl/StorageProductList.ascx" TagName="StorageProductList"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/StorageBorrowList.ascx" TagName="StorageBorrowList"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/StorageTransferProduct.ascx" TagName="StorageTransferProduct"
    TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>调拨出库</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>
    <script src="../../../js/office/StorageManager/StorageTransferOut.js" type="text/javascript"></script>

    
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
    <uc7:StorageTransferProduct ID="StorageTransferProduct1" runat="server" />
    <input id="txtAction" value="ADD" type="hidden" runat="server" />
    <input id="txtLastRowID" type="hidden" value="1" />
    <input id="txtLastSortNo" type="hidden" value="1" />
    <input id="txtTransferID" type="hidden" runat="server" />
    <input type="hidden" id="txtIsMoreUnit" runat="server" />
        <input  type="hidden" id="hidIsBatchNo" runat="server"/>
            <input type="hidden" id="hidSelPoint" runat="server" />
    <uc4:FlowApply ID="FlowApply1" runat="server" />
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="msgError" runat="server" />
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
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
                    新建库存调拨单
                </div>
            </td>
        </tr>
        <tr>
            <td height="40" valign="top" colspan="2">
                <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="30" class="tdColInput" style="width: 100%">
                        <span runat="server" visible="false" id="img_btn_confirmOUT">
                            <img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确认" onclick="Save();"
                                style="cursor: pointer; float:left" id="imgConfirm" />
                            <img src="../../../Images/Button/UnClick_qr.jpg" alt="确认" id="imgUnConfirm" style="float: left;
                                display: none;" /></span>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回列表" style="cursor: pointer;
                                float: left;" id="imgBack" onclick="backtolsit();" />
                            <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style="cursor: pointer; float:right;  display:none;"
                                id="imgPrint"  />
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
                            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td>
                                                    基本信息
                                                </td>
                                                <td align="right">
                                                    <div id='divInterviewInfo'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblInterviewInfo','divInterviewInfo')" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblInterviewInfo" style="display: block">
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨单编号
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <div runat="server" id="div_InNo_uc">
                                            <uc6:CodingRuleControl ID="txtRuleCodeNo" runat="server" />
                                        </div>
                                        <div id="div_InNo_Lable" runat="server" style="display: none;">
                                            <input type="text" id="txtNo" runat="server" class="tdinput tboxsize" disabled="true" />
                                        </div>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨单主题
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtTitle" type="text" runat="server" class="tdinput tboxsize"  disabled="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨申请人
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input type="text" id="UserApplyUser" readonly class="tdinput tboxsize" />
                                        <input type="hidden" id="txtApplyUserID" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        要货部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="DeptApplyDept" class="tdinput tboxsize" disabled="true" />
                                        <input type="hidden" id="txtApplyDeptID" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        调入仓库
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlInStorageID" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        要求到货日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtRequireInDate" class="tdinput tboxsize" disabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        调拨原因
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlReasonType" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        调货部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="DeptOutDeptID" class="tdinput tboxsize" disabled="true" />
                                        <input type="hidden" id="txtOutDeptID" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        调出仓库
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlOutStorageID" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        业务状态
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlBusiStatus" runat="server" Enabled="false">
                                            <asp:ListItem Value="1">调拨申请</asp:ListItem>
                                            <asp:ListItem Value="2">调拨出库</asp:ListItem>
                                            <asp:ListItem Value="3">调拨入库</asp:ListItem>
                                            <asp:ListItem Value="4">调拨完成</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        出库人<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="UsertxtOutUserID" type="text" class="tdinput tboxsize" readonly onclick="alertdiv('UsertxtOutUserID,txtOutUserID')" />
                                        <input id="txtOutUserID" type="hidden" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        出库日期<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="txtOOutDate" type="text" class="tdinput tboxsize" readonly onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOOutDate')})" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        摘要
                                    </td>
                                    <td height="20" class="tdColInput" colspan="5">
                                        <asp:TextBox ID="tboxSummary" runat="server" MaxLength="250" TextMode="MultiLine"
                                            Enabled="false" Width="85%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
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
                            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblTotal" style="display: block">
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨数量合计
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtTotalCount" type="text" disabled="true" class="tdinput tboxsize" runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨金额合计
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtTotalPrice" type="text" disabled="true" class="tdinput tboxsize" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨费用
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input id="txtTransferFeeSum" type="text" class="tdinput tboxsize" value="0" disabled="true"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        出库数量合计
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtOutCount" type="text" readonly class="tdinput tboxsize" runat="server" disabled="true"  />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        出库金额合计
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtOutFeeSum" type="text" readonly class="tdinput tboxsize"  disabled="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                    </td>
                                </tr>
                            </table>
                            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
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
                            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblRemark" style="display: block">
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
                                            class="tdinput tboxsize"  disabled="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        单据状态
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <asp:DropDownList ID="ddlBillStatus" runat="server" Enabled="false" class="tdinput">
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
                                            Width="85%"></asp:TextBox>
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
                            <table width="98%" align="center">
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
                            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                            <tr>
                                                <td>
                                                    调拨单明细
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
                            <div id="divlendList" style="display: block;">
                                <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                    <tr style="display: none;">
                                        <td height="28" bgcolor="#FFFFFF">
                                            <img src="../../../images/Button/Show_add.jpg" width="34" height="24" style="cursor: hand"
                                                onclick="AddRow();" alt="添加" />
                                            <img src="../../../images/Button/Show_del.jpg" width="34" height="24" style="cursor: hand"
                                                id="imgDelReturnDetail" onclick="RemoveRow();" alt="删除" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" id="tblTransfer" style="behavior: url(../../../draggrid.htc)"
                                    align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                    <tbody>
                                        <tr>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                选择<input type="checkbox" visible="false" name="checkall" id="chkDetail" onclick="selall();"
                                                    value="checkbox" />
                                            </td>
                                            <td height="20" align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                序号
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                物品编号<span class="redbold">*</span>
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
                                            
                                            <!--计量单位-->
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicUnitTd">
                                                基本单位
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" runat="server" id="BasicNumTd">
                                                基本数量
                                            </td>
                                            <!--#计量单位-->
                                            
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                单位
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                调拨单价
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                调拨数量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                调拨金额
                                            </td>
                                          <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                               出库数量<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                出库金额
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" style="width: 1%; display: none" class="ListTitle">
                                                备注
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
                <!-- </div> -->
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
