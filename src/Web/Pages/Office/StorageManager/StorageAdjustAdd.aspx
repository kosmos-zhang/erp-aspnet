<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageAdjustAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageAdjustAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/ProductInfoBatchControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc4" %>
<%@ Register src="../../../UserControl/GetGoodsInfoByBarCode.ascx" tagname="GetGoodsInfoByBarCode" tagprefix="uc5" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc6" %>
<%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建日常调整单</title>
 <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/DeleteFile.js" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageAdjustAdd.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script type="text/javascript">
    var intMasterProductID ='<%=intMasterProductID %>';
    var glb_BillTypeFlag ='<%=XBase.Common.ConstUtil.CODING_RULE_Storage_NO %>';
    var glb_BillTypeCode ='<%=XBase.Common.ConstUtil.CODING_RULE_StoAdjust_NO %>';

    var glb_BillID = intMasterProductID;//单据ID 
    var FlowJS_HiddenIdentityID ='hiddenAdjustID';
     var glb_IsComplete = true;    
     var glbBtn_IsUnConfirm = false; 
    var FlowJS_BillStatus ='ddlBillStatus';
    var FlowJs_BillNo='lbInfoNo';
   function SelectSubAll()
   {
       var chk=document.getElementsByName('Chk');
       for(var i=0;i<chk.length;i++)
       {
          if(chk[i].checked)
          {
            chk[i].checked=false;
          }
          else
          {
            chk[i].checked=true;
          }
       }
   }
   function PrintAdjust()
   {
        var AdjustID=document.getElementById('hiddenAdjustID').value;
         if (AdjustID == "" || AdjustID == "0") {
        popMsgObj.Show("打印|", "请保存单据再打印|");
        return;
    }
        if(parseFloat(AdjustID)>0)
        {
//            if(!confirm('请确认您的单据已保存？'))
//            {
//                return false;
//            }
            window.open("../../../Pages/PrinttingModel/StorageManager/PrintStorageAdjust.aspx?ID="+AdjustID);
        }
   }
     function BackToPage()
    {
        requestQuaobj = GetRequest();   
        var intFromType=requestQuaobj['intFromType'];
        var moduleID=requestQuaobj['ListModuleID'];
        if(intFromType!=null)
        {
            if(intFromType!=''  && intFromType=='2')
            {
                location.href='../../../DeskTop.aspx';
            }
            if(intFromType!='' && intFromType=='3')
            {
                location.href='../../../Pages/Personal/WorkFlow/FlowMyApply.aspx?ModuleID='+moduleID;
            }
            if(intFromType!='' && intFromType=='4')
            {
                location.href='../../../Pages/Personal/WorkFlow/FlowMyProcess.aspx?ModuleID='+moduleID;
            }
            if(intFromType!='' && intFromType=='5')
            {
                location.href='../../../Pages/Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID='+moduleID;
            }
        }
        else
        {
            history.back();
        }
    }
    function SearchPro()
    {
        var ID=document.getElementById('ddlInStorage').value;
        if(parseFloat(ID)<1)
        {
              popMsgObj.ShowMsg('请先选择一个仓库！');
              return false;
        }
        GetGoodsInfoByBarCode(ID);
    }
    </script>

    <style type="text/css">
        #Select1
        {
            width: 88px;
        }
        #TextArea1
        {
            width: 853px;
        }
        #txt_Remark
        {
            width: 595px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <uc5:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <input id="HiddenPoint" type="hidden" runat="server" />
    <input id="HiddenMoreUnit" type="hidden" runat="server" />
    <uc4:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc7:StorageSnapshot ID="StorageSnapshot1" runat="server" />
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <input id="hiddenAdjustID" type="hidden" value="0" />
    <!--本单据的ID-->
    <!-- Start 消息提示-->
    <uc1:Message ID="Message1" runat="server" />
    <!-- End 消息提示-->
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
                
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <% if (this.AdjustID > 0)
                               { %>
                            日常调整单
                            <%}
                               else
                               { %>
                            新建日常调整单
                            <%} %>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <img id="btnSave" runat="server" visible="false" src="../../../images/Button/Bottom_btn_save.jpg"
                                                        onclick="Fun_Save_Adjust();" style="cursor: pointer" title="保存日常调整单" />
                                                    <img runat="server" visible="false" id="UnbtnSave" src="../../../Images/Button/UnClick_bc.jpg"
                                                        style="cursor: pointer; display: none" />
                                                </td>
                                                <td>
                                                    <span runat="server" visible="false" id="GlbFlowButtonSpan"></span>
                                                </td>
                                                <td>
                                                    <img style="display: none" onclick="BackToPage();" id="btnReturn" src="../../../images/Button/Bottom_btn_back.jpg"
                                                        border="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right">
                                        <img onclick="PrintAdjust();" id="btnPrint" src="../../../images/Button/Main_btn_print.jpg"
                                            style="cursor: pointer" title="打印" />
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <uc3:FlowApply ID="FlowApply1" runat="server" />
                            <!-- End 流程处理-->
                            <input type="hidden" id="txtIdentityID" value="0" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <td>
                                    基本信息
                                </td>
                                <td align="right">
                                    <div id='searchClick'>
                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
        id="Tb_01">
        <tr>
            <td height="20" align="right" class="tdColTitle" width="10%">
                单据编号<span class="redbold">*</span>
            </td>
            <td height="20" class="tdColInput" width="23%">
                <div id="divCodeRuleUC" runat="server">
                    <!-- Start Code Rule -->
                    <uc2:CodingRuleControl ID="checkNo" runat="server" />
                    <!-- End Code Rule -->
                </div>
                <div id="divTaskNo" style="display: none" class="tdinput">
                    <input id="lbInfoNo" style="width: 230px" class="tdinput" readonly runat="server"
                        type="text" />
                </div>
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
                单据标题
            </td>
            <td height="20" class="tdColInput" width="23%">
                <asp:TextBox ID="txtSubject" MaxLength="50" runat="server" CssClass="tdinput" Width="93%"
                    SpecialWorkCheck="单据标题"></asp:TextBox>
            </td>
            <td class="tdColTitle" width="10%">
                经办人<span class="redbold">*</span>
            </td>
            <td class="tdColInput" width="24%">
                <input id="UserExecutor" readonly onclick="alertdiv('UserExecutor,hiddentxtExecutor');"
                    class="tdinput" type="text" />
                <input type="hidden" id="hiddentxtExecutor" value="0" />
            </td>
        </tr>
        <tr>
            <td height="20" align="right" class="tdColTitle" width="10%">
                调整部门<span class="redbold">*</span>
            </td>
            <td height="20" class="tdColInput" width="23%">
                <input type="text" id="DeptID" class="tdinput" readonly width="95%" onclick="alertdiv('DeptID,hiddenDeptID');" />
                <input type="hidden" id="hiddenDeptID" runat="server" />
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
                调整仓库<span class="redbold">*</span>
            </td>
            <td height="20" class="tdColInput" width="23%">
                <asp:DropDownList ID="ddlInStorage" runat="server">
                </asp:DropDownList>
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
                调整原因<span class="redbold">*</span>
            </td>
            <td height="20" class="tdColInput" width="24%">
                <asp:DropDownList ID="ddlReasonType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="20" align="right" class="tdColTitle" width="10%">
                调整日期
            </td>
            <td height="20" class="tdColInput" width="23%">
                <input onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('AdjustDate')})" readonly
                    runat="server" id="AdjustDate" type="text" class="tdinput" />
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
                摘要
            </td>
            <td height="20" class="tdColInput" width="23%">
                <asp:TextBox SpecialWorkCheck="摘要" ID="Summary" runat="server" CssClass="tdinput"
                    Width="95%"></asp:TextBox>
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
            </td>
            <td height="20" class="tdColInput" width="24%">
            </td>
        </tr>
    </table>
    <uc6:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
        <tr>
            <td height="20" bgcolor="#F4F0ED" class="Blue">
                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            合计信息
                        </td>
                        <td align="right">
                            <div id='divButtonTotal'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','divButtonTotal')" /></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
        id="Tb_02">
        <tr>
            <td height="20" align="right" class="tdColTitle" width="10%">
                调整数量合计
            </td>
            <td height="20" class="tdColInput" width="23%">
                <input id="TotalPrice" onchange="Number_round(this,2);" type="text" value="0" disabled
                    class="tdinput" width="95%" />
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
                调整金额合计
            </td>
            <td height="20" class="tdColInput" width="23%">
                <asp:TextBox onchange="Number_round(this,2);" ID="CountTotal" Text="0" Enabled="false"
                    CssClass="tdinput" runat="server"></asp:TextBox>
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
            </td>
            <td height="20" class="tdColInput" width="24%">
                <asp:TextBox ID="tbUnit" runat="server" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                <input type="hidden" id="hiddentbUnit" value="0" runat="server" />
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
        <tr>
            <td height="20" bgcolor="#F4F0ED" class="Blue">
                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            备注信息
                        </td>
                        <td align="right">
                            <div id='divInfo'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','divInfo')" /></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
        id="Tb_04">
        <tr>
            <td class="tdColTitle" width="10%">
                单据状态
            </td>
            <td class="tdColInput" width="23%">
                <%--<select disabled id="ddlBillStatus">
                                    <option selected  Value="1">制单</option>
                                       <option  Value="2">执行</option>
                                      <option  Value="3">变更</option>
                                      <option  Value="4">手工结单</option>
                                       <option  Value="5">自动结单</option>
                                </select>--%><input id="tbBillStatus" disabled class="tdinput" value="制单" type="text" /><input
                                    id="ddlBillStatus" type="hidden" value="1" />
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
                制单人
            </td>
            <td height="20" class="tdColInput" width="23%">
                <asp:TextBox ID="tbCreater" Enabled="false" runat="server" CssClass="tdinput"></asp:TextBox>
                <input type="hidden" id="txtCreator" name="txtCreator" class="tdinput" value="0"
                    runat="server" readonly />
            </td>
            <td height="20" align="right" class="tdColTitle" width="10%">
                制单日期
            </td>
            <td height="20" class="tdColInput" width="24%">
                <asp:TextBox ID="txtCreateDate" MaxLength="50" runat="server" CssClass="tdinput"
                    disabled Width="95%" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="20" align="right" class="tdColTitle">
                确认人
            </td>
            <td height="20" class="tdColInput">
                <input type="text" style="display: none" id="txtConfirmor" name="txtConfirmor" class="tdinput"
                    runat="server" disabled />
                <input type="hidden" id="txtConfirmorReal" name="txtConfirmorReal" class="tdinput"
                    value="0" runat="server" disabled />
            </td>
            <td height="20" align="right" class="tdColTitle">
                确认日期
            </td>
            <td height="20" align="left" class="tdinput">
                <input id="txtConfirmDate" style="display: none" maxlength="50" runat="server" class="tdinput"
                    disabled width="95%" />
            </td>
            <td class="tdColTitle">
                结单人
            </td>
            <td class="tdColInput">
                <input type="hidden" id="txtCloser" name="txtCloser" value="0" class="tdinput" runat="server"
                    readonly />
                <input type="text" id="txtCloserReal" style="display: none" name="txtCloserReal"
                    class="tdinput" runat="server" disabled />
            </td>
        </tr>
        <tr>
            <td height="20" align="right" class="tdColTitle">
                结单日期
            </td>
            <td height="20" align="left" class="tdColInput">
                <input type="text" style="display: none" id="txtCloseDate" maxlength="50" runat="server"
                    class="tdinput" width="95%" disabled />
            </td>
            <td height="20" align="right" class="tdColTitle">
                最后更新人
            </td>
            <td height="20" align="left" class="tdColInput">
                <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                    Width="95%" disabled Text=""></asp:TextBox><input type="hidden" id="hiddenModifiedUserID" />
            </td>
            <td class="tdColTitle">
                最后更新日期
            </td>
            <td class="tdColInput">
                <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                    Width="95%" disabled Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdColTitle" width="100">
                备注
            </td>
            <td class="tdColInput" colspan="5">
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="tdinput"
                    Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdColTitle" width="100">
                附件
            </td>
            <td class="tdColInput" colspan="5">
                <input id="hfPageAttachment" type="hidden" />
                <div id="divUploadAttachment" runat="server">
                    <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                </div>
                <div id="divDealAttachment" runat="server" style="display: none;">
                    <table>
                        <tr>
                            <td>
                                <a href="#" onclick="DealAttachment('download');">
                                    <div id="attachname">
                                    </div>
                                </a>
                            </td>
                            <td>
                                <a href="#" onclick="DealAttachment('clear');">删除附件</a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="right" valign="top">
                <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
            </td>
            <td width="8">
            </td>
        </tr>
        <tr>
            <td height="25" valign="top">
                &nbsp; <span class="Blue">日常调整单明细</span>
            </td>
            <td align="right" valign="top">
                <div id='searchClick3'>
                    <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('Tb_05','searchClick3')" /></div>
            </td>
            <td width="10">
            </td>
        </tr>
    </table>
    </td> </tr>
    <tr>
        <td colspan="2" id="Tb_05">
            <!-- Start 按钮操作 -->
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="28" bgcolor="#FFFFFF" valign="bottom">
                        <img runat="server" visible="false" id="imgAdd" src="../../../images/Button/Show_add.jpg"
                            style="cursor: hand" onclick="AddSignRow();" />
                        <img runat="server" visible="false" id="btn_del" src="../../../images/Button/Show_del.jpg"
                            style="cursor: hand" onclick="DeleteTheSignRow();" /><img 
                            id="btnSubSnapshot" alt="库存快照" onclick="ShowSnapshot();" 
                            src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" />
                        <img alt="条码扫描" visible="false" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods" style="cursor: pointer" 
                                                    onclick="SearchPro()" runat="server" />   <img alt="条码扫描" runat="server" visible="false" src="../../../Images/Button/btn_tmsmu.jpg" style="display:none"  id="unbtnGetGoods"   />
                    </td>
                </tr>
            </table>
            <!-- End 按钮操作 -->
            <!-- Start Product Info-->
            <!-- End Product Info -->
            <!-- Start Master Product Schedule Info -->
            <!-- End Master Product Schedule Info -->
            <!-- Start Bom -->
            <!-- End Bom -->
            <!-- Start Routing -->
            <!-- End Routing -->
            <table width="99%" border="0" id="dg_Log" align="center" cellpadding="0" cellspacing="1"
                bgcolor="#999999">
                <tr>
                    <td bgcolor="#E6E6E6" class="Blue" width="50" align="center">
                        <input type="checkbox" name="CheckAll" id="CheckAll" onclick="SelectSubAll()" title="全选"
                            style="cursor: hand" />
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        物品编号<span class="redbold">*</span>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        物品名称
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width:10%" valign="middle">
                                批次
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="display:none" id="baseuint">
                                基本单位
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="display:none" id="basecount">
                                基本数量
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        单位
                    </td>
                    <td width="5%" align="left" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        调整类型<span class="redbold">*</span>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        调整数量<span class="redbold">*</span>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        成本单价<span class="redbold">*</span>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        调整金额<span class="redbold">*</span>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        备注
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="2" bgcolor="#999999">
                    </td>
                </tr>
            </table>
            <br />
            <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
        </td>
    </tr>
    </table>
    </form>
</body>
</html>

<script type="text/javascript">
var AdjustID ='<%=AdjustID %>';
</script>

