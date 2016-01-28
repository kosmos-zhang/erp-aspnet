<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdversaryPrint.aspx.cs" Inherits="Pages_PrinttingModel_SellManager_AdversaryPrint" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>竞争对手档案</title>
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
       display: none "><%----%>
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
                                    <table border="0" cellspacing="1" bgcolor="#CCCCCC" style="font-size: 12px;">
                                        <tr>
                                            <td bgcolor="#FFFFFF" align="left">
                                                <table width="100%" border="0" align="left" cellspacing="0">
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            对手基本信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustNo" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="txtMRPNo" value="对手编号：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustTypeName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="txtSubject" value="对手类别：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustClassName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="txtPricipalReal" value="对手细分：" size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text1" value="对手名称：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckShortNam" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text2" value="对手简称：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckPYShort" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text3" value="对手拼音代码：" size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckAreaIDName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text4" value="对手所在区域：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            
                                                        </td>
                                                        <td align="left">
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            对手辅助信息
                                                        </td>
                                                    </tr>                                                    
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckSetupDate" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text5" value="成立时间：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckArtiPerson" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text6" value="法人代表：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCompanyTypeName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text7" value="单位性质：" size="10" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckStaffCount" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text8" value="员工总数：" size="8" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckSetupMoney" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text9" value="注册资本(万元)：" size="13" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckSetupAddress" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text10" value="注册地址：" size="8" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckwebsite" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text11" value="公司网址：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCapitalScale" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text12" value="资产规模(万元)：" size="15" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckSellArea" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text13" value="经营范围：" size="10" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckSaleroomY" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text14" value="年销售额(万元)：" size="15" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckProfitY" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text15" value="年利润额(万元)：" size="15" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckTaxCD" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text16" value="税务登记号：" size="12" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckBusiNumber" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text17" value="营业执照号：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckIsTaxName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text18" value="一般纳税人：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckAddress" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text19" value="地址：" size="10" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckPost" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text20" value="邮编：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckContactName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text21" value="联系人：" size="10" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckTel" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text22" value="电话：" size="6" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckMobile" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text23" value="手机：" size="6" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckemail" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text24" value="邮件：" size="6" />
                                                        </td>
                                                        <td align="left">
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            对手分析及对策
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckStrBillStatusText" id="ckCustNote" value="strBillStatusText" /><input readonly="readonly" 
                                                                type="text" id="txtStrBillStatusText" value="对手简介：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCreatorReal" id="ckProduct" value="CreatorReal" /><input readonly="readonly" 
                                                                type="text" id="txtCreatorReal" value="主打产品：" size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCreateDate" id="ckProject" value="CreateDate" /><input readonly="readonly" 
                                                                type="text" id="txtCreateDate" value="竞争产品/方案：" size="15" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmorReal" id="ckPower" value="ConfirmorReal" /><input readonly="readonly" 
                                                                type="text" id="txtConfirmorReal" value="竞争能力：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckConfirmDate" id="ckAdvantage" value="ConfirmDate" /><input readonly="readonly" 
                                                                type="text" id="txtConfirmDate" value="竞争优势：" size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckCloserReal" id="ckdisadvantage" value="CloserReal" /><input readonly="readonly" 
                                                                type="text" id="txtCloserReal" value="竞争劣势：" size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ckPolicy" value="CloseDate" /><input readonly="readonly" 
                                                                type="text" id="txtCloseDate" value="应对策略：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckModifiedUserID" id="ckMarket" value="ModifiedUserID" /><input readonly="readonly" 
                                                                type="text" id="txtModifiedUserID" value="市场占有率(%)：" size="12" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckModifiedDate" id="ckSellMode" value="ModifiedDate" /><input readonly="readonly" 
                                                                type="text" id="txtModifiedDate" value="销售模式：" size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            对手备注信息
                                                        </td>
                                                    </tr>
                                                   <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ckUsedStatusName" value="CloseDate" /><input readonly="readonly" 
                                                                type="text" id="Text25" value="启用状态：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckModifiedUserID" id="ckEmployeeName" value="ModifiedUserID" /><input readonly="readonly" 
                                                                type="text" id="Text26" value="制单人：" size="10" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckModifiedDate" id="ckCreatDate" value="ModifiedDate" /><input readonly="readonly" 
                                                                type="text" id="Text27" value="制单日期：" size="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ckModifiedUserID" value="CloseDate" /><input readonly="readonly" 
                                                                type="text" id="Text28" value="最后更新人：" size="10" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckModifiedUserID" id="ckModifiedDate" value="ModifiedUserID" /><input readonly="readonly" 
                                                                type="text" id="Text29" value="最后更新日：" size="10" />
                                                        </td>
                                                        <td>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckCloseDate" id="ckRemark" value="CloseDate" /><input readonly="readonly" 
                                                                type="text" id="Text31" value="备注：" size="6" />
                                                        </td>
                                                        <td width="20%" align="left">                                                            
                                                        </td>
                                                        <td>                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            对手动态信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="100%" border="0" cellspacing="1"  id="listSetDetail">
                                                                <tr>                                                                   
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ckdProdNo" id="ckDynamic" value="ProdNo" /><input type="text" readonly="readonly" 
                                                                            id="txtDProductNo" value="对手动态" size="15" />
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
        /*加载扩展属性名称*/
        LoadExtTableName('officedba.AdversaryInfo');
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
                                ['ckCustTypeName', 'CustTypeName'],
                                ['ckCustClassName', 'CustClassName'],
                                
                                ['ckCustName', 'CustName'],
                                ['ckShortNam', 'ShortNam'],
                                ['ckPYShort', 'PYShort'],
                                ['ckAreaIDName', 'AreaIDName'],
                                
                                ['ckSetupDate', 'SetupDate'],
                                ['ckArtiPerson', 'ArtiPerson'],                                
                                ['ckCompanyTypeName', 'CompanyTypeName'],
                                
                                ['ckStaffCount', 'StaffCount'],                                
                                ['ckSetupMoney', 'SetupMoney'],
                                ['ckSetupAddress', 'SetupAddress'],
                                
                                ['ckwebsite', 'website'],                                
                                ['ckCapitalScale', 'CapitalScale'],
                                ['ckSellArea', 'SellArea'],
                                
                                ['ckSaleroomY', 'SaleroomY'],                                
                                ['ckProfitY', 'ProfitY'],
                                ['ckTaxCD', 'TaxCD'],
                                
                                ['ckBusiNumber', 'BusiNumber'],                                
                                ['ckIsTaxName', 'IsTaxName'],
                                ['ckAddress', 'Address'],
                                
                                ['ckPost', 'Post'],                                
                                ['ckContactName', 'ContactName'],
                                ['ckTel', 'Tel'],
                                
                                ['ckMobile', 'Mobile'],
                                 ['ckemail', 'email'],
                                 
                                 ['ckCustNote', 'CustNote'],
                                 ['ckProduct', 'Product'],
                                 ['ckProject', 'Project'],
                                 ['ckPower', 'Power'],
                                 ['ckAdvantage', 'Advantage'],
                                 ['ckdisadvantage', 'disadvantage'],
                                 ['ckPolicy', 'Policy'],
                                 ['ckMarket', 'Market'],
                                 ['ckSellMode', 'SellMode'],
                                 ['ckUsedStatusName', 'UsedStatusName'],
                                 ['ckEmployeeName', 'EmployeeName'],
                                 ['ckCreatDate', 'CreatDate'],
                                   ['ckModifiedUserID', 'ModifiedUserID'],
                                 ['ckModifiedDate', 'ModifiedDate'],
                                
                                ['ckRemark', 'Remark']);
                                
        /*明细表：复选框及其对应的字段*/
        var dbDetail = new Array(['ckDynamic', 'Dynamic']);
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
        var toLocation='AdversaryPrint.aspx?no=' + strPlanNo;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        
        if (document.getElementById('ckCustNo').checked) strBaseFields = strBaseFields + "CustNo|";
        if (document.getElementById('ckCustTypeName').checked) strBaseFields = strBaseFields + "CustTypeName|";
        if (document.getElementById('ckCustClassName').checked) strBaseFields = strBaseFields + "CustClassName|";
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";
        if (document.getElementById('ckShortNam').checked) strBaseFields = strBaseFields + "ShortNam|";
        if (document.getElementById('ckPYShort').checked) strBaseFields = strBaseFields + "PYShort|";        
        if (document.getElementById('ckAreaIDName').checked) strBaseFields = strBaseFields + "AreaIDName|";
        if (document.getElementById('ckSetupDate').checked) strBaseFields = strBaseFields + "SetupDate|";        
        if (document.getElementById('ckArtiPerson').checked) strBaseFields = strBaseFields + "ArtiPerson|";        
        if (document.getElementById('ckCompanyTypeName').checked) strBaseFields = strBaseFields + "CompanyTypeName|";
        if (document.getElementById('ckStaffCount').checked) strBaseFields = strBaseFields + "StaffCount|";
        if (document.getElementById('ckSetupMoney').checked) strBaseFields = strBaseFields + "SetupMoney|";
        if (document.getElementById('ckSetupAddress').checked) strBaseFields = strBaseFields + "SetupAddress|";        
        if (document.getElementById('ckwebsite').checked) strBaseFields = strBaseFields + "website|";
        if (document.getElementById('ckCapitalScale').checked) strBaseFields = strBaseFields + "CapitalScale|";
        if (document.getElementById('ckSellArea').checked) strBaseFields = strBaseFields + "SellArea|";
        
        if (document.getElementById('ckSaleroomY').checked) strBaseFields = strBaseFields + "SaleroomY|";
        if (document.getElementById('ckProfitY').checked) strBaseFields = strBaseFields + "ProfitY|";
        if (document.getElementById('ckTaxCD').checked) strBaseFields = strBaseFields + "TaxCD|";
        if (document.getElementById('ckBusiNumber').checked) strBaseFields = strBaseFields + "BusiNumber|";
        if (document.getElementById('ckIsTaxName').checked) strBaseFields = strBaseFields + "IsTaxName|";
        if (document.getElementById('ckAddress').checked) strBaseFields = strBaseFields + "Address|";
        if (document.getElementById('ckPost').checked) strBaseFields = strBaseFields + "Post|";
        if (document.getElementById('ckContactName').checked) strBaseFields = strBaseFields + "ContactName|";
        if (document.getElementById('ckTel').checked) strBaseFields = strBaseFields + "Tel|";
        if (document.getElementById('ckMobile').checked) strBaseFields = strBaseFields + "Mobile|";
        if (document.getElementById('ckemail').checked) strBaseFields = strBaseFields + "email|";
        if (document.getElementById('ckCustNote').checked) strBaseFields = strBaseFields + "CustNote|";
         if (document.getElementById('ckProduct').checked) strBaseFields = strBaseFields + "Product|";
        if (document.getElementById('ckProject').checked) strBaseFields = strBaseFields + "Project|";
        if (document.getElementById('ckPower').checked) strBaseFields = strBaseFields + "Power|";
        if (document.getElementById('ckAdvantage').checked) strBaseFields = strBaseFields + "Advantage|";
        if (document.getElementById('ckdisadvantage').checked) strBaseFields = strBaseFields + "disadvantage|";
        if (document.getElementById('ckPolicy').checked) strBaseFields = strBaseFields + "Policy|";
        if (document.getElementById('ckMarket').checked) strBaseFields = strBaseFields + "Market|";
        if (document.getElementById('ckSellMode').checked) strBaseFields = strBaseFields + "SellMode|";
        if (document.getElementById('ckUsedStatusName').checked) strBaseFields = strBaseFields + "UsedStatusName|";
         if (document.getElementById('ckEmployeeName').checked) strBaseFields = strBaseFields + "EmployeeName|";
        if (document.getElementById('ckCreatDate').checked) strBaseFields = strBaseFields + "CreatDate|";
        if (document.getElementById('ckModifiedUserID').checked) strBaseFields = strBaseFields + "ModifiedUserID|";
        if (document.getElementById('ckModifiedDate').checked) strBaseFields = strBaseFields + "ModifiedDate|";
        if (document.getElementById('ckRemark').checked) strBaseFields = strBaseFields + "Remark|";
       
        /*明细信息*/
        if (document.getElementById('ckDynamic').checked) strDetailFields = strDetailFields + "Dynamic|";
      
        /*保存打印参数设置*/
        
        SaveCommonPrintParameterSet(strBaseFields,strDetailFields,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }

    
</script>