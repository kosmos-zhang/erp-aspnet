<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustTalk_Add.aspx.cs" Inherits="Pages_Office_CustManager_CustTalk_Add" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRuleControl" tagprefix="uc3" %>

<%@ Register src="../../../UserControl/CustLinkManSel.ascx" tagname="CustLinkManSel" tagprefix="uc2" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建客户洽谈</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>       
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>    
    <script src="../../../js/office/CustManager/LinkManSele.js" type="text/javascript"></script>
    <script src="../../../js/common/EmployeeSel.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/CustTalkAdd.js" type="text/javascript"></script>
    <style type="text/css">


	.OfficeThingsListCss
    {
	    position:absolute;top:250px;left:250px;
	    border-width:1pt;border-color:#EEEEEE;border-style:solid;
	    width:800px;
	    display:none;
	    height:220px;
	    z-index:1;
	}
.jPagerBar{FONT-SIZE: 12px;FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;}

    </style>
</head>
<body>
    <form id="form1" runat="server">
                        <input type="hidden" runat="server" id="txtRecorder" />
                        <input type="hidden" runat="server" id="txtChairman" />
                        <input type="hidden" runat="server" id="txtSender" />
    <span id="Forms" class="Spantype"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                <uc4:Message ID="Message1" runat="server" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        
                        <td height="30" align="center" class="Title" id="TitleAdd">
                            新建客户洽谈
                        </td>
                        <td height="30" align="center" class="Title" id="TitleModify" style="display:none">
                            客户洽谈
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">                           
                            <table width="100%">
                                <tr>
                                    <td>
                                       <img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save" style="cursor:hand"  onclick="AddCustTalk();" visible="false" runat="server" />
        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" onclick="Back();" />
                                                
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer" onclick="PagePrint();"
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
                <asp:HiddenField ID="hfCustTalkID" runat="server" />
                
            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01" >
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">洽谈编号<span class="redbold"> *</span></td>
                <td height="22" bgcolor="#FFFFFF" style="width:23%">
                    <div id="divddlTalkNo" runat="server">
                        <uc3:CodingRuleControl ID="ddlTalkNo" runat="server" />
                    </div>
                    <div id="divTalkNo" runat="server" style="color: #ACA899">
                    </div>
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">洽谈主题<span class="redbold"> *</span></td>
                <td height="22" bgcolor="#FFFFFF" colspan="3">
                    
                
                    <input name="txtTitle" id="txtTitle" type="text" class="tdinput" size="60"  SpecialWorkCheck="洽谈主题"
                        maxlength="50" /></td>
              </tr>
              
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户名称
                   <span class="redbold"> *</span></td>
                <td height="22" bgcolor="#FFFFFF" style="width:26%">
                    
                
                    <uc1:CustNameSel ID="CustNameSel1" runat="server" />
                
                            </td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">客户联系人<span class="redbold"> *</span></td>
                <td height="22" bgcolor="#FFFFFF" style="width:22%">
                    
                
                     <uc2:CustLinkManSel ID="CustLinkManSel1" runat="server" />
                                                </td>
                <td height="22" align="right" bgcolor="#E6E6E6" style="width:10%">执行人<span class="redbold"> *</span></td>
                <td height="22" bgcolor="#FFFFFF" style="width:22%">
                    <input id="UserLinker" type="text"  runat="server" readonly class="tdinput" size="30" maxlength="50" onclick="alertdiv('UserLinker,txtJoinUser');" />
                        <input type="hidden" runat="server" id="txtJoinUser" />
                  </td>
              </tr>
              
              <tr>
                <td align="right" bgcolor="#E6E6E6">优先级</td>
                <td align="left" bgcolor="#FFFFFF">
                    <select  name="selePriority" width="20px" id="selePriority">
                                  <option value="0">--请选择--</option>
                                  <option value="1">暂缓</option>
                                  <option value="2">普通</option>
                                  <option value="3">尽快</option>
                                  <option value="4">立即</option>
                                </select></td>
                <td align="right" bgcolor="#E6E6E6">洽谈方式</td>
                <td align="left" bgcolor="#FFFFFF" >                    
                    <select width="20px" id="ddlTalkType">
                                    <option value="0">--请选择--</option>
                                  <option value="1">电话</option>
                                  <option value="2">传真</option>
                                  <option value="3">邮件</option>
                                   <option value="4">远程在线</option>
                                   <option value="5">会晤拜访</option>
                                   <option value="6">综合</option>
                                </select>
                            </td>
                <td align="right" bgcolor="#E6E6E6" >状态<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" >
                    <select  name="seleStatus" width="20px" id="seleStatus">                                 
                                  <option value="1">未开始</option>
                                  <option value="2">进行中</option>
                                  <option value="3">已完成</option>                                  
                                </select></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6">完成期限<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF">
                    <input name="txtCompleteDate" id="txtCompleteDate" type="text" class="tdinput" style="width:80px;" readonly="readonly"
                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtLinkDate')})"
                        maxlength="10" /> <select  id="SeleHour" >
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
                         </td></td>
                <td align="right" bgcolor="#E6E6E6">建档人</td>
                <td align="left" bgcolor="#FFFFFF" >
                    <input name="txtCreator" id="txtCreator" type="text"  runat="server"
                        class="tdinput" size="15" maxlength="50" disabled="disabled" /><asp:HiddenField  
                        ID="hfCreator" runat="server" />
                            </td>
                <td align="right" bgcolor="#E6E6E6" >建档日期<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" >
                    <input name="txtCreatedDate" id="txtCreatedDate" runat="server" type="text" disabled="disabled" class="tdinput"  /></td>
              </tr>
              
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">行动描述</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtContents" id="txtContents" rows="4" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">客户反馈</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtFeedback" id="txtFeedback" rows="4" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">效果评估</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtResult" id="txtResult" rows="4" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">备注</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtRemark" id="txtRemark" rows="4" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">可查看该洽谈信息的人员</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
              </tr>
              </table>
           <input name="txtModifiedDate" id="txtModifiedDate" runat="server" type="hidden" class="tdinput" disabled="disabled" />
           <input name="txtModifiedUserID" id="txtModifiedUserID" type="hidden"  runat="server" class="tdinput" disabled="disabled" />
          <br />
              
            </td>
        </tr>
    </table>    
    </form>
</body>
</html>
