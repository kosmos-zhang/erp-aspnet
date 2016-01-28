<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddExtAttribute.aspx.cs" Inherits="Pages_Office_SupplyChain_AddExtAttribute" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自定义项设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
   <%-- <link href="../../../js/JQuery/jquery.alerts-1.1/jquery.alerts.css" rel="stylesheet"  type="text/css" />--%>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <%--<script src="../../../js/JQuery/jquery.alerts-1.1/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery.alerts-1.1/jquery.alerts.js" type="text/javascript"></script>--%>
    <script src="../../../js/office/SupplyChain/AddExtAttribute.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <div id="divPageMask" style="display: none">
        <iframe id="PageMaskIframe" frameborder="0" width="100%"></iframe>
    </div>
    <div id="popupContent">
    </div>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                            单据扩展属性设置
                         
                            <uc2:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <span runat="server" id="span_btn_save" style="float: left">
                                                <%--<input  type="button" id="" title="保存"   value=" 保 存 " class="busBtn"   onclick="Save();" runat="server" /><%--visible="false"----%>
                                                <img src="../../../images/Button/Bottom_btn_save.jpg" title="保存" id="imgSave"
                                style="cursor: pointer" border="0" onclick="Save();"  runat="server" /><%--visible="false"--%>
                                                 <%--<input  type="button" id="" title="返回"   value=" 返 回 " class="busBtn"   onclick="DoBack();" runat="server"/>--%>
                                                 <img src="../../../Images/Button/Bottom_btn_back.jpg" style="cursor: pointer" title="返回"  id="btnback" onclick="DoBack();"  />
                                            </span>
                                            </td>
                                </tr>
                            </table>
                           
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
                                        功能模块<span class="redbold">*</span>
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <select id="FunctionModule" width="93%" onchange="LoadBillType()">
                                            <option value="1">销售管理</option>
                                            <option value="2">采购管理</option>
                                            <option value="3">库存管理</option>
                                            <option value="4">生产管理</option>
                                            <option value="5">质检管理</option>
                                            <option value="6">门店配送</option>
                                            <option value="7">门店管理</option>
                                            <option value="8">项目管理</option>
                                            <%--<option value="8">物品档案</option>           --%>                                 
                                        </select>
                                    </td>
                                    <td align="right" width="10%">
                                        单据类型<span class="redbold">*</span>
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%" align="left">
                                        <select id="FormType" width="93%">                                            
                                        </select>
                                    </td>
                                    <td align="right" width="10%">
                                        编号<span class="redbold">*</span>
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <input id="ModelName" maxlength="30" specialworkcheck="编号" specialworkcheck="模板编号" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                <td align="right" width="10%">
                                        自定义项1
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <input id="Custom1" maxlength="20" type="text" specialworkcheck="自定义项1" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项2
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom2" maxlength="20" type="text" specialworkcheck="自定义项2" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项3
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom3" maxlength="20" type="text" specialworkcheck="自定义项3" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                <td align="right">
                                        自定义项4
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom4" maxlength="20" specialworkcheck="自定义项4" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项5
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom5" maxlength="20" specialworkcheck="自定义项5" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项6
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom6" maxlength="20" specialworkcheck="自定义项6" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                <td align="right">
                                        自定义项7
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom7" maxlength="20" specialworkcheck="自定义项7" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项8 
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom8" maxlength="20" specialworkcheck="自定义项8 " type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项9
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom9" maxlength="20" specialworkcheck="自定义项9" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                   
                                </tr>   
                                <tr>
                                 <td align="right">
                                        自定义项10
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom10" maxlength="20" specialworkcheck="自定义项10" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right" id="td_11" style="display:none">
                                        自定义项11
                                    </td>
                                    <td bgcolor="#FFFFFF" id="td_11c" style="display:none">
                                        <input id="Custom11" maxlength="20" specialworkcheck="自定义项11" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right" id="td_12" style="display:none">
                                         自定义项12
                                    </td>
                                    <td bgcolor="#FFFFFF" id="td_12c" style="display:none">
                                         <input id="Custom12" maxlength="20" specialworkcheck="自定义项12" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    
                                </tr> 
                                <tr id="tr_13" style="display:none">
                                <td align="right">
                                        自定义项13
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom13" maxlength="20" specialworkcheck="自定义项13" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项14
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom14" maxlength="20" specialworkcheck="自定义项14" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项15
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom15" maxlength="20" specialworkcheck="自定义项15" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    
                                </tr>
                                <tr id="tr_16" style="display:none">
                                <td align="right">
                                        自定义项16
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom16" maxlength="20" specialworkcheck="自定义项16" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项17
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom17" maxlength="20" specialworkcheck="自定义项17" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项18
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom18" maxlength="20" specialworkcheck="自定义项18" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    
                                </tr>
                                <tr id="tr_19" style="display:none">
                                <td align="right">
                                        自定义项19
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom19" maxlength="20" specialworkcheck="自定义项19" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        自定义项20
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="Custom20" maxlength="20" specialworkcheck="自定义项20" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    
                                </tr>                                    
                            </table>
                            <!-- 自定义项 -->                     
                            <%--<uc1:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />--%>
                          
                        </td>
                    </tr>
                </table>
                <br />
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
            </td>
        </tr>
    </table>
    </form>
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
</body>
</html>
