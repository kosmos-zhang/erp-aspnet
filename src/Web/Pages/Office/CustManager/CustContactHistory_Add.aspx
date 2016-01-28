<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustContactHistory_Add.aspx.cs" Inherits="Pages_Office_CustManager_CustContactHistory_Add" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc1" %>

<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRuleControl" tagprefix="uc3" %>

<%@ Register src="../../../UserControl/CustLinkManSel.ascx" tagname="CustLinkManSel" tagprefix="uc4" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加客户联络信息</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/CustContactHistoryAdd.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/LinkManSele.js" type="text/javascript"></script>
    <script src="../../../js/common/EmployeeSel.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    
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
                            新建客户联络
                        </td>
                        <td height="30" align="center" class="Title" id="TitleModify" style="display:none">
                            客户联络
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">                           
                            <table width="100%">
                                <tr>
                                    <td>
                                       <img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" visible="false" runat="server" id="btn_save" style="cursor:hand"  onclick="AddContactHistoryData();" />
        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" height="25" onclick="Back();" />
                                                
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
                </table><asp:HiddenField ID="hfCustContactID" runat="server" />
                
                
            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01" >
              <tr>
              <td align="right" bgcolor="#E6E6E6">联络单编号<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" >
                <div id="divddlContactNo" runat="server"><uc3:CodingRuleControl ID="ddlContactNo" runat="server" /></div>
                    <div id="divContacytNo" runat="server" style="color: #ACA899"></div>
                  </td>
                <td align="right" bgcolor="#E6E6E6"  >联络主题<span class="redbold"> *</span></td>
                <td bgcolor="#FFFFFF"  colspan="3">
                    <input name="txtTitle" id="txtTitle" SpecialWorkCheck="联络主题" type="text" class="tdinput" size="60" 
                        maxlength="50" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户名称<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" style="width:25%">
                    <uc1:CustNameSel ID="CustNameSel1" runat="server" />
                    
                            </td>
                <td align="right" bgcolor="#E6E6E6"  style="width:9%">我方联络人<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" style="width:15%" ><input name="UserEmplNameL" id="UserEmplNameL" type="text"  runat="server"
                        class="tdinput" maxlength="50" onclick="alertdiv('UserEmplNameL,hfEmployeeID');" />
                        <input type="hidden" runat="server" id="hfEmployeeID" />
                           
                            </td>
                <td align="right" bgcolor="#E6E6E6" style="width:10%">客户联络人</td>
                <td align="left" bgcolor="#FFFFFF" style="width:24%" >  <uc4:CustLinkManSel ID="CustLinkManSel1" runat="server" /></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#E6E6E6">联络事由<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF">
                    <asp:DropDownList ID="ddlLinkReasonID" runat="server">
                    </asp:DropDownList>
                    
                            </td>
                <td align="right" bgcolor="#E6E6E6">联络方式<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" >

                         <select width="20px" id="ddlLinkMode" name="D1">                                    
                                  <option value="1">电话</option>
                                  <option value="2">传真</option>
                                  <option value="3">邮件</option>
                                   <option value="4">远程在线</option>
                                   <option value="5">会晤拜访</option>
                                   <option value="6">综合</option>
                                </select>
                  
                            </td>
                <td align="right" bgcolor="#E6E6E6" >联络时间<span class="redbold"> *</span></td>
                <td align="left" bgcolor="#FFFFFF" >
                <input id="txtLinkDate" type="text" class="tdinput" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtLinkDate')})"  />
                        
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">联络内容</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtContents" id="txtContents" rows="5" cols="80"></textarea></td>
              </tr>
              <tr>
                <td height="22" align="right" bgcolor="#E6E6E6">可查看该联络信息的人员</td>
                <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
              </tr>
              </table>
          
          <br />
              
            </td>
        </tr>
    </table>    
     <input type="hidden" runat="server" id="txtRecorder" />
                        <input type="hidden" runat="server" id="txtChairman" />
                        <input type="hidden" runat="server" id="txtSender" /><uc2:Message ID="Message1" runat="server" />
    <span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
