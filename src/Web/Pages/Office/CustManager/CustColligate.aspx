<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustColligate.aspx.cs" Inherits="Pages_Office_CustManager_CustColligate" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc2" %>
<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>综合查询</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
<script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/common/page.js" type="text/javascript"></script>
<script src="../../../js/common/Common.js" type="text/javascript"></script>
<script src="../../../js/office/CustManager/CustColligate.js" type="text/javascript"></script>

    <script src="../../../js/office/CustManager/CustCollTree.js" type="text/javascript"></script>

<style type="text/css">
.busBtn
{
	background: url(../../../Images/default/btnbg2.jpg) 0px -5px;
	border: 1px solid #cccccc;
	padding-top: 2px;
	cursor: pointer;
}

.busBtn2
{
	background: url(../../../Images/default/btnbg.gif) 0px -5px;
	border: 1px solid #cccccc;
	padding-top: 2px;
	cursor: pointer;
}
 /*选项卡2*/.Menubox
{
    height: 28px;
    line-height: 28px;
    overflow: hidden;
}

#mainindex2{
        margin-top:10px;
        margin-left:10px;
		background-color:#F0f0f0;
      	font-family:"tahoma";
      	color:#333333;
      	font-size:12px; 
      	width:98%;
}
    
    .style1
    {
        width: 10%;
        height: 19px;
    }
    .style2
    {
        width: 26%;
        height: 19px;
    }
    .style3
    {
        width: 22%;
        height: 19px;
    }
    
</style>
</head>
<body>
   <form id="form1" runat="server">

<table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="Table2">
<tr>
<td valign="top" style="width:15%;" >
        <table width="95%" id="mainindex" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
                <td height="20" bgcolor="#F4F0ED" >
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="3" class="Blue">
                                
                                <a href="#" onclick="MenuChange('TreeBomNo');">客 户 列 表</a>
                               
                            </td>
                        </tr>
                        <tr>
                            <td style="width:18%;">
                               查找
                            </td>
                            <td style="width:5%;">
                               <img src="../../../Images/search.gif" style="cursor: pointer" onclick="SearchCustData();" />
                            </td>
                             <td>
                              <uc1:CustNameSel ID="CustNameSel1" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="width: 180px; height: 380px; overflow: auto; border: 1px solid #CCCCCC;">
                                    <div>
                                        <div id="M2_0" style="display: block; width: 180;">
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%--<table id="mainindex" width="15%" height="57" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <div style="width: 180px; height: 380px; overflow: auto; border: 1px solid #CCCCCC;">
                        <div>
                            <div id="M2_0" style="display: block; width: 180;">
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>--%>
</td>


<td valign="top" align="left" style="width:85%;" >
        
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="checktable" id="Table3">
<tr>
<td>
        
<!--检索条件Begin-->
<table style="width:98%;" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex2">
  <tr style="width:98%;">
    <td valign="top" style="width:98%;">
    <img src="../../../images/Main/Line.jpg" width="122" height="7" />    
    </td>    
  </tr> 
  <tr style="width:98%;">
    <td colspan="2" style="width:98%;" >
 
    <table width="100%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0"  bgcolor="#FFFFFF">
      <tr>
      <td>
      <input id="btn_da" onclick="ShowList('1')" style="height:22px;width:60px;text-align:center" class="busBtn2" type="button" value="客户档案"/>
      </td>
      <td>
      <input id="btn_lxr" onclick="ShowList('2')" style="height:22px;width:50px;text-align:center"  class="busBtn" type="button" value="联系人" />
       </td>
      <td>
      <input id="btn_ll" onclick="ShowList('3')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="客户联络" />
       </td>
      <td>
      <input id="btn_qt" onclick="ShowList('4')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="客户洽谈" />
       </td>
      <td>
      <input id="btn_gh" onclick="ShowList('5')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="客户关怀" />
       </td>
      <td>
      <input id="btn_fw" onclick="ShowList('6')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="客户服务" />
       </td>
      <td>
      <input id="btn_ts" onclick="ShowList('7')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="客户投诉" />
       </td>
      <td>
      <input id="btn_jy" onclick="ShowList('8')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="客户建议" />
        </td>
      <td>
       <input id="btn_ld" type="hidden" />
      <%--<input id="btn_ld" onclick="ShowList('9')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="客户来电" />--%>
        <input id="btn_jh" onclick="ShowList('14')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="销售机会" />
       </td>
      <td>
      <input id="btn_gm" onclick="ShowList('10')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="购买记录" />
       </td>
      <td>
      <input id="btn_fh" onclick="ShowList('11')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="发货记录" />
       </td>
      <td>
      <input id="btn_hk" onclick="ShowList('12')" style="height:22px;width:60px;text-align:center"  class="busBtn" type="button" value="回款计划" />
       </td>
      <td>
      <input id="btn_jl" onclick="ShowList('13')" style="height:22px;width:70px;text-align:center"  class="busBtn" type="button" value="预付及回款" />
      
      </td>
      </tr>
      </table>      
    </td>
  </tr>
