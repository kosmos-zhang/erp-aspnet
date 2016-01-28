<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputCompanyRoyalty.aspx.cs"
    Inherits="Pages_Office_HumanManager_InputCompanyRoyalty" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公司提成录入</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/HumanManager/InputCompanyRoyalty.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
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
    <form id="form1" runat="server">
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
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        分公司
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input name="txtSubCom" id="txtSubCom" type="text" class="tdinput" size="19" readonly="readonly"
                                            onclick="getsubcompany('txtSubCom,hidtxtSubCom');" runat="server" style="width: 95%" />
                                        <input type="hidden" id="hidtxtSubCom" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        记录时间段:
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input name="txtDateStart" id="txtDateStart" type="text" class="tdinput" readonly="readonly"
                                            size="13" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtDateStart')})"
                                            runat="server" />
                                        至
                                        <input name="txtDateEnd" id="txtDateEnd" type="text" class="tdinput" readonly="readonly"
                                            size="13" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtDateEnd')})"
                                            runat="server" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                    </td>
                                    <td width="22%" bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="txtorderBy" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch"
                                            runat="server" style='cursor: hand;' onclick="DoSearch();" visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintb"
        id="mainindex2">
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
                公司提成录入
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" id="btnNew" runat="server"
                                alt="新建" style='cursor: hand;' onclick="ShowNewDiv();" visible="false" />
                            <img id="btnDel" runat="server" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" style='cursor: hand;' border="0" onclick="DoDel();" visible="false" />
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
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <input type="checkbox" name="checkall" id="checkall" onclick="SelectAllCk()" value="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <div class="orderClick">
                                    查看</div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="30%">
                                <div class="orderClick" onclick="OrderBy('DeptID','oc1');return false;">
                                    分公司<span id="oc1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="30%">
                                <div class="orderClick" onclick="OrderBy('BusinessMoney','oc2');return false;">
                                    业务量<span id="oc2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="30%">
                                <div class="orderClick" onclick="OrderBy('RecordDate','oc3');return false;">
                                    记录时间<span id="oc3" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div style="overflow-y: auto">
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
                </div>
                <br />
            </td>
        </tr>
    </table>
    <%---------------------------------------------------新建div-Start-----------------------------------------------%>
    <div id="divBackShadow" style="display: none; z-index: 100">
        <iframe id="BackShadowIframe" frameborder="0" width="100%"></iframe>
    </div>
    <div id="divNew" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 650px; z-index: 1000; position: absolute; display: none; top: 25%; left: 60%;
        margin: 5px 0 0 -400px;" class="checktable1">
        <table width="100%">
            <tr>
                <td>
                    <input type="hidden" id="txtIndentityID" value="0" />
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="DoSave();"
                        id="btnSave" runat="server" visible="true" style="cursor: pointer" />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="DoHide();" />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
            id="Tb_01" style="display: block">
            <tr>
                <td align="right" bgcolor="#E6E6E6" width="10%">
                    分公司<span class="redbold">*</span>
                </td>
                <td bgcolor="#FFFFFF" width="23%">
                    <input name="txtSubCom_uc" id="txtSubCom_uc" maxlength="100" type="text" class="tdinput"
                        size="15" style="width: 95%" onclick="getsubcompany('txtSubCom_uc,hidtxtSubCom_uc');"
                        readonly="readonly" />
                    <input type="hidden" id="hidtxtSubCom_uc" />
                </td>
                <td align="right" bgcolor="#E6E6E6" width="10%">
                    业务量<span class="redbold">*</span>
                </td>
                <td bgcolor="#FFFFFF" width="23%">
                    <input name="txtBusinessMoney" id="txtBusinessMoney" maxlength="100" type="text"
                        class="tdinput" value="0" onblur="CheckNum(this.id)" size="15" style="width: 95%" />
                </td>
                <td align="right" bgcolor="#E6E6E6" width="10%">
                    记录时间<span class="redbold">*</span>
                </td>
                <td bgcolor="#FFFFFF">
                    <input type="text" id="txtRecordDate" class="tdinput" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtRecordDate')})" />
                </td>
            </tr>
        </table>
    </div>
    <%---------------------------------------------------生产任务单div-End-----------------------------------------------%>
    <uc1:Message ID="Message1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
