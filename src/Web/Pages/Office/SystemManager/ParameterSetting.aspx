<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParameterSetting.aspx.cs"
    Inherits="Pages_Office_SystemManager_ParameterSetting" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参数设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/office/SystemManager/ParameterSetting.js" type="text/javascript"></script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            参数设置
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top" align="center">
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td bgcolor="#ffffff" align="center" valign="bottom">
                            <!-- Start 基本信息 -->
                            <table width="99%" border="0" align="center" cellpadding="2" id="Tb_01">
   
                                <tr>
                                    <td align="right">
                                        条码：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="dioCB1" name="dioCB" runat="server" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioCB2" name="dioCB" checked="true"
                                            runat="server" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgBarCodeContorl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(2,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：启用条码之后，可以使用条码扫描仪进行扫描物品</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        多计量单位：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioMU1" name="dioMU" runat="server" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioMU2" name="dioMU" checked="true"
                                            runat="server" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgMoreUnitControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(3,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td>
                                        <div class="sysinfo">
                                            提示：启用多计量单位，请把未做完的单据做完后再启用；启用后不可再停用</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        自动生成凭证：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="radvoucher1" name="radvoucher" runat="server" value="1" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" checked id="radvoucher2" name="radvoucher" runat="server" value="2" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgVoucherControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(6,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：必须先设置辅助核算，才能设置自动生成凭证</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        自动审核登帐：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="radapply1" name="radapply" runat="server" value="3" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="radapply2" name="radapply" checked="true" runat="server"
                                            value="4" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgApplyControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(7,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：只有启用了自动生成凭证的，才可以启用自动审核登账</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        超订单发/到货：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="radOver1" name="radover" runat="server" value="1" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="radOver2" name="radover" checked="true" runat="server" value="2" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgOverControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(8,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：启用超订单发/到货时,在销售发货(采购到货)时允许大于订单数量发货(到货)
                                        </div>
                                    </td>
                                </tr>
                             <tr>
                                    <td align="right" width="15%">
                                        出入库是否显示价格：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioBN1" name="dioBN" runat="server" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioBN2" name="dioBN" checked="true"
                                            runat="server" />停用
                                    </td>
                                    <td align="right" width="2%">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgStorageControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(1,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF" width="19%">
                                        <div class="sysinfo">
                                            提示：限制仓管员对出库或入库的详细价格的了解</div>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        允许出入库价格为零：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioZero1" name="dioZero" runat="server" value="1" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioZero2" name="dioZero" checked="true" runat="server" value="2" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgInOutPriceControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(9,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认不允许出入库价格设为零
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        小数精度设置：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <select id="SelPoint" runat="server">
                                            <option value="1">1</option>
                                            <option value="2" selected>2</option>
                                            <option value="3">3</option>
                                            <option value="4">4</option>
                                            <option value="5">5</option>
                                            <option value="6">6</option>
                                        </select>&nbsp;位
                                    </td>
                                    <td align="left">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" style="cursor: pointer" alt="保存"
                                            id="btn_point" border="0" onclick="ParameterSetting(5,true);" runat="server"
                                            visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：系统缺省默认为2位小数，最大支持6位小数</div>
                                    </td>
                                </tr>
                            </table>
                            <!-- End 基本信息 -->
                            <br />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
