<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustLinkPrint.aspx.cs" Inherits="Pages_PrinttingModel_CustManager_CustLinkPrint" ValidateRequest="false" %>

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
                                <div id="divSet" class="setDiv" style=" width:70%;display: none; "><%-- --%>
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
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustShort" value="ckCustID" /><input type="text" readonly="readonly" 
                                                                id="Text1" value="拼音缩写：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustTypeManage" value="ckCustTel" /><input type="text" readonly="readonly" 
                                                                id="Text2" value="客户管理分类：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustTypeSell" value="ckCustType" /><input readonly="readonly" 
                                                                type="text" id="Text3" value="客户营销分类：" size="12" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCreditGradeNm" value="ckHapSource" /><input type="text" readonly="readonly" 
                                                                id="Text4" value="客户优质级别：" size="14" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCustTypeTime" value="ckFindDate" /><input type="text" readonly="readonly" 
                                                                id="Text5" value="客户时间分类：" size="14" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustClassName" value="ckSeller" /><input readonly="readonly" 
                                                                type="text" id="Text6" value="客户细分：" size="14" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckSex" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text28" value="性别：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckLinkTypeName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text29" value="联系人类型：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckPaperNum" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text30" value="身份证号：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustTypaNm" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text8" value="客户类别：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCreatorName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text9" value="建档人：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCreatedDate" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text10" value="建档日期：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                           业务信息
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCustNote" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text13" value="客户简介：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCountryName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text14" value="国家地区：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckAreaName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text15" value="区域：" size="12" />
                                                        </td>                                                        
                                                    </tr>                                                    
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckBusiType" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text16" value="业务类型：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckManagerName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text17" value="分管业务员：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckLinkCycleNm" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text18" value="联络期限(天)：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            财务信息
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckReceiveAddress" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text19" value="收货地址：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCreditManage" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text20" value="允许延期付款：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckMaxCreditDate" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text21" value="帐期天数(天)：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckPayTypeNm" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text22" value="结算方式：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckRelaGrade" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text23" value="关系等级：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckUsedStatus" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text24" value="启用状态：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            客户关怀
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckModifiedUserID" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text25" value="最后更新用户：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckModifiedDate" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text26" value="最后更新日期：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckCustNum" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text27" value="卡号：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckBirthday" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text31" value="生日：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckWorkTel" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text32" value="电话：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckHandset" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text33" value="手机：" size="12" />
                                                        </td>                                                        
                                                    </tr>                                                   
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckFax" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text34" value="传真：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckPosition" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text35" value="职务：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckAge" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text36" value="年龄：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckPost" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text37" value="邮编：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckMailAddress" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text38" value="Eamil：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckHomeTown" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text39" value="籍贯：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckNationalName" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text40" value="民族：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCultureLevelName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text41" value="所受教育：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckProfessionalName" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text42" value="所学专业：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckIncomeYear" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text43" value="年收入情况：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckFuoodDrink" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text44" value="饮食偏好：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckLoveMusic" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text45" value="喜欢的音乐：" size="12" />
                                                        </td>                                                        
                                                    </tr>                                                    
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckLoveColor" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text46" value="喜欢的颜色：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckLoveSmoke" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text47" value="喜欢的香烟：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckLoveDrink" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text48" value="爱喝的酒：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckLoveTea" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text49" value="爱喝的茶：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckLoveBook" id="ckLoveBook" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text50" value="喜欢的书籍：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckLoveSport" id="ckLoveSport" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text51" value="喜欢的运动：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckLoveClothes" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text52" value="喜欢的品牌服饰：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCosmetic" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text53" value="喜欢的品牌化妆品：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckNature" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text54" value="性格描述：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckAppearance" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text55" value="外表描述：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckAdoutBody" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text56" value="健康状况：" size="12" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckPricipalReal" id="ckAboutFamily" value="PricipalReal" /><input readonly="readonly" 
                                                                type="text" id="Text57" value="家人情况：" size="12" />
                                                        </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckMRPNo" id="ckCar" value="MRPNo" /><input type="text" readonly="readonly" 
                                                                id="Text58" value="开什么车：" size="12" />
                                                        </td>
                                                        <td width="20%" align="left">
                                                            <input type="checkbox" name="ckSubject" id="ckCanViewUserName" value="Subject" /><input type="text" readonly="readonly" 
                                                                id="Text59" value="可查看该客户档案的人员：" size="12" />
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
                                ['ckCustBig', 'BigType'],                                
                               
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
                               
                                
                                ['ckBusiType', 'BusiType'],
                                ['ckManagerName', 'ManagerName'],
                               
                                ['ckLinkCycleNm', 'LinkCycleNm'],
                                ['ckReceiveAddress', 'ReceiveAddress'],
                                
                                ['ckCreditManage', 'CreditManage'],
                              
                                ['ckMaxCreditDate', 'MaxCreditDate'],
                                ['ckPayTypeNm', 'PayTypeNm'],
                               
                                ['ckRelaGrade', 'RelaGrade'],
                                ['ckUsedStatus', 'UsedStatus'],
                               
                                ['ckModifiedUserID', 'ModifiedUserID'],
                                ['ckModifiedDate', 'ModifiedDate'],
                                ['ckCustNum', 'CustNum'],
                                ['ckSex', 'Sex'],
                                ['ckLinkTypeName', 'LinkTypeName'],
                                ['ckPaperNum', 'PaperNum'],
                                ['ckBirthday', 'Birthday'],
                                ['ckWorkTel', 'WorkTel'],
                                ['ckHandset', 'Handset'],
                                ['ckFax', 'Fax'],
                                ['ckPosition', 'Position'],
                                ['ckAge', 'Age'],
                                ['ckPost', 'Post'],
                                ['ckMailAddress', 'MailAddress'],
                                ['ckHomeTown', 'HomeTown'],
                                ['ckNationalName', 'NationalName'],
                                ['ckCultureLevelName', 'CultureLevelName'],
                                ['ckProfessionalName', 'ProfessionalName'],
                                ['ckIncomeYear', 'IncomeYear'],
                                ['ckFuoodDrink', 'FuoodDrink'],
                                ['ckLoveMusic', 'LoveMusic'],
                                ['ckLoveColor', 'LoveColor'],
                                ['ckLoveSmoke', 'LoveSmoke'],
                                ['ckLoveDrink', 'LoveDrink'],
                                ['ckLoveTea', 'LoveTea'],
                                ['ckLoveBook', 'LoveBook'],
                                ['ckLoveSport', 'LoveSport'],
                                ['ckLoveClothes', 'LoveClothes'],
                                ['ckCosmetic', 'Cosmetic'],
                                ['ckNature', 'Nature'],                                
                                ['ckAppearance', 'Appearance'],
                                ['ckAdoutBody', 'AdoutBody'],
                                ['ckAboutFamily', 'AboutFamily'],
                                ['ckCar', 'Car'],
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
        var toLocation='CustLinkPrint.aspx?id=' + PlanNo + '&CustBig=' + CustBig;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        
        if (document.getElementById('ckCustNo').checked) strBaseFields = strBaseFields + "CustNo|";
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";
        if (document.getElementById('ckCustBig').checked) strBaseFields = strBaseFields + "BigType|";
            
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
               
        if (document.getElementById('ckBusiType').checked) strBaseFields = strBaseFields + "BusiType|";
        if (document.getElementById('ckManagerName').checked) strBaseFields = strBaseFields + "ManagerName|";
      
        if (document.getElementById('ckReceiveAddress').checked) strBaseFields = strBaseFields + "ReceiveAddress|";
        
        if (document.getElementById('ckCreditManage').checked) strBaseFields = strBaseFields + "CreditManage|";
        
        if (document.getElementById('ckMaxCreditDate').checked) strBaseFields = strBaseFields + "MaxCreditDate|";
        if (document.getElementById('ckPayTypeNm').checked) strBaseFields = strBaseFields + "PayTypeNm|";
        
        if (document.getElementById('ckRelaGrade').checked) strBaseFields = strBaseFields + "RelaGrade|";
        if (document.getElementById('ckUsedStatus').checked) strBaseFields = strBaseFields + "UsedStatus|";
       
        if (document.getElementById('ckModifiedUserID').checked) strBaseFields = strBaseFields + "ModifiedUserID|";
        if (document.getElementById('ckModifiedDate').checked) strBaseFields = strBaseFields + "ModifiedDate|";
        
        if (document.getElementById('ckCustNum').checked) strBaseFields = strBaseFields + "CustNum|";
        if (document.getElementById('ckSex').checked) strBaseFields = strBaseFields + "Sex|";
        if (document.getElementById('ckLinkTypeName').checked) strBaseFields = strBaseFields + "LinkTypeName|";
        if (document.getElementById('ckPaperNum').checked) strBaseFields = strBaseFields + "PaperNum|";
        if (document.getElementById('ckBirthday').checked) strBaseFields = strBaseFields + "Birthday|";
        if (document.getElementById('ckWorkTel').checked) strBaseFields = strBaseFields + "WorkTel|";
        if (document.getElementById('ckHandset').checked) strBaseFields = strBaseFields + "Handset|";
        if (document.getElementById('ckFax').checked) strBaseFields = strBaseFields + "Fax|";
        if (document.getElementById('ckPosition').checked) strBaseFields = strBaseFields + "Position|";
        if (document.getElementById('ckAge').checked) strBaseFields = strBaseFields + "Age|";
        if (document.getElementById('ckPost').checked) strBaseFields = strBaseFields + "Post|";
        if (document.getElementById('ckMailAddress').checked) strBaseFields = strBaseFields + "MailAddress|";
        if (document.getElementById('ckHomeTown').checked) strBaseFields = strBaseFields + "HomeTown|";
        if (document.getElementById('ckNationalName').checked) strBaseFields = strBaseFields + "NationalName|";
        if (document.getElementById('ckCultureLevelName').checked) strBaseFields = strBaseFields + "CultureLevelName|";
        if (document.getElementById('ckProfessionalName').checked) strBaseFields = strBaseFields + "ProfessionalName|";
        if (document.getElementById('ckIncomeYear').checked) strBaseFields = strBaseFields + "IncomeYear|";
        if (document.getElementById('ckFuoodDrink').checked) strBaseFields = strBaseFields + "FuoodDrink|";
        if (document.getElementById('ckLoveMusic').checked) strBaseFields = strBaseFields + "LoveMusic|";
        if (document.getElementById('ckLoveColor').checked) strBaseFields = strBaseFields + "LoveColor|";
        if (document.getElementById('ckLoveSmoke').checked) strBaseFields = strBaseFields + "LoveSmoke|";
        if (document.getElementById('ckLoveDrink').checked) strBaseFields = strBaseFields + "LoveDrink|";
        if (document.getElementById('ckLoveTea').checked) strBaseFields = strBaseFields + "LoveTea|";
        if (document.getElementById('ckLoveBook').checked) strBaseFields = strBaseFields + "LoveBook|";
        if (document.getElementById('ckLoveSport').checked) strBaseFields = strBaseFields + "LoveSport|";
        if (document.getElementById('ckLoveClothes').checked) strBaseFields = strBaseFields + "LoveClothes|";
        if (document.getElementById('ckCosmetic').checked) strBaseFields = strBaseFields + "Cosmetic|";
        if (document.getElementById('ckNature').checked) strBaseFields = strBaseFields + "Nature|";
        if (document.getElementById('ckAppearance').checked) strBaseFields = strBaseFields + "Appearance|";
        if (document.getElementById('ckAdoutBody').checked) strBaseFields = strBaseFields + "AdoutBody|";
        if (document.getElementById('ckAboutFamily').checked) strBaseFields = strBaseFields + "AboutFamily|";
        if (document.getElementById('ckCar').checked) strBaseFields = strBaseFields + "Car|";       
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
