<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustComplain_Add.aspx.cs" Inherits="Pages_Office_CustManager_CustComplain_Add" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRuleControl" tagprefix="uc2" %>

<%@ Register src="../../../UserControl/CustLinkManSel.ascx" tagname="CustLinkManSel" tagprefix="uc3" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>新建客户投诉</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/LinkManSele.js" type="text/javascript"></script>    
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/CustComplainAdd.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
   
</head>
<body>
      <form id="form1" runat="server">
                       
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title" id="TitleAdd">
                            新建客户投诉
                        </td>
                        <td height="30" align="center" class="Title" id="TitleModify" style="display:none">
                            客户投诉
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">                           
                            <table width="100%">
                                <tr>
                                    <td>
                                       <img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save" visible="false" runat="server" style="cursor:hand"  onclick="AddCustComplain();" />
        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" onclick="Back();" />
                                                
                                        </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"  onclick="PagePrint();"
                                            title="打印" />
                                    </td>
                                </tr>
                            </table>                          
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfComplainID" runat="server" />
               
            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01" >
              <tr>
              
              <td height="22" align="right" bgcolor="#E6E6E6">投诉单编号<span class="redbold"> *</span></td>
                <td height="22" align="left" bgcolor="#FFFFFF">
                <div id="divddlComplainNo" runat="server">
                    <uc2:CodingRuleControl ID="ddlComplainNo" runat="server" />
                    </div>
                    <div id="divComplainNo" runat="server" style="color: #ACA899"></div>
                  </td>

                <td height="22" align="right" bgcolor="#E6E6E6">投诉主题<span class="redbold"> *</span></td>
                <td height="22" bgcolor="#FFFFFF" colspan="3">
                    <input name="txtTitle" id="txtTitle" type="text" class="tdinput" style="width:95%" SpecialWorkCheck="主题"
                        maxlength="50" /></td>
                
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6"  style="width:10%">客户名称<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" style="width:26%">
                    <uc1:CustNameSel ID="CustNameSel1" runat="server" />
                            </td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户联系人<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                    <uc3:CustLinkManSel ID="CustLinkManSel1" runat="server" />
                            </td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">接待人<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" style="width:22%">
                    <input name="txtDestClerk" id="UserDestClerk" type="text" class="tdinput" onclick="alertdiv('UserDestClerk,hfDestClerkID')" maxlength="20" />
                    <asp:HiddenField ID="hfDestClerkID" runat="server" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6">被投诉人</td>
                <td align="left" bgcolor="#FFFFFF">
                    <input id="UserComplainedMan" type="text" class="tdinput" onclick="alertdiv('UserComplainedMan,hfComplainedManID')" maxlength="20" />
                    <input id="hfComplainedManID" type="hidden" /></td>
                <td align="right" bgcolor="#E6E6E6">投诉分类<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" >
                    <asp:DropDownList ID="ddlComplainType" runat="server">
                    </asp:DropDownList>
                            </td>
                <td align="right" bgcolor="#E6E6E6" >紧急程度<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" >
                    <select  name="seleCritical" id="seleCritical">
                                  <option value="1">宽松</option>
                                  <option value="2">一般</option>
                                  <option value="3">较急</option>
                                  <option value="4">紧急</option>
                                  <option value="5">特急</option>
                                </select></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6">处理状态<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF">
                    <select  name="seleState" id="seleState">
                                  <option value="1">处理中</option>
                                  <option value="2">未处理</option>
                                  <option value="3">已处理</option>                                  
                                </select>
                            </td>
                <td align="right" bgcolor="#E6E6E6">花费时间</td>
                <td align="left" bgcolor="#FFFFFF" >
                    <input name="txtSpendTime" id="txtSpendTime" type="text" class="tdinput" onkeydown="FractionDigits(this)"
                        maxlength="7" value="0.00" /></td>
                <td align="right" bgcolor="#E6E6E6" >花费时间单位<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" ><select  name="seleDateUnit" id="seleDateUnit">
                                  <option value="1">小时</option>
                                  <option value="2">天</option>
                                  <option value="3">月</option>
                                </select></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6">投诉时间<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF">
                    <input name="txtComplainDate" id="txtComplainDate" type="text" class="tdinput" readonly="readonly" style="width:80px;" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtLinkDate')})"
                        maxlength="19" />
                        <select  id="SeleHour" >
                                <option value='00:'>00</option>
                                <option value='01:'>01</option>
                                <option value='02:'>02</option>
                                <option value='03:'>03</option>
                                <option value='04:'>04</option>
                                <option value='05:'>05</option>
                                <option value='06:'>06</option>
                                <option value='07:'>07</option>
                                <option value='08:'>08</option>
                                <option value='09:'>09</option>
                                <option value='10:'>10</option>
                                <option value='11:'>11</option>
                                <option value='12:'>12</option>
                                <option value='13:'>13</option>
                                <option value='14:'>14</option>
                                <option value='15:' selected>15</option>
                                <option value='16:'>16</option>
                                <option value='17:'>17</option>
                                <option value='18:'>18</option>
                                <option value='19:'>19</option>
                                <option value='20:'>20</option>
                                <option value='21:'>21</option>
                                <option value='22:'>22</option>
                                <option value='23:'>23</option>
                                </select>时
                                <select  id="SeleMinute" >
                                <option value='00'>00</option>
                                <option value='01'>01</option>
                                <option value='02'>02</option>
                                <option value='03'>03</option>
                                <option value='04'>04</option>
                                <option value='05'>05</option>
                                <option value='06'>06</option>
                                <option value='07'>07</option>
                                <option value='08'>08</option>
                                <option value='09'>09</option>
                                <option value='10'>10</option>
                                <option value='11'>11</option>
                                <option value='12'>12</option>
                                <option value='13'>13</option>
                                <option value='14'>14</option>
                                <option value='15'>15</option>
                                <option value='16'>16</option>
                                <option value='17'>17</option>
                                <option value='18'>18</option>
                                <option value='19'>19</option>
                                <option value='20'>20</option>
                                <option value='21'>21</option>
                                <option value='22'>22</option>
                                <option value='23'>23</option>
                                <option value='24'>24</option>
                                <option value='25'>25</option>
                                <option value='26'>26</option>
                                <option value='27'>27</option>
                                <option value='28'>28</option>
                                <option value='29'>29</option>
                                <option value='30'>30</option>
                                <option value='31'>31</option>
                                <option value='32'>32</option>
                                <option value='33'>33</option>
                                <option value='34'>34</option>
                                <option value='35'>35</option>
                                <option value='36'>36</option>
                                <option value='37'>37</option>
                                <option value='38'>38</option>
                                <option value='39'>39</option>
                                <option value='40'>40</option>
                                <option value='41'>41</option>
                                <option value='42'>42</option>
                                <option value='43'>43</option>
                                <option value='44'>44</option>
                                <option value='45'>45</option>
                                <option value='46'>46</option>
                                <option value='47'>47</option>
                                <option value='48'>48</option>
                                <option value='49'>49</option>
                                <option value='50'>50</option>
                                <option value='51'>51</option>
                                <option value='52'>52</option>
                                <option value='53'>53</option>
                                <option value='54'>54</option>
                                <option value='55'>55</option>
                                <option value='56'>56</option>
                                <option value='57'>57</option>
                                <option value='58'>58</option>
                                <option value='59'>59</option>
                                </select>分
                                </td>
                <td align="right" bgcolor="#E6E6E6">客户联系电话</td>
                <td align="left" bgcolor="#FFFFFF" >
                    <input name="txtCustLinkTel" id="txtCustLinkTel" type="text" class="tdinput" size="15" 
                        maxlength="25" /></td>
                <td align="right" bgcolor="#E6E6E6" ></td>
                <td align="left" bgcolor="#FFFFFF" ></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">投诉内容</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtContents" id="txtContents" rows="4" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">处理过程</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtDisposalProcess" id="txtDisposalProcess" rows="4" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">客户反馈</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtFeedback" id="txtFeedback" rows="4" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">备注</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtRemark" id="txtRemark" rows="4" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">可查看该投诉信息的人员</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
              </tr>
              </table>
          <input id="txtModifiedUserID" type="hidden"  runat="server" class="tdinput" disabled="disabled" />
          <input id="txtModifiedDate" runat="server" type="hidden" class="tdinput" disabled="disabled" />
          <br />
              
            </td>
        </tr>
    </table>    
     <input type="hidden" runat="server" id="txtRecorder" />
                        <input type="hidden" runat="server" id="txtChairman" />
                        <input type="hidden" runat="server" id="txtSender" /><uc4:Message ID="Message1" runat="server" />
    <span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
