<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputFloatSalary.aspx.cs"
    Inherits="Pages_Office_HumanManager_InputFloatSalary" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/Human/SalaryPieceItem.ascx" TagName="SalaryPiece"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Human/SalaryTimeItem.ascx" TagName="SalaryTime"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/Human/SalaryRoyaltyItem.ascx" TagName="SalaryRoyalty"
    TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>浮动工资录入</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/office/HumanManager/InputFloatSalary.js" type="text/javascript"></script>

    <style type="text/css">
        #mainindex2
        {
            margin-top: 10px;
            margin-left: 10px;
            background-color: #F0f0f0;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
        }
    </style>
    <style type="text/css">
        #tblMain
        {
            margin-top: 0px;
            margin-left: 0px;
            background-color: #F0f0f0;
            font-family: tahoma;
            color: #333333;
            font-size: 12px;
        }
        .errorMsg
        {
            filter: progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
            position: absolute;
            top: 240px;
            left: 450px;
            border-width: 1pt;
            border-color: #666666;
            border-style: solid;
            width: 290px;
            display: none;
            margin-top: 10px;
            z-index: 21;
        }
        .maintb
        {
            margin-top: 10px;
            margin-left: 0px;
            background-color: #F0f0f0;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <uc3:SalaryPiece ID="SalaryPiece1" runat="server" />
    <uc4:SalaryTime ID="SalaryTime1" runat="server" />
    <uc5:SalaryRoyalty ID="SalaryRoyalty1" runat="server" />
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="maintb"
        id="mainindex1">
        <tr>
            <td valign="top" colspan="2">
            </td>
        </tr>
        <tr>
            <td style="width: 80%" valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" />
            </td>
        </tr>
        <tr height="20" align="right">
            <td colspan='3' width='100%'>
              &nbsp; <a href="InputCompanyRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">公司提成</a>&nbsp;
                            &nbsp;<a href="InputDepatmentRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">部门提成</a>&nbsp;
                               &nbsp;<a href="InputPersonalRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">个人业务提成</a>&nbsp;
                &nbsp;<a href="#" style="text-decoration: none; color :Blue" onclick="ChangePanel('1'); ">计件工资</a>&nbsp;
                &nbsp;<a href="#" style="text-decoration: none; color :Blue" onclick="ChangePanel('2')">计时工资</a>&nbsp;
                &nbsp;<a href="#" style="text-decoration: none; color :Blue" onclick="ChangePanel('3')">产品单品提成</a>&nbsp;
                &nbsp;<a href="InputPerformanceRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">绩效工资</a>&nbsp;
            </td>
        </tr>
            <tr>
            <td valign="top" class="Blue">
                
            </td>
            <td align="right" valign="top">
                 
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <input type="hidden" id="txtOprateSalary" value="1" />&nbsp;
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblSearch','divSearch')" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td width="10%" height="20" class="tdColTitle">
                                        员工编号
                                    </td>
                                    <td width="23%" class="tdColInput">
                                        <asp:TextBox ID="txtEmployeeNo" Width="95%" CssClass="tdinput" runat="server" SpecialWorkCheck="员工编号"></asp:TextBox>
                                    </td>
                                    <td width="10%" class="tdColTitle">
                                        员工姓名
                                    </td>
                                    <td width="23%" class="tdColInput">
                                        <asp:TextBox ID="txtEmployeeName" Width="95%" CssClass="tdinput" runat="server" SpecialWorkCheck="员工姓名"></asp:TextBox>
                                    </td>
                                    <td width="10%" class="tdColTitle">
                                        所在岗位
                                    </td>
                                    <td width="24%" class="tdColInput">
                                        <asp:DropDownList ID="ddlQuarter" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">
                                        <div id="divSalaryTitle">
                                            计件项目</div>
                                    </td>
                                    <td class="tdColInput">
                                        <div id="divPiece">
                                            <%--   <asp:DropDownList ID="ddlPiecework" runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox ID="txtSearchPiece" runat="server" onclick="popTemplateObj.ShowList('txtSearchPiece')"
                                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        </div>
                                        <div id="divTime" style="display: none;">
                                            <%--       <asp:DropDownList ID="ddlTime" runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox ID="txtSearchTime" runat="server" onclick="popTimeObj.ShowList('txtSearchTime')"
                                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        </div>
                                        <div id="divCommission" style="display: none;">
                                            <%--    <asp:DropDownList ID="ddlCommission" runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox ID="txtSearchRoyalty" runat="server" onclick="popRoyaltyObj.ShowList('txtSearchRoyalty')"
                                                ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td class="tdColTitle">
                                        日期
                                    </td>
                                    <td class="tdColInput">
                                        <asp:TextBox ID="txtStartDate" Width="45%" CssClass="tdinput" ReadOnly="true" runat="server"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>至
                                        <asp:TextBox ID="txtEndDate" Width="45%" CssClass="tdinput" ReadOnly="true" runat="server"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"></asp:TextBox>
                                    </td>
                                    <td class="tdColTitle">
                                    </td>
                                    <td class="tdColInput">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false"
                                            runat="server" style='cursor: pointer;' onclick='DoSearch()' />
                                        <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false"  style='cursor:pointer;' onclick="ClearInput()" width="52" height="23" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList">
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td height="20" colspan="2" align="center" valign="top" class="Title" style="width:100%">
               <div  id="itemName"></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="40" valign="top" colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="30" class="tdColInput">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew"
                                            runat="server" style="cursor: hand" onclick="DoNew();" />
                                        <%--     <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" height="25" onclick="DoSave();"/>--%>
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="true" id="btnDelete"
                                            runat="server" onclick="DoDelete()" style='cursor: pointer;' />
                                    </td>
                                    <td align="right" class="tdColInput">
                                        <%-- <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="导出" id="btnExport" onclick="DoPrint();" style="cursor:hand"  />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" align="center" border="0" cellpadding="0" cellspacing="0" id="tblDetail">
                    <tr>
                        <td style="width: 100%">
                            <div id="divFloatSalaryDetail" style="width: 100%" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divNewInputPage" runat="server" style="padding: 10px; width: 800px; z-index: 1;
        position: absolute; top: 20%; left: 15%; display: none;">
        <input type="hidden" id="hidEditFlag" runat="server" />
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="checktable"
            id="mainindex2">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" />
                </td>
            </tr>
            <tr>
                <td>
                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false"
                        alt="保存" id="btnNewSave" style="cursor: hand" onclick="DoNewSave();" />
                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="true"
                        alt="返回" id="btnBack" onclick="DoBack();" style="cursor: hand" />
                </td>
                <td align="right">
                    <%--           <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand"   />--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="tblBaseInfo" style="display: block">
                        <tr>
                            <td height="20" class="tdColTitle" width="10%">
                                人员编号<span class="redbold">*</span>
                            </td>
                            <td height="20" class="tdColInput" width="23%">
                                <input type="hidden" id="txtNewEmployeeID" />
                                <asp:TextBox ID="txtNewEmployeeNo" onclick="SelectEmployeeInfo();" runat="server"
                                    CssClass="tdinput" Width="95%"></asp:TextBox>
                            </td>
                            <td height="20" class="tdColTitle" width="10%">
                                姓名
                            </td>
                            <td height="20" class="tdColInput" width="23%">
                                <asp:TextBox ID="txtNewEmployeeName" runat="server" CssClass="tdinput" Enabled="false"
                                    Width="95%"></asp:TextBox>
                            </td>
                            <td height="20" class="tdColTitle" width="10%">
                                部门
                            </td>
                            <td height="20" class="tdColInput" width="24%">
                                <asp:TextBox ID="txtNewDept" runat="server" CssClass="tdinput" Enabled="false" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="tdColTitle">
                                岗位
                            </td>
                            <td height="20" class="tdColInput">
                                <asp:TextBox ID="txtNewQuarter" runat="server" CssClass="tdinput" Enabled="false"
                                    Width="95%"></asp:TextBox>
                            </td>
                            <td height="20" class="tdColTitle">
                                岗位职等
                            </td>
                            <td height="20" class="tdColInput">
                                <asp:TextBox ID="txtNewQuarterLevel" runat="server" CssClass="tdinput" Enabled="false"
                                    Width="95%"></asp:TextBox>
                            </td>
                            <td height="20" class="tdColTitle">
                                日期<span class="redbold">*</span>
                            </td>
                            <td height="20" class="tdColInput">
                                <input id="txtSystemDate" runat="server" type="hidden" />
                                <asp:TextBox ID="txtInputDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput"
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtInputDate')})"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="tdColTitle">
                                <div id="divNewSalaryTitle">
                                    计件项目<span class="redbold">*</span></div>
                            </td>
                            <td height="20" class="tdColInput">
                                <div id="divNewPiece">
                                    <%--    <asp:DropDownList ID="ddlNewPiecework" runat="server"></asp:DropDownList>--%>
                                    <asp:TextBox ID="txtPiece" runat="server" onclick="popTemplateObj.ShowList('txtPiece')"
                                        ReadOnly="true" onpropertychange="ChangeItemNo();" CssClass="tdinput" Width="95%"></asp:TextBox>
                                </div>
                                <div id="divNewTime" style="display: none;">
                                    <%-- <asp:DropDownList ID="ddlNewTime" runat="server"></asp:DropDownList>--%>
                                    <asp:TextBox ID="txtTime" runat="server" onclick="popTimeObj.ShowList('txtTime')"
                                        ReadOnly="true" onpropertychange="ChangeItemNo();" CssClass="tdinput" Width="95%"></asp:TextBox>
                                </div>
                                <div id="divNewCommission" style="display: none;">
                                    <%-- <asp:DropDownList ID="ddlNewCommission" runat="server"></asp:DropDownList>--%>
                                    <asp:TextBox ID="txtRoyalty" runat="server" onclick="popRoyaltyObj.ShowList('txtRoyalty')"
                                        ReadOnly="true" onpropertychange="ChangeItemNo();" CssClass="tdinput" Width="95%"></asp:TextBox>
                                </div>
                            </td>
                            <td height="20" class="tdColTitle" id="tdMessage">
                                单价
                            </td>
                            <td height="20" class="tdColInput">
                                <asp:TextBox ID="txtNewUnitPrice" runat="server" CssClass="tdinput" Enabled="false"
                                    Width="95%"></asp:TextBox>
                            </td>
                            <td height="20" class="tdColTitle">
                                <div id="divAmountTitle">
                                    数量<span class="redbold">*</span></div>
                            </td>
                            <td height="20" class="tdColInput">
                                <input type="text" id="txtNewAmount" maxlength="12" class="tdinput" style="width: 95%;"
                                    onblur="CaculateMoney();" onchange="Number_round(this,2)"   />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="tdColTitle">
                                金额
                            </td>
                            <td height="20" class="tdColInput">
                                <asp:TextBox ID="txtNewSalaryTotal" runat="server" CssClass="tdinput" Enabled="false"
                                    Width="95%" onchange="Number_round(this,2)"></asp:TextBox>
                            </td>
                            <td height="20" class="tdColTitle">
                            </td>
                            <td height="20" class="tdColInput">
                            </td>
                            <td height="20" class="tdColTitle">
                            </td>
                            <td height="20" class="tdColInput">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="10">
                </td>
            </tr>
        </table>
    </div>
    <div id="popupContent">
    </div>
    <span id="Forms" class="SpanStype2"></span><span id="spanMsg" class="errorMsg" style="z-index: 1005">
    </span>
    <uc1:Message ID="msgError" runat="server" />
    </form>
</body>
</html>
