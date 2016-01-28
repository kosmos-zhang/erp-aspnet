<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputInsuEmployee.aspx.cs" Inherits="Pages_Office_HumanManager_InputInsuEmployee" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>社会保险录入</title>
   <%-- <link rel="stylesheet" type="text/css" href="../../../css/default.css" />--%>
    <link rel="stylesheet" type="text/css" href="../../../css/HumanManager.css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/InputInsuEmployee.js" type="text/javascript"></script>
    <style type="text/css">
        #tblMain
        {
            margin-top:0px;
            margin-left:0px;
		    background-color:#F0f0f0;
      	    font-family:tahoma;
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
<table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblMain">
    <tr>
        <td valign="top" colspan="2">
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
                                <td width="10%" height="20" class="tdColTitle">员工编号</td>
                                <td width="23%" class="tdColInput">
                                    <asp:TextBox ID="txtEmployeeNo" Width="95%" CssClass="tdinput" runat="server"  SpecialWorkCheck="员工编号"></asp:TextBox>
                                </td>
                                <td width="10%" class="tdColTitle">员工姓名</td>
                                <td width="23%" class="tdColInput">
                                    <asp:TextBox ID="txtEmployeeName" Width="95%" CssClass="tdinput" runat="server" SpecialWorkCheck="员工姓名"></asp:TextBox>
                                </td>
                                <td width="10%" class="tdColTitle">所在岗位</td>
                                <td width="24%" class="tdColInput">
                                    <asp:DropDownList ID="ddlQuarter" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdColTitle">保险名称</td>
                                <td class="tdColInput">
                                    <asp:TextBox ID="txtInsuranceName" MaxLength="50" Width="95%" CssClass="tdinput" runat="server" SpecialWorkCheck="保险名称"></asp:TextBox>
                                </td>
                                <td class="tdColTitle"></td>
                                <td class="tdColInput"></td>
                                <td class="tdColTitle"></td>
                                <td class="tdColInput"></td>
                            </tr>                   
                            <tr>
                                <td colspan="6" align="center" bgcolor="#FFFFFF">
                                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='DoSearch()'   />
                                    <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false"  style='cursor:pointer;' onclick="ClearInput()" width="52" height="23" />--%>
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
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                 
                    <tr>
                        <td height="20" colspan="2" align="center" valign="top" class="Title">社会保险录入</td>
                    </tr>
                    </table></td></tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSave();"/>
                                    <span class="Blue" style="height:20px; font-size:15px">缴费基数: </span>
                                    <input id="txtNum"   type="text"  onchange="Number_round(this,2)" />
                                    <span class="Blue" style="height:20px; font-size:15px">参保时间:</span><asp:TextBox 
                                        ID="txtPStartDate" runat="server"  
                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})" 
                                        ReadOnly="true" Width="12%"></asp:TextBox>
                                 <%--   <input id="" runat="server" visible="false" onclick="insertData();" type="button" value="批量录入" />--%>
                                      <img src="../../../Images/Button/btn_plsz.jpg" runat="server" visible="false" alt="批量录入" id="btnInput" style="cursor:hand"  onclick="insertData();"/>
                                    </td>
                                <td align="right" class="tdColInput">
                             <%--       <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand"   />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" valign="top">
<div style="overflow-y:scroll;width:100%; line-height:14pt;letter-spacing:0.2em;height:400px;  overflow-x:scroll; ">
    <table    border="0"  cellpadding="0" cellspacing="0" id="tblDetail" style="height :800px; vertical-align:top">
        <tr>
            <td style="width:150%"  valign="top">
                <div id="divInsuDetail"  runat="server"  style="padding-bottom :10px; padding-top:0px"></div>
            </td>
        </tr>
        <tr>
            <td height="10"></td>
        </tr>
    </table>
   </div> 
    </td></tr>
</table>
<div id="popupContent"></div>
<uc1:Message ID="msgError" runat="server" />

<span id="Forms" class="Spantype2"></span>
<span id="spanMsg" class="errorMsg"></span>

</form>
</body>
</html>