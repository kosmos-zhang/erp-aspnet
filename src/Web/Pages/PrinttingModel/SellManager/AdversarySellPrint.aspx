<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdversarySellPrint.aspx.cs" Inherits="Pages_PrinttingModel_SellManager_AdversarySellPrint" ValidateRequest="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>销售竞争分析</title>
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
            width: 14%;
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
            width: 12%;
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
            <!-- Start 第二明细 -->
            <br />
            <table width="640px" border="0" cellpadding="0" cellspacing="1">
                <tbody id="tableDetail2" runat="server">
                </tbody>
            </table>
            <!-- End 第二明细 -->
        </div>
        <input type="hidden" id="hiddExcel" runat="server" />
        
    </div>
    <!-- Start 参数设置 -->
  <div align="center" id="div_InInfo" style="width: 70%; z-index: 100; position: absolute;
       display: none"><%-- --%>
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
                            <td bgcolor="#FFFFFF" align="center" valign="top" style="width: 90%" >
                                <div id="divSet"  class="setDiv" style="display: none;"><%----%>
                                    <!-- Start 打印模板设置 -->
                                    <table border="0" cellspacing="1" bgcolor="#CCCCCC" style="font-size: 12px; width:80%;">
                                        <tr>
                                            <td bgcolor="#FFFFFF" align="left">
                                                <table width="100%" border="0" align="left" cellspacing="0">
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            对手基本信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustNo" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="txtMRPNo" value="对手编号：" size="8" />
                                                        </td>
                                                        <td width="30%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckChanceNo" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="txtSubject" value="销售机会：" size="8" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="txtPricipalReal" value="竞争客户：" size="8" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckPrice" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text1" value="对手产品报价：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckProject" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text2" value="竞争产品/方案：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckPower" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text3" value="竞争能力：" size="8" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckAdvantage" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text4" value="竞争优势：" size="8" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckdisadvantage" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text30" value="竞争劣势：" size="8" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckPolicy" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text32" value="应对策略：" size="8" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            备注信息
                                                        </td>
                                                    </tr>                                                    
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckEmployeeName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text5" value="制单人：" size="8" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCreatDate" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text6" value="制单日期：" size="8" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckModifiedUserID" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text7" value="最后更新人：" size="8" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckModifiedDate" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text8" value="最后更新日期：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckRemark" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text9" value="备注：" size="6" />
                                                        </td>
                                                        <td align="left">
                                                           
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
     var strPlanNo = document.getElementById("hidPlanNo").value;
    
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
//        /*加载扩展属性名称*/
//        LoadExtTableName('officedba.AdversaryInfo');
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
                                ['ckCustNo', 'CustNo'],
                                ['ckChanceNo', 'ChanceNo'],
                                ['ckCustName', 'CustName'],

                                ['ckPrice', 'Price'],
                                ['ckProject', 'Project'],
                                ['ckPower', 'Power'],
                                ['ckAdvantage', 'Advantage'],
                                
                                ['ckdisadvantage', 'disadvantage'],
                                ['ckPolicy', 'Policy'],                                
                                ['ckEmployeeName', 'EmployeeName'],
                                
                                ['ckCreatDate', 'CreatDate'],                                
                                ['ckModifiedUserID', 'ModifiedUserID'],
                                ['ckModifiedDate', 'ModifiedDate'],                                
                                
                                ['ckRemark', 'Remark']);                                
       
        /*加载打印参数设置
          注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
        */
        LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase, "",null);
    }

    /*3:保存打印模板设置*/
    function SavePrintSetting() {

        var strBaseFields = "";
        var strDetailFields = "";
        var strDetailSecondFields = "";
        var toLocation='AdversarySellPrint.aspx?no=' + strPlanNo;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        
        if (document.getElementById('ckCustNo').checked) strBaseFields = strBaseFields + "CustNo|";
        if (document.getElementById('ckChanceNo').checked) strBaseFields = strBaseFields + "ChanceNo|";
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";
        if (document.getElementById('ckPrice').checked) strBaseFields = strBaseFields + "Price|";
        if (document.getElementById('ckProject').checked) strBaseFields = strBaseFields + "Project|";
        if (document.getElementById('ckPower').checked) strBaseFields = strBaseFields + "Power|";        
        if (document.getElementById('ckAdvantage').checked) strBaseFields = strBaseFields + "Advantage|";
        if (document.getElementById('ckdisadvantage').checked) strBaseFields = strBaseFields + "disadvantage|";        
        if (document.getElementById('ckPolicy').checked) strBaseFields = strBaseFields + "Policy|";        
        if (document.getElementById('ckEmployeeName').checked) strBaseFields = strBaseFields + "EmployeeName|";
        if (document.getElementById('ckCreatDate').checked) strBaseFields = strBaseFields + "CreatDate|";
        if (document.getElementById('ckModifiedUserID').checked) strBaseFields = strBaseFields + "ModifiedUserID|";
        if (document.getElementById('ckModifiedDate').checked) strBaseFields = strBaseFields + "ModifiedDate|";        
        if (document.getElementById('ckRemark').checked) strBaseFields = strBaseFields + "Remark|";
      
       
//        /*明细信息*/
//        if (document.getElementById('ckDynamic').checked) strDetailFields = strDetailFields + "Dynamic|";
      
        /*保存打印参数设置*/
         SaveCommonPrintParameterSet(strBaseFields,strDetailFields,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }

    
</script>
