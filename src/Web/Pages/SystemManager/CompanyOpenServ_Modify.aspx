<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyOpenServ_Modify.aspx.cs" Inherits="Pages_SystemManager_CompanyOpenServ_Modify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户公司信息修改</title>
    <link href="../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../js/SystemManager/CompanyOpenServModify.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMain" runat="server" onsubmit="return submitFlag;">
    <div id="popupContent"></div>
    <div class="divbox" style="width:700px;">
    <div class="divboxtitle"><span>客户业务信息修改</span><div class="clearbox"></div></div>
    <div class="divboxbody">
    <div class="divboxbodyleft">
            <ul>
                <li id="OneColumnName">客户代码</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtCompanyCD"  MaxLength="8" runat="server" Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="CompanyCDTip" runat="server"></div>
                </li>
             </ul>
             <ul>
                <li id="OneColumnName">角色数上限</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtMaxRoles"  MaxLength="2" runat="server"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="MaxRolesTip" runat="server"></div>
                </li>
             </ul>
             <ul id="ulPassword" runat="server">
                <li id="OneColumnName">用户数上限</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtMaxUser"  MaxLength="4" runat="server"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="MaxUserTip" runat="server"></div>
                </li>
             </ul>
             <ul id="ulRePassword" runat="server">
                <li id="OneColumnName">文件大小总限</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtMaxDocSize"  MaxLength="10" runat="server"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="MaxDocSizeTip" runat="server"></div>
                </li>
             </ul>
             <ul>
                <li id="OneColumnName">文件大小上限</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtSingleDocSize"  MaxLength="50" runat="server"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="SingleDocSizeTip" runat="server"></div>
                </li>
              </ul>
             <ul>
                <li id="OneColumnName">文件个数上限</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtMaxDocNum"  MaxLength="50" runat="server"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="MaxDocNumTip" runat="server"></div>
                </li>
              </ul>
              <ul>
               <li id="OneColumnName">生效日期</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtOpenDate" MaxLength="10" runat="server"  Width="115px"></asp:TextBox>
                    <img onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})" src="../../js/Calendar/skin/datePicker.gif" width="15px" height="22" align="absmiddle">
                </li>
                <li id="CommonMessage">
                    <div id="OpenDateTip" runat="server"></div>
                </li>
             </ul>
             <ul>
                <li id="OneColumnName">失效日期</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtCloseDate" MaxLength="10" runat="server"  Width="115px"></asp:TextBox>
                    <img onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCloseDate')})" src="../../js/Calendar/skin/datePicker.gif" width="15px" height="22" align="absmiddle">
                </li>
                <li id="CommonMessage">
                    <div id="CloseDateTip" runat="server"></div>
                </li>
            </ul>    
            <ul>
                <li id="OneColumnName">备注</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtRemark" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="RemarkTip" runat="server"></div>
                </li>
            </ul>
            <div id="BtnArea">
                <asp:ImageButton ID="btnModify" runat="server" OnClick="btnModify_Click" OnClientClick="DoCheck();" ImageUrl="~/Images/Button/Button_confirm.jpg" />
                <asp:ImageButton ID="btnBack" runat="server" OnClientClick="DoBack();" ImageUrl="~/Images/button/button_Back.jpg"/>
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
    </div>
    </div>
    </div>
    </form>
</body>
</html>
