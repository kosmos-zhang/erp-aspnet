<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCheckReportAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageCheckReportAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/CheckApplay.ascx" TagName="CheckApplay" TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/ReportProductInfo.ascx" TagName="ReportProductInfo"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/ReportMan.ascx" TagName="ReportMan" TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/ManProductInfo.ascx" TagName="ManProductInfo"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/ReportFrom.ascx" TagName="ReportFrom" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/CheckReportPurControl.ascx" TagName="PurchaseArriveSelect"
    TagPrefix="uc9" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc10" %>
<%@ Register Src="../../../UserControl/CheckReportPurDetail.ascx" TagName="CheckReportPurDetail"
    TagPrefix="uc11" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc12" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc13" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>质检报告</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/DeleteFile.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageCheckReportAdd.js" type="text/javascript"></script>

    <script type="text/javascript">
    var intMasterProductID ='<%=intMasterProductID %>';
    var glb_BillTypeFlag ='<%=XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_QUALITY %>';
    var glb_BillTypeCode ='<%=XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_REPORT %>';

    var glb_BillID = intMasterProductID;//单据ID 
    var FlowJS_HiddenIdentityID ='hiddenReportId';
     var glb_IsComplete = true;     
    var FlowJS_BillStatus ='txtBillStatus';
    var FlowJs_BillNo='lbInfoNo';
    var selPoint = <%= SelPoint %>;// 小数位数
    
		function CloseDiv(){
	 closeRotoscopingDiv(false,'divBackShadow');
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
        .style1
        {
            background-color: #E6E6E6;
            text-align: right;
            height: 3px;
        }
        .style2
        {
            background-color: #FFFFFF;
            height: 3px;
        }
    </style>

    <script type="text/javascript">

        //选择往来客户
        function SelectCust() {
            var TheFromType = document.getElementById('sltFromType').value;
            if (TheFromType != '3') {
                var url = "../../../Pages/Office/StorageManager/StorageReportCust.aspx";
                var returnValue = window.showModalDialog(url, "", "dialogWidth=300px;dialogHeight=450px");
                if (returnValue != "" && returnValue != null && returnValue != 'clear') {
                    var value = returnValue.split("|");
                    document.getElementById("tbCustID").value = value[1].toString();
                    document.getElementById('hiddentbCustID').value = value[0].toString();
                    document.getElementById('tbCustBigID').value = value[3].toString();
                    document.getElementById('hiddentbCustBigID').value = value[2].toString();
                }
                else if (returnValue == 'clear') {
                    document.getElementById("tbCustID").value = "";
                    document.getElementById('hiddentbCustID').value = "0";
                    document.getElementById('tbCustBigID').value = "";
                    document.getElementById('hiddentbCustBigID').value = "0";
                }
            }
        }
        function changeData() {
            document.getElementById('UserPrincipal').readOnly = true;
            document.getElementById('Depttxt').readOnly = true;
            document.getElementById('tbProductCount').readOnly = false;
            document.getElementById('tbProName').readOnly = false;
            document.getElementById('tbUnit').readOnly = false;
            document.getElementById('SampleNum').readOnly = false;
            document.getElementById('ApplyID').value = '';
            document.getElementById('UserChecker').value = '';
            document.getElementById('hiddentbChecker').value = '0';
            document.getElementById('DeptChecker').value = '';
            document.getElementById('hiddentbCheckDept').value = '0';
            document.getElementById('tbProNo').value = '';
            document.getElementById('tbProName').value = '';
            document.getElementById('tbUnit').value = '';
            document.getElementById('ddlCheckType').disabled = false;
            document.getElementById('tbCustID').value = '';
            document.getElementById('tbCustBigID').value = '';
            document.getElementById('UserPrincipal').value = '';
            document.getElementById('Depttxt').value = '';
            document.getElementById('CheckContent').value = '';
            document.getElementById('tbProductCount').value = '';
            var ddlFromType = document.getElementById('sltFromType').value;
            document.getElementById('tbProductCount').value = '';
            document.getElementById('NotPassNum').value = '';
            document.getElementById('ddlisRecheck').value = '0';
            document.getElementById('SampleNum').value = '';
            document.getElementById('PassNum').value = '';
            document.getElementById('CheckResult').value = '';
            document.getElementById('CheckStandard').value = '';
            document.getElementById('PassPercent').value = '';
            document.getElementById('ddlisPass').value = '00';
            document.getElementById('mySpecification').value = '';
            if (ddlFromType == '0') {
                document.getElementById('btnGetGoods').style.display = '';
            }
            else {
                document.getElementById('btnGetGoods').style.display = 'none';
            }
            ClearSignRow();
        }
        function FillBaseInfo() {
            var TheFromType = document.getElementById('sltFromType').value;
            if (TheFromType == "1") {
                popReportObj.ShowList(1);   //1新建2列表
                document.getElementById('ddlCheckType').disabled = true;
                document.getElementById('CheckContent').readOnly = false;
            }
            if (TheFromType == "2") {
                popReportFromTypeObj.ShowFromTypeList(1);
                document.getElementById('ddlCheckType').disabled = true;
                document.getElementById('CheckContent').readOnly = true;
            }
            if (TheFromType == "3") {
                document.getElementById('tbCustID').value = '';
                document.getElementById('tbCustBigID').value = '';
                document.getElementById('UserChecker').value = '';
                document.getElementById('DeptChecker').value = '';
                document.getElementById('ddlCheckType').disabled = false;
                document.getElementById('CheckContent').readOnly = false;
                popReportManObj.ShowList(1);
            }
            if (TheFromType == '0') {

            }
            if (TheFromType == "4") {
                popReportPurObj.ShowList(1);
                document.getElementById('ddlCheckType').disabled = false;
                document.getElementById('tbProductCount').readOnly = false;
                document.getElementById('CheckContent').readOnly = false;
            }
        }
        function FillFromArrive(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r) {
            document.getElementById('ApplyID').value = q;
            document.getElementById('hiddenApplyID').value = p;
            document.getElementById('tbProNo').value = b;
            document.getElementById('tbProName').value = c;

            document.getElementById('hiddenProID').value = a;
            document.getElementById('hiddentbUnit').value = e;
            document.getElementById('tbUnit').value = f;
            document.getElementById('hiddenDetailID').value = '';
            document.getElementById('divPurchaseArrive').style.display = 'none';
        }
        function Fun_FillParent_Content(a, b, c, d, e, f, g, h, i, j, k, l) {
            document.getElementById('tbProName').value = c;
            document.getElementById('hiddenProID').value = a;
            document.getElementById('tbProNo').value = b;
            document.getElementById('tbUnit').value = f;
            document.getElementById('hiddentbUnit').value = e;
            document.getElementById('mySpecification').value = j;
            document.getElementById('divStorageProduct').style.display = 'none';
        }
        function LoadPro() {
            var TheFromType = document.getElementById('sltFromType').value;
            var hiddenApplyID = document.getElementById('hiddenApplyID').value;
            if (TheFromType != "0") {
                if (parseFloat(hiddenApplyID) < 1) {
                    popMsgObj.ShowMsg('请先选择一个源单！');
                    return false;
                }
            }

            document.getElementById('tbProName').readOnly = true;
            document.getElementById('tbUnit').readOnly = true;
            document.getElementById('tbProNo').readOnly = true;
            if (TheFromType == "1") {
                popReportProObj.ShowProList();

            }
            if (TheFromType == '3') {
                popReportManProObj.ShowManProList();
            }

            if (TheFromType == "0") {
                popTechObj.ShowList(1);
            }
            if (TheFromType == "4") {
                popReportPurDetailObj.ShowList(1);
            }

        }
        function CheckChecker() {
            var TheFromType = document.getElementById('sltFromType').value;
            if (TheFromType == '0' || TheFromType == '4' || TheFromType == '3') {
                alertdiv('UserChecker,hiddentbChecker');

            }
        }
        function CheckDept() {
            var TheFromType = document.getElementById('sltFromType').value;
            if (TheFromType == '0' || TheFromType == '4' || TheFromType == '3') {
                alertdiv('DeptChecker,hiddentbCheckDept');
            }
        }
        function checkProMode() {

            //       var myCheckMode=document.getElementById('ddlCheckMode').value; //物品的检验方式             ------------根据需求以后删除
            //       var IsPass=document.getElementById('ddlisPass').value;           //是否合格
            //       var CheckCount=document.getElementById('tbProductCount').value;  //报检数量
            //       if(CheckCount=='')
            //       {
            //            popMsgObj.ShowMsg('请先输入物品的报检数量！');
            //            document.getElementById('ddlisPass').value='0';
            //            return false;
            //       }
            //       else
            //       {
            //            if(myCheckMode=='1' && IsPass=='1')
            //                {
            //                
            //                 document.getElementById('PassNum').value= parseFloat(CheckCount);
            //                 document.getElementById('PassNum').readOnly=true;
            //                 document.getElementById('NotPassNum').value='0';
            //                 document.getElementById('NotPassNum').readOnly=true;
            //               }
            //            else if(myCheckMode=='1' && IsPass=='0')
            //               {
            //                 document.getElementById('NotPassNum').value= parseFloat(CheckCount);
            //                 document.getElementById('PassNum').value='0';
            //                 document.getElementById('NotPassNum').readOnly=true;
            //                 document.getElementById('PassNum').readOnly=true;
            //               }
            //               else if(myCheckMode=='2' && IsPass=='1')
            //               {
            //                    document.getElementById('PassNum').value=document.getElementById('tbProductCount').value;
            //                    document.getElementById('NotPassNum').value='0';
            //               }
            //               else if(myCheckMode=='2' && IsPass=='0')
            //               {
            //                 document.getElementById('PassNum').value='0';
            //                 document.getElementById('NotPassNum').value=document.getElementById('tbProductCount').value;
            //                 document.getElementById('PassNum').readOnly=true;
            //                 document.getElementById('NotPassNum').readOnly=true;
            //               }
            //               else
            //               {
            //                 document.getElementById('NotPassNum').readOnly=false;
            //                 document.getElementById('PassNum').readOnly=false;
            //               }
            //       }
        }
        function GetPassPrcent() {
            var pc = $("#tbProductCount").val();
            var sn = $("#SampleNum").val();
            var pn = $("#PassNum").val();
            if ($("#ddlCheckMode").val() == '2') {// 抽检
                if (pc != '' && sn != '' && parseFloat(sn) > parseFloat(pc)) {
                    popMsgObj.ShowMsg('抽检数量不能大于报检数量！');
                    $("#SampleNum").val(FormatAfterDotNumber(0, selPoint))
                    return;
                }
                else if (pn != '' && sn != '' && parseFloat(pn) > parseFloat(sn)) {
                    popMsgObj.ShowMsg('合格数量不能大于抽检数量！');
                    $("#PassNum").val(FormatAfterDotNumber(0, selPoint))
                    return;
                }
            }
            else {
                if (pc != '' && pn != '' && parseFloat(pn) > parseFloat(pc)) {
                    popMsgObj.ShowMsg('合格数量不能大于报检数量！');
                    $("#PassNum").val(FormatAfterDotNumber(0, selPoint))
                    return;
                }
            }

            var myProductCount = document.getElementById('tbProductCount'); //报检数量
            if ($("#ddlCheckMode").val() == '2')   //抽检
            {
                myProductCount = document.getElementById('SampleNum'); // 抽检数量
            }
            var myPassNum = document.getElementById('PassNum');             //合格数量
            if (myProductCount.value != '' && myPassNum.value != '' && parseFloat(myProductCount.value) >= 0 && parseFloat(myPassNum.value) >= 0) {
                if (parseFloat(myProductCount.value) == 0) {
                    document.getElementById('PassPercent').value = FormatAfterDotNumber(100, selPoint);
                }
                else {
                    document.getElementById('PassPercent').value = FormatAfterDotNumber((parseFloat(myPassNum.value) / parseFloat(myProductCount.value)) * 100, selPoint);
                }
                document.getElementById('NotPassNum').value = FormatAfterDotNumber(parseFloat(myProductCount.value) - parseFloat(myPassNum.value), selPoint);
            }
        }
        function selectPeople() {
            var FromType = document.getElementById('sltFromType');
            if (FromType.value != '4') {
                alertdiv('UserPrincipal,hiddentxPrincipal');
            }
        }
        function selectDept() {
            var FromType = document.getElementById('sltFromType');
            if (FromType.value != '4') {
                alertdiv('Depttxt,hiddentbDept');
            }
        }
        function PrintCheckReport() {
            var ReportNo = document.getElementById('lbInfoNo').value;
            var ReportID = document.getElementById('hiddenReportId').value;
            if (parseInt(ReportID) < 1) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先保存！");
                return;
            }
            window.open("../../../Pages/PrinttingModel/StorageManager/StorageCheckReportPrint.aspx?ID=" + ReportID + "&ReportNo=" + ReportNo);
        }
        function BackToPage() {
            requestQuaobj = GetRequest();
            var intFromType = requestQuaobj['intFromType'];
            var moduleID = requestQuaobj['ListModuleID'];
            if (intFromType != null) {
                if (intFromType != '' && intFromType == '2') {
                    location.href = '../../../DeskTop.aspx';
                }
                if (intFromType != '' && intFromType == '3') {
                    location.href = '../../../Pages/Personal/WorkFlow/FlowMyApply.aspx?ModuleID=' + moduleID;
                }
                if (intFromType != '' && intFromType == '4') {
                    location.href = '../../../Pages/Personal/WorkFlow/FlowMyProcess.aspx?ModuleID=' + moduleID;
                }
                if (intFromType != '' && intFromType == '5') {
                    location.href = '../../../Pages/Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=' + moduleID;
                }
            }
            else {
                history.back();
            }
        }
    </script>

