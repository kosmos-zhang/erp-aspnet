<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyOpenServ_Query.aspx.cs" Inherits="Pages_SystemManager_CompanyOpenServ_Query" %>
<%--<%@ Register src="~/UserControl/PageControl.ascx" tagname="PageList" tagprefix="ucPaged" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公司业务开通</title>
    <link href="../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../js/SystemManager/CompanyOpenServQuery.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMain" runat="server" onsubmit="return submitFlag;">
    <div id="popupContent"></div>
    <div class="divbox">
	    <div class="divboxtitle"><span>检索条件</span><div class="clearbox"></div></div>
        <div class="divboxbody">
    	    <div class="divboxbodyleft">	
                <ul>
                    <li id="ThreeColumnName">公司代码</li>
                    <li id="ThreeColumnInput"><asp:TextBox ID="txtCompanyCD"  MaxLength="10" runat="server"></asp:TextBox></li>
                    <li id="ThreeColumnName">生效日期</li>
                    <li id="ThreeColumnInput"><asp:TextBox ID="txtOpenDate"  MaxLength="10" runat="server"></asp:TextBox>&nbsp;<img onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})" src="../../js/Calendar/skin/datePicker.gif" width="15px" height="22" align="absmiddle"></li>
                    <li id="ThreeColumnName">失效日期</li>
                    <li id="ThreeColumnInput"><asp:TextBox ID="txtCloseDate"  MaxLength="10" runat="server"></asp:TextBox>&nbsp;<img onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})" src="../../js/Calendar/skin/datePicker.gif" width="15px" height="22" align="absmiddle"></li>
                </ul>
                <asp:ImageButton ID="btnSearch" runat="server" Text="查询" OnClientClick="DoSearch();" onclick="btnSearch_Click" ImageUrl="~/Images/Button/Button_Check.jpg"/>
            </div>
            <br /><br />
        </div>
    </div>
    <br />
    <div class="divbox">
	    <div class="divboxtitle"><span>客户列表：</span><div class="clearbox"></div></div>
        <div class="divboxbody">
    	    <div class="divboxbodyhack">
                <%--<ucPaged:PageList ID="ucCompanyInfo" runat="server"  />--%>
            </div>
        </div>
	    <div class="divboxbodyright">
            <asp:ImageButton ID="btnAdd" runat="server" OnClientClick="DoModify('0');" ImageUrl="~/Images/Button/Button_Add.jpg" />
            &nbsp;
            <asp:ImageButton ID="btnUpdate" runat="server" OnClientClick="DoModify('1');" ImageUrl="~/Images/Button/Button_Edit.jpg" />
            &nbsp;
            <asp:ImageButton ID="btnDelete" runat="server" onclick="btnDelete_Click"  OnClientClick="DoDelete();" ImageUrl="~/Images/Button/Button_Delete.jpg" />
        </div>
	    <div class="divboxbodyhack">
            <asp:Label ID="lblMessage" runat="server" CssClass="redbold"></asp:Label>            
            <asp:HiddenField ID="hidDelete" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
