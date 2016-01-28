<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputSalary.aspx.cs" Inherits="Pages_Office_HumanManager_InputSalary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工资录入</title>

    <script src="../../../js/office/HumanManager/InputSalary.js" type="text/javascript"></script>

    <style type="text/css">
        #tblMain
        {
            margin-top: 5px;
            margin-left: 5px;
            background-color: #F0f0f0;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
        }
        .settable
        {
            filter: progid:dximagetransform.microsoft.dropshadow(color=#000000,offx=2,offy=3,positive=true);
        }
        body
        {
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
            line-height: 120%;
            text-decoration: none;
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            background-color: #666666;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="settable1" id="tblMain">
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none; color :Blue" onclick="ChangePage('1')">固定工资项录入</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none; color :Blue" onclick="ChangePage('2')">浮动工资项录入</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none; color :Blue" onclick="ChangePage('3')">社会保险录入</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none; color :Blue" onclick="ChangePage('4')">个人所得税录入</a>&nbsp;
                        </td>
                        <%-- <td>
                        &nbsp;<a href="#" style="text-decoration:none" onclick="ChangePage('5')">部门提成工资录入</a>&nbsp;
                    </td>--%>
                        <%--<td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('11')">公司提成录入</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('13')">绩效工资录入</a>&nbsp;
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" style="background-color: #F0f0f0;">
                <div id="divIFrame">
                    <iframe width="100%" frameborder="0" scrolling="no" id="salaryPage" onload="ReSetIFrameHeight();"
                        src="InputSalaryFixed.aspx?ModuleID=2011702"></iframe>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