</table>
<!--检索条件End-->

<!--9个列表主体 Begin-->




<!--客户档案列表js Begin-->
<div id="div_da"  style="display:block;">
 <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01" >
              <tr>
              <td align="center" height="30" class="Title" align="right" bgcolor="#FFFFFF" colspan="6">客户档案</td>
               
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户编号</td>
                <td align="left" bgcolor="#FFFFFF" style="width:26%">
                    <input id="txtCustNo" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户名称</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtCustName" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户大类</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                    <input id="txtCustBig" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户管理分类</td>
                <td align="left" bgcolor="#FFFFFF" style="width:26%">
                    <input id="txtCustTypeManage" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户营销分类</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtCustTypeSell" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户优质级别</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtCreditGrade" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户时间分类</td>
                <td align="left" bgcolor="#FFFFFF" style="width:26%">
                    <input id="txtCustTypeTime" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户细分</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtCustClass" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户类别</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                    <input id="txtCustType" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户简介</td>
                <td align="left" bgcolor="#FFFFFF" colspan="5">
                    <input id="txtCustNote" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" class="style1">联络期限</td>
                <td align="left" bgcolor="#FFFFFF" class="style2">
                    <input id="txtLinkCycle" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" class="style1">收货地址</td>
                <td align="left" bgcolor="#FFFFFF" class="style3">
                    <input id="txtReceiveAddress" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" class="style1">国家地区</td>
                <td align="left" bgcolor="#FFFFFF" class="style3">
                    <input id="txtCountryID" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">区域</td>
                <td align="left" bgcolor="#FFFFFF" style="width:26%">
                   <input id="txtAreaID" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">业务类型</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtBusiType" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">分管业务员</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                    <input id="txtManager" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">手机</td>
                <td align="left" bgcolor="#FFFFFF" style="width:26%">
                    <input id="txtMobile" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">传真</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtFax" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">电话</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                    <input id="txtTel" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">允许延期付款</td>
                <td align="left" bgcolor="#FFFFFF" style="width:26%">
                   <input id="txtCreditManage" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">结算方式</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtPayType" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">信用额度</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtMaxCredit" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">账期天数</td>
                <td align="left" bgcolor="#FFFFFF" style="width:26%">
                    <input id="txtMaxCreditDate" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">建档人</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                    <input id="txtCreator" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">建档日期</td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                   <input id="txtCreatDate" type="text" class="tdinput" style="width: 95%" disabled="disabled" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">可查看该档案的人员</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" ></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">预付款</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <input id="txtTotalPrice" type="text" class="tdinput" 
                        disabled="disabled" /></td>
              </tr>
              </table>




</div>
<!--客户档案列表js End-->

