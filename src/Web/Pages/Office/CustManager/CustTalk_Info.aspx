<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustTalk_Info.aspx.cs" Inherits="Pages_Office_CustManager_CustTalk_Info" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户洽谈列表</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>    
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>    
   <script src="../../../js/office/CustManager/CustTalkInfo.js" type="text/javascript"></script>    
</head>
<body>
     <form id="form1" runat="server">

<table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /><uc1:Message 
            ID="Message1" runat="server" />
      </td>
    <td rowspan="2" align="right" valign="top"><div id='searchClick'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('searchtable','searchClick')"/></div>&nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td  valign="top" class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
      </td>
  </tr>
  <tr>
    <td colspan="2"  >
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">对应客户</td>
            <td width="10%" bgcolor="#FFFFFF">
                <uc2:CustNameSel ID="CustNameSel1" runat="server" />
              </td>
            <td width="10%" bgcolor="#E7E7E7" align="right">洽谈方式</td>
            <td width="10%" bgcolor="#FFFFFF"><select width="20px" id="ddlTalkType" runat="server">
                                    <option value="0">--请选择--</option>
                                  <option value="1">电话</option>
                                  <option value="2">传真</option>
                                  <option value="3">邮件</option>
                                   <option value="4">远程在线</option>
                                   <option value="5">会晤拜访</option>
                                   <option value="6">综合</option>
                                </select></td>
            <td width="10%" bgcolor="#E7E7E7" align="right">优先级</td>
            <td width="10%" bgcolor="#FFFFFF">
                <select id="selePriority" name="selePriority" width="20px"  runat="server">
                    <option value="0">--请选择--</option>
                    <option value="1">暂缓</option>
                    <option value="2">普通</option>
                    <option value="3">尽快</option>
                    <option value="4">立即</option>
                </select></td>
            <td width="10%" bgcolor="#E7E7E7"  align="right">完成期限</td>            
            <td bgcolor="#FFFFFF" style="width: 25%">
                    <input name="txtTalkBegin" id="txtTalkBegin"  runat="server" type="text" class="tdinput" size="10"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtTalkBegin')})" /> 
                    至<input name="txtTalkEnd" id="txtTalkEnd"  runat="server" type="text" class="tdinput" size="10"  readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtTalkEnd')})" /> </td>
            
          </tr>
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">洽谈主题</td>
            <td bgcolor="#FFFFFF" colspan="3" style="width: 20%">
                    <input name="txtTitle" id="txtTitle" type="text" class="tdinput" style="width:95%" SpecialWorkCheck="洽谈主题" runat="server"
                        maxlength="50" /></td>
            <td width="10%" bgcolor="#E7E7E7" align="right">状态</td>
            <td width="10%" bgcolor="#FFFFFF">
                    <select  name="seleStatus" width="20px" id="seleStatus"  runat="server">
                                    <option value="0">--请选择--</option>
                                  <option value="1">未开始</option>
                                  <option value="2">进行中</option>
                                  <option value="3">已完成</option>
                                </select></td>
            <td width="10%" bgcolor="#E7E7E7"  align="right">&nbsp;</td>            
            <td bgcolor="#FFFFFF" style="width: 25%">
                &nbsp;</td>
            
          </tr>
          <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchTalkData(1)'  visible="false" runat="server"  /> </td>
          </tr>
        </table></td>
      </tr>
    </table>
    </td>
  </tr>
</table>
<table width="95%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
 <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
      </td>
    <td rowspan="2" align="right" valign="top">
    &nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户洽谈列表</td>
  </tr>
  <tr>
    <td height="35" colspan="2" valign="top"><table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="28" bgcolor="#FFFFFF"><img id="btn_create"  src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" style='cursor:hand;' onclick="CreateTalk()" visible="false" runat="server" />
          <img id="btn_del"  src="../../../Images/Button/Main_btn_delete.jpg" alt="删除" style='cursor:hand;' onclick="DelTalkInfo()" visible="false" runat="server" />
          <asp:ImageButton ID="btnImport" runat="server" ImageUrl="~/Images/Button/Main_btn_out.jpg" onclick="btnImport_Click" /></td>
        </tr>
      </table>
      </td>
  </tr>
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" visible="false" name="checkall" id="checkall" onclick="AllSelect('checkall','Checkbox1');" value="checkbox" /></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('TalkNo','oTalkNo');return false;">洽谈编号<span id="oTalkNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('title','otitle');return false;">洽谈主题<span id="otitle" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('custnam','ocustnam');return false;">客户名称<span id="ocustnam" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('linkmanname','olinkmanname');return false;">客户联系人<span id="olinkmanname" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Priority','oPriority');return false;">优先级<span id="oPriority" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('typename','otypename');return false;">洽谈方式<span id="otypename" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CompleteDate','oCompleteDate');return false;">完成期限<span id="oCompleteDate" class="orderTip"></span></div></th>
        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Linker','oLinker');return false;">执行人<span id="oLinker" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Status','oStatus');return false;">状态<span id="oStatus" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('EmployeeName','oEmployeeName');return false;">创建人<span id="oEmployeeName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CreatedDate','oCreatedDate');return false;">创建日期<span id="oCreatedDate" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecount"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage">
              <input name="text" type="text" id="Text2" style="display:none" />
              <span id="pageDataList1_Total"></span>每页显示
              <input name="text" type="text" id="ShowPageCount" maxlength="4"/>
              条  转到第
              <input name="text" type="text" id="ToPage"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark"></a>
<span id="Forms" class="Spantype"></span>
<input id="hiddExpOrder" type="hidden" runat="server" />
<input id="hiddCustID" type="hidden" runat="server" />
<input id="hiddUserId" type="hidden" runat="server" />
</form>
</body>
</html>
