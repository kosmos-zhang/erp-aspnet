<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalFlowSetAdd.aspx.cs"
    Inherits="Pages_Office_SystemManager_Default2" %>

<%@ Register Src="../../../UserControl/SelectBillType.ascx" TagName="SelectBillType"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/Department.ascx" TagName="Department" TagPrefix="uc2 " %>
<%@ Register Src="../../../UserControl/BillTypeControl.ascx" TagName="BillTypeControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <title>审批流程添加</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/office/SystemManager/ApprovalFlowSet.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function abc() {
            alert(document.getElementById('DeptID').value);
        }
    </script>

    <style type="text/css">
        #Button2
        {
            width: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc4:Message ID="Message1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <div>
        <input id="hd_typecode" type="hidden" />
        <table width="99%" border="0" cellspacing="0" cellpadding="0" style="margin-left: 6px;">
            <tr>
                <td height="30" align="center" class="Title" bgcolor="#FFFFFF">
                    审批流程设置
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
            style="margin-left: 2px;">
            <tr>
                <td height="28" bgcolor="#FFFFFF">
                    <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="Save_UserInfo"
                        style="cursor: hand; float: left" border="0" onclick="Fun_Save_Flow();" visible="false"
                        runat="server" />
                    <img alt="发布" style="cursor: hand; float: left" id="btn_publish" visible="false"
                        runat="server" onclick="Publish()" src="../../../Images/Button/Bottom_btn_add.jpg" /><img
                            alt="停止" style="cursor: hand; float: left" id="btn_stop" onclick="stop()" src="../../../Images/Button/Bottom_btn_stop.jpg"
                            visible="false" runat="server" /><asp:ImageButton ID="btn_back" Style="cursor: hand;
                                float: left" runat="server" ImageUrl="~/Images/Button/Bottom_btn_back.jpg" OnClick="btn_back_Click" />
                    <asp:HiddenField ID="hidfcode" runat="server" />
                    <asp:HiddenField ID="hd_usestatus" runat="server" />
                    <asp:HiddenField ID="hd_typeflag" runat="server" />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="2" cellspacing="1" bgcolor="#999999" style="margin-left: 6px">
            <tr>
                <td height="20" bgcolor="#F4F0ED" class="Blue">
                    <table width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr>
                            <td>
                                基础信息
                            </td>
                            <td align="right">
                                <div id='searchClick'>
                                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01"
            style="display: block; margin-left: 6px">
            <tr>
                <td align="right" bgcolor="#E6E6E6" width="13%">
                    流程编号<span class="redbold">*</span>
                </td>
                <td bgcolor="#FFFFFF" width="25%">
                    <uc5:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                </td>
                <td align="right" bgcolor="#E6E6E6" width="13%">
                    流程名称<span class="redbold">*</span>
                </td>
                <td bgcolor="#FFFFFF" width="22%">
                    <input id="txt_FlowName" class="tdinput" name="txt_flowname" size="15" specialworkcheck="流程名称"
                        type="text" />
                </td>
                <td align="right" bgcolor="#E6E6E6" width="13%">
                    部门名称
                </td>
                <td bgcolor="#FFFFFF" width="22%">
                    <input name="DeptName" id="DeptName" onclick="alertdiv('DeptName,DeptID');" class="tdinput"
                        readonly type="text" style="width: 99%" />
                    <input type="hidden" id="DeptID" runat="server" />
                    <%--  <input id="DeptName" class="tdinput" name="DeptName" size="15" 
                                type="text" onclick="popDeptObj.Show('DeptName','DeptID');" /><uc2:department 
                                ID="Department1" runat="server" /><input type="hidden" id="DeptID" name="DeptID" runat="server" />--%>
                </td>
            </tr>
            <tr id="CloseDate">
                <td height="20" align="right" bgcolor="#E6E6E6">
                    单据分类标志<span class="redbold">*</span>
                </td>
                <td height="20" bgcolor="#FFFFFF">
                    <uc1:SelectBillType ID="SelectBillType1" runat="server" />
                    <input id="txt_flag" type="text" class="tdinput" runat="server" disabled="disabled" />
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    单据分类<span class="redbold">*</span>
                </td>
                <td height="20" bgcolor="#FFFFFF" class="tdinput">
                    <uc3:BillTypeControl ID="BillTypeControl1" runat="server" />
                    <input id="txt_BillTypeName" class="tdinput" name="txt_BillTypeName" size="15" type="text"
                        runat="server" disabled="disabled" /><input id="txt_typeflag" type="hidden" />
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    启用状态
                </td>
                <td height="20" bgcolor="#FFFFFF" class="tdinput">
                    <font color="red">
                        <select name="sel_status" class="tdinput" id="sel_status" runat="server" disabled="disabled"
                            width="119px">
                            <option value="0">草稿</option>
                            <option value="1">停止</option>
                            <option value="2">发布</option>
                        </select></font>
                </td>
            </tr>
            <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">是否手机短信提醒
                </td>
                <td height="20" bgcolor="#FFFFFF">
                <select name="sel_SendMS" class="tdinput" id="sel_SendMS" runat="server" width="119px">
                                          <option value="0">否</option>
                                          <option value="1" selected>是</option>

                                       </select>
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6">
                </td>
                <td height="20" bgcolor="#FFFFFF" class="tdinput">
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    
                </td>
                <td height="20" bgcolor="#FFFFFF" class="tdinput">
                </td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="1" cellspacing="0" bgcolor="#999999" style="margin-left: 6px">
            <tr>
                <td height="20" bgcolor="#F4F0ED" class="Blue">
                    <table width="100%" border="0" cellspacing="0" cellpadding="1">
                        <tr>
                            <td>
                                流程步骤信息
                            </td>
                            <td align="right">
                                <div id='searchClick2'>
                                    <img src="../../../images/Main/Open.jpg" style="cursor: pointer" onclick="oprItem('alltable','searchClick2')" /></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" cellpadding="0" cellspacing="0" id="alltable" style="margin-left: 5px">
            <tr>
                <td>
                    <table style="width: 100%" align="center" border="0">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">
                                <img alt="" src="../../../Images/Button/Show_add.jpg" onclick="AddSignRowflag();"
                                    id="StepAdd" visible="false" runat="server" /><img alt="" src="../../../Images/Button/Show_del.jpg"
                                        onclick="DeleteDetailRow();" id="StepDel" visible="false" runat="server" /><img alt=""
                                            src="../../../Images/Button/Up.jpg" onclick="SwapRow();" id="StepUp" visible="false"
                                            runat="server" /><img alt="" src="../../../Images/Button/Down.jpg" onclick="SwapRowDown();"
                                                id="StepDown" visible="false" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" id="dg_Log" style="behavior: url(../../../draggrid.htc)"
                        cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                选择<input type="checkbox" visible="false" name="checkall" id="checkall" onclick="selectall()"
                                    value="checkbox" />
                            </td>
                            <td height="20" align="center" bgcolor="#E6E6E6" class="ListTitle">
                                流程步骤
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                步骤描述 <span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                步骤处理人 <span class="redbold">*</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
    </div>
    <p>
        <input id="hf_flowid" type="hidden" runat="server" /><input id="hf_typeflag" type="hidden"
            runat="server" /></p>
    <p>
        <input id="hf_typecode" type="hidden" runat="server" />
    </p>
    <asp:HiddenField ID="txtData" runat="server" />
    <p>
        <input id="flowid" type="hidden" value="0" />
    </p>
    <p>
        <input id="hf_flag" type="hidden" value="1" />
    </p>
    </form>
</body>
</html>
