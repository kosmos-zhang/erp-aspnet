<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustAddPrint.aspx.cs" Inherits="Pages_PrinttingModel_CustManager_CustAddPrint" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户信息打印</title>   
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
                            <td bgcolor="#FFFFFF" align="center" valign="top" style="width: 90%">
                                <div id="divSet" class="setDiv" style=" width:70%;display: none;"><%--  --%>
                                    <!-- Start 打印模板设置 -->
                                    <table border="0" cellspacing="1" bgcolor="#CCCCCC" style="font-size: 12px; width:100%;">
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
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustNo" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="txtMRPNo" value="客户编号：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="txtSubject" value="客户名称：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustBig" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="txtPricipalReal" value="客户大类：" size="12" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustNam" value="ckCustID" /><input type="text" readonly="readonly" 
                                                                id="Text1" value="客户简称：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustShort" value="ckCustTel" /><input type="text" readonly="readonly" 
                                                                id="Text2" value="拼音缩写：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustTypeManage" value="ckCustType" /><input readonly="readonly" 
                                                                type="text" id="Text3" value="客户管理分类：" size="12" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustTypeSell" value="ckHapSource" /><input type="text" readonly="readonly" 
                                                                id="Text4" value="客户营销分类：" size="14" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCreditGradeNm" value="ckFindDate" /><input type="text" readonly="readonly" 
                                                                id="Text5" value="客户优质级别：" size="14" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustTypeTime" value="ckSeller" /><input readonly="readonly" 
                                                                type="text" id="Text6" value="客户时间分类：" size="14" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustClassName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text8" value="客户细分：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustTypaNm" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text9" value="客户类别：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCreatorName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text10" value="建档人：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCreatedDate" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text13" value="建档日期：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustNote" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text14" value="客户简介：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCountryName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text15" value="国家地区：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            业务信息
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckAreaName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text16" value="区域：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckProvince" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text17" value="省：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCity" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text18" value="市(县)：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckBusiType" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text19" value="业务类型：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckManagerName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text20" value="分管业务员：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckContactName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text21" value="联系人：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckTel" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text22" value="电话：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckMobile" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text23" value="手机：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckFax" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text24" value="传真：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckOnLine" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text25" value="在线咨询：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckWebSite" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text26" value="公司网址：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckPost" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text27" value="邮编：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckemail" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text28" value="电子邮件：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckFirstBuyDate" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text29" value="首次交易日期：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCarryTypeNm" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text30" value="运送方式：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckTakeTypeNm" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text31" value="交货方式：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckLinkCycleNm" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text32" value="联络期限(天)：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckReceiveAddress" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text33" value="收货地址：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            财务信息
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckSellArea" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text34" value="经营范围：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCreditManage" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text35" value="允许延期付款：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckMaxCredit" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text36" value="信用额度(元)：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckMaxCreditDate" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text37" value="帐期天数(天)：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckPayTypeNm" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text38" value="结算方式：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCurrencyaNm" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text39" value="结算币种：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckBillTypeNm" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text40" value="发票类型：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckMoneyTypeNm" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text41" value="支付方式：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckOpenBank" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text42" value="开户行：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckAccountMan" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text43" value="户名：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckAccountNum" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text44" value="账号：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCompanyType" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text45" value="单位性质：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            辅助信息
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCapitalScale" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text46" value="资产规模(万元)：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckSetupDate" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text47" value="成立时间：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckSetupMoney" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text48" value="注册资本(万元)：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckStaffCount" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text49" value="员工总数(个)：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckArtiPerson" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text50" value="法人代表：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckTrade" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text51" value="行业：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckBusiNumber" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text52" value="营业执照号：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckSetupAddress" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text53" value="注册地址：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckTaxCD" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text54" value="税务登记号：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckIsTax" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text55" value="是否为一般纳税人：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckSource" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text56" value="客户来源：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckSaleroomY" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text57" value="年销售额(万元)：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckProfitY" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text58" value="年利润额(万元)：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckSellMode" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text59" value="销售模式：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustSupe" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text60" value="上级客户：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckMeritGrade" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text61" value="价值评估：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckPhase" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text62" value="阶段：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckHotIs" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text63" value="热点客户：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckHotHow" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text64" value="热度：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckRelaGrade" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text65" value="关系等级：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckUsedStatus" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text66" value="启用状态：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckRelation" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text67" value="关系描述：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckRemark" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text68" value="备注：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckModifiedUserID" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text69" value="最后更新用户：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            经营信息
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckModifiedDate" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text70" value="最后更新日期：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCompanyValues" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text71" value="经营理念：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCatchWord" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text72" value="企业口号：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckManageValues" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text73" value="企业文化概述：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckPotential" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text74" value="发展潜力：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckProblem" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text75" value="存在问题：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckAdvantages" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text76" value="市场优劣势：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckTradePosition" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text77" value="行业地位：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCompetition" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text78" value="竞争对手：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCollaborator" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text79" value="合作伙伴：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckManagePlan" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text80" value="发展计划：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCollaborate" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text81" value="合作方法：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCanViewUserName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text82" value="可查看该客户档案的人员：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckExtField1" id="ckExtField1" value="ExtField1" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField1" value="" size="12" style="display: none" />
                                                        </td>                                                        
                                                    </tr>                                                                                              
                                                    <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField2" id="ckExtField2" value="ExtField2" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField2" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField3" id="ckExtField3" value="ExtField3" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField3" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField4" id="ckExtField4" value="ExtField4" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField4" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField5" id="ckExtField5" value="ExtField5" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField5" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField6" id="ckExtField6" value="ExtField6" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField6" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField7" id="ckExtField7" value="ExtField7" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField7" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="ckExtField8" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField8" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="ckExtField9" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField9" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="ckExtField10" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField10" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                         <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="Checkbox11" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField11" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="Checkbox12" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField12" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="Checkbox13" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField13" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                         <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="Checkbox14" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField14" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="Checkbox15" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField15" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="Checkbox16" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField16" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                         <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="Checkbox17" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField17" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="Checkbox18" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField18" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="Checkbox19" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField19" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                         <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="Checkbox20" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField20" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="Checkbox21" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField21" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="Checkbox22" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField22" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                         <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="Checkbox23" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField23" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="Checkbox24" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField24" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="Checkbox25" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField25" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                         <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="Checkbox26" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField26" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="Checkbox27" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField27" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ckExtField10" id="Checkbox28" value="ExtField10" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField28" value="" size="12" style="display: none" />
                                                            </td>
                                                        </tr>
                                                         <tr class='ext'>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField8" id="Checkbox29" value="ExtField8" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField29" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <input type="checkbox" name="ckExtField9" id="Checkbox30" value="ExtField9" style="display: none" /><input readonly="readonly" 
                                                                    type="text" id="txtExtField30" value="" size="12" style="display: none" />
                                                            </td>
                                                            <td>
                                                                
                                                            </td>
                                                        </tr>
                                                    </tbody>                         
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <input type="hidden" id="hidBillTypeFlag" name="hidBillTypeFlag" runat="server" />
                                    <input type="hidden" id="hidPrintTypeFlag" name="hidPrintTypeFlag" runat="server" />
                                    <input type="hidden" id="isSeted" value="0" runat="server" />
                                    <input type="hidden" id="hidPlanNo" name="planno" runat="server" />
                                    <input type="hidden" id="hidCustBig" name="hidCustBig" runat="server" />
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
     var CustBig = document.getElementById("hidCustBig").value;
    
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
        LoadExtTableName('officedba.CustInfo');
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
                                ['ckExtField11', 'ExtField11'],
                                ['ckExtField12', 'ExtField12'],
                                ['ckExtField13', 'ExtField13'],
                                ['ckExtField14', 'ExtField14'],
                                ['ckExtField15', 'ExtField15'],
                                ['ckExtField16', 'ExtField16'],
                                ['ckExtField17', 'ExtField17'],
                                ['ckExtField18', 'ExtField18'],
                                ['ckExtField19', 'ExtField19'],
                                ['ckExtField20', 'ExtField20'],
                                ['ckExtField21', 'ExtField21'],
                                ['ckExtField22', 'ExtField22'],
                                ['ckExtField23', 'ExtField23'],
                                ['ckExtField24', 'ExtField24'],
                                ['ckExtField25', 'ExtField25'],
                                ['ckExtField26', 'ExtField26'],
                                ['ckExtField27', 'ExtField27'],
                                ['ckExtField28', 'ExtField28'],
                                ['ckExtField29', 'ExtField29'],
                                ['ckExtField30', 'ExtField30'],
                                
                                ['ckCustNo', 'CustNo'],
                                ['ckCustName', 'CustName'],
                                ['ckCustBig', 'CustBig'],
                                
                                ['ckCustNam', 'CustNam'],
                                ['ckCustShort', 'CustShort'],
                                ['ckCustTypeManage', 'CustTypeManage'],
                                
                                ['ckCustTypeSell', 'CustTypeSell'],
                                ['ckCreditGradeNm', 'CreditGradeNm'],                                
                                ['ckCustTypeTime', 'CustTypeTime'],
                                
                                ['ckCustClassName', 'CustClassName'],                                
                                ['ckCustTypaNm', 'CustTypaNm'],
                                
                                ['ckCreatorName', 'CreatorName'],
                                
                                ['ckCreatedDate', 'CreatedDate'],
                                
                                ['ckCustNote', 'CustNote'],
                                ['ckCountryName', 'CountryName'],
                                
                                ['ckAreaName', 'AreaName'],                                
                                ['ckProvince', 'Province'],
                                
                                ['ckCity', 'City'],
                                
                                ['ckBusiType', 'BusiType'],
                                ['ckManagerName', 'ManagerName'],
                                ['ckContactName', 'ContactName'],
                                ['ckTel', 'Tel'],
                                ['ckMobile', 'Mobile'],
                                ['ckFax', 'Fax'],
                                ['ckOnLine', 'OnLine'],
                                ['ckWebSite', 'WebSite'],
                                ['ckPost', 'Post'],
                                ['ckemail', 'email'],
                                ['ckFirstBuyDate', 'FirstBuyDate'],
                                ['ckCarryTypeNm', 'CarryTypeNm'],
                                ['ckTakeTypeNm', 'TakeTypeNm'],
                                ['ckLinkCycleNm', 'LinkCycleNm'],
                                ['ckReceiveAddress', 'ReceiveAddress'],
                                ['ckSellArea', 'SellArea'],
                                ['ckCreditManage', 'CreditManage'],
                                ['ckMaxCredit', 'MaxCredit'],
                                ['ckMaxCreditDate', 'MaxCreditDate'],
                                ['ckPayTypeNm', 'PayTypeNm'],
                                ['ckCurrencyaNm', 'CurrencyaNm'],
                                ['ckBillTypeNm', 'BillTypeNm'],
                                ['ckMoneyTypeNm', 'MoneyTypeNm'],
                                ['ckOpenBank', 'OpenBank'],
                                ['ckAccountMan', 'AccountMan'],
                                ['ckAccountNum', 'AccountNum'],
                                ['ckCompanyType', 'CompanyType'],
                                ['ckCapitalScale', 'CapitalScale'],
                                ['ckSetupDate', 'SetupDate'],
                                ['ckSetupMoney', 'SetupMoney'],
                                ['ckStaffCount', 'StaffCount'],
                                ['ckArtiPerson', 'ArtiPerson'],
                                ['ckTrade', 'Trade'],
                                ['ckBusiNumber', 'BusiNumber'],
                                ['ckSetupAddress', 'SetupAddress'],
                                ['ckTaxCD', 'TaxCD'],
                                ['ckIsTax', 'IsTax'],
                                ['ckSource', 'Source'],
                                ['ckSaleroomY', 'SaleroomY'],
                                ['ckProfitY', 'ProfitY'],
                                ['ckSellMode', 'SellMode'],
                                ['ckCustSupe', 'CustSupe'],
                                ['ckMeritGrade', 'MeritGrade'],
                                ['ckPhase', 'Phase'],
                                ['ckHotIs', 'HotIs'],
                                ['ckHotHow', 'HotHow'],
                                ['ckRelaGrade', 'RelaGrade'],
                                ['ckUsedStatus', 'UsedStatus'],
                                ['ckRelation', 'Relation'],
                                ['ckRemark', 'Remark'],
                                ['ckModifiedUserID', 'ModifiedUserID'],
                                ['ckModifiedDate', 'ModifiedDate'],
                                ['ckCompanyValues', 'CompanyValues'],
                                ['ckCatchWord', 'CatchWord'],
                                ['ckManageValues', 'ManageValues'],
                                ['ckPotential', 'Potential'],
                                ['ckProblem', 'Problem'],
                                ['ckAdvantages', 'Advantages'],
                                ['ckTradePosition', 'TradePosition'],
                                ['ckCompetition', 'Competition'],
                                ['ckCollaborator', 'Collaborator'],
                                ['ckManagePlan', 'ManagePlan'],
                                ['ckCollaborate', 'Collaborate'],
                                ['ckCanViewUserName', 'CanViewUserName']
                                );
                                       
        /*加载打印参数设置
          注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
        */
        LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase,"",null);
    }

    /*3:保存打印模板设置*/
    function SavePrintSetting() {

        var strBaseFields = "";
        var strDetailFields = "";
        var strDetailSecondFields = "";
        var toLocation='CustAddPrint.aspx?id=' + PlanNo + '&CustBig=' + CustBig;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        
        if (document.getElementById('ckCustNo').checked) strBaseFields = strBaseFields + "CustNo|";
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";
        if (document.getElementById('ckCustBig').checked) strBaseFields = strBaseFields + "CustBig|";
        
        if (document.getElementById('ckCustNam').checked) strBaseFields = strBaseFields + "CustNam|";        
        if (document.getElementById('ckCustShort').checked) strBaseFields = strBaseFields + "CustShort|";
        if (document.getElementById('ckCustTypeManage').checked) strBaseFields = strBaseFields + "CustTypeManage|"; 
               
        if (document.getElementById('ckCustTypeSell').checked) strBaseFields = strBaseFields + "CustTypeSell|";
        if (document.getElementById('ckCreditGradeNm').checked) strBaseFields = strBaseFields + "CreditGradeNm|";        
        if (document.getElementById('ckCustTypeTime').checked) strBaseFields = strBaseFields + "CustTypeTime|";   
             
        if (document.getElementById('ckCustClassName').checked) strBaseFields = strBaseFields + "CustClassName|";
        if (document.getElementById('ckCustTypaNm').checked) strBaseFields = strBaseFields + "CustTypaNm|";
        
        if (document.getElementById('ckCreatorName').checked) strBaseFields = strBaseFields + "CreatorName|";
        
        if (document.getElementById('ckCreatedDate').checked) strBaseFields = strBaseFields + "CreatedDate|";
        
        if (document.getElementById('ckCustNote').checked) strBaseFields = strBaseFields + "CustNote|";
        if (document.getElementById('ckCountryName').checked) strBaseFields = strBaseFields + "CountryName|";
        
        if (document.getElementById('ckAreaName').checked) strBaseFields = strBaseFields + "AreaName|";
        if (document.getElementById('ckProvince').checked) strBaseFields = strBaseFields + "Province|";
        
        if (document.getElementById('ckCity').checked) strBaseFields = strBaseFields + "City|";
        
        if (document.getElementById('ckBusiType').checked) strBaseFields = strBaseFields + "BusiType|";
        if (document.getElementById('ckManagerName').checked) strBaseFields = strBaseFields + "ManagerName|";
        if (document.getElementById('ckContactName').checked) strBaseFields = strBaseFields + "ContactName|";
        if (document.getElementById('ckTel').checked) strBaseFields = strBaseFields + "Tel|";
        if (document.getElementById('ckMobile').checked) strBaseFields = strBaseFields + "Mobile|";
        if (document.getElementById('ckFax').checked) strBaseFields = strBaseFields + "Fax|";
        if (document.getElementById('ckOnLine').checked) strBaseFields = strBaseFields + "OnLine|";
        if (document.getElementById('ckWebSite').checked) strBaseFields = strBaseFields + "WebSite|";
        if (document.getElementById('ckPost').checked) strBaseFields = strBaseFields + "Post|";
        if (document.getElementById('ckemail').checked) strBaseFields = strBaseFields + "email|";
        if (document.getElementById('ckFirstBuyDate').checked) strBaseFields = strBaseFields + "FirstBuyDate|";
        if (document.getElementById('ckCarryTypeNm').checked) strBaseFields = strBaseFields + "CarryTypeNm|";
        if (document.getElementById('ckTakeTypeNm').checked) strBaseFields = strBaseFields + "TakeTypeNm|";
        if (document.getElementById('ckLinkCycleNm').checked) strBaseFields = strBaseFields + "LinkCycleNm|";
        if (document.getElementById('ckReceiveAddress').checked) strBaseFields = strBaseFields + "ReceiveAddress|";
        if (document.getElementById('ckSellArea').checked) strBaseFields = strBaseFields + "SellArea|";
        if (document.getElementById('ckCreditManage').checked) strBaseFields = strBaseFields + "CreditManage|";
        if (document.getElementById('ckMaxCredit').checked) strBaseFields = strBaseFields + "MaxCredit|";
        if (document.getElementById('ckMaxCreditDate').checked) strBaseFields = strBaseFields + "MaxCreditDate|";
        if (document.getElementById('ckPayTypeNm').checked) strBaseFields = strBaseFields + "PayTypeNm|";
        if (document.getElementById('ckCurrencyaNm').checked) strBaseFields = strBaseFields + "CurrencyaNm|";
        if (document.getElementById('ckBillTypeNm').checked) strBaseFields = strBaseFields + "BillTypeNm|";
        if (document.getElementById('ckMoneyTypeNm').checked) strBaseFields = strBaseFields + "MoneyTypeNm|";
        if (document.getElementById('ckOpenBank').checked) strBaseFields = strBaseFields + "OpenBank|";
        if (document.getElementById('ckAccountMan').checked) strBaseFields = strBaseFields + "AccountMan|";
        if (document.getElementById('ckAccountNum').checked) strBaseFields = strBaseFields + "AccountNum|";
        if (document.getElementById('ckCompanyType').checked) strBaseFields = strBaseFields + "CompanyType|";
        if (document.getElementById('ckCapitalScale').checked) strBaseFields = strBaseFields + "CapitalScale|";
        if (document.getElementById('ckSetupDate').checked) strBaseFields = strBaseFields + "SetupDate|";
        if (document.getElementById('ckSetupMoney').checked) strBaseFields = strBaseFields + "SetupMoney|";
        if (document.getElementById('ckStaffCount').checked) strBaseFields = strBaseFields + "StaffCount|";
        if (document.getElementById('ckArtiPerson').checked) strBaseFields = strBaseFields + "ArtiPerson|";
        if (document.getElementById('ckTrade').checked) strBaseFields = strBaseFields + "Trade|";
        if (document.getElementById('ckBusiNumber').checked) strBaseFields = strBaseFields + "BusiNumber|";
        if (document.getElementById('ckSetupAddress').checked) strBaseFields = strBaseFields + "SetupAddress|";
        if (document.getElementById('ckTaxCD').checked) strBaseFields = strBaseFields + "TaxCD|";
        if (document.getElementById('ckIsTax').checked) strBaseFields = strBaseFields + "IsTax|";
        if (document.getElementById('ckSource').checked) strBaseFields = strBaseFields + "Source|";
        if (document.getElementById('ckSaleroomY').checked) strBaseFields = strBaseFields + "SaleroomY|";
        if (document.getElementById('ckProfitY').checked) strBaseFields = strBaseFields + "ProfitY|";
        if (document.getElementById('ckSellMode').checked) strBaseFields = strBaseFields + "SellMode|";
        if (document.getElementById('ckCustSupe').checked) strBaseFields = strBaseFields + "CustSupe|";
        if (document.getElementById('ckMeritGrade').checked) strBaseFields = strBaseFields + "MeritGrade|";
        if (document.getElementById('ckPhase').checked) strBaseFields = strBaseFields + "Phase|";
        if (document.getElementById('ckHotIs').checked) strBaseFields = strBaseFields + "HotIs|";
        if (document.getElementById('ckHotHow').checked) strBaseFields = strBaseFields + "HotHow|";
        if (document.getElementById('ckRelaGrade').checked) strBaseFields = strBaseFields + "RelaGrade|";
        if (document.getElementById('ckUsedStatus').checked) strBaseFields = strBaseFields + "UsedStatus|";
        if (document.getElementById('ckRelation').checked) strBaseFields = strBaseFields + "Relation|";
        if (document.getElementById('ckRemark').checked) strBaseFields = strBaseFields + "Remark|";
        if (document.getElementById('ckModifiedUserID').checked) strBaseFields = strBaseFields + "ModifiedUserID|";
        if (document.getElementById('ckModifiedDate').checked) strBaseFields = strBaseFields + "ModifiedDate|";
        if (document.getElementById('ckCompanyValues').checked) strBaseFields = strBaseFields + "CompanyValues|";
        if (document.getElementById('ckCatchWord').checked) strBaseFields = strBaseFields + "CatchWord|";
        if (document.getElementById('ckManageValues').checked) strBaseFields = strBaseFields + "ManageValues|";
        if (document.getElementById('ckPotential').checked) strBaseFields = strBaseFields + "Potential|";
        if (document.getElementById('ckProblem').checked) strBaseFields = strBaseFields + "Problem|";
        if (document.getElementById('ckAdvantages').checked) strBaseFields = strBaseFields + "Advantages|";
        if (document.getElementById('ckTradePosition').checked) strBaseFields = strBaseFields + "TradePosition|";
        if (document.getElementById('ckCompetition').checked) strBaseFields = strBaseFields + "Competition|";
        if (document.getElementById('ckCollaborator').checked) strBaseFields = strBaseFields + "Collaborator|";
        if (document.getElementById('ckManagePlan').checked) strBaseFields = strBaseFields + "ManagePlan|";
        if (document.getElementById('ckCollaborate').checked) strBaseFields = strBaseFields + "Collaborate|";
        if (document.getElementById('ckCanViewUserName').checked) strBaseFields = strBaseFields + "CanViewUserName|";
              
        for(var i=0;i<10;i++)
        {
            if(document.getElementById('ckExtField'+(i+1)).style.display=='block'||document.getElementById('ckExtField'+(i+1)).style.display=='')
            {
                if (document.getElementById('ckExtField'+(i+1)).checked) strBaseFields = strBaseFields + "ExtField"+(i+1)+"|";
            }
            
        }
        /*明细信息*/
          
     
        /*保存打印参数设置*/
        
        SaveCommonPrintParameterSet(strBaseFields,strDetailFields,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }

    
</script>
