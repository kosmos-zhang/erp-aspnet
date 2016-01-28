<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCheckNotPassAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageCheckNotPassAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/StorageNoPass.ascx" TagName="StorageNoPass"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>不合格处置单</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/DeleteFile.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/StorageManager/StorageCheckNotPassAdd.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script type="text/javascript">
    var intMasterProductID ='<%=intMasterProductID %>';
    var glb_BillTypeFlag ='<%=XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_QUALITY %>';
    var glb_BillTypeCode ='<%=XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_NOPASS %>';

    var glb_BillID = intMasterProductID;//单据ID 
    var FlowJS_HiddenIdentityID ='hiddenNoPassID';
     var glb_IsComplete = true;     
    var FlowJS_BillStatus ='ddlBillStatus';
    var FlowJs_BillNo='lbInfoNo';
    var selPoint = <%= SelPoint %>;// 小数位数
   
    </script>

    <script type="text/javascript">
 function removeAll()
 {
      var ReportNO=document.getElementById('ReportNO'); //name
   var ReportID=document.getElementById('hiddenReportID');//id
   var ProductName=document.getElementById('tbProName');
   var ProductID=document.getElementById('hiddenProID');  
   var ProductNo=document.getElementById('ProductNo');
   var Specification=document.getElementById('Specification');   
   var UnitName=document.getElementById('txtUnit');
   var UnitID=document.getElementById('hiddentxtUnit');   
   var NoPassNum=document.getElementById('txtNoPassNum');
   

   
   ReportNO.value='';
   ReportID.value='0';
   ProductName.value='';
   ProductID.value='0';
   ProductNo.value='';
   Specification.value='';
   UnitName.value='';
   UnitID.value='0';
   NoPassNum.value='0.00'

   closeNoPassdiv();
 }
 function Fun_FillReport1(a,b,c,d,e,f,g,h,i,J)
{

   var ReportNO=document.getElementById('ReportNO'); //name
   var ReportID=document.getElementById('hiddenReportID');//id
   var ProductName=document.getElementById('tbProName');
   var ProductID=document.getElementById('hiddenProID');  
   var ProductNo=document.getElementById('ProductNo');
   var Specification=document.getElementById('Specification');   
   var UnitName=document.getElementById('txtUnit');
   var UnitID=document.getElementById('hiddentxtUnit');   
   var NoPassNum=document.getElementById('txtNoPassNum');
   

   
   ReportNO.value=a;
   ReportID.value=b;
   ProductName.value=c;
   ProductID.value=d;
   ProductNo.value=e;
   Specification.value=f;
   UnitName.value=g;
   UnitID.value=h;
   NoPassNum.value=parseFloat(i).toFixed(selPoint);
   closeNoPassdiv();
   
}
function PrintNoPass()
{
    var NoPassId=document.getElementById('hiddenNoPassID').value;
    if(parseInt(NoPassId) < 1)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageCheckNotPassPrint.aspx?ID="+NoPassId);
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
    </script>

</head>
<body>
    <br />
    <form id="Form1" runat="server">
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <uc4:StorageNoPass ID="StorageNoPass1" runat="server" />
    <uc3:FlowApply ID="FlowApply1" runat="server" />
    <input id="hiddenNoPassID" type="hidden" value="0" />
    <!--存储本单据ID-->
    <input id="hiddenBillStatusID" type="hidden" value="-1" /><!--存储单据的状态-->
    <input id="hiddenFlowStatusID" type="hidden" value="-1" /><!--存储到修改页面单据的审批状态-->
    <!-- Start 消息提示-->
    <uc1:Message ID="Message1" runat="server" />
    <!-- End 消息提示-->
    <span id="Forms" class="Spantype"></span>
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
                            <%if (this.NoPassID > 0)
                              { %>
                            不合格品处置单
                            <%}
                              else
                              { %>
                            新建不合格品处置单
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
                                                    <img runat="server" visible="false" id="btnSave" src="../../../images/Button/Bottom_btn_save.jpg"
                                                        onclick="Fun_Save_NoPass();" style="cursor: pointer" title="保存不合格处置单" />
                                                </td>
                                                <td>
                                                    <img runat="server" visible="false" id="UnbtnSave" src="../../../Images/Button/UnClick_bc.jpg"
                                                        style="cursor: pointer; display: none" />
                                                </td>
                                                <td>
                                                    <span runat="server" visible="false" id="GlbFlowButtonSpan"></span>
                                                </td>
                                                <td>
                                                    <img onclick="BackToPage();" style="display: none" id="btnReturn" src="../../../images/Button/Bottom_btn_back.jpg"
                                                        border="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right">
                                        <img onclick="PrintNoPass();" id="btnPrint" src="../../../images/Button/Main_btn_print.jpg"
                                            style="cursor: pointer" title="打印" />
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
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
                                <tr>
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
                                <input id="lbInfoNo" class="tdinput" readonly runat="server" type="text" />
                            </div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单据主题
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="tdinput" Width="93%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            源单类型<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="24%">
                            <input id="sltFromTypetext" readonly class="tdinput" type="text" value="质检报告单" />
                            <input id="sltFromType" type="hidden" value="1" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            质检报告单<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="ReportNO" readonly class="tdinput" width="95%" onclick="FillReportInfo();" />
                            <input type="hidden" id="hiddenReportID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            处置负责人<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="UserExecutor" readonly class="tdinput" onclick="alertdiv('UserExecutor,hiddentbExecutorID');"
                                width="95%" />
                            <input type="hidden" id="hiddentbExecutorID" value="0" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            处理日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="tbProcessDate" CssClass="tdinput" ReadOnly="true" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndCheckDate')})"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <uc5:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        物品信息
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
                            物品名称
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="tbProName" CssClass="tdinput" ReadOnly="true" runat="server"></asp:TextBox>
                            <input id="hiddenProID" type="hidden" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            物品编号
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="ProductNo" runat="server" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            规格
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input id="Specification" type="text" readonly class="tdinput" width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单位
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input readonly id="txtUnit" type="text" class="tdinput" />
                            <input id="hiddentxtUnit" type="hidden" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            不合格数量
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="txtNoPassNum"  readonly type="text" class="tdinput" />
                        </td>
                        <td height="20" align="right" class="tdColInput" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="24%">
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
                                </select>--%>
                            <input id="ddlBillStatus" type="hidden" value="1" /><input class="tdinput" disabled
                                id="tbBillStatus" value="制单" type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="tbCreater" ReadOnly="true" runat="server" CssClass="tdinput"></asp:TextBox>
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
                            <div id="divConfirmor" style="display: none">
                                <input type="text" id="txtConfirmor" name="txtConfirmor" class="tdinput" runat="server"
                                    readonly />
                                <input type="hidden" id="txtConfirmorReal" name="txtConfirmorReal" class="tdinput"
                                    value="0" runat="server" readonly /></div>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td height="20" align="left" class="tdinput">
                            <div id="divConfirmorDate" style="display: none">
                                <asp:TextBox ID="txtConfirmDate" MaxLength="50" runat="server" CssClass="tdinput"
                                    disabled Width="95%" Text=""></asp:TextBox></div>
                        </td>
                        <td class="tdColTitle" width="10%">
                            结单人
                        </td>
                        <td class="tdColInput">
                            <div id="divCloser" style="display: none">
                                <input type="hidden" id="txtCloser" name="txtCloser" value="0" class="tdinput" runat="server"
                                    readonly />
                                <input type="text" id="txtCloserReal" name="txtCloserReal" class="tdinput" runat="server"
                                    disabled readonly /></div>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            结单日期
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <div id="divCloserDate" style="display: none">
                                <asp:TextBox ID="txtCloseDate" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                    disabled Text=""></asp:TextBox></div>
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            最后更新人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox><input runat="server" type="hidden" id="hiddenModifiedUserID" />
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
                            <input id="hfPageAttachment" name="hfPageAttachment" value="" type="hidden" />
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
                            &nbsp; <span class="Blue">不合格单处置明细</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick3'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('Tb_05','searchClick3')" /></div>
                        </td>
                        <td width="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="Tb_05">
                <!-- Start 按钮操作 -->
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF" valign="bottom">
                            <img runat="server" visible="false" id="imgAdd" src="../../../images/Button/Show_add.jpg"
                                style="cursor: hand" onclick="AddSignRow();" />
                            <img runat="server" visible="false" id="imgDel" src="../../../images/Button/Show_del.jpg"
                                style="cursor: hand" onclick="DeleteSignRow();" />&nbsp;
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
                            不合格原因<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            数量<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            处置方式<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            比率(%)
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
var NoPassID ='<%=NoPassID %>';
</script>

