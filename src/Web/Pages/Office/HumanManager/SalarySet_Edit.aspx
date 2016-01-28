<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalarySet_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_SalarySet_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工资项设置</title>

    <script src="../../../js/office/HumanManager/SalarySet_Edit.js" type="text/javascript"></script>

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
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('10')">人员薪资项设置</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('1')">固定工资项设置</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('2')">浮动工资设置</a>&nbsp;
                        </td>
                        <%--<td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('3')">计时工资设置</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('4')">提成工资设置</a>&nbsp;
                        </td>--%>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('5')">社会保险设置</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('7')">个人所得税设置</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('6')">岗位工资设置</a>&nbsp;
                        </td>
                        <%--<td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('8')">公司提成设置</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('9')">绩效工资设置</a>&nbsp;
                        </td>
                        <td>
                            &nbsp;<a href="#" style="text-decoration: none" onclick="ChangePage('12')">考核基数设置</a>&nbsp;
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
                        src="SalaryEmployeeStructureSet.aspx?ModuleID=2011701"></iframe>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
