<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseRequireInfo.aspx.cs"
    Inherits="Pages_Office_PurchaseManager_PurchaseRequireInfo" %>

 
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>采购需求列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/office/PurchaseManager/PurchaseRequireInfo.js" type="text/javascript"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <style  type="text/css">
       #Product_type
    {
        border: solid 1px #111111;
        width: 165px;
        z-index: 11;
        display: none;
        position: absolute;
        background-color: White;
    }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">

        <input id="HiddenPoint" type="hidden" runat="server" />
 
    
    <a name="DetailListMark"></a>
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                <div style="display: none">
                    <input type="text" id="SearchCondition" />
                </div>
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblSearch','divSearch')" />
                </div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">
                                        物料分类
                                    </td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtProductTypeName" id="txtProductTypeName" maxlength="50" onclick="shotypeList()"
                                            readonly="readonly" type="text" class="tdinput" style="width: 95%" />
                                        <input name="txtProductTypeID" id="txtProductTypeID"  type="hidden" runat="server"/>
                                    </td>
                                    <td class="tdColTitle" width="10%">
                                        物料名称
                                    </td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtProductNo" id="txtProductNo" type="hidden"  />
                                        <input name="txtProductID" id="txtProductID" type="hidden" runat="server" />
                                      <uc3:ProductInfoControl ID="ProductInfoControl1" runat="server" />
                                        <input id="txtProductName" class="tdinput" maxlength="25" name="txtProductName" onclick="popTechObj.ShowList('','txtProductName','txtProductID');"
                                            readonly="readonly" style="width: 99%" type="text" />
                                    </td>
                                    <td class="tdColTitle">
                                        生成计划情况
                                    </td>
                                    <td class="tdColInput">
                                        <select name="ddlCreate" class="tdinput" width="119px" id="ddlCreate" runat="server">
                                            <option value="0" selected="selected">--请选择--</option>
                                            <option value="1">没有生成</option>
                                            <option value="2">已部分生成</option>
                                            <option value="3">已生成完毕</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">
                                        起始需求时间
                                    </td>
                                    <td class="tdColInput">
                                        <input type="hidden" id="hidOrderBy"  runat="server" value="MRPNo ASC" />
                                        <input name="txtStartRequireDate" id="txtStartRequireDate" maxlength="25" type="text" runat="server"
                                            class="tdinput" style="width: 95%" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})" />
                                    </td>
                                    <td class="tdColTitle">
                                        终止需求时间
                                    </td>
                                    <td class="tdColInput">
                                        <input name="txtEndRequireDate" id="txtEndRequireDate" maxlength="25" type="text" runat="server"
                                            class="tdinput" style="width: 99%" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})" />
                                    </td>
                                    <td class="tdColTitle">
                                    </td>
                                    <td class="tdColInput">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="hdSearchCondition" name="hdSearchCondition" />
                                        <img runat="server" visible="false" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                            id="btnSearch" runat="server" style='cursor: pointer;' onclick='SearchPurchaseRequire()' />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
      
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
    <tr>
            <td colspan="2">
                <table width="98%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList">
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" /><input type="hidden" id="hfModuleID" runat="server" />
                        </td>
                        <td align="center" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="2">
                        </td>
                    </tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">
                            采购需求列表
                        </td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <img runat="server" visible="false" src="../../../images/Button/Main_btn_delete.jpg"
                                            alt="删除" id="btnDelete" runat="server" onclick="DeletePurRequireInfo()" style='cursor: pointer;
                                            display: none;' />
                                        <img runat="server" visible="false" alt="生成计划" src="../../../Images/Button/Bottom_btn_plan.jpg"
                                            runat="server" id="btnGenerate" onclick="GeneratePurPlan();" />
                                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                alt="导出Excel" OnClick="btnImport_Click"/>
                                        <uc2:Message ID="Message1" runat="server" />
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
                                            选择<input type="checkbox" id="checkall" onclick="SelectAll();" />
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('MRPNo','oID');return false;">
                                                物料需求计划单编号<span id="oID" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('ProdNo','oGroup');return false;">
                                                物料编码<span id="oGroup" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('ProductName','Span1');return false;">
                                                物料名称<span id="Span1" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('ProductTypeName','Span2');return false;">
                                                物料分类<span id="Span2" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('Specification','Span3');return false;">
                                                规格<span id="Span3" class="orderTip"></span></div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('ColorName','Span3');return false;">
                                                颜色<span id="Span10" class="orderTip"></span></div>
                                        </th>
                                        
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('UnitName','Span4');return false;">
                                                单位<span id="Span4" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('NeedCount','Span5');return false;">
                                                订单需求量<span id="Span5" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('HasNum','Span6');return false;">
                                                现有存量<span id="Span6" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('WantingNum','Span8');return false;">
                                                需申购数量<span id="Span8" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('WaitingDays','Span9');return false;">
                                                采购提前期<span id="Span9" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('OrderCount','Span11');return false;">
                                                已计划数量<span id="Span11" class="orderTip"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('RequireDate','Span7');return false;">
                                                需求日期<span id="Span7" class="orderTip"></span></div>
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
                                                    <div id="pagePurRequirecount">
                                                    </div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="pageDataList1_Pager" class="jPagerBar">
                                                    </div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="divpage">
                                                        <input name="Text2" type="text" id="Text2" style="display: none" />
                                                        <span id="pageDataList1_Total"></span>每页显示
                                                        <input name="text" type="text" id="ShowPageCount" />条 转到第
                                                        <input name="text" type="text" id="ToPage" />页
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
            </td>
        </tr>
    </table>
     <div id="Product_type" style="display: none;">
            <iframe id="bbb" style="position: absolute; z-index: -1; width: 165px; height: 100px;"
                frameborder="0"></iframe>
            <div style="background-color: Silver; padding: 3px; height: 20px; padding-left: 50px;
                padding-top: 1px">
                <a href="javascript:hideTypeList()" style="float: right;">清空</a>
            </div>
            <div style="padding-top: 5px; height: 300px; width: 165px; overflow: auto; margin-top: 1px">
                <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
                </asp:TreeView>
            </div>
        </div> 
    </form>
    <span id="Forms" class="Spantype" name="Forms"></span>
</body>
</html>
