<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryEmployeeStructureSet.aspx.cs"
    Inherits="Pages_Office_HumanManager_SalaryEmployeeStructureSet" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员薪资结构设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/office/HumanManager/SalaryEmployeeStructureSet.js" type="text/javascript"></script>

    <style type="text/css">
        #tblMain
        {
            margin-top: 0px;
            margin-left: 0px;
            background-color: #F0f0f0;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable"
            id="tblMain">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="30" align="center" colspan="2" class="Title">
                    人员薪资结构设置
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="30" class="tdColInput">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <%--<img src="../../../images/Button/Main_btn_add.jpg" alt="添加" id="btnAdd" visible="true"
                                                style="cursor: hand" onclick="DoAdd();" runat="server" />--%>
                                            <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false"
                                                alt="保存" id="btnSave" style="cursor: hand" onclick="DoSave();" />
                                            <%--<img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="true" id="btnDelete"
                                                runat="server" onclick="DoDelete()" style='cursor: hand;' />--%>
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
                <td>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td align="left" width="50%" valign="top">
                                <table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td height="20" bgcolor="#E6E6E6" class="Blue">
                                            人员选择
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#F4F0ED" valign="top" height="50px">
                                            <div style="overflow-x: hidden; overflow-y: auto; height: 500px">
                                                <asp:TreeView ID="tvUserTree" runat="server" ShowLines="True" ExpandDepth="2">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" valign="top" bgcolor="#F4F0ED">
                                <table id="tbchk" width="100%" border="0" align="center" cellpadding="1" cellspacing="1"
                                    bgcolor="#F4F0ED">
                                    <tr>
                                        <td align="right" width="30%">
                                            <input type="checkbox" id="ckAll" onclick="SelectAll()" />姓名:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbName" runat="server" Text=""></asp:Label>
                                            <input type="hidden" id="hidUserID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <input type="checkbox" id="chCom" onclick="ShowSpan('chCom','spanCom')" />
                                        </td>
                                        <td align="left">
                                            享有公司业务提成&nbsp;&nbsp;<span id="spanCom" style="display: none">个人提成率(%)<span class="redbold">*</span><input
                                                type="text" id="txtComRate" style="width: 100px; border-width: 1pt; background-color: #ffffff;"
                                                onchange="Number_round(this,4);" onkeydown="Numeric_OnKeyDown();" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckDep" onclick="ShowSpan('ckDep','spanDep')" />
                                        </td>
                                        <td align="left">
                                            享有部门业务提成&nbsp;&nbsp;<span id="spanDep" style="display: none">个人提成率(%)<span class="redbold">*</span><input
                                                type="text" id="txtDepRate" style="width: 100px; border-width: 1pt; background-color: #ffffff;"
                                                onchange="Number_round(this,4);" onkeydown="Numeric_OnKeyDown();" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckFix" />
                                        </td>
                                        <td align="left">
                                            享有固定工资
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckQute" />
                                        </td>
                                        <td align="left">
                                            享有岗位工资
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckPerforman" />
                                        </td>
                                        <td align="left">
                                            享有绩效
                                        </td>
                                    </tr>
                                       <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckInsur" />
                                        </td>
                                        <td align="left">
                                            享有社会保险
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckPerIn" />
                                        </td>
                                        <td align="left">
                                            享有个人所得税
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckPerRoy" />
                                        </td>
                                        <td align="left">
                                            享有个人业务提成
                                        </td>
                                    </tr>
                                       <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckPiece" />
                                        </td>
                                        <td align="left">
                                            享有计件工资
                                        </td>
                                    </tr>
                                    
                                        <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckTime" />
                                        </td>
                                        <td align="left">
                                            享有计时工资
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <input type="checkbox" id="ckPro" />
                                        </td>
                                        <td align="left">
                                            享有产品单品提成
                                        </td>
                                    </tr>
                                    
                                 
                                 
                                  
                                  
                                
                                   
                                  
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
