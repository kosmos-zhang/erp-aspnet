<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryWorkByPiece.aspx.cs" Inherits="Pages_Office_HumanManager_SalaryWorkByPiece" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>计件工资分析报表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <style type="text/css">
        #tblMain
        {
            margin-top:0px;
            margin-left:0px;
		    background-color:#F0f0f0;
      	    font-family:"tahoma";
      	    color:#333333;
      	    font-size:12px; 
        }
        .errorMsg
        {
	        filter:progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
	        position:absolute;
	        top:240px;
	        left:450px;
	        border-width:1pt;
	        border-color:#666666;
	        border-style:solid;
	        width:290px;
	        display:none;
	        margin-top:10px;
	        z-index:21;
        }
    </style>
</head>
<body>
<form id="frmMain" runat="server">
<span id="spanMsg" class="errorMsg"></span>        
<a name="DetailListMark"></a>
<span id="Forms" class="Spantype" name="Forms"></span>
    <table width="100%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="tblMain">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                                <tr>
                                    <td width="10%" height="20" class="tdColTitle">部门</td>
                                    <td width="23%" class="tdColInput">
                                        <asp:DropDownList ID="ddlDeptName" runat="server">
                                           </asp:DropDownList>
                                    </td>
                                    <td width="10%" class="tdColTitle">起始月份</td>
                                    <td width="23%" class="tdColInput">
                               
                                                <asp:DropDownList ID="ddlStartMonth" runat="server">
                                                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1月" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="2月" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="3月" Value="03"></asp:ListItem>
                                                 <asp:ListItem Text="4月" Value="04"></asp:ListItem>
                                                 <asp:ListItem Text="5月" Value="05"></asp:ListItem>
                                                 <asp:ListItem Text="6月" Value="06"></asp:ListItem>
                                                 <asp:ListItem Text="7月" Value="07"></asp:ListItem>
                                                  <asp:ListItem Text="8月" Value="08"></asp:ListItem>
                                                   <asp:ListItem Text="9月" Value="09"></asp:ListItem>
                                                   <asp:ListItem Text="10月" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11月" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12月" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%" class="tdColTitle">结束月份</td>
                                    <td width="24%" class="tdColInput">
                                              <asp:DropDownList ID="ddlEndMonth" runat="server">
                                                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1月" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="2月" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="3月" Value="03"></asp:ListItem>
                                                 <asp:ListItem Text="4月" Value="04"></asp:ListItem>
                                                 <asp:ListItem Text="5月" Value="05"></asp:ListItem>
                                                 <asp:ListItem Text="6月" Value="06"></asp:ListItem>
                                                 <asp:ListItem Text="7月" Value="07"></asp:ListItem>
                                                  <asp:ListItem Text="8月" Value="08"></asp:ListItem>
                                                   <asp:ListItem Text="9月" Value="09"></asp:ListItem>
                                                   <asp:ListItem Text="10月" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11月" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12月" Value="12"></asp:ListItem>
                                                    </asp:DropDownList>
                                    </td>
                                </tr>  
                                 <tr>
                                   
                                    <td class="tdColTitle" width="10%">
                                        被考核人</td>
                                    <td class="tdColInput" width="23%">
                                             <asp:TextBox ID="UserApplyUserName" MaxLength="50" onclick="alertdiv('UserApplyUserName,txtSearchEmployee');"
                                runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtSearchEmployee" runat="server" />
                                    </td>
                                      <td class="tdColTitle" width="10%">年度</td> <td class="tdColInput" width="23%">         
                                          <asp:DropDownList ID="ddlYear" runat="server">
                                             
                                                    </asp:DropDownList></td>
                                        <td class="tdColTitle" width="10%"></td> <td class="tdColInput" width="23%"></td>
                                    
                                </tr>                   
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" />
                                        
                                        <asp:ImageButton ID="imgSearch" runat="server"  
                                            ImageUrl="../../../images/Button/Bottom_btn_search.jpg" 
                                            onclick="imgSearch_Click" Height="23px" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">

                <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">计件工资分析报表</td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" PrintMode="ActiveX"  HasCrystalLogo="False" />
                            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            </CR:CrystalReportSource>
                            
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>
    
    
    
</form>
</body>
</html>
