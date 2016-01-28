<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageJournal.aspx.cs" Inherits="Pages_Office_StorageManager_StorageJournal"   EnableEventValidation="false"  %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/StorageManager/StorageJournal.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/common.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">




        $(document).ready(function() {
            // Your code here...

        var obj = document.getElementById("ddlBatchNo");
        obj.options.length = 1;

        var Storage = "";

        var ProductNo = "";
        //定义反确认动作变量
        var action = "GetBatchNo";
        var postParam = "action=" + action + "&Storage=" + Storage + "&ProductNo=" + ProductNo;
        $.ajax(
        {
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageSearchInfo.ashx?" + postParam,
            dataType: 'html', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
            },
            success: function(msg) {
                var msginfo = msg.toString().split(',');
                for (var i = msginfo.length - 1; i >= 0; i--) {
                    if (msginfo[i].toString() != "") {
                        var varItem = new Option(msginfo[i].toString(), msginfo[i].toString());
                        obj.options.add(varItem);
                    }
                }
            }
        });
    
        });

        function changeUrl() {
            var ret = document.getElementById("ddlShowType").value;
            if (ret == "1") {
                window.location.href = "StorageJournalDetails.aspx?ModuleID=2051903";
            }
            else {
                window.location.href = "StorageJournal.aspx?ModuleID=2051903";
            }
        }
    </script>
<script type="text/javascript" language="javascript">
    function ClearPkroductInfo() {
        document.getElementById("txtProductNo").value = "";
        document.getElementById("hiddenProductID").value = "";
        closeProductdiv();
    }
</script>
    <title>库存流水账查询</title>
</head>
<body>
    <form id="form1" runat="server">
    <input id="HiddenPoint" type="hidden" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable1','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable1" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        仓库
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <asp:DropDownList ID="ddlStorage" runat="server" >
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" bgcolor="#E6E6E6" width="10%">
                                        物品编号
                                    </td>
                                    <td height="20" bgcolor="#FFFFFF" width="25%">
                                        <input name="txtProductNo" id="txtProductNo" type="text" class="tdinput" readonly="readonly"
                                            size="19" onclick="popTechObj.ShowList('a','txtProductNo','hiddenProductID')"
                                            style="width: 95%" runat="server"   />
                                        <input type="hidden" id="hiddenProductID" runat="server"  />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        &nbsp;供应商
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <uc4:ProviderInfo ID="ProviderInfo1" runat="server" />
                                        <input type="hidden" id="txtProviderID" runat="server" />
                                        <asp:TextBox ID="txtProviderName" MaxLength="50" runat="server" CssClass="tdinput"
                                            ReadOnly="true" Width="95%" onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName');"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        开始时间
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="txtStartDate" runat="server" class="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})"
                                            ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="HidStartDate" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        结束时间
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="txtEndDate" runat="server" class="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})"
                                            ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="HidEndDate" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" id="trNewAttr">
                                        <span id="OtherConditon" style="display: none">其他条件</span>
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <uc3:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        批次</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                          <asp:DropDownList ID="ddlBatchNo" runat="server">
                                            <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据类型</td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <asp:DropDownList ID="ddlSourceType" runat="server">
                                            <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">期初库存录入</asp:ListItem>
                                            <asp:ListItem Value="2">期初库存批量导入</asp:ListItem>
                                            <asp:ListItem Value="3">采购入库单</asp:ListItem>
                                            <asp:ListItem Value="4">生产完工入库单</asp:ListItem>
                                            <asp:ListItem Value="5">其他入库单</asp:ListItem>
                                            <asp:ListItem Value="6">红冲入库单</asp:ListItem>
                                            <asp:ListItem Value="7">销售出库单</asp:ListItem>
                                            <asp:ListItem Value="8">其他出库单</asp:ListItem>
                                            <asp:ListItem Value="9">红冲出库单</asp:ListItem>
                                            <asp:ListItem Value="10">借货申请单</asp:ListItem>
                                            <asp:ListItem Value="11">借货返还单</asp:ListItem>
                                            <asp:ListItem Value="12">调拨出库</asp:ListItem>
                                            <asp:ListItem Value="13">调拨入库</asp:ListItem>
                                            <asp:ListItem Value="14">日常调整单</asp:ListItem>
                                            <asp:ListItem Value="15">期末盘点单</asp:ListItem>
                                            <asp:ListItem Value="16">库存报损单</asp:ListItem>
                                            <asp:ListItem Value="17">领料单</asp:ListItem>
                                            <asp:ListItem Value="18">退料单</asp:ListItem>
                                            <asp:ListItem Value="19">配送单</asp:ListItem>
                                            <asp:ListItem Value="20">配送退货单</asp:ListItem>
                                            <asp:ListItem Value="21">门店销售管理</asp:ListItem>
                                            <asp:ListItem Value="22">门店销售退货</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" >
                                        单据编号</td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                    <asp:TextBox ID="txtSourceNo" runat="server" class="tdinput"></asp:TextBox>
                                    <asp:CheckBox ID="ckbIsM" runat="server" Text="模糊匹配" />
                                         </td>
                                </tr>
                                <tr>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        创建人</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                          <asp:TextBox ID="UserCreator" MaxLength="50" onclick="alertdiv('UserCreator,txtCreatorID');"  ReadOnly="true"
                                runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtCreatorID" runat="server" /></td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        显示类别</td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <asp:DropDownList ID="ddlShowType" runat="server" onchange="changeUrl();">
                                            <asp:ListItem Value="2">汇总</asp:ListItem>
                                            <asp:ListItem Value="1">明细</asp:ListItem>
                                          
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        &nbsp;</td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="txtorderBy" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            id="btn_Search" runat="server" onclick='Fun_Search_StorageInfo();' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                库存流水账列表             </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <asp:ImageButton ID="btnImport" runat="server" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                alt="导出Excel" OnClick="btnImport_Click" Visible="true" />
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
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                height="20">
                                <div class="orderClick" onclick="OrderBy('StorageNo','Span4');return false;">
                                    仓库编号<span id="Span4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('StorageName','oc1');return false;">
                                    仓库名称<span id="oc1" class="orderTip"></span></div>
                            </th>
                            
                             <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" >
                                    批次<span id="Span9" class="orderTip"></span></div>
                            </th>
                            
                            
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ProdNo','Span1');return false;">
                                    物品编号<span id="Span1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ProductName','oc4');return false;">
                                    物品名称<span id="oc4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Specification','Span3');return false;">
                                    规格型号<span id="Span3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ProductSize','oc5');return false;">
                                    尺寸<span id="oc5" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FromAddr','Span2');return false;">
                                    产地<span id="Span2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('InProductCount','Span6');return false;">
                                    入库总量<span id="Span6" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                id="OutStorage">
                                <div class="orderClick" onclick="OrderBy('OutProductCount','Span5');return false;">
                                    出库总量<span id="Span5" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                id="NowStorage">
                                <div class="orderClick" onclick="OrderBy('NowProductCount','Span7');return false;">
                                    现有存量<span id="Span7" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                id="thHole" style="display: none">
                                <div id="divClick" class="orderClick" onclick="">
                                    <span id="newItem"></span><span id="Span8" class="orderTip"></span>
                                </div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div style="overflow-y: auto;">
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
    <uc1:Message ID="Message1" runat="server" />
    <uc2:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <asp:HiddenField ID="hiddenEFIndex" runat="server" />
    <asp:HiddenField ID="hiddenEFIndexName" runat="server" />
    <asp:HiddenField ID="hiddenEFDesc" runat="server" />
    </form>
</body>
</html>
