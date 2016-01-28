<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintSellGathering.aspx.cs" Inherits="Pages_PrinttingModel_SellManager_PrintSellGathering" 
        ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

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
            width: 10%;
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
            width: 10%;
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
            word-break: break-all;
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

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <title>销售回款计划</title>

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
        </div>
        <input type="hidden" id="hiddExcel" runat="server" />
        
    </div>
    <!-- Start 参数设置 -->
    <div align="center" id="div_InInfo" style="width: 70%; z-index: 100; position: absolute;
        display: none">
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
                                <div id="divSet" style="display: none;" class="setDiv">
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
                                                            <input type="checkbox" name="ckGatheringNo" id="ckGatheringNo" value="GatheringNo" /><input type="text"
                                                                id="txtGatheringNo" value="回款计划编号：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckTitle" id="ckTitle" value="Title" /><input type="text"
                                                                id="txtTitle" value="主题：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckfromTypeName" id="ckfromTypeName" value="fromTypeName" /><input
                                                                type="text" id="txtfromTypeName" value="源单类型：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckBillNo" id="ckBillNo" value="BillNo" /><input
                                                                type="text" id="txtBillNo" value="源单编号：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCustName" id="ckCustName" value="CustName" /><input type="text"
                                                                id="txtCustName" value="客户名称：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckTypeName" id="ckTypeName" value="TypeName" /><input
                                                                type="text" id="txtTypeName" value="客户类型：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckTel" id="ckTel" value="Tel" /><input type="text"
                                                                id="txtTel" value="客户电话：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCurrencyName" id="ckCurrencyName" value="CurrencyName" /><input type="text"
                                                                id="txtCurrencyName" value="币种：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckSellerName" id="ckSellerName" value="SellerName" /><input
                                                                type="text" id="txtSellerName" value="业务员：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckDeptName" id="ckDeptName" value="DeptName" /><input type="text"
                                                                id="txtDeptName" value="部门：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckPlanPrice" id="ckPlanPrice" value="PlanPrice" /><input
                                                                type="text" id="txtPlanPrice" value="计划回款金额：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPlanGatherDate" id="ckPlanGatherDate" value="PlanGatherDate" /><input
                                                                type="text" id="txtPlanGatherDate" value="计划回款时间：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckFactPrice" id="ckFactPrice" value="FactPrice" /><input
                                                                type="text" id="txtFactPrice" value="实际回款金额：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckFactGatherDate" id="ckFactGatherDate" value="FactGatherDate" /><input
                                                                type="text" id="txtFactGatherDate" value="实际回款时间：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckLinkBillNo" id="ckLinkBillNo" value="LinkBillNo" /><input type="text"
                                                                id="txtLinkBillNo" value="回款相关单号：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckGatheringTime" id="ckGatheringTime" value="GatheringTime" /><input
                                                                type="text" id="txtGatheringTime" value="期次：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckstateName" id="ckstateName" value="stateName" /><input type="text"
                                                                id="txtstateName" value="状态：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckExtField1" id="ckExtField1" value="ExtField1" style="display: none" /><input
                                                                type="text" id="txtExtField1" value="" size="20" style="display: none" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <!--START 扩展属性-->
                                                    <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField2" id="ckExtField2" value="ExtField2" style="display: none" /><input
                                                                    type="text" id="txtExtField2" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField3" id="ckExtField3" value="ExtField3" style="display: none" /><input
                                                                    type="text" id="txtExtField3" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td align="left">
                                                                <input type="checkbox" name="ckExtField4" id="ckExtField4" value="ExtField4" style="display: none" /><input
                                                                    type="text" id="txtExtField4" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField5" id="ckExtField5" value="ExtField5" style="display: none" /><input
                                                                    type="text" id="txtExtField5" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField6" id="ckExtField6" value="ExtField6" style="display: none" /><input
                                                                    type="text" id="txtExtField6" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td align="left">
                                                                <input type="checkbox" name="ckExtField7" id="ckExtField7" value="ExtField7" style="display: none" /><input
                                                                    type="text" id="txtExtField7" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="ckExtField8" value="ExtField8" style="display: none" /><input
                                                                    type="text" id="txtExtField8" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="ckExtField9" value="ExtField9" style="display: none" /><input
                                                                    type="text" id="txtExtField9" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                            <td align="left">
                                                                <input type="checkbox" name="ckExtField10" id="ckExtField10" value="ExtField10" style="display: none" /><input
                                                                    type="text" id="txtExtField10" value="" size="20" style="display: none" readonly="readonly" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <!--END 扩展属性-->
                                                   <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            备注信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCreatorName" id="ckCreatorName" value="CreatorName" /><input
                                                                type="text" id="txtCreatorName" value="制单人：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCreateDate" id="ckCreateDate" value="CreateDate" /><input
                                                                type="text" id="txtCreateDate" value="制单日期：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckModifiedUserID" id="ckModifiedUserID" value="ModifiedUserID" /><input
                                                                type="text" id="txtModifiedUserID" value="最后更新人：" size="20" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckModifiedDate" id="ckModifiedDate" value="ModifiedDate" /><input
                                                                type="text" id="txtModifiedDate" value="最后更新日期：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckRemark" id="ckRemark" value="Remark" /><input type="text"
                                                                id="txtRemark" value="备注：" size="20" readonly="readonly" />
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <input type="text" id="txtDSortNo" value="" size="80" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <input type="hidden" id="hidBillTypeFlag" name="hidBillTypeFlag" runat="server" />
                                    <input type="hidden" id="hidPrintTypeFlag" name="hidPrintTypeFlag" runat="server" />
                                    <input type="hidden" id="isSeted" value="0" runat="server" />
                                    <input type="hidden" id="hiddckSellGatheringNo" runat="server" />
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

