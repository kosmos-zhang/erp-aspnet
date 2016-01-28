<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellRank.aspx.cs" Inherits="Pages_Office_SellReport_SellRank" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>公共分类</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
    function GetNowDate()
    {
        $("#HidBeginTime").val($("#txt_begintime").val());
        $("#HidEndTime").val($("#txt_endtime").val());
    }
    </script>
</head>
<body>
    <form id="frmMain" runat="server">
    <asp:HiddenField ID="HidBeginTime" runat="server" />
    <asp:HiddenField ID="HidEndTime" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件<input 
                    id="hf_typeflag" type="hidden" runat="server" />
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
                                    统计方式</td>
                                    <td width="15%" bgcolor="#FFFFFF">
                                        <select id="summaryType" style="width:100%" runat="server">
                                            <option value="1">按部门/分店统计</option>
                                            <option value="2">按业务员业绩统计</option>
                                            <option value="3">按产品统计</option>
                                        </select>
                                    </td>
                                    <td width="5%" height="20" bgcolor="#E7E7E7" align="right">
                                    排名依据</td>
                                    <td width="15%" bgcolor="#FFFFFF">
                                        <select id="typeorder" style="width:100%" runat="server">
                                            <option value="0">金额</option>
                                            <option value="1">数量</option>
                                        </select>
                                    </td>
                                    <td width="5%" bgcolor="#E7E7E7" align="right">
                                        统计时间
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                       <asp:TextBox ID="txt_begintime" MaxLength="50" runat="server" CssClass="tdinput"
                                            Width="100%" Text="" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txt_begintime')})" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td bgcolor="#E7E7E7">至</td>
                                    <td width="45%" bgcolor="#FFFFFF">
                                       <asp:TextBox ID="txt_endtime" MaxLength="50" runat="server" CssClass="tdinput"
                                            Width="100%" Text="" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txt_endtime')})" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" align="center" bgcolor="#FFFFFF">
                                        <asp:ImageButton ID="btn_query" 
                                            ImageUrl="../../../images/Button/Bottom_btn_search.jpg" OnClientClick="GetNowDate()" Visible="false" runat="server" 
                                            onclick="btn_query_Click" />
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
               销售统计排名情况
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" style="height:22px">
                                序号
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" style="height:22px">
                                <asp:Label ID="lbl_title" runat="server" Text="部门/分店名称"></asp:Label>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                总金额
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                总数量
                            </th>
                        </tr>
                        <asp:Repeater ID="rpt_result1" runat="server">
                        <ItemTemplate>
                        <tr>
                            <td align="center" bgcolor="#FFFFFF" style="height:25px">
                                <%#index++ %>
                            </td>
                            <td align="center"  bgcolor="#FFFFFF">
                                <%#Eval("DeptName")%>
                            </td>
                            <td align="center"  bgcolor="#FFFFFF">
                                <%#Eval("PriceTotal")%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%#Eval("NumTotal")%>
                            </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <br />
                <br />
            </td>
        </tr>
    </table>  <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>