<!--客户联系人列表js Begin-->
<div id="div_lxr" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户联系人列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_lxr" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('LinkManName','oLinkManName');return false;">联系人姓名<span id="oLinkManName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('CustName','oCustName');return false;">对应客户<span id="oCustName" class="orderTip"></span></div></th>                
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('LinkTypeName','oLinkTypeName');return false;">联系人类型<span id="oLinkTypeName" class="orderTip"></span></div></th>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('Important','oImportant');return false;">重要程度<span id="oImportant" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('WorkTel','oWorkTel');return false;">工作电话<span id="oWorkTel" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('Handset','oHandset');return false;">手机号<span id="oHandset" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('Fax','oFax');return false;">传真<span id="oFax" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('HomeTel','oHomeTel');return false;">家庭电话<span id="oHomeTel" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('QQ','oQQ');return false;">QQ<span id="oQQ" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_lxr('Birthday','oBirthday');return false;">生日<span id="oBirthday" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecount_lxr"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_lxr" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_lxr">
              <input name="text" type="text" id="Text1" style="display:none" />
              <span id="pageDataList1_Total_lxr"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_lxr" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_lxr"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_lxr($('#ShowPageCount_lxr').val(),$('#ToPage_lxr').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_lxr"></a>
</div>
<!--客户联系人列表js End-->

<!--客户联络列表js Begin-->
<div id="div_ll" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户联络列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_ll" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ll('ContactNo','oContactNo');return false;">联络单编号<span id="oContactNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ll('Title','oTitle');return false;">联络主题<span id="oTitle" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ll('CustName','oCustName');return false;">对应客户<span id="oCustName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ll('EmployeeName','oEmployeeName');return false;">我方联络人<span id="oEmployeeName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ll('LinkDate','oLinkDate');return false;">联络时间<span id="oLinkDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ll('LinkManName','oLinkManName');return false;">客户联络人<span id="oLinkManName" class="orderTip"></span></div></th>
                        
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecount_ll"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_ll" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_ll">
              <input name="text" type="text" id="Text3" style="display:none" />
              <span id="pageDataList1_Total_ll"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_ll" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_ll"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_ll($('#ShowPageCount_ll').val(),$('#ToPage_ll').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_ll"></a>
</div>
<!--客户联络列表js End-->


<!--客户洽谈列表js Begin-->
<div id="div_qt" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户洽谈列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_qt" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_qt('TalkNo','oTalkNo');return false;">洽谈编号<span id="oTalkNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_qt('Title','oTitle_qt');return false;">洽谈主题<span id="oTitle_qt" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_qt('CustName','oCustName_qt');return false;">对应客户<span id="oCustName_qt" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_qt('LinkManName','oLinkManName');return false;">客户联系人<span id="oLinkManName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_qt('CompleteDate','oCompleteDate');return false;">完成期限<span id="oCompleteDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_qt('EmployeeName','oEmployeeName');return false;">执行人<span id="oEmployeeName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_qt('Status','oStatus');return false;">状态<span id="oStatus" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_qt"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_qt" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_qt">
              <input name="text" type="text" id="Text4" style="display:none" />
              <span id="Span21"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_qt" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_qt"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_qt($('#ShowPageCount_qt').val(),$('#ToPage_qt').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_qt"></a>
</div>
<!--客户洽谈列表js End-->

<!--客户关怀列表js Begin-->
<div id="div_gh" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户关怀列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_gh" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gh('LoveNo','oLoveNo');return false;">关怀编号<span id="oLoveNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gh('Title','oTitle_gh');return false;">关怀主题<span id="oTitle_gh" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gh('CustName','oCustName');return false;">对应客户<span id="oCustName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gh('LinkManName','oLinkManName_gh');return false;">客户联系人<span id="oLinkManName_gh" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gh('LoveDate','oLoveDate');return false;">关怀时间<span id="oLoveDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gh('EmployeeName','oEmployeeName');return false;">执行人<span id="oEmployeeName" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_gh"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_gh" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_gh">
              <input name="text" type="text" id="Text5" style="display:none" />
              <span id="Span28"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_gh" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_gh"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_gh($('#ShowPageCount_gh').val(),$('#ToPage_gh').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_gh"></a>
</div>
<!--客户关怀列表js End-->

<!--客户服务列表js Begin-->
<div id="div_fw" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户服务列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_fw" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fw('ServeNo','oServeNo');return false;">服务编号<span id="oServeNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fw('CustName','oCustName');return false;">服务主题<span id="oCustName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fw('CustName','oCustName');return false;">对应客户<span id="oCustName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fw('BeginDate','oBeginDate');return false;">服务时间<span id="oBeginDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fw('LinkManName','oLinkManName_fw');return false;">客户联络人<span id="oLinkManName_fw" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fw('EmployeeName','oEmployeeName_fw');return false;">执行人<span id="oEmployeeName_fw" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_fw"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_fw" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_fw">
              <input name="text" type="text" id="Text6" style="display:none" />
              <span id="Span36"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_fw" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_fw"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_fw($('#ShowPageCount_fw').val(),$('#ToPage_fw').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_fw"></a>
</div>
<!--客户服务列表js End-->

<!--客户投诉列表js Begin-->
<div id="div_ts" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户投诉列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_ts" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ts('ComplainNo','oComplainNo');return false;">投诉编号<span id="oComplainNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ts('Title','oTitle_ts');return false;">投诉主题<span id="oTitle_ts" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ts('CustName','oCustName_ts');return false;">对应客户<span id="oCustName_ts" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ts('ComplainDate','oComplainDate');return false;">投诉时间<span id="oComplainDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ts('Critical','oCritical');return false;">紧急程度<span id="oCritical" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ts('EmployeeName','oEmployeeName_ts');return false;">接待人<span id="oEmployeeName_ts" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ts('state','ostate_ts');return false;">处理状态<span id="ostate_ts" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_ts"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_ts" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_ts">
              <input name="text" type="text" id="Text7" style="display:none" />
              <span id="Span43"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_ts" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_ts"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_ts($('#ShowPageCount_ts').val(),$('#ToPage_ts').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_ts"></a>
</div>
<!--客户服务列表js End-->

<!--客户建议列表js Begin-->
<div id="div_jy" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户建议列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_jy" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jy('AdviceNo','oAdviceNo');return false;">建议编号<span id="oAdviceNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jy('Title','oTitle_jy');return false;">建议主题<span id="oTitle_jy" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jy('CustName','oCustName_jy');return false;">对应客户<span id="oCustName_jy" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jy('LinkManName','oLinkManName');return false;">客户联系人<span id="oLinkManName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jy('EmployeeName','oEmployeeName');return false;">接待人<span id="oEmployeeName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jy('Accept','oAccept');return false;">采纳程度<span id="oAccept" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jy('AdviceDate','oAdviceDate');return false;">建议时间<span id="oAdviceDate" class="orderTip"></span></div></th>
                        
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_jy"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_jy" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_jy">
              <input name="text" type="text" id="Text8" style="display:none" />
              <span id="Span52"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_jy" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_jy"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_jy($('#ShowPageCount_jy').val(),$('#ToPage_jy').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_jy"></a>
</div>
<!--客户建议列表js End-->

<!--客户来电记录列表js Begin-->
<div id="div_ld" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">客户来电记录列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_ld" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ld('CustName','oCustName');return false;">对应客户<span id="oCustName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ld('Tel','oTel');return false;">来电号码<span id="oTel" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ld('Title','oTitle');return false;">来电标题<span id="oTitle" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ld('CallTime','oCallTime');return false;">来电时间<span id="oCallTime" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ld('Callor','oCallor');return false;">来电人<span id="oCallor" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_ld('EmployeeName','oEmployeeName_ld');return false;">记录人<span id="oEmployeeName_ld" class="orderTip"></span></div></th>
                        
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_ld"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_ld" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_ld">
              <input name="text" type="text" id="Text9" style="display:none" />
              <span id="Span60"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_ld" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_ld"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_ld($('#ShowPageCount_ld').val(),$('#ToPage_ld').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_ld"></a>
</div>
<!--客户建议列表js End-->

<!--销售机会列表js Begin-->
<div id="div_jh" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">销售机会列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_jh" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jh('ChanceNo','oChanceNo');return false;">机会编号<span id="oChanceNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jh('Title','oTitle_jh');return false;">机会主题<span id="oTitle_jh" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jh('EmployeeName','oEmployeeName_jh');return false;">业务员<span id="oEmployeeName_jh" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jh('FindDate','oFindDate');return false;">发现时间<span id="oFindDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jh('PhaseName','oPhaseName');return false;">阶段<span id="oPhaseName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jh('StateName','oStateName');return false;">状态<span id="oStateName" class="orderTip"></span></div></th>
        <%--<th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" ><span id="Span5" class="orderTip"></span></div></th>--%>
                        
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_jh"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_jh" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_jh">
              <input name="text" type="text" id="Text13" style="display:none" />
              <span id="Span7"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_jh" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_jh"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_jh($('#ShowPageCount_jh').val(),$('#ToPage_jh').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_jh"></a>
</div>
<!--销售机会列表js End-->

<!--购买记录列表js Begin-->
<div id="div_gm" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">购买记录列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_gm" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gm('OrderDate','oOrderDate');return false;">日期<span id="oOrderDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gm('OrderNo','oOrderNo');return false;">订单编号<span id="oOrderNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_gm('RealTotal','oRealTotal');return false;">金额<span id="oRealTotal" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" ><span id="Span4" class="orderTip"></span></div></th>
                        
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_gm"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_gm" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_gm">
              <input name="text" type="text" id="Text2" style="display:none" />
              <span id="Span6"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_gm" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_gm"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_gm($('#ShowPageCount_gm').val(),$('#ToPage_gm').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_gm"></a>
</div>
<!--购买记录列表js End-->

<!--发货记录列表js Begin-->
<div id="div_fh" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">发货记录(发货单明细)</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_fh" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fh('SendDate','oSendDate');return false;">日期<span id="oSendDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fh('SendNo','oSendNo');return false;">发货单编号<span id="oSendNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fh('ProductName','oProductName');return false;">品名<span id="oProductName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fh('Specification','oSpecification');return false;">规格<span id="oSpecification" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fh('TypeName','oTypeName');return false;">颜色<span id="oTypeName" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_fh('ProductCount','oProductCount');return false;">数量<span id="oProductCount" class="orderTip"></span></div></th>
                        
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_fh"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_fh" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_fh">
              <input name="text" type="text" id="Text10" style="display:none" />
              <span id="Span11"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_fh" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_fh"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_fh($('#ShowPageCount_fh').val(),$('#ToPage_fh').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_fh"></a>
</div>
<!--发货记录列表js End-->

<!--回款计划列表js Begin-->
<div id="div_hk" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">回款计划列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_hk" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_hk('PlanGatherDate','oPlanGatherDate');return false;">计划日期<span id="oPlanGatherDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_hk('GatheringNo','oGatheringNo');return false;">计划编号<span id="oGatheringNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_hk('PlanPrice','oPlanPrice');return false;">计划回款金额<span id="oPlanPrice" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_hk('FactGatherDate','oFactGatherDate');return false;">实到日期<span id="oFactGatherDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_hk('FactPrice','oFactPrice');return false;">实到金额<span id="oFactPrice" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_hk('State','oState');return false;">状态<span id="oState" class="orderTip"></span></div></th>
                        
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_hk"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_hk" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_hk">
              <input name="text" type="text" id="Text11" style="display:none" />
              <span id="Span17"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_hk" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_hk"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_hk($('#ShowPageCount_hk').val(),$('#ToPage_hk').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_hk"></a>
</div>
<!--回款计划列表js End-->

<!--回款记录列表js Begin-->
<div id="div_jl" style="display:none;">
<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" />
    
    </td>
    <td align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">回款记录列表</td></tr>
  <tr>
    <td colspan="2">    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList_jl" bgcolor="#999999">
    <tbody>
      <tr>        
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jl('AcceDate','oAcceDate');return false;">日期<span id="oAcceDate" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jl('TotalPrice','oTotalPrice');return false;">金额<span id="oTotalPrice" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jl('AcceWay','oAcceWay');return false;">付款方式<span id="oAcceWay" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jl('InvoiceType','oInvoiceType');return false;">票据类型<span id="oInvoiceType" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jl('BillingNum','oBillingNum');return false;">票号<span id="oBillingNum" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy_jl('CreateDate','oCreateDate');return false;">开票日期<span id="oCreateDate" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>    
      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%" ><div id="pagecount_jl"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager_jl" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage_jl">
              <input name="text" type="text" id="Text12" style="display:none" />
              <span id="Span23"></span>每页显示
              <input name="text" type="text" id="ShowPageCount_jl" maxlength="4"  style="width:30px;"/>
              条  转到第
              <input name="text" type="text" id="ToPage_jl"  style="width:30px;"/>
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go"  align="absmiddle" onclick="ChangePageCountIndex_jl($('#ShowPageCount_jl').val(),$('#ToPage_jl').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
<a name="pageDataList1Mark_jl"></a>
</div>
<!--回款记录列表js End-->


</td>
</tr>
</table>
<!--9个列表主体 End-->

</td>
</tr>
</table>
    
    
    
<uc2:message ID="Message1" runat="server" />
<span id="Forms" class="Spantype"></span>

</form>
    <input id="hidCustID_Tree" type="hidden" />
    <input id="hidCustNo_Tree" type="hidden" />
</body>
</html>
