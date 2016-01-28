<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Common_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
   
    <script src="../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 78px;
        }
        .style2
        {
            width: 159px;
        }
        .style3
        {
            width: 93px;
        }
        #Text1
        {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">


    <table style="width:100%;">
        <tr>
            <td class="style1" >
     <div  onclick="alert_div('james');" style="width: 74px; text-align:right"> <a href="#">选择人员</a></div> 
            </td>
            <td class="style2">
                <input id="UserText1" type="text" onclick="alertdiv('UserText1,Text5');"  /></td>
            <td class="style3">
             <div  onclick="alert_div();" style="width: 74px"> <a href="#">选择部门</a></div> 
                
                </td>
            <td>
                <input id="DeptText3" type="text"  onclick="alertdiv('DeptText3,Text6');"  /></td>
        </tr>
        <tr>
                     <td class="style1">
     <div  onclick="alert_div('123');" style="width: 74px; height: 16px;"> <a href="#">选择人员</a></div> 
            </td>
            
                   <input id="Text2" type="text"  /></td>
            </td>
            <td class="style3">
                <input id="UserText4" type="text" onclick="alertdiv('UserText4,Text4');"  /></td>
            <td>
                选择部门</td>
                         <td>
                             <input id="Depta" type="text" onclick="alertdiv('Depta,Text1,2');"  /></td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:HiddenField ID="txtHiddenFieldID" runat="server" />
    <p>
        <input id="txtCpntrolID" type="text" value="User|Text1,Dept|Text3" /></p>
    <p>
        &nbsp;</p>
    <p>
        <input id="Button1" type="button" value="button" onclick="a();" /></p>
    </form>
    <p>
        <input id="Text1" type="text" />
        <input id="Text4" type="text" /><input id="Text6" type="text" /></p>
    <p>
        <input id="Text7" type="text" /></p>
    <p>
 
        <input id="Text5" type="text"   onclick="alertHidenDiv();" />
       <input type="radio" name="fruit" value = "Apple">苹果<br>
            <input type="radio" name="fruit" value = "Apple">a<br>
        
        </p>
    <p>
        <input id="Button2" type="button" value="button" onclick="AlertMsg();" /></p>
        
        <div style=""></div>
</body>
</html>
