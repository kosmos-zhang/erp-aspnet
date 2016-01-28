<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellChancePrint.aspx.cs" Inherits="Pages_PrinttingModel_SellManager_SellChancePrint" ValidateRequest="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售机会</title>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
     <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
     <script src="../../../js/common/PrintParameterSetting.js" type="text/javascript"></script>
         <style type="text/css">
        @media print
        {
            .onlyShow
            {
                display: none;
            }
            .onlyPrint
            {
                border-bottom: 1px solid #000000;
                page-break-before: always;
            }
        }
    </style>
    <style type="text/css" media="print">
        .noprint
        {
            border: 0px;
        }
        .noprint2
        {
            display: none;
        }
    </style>
    <style type="text/css" id="cssID">
        .busBtn
        {
            background: url(../../../Images/default/btnbg.gif) 0px -5px;
            border: 1px solid #cccccc;
            padding-top: 2px;
            cursor: pointer;
        }
        .trTitle
        {
            text-align: left;
            vertical-align: middle;
            padding-left: 10px;
            height: 36px;
            font-size: 16px;
            border: 1px solid #000000;
        }
        .tdFirstTitleMyLove
        {
            width: 6%;
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            font-weight: bold;
        }
        .tdFirstTitle
        {
            width: 12%;
            border: 1px solid #000000;
            text-align: right;
            border-top: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
        }
        .tdTitle
        {
            border: 1px solid #000000;
            text-align: center;
            border-left: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
        }
        .tdTitle2
        {
            width: 12%;
            border: 1px solid #000000;
            text-align: right;
            border-left: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-top: none;
        }
        .tdContent
        {
            width: 8%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            padding: 8px 0px 8px 5px;
            overflow: visible;
            word-break: break-all;
            font-size: 12px;
            font-weight: bold;
        }
        .tdContent2
        {
            width: 48%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            padding: 8px 0px 8px 5px;
            overflow: visible;
            word-break: break-all;
            font-size: 12px;
        }
        .tdLastContent
        {
            width: 24%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            overflow: visible;
            word-break: break-all;
            padding: 8px 0px 8px 5px;
            font-size: 12px;
        }
        .tdColContent
        {
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            overflow: visible;
            word-break: break-all;
            padding: 8px 0px 8px 5px;
            font-size: 12px;
        }
        .tdDetail
        {
            border: 1px solid #000000;
            text-align: left;
            width: 100%;
            border-bottom: none;
            overflow: visible;
            word-break: break-all;
            padding: 5px 0px 5px 5px;
            font-size: 12px;
        }
        .tdPageLast td
        {
            border: 1px solid #000000;
            text-align: left;
            width: 100%;
            overflow: visible;
            word-break: break-all;
            padding: 5px 0px 5px 5px;
            font-size: 12px;
        }
        .trDetailFirst
        {
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-top: none;
            
        }
        .trDetail
        {
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-left: none;
            border-top: none;
            word-break:break-all;
        }
        .setDiv
        {
            width: 796px;
            overflow-x: auto;
            overflow-y: auto;
            height: 400px;
            scrollbar-face-color: #E7E7E7;
            scrollbar-highlight-color: #ffffff;
            scrollbar-shadow-color: COLOR:#000000;
            scrollbar-3dlight-color: #ffffff;
            scrollbar-darkshadow-color: #ffffff;
        }
    </style>
    
    <script type="text/javascript">
        //此段js兼容ff的outerHTML，去掉后outerHTML在ff下不可用
        if (typeof (HTMLElement) != "undefined" && !window.opera) {
            HTMLElement.prototype.__defineGetter__("outerHTML", function() {
                var a = this.attributes, str = "<" + this.tagName, i = 0; for (; i < a.length; i++)
                    if (a[i].specified)
                    str += " " + a[i].name + '="' + a[i].value + '"';
                if (!this.canHaveChildren)
                    return str + " />";
                return str + ">" + this.innerHTML + "</" + this.tagName + ">";
            });
            HTMLElement.prototype.__defineSetter__("outerHTML", function(s) {
                var r = this.ownerDocument.createRange();
                r.setStartBefore(this);
                var df = r.createContextualFragment(s);
                this.parentNode.replaceChild(df, this);
                return s;
            });
            HTMLElement.prototype.__defineGetter__("canHaveChildren", function() {
                return !/^(area|base|basefont|col|frame|hr|img|br|input|isindex|link|meta|param)$/.test(this.tagName.toLowerCase());
            });
        }


        //打印的方法
        function pageSetup() {
            try {
                window.print();
            }
            catch (e) {
                alert("您的浏览器不支持此功能,请选择：文件→打印(P)…")
            }
        }

        //获取导出至excel的html的方法
        function fnGetTable() {
            var o_hid = document.getElementById("hiddExcel");
            o_hid.value = "";
            o_hid.value = o_hid.value + document.getElementById("cssID").outerHTML + document.getElementById("divMain").innerHTML;

            return true;
        }

    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WB" width="0">
        </object>
        <span class="noprint2" style="text-align: center; margin-top: 4px; width: 640px;">
            <input type="button" id="print" value=" 打 印 " onclick="pageSetup();" class="busBtn" />
            <asp:Button ID="btnImport" runat="server" Text=" 导 出 " CssClass="busBtn" OnClientClick="return fnGetTable();"
                OnClick="btnImport_Click" />
            <input type="button" id="btnSet" value=" 打印模板设置 " onclick="ShowPrintSetting();" class="busBtn" />
        </span>
        <div id="divMain" align="center">
            <table width="640px" border="0" style="font-size: 12px;">
                <tbody id="tableBase" runat="server">
                </tbody>
            </table>
            <table width="640px" border="0" cellpadding="0" cellspacing="1">
                <tbody id="tableDetail" runat="server">
                </tbody>
            </table>
            
        </div>
        <input type="hidden" id="hiddExcel" runat="server" />
        
    </div>
    <!-- Start 参数设置 -->
    <div align="center" id="div_InInfo" style="width: 70%; z-index: 100; position: absolute;
      display: none "><%-- --%>
        <table border="0" cellspacing="1" bgcolor="#999999" style="width: 70%">
            <tr>
                <td bgcolor="#EEEEEE" align="center">
                    <table width="100%">
                        <tr>
                            <td align="left" onmousedown="MoveDiv('div_InInfo',event)" title="点击此处可以拖动窗口" onmousemove="this.style.cursor='move';"
                                style="font-size: 12px; font-weight: bold;">
                                &nbsp;&nbsp;打印模板设置
                            </td>
                            <td width="50" align="right">
                                <img src="../../../images/default/0420close.gif" onclick='ClosePrintSetting();' style="cursor: hand;" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" cellspacing="1" bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF" align="center" valign="top" style="width: 90%">
                                <div id="divSet" class="setDiv" style="display: none;"><%-- --%>
                                    <!-- Start 打印模板设置 -->
                                    <table border="0" cellspacing="1" bgcolor="#CCCCCC" style="font-size: 12px;">
                                        <tr>
                                            <td bgcolor="#FFFFFF" align="left">
                                                <table width="100%" border="0" align="left" cellspacing="0">
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            基本信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckChanceNo" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="txtMRPNo" value="机会编号：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckTitle" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="txtSubject" value="机会主题：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckChanceTypeName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="txtPricipalReal" value="机会类型：" size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustName" value="ckCustID" /><input type="text" readonly="readonly" 
                                                                id="Text1" value="客户名称：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustTel" value="ckCustTel" /><input type="text" readonly="readonly" 
                                                                id="Text2" value="客户电话：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustTypeName" value="ckCustType" /><input readonly="readonly" 
                                                                type="text" id="Text3" value="客户类型：" size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckHapSourceName" value="ckHapSource" /><input type="text" readonly="readonly" 
                                                                id="Text4" value="机会来源：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckFindDate" value="ckFindDate" /><input type="text" readonly="readonly" 
                                                                id="Text5" value="发现日期：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckSellerName" value="ckSeller" /><input readonly="readonly" 
                                                                type="text" id="Text6" value="业务员：" size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckDeptName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text8" value="部门：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckProvideMan" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text9" value="提供人：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckRequires" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text10" value="需求描述：" size="10" />
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCanViewUserName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text11" value="可查看该机会人员：" size="15" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckRemindTime" id="ckRemindTime" value="RemindTime" /><input type="text" readonly="readonly" 
                                                                id="Text19" value="手机提醒时间：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckRemindMTel" id="ckRemindMTel" value="RemindMTel" /><input readonly="readonly" 
                                                                type="text" id="Text20" value="提醒手机号：" size="10" />
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckReceiverName" id="ckReceiverName" value="ReceiverName" /><input type="text" readonly="readonly" 
                                                                id="Text18" value="接收人：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                           <input type="checkbox" name="ckRemindContent" id="ckRemindContent" value="RemindContent" /><input type="text" readonly="readonly" 
                                                                id="Text17" value="提醒内容：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                             <input type="checkbox" name="ckExtField1" id="ckExtField1" value="ExtField1" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField1" value="" size="10" style="display: none" />
                                                        </td>
                                                    </tr>                                                   
                                                    <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField2" id="ckExtField2" value="ExtField2" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField2" value="" size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField3" id="ckExtField3" value="ExtField3" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField3" value="" size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField4" id="ckExtField4" value="ExtField4" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField4" value="" size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField5" id="ckExtField5" value="ExtField5" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField5" value="" size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField6" id="ckExtField6" value="ExtField6" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField6" value="" size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField7" id="ckExtField7" value="ExtField7" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField7" value="" size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="ckExtField8" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField8" value="" size="10" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="ckExtField9" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField9" value="" size="10" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="ckExtField10" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField10" value="" size="10" style="display: none" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    
                                                     <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            预期信息
                                                        </td>
                                                    </tr>
                                                   <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckStrBillStatusText" id="ckIntendMoney" value="strBillStatusText" /><input readonly="readonly" 
                                                                type="text" id="Text7" value="预期金额：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCreatorReal" id="ckIntendDate" value="CreatorReal" /><input readonly="readonly" 
                                                                type="text" id="Text12" value="预期签单日：" size="12" />
                                                        </td>
                                                            <td>
                                                               
                                                            </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            备注信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckStrBillStatusText" id="ckCreatorName" value="strBillStatusText" /><input readonly="readonly" 
                                                                type="text" id="txtStrBillStatusText" value="制单人：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCreatorReal" id="ckCreateDate" value="CreatorReal" /><input readonly="readonly" 
                                                                type="text" id="txtCreatorReal" value="制单日期：" size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCreateDate" id="ckModifiedUserID" value="CreateDate" /><input readonly="readonly" 
                                                                type="text" id="txtCreateDate" value="最后更新人：" size="10" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmorReal" id="ckModifiedDate" value="ConfirmorReal" /><input readonly="readonly" 
                                                                type="text" id="txtConfirmorReal" value="最后更新日期：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmDate" id="ckIsQuotedName" value="ConfirmDate" /><input readonly="readonly" 
                                                                type="text" id="txtConfirmDate" value="是否被报价：" size="12" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCloserReal" id="ckRemark" value="CloserReal" /><input readonly="readonly" 
                                                                type="text" id="txtCloserReal" value="备注：" size="8" />
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            明细信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="100%" border="0" cellspacing="1"  id="listSetDetail">
                                                                <tr>                                                                   
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdProdNo" id="ckPhaseName" value="ProdNo" /><input type="text" readonly="readonly" 
                                                                            id="txtDProductNo" value="阶段" size="6" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdProductName" id="ckPushDate" value="ProductName" /><input readonly="readonly" 
                                                                            type="text" id="txtDProductName" value="日期" size="6" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdUnitName" id="ckEmployeeName" value="UnitName" /><input readonly="readonly" 
                                                                            type="text" id="txtDUnitName" value="业务员" size="8" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdGrossCount" id="ckStateName" value="GrossCount" /><input readonly="readonly" 
                                                                            type="text" id="txtDGrossCount" value="状态" size="6" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanCount" id="ckTypeName" value="PlanCount" /><input readonly="readonly" 
                                                                            type="text" id="txtDPlanCount" value="可能性" size="8" />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdPlanDate" id="ckPushRemark" value="PlanDate" /><input readonly="readonly" 
                                                                            type="text" id="txtDPlanDate" value="阶段备注" size="12" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                    </tr>                                                    
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <input type="hidden" id="hidBillTypeFlag" name="hidBillTypeFlag" runat="server" />
                                    <input type="hidden" id="hidPrintTypeFlag" name="hidPrintTypeFlag" runat="server" />
                                    <input type="hidden" id="isSeted" value="0" runat="server" />
                                    <input type="hidden" id="hidPlanNo" name="planno" runat="server" />
                                    <!-- End 打印模板设置 -->
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <input type="button" id="btnPrintSave" name="btnPrintSave" value=" 保 存 " class="busBtn"
                                    onclick="SavePrintSetting();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divPageMask" style="display: none">
        <iframe id="PageMaskIframe" frameborder="0" width="100%"></iframe>
    </div>
    <!-- End 参数设置-->
    </form>
    <p>
