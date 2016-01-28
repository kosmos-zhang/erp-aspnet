<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellPlanAdd.aspx.cs" Inherits="Pages_Office_SellManager_SellPlanAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc9" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"> </script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/TreeView.js" type="text/javascript"></script>

    <script src="../../../js/office/SellManager/SellPlanAdd.js" type="text/javascript"></script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <div id="div_Add" style="border: solid 10px #898989; background: #fff; padding: 10px;
        width: 600px; z-index: 21; position: absolute; top: 70%; left: 60%; margin: 10px 0 0 -400px;
        display: none">
        
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right" style="padding: 5px; width: 20%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px; border-left: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    总结内容
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;"
                    colspan="3">
                    <textarea rows="4" id="SummarizeNote" specialworkcheck="总结内容" cols="4" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding: 5px; width: 20%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px; border-left: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    计划实绩
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;"
                    colspan="3">
                    <textarea rows="4" id="AimRealResult" specialworkcheck="目标实绩" cols="4" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding: 5px; width: 20%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px; border-left: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    差额
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;"
                    colspan="3">
                    <textarea rows="2" id="Difference" specialworkcheck="差额" cols="2" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding: 5px; width: 20%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px; border-left: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    完成情况<span class="redbold">*</span>
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;
                    width: 30%;">
                    <select id='AddOrCut' style="width: 120px;">
                        <option selected="selected" value='0'>低于计划值</option>
                        <option value='1'>等于计划值</option>
                        <option value='2'>超过计划值</option>
                    </select>
                </td>
                <td align="right" style="padding: 5px; width: 20%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    计划达成率(%)<span class="redbold">*</span>
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;
                    width: 30%;">
                    <input type="text" id="CompletePercent" style="width: 120px;" maxlength="6" />
                </td>
            </tr>
            <tr>
                <td align="right" style="padding: 5px; width: 20%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px; border-left: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    总结人
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;
                    width: 30%;">
                    <asp:HiddenField ID="hiddSummarizer" runat="server" />
                    <asp:TextBox ID="Summarizer" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                </td>
                <td align="right" style="padding: 5px; width: 20%; border-top: solid #9C9A9C 1px;
                    border-right: solid #9C9A9C 1px;" bgcolor="#E7E7E7">
                    总结时间
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; padding: 5px;
                    width: 30%;">
                    <asp:HiddenField ID="hiddSummarizeDate" runat="server" />
                    <asp:TextBox ID="SummarizeDate" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="border: solid #9C9A9C 1px" colspan="2">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="fnSumm();"
                        id="btnSumm" runat="server" style="padding-right: 100px; margin-top: 4px;" />
                </td>
                <td style="border-top: solid #9C9A9C 1px; border-right: solid #9C9A9C 1px; border-bottom: solid #9C9A9C 1px;"
                    colspan="2">
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();"
                        style="padding-left: 120px; margin-top: 5px;" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc7:Message ID="Message1" runat="server" />
    <div>
        <table style="width: 95%;" border="0" cellpadding="0" cellspacing="0" class="maintable"
            id="mainindex">
            <tr>
                <td valign="top">
                    <input type="hidden" id="hiddenEquipCode" value="" />
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td align="center" valign="top">
                </td>
            </tr>
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="center" class="Title">
                                <label id="lblTitle">
                                    新建销售计划</label>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                <table width="100%">
                                    <tr>
                                        <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                            <img runat="server" visible="false" src="../../../images/Button/Bottom_btn_save.jpg"
                                                alt="保存" id="btn_save" style="cursor: hand" onclick="InsertSellOfferData();" />
                                            <img runat="server" visible="false" alt="保存" src="../../../Images/Button/UnClick_bc.jpg"
                                                id="imgUnSave" style="display: none;" />
                                            <span runat="server" visible="false" id="GlbFlowButtonSpan"></span>
                                            <img src="../../../Images/Button/Bottom_btn_back.jpg" style="display: none; cursor: hand"
                                                alt="返回" id="ibtnBack" onclick="fnBack();" />
                                            
                                        </td>
                                        <td align="right" bgcolor="#FFFFFF" style="padding-top: 5px; width: 70px;">
                                            <img id="btnPrint" alt="打印" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                                title="打印" onclick="fnPrintOrder()" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="6">
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            基本信息
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
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="Tb_01">
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" style="width: 10%">
                                计划编号<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 22%">
                                <uc3:CodingRuleControl ID="SellPlanUC" runat="server" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 11%">
                                计划名称<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" style="width: 22%">
                                <input type="text" id="Title" specialworkcheck="目标名称" class="tdinput" maxlength="50"
                                    style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 11%">
                                计划类型<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF">
                                <select id="PlanType" onchange="fnType()" style="width: 120px;">
                                    <option value="4">日</option>
                                    <option value="5">周</option>
                                    <option value="1">月</option>
                                    <option value="2">季</option>
                                    <option value="6">半年</option>
                                    <option value="3">年</option>
                                    <option value="7">其他</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                计划时期<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:TextBox ID="PlanDateTime" runat="server" Width="95%" class="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SendDate')})"></asp:TextBox>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="80px" Style="display: none;">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlMonth" runat="server" Width="80px" Style="display: none;">
                                    <asp:ListItem Value="1">1月</asp:ListItem>
                                    <asp:ListItem Value="2">2月</asp:ListItem>
                                    <asp:ListItem Value="3">3月</asp:ListItem>
                                    <asp:ListItem Value="4">4月</asp:ListItem>
                                    <asp:ListItem Value="5">5月</asp:ListItem>
                                    <asp:ListItem Value="6">6月</asp:ListItem>
                                    <asp:ListItem Value="7">7月</asp:ListItem>
                                    <asp:ListItem Value="8">8月</asp:ListItem>
                                    <asp:ListItem Value="9">9月</asp:ListItem>
                                    <asp:ListItem Value="10">10月</asp:ListItem>
                                    <asp:ListItem Value="11">11月</asp:ListItem>
                                    <asp:ListItem Value="12">12月</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlWeek" runat="server" Width="80px" Style="display: none;">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSeason" runat="server" Width="80px" Style="display: none;">
                                    <asp:ListItem Value="1">第一季度</asp:ListItem>
                                    <asp:ListItem Value="2">第二季度</asp:ListItem>
                                    <asp:ListItem Value="3">第三季度</asp:ListItem>
                                    <asp:ListItem Value="4">第四季度</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="Half" runat="server" Width="80px" Style="display: none;">
                                    <asp:ListItem Value="1">上半年</asp:ListItem>
                                    <asp:ListItem Value="2">下半年</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                开始时间
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input name="StartDate" style="width: 95%;" readonly="readonly" id="StartDate" class="tdinput"
                                    type="text" style="width: 95%;" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('StartDate')})" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结束时间
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="EndDate" id="EndDate" readonly="readonly" class="tdinput" type="text"
                                    style="width: 95%;" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndDate')})" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                                最低计划额(元)<span class="redbold">*</span>
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <input type="text" class="tdinput" id="MinPlanTotal" maxlength="20" style="width: 95%" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                计划总金额(元)<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input type="text" class="tdinput" id="PlanTotal" maxlength="20" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" style="width: 11%">
                            </td>
                            <td bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                                激励方案
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" colspan="5" style="width: 89%">
                                <textarea rows="4" id="Hortation" specialworkcheck="激励方案" cols="4" style="width: 95%"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                                可查看人员
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" colspan="5" style="width: 89%">
                                <textarea rows="4" id="UserCanViewUser" cols="4" readonly="readonly" onclick="alertdiv('UserCanViewUser,CanViewUserNameHidden,2');"
                                    style="width: 95%"></textarea>
                                <input type="hidden" id="CanViewUserNameHidden" />
                            </td>
                        </tr>
                    </table>
                    <uc1:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            备注信息
                                        </td>
                                        <td align="right">
                                            <div id='searchClick2'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick2')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_03">
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%">
                                单据状态
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%">
                                <label id="BillStatus">
                                    制单</label>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%">
                                制单人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 22%">
                                <asp:TextBox ID="Creator" Width="120px" runat="server" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" style="width: 11%">
                                制单日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%">
                                <asp:TextBox ID="CreateDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                确认人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="Confirmor" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                确认日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ConfirmDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结单人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="Closer" runat="server" Width="120px" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结单日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="CloseDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新人
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ModifiedUserID" runat="server" Width="120px" CssClass="tdinput"
                                    Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新日期
                            </td>
                            <td height="20" align="left" bgcolor="#FFFFFF">
                                <asp:TextBox ID="ModifiedDate" runat="server" Width="120px" CssClass="tdinput" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                备注
                            </td>
                            <td height="20" colspan="5" align="left" bgcolor="#FFFFFF">
                                <textarea rows="4" id="Remark" specialworkcheck="备注" cols="4" style="width: 95%"></textarea>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            计划明细
                                        </td>
                                        <td align="right">
                                            <div id='Div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Table1','Div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Table1">
                        <tr>
                            <td height="30" align="right" bgcolor="#E6E6E6" style="width: 11%">
                                操作类型
                            </td>
                            <td height="30" align="left" bgcolor="#FFFFFF" colspan="5">
                                <img runat="server" visible="false" src="../../../Images/Button/btn_tjtjmx.jpg" id="btnType2"
                                    alt="添加同级明细" onclick="fnChangeType(2)" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_tjtjmxu.jpg"
                                    style="display: none" id="ubtnType2" alt="添加同级明细" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_tjxjmx.jpg" id="btnType3"
                                    alt="添加下级明细" onclick="fnChangeType(3)" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_tjxjmxu.jpg"
                                    style="display: none" id="ubtnType3" alt="添加下级明细" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_xgbjmx.jpg" id="btnType4"
                                    alt="修改本级明细" onclick="fnChangeType(4)" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_xgbjmxu.jpg"
                                    style="display: none" id="ubtnType4" alt="修改本级明细" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_scmx.jpg" id="btnType5"
                                    alt="删除明细" onclick="fnChangeType(5)" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_scmxu.jpg" style="display: none"
                                    id="ubtnType5" alt="删除明细" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_zjmx.jpg" style="display: none"
                                    id="btnType6" alt="总结明细" onclick="fnChangeType(6)" />
                                <img runat="server" visible="false" src="../../../Images/Button/btn_zjmxu.jpg" id="ubtnType6"
                                    alt="总结明细" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="cell">
                                <div id="detailPanel" style="width: 100%; padding-bottom: 5px; padding-top: 5px;">
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="width: 100%; height: 20px;">
                    </div>
                </td>
            </tr>
        </table>
        <input type="radio" name="type" style="display: none;" id="type1" value="1" checked="checked"
            onclick="fnChangeType(1)" />
        <input type="radio" name="type" style="display: none;" id="type2" value="2" onclick="fnChangeType(2)" />
        <input type="radio" name="type" style="display: none;" id="type3" value="2" onclick="fnChangeType(3)" />
        <input type="radio" name="type" style="display: none;" id="type4" value="3" onclick="fnChangeType(4)" />
        <input type="radio" name="type" style="display: none;" id="type5" value="4" onclick="fnChangeType(5)" />
        <input disabled="disabled" type="radio" style="display: none;" name="type" id="type6"
            value="5" onclick="fnChangeType(6)" />
    </div>
    <div id="divDetalShow" style="border: solid 10px #898989; background: #fff; padding: 10px;
        width: 600px; z-index: 21; position: absolute; top: 60%; left: 55%; margin: 10px 0 0 -400px;
        display: none">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 20%;border:solid #9C9A9C 1px">
                    请选择<span class="redbold">*</span>
                </td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width: 30%; border:solid #9C9A9C 1px; border-left:none;">
                    <select id="ddlDetailType" onchange="fnChangeDetailType()" style="width: 120px;">
                        <option value="1">部门</option>
                        <option value="2">员工</option>
                        <option value="3">物品</option>
                    </select>
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 20%; border:solid #9C9A9C 1px; border-left:none;">
                    <label id="lblDetailType">
                        部门</label><span class="redbold">*</span>
                </td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width: 30%; border:solid #9C9A9C 1px; border-left:none;">
                    <input id="DeptId" class="tdinput" onclick="alertdiv('DeptId,hiddDeptID');" readonly="readonly"
                        style="width: 90%;" type="text" />
                    <input id="UserSeller" class="tdinput" onclick="alertdiv('UserSeller,hiddSeller');"
                        readonly="readonly" style="width: 90%; display: none;" type="text" />
                    <input id='ProductID' readonly='readonly' style='width: 90%; display: none;' type='text'
                        onclick="popTechObj.ShowList(ProductID)" class='tdinput' />
                </td>
            </tr>
            <tr>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 20%; border:solid #9C9A9C 1px; border-top:none;">
                    最低计划额(元)<span class="redbold">*</span>
                </td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width: 30%; border:solid #9C9A9C 1px; border-top:none;border-left:none;">
                    <input type="text" class="tdinput" id="MinDetailotal" maxlength="20" style="width: 95%" />
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 20%; border:solid #9C9A9C 1px; border-top:none;border-left:none;">
                    计划额(元)<span class="redbold">*</span>
                </td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width: 30%; border:solid #9C9A9C 1px; border-top:none;border-left:none;">
                    <input type="text" class="tdinput" id="DetailTotal" maxlength="20" style="width: 95%" />
                </td>
            </tr>
            <tr>
                <td align="right" style="border: solid #9C9A9C 1px; border-top:none;" colspan="2">
                    <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save0" style="cursor: hand;
                        margin-top: 5px; padding-right: 10px;" onclick="InsertSellOfferData();" />
                </td>
                <td style="border-right: solid #9C9A9C 1px; border-bottom: solid #9C9A9C 1px;"
                    colspan="2">
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="fnChangeType(1)"
                        style="padding-left: 10px; margin-top: 5px;" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <uc5:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <asp:HiddenField ID="hiddSeller" runat="server" />
    <uc9:FlowApply ID="FlowApply1" runat="server" />
    <input type="hidden" id="HiddenURLParams" runat="server" />
    <input type="hidden" id='hiddBillStatus' value='1' />
    <input type="hidden" id="hiddDeptID" />
    <input type="hidden" id="hiddProID" />
    <input type="hidden" id="hiddOrderID" value='0' />
    <input type="hidden" id="hiddSelectValue" />
    <input type="hidden" id="hiddDetailCount" value="0" />
    </form>

    <script type="text/javascript">
    var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.CODING_RULE_SELL %>;
    var glb_BillTypeCode = <%=XBase.Common.ConstUtil.CODING_RULE_SELLPLAN_NO %>;
    var glb_BillID = 0;                                //单据ID
    var glb_IsComplete = true;                                          //是否需要结单和取消结单(小写字母)
    var FlowJS_HiddenIdentityID ='hiddOrderID';                      //自增长后的隐藏域ID
    var FlowJs_BillNo ='SellPlanUC_txtCode';          //当前单据编码名称
    var FlowJS_BillStatus ='hiddBillStatus';                             //单据状态ID
    
    var precisionLength=<%=SelPoint %>;//小数精度
    </script>

</body>
</html>
