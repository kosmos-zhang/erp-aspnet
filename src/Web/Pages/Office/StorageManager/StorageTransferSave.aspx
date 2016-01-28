<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageTransferSave.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageTransferSave" %>

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
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc8" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc2" %>
<%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc9" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建库存调拨单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
   
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

    <script language="javascript" type="text/javascript">
    var intMasterProductID = <%=intMasterProductID %>;
    var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.BILL_TYPEFLAG_STORAGE %>;
    var glb_BillTypeCode =<%=XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_TRANSFER %>;
    var glb_BillID = intMasterProductID;//单据ID
    var glb_IsComplete = true;  
    var FlowJS_HiddenIdentityID ='txtTransferID';
    var FlowJS_BillStatus ='ddlBillStatus';
    var FlowJs_BillNo='txtNo';
    var glbtn_IsClose=true;
    
    </script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageTransferSave.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmMain" runat="server">
    <uc7:StorageTransferProduct ID="StorageTransferProduct1" runat="server" />
    <input type="hidden" id="txtIsMoreUnit" runat="server" />
    <input id="txtAction" value="ADD" type="hidden" runat="server" />
    <input id="txtLastRowID" type="hidden" value="1" />
    <input id="txtLastSortNo" type="hidden" value="1" />
    <input id="txtTransferID" type="hidden" runat="server" value="0" />
    <input id="txtCurrentDate" type="hidden" runat="server" />
    <input id="txtCurrentUserID" type="hidden" runat="server" />
    <input id="txtCurrentUserName" type="hidden" runat="server" />
    <input id="txtFlowStatus" type="hidden" runat="server" />
    <input id="txtIsBack" type="hidden" runat="server" />
    <input id="txtProductRowID" type="hidden" runat="server" />
    <input type="hidden" id="hidSelPoint" runat="server" />
     <input  type="hidden" id="hidIsBatchNo" runat="server"/>
    <uc4:FlowApply ID="FlowApply1" runat="server" />
    <div id="popupContent">
        <uc9:StorageSnapshot ID="StorageSnapshot1" runat="server" />
    </div>
    <div id="divPageMask" style="display: none">
        <iframe id="PageMaskIframe" frameborder="0" width="100%"></iframe>
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="msgError" runat="server" />
    <uc8:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
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
                    新建库存调拨单
                </div>
            </td>
        </tr>
        <tr>
            <td height="40" valign="top" colspan="2">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="30" class="tdColInput" style="width: 100%">
                            <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" style="cursor: pointer;
                                display: none; float: left" id="imgNew" onclick="NewPage();" />
                            <span id="span_btn_save" runat="server" visible="false">
                                <img src="../../../Images/Button/UnClick_bc.jpg" alt="保存" style="cursor: pointer;
                                    float: left; display: none" id="imgUnClickSave" />
                                <img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;
                                    float: left" id="imgSave" onclick="Save();" /></span> <span id="GlbFlowButtonSpan"
                                        style="float: left" visible="false" runat="server"></span>
                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回列表" style="cursor: pointer;
                                float: left;" id="imgBack" onclick="backtolsit();" />
                            <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style="cursor: pointer;
                                float: right" id="imgPrint" onclick="BillPrint();" />
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
                <!-- <div style="height:500px;overflow-y:scroll;"> -->
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
                                        调拨单编号<span class="redbold">*</span>
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
                                        <input id="txtTitle" type="text" runat="server" class="tdinput tboxsize" maxlength="50"
                                            specialworkcheck="调拨单主题" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨申请人<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input type="text" id="UserApplyUser" onclick="alertdiv('UserApplyUser,txtApplyUserID')"
                                            readonly class="tdinput tboxsize" />
                                        <input type="hidden" id="txtApplyUserID" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        要货部门<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="DeptApplyDept" class="tdinput tboxsize" readonly onclick="alertdiv('DeptApplyDept,txtApplyDeptID')" />
                                        <input type="hidden" id="txtApplyDeptID" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        调入仓库<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlInStorageID" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        要求到货日期<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtRequireInDate" class="tdinput tboxsize" readonly onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtRequireInDate')})" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        调拨原因
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlReasonType" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        调货部门<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="DeptOutDeptID" class="tdinput tboxsize" onclick="alertdiv('DeptOutDeptID,txtOutDeptID')"
                                            readonly />
                                        <input type="hidden" id="txtOutDeptID" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        调出仓库<span class="redbold">*</span>
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlOutStorageID" runat="server" onchange="clearDetail();">
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
                                        <asp:TextBox ID="tboxSummary" runat="server" MaxLength="250" TextMode="MultiLine"
                                            Width="85%"></asp:TextBox>
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
                                        <input id="txtTransferFeeSum" type="text" class="tdinput tboxsize" value="0" onchange="Number_round(this,<%=UserInfo.SelPoint %>)" />
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
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
                            <table width="99%" align="center">
                                <tr>
                                    <td valign="top">
                                        <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                                    </td>
                                    <td align="right" valign="top" style="width: auto">
                                    </td>
                                    <td>
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
                            <div id="divlendList">
                                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td height="28" bgcolor="#FFFFFF">
                                            <img src="../../../images/Button/Show_add.jpg" style="cursor: hand" onclick="AddRow();"
                                                alt="添加" id="btnAddDetailRow" runat="server" visible="false" />
                                            <img src="../../../images/Button/Show_del.jpg" style="cursor: hand" id="imgDelReturnDetail"
                                                onclick="RemoveRow();" alt="删除" runat="server" visible="false" />
                                            <img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods" style="cursor: pointer;display:none;" onclick="Showtmsm()" runat="server" visible="false" /> 
                                           
                                           <img  id="btnSubSnapshot" alt="库存快照" onclick="ShowSnapshot();"  src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="99%" border="0" id="tblTransfer" style="behavior: url(../../../draggrid.htc)"
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
                                                调拨数量<span class="redbold">*</span>
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                调拨金额
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                已出库数量
                                            </td>
                                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                                已入库数量
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