<script src="../../../js/common/PrintParameterSetting.js" type="text/javascript"></script>

<script language="javascript">
    var sellGatheringNo = $("#hiddckSellGatheringNo").val();
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
        LoadExtTableName('officedba.SellGathering');
        /*加载打印模板设置信息*/
        LoadPrintSettingInfo();
    }

    /*2:加载打印模板设置信息*/
    function LoadPrintSettingInfo() {

        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        var hidIsSeted = document.getElementById('isSeted').value;

        /*主表：复选框及其对应的字段*/
        var dbBase = new Array( 
                                ['ckGatheringNo', 'GatheringNo'],
                                ['ckTitle', 'Title'],
                                ['ckfromTypeName', 'fromTypeName'],
                                ['ckBillNo', 'BillNo'],
                                ['ckCustName', 'CustName'],
                                ['ckTypeName', 'TypeName'],
                                ['ckTel', 'Tel'],
                                ['ckCurrencyName', 'CurrencyName'],
                                ['ckSellerName', 'SellerName'],
                                ['ckDeptName', 'DeptName'],
                                ['ckPlanPrice', 'PlanPrice'],
                                ['ckPlanGatherDate', 'PlanGatherDate'],
                                ['ckFactPrice', 'FactPrice'],
                                ['ckFactGatherDate', 'FactGatherDate'],
                                ['ckLinkBillNo', 'LinkBillNo'],
                                ['ckGatheringTime', 'GatheringTime'],
                                ['ckstateName', 'stateName'],
                                ['ckCreatorName', 'CreatorName'],
                                ['ckCreateDate', 'CreateDate'],
                                ['ckModifiedUserID', 'ModifiedUserID'],
                                ['ckModifiedDate', 'ModifiedDate'],
                                ['ckRemark', 'Remark'],
                                ['ckExtField1', 'ExtField1'],
                                ['ckExtField2', 'ExtField2'],
                                ['ckExtField3', 'ExtField3'],
                                ['ckExtField4', 'ExtField4'],
                                ['ckExtField5', 'ExtField5'],
                                ['ckExtField6', 'ExtField6'],
                                ['ckExtField7', 'ExtField7'],
                                ['ckExtField8', 'ExtField8'],
                                ['ckExtField9', 'ExtField9'],
                                ['ckExtField10', 'ExtField10']
                                );                                 

        /*加载打印参数设置
          注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
        */
        LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase, null,null);
    }

    /*3:保存打印模板设置*/
    function SavePrintSetting() {

        
        var strBaseFields = "";
        //var strDetailFields = "";
        //var strDetailSecondFields = "";
        var toLocation='PrintSellGathering.aspx?no=' + sellGatheringNo;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        if (document.getElementById('ckGatheringNo').checked) strBaseFields = strBaseFields + "GatheringNo|";
        if (document.getElementById('ckTitle').checked) strBaseFields = strBaseFields + "Title|";
        if (document.getElementById('ckfromTypeName').checked) strBaseFields = strBaseFields + "fromTypeName|";
        if (document.getElementById('ckBillNo').checked) strBaseFields = strBaseFields + "BillNo|";
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";
        if (document.getElementById('ckTypeName').checked) strBaseFields = strBaseFields + "TypeName|";
        if (document.getElementById('ckTel').checked) strBaseFields = strBaseFields + "Tel|";
        if (document.getElementById('ckCurrencyName').checked) strBaseFields = strBaseFields + "CurrencyName|";
        if (document.getElementById('ckSellerName').checked) strBaseFields = strBaseFields + "SellerName|";
        if (document.getElementById('ckDeptName').checked) strBaseFields = strBaseFields + "DeptName|";
        if (document.getElementById('ckPlanPrice').checked) strBaseFields = strBaseFields + "PlanPrice|";
        if (document.getElementById('ckPlanGatherDate').checked) strBaseFields = strBaseFields + "PlanGatherDate|";
        if (document.getElementById('ckFactPrice').checked) strBaseFields = strBaseFields + "FactPrice|";
        if (document.getElementById('ckFactGatherDate').checked) strBaseFields = strBaseFields + "FactGatherDate|";
        if (document.getElementById('ckLinkBillNo').checked) strBaseFields = strBaseFields + "LinkBillNo|";
        if (document.getElementById('ckGatheringTime').checked) strBaseFields = strBaseFields + "GatheringTime|";
        if (document.getElementById('ckstateName').checked) strBaseFields = strBaseFields + "stateName|";
        if (document.getElementById('ckCreatorName').checked) strBaseFields = strBaseFields + "CreatorName|";
        if (document.getElementById('ckCreateDate').checked) strBaseFields = strBaseFields + "CreateDate|";
        if (document.getElementById('ckModifiedUserID').checked) strBaseFields = strBaseFields + "ModifiedUserID|";
        if (document.getElementById('ckModifiedDate').checked) strBaseFields = strBaseFields + "ModifiedDate|";
        if (document.getElementById('ckRemark').checked) strBaseFields = strBaseFields + "Remark|";
        for(var i=0;i<10;i++)
        {
            if(document.getElementById('ckExtField'+(i+1)).style.display=='block'||document.getElementById('ckExtField'+(i+1)).style.display=='')
            {
                if (document.getElementById('ckExtField'+(i+1)).checked) strBaseFields = strBaseFields + "ExtField"+(i+1)+"|";
            }
            
        }
        /*保存打印参数设置*/
        SaveCommonPrintParameterSet(strBaseFields,"","",hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }  
    
</script>


