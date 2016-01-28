<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankInfo.aspx.cs" Inherits="Pages_Office_SupplyChain_BankInfo" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>银行信息</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMain" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <div id="divBackShadow" style="display: none">
    <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
        width="100%"></iframe>
</div>
    <div id="div_Add"   style="border: solid 10px #898989;  background: #fff;  padding: 10px; width: 600px; z-index:21; position: absolute;top: 50%; left: 60%; margin: -200px 0 0 -400px; display:none ">
<%--<iframe id="aaaa" style="position: absolute; z-index: -1; width:400px; height:10px;" frameborder="1">  </iframe>--%>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999" style="margin-left:7px">
      <tr>
        <td height="28" bgcolor="#FFFFFF">
            <img alt="保存"  id="btn_save" runat="server" visible="false" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="InsertBank();"/>
            <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();" />
                   </td>
          </tr>
      </table>
        
        <table width="100%"  border="0" align="center" cellpadding="0" cellspacing="0">
         <tr height="26px">
           <td   align="right">
             银行编号：<span class="redbold">*</span></td>
           <td  >
                <asp:TextBox ID="txt_CustNo" specialworkcheck="银行编号" Width="170px" runat="server"  onblur="checkonly();"  MaxLength="50"></asp:TextBox>
                    </td>
                      <td   align="right">
             银行名称：<span class="redbold">*</span>
           </td>
           <td  >
                <asp:TextBox ID="txt_CustName" specialworkcheck="银行名称" onmouseout="LoadPYShort();" Width="170px" runat="server" MaxLength="100"></asp:TextBox>
            </td>
       </tr>
       
       
       
          <tr height="26px">
           <td   align="right">
             银行简称：
           </td>
           <td  >
                <asp:TextBox ID="txt_CustNam" specialworkcheck="银行简称"  Width="170px"  runat="server" MaxLength="50"></asp:TextBox>
            </td>
             <td   align="right">
             银行拼音代码：
           </td>
           <td  >
                <asp:TextBox ID="txt_PYShort" specialworkcheck="银行拼音代码"  Width="170px" runat="server" MaxLength="50"></asp:TextBox>
            </td>
       </tr>
        <tr height="26px">
                <td    align="right">
                   联系人：</td>
                <td >
                   <asp:TextBox ID="txt_ContactName" specialworkcheck="联系人" Width="170px" runat="server" MaxLength="50"></asp:TextBox>
                </td>
                  <td    align="right">
                   电话：</td>
                <td >
                    <asp:TextBox ID="txt_Tel"  Width="170px"  specialworkcheck="电话" runat="server" MaxLength="50"></asp:TextBox>
                </td>
                </tr>
        <tr height="26px">
           <td   align="right">
                    建档日期：<span class="redbold">*</span></td>
                <td  >
                    <input id="txt_CreateDate"  disabled name="txtbuydate0" runat="server"
                                size="15" type="text" style=" width:170px"/>
                </td>
          <td   align="right">
                    建档人：<span class="redbold">*</span></td>
                <td  >
                   <asp:TextBox ID="UserPrincipal" Enabled="false" runat="server"
                                Width="170px"></asp:TextBox>
                            <input type="hidden" id="txtPrincipal" runat="server" />
                </td>
              
                </tr>
                 <tr height="26px">
                <td   align="right">
                    地址：</td>
                <td  >
                    <asp:TextBox ID="txt_Addr" specialworkcheck="地址"  Width="170px" runat="server" MaxLength="50"></asp:TextBox>
                </td>
                 <td   align="right">
                    备注：</td>
                <td  >
                    <asp:TextBox ID="txt_Remark"  Width="170px" runat="server" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                </td>
                </tr>
                 <tr height="26px">
              
                    <td   align="right">
                    传真：</td>
                <td  >
                    <asp:TextBox ID="txt_Fax"  Width="170px" runat="server" MaxLength="50"></asp:TextBox>
                </td>
                 <td   align="right">
                    手机：</td>
                <td  >
                    <asp:TextBox ID="txt_Mobile"  Width="170px" runat="server" MaxLength="50"></asp:TextBox>
                </td>
              
                </tr>
          <tr height="26px">
          <td    align="right">
                    启用状态：</td>
                <td   >
                    启用<input id="rd_use" type="radio" value="1" name="RadUsedStatus"  checked=checked/>
                    停用<input id="rd_notuse" type="radio" value="0" name="RadUsedStatus" /></td>
          </tr>
        </table>

</div>
   
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
                                        银行编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txt_BankNo"  class="tdinput" type="text" maxlength="50" /></td>
                                           <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                       银行名称
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txt_BankName"  class="tdinput" type="text" maxlength="100" /></td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        启用状态
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                       <select id="UsedStatus" runat="server" name="SetPro2" width="139px">
                                        <option value="">--请选择--</option>
                           <option value="1">启用</option>
                          <option value="0">停用</option>
                                </select>
                                    </td>
                                </tr>
                                 <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        联系人</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                         <asp:HiddenField ID="txtPlanNoHidden" runat="server" />
                                         <input id="txt_lx"  class="tdinput" type="text" maxlength="50" /></td>
                                           <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                       拼音缩写
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF" colspan="3">
                                        <input id="txt_py"  class="tdinput" type="text"  maxlength="50" /></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_BankInfo()' id="btnQuery" visible="false" runat="server" />
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
            银行档案列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
           <img alt=""  src="../../../Images/Button/Bottom_btn_new.jpg" 
                onclick="Show();" runat="server" visible="false" id="btnNew" /><img alt="" id="btn_del"  runat="server" visible="false" src="../../../Images/Button/Main_btn_delete.jpg" 
                                onclick="DelBankInfo();"  /></td>
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
                                <div class="orderClick" onclick="OrderBy('CustNo','oGroup');return false;">
                                   银行编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustName','oC1');return false;">
                                    银行名称<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ContactName','oC4');return false;">
                                    联系人<span id="oC4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Tel','oC4');return false;">
                                    联系电话<span id="Span1" class="orderTip"></span></div>
                            </th>
                               <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UsedStatus','oC1');return false;">
                                    启用状态<span id="Span7" class="orderTip"></span></div>
                            </th>
                               <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Creator','oC1');return false;">
                                    建档人<span id="Span8" class="orderTip"></span></div>
                            </th>
                               <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CreateDate','oC1');return false;">
                                    建档日期<span id="Span9" class="orderTip"></span></div>
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
    </table> <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>

<script src="../../../js/office/SupplyChain/BankInfo.js" type="text/javascript"></script>

