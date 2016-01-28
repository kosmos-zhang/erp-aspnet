<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductInfoList.aspx.cs"
    Inherits="Pages_Office_SupplyChain_ProductInfoList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/office/SupplyChain/ProductInfoList.js" type="text/javascript"></script>

    <style type="text/css">
        #pageDataList1 TD
        {
            color: #333333;
        }
        #userList
        {
            border: solid 1px #111111;
            width: 200px;
            z-index: 11;
            display: none;
            position: absolute;
            background-color: White;
        }
        #editPanel
        {
            width: 400px;
            background-color: #fefefe;
            position: absolute;
            border: solid 1px #000000;
            padding: 5px;
        }
    </style>

    <script type="text/javascript">
      function hidetxtUserList()
    {
        hideUserList();
        document.getElementById("txt_TypeID").value="";
          document.getElementById("txt_ID").value="";
    }
    
       function getChildNodes(nodeTable)
      {
            if(nodeTable.nextSibling == null)
                return [];
            var nodes = nodeTable.nextSibling;  
           
            if(nodes.tagName == "DIV")
            {
                return nodes.childNodes;//return childnodes's nodeTables;
            }
            return [];
      }
        function showUserList()
        {
            var list = document.getElementById("userList");
           
            if(list.style.display != "none")
            {
                list.style.display = "none";
                return;
            }
            
            var pos = elePos(document.getElementById("txt_TypeID"));
            
            list.style.left = pos.x;
            list.style.top = pos.y+20;
            document.getElementById("userList").style.display = "block";
        }
        
        
        function hideUserList()
        {
            document.getElementById("userList").style.display = "none";
        }
        function GetQuery()
        {
          //把扩展属性的值传给隐藏域
            document.getElementById("HdselEFIndex").value = document.getElementById("GetBillExAttrControl1_SelExtValue").value;
            document.getElementById("HdtxtEFDesc").value = document.getElementById("GetBillExAttrControl1_TxtExtValue").value;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="HdselEFIndex" type="hidden" runat="server" />
    <input id="HdtxtEFDesc" type="hidden" runat="server" />
    <input id="HiddenBarCode" type="hidden" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                <uc1:Message ID="Message1" runat="server" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                <uc1:Message ID="Message2" runat="server" />
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
                                        物品分类
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_TypeID" readonly onclick="showUserList()" class="tdinput"
                                            runat="server"><asp:HiddenField ID="hidSearchCondition" runat="server" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        物品编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_ProdNo" specialworkcheck="物品编号" name="txtConfirmorReal0"
                                            class="tdinput" runat="server" /><input id="txt_ID" runat="server" type="hidden" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        拼音缩写
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_PYShort" specialworkcheck="拼音缩写" name="txtConfirmorReal2"
                                            class="tdinput" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        物品名称
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="txt_ProductName" specialworkcheck="物品名称" MaxLength="50" runat="server"
                                            CssClass="tdinput" Width="51%"></asp:TextBox>
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        条码
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txt_BarCode" cols="20" specialworkcheck="条码" name="S1" class="tdinput"
                                            runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        规格型号
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_Specification"  name="txtConfirmorReal3"
                                            class="tdinput" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        &nbsp;启用状态
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select id="UsedStatus" runat="server" name="SetPro2" width="139px">
                                            <option value="">--请选择--</option>
                                            <option value="1">启用</option>
                                            <option value="0">停用</option>
                                        </select>
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        审核状态
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select id="CheckStatus" runat="server" name="SetPro3" width="139px">
                                            <option value="">--请选择--</option>
                                            <option value="0">草稿</option>
                                            <option value="1">已审</option>
                                        </select>
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        颜色
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <asp:DropDownList ID="selCorlor" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="table-item" id="DivTd">
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        <span id="OtherConditon" style="display: none">其他条件</span>
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtSubStore" type="text" class="tdinput tboxsize" maxlength="25" style="display: none" />
                                        <uc3:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <asp:HiddenField ID="hidModuleID" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_ProductInfo()' id="btnQuery" visible="false" runat="server" />
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
                <input id="hf_ID" type="hidden" />
            物品档案列表
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg" onclick="Show();" runat="server"
                                visible="false" id="btnNew" />
                            <img alt="" id="btnDel" runat="server" visible="false" src="../../../Images/Button/Main_btn_delete.jpg"
                                onclick="Fun_Delete_ProductInfo();" /><asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                    AlternateText="导出Excel" runat="server" OnClick="btnImport_Click" OnClientClick="GetQuery();" />
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
                                <div class="orderClick" onclick="OrderBy('ProdNo','oGroup');return false;">
                                    物品编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ProductName','oC2');return false;">
                                    物品名称<span id="oC2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('TypeName','oC3');return false;">
                                    物品分类<span id="oC3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UnitName','oC5');return false;">
                                    基本单位<span id="oC5" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Specification','Span9');return false;">
                                    规格型号<span id="Span9" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ColorName','ColorName');return false;">
                                    颜色<span id="ColorName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Creator','Span7');return false;">
                                    建档人<span id="Span7" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    建档时间<span id="Span8" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CheckStatus','Span5');return false;">
                                    审核状态<span id="Span1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UsedStatus','Span5');return false;">
                                    启用状态<span id="Span5" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                id="ThShow" style="display: none">
                                <div class="orderClick" id="DivOtherC" onclick="OrderBy('UsedStatus','Span5');return false;">
                                    <span id="Span2" class="orderTip"></span>
                                </div>
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
                                            <input name="text" type="hidden" id="Text2" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
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
    <div id="userList" style="display: none;">
        <iframe id="aaaa" style="position: absolute; z-index: -1; width: 200px; height: 100px;"
            frameborder="0"></iframe>
        <div style="background-color: Silver; padding: 3px; height: 20px; padding-left: 50px;
            padding-top: 1px">
            <a href="javascript:hidetxtUserList()" style="float: right;">关闭</a>
        </div>
        <div style="padding-top: 5px; height: 300px; width: 200px; overflow: auto; margin-top: 1px">
            <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
            </asp:TreeView>
        </div>
    </div>
    </form>
</body>
</html>
