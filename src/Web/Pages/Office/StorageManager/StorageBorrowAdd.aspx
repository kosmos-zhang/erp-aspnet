<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageBorrowAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageBorrowAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/StorageProductList.ascx" TagName="StorageProductList"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Common/StorageSnapshot.ascx" TagName="StorageSnapshot"
    TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建借货申请单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/office/StorageManager/StorageBorrow_Add.js" type="text/javascript"></script>

    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
    var intMasterProductID = <%=intMasterProductID %>;
    var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.BILL_TYPEFLAG_STORAGE %>;
    var glb_BillTypeCode =<%=XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_BORROW %>;
    var glb_BillID = intMasterProductID;//单据ID
    var glb_IsComplete = true;  
    var glbBtn_IsUnConfirm = false;
    var FlowJS_HiddenIdentityID ='borrowid';
    var FlowJS_BillStatus ='ddlBillStatus';
    var FlowJs_BillNo='txtNo';
    </script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

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
<body >
    <form id="frmMain" runat="server">
    <input type="hidden" id="rowlastid" value="0" />
    <input type="hidden" id="lastrownum" value="1" />
    <input type="hidden" id="action" value="ADD" runat="server" />
    <input type="hidden" id="borrowid" value="0" runat="server" />
    <input type="hidden" id="borrowno" value="" runat="server" />
    <input type="hidden" id="txtCurrentUserName" runat="server" />
    <input type="hidden" id="txtCurrentDate" runat="server" />
    <input type="hidden" id="txtIsBack" runat="server" />
    <input type="hidden" id="txtIsMoreUnit" runat="server" />
    <input type="hidden" id="txtProductRowID" />
    <input type="hidden" id="hidSelPoint" runat="server" />
    <input  type="hidden" id="hidIsBatchNo" runat="server"/>
    <uc4:FlowApply ID="FlowApply1" runat="server" />
    <div id="popupContent">
        <uc7:StorageSnapshot ID="StorageSnapshot1" runat="server" />
    </div>
    <div id="divPageMask" style="display: none">
        <iframe id="PageMaskIframe" frameborder="0" width="100%"></iframe>
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="msgError" runat="server" />
    <uc3:StorageProductList ID="StorageProductList1" runat="server" />
    <uc5:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
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
                    新建借货申请单
                </div>
            </td>
        </tr>
        <tr>
            <td height="40" valign="top" colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr style="border: 0;">
                        <td height="30" class="tdColInput" style="border: 0px; border-color: #FFFFFF">
                            <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" style="cursor: pointer;
                                display: none; float: left" id="imgNew" onclick="NewPage();" />
                            <span style="float: left" id="span_btn_save" visible="false" runat="server">
                                <img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;
                                    float: left" id="imgSave" onclick="SubmitData();" />
                                <img src="../../../Images/Button/UnClick_bc.jpg" alt="保存" style="cursor: pointer;
                                    float: left; display: none" id="imgUnSave" /></span> <span id="GlbFlowButtonSpan"
                                        style="float: left" runat="server" visible="false"></span>
                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回列表" style="cursor: pointer;
                                float: left; display: none;" id="imgBack" onclick="backtolsit();" />
                            <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style="float: right;
                                cursor: pointer;" id="imgPrint" onclick="BillPrint();" />
                            <input type="hidden" id="GlbFlowIsDefine" name="GlbFlowIsDefine" value="0" />
                            <!-- 参数设置：是否启用条码 -->
                            <input type="hidden" id="hidCodeBar" runat="server" value="" />
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
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                            <tr>
                                                <td>
                                                    基本信息
                                                </td>
                                                <td align="right">
                                                    <div id='divInterviewInfo'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblInterviewInfo1','divInterviewInfo')"
                                                            alt="展开收起栏目" />
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
                                        借货单编号<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <div runat="server" id="div_InNo_uc">
                                            <uc6:CodingRuleControl ID="txtRuleCodeNo" runat="server" />
                                        </div>
                                        <div id="div_InNo_Lable" runat="server" style="display: none;">
                                            <input type="text" id="txtNo" runat="server" class="tdinput tboxsize" disabled="true"
                                                maxlength="25" />
                                        </div>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货单主题
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="tboxTitle" type="text" runat="server" class="tdinput tboxsize" maxlength="50" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货人<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input type="hidden" id="txtBorrower" />
                                        <input type="text" id="UserBorrower" onclick="alertdiv('UserBorrower,txtBorrower')"
                                            readonly class="tdinput tboxsize" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        借货部门<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="DeptBorrowDeptName" readonly="readonly" onclick="alertdiv('DeptBorrowDeptName,txtBorrowDeptID')" class="tdinput tboxsize" />
                                        <input type="hidden" id="txtBorrowDeptID" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        借货原因
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlBorrowReason" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        借货日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="tboxBorrowTime" runat="server" MaxLength="10" CssClass="tdinput"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('tboxBorrowTime')})" ReadOnly="true"
                                            Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        借出仓库<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlDepot" runat="server" onchange="clearRow();">
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        出库日期<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="tboxOutDate" class="tdinput tboxsize" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('tboxOutDate')})"
                                            readonly="true" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        出库人<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="hidden" id="txtTransactor" />
                                        <input type="text" id="UserTransactor" class="tdinput tboxsize" onclick="alertdiv('UserTransactor,txtTransactor')" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        借出部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="hidden" id="txtOutDept" />
                                        <input type="text" id="DeptOutDept" onclick="alertdiv('DeptOutDept,txtOutDept')"
                                            readonly class="tdinput tboxsize" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                    </td>
                                    <td height="20" class="tdColInput">
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                    </td>
                                    <td height="20" class="tdColInput">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        摘要
                                    </td>
                                    <td height="20" class="tdColInput" colspan="5">
                                        <asp:TextBox ID="tboxSummary" runat="server" MaxLength="20" TextMode="MultiLine"
                                            Width="85%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <uc2:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                            <tr onclick="oprItem('tblTotal','divTotal')" style="cursor: pointer" title="展开收起栏目">
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
                                        借货数量合计
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="tboxTotalCount" type="text" disabled="true" class="tdinput tboxsize" runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货金额合计
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="tboxTotalPrice" type="text" disabled="true" class="tdinput tboxsize" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <div runat="server" id="div3">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                            <tr>
                                                <td>
                                                    备注信息
                                                </td>
                                                <td align="right">
                                                    <div id='divRemark'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblRemark','divRemark')"
                                                            alt="展开收起栏目" />
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
                                        <input id="tboxCreator" type="text" disabled class="tdinput tboxsize" runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        制单日期
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input type="text" value="<%=DateTime.Now.ToString("yyyy-MM-dd")%>" id="tboxCreateDate"
                                            class="tdinput tboxsize" disabled="disabled" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        单据状态
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <asp:DropDownList ID="ddlBillStatus" runat="server" Enabled="false">
                                            <asp:ListItem Text="制单" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="执行" Value="2"></asp:ListItem>
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
                                        <asp:TextBox ID="tboxRemark" runat="server" TextMode="MultiLine" Width="85%" MaxLength="400"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        确认人
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxConfirmor" type="text" runat="server" class="tdinput tboxsize" disabled />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        确认日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxConfirmorDate" disabled type="text" class="tdinput tboxsize" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        结单人
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxCloser" type="text" runat="server" class="tdinput tboxsize" disabled />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        结单日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxCloseDate" type="text" runat="server" class="tdinput tboxsize" disabled />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        最后更新人
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tbxoModifiedUser" type="text" runat="server" class="tdinput tboxsize"
                                            disabled />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        最后更新日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input id="tboxModifiedDate" type="text" runat="server" class="tdinput tboxsize"
                                            disabled />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
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
                                                    借货单明细
                                                </td>
                                                <td align="right">
                                                    <div id='divLendInfo'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('divlendList','divLendInfo')"
                                                            alt="展开收起栏目" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div id="divlendList">
                                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td height="28" bgcolor="#FFFFFF">
                                            <img src="../../../images/Button/Show_add.jpg" style="cursor: hand" onclick="addProductList();"
                                                alt="添加" runat="server" id="btnAddDetailRow" visible="false" />
                                            <img src="../../../images/Button/Show_del.jpg" style="cursor: hand" onclick="DeleteRow();"
                                                alt="删除" runat="server" id="btnDelDetailRow" visible="false" />
                                            <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();"
                                                alt="库存快照" />
                                        </td>
                                    </tr>
                                </table>
                                
                                <table width="99%" border="0" id="tblProductlist" style="behavior: url(../../../draggrid.htc)"
                                    align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                    <tbody>
                                        <tr>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 4%">
                                                选择<input type="checkbox" visible="false" name="checkall" id="checkall" onclick="selall();"
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
                                                现有存量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                借出单价
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                借货数量<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                借货金额
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                预计返还日期<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                预计返还数量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                已返还数量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                                备注
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                            </div>
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