</body>
</html>

<script language="javascript">
     var PlanNo = document.getElementById("hidPlanNo").value;
    
    //弹出单据显示信息
    function ShowPrintSetting() {
        
        document.getElementById('div_InInfo').style.display = 'block';
        CenterToDocument("div_InInfo", true);
        openRotoscopingDiv(false, "divPageMask", "PageMaskIframe");
        document.getElementById('divSet').style.display='block';
        initPage();
    }
    
    /*关闭弹出层*/
    function ClosePrintSetting() {
        document.getElementById('div_InInfo').style.display = 'none';
        closeRotoscopingDiv(false, 'divPageMask');
    } 
    
    /*初始化*/
    function initPage()
    {
        /*加载扩展属性名称*/
        LoadExtTableName('officedba.SellChance');
        /*加载打印模板设置信息*/
        LoadPrintSettingInfo();

    }
    


    /*2:加载打印模板设置信息*/
    function LoadPrintSettingInfo() {

        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        var hidIsSeted = document.getElementById('isSeted').value;

        /*主表：复选框及其对应的字段*/
        var dbBase = new Array( ['ckExtField1', 'ExtField1'],
                                ['ckExtField2', 'ExtField2'],
                                ['ckExtField3', 'ExtField3'],
                                ['ckExtField4', 'ExtField4'],
                                ['ckExtField5', 'ExtField5'],
                                ['ckExtField6', 'ExtField6'],
                                ['ckExtField7', 'ExtField7'],
                                ['ckExtField8', 'ExtField8'],
                                ['ckExtField9', 'ExtField9'],
                                ['ckExtField10', 'ExtField10'],
                                
                                ['ckChanceNo', 'ChanceNo'],
                                ['ckTitle', 'Title'],
                                ['ckChanceTypeName', 'ChanceTypeName'],
                                
                                ['ckCustName', 'CustName'],
                                ['ckCustTel', 'CustTel'],
                                ['ckCustTypeName', 'CustTypeName'],
                                
                                ['ckHapSourceName', 'HapSourceName'],
                                ['ckFindDate', 'FindDate'],                                
                                ['ckSellerName', 'SellerName'],
                                
                                ['ckDeptName', 'DeptName'],                                
                                ['ckProvideMan', 'ProvideMan'],
                                
                                ['ckRequires', 'Requires'],
                                
                                ['ckCanViewUserName', 'CanViewUserName'],
                                ['ckRemindTime', 'RemindTime'],
                                ['ckRemindMTel', 'RemindMTel'],
                                ['ckReceiverName', 'ReceiverName'],
                                ['ckRemindContent', 'RemindContent'],
                                
                                ['ckIntendMoney', 'IntendMoney'],
                                ['ckIntendDate', 'IntendDate'],
                                
                                ['ckCreatorName', 'CreatorName'],                                
                                ['ckCreateDate', 'CreateDate'],
                                
                                ['ckModifiedUserID', 'ModifiedUserID'],
                                
                                ['ckModifiedDate', 'ModifiedDate'],
                                ['ckIsQuotedName', 'IsQuotedName'],
                               
                                ['ckRemark', 'Remark']);
                                
        /*明细表：复选框及其对应的字段*/
        var dbDetail = new Array(['ckPhaseName', 'PhaseName'],
                                 ['ckPushDate', 'PushDate'],
                                 ['ckEmployeeName', 'EmployeeName'],
                                 ['ckStateName', 'StateName'],
                                 ['ckTypeName', 'TypeName'],
                                 ['ckPushRemark', 'Remark']);
        /*加载打印参数设置
          注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
        */
        LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase, dbDetail,null);
    }

    /*3:保存打印模板设置*/
    function SavePrintSetting() {

        var strBaseFields = "";
        var strDetailFields = "";
        var strDetailSecondFields = "";
        var toLocation='SellChancePrint.aspx?no=' + PlanNo;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        
        if (document.getElementById('ckChanceNo').checked) strBaseFields = strBaseFields + "ChanceNo|";
        if (document.getElementById('ckTitle').checked) strBaseFields = strBaseFields + "Title|";
        if (document.getElementById('ckChanceTypeName').checked) strBaseFields = strBaseFields + "ChanceTypeName|";
        
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";        
        if (document.getElementById('ckCustTel').checked) strBaseFields = strBaseFields + "CustTel|";
        if (document.getElementById('ckCustTypeName').checked) strBaseFields = strBaseFields + "CustTypeName|"; 
               
        if (document.getElementById('ckHapSourceName').checked) strBaseFields = strBaseFields + "HapSourceName|";
        if (document.getElementById('ckFindDate').checked) strBaseFields = strBaseFields + "FindDate|";        
        if (document.getElementById('ckSellerName').checked) strBaseFields = strBaseFields + "SellerName|";   
             
        if (document.getElementById('ckDeptName').checked) strBaseFields = strBaseFields + "DeptName|";
        if (document.getElementById('ckProvideMan').checked) strBaseFields = strBaseFields + "ProvideMan|";
        
        if (document.getElementById('ckRequires').checked) strBaseFields = strBaseFields + "Requires|";
        
        if (document.getElementById('ckCanViewUserName').checked) strBaseFields = strBaseFields + "CanViewUserName|";
        if (document.getElementById('ckRemindTime').checked) strBaseFields = strBaseFields + "RemindTime|";
        if (document.getElementById('ckRemindMTel').checked) strBaseFields = strBaseFields + "RemindMTel|";
        if (document.getElementById('ckReceiverName').checked) strBaseFields = strBaseFields + "ReceiverName|";
        if (document.getElementById('ckRemindContent').checked) strBaseFields = strBaseFields + "RemindContent|";
        
        if (document.getElementById('ckIntendMoney').checked) strBaseFields = strBaseFields + "IntendMoney|";
        if (document.getElementById('ckIntendDate').checked) strBaseFields = strBaseFields + "IntendDate|";
        
        if (document.getElementById('ckCreatorName').checked) strBaseFields = strBaseFields + "CreatorName|";
        if (document.getElementById('ckCreateDate').checked) strBaseFields = strBaseFields + "CreateDate|";
        
        if (document.getElementById('ckModifiedUserID').checked) strBaseFields = strBaseFields + "ModifiedUserID|";
        
        if (document.getElementById('ckModifiedDate').checked) strBaseFields = strBaseFields + "ModifiedDate|";
        if (document.getElementById('ckIsQuotedName').checked) strBaseFields = strBaseFields + "IsQuotedName|";
        if (document.getElementById('ckRemark').checked) strBaseFields = strBaseFields + "Remark|";
        for(var i=0;i<10;i++)
        {
            if(document.getElementById('ckExtField'+(i+1)).style.display=='block'||document.getElementById('ckExtField'+(i+1)).style.display=='')
            {
                if (document.getElementById('ckExtField'+(i+1)).checked) strBaseFields = strBaseFields + "ExtField"+(i+1)+"|";
            }
            
        }
        /*明细信息*/
        if (document.getElementById('ckPhaseName').checked) strDetailFields = strDetailFields + "PhaseName|";
        if (document.getElementById('ckPushDate').checked) strDetailFields = strDetailFields + "PushDate|";
        if (document.getElementById('ckEmployeeName').checked) strDetailFields = strDetailFields + "EmployeeName|";
        if (document.getElementById('ckStateName').checked) strDetailFields = strDetailFields + "StateName|";
        if (document.getElementById('ckTypeName').checked) strDetailFields = strDetailFields + "TypeName|";
        if (document.getElementById('ckPushRemark').checked) strDetailFields = strDetailFields + "Remark|";      
     
        /*保存打印参数设置*/
        
        SaveCommonPrintParameterSet(strBaseFields,strDetailFields,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }

    
</script>