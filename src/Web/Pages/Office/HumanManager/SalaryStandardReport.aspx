<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryStandardReport.aspx.cs" Inherits="Pages_Office_HumanManager_SalaryStandardReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工资标准表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
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
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                                <tr>
                                    <td width="10%" height="20" class="tdColTitle">岗位</td>
                                    <td width="23%" class="tdColInput">
                                        <asp:DropDownList ID="ddlSearchQuarter" runat="server"></asp:DropDownList>
                                    </td>
                                    <td width="10%" class="tdColTitle">岗位职等</td>
                                    <td width="23%" class="tdColInput">
                               
                                                <asp:DropDownList ID="ddlSearchQuaterAdmin" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%" class="tdColTitle"></td>
                                    <td width="24%" class="tdColInput">
                                        
                                    </td>
                                </tr>                 
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" />
                                        <asp:ImageButton ID="imgSearch" runat="server"       ImageUrl="../../../images/Button/Bottom_btn_search.jpg"    onclick="imgSearch_Click"  Visible="false" />   
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr><td colspan="2" height="5"></td></tr>
        <tr>
            <td colspan="2">

                <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr><td colspan="2" height="2"></td></tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">工资标准表</td>
                    </tr>
                    <%--<tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand" height="25" onclick="DoNew();"/>
                                        <img src="../../../Images/Button/Bottom_btn_edit.jpg" alt="修改" visible="false" id="btnModify" runat="server" style="cursor:hand" height="25" onclick="DoModify();"/>
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;' width="51" height="25" />
                                        <img src="../../../images/Button/Main_btn_out.jpg" alt="导出" visible="false" id="btnExport" runat="server" onclick="DoExport()" style='cursor:pointer;' width="51" height="25" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2" align="center">
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  PrintMode="ActiveX"  HasCrystalLogo="False"/>
                            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            </CR:CrystalReportSource>
                            
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>
    
    <div id="divEditSalary" runat="server" style="background: #fff; padding: 10px; width: 800px; z-index:1; position: absolute;top: 20%; left: 15%;display:none;  ">    
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblSalaryInfo">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="30" class="tdColInput">
                                <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" height="25" onclick="DoSave();"/>
                                <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="false" id="btnBack" runat="server" style="cursor:hand" height="25" onclick="DoBack();"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- <div style="height:500px;overflow-y:scroll;"> -->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
                        <tr>
                            <td  colspan="2">
                                <table>
                                    <tr>
                                        <td colspan="2" height="4"><input type="hidden" id="hidID" /></td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                                    <tr>
                                        <td height="25" class="tdColTitle" width="10%">岗位<span class="redbold">*</span></td>
                                        <td height="25" class="tdColInput" width="23%">
                                            <div id="divCodeRule" runat="server">
                                                <asp:DropDownList ID="ddlCodeType" runat="server"></asp:DropDownList>
                                            </div>
                                        </td>
                                        <td class="tdColTitle" width="10%">岗位职等<span class="redbold">*</span></td>
                                        <td class="tdColInput" width="23%">
                                         
                                             <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                                        </td>
                                        <td class="tdColTitle" width="10%">工资项<span class="redbold">*</span></td>
                                        <td class="tdColInput" width="24%">
                                            <asp:DropDownList ID="ddlSalaryItem" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdColTitle">金额<span class="redbold">*</span></td>
                                        <td class="tdColInput">
                                            <asp:TextBox ID="txtUnitPrice" runat="server" Width="95%" MaxLength="10" CssClass="tdinput"></asp:TextBox>
                                        </td>
                                        <td class="tdColTitle">启用状态</td>
                                        <td class="tdColInput">
                                            <asp:DropDownList ID="ddlUsedStatus" runat="server">
                                                <asp:ListItem Value="0" Text="停用"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdColTitle">备注</td>
                                        <td class="tdColInput">
                                            <asp:TextBox ID="txtRemark" runat="server" Width="95%" MaxLength="50" CssClass="tdinput"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="2" height="10"></td></tr>
                    </table>
                <!-- </div> -->
                </td>
            </tr>
        </table>    
    </div>
    
</form>
</body>
</html>
