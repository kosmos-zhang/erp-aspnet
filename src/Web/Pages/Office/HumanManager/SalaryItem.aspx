<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryItem.aspx.cs" Inherits="Pages_Office_HumanManager_SalaryItem" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
 
 
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工资项设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/SalaryItem.js" type="text/javascript"></script>
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
        #divtop
        {
          vertical-align :top;
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
        <td height="30" align="center" colspan="2" class="Title">
            
            固定工资项设置</td>
    </tr>
    <tr>

        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" >
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                <img src="../../../images/Button/Main_btn_add.jpg"  alt="添加" id="btnAdd" visible="false" style="cursor:hand" onclick="DoAdd();"   runat="server"/>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSave();"/>
                                    
                                    <img src="../../../images/Button/Bottom_btn_up.jpg"   alt="上移" visible="false" id="btnUp" style="cursor:hand" onclick="DoChangeRow('up');"  runat="server"/>
                                    <img src="../../../images/Button/Bottom_btn_down.jpg"   alt="下移" visible="false" id="btnDown" style="cursor:hand" onclick="DoChangeRow('down');"  runat="server"/>
                                    <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:hand;'   />
<%--    <input   id="Button1" type="button" value="button" onclick="gggg()" />--%>
                                </td>
                                <td align="right" class="tdColInput">
                                    <%--<img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand"   />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
    <td  valign="top" align="left">
    <div  style="overflow-y:auto;width:100%; line-height:14pt;letter-spacing:0.2em;height:400px;  overflow-x:auto; vertical-align :top"> 
    <table width="99%"   border="0" cellpadding="0" cellspacing="0" id="tblmain" style="height :500px; vertical-align:top">
        <tr>
            <td colspan="2" valign="top">
                <div id="divSalaryList" runat="server"  ></div>
            </td>
        </tr>
        <tr><td colspan="2" height="10"></td></tr>
    </table>
  </div>
    
</td>
</tr>
</table>
 
 <div id="divEditCheckItem" runat="server" style="background: #fff; padding: 10px; width: 800px; z-index:900; position: absolute;top: 20%; left: 25%;  display:none    ">    
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblDepttemInfo">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
                        <tr><td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                            <tr>
                                <td height="30" class="tdColInput">
                                    <img src="../../../Images/Button/Bottom_btn_confirm.jpg" runat="server" visible="true" alt="保存" id="Img1" style="cursor:hand"   onclick="DoCheck();"/>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand"   onclick="DoBack();"/>
                                </td>
                                <td height="30" class="tdColInput" align="right">
                                   <%-- <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" visible="false" id="btnPrint" style="cursor:hand" height="25" />--%>
                                </td>
                            </tr>
                        </table></td></tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- <div style="height:500px;overflow-y:scroll;"> -->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1"  >
                        <tr>
                            <td  colspan="2">
                                <table>
                                    <tr>
                                        <td colspan="2" height="4">
                                            <input type="hidden" id="hidEditFlag" runat="server" />
                                            <input type="hidden" id="hidItemNo" runat="server" />
                                                <input type="hidden" id="hidRowNo" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                                    <tr>
                                    <td height="20"   class="tdColTitle"  style="width:6%" > 公式说明 </td>
                                         <td height="50" class="tdColInput"  colspan='5'>
                                          1>公式参数:组成{}，如此项薪资由基础工资(固定工资项编号23)-缺勤工资(固定工资项编号25)。则计算公式为{23}-{25}即可.<br />
                                          2>支持运算符:加号(+)、减号(-)、乘号(*)、除号(/)、括号(())、中括号([]).<br />
                                          3>特定参数：出勤率（{@}） <br />
                                          4>此处大括号({})仅限用于设置参数，如用于参加运算，结果错误，请自行负责。<br />
                                          5>举例:计算出勤工资={2}*{@}含义为：固定工资*出勤率<br />
                                          6>编辑计算公式，请点击保存按钮，以保护您的新添加数据<br />
                                          7>保存后，请在工资录入->固定工资录入页面录入数据，点击保存，才可正确生成报表，谢谢您的使用。<br />
                                         </td>
                                  
                                        
                                    </tr>
                                   
                                    
                                    
                                   
                                    <tr> 
                                    <td height="20" align="right" class="tdColTitle" width="1%">公式<span class="redbold">*</span></td>
                                        <td height="50" class="tdColInput"  colspan='5'>
                                            <input  type="text"  id="inCaculator1" style ="height:100%; width:100%" runat="server" />
                                        
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
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<span id="spanMsg" class="errorMsg"  style="z-index:1005"></span>
<%--<uc1:Message ID="msgError" runat="server" />--%>
</form>
</body>
</html>