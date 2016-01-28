<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubDayEndPrint.aspx.cs" Inherits="Pages_PrinttingModel_StorageManager_SubDayEndPrint"  ValidateRequest ="false"%>
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

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <title>门店日结</title>

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
            <input type="button" id="btnSet" value=" 打印模板设置 " onclick="ShowPrintSetting();" class="busBtn" runat="server" />
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
                                                        <td   align="left">
                                                            <input type="checkbox" name="ckDailyDate" id="ckDailyDate" value="DailyDate" /><input type="text"
                                                                id="txtDailyDate" value="日结日期：" size="20" readonly />
                                                        </td>
                                                        <td   align="left">
                                                            <input type="checkbox" name="ckDeptName" id="ckDeptName" value="DeptName" /><input type="text"
                                                                id="txtDeptName" value="分店：" size="20" readonly/>
                                                        </td>
                                                        <td align="left"> 
                                                                  <input type="checkbox" name="ckProductName" id="ckProductName" value="ProductName" /><input type="text"
                                                                id="txtProductName" value="品名：" size="20"  readonly/> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                     <td   align="left">
                                                      <input type="checkbox" name="ckBatchNo" id="ckBatchNo" value="BatchNo" /><input
                                                                type="text" id="txtBatchNo" value="批次：" size="20" readonly/> 
                                                        </td>
                                                        <td   align="left"> 
                                                          <input type="checkbox" name="ckProductNo" id="ckProductNo" value="ProductNo" /><input
                                                                type="text" id="txtProductNo" value="物品编号：" size="20" readonly/> 
                                                        </td> 
                                                        <td> 
                                                     
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            合计信息
                                                        </td>
                                                    </tr>
                                                           <tr>
                                                     <td   align="left"> 
                                                                        <input type="checkbox" name="ckOutTotal" id="ckOutTotal" value="OutTotal" /><input type="text"
                                                                id="txtOutTotal" value="出库总数量：" size="20"  readonly/> 
                                                        </td>
                                                        <td   align="left">
                                                        <input type="checkbox" name="ckTodayCount" id="ckTodayCount" value="TodayCount" /><input type="text"
                                                                id="txtTodayCount" value="当日结存量：" size="20"  readonly/> 
                                                        </td>
                                                        <td>
                                                           <input type="checkbox" name="ckSaleFee" id="ckSaleFee" value="SaleFee" /><input type="text"
                                                                id="txtSaleFee" value="当日销售金额：" size="20"  readonly/> 
                                                                </td>
                                                    </tr>
                                                      <tr>
                                                     <td   align="left">
                                                   <input type="checkbox" name="ckSaleBackFee" id="ckSaleBackFee" value="SaleBackFee" /><input type="text"
                                                                id="txtSaleBackFee" value="当日销售退货金额：" size="20"  readonly/> 
                                                        </td>
                                                        <td   align="left">
                                                             <input type="checkbox" name="ckSendOutCount" id="ckSendOutCount" value="SendOutCount" /><input type="text"
                                                                id="txtSendOutCount" value="配送退货出库数量：" size="20"  readonly/> 
                                                        </td>
                                                        <td>
                                                          <input type="checkbox" name="ckInitInCount" id="ckInitInCount" value="InitInCount" /><input type="text"
                                                                id="txtInitInCount" value="期初库存录入数量：" size="20"  readonly/> 
                                                                </td>
                                                    </tr> 
                                                    <tr>
                                                     <td   align="left">
                                                     <input type="checkbox" name="ckSendInCount" id="ckSendInCount" value="SendInCount" /><input  type="text" id="txtSendInCount" value="配送入库数量：" size="20" readonly/>  
                                                        </td>
                                                        <td   align="left">
                                                          <input type="checkbox" name="ckDispInCont" id="ckDispInCont" value="DispInCont" /><input type="text"
                                                                id="txtDispInCont" value="门店调拨入库数量：" size="20"  readonly/>    
                                                        </td>
                                                        <td> 
                                                             <input type="checkbox" name="ckDispOutCount" id="ckDispOutCount" value="DispOutCount" /><input
                                                                type="text" id="txtDispOutCount" value="门店调拨出库数量：" size="20" readonly/> 
                                                                </td>
                                                    </tr> 
                                                   <tr>
                                                     <td   align="left"> 
                                                       <input type="checkbox" name="ckSaleOutCount" id="ckSaleOutCount" value="SaleOutCount" /><input
                                                                type="text" id="txtSaleOutCount" value="销售出库数量(总店发货模式)：" size="20" readonly />   
                                                        </td>
                                                        <td   align="left"> 
                                                                     <input type="checkbox" name="ckSaleBackInCount" id="ckSaleBackInCount" value="SaleBackInCount" /><input type="text"
                                                                id="txtSaleBackInCount" value="销售退货入库数量(总店发货模式)：" size="20"  readonly/> 
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ckSubSaleBackInCount" id="ckSubSaleBackInCount" value="SubSaleBackInCount" /><input type="text"
                                                                id="txtSubSaleBackInCount" value="销售退货入库数量(分店发货模式)：" size="20"  readonly/> 
                                                                </td>
                                                    </tr>
                                                    <tr>
                                                     <td   align="left"> 
                                                              <input type="checkbox" name="ckSubSaleOutCount" id="ckSubSaleOutCount" value="SubSaleOutCount" /><input type="text"
                                                                id="txtSubSaleOutCount" value="销售出库数量(分店发货模式)：" size="20"  readonly/>
                                                        </td>
                                                        <td   align="left"> 
                                                         <input type="checkbox" name="ckCreatorName"  id="ckCreatorName" value="CreatorName" /><input type="text"
                                                                id="txtCreator" value="操作人：" size="20"  readonly/>
                                                        </td>
                                                        <td>  
                                                         <input type="checkbox" name="ckCreateDate" id="ckCreateDate" value="CreateDate" /><input type="text"
                                                                id="txtCreateDate" value="操作日期：" size="20"  readonly/>
                                                                </td>
                                                    </tr> 
                                              <tr>
                                              <td>   <input type="checkbox" name="ckInTotal" id="ckInTotal" value="InTotal" /><input type="text"
                                                                id="txtInTotal" value="入库总数量：" size="20"  readonly/> </td>
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

