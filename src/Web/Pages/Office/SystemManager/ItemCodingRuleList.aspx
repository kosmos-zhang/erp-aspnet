<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemCodingRuleList.aspx.cs" Inherits="Pages_Office_SystemManager_ItemCodingRuleList" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编号规则设置</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
     <style>
  *{padding:0; margin:0} /*此行样式一定要加，不然可能会引起BUG出现。*/

  #div_Add{
		position:absolute;
		width:200px;
		height:250px;
		font-size:12px;
		background:#666;
		border:1px solid #000;
		z-index:950;
		display:none;
		
  }
 </style>
</head>
<body>
    <form id="frmMain" runat="server"> <uc1:Message ID="Message1" runat="server" />


<%-- <div  id="name"  align="right">
               单据名称：<span class="redbold">*</span></div>--%>
         


                    
                    
                    
    
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
               编号规则设置
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
            <img alt="" src="../../../Images/Button/Bottom_btn_new.jpg"
                onclick="Show();" runat="server" visible="false" id="btnNew" /><img alt="" src="../../../Images/Button/Main_btn_delete.jpg" 
                                onclick="DelItemCodingRule();" runat="server" visible="false" id="btnDel"/><input 
                                id="hf_typeflag" type="hidden" runat="server" value="" /><input id="hf_ID" 
                                type="hidden" /><asp:HiddenField ID="hf_name" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999" >
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAll();" />
                            </th>
                             <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RuleName','oGroup');return false;">
                                    编号规则名称<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th  align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div id="jname" class="orderClick" onclick="OrderBy('typeid','oID');return false;">
                                    单据名称<span id="oID" class="orderTip"></span></div>
                            </th>
                              <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RulePrefix','Span4');return false;">
                                    编号前缀<span id="Span4" class="orderTip"></span></div>
                            </th>
                                <th id="datetypeflag" runat="server" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RuleDateType','Span5');return false;">
                                    日期类型<span id="Span5" class="orderTip"></span></div>
                            </th>
                             <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RuleNoLen','Span8');return false;">
                                    流水号长度
                               <span id="Span8" class="orderTip"></span></div>
                            </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RuleExample','Span6');return false;">
                                    编号示例<span id="Span6" class="orderTip"></span></div>
                            </th>
                                 <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('IsDefault','Span7');return false;">
                                    <span>是否为缺省规则</span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UsedStatus','Span3');return false;">
                                    启用状态<span id="Span3" class="orderTip"></span></div>
                            </th>
                          <%--  <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Remark','Span2');return false;">
                                    备注<span id="Span2" class="orderTip"></span></div>
                            </th>--%>
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
    </table> <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>   <div id="divBackShadow" style="display: none">
    <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
        width="100%"></iframe></div>
     <div id="div_Add"   style="border: solid 10px #898989; z-index:21; background: #fff;  padding: 10px; width: 400px; top: 48%; left: 63%; margin: -200px 0 0 -400px; ">
    <table width="92%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999" style=" margin-left:34px">
      <tr>
        <td height="28" bgcolor="#FFFFFF" align="left">
          
            <img alt="保存"  src="../../../Images/Button/Bottom_btn_save.jpg" onclick="InsertItemCodingRule();" id="btnSave" runat="server" visible="false"/>
            <img alt="返回"  src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();" /></td>
          </tr>
      </table>
        
        <table width="100%"  border="0" align="center" cellpadding="0" cellspacing="1">
        <tr  >
         <td  id="name" align="right">
                    单据名称：<span class="redbold">*</span></td>
                <td >
                    <asp:DropDownList ID="drp_typecode" runat="server" Width="164px">
                    </asp:DropDownList>
                </td>
        </tr>
      <%--   <tr>
           <td  id="name"  align="right">
               单据名称：<span class="redbold">*</span></td>
           <td>
                  
                    </td>
       </tr>--%>
       <tr>
       <td align="right">
                    编号规则名称：<span class="redbold">*</span></td>
                <td >
                    <asp:TextBox ID="txt_RuleName"  Width="160px" runat="server" specialworkcheck="编号规则名称"></asp:TextBox>
                </td>
       </tr>
        <tr >
                <td    align="right">
                    编号前缀：<span class="redbold">*</span></td>
                <td >
                    <asp:TextBox ID="txt_RulePrefix"  Width="160px" runat="server" onblur="fillRuleExample();" specialworkcheck="编号前缀"></asp:TextBox>
                </td>
                </tr>
        <tr id="datetype">
                <td   align="right">
                    日期类型：</td>
                <td  >
                    <input id="rd_year" type="radio" value="1" name="RadUsedStatus"  checked=checked/>年<input id="rd_yearm" type="radio" value="1" name="RadUsedStatus"/>年月<input id="rd_yearmd" type="radio" value="1" name="RadUsedStatus"/>年月日
                </td>
                </tr>
                  <tr>
                <td   align="right">
                    流水号长度：<span class="redbold">*</span></td>
                <td  >
                    <asp:TextBox ID="txt_RuleNoLen" onblur="fillRuleExample();"  Width="160px" runat="server"></asp:TextBox>
                </td>
                </tr>
                  <tr>
                <td   align="right">
                    编号示例：</td>
                <td  >
                    <asp:TextBox ID="txt_RuleExample" runat="server"  Width="160px" ReadOnly="true" Enabled="false"></asp:TextBox>
                </td>
                </tr>
                  <tr>
                <td   align="right">
                    备注：</td>
                <td  >
                    <asp:TextBox ID="txt_Remark"  Width="160px" runat="server" MaxLength="100" specialworkcheck="备注"></asp:TextBox>
                </td>
                </tr>
          <tr>
          <td    align="right">
                    是否为缺省规则：</td>
                <td   >
                    <input id="chk_default" type="checkbox" checked="checked" />
                </td>
          </tr>
            <tr>
          <td    align="right">
                    启用状态：</td>
                <td   >
                    <asp:DropDownList ID="drp_use" runat="server">
                       <asp:ListItem Value="0">停用</asp:ListItem>
                        <asp:ListItem Value="1">启用</asp:ListItem>
                    </asp:DropDownList>  </td>
          </tr>
        </table>

</div>
    </form>
</body>
</html>
<script src="../../../js/office/SystemManager/ItemCodingRule.js" type="text/javascript"></script>
