<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company_Edit.aspx.cs" Inherits="Pages_Office_SystemManager_Company_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业信息修改</title>
     <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #SuperCompany
        {
            width: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="popupContent"></div>
    <div class="divbox" style="width:700px;">
    <div class="divboxtitle"><span> 企业信息管理_修改</span><div class="clearbox"></div></div>
     <div class="divbox" style="width:700px;">
     
       <div id="BtnArea">
           <span style="font-size: 14px; font-weight: bold">修改企业信息</span> </div>
    <div class="divboxtitle"><span><asp:ImageButton ID="btnModify" runat="server"  OnClientClick="DoCheck();" ImageUrl="~/Images/Button/Button_confirm.jpg" />
                </span><div class="clearbox"></div></div>
    <div class="divboxbody">
    <div class="divboxbodyleft">
             <table width="100%"  border="1" cellspacing="0" cellpadding="0" style="border:1pt solid #CCCCCC;">
                              <tr align="right">
                                <td align=right style="background-color:#D9D9D9">公司代码</td>
                                <td align="left" class="style1">
                                    <asp:TextBox ID="txtCompanyCD"  MaxLength="10" runat="server"  
                                        CssClass="input3" Width="150px" Enabled="False"></asp:TextBox>
                                    </td>
                                <td align=right style="background-color:#D9D9D9">公司编号&nbsp;</td>
                                <td align="left" class="style2">
                                    <asp:TextBox ID="txtCompanyNo"  MaxLength="50" runat="server"  
                                        CssClass="input3" Width="150px" ReadOnly="True" Enabled="False"></asp:TextBox> </td>
                                <td align=right style="background-color:#D9D9D9">公司名称</td>
                                <td align="left" height="26"> <font color=red>
                                    <asp:TextBox ID="txtCompanyName" runat="server" Width="150px"></asp:TextBox>
                                    </font></td>
                              </tr>
                              <tr align="right">
                                <td style="background-color:#D9D9D9">上级公司</td>
                                <td align="left" class="style1"> <font color=red>
    <select id="SuperCompany" name="SetPro0" runat="server" onchange="javascript:GetCity();" >
        <option>
        </option>
    </select></font></td>
                                <td width="11%" style="background-color:#D9D9D9">启用状态</td>
                                <td align="left" style="margin-left: 40px" class="style2">
                    <asp:CheckBox ID="chkLockFlag0" runat="server" Text="启用"/>
                                        </td>
                               <td width="11%" style="background-color:#D9D9D9">描述</td>
                           <td align="left" height="26"> <font color=red>
                                    <asp:TextBox ID="txtDescription" runat="server" Width="150px"></asp:TextBox>
                                    </font></td>
                              </tr>
                              </table>
            
          
            <div>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
    </div>
    </div>
</div>
    </form>
</body>
</html>