<script language="javascript" type="text/javascript">
    var intMrpID = <%=intMrpID %>;
        var intBatchNo =  ' <%=BatchNo %>';
            var intDailyDate = '<%=DailyDate %>';
                var intStorageID = <%=DeptID %>;
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
     
        /*加载打印模板设置信息*/
        LoadPrintSettingInfo();

    }
    


    /*2:加载打印模板设置信息*/
    function LoadPrintSettingInfo() {

        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        var hidIsSeted = document.getElementById('isSeted').value;


        /*主表：复选框及其对应的字段*/
        var dbBase = new Array(    ['ckDailyDate', 'DailyDate'],
                                ['ckDeptName', 'DeptName'], 
                                ['ckProductName', 'ProductName'],
                                ['ckBatchNo', 'BatchNo'],
                                ['ckProductNo', 'ProductNo'],
                                ['ckInTotal', 'InTotal'],
                                ['ckOutTotal', 'OutTotal'],
                                ['ckTodayCount', 'TodayCount'], 
                                ['ckSaleFee', 'SaleFee'], 
                                ['ckSaleBackFee', 'SaleBackFee'], 
                                ['ckSendOutCount', 'SendOutCount'],
                                ['ckInitInCount', 'InitInCount'], 
                                ['ckSendInCount', 'SendInCount'],  
                                ['ckDispInCont', 'DispInCont'],  
                                ['ckDispOutCount', 'DispOutCount'],  
                                ['ckSaleOutCount', 'SaleOutCount'],  
                                ['ckSaleBackInCount', 'SaleBackInCount'],   
                                ['ckSubSaleBackInCount', 'SubSaleBackInCount'],   
                                ['ckSubSaleOutCount', 'SubSaleOutCount'],   
                                ['ckCreatorName', 'CreatorName'],   
                                ['ckCreateDate', 'CreateDate']  );
 
    
                                 

        /*加载打印参数设置
          注：有两个明细的模块需传dbSecondDetail,只有一个明细的dbSecondDetail传null
        */
        LoadCommonPrintParameterSet(hidBillTypeFlag, hidPrintTypeFlag, hidIsSeted,dbBase, null,null);
    }

    /*3:保存打印模板设置*/
    function SavePrintSetting() {

        
        var strBaseFields = ""; 
        var toLocation='SubDayEndPrint.aspx?ProductID=' + intMrpID+"&BatchNo="+intBatchNo +"&DailyDate="+intDailyDate+"&DeptID="+intStorageID ;
        var hidBillTypeFlag = document.getElementById('hidBillTypeFlag').value;
        var hidPrintTypeFlag = document.getElementById('hidPrintTypeFlag').value;
        /*主表信息*/
        if (document.getElementById('ckDailyDate').checked) strBaseFields = strBaseFields + "DailyDate|";
        if (document.getElementById('ckDeptName').checked) strBaseFields = strBaseFields + "DeptName|";
            if (document.getElementById('ckProductName').checked) strBaseFields = strBaseFields + "ProductName|";  
        if (document.getElementById('ckBatchNo').checked) strBaseFields = strBaseFields + "BatchNo|";
              if (document.getElementById('ckProductNo').checked) strBaseFields = strBaseFields + "ProductNo|";     
            if (document.getElementById('ckInTotal').checked) strBaseFields = strBaseFields + "InTotal|";
                  if (document.getElementById('ckOutTotal').checked) strBaseFields = strBaseFields + "OutTotal|"; 
                           if (document.getElementById('ckTodayCount').checked) strBaseFields = strBaseFields + "TodayCount|"; 
                            if (document.getElementById('ckSaleFee').checked) strBaseFields = strBaseFields + "SaleFee|";       
                              if (document.getElementById('ckSaleBackFee').checked) strBaseFields = strBaseFields + "SaleBackFee|";     
                                    if (document.getElementById('ckSendOutCount').checked) strBaseFields = strBaseFields + "SendOutCount|";     
                                      if (document.getElementById('ckSendInCount').checked) strBaseFields = strBaseFields + "SendInCount|"; 
                                         if (document.getElementById('ckInitInCount').checked) strBaseFields = strBaseFields + "InitInCount|";     
                                                    if (document.getElementById('ckDispInCont').checked) strBaseFields = strBaseFields + "DispInCont|";
                                     if (document.getElementById('ckDispOutCount').checked) strBaseFields = strBaseFields + "DispOutCount|";
                                       if (document.getElementById('ckSaleOutCount').checked) strBaseFields = strBaseFields + "SaleOutCount|";   
           if (document.getElementById('ckSaleBackInCount').checked) strBaseFields = strBaseFields + "SaleBackInCount|";
            if (document.getElementById('ckSubSaleBackInCount').checked) strBaseFields = strBaseFields + "SubSaleBackInCount|";  
             if (document.getElementById('ckSubSaleOutCount').checked) strBaseFields = strBaseFields + "SubSaleOutCount|";  
             if (document.getElementById('ckCreatorName').checked) strBaseFields = strBaseFields + "CreatorName|";   
      if (document.getElementById('ckCreateDate').checked) strBaseFields = strBaseFields + "CreateDate|";   
        /*保存打印参数设置*/
        
        SaveCommonPrintParameterSet(strBaseFields,null,null,hidBillTypeFlag,hidPrintTypeFlag,toLocation);
    }


    
    
</script>
