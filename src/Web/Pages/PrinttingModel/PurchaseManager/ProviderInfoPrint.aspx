<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderInfoPrint.aspx.cs" Inherits="Pages_PrinttingModel_PurchaseManager_ProviderInfoPrint" ValidateRequest="false" %>

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

    <title>供应商档案</title>

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
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckCustNo" id="ckCustNo" value="CustNo" /><input type="text" readonly="readonly"
                                                                id="txtCustNo" value="供应商编号：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckCustTypeName" id="ckCustTypeName" value="CustTypeName" /><input type="text" readonly="readonly"
                                                                id="txtCustType" value="供应商类别：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckCustClassName" id="ckCustClassName" value="CustClass" /><input
                                                                type="text" readonly="readonly" id="txtCustClass" value="供应商分类：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckCustName" id="ckCustName" value="CustName" /><input
                                                                type="text" readonly="readonly" id="txtCustName" value="供应商名称：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckCustNam" id="ckCustNam" value="CustNam" /><input type="text" readonly="readonly"
                                                                id="txtCustNam" value="供应商简称：" size="20" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckPYShort" id="ckPYShort" value="PYShort"  /><input
                                                                type="text" readonly="readonly" id="txtPYShort" value="供应商拼音代码" size="20"  />
                                                        </td>
                                                    </tr>
                                                    
                                                      <tr>
                                                        <td  align="left" colspan="3">
                                                            <input type="checkbox" name="ckCustNote" id="ckCustNote" value="CustNote" /><input
                                                                type="text" readonly="readonly" id="txtCustNote" value="供应商简介：" size="20" />
                                                        </td>
                                                       
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            业务信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckCountryName" id="ckCountryName" value="CountryID" /><input
                                                                type="text" readonly="readonly" id="txtCountryID" value="国家地区：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckProvince" id="ckProvince" value="Province" /><input
                                                                type="text" readonly="readonly" id="txtProvince" value="省：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckCity" id="ckCity" value="City" /><input
                                                                type="text" readonly="readonly" id="txtCity" value="市：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                       <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckPost" id="ckPost" value="Post" /><input
                                                                type="text" readonly="readonly" id="txtPost" value="邮编：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckContactName" id="ckContactName" value="ContactName" /><input
                                                                type="text" readonly="readonly" id="txtContactName" value="联系人：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckTel" id="ckTel" value="Tel" /><input
                                                                type="text" readonly="readonly" id="txtTel" value="电话：" size="20" />
                                                        </td>
                                                    </tr>
                                                       <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckFax" id="ckFax" value="Fax" /><input
                                                                type="text" readonly="readonly" id="txtFax" value="传真：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckMobile" id="ckMobile" value="Mobile" /><input
                                                                type="text" readonly="readonly" id="txtMobile" value="手机：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckemail" id="ckemail" value="email" /><input
                                                                type="text" readonly="readonly" id="txtemail" value="邮件：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                     <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckOnLine" id="ckOnLine" value="OnLine" /><input
                                                                type="text" readonly="readonly" id="txtOnLine" value="在线咨询：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckWebSite" id="ckWebSite" value="WebSite" /><input
                                                                type="text" readonly="readonly" id="txtWebSite" value="公司网址：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckTakeTypeName" id="ckTakeTypeName" value="TakeType" /><input
                                                                type="text" readonly="readonly" id="txtTakeType" value="交货方式：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                      <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckCarryTypeName" id="ckCarryTypeName" value="CarryType" /><input
                                                                type="text" readonly="readonly" id="txtCarryType" value="运送方式：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckCreditGradeName" id="ckCreditGradeName" value="CreditGrade" /><input
                                                                type="text" readonly="readonly" id="txtCreditGrade" value="供应商优先级别：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckHotIsName" id="ckHotIsName" value="HotIs" /><input
                                                                type="text" readonly="readonly" id="txtHotIs" value="热点供应商：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                       <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckUsedStatusName" id="ckUsedStatusName" value="UsedStatus" /><input
                                                                type="text" readonly="readonly" id="txtUsedStatus" value="启用状态：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckManagerName" id="ckManagerName" value="Manager" /><input
                                                                type="text" readonly="readonly" id="txtManager" value="分管采购员：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckLinkCycle" id="ckLinkCycle" value="LinkCycle" /><input
                                                                type="text" readonly="readonly" id="txtLinkCycle" value="联络期限：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                       <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckAreaName" id="ckAreaName" value="AreaID" /><input
                                                                type="text" readonly="readonly" id="txtAreaID" value="所在地区：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckSendAddress" id="ckSendAddress" value="SendAddress" /><input
                                                                type="text" readonly="readonly" id="txtSendAddress" value="发货地址：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckSellArea" id="ckSellArea" value="SellArea" /><input
                                                                type="text" readonly="readonly" id="txtSellArea" value="经营范围：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            财务信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckPayTypeName" id="ckPayTypeName" value="PayType" /><input
                                                                type="text" readonly="readonly" id="txtPayType" value="结算方式：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckCurrencyTypeName" id="ckCurrencyTypeName" value="CurrencyTypeName" /><input
                                                                type="text" readonly="readonly" id="txtCurrencyTypeName" value="币种：" size="20" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckOpenBank" id="ckOpenBank" value="OpenBank" /><input
                                                                type="text" readonly="readonly" id="txtOpenBank" value="开户行：" size="20" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckAccountMan" id="ckAccountMan" value="AccountMan" /><input
                                                                type="text" readonly="readonly" id="txtAccountMan" value="户名：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckAccountNum" id="ckAccountNum" value="AccountNum" /><input
                                                                type="text" readonly="readonly" id="txtAccountNum" value="帐号：" size="20" />
                                                        </td>
                                                        <td>
                                                           &nbsp;
                                                        </td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            辅助信息
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                    <tr>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckSetupDate" id="ckSetupDate" value="SetupDate" /><input
                                                                type="text" readonly="readonly" id="txtSetupDate" value="成立时间：" size="20" />
                                                                
                                                          
                                                        </td>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckArtiPerson" id="ckArtiPerson" value="ArtiPerson" /><input
                                                                type="text" readonly="readonly" id="txtArtiPerson" value="法人代表：" size="20" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckIsTaxName" id="ckIsTaxName" value="IsTax" /><input
                                                                type="text" readonly="readonly" id="txtIsTax" value="一般纳税人：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckTaxCD" id="ckTaxCD" value="TaxCD" /><input type="text" readonly="readonly"
                                                                id="txtTaxCD" value="税务登记号：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                          <input type="checkbox" name="ckBusiNumber" id="ckBusiNumber" value="BusiNumber" /><input
                                                                type="text" readonly="readonly" id="txtBusiNumber" value="营业执照号：" size="20" />
                                                        </td>
                                                        <td>
                                                          <input type="checkbox" name="ckHotHowName" id="ckHotHowName" value="HotHow" /><input
                                                                type="text" readonly="readonly" id="txtHotHow" value="热度：" size="20" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <input type="hidden" id="hidBillTypeFlag" name="hidBillTypeFlag" runat="server" />
                                    <input type="hidden" id="hidPrintTypeFlag" name="hidPrintTypeFlag" runat="server" />
                                    <input type="hidden" id="isSeted" value="0" runat="server" />
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
    var intMrpID = <%=intMrpID %>;
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
        //LoadExtTableName('officedba.MRP');
        /*加载打印模板设置信息*/
        LoadPrintSettingInfo();

    }
    


    /*2:加载打印模板设置信息*/
    function LoadPrintSettingInfo() {

        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        var hidIsSeted = document.getElementById('isSeted').value;


        /*主表：复选框及其对应的字段*/
        var dbBase = new Array( [ 'ckCustNo', 'CustNo'], 
                                [ 'ckCustTypeName', 'CustTypeName'], 
                                [ 'ckCustClassName', 'CustClassName'],
                                [ 'ckCustName', 'CustName'],
                                [ 'ckCustNam', 'CustNam'],
                                [ 'ckPYShort', 'PYShort'],
                                [ 'ckCustNote', 'CustNote'],
                                [ 'ckCountryName', 'CountryName'],
                                [ 'ckProvince', 'Province'],
                                [ 'ckCity', 'City'],
                                [ 'ckPost', 'Post'],
                                [ 'ckContactName', 'ContactName'],
                                [ 'ckTel', 'Tel'],
                                [ 'ckFax', 'Fax'],
                                [ 'ckMobile', 'Mobile'],
                                [ 'ckemail', 'email'],
                                [ 'ckOnLine', 'OnLine'],
                                [ 'ckWebSite', 'WebSite'],
                                [ 'ckTakeTypeName', 'TakeTypeName'],
                                [ 'ckCarryTypeName', 'CarryTypeName'],
                                [ 'ckCreditGradeName', 'CreditGradeName'],
                                [ 'ckHotIsName', 'HotIsName'],
                                [ 'ckUsedStatusName', 'UsedStatusName'],
                                [ 'ckManagerName', 'ManagerName'],
                                [ 'ckLinkCycle', 'LinkCycle'],
                                [ 'ckAreaName', 'AreaName'],
                                [ 'ckSendAddress', 'SendAddress'],
                                [ 'ckSellArea', 'SellArea'],
                                [ 'ckPayTypeName', 'PayTypeName'],
                                [ 'ckCurrencyTypeName', 'CurrencyTypeName'],
                                [ 'ckOpenBank', 'OpenBank'],
                                [ 'ckAccountMan', 'AccountMan'],
                                [ 'ckAccountNum', 'AccountNum'],
                                [ 'ckSetupDate', 'SetupDate'],
                                [ 'ckArtiPerson', 'ArtiPerson'],
                                [ 'ckIsTaxName', 'IsTaxName'],
                                [ 'ckTaxCD', 'TaxCD'],
                                [ 'ckBusiNumber', 'BusiNumber'],
                                [ 'ckHotHowName', 'HotHowName'] );
      
                                 

        /*加载打印参数设置
          注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
        */
        LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase, null,null);
    }

    /*3:保存打印模板设置*/
    function SavePrintSetting() {

        
        var strBaseFields = "";
        var strDetailFields = "";
        var strDetailSecondFields = "";
        var toLocation='ProviderInfoPrint.aspx?ID=' + intMrpID;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/                 
        if (document.getElementById('ckCustNo').checked) strBaseFields = strBaseFields + "CustNo|";
        if (document.getElementById('ckCustTypeName').checked) strBaseFields = strBaseFields + "CustTypeName|";
        if (document.getElementById('ckCustClassName').checked) strBaseFields = strBaseFields + "CustClassName|";
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|";
        if (document.getElementById('ckCustNam').checked) strBaseFields = strBaseFields + "CustNam|";
        if (document.getElementById('ckPYShort').checked) strBaseFields = strBaseFields + "PYShort|";
        if (document.getElementById('ckCustNote').checked) strBaseFields = strBaseFields + "CustNote|";
        if (document.getElementById('ckCountryName').checked) strBaseFields = strBaseFields + "CountryName|";
        if (document.getElementById('ckProvince').checked) strBaseFields = strBaseFields + "Province|";
        if (document.getElementById('ckCity').checked) strBaseFields = strBaseFields + "City|";
        if (document.getElementById('ckPost').checked) strBaseFields = strBaseFields + "Post|";
        if (document.getElementById('ckContactName').checked) strBaseFields = strBaseFields + "ContactName|";
        if (document.getElementById('ckTel').checked) strBaseFields = strBaseFields + "Tel|";
        if (document.getElementById('ckFax').checked) strBaseFields = strBaseFields + "Fax|";
        if (document.getElementById('ckMobile').checked) strBaseFields = strBaseFields + "Mobile|";
        if (document.getElementById('ckemail').checked) strBaseFields = strBaseFields + "email|";
        if (document.getElementById('ckOnLine').checked) strBaseFields = strBaseFields + "OnLine|";
        if (document.getElementById('ckWebSite').checked) strBaseFields = strBaseFields + "WebSite|";
        if (document.getElementById('ckTakeTypeName').checked) strBaseFields = strBaseFields + "TakeTypeName|";
        if (document.getElementById('ckCarryTypeName').checked) strBaseFields = strBaseFields + "CarryTypeName|";
        if (document.getElementById('ckCreditGradeName').checked) strBaseFields = strBaseFields + "CreditGradeName|";
        if (document.getElementById('ckHotIsName').checked) strBaseFields = strBaseFields + "HotIsName|";
        if (document.getElementById('ckUsedStatusName').checked) strBaseFields = strBaseFields + "UsedStatusName|";
        if (document.getElementById('ckManagerName').checked) strBaseFields = strBaseFields + "ManagerName|";
        if (document.getElementById('ckLinkCycle').checked) strBaseFields = strBaseFields + "LinkCycle|";
        if (document.getElementById('ckAreaName').checked) strBaseFields = strBaseFields + "AreaName|";
        if (document.getElementById('ckSendAddress').checked) strBaseFields = strBaseFields + "SendAddress|";
        if (document.getElementById('ckSellArea').checked) strBaseFields = strBaseFields + "SellArea|";
        if (document.getElementById('ckPayTypeName').checked) strBaseFields = strBaseFields + "PayTypeName|";
        if (document.getElementById('ckCurrencyTypeName').checked) strBaseFields = strBaseFields + "CurrencyTypeName|";
        if (document.getElementById('ckOpenBank').checked) strBaseFields = strBaseFields + "OpenBank|";
        if (document.getElementById('ckAccountMan').checked) strBaseFields = strBaseFields + "AccountMan|";
        if (document.getElementById('ckAccountNum').checked) strBaseFields = strBaseFields + "AccountNum|";
        if (document.getElementById('ckSetupDate').checked) strBaseFields = strBaseFields + "SetupDate|";
        if (document.getElementById('ckArtiPerson').checked) strBaseFields = strBaseFields + "ArtiPerson|";
        if (document.getElementById('ckIsTaxName').checked) strBaseFields = strBaseFields + "IsTaxName|";
        if (document.getElementById('ckTaxCD').checked) strBaseFields = strBaseFields + "TaxCD|";
        if (document.getElementById('ckBusiNumber').checked) strBaseFields = strBaseFields + "BusiNumber|";
        if (document.getElementById('ckHotHowName').checked) strBaseFields = strBaseFields + "HotHowName|"; 
    
//        for(var i=0;i<10;i++)
//        {
//            if(document.getElementById('ckExtField'+(i+1)).style.display=='block'||document.getElementById('ckExtField'+(i+1)).style.display=='')
//            {
//                if (document.getElementById('ckExtField'+(i+1)).checked) strBaseFields = strBaseFields + "ExtField"+(i+1)+"|";
//            }
//            
//        }
       
        
        SaveCommonPrintParameterSet(strBaseFields,null,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }


    
    
</script>