</head>
<body>
    <form id="Form1" runat="server">
    <div id="divBackShadow" style="display: none; height: 100%">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%" height="100%"></iframe>
    </div>
    <input id="isupdate" type="hidden" value="0" /><!--判断单据是否被其他的单据引用 修改需要-->
    <input id="isflow" type="hidden" value="0" /><!---判断制单状态的单据是否提交审批-->
    <uc10:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc11:CheckReportPurDetail ID="CheckReportPurDetail1" runat="server" />
    <uc9:PurchaseArriveSelect ID="ReportPur" runat="server" />
    <uc8:CheckApplay ID="CheckApplay1" runat="server" />
    <uc7:ReportFrom ID="ReportFrom1" runat="server" />
    <uc6:ManProductInfo ID="ManProductInfo1" runat="server" />
    <uc5:ReportMan ID="ReportMan1" runat="server" />
    <uc4:ReportProductInfo ID="ReportProductInfo1" runat="server" />
    <uc12:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <input id="hiddenApplyNo" type="hidden" value="0" />
    <!--源单编号-->
    <input id="hiddenDept" type="hidden" value="0" />
    <!--报检部门ID-->
    <input id="hiddenUserID" type="hidden" value="0" />
    <!--检验人员-->
    <input id="hiddenReportId" type="hidden" value="0" />
    <!--值为0新建其他为修改 保存单据ID-->
    <%-- <input id="hiddenDetailID" type="hidden" value="0" /><!---存储物品信息源单ID-->--%>
    <input type="hidden" id="hiddenApplyID" value="0" runat="server" /><!--源单具体明细ID--->
    <input id="hiddenLineNo" type="hidden" value="0" /><!--源单行号-->
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
                            <% if (this.ReportID > 0)
                               { %>
                            &nbsp;质检报告
                            <%}
                               else
                               { %>
                            新建质检报告
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
                                                        onclick="Fun_Save_CheckReport();" style="cursor: pointer" title="保存质检报告单" />
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
                                        <img onclick="PrintCheckReport();" id="btnPrint" src="../../../images/Button/Main_btn_print.jpg"
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
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            报告编号<span class="redbold">*</span>
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
                            报告主题
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtSubject" MaxLength="50" runat="server" CssClass="tdinput" Width="93%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            源单类型<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" width="24%">
                            <select id="sltFromType" onchange="changeData();" name="sltFromType">
                                <option value="0">无来源</option>
                                <option value="1" selected>质检申请单</option>
                                <option value="2">质检报告单</option>
                                <option value="3">生产任务单</option>
                                <option value="4">采购到货单</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            源单编号
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="ApplyID" class="tdinput" width="95%" onclick="FillBaseInfo();" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            往来单位
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="tbCustID" readonly class="tdinput" width="95%" onclick="SelectCust();" />
                            <input type="hidden" id="hiddentbCustID" value="0" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            往来单位大类
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="tbCustBigID" CssClass="tdinput" ReadOnly="true" runat="server"></asp:TextBox>
                            <input type="hidden" value="0" id="hiddentbCustBigID" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            生产负责人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox onclick="selectPeople();" ID="UserPrincipal" runat="server" ReadOnly="true"
                                CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="hiddentxPrincipal" value="0" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            生产部门
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox onclick="selectDept();" ID="Depttxt" runat="server" ReadOnly="true"
                                CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" value="0" id="hiddentbDept" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            质检类别<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select id="ddlCheckType">
                                <option value="1">进货检验</option>
                                <option value="2">过程检验</option>
                                <option value="3">最终检验</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            检验日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="CheckDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndCheckDate')})"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            报检人员<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="UserChecker" readonly="readonly" onclick="CheckChecker();" class="tdinput"
                                width="95%" type="text" />
                            <input type="hidden" id="hiddentbChecker" value="0" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            报检部门<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input id="DeptChecker" class="tdinput" onclick="CheckDept();" readonly="readonly" />
                            <input type="hidden" id="hiddentbCheckDept" value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            检验人员<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="UserCheck" runat="server" onclick="alertdiv('UserCheck,hiddenUserID');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            检验部门<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="DeptChecking" runat="server" onclick="alertdiv('DeptChecking,hiddenDept');"
                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            检验方案
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="CheckContent" CssClass="tdinput" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <uc13:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    物品信息
                                                </td>
                                                <td>
                                                    <img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods" style="cursor: pointer;
                                                        display: none" onclick="GetGoodsInfoByBarCode()" runat="server" visible="false" />
                                                </td>
                                            </tr>
                                        </table>
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
                            物品编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="tbProNo" readonly type="text" onclick="LoadPro();" class="tdinput" width="95%" />
                            <input id="FromDetailID" type="hidden" value="0" /><!--源单明细ID回写采购需要-->
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            物品名称
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="tbProName" CssClass="tdinput" ReadOnly="true" runat="server"></asp:TextBox>
                            <input id="hiddenProID" value="0" type="hidden" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单位
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="tbUnit" runat="server" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="hiddentbUnit" value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            规格
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="mySpecification" readonly name="mySpecification" type="text" class="tdinput" />
                        </td>
                        <td height="20" align="right" class="tdColInput" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            &nbsp;
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
                                        检验信息
                                    </td>
                                    <td align="right">
                                        <div id='divCheck'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','divCheck')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            报检数量<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input onchange="Number_round(this,selPoint);" maxlength="10" id="tbProductCount"
                                onblur="GetPassPrcent();" class="tdinput" width="95%" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            抽样数量<span id="sSample" class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="SampleNum" onchange="Number_round(this,selPoint);" class="tdinput" maxlength="10"
                                width="95%" onblur="GetPassPrcent();" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            检验方式
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select id="ddlCheckMode" onchange="changeCheckMode();">
                                <option value="1">全检</option>
                                <option selected value="2">抽检</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            合格数量<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input maxlength="10" onchange="Number_round(this,selPoint);" onblur="GetPassPrcent();"
                                id="PassNum" class="tdinput" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            不合格数量
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox onchange="Number_round(this,selPoint);" ID="NotPassNum" ReadOnly="true"
                                runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            合格率(%)
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="PassPercent" ReadOnly="true" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style1" width="10%">
                            检验标准
                        </td>
                        <td class="style2" width="23%">
                            <asp:TextBox ID="CheckStandard" CssClass="tdinput" runat="server"></asp:TextBox>
                        </td>
                        <td align="right" class="style1" width="10%">
                            检验结果描述
                        </td>
                        <td class="style2" width="23%">
                            <input id="CheckResult" style="width: 98%" type="text" class="tdinput" onblur="" />
                        </td>
                        <td align="right" class="style1" width="10%">
                            检验结果
                        </td>
                        <td class="style2" width="24%">
                            <select id="ddlisPass" onchange="checkProMode();">
                                <option selected value="00">--请选择--</option>
                                <option value="1">合格</option>
                                <option value="0">不合格</option>
                            </select>
                            <input type="hidden" id="Hidden18" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            是否需要复检<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select id="ddlisRecheck">
                                <option value="1">是</option>
                                <option selected value="0">否</option>
                            </select>
                            <input type="hidden" id="Hidden19" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="hidden" id="Hidden20" runat="server" />
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
                            <input type="hidden" id="txtBillStatus" name="txtBillStatus" class="tdinput" value="1" />
                            <input type="text" id="ddlBillStatus" name="ddlBillStatus" class="tdinput" disabled
                                value="制单" />
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
                            <div id="divConfirmor" style="display: none">
                                <input type="text" id="txtConfirmor" name="txtConfirmor" class="tdinput" runat="server"
                                    disabled />
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
                        <td class="tdColTitle">
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
                            <textarea id="txtRemark" style="width: 99%" class="tdinput" cols="20" rows="2"></textarea>
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
                            &nbsp; <span class="Blue">质检报告明细</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick3'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('Tb_05','searchClick3')" /></div>
                        </td>
                        <td width="10">
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="2" id="Tb_05">
                            <!-- Start 按钮操作 -->
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" valign="bottom">
                                        <img runat="server" visible="false" id="btn_add" src="../../../images/Button/Show_add.jpg"
                                            style="cursor: hand" onclick="AddSignRow();" />
                                        <img runat="server" visible="false" src="../../../images/Button/Show_del.jpg" style="cursor: hand"
                                            id="DelDetail" onclick="DeleteSignRow();" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" id="dg_Log" align="center" cellpadding="0" cellspacing="1"
                                bgcolor="#999999">
                                <tr>
                                    <td bgcolor="#E6E6E6" class="Blue" width="50" align="center">
                                        <input type="checkbox" name="CheckAll" id="CheckAll" onclick="SelectSubAll()" title="全选"
                                            style="cursor: hand" />
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" align="center">
                                        序号
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        检验项目
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        检验指标
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        检验值
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        检验结论
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        检验结果<span class="redbold">*</span>
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        检验数量<span class="redbold">*</span>
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        合格数量<span class="redbold">*</span>
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        不合格数
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        检验人员<span class="redbold">*</span>
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        检验部门<span class="redbold">*</span>
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        标准值
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        指标上限
                                    </td>
                                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                        指标下限
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
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script type="text/javascript">
    var ReportID = '<%=ReportID %>';
</script>

