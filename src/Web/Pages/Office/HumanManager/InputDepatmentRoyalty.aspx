<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputDepatmentRoyalty.aspx.cs"
    Inherits="Pages_Office_HumanManager_InputDepatmentRoyalty" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>部门提成录入</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
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
        .tblMain
        {
            margin-top: 0px;
            margin-left: 0px;
            background-color: #F0f0f0;
            font-family: tahoma;
            color: #333333;
            font-size: 12px;
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
        .errorMsg
        {
            filter: progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
            position: absolute;
            top: 280px;
            left: 450px;
            border-width: 1pt;
            border-color: #666666;
            width: 290px;
            display: none;
            margin-top: 10px;
            z-index: 21;
        }
        .settable
        {
            filter: progid:dximagetransform.microsoft.dropshadow(color=#000000,offx=2,offy=3,positive=true);
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
     <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintb"
        id="mainindex1">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr height="20" align="right">
            <td colspan='3' width='100%'>
                     &nbsp; <a href="InputCompanyRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">公司提成</a>&nbsp;
                            &nbsp;<a href="InputDepatmentRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">部门提成</a>&nbsp;
                               &nbsp;<a href="InputPersonalRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">个人业务提成</a>&nbsp;
                 &nbsp;<a href="InputFloatSalary.aspx?ModuleID=2011702&type=1" style="text-decoration: none; color :Blue"  >计件工资</a>&nbsp;
                &nbsp;<a href="InputFloatSalary.aspx?ModuleID=2011702&type=2" style="text-decoration: none; color :Blue"  >计时工资</a>&nbsp;
                &nbsp;<a href="InputFloatSalary.aspx?ModuleID=2011702&type=3" style="text-decoration: none; color :Blue" >产品单品提成</a>&nbsp;
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
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"> <uc1:Message ID="Message2" runat="server" /><input
                    id="hf_typeflag" type="hidden" runat="server" />
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td width="15%" height="20" bgcolor="#E7E7E7" align="right">
                                        部门名称<span id="oC2" class="orderTip"></span>
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input type="hidden" id="DeptID" value="" />
                                        <input id="DeptName" type="text" onclick="alertdiv('DeptName,DeptID');" class="tdinput" readonly />
                                    </td>
                                    <td width="15%" bgcolor="#E7E7E7" align="right">
                                        生成日期
                                    </td>
                                    <td width="45%" bgcolor="#FFFFFF">
                                        <input id="txtOpenDate" runat="server" class="tdinput" readonly="readonly" name="txtOpenDate"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})" />至
                                        <input id="txtCloseDate" runat="server" class="tdinput" readonly="readonly" name="txtCloseDate"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCloseDate')})" size="15" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_UserInfo()' id="btnSearch" visible="false" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                <input id="hf_ID" type="hidden" />
                <input id="hidSearchCondition" type="hidden" />
                部门提成录入列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg" onclick="Show();" runat="server"
                                visible="true" id="btnNew" /><img alt="" src="../../../Images/Button/Main_btn_delete.jpg"
                                    onclick="DelCodePubInfo();" id="btnDel" runat="server" visible="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAll();" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('DeptName','oGroup');return false;">
                                    部门名称<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BusinessMoney','oC1');return false;">
                                    业务量<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CreateTime','Span3');return false;">
                                    生成日期<span id="Span3" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pagecount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <div id="divBackShadow" style="display: none;">
        <iframe id="BackShadowIframe" frameborder="0" width="100%"></iframe>
    </div>
    <div id="div_Add" style="padding: 10px; width: 500px; z-index: 1000; position: absolute;
        top: 20%; left: 20%; display: none">
        <table width="92%" border="0" id="mainindex2" align="center" style="margin-left: 34px;
            z-index: 100" class="checktable">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" />
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#FFFFFF" align="left">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="InsertCodePublicType();"
                        id="btnSave" runat="server" visible="true" />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" style="z-index: 100"
                        id="tblBaseInfo">
                        <tr height="30px">
                            <td align="right" class="tdColTitle">
                                部门名称：<span class="redbold">*</span>
                            </td>
                            <td class="tdColInput">
                                <input type="hidden" id="Dept_ID" value="" />
                                <input id="Dept_Name" type="text" style="width: 200px" readonly onclick="alertdiv('Dept_Name,Dept_ID');" />
                            </td>
                        </tr>
                        <tr height="30px">
                            <td align="right" class="tdColTitle">
                                业务量：<span class="redbold">*</span>
                            </td>
                            <td class="tdColInput">
                                <asp:TextBox ID="txtBusinessMoney" specialworkcheck="业务量" Width="200px" runat="server"
                                    onkeydown="Numeric_OnKeyDown();" onchange="Number_round(this,'2');"></asp:TextBox>
                            </td>
                        </tr>
                        <tr height="30px">
                            <td align="right" class="tdColTitle">
                                生成日期<span class="redbold">*</span>：
                            </td>
                            <td class="tdColInput">
                                <input id="txtCreateTime" runat="server" style="width: 200px" readonly="readonly"
                                    name="txtOpenDate" onclick="J.calendar.get()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="popupContent">
    </div>
    <span id="spanMsg" class="errorMsg"></span>
    </form>
</body>
</html>

<script src="../../../js/office/HumanManager/InputDepatmentRoyalty.js" type="text/javascript"></script>

