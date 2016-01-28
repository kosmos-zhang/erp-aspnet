<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderLinkManPrint.aspx.cs" Inherits="Pages_PrinttingModel_PurchaseManager_ProviderLinkManPrint" ValidateRequest="false"%>

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

    <title>供应商联系人档案</title>

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
                                                            <input type="checkbox" name="ckLinkManName" id="ckLinkManName" value="LinkManName" /><input type="text" readonly="readonly"
                                                                id="txtLinkManName" value="联系人姓名：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckCustName" id="ckCustName" value="CustName" /><input type="text" readonly="readonly"
                                                                id="txtCustName" value="供应商姓名：" size="20" />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ckSexName" id="ckSexName" value="SexName" /><input
                                                                type="text" readonly="readonly" id="txtSexName" value="性别：" size="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckImportantName" id="ckImportantName" value="ImportantName" /><input type="text" readonly="readonly"
                                                                id="txtImportantName" value="重要程度：" size="20" />
                                                        </td>
                                                        
                                                          <td  align="left">
                                                          <input type="checkbox" name="ckAppellation" id="ckAppellation" value="Appellation" /><input
                                                                type="text" readonly="readonly" id="txtAppellation" value="称谓：" size="20" />
                                                        </td>
                                                        
                                                        <td>
                                                            <input type="checkbox" name="ckCompany" id="ckCompany" value="Company"  /><input
                                                                type="text" readonly="readonly" id="txtCompany" value="单位" size="20"  />
                                                        </td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckDepartment" id="ckDepartment" value="Department" /><input
                                                                type="text" readonly="readonly" id="txtDepartment" value="部门：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckPosition" id="ckPosition" value="Position" /><input
                                                                type="text" readonly="readonly" id="txtPosition" value="职务：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckOperation" id="ckOperation" value="Operation" /><input
                                                                type="text" readonly="readonly" id="txtOperation" value="负责业务：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                  
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            联系信息
                                                        </td>
                                                    </tr>
                                                   
                                                    
                                                       <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckWorkTel" id="ckWorkTel" value="WorkTel" /><input
                                                                type="text" readonly="readonly" id="txtWorkTel" value="工作电话：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckFax" id="ckFax" value="Fax" /><input
                                                                type="text" readonly="readonly" id="txtFax" value="传真：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckHandset" id="ckHandset" value="Handset" /><input
                                                                type="text" readonly="readonly" id="txtHandset" value="移动电话：" size="20" />
                                                        </td>
                                                    </tr>
                                                       <tr>
                                                       <td  align="left">
                                                            <input type="checkbox" name="ckMailAddress" id="ckMailAddress" value="MailAddress" /><input
                                                                type="text" readonly="readonly" id="txtMailAddress" value="邮件地址：" size="20" />
                                                        </td>
                                                        
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckHomeTel" id="ckHomeTel" value="HomeTel" /><input
                                                                type="text" readonly="readonly" id="txtHomeTel" value="家庭电话：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckMSN" id="ckMSN" value="MSN" /><input
                                                                type="text" readonly="readonly" id="txtMSN" value="MSN：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                     <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckQQ" id="ckQQ" value="QQ" /><input
                                                                type="text" readonly="readonly" id="txtQQ" value="QQ：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckPost" id="ckPost" value="Post" /><input
                                                                type="text" readonly="readonly" id="txtPost" value="邮编：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckLinkTypeName" id="ckLinkTypeName" value="LinkType" /><input
                                                                type="text" readonly="readonly" id="txtLinkType" value="联系人类型：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                      <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckAge" id="ckAge" value="Age" /><input
                                                                type="text" readonly="readonly" id="txtAge" value="年龄：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckPaperType" id="ckPaperType" value="PaperType" /><input
                                                                type="text" readonly="readonly" id="txtPaperType" value="证件类型：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckPaperNum" id="ckPaperNum" value="PaperNum" /><input
                                                                type="text" readonly="readonly" id="txtPaperNum" value="证件号：" size="20" />
                                                        </td>
                                                    </tr>
                                                    
                                                       <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckModifiedDate" id="ckModifiedDate" value="ModifiedDate" /><input
                                                                type="text" readonly="readonly" id="txtModifiedDate" value="最后更新日期：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckModifiedUserID" id="ckModifiedUserID" value="ModifiedUserID" /><input
                                                                type="text" readonly="readonly" id="txtModifiedUserID" value="最后更新人：" size="20" />
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            关怀点
                                                        </td>
                                                    </tr>
                                                    
                                                       <tr>
                                                        <td  align="left">
                                                            <input type="checkbox" name="ckBirthday" id="ckBirthday" value="Birthday" /><input
                                                                type="text" readonly="readonly" id="txtBirthday" value="生日：" size="20" />
                                                        </td>
                                                        <td  align="left">
                                                         <input type="checkbox" name="ckLikes" id="ckLikes" value="Likes" /><input
                                                                type="text" readonly="readonly" id="txtLikes" value="爱好：" size="20" />
                                                        </td>
                                                        <td>
                                                         <input type="checkbox" name="ckRemark" id="ckRemark" value="Remark" /><input
                                                                type="text" readonly="readonly" id="txtRemark" value="备注：" size="20" />
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
        var dbBase = new Array( [ 'ckLinkManName', 'LinkManName'], 
                                [ 'ckCustName', 'CustName'], 
                                [ 'ckSexName', 'SexName'],
                                [ 'ckImportantName', 'ImportantName'],
                                [ 'ckAppellation', 'Appellation'],
                                [ 'ckCompany', 'Company'],
                                [ 'ckDepartment', 'Department'],
                                [ 'ckPosition', 'Position'],
                                [ 'ckOperation', 'Operation'],
                                [ 'ckWorkTel', 'WorkTel'],
                                [ 'ckFax', 'Fax'],
                                [ 'ckHandset', 'Handset'],
                                [ 'ckMailAddress', 'MailAddress'],
                                [ 'ckHomeTel', 'HomeTel'],
                                [ 'ckMSN', 'MSN'],
                                [ 'ckQQ', 'QQ'],
                                [ 'ckPost', 'Post'],
                                [ 'ckLinkTypeName', 'LinkTypeName'],
                                [ 'ckAge', 'Age'],
                                [ 'ckPaperType', 'PaperType'],
                                [ 'ckPaperNum', 'PaperNum'],
                                [ 'ckModifiedDate', 'ModifiedDate'],
                                [ 'ckModifiedUserID', 'ModifiedUserID'],
                                [ 'ckBirthday', 'Birthday'],
                                [ 'ckLikes', 'Likes'],
                                [ 'ckRemark', 'Remark'] );
      
                                 

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
        var toLocation='ProviderLinkManPrint.aspx?ID=' + intMrpID;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/         
        
        
        
        if (document.getElementById('ckLinkManName').checked) strBaseFields = strBaseFields + "LinkManName|"; 
        if (document.getElementById('ckCustName').checked) strBaseFields = strBaseFields + "CustName|"; 
        if (document.getElementById('ckSexName').checked) strBaseFields = strBaseFields + "SexName|";
        if (document.getElementById('ckImportantName').checked) strBaseFields = strBaseFields + "ImportantName|";
        if (document.getElementById('ckAppellation').checked) strBaseFields = strBaseFields + "Appellation|";
        if (document.getElementById('ckCompany').checked) strBaseFields = strBaseFields + "Company|";
        if (document.getElementById('ckDepartment').checked) strBaseFields = strBaseFields + "Department|";
        if (document.getElementById('ckPosition').checked) strBaseFields = strBaseFields + "Position|";
        if (document.getElementById('ckOperation').checked) strBaseFields = strBaseFields + "Operation|";
        if (document.getElementById('ckWorkTel').checked) strBaseFields = strBaseFields + "WorkTel|";
        if (document.getElementById('ckFax').checked) strBaseFields = strBaseFields + "Fax|";
        if (document.getElementById('ckHandset').checked) strBaseFields = strBaseFields + "Handset|";
        if (document.getElementById('ckMailAddress').checked) strBaseFields = strBaseFields + "MailAddress|";
        if (document.getElementById('ckHomeTel').checked) strBaseFields = strBaseFields + "HomeTel|";
        if (document.getElementById('ckMSN').checked) strBaseFields = strBaseFields + "MSN|";
        if (document.getElementById('ckQQ').checked) strBaseFields = strBaseFields + "QQ|";
        if (document.getElementById('ckPost').checked) strBaseFields = strBaseFields + "Post|";
        if (document.getElementById('ckLinkTypeName').checked) strBaseFields = strBaseFields + "LinkTypeName|";
        if (document.getElementById('ckAge').checked) strBaseFields = strBaseFields + "Age|";
        if (document.getElementById('ckPaperType').checked) strBaseFields = strBaseFields + "PaperType|";
        if (document.getElementById('ckPaperNum').checked) strBaseFields = strBaseFields + "PaperNum|";
        if (document.getElementById('ckModifiedDate').checked) strBaseFields = strBaseFields + "ModifiedDate|";
        if (document.getElementById('ckModifiedUserID').checked) strBaseFields = strBaseFields + "ModifiedUserID|";
        if (document.getElementById('ckBirthday').checked) strBaseFields = strBaseFields + "Birthday|";
        if (document.getElementById('ckLikes').checked) strBaseFields = strBaseFields + "Likes|";
        if (document.getElementById('ckRemark').checked) strBaseFields = strBaseFields + "Remark|"; 
        SaveCommonPrintParameterSet(strBaseFields,null,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }


    
    
</script>

